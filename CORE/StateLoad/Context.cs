using System;
using exception.configuration;
using System.Collections.Generic;
namespace Candidate_Management.CORE.Loading
{
    /// <summary>
    /// add The Iloading inside Linkedin.
    /// </summary>
    public class Context
    {
        private LinkedList<Iloading>  folders = new LinkedList<Iloading>();

        public void setFolders(Iloading loading){
            folders.AddLast(loading);
        }
        /// <summary>
        /// Execute all loading method
        /// </summary>
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