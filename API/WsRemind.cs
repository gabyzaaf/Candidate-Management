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
using core.remind;

/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.API
{

    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class RemindController:Controller
    {
            /// <summary>
            /// Extract the candidate Without Report.
            /// </summary>
            /// <param name="token">User token</param>
            /// <returns></returns>
            [HttpGet("candidate/withoutRapport/{token}")]
            public IActionResult getCandidateWithoutReport(string token){
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

            /// <summary>
            /// Extract the Reminds Information.
            /// </summary>
            /// <param name="token">User token</param>
            /// <returns></returns>
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
            /// <summary>
            /// This method will get Candidate Information by work type.
            /// The dataset was in Kaggle.
            /// </summary>
            /// <param name="choice"></param>
            /// <param name="token"></param>
            /// <returns></returns>
            [HttpGet("stat/mlcandidate/{choice}/{token}")]
            public IActionResult getMlCandidateInformationByWorkChoice(string choice,string token){
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
            /// <summary>
            /// Update the Job state for the Remind.
            /// </summary>
            /// <param name="user">Feature for the User</param>
            /// <returns></returns>
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

                      new WsCustomeInfoException("S001",$"L'email du candidat {user.candidateEmail} a bien ete envoye ");
                      State state = new State(){code=1,content=$"Le job avec l'id {idJob} a été modifié ",success=true};
                     return new ObjectResult(state);
                }catch(Exception exc){
                    new WsCustomeException(this.GetType().Name,exc.Message);
                    State state = new State(){code=3,content=exc.Message,success=false};
                    return CreatedAtRoute("SendRemindsError", new { error = state },state);
                }
            }
            /// <summary>
            /// You get the plugin list available, this method is load when the System Start.
            /// </summary>
            /// <returns></returns>
            [HttpGet("display/plugins/list")]
            public IActionResult changeJobState(){
               ArrayList pluginList = new ArrayList();
               try{
                    IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                    pluginList = isql.getPluginList();
                    return new ObjectResult(pluginList);
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