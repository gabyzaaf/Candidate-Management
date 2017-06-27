using core.configuration;
using CORE.LogManager;
namespace Candidate_Management.CORE.Exceptions
{
    public class RemindCustomException : System.Exception
    {
        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
          public RemindCustomException(string code,string message):base(message){            
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);    
         }
    }
}