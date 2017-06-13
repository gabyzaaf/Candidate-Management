package com.example.fabiengamel.candidatemanagement.Requests;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Fabien gamel on 07/06/2017.
 */
public class UpdateRequest extends JsonObjectRequest {

    private static final String UPDATE_REQUEST_URL = "http://192.168.1.17:5000/api/user/update/candidat/";
    Map<String, String> headers;
    JSONObject jsonBody;
    String requestBody;


    public UpdateRequest(String sessionId, String Name, String Firstname, String mail,String phone, String sexe, String action,
                      int year, String link, String crCall, String ns, Boolean email, int prix, Response.Listener<JSONObject> listener,
                      Response.ErrorListener errorListener) throws JSONException {
        super(Request.Method.POST, UPDATE_REQUEST_URL, null, listener, errorListener);

        jsonBody = new JSONObject();
        jsonBody.put("session_id", sessionId);
        jsonBody.put("Name", Name);
        jsonBody.put("Firstname", Firstname);
        jsonBody.put("emailAdress", mail);
        jsonBody.put("phone", phone);
        jsonBody.put("sexe", sexe);
        jsonBody.put("action", action);
        jsonBody.put("year", year);
        jsonBody.put("link", link);
        jsonBody.put("crCall", crCall);
        jsonBody.put("ns", ns);
        jsonBody.put("email", email);
        jsonBody.put("idependant", prix);
        requestBody = jsonBody.toString();

        headers = new HashMap<>();
        headers.put("Content-Type", "application/json");
    }

    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        return headers;
    }

    @Override
    public byte[] getBody() /*throws AuthFailureError */{
        try {
            return requestBody == null ? null : requestBody.getBytes("utf-8");
        } catch (UnsupportedEncodingException uee) {
            VolleyLog.wtf("Unsupported Encoding while trying to get the bytes of %s using %s", requestBody, "utf-8");
            return null;
        }
    }

}
