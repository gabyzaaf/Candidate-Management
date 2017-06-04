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
        
        
        let url = createURLrequest(url: "http://localhost:5000/api/User/Candidates/search/"+array[index].getEmail()+"/"+user.getToken())
        
        webServiceGet(request: url){(result)->() in
            print("the candidat numbers is -----> \(result.count)")
            if result.count == 0 {
                print("ko")
            }else{
                print(result)
            }
            
        }
        
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
