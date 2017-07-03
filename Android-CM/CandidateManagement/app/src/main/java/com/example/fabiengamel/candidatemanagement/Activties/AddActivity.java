package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
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
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.AddReportRequest;
import com.example.fabiengamel.candidatemanagement.Requests.AddRequest;
import com.example.fabiengamel.candidatemanagement.Utils.Tools;

import org.json.JSONException;
import org.json.JSONObject;

public class AddActivity extends AppCompatActivity {

    EditText etName;
    EditText etFirstname;
    EditText etMail;
    EditText etPhone;
    EditText etZipcode;
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
    Button bAdd;
    String action = "";
    EditText etPRix;
    TextView tvPrix;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        InitContent();

    }

    public void InitContent() {
        etName = (EditText)findViewById(R.id.etNom);
        etFirstname = (EditText)findViewById(R.id.etPrenom);
        etPhone = (EditText)findViewById(R.id.etPhone);
        etYear = (EditText)findViewById(R.id.etYear);
        etLink = (EditText)findViewById(R.id.etLink);
        etCrCall = (EditText)findViewById(R.id.etcrCall);
        etNs = (EditText)findViewById(R.id.etNs);
        etMail = (EditText)findViewById(R.id.etMailAdd);
        spAction = (Spinner)findViewById(R.id.spActionAdd);
        rdHomme = (RadioButton)findViewById(R.id.rbMasculin);
        rdFemme = (RadioButton)findViewById(R.id.rbFeminin);
        rdEmailyes = (RadioButton)findViewById(R.id.rbTrue);
        rdEmailNo = (RadioButton)findViewById(R.id.rbFalse);
        etJobIdeal = (EditText)findViewById(R.id.etLocationNoteAdd);
        etPisteCoute = (EditText)findViewById(R.id.etPicouteNoteAdd);
        etPiste = (EditText)findViewById(R.id.etPisteNoteAdd);
        etLocation = (EditText)findViewById(R.id.etLocationNoteAdd);
        etEnglish = (EditText)findViewById(R.id.etEnglishNoteAdd);
        etNational = (EditText)findViewById(R.id.etNationalityNoteAdd);
        etCompetences = (EditText)findViewById(R.id.etCompetencesAdd);
        etXpNote = (EditText)findViewById(R.id.etXpNoteAdd);
        etNsNote = (EditText)findViewById(R.id.etNsNoteAdd);
        etNote = (EditText)findViewById(R.id.etNoteAdd);
        etPRix = (EditText)findViewById(R.id.etPrixAdd);
        bAdd = (Button)findViewById(R.id.bAdd);
        tvPrix = (TextView)findViewById(R.id.tvPrixAdd);
        etZipcode = (EditText)findViewById(R.id.etZipcodeAdd);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.actions_array, R.layout.spinner_custom);
        adapter.setDropDownViewResource(R.layout.spiner_dropdown_custom);
        spAction.setAdapter(adapter);

        spAction.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
                action = spAction.getSelectedItem().toString();
                if(action.matches("freelance"))
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                    builder.setMessage("Vous avez selectionné Freelance : le champs Prix obligatoire est apparu en bas de la page")
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

        bAdd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Tools tool = new Tools();
                if (!tool.isEmailValid(etMail.getText())) {
                    Toast.makeText(AddActivity.this, "Email non valide", Toast.LENGTH_LONG).show();
                } else if(!CheckEmptyField()) {
                    Toast.makeText(AddActivity.this, "Veuillez remplir les champs obligatoires", Toast.LENGTH_LONG).show();
                }
                else{
                    AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                    builder.setTitle("Ajouter le candidat ?");
                    builder.setPositiveButton("Oui",
                            new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog,
                                                    int which) {
                                    AddCandidat();
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

    @Override
    public void onBackPressed() {
        AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
        builder.setTitle("Quitter l'ajout ?");
        builder.setPositiveButton("Oui",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog,
                                        int which) {
                        Intent i = new Intent(AddActivity.this, MainActivity.class);
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

    public boolean onCreateOptionsMenu(Menu menu) {
        return true;
    }

    public boolean CheckEmptyField() {
        if(etName.getText().toString().matches("") || etFirstname.getText().toString().matches("") ||
                etMail.getText().toString().matches("") || etPhone.getText().toString().matches("") ||
                etYear.getText().toString().matches("") || etNote.getText().toString().matches("")) {
            return false;
        }
        else if (!rdFemme.isChecked() && !rdHomme.isChecked()) {
            return false;
        }

        return true;
    }


    public void AddCandidat() {
               Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {
                    Log.d("ADD :", response.toString());
                    try {
                        if(response.getBoolean("success")) {
                        AddReport();
                        }
                        else {
                            String erreur = response.getString("content");
                            AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
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
                    AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                    builder.setMessage("ERREUR SERVEUR : "+error.toString())
                            .setNegativeButton("Réessayer", null)
                            .create()
                            .show();
                }
            };

            AddRequest addRequest = null;
            try {
                User user = User.getCurrentUser();
                String sessionId = user.sessionId;

                //valeurs champs
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

                //champs facultatifs
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
                addRequest = new AddRequest(sessionId,name,firstname, mail, phone, zipcode, sexe, action, year, link,
                        crCall, ns, email, prix, responseListener, errorListener);

            } catch (JSONException e) {
                AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                builder.setMessage(e.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
                e.printStackTrace();
            }
            RequestQueue queue = Volley.newRequestQueue(AddActivity.this);
            queue.add(addRequest);

    }

    public void AddReport() {

        Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Log.d("REPORT :", response.toString());

                try {
                    if(response.getBoolean("success")) {
                        AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                        builder.setTitle("Le candidat a bien été ajouté");
                        builder.setNeutralButton("Ok", new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog,
                                                int which) {
                                startActivity(new Intent(AddActivity.this, MainActivity.class));
                            }
                        });
                        AlertDialog alert = builder.create();
                        alert.show();
                    }
                    else {
                        //erreur
                        String erreur = response.getString("content");
                        AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                        builder.setMessage("json_report"+erreur)
                                .setNegativeButton("Réessayer", null)
                                .create()
                                .show();
                    }

                } catch (JSONException e) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
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
                //Gestion error
                AlertDialog.Builder builder = new AlertDialog.Builder(AddActivity.this, R.style.MyDialogTheme);
                builder.setMessage("ERREUR SERVEUR : "+error.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
            }
        };

        AddReportRequest addReportRequest = null;
        try {
            User user = User.getCurrentUser();
            String sessionId = user.sessionId;
            String mail = etMail.getText().toString();

            //champs facultatifs
            String link = etLink.getText().toString();
            String crCall = etCrCall.getText().toString();
            String jobIdeal = etJobIdeal.getText().toString() ;
            String pisteNote = etPiste.getText().toString();
            String pieCoute = etPisteCoute.getText().toString();
            String locationNote = etLocation.getText().toString();
            String englishNote = etEnglish.getText().toString();
            String national = etNational.getText().toString();
            String competences = etCompetences.getText().toString();
            String nsnote = etNsNote.getText().toString();
            String xpnote = etXpNote.getText().toString();
            int note = Integer.parseInt(etNote.getText().toString());


             addReportRequest = new AddReportRequest(mail,sessionId, note,link,xpnote,nsnote, jobIdeal, pisteNote,
                     pieCoute, locationNote, englishNote, national, competences,  responseListener, errorListener);

        } catch (JSONException e) {
            Toast.makeText(this, ""+e, Toast.LENGTH_LONG).show();
            e.printStackTrace();
        }

        RequestQueue queue = Volley.newRequestQueue(AddActivity.this);
        queue.add(addReportRequest);

    }

}

