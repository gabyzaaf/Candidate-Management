using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using exception.configuration;
using Microsoft.Extensions.Configuration;

namespace core.configuration{
    /* 
    All the attributes can't be empty
    */
    public class JsonConfiguration{

        public static JsonConfiguration conf = null;
        private static readonly string configurationLog = "logPath";
        private static readonly string configurationPluginFolder="pluginPath";

        private static readonly string configurationCode = "ErrorCode";

        private static readonly string configurationEmail="email";

        private static readonly string configurationSql="SQL";
       
        private static readonly string emailTemplate = "emailTemplate";

        private static readonly string  bashScriptPath = "bashScriptPath";

        private static readonly string UrlOrIpAdressWithPort = "UrlOrIpAdressWithPort";

        private static IConfigurationRoot configuration = null ;

        
        /// <summary>
        /// Create a Single Instance for JsonConfiguration
        /// </summary>
        /// <returns></returns>
        public static JsonConfiguration getInstance(){
             if(configuration==null || conf == null){
                return new JsonConfiguration();
             }
             return conf;
        }
        private JsonConfiguration(){
                try{
                     var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                    configuration = builder.Build();
                    conf = this;
                }catch(Exception){
                    throw new ConfigurationCustomException();
                }  
            }
            
        
        /// <summary>
        /// Get Log path.
        /// </summary>
        /// <returns></returns>
        public string getLogPath(){
            settingsExist();
            if(String.IsNullOrEmpty(configuration[configurationLog])){
                throw new Exception("Ne contiens aucun fichier de configuration");
            }
            return configuration[configurationLog];
        }

        /// <summary>
        /// Get plugin Folder.
        /// </summary>
        /// <returns></returns>
        public string getPluginFolder(){
            settingsExist();
            if(String.IsNullOrEmpty(configuration[configurationPluginFolder])){
                throw new ConfigurationCustomException(this.GetType().Name,"Ne contiens aucun dossier de plugin");
            }
            if(!Directory.Exists(configuration[configurationPluginFolder])){
                 throw new ConfigurationCustomException(this.GetType().Name,"Le dossier n'existe pas veuillez creer le dossier de plugin");
            }
            return replaceSlashFolderIfNotExist(configuration[configurationPluginFolder]);
        }
        

        /// <summary>
        /// Extract the errors.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Dictionary<string,string> getErrors(string country){
            settingsExist();
            bool found = false;
            Dictionary<string,string> errorDico = new Dictionary<string,string>();
            if(String.IsNullOrEmpty(country)){
                throw new ConfigurationCustomException(this.GetType().Name,"Votre reference country est null");
            }
            if(String.IsNullOrEmpty(configuration.GetSection(configurationCode).Key)){
                throw new ConfigurationCustomException(this.GetType().Name,$"votre reference {configurationCode} n'existe pas dans votre fichier de appsettings");
            }
            IEnumerable<IConfigurationSection> liste =  configuration.GetSection(configurationCode).GetChildren();
            if(liste==null){
                throw new ConfigurationCustomException(this.GetType().Name,$"Votre appsettings ne contient aucun element");
            }
            foreach(IConfigurationSection element in liste){
                if(element.Key.Equals(country)){
                    found = true;
                    IEnumerable<IConfigurationSection> errors =  element.GetChildren();
                    if(errors==null){
                        throw new ConfigurationCustomException(this.GetType().Name,$"Vous n'avez pas les données d'erreur inseré dans votre fichier appsettings");
                    }
                    foreach(IConfigurationSection conf in errors){
                        if(!errorDico.ContainsKey(conf.Key)){
                            errorDico.Add(conf.Key,conf.Value);
                        }
                    }
                }
            }
            if(found==false){
                throw new ConfigurationCustomException(this.GetType().Name,$"Aucun pays n'a ete trouve selon vos criteres");
            }
            return errorDico;
        }
         
