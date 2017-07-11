using Newtonsoft.Json;
using System.IO;
using System;
using exceptions;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace conf{

    public class ConfigurationData{

        public string email {get;set;}
        public string password {get;set;}
        public string emailTemplatePath {private get;set;}
        public string smtp {get;set;}
        public string logFile {private get;set;}
        public string fromAdressTitle {get;set;}
        public string toAdressTitle {get;set;}
        public string subject {get;set;}
        
        private static ConfigurationData conf = null;
        /// <summary>
        /// Sigleton pattern
        /// </summary>
        /// <returns></returns>
        public static ConfigurationData getInstance()
        {
            if(conf == null){
                using (StreamReader r = new StreamReader("configuration.json"))
                {
                    string json = r.ReadToEnd();
                    conf = JsonConvert.DeserializeObject<ConfigurationData>(json);
                }
            
            }
            return conf;  
        }

        private ConfigurationData(){

        }
        /// <summary>
        /// Extract the Email Template PAth
        /// </summary>
        /// <returns></returns>
        public string getEmailTemplatePath(){
            if(String.IsNullOrEmpty(emailTemplatePath)){
                throw new EmailCustomException("Vous n'avez pas renseigné le chemin pour les templates");
            }
            if(!Directory.Exists(emailTemplatePath)){
                throw new EmailCustomException($"Le repertoire specifié {emailTemplatePath} n'existe pas");
            }
            return emailTemplatePath;
        }

        /// <summary>
        /// Extract the Log path
        /// </summary>
        /// <returns></returns>
        public string getLogPath(){
            if(String.IsNullOrEmpty(logFile)){
                throw new LogNotExistException("Le fichier de log a mal été renseigné");
            }
            if(Directory.Exists(logFile)){
                throw new LogNotExistException($"Le chemin spécifié {logFile} est un repertoire, ca doit etre un fichier");
            }
            return logFile;
        }
    }


}