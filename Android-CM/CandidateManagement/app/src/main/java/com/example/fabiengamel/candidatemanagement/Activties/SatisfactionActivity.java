package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
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
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.SatisfactionRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

public class SatisfactionActivity extends AppCompatActivity {

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
    Spinner spSalaire;
    String salary;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_satisfaction);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        InitContent();
    }


    public void InitContent() {

        etNbProject = (EditText)findViewById(R.id.etNbProjectSatisfaction);
        etNbNbHours = (EditText)findViewById(R.id.etNbHoursByMonthSatisfaction);
        etNbYears = (EditText)findViewById(R.id.etTimeSpendSatisfaction);
        bPredict = (Button)findViewById(R.id.bPredictSatisfaction);
        rdAccidentTrue = (RadioButton)findViewById(R.id.rbaccidentTrueSatisfaction);
        rdAccidentFalse = (RadioButton)findViewById(R.id.rbaccidentFalseSatisfaction);
        rdPromoTrue = (RadioButton)findViewById(R.id.rbPromoTrueSatisfaction);
        rdPromoFalse = (RadioButton)findViewById(R.id.rbPromoFalseSatisfaction);
        radioGroupAccident = (RadioGroup)findViewById(R.id.rgaccidentSatisfaction);
        radioGroupPromo  = (RadioGroup)findViewById(R.id.rgPromoSatisfaction);
        spSalaire = (Spinner)findViewById(R.id.spSalaireSatisfaction);

        radioGroupAccident.check(R.id.rbaccidentFalseSatisfaction);
        radioGroupPromo.check(R.id.rbPromoFalseSatisfaction);

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
                    Toast.makeText(SatisfactionActivity.this, "Veuillez remplir les champs obligatoires", Toast.LENGTH_LONG).show();
                } else {

                    PredictSatisfaction();
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


    public void PredictSatisfaction()
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


                    Double res = Values.getDouble(0) * 100;
                    String result = String.valueOf(res);



                    AlertDialog.Builder builder = new AlertDialog.Builder(SatisfactionActivity.this, R.style.MyDialogTheme);
                    builder.setMessage("Taux de satisfaction éstimé : "+result+"%")
                            .setNeutralButton("Ok", null)
                            .create()
                            .show();

                } catch (JSONException e) {
                    e.printStackTrace();
                    AlertDialog.Builder builder = new AlertDialog.Builder(SatisfactionActivity.this, R.style.MyDialogTheme);
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
                AlertDialog.Builder builder = new AlertDialog.Builder(SatisfactionActivity.this, R.style.MyDialogTheme);
                builder.setMessage("ERREUR SERVEUR : "+error.toString())
                        .setNegativeButton("Réessayer", null)
                        .create()
                        .show();
            }
        };

        SatisfactionRequest satisfactionRequest = null;
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

            satisfactionRequest = new SatisfactionRequest(nbProject, nbHours, companyTime, accident, promo, salary, responseListener, errorListener);
        } catch (JSONException e) {
            e.printStackTrace();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        RequestQueue queue = Volley.newRequestQueue(SatisfactionActivity.this);
        queue.add(satisfactionRequest);
    }


}
