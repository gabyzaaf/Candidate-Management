using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.Adapter.Inteface;
using core.success;
using exception.ws;
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

       [HttpGet("{error}", Name = "GetErrorsCandidate")]
        public IActionResult ErrorList(ArrayList errors)
        {
            
            return new ObjectResult(errors);
        }
          

    }
}