using System;
using CORE.LogManager;
using core.configuration;
namespace exception.loading
{
    public class LoadingCustomException : Exception
    {
        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());

        public LoadingCustomException(string code,string message):base(message){
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);
        }
    }
}