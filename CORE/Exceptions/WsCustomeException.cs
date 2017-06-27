
using core.configuration;
using CORE.LogManager;


namespace exception.ws
{

    public class WsCustomeException:System.Exception{
       
          LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
          public WsCustomeException(string code,string message):base(message){            
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);    
         }

    }


}