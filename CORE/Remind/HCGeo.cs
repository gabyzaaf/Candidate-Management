using System;
using Core.Adapter.Inteface;
using scheduler;
using System.Collections;
using System.Collections.Generic;
using core.configuration;
namespace Candidate_Management.CORE.Remind
{
    public class HCGeo :  LegacyRemind ,Iremind
    { 
         private DateTime date = new DateTime();
         public int id {get;set;}
         private string fileName = null; 

         public void add(int id,DateTime date){
            // remind toutes les 2 semaines
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            this.date = date.AddMonths(6);
            isql.remindType(id,this.date);
         }
         
         public void update(int id,DateTime date){
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            this.date = date.AddMonths(6);
            isql.updateRemindType(id,this.date);
         }

         public void exec(string token,DateTime meeting){
            Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
            string currentDate = $"{date.Month}/{date.Day}/{date.Year}";
            string currentHourMinute = $"{date.Hour}:{date.Minute}";
            string emailPluginPath = "/var/candidate/plugins/Candidate-Management/bin/Debug/netcoreapp2.0/email.dll";
            string filePathTemplate = "/var/candidate/plugins/Candidate-Management/bin/Debug/netcoreapp2.0/sample.txt";
            string cmd = $"./script.sh {currentHourMinute} {currentDate}  {emailPluginPath} {token}  {filePathTemplate} {candidateInformation["nom"]} {candidateInformation["prenom"]} {meeting}";
            Console.WriteLine(cmd);    
            Schedule schedule = new Schedule(cmd);
            schedule.executeTask(); 
         }

         private void checkFileNameIsNull(){
            if(fileName == null){
                fileName = this.GetType().Name;
            }
        }
    }
}