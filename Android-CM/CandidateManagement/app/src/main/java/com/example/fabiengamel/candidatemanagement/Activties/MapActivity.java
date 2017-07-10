package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v4.app.FragmentActivity;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
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

public class MapActivity extends AppCompatActivity implements OnMapReadyCallback {

    private GoogleMap mMap;
    EditText etNom;
    Button bSearch;
    public String nom = "";
    String town;
    String nameRetour;
    String action;
    String prenom;
    User user;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        InitContent();

        String candidateName;
        String candidateFirstname;
        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                candidateName= null;
                candidateFirstname = null;
            } else {
                candidateName= extras.getString("candidateName");
                candidateFirstname = extras.getString("candidateFirstname");
                GetCandidateZipCode(candidateName);
            }
        } else {
            candidateName= (String) savedInstanceState.getSerializable("candidateName");
            candidateFirstname= (String) savedInstanceState.getSerializable("candidateFirstname");
        }
        prenom = candidateFirstname;
        nameRetour = candidateName;
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


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                Intent i = new Intent(MapActivity.this, SearchActivity.class);
                i.putExtra("candidateName", etNom.getText().toString());
                startActivity(i);
                finish();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        return true;
    }

    @Override
    public void onBackPressed() {
        Intent i = new Intent(MapActivity.this, SearchActivity.class);
        if(!nameRetour.matches("")) {
            i.putExtra("candidateName", nameRetour);
            startActivity(i);
        }
        else{
            i.putExtra("candidateName", etNom.getText().toString());
            startActivity(i);
        }
    }

    public void addMarker(Double lat, Double lng){
        LatLng latLng = new LatLng(lat,lng);
        Candidate candidate = Candidate.getCurrentCandidate();

        mMap.addMarker(new MarkerOptions()
                .position(latLng)
                .title(candidate.getFirstname() + " " + candidate.getLastname())
                .snippet(candidate.getPhone()+" "+candidate.getZipcode()+" "+town));
        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(latLng, 11));
    }

    String name;
    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;

        mMap.setOnInfoWindowClickListener(new GoogleMap.OnInfoWindowClickListener() {
            @Override
            public void onInfoWindowClick(final Marker arg0) {
                    String[] parts = arg0.getTitle().split(" ");
                    name = parts[parts.length-1];
                    AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this);
                    builder.setMessage("Accéder à la fiche candidat");
                    builder.setPositiveButton("Oui",
                            new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog,
                                                    int which) {
                                    Intent i = new Intent(MapActivity.this, SearchActivity.class);
                                    i.putExtra("candidateName", name);
                                    startActivity(i);
                                }
                            });
                    builder.setNegativeButton("Annuler",
                            new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog,
                                                    int which) {
                                    dialog.dismiss();
                                }
                            });
                    AlertDialog alert = builder.create();
                    alert.show();
            }
        });
    }


    public void GetCandidateZipCode(String nom) {
        user = User.getCurrentUser();
        final Candidate candidate = Candidate.getCurrentCandidate();
        final String[] firstname = {""};

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/" +nom+"/"+user.getSessionId();

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche map :" ;

                    @Override
                    public void onResponse(final JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                                JSONObject jsonOBject = response.getJSONObject(0);
                            //if there is an error
                                if(jsonOBject.has("success"))
                                {
                                    Toast.makeText(MapActivity.this, "Erreur : " + jsonOBject.getString("content"), Toast.LENGTH_LONG).show();
                                }
                                //IF not
                                else {
                                    if(response.length() > 1){
                                        //more than one candidat found, need to choice just one by his firstname
                                        final EditText edittext = new EditText(MapActivity.this);
                                        //Ask for his firstname
                                        AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this, R.style.MyDialogTheme);
                                        builder.setTitle("Nom similaire");
                                        builder.setMessage("Plusieurs candidats ont le même nom, veuillez préciser le prénom :");
                                        builder.setView(edittext);
                                        builder.setPositiveButton("Localiser !",
                                                new DialogInterface.OnClickListener() {
                                                    @Override
                                                    public void onClick(DialogInterface dialog, int which) {
                                                        firstname[0] = edittext.getText().toString();
                                                        Boolean found = false;
                                                        outerloop:
                                                        try {
                                                            //searching for the right candidat
                                                            for (int i = 0; i < response.length(); i++) {
                                                                JSONObject jsonOBject = response.getJSONObject(i);
                                                                if (jsonOBject.getString("prenom").matches(firstname[0]) && found == false) {
                                                                    found = true;
                                                                    candidate.setZipcode(jsonOBject.getString("zipcode"));
                                                                    candidate.setFirstname(jsonOBject.getString("prenom"));
                                                                    candidate.setLastname(jsonOBject.getString("nom"));
                                                                    candidate.setPhone(jsonOBject.getString("phone"));
                                                                    candidate.setLien(jsonOBject.getString("lien"));
                                                                    candidate.setActions(jsonOBject.getString("actions"));

                                                                    Candidate.setCurrentCandidate(candidate);

                                                                    if(!candidate.getZipcode().matches("")) {
                                                                        GetCandidatePosition(candidate.getZipcode());
                                                                    } else {
                                                                        Toast.makeText(MapActivity.this, "Code postal non renseigné pour ce candidat", Toast.LENGTH_LONG).show();
                                                                    }
                                                                    break outerloop;
                                                                }
                                                            }
                                                            //If it doesn't exist : try with another or dismiss
                                                            if(!found){
                                                                AlertDialog.Builder dialogFalse = new AlertDialog.Builder(MapActivity.this, R.style.MyDialogTheme);
                                                                dialogFalse.setMessage("Le candidat "+firstname[0]+" "+etNom.getText().toString()+" n'existe pas");
                                                                dialogFalse.setNeutralButton("Réessayer",
                                                                        new DialogInterface.OnClickListener() {
                                                                            @Override
                                                                            public void onClick(DialogInterface dialog, int which) {
                                                                                GetCandidateZipCode(etNom.getText().toString());
                                                                            }
                                                                        });
                                                                dialogFalse.setNegativeButton("Annuler",
                                                                        new DialogInterface.OnClickListener() {
                                                                            @Override
                                                                            public void onClick(DialogInterface dialog, int which) {
                                                                                dialog.dismiss();
                                                                            }
                                                                        });
                                                                AlertDialog alertFalse = dialogFalse.create();
                                                                alertFalse.show();
                                                            }
                                                        }
                                                        catch(JSONException e){
                                                            Toast.makeText(MapActivity.this, "Erreur : " +e, Toast.LENGTH_LONG).show();
                                                            e.printStackTrace();
                                                        }
                                                    }
                                                });
                                        AlertDialog alert = builder.create();
                                        alert.show();
                                    }
                                    //Only one candidat found with this name
                                    else if (response.length() == 1) {
                                        candidate.setZipcode(jsonOBject.getString("zipcode"));
                                        candidate.setFirstname(jsonOBject.getString("prenom"));
                                        candidate.setLastname(jsonOBject.getString("nom"));
                                        candidate.setPhone(jsonOBject.getString("phone"));
                                        candidate.setLien(jsonOBject.getString("lien"));
                                        candidate.setActions(jsonOBject.getString("actions"));

                                        Candidate.setCurrentCandidate(candidate);

                                        if(!candidate.getZipcode().matches("")) {
                                            GetCandidatePosition(candidate.getZipcode());
                                        } else {
                                            Toast.makeText(MapActivity.this, "Code postal non renseigné pour ce candidat", Toast.LENGTH_LONG).show();
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
        String url ="http://maps.googleapis.com/maps/api/geocode/json?address="+zipcode+"&sensor=false&components=country:FR" ;

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
                            JSONArray components = item.getJSONArray("address_components");
                            JSONObject itemcompo = components.getJSONObject(1);
                            town = itemcompo.getString("long_name");
                            JSONObject location = item.getJSONObject("geometry").getJSONObject("location");

                            Double lat = location.getDouble("lat");
                            Double lng = location.getDouble("lng");

                            if(lat != null || lng != null) {
                                Toast.makeText(MapActivity.this, town, Toast.LENGTH_LONG).show();

                                addMarker(lat, lng);
                            }else {
                                Toast.makeText(MapActivity.this, "La position n'a pas été trouvée", Toast.LENGTH_LONG).show();
                            }

                        }catch(JSONException e){
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

    public void getCandidatePositionByAction(){

        user = User.getCurrentUser();
        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/candidate/actions/" + action +"/"+user.getSessionId() ;

        JsonArrayRequest getCandidatesRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche action : " ;
                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        String errorToken = "Aucun token ayant ce numero "+user.getSessionId()+" existe veuillez vous identifier";
                        try {
                            for(int i=0; i<response.length();i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                Log.d(TAG, "GET ACTION " + jsonOBject.toString()) ;

                                if(jsonOBject.has("content"))
                                {
                                    if(jsonOBject.getString("content").matches(errorToken)){
                                        AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this, R.style.MyDialogTheme);
                                        builder.setMessage("Veuillez vous reconnecter");
                                        builder.setPositiveButton("Ok",
                                                new DialogInterface.OnClickListener() {
                                                    @Override
                                                    public void onClick(DialogInterface dialog, int which) {
                                                        startActivity(new Intent(MapActivity.this, LoginActivity.class));
                                                    }
                                                });
                                        AlertDialog alert = builder.create();
                                        alert.show();

                                    }
                                    else{
                                        AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this);
                                        builder.setMessage("Erreur : " +jsonOBject.getString("content"))
                                                .setNeutralButton("Réessayer", null)
                                                .create()
                                                .show();
                                    }
                                }
                                else {
                                    if(!jsonOBject.getString("email").matches(""))
                                    {
                                        //addmarkerposition for each ones
                                        Candidate candidate = Candidate.getCurrentCandidate();
                                        candidate.setFirstname(jsonOBject.getString("prenom"));
                                        candidate.setLastname(jsonOBject.getString("nom"));
                                        candidate.setEmail(jsonOBject.getString("email"));
                                        candidate.setZipcode(jsonOBject.getString("zipcode"));

                                    }
                                }
                            }
                        } catch (JSONException e) {
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
                        AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this);
                        builder.setMessage("Erreur : " +error.toString())
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
        queue.add(getCandidatesRequest);
    }

    @Override
    protected void onRestart(){
        super.onRestart();
        AlertDialog.Builder builder = new AlertDialog.Builder(MapActivity.this, R.style.MyDialogTheme);
        builder.setMessage("Veuillez vous reconnecter");
        builder.setPositiveButton("Ok",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        startActivity(new Intent(MapActivity.this, LoginActivity.class));
                    }
                });
        AlertDialog alert = builder.create();
        alert.show();
    }
}


