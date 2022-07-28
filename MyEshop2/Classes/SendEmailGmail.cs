using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace MyEshop2.Classes
{
    public class SendEmailGmail
    {

        public static void Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer= new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("melikasojodi@gmail.com","سایت دانشجویار");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body=Body;
            mail.IsBodyHtml = true;


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("melikasojodi@gmail.com","09370745656" );
        }
    }
}