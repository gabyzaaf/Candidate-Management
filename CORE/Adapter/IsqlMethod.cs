using System;
using System.Collections;
using System.Collections.Generic;
using core.candidat;
using core.report;
using core.success;
using core.user;
using Candidate_Management.CORE.LoadingTemplates;

namespace Core.Adapter.Inteface
{

    public interface IsqlMethod{
        void TokenExist(string token);
         bool UserCanRead(string token);
         
         bool UserCanUpdate(string token);

         bool UserCanDelete(string token);


         void Authentification(string email,string password);

         ArrayList searchCandidateById(int id);

         ArrayList searchCandidate(string nom,string token);

         User GenerateToken();

         void addTokenToUser(string token,string email);

         bool verifyTheTokenExist(string token);

         void addCandidate(Candidat candidat,int id);

         int getIdFromToken(string token);

         bool CandidatAlreadyExist(Candidat candidat);

         void addFreeLance(int prix,int id);

         int getIdFromCandidateEmail(string email);

         

         void remindType(int id,DateTime date);

         void updateRemindType(int id,DateTime date);

         void typeAction(string actionType,int prix,DateTime date,int id,string type,string token);

         void updateCandidate(Candidat candidat,int id);

          ArrayList searchCandidateMobile(string nom,string token);

          bool reportAlreadyExist(int idCandidat);

          void addReport(Report report,int idCandidat);

          void updateReport(Report report,int idCandidat);

          ArrayList searchCandidateFromEmail(string email,string token);

          ArrayList searchCandidateByAction(string actions,string token);

          void addEmailTemplates(Template emailTemplate);

          ArrayList emailTemplateExist(string title);

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
    }

} 