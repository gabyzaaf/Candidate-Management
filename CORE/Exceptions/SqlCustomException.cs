using System;
using System.IO;
using ConsoleApplication;
using core.configuration;
using CORE.LogManager;
using Serilog;

namespace exception.sql
{

    public class SqlCustomException:System.Exception{
        // JsonConfiguration.conf.getLogPath()
        LogManager log = new LogManager(Program.jsonConf.getLogPath());

        public SqlCustomException(string code,string message):base(message){            
                 
         log.Write("[Error]",this.GetType().Name+" - "+code+" - "+message);       
        }
        
    }

}