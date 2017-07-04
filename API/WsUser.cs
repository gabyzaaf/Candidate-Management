using System;
using System.Collections;
using core.candidat;
using core.report;
using core.success;
using core.user;
using Core.Adapter.Inteface;
using exception.ws;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Candidate_Management.CORE.Exceptions;

namespace API.wsUser
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class UserController : Controller{

        private static readonly string specificException = "Object reference not set to an instance of an object.";

        [HttpPost("admin/auth/")]
        public IActionResult  GetAuthentification([FromBody]User user){
            try{
                checkUser(user);
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.Authentification(user.email,user.password);
                User newUser = isql.GenerateToken();
                if(!isql.verifyTheTokenExist(newUser.sessionId)){
                    isql.addTokenToUser(newUser.sessionId,user.email);
                    newUser.email = user.email;
                }else{
                    throw new Exception("Le token de l'utilisateur existe deja");
                }
                new WsCustomeInfoException("DC01",$"The user with the email {newUser.email} is connected");
                return new ObjectResult(newUser);
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                State state = new State(){code=1,content=exc.Message,success=false};
                return CreatedAtRoute("GetNote", new { error = state },state);
                
            }  
        }
       


        [HttpGet("{error}", Name = "GetNote")]
        public IActionResult GetById(State error)
        {
            Console.WriteLine("In GetNote function");
            return new ObjectResult(error);
        }

        [HttpGet("{error}", Name = "GetErrors")]
        public IActionResult ErrorList(ArrayList errors)
        {
            Console.WriteLine("In GetNote function");
            return new ObjectResult(errors);
        }

        [HttpGet("Candidates/recherche/{name}/{token}")]
         public IActionResult searchCandidate(string name,string token){
            try{
               
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                ArrayList candidatListe = null;
                if(isql.UserCanRead(token)){
                    
                    candidatListe = isql.searchCandidate(name,token);
                }else{
                    throw new Exception("Vous n'avez pas le droit de lecture");
                }
                return new ObjectResult(candidatListe);
            }catch(Exception exception){
                new WsCustomeException(this.GetType().Name,exception.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=3,content=exception.Message,success=false});

                return CreatedAtRoute("GetErrors", new { error = errorList },errorList);
            }
            
         }
         [HttpGet("Candidates/recherche/mobile/{name}/{token}")]
          public IActionResult searchCandidateMobile(string name,string token){
            try{
               
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                ArrayList candidatListe = null;
                if(isql.UserCanRead(token)){
                    
                    candidatListe = isql.searchCandidateMobile(name,token);
                }else{
                    throw new Exception("Vous n'avez pas le droit de lecture");
                }
                return new ObjectResult(candidatListe);
            }catch(Exception exception){
                new WsCustomeException(this.GetType().Name,exception.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=3,content=exception.Message,success=false});

                return CreatedAtRoute("GetErrors", new { error = errorList },errorList);
            }
            
         }
         

          [HttpPost("add/candidat/")]
          /** 
            1) Verifier si l'utilisateur à le droit de modification.
            2) Verifier si le candidat existe deja.
            3) Recuperer l'id de l'utilisateur afilié au candidat
            4) Ajouter le candidat
            5) Ajoute le type d'action [FromBody]
          */
        public IActionResult addCandidat([FromBody] Candidat candidat){
            try{
                
                checkCandidat(candidat);
                
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanUpdate(candidat.session_id);
                
                if(isql.CandidatAlreadyExist(candidat)){
                     throw new Exception("Le candidat est deja existant dans votre systeme");
                } 
                
                int idUser = isql.getIdFromToken(candidat.session_id);
                isql.addCandidate(candidat,idUser);
                int idCandidat = isql.getIdFromCandidateEmail(candidat.emailAdress);
                isql.typeAction(candidat.action,candidat.independant,DateTime.Now,idCandidat,"ADD",candidat.session_id);
                return new ObjectResult(new State(){code=3,content="Le candidat a ete ajoute à votre systeme",success=true});
            }catch(Exception exc){
                if(exc.Message.Equals(specificException)){
                    new WsCustomeInfoException(this.GetType().Name,$"Le candidat modifie ne possede pas de remind associe a cette action {candidat.action}");
                    return new ObjectResult(new State(){code=4,content=$"Le candidat a ete ajoute à votre systeme mais ne possede pas de remind avec l'action {candidat.action}",success=true});
                }
                new WsCustomeException(this.GetType().Name,exc.Message);
                State state = new State(){code=1,content=exc.Message,success=false};
                return CreatedAtRoute("GetNote", new { error = state },state);
            }  
        }

        [HttpPost("update/candidat/")]
        public IActionResult updateCandidat([FromBody]Candidat candidat){
            try{
                checkCandidat(candidat);
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanUpdate(candidat.session_id);
                if(!isql.CandidatAlreadyExist(candidat))
                    throw new Exception("Le candidat n'est pas existant dans votre systeme, veuillez le creer");
                int idUser = isql.getIdFromToken(candidat.session_id);
                int idCandidat = isql.getIdFromCandidateEmail(candidat.emailAdress);
                isql.updateCandidate(candidat,idUser);
                isql.typeAction(candidat.action,candidat.independant,DateTime.Now,idCandidat,"UPDATE",candidat.session_id);
                return new ObjectResult(new State(){code=4,content="Le candidat a ete modifie dans votre systeme",success=true});
            }catch(Exception exc){
                if(exc.Message.Equals(specificException)){
                    new WsCustomeInfoException(this.GetType().Name,$"Le candidat modifie ne possede pas de remind associe a cette action {candidat.action}");
                    return new ObjectResult(new State(){code=4,content=$"Le candidat a ete modifie dans votre systeme mais ne possede pas de remind avec l'action {candidat.action}",success=true});
                }
                new WsCustomeException(this.GetType().Name,exc.Message);
                State state = new State(){code=1,content=exc.Message,success=false};
                return CreatedAtRoute("GetNote", new { error = state },state);
            }
        }
        private void checkCandidat(Candidat candidat){
            if(candidat==null){
                throw new Exception("Vous devez creer votre candidat convenablement prealablement");
            }
        }

        private void checkReport(Report report){
            if(report==null){
                throw new Exception("Vous devez creer votre report convenablement prealablement");
            }

        }

        private void checkUser(User user){
            if(user==null){
                
                throw new Exception("Vous devez creer votre utilisateur convenablement prealablement");
            }
        }

        [HttpPost("add/candidat/report")]
        public IActionResult addReportCandidat([FromBody]Report report){
            try{
                
                checkReport(report);
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanUpdate(report.sessionId);
                
                int idCandidat = isql.getIdFromCandidateEmail(report.emailCandidat);
                
                if(isql.reportAlreadyExist(idCandidat)){
                     throw new Exception("Le report est deja existant dans votre systeme, si vous souhaitez le changer vous devez le modifier");
                }
                
                isql.addReport(report,idCandidat);
                return new ObjectResult(new State(){code=3,content="Le report a ete ajoute parfaitement à votre system",success=true});
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                State state = new State(){code=1,content=exc.Message,success=false};
                return CreatedAtRoute("GetNote", new { error = state },state);
                
            }  
        }

         [HttpPost("update/candidat/report")]
        public IActionResult UpdateReportCandidat([FromBody]Report report){
            try{
                
                checkReport(report);
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                isql.UserCanUpdate(report.sessionId);
                int idCandidat = isql.getIdFromCandidateEmail(report.emailCandidat);
                if(!isql.reportAlreadyExist(idCandidat)){
                    throw new Exception("Le report n'existe pas, vous devez le creer au prealable");
                }
                isql.updateReport(report,idCandidat);
                return new ObjectResult(new State(){code=3,content="Le report a ete modifie parfaitement à votre system",success=true});
            }catch(Exception exc){
                new WsCustomeException(this.GetType().Name,exc.Message);
                State state = new State(){code=1,content=exc.Message,success=false};
                return CreatedAtRoute("GetNote", new { error = state },state);
                
            }  
        }

         [HttpGet("Candidates/search/{email}/{token}")]
        public IActionResult UpdateReportCandidat(string email,string token){
             try{
               
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                ArrayList candidatListe = null;
                if(isql.UserCanRead(token)){
                    candidatListe = isql.searchCandidateFromEmail(email,token);
                }else{
                    throw new Exception("Vous n'avez pas le droit de lecture");
                }
                return new ObjectResult(candidatListe);
            }catch(Exception exception){
                new WsCustomeException(this.GetType().Name,exception.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=3,content=exception.Message,success=false});
                return CreatedAtRoute("GetErrors", new { error = errorList },errorList);
            }
            
        }

        [HttpGet("Disconnect/{token}")]
        public IActionResult disconnectTheUser(string token){
            try{
                if(String.IsNullOrEmpty(token)){
                    return new ObjectResult(new User(null,null,null,null));
                }
                IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
                int id = isql.getIdFromToken(token);
                string emailForLogTheUser = isql.getUserEmailFromId(id);
                isql.disconnectUser(id);
                new WsCustomeInfoException("DC02",$"The user with the email {emailForLogTheUser} is disconnected");
                return new ObjectResult(new User(null,null,null,null));
            }catch(Exception exception){
                 new WsCustomeException(this.GetType().Name,exception.Message);
                ArrayList errorList = new ArrayList();
                errorList.Add(new State(){code=3,content=exception.Message,success=false});
                return CreatedAtRoute("GetErrors", new { error = errorList },errorList);
            }
        }


        
    }



}
