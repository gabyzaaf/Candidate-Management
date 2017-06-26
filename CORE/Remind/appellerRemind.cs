using System;
using System.Collections;
using System.Collections.Generic;
using Core.Adapter.Inteface;
namespace Candidate_Management.CORE.Remind
{
    public class appellerRemind : LegacyRemind ,Iremind
    {
        private DateTime date = new DateTime();
        public int id {get;set;}

        public void add(int id,DateTime date){
            this.id = id;
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            date = date.AddDays(1);
            isql.remindType(id,date);
        }
        
        public void update(int id,DateTime date){
           this.id = id;
           IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
           date = date.AddDays(1);
           isql.updateRemindType(id,date);
        }

        public void exec(int idJob,string fileName,string candidateName,string Candidatefirstname, DateTime meeting){
            Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
            string cmd = $"./script.sh {date.Hour}:{date.Minute} {date.Month}/{date.Day}/{date.Year} {fileName} {idJob} {fileName} {candidateInformation["nom"]} {candidateInformation["prenom"]} {meeting}";
            Console.WriteLine(cmd);
        }
    }
}