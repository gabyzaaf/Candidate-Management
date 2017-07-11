using Core.Adapter.Inteface;
using System;
namespace Core.Adapter{

    
    public class ChoiceTypeDb{

        public IsqlMethod isql{get;set;}
        
        public ChoiceTypeDb(string type){
            if("mysql".Equals(type)){
                isql = new MysqlDb();
            }else{
                throw new Exception($"Le choix demande {type} n'a pas ete implemente");
            }
        }    

    }


}