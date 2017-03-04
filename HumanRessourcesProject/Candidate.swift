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
    
    
    init(lastname:String,firstname:String){
        self.lastname = lastname
        self.firstname = firstname
    }
    
    public func getLastName()->String{
        return self.lastname;
    }
    
    public func getFirstName()->String{
        return self.firstname;
    }
    
    
    
}