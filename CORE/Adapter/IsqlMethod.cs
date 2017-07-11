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
         /// <param name="id"></param>
         void updateCandidate(Candidat candidat,int id);

          ArrayList searchCandidateMobile(string nom,string token);

          bool reportAlreadyExist(int idCandidat);

          void addReport(Report report,int idCandidat);

          void updateReport(Report report,int idCandidat);

          ArrayList searchCandidateFromEmail(string email,string token);

          ArrayList searchCandidateByAction(string actions,string token);

          void addEmailTemplates(Template emailTemplate);

          bool emailTemplateExist(string title);

          ArrayList emailTemplateTiltes(int limite1,int limite2);
          
          ArrayList emailTemplateContentFromTitle(string title);

          void updateTemplateEmailFromTitle(string title,string content);

          void deleteTemplateEmailFromTitle(string title);

          bool remindExistByCandidate(int id);
          
          LinkedList<Candidat> getCandidatWithoutReport();

          ArrayList getRemindInformationForCalendar();

          void stateChoiceExist(string choice);

          ArrayList getDatasFromChoice(string choice);

          void disconnectUser(int id);

          string getUserEmailFromId(int id);

          void changeJobState(int id);

          bool remindExistByJob(int id);

          void remindAlreadyUpdated(int id);

          int getLastCandidateIdFromRemind(int userId);

          string getCandidateEmailFromId(int candidateId);

          bool pluginExist(Plugin plugin);

          void addPlugin(Plugin plugin);

          ArrayList getPluginList();

          string getPluginChoiceFromCandidate(string emailCandidat);
          
          void deleteCandidateById(int id);

          ArrayList searchCandidateWithSpecificEmail(string emailCandidat);

          ArrayList getCandidatesListWithLimite(int limite1,int limite2);
    }

} 