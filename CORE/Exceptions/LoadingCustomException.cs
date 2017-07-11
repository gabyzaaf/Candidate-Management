using System;
using CORE.LogManager;
using core.configuration;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace exception.loading
{
    public class LoadingCustomException : Exception
    {
        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
        
        /// <summary>
        /// Specific Exception for loading feature (loading template + loading plugin)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public LoadingCustomException(string code,string message):base(message){
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);
        }
    }
}