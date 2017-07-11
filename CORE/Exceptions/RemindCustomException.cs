using core.configuration;
using CORE.LogManager;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.CORE.Exceptions
{
    public class RemindCustomException : System.Exception
    {
        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
        
        /// <summary>
        /// Specific exception for handle the errors remind
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public RemindCustomException(string code,string message):base(message){            
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);    
         }
    }
}