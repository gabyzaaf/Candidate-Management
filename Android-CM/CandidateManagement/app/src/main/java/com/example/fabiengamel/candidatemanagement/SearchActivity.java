package com.example.fabiengamel.candidatemanagement;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.Candidate;
import com.example.fabiengamel.candidatemanagement.Models.User;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class SearchActivity extends AppCompatActivity {

    EditText etNom;
    Button bRecherche;
    TextView tvResult;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        etNom = (EditText)findViewById(R.id.etName);
        bRecherche = (Button)findViewById(R.id.bSearchSingle);
        tvResult = (TextView)findViewById(R.id.tvSingleCand);
        tvResult.setMovementMethod(new ScrollingMovementMethod());

        bRecherche.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SearchCandidate();
            }
        });
    }

    public void SearchCandidate() {
        User user = new User();
        user = User.getCurrentUser();

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
                                        tvResult.setText("Informations sur le candidat: ");
                                        tvResult.append("\n");
                                        tvResult.append("\n");
                                        tvResult.append("Nom    : " + jsonOBject.getString("nom"));
                                        tvResult.append("\n");
                                        tvResult.append("Prénom : " + jsonOBject.getString("prenom"));
                                        tvResult.append("\n");
                                        tvResult.append("N°Phone: " + jsonOBject.getString("phone"));
                                        tvResult.append("\n");
                                        tvResult.append(" Lien  : " + jsonOBject.getString("lien"));
                                        tvResult.append("\n");
                                        tvResult.append("Action : " + jsonOBject.getString("actions"));
                                        tvResult.append("\n");
                                        tvResult.append("crCall : " + jsonOBject.getString("crCall"));
                                        tvResult.append("\n");
                                        tvResult.append("\n");
                                        tvResult.append("Entretien du candidat : ");
                                        tvResult.append("\n");
                                        tvResult.append("\n");
                                        tvResult.append("Note            : " + jsonOBject.getString("note"));
                                        tvResult.append("\n");
                                        tvResult.append("XpNote          : " + jsonOBject.getString("xpNote"));
                                        tvResult.append("\n");
                                        tvResult.append("PisteNote       : " + jsonOBject.getString("pisteNote"));
                                        tvResult.append("\n");
                                        tvResult.append("PieCouteNote    : " + jsonOBject.getString("pieCouteNote"));
                                        tvResult.append("\n");
                                        tvResult.append("LocationNote    : " + jsonOBject.getString("locationNote"));
                                        tvResult.append("\n");
                                        tvResult.append(" EnglishNote    : " + jsonOBject.getString("EnglishNote"));
                                        tvResult.append("\n");
                                        tvResult.append("NationalityNote : " + jsonOBject.getString("nationalityNote"));
                                        tvResult.append("\n");
                                        tvResult.append(" Competences    : " + jsonOBject.getString("competences"));
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
