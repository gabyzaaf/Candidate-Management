using System;
using System.IO;
using exception.loading;
namespace Candidate_Management.CORE.LoadingTemplates
{
    public class Template
    {
        public string token{get;set;}
        private string path;
        public string title{get;set;}
        public string content{private get;set;}

        public Template(){

        }

        public Template(string _path){
            try{
                checkPath(_path);
                path = _path;
                if(path.Contains("/")){
                    string[] containsThePath = path.Split("/");
                    int size = (containsThePath.Length-1);
                    title = containsThePath[size];
                }
                title = title.Replace(".txt","");
                
            }catch(Exception exc){
                throw new LoadingCustomException("L01",exc.Message);
            }
            
        }

        public string getContent(){
            try{
                checkPath(path);
                if(String.IsNullOrEmpty(content)){
                    content = File.ReadAllText(path);
                }
                return content;
            }catch(Exception exc){
               throw new LoadingCustomException("L02",exc.Message);
            }   
        }

        private void checkPath(string _path){
             if(String.IsNullOrEmpty(_path)){
                    throw new Exception("Le chemin du fichier est vide");
            }
        }

    }
}