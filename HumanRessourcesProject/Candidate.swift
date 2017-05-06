//
//  Candidate.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 05/03/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import Foundation


class Candidate{

    private var lastname:String
    private var firstname:String
    private var id:Int
    private var sexe:String
    private var phone:String
    private var email:String
    private var actions:String
    private var annee:String
    private var lien:String
    private var crCall:String
    private var NS:String
    private var approche_email:String
    private var note:Int
    private var link:String
    private var xpNote:String
    private var jobIdealNote:String
    private var pisteNote:String
    private var pieCouteNote:String
    private var locationNote:String
    private var EnglishNote:String
    private var nationalityNote:String
    private var competences:String
    
    init(id:Int=0,lastname:String="",firstname:String="",email:String?="",sexe:String?="",phone:String?="",actions:String?="",annee:String?="",lien:String?="",crCall:String?="",NS:String?="",approche_email:String?="",note:Int?=0,link:String?="",xpNote:String?="",jobIdealNote:String?="",pisteNote:String?="",pieCouteNote:String?="",locationNote:String?="",EnglishNote:String?="",nationalityNote:String?="",competences:String?=""){
        self.id = id
        self.lastname = lastname
        self.firstname = firstname
        self.sexe = sexe!
        self.phone = phone!
        self.email = email!
        self.actions = actions!
        self.annee = annee!
        self.lien = lien!
        self.crCall = crCall!
        self.NS = NS!
        self.approche_email = approche_email!
        self.note = note!
        self.link = link!
        self.xpNote = xpNote!
        self.jobIdealNote = jobIdealNote!
        self.pisteNote = pisteNote!
        self.pieCouteNote = pieCouteNote!
        self.locationNote = locationNote!
        self.EnglishNote = EnglishNote!
        self.nationalityNote = nationalityNote!
        self.competences = competences!
    }
    
    public func getLastName()->String{
        return self.lastname;
    }
    
    public func getFirstName()->String{
        return self.firstname;
    }
    
    public func getEmail()->String{
        return self.email;
    }
    
    public func getXpNote()->String{
        return self.xpNote;
    }
    
    public func getNsNote()->String{
        return self.NS
    }
    
    public func getjobIdealNote()->String{
        return self.jobIdealNote
    }
    
    public func getpisteNote()->String{
        return self.pisteNote
    }
    
    public func getpieCouteNote()->String{
        return self.pieCouteNote
    }
    
    public func getlocationNote()->String{
        return self.locationNote
    }
    
    public func getEnglishNote()->String{
        return self.EnglishNote
    }
    
    public func getCompetences()->String{
        return self.competences
    }
}