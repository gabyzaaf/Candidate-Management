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

        private bool messageExists(Template message){
              try{
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                ArrayList liste = isql.emailTemplateExist(message.title);
                Dictionary<String,String> dico = (Dictionary<String,String>)liste[0];
                bool existe = Convert.ToBoolean(Int32.Parse(dico["nb"]));
                return existe;
              }catch(Exception exc){
                Console.WriteLine($" exception : {exc.Message}");
                return false;
              }   
        }

        private void addMessage(ArrayList emailListe){
            foreach (Template email in emailListe)
            {
                if(!messageExists(email)){
                   IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                   isql.addEmailTemplates(email);
                }
            }
        }

        private void injectFolderInsideTheSystem(){
            ArrayList liste = new ArrayList();
            try{
                getFilesFromTheFolder();
                foreach(var pathWithFile in contents){
                    liste.Add(new Template(pathWithFile));
                }
                addMessage(liste);
            }catch(Exception exc){
                Console.WriteLine(exc.Message);
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