package com.example.fabiengamel.candidatemanagement.Models;

/**
 * Created by Fabien gamel on 12/06/2017.
 */
public class Meeting {

    private String note;
    private String link;
    private String xpNote;
    private String nsNote;
    private String jobIdealNote;
    private String pisteNote;
    private String pieCouteNote;
    private String locationNote;
    private String EnglishNote;
    private String nationalityNote;
    private String competences;

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

    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public String getLink() {
        return link;
    }

    public void setLink(String link) {
        this.link = link;
    }

    public String getXpNote() {
        return xpNote;
    }

    public void setXpNote(String xpNote) {
        this.xpNote = xpNote;
    }

    public String getNsNote() {
        return nsNote;
    }

    public void setNsNote(String nsNote) {
        this.nsNote = nsNote;
    }

    public String getJobIdealNote() {
        return jobIdealNote;
    }

    public void setJobIdealNote(String jobIdealNote) {
        this.jobIdealNote = jobIdealNote;
    }

    public String getPisteNote() {
        return pisteNote;
    }

    public void setPisteNote(String pisteNote) {
        this.pisteNote = pisteNote;
    }

    public String getPieCouteNote() {
        return pieCouteNote;
    }

    public void setPieCouteNote(String pieCouteNote) {
        this.pieCouteNote = pieCouteNote;
    }

    public String getLocationNote() {
        return locationNote;
    }

    public void setLocationNote(String locationNote) {
        this.locationNote = locationNote;
    }

    public String getEnglishNote() {
        return EnglishNote;
    }

    public void setEnglishNote(String englishNote) {
        EnglishNote = englishNote;
    }

    public String getNationalityNote() {
        return nationalityNote;
    }

    public void setNationalityNote(String nationalityNote) {
        this.nationalityNote = nationalityNote;
    }

    public String getCompetences() {
        return competences;
    }

    public void setCompetences(String competences) {
        this.competences = competences;
    }
}
