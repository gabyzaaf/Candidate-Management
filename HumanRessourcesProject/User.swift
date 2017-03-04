//
//  User.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 27/02/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import Foundation

import Foundation

class User{
    
    var email:String;
    var password:String;
    private var dico:NSMutableDictionary;
    
    init(email:String ,password:String){
        self.email = email;
        self.password = password;
        self.dico = NSMutableDictionary()
        self.dico.setValue(self.email, forKey: "email")
        self.dico.setValue(self.password, forKey: "password")
        
    }
    
    func toDico()->NSMutableDictionary{
        return self.dico
    }
}