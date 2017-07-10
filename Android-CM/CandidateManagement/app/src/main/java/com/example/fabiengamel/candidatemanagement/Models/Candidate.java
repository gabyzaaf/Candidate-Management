package com.example.fabiengamel.candidatemanagement.Models;

/**
 * Created by Fabien gamel on 07/06/2017.
 */
public class Candidate {

    private int id;
    private String lastname;
    private String firstname;
    private String phone;
    private String email;
    private String sexe;
    private String actions;
    private int annee;
    private String lien;
    private String crCall;
    private String NS;
    private String prix;
    private String zipcode;

    private Candidate () {

    }
    private  static Candidate currentCandidate = null;

    public static Candidate getCurrentCandidate()
    {
        if(currentCandidate == null)
        {
            currentCandidate = new Candidate();
        }
        return currentCandidate;
    }

    public static void setCurrentCandidate(Candidate candidate)
    {
        currentCandidate = candidate;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getLastname() {
        return lastname;
    }

    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public String getFirstname() {
        return firstname;
    }

    public void setFirstname(String firstname) {
        this.firstname = firstname;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getSexe() {
        return sexe;
    }

    public void setSexe(String sexe) {
        this.sexe = sexe;
    }

    public String getActions() {
        return actions;
    }

    public void setActions(String actions) {
        this.actions = actions;
    }

    public int getAnnee() {
        return annee;
    }

    public void setAnnee(int annee) {
        this.annee = annee;
    }

    public String getLien() {
        return lien;
    }

    public void setLien(String lien) {
        this.lien = lien;
    }

    public String getCrCall() {
        return crCall;
    }

    public void setCrCall(String crCall) {
        this.crCall = crCall;
    }

    public String getNS() {
        return NS;
    }

    public void setNS(String NS) {
        this.NS = NS;
    }

    public String getPrix() {
        return prix;
    }

    public void setPrix(String prix) {
        this.prix = prix;
    }

    public String getZipcode() {
        return zipcode;
    }

    public void setZipcode(String zipcode) {
        this.zipcode = zipcode;
    }
}
