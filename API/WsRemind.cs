using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.Adapter.Inteface;
using core.success;
using exception.ws;
using System.Collections.Generic;
using Factory;

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



            [HttpGet("{error}", Name = "SendRemindsError")]
            public IActionResult ErrorList(ArrayList errors)
            {
                return new ObjectResult(errors);
            }
    }
}