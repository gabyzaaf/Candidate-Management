//
//  User.swift
//  HR
//
//  Created by zaafrani Gabriel on 30/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import Foundation

class User{

    private var email:String
    private var token:String

    public init(_email:String="",_token:String=""){
        self.email = _email
        self.token = _token
    }


    func getEmail()->String{
        return self.email
    }
    
    func getToken()->String{
        return self.token
    }

}
