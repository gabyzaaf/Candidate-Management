package com.example.fabiengamel.candidatemanagement.Activties;

import android.Manifest;
import android.app.ActionBar;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
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
import android.view.Window;
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
    User user;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        InitContent();

        //Get the name if it's come from another activity
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

    public void InitContent() {
        etNom = (EditText)findViewById(R.id.etName);
        bRecherche = (Button)findViewById(R.id.bSearchSingle);
        bModify = (Button)findViewById(R.id.bModify);
        tvResult = (TextView)findViewById(R.id.tvSingleCand);
        tvResult.setMovementMethod(new ScrollingMovementMethod());
        bLocate = (Button)findViewById(R.id.bLocateSearch);
        bSMS = (Button)findViewById(R.id.bSMS);
        tvInfo = (TextView)findViewById(R.id.textView2);

        //Go to location activity
        bLocate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(SearchActivity.this, MapActivity.class);
                i.putExtra("candidateName", candidate.getLastname());
                startActivity(i);
            }
        });
        //Go to update activity
        bModify.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(SearchActivity.this, UpdateActivity.class));
            }
        });
        //Search candidate
        bRecherche.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //hide keyboard
                InputMethodManager inputManager = (InputMethodManager)
                        getSystemService(Context.INPUT_METHOD_SERVICE);
                inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(),
                        InputMethodManager.HIDE_NOT_ALWAYS);
                //Set textview result and get name in edittext
                tvResult.setText("");
                String nom = etNom.getText().toString();
                //Get candidate datas
                if(!nom.matches("")) {
                    getEmail(nom);
                }
                else {
                    AlertDialog.Builder builder = new AlertDialog.Builder(SearchActivity.this, R.style.MyDialogTheme);
                    builder.setMessage("Veuillez saisir un nom")
                            .setNegativeButton("OK", null)
                            .create()
                            .show();
                }
            }
        });
        //Go to SMS activity
        bSMS.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //Pass datas needed for sms
                Intent i = new Intent(SearchActivity.this, SMSActivity.class);
                i.putExtra("candidatePhone", candidate.getPhone());
                i.putExtra("candidateName", candidate.getLastname());
                i.putExtra("candidateFirstname", candidate.getFirstname());
                i.putExtra("candidateAction", candidate.getActions());
                startActivity(i);
            }
        });
        //hide buttons on text changed
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
    //When press back navigation button
    public void onBackPressed() {
        Intent i = new Intent(SearchActivity.this, MainActivity.class);
        startActivity(i);
    }
    //When press Action bar return button
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                Intent i = new Intent(SearchActivity.this, MainActivity.class);
                startActivity(i);
                finish();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        return true;
    }

    //Search candidate datas (after getting his email adress)
    public void SearchCandidate(String nom, final String prenom) {
        //Instance a new report
        final Meeting report = Meeting.getCurrentMeeting();
        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/" +nom+"/"+user.getSessionId() ;

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche candidat : " ;
                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        String errorToken = "Aucun token ayant ce numero "+user.getSessionId()+" existe veuillez vous identifier";
                        try {
                            for (int i = 0; i < response.length(); i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                    if(jsonOBject.has("success"))
                                    {
                                        if(jsonOBject.getString("content").matches(errorToken)){
                                            AlertDialog.Builder builder = new AlertDialog.Builder(SearchActivity.this, R.style.MyDialogTheme);
                                            builder.setMessage("Veuillez vous reconnecter");
                                            builder.setPositiveButton("Ok",
                                                    new DialogInterface.OnClickListener() {
                                                        @Override
                                                        public void onClick(DialogInterface dialog, int which) {
                                                            startActivity(new Intent(SearchActivity.this, LoginActivity.class));
                                                        }
                                                    });
                                            AlertDialog alert = builder.create();
                                            alert.show();

                                        }
                                        tvInfo.setVisibility(View.VISIBLE);
                                        tvResult.setText("Erreur : " + jsonOBject.getString("content"));
                                    }
                                else if(candidate != null) {
                                        if (jsonOBject.getString("prenom").matches(prenom) || prenom.matches("")) {
                                            candidate.setFirstname(jsonOBject.getString("prenom"));
                                            candidate.setLastname(jsonOBject.getString("nom"));
                                            candidate.setPhone(jsonOBject.getString("phone"));
                                            candidate.setZipcode(jsonOBject.getString("zipcode"));
                                            candidate.setLien(jsonOBject.getString("lien"));
                                            candidate.setActions(jsonOBject.getString("actions"));
                                            candidate.setSexe(jsonOBject.getString("sexe"));
                                            candidate.setCrCall(jsonOBject.getString("crCall"));
                                            candidate.setAnnee(Integer.parseInt(jsonOBject.getString("annee")));
                                            report.setNote(jsonOBject.getString("note"));
                                            report.setXpNote(jsonOBject.getString("xpNote"));
                                            report.setPisteNote(jsonOBject.getString("pisteNote"));
                                            report.setPieCouteNote(jsonOBject.getString("pieCouteNote"));
                                            report.setLocationNote(jsonOBject.getString("locationNote"));
                                            report.setNationalityNote(jsonOBject.getString("nationalityNote"));
                                            report.setEnglishNote(jsonOBject.getString("EnglishNote"));
                                            report.setCompetences(jsonOBject.getString("competences"));

                                            Candidate.setCurrentCandidate(candidate);
                                            Meeting.setCurrentMeeting(report);

                                            tvResult.append("\n");
                                            tvResult.append("\n");
                                            tvResult.append("Nom : " + candidate.getLastname());
                                            tvResult.append("\n");
                                            tvResult.append("Prénom : " + candidate.getFirstname());
                                            tvResult.append("\n");
                                            tvResult.append("Sexe : " + candidate.getSexe());
                                            tvResult.append("\n");
                                            tvResult.append("Email : " + candidate.getEmail());
                                            tvResult.append("\n");
                                            tvResult.append("N°Phone : " + candidate.getPhone());
                                            tvResult.append("\n");
                                            tvResult.append("Code postal : " + candidate.getZipcode());
                                            tvResult.append("\n");
                                            tvResult.append("Année : " + candidate.getAnnee());
                                            tvResult.append("\n");
                                            tvResult.append("Lien : " + candidate.getLien());
                                            tvResult.append("\n");
                                            tvResult.append("Action : " + candidate.getActions());
                                            tvResult.append("\n");

                                            if (candidate.getActions().matches("freelance")) {
                                                tvResult.append("Prix : " + candidate.getPrix());
                                                tvResult.append("\n");
                                            }

                                            tvResult.append("crCall : " + candidate.getCrCall());
                                            tvResult.append("\n");
                                            tvResult.append("\n");
                                            tvResult.append("Entretien du candidat : ");
                                            tvResult.append("\n");
                                            tvResult.append("\n");
                                            tvResult.append("Note : " + report.getNote());
                                            tvResult.append("\n");
                                            tvResult.append("XpNote : " + report.getXpNote());
                                            tvResult.append("\n");
                                            tvResult.append("NSNote : " + report.getNsNote());
                                            tvResult.append("\n");
                                            tvResult.append("JobIdealNote : " + report.getJobIdealNote());
                                            tvResult.append("\n");
                                            tvResult.append("PisteNote : " + report.getPisteNote());
                                            tvResult.append("\n");
                                            tvResult.append("PieCouteNote : " + report.getPieCouteNote());
                                            tvResult.append("\n");
                                            tvResult.append("LocationNote : " + report.getLocationNote());
                                            tvResult.append("\n");
                                            tvResult.append("EnglishNote : " + report.getEnglishNote());
                                            tvResult.append("\n");
                                            tvResult.append("NationalityNote : " + report.getNationalityNote());
                                            tvResult.append("\n");
                                            tvResult.append("Competences : " + report.getCompetences());
                                            showButton();
                                        }
                                    }
                                }
                            }catch(JSONException e){
                                tvResult.append("Erreur de lecture : ");
                                tvResult.append("\n");
                                tvResult.append("" + e);
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
        user = User.getCurrentUser();
        final RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/mobile/" +nom+"/"+user.getSessionId();
        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Recherche candidat : " ;

                    @Override
                    public void onResponse(final JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                            final String[] firstname = {""};

                            if(response.length() > 1){
                                //more than one candidat found, need to choice just one by his firstname
                                final EditText edittext = new EditText(SearchActivity.this);
                                //Ask for his firstname
                                    AlertDialog.Builder builder = new AlertDialog.Builder(SearchActivity.this, R.style.MyDialogTheme);
                                    builder.setTitle("Nom similaire");
                                    builder.setMessage("Plusieurs candidats ont le même nom, veuillez préciser le prénom :");
                                    builder.setView(edittext);
                                    builder.setPositiveButton("Chercher !",
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
                                                                candidate = Candidate.getCurrentCandidate();
                                                                candidate.setEmail(jsonOBject.getString("email"));
                                                                Candidate.setCurrentCandidate(candidate);
                                                                SearchCandidate(etNom.getText().toString(), firstname[0]);
                                                                break outerloop;
                                                            }
                                                        }
                                                        //If it doesn't exist : try with another or dismiss
                                                        if(!found){
                                                            AlertDialog.Builder dialogFalse = new AlertDialog.Builder(SearchActivity.this, R.style.MyDialogTheme);
                                                            dialogFalse.setMessage("Le candidat "+firstname[0]+" "+etNom.getText().toString()+" n'existe pas");
                                                            dialogFalse.setNeutralButton("Réessayer",
                                                                    new DialogInterface.OnClickListener() {
                                                                        @Override
                                                                        public void onClick(DialogInterface dialog, int which) {
                                                                            getEmail(etNom.getText().toString());
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
                                                        tvResult.setVisibility(View.VISIBLE);
                                                        tvResult.append("Une erreur serveur est survenue : "+e.toString());
                                                        e.printStackTrace();
                                                    }
                                                }
                                            });
                                    AlertDialog alert = builder.create();
                                    alert.show();
                            }
                            //Only one candidat found with this name
                            else if (response.length() == 1) {
                                JSONObject jsonOBject = response.getJSONObject(0);
                                candidate = Candidate.getCurrentCandidate();
                                candidate.setEmail(jsonOBject.getString("email"));
                                Candidate.setCurrentCandidate(candidate);
                                SearchCandidate(etNom.getText().toString(), "");
                            }
                        }catch(JSONException e){
                            AlertDialog.Builder builder = new AlertDialog.Builder(SearchActivity.this, R.style.MyDialogTheme);
                            builder.setMessage("Le candidat "+etNom.getText().toString()+" n'existe pas")
                                    .setNegativeButton("Réessayer", null)
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
                        tvResult.setVisibility(View.VISIBLE);
                        tvResult.append("Une erreur serveur est survenue : " + error.toString());
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
    public void showButton(){
        bModify.setVisibility(View.VISIBLE);
        bLocate.setVisibility(View.VISIBLE);
        bSMS.setVisibility(View.VISIBLE);
        tvInfo.setVisibility(View.VISIBLE);
        tvResult.setVisibility(View.VISIBLE);
    }
}
