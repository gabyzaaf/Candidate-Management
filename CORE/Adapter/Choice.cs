using Core.Adapter.Inteface;
using System;
namespace Core.Adapter{

    
    public class ChoiceTypeDb{

        public IsqlMethod isql{get;set;}
        // variabiliser cette partie.
        public ChoiceTypeDb(string type){
            if("mysql".Equals(type)){
                isql = new MysqlDb();
            }else{
                throw new Exception($"Le choix demandé {type} n'a pas été implémenté");
            }
        }    

    }


}