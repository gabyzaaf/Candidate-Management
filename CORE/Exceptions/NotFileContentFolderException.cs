 using CORE.LogManager;
 using core.configuration;
 using System;
namespace exception.configuration
{
    public class NotFileContentFolderException :System.Exception
    {
        public NotFileContentFolderException(string code,string message){
            LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
            log.Write("[Information]",$" {this.GetType().Name} - {code} - {message}");
        }
    }
}