package com.example.fabiengamel.candidatemanagement.Requests;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Fabien gamel on 26/06/2017.
 */
public class TimeRequest extends JsonObjectRequest {

    private static final String TIME_REQUEST_URL ="https://ussouthcentral.services.azureml.net/workspaces/5587f0c68bac40fcb8ad1bf09cdc2c99/services/8984cc55e3fa40c0b07601ff8b98969f/execute?api-version=2.0&details=true";
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



    public TimeRequest(String satisfaction, String nbProject, String nbHoursMonth, String accident, String promo, String salary,
                       Response.Listener<JSONObject> listener,Response.ErrorListener errorListener) throws JSONException, UnsupportedEncodingException {
        super(Request.Method.POST, TIME_REQUEST_URL, null, listener, errorListener);

        jsonBody = new JSONObject();
        Inputs = new JSONObject();
        input1 = new JSONObject();
        GlobalParameters = new JSONObject();

        ColumnNames = new JSONArray();
        ColumnNames.put(0, "satisfaction_level");
        ColumnNames.put(1, "number_project");
        ColumnNames.put(2, "average_montly_hours");
        ColumnNames.put(3, "Work_accident");
        ColumnNames.put(4, "left");
        ColumnNames.put(5, "promotion_last_5years");
        ColumnNames.put(6, "salary");


        Values = new JSONArray();
        v = new JSONArray();
        v.put(0, satisfaction);
        v.put(1, nbProject);
        v.put(2, nbHoursMonth);
        v.put(3, accident);
        v.put(4, "0");
        v.put(5, promo);
        v.put(6, salary);
        Values.put(v);

        jsonBody.put("Inputs",Inputs.put("input1", input1.put("ColumnNames", ColumnNames).put("Values", Values))).put("GlobalParameters", GlobalParameters);
        requestBody = jsonBody.toString();

        headers = new HashMap<>();
        headers.put("Authorization", "Bearer maSaoVG9B+8LaCoSpL4nC03qbnobKcQqT2KlOK8YtedY5huPqBG0EmU7tnkeDKIhEvAraFvmOHJHaEif+KURvg==");
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