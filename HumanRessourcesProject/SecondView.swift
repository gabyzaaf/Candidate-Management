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
    @IBOutlet weak var Isearch: UITextField!
    override func viewDidLoad() {
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
            value = "http://localhost:5000/api/User/Candidates/list/"+Isearch.text!
            var op =  createResult(value)
            displayResultGet(op.value,array: ["name"]){(result)->() in
                if result.count == 0{
                    print("Error the array is empty")
                }else{
                    dispatch_async(dispatch_get_main_queue()){
                    self.ErrorMessage.text=""
                    for index in 0...(result.count-1){
                       self.dico[index] = Candidate(lastname: (result[index]["name"] as? String)!,firstname: (result[index]["firstname"]as? String)!)
                    }
                    
                        for object in self.dico {
                            
                            print("the firstname \((object as! Candidate).getFirstName()), lastname \((object as! Candidate).getLastName())")
                        }
                        self.performSegueWithIdentifier("third", sender: self)
                }
                }
            }

        }
    
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "third"{
            let vc : ThirdView = segue.destinationViewController as! ThirdView
            vc.dictionnary = self.dico
            
        }
    }
    

}