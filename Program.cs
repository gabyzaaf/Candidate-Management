using System;
using actions;
using actionUser;
using exceptions;
using System.IO;
using conf;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Candidate_Management.Core;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace email
{
    class Program
    {

        private ConfigurationData conf = null;

        public static void writeLogs(string pathWithFile,string content){
            using (StreamWriter sw = File.AppendText(pathWithFile)) 
            {
                sw.WriteLine(content);
            }
        }
        /// <summary>
        ///     Data enter : 
        ///        jobId [0]
       ///         FileName [2].
        ///        Candidate Name [3].
        ///        Candidate Firstname [4].
        ///        Meetings Optionnal [5].
        ///        Candidate email [6].
        /// </summary>
        /// <param name="args">parameters array</param>
        static void Main(string[] args)
        {
             ConfigurationData conf = null;
            try{
                conf = ConfigurationData.getInstance();
                Program.writeLogs(conf.getLogPath(),string.Format($"the date is {DateTime.Now.ToString()} BEGIN PROGRAM"));
                UserFeature users = new UserFeature(Int32.Parse(args[0]),args[1],args[2],args[3],DateTime.Parse(args[4]),args[5]);
                string json = JsonConvert.SerializeObject(users);
                POST("http://localhost:5000/api/Remind/change/job/state/",json);
                ActionUser action = new ActionUser(users);
                Console.WriteLine(action.transformTextFromCandidate());
                action.sendEmail(users.candidateEmail,action.transformTextFromCandidate(),"gaby");
                Program.writeLogs(conf.getLogPath(),"END PROGRAM");
            }catch(LogNotExistException logException){
                Console.WriteLine(logException.Message);
            }catch(Exception exc){
               
                new EmailCustomException(exc.Message).writeLog(conf.getLogPath());
            }
             
             
        }

        public static void POST(string url, string jsonContent) 
        {
            ConfigurationData conf = null;
            conf = ConfigurationData.getInstance();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream()) {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string responseText = reader.ReadToEnd();
                        State state = JsonConvert.DeserializeObject<State>(responseText);
                        if(state.success == false){
                            throw new Exception(state.content);
                        }
                    }
                    length = response.ContentLength;
                }
            }
            catch (WebException ex) {
                throw ex;
            }
        }


        
 
    }
}
