using System;
using System.IO;
using actions;
using exceptions;
using MailKit.Net.Smtp;
using MimeKit;
using conf;
using MailKit.Security;

namespace actionUser{


    public class ActionUser{

        private UserFeatures user;
        private ConfigurationData conf = ConfigurationData.getInstance();

        public ActionUser(UserFeatures _feature){
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
       
         // string email,  string emailBody, string firstName
         public void sendEmail(string email, string message,string firstName)
        {
            try 
            { 
                //From Address 
                string FromAddress = conf.email; 
                string FromAdressTitle = "Email from ASP.NET Core 1.1"; 
                //To Address 
                string ToAddress = email; 
                string ToAdressTitle = "Microsoft ASP.NET Core"; 
                string Subject = "Hello World - Sending email using ASP.NET Core 1.1"; 
                string BodyContent = message; 
    
                //Smtp Server 
                string SmtpServer = "smtp.gmail.com"; 
                //Smtp Port Number 
                int SmtpPortNumber = 587; 
    
                var mimeMessage = new MimeMessage(); 
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress)); 
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress)); 
                mimeMessage.Subject = Subject; 
                mimeMessage.Body = new TextPart("plain") 
                { 
                    Text = BodyContent 
    
                }; 
    
                using (var client = new SmtpClient()) 
                { 
    
                    client.Connect(SmtpServer, SmtpPortNumber, false); 
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
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