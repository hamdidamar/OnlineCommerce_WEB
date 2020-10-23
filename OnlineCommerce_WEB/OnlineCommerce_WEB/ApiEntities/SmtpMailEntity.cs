using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCommerce_WEB.ApiEntities
{
    public class SmtpMailEntity
    {
        public string senderEMailAdress;
        public string senderMailUsername;
        public string senderMailPassword;
        public string receiverEMailAdress;
        public string subject;
        public string body;

    }
}