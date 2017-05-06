//
//  DisplayView2.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 07/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit


class DisplayView2 : UIViewController{
    var dico:NSMutableArray = NSMutableArray()
    
    
    
    @IBOutlet weak var tIdeal: UITextView!
    
    @IBOutlet weak var tPiste: UITextView!
    
     @IBOutlet weak var tPie: UITextView!
    override func viewDidLoad() {

        for tmpCandidate in dico{
            self.tIdeal.text = (tmpCandidate as? Candidate)?.getjobIdealNote()
            self.tPiste.text =  (tmpCandidate as? Candidate)?.getpisteNote()
            self.tPie.text = (tmpCandidate as? Candidate)?.getpieCouteNote()
        }
        super.viewDidLoad()
    }
   
    @IBAction func BnextView(sender: AnyObject) {
            performSegueWithIdentifier("showSegue3", sender: self)
    }

    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "showSegue3"{
            let vc : DisplayView3 = segue.destinationViewController as!DisplayView3
            vc.dico = self.dico
            
            
        }
    }
    



}
