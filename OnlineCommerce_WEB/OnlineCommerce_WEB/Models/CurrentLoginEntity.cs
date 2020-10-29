using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCommerce_WEB.Models
{
    public class CurrentLoginEntity
    {
        // Mevcut giriş yapan kullanıcının bilgileri
        public static int ID { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static bool IsLogin { get; set; }
        public static string Name { get; set; }
    }
}