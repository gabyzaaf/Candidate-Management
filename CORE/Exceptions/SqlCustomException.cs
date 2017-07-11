using System;
using System.IO;
using ConsoleApplication;
using core.configuration;
using CORE.LogManager;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace exception.sql
{

    public class SqlCustomException:System.Exception{

        LogManager log = new LogManager(JsonConfiguration.conf.getLogPath());

        /// <summary>
        /// Specific exception for handle the SQL exception
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SqlCustomException(string code,string message):base(message){            
            log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);       
        }

    }

}