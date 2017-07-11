/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.CORE.Load
{
    public class Plugin
    {
        public int id{get;set;}
        public string name{get;set;}

        public Plugin(){

        }

        public Plugin(string _name){
            this.name = _name;
        }
    }
}