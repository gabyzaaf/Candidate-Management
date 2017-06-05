//
//  DisplayCandidate.swift
//  HR
//
//  Created by zaafrani Gabriel on 31/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit


class DisplayCandidate:  UIViewController,UITableViewDataSource,UITableViewDelegate {

    var user:User = User()
    var array = [Candidate]()
    var index:Int = 0
    
    @IBOutlet weak var tableViewCustom: UITableView!
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
       
        return array.count;
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "cell",for: indexPath) as! ViewControllerTableViewCell
        cell.Lname.text = array[indexPath.row].getName()
        cell.Lfirstname.text = array[indexPath.row].getFirstname()
        cell.Lemail.text = array[indexPath.row].getEmail()
        return (cell)
    }
    
   
  
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath){
        index = indexPath[1]
        getCandidate()
    }

  
    override public func viewDidLoad() {
        super.viewDidLoad()
        self.tableViewCustom.delegate = self
        self.tableViewCustom.dataSource = self
    }
    
    override public func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
        
    }
    
    
    func getCandidate() ->Void{
        
        var candidat = Candidate()
        let url = createURLrequest(url: "http://localhost:5000/api/User/Candidates/search/"+array[index].getEmail()+"/"+user.getToken())
        
        webServiceGet(request: url){(result)->() in
            
            if result.count == 0 {
                print("ko")
            }else{
                for i in 0...(result.count-1) {
                    let dico = result[i] as! NSDictionary
                   
                    
                    var sexe = (dico["sexe"] as! String)
                   
                    
                    
                    candidat = Candidate(_id: "", _nom: dico["nom"] as! String, _prenom: dico["prenom"] as! String, _email: dico["email"] as! String, _englishNote: dico["EnglishNote"] as! String, _ns: dico["NS"] as! String, _action: dico["actions"] as! String, _annee: Int(dico["annee"] as! String)!, _approcheEmail: self.transformStringToBool(text: (dico["approche_email"] as! String)), _competence: dico["competences"] as! String, _crCall: dico["crCall"] as! String, _jobIdealNote: dico["jobIdealNote"] as! String, _lien: dico["lien"] as! String, _link: dico["link"] as! String, _locationNote: dico["locationNote"] as! String, _nationalityNote: dico["nationalityNote"] as! String, _nsNote: dico["nsNote"] as! String, _phone: dico["phone"] as! String, _pieCouteNote: dico["pieCouteNote"] as! String, _pisteNote: dico["pisteNote"] as! String, _sexe: sexe.characters[sexe.characters.startIndex] , _xpNote: dico["xpNote"] as! String)
                
                }
            }
            
        }
        
    }
    
    func transformStringToBool(text:String)->Bool {
        if("True".caseInsensitiveCompare(text) == ComparisonResult.orderedSame){
            return true
        }
        return false
    }
    
    /*
    override func prepare(for segue: UIStoryboardSegue, sender: Any?)   {
        if segue.identifier == "Showcandidates" {
            let vc : DisplayCandidate = segue.destination as! DisplayCandidate
            vc.array = self.array
        }
    }
    */


    


}
