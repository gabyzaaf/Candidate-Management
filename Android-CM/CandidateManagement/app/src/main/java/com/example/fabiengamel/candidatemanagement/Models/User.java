package com.example.fabiengamel.candidatemanagement.Models;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Fabien gamel on 06/06/2017.
 */
public class User {

    private String email;
    private String name;
    private String sessionId;

    private User()
    {
    }

    private  static User currentUser = null;
    public static User getCurrentUser()
    {
        if(currentUser == null)
        {
            currentUser = new User();
        }
        return currentUser;
    }

    public static void setCurrentUser(User user)
    {
        currentUser = user;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSessionId() {
        return sessionId;
    }

    public void setSessionId(String sessionId) {
        this.sessionId = sessionId;
    }
}
