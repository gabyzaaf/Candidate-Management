using Newtonsoft.Json;
using System.IO;
using System;
using exceptions;
namespace conf{

    public class ConfigurationData{

        public string email {get;set;}
        public string password {get;set;}
        public string emailTemplatePath {private get;set;}
        public string smtp {get;set;}
        
        private static ConfigurationData conf = null;

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

        public string getEmailTemplatePath(){
            if(String.IsNullOrEmpty(emailTemplatePath)){
                throw new EmailCustomException("Vous n'avez pas renseigné le chemin pour les templates");
            }
            if(!Directory.Exists(emailTemplatePath)){
                throw new EmailCustomException($"Le repertoire specifié {emailTemplatePath} n'existe pas");
            }
            return emailTemplatePath;
        }
    }


}