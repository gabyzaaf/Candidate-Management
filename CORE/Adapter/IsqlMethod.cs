using System;
using System.Collections;
using System.Collections.Generic;
using core.candidat;
using core.report;
using core.success;
using core.user;
using Candidate_Management.CORE.LoadingTemplates;
using Candidate_Management.CORE.Load;

namespace Core.Adapter.Inteface
{

    public interface IsqlMethod{
         /// <summary>
         /// Verify if the User token Exist
         /// </summary>
         /// <param name="token">User token</param>
         void TokenExist(string token);
         /// <summary>
         /// Check the permission for User can Read inside the Database
         /// </summary>
         /// <param name="token"></param>
         /// <returns></returns>
         bool UserCanRead(string token);
         
         /// <summary>
         /// Check the permission for User can Update inside the Database
         /// </summary>
         /// <param name="token">User Token</param>
         /// <returns></returns>
         bool UserCanUpdate(string token);

         /// <summary>
         /// Check the permission for User can Delete inside the Database
         /// </summary>
         /// <param name="token"></param>
         /// <returns></returns>
         bool UserCanDelete(string token);

         /// <summary>
         /// Create the Authentification in the system 
         /// </summary>
         /// <param name="email">User email</param>
         /// <param name="password">password email</param>
         void Authentification(string email,string password);


         /// <summary>
         /// Extract the candidate by this Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
         ArrayList searchCandidateById(int id);


         /// <summary>
         /// Extract candidat with report.
         /// </summary>
         /// <param name="nom">candidate name</param>
         /// <param name="token">User token</param>
         /// <returns></returns>
         ArrayList searchCandidate(string nom,string token);

         /// <summary>
         /// Generate Token
         /// </summary>
         /// <returns></returns>
         User GenerateToken();


         /// <summary>
         /// Insert token to the User connecting
         /// </summary>
         /// <param name="token">User token</param>
         /// <param name="email">User email</param>
         void addTokenToUser(string token,string email);


         /// <summary>
         /// Verify if the Token Exist
         /// </summary>
         /// <param name="token"></param>
         /// <returns></returns>
         bool verifyTheTokenExist(string token);

         /// <summary>
         /// Add the Candidate inside the system.
         /// </summary>
         /// <param name="candidat"></param>
         /// <param name="id"></param>
         void addCandidate(Candidat candidat,int id);


         /// <summary>
         /// Extract the User ID from the Token
         /// </summary>
         /// <param name="token">User Token</param>
         /// <returns></returns>
         int getIdFromToken(string token);

         /// <summary>
         /// Verify if the Candidate already exist in the system.
         /// </summary>
         /// <param name="candidat"></param>
         /// <returns></returns>
         bool CandidatAlreadyExist(Candidat candidat);


         /// <summary>
         /// Add candidate FreeLance in the System.
         /// </summary>
         /// <param name="prix"></param>
         /// <param name="id"></param>
         void addFreeLance(int prix,int id);

         /// <summary>
         /// get the Id from the Candidate Email
         /// </summary>
         /// <param name="email">Candidate Email</param>
         /// <returns></returns>
         int getIdFromCandidateEmail(string email);

         

         /// <summary>
         /// create the job remind for ADD or Update.
         /// </summary>
         /// <param name="id">User id</param>
         /// <param name="date">Current time</param>
         void remindType(int id,DateTime date);


         /// <summary>
         /// Update the Remind Job
         /// </summary>
         /// <param name="id">User Id</param>
         /// <param name="date">Current time</param>
         void updateRemindType(int id,DateTime date);

         /// <summary>
         /// This method are created for Factoring the remind Add and the remind Update and FreeLance
         /// </summary>
         /// <param name="actionType">Freelance or not</param>
         /// <param name="prix">Freelance price</param>
         /// <param name="date">Date add</param>
         /// <param name="id">User Id</param>
         /// <param name="type">ADD or UPDATE</param>
         /// <param name="token">User token</param>
         void typeAction(string actionType,int prix,DateTime date,int id,string type,string token);

         /// <summary>
         /// Update the candidate Information
         /// </summary>
         /// <param name="candidat">candidate class</param>
         /// <param name="id">Token </param>
         void updateCandidate(Candidat candidat,int id);


         /// <summary>
         /// Extract the candidate for the mobile.
         /// </summary>
         /// <param name="nom">Candidate Name</param>
         /// <param name="token">Token User</param>
         /// <returns>Only 3 values by Candidate</returns>
          ArrayList searchCandidateMobile(string nom,string token);

          /// <summary>
          /// Verify if the report Already Exist.
          /// </summary>
          /// <param name="idCandidat">Candidate Id</param>
          /// <returns></returns>
          bool reportAlreadyExist(int idCandidat);

          /// <summary>
          /// Create new report inside the System.
          /// </summary>
          /// <param name="report">Report class</param>
          /// <param name="idCandidat">Candidate Id</param>
          void addReport(Report report,int idCandidat);

          /// <summary>
          /// Update the report Content
          /// </summary>
          /// <param name="report"></param>
          /// <param name="idCandidat"></param>
          void updateReport(Report report,int idCandidat);

