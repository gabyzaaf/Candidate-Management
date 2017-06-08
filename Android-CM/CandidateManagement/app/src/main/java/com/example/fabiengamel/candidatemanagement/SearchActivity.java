package com.example.fabiengamel.candidatemanagement;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
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
        String url ="http://localhost:5000/api/User/Candidates/recherche/" + nom +"/"+user.sessionId ;

        JsonArrayRequest getCandidatesRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche action : " ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());

                            tvResult.append("coucou");
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
    }
}
