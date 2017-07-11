using System;
using System.IO;
using exception.configuration;

namespace CORE.LogManager{


    public class LogManager{

        private string file;

        public LogManager(string path){
            this.file = path;
        }

        /// <summary>
        /// Verify the File existing.
        /// </summary>
        private void checkFileExist(){
            try{
                if(!File.Exists(this.file)){
                    using(File.Create(this.file)){

                    }
                }
            }catch(Exception exc){
                throw new ConfigurationCustomException();
            }
            
        }

        /// <summary>
        /// Write the content inside the log.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public void Write(string type,string content){
            if(string.IsNullOrEmpty(content)){
                throw new Exception();
            }
            checkFileExist();
            using (FileStream fs = new FileStream(this.file,FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(DateTime.Now.ToString()+" - "+type+" - "+content);
                
            }
            
        }



    }
}