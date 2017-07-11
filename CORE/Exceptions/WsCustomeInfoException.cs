using core.configuration;
using CORE.LogManager;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.CORE.Exceptions
{
    public class WsCustomeInfoException : System.Exception
    {
        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
        
        /// <summary>
        /// Specific Exception for Trace inside the System the User Behavior.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public WsCustomeInfoException(string code,string message):base(message){            
            log.Write("[Information]",this.GetType().Name+" - "+code+" - "+message);    
         }
    }
}