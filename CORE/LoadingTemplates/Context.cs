using System;
using exception.configuration;
namespace Candidate_Management.CORE.LoadingTemplates
{
    public class Context
    {
        Iloading load;
        public Context(Iloading _load){
            load = _load;
        }

        public void executeLoading(){
            try{
                load.loadFiles();
            }catch(Exception exc){
                throw exc;
            }
            
        }
    }
}