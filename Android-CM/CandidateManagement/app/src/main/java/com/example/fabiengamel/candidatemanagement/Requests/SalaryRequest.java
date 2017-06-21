package com.example.fabiengamel.candidatemanagement.Requests;

import android.app.AlertDialog;
import android.text.InputType;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.example.fabiengamel.candidatemanagement.Activties.PredictSalaryActivity;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Fabien gamel on 18/06/2017.
 */
public class SalaryRequest extends JsonObjectRequest {

    private static final String SALARY_REQUEST_URL ="https://ussouthcentral.services.azureml.net/workspaces/5587f0c68bac40fcb8ad1bf09cdc2c99/services/7d6a742eefd743b3a28c633b653d6809/score";
    Map<String, String> headers;
    Map<String, String> params;
    JSONObject jsonBody;
    JSONObject Instance;
    JSONObject FeatureVector;
    JSONObject GlobalParameters;
    String requestBody;



    public SalaryRequest(String nbProject, String nbHoursMonth, String companyTime, String accident, String promo,
                                Response.Listener<JSONObject> listener,Response.ErrorListener errorListener) throws JSONException, UnsupportedEncodingException {
        super(Request.Method.POST, SALARY_REQUEST_URL, null, listener, errorListener);

        jsonBody = new JSONObject();
        jsonBody.put("Id", "score00001");
        Instance = new JSONObject();
        FeatureVector = new JSONObject();
        FeatureVector.put("number_project", "0");
        FeatureVector.put("average_montly_hours", "0");
        FeatureVector.put("time_spend_company", "0");
        FeatureVector.put("Work_accident", "0");
        FeatureVector.put("promotion_last_5years", "0");
        GlobalParameters = new JSONObject();

        jsonBody.put("Instance", Instance.put("FeatureVector", FeatureVector).put("GlobalParameters",GlobalParameters));
        requestBody = jsonBody.toString();
        headers = new HashMap<>();
        headers.put("Content-Type", "application/json");
        headers.put("Authorization", "Bearer kCOq3e2iLBwBIzQY5vOG+FNbYSNrwSIvfmBi5xp+a3AhMsAKC8WSh1WKuMxmEQChYAaC8fQRRlYDrfV6OMTPrw==");

    }

    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        return headers;
    }

    @Override
    public byte[] getBody() /*throws AuthFailureError*/ {
        try {
            return requestBody.getBytes("utf-8");
        } catch (UnsupportedEncodingException uee) {
            VolleyLog.wtf("Unsupported Encoding while trying to get the bytes of %s using %s", requestBody, "utf-8");
            return null;
        }
    }

}
