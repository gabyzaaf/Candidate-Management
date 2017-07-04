using System;
using exception.configuration;
using System.Collections.Generic;
namespace Candidate_Management.CORE.Loading
{
    public class Context
    {
        private LinkedList<Iloading>  folders = new LinkedList<Iloading>();

        public void setFolders(Iloading loading){
            folders.AddLast(loading);
        }

        public void executeLoading(){
            try{
                foreach (Iloading folder in folders)
                {
                    folder.loading();
                }
            }catch(Exception exc){
                throw exc;
            }
            
        }
    }
}