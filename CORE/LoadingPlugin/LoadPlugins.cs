using System.IO;
using System;
using System.Collections.Generic;
namespace Candidate_Management.CORE.LoadingPlugin
{
    public class LoadPlugins
    {

        public LinkedList<string> liste ;
        public string folderPath{get;set;}
        private static readonly string configurationFile="config.json";
        private static readonly string excutionfileExtension = ".dll";


        public LoadPlugins(string _folderPath){
            if(String.IsNullOrEmpty(_folderPath)){
                throw new Exception("The path is empty");
            }
            folderPath = _folderPath;
        }

        public string[] getPluginFromFolders(){
            string[] pluginsFromPath = Directory.GetFiles(folderPath);
            return pluginsFromPath;
        }
        /*
            This function need to check if the file is dll extension or json extension
         */
        public bool checkfile(string[] fileList){
            bool verifConfFile = false;
            bool verifExecutionFileExtension = false;
            if(fileList.Length == 0){
                throw new Exception("The folder not contains file");
            }
            foreach (var item in fileList)
            {
                if(item.EndsWith(configurationFile)){
                    verifConfFile = true;
                } 
                if(Path.GetExtension(item).Equals(excutionfileExtension)){
                    verifExecutionFileExtension = true;
                }
                Console.WriteLine($"the file is {item}");
            }
            return verifConfFile && verifExecutionFileExtension;
        }

    }
}