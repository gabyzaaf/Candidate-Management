using System.IO;
using core.configuration;
using CORE.LogManager;


/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace exception.configuration{

    public class ConfigurationCustomException : System.Exception{
        LogManager log = new LogManager(Directory.GetCurrentDirectory()+"/configuration.log");
        public ConfigurationCustomException():base(){
            log.Write("[Error]",$"{this.GetType().Name} - Le fichier appsettings.json n'est pas present dans votre systeme ou est non conforme");   
        }
        /// <summary>
        /// This exception handle The specific exception from Configuration file.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Message</param>
        public ConfigurationCustomException(string code,string message){
            LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
            log.Write("[Error]",$" {this.GetType().Name} - {code} - {message}");
        }

    }

}