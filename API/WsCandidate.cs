using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.Adapter.Inteface;
using core.success;
using exception.ws;
using Candidate_Management.CORE.LoadingTemplates;
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
                if(String.IsNullOrEmpty(emailTemplate.getContent())){
                    throw new Exception("Le contenu de l'email n'existe pas ");
                }
                return null;
                // creer la requete sql ...
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