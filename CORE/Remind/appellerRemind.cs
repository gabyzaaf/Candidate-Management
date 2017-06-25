using System;
using Core.Adapter.Inteface;
using System;

namespace Candidate_Management.CORE.Remind
{
    public class appellerRemind :Iremind
    {
        private DateTime date = new DateTime();

        public void add(int id,DateTime date){
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            date = date.AddDays(1);
            isql.remindType(id,date);
        }
        
        public void update(int id,DateTime date){
           IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
           date = date.AddDays(1);
           isql.updateRemindType(id,date);
        }

        public void exec(int idJob,string fileName,string candidateName,string Candidatefirstname, DateTime meeting){
            
            string cmd = $"./script.sh {date.Hour}:{date.Minute} {date.Month}/{date.Day}/{date.Year} {fileName} {idJob}";
            Console.WriteLine(cmd);
        }
    }
}