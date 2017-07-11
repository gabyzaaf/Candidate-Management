using System;
using Core.Adapter.Inteface;
using scheduler;
using System.Collections;
using System.Collections.Generic;
using core.configuration;
namespace Candidate_Management.CORE.Remind
{
    public class enCours : LegacyRemind , Iremind
    {
         private DateTime date = new DateTime();
         public int id {get;set;}
         

         /// <summary>
         /// Add 2 weeks to add in the current date
         /// </summary>
         /// <param name="id"></param>
         /// <param name="date"></param>
         public void add(int id,DateTime date){
            // remind toutes les 2 semaines
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
             this.date = date.AddDays(2*7);
            isql.remindType(id,this.date);
            emailCandidate = isql.getCandidateEmailFromId(id);
         }
         
         /// <summary>
         /// Update the current date of 2 weeks.
         /// </summary>
         /// <param name="id"></param>
         /// <param name="date"></param>
         public void update(int id,DateTime date){
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            this.date = date.AddDays(2*7);
            isql.updateRemindType(id,this.date);
            emailCandidate = isql.getCandidateEmailFromId(id);
         }


         /// <summary>
         /// Execute The command outside the system.
         /// </summary>
         /// <param name="token"></param>
         /// <param name="meeting"></param>
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