using System;
using Core.Adapter.Inteface;
namespace Candidate_Management.CORE.Remind
{
    public class HCGeo : Iremind
    { 
       

         public void add(int id,DateTime date){
            // remind toutes les 2 semaines
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            isql.remindType(id,date.AddMonths(6));
         }
         
         public void update(int id,DateTime date){
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            isql.updateRemindType(id,date.AddMonths(6));
         } 
    }
}