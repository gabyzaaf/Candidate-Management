using Core.Adapter;
using Core.Adapter.Inteface;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Factory{

    public class Factory{

        public static IsqlMethod GetSQLInstance(string type){
            return new ChoiceTypeDb(type).isql;
        }

    }

}