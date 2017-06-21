package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
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
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.PredictSalaryRequest;
import com.example.fabiengamel.candidatemanagement.Requests.SalaryRequest;
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

                    PredictSalary();
                   /* try {
                        TestJsonBody();
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }*/
                }
            }
        });

    }

    public boolean CheckEmptyField() {
        if(etNbProject.getText().toString().matches("") || etNbNbHours.getText().toString().matches("") ||
                etNbYears.getText().toString().matches("")) {
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
                    if(result.matches("low"))
                    {
                        probability = lowProbability;
                    }
                    else if(result.matches("medium")) {
                        probability = mediumProbability;
                    }
                    else if(result.matches("high")) {
                        probability = highProbability;
                    }

                    AlertDialog.Builder builder = new AlertDialog.Builder(PredictSalaryActivity.this);
                    builder.setMessage("Tranche de salaire éstimée : "+result+" avec une probabilité de : "+probability)
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


    JSONObject jsonBody;
    JSONObject Inputs;
    JSONObject input1;
    JSONObject GlobalParameters;
    JSONArray ColumnNames;
    JSONArray Values;
    JSONArray ValuesNone;

    public void TestJsonBody() throws JSONException {
        jsonBody = new JSONObject();
        Inputs = new JSONObject();
        input1 = new JSONObject();
        GlobalParameters = new JSONObject();

       /* ColumnNames = new JSONArray();
        ColumnNames.put(0, "number_project");
        ColumnNames.put(1, "average_montly_hours");
        ColumnNames.put(2, "time_spend_company");
        ColumnNames.put(3, "Work_accident");
        ColumnNames.put(4, "promotion_last_5years");

        Values = new JSONArray();
        
        Values.put(0, "2");
        Values.put(1, "167");
        Values.put(2, "4");
        Values.put(3, "0");
        Values.put(4, "0");


        jsonBody.put("Inputs",Inputs.put("input1", input1.put("ColumnNames", ColumnNames).put("Values", Values))).put("GlobalParameters", GlobalParameters);*/

        input1.put("number_project", "2");
        input1.put("average_montly_hours", "189");
        input1.put("time_spend_company", "2");
        input1.put("Work_accident", "0");
        input1.put("promotion_last_5years", "1");
        jsonBody.put("Inputs",Inputs.put("input1", input1).put("GlobalParameters", GlobalParameters));
        String body = jsonBody.toString();
       // body = body.substring(0,147)+"["+body.substring(147,170)+"]"+body.substring(170,body.length());
       // body = body.substring(1, body.length()-1);


        AlertDialog.Builder builder = new AlertDialog.Builder(PredictSalaryActivity.this);
        builder.setMessage(" "+body)
                .setNeutralButton("Ok", null)
                .create()
                .show();


    }

}
