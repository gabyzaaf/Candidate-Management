using System;
using Core.Adapter.Inteface;
using scheduler;
using System.Collections;
using System.Collections.Generic;
using core.configuration;
namespace Candidate_Management.CORE.Remind
{
    public class aRelancerMail: LegacyRemind , Iremind
    {
        private DateTime date = new DateTime();
        public int id {get;set;}
        

        public void add(int id,DateTime date){
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            this.date = date.AddDays(2);
            isql.remindType(id,this.date);
        }
        
        public void update(int id,DateTime date){
            this.id = id;
           IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
           this.date = date.AddDays(2);
           isql.updateRemindType(id,this.date);
        }
        
        public void exec(string token,DateTime meeting){
          
           checkFileNameIsNull();
           Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
           string pathAndFile = getPathNameAndFileFromTemplate(fileName);
           string currentDate = $"{date.Month}/{date.Day}/{date.Year}";
           string currentHourMinute = $"{date.Hour}:{date.Minute}";
           string emailPluginPath = "/var/candidate/plugins/Candidate-Management/bin/Debug/netcoreapp2.0/email.dll";
           string filePathTemplate = $"{JsonConfiguration.getInstance().getEmailTemplatePath()}{this.fileName}";
           string cmd = $"./script.sh {currentHourMinute} {currentDate}  {emailPluginPath} {remindId}  {filePathTemplate} {candidateInformation["nom"]} {candidateInformation["prenom"]} {currentDate} {emailCandidate}";
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