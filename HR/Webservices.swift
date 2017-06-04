//
//  Webservices.swift
//  HR
//
//  Created by zaafrani Gabriel on 30/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import Foundation


func getWebservices ( urlString : String, httpMethod: String , data: Data , completion: @escaping (_ result: [String:AnyObject]) -> Void)
{
    
    let request = NSMutableURLRequest(url: NSURL(string: urlString)! as URL)
    // Set the method to POST
    request.httpMethod = httpMethod
    request.setValue("application/json", forHTTPHeaderField: "Content-Type")
    
    // Set the POST/put body for the request
    request.httpBody = data
    request.setValue(String.init(format: "%i", (data.count)), forHTTPHeaderField: "Content-Length")
    
    let session = URLSession.shared
    
    let task = session.dataTask(with: request as URLRequest, completionHandler: {data, response, error -> Void in
        if data == nil
        {
            var errorResponse = [String : AnyObject]()
            errorResponse["Error"] = "Issue" as AnyObject?
            completion(errorResponse)
        }
        else
        {
            if  let utf8Text = String(data: data! , encoding: .utf8) {
                completion(convertStringToDictionary(text: utf8Text)! as! [String : AnyObject])
            }
            else
            {
                var errorResponse = [String : AnyObject]()
                errorResponse["Error"] = "Issue" as AnyObject?
                completion(errorResponse)
            }
        }
    })
    task.resume()
}

func convertStringToDictionary(text: String) -> NSDictionary? {
    if let data = text.data(using: String.Encoding.utf8) {
        do {
            return try JSONSerialization.jsonObject(with: data, options: []) as? NSDictionary as! [String : AnyObject]? as NSDictionary?
        } catch let error as NSError {
            var errorResponse = [String : AnyObject]()
            errorResponse["Error"] = "Issue" as AnyObject?
            print(error)
            return errorResponse as NSDictionary?
        }
    }
    return nil
}


func createURLrequest(url:String)->NSMutableURLRequest{
    let myurl = NSURL(string: url )
    let request = NSMutableURLRequest(url:myurl! as URL)
    request.setValue("application/json; charset=utf-8", forHTTPHeaderField:"Content-Type")
    return request;

}

func webServiceGet(request:NSMutableURLRequest,completion:@escaping (_ result: NSArray)->()){
    request.httpMethod="GET"
    var dico : NSMutableDictionary
    dico = NSMutableDictionary()
    let task = URLSession.shared.dataTask(with: request as URLRequest) {
        data,response,error in
        if(error != nil){
            print("error = \(error)")
            return
        }
        let responseString = NSString(data: data!, encoding: String.Encoding.utf8.rawValue)
        
        
        var err: NSError?
        do{
            var errori: NSError?
            let jsonArr = try JSONSerialization.jsonObject(with: data!, options: JSONSerialization.ReadingOptions.allowFragments) as! NSArray
            print(jsonArr.count)
            
            completion(jsonArr)
            
        }catch{
            print(error)
        }
        
    }
    
    task.resume()
    
}
