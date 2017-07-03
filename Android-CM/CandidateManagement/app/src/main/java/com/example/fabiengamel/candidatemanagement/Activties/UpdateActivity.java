package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.Candidate;
import com.example.fabiengamel.candidatemanagement.Models.Meeting;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.UpdateReportRequest;
import com.example.fabiengamel.candidatemanagement.Requests.UpdateRequest;
import com.example.fabiengamel.candidatemanagement.Utils.Tools;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.concurrent.TimeUnit;

public class UpdateActivity extends AppCompatActivity {

    EditText etName;
    EditText etFirstname;
    EditText etMail;
    EditText etPhone;
    RadioButton rdHomme;
    RadioButton rdFemme;
    Spinner spAction;
    EditText etYear;
    EditText etLink;
    EditText etCrCall;
    EditText etNs;
    RadioButton rdEmailyes;
    RadioButton rdEmailNo;
    EditText etJobIdeal;
    EditText etPiste;
    EditText etPisteCoute;
    EditText etLocation;
    EditText etEnglish;
    EditText etNational;
    EditText etCompetences;
    EditText etNote;
    EditText etXpNote;
    EditText etNsNote;
    String action = "";
    Button bUpdate;
    RadioGroup sexes;
    RadioGroup approche_emaill;
    EditText etPRix;
    TextView tvPrix;
    EditText etZipcode;
    User user;
    Candidate candidate;
    Meeting report;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        try {
            InitContent();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

    }