          /// <summary>
          /// Extract the candidate By Email
          /// </summary>
          /// <param name="email">Candidate Email</param>
          /// <param name="token">User token</param>
          /// <returns></returns>
          ArrayList searchCandidateFromEmail(string email,string token);


          /// <summary>
          /// Extract the Candidate List By Action
          /// </summary>
          /// <param name="actions">Candidate Action</param>
          /// <param name="token">User Token</param>
          /// <returns></returns>
          ArrayList searchCandidateByAction(string actions,string token);


          /// <summary>
          /// Add Email template Inside the System.
          /// </summary>
          /// <param name="emailTemplate"></param>
          void addEmailTemplates(Template emailTemplate);

          /// <summary>
          /// Email template Exist inside the System
          /// </summary>
          /// <param name="title"></param>
          /// <returns></returns>
          bool emailTemplateExist(string title);

          /// <summary>
          /// Extract Email Template Beetween limite
          /// </summary>
          /// <param name="limite1">limit 1</param>
          /// <param name="limite2">limit 2</param>
          /// <returns></returns>
          ArrayList emailTemplateTiltes(int limite1,int limite2);
          

          /// <summary>
          /// get Template Content from the Title
          /// </summary>
          /// <param name="title"></param>
          /// <returns></returns>
          ArrayList emailTemplateContentFromTitle(string title);

          /// <summary>
          /// Update Template Email From Title.
          /// </summary>
          /// <param name="title"></param>
          /// <param name="content"></param>
          void updateTemplateEmailFromTitle(string title,string content);


          /// <summary>
          /// Dete template email from the title file
          /// </summary>
          /// <param name="title">File Title</param>
          void deleteTemplateEmailFromTitle(string title);


          /// <summary>
          /// The candidate have already a remind.
          /// </summary>
          /// <param name="id"></param>
          /// <returns></returns>
          bool remindExistByCandidate(int id);
          

          /// <summary>
          /// Extract the candidate list without report.
          /// </summary>
          /// <returns></returns>
          LinkedList<Candidat> getCandidatWithoutReport();

          /// <summary>
          /// Get the remind Informations from the Calendar
          /// </summary>
          /// <returns></returns>
          ArrayList getRemindInformationForCalendar();

          /// <summary>
          /// Verify if the choice exist Inside the System.
          /// </summary>
          /// <param name="choice">choice Name</param>
          void stateChoiceExist(string choice);


          /// <summary>
          /// Extract Datas from the Choice.
          /// </summary>
          /// <param name="choice">choice from the kaggle Dataset</param>
          /// <returns></returns>
          ArrayList getDatasFromChoice(string choice);

          /// <summary>
          /// Disconnect the User
          /// </summary>
          /// <param name="id"></param>
          void disconnectUser(int id);

          /// <summary>
          /// Extract the UserEmail from the Id
          /// </summary>
          /// <param name="id">UserID</param>
          /// <returns></returns>
          string getUserEmailFromId(int id);


          /// <summary>
          /// Change the JobState.
          /// </summary>
          /// <param name="id">JobId</param>
          void changeJobState(int id);

          /// <summary>
          /// Verify if the Remind exist for a specific JobId
          /// </summary>
          /// <param name="id">JobId</param>
          /// <returns></returns>
          bool remindExistByJob(int id);

          /// <summary>
          /// Verify if the remind had already update
          /// </summary>
          /// <param name="id">Remind ID</param>
          void remindAlreadyUpdated(int id);


          /// <summary>
          /// Get The Candidat Id From the remind list
          /// </summary>
          /// <param name="userId">UserID</param>
          /// <returns></returns>
          int getLastCandidateIdFromRemind(int userId);


          /// <summary>
          /// Extract the candidate Email from The candidate ID
          /// </summary>
          /// <param name="candidateId">Candidate Id</param>
          /// <returns></returns>
          string getCandidateEmailFromId(int candidateId);

          /// <summary>
          /// Verify if the plugin exist
          /// </summary>
          /// <param name="plugin"></param>
          /// <returns></returns>
          bool pluginExist(Plugin plugin);

          /// <summary>
          /// Add plugin in the system (This feature is load when start the system)
          /// </summary>
          /// <param name="plugin"></param>
          void addPlugin(Plugin plugin);

          /// <summary>
          /// Extract the plugin List
          /// </summary>
          /// <returns></returns>
          ArrayList getPluginList();


          /// <summary>
          /// Get the plugin choice from the candidate
          /// </summary>
          /// <param name="emailCandidat"></param>
          /// <returns></returns>
          string getPluginChoiceFromCandidate(string emailCandidat);
        
          /// <summary>
          /// Delete the candidate by Id
          /// </summary>
          /// <param name="id">Candidate ID</param>
          void deleteCandidateById(int id);

          /// <summary>
          /// Extract Candidate By specific Email
          /// </summary>
          /// <param name="emailCandidat"></param>
          /// <returns></returns>
          ArrayList searchCandidateWithSpecificEmail(string emailCandidat);

          /// <summary>
          /// Get Candidate With or Without report.
          /// I am using the limite for not add all the Candidate in the CLR.
          /// </summary>
          /// <param name="limite1"></param>
          /// <param name="limite2"></param>
          /// <returns></returns>
          ArrayList getCandidatesListWithLimite(int limite1,int limite2);
    }

} 