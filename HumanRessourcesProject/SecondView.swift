//
//  SecondView.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 27/02/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//


import UIKit

class SecondView: UIViewController{

    var dico:NSMutableArray = NSMutableArray()
    @IBOutlet weak var ErrorMessage: UILabel!
    var token:String!
    var content:String = ""
    var found:Bool = false
    @IBOutlet weak var Isearch: UITextField!
    override func viewDidLoad() {
         print("the token is \(self.token)")
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func Bsearch(sender: AnyObject) {
       
        ErrorMessage.textColor=UIColor.redColor()
        
        var search:String  = Isearch.text!
        if (search.isEmpty) {
            ErrorMessage.text="Error the content is empty"
        }else{
            var value:String
            value = "http://localhost:5000/api/User/Candidates/recherche/mobile/"+Isearch.text!+"/"+self.token
            var op =  createResult(value)
            displayResultGet(op.value,array: ["name"]){(result)->() in
                if result.count == 0{
                    print("Error the array is empty")
                }else{
                    dispatch_async(dispatch_get_main_queue()){
                    self.ErrorMessage.text=""
                        if(self.dico.count != 0){
                            self.dico.removeAllObjects();
                        }
                    for index in 0...(result.count-1){
                        var cont = result[index]
                        if (cont.objectForKey("content") != nil) {
                            self.content = result[index]["content"] as! String
                            self.found = false;
                            print(self.content)
                        }else{
                            self.found = true
                            var tmpCandidate:Candidate = Candidate(id:(Int((result[index]["id"] as? String)!))!,lastname:(result[index]["nom"] as? String)!,firstname:(result[index]["prenom"] as? String)!,email:(result[index]["email"] as? String)!)
                           
                            self.dico.insertObject(tmpCandidate, atIndex: index)

                           
                        }
                    }
                        if self.found == true{
                            for object in self.dico {
                             print("the firstname \((object as! Candidate).getFirstName()), email \((object as! Candidate).getEmail())")
                        }
                                self.performSegueWithIdentifier("third", sender: self)
                        }else{
                            self.ErrorMessage.text = self.content
                        }
                }
                }
            }

        }
    
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "third"{
            let vc : ThirdView = segue.destinationViewController as! ThirdView
            vc.dictionnary = self.dico
            vc.token = self.token
            
        }
    }
    

}