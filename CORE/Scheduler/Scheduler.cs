using System;
using System.Diagnostics;

namespace scheduler{

    public class Schedule{

        private string cmd;

        public Schedule(string command){
            this.cmd = command;
        }

        public void executeTask(){
            try{
                ProcessStartInfo _processStartInfo = new ProcessStartInfo();
                _processStartInfo.WorkingDirectory = @"/Users/zaafranigabriel/Documents/5A/Projet Annuel/final/Plugin-CM/";
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