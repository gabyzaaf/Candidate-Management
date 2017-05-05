<<<<<<< HEAD
using System;
using System.IO;
using ConsoleApplication;
=======
using System.IO;
>>>>>>> 4e792f0adc8b15be180bec111212b62923beb282
using core.configuration;
using CORE.LogManager;

namespace exception.configuration{

    public class ConfigurationCustomException : System.Exception{
        LogManager log = new LogManager(Directory.GetCurrentDirectory()+"/configuration.log");
        public ConfigurationCustomException():base(){
        log.Write("[Error]",$"{this.GetType().Name} - Le fichier appsettings.json n'est pas présent dans votre system ou votre repertoire de log n'est pas conforme");   
            Console.WriteLine("[Error] Le fichier appsettings.json n'est pas présent dans votre system ou votre repertoire de log n'est pas conforme");
        }

        public ConfigurationCustomException(string code,string message){
            //LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
            LogManager log = new LogManager(Program.jsonConf.getLogPath());
            log.Write("[Error]",$" {this.GetType().Name} - {code} - {message}");
        }

    }

}
