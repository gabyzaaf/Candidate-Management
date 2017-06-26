using System.Collections.Generic;
using Core.Adapter.Inteface;
using System.Collections;
namespace Candidate_Management.CORE.Remind
{
    public abstract class LegacyRemind
    {
        protected Dictionary<string,string> getCandidateNameFromId(int id){
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            ArrayList liste = isql.searchCandidateById(id);
            Dictionary<string,string> candidateInformation = (Dictionary<string,string>)liste[0];
            return candidateInformation;
        }
    }
}