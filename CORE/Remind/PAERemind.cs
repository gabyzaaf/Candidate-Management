using System;
using Core.Adapter.Inteface;
using System.Collections;
using System.Collections.Generic;
using core.configuration;
using scheduler;
namespace Candidate_Management.CORE.Remind
{
    public class PAERemind : LegacyRemind ,Iremind
    {
         private DateTime date = new DateTime();
         public int id {get;set;}
        
         /// <summary>
         /// ADD the current date of 6 months.
         /// </summary>
         /// <param name="id"></param>
         /// <param name="date"></param>
        public void add(int id,DateTime date){
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            this.date  = date.AddMonths(6);
            isql.remindType(id,this.date);
            emailCandidate = isql.getCandidateEmailFromId(id);
        }
        
         /// <summary>
         /// ADD the current date of 6 months.
         /// </summary>
         /// <param name="id"></param>
         /// <param name="date"></param>
        public void update(int id,DateTime date){
           this.id = id;
           IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
           this.date  = date.AddMonths(6);
           isql.updateRemindType(id,date.AddMonths(6));
           emailCandidate = isql.getCandidateEmailFromId(id);
        }
        
        public void exec(string token,DateTime meeting){
           checkFileNameIsNull();
           Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
           string pathAndFile = getPathNameAndFileFromTemplate(fileName);
           string currentDate = $"{date.Month}/{date.Day}/{date.Year}";
           string currentHourMinute = $"{date.Hour}:{date.Minute}";
           string emailPluginPath = getDllPathEmailFromEmailCandidat(emailCandidate);
           string filePathTemplate = $"{JsonConfiguration.getInstance().getEmailTemplatePath()}{this.fileName}";
           string cmd = $"./script.sh {currentHourMinute} {currentDate}  {emailPluginPath} {remindId}  {filePathTemplate} {candidateInformation["nom"]} {candidateInformation["prenom"]} {currentDate} {emailCandidate}";
           traceOutSideTheSystem(cmd);
           Schedule schedule = new Schedule(cmd);
           schedule.executeTask(); 
        }

       
    }
}