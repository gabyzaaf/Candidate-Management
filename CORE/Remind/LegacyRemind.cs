using System.Collections.Generic;
using Core.Adapter.Inteface;
using System.Collections;
using System;
using core.configuration;
namespace Candidate_Management.CORE.Remind
{
    public abstract class LegacyRemind
    {

        protected static readonly string extension = ".txt";
        protected int remindId;
        protected string emailCandidate = null;

        protected Dictionary<string,string> getCandidateNameFromId(int id){
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
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

        
    }
}