//
//  Candidate.swift
//  HR
//
//  Created by zaafrani Gabriel on 31/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit


public class Candidate{


    private var id:String
    private var name:String
    private var firstname:String
    private var email:String
    
    private var englishNote:String
    private var NS:String
    private var action:String
    private var annee:Int
    private var approcheEmail:Bool
    private var competence:String
    private var crCall:String
    private var jobIdealNote:String
    private var lien:String
    private var link:String
    private var locationNote:String
    private var nationalityNote:String
    private var nsNote:String
    private var phone:String
    private var pieCouteNote:String
    private var pisteNote:String
    private var sexe:Character
    private var xpNote:String
    
   
    
    public init(_id:String="",_nom:String="",_prenom:String="",_email:String="",_englishNote:String="",_ns:String="",_action:String="",_annee:Int=0,_approcheEmail:Bool=false,_competence:String="",_crCall:String="",_jobIdealNote:String="",_lien:String="",_link:String="",_locationNote:String="",_nationalityNote:String="",_nsNote:String="",_phone:String="",_pieCouteNote:String="",_pisteNote:String="",_sexe:Character="e",_xpNote:String=""){
        self.id = _id
        self.name = _nom
        self.firstname = _prenom
        self.email = _email
        
        self.englishNote = _englishNote
        self.NS = _ns
        self.action = _action
        self.annee = _annee
        self.approcheEmail = _approcheEmail
        self.competence = _competence
        self.crCall = _crCall
        self.jobIdealNote = _jobIdealNote
        self.lien = _lien
        self.link = _link
        self.locationNote = _locationNote
        self.nationalityNote = _nationalityNote
        self.nsNote = _nsNote
        self.phone = _phone
        self.pieCouteNote = _pieCouteNote
        self.pisteNote = _pisteNote
        self.sexe = _sexe
        self.xpNote = _xpNote
    }

    
    public func getId()->String{
        return self.id
    }
    
    public func getName()->String{
        return self.name
    }
    
    public func getFirstname()->String{
        return self.firstname
    }
    
    public func getEmail()->String{
        return self.email
    }
    
    public func getEnglishNote()->String{
        return self.englishNote
    }
    
    public func getNs()->String{
        return self.NS
    }
    
    public func getAction()->String{
        return self.action
    }

    public func getAnnee()->Int{
        return self.annee
    }

    public func getApprocheEmail()->Bool{
        return self.approcheEmail
    }

    public func getCompetence()->String{
        return self.competence
    }
    
    public func getCrCall()->String{
        return self.crCall
    }

    public func getJobIdealNote()->String{
        return self.jobIdealNote
    }

    public func getLien()->String{
        return self.lien
    }
    
    public func getLink()->String{
        return self.link
    }
    
    
    public func getLocationNote()->String{
        return self.locationNote
    }

    public func getNationalityNote()->String{
        return self.nationalityNote
    }

    public func getNsNote()->String{
        return self.nsNote
    }

    public func getPhone()->String{
        return self.phone
    }

    public func getPieCoutNote()->String{
        return self.pieCouteNote
    }
    
    public func getPisteNote()->String{
        return self.crCall
    }

    public func getSexe()->Character{
        return self.sexe
    }

    public func getXpNote()->String{
        return self.xpNote
    }


}
