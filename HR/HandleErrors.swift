//
//  HandleErrors.swift
//  HR
//
//  Created by zaafrani Gabriel on 03/06/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit




func handleAction(label:UILabel,message:String)->(){
    label.textColor = UIColor.red
    label.text = message
}

func handleInput(textField:UITextField,label:UILabel)->Bool{
    if textField.text == "" {
        handleAction(label: label, message: "votre champ est vide")
        return false
    }
    return true
}
