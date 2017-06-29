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
public class PredictSalaryRequest extends JsonObjectRequest {

    private static final String SALARY_REQUEST_URL ="https://ussouthcentral.services.azureml.net/workspaces/5587f0c68bac40fcb8ad1bf09cdc2c99/services/7d6a742eefd743b3a28c633b653d6809/execute?api-version=2.0&details=true";
    Map<String, String> headers;
    Map<String, String> params;
    JSONObject jsonBody;
    JSONObject Inputs;
    JSONObject input1;
    JSONObject GlobalParameters;
    JSONArray ColumnNames;
    JSONArray Values;
    JSONArray v;
    String requestBody;



    public PredictSalaryRequest(String nbProject, String nbHoursMonth, String companyTime, String accident, String promo,
                                Response.Listener<JSONObject> listener,Response.ErrorListener errorListener) throws JSONException, UnsupportedEncodingException {
        super(Request.Method.POST, SALARY_REQUEST_URL, null, listener, errorListener);

        jsonBody = new JSONObject();
        Inputs = new JSONObject();
        input1 = new JSONObject();
        GlobalParameters = new JSONObject();

        ColumnNames = new JSONArray();
        ColumnNames.put(0, "number_project");
        ColumnNames.put(1, "average_montly_hours");
        ColumnNames.put(2, "time_spend_company");
        ColumnNames.put(3, "Work_accident");
        ColumnNames.put(4, "promotion_last_5years");

        Values = new JSONArray();
        v = new JSONArray();

        v.put(0, nbProject);
        v.put(1, nbHoursMonth);
        v.put(2, companyTime);
        v.put(3, accident);
        v.put(4, promo);
        Values.put(v);

        jsonBody.put("Inputs",Inputs.put("input1", input1.put("ColumnNames", ColumnNames).put("Values", Values))).put("GlobalParameters", GlobalParameters);
        requestBody = jsonBody.toString();

        headers = new HashMap<>();
        headers.put("Authorization", "Bearer kCOq3e2iLBwBIzQY5vOG+FNbYSNrwSIvfmBi5xp+a3AhMsAKC8WSh1WKuMxmEQChYAaC8fQRRlYDrfV6OMTPrw==");
    }

    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        return headers;
    }


    @Override
    public byte[] getBody() {
        try {
            return requestBody.getBytes("utf-8");
        } catch (UnsupportedEncodingException uee) {
            VolleyLog.wtf("Unsupported Encoding while trying to get the bytes of %s using %s", requestBody, "utf-8");
            return null;
        }
    }

}
