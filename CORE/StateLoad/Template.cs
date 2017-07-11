using System;
using System.IO;
using exception.loading;
using core.configuration;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.CORE.LoadingTemplates
{
    public class Template
    {
        public string token{get;set;}
        public string path{get;set;}
        public string title{get;set;}
        public string content{get;set;}

        public Template(){

        }

        public Template(string _path,string _title, string _content){
            this.path = _path;
            this.title = _title;
            this.content = _content;
        }

       

        

    }
}