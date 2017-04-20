//
//  Ws.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 02/03/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import Foundation

class Ws{


    var dicto:NSMutableDictionary = NSMutableDictionary();
    
    func displayResult(request:NSMutableURLRequest,json:String,array:NSArray)->Void{
        var dico : NSMutableDictionary
        dico = NSMutableDictionary()
        var element:String
        
        request.HTTPBody=json.dataUsingEncoding(NSUTF8StringEncoding)
        var item:String
        let task = NSURLSession.sharedSession().dataTaskWithRequest(request) {
            data,response,error in
            if(error != nil){
                print("error = \(error)")
                return
            }
            
            let responseString = NSString(data: data!, encoding: NSUTF8StringEncoding)
            print("response data is --> \(responseString)");
            
            var err: NSError?
            do{
                let json = try NSJSONSerialization.JSONObjectWithData(data!, options: .MutableContainers) as? NSDictionary
                
                if let parseJson = json {
                    for element in array as! [String]{
                        dico[element]=parseJson[element] as? String
                        print("value is \(parseJson[element])");
                    }
                   self.dicto = dico
                    
                }
            }catch{
                print(error)
            }
            
        }
        task.resume()
    }





}