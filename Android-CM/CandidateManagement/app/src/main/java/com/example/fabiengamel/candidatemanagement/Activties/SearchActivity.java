package com.example.fabiengamel.candidatemanagement.Activties;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

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

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class SearchActivity extends AppCompatActivity {

    EditText etNom;
    Button bRecherche;
    TextView tvResult;
    Button bModify;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        etNom = (EditText)findViewById(R.id.etName);
        bRecherche = (Button)findViewById(R.id.bSearchSingle);
        bModify = (Button)findViewById(R.id.bModify);
        tvResult = (TextView)findViewById(R.id.tvSingleCand);
        tvResult.setMovementMethod(new ScrollingMovementMethod());

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
                SearchCandidate();
            }
        });
    }

    public void SearchCandidate() {
        User user = User.getCurrentUser();
        final Candidate candidate = new Candidate();
        final Meeting report = new Meeting();

        String nom = etNom.getText().toString();
        RequestQueue queue = Volley.newRequestQueue(this);
        String url ="http://192.168.1.17:5000/api/user/Candidates/recherche/" +nom+"/"+user.sessionId ;

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche action : " ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                            for (int i = 0; i < response.length(); i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                    if(jsonOBject.has("success"))
                                    {
                                        tvResult.setText("Erreur : " + jsonOBject.getString("content"));
                                    }
                                else {
                                        candidate.firstname = jsonOBject.getString("prenom");
                                        candidate.lastname = jsonOBject.getString("nom");
                                        candidate.phone =  jsonOBject.getString("phone");
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

                                        tvResult.setText("Informations sur le candidat: ");

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
                                        tvResult.append("N°Phone : " + candidate.phone );
                                        tvResult.append("\n");
                                        tvResult.append("Année : " + candidate.annee);
                                        tvResult.append("\n");
                                        tvResult.append("Lien : " + candidate.lien );
                                        tvResult.append("\n");
                                        tvResult.append("Action : " + candidate.actions);
                                        tvResult.append("\n");
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
                                        tvResult.append("NationalityNote : " +report.nationalityNote );
                                        tvResult.append("\n");
                                        tvResult.append("Competences : " + report.competences);
                                        bModify.setVisibility(View.VISIBLE);
                                    }

                                }
                            }catch(JSONException e){
                                tvResult.append("Erreur de lecture : ");
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
}
