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
 * Created by Fabien gamel on 12/06/2017.
 */
public class UpdateReportRequest extends JsonObjectRequest {

    private static final String UPDATE_REPORT_REQUEST_URL ="http://192.168.1.17:5000/api/user/update/candidat/report";
    Map<String, String> headers;
    JSONObject jsonBody;
    String requestBody;

    public UpdateReportRequest(String mail, String sessionId,String note, String link, String xpNote, String nsNote, String jobIdealNote,
                            String pisteNote, String pieCouteNote, String locationNote, String EnglishNote, String nationalityNote, String competences,
                            Response.Listener<JSONObject> listener,Response.ErrorListener errorListener) throws JSONException {
        super(Request.Method.POST, UPDATE_REPORT_REQUEST_URL, null, listener, errorListener);

        jsonBody = new JSONObject();
        jsonBody.put("emailCandidat", mail);
        jsonBody.put("sessionId", sessionId);
        jsonBody.put("note", note);
        jsonBody.put("link", link);
        jsonBody.put("xpNote", xpNote);
        jsonBody.put("nsNote", nsNote);
        jsonBody.put("jobIdealNote", jobIdealNote);
        jsonBody.put("pisteNote", pisteNote);
        jsonBody.put("pieCouteNote", pieCouteNote);
        jsonBody.put("locationNote", locationNote);
        jsonBody.put("EnglishNote", EnglishNote);
        jsonBody.put("nationalityNote", nationalityNote);
        jsonBody.put("competences", competences);
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
