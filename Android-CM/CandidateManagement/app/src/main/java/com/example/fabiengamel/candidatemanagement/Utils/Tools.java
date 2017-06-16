package com.example.fabiengamel.candidatemanagement.Utils;

/**
 * Created by Fabien gamel on 15/06/2017.
 */
public class Tools {

    public boolean isEmailValid(CharSequence email) {
        return android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches();
    }
}
