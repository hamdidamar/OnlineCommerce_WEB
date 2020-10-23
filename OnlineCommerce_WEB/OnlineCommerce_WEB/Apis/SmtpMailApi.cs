using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using OnlineCommerce_WEB.ApiEntities;

namespace OnlineCommerce_WEB.Apis
{
    public class SmtpMailApi
    {
        private static SmtpMailApi _instance = null;

        private SmtpMailApi()
        {

        }
        public static SmtpMailApi Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SmtpMailApi();
                }
                return _instance;
            }
        }


        public bool SendEMail(SmtpMailEntity mailEntity)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.IsBodyHtml = false;
                mail.From = new MailAddress(mailEntity.senderEMailAdress);
                mail.To.Add(mailEntity.receiverEMailAdress);
                mail.Subject = mailEntity.subject;
                mail.Body = mailEntity.body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailEntity.senderMailUsername, mailEntity.senderMailPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}