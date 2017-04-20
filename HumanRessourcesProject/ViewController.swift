//
//  ViewController.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 27/02/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    @IBOutlet weak var IEmail: UITextField!
    
    @IBOutlet weak var IMessage: UILabel!
    @IBOutlet var ILabel: UIView!
    
    @IBOutlet weak var IPassword: UITextField!
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    @IBAction func Bconnexion(sender: AnyObject) {
        var user = User(email: IEmail.text!,password: IPassword.text!)
        var op =  createResult("http://localhost:5000/api/User/Authentification")
        displayResult(op.value,json: serializeJson(user.toDico()),array: ["email","password","sessionId"]){(result)->() in
            if result["sessionId"] != nil{
                print("Is ok")
                dispatch_async(dispatch_get_main_queue()){
                    self.performSegueWithIdentifier("id", sender: self)
                }
            }else{
                print("Is ko")
                self.IMessage.text = "its ko"
            }
            
        }
        

     
        
        
        
    }
    
    


    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
            if segue.identifier == "id"{
                let vc : SecondView = segue.destinationViewController as! SecondView
                
            }
        }

}