    public void InitContent() throws InterruptedException {

        candidate = Candidate.getCurrentCandidate();
        report = Meeting.getCurrentMeeting();
        etName = (EditText)findViewById(R.id.etNomUpdate);
        etFirstname = (EditText)findViewById(R.id.etPrenomUpdate);
        etPhone = (EditText)findViewById(R.id.etPhoneUpdate);
        etYear = (EditText)findViewById(R.id.etYearUpdate);
        etLink = (EditText)findViewById(R.id.etLinkUpdate);
        etCrCall = (EditText)findViewById(R.id.etcrCallUpdate);
        etNs = (EditText)findViewById(R.id.etNsUpdate);
        etMail = (EditText)findViewById(R.id.etMailUpdate);
        spAction = (Spinner)findViewById(R.id.spActionAddUpdate);
        rdHomme = (RadioButton)findViewById(R.id.rbMasculinUpdate);
        rdFemme = (RadioButton)findViewById(R.id.rbFemininUpdate);
        rdEmailyes = (RadioButton)findViewById(R.id.rbTrueUpdate);
        rdEmailNo = (RadioButton)findViewById(R.id.rbFalseUpdate);
        etJobIdeal = (EditText)findViewById(R.id.etJobIdealNoteUpdate);
        etPisteCoute = (EditText)findViewById(R.id.etPicouteNoteUpdate);
        etPiste = (EditText)findViewById(R.id.etPisteNoteUpdate);
        etLocation = (EditText)findViewById(R.id.etLocationNoteUpdate);
        etEnglish = (EditText)findViewById(R.id.etEnglishNoteUpdate);
        etNational = (EditText)findViewById(R.id.etNationalityNoteUpdate);
        etCompetences = (EditText)findViewById(R.id.etCompetencesUpdate);
        etXpNote = (EditText)findViewById(R.id.etXpNoteUpdate);
        etNsNote = (EditText)findViewById(R.id.etNsNoteUpdate);
        etNote = (EditText)findViewById(R.id.etNoteUpdate);
        bUpdate = (Button)findViewById(R.id.bUpdate);
        sexes = (RadioGroup)findViewById(R.id.rgSexeUpdate);
        approche_emaill = (RadioGroup)findViewById(R.id.rgEmailUpdate);
        etPRix = (EditText)findViewById(R.id.etPrixUpdate);
        tvPrix = (TextView)findViewById(R.id.tvPrixUpdate);
        etZipcode = (EditText)findViewById(R.id.etZipcodeUpdate);

        bUpdate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Tools tool = new Tools();
                if (!tool.isEmailValid(etMail.getText())) {
                    Toast.makeText(UpdateActivity.this, "Email non valide", Toast.LENGTH_LONG).show();
                } else {
                    AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                    builder.setTitle("Modifier le candidat ?");
                    builder.setPositiveButton("Oui",
                            new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog,
                                                    int which) {
                                    UpdateCandidate();
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

            }
        });

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.actions_array, R.layout.spinner_custom);
        adapter.setDropDownViewResource(R.layout.spiner_dropdown_custom);
        spAction.setAdapter(adapter);

        spAction.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
                action = spAction.getSelectedItem().toString();
                if(action.matches("freelance"))
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                    builder.setMessage("Vous avez selectionné Freelance : le champs Prix obligatoire est disponible en bas de la page")
                            .setNeutralButton("Compris !", null)
                            .create()
                            .show();
                    etPRix.setVisibility(View.VISIBLE);
                    tvPrix.setVisibility(View.VISIBLE);
                }
                else {
                    etPRix.setVisibility(View.INVISIBLE);
                    tvPrix.setVisibility(View.INVISIBLE);
                }
            }

            public void onNothingSelected(AdapterView<?> parent) {
                action = "interne";
            }
        });

        etName.setText(candidate.lastname);
        etFirstname.setText(candidate.firstname);
        etMail.setText(candidate.email);
        if(candidate.sexe.matches("M")){
            sexes.check(rdHomme.getId());
        } else if (candidate.sexe.matches("F")) {
            sexes.check(rdFemme.getId());
        }
        etYear.setText(String.valueOf(candidate.annee));
        etLink.setText(candidate.lien);
        etCrCall.setText(candidate.crCall);
        etNs.setText(candidate.NS);
        etPhone.setText(candidate.phone);
        etZipcode.setText(candidate.zipcode);
        if(candidate.approche_email){
            approche_emaill.check(rdEmailyes.getId());
        }else {
            approche_emaill.check(rdEmailNo.getId());
        }
        etNote.setText(report.note);
        etXpNote.setText(report.xpNote);
        etNsNote.setText(report.nsNote);
        etJobIdeal.setText(report.jobIdealNote);
        etPiste.setText(report.pisteNote);
        etPisteCoute.setText(report.pieCouteNote);
        etLocation.setText(report.locationNote);
        etEnglish.setText(report.EnglishNote);
        etNational.setText(report.nationalityNote);
        etCompetences.setText((report.competences));
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

    @Override
    public void onBackPressed() {
        AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
        builder.setTitle("Quitter la modification ?");
        builder.setPositiveButton("Oui",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog,
                                        int which) {
                        Intent i = new Intent(UpdateActivity.this, SearchActivity.class);
                        i.putExtra("candidateName", etName.getText().toString());
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



    public void UpdateCandidate(){

        Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Log.d("UPDATE :", response.toString());
                try {
                    if(response.getBoolean("success")) {
                        UpdateReport();
                    }
                    else {
                        String erreur = response.getString("content");
                        AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                        builder.setMessage("json_add"+erreur)
                                .setNegativeButton("Réessayer", null)
                                .create()
                                .show();
                    }

                } catch (JSONException e) {
                    e.printStackTrace();
                }

            }

        };

        Response.ErrorListener errorListener = new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                builder.setMessage("ERREUR SERVEUR : "+error.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
            }
        };

        UpdateRequest updateRequest = null;
        try {
            user = User.getCurrentUser();
            String sessionId = user.sessionId;

            String mail = etMail.getText().toString();
            String name = etName.getText().toString();
            String firstname = etFirstname.getText().toString();
            String phone = etPhone.getText().toString();
            String zipcode = etZipcode.getText().toString();
            String sexe = "";

            if(rdHomme.isChecked()) {
                sexe = "M";
            } else if(rdFemme.isChecked()){
                sexe = "F";
            }
            int year = Integer.parseInt(etYear.getText().toString());

            String link = etLink.getText().toString();
            String crCall = etCrCall.getText().toString();
            String ns = etNs.getText().toString();
            boolean email = false;
            if(rdEmailyes.isChecked()) {
                email = true;
            } else if(rdEmailNo.isChecked()){
                email = false;
            }
            String prix;
            if(action.matches("freelance")) {
                prix = etPRix.getText().toString();
            }
            else {
                prix = "0";
            }

            updateRequest = new UpdateRequest(sessionId,name,firstname, mail, phone,zipcode, sexe, action, year, link,
                    crCall, ns, email, prix, responseListener, errorListener);

        } catch (JSONException e) {
            AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
            builder.setMessage(e.toString())
                    .setNegativeButton("Réessayer", null)
                    .create()
                    .show();
            e.printStackTrace();
        }
        RequestQueue queue = Volley.newRequestQueue(UpdateActivity.this);
        queue.add(updateRequest);


    }

    public void UpdateReport() {
        Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Log.d("REPORT :", response.toString());

                try {
                    if(response.getBoolean("success")) {
                        AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                        builder.setTitle("Le candidat a bien été modifié");
                        builder.setNeutralButton("Ok", new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog,
                                                int which) {
                                startActivity(new Intent(UpdateActivity.this, MainActivity.class));
                            }
                        });
                        AlertDialog alert = builder.create();
                        alert.show();

                    }
                    else {
                        //erreur
                        String erreur = response.getString("content");
                        AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                        builder.setMessage("json_report"+erreur)
                                .setNegativeButton("Réessayer", null)
                                .create()
                                .show();
                    }

                } catch (JSONException e) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                    builder.setMessage(e.toString())
                            .setNegativeButton("Réessayer", null)
                            .create()
                            .show();
                    e.printStackTrace();
                }
            }
        };

        Response.ErrorListener errorListener = new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                AlertDialog.Builder builder = new AlertDialog.Builder(UpdateActivity.this, R.style.MyDialogTheme);
                builder.setMessage("ERREUR SERVEUR : "+error.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
            }
        };

        UpdateReportRequest updateReportRequest = null;
        try {
            user = User.getCurrentUser();
            String sessionId = user.sessionId;
            String mail = etMail.getText().toString();

            String link = etLink.getText().toString();
            String jobIdeal = etJobIdeal.getText().toString() ;
            String pisteNote = etPiste.getText().toString();
            String pieCoute = etPisteCoute.getText().toString();
            String locationNote = etLocation.getText().toString();
            String englishNote = etEnglish.getText().toString();
            String national = etNational.getText().toString();
            String competences = etCompetences.getText().toString();
            String nsnote = etNsNote.getText().toString();
            String xpnote = etXpNote.getText().toString();
            String note = etNote.getText().toString();

            updateReportRequest = new UpdateReportRequest(mail,sessionId, note,link,xpnote,nsnote, jobIdeal, pisteNote,
                    pieCoute, locationNote, englishNote, national, competences, responseListener, errorListener);

        } catch (JSONException e) {
            Toast.makeText(this, "" + e, Toast.LENGTH_LONG).show();
            e.printStackTrace();
        }

        RequestQueue queue = Volley.newRequestQueue(UpdateActivity.this);
        queue.add(updateReportRequest);

    }
}
