package com.example.fabiengamel.candidatemanagement;

import android.app.AlertDialog;
import android.content.Intent;
import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.Spinner;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.Requests.AddReportRequest;
import com.example.fabiengamel.candidatemanagement.Requests.AddRequest;
import com.example.fabiengamel.candidatemanagement.Requests.LoginRequest;

import org.json.JSONException;
import org.json.JSONObject;

public class AddActivity extends AppCompatActivity {

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
    Button bAdd;
    Button bRefresh;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add);

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
        bAdd = (Button)findViewById(R.id.bAdd);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.actions_array, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spAction.setAdapter(adapter);

        bAdd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                AddCandidat();
            }
        });

    }

    public boolean CheckEmptyField() {
        if(etName.getText().toString().matches("") || etFirstname.getText().toString().matches("") ||
                etMail.getText().toString().matches("") || etPhone.getText().toString().matches("") ||
                etYear.getText().toString().matches(""))
        {
            return false;
        }

        return true;
    }


    public void AddCandidat() {

        if(!CheckEmptyField()) {
            Toast.makeText(this, "Veuillez remplir les champs obligatoires", Toast.LENGTH_LONG).show();
        }

        else{
               Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {
                    Log.d("ADD :", response.toString());

                    // JSONArray results = null;
                    try {
                        User u = new User();
                        if(response.getBoolean("success")) {
                            //ajout report
                        AddReport();
                        }
                        else {
                            //erreur
                        }

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }

                }

            };

            Response.ErrorListener errorListener = new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    //Gestion error
                }
            };

            AddRequest addRequest = null;
            try {
                User user = new User();
                String sessionId = user.sessionId;

                //valeurs champs
                String mail = etMail.getText().toString();
                String name = etName.getText().toString();
                String firstname = etFirstname.getText().toString();
                String phone = etPhone.getText().toString();
                String sexe = "";
                if(rdHomme.isChecked()) {
                    sexe = "M";
                } else if(rdFemme.isChecked()){
                    sexe = "F";
                }
                String action = spAction.getSelectedItem().toString();
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

                addRequest = new AddRequest(sessionId,name,firstname, mail, phone, sexe, action, year, link,
                        crCall, ns, email,responseListener, errorListener);

            } catch (JSONException e) {
                Toast.makeText(this, ""+e, Toast.LENGTH_LONG).show();
                e.printStackTrace();
            }
            RequestQueue queue = Volley.newRequestQueue(AddActivity.this);
            queue.add(addRequest);
        }
        }

    public void AddReport() {

        Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Log.d("REPORT :", response.toString());

                // JSONArray results = null;
                try {
                    User u = new User();
                    if(response.getBoolean("success")) {
                        //Afficher success
                    }
                    else {
                        //erreur
                    }

                } catch (JSONException e) {
                    e.printStackTrace();
                }

            }

        };

        Response.ErrorListener errorListener = new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

                //Gestion error
            }
        };

        AddReportRequest addReportRequest = null;
        try {
            User user = new User();
            String sessionId = user.sessionId;

            //valeurs champs -------------------------------------> à modifier pour cette requête
            String mail = etMail.getText().toString();
            String name = etName.getText().toString();
            String firstname = etFirstname.getText().toString();
            String phone = etPhone.getText().toString();
            String sexe = "";
            if(rdHomme.isChecked()) {
                sexe = "M";
            } else if(rdFemme.isChecked()){
                sexe = "F";
            }
            String action = spAction.getSelectedItem().toString();
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
            //---------------------------> requête à construire
             addReportRequest = new AddReportRequest(sessionId,mail,firstname, mail, phone, sexe, action, link,
                    crCall, ns,"","", responseListener, errorListener);

        } catch (JSONException e) {
            Toast.makeText(this, ""+e, Toast.LENGTH_LONG).show();
            e.printStackTrace();
        }

        RequestQueue queue = Volley.newRequestQueue(AddActivity.this);
        queue.add(addReportRequest);

    }

}

