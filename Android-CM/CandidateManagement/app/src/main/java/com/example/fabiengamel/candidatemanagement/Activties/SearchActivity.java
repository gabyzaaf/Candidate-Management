package com.example.fabiengamel.candidatemanagement.Activties;

import android.Manifest;
import android.app.ActionBar;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.text.Editable;
import android.text.TextWatcher;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.Candidate;
import com.example.fabiengamel.candidatemanagement.Models.Meeting;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Utils.APIConstants;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;
import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.TimeUnit;


public class SearchActivity extends AppCompatActivity {

    EditText etNom;
    Button bRecherche;
    TextView tvResult;
    TextView tvInfo;
    Button bModify;
    Button bLocate;
    Button bSMS;
    Candidate candidate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        InitContent();

        String candidateName;
        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                candidateName= null;
                bModify.setVisibility(View.INVISIBLE);
                bLocate.setVisibility(View.INVISIBLE);
                bSMS.setVisibility(View.INVISIBLE);
                tvInfo.setVisibility(View.INVISIBLE);
            } else {
                candidateName= extras.getString("candidateName");
                getEmail(candidateName);
                SearchCandidate(candidateName);
            }
        } else {
            candidateName= (String) savedInstanceState.getSerializable("candidateName");
            bModify.setVisibility(View.INVISIBLE);
            bLocate.setVisibility(View.INVISIBLE);
            bSMS.setVisibility(View.INVISIBLE);
            tvInfo.setVisibility(View.INVISIBLE);
            tvResult.setVisibility(View.INVISIBLE);
        }
        etNom.setText(candidateName);
    }

    public void InitContent(){
        etNom = (EditText)findViewById(R.id.etName);
        bRecherche = (Button)findViewById(R.id.bSearchSingle);
        bModify = (Button)findViewById(R.id.bModify);
        tvResult = (TextView)findViewById(R.id.tvSingleCand);
        tvResult.setMovementMethod(new ScrollingMovementMethod());
        bLocate = (Button)findViewById(R.id.bLocateSearch);
        bSMS = (Button)findViewById(R.id.bSMS);
        tvInfo = (TextView)findViewById(R.id.textView2);


        bLocate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(SearchActivity.this, MapActivity.class);
                Candidate c = Candidate.getCurrentCandidate();
                i.putExtra("candidateName", c.lastname);
                startActivity(i);
            }
        });

        bModify.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(SearchActivity.this, UpdateActivity.class));
            }
        });

        bRecherche.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                InputMethodManager inputManager = (InputMethodManager)
                        getSystemService(Context.INPUT_METHOD_SERVICE);

                inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(),
                        InputMethodManager.HIDE_NOT_ALWAYS);

                tvResult.setText("");
                String nom = etNom.getText().toString();

                getEmail(nom);
                SearchCandidate(nom);
            }
        });

        bSMS.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(SearchActivity.this, SMSActivity.class);
                Candidate c = Candidate.getCurrentCandidate();
                i.putExtra("candidatePhone", c.phone);
                i.putExtra("candidateName", c.lastname);
                i.putExtra("candidateFirstname", c.firstname);
                i.putExtra("candidateAction", c.actions);
                startActivity(i);
            }
        });

        etNom.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {
            }

            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            public void onTextChanged(CharSequence s, int start, int before, int count) {
                bModify.setVisibility(View.INVISIBLE);
                bLocate.setVisibility(View.INVISIBLE);
                bSMS.setVisibility(View.INVISIBLE);
                tvInfo.setVisibility(View.INVISIBLE);
                tvResult.setVisibility(View.INVISIBLE);
            }
        });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                finish();
                return true;
        }

        return super.onOptionsItemSelected(item);
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        return true;
    }

    public void SearchCandidate(String nom) {
        User user = User.getCurrentUser();
        candidate = Candidate.getCurrentCandidate();
        final Meeting report = new Meeting();


        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/" +nom+"/"+user.sessionId ;

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche candidat : " ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                            for (int i = 0; i < response.length(); i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                    if(jsonOBject.has("success"))
                                    {
                                        tvInfo.setVisibility(View.VISIBLE);
                                        tvResult.setText("Erreur : " + jsonOBject.getString("content"));
                                    }
                                else {

                                        candidate.firstname = jsonOBject.getString("prenom");
                                        candidate.lastname = jsonOBject.getString("nom");
                                      //  candidate.email = mail;
                                        candidate.phone =  jsonOBject.getString("phone");
                                        candidate.zipcode = jsonOBject.getString("zipcode");
                                        candidate.lien = jsonOBject.getString("lien");
                                        candidate.actions = jsonOBject.getString("actions");
                                        candidate.sexe = jsonOBject.getString("sexe");
                                        candidate.crCall = jsonOBject.getString("crCall");
                                        candidate.annee = Integer.parseInt(jsonOBject.getString("annee"));
                                        candidate.approche_email = Boolean.valueOf(jsonOBject.getString("approche_email"));
                                        report.note = jsonOBject.getString("note");
                                        report.xpNote = jsonOBject.getString("xpNote");
                                        report.pisteNote = jsonOBject.getString("pisteNote");
                                        report.pieCouteNote = jsonOBject.getString("pieCouteNote");
                                        report.locationNote =jsonOBject.getString("locationNote");
                                        report.nationalityNote = jsonOBject.getString("nationalityNote");
                                        report.EnglishNote = jsonOBject.getString("EnglishNote");
                                        report.competences = jsonOBject.getString("competences");


                                        Candidate.setCurrentCandidate(candidate);
                                        Meeting.setCurrentMeeting(report);

                                        tvResult.append("\n");
                                        tvResult.append("\n");
                                        tvResult.append("Nom : " + candidate.lastname);
                                        tvResult.append("\n");
                                        tvResult.append("Prénom : " + candidate.firstname);
                                        tvResult.append("\n");
                                        tvResult.append("Sexe : " + candidate.sexe);
                                        tvResult.append("\n");
                                        tvResult.append("Email : " + candidate.email);
                                        tvResult.append("\n");
                                        tvResult.append("N°Phone : " + candidate.phone );
                                        tvResult.append("\n");
                                        tvResult.append("Code postal : " + candidate.zipcode );
                                        tvResult.append("\n");
                                        tvResult.append("Année : " + candidate.annee);
                                        tvResult.append("\n");
                                        tvResult.append("Lien : " + candidate.lien );
                                        tvResult.append("\n");
                                        tvResult.append("Action : " + candidate.actions);
                                        tvResult.append("\n");
                                        if(candidate.actions.matches("freelance")){
                                            tvResult.append("Prix : " + candidate.prix);
                                            tvResult.append("\n");
                                        }
                                        tvResult.append("crCall : " + candidate.crCall);
                                        tvResult.append("\n");
                                        tvResult.append("Approche_email : " + String.valueOf(candidate.approche_email));
                                        tvResult.append("\n");
                                        tvResult.append("\n");
                                        tvResult.append("Entretien du candidat : ");
                                        tvResult.append("\n");
                                        tvResult.append("\n");
                                        tvResult.append("Note : " + report.note);
                                        tvResult.append("\n");
                                        tvResult.append("XpNote : " + report.xpNote);
                                        tvResult.append("\n");
                                        tvResult.append("NSNote : " + report.nsNote);
                                        tvResult.append("\n");
                                        tvResult.append("JobIdealNote : " + report.jobIdealNote);
                                        tvResult.append("\n");
                                        tvResult.append("PisteNote : " + report.pisteNote);
                                        tvResult.append("\n");
                                        tvResult.append("PieCouteNote : " + report.pieCouteNote);
                                        tvResult.append("\n");
                                        tvResult.append("LocationNote : " + report.locationNote);
                                        tvResult.append("\n");
                                        tvResult.append("EnglishNote : " +report.EnglishNote );
                                        tvResult.append("\n");
                                        tvResult.append("NationalityNote : " + report.nationalityNote);
                                        tvResult.append("\n");
                                        tvResult.append("Competences : " + report.competences);
                                        showButton();
                                    }

                                }
                            }catch(JSONException e){
                                tvResult.append("Erreur de lecture : ");
                                tvResult.append("\n");
                                tvResult.append(""+e);
                                e.printStackTrace();
                            } catch (InterruptedException e) {
                            tvResult.append("Erreur système : ");
                            tvResult.append("\n");
                            tvResult.append(""+e);
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
                        tvResult.append("Une erreur serveur est survenue : " + error.toString());
                        tvResult.setVisibility(View.VISIBLE);
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

    public void getEmail(String nom){
        User user = User.getCurrentUser();

        final RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/mobile/" +nom+"/"+user.sessionId ;

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche candidat : " ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        try {

                                JSONObject jsonOBject = response.getJSONObject(0);
                                candidate = new Candidate();
                                candidate.email = jsonOBject.getString("email");
                                Candidate.setCurrentCandidate(candidate);

                        }catch(JSONException e){

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
                        tvResult.setVisibility(View.VISIBLE);
                        tvResult.append("Une erreur serveur est survenue : "+error.toString());
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

    public void showButton() throws InterruptedException {
        TimeUnit.SECONDS.sleep(2);
        bModify.setVisibility(View.VISIBLE);
        bLocate.setVisibility(View.VISIBLE);
        bSMS.setVisibility(View.VISIBLE);
        tvInfo.setVisibility(View.VISIBLE);
        tvResult.setVisibility(View.VISIBLE);
    }

}
