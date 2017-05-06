//
//  DisplayCandidate.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 29/04/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//




import UIKit

class DisplayCandidate: UIViewController{
    
    
 
    @IBOutlet weak var Lnom: UILabel!
    @IBOutlet weak var Lprenom: UILabel!
    var email:String = String()
    var token:String = String()
    @IBOutlet weak var LxpNote: UITextView!
    @IBOutlet weak var LnsNote: UITextView!
     var dico:NSMutableArray = NSMutableArray()
     var content:String = ""
    var found:Bool = false
    @IBOutlet weak var Linformation: UILabel!
    override func viewDidLoad() {
        print("*********** le token est --> \(self.token) -- l'email \(self.email)");
        var value:String
        value = "http://localhost:5000/api/User/Candidates/search/"+self.email+"/"+self.token+""
        var op =  createResult(value)
        displayResultGet(op.value,array: ["name"]){(result)->() in
            if result.count == 0{
                print("Error the array is empty")
            }else{
                dispatch_async(dispatch_get_main_queue()){
                    self.Linformation.text="Informations Candidat"
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
                            var tmp:Candidate = Candidate(id:0,lastname: (result[index]["nom"] as? String)!,firstname: (result[index]["prenom"]as? String)!,email: (result[index]["email"]as? String)!,xpNote:(result[index]["xpNote"]as? String),
                                NS:(result[index]["nsNote"]as? String),jobIdealNote:(result[index]["jobIdealNote"]as? String),pisteNote:(result[index]["pisteNote"]as? String),
                                pieCouteNote:(result[index]["pieCouteNote"]as? String),
                                locationNote:(result[index]["locationNote"]as? String),
                                EnglishNote:(result[index]["EnglishNote"]as? String),
                                competences:(result[index]["competences"]as? String))
                            
                            self.dico.insertObject(tmp, atIndex: index)
                            //self.dico[index]
                        }
                    }
                    if self.found == false{
                        self.Linformation.text = self.content
                        
                    }
                    self.assignValueToObject();
                }
            }
        }
        print("BEFORE SUPER value -----> \(self.dico.count)");
        
        super.viewDidLoad()
                // Do any additional setup after loading the view, typically from a nib.
    }
    
    @IBAction func Bnext(sender: AnyObject) {
        performSegueWithIdentifier("segueView2", sender: self)
    }
    
    func assignValueToObject(){
        for candidate in self.dico {
            print("candidate name is  ----> \((candidate as! Candidate).getLastName())")
            Lnom.text = (candidate as! Candidate).getLastName()
            Lprenom.text = (candidate as! Candidate).getFirstName()
            LxpNote.text = (candidate as! Candidate).getXpNote()
            LnsNote.text = (candidate as! Candidate).getNsNote()
        }
    }
    
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "segueView2"{
            let vc : DisplayView2 = segue.destinationViewController as!DisplayView2
            vc.dico = self.dico
            
            
        }
    }

}