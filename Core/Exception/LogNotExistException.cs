using System;
namespace exceptions
{
    public class LogNotExistException : System.Exception {

        private string content;
        public LogNotExistException(string _content):base(_content){
            this.content = _content;
        }

    }
   
}