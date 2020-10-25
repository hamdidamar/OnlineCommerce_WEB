using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCommerce_WEB.Models
{
    public class ShoppingCart
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public float price { get; set; }
        public int amount { get; set; }
        public float bill { get; set; }

    }
}