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
    public class UserManagementController:Controller
    {
        [HttpPost("manager/add")]
        public IActionResult updateContentEmailFromTitle([FromBody]UserData userInformation){
            return new ObjectResult(userInformation);
        }   
           
    }
}