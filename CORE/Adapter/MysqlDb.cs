using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using core.candidat;
using core.configuration;
using core.report;
using core.success;
using core.user;
using Core.Adapter.Inteface;
using exception.sql;
using MySql.Data.MySqlClient;
using Candidate_Management.CORE.LoadingTemplates;
using Candidate_Management.CORE.Remind;
using Candidate_Management.CORE.Load;

namespace Core.Adapter{

    public class MysqlDb : IsqlMethod
    {
        
        private MySqlConnection connection = null;
        private void connect(){
            try{
            if(connection==null){
                
                connection  = new MySqlConnection
                {
                   ConnectionString = JsonConfiguration.getInstance().getConnectionSql()
                };
                connection.Open();
                
            }
            }catch(Exception exception){
                throw new SqlCustomException(this.GetType().Name,exception.Message);
            }
        } 

      /// <summary>
      /// Execute the SQL queries with all the parameters.
      /// All the parameters are protected again the SQL Injection
      /// </summary>
      /// <param name="query">SQL query</param>
      /// <param name="dico">Key value parameters</param>
      /// <param name="results">The result present in the select</param>
      /// <returns></returns>
        private ArrayList queryExecute(string query,Dictionary<String,Object> dico,LinkedList<String> results){
            ArrayList listeHash= null;
            try{
            if(String.IsNullOrEmpty(query)){
                throw new Exception("La requete n'est pas conforme");
            }
            if(dico==null || dico.Count==0){
                throw new Exception("Le dictionnaire n'est pas conforme");
            }
            
            Dictionary<String,String> outPuts = null;
           
                connect();
                MySqlCommand command = new MySqlCommand(query,connection);
                foreach(var item in dico){
                    command.Parameters.AddWithValue(item.Key,item.Value);
                }
                if(results == null ){
                    command.ExecuteNonQuery();
                }else{
                    listeHash  = new ArrayList();
                    outPuts = new Dictionary<String,String>();
                    using (MySqlDataReader reader =  command.ExecuteReader()){
                       while(reader.Read()){  
                        foreach(string result in results){
                            outPuts.Add(result,reader[result].ToString());
                        }
                        listeHash.Add(outPuts);
                        outPuts = new Dictionary<String,String>();
                       }
                    }
                    
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }finally{
                disconnect();
            }
            return listeHash;
        }
        /// <summary>
         /// Create the Authentification in the system 
         /// </summary>
         /// <param name="email">User email</param>
         /// <param name="password">password email</param>
        public void Authentification(string email, string password)
        {
            
            try{
                if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password)){
                    throw new Exception("Email ou mot de passe incorrect");
                 }
                 Dictionary<String,Object> param = new Dictionary<String,Object>();
                 param.Add("@email",email);
                 param.Add("@pass",password);
                 LinkedList<String> element = new LinkedList<String>();
                 element.AddLast("nb");
                 ArrayList liste = queryExecute("SELECT count(*) as nb FROM user where email=@email and mdp=md5(@pass)",param,element);
                 Dictionary<String,String> dico = (Dictionary<String,String>)liste[0];
                 if(dico==null || dico.Count==0){
                     throw new Exception("Vos identifiants sont incorrects");
                 }
                  if(int.Parse(dico["nb"])==0){
                      throw new Exception("Email ou mot de passe incorrect");
                  }
            }catch(Exception exception){
                throw new SqlCustomException(this.GetType().Name,exception.Message);
            }
            
        }
        /// <summary>
         /// Verify if the User token Exist
         /// </summary>
         /// <param name="token">User token</param>
        public void TokenExist(string token)
        {
           try{
                if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token est vide, veuillez vous reconnecter");
                }
                string sql = "select count(*) as nb from user where session_id=@session_id";
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@session_id",token);
                LinkedList<string> listes = new LinkedList<string>();
                listes.AddLast("nb");
                ArrayList liste =queryExecute(sql,param,listes);
                if(liste.Count==0){
                    throw new Exception("Aucun element n'a ete retourne");
                }
                Dictionary<String,String> dico = (Dictionary<String,String>)liste[0];
                if(dico==null || dico.Count==0){
                        throw new Exception("Aucun element n'a ete retourné veuillez vous connecter");
                }
                if(int.Parse(dico["nb"])>=1){
                        throw new Exception("Veuillez vous reconnecter");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

        
         /// <summary>
         /// Check the permission for User can Read inside the Database
         /// </summary>
         /// <param name="token"></param>
         /// <returns></returns>
        public bool UserCanRead(string token)
        {
            try{
                if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token n'existe pas ");
                }
                Dictionary<String,Object> input = new Dictionary<String,Object>();
                input.Add("@token",token);
                LinkedList<String> result = new LinkedList<String>();
                result.AddLast("regle_lecture");
                ArrayList outputs = queryExecute("SELECT regle_lecture from user where session_id=@token",input,result);
                if(outputs.Count==0){
                    throw new Exception("Aucun token ayant ce numero "+token+" existe veuillez vous identifier");
                }
                Dictionary<String,String> element = (Dictionary<String,String>) outputs[0];
               
                if(!element.ContainsKey("regle_lecture")){
                    throw new Exception(" Votre token ne vous permet pas de lire");
                }
                if(!bool.Parse(element["regle_lecture"])){
                    throw new Exception(" Vous n'avez pas les droits necessaires pour effectuer un ajout ou une modification sur un candidat");
                }
                return Boolean.Parse(element["regle_lecture"]);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
           
            
            
        }
        /// <summary>
         /// Check the permission for User can Update inside the Database
         /// </summary>
         /// <param name="token">User Token</param>
         /// <returns></returns>
        public bool UserCanUpdate(string token)
        {
            Dictionary<String,String> element;
            try{
                
                if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token n'existe pas ");
                }
                
                Dictionary<String,Object> input = new Dictionary<String,Object>();
                input.Add("@token",token);
                
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("regle_modification");
                
                ArrayList dicos =  queryExecute("SELECT regle_modification from user where session_id=@token",input,results);
                if(dicos.Count == 0){
                        
                        throw new Exception("Aucun token ayant ce numero "+token+" existe veuillez vous identifier");
                }
                element = (Dictionary<String,String>) dicos[0];
                if(!element.ContainsKey("regle_modification")){
                    throw new Exception("Votre token ne vous permet pas de modifier");
                }
                if(!bool.Parse(element["regle_modification"])){
                    throw new Exception(" Vous n'avez pas les droits necessaires pour effectuer un ajout ou une modification sur un candidat");
                }
                 
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return bool.Parse(element["regle_modification"]);
        }

         /// <summary>
         /// Check the permission for User can Delete inside the Database
         /// </summary>
         /// <param name="token"></param>
         /// <returns></returns>
       public bool UserCanDelete(string token){
        Dictionary<String,String> element;
            try{
                if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token n'existe pas ");
                }
                Dictionary<String,Object> input = new Dictionary<String,Object>();
                input.Add("@token",token);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("regle_suppression");
                ArrayList dicos =  queryExecute("SELECT regle_suppression from user where session_id=@token",input,results);
                if(dicos.Count==0){
                        throw new Exception("Aucun token ayant ce numero "+token+" existe veuillez vous identifier");
                }
                element = (Dictionary<String,String>) dicos[0];
                if(!element.ContainsKey("regle_suppression")){
                    throw new Exception("Votre token ne vous permet pas de supprimer");
                }
                if(!bool.Parse(element["regle_suppression"])){
                    throw new Exception(" Vous n'avez pas les droits necessaires pour effectuer une suppression");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return bool.Parse(element["regle_suppression"]);
       }

        /// <summary>
         /// Generate Token
         /// </summary>
         /// <returns></returns>
        public User GenerateToken()
        {
            Random rnd = new Random();
            int nb = rnd.Next(1,100);
            return new User(){sessionId=nb.ToString()};
        }
        /// <summary>
         /// Insert token to the User connecting
         /// </summary>
         /// <param name="token">User token</param>
         /// <param name="email">User email</param>
        public void addTokenToUser(string token,string email)
        {
              try{
                if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token est vide");
                }
                if(String.IsNullOrEmpty(email)){
                    throw new Exception("L'email est vide");
                 }     
                Dictionary<String,Object> input = new Dictionary<String,Object>();
                input.Add("@session",token);
                input.Add("@email",email);
                queryExecute("Update user set session_id=@session where email=@email",input,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            
            
        }

        private void disconnect(){
                if(connection!=null){
                    connection.Close();
                    connection = null;  
                }
                
        }

         /// <summary>
         /// Extract candidat with report.
         /// </summary>
         /// <param name="nom">candidate name</param>
         /// <param name="token">User token</param>
         /// <returns></returns>
        public ArrayList searchCandidate(string nom, string token)
        {
            ArrayList output=null;
            try{
                if(String.IsNullOrEmpty(nom)){
                    throw new Exception("Le nom du candidat est vide");
                }
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@nom",nom);
                LinkedList<String> results = new LinkedList<String>(); 
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("sexe");
                results.AddLast("phone");
                results.AddLast("zipcode");
                results.AddLast("actions");
                results.AddLast("annee");
                results.AddLast("lien");
                results.AddLast("crCall");
                results.AddLast("NS");
                results.AddLast("pluginType");
                results.AddLast("email");
                results.AddLast("note");
                results.AddLast("link");
                results.AddLast("xpNote");
                results.AddLast("nsNote");
                results.AddLast("jobIdealNote");
                results.AddLast("pisteNote");
                results.AddLast("pieCouteNote");
                results.AddLast("locationNote");
                results.AddLast("EnglishNote");
                results.AddLast("nationalityNote");
                results.AddLast("competences");
                output = queryExecute("SELECT * from candidate,meeting where candidate.id = meeting.fid_candidate_meeting and nom=@nom",dico,results);
                if(output == null || output.Count==0){
                    throw new Exception("Aucun candidat ne possede vos criteres de recherche");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return output;
        }
        /// <summary>
         /// Verify if the Token Exist
         /// </summary>
         /// <param name="token"></param>
         /// <returns></returns>
        public bool verifyTheTokenExist(string token)
        {
            try{
            if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token n'existe pas");
                }
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@session_id",token);
                LinkedList<String> returns = new LinkedList<String>();
                returns.AddLast("nb");
                ArrayList output= queryExecute("SELECT count(*) as nb from user where session_id=@session_id",dico,returns);
                Dictionary<String,String> element = (Dictionary<String,String>) output[0];
                if(!element.ContainsKey("nb")){
                    throw new Exception("Aucun utilisateur ne possede ce token");
                }
                if(int.Parse(element["nb"])==0){
                    return false;
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return true;
        }
        /// <summary>
         /// Add the Candidate inside the system.
         /// </summary>
         /// <param name="candidat"></param>
         /// <param name="id"></param>
        public void addCandidate(Candidat candidat,int id)
        {
            if(candidat==null){
                throw new Exception("Vous devez renseigner les informations du candidat");
            }
            if(id == 0){
                throw new Exception("Vous devez avoir un identifiant valide");
            }
            Dictionary<String,Object> dico = new Dictionary<String,Object>();
            try{
                dico.Add("@nom",candidat.Name);
                dico.Add("@prenom",candidat.Firstname);
                dico.Add("@num",candidat.phone);
                dico.Add("@emailAdress",candidat.emailAdress);
                dico.Add("@sexe",candidat.sexe);
                dico.Add("@etat",candidat.action);
                dico.Add("@annee",candidat.year);
                dico.Add("@zipcode",candidat.zipcode);
                dico.Add("@lien",candidat.link);
                dico.Add("@crcall",candidat.crCall);
                dico.Add("@ns",candidat.ns);
                dico.Add("@pluginType",candidat.pluginType);
                dico.Add("@userId",id);
                queryExecute("insert into candidate (nom,prenom,phone,email,zipcode,sexe,actions,annee,lien,crCall,NS,pluginType,fid_user_candidate) values (@nom,@prenom,@num,@emailAdress,@zipcode,@sexe,@etat,@annee,@lien,@crcall,@ns,@pluginType,@userId)",dico,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,$"{exc.Message}");
            }
          
        }

        /// <summary>
         /// Extract the User ID from the Token
         /// </summary>
         /// <param name="token">User Token</param>
         /// <returns></returns>
        public int getIdFromToken(string token)
        {
            Dictionary<String,String> element;
            try{
                if(String.IsNullOrEmpty(token)){
                    throw new Exception(" Votre token n'est pas valide");
                }
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@session_id",token);
                LinkedList<String> returnValue = new LinkedList<String>();
                returnValue.AddLast("id");
                ArrayList output = queryExecute("SELECT id from user where session_id=@session_id",param,returnValue);
                if(output == null || output.Count==0){
                    throw new Exception("Votre token de session n'est pas conforme");
                }
                if(output.Count>1){
                    throw new Exception("Votre token a ete usurpe, veuillez vous reconnecter ou contacter l'administrateur");
                }
                element = (Dictionary<String,String>)output[0];
                if(!element.ContainsKey("id")){
                    throw new Exception("Votre session id est incorrect");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return int.Parse(element["id"]);
            
        }


        /// <summary>
         /// Verify if the Candidate already exist in the system.
         /// </summary>
         /// <param name="candidat"></param>
         /// <returns></returns>
        public bool CandidatAlreadyExist(Candidat candidat)
        {
            try{
            if(candidat == null){
                throw new Exception("Vos parametres ne sont pas conformes");
            }
            if(String.IsNullOrEmpty(candidat.emailAdress)){
                throw new Exception("L'adresse email saisie du candidat n'est pas conforme");
            }
            
            Dictionary<String,Object> param = new Dictionary<String,Object>();
            param.Add("@email",candidat.emailAdress);
            LinkedList<String> result = new LinkedList<String>();
            result.AddLast("nb");
            ArrayList output =queryExecute("SELECT count(*) as nb from candidate where email=@email",param,result);
            if(output.Count==0){
                return false;
            }
            Dictionary<String,String> element = (Dictionary<String,String>) output[0];
            if(!element.ContainsKey("nb")){
                return false;
            }
            if(int.Parse(element["nb"])==0){
                return false;
            }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return true;
        }

        private void checkPrice(int prix){
            if(prix <= 0){
                throw new Exception("Le prix saisi n'est pas conforme");
            }
        }

        /// <summary>
         /// Add candidate FreeLance in the System.
         /// </summary>
         /// <param name="prix"></param>
         /// <param name="id"></param>
        public void addFreeLance(int prix,int id)
        {
           
            checkPrice(prix);
            checkId(id);
            try{
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@value",prix);
                param.Add("@fid",id);
                queryExecute("insert into internNumeric (contentType,fid_candidate_internNumeric) values (@value,@fid)",param,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            
        }



        // <summary>
         /// get the Id from the Candidate Email
         /// </summary>
         /// <param name="email">Candidate Email</param>
         /// <returns></returns>
        public int getIdFromCandidateEmail(string email)
        {
            try{
                if(String.IsNullOrEmpty(email)){
                    throw new Exception("L'email n'est pas conforme");
                }
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@email",email);
                LinkedList<String> liste = new LinkedList<String>();
                liste.AddLast("id");
                ArrayList output = queryExecute("SELECT id  from candidate where email=@email",dico,liste);
                if(output == null || output.Count==0){
                    throw new Exception("Aucun candidat ne possede cet email");
                }
                if(output.Count>1){
                    throw new Exception("Votre candidat est présent plusieurs fois dans la base de données");
                }
                Dictionary<String,String> element = (Dictionary<String,String>)output[0];
                if(!element.ContainsKey("id")){
                     throw new Exception("Le candidat n'est pas présent dans votre application");
                }
                return int.Parse(element["id"]); 
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);;
            }
        }

        private void checkId(int id){
            if(id<=0){
                throw new Exception("L'identifiant n'est pas conforme");
            }
        }

        private void checkDate(DateTime date){
            if(date == null){
                throw new Exception("La date mise en parametre n'est pas conforme");
            }
        }

        /// <summary>
         /// create the job remind for ADD or Update.
         /// </summary>
         /// <param name="id">User id</param>
         /// <param name="date">Current time</param>
        public void remindType(int id,DateTime date)
        {

            try{
                 
                 checkId(id);
                 checkDate(date);
                 Dictionary<String,Object> param = new Dictionary<String,Object>();
                 param.Add("@date",date.Date);
                 param.Add("@fid",id);
                 param.Add("@finish",false);
                 queryExecute("insert into remind (dates,fid_candidate_remind,finish) values (@date,@fid,@finish)",param,null);
            }catch(Exception exception){
                throw new SqlCustomException(this.GetType().Name,exception.Message);
            }

        
        }


        /// <summary>
         /// Update the Remind Job
         /// </summary>
         /// <param name="id">User Id</param>
         /// <param name="date">Current time</param>
        public void updateRemindType(int id,DateTime date){
            try{
                
                checkId(id);
                checkDate(date);
                if(!remindExistByCandidate(id)){
                    remindType(id,date);
                }else{
                     Dictionary<String,Object> param = new Dictionary<String,Object>();
                    param.Add("@date",date.Date);
                    param.Add("@fid",id);
                    queryExecute("update remind set dates=@date where fid_candidate_remind=@fid",param,null);     
                }
               }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }
        public void updateFreeLance(int prix,int id){
            try{
                checkId(id);
                checkPrice(prix);
                
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@price",prix);
                param.Add("@id",id);
                queryExecute("update internNumeric set contentType=@price where fid_candidate_internNumeric=@id",param,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

        /// <summary>
         /// This method are created for Factoring the remind Add and the remind Update and FreeLance
         /// </summary>
         /// <param name="actionType">Freelance or not</param>
         /// <param name="prix">Freelance price</param>
         /// <param name="date">Date add</param>
         /// <param name="id">User Id</param>
         /// <param name="type">ADD or UPDATE</param>
         /// <param name="token">User token</param>
        public void typeAction(string actionType,int prix,DateTime date,int id,string type,string token){
                try{
                    
                        if ("freelance".Equals(actionType)){
                            
                            if("ADD".Equals(type)){
                                addFreeLance(prix,id);
                                
                            }else if("UPDATE".Equals(type)){
                                updateFreeLance(prix,id);
                                
                            }      
                        }else{
                            
                            ContextRemindExecution remindExecution = new ContextRemindExecution(FactoryRemind.createRemind(actionType));
                            if("ADD".Equals(type)){
                                remindExecution.executeAdd(id,date);
                            }else if("UPDATE".Equals(type)){
                                remindExecution.executeUpdate(id,date);
                            }
                            
                            remindExecution.execTheAtCommand(token,DateTime.Now); 
                        }
                }catch(Exception exc){
                    throw new SqlCustomException(this.GetType().Name,exc.Message);
                }
            }
        

            private void checkCandidateExist(Candidat candidat){
                if(candidat==null){
                    throw new Exception("Veuillez renseigner tous les champs de votre candidat");
                }
                if(String.IsNullOrEmpty(candidat.session_id)){
                    // loguer l'information
                    throw new Exception("Vous n'avez pas les droits associes a la modification du candidat");
                }
            }

            /// <summary>
            /// Update the candidate Information
            /// </summary>
            /// <param name="candidat">candidate class</param>
            /// <param name="id">Token </param>
            public void updateCandidate(Candidat candidat,int id){
                try{
                    
                    checkCandidateExist(candidat);
                    checkId(id);
                    Dictionary<String,Object> param = new Dictionary<String,Object>();
                    param.Add("@nom",candidat.Name);
                    param.Add("@prenom",candidat.Firstname);
                    param.Add("@phone",candidat.phone);
                    param.Add("@sexe",candidat.sexe);
                    param.Add("@zipcode",candidat.zipcode);
                    param.Add("@actions",candidat.action);
                    param.Add("@annee",candidat.year);
                    param.Add("@lien",candidat.link);
                    param.Add("@crCall",candidat.crCall);
                    param.Add("@ns",candidat.ns);
                    param.Add("@email",candidat.emailAdress);
                    queryExecute("update candidate set nom=@nom,prenom=@prenom,phone=@phone,zipcode=@zipcode,sexe=@sexe,actions=@actions,annee=@annee,lien=@lien,crCall=@crCall,NS=@ns where email=@email",param,null);
                
                }catch(Exception exc){
                    throw new SqlCustomException(this.GetType().Name,exc.Message);
                }
            }
        /// <summary>
         /// Extract the candidate for the mobile.
         /// </summary>
         /// <param name="nom">Candidate Name</param>
         /// <param name="token">Token User</param>
         /// <returns>Only 3 values by Candidate</returns>
        public ArrayList searchCandidateMobile(string nom, string token)
        {
            //
            ArrayList output=null;
            try{
                if(String.IsNullOrEmpty(nom)){
                    throw new Exception("Le nom du candidat est vide");
                }
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@nom",nom);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("id");
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("email");
                output = queryExecute("SELECT candidate.id,nom,prenom,email from candidate,meeting where candidate.id = meeting.fid_candidate_meeting and nom=@nom",dico,results);
                if(output == null || output.Count==0){
                    throw new Exception("Aucun candidat ne possede vos criteres de recherche");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return output;
        }
        /// <summary>
          /// Verify if the report Already Exist.
          /// </summary>
          /// <param name="idCandidat">Candidate Id</param>
          /// <returns></returns>
        public bool reportAlreadyExist(int idCandidat)
        {
            try{
                if(idCandidat <= 0){
                    throw new Exception("L'identifiant du candidat n'est pas valable");
                }
                string sql = "select count(*) as nb from meeting where fid_candidate_meeting=@fid";
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@fid",idCandidat);
                ArrayList listeReturn = queryExecute(sql,dico,results);
                if(listeReturn == null || listeReturn.Count==0){
                    throw new Exception("Aucun element retourné est lié a la fiche candidat");
                }
                Dictionary<String,String> returnData = (Dictionary<String,String>)listeReturn[0];
                return Convert.ToBoolean(int.Parse(returnData["nb"]));
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

        private void checkReport(Report report){
            if(report==null){
                throw new Exception("Veuillez inserer tous les champs de votre fiche candidat");
            }
            if(String.IsNullOrEmpty(report.sessionId)){
                throw new Exception("Vous n'avez pas les droits necessaires pour ajouter ou modifier une fiche candidat");
            }
        }
          /// <summary>
          /// Create new report inside the System.
          /// </summary>
          /// <param name="report">Report class</param>
          /// <param name="idCandidat">Candidate Id</param>
        public void addReport(Report report,int idCandidat)
        {
         try{
            checkReport(report);
            string sql = "insert into meeting (note,link,xpNote,nsNote,jobIdealNote,pisteNote,pieCouteNote,locationNote,EnglishNote,nationalityNote,competences,fid_candidate_meeting) values (@note,@link,@xpNote,@nsNote,@jobIdealNote,@pisteNote,@pieCouteNote,@locationNote,@EnglishNote,@nationalityNote,@competences,@fid_candidate_meeting)";
            Dictionary<String,Object> dico = new Dictionary<String,Object>();
            dico.Add("@note",report.note);
            dico.Add("@link",report.link);
            dico.Add("@xpNote",report.xpNote);
             dico.Add("@nsNote",report.nsNote);
             dico.Add("@jobIdealNote",report.jobIdealNote);
            dico.Add("@pisteNote",report.pisteNote);
            dico.Add("@pieCouteNote",report.pieCouteNote);
            dico.Add("@locationNote",report.locationNote);
            dico.Add("@EnglishNote",report.EnglishNote);
            dico.Add("@nationalityNote",report.nationalityNote);
            dico.Add("@competences",report.competences);
            dico.Add("@fid_candidate_meeting",idCandidat);
            queryExecute(sql,dico,null);
         }catch(Exception exc){
            throw new SqlCustomException(this.GetType().Name,exc.Message);
         }
        }
          /// <summary>
          /// Update the report Content
          /// </summary>
          /// <param name="report"></param>
          /// <param name="idCandidat"></param>
        public void updateReport(Report report, int idCandidat)
        {
           try{ 
           checkReport(report);
           string sqlUpdate = "update  meeting set note =@note,link=@link,xpNote=@xpNote,nsNote=@nsNote,jobIdealNote=@jobIdealNote,pisteNote=@pisteNote,pieCouteNote=@pieCouteNote,locationNote=@locationNote,EnglishNote=@EnglishNote,nationalityNote=@nationalityNote,competences=@competences where fid_candidate_meeting=@fid_candidate_meeting";
           Dictionary<String,Object> dico = new Dictionary<String,Object>();
           dico.Add("@note",report.note);
           dico.Add("@link",report.link);
           dico.Add("@xpNote",report.xpNote);
           dico.Add("@nsNote",report.nsNote);
           dico.Add("@jobIdealNote",report.jobIdealNote);
           dico.Add("@pisteNote",report.pisteNote);
           dico.Add("@pieCouteNote",report.pieCouteNote);
           dico.Add("@locationNote",report.locationNote);
           dico.Add("@EnglishNote",report.EnglishNote);
           dico.Add("@nationalityNote",report.nationalityNote);
           dico.Add("@competences",report.competences);
           dico.Add("@fid_candidate_meeting",idCandidat);
           queryExecute(sqlUpdate,dico,null);
           }catch(Exception exc){
            throw new SqlCustomException(this.GetType().Name,exc.Message);
           }
        }

        private void checkEmail(string email){
            try{
                if(String.IsNullOrEmpty(email)){
                    throw new Exception("Votre champ email est vide ");
                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if(!match.Success){
                    throw new Exception("L'email n'est pas conforme");
                }
            }catch(Exception exc){
                throw exc;
            }
            

        }
          /// <summary>
          /// Extract the candidate By Email
          /// </summary>
          /// <param name="email">Candidate Email</param>
          /// <param name="token">User token</param>
          /// <returns></returns>
         public ArrayList searchCandidateFromEmail(string email, string token)
        {
            ArrayList output;
            try{
                checkEmail(email);
                if(String.IsNullOrEmpty(token)){
                    throw new Exception("Le token du candidat est vide");
                }
                string sql = "SELECT * from candidate,meeting where candidate.id = meeting.fid_candidate_meeting and candidate.email=@email";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@email",email);
                LinkedList<String> results = new LinkedList<String>(); 
                results.AddLast("id");
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("sexe");
                results.AddLast("phone");
                results.AddLast("email");
                results.AddLast("actions");
                results.AddLast("annee");
                results.AddLast("lien");
                results.AddLast("crCall");
                results.AddLast("NS");
                results.AddLast("pluginType");
                results.AddLast("note");
                results.AddLast("link");
                results.AddLast("xpNote");
                results.AddLast("nsNote");
                results.AddLast("jobIdealNote");
                results.AddLast("pisteNote");
                results.AddLast("pieCouteNote");
                results.AddLast("locationNote");
                results.AddLast("EnglishNote");
                results.AddLast("nationalityNote");
                results.AddLast("competences");
                output = queryExecute(sql,dico,results);
                if(output == null || output.Count==0){
                    throw new Exception("Aucun candidat ne possede vos criteres de recherche");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return output;
        }

          /// <summary>
          /// Extract the Candidate List By Action
          /// </summary>
          /// <param name="actions">Candidate Action</param>
          /// <param name="token">User Token</param>
          /// <returns></returns>
        public ArrayList searchCandidateByAction(string actions,string token){
            ArrayList output;
            try{
                if(String.IsNullOrEmpty(actions)){
                    throw new Exception("L'action de l'utilisateur est vide ");
                }
                string sql = "select * from candidate where actions = @actions";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@actions",actions);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("phone");
                results.AddLast("email");
                results.AddLast("actions");
                results.AddLast("annee");
                results.AddLast("lien");
                results.AddLast("crCall");
                results.AddLast("zipcode");
                results.AddLast("NS");
                output = queryExecute(sql,dico,results);
                if(output == null || output.Count==0){
                    throw new Exception($"Aucun candidat ne possede vos criteres d'action : {actions}");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return output;
        }

         /// <summary>
          /// Add Email template Inside the System.
          /// </summary>
          /// <param name="emailTemplate"></param>
        public void addEmailTemplates(Template emailTemplate){
          
            try{
                string sql = "insert into message (titre_message,contenu_message) VALUES (@title,@content)";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@title",emailTemplate.title);
                dico.Add("@content",emailTemplate.content);
                queryExecute(sql,dico,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
             
        }
          /// <summary>
          /// Email template Exist inside the System
          /// </summary>
          /// <param name="title"></param>
          /// <returns></returns>
        public bool emailTemplateExist(string title){
            ArrayList output;
            try{
                if(String.IsNullOrEmpty(title)){
                    throw new Exception("Le titre du template d'email est vide");
                }
                string sql = "select count(*) as nb from message where titre_message = @title";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@title",title);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                output = queryExecute(sql,dico,results);
                if(output == null || output.Count==0){
                    throw new Exception($"Aucun template d'email ne possede ce titre : {title}");
                }
                Dictionary<string,string> dicoWithDatas = (Dictionary<string,string>)output[0];
                int existValue = Int32.Parse(dicoWithDatas["nb"]);
                return Convert.ToBoolean(existValue);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
           
        }
         /// <summary>
          /// Extract Email Template Beetween limite
          /// </summary>
          /// <param name="limite1">limit 1</param>
          /// <param name="limite2">limit 2</param>
          /// <returns></returns>
        public ArrayList emailTemplateTiltes(int limite1,int limite2){
            ArrayList output;
            try{
                string sql = "select titre_message from message limit @lim1, @lim2";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@lim1",limite1);
                dico.Add("@lim2",limite2);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("titre_message");
                output = queryExecute(sql,dico,results);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            return output;
        }

          /// <summary>
          /// get Template Content from the Title
          /// </summary>
          /// <param name="title"></param>
          /// <returns></returns>
        public ArrayList emailTemplateContentFromTitle(string title){
             ArrayList output;
             try{
                if(String.IsNullOrEmpty(title)){
                    throw new Exception("Le titre de votre email est vide");
                }
                string sql = "select contenu_message from message where titre_message=@title";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@title",title);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("contenu_message");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                     throw new Exception($"Vous ne possedez aucun contenu d'email ayant ce titre : {title}");
                }
             }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
             }
             return output;
        }
         /// <summary>
          /// Update Template Email From Title.
          /// </summary>
          /// <param name="title"></param>
          /// <param name="content"></param>
        public void updateTemplateEmailFromTitle(string title,string content){
             try{
                if(String.IsNullOrEmpty(title)){
                    throw new Exception("Le titre de votre email est vide");
                }
                if(String.IsNullOrEmpty(content)){
                    throw new Exception("Le contenu de votre email est vide");
                }
                string sql = "update message set contenu_message=@content where titre_message=@title";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@content",content);
                dico.Add("@title",title);
                queryExecute(sql,dico,null);
             }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
             }
        }

          /// <summary>
          /// Dete template email from the title file
          /// </summary>
          /// <param name="title">File Title</param>
        public void deleteTemplateEmailFromTitle(string title){
            try{
                if(String.IsNullOrEmpty(title)){
                    throw new Exception("Le titre de votre email est vide");
                }
                string sql = "delete from message where titre_message=@title";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@title",title);
                queryExecute(sql,dico,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }
        /// <summary>
         /// Extract the candidate by this Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public ArrayList searchCandidateById(int id){
            ArrayList output = null;
            try{
                if(id == 0){
                    throw new Exception("L'id du candidate ne peut pas etre egale à 0");
                }
                string sql = "select nom,prenom from candidate where id=@id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",id);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nom");
                results.AddLast("prenom");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                     throw new Exception($"Aucun candidat n'existe avec l'identifiant : {id}");
                }
                return output;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

         /// <summary>
          /// The candidate have already a remind.
          /// </summary>
          /// <param name="id"></param>
          /// <returns></returns>
        public bool remindExistByCandidate(int id){
            ArrayList output = null;
            try{
                if(id == 0){
                throw new SqlCustomException(this.GetType().Name,"L'identifiant ne peut etre egale a 0");
                }
                string sql = "select count(*) as nb from remind where fid_candidate_remind=@id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",id);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Aucun remind n'existe avec l'identifiant suivant : {id}");
                }
                Dictionary<string,string> datas = (Dictionary<string,string>)output[0];
                int existe = Int32.Parse(datas["nb"]);
                return Convert.ToBoolean(existe);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            
        }
          /// <summary>
          /// Extract the candidate list without report.
          /// </summary>
          /// <returns></returns>
        public LinkedList<Candidat> getCandidatWithoutReport()
        {
            ArrayList output = null;
            try{
                string sql = "SELECT * from candidate where id not in (select fid_candidate_meeting from meeting) order by @id @desc";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                Dictionary<string,string> dataByLine = null;
                LinkedList<Candidat> candidateList = new LinkedList<Candidat>();
                dico.Add("@id","id");
                dico.Add("@desc","desc");
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("email");
                results.AddLast("phone");
                results.AddLast("zipcode");
                results.AddLast("actions");
                results.AddLast("sexe");
                results.AddLast("annee");
                results.AddLast("lien");
                results.AddLast("crCall");
                results.AddLast("NS");
                output = queryExecute(sql,dico,results);
                int numberDatasReturned = output.Count;
                if(numberDatasReturned == 0){
                     throw new Exception($"Toutes les fiches candidat ont été liées à une fiche entretien");
                }
               
                for(int i = 0;i<numberDatasReturned;i++){
                    dataByLine = (Dictionary<string,string>) output[i];
                 
                    Candidat candidat = new Candidat(dataByLine["nom"],dataByLine["prenom"],dataByLine["email"],dataByLine["zipcode"],dataByLine["phone"],dataByLine["actions"],dataByLine["annee"],dataByLine["sexe"],dataByLine["lien"],dataByLine["crCall"],dataByLine["NS"]);
                    candidateList.AddLast(candidat);
                } 
                return candidateList;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
         
        }
          /// <summary>
          /// Get the remind Informations from the Calendar
          /// </summary>
          /// <returns></returns>
        public ArrayList getRemindInformationForCalendar(){
                ArrayList output = null;
                try{
                    string sql = "select * from  candidate c ,remind r where r.fid_candidate_remind = c.id";
                    Dictionary<String,Object> dico = new Dictionary<String,Object>();
                    dico.Add("@id","c.id");
                    LinkedList<String> results = new LinkedList<String>();
                    results.AddLast("nom");
                    results.AddLast("prenom");
                    results.AddLast("email");
                    results.AddLast("dates");
                    results.AddLast("zipcode");
                    results.AddLast("actions");
                    output = queryExecute(sql,dico,results);
                    if(output.Count == 0){
                        throw new Exception("Il n'existe aucun remind");
                    }
                    return output;
                }catch(Exception exc){
                    throw new SqlCustomException(this.GetType().Name,exc.Message);
                }
        }
        /// <summary>
          /// Verify if the choice exist Inside the System.
          /// </summary>
          /// <param name="choice">choice Name</param>
        public void stateChoiceExist(string choice){
            ArrayList output = null;
            try{
                string sql = "select count(*) as nb from mlcandidate where sales = @choice";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@choice",choice);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                output = queryExecute(sql,dico,results);
                Dictionary<string,string> numbersDatasRetournedByChoice = (Dictionary<string,string>)output[0];
                int valueReturned = Int32.Parse(numbersDatasRetournedByChoice["nb"]);
                if(valueReturned == 0){
                    throw new Exception($"Votre choix {choice} n'existe pas ");
                }
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }
         /// <summary>
          /// Extract Datas from the Choice.
          /// </summary>
          /// <param name="choice">choice from the kaggle Dataset</param>
          /// <returns></returns>
        public ArrayList getDatasFromChoice(string choice){
            ArrayList output = null;
            try{
                string sql = "select * from mlcandidate where sales = @choice";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@choice",choice);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("satisfaction_level");
                results.AddLast("last_evaluation");
                results.AddLast("number_project");
                results.AddLast("average_montly_hours");
                results.AddLast("time_spend_company");
                results.AddLast("work_accident");
                results.AddLast("promotion_last_5years");
                results.AddLast("sales");
                results.AddLast("salary");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Votre choix {choice} n'existe pas");
                }
                return output;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            
        }
           /// <summary>
          /// Disconnect the User
          /// </summary>
          /// <param name="id"></param>
        public void disconnectUser(int id){
            try{
                if(id == 0){
                    throw new Exception("L'identifiant de l'utilisateur n'est pas conforme");
                }
                string sql = "update user set session_id = null where id = @id";
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@id",id);
                queryExecute(sql,param,null); 
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
            
        }

          /// <summary>
          /// Extract the UserEmail from the Id
          /// </summary>
          /// <param name="id">UserID</param>
          /// <returns></returns>
        public string getUserEmailFromId(int id){
            try{
                ArrayList output = null;
                if(id == 0){
                    throw new Exception("L'identifiant de l'utilisateur n'est pas conforme");
                }
                string sql = "select email from user where id = @id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",id);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("email");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception("Aucun utilisateur ne possede cet identifiant");
                }
                Dictionary<string,string> userEmailData = (Dictionary<string,string>) output[0];
                return userEmailData["email"];
            }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }
          /// <summary>
          /// Change the JobState.
          /// </summary>
          /// <param name="id">JobId</param>
        public void changeJobState(int id){
            try{
                ArrayList output = null;
                if(id <= 0){
                    throw new Exception("Votre job n'est pas conforme car l'identifiant est inferieur ou egale à 0");
                }
                string sql = "update remind set finish = true where id = @id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",id);
                queryExecute(sql,dico,null);
            }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }
          /// <summary>
          /// Verify if the Remind exist for a specific JobId
          /// </summary>
          /// <param name="id">JobId</param>
          /// <returns></returns>
        public bool remindExistByJob(int id){
            try{
                ArrayList output = null;
                if(id <= 0){
                    throw new Exception("Votre job n'est pas conforme car il est inferieur ou égale à 0");
                }
                string sql = "select count(*) as nb from remind where id = @id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",id);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Aucun job n'existe avec l'identifiant {id}");
                }
                Dictionary<string,string> userEmailData = (Dictionary<string,string>) output[0];
                int nbValue = Int32.Parse(userEmailData["nb"]);
                if(nbValue > 1){
                    throw new Exception("Vous devez avoir uniquement 1 job avec le meme identifiant");
                }
                if(nbValue == 0){
                    return false;
                }
                return true;
            }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

          /// <summary>
          /// Verify if the remind had already update
          /// </summary>
          /// <param name="id">Remind ID</param>
        public void remindAlreadyUpdated(int id){
            try{
                ArrayList output = null;
                if(id <= 0){
                    throw new Exception("Votre job n'est pas conforme car il est inferieur ou égale à 0");
                }
                string sql = "select count(*) as nb from remind where id = @id and finish = 1";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",id);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Aucun job n'existe avec l'identifiant {id}");
                }
                Dictionary<string,string> userEmailData = (Dictionary<string,string>) output[0];
                int nbValue = Int32.Parse(userEmailData["nb"]);
                if(nbValue == 1){
                    throw new Exception($"Votre job avec l'identifiant {id} a déja été envoyé");
                }
            }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

          /// <summary>
          /// Get The Candidat Id From the remind list
          /// </summary>
          /// <param name="userId">UserID</param>
          /// <returns></returns>
        public int getLastCandidateIdFromRemind(int userId){
            try{
                ArrayList output = null;
                if(userId <= 0){
                    throw new Exception($"L'id de votre candidat ({userId}) n'est pas conforme ");
                }
                string sql = "select max(id) as total from remind where fid_candidate_remind = @id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",userId);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("total");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Aucun job n'existe avec l'identifiant {userId}");
                }
                Dictionary<string,string> userEmailData = (Dictionary<string,string>) output[0];
                int nbValue = Int32.Parse(userEmailData["total"]);
                if(nbValue == 0){
                    throw new Exception($"Votre utilisateur avec l'identifiant ({userId}) ne possede aucun remind");
                }
                return nbValue;
            }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

         /// <summary>
          /// Extract the candidate Email from The candidate ID
          /// </summary>
          /// <param name="candidateId">Candidate Id</param>
          /// <returns></returns>
        public string getCandidateEmailFromId(int candidateId){
            try{
                
                ArrayList output = null;
                if(candidateId <= 0){
                    throw new Exception($"L'id de votre candidat ({candidateId}) n'est pas conforme ");
                }
                string sql = "select email from candidate where id=@id";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@id",candidateId);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("email");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Aucun job n'existe avec l'identifiant {candidateId}");
                }
                Dictionary<string,string> userEmailData = (Dictionary<string,string>) output[0];
                string emailCandidat = userEmailData["email"];
                return emailCandidat;
            }catch(Exception exc){
                 throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }
         /// <summary>
          /// Verify if the plugin exist
          /// </summary>
          /// <param name="plugin"></param>
          /// <returns></returns>
        public bool pluginExist(Plugin plugin){
            try{
                ArrayList output = null;
                pluginIsNullOrNameIsEmpty(plugin);
                string sql = "select count(*) as nb from pluginsInfo where pluginName = @name";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@name",plugin.name);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("nb");
                output = queryExecute(sql,dico,results);
                Dictionary<string,string> numberPlugins = (Dictionary<string,string>)output[0];
                int pluginCount = Int32.Parse(numberPlugins["nb"]);
                if(pluginCount==0){
                    return false;
                }
                return true;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,$"{exc.Message}");
            }
        }

         /// <summary>
          /// Add plugin in the system (This feature is load when start the system)
          /// </summary>
          /// <param name="plugin"></param>
        public void addPlugin(Plugin plugin){
            try{
                pluginIsNullOrNameIsEmpty(plugin);
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@name",plugin.name);
                queryExecute("insert into pluginsInfo (pluginName) values (@name)",param,null);
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,$"{exc.Message}");
            }
            

        }

        private void pluginIsNullOrNameIsEmpty(Plugin plugin){
            if(plugin == null){
                throw new Exception("Les informations lie au plugin ne peuvent pas etre vides ");
            }
            if(String.IsNullOrEmpty(plugin.name)){
                throw new Exception("Le nom du plugin ne peut etre vide");
            }
        }
          /// <summary>
          /// Extract the plugin List
          /// </summary>
          /// <returns></returns>
        public ArrayList getPluginList(){
            try{
                ArrayList output = null;
                string sql = "select pluginName from pluginsInfo";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@table","pluginsInfo");
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("pluginName");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception("Vous ne possedez aucun plugin");
                }
                return output;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,$"{exc.Message}");
            }
        }

          /// <summary>
          /// Get the plugin choice from the candidate
          /// </summary>
          /// <param name="emailCandidat"></param>
          /// <returns></returns>
        public string getPluginChoiceFromCandidate(string emailCandidat){
            try{
                
                if(String.IsNullOrEmpty(emailCandidat)){
                    throw new Exception("L'email du candidat passe en parametre est vide");
                }
                ArrayList output = null;
                string sql = "select pluginType from candidate where email = @email";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@email",emailCandidat);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("pluginType");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception("Vous ne possedez aucun plugin type pour votre candidat");
                }
                Dictionary<string,string> datas = (Dictionary<string,string>)output[0];
                if(String.IsNullOrEmpty(datas["pluginType"])){
                    throw new Exception("L'email du candidat passe en parametre est vide");
                }
                return datas["pluginType"];
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,$"{exc.Message}");
            }
        }
          /// <summary>
          /// Delete the candidate by Id
          /// </summary>
          /// <param name="id">Candidate ID</param>
        public void deleteCandidateById(int id){
             try{
                if(id == 0){
                    throw new Exception("L'identifiant de l'utilisateur n'est pas conforme");
                }
                string sql = "delete from candidate where id = @id";
                Dictionary<String,Object> param = new Dictionary<String,Object>();
                param.Add("@id",id);
                queryExecute(sql,param,null); 
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

          /// <summary>
          /// Extract Candidate By specific Email
          /// </summary>
          /// <param name="emailCandidat"></param>
          /// <returns></returns>
        public ArrayList searchCandidateWithSpecificEmail(string emailCandidat){
              try{    
                if(String.IsNullOrEmpty(emailCandidat)){
                    throw new Exception("L'email du candidat passe en parametre est vide");
                }
                ArrayList output = null;
                string sql = "select * from candidate where email = @email";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@email",emailCandidat);
                LinkedList<String> results = new LinkedList<String>();
                results.AddLast("id");
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("phone");
                results.AddLast("email");
                results.AddLast("zipcode");
                results.AddLast("sexe");
                results.AddLast("actions");
                results.AddLast("annee");
                results.AddLast("lien");
                results.AddLast("crCall");
                results.AddLast("pluginType");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Vous ne possedez aucun critere de recherche pour l'adresse email : {emailCandidat}");
                }   
                return output;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }


          /// <summary>
          /// Get Candidate With or Without report.
          /// I am using the limite for not add all the Candidate in the CLR.
          /// </summary>
          /// <param name="limite1"></param>
          /// <param name="limite2"></param>
          /// <returns></returns>
        public ArrayList getCandidatesListWithLimite(int limite1,int limite2){
            try{
                if(limite1 > limite2){
                    throw new Exception("La limite1 ne peut pas etre superieur à la limite 2");
                }
                ArrayList output = null;
                string sql = "select * from candidate  limit @limite1,@limite2";
                Dictionary<String,Object> dico = new Dictionary<String,Object>();
                dico.Add("@limite1",limite1);
                dico.Add("@limite2",limite2);
                LinkedList<String> results = new LinkedList<String>();  
                results.AddLast("nom");
                results.AddLast("prenom");
                results.AddLast("sexe");
                results.AddLast("phone");
                results.AddLast("email");
                results.AddLast("zipcode");
                results.AddLast("actions");
                results.AddLast("annee");
                results.AddLast("lien");
                results.AddLast("CrCall");
                results.AddLast("NS");
                output = queryExecute(sql,dico,results);
                if(output.Count == 0){
                    throw new Exception($"Vous n'avez aucun candidat entre les limites {limite1} et {limite2}");
                }   
                return output;
            }catch(Exception exc){
                throw new SqlCustomException(this.GetType().Name,exc.Message);
            }
        }

    }
}
