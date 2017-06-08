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
        bRefresh = (Button)findViewById(R.id.bRefresh);

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

        bRefresh.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                RefreshAll();
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
        else if(!rdFemme.isActivated() || !rdHomme.isActivated()) {
            return false;
        }
        return true;
    }
    public void AddCandidat() {

        if(!CheckEmptyField()) {
            Toast.makeText(this, "Veuillez remplir les champs obligatoires", Toast.LENGTH_LONG).show();
        }
        else if(rdFemme.isActivated() && rdHomme.isActivated()){
            Toast.makeText(this, "Choisissez un seul sexe", Toast.LENGTH_LONG).show();
        }
        else{

            Toast.makeText(this, "C'est parti", Toast.LENGTH_LONG).show();
            //Récupérer toutes les valeurs des champs

            //Faire la requête
         /*   Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {

                @Override
                public void onResponse(JSONObject response) {
                    Log.d("LOGIN :", response.toString());

                    // JSONArray results = null;
                    try {
                        User u = new User();
                        u.email = response.getString("email");
                        u.sessionId = response.getString("sessionId");
                        User.setCurrentUser(u);

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }

                    Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                    LoginActivity.this.startActivity(intent);
                }

            };

            Response.ErrorListener errorListener = new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    error.printStackTrace();
                    Log.d("log2=", error.toString());
                    AlertDialog.Builder builder = new AlertDialog.Builder(LoginActivity.this);
                    builder.setMessage(error.toString())
                            .setNegativeButton("Réessayer", null)
                            .create()
                            .show();
                }
            };

            LoginRequest loginRequest = null;
            try {
                loginRequest = new LoginRequest(mail, password, responseListener, errorListener);
            } catch (JSONException e) {
                e.printStackTrace();
            }
            RequestQueue queue = Volley.newRequestQueue(LoginActivity.this);
            queue.add(loginRequest);
        }*/
        }

    }

    public void RefreshAll() {
        //vider tout les champs
    }
}
