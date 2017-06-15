using System;
using Candidate_Management.CORE.LoadingTemplates;
using core.configuration;
using System.IO;
using exception.configuration;
using System.Collections.Generic;
using Core.Adapter;
using Core.Adapter.Inteface;
using System.Collections;

namespace Candidate_Management.CORE.LoadingTemplates
{
    public class LoadingEmailTemplate : Iloading
    {
        private string[] contents;

        private void getFilesFromTheFolder(){
            try{
                contents =  JsonConfiguration.getInstance().getEmailTemplate();
                if(contents.Length == 0){
                     new NotFileContentFolderException("c01","Aucun template d'email n'existe actuellement, veuillez en créer");
                }
            }catch(IOException exc){
                
                throw new ConfigurationCustomException(this.GetType().Name,$"{exc.Message}");
            }
        }

        private void injectFolderInsideTheSystem(){
            try{
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                ArrayList liste = isql.emailTemplateExist("file1.txt");
                Dictionary<String,String> dico = (Dictionary<String,String>)liste[0];
                Console.WriteLine($"After liste the element {dico["nb"]}");
            }catch(Exception exc){
                Console.WriteLine(exc.Message);
            }
        }

        private void display(){
            foreach (var item in contents)
            {
                Console.WriteLine($"the file is {item}");
            }
        }

        public void loadFiles(){
            try{
                this.getFilesFromTheFolder();
                this.injectFolderInsideTheSystem();
            }catch(NotFileContentFolderException notFile){
                throw notFile;
            }
            
        }
    }
}