package com.example.fabiengamel.candidatemanagement.Models;

/**
 * Created by Fabien gamel on 12/06/2017.
 */
public class Meeting {

    public String note;
    public String link;
    public String xpNote;
    public String nsNote;
    public String jobIdealNote;
    public String pisteNote;
    public String pieCouteNote;
    public String locationNote;
    public String EnglishNote;
    public String nationalityNote;
    public String competences;

    private  static Meeting currentMeeting = null;

    public static Meeting getCurrentMeeting()
    {
        if(currentMeeting == null)
        {
            currentMeeting = new Meeting();
        }
        return currentMeeting;
    }

    public static void setCurrentMeeting(Meeting meeting)
    {
        currentMeeting = meeting;
    }
}
