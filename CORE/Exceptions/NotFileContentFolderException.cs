 using CORE.LogManager;
 using core.configuration;
 using System;
 /*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace exception.configuration
{
    public class NotFileContentFolderException :System.Exception
    {
        /// <summary>
        /// Specific Exception for the Not File content during the Loading
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        public NotFileContentFolderException(string code,string message){
            LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());
            log.Write("[Information]",$" {this.GetType().Name} - {code} - {message}");
        }
    }
}