         /// <summary>
         /// get the email from the administrator
         /// </summary>
         /// <returns></returns>
         public string getEmail(){
             try{
                settingsExist();
                if(String.IsNullOrEmpty(configuration[configurationEmail])){
                    throw new Exception("Vous ne possedez pas d'email dans votre fichier de configuration");
                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(configuration[configurationEmail]);
                if (!match.Success){
                    throw new Exception("Votre adresse email n'est pas conforme");
                }
                return configuration[configurationEmail];
             }catch(Exception exception){
                throw new ConfigurationCustomException(this.GetType().Name,$"{exception.Message}");
             }
         }
         

         /// <summary>
         /// Extract the SQL connection.
         /// </summary>
         /// <returns></returns>
         public string getConnectionSql(){
            string connectionString = null;
            try{
                settingsExist();
                if(String.IsNullOrEmpty(configuration.GetSection(configurationSql).Key)){
                    throw new Exception("Votre fichier appsettings n'est pas conforme avec la partie SQL");
                }
                IEnumerable<IConfigurationSection> connectionsSql =  configuration.GetSection(configurationSql).GetChildren();
                if(connectionsSql==null){
                    throw new Exception("Aucune donnee de configuration est présente");
                }
                foreach(IConfigurationSection section in connectionsSql){
                    if(String.IsNullOrEmpty(section.Value)){
                        throw new Exception("Votre champs de connection est vide");
                    }
                    connectionString =  section.Value;
                }
                if(String.IsNullOrEmpty(connectionString)){
                    throw new Exception("Votre champs de connection est vide");
                }
                return connectionString;
            }catch(Exception exception){
                throw new ConfigurationCustomException(this.GetType().Name,$"{exception.Message}");
            }
         }

         /// <summary>
         /// get the email Path
         /// </summary>
         /// <returns></returns>
         public string getEmailTemplatePath(){
             try{
                settingsExist();
                if(String.IsNullOrEmpty(configuration[emailTemplate])){
                    throw new Exception("Votre fichier appsettings n'est pas conforme avec vos template d email");
                }
                return replaceSlashFolderIfNotExist(configuration[emailTemplate]);
             }catch(Exception exception){
                 throw new ConfigurationCustomException(this.GetType().Name,$"{exception.Message}");
             }
         }

         /// <summary>
         /// extract the URL or IP adress
         /// </summary>
         /// <returns>By Default http://localhost:5000 </returns>
         public string getUrlOrIpAdressWithPort(){
             try{
                settingsExist();
                if(String.IsNullOrEmpty(configuration[UrlOrIpAdressWithPort])){
                    return "http://localhost:5000";
                }
                return configuration[UrlOrIpAdressWithPort];
             }catch(Exception exc){
                throw new ConfigurationCustomException(this.GetType().Name,$"{exc.Message}");
             }
         }

         /// <summary>
         /// Extract the Bash Script Path
         /// </summary>
         /// <returns></returns>
         public string getTheBashScriptPath(){
            try{
                settingsExist();
                if(String.IsNullOrEmpty(configuration[bashScriptPath])){
                    throw new Exception("Votre fichier appsettings n'est pas conforme avec votre repertoire de script");
                }
                string bashFolderPath = replaceSlashFolderIfNotExist(configuration[bashScriptPath]);
                if(!Directory.Exists(bashFolderPath)){
                    throw new Exception("Le repertoire de vos fichiers de script n'existe pas, veuillez le créer");
                }
                
                return bashFolderPath;
            }catch(Exception exception){
                throw new ConfigurationCustomException(this.GetType().Name,$"{exception.Message}");
            }
         }

         private string replaceSlashFolderIfNotExist(string directory){
            if(!directory.EndsWith("/")){
                directory = $"{directory}/";
            }
            return directory;
         }

         /// <summary>
         /// Extract the Email tempate Files
         /// </summary>
         /// <returns></returns>
         public string[] getEmailTemplateFiles(){ 
             try{
                
                string emailTemplates = getEmailTemplatePath();
                if(!Directory.Exists(configuration[emailTemplate])){
                    throw new Exception("Le dossier n'existe pas veuillez creer le dossier de email template");
                }
                string[] filesInsideTheDirectory =  Directory.GetFiles(emailTemplates);
                return filesInsideTheDirectory;
             }catch(NotFileContentFolderException ex){
                throw ex;
             }catch(Exception exception){
                 throw new ConfigurationCustomException(this.GetType().Name,$"{exception.Message}");
             }
             
         }


        private void settingsExist(){
            if(configuration==null){
                throw new ConfigurationCustomException();
            }
        }
        

    }


}