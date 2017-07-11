using System;
using System.Diagnostics;
using core.configuration;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace scheduler{

    public class Schedule{

        private string cmd;

        public Schedule(string command){
            this.cmd = command;
        }

        /// <summary>
        /// Execute commmand outside the System.
        /// </summary>
        public void executeTask(){
            try{
                ProcessStartInfo _processStartInfo = new ProcessStartInfo();
                _processStartInfo.WorkingDirectory = JsonConfiguration.getInstance().getTheBashScriptPath();
                _processStartInfo.FileName         = @"bash";
                _processStartInfo.Arguments        = cmd;
                _processStartInfo.CreateNoWindow   = true;
                
                Process myProcess = Process.Start(_processStartInfo);
                myProcess.WaitForExit();
                
            }catch(Exception exc){
                Console.WriteLine(exc.Message);
            }
        }

    }


}