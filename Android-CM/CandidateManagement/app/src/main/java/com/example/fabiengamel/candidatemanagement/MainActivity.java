package com.example.fabiengamel.candidatemanagement;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.util.Log;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
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
import com.example.fabiengamel.candidatemanagement.Models.User;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    NavigationView navigationView;
    TextView tvCandidates;
    TextView tvWelcome;
    ImageView ivLogo;
    Button bMail;
    Spinner spActions;
    String action ="";
    List<Candidate> candidates;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
        tvWelcome = (EditText)findViewById(R.id.tvWelcome);
        tvCandidates = (EditText)findViewById(R.id.tvCandidates);
        ivLogo = (ImageView)findViewById(R.id.imageView);
        bMail = (Button)findViewById(R.id.bMail);
        spActions = (Spinner)findViewById(R.id.spActions);


        InitContent();
        setMenu();

        spActions.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
                action = spActions.getSelectedItem().toString();
                GetCandidatesByActions(action);
            }

            @Override
            public void onNothingSelected(AdapterView<?> arg0) {

            }
        });

        bMail.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SendMail();
            }
        });
    }

    private void setMenu() {

        MenuItem nav_search = (MenuItem)  navigationView.getMenu().findItem(R.id.nav_search);
        nav_search.setVisible(true);

        MenuItem nav_add = (MenuItem)  navigationView.getMenu().findItem(R.id.nav_add);
        nav_add.setVisible(true);

        MenuItem nav_calendar = (MenuItem)  navigationView.getMenu().findItem(R.id.nav_calendar);
        nav_calendar.setVisible(true);
    }

    private void InitContent() {
        User user = new User();
        user = User.getCurrentUser();

        tvWelcome.setText("Bonjour " + user.email);
        //tvCandidates.append("Voici la liste des candidats : ");
        //Init spinner
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.actions_array, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spActions.setAdapter(adapter);

        //créer fonction récupération des candidats à relancer
        //ajouter bouton d'envoi des mails
        //créer fonction envoi mail en fonction du statut d'envoi
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_search) {
            startActivity(new Intent(this, SearchActivity.class));
        } else if (id == R.id.nav_add) {
            startActivity(new Intent(this, AddActivity.class));
        } else if (id == R.id.nav_calendar) {
            startActivity(new Intent(this, AgendaActivity.class));
        }
        else if (id == R.id.nav_close) {
            startActivity(new Intent(this, LoginActivity.class));
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public void GetCandidatesByActions(String action) {

        User user = new User();
        user = User.getCurrentUser();
        candidates = new ArrayList<Candidate>();

        if(action.matches("aRelancerMail")){
            bMail.setVisibility(View.VISIBLE);
        }
        //requete get de recup
        RequestQueue queue = Volley.newRequestQueue(this);
        String url ="http://localhost:5000/api/User/Candidates/recherchebyaction/" + action +"/"+user.sessionId ;

        JsonArrayRequest getCandidatesRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche action : " ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                            for(int i=0; i<response.length();i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                Log.d(TAG, "json (" + i + ") = " + jsonOBject.toString()) ;

                                Candidate candidate = new Candidate();
                                candidate.firstname = jsonOBject.getString("Firstname");
                                candidate.lastname  = jsonOBject.getString("Name");
                                candidate.email = jsonOBject.getString("email");
                                //Instancier champs nécessaires

                                candidates.add(candidate);

                                tvCandidates.append(candidate.firstname+" "+candidate.lastname);
                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                            tvCandidates.append("Une erreur de lecture est survenue : " + e.toString());
                        }
                    }
                },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // TODO Auto-generated method stub
                        Log.d("ERROR", "error => " + error.toString());
                        tvCandidates.append("Une erreur serveur est survenue : "+error.toString());
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
    public void SendMail() {

        for (Candidate candidate : candidates) {
            //récupérer email
            //envoiemail
        }

        Toast.makeText(this, "Aucun mail à envoyer", Toast.LENGTH_LONG).show();
    }
}
