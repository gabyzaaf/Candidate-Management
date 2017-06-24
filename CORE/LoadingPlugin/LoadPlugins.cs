using System.IO;
using System;
using System.Collections.Generic;
namespace Candidate_Management.CORE.LoadingPlugin
{
    public class LoadPlugins
    {
        public LinkedList<string> liste ;
        public string folderPath{get;set;}
        
        public LoadPlugins(string _folderPath){
            if(String.IsNullOrEmpty(_folderPath)){
                throw new Exception("The path is empty");
            }
            folderPath = _folderPath;
        }

        public LinkedList<string> getPluginFromFolders(){
            string[] pluginsFromPath = Directory.GetFiles(folderPath);
            foreach (var plugin in pluginsFromPath)
            {
                Console.WriteLine($"The plugin is {plugin}");
            }
            return null;
        }
        /*
            This function need to check if the file is dll extension or json extension
         */
        private void checkfile(){
            
        }

    }
}