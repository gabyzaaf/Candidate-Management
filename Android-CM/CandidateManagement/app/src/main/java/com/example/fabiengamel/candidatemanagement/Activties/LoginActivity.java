package com.example.fabiengamel.candidatemanagement.Activties;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Build;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.example.fabiengamel.candidatemanagement.Models.User;
import com.example.fabiengamel.candidatemanagement.R;
import com.example.fabiengamel.candidatemanagement.Requests.LoginRequest;
import com.example.fabiengamel.candidatemanagement.Utils.Tools;

import org.json.JSONException;
import org.json.JSONObject;

public class LoginActivity extends AppCompatActivity {

    EditText etMail;
    EditText etPassword;
    Button bLogin;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        InitContent();
    }

    public void InitContent() {
        etMail = (EditText) findViewById(R.id.etMailAdd);
        etPassword = (EditText) findViewById(R.id.etPassword);
        bLogin = (Button) findViewById(R.id.bLogin);


        bLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Login();
                // Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                // LoginActivity.this.startActivity(intent);
            }
        });
    }

    public void onBackPressed() {
        Intent i = new Intent(LoginActivity.this, LoginActivity.class);
        startActivity(i);
    }

    private void Login() {
        final ProgressDialog dialog = ProgressDialog.show(LoginActivity.this, "", "Chargement en cours...", true);
        final String mail = etMail.getText().toString();
        final String password = etPassword.getText().toString();
        Tools tool = new Tools();

        if(mail.matches(""))
        {
            Toast.makeText(this, "Renseignez votre email", Toast.LENGTH_LONG).show();
        }
        else if(password.matches(""))
        {
            Toast.makeText(this, "Renseignez votre mot de passe", Toast.LENGTH_LONG).show();
        }
        else if(!tool.isEmailValid(mail)){
            Toast.makeText(this, "Email non valide", Toast.LENGTH_LONG).show();
        }

        else {
            Response.Listener<JSONObject> responseListener = new Response.Listener<JSONObject>() {

                @Override
                public void onResponse(JSONObject response) {
                    Log.d("LOGIN :", response.toString());

                    try {

                        if(response.has("content"))
                        {
                            AlertDialog.Builder builder = new AlertDialog.Builder(LoginActivity.this, R.style.MyDialogTheme);
                            builder.setMessage(""+response.getString("content"))
                                    .setNeutralButton("Réessayer", null)
                                    .create()
                                    .show();
                        }
                        else {
                            User u = User.getCurrentUser();
                            u.setEmail(response.getString("email"));
                            u.setSessionId(response.getString("sessionId"));
                            User.setCurrentUser(u);
                            if (dialog != null)
                                dialog.cancel();
                            Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                            LoginActivity.this.startActivity(intent);
                        }

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }

                }

            };

            Response.ErrorListener errorListener = new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    error.printStackTrace();
                    Log.d("log2=", error.toString());
                    AlertDialog.Builder builder = new AlertDialog.Builder(LoginActivity.this);
                    builder.setMessage("Erreur de connexion au serveur")
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
        }
    }
}
