
using core.configuration;
using CORE.LogManager;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace exception.ws
{

    public class WsCustomeException:System.Exception{
       
          LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
          /// <summary>
          /// Speicifc exception for handle the webservice.
          /// </summary>
          /// <param name="code"></param>
          /// <param name="message"></param>
          /// <returns></returns>
          public WsCustomeException(string code,string message):base(message){            
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);    
         }

    }


}