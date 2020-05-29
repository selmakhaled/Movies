using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Movies.Models
{
    public class EmailManager
    {
        public static void SendEmail(string Subject, string Body, string Reciever, string moviePic)
        {
            MailMessage mail = new MailMessage("k900000001@gmail.com", Reciever);

            mail.Subject = Subject;

            mail.Body = Body;
            mail.IsBodyHtml = true;
            mail.Sender = new MailAddress("k900000001@gmail.com");
            //      mail.Attachments.Add(new Attachment("~/Images/" + moviePic));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.UseDefaultCredentials = false;


            smtp.Credentials = new NetworkCredential("k900000001@gmail.com", "9988770000");


            try
            {
                smtp.Send(mail);


            }
            catch (Exception) { }

        }


        public static void SendEmail(string Subject, string Body, string Reciever)
        {
            MailAddress from = new MailAddress("k900000001@gmail.com");
            MailAddress to = new MailAddress(Reciever);

            MailMessage mail = new MailMessage(from, to);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            mail.Sender = new MailAddress("k900000001@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("k900000001@gmail.com", "9988770000");
            smtp.Send(mail);

        }













    }
}