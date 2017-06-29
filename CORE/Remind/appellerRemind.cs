using System;
using System.Collections;
using System.Collections.Generic;
using Core.Adapter.Inteface;
using core.configuration;
namespace Candidate_Management.CORE.Remind
{
    public class appellerRemind : LegacyRemind ,Iremind
    {
        private DateTime date = new DateTime();
        public int id {get;set;}
        private string fileName = null; 
         

        private void checkFileNameIsNull(){
            if(fileName == null){
                fileName = this.GetType().Name;
            }
        }

        public void add(int id,DateTime date){
            this.id = id;
            checkFileNameIsNull();
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            date = date.AddDays(1);
            isql.remindType(id,date);
        }
        
        public void update(int id,DateTime date){
           this.id = id;
           checkFileNameIsNull();
           IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
           date = date.AddDays(1);
           isql.updateRemindType(id,date);
        }

        public void changeTestDate(){
            date = date.AddMinute(2);
        }

        public void exec(string token,DateTime meeting){
            checkFileNameIsNull();
            Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
            string pathAndFile = getPathNameAndFileFromTemplate(fileName);
           // string cmd = $"./script.sh {date.Hour}:{date.Minute} {date.Month}/{date.Day}/{date.Year}  {token} {pathAndFile} {candidateInformation["nom"]} {candidateInformation["prenom"]} {meeting}";
           string cmd = $"./script.sh {date.Hour}:{date.Minute} {date.Month}/{date.Day}/{date.Year}  {token} sample.txt {candidateInformation["nom"]} {candidateInformation["prenom"]} {meeting}";
            Console.WriteLine(cmd);
            
            //Schedule schedule = new Schedule(cmd);
            //schedule.executeTask(); 
        }
    }
}