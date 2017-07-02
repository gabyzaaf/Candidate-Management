using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.Adapter.Inteface;
using core.success;
using exception.ws;
using System.Collections.Generic;
using Factory;
using Candidate_Management.CORE;
using Candidate_Management.CORE.Exceptions;
namespace Candidate_Management.API
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class RemindController:Controller
    {
            [HttpGet("candidate/withoutRapport/{token}")]
            public IActionResult getTheRemindsForTheCalendar(string token){
                try{
                   
                    if(String.IsNullOrEmpty(token)){
                        throw new Exception("Le token ne peut etre vide");
                    }
                    IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                    isql.UserCanRead(token);
                    return new ObjectResult(isql.getCandidatWithoutReport());
                }catch(Exception exc){
                    new WsCustomeException(this.GetType().Name,exc.Message);
                    State state = new State(){code=1,content=exc.Message,success=false};
                    return CreatedAtRoute("SendRemindsError", new { error = state },state);
                }
            }


            [HttpGet("calendar/remind/informations/{token}")]
            public IActionResult getTheRemindsInformations(string token){
                try{
                    if(String.IsNullOrEmpty(token)){
                        throw new Exception("Le token ne peut etre vide");
                    }
                    IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                    isql.UserCanRead(token);
                    return new ObjectResult(isql.getRemindInformationForCalendar());
                }catch(Exception exc){
                    new WsCustomeException(this.GetType().Name,exc.Message);
                    State state = new State(){code=2,content=exc.Message,success=false};
                    return CreatedAtRoute("SendRemindsError", new { error = state },state);
                }
            }

            [HttpGet("stat/mlcandidate/{choice}/{token}")]
            public IActionResult getTheRemindsInformations(string choice,string token){
                try{
                    if(String.IsNullOrEmpty(token)){
                        throw new Exception("Le token ne peut etre vide");
                    }
                    if(String.IsNullOrEmpty(choice)){
                        throw new Exception("Votre choix ne peut etre vide");
                    }
                    IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                    isql.UserCanRead(token);
                    isql.stateChoiceExist(choice);
                    return new ObjectResult(isql.getDatasFromChoice(choice));
                }catch(Exception exc){
                    new WsCustomeException(this.GetType().Name,exc.Message);
                    State state = new State(){code=3,content=exc.Message,success=false};
                    return CreatedAtRoute("SendRemindsError", new { error = state },state);
                }
                
            }

            [HttpPost("change/job/state/")]
            public IActionResult changeJobState([FromBody]UserFeature user){
                try{
                    int idJob = user.jobId;
                    
                    if(idJob <= 0){
                        throw new Exception("L'id ne peut etre inferrieur ou egale à 0");
                     }
                     IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                     bool jobExist = isql.remindExistByJob(idJob);
                     
                     if(!jobExist){
                        throw new Exception($"Le job spécifié avec l'identifiant {idJob} n'existe pas ");
                     }
                      
                      isql.remindAlreadyUpdated(idJob);
                      isql.changeJobState(idJob);

                      new WsCustomeInfoException("S001",$"The email for the candidate {user.candidateEmail} had been send ");
                      State state = new State(){code=1,content=$"Le job avec l'id {idJob} a été modifié ",success=true};
                     return new ObjectResult(state);
                }catch(Exception exc){
                    new WsCustomeException(this.GetType().Name,exc.Message);
                    State state = new State(){code=3,content=exc.Message,success=false};
                    return CreatedAtRoute("SendRemindsError", new { error = state },state);
                }
                
                
            }

            [HttpGet("{error}", Name = "SendRemindsError")]
            public IActionResult ErrorList(ArrayList errors)
            {
                return new ObjectResult(errors);
            }
    }
}