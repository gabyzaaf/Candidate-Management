using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
namespace Candidate_Management.API
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class CandidateController:Controller
    {
        
 
        [HttpGet("actions/{candidateaction}")]
        public IActionResult loadLibrary(string candidateaction){
            try{
                if(String.IsNullOrEmpty(candidateaction)){
                    throw new Exception("La variable est vide");
                }
                return new ObjectResult("OK");
            }catch(Exception exc){
                return new ObjectResult($"KO - {exc.Message}");
            }
        }

         
          

    }
}