using System;
using System.IO;
namespace Candidate_Management.CORE.LoadingTemplates
{
    public class Template
    {
        private string path;
        public string title{get;set;}
        public string content{private get;set;}

        public Template(string _path){
            try{
                path = _path;
                if(path.Contains("/")){
                    string[] containsThePath = path.Split("/");
                    int size = (containsThePath.Length-1);
                    title = containsThePath[size];
                }
                title = title.Replace(".txt","");
                
            }catch(Exception exc){
                // create specific exception for Loading
                Console.WriteLine(exc.Message);
            }
            
        }

        public string getContent(){
            try{
                if(String.IsNullOrEmpty(content)){
                    content = File.ReadAllText(path);
                }
                return content;
            }catch(ArgumentNullException argumentNull){
                // create specific exception for Loading
                Console.WriteLine($"The exception is inside about the file path :  {argumentNull.Message}");
                throw argumentNull;
            }   
        }


        public override string ToString()
        {
            return $"the path is {path} - the title is {title} - the content is {content} ";
        }
    }
}