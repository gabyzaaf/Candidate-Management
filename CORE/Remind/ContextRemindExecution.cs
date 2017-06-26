using System;
namespace Candidate_Management.CORE.Remind
{
    public class ContextRemindExecution
    {
        public Iremind iremind{get;set;}

        public ContextRemindExecution(Iremind remind){
            iremind = remind;
        }

        public void executeAdd(int id,DateTime date){
            iremind.add(id,date);
        }

        
        public void executeUpdate(int id,DateTime date){
            iremind.update(id,date);
        }

        public void execTheAtCommand(string token,DateTime meeting){
            iremind.exec(token,meeting);
        }
    }
}