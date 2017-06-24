package com.example.fabiengamel.candidatemanagement.Activties;

import android.Manifest;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import android.content.pm.PackageManager;
import android.telephony.SmsManager;


import com.example.fabiengamel.candidatemanagement.R;


public class SMSActivity extends AppCompatActivity {

    EditText etContentSMS;
    TextView tvName;
    TextView tvFirstname;
    TextView tvPhone;
    TextView tvAction;
    Button bSend;
    String phoneNo;
    String message;
    private static final int MY_PERMISSIONS_REQUEST_SEND_SMS =0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sms);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        //Récupération du nom lors de l'activity recherche
        String candidateName= "";
        String phone= "";
        String prenom = "";
        String action = "";
        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                candidateName= null;
                phone = null;
                prenom = null;
                action = null;
            } else {
                candidateName= extras.getString("candidateName");
                phone = extras.getString("candidatePhone");
                prenom = extras.getString("candidateFirstname");
                action = extras.getString("candidateAction");
            }
        } else {
            candidateName= (String) savedInstanceState.getSerializable("candidateName");
        }

        InitContent();
        if(!phone.matches("")){
            tvPhone.setText(phone);
        }
        if(!action.matches("")){
            tvAction.setText(action);
        }
        if(!prenom.matches("")){
            tvFirstname.setText(prenom);
        }
        if(!candidateName.matches("")){
            tvName.setText(candidateName);
        }

    }

    public void InitContent() {

        tvAction = (TextView)findViewById(R.id.tvActionSMS);
        tvFirstname = (TextView)findViewById(R.id.tvPrenomSMS);
        tvName = (TextView)findViewById(R.id.tvNomSMS);
        tvPhone = (TextView)findViewById(R.id.tvPhoneSMS);
        etContentSMS = (EditText)findViewById(R.id.etContentSMS);
        bSend = (Button)findViewById(R.id.bSendSMS);

        bSend.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(!tvPhone.getText().toString().matches("")) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(SMSActivity.this);
                    builder.setTitle("Envoyer : "+etContentSMS.getText().toString());
                    builder.setPositiveButton("Oui",
                            new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog,
                                                    int which) {
                                    phoneNo = tvPhone.getText().toString();
                                    message = etContentSMS.getText().toString();
                                    sendSMS();
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
                else{
                    AlertDialog.Builder builder = new AlertDialog.Builder(SMSActivity.this);
                    builder.setMessage("Erreur : N°Tel non disponible...")
                            .setNegativeButton("Annuler", null)
                            .create()
                            .show();
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


    protected void sendSMS() {

        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.SEND_SMS)
                != PackageManager.PERMISSION_GRANTED) {

            ActivityCompat.requestPermissions(this,
                    new String[]{Manifest.permission.SEND_SMS},
                    MY_PERMISSIONS_REQUEST_SEND_SMS);

        } else {
            SendTextMsg();
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {
            case MY_PERMISSIONS_REQUEST_SEND_SMS: {
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    SendTextMsg();
                } else {
                    Toast.makeText(getApplicationContext(),
                            "L'envoi du SMS a échoué (Revoir le numéro ?)", Toast.LENGTH_LONG).show();
                }
            }
        }
    }

    private void SendTextMsg() {
        SmsManager smsManager = SmsManager.getDefault();
        smsManager.sendTextMessage(phoneNo, null, message, null, null);

        Toast.makeText(getApplicationContext(), "le SMS a été envoyé correctement",
                Toast.LENGTH_LONG).show();
    }

      /*  public String GetSMSContent(String action) {

        User user = User.getCurrentUser();
        final Candidate candidate = new Candidate();


        RequestQueue queue = Volley.newRequestQueue(this);
        String url = APIConstants.BASE_URL+"/api/user/Candidates/recherche/" +nom+"/"+user.sessionId ;

        JsonArrayRequest searchRequest = new JsonArrayRequest(Request.Method.GET, url, null,
                new Response.Listener<JSONArray>()
                {
                    public static final String TAG ="Get email template : " ;

                    @Override
                    public void onResponse(JSONArray response) {
                        Log.d("Response", response.toString());
                        try {
                            for (int i = 0; i < response.length(); i++) {
                                JSONObject jsonOBject = response.getJSONObject(i);
                                if(jsonOBject.has("success"))
                                {

                                }
                                else {



                                }

                            }
                        }catch(JSONException e){
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


        return "todo";
    }*/

}
