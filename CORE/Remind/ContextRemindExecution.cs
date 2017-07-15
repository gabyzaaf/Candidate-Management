using System;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.CORE.Remind
{
    /// <summary>
    /// This class is use for create the context for the DESIGN PATTERN Strategy
    /// </summary>
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