package com.example.fabiengamel.candidatemanagement.Models;

/**
 * Created by Fabien gamel on 07/06/2017.
 */
public class Candidate {

    public int id;
    public String lastname;
    public String firstname;
    public String phone;
    public String email;
    public String sexe;
    public enum actions {interne, enCours, RN,trop_relance, freelance, etranger,
    appelerRemind, aRelancerMail, aRelancerLKD, PAERemind, HcJunior, HcLangue,
    HCGeo, HCSenior, HCPasDev, HCMFST, HCBacMoins5}
    public int annee;
    public String lien;
    public String crCall;
    public String NS;
    public int approche_email;
    public int fid_user_candidate;


}
