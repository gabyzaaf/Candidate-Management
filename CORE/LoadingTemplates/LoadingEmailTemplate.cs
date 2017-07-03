using System;
using Candidate_Management.CORE.LoadingTemplates;
using core.configuration;
using System.IO;
using exception.configuration;
using System.Collections.Generic;
using Core.Adapter;
using Core.Adapter.Inteface;
using System.Collections;
using Candidate_Management.CORE.LoadingTemplates;

namespace Candidate_Management.CORE.LoadingTemplates
{
    public class LoadingEmailTemplate : Iloading
    {
        private static readonly string extension = ".txt";
        private string[] containAllTheFiles;
        private ArrayList containFileOnlyWithExtension = new ArrayList();
        private IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");

        private void getFilesFromTheFolder(){
            try{
                containAllTheFiles =  JsonConfiguration.getInstance().getEmailTemplateFiles();
                if(containAllTheFiles.Length == 0){
                     new NotFileContentFolderException("c01","Aucun template d'email n'existe actuellement, veuillez en créer");
                }
            }catch(IOException exc){
                
                throw new ConfigurationCustomException(this.GetType().Name,$"{exc.Message}");
            }
        }

        private void filterWithTextExtension(){
            foreach(string file in containAllTheFiles){
                if(file.EndsWith(extension)){
                    string[] fileContent = file.Split("/");
                    string dfile = fileContent[fileContent.Length-1];
                    containFileOnlyWithExtension.Add(dfile);
                }        
            }
        }

        private void addTheFileInsideTheSystem(){
            foreach(string element in containFileOnlyWithExtension){
                if(!isql.emailTemplateExist(element)){
                    string fileWithExtension = $"{JsonConfiguration.getInstance().getEmailTemplatePath()}{element}";
                    string fileContent = File.ReadAllText(fileWithExtension);
                    Template template = new Template(fileWithExtension,element,fileContent);
                    isql.addEmailTemplates(template);
                    
                }
            }
        }

        
        

        public void loadFiles(){
            try{
                getFilesFromTheFolder(); // get the file for the folder
                filterWithTextExtension(); // add the files with extension inside the array list
                addTheFileInsideTheSystem();
            }catch(NotFileContentFolderException notFile){
                throw notFile;
            }
            
        }
    }
}