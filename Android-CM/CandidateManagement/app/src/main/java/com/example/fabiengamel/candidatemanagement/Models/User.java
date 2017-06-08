package com.example.fabiengamel.candidatemanagement.Models;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Fabien gamel on 06/06/2017.
 */
public class User {

    public String email;
    public String name;
    public String sessionId;

    public User()
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
}
