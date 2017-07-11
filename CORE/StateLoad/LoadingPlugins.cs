using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using core.configuration;
using Core.Adapter.Inteface;
using Candidate_Management.CORE.Load;
using Candidate_Management.CORE.Exceptions;

namespace Candidate_Management.CORE.Loading
{
    public class LoadingPlugins : Iloading
    {
        private static readonly string configurationFile = "configuration.json";
        private static readonly string extension = ".dll";
        private ArrayList pathWithPugin = new ArrayList();
        private IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");

        /// <summary>
        /// Extract the directories From the plugin path
        /// </summary>
        /// <returns>string array</returns>
        private string[] getDirectoriesFromPluginPath(){
            string folderPluginPath = JsonConfiguration.getInstance().getPluginFolder();
            string[] directories = Directory.GetDirectories(folderPluginPath);
            if(directories.Length == 0){
                throw new Exception($"Dans la fonction LoadFoldersFromFolder Le repertoire de plugin {folderPluginPath} est vide ");
            }
            return directories;
        }

        private void getDirectoryContainsDllAndConfigurationFile(string[] directoryList){
            
            string fileName = null;
            bool dllFound = false;
            bool configurationFileFound = false;
            foreach (string directory in directoryList)
            {
                string[] containsAllFileSplited = directory.Split("/");
                fileName = containsAllFileSplited[containsAllFileSplited.Length-1];
                string[] files = Directory.GetFiles(directory);
                foreach(string file in files){
                    if(file.Contains($"{fileName}{extension}")){
                        dllFound = true;
                    }
                    if(file.Contains(configurationFile)){
                        configurationFileFound = true;
                    }
                }
                if(dllFound==true && configurationFileFound==true){
                    pathWithPugin.Add(new Plugin(fileName));
                }
            }
            if(pathWithPugin.Count == 0){
                throw new Exception("Aucun plugin d'email n'est disponible");
            }
          
        }

        private void addPluginInsideTheSystem(){
            foreach(Plugin plugin in pathWithPugin){
                if(!isql.pluginExist(plugin)){
                    isql.addPlugin(plugin);
                }
            }
        }



        public void loading(){
            try{
                string[] directories = getDirectoriesFromPluginPath(); // get the files From the plugin Folder
                getDirectoryContainsDllAndConfigurationFile(directories); // add the files with extension inside the array list
                addPluginInsideTheSystem(); // add the plugins inside the database
                new WsCustomeInfoException("Plugins","Les plugins ont bien été chargé au démarrage");
            }catch(Exception exc){
                throw exc;
            }
            
        }
    }
}