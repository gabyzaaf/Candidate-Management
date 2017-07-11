using System;
using System.IO;
using actions;
using exceptions;
using MailKit.Net.Smtp;
using MimeKit;
using conf;
using MailKit.Security;
namespace actionUser{

/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
    public class ActionUser{

        private UserFeature user;
        private ConfigurationData conf = ConfigurationData.getInstance();

        public ActionUser(UserFeature _feature){
            this.user=_feature;
        }


        public void checkToSend(){

        }

        public string transformTextFromCandidate(){
            if(string.IsNullOrEmpty(user.fileName)){
                throw new Exception("Le contenu du fichier est vide, veuillez le completer");
            }
            string content = File.ReadAllText(user.fileName);
            return content.Replace("<prenom>",user.candidateFirstname).Replace("<pnom>",user.candidateName).Replace("<pemail>",user.candidateEmail);
        }
       
        
         public void sendEmail(string email, string message,string firstName)
        {
            try 
            { 
                
                string FromAddress = conf.email; 
                 
                //To Address 
                string ToAddress = email; 
                //string toAdressTitle = "Microsoft ASP.NET Core"; 
                string subject = "Hello World - Sending email using ASP.NET Core 1.1"; 
                string BodyContent = message; 
    
                //Smtp Server 
                string SmtpServer = conf.smtp; 
                //Smtp Port Number 
                int SmtpPortNumber = 587; 
    
                var mimeMessage = new MimeMessage(); 
                mimeMessage.From.Add(new MailboxAddress(conf.fromAdressTitle, FromAddress)); 
                mimeMessage.To.Add(new MailboxAddress(conf.toAdressTitle, ToAddress)); 
                mimeMessage.Subject = conf.subject; 
                mimeMessage.Body = new TextPart("plain") 
                { 
                    Text = BodyContent 
    
                }; 
                using (var client = new SmtpClient()) 
                { 
    
                    client.Connect(SmtpServer, SmtpPortNumber, false); 
                    client.Authenticate(conf.email, conf.password); 
                    client.Send(mimeMessage); 
                    Console.WriteLine("The mail has been sent successfully !!"); 
                    client.Disconnect(true); 
    
                } 
            } 
            catch (Exception ex) 
            { 
                throw ex; 
            } 
            
        }


    } 


       
}