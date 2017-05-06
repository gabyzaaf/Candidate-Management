//
//  ThirdView.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 04/03/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit

class ThirdView: UIViewController,UITableViewDataSource,UITableViewDelegate{
    
    var token:String = String()
    var email:String = String()
    @IBOutlet weak var tableView: UITableView!
    var dictionnary:NSMutableArray = [];
    var tmpCandidate:Candidate = Candidate();
    var paste:Int = 0;
    var candidate:Candidate = Candidate();
    override func viewDidLoad() {
        super.viewDidLoad()
        self.tableView.dataSource = self
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func tableView(tableView:UITableView, numberOfRowsInSection section:Int) -> Int
    {
       
        return dictionnary.count
    }
    
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell
    {
        var cell = self.tableView.dequeueReusableCellWithIdentifier("mycell",forIndexPath: indexPath) as! TableViewCustom
        cell.nom.text = (dictionnary[paste] as! Candidate).getLastName()
        cell.email.text = (dictionnary[paste] as! Candidate).getEmail()
        paste++
        return cell
    }

    func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath) {
       
        
        let currentCell = tableView.cellForRowAtIndexPath(indexPath)! as! TableViewCustom
        self.candidate = Candidate(id: 0,lastname: currentCell.nom.text!,firstname: "",email: currentCell.email.text)
         self.performSegueWithIdentifier("candidateShow", sender: self)
        
    
    }

    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "candidateShow"{
            let vc : DisplayCandidate = segue.destinationViewController as!DisplayCandidate
            print("\(self.candidate.getLastName())");
            vc.email = self.candidate.getEmail()
            vc.token = self.token
          
            
        }
    }
    
}
