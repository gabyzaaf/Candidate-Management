//
//  SearchCandidate.swift
//  HR
//
//  Created by zaafrani Gabriel on 30/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit



class SearchCandidate:  UIViewController {

    var user = User()
    var array = [Candidate]()
    var found:Bool = true
    
    @IBOutlet weak var Lerror: UILabel!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        print(user.getEmail())
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
        
    }

    @IBOutlet weak var Tsearch: UITextField!

    @IBAction func Bsearch(_ sender: Any) {
        if handleInput(textField: Tsearch, label: Lerror) {
        let url = createURLrequest(url: "http://localhost:5000/api/user/Candidates/recherche/mobile/"+Tsearch.text!+"/"+user.getToken()+"")
        
        webServiceGet(request: url){(result)->() in
            print("the candidat numbers is -----> \(result.count)")
            if result.count == 0 {
                print("ko")
            }else{
                self.array.removeAll()
                for index in 0...(result.count-1) {
                    let dico = result[index] as! NSDictionary
                    if dico["success"] != nil {
                        handleAction(label: self.Lerror, message: "votre recherche n'existe pas")
                        self.found = false
                    }else{
                        self.array.append(Candidate(_id: dico["id"] as! String,_nom: dico["nom"] as! String,_prenom: dico["prenom"] as! String,_email: dico["email"] as! String))
                        self.found = true
                    }
                }
                
                if self.found == true {
                    OperationQueue.main.addOperation {
                        self.performSegue(withIdentifier: "Showcandidates", sender: self)
                    }
                }
                
                
            }
            
          }
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?)   {
        if segue.identifier == "Showcandidates" {
          let vc : DisplayCandidate = segue.destination as! DisplayCandidate
            vc.array = self.array
            vc.user = self.user
        }
    }
    
    }



