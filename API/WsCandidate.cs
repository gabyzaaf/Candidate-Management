using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.Adapter.Inteface;
using core.success;
using exception.ws;
using Candidate_Management.CORE.LoadingTemplates;
using System.Collections.Generic;
using core.user;
using Candidate_Management.CORE;

namespace Candidate_Management.API
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class CandidateController:Controller
    {
        
 
        [HttpGet("actions/{candidateaction}/{token}")]
        public IActionResult UserActionFromCandidate(string candidateaction,string token){
            ArrayList candidatListe = null;
            try{
                if(String.IsNullOrEmpty(token)){
                     throw new Exception("Le token est vide");
                }
               
                if(String.IsNullOrEmpty(candidateaction)){
                    throw new Exception("La variable est vide");
                }
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                if(!isql.UserCanRead(token)){
                    throw new Exception("Erreur vous n'avez pas les droits necessaire pour lire");
                }
                candidatListe = isql.searchCandidateByAction(candidateaction,token);
                return new ObjectResult(candidatListe);
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=4,content=exc.Message,success=false});
                return CreatedAtRoute("GetErrorsCandidate", new { error = errorList },errorList);
            }
        }

        [HttpGet("email/template/titles/{token}/{lim1}/{lim2}")]
        public IActionResult titleCandidate(string token,int lim1,int lim2){
            ArrayList emailTitles = null;
            try{
                if(String.IsNullOrEmpty(token)){
                     throw new Exception("Le token est vide");
                }
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                if(!isql.UserCanRead(token)){
                    throw new Exception("Erreur vous n'avez pas les droits necessaire pour lire");
                }
                emailTitles = isql.emailTemplateTiltes(lim1,lim2);
                return new ObjectResult(emailTitles);
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=5,content=exc.Message,success=false});
                return CreatedAtRoute("GetErrorsCandidate", new { error = errorList },errorList);
            }
        }

        [HttpGet("template/email/{token}/{title}")]
        public IActionResult contentEmail(string token,string title){
            
             ArrayList emailContent = null;
             try{
                if(String.IsNullOrEmpty(token)){
                     throw new Exception("Le token est vide");
                }
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                if(!isql.UserCanRead(token)){
                        throw new Exception("Erreur vous n'avez pas les droits necessaire pour lire");
                }
                emailContent = isql.emailTemplateContentFromTitle(title);
                return new ObjectResult(emailContent);
             }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=5,content=exc.Message,success=false});
                return CreatedAtRoute("GetErrorsCandidate", new { error = errorList },errorList);
             }
        }

        [HttpPost("template/email/update")]
        public IActionResult updateContentEmailFromTitle([FromBody]Template emailTemplate){
            
            try{
                
                if(emailTemplate == null){
                    throw new Exception("L'email template n'existe pas");
                }
                
                if(String.IsNullOrEmpty(emailTemplate.token)){
                    throw new Exception("Le token n'existe pas, vous devez le renseigner");
                }
                
                if(String.IsNullOrEmpty(emailTemplate.content)){
                    throw new Exception("Le contenu de l'email n'existe pas ");
                }
                
                if(String.IsNullOrEmpty(emailTemplate.title)){
                    throw new Exception("Le titre du fichier template email n'existe pas ");
                }
                
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanUpdate(emailTemplate.token);
                
                bool templateExist = isql.emailTemplateExist(emailTemplate.title);

                if(!templateExist){
                    throw new Exception($"Aucun template existe avec votre titre : {emailTemplate.title}");
                }
                isql.updateTemplateEmailFromTitle(emailTemplate.title,emailTemplate.content);
                return new ObjectResult(new State(){code=4,content="Le template d'email a bien ete modifie",success=true}); 
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=5,content=exc.Message,success=false});
                return CreatedAtRoute("GetErrorsCandidate", new { error = errorList },errorList);
            } 
        }

        [HttpPost("template/email/delete")]
        public IActionResult deleteContentEmailFromTitle([FromBody]Template emailTemplate){
           
            try{
                if(emailTemplate == null){
                    throw new Exception("L'email template n'existe pas");
                }
                if(String.IsNullOrEmpty(emailTemplate.token)){
                    throw new Exception("Le token n'existe pas, vous devez le renseigner");
                }
                if(String.IsNullOrEmpty(emailTemplate.title)){
                    throw new Exception("Le titre du fichier template email n'existe pas ");
                }
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanDelete(emailTemplate.token);
                bool templateExist = isql.emailTemplateExist(emailTemplate.title);
                if(!templateExist){
                    throw new Exception($"Aucun template existe avec votre titre : {emailTemplate.title}");
                }
                isql.deleteTemplateEmailFromTitle(emailTemplate.title);
                return new ObjectResult(new State(){code=4,content=$"Le template d'email ayant le titre {emailTemplate.title} à bien ete supprime",success=true}); 
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=5,content=exc.Message,success=false});
                return CreatedAtRoute("GetErrorsCandidate", new { error = errorList },errorList);
            } 
        }

         
        [HttpPost("delete/byEmail")]
        public IActionResult deleteCandidateById([FromBody]CandidateDelete candidateDelete){
            try{
                if(candidateDelete == null){
                    throw new Exception("Les informations de l'utilisateur passé en paramètre sont vides");
                }
                if(String.IsNullOrEmpty(candidateDelete.token)){
                    throw new Exception("Le token de l'utilisateur ne peut etre vide");
                }
                if(String.IsNullOrEmpty(candidateDelete.email)){
                    throw new Exception("L'email du candidat ne peut etre vide");
                }
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanDelete(candidateDelete.token);
                ArrayList CandidatesInformations = isql.searchCandidateFromEmail(candidateDelete.email,candidateDelete.token);
                Dictionary<string,string> CandidateInformation = (Dictionary<string,string>)CandidatesInformations[0];
                int candidateId = Int32.Parse(CandidateInformation["id"]);
                isql.deleteCandidateById(candidateId);
                return new ObjectResult(new State(){code=4,content=$"Le Candidat a bien été supprimé",success=true});
            return new ObjectResult(candidateDelete);
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=5,content=exc.Message,success=false});
                return CreatedAtRoute("GetErrorsCandidate", new { error = errorList },errorList);
            } 
        }
          

       [HttpGet("{error}", Name = "GetErrorsCandidate")]
        public IActionResult ErrorList(ArrayList errors)
        {
            return new ObjectResult(errors);
        }

        
          
      

    }
}