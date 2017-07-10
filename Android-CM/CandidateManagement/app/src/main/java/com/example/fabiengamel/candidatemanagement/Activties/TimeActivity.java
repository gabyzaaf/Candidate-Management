package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.app.ProgressDialog;
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
import android.widget.RadioGroup;
import android.widget.Spinner;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.TimeRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

public class TimeActivity extends AppCompatActivity {

    EditText etNbProject;
    EditText etNbNbHours;
    EditText etSatisfaction;
    Button bPredict;
    RadioButton rdAccidentTrue;
    RadioButton rdAccidentFalse;
    RadioButton rdPromoTrue;
    RadioButton rdPromoFalse;
    RadioGroup radioGroupAccident;
    RadioGroup radioGroupPromo;
    Spinner spSalaire;
    String salary;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_time);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        InitContent();
    }


    public void InitContent() {

        etNbProject = (EditText)findViewById(R.id.etNbProjectTime);
        etNbNbHours = (EditText)findViewById(R.id.etNbHoursByMonthTime);
        etSatisfaction = (EditText)findViewById(R.id.etSatisfactionTime);
        bPredict = (Button)findViewById(R.id.bPredictTime);
        rdAccidentTrue = (RadioButton)findViewById(R.id.rbaccidentTrueTime);
        rdAccidentFalse = (RadioButton)findViewById(R.id.rbaccidentFalseTime);
        rdPromoTrue = (RadioButton)findViewById(R.id.rbPromoTrueTime);
        rdPromoFalse = (RadioButton)findViewById(R.id.rbPromoFalseTime);
        radioGroupAccident = (RadioGroup)findViewById(R.id.rgaccidentTime);
        radioGroupPromo  = (RadioGroup)findViewById(R.id.rgPromoTime);
        spSalaire = (Spinner)findViewById(R.id.spSalaireTime);

        radioGroupAccident.check(R.id.rbaccidentFalseTime);
        radioGroupPromo.check(R.id.rbPromoFalseTime);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.salaire_array, R.layout.spinner_custom);
        adapter.setDropDownViewResource(R.layout.spiner_dropdown_custom);
        spSalaire.setAdapter(adapter);

        spSalaire.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
                salary = spSalaire.getSelectedItem().toString();
            }

            public void onNothingSelected(AdapterView<?> parent) {
                salary = "low";
            }
        });

        bPredict.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (!CheckEmptyField()) {
                    Toast.makeText(TimeActivity.this, "Veuillez remplir les champs obligatoires", Toast.LENGTH_LONG).show();
                }  else if(!checkCoherentValues()) {
                    Toast.makeText(TimeActivity.this, "Veuillez remplir avec des valeurs cohérentes", Toast.LENGTH_LONG).show();
                }
                else {

                    PredictTime();
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

    public boolean onCreateOptionsMenu(Menu menu) {
        return true;
    }

    public boolean CheckEmptyField() {
        if(etNbProject.getText().toString().matches("") || etNbNbHours.getText().toString().matches("") ||
                etSatisfaction.getText().toString().matches("")) {
            return false;
        }
        return true;
    }

    public boolean checkCoherentValues(){
        if(Integer.parseInt(etNbNbHours.getText().toString()) > 260 ){
            return false;
        }
        return true;
    }

    public void PredictTime()
    {
        final ProgressDialog dialog = ProgressDialog.show(TimeActivity.this, "", "Chargement en cours...", true);
        Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {

            @Override
            public void onResponse(JSONObject response) {
                Log.d("LOGIN :", response.toString());

                JSONArray Values = null;
                try {

                    JSONObject Results = response.getJSONObject("Results");
                    JSONObject output1 = Results.getJSONObject("output1");
                    JSONObject value = output1.getJSONObject("value");
                    Values = value.getJSONArray("Values").getJSONArray(0);
                    String result = (String) Values.get(0);

                    if (dialog != null)
                        dialog.cancel();
                    AlertDialog.Builder builder = new AlertDialog.Builder(TimeActivity.this, R.style.MyDialogTheme);
                    builder.setMessage("Temps restant dans l'entreprise éstimé : "+result+" an(s)")
                            .setNeutralButton("Ok", null)
                            .create()
                            .show();

                } catch (JSONException e) {
                    if (dialog != null)
                        dialog.cancel();
                    AlertDialog.Builder builder = new AlertDialog.Builder(TimeActivity.this, R.style.MyDialogTheme);
                    builder.setMessage(e.toString())
                            .setNeutralButton("Ok", null)
                            .create()
                            .show();
                }
            }

        };

        Response.ErrorListener errorListener = new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                if (dialog != null)
                    dialog.cancel();
                Log.d("log2=", error.toString());
                AlertDialog.Builder builder = new AlertDialog.Builder(TimeActivity.this, R.style.MyDialogTheme);
                builder.setMessage("ERREUR SERVEUR : "+error.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
            }
        };

        TimeRequest timeRequest = null;
        try {
            String nbProject = etNbProject.getText().toString();
            String nbHours = etNbNbHours.getText().toString();
            String accident = "";
            String promo = "";

            if(rdAccidentTrue.isChecked()){
                accident = "1";
            }
            else if(rdAccidentFalse.isChecked()){
                accident = "0";
            }
            if(rdPromoTrue.isChecked()){
                promo = "1";
            }
            else if(rdPromoFalse.isChecked()){
                promo = "0";
            }

            String resultat = etSatisfaction.getText().toString();
            int sati = Integer.parseInt(resultat);
            float sat = (float) sati / 100;
            String satisfaction = String.valueOf(sat);

            timeRequest = new TimeRequest(satisfaction, nbProject, nbHours, accident, promo, salary, responseListener, errorListener);
        } catch (JSONException e) {
            if (dialog != null)
                dialog.cancel();
            AlertDialog.Builder builder = new AlertDialog.Builder(TimeActivity.this, R.style.MyDialogTheme);
            builder.setMessage(e.toString())
                    .setNegativeButton("Réessayer", null)
                    .create()
                    .show();
        } catch (UnsupportedEncodingException e) {
            if (dialog != null)
                dialog.cancel();
            AlertDialog.Builder builder = new AlertDialog.Builder(TimeActivity.this, R.style.MyDialogTheme);
            builder.setMessage(e.toString())
                    .setNegativeButton("Réessayer", null)
                    .create()
                    .show();
        }
        RequestQueue queue = Volley.newRequestQueue(TimeActivity.this);
        queue.add(timeRequest);
    }

    @Override
    protected void onRestart(){
        super.onRestart();
        AlertDialog.Builder builder = new AlertDialog.Builder(TimeActivity.this, R.style.MyDialogTheme);
        builder.setMessage("Veuillez vous reconnecter");
        builder.setPositiveButton("Ok",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        startActivity(new Intent(TimeActivity.this, LoginActivity.class));
                    }
                });
        AlertDialog alert = builder.create();
        alert.show();
    }
}