using System;
using System.IO;

namespace exceptions{


    public class EmailCustomException : Exception{

        private string content;
        public EmailCustomException(string _content):base(_content){
            this.content = _content;
            display();
        }

        public void display(){
            Console.WriteLine($"In display function the exception content is {this.content}");
        }

        public void writeLog(string pathWithFile){
             using (StreamWriter sw = File.AppendText(pathWithFile)) 
            {
                sw.WriteLine(this.content);
            }
        }

    }




}