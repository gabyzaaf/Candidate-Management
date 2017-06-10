package com.example.fabiengamel.candidatemanagement.Requests;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Fabien gamel on 07/06/2017.
 */
public class AddRequest extends JsonObjectRequest {
    private static final String ADD_REQUEST_URL = "http://192.168.1.17:5000/api/user/add/candidat/";
    Map<String, String> headers;
    JSONObject jsonBody;
    String requestBody;


    public AddRequest(String sessionId, String Name, String Firstname, String mail,String phone, String sexe, String action,
                      int year, String link, String crCall, String ns, Boolean email, Response.Listener<JSONObject> listener,
                      Response.ErrorListener errorListener) throws JSONException {
        super(Request.Method.POST, ADD_REQUEST_URL, null, listener, errorListener);

        jsonBody = new JSONObject();
        jsonBody.put("session_id", sessionId);
        jsonBody.put("Name", Name);
        jsonBody.put("Firstname", Firstname);
        jsonBody.put("emailAdress", mail);
        jsonBody.put("phone", phone);
        jsonBody.put("sexe", sexe);
        jsonBody.put("actions", action);
        jsonBody.put("year", year);
        jsonBody.put("link", link);
        jsonBody.put("crCall", crCall);
        jsonBody.put("ns", ns);
        jsonBody.put("email", email);
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
