//
//  DisplayView3.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 07/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit


class DisplayView3 : UIViewController{

    @IBOutlet weak var tLocation: UITextView!
    @IBOutlet weak var tEnglish: UITextView!
    @IBOutlet weak var tCompetences: UITextView!
    var dico:NSMutableArray = NSMutableArray()
    
    override func viewDidLoad() {
        for tmpCandidate in dico{
            tLocation.text = (tmpCandidate as? Candidate)?.getlocationNote()
            tEnglish.text = (tmpCandidate as? Candidate)?.getEnglishNote()
            tCompetences.text = (tmpCandidate as? Candidate)?.getCompetences()
        }
        
        super.viewDidLoad()
        
    }

    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    

}