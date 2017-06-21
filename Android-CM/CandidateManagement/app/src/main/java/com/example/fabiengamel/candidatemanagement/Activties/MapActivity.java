package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.FragmentActivity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.Candidate;
import com.example.fabiengamel.candidatemanagement.Models.Meeting;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Utils.APIConstants;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Dictionary;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class MapActivity extends FragmentActivity implements OnMapReadyCallback {

    private GoogleMap mMap;
    EditText etNom;
    Button bSearch;
    public String nom = "";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);



        InitContent();

        //Récupération du nom lors de l'activity recherche
        String candidateName;
        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                candidateName= null;
            } else {
                candidateName= extras.getString("candidateName");
                GetCandidateZipCode(candidateName);
            }
        } else {
            candidateName= (String) savedInstanceState.getSerializable("candidateName");
        }
        etNom.setText(candidateName);

    }

    public void InitContent() {

        bSearch = (Button)findViewById(R.id.bLocateMap);
        etNom = (EditText)findViewById(R.id.etNameMap);


        bSearch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                InputMethodManager inputManager = (InputMethodManager)
                        getSystemService(Context.INPUT_METHOD_SERVICE);

                inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(),
                        InputMethodManager.HIDE_NOT_ALWAYS);

                nom = etNom.getText().toString();
                if (!nom.matches("")) {
                    GetCandidateZipCode(nom);
                } else {
                    Toast.makeText(MapActivity.this, "Veuillez saisir un nom ", Toast.LENGTH_LONG).show();
                }

            }
        });
    }

    public void addMarker(Double lat, Double lng){
        LatLng latLng = new LatLng(lat, lng);
        Candidate candidate = Candidate.getCurrentCandidate();

        //mMap.clear();
        mMap.addMarker(new MarkerOptions()
                .position(latLng)
                .title(candidate.firstname + " " + candidate.lastname)
                .snippet(candidate.phone+" "+candidate.zipcode));


        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(latLng, 13));
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;

        mMap.setOnInfoWindowClickListener(new GoogleMap.OnInfoWindowClickListener() {
            @Override
            public void onInfoWindowClick(Marker arg0) {
                Candidate candidate = Candidate.getCurrentCandidate();
                if (arg0 != null && arg0.getTitle().equals(candidate.firstname + " " + candidate.lastname)) {
                    Intent intent = new Intent(MapActivity.this, SearchActivity.class);
                    intent.putExtra("candidateName", candidate.lastname);
                    startActivity(intent);
                }
            }
        });
    }

    public void GetCandidateZipCode(String nom) {
        User user = User.getCurrentUser();
        final Candidate candidate = new Candidate();

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/" +nom+"/"+user.sessionId ;

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche map :" ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                            for (int i = 0; i < response.length(); i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                if(jsonOBject.has("success"))
                                {
                                    Toast.makeText(MapActivity.this, "Erreur : " + jsonOBject.getString("content"), Toast.LENGTH_LONG).show();
                                }
                                else {
                                    //En attente des zipcodes en base
                                    //candidate.zipcode = jsonOBject.getString("zipcode");
                                    candidate.firstname = jsonOBject.getString("prenom");
                                    candidate.lastname = jsonOBject.getString("nom");
                                    candidate.phone =  jsonOBject.getString("phone");
                                    candidate.lien = jsonOBject.getString("lien");
                                    candidate.actions = jsonOBject.getString("actions");
                                    candidate.zipcode = "93100";
                                    Candidate.setCurrentCandidate(candidate);

                                    if(candidate.zipcode != null) {
                                        GetCandidatePosition(candidate.zipcode);
                                       // Toast.makeText(MapActivity.this, "zipcode : " +candidate.zipcode, Toast.LENGTH_LONG).show();

                                    } else {
                                        Toast.makeText(MapActivity.this, "Code postal inexistant pour ce candidat", Toast.LENGTH_LONG).show();
                                    }
                                }
                            }
                        }catch(JSONException e){
                            Toast.makeText(MapActivity.this, "Erreur : " +e, Toast.LENGTH_LONG).show();
                            e.printStackTrace();
                        }
                    }

                },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // TODO Auto-generated method stub
                        Log.d("ERROR", "error => " + error.toString());
                        Toast.makeText(MapActivity.this, "Erreur serveur: " + error.toString(), Toast.LENGTH_LONG).show();
                    }
                }
        ) {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> params = new HashMap<>();
                return params;
            }
        };
        queue.add(searchRequest);
    }

    public void GetCandidatePosition(String zipcode){

        RequestQueue queue = Volley.newRequestQueue(this);
        String url ="http://maps.googleapis.com/maps/api/geocode/json?address="+zipcode+"&sensor=false" ;

        JsonObjectRequest LatLngRequest = new JsonObjectRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONObject>()
                {
                    public static final String TAG ="Recherche map :" ;

                    @Override
                    public void onResponse(JSONObject response) {
                        Log.d("Response", response.toString());
                        try {

                            JSONArray  results = response.getJSONArray("results");
                            JSONObject item = results.getJSONObject(0);
                            JSONObject location = item.getJSONObject("geometry")
                                    .getJSONObject("location");

                            Double lat = location.getDouble("lat");
                            Double lng = location.getDouble("lng");

                            if(lat != null || lng != null) {
                               // Toast.makeText(MapActivity.this, "lat "+lat+" lng : "+lng, Toast.LENGTH_LONG).show();
                                addMarker(lat, lng);
                            }else {
                                Toast.makeText(MapActivity.this, "La position n'a pas été trouvée", Toast.LENGTH_LONG).show();
                            }

                        }catch(JSONException e){
                            //Toast.makeText(MapActivity.this, "Erreur : " +e, Toast.LENGTH_LONG).show();
                            AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this);
                            builder.setMessage("Erreur : " +e)
                                    .setNeutralButton("Réessayer", null)
                                    .create()
                                    .show();
                            e.printStackTrace();
                        }
                    }

                },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // TODO Auto-generated method stub
                        Log.d("ERROR", "error => " + error.toString());
                        //  Toast.makeText(MapActivity.this, "Erreur serveur: " + error.toString(), Toast.LENGTH_LONG).show();
                        AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this);
                        builder.setMessage("Erreur serveur: " + error.toString())
                                .setNeutralButton("Réessayer", null)
                                .create()
                                .show();
                    }
                }
        ) {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> params = new HashMap<>();
                return params;
            }
        };
        queue.add(LatLngRequest);


    }

}
