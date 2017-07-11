package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
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
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Utils.APIConstants;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    /*********************** Variables ***********************************************************/
    NavigationView navigationView;
    TextView tvCandidates;
    TextView tvWelcome;
    ImageView ivLogo;
    Spinner spActions;
    String action ="";
   // List<Candidate> candidates;
    User user;

    /**************************** On activity create *********************************************/
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

        InitContent();
    }

    /*************** Initialize content *************************************************/
    private void InitContent() {
        user = User.getCurrentUser();

        navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
        tvWelcome = (TextView)findViewById(R.id.tvWelcome);
        tvCandidates = (TextView)findViewById(R.id.tvCandidates);
        ivLogo = (ImageView)findViewById(R.id.imageView);
        spActions = (Spinner)findViewById(R.id.spActions);
        tvWelcome.setText("Bonjour " + user.getEmail());
        tvCandidates.setMovementMethod(new ScrollingMovementMethod());

        /************************** Initialize spinner *************************************************/

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.actions_array, R.layout.spinner_custom);
        adapter.setDropDownViewResource(R.layout.spiner_dropdown_custom);
        spActions.setAdapter(adapter);
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
    }

    /******************* On return (navigation bar) press ******************************/
    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            deconnect();
        }
    }

    /******************** Create action bar ***********************************/
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {return true;}
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        return super.onOptionsItemSelected(item);
    }

    /*********************** Initialize navigation menu ************************************************/
    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_search) {
            startActivity(new Intent(this, SearchActivity.class));
        } else if (id == R.id.nav_add) {
            startActivity(new Intent(this, AddActivity.class));
        }
        else if (id == R.id.nav_close) {
            deconnect();
        }
        else if (id == R.id.nav_map) {
            startActivity(new Intent(this, MapActivity.class));
        }
        else if (id == R.id.nav_salary) {
            startActivity(new Intent(this, PredictSalaryActivity.class));
        }
        else if (id == R.id.nav_satisfaction) {
            startActivity(new Intent(this, SatisfactionActivity.class));
        }
        else if (id == R.id.nav_time) {
            startActivity(new Intent(this, TimeActivity.class));
        }
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    /************************ Request server to have candidates by actions *******************************************/

    public void GetCandidatesByActions(String action) {
        //candidates = new ArrayList<Candidate>();
        final ProgressDialog dialog = ProgressDialog.show(MainActivity.this, "", "Chargement en cours...", true);
        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/candidate/actions/" +action+"/"+user.getSessionId();

        tvCandidates.setText("Liste des candidats "+action+"(s) :");
        tvCandidates.append("\n");
        tvCandidates.append("\n");

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
                                        if (dialog != null)
                                            dialog.cancel();
                                        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this, R.style.MyDialogTheme);
                                        builder.setMessage("Veuillez vous reconnecter");
                                        builder.setPositiveButton("Ok",
                                                new DialogInterface.OnClickListener() {
                                                    @Override
                                                    public void onClick(DialogInterface dialog, int which) {
                                                        startActivity(new Intent(MainActivity.this, LoginActivity.class));
                                                    }
                                                });
                                        AlertDialog alert = builder.create();
                                        alert.show();
                                    }
                                    else{
                                        if (dialog != null)
                                            dialog.cancel();
                                    tvCandidates.append(jsonOBject.getString("content"));
                                    }
                                }
                                else {
                                    Candidate candidate = Candidate.getCurrentCandidate();
                                    candidate.setFirstname(jsonOBject.getString("prenom"));
                                    candidate.setLastname(jsonOBject.getString("nom"));
                                    candidate.setEmail(jsonOBject.getString("email"));
                                    //candidates.add(candidate);
                                    tvCandidates.append(candidate.getFirstname() + " " + candidate.getLastname());
                                    tvCandidates.append("\n");
                                    tvCandidates.append(candidate.getEmail());
                                    tvCandidates.append("\n");
                                    tvCandidates.append("\n");
                                    if (dialog != null)
                                        dialog.cancel();
                                }
                            }
                        } catch (JSONException e) {
                            if (dialog != null)
                                dialog.cancel();
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
                        if (dialog != null)
                            dialog.cancel();
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

    /******************************** Deconnect user *********************************************************/
    public void deconnect() {
        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this, R.style.MyDialogTheme);
        builder.setMessage("Se déconnecter ?");
        builder.setPositiveButton("Oui",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog,
                                        int which) {
                        startActivity(new Intent(MainActivity.this, LoginActivity.class));
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

    /*********************** on Activity resume **********************************************/
    @Override
    protected void onResume(){
        super.onResume();
        user = User.getCurrentUser();
    }

}
