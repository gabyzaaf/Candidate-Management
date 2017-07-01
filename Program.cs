using System;
using actions;
using actionUser;
using exceptions;
using System.IO;
using conf;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace email
{
    class Program
    {


        public static void writeLogs(string pathWithFile,string content){
            using (StreamWriter sw = File.AppendText(pathWithFile)) 
            {
                sw.WriteLine(content);
            }
        }

        static void Main(string[] args)
        {
            // "/Users/zaafranigabriel/Documents/5A/Projet Annuel/final/plugins/email/sample.txt"
            /*
                Data enter : 
                UserEmail [0].
                FileName [1].
                Candidate Name [2].
                Candidate Firstname [3].
                Meetings Optionnal [4].
                Candidate email [5].
             */
             
            try{
                
                Program.writeLogs("/Users/zaafranigabriel/Documents/logs/log.txt",string.Format($"the date is {DateTime.Now.ToString()} BEGIN PROGRAM"));
                UserFeatures users = new UserFeatures(args[0],args[1],args[2],args[3],DateTime.Parse(args[4]),args[5]);
                ActionUser action = new ActionUser(users);
                Console.WriteLine(action.transformTextFromCandidate());
                action.sendEmail("gabriel.zaafrani@gmail.com",action.transformTextFromCandidate(),"gaby");
                Program.writeLogs("/Users/zaafranigabriel/Documents/logs/log.txt","END PROGRAM");
            }catch(Exception exc){
                // /Users/zaafranigabriel/Documents/logs/log.txt
                new EmailCustomException(exc.Message).writeLog("/Users/zaafranigabriel/Documents/logs/log.txt");
            }
            
        }


        
 
    }
}
