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
    var token:String = ""
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
        let user = User(email: IEmail.text!,password: IPassword.text!)
        let op =  createResult("http://localhost:5000/api/user/admin/auth/")
        displayResult(op.value,json: serializeJson(user.toDico()),array: ["email","password","sessionId"]){(result)->() in
            if result["sessionId"] != nil{
                print("Is ok")
              
                dispatch_async(dispatch_get_main_queue()){
                    self.token = result["sessionId"] as! String
                    self.performSegueWithIdentifier("id", sender: self)
                    self.IMessage.text = ""
                    
                }
            }else{
                dispatch_async(dispatch_get_main_queue()){
                self.IMessage.text = String(result["content"]!)
                }
            }
            
        }
    
    }
    

    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
            if segue.identifier == "id"{
                let vc : SecondView = segue.destinationViewController as! SecondView
                print("self token is ----> \(self.token)")
                vc.token = self.token
            }
        }

}


