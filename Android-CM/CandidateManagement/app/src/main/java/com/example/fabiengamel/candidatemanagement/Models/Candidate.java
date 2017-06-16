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
   /* public enum actions {interne, enCours, RN,trop_relance, freelance, etranger,
    appelerRemind, aRelancerMail, aRelancerLKD, PAERemind, HcJunior, HcLangue,
    HCGeo, HCSenior, HCPasDev, HCMFST, HCBacMoins5}*/
    public String actions;
    public int annee;
    public String lien;
    public String crCall;
    public String NS;
    public boolean approche_email;
    public int fid_user_candidate;
    public int prix;
    public String zipcode;

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

}
