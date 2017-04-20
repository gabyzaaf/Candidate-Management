//
//  functions.swift
//  HumanRessourcesProject
//
//  Created by zaafrani Gabriel on 27/02/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import Foundation

import Foundation

func serializeJson(para:NSMutableDictionary)->String{
    let jsonData = try! NSJSONSerialization.dataWithJSONObject(para, options: NSJSONWritingOptions())
    let jsonString = NSString(data: jsonData, encoding: NSUTF8StringEncoding) as! String
    return jsonString
}


func createResult(url:String)->(value:NSMutableURLRequest,function:(NSMutableURLRequest,String,NSArray,completion:(result: NSDictionary)->())->Void){
    let myurl = NSURL(string: url )
    let request = NSMutableURLRequest(URL:myurl!)
    request.setValue("application/json; charset=utf-8", forHTTPHeaderField:"Content-Type")
    
    return (request,displayResult)
}

func getDico(dico:NSDictionary) -> NSDictionary{
    return dico
}




func getBoolValue(number : Int, completion: (result: Bool)->()) {
    if number > 5 {
        completion(result: true)
    } else {
        completion(result: false)
    }
}

func displayResult(request:NSMutableURLRequest,json:String,array:NSArray,completion:(result: NSDictionary)->()){
    request.HTTPMethod="POST"
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
                completion(result: dico)
                
            }
        }catch{
            print(error)
        }
        
    }
    
    task.resume()

}



func displayResultGet(request:NSMutableURLRequest,array:NSArray,completion:(result: NSArray)->()){
    request.HTTPMethod="GET"
    var dico : NSMutableDictionary
    dico = NSMutableDictionary()
    var element:String
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
             var errori: NSError?
        let jsonArr = try NSJSONSerialization.JSONObjectWithData(data!, options: NSJSONReadingOptions.AllowFragments) as! NSArray
        print(jsonArr.count)
         
            completion(result: jsonArr)
            
        }catch{
            print(error)
        }
        
    }
    
    task.resume()
    
}

