using System.Collections.Generic;
using Core.Adapter.Inteface;
using System.Collections;
using System;
using core.configuration;
using Candidate_Management.CORE.Exceptions;
namespace Candidate_Management.CORE.Remind
{
    public abstract class LegacyRemind
    {

        protected static readonly string extension = ".txt";
        protected static readonly string pluginExtension = ".dll";
        protected int remindId;
        protected string emailCandidate = null;
        protected string fileName = null; 
        protected DateTime date = new DateTime();
        protected IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");

        protected Dictionary<string,string> getCandidateNameFromId(int id){   
            ArrayList liste = isql.searchCandidateById(id);
            Dictionary<string,string> candidateInformation = (Dictionary<string,string>)liste[0];
            return candidateInformation;
        }

        protected string getPathNameAndFileFromTemplate(string fileName){
            if(String.IsNullOrEmpty(fileName)){
                throw new Exception("Le nom de fichier pour les templates d'email ne peut pas etre vide");
            }
            JsonConfiguration json = JsonConfiguration.getInstance();
            string pathAndFile = $"{json.getEmailTemplatePath()}{fileName}{extension}";
            return pathAndFile;
        }

        protected void checkFileNameIsNull(){
            if(fileName == null){
                fileName = $"{this.GetType().Name}{extension}";
            }
        }
        
        protected void traceOutSideTheSystem(string cmd){
            new WsCustomeInfoException("SendRemind",$"The message was sent on time {date} + the command is {cmd}");
        }

        protected string getDllPathEmailFromEmailCandidat(string emailCandidate){
            string pluginChoice =  isql.getPluginChoiceFromCandidate(emailCandidate);
            string pluginFolder = JsonConfiguration.getInstance().getPluginFolder();
            string dllFileToExecute = $"{pluginFolder}{pluginChoice}/{pluginChoice}{pluginExtension}";
            return dllFileToExecute;
        }
    }
}