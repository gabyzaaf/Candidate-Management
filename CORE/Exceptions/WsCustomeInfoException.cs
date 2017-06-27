using core.configuration;
using CORE.LogManager;
namespace Candidate_Management.CORE.Exceptions
{
    public class WsCustomeInfoException : System.Exception
    {
        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
        public WsCustomeInfoException(string code,string message):base(message){            
            log.Write("[Information]",this.GetType().Name+" - "+code+" - "+message);    
         }
    }
}