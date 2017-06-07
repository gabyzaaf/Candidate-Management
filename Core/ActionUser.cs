using System;
using System.IO;
using actions;
using exceptions;
using System.Net.Mail;

namespace actionUser{


    public class ActionUser{

        private UserFeatures user;

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
            return content.Replace("<prenom>","Gabriel").Replace("<pnom>","toto");
        }


       public void sendEmail(string email,  string emailBody, string firstName)
        {
            
        try{
            if(String.IsNullOrEmpty(email)){
                throw new Exception("the email is empty");
            }
            if(String.IsNullOrEmpty(emailBody)){
                throw new Exception("the email body is empty");
            }
            MailAddress fromAddress = new MailAddress("upload.file.travail@gmail.com");
            MailAddress toAddress = new MailAddress(email, firstName);
            const string fromPassword = "ESGI@2017";
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = "hello world";
            message.Body = emailBody;
            smtp.Send(message);
        }catch(FormatException format){
            Console.WriteLine($"the format error is {format.Message} for {email}");
        }
        catch(EmailCustomException exc){
            Console.WriteLine($"the error is {exc.Message}");
        }
        
    } 

       

    }

}