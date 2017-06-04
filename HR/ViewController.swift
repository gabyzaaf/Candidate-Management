//
//  ViewController.swift
//  HR
//
//  Created by zaafrani Gabriel on 29/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    @IBOutlet weak var Lerror: UILabel!
    var user = User();
    override func viewDidLoad() {
        super.viewDidLoad()
        Lerror.text = ""
        
        // Do any additional setup after loading the view, typically from a nib.
    }
    
    
    
    @IBOutlet weak var Tlogin: UITextField!

    @IBOutlet weak var Tpassword: UITextField!
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    


    @IBAction func Bvalidator(_ sender: Any) {
        
        let urlString = "http://localhost:5000/api/user/admin/auth/"
        var data = Data()
        do{
            let finalDict  = NSMutableDictionary()
            // finalDict.setValue(infoValue, forKey: "info")
            
            finalDict.setValue(Tlogin.text, forKey: "email")
            finalDict.setValue(Tpassword.text, forKey: "password")
            
            let newdata = try JSONSerialization.data(withJSONObject:finalDict , options: [])
            let newdataString = String(data: newdata, encoding: String.Encoding.utf8)!
            print(newdataString)
            
            data = newdataString.data(using: .utf8)!
            
            let another =     try JSONSerialization.jsonObject(with: data, options: []) as? NSDictionary as! [String : AnyObject]? as NSDictionary?
            print(another!)
            
        }
        catch let error as NSError {
            print(error)
        }
        
        getWebservices(urlString: urlString, httpMethod: "POST", data: data) { (response) in
            print("the response is \(response)")
            let mainData = response["sessionId"] as? String
            
            if mainData == nil
            {
                OperationQueue.main.addOperation {
                    handleAction(label: self.Lerror,message: response["content"] as! String)
                }
            }
            else
            {
                self.user = User(_email: response["email"] as! String,_token: response["sessionId"] as! String);
                
                OperationQueue.main.addOperation {
                    self.performSegue(withIdentifier: "idAuth", sender: self)
                }
            }
        }
    
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?)   {
        if segue.identifier == "idAuth"{
            let sc : SearchCandidate = segue.destination as! SearchCandidate
            sc.user = self.user
        }
    }
    
    
    
    
}

