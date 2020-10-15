using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCommerce_WEB.Models
{
    public class LoginEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; set; }
    }
}