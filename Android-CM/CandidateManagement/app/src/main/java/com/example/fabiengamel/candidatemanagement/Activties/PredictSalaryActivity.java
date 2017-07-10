package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.InputFilter;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.PredictSalaryRequest;
import com.example.fabiengamel.candidatemanagement.Requests.SalaryRequest;
import com.example.fabiengamel.candidatemanagement.Utils.InputFilterMinMax;
import com.example.fabiengamel.candidatemanagement.Utils.Tools;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

public class PredictSalaryActivity extends AppCompatActivity {

    EditText etNbProject;
    EditText etNbNbHours;
    EditText etNbYears;
    Button bPredict;
    RadioButton rdAccidentTrue;
    RadioButton rdAccidentFalse;
    RadioButton rdPromoTrue;
    RadioButton rdPromoFalse;
    RadioGroup radioGroupAccident;
    RadioGroup radioGroupPromo;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_predict_salary);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        InitContent();
    }


    public void InitContent() {

        etNbProject = (EditText)findViewById(R.id.etNbProjectSalary);
        etNbNbHours = (EditText)findViewById(R.id.etNbHoursByMonthSalary);
        etNbYears = (EditText)findViewById(R.id.etTimeSpendSalary);
        bPredict = (Button)findViewById(R.id.bPredictSalary);
        rdAccidentTrue = (RadioButton)findViewById(R.id.rbaccidentTrueSalary);
        rdAccidentFalse = (RadioButton)findViewById(R.id.rbaccidentFalseSalary);
        rdPromoTrue = (RadioButton)findViewById(R.id.rbPromoTrueSalary);
        rdPromoFalse = (RadioButton)findViewById(R.id.rbPromoFalseSalary);
        radioGroupAccident = (RadioGroup)findViewById(R.id.rgaccidentSalary);
        radioGroupPromo  = (RadioGroup)findViewById(R.id.rgPromoSalary);

        radioGroupAccident.check(R.id.rbaccidentFalseSalary);
        radioGroupPromo.check(R.id.rbPromoFalseSalary);

        bPredict.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (!CheckEmptyField()) {
                    Toast.makeText(PredictSalaryActivity.this, "Veuillez remplir les champs obligatoires", Toast.LENGTH_LONG).show();
                } else {

                    int nbhours = Integer.valueOf(etNbNbHours.getText().toString());
                    if (nbhours < 100) {
                        Toast.makeText(PredictSalaryActivity.this, "Veuillez saisir un nombre d'heures correct", Toast.LENGTH_LONG).show();
                    } else if (!checkCoherentValues()) {
                        Toast.makeText(PredictSalaryActivity.this, "Veuillez remplir avec des valeurs cohérentes", Toast.LENGTH_LONG).show();
                    } else {
                        PredictSalary();
                    }
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
                etNbYears.getText().toString().matches("")) {
            return false;
        }
        return true;
    }

    public boolean checkCoherentValues(){
        if(Integer.parseInt(etNbNbHours.getText().toString()) > 260 || Integer.parseInt(etNbYears.getText().toString()) > 20){
            return false;
        }
        return true;
    }

    public void PredictSalary()
    {
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

                    String highProbability = (String) Values.get(0);
                    String lowProbability = (String) Values.get(1);
                    String mediumProbability = (String) Values.get(2);
                    String result = (String) Values.get(3);
                    String probability = "";
                    String resultAffich = "";

                    if(result.matches("low"))
                    {
                        probability = lowProbability;
                        resultAffich = "FAIBLE";
                    }
                    else if(result.matches("medium")) {
                        probability = mediumProbability;
                        resultAffich = "MOYEN";
                    }
                    else if(result.matches("high")) {
                        probability = highProbability;
                        resultAffich = "FORT";
                    }

                    Double res = Double.valueOf(probability) * 100;
                    String probabilityAffich = String.valueOf(res);
                    probabilityAffich = probabilityAffich.substring(0, 2);


                    AlertDialog.Builder builder = new AlertDialog.Builder(PredictSalaryActivity.this);
                    builder.setMessage("Tranche de salaire éstimée : "+resultAffich+" avec une probabilité de : "+probabilityAffich+"%")
                            .setNeutralButton("Ok", null)
                            .create()
                            .show();

                } catch (JSONException e) {
                    e.printStackTrace();
                    AlertDialog.Builder builder = new AlertDialog.Builder(PredictSalaryActivity.this);
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
                error.printStackTrace();
                Log.d("log2=", error.toString());
                AlertDialog.Builder builder = new AlertDialog.Builder(PredictSalaryActivity.this);
                builder.setMessage("ERREUR SERVEUR : "+error.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
            }
        };

        PredictSalaryRequest salaryRequest = null;
        try {
            String nbProject = etNbProject.getText().toString();
            String nbHours = etNbNbHours.getText().toString();
            String companyTime = etNbYears.getText().toString();
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


            salaryRequest = new PredictSalaryRequest(nbProject, nbHours, companyTime, accident, promo, responseListener, errorListener);
        } catch (JSONException e) {
            e.printStackTrace();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        RequestQueue queue = Volley.newRequestQueue(PredictSalaryActivity.this);
        queue.add(salaryRequest);
    }
    @Override
    protected void onRestart(){
        super.onRestart();
        AlertDialog.Builder builder = new AlertDialog.Builder(PredictSalaryActivity.this, R.style.MyDialogTheme);
        builder.setMessage("Veuillez vous reconnecter");
        builder.setPositiveButton("Ok",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        startActivity(new Intent(PredictSalaryActivity.this, LoginActivity.class));
                    }
                });
        AlertDialog alert = builder.create();
        alert.show();
    }
}
