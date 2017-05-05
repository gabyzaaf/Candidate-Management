
using ConsoleApplication;
using core.configuration;
using CORE.LogManager;
using Serilog;

namespace exception.ws
{

    public class WsCustomeException:System.Exception{
        
          LogManager log = new LogManager(Program.jsonConf.getLogPath());
        public WsCustomeException(string code,string message):base(message){
                      
        log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);    
        }

       
        

    }


}