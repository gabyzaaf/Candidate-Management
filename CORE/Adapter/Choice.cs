using Core.Adapter.Inteface;
using System;
namespace Core.Adapter{

    /// <summary>
    /// This class is a factory
    /// </summary>
    public class ChoiceTypeDb{

        public IsqlMethod isql{get;set;}
        /// <summary>
        /// This class generate the database type.
        /// </summary>
        /// <param name="type">SQL DATABASE</param>
        public ChoiceTypeDb(string type){
            if("mysql".Equals(type)){
                isql = new MysqlDb();
            }else{
                throw new Exception($"Le choix demande {type} n'a pas ete implemente");
            }
        }    

    }


}