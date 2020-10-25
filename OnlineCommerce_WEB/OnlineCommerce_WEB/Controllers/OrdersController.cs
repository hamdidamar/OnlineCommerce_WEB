using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCommerce_WEB.Models;
using OnlineCommerce_WEB.Models.EntityFramework;

namespace OnlineCommerce_WEB.Controllers
{
    public class OrdersController : Controller
    {
        OnlineCommerceEntities db = new OnlineCommerceEntities();

        List<ShoppingCart> shoppingList = new List<ShoppingCart>();
        // GET: Orders
        public ActionResult Index()
        {
            TempData.Keep(); // Sepetteki ürünler kaybolmasın diye
            return View();
        }

        
        [HttpPost]
        public ActionResult AddToCart(int productID, FormCollection formCollection)
        {
            Products prod = db.Products.Where(x => x.ID == productID).SingleOrDefault();

            var productAmount = formCollection["Amount"];

            ShoppingCart cart = new ShoppingCart();
            cart.productID = prod.ID;
            cart.productName = prod.Name;
            cart.price = (float)prod.Price;
            cart.amount = int.Parse(productAmount);
            cart.bill = cart.price * cart.amount;


            if (TempData["shoppingList"]==null)
            {
                shoppingList.Add(cart);
                TempData["shoppingList"] = shoppingList;
                
            }
            else
            {
                List<ShoppingCart> tempShoppingList = TempData["shoppingList"] as List<ShoppingCart>;
                tempShoppingList.Add(cart);
                TempData["shoppingList"] = tempShoppingList;
            }


            if (TempData["shoppingList"]!=null) // Sepetteki toplam ürün fiyatı için
            {
                float totalBill = 0;
                List<ShoppingCart> tempShoppingList = TempData["shoppingList"] as List<ShoppingCart>;
                foreach (var item in tempShoppingList)
                {
                    totalBill += item.bill;
                }
                TempData["TotalBill"] = totalBill;
            }

            
            TempData.Keep();

            return Redirect("/Products");
        }

        public ActionResult ShoppingCart()
        {
            TempData.Keep(); // Sepetteki ürünler kaybolmasın diye
            return View();
        }

        public ActionResult DeleteFromCart(int id)
        {
            var shoppingList = TempData["shoppingList"] as List<ShoppingCart>;
            shoppingList = shoppingList.Where(cart => cart.productID != id).ToList();

            var item = shoppingList.Where(cart => cart.productID == id).SingleOrDefault();

            TempData["TotalBill"] = shoppingList.Sum(c => c.bill);

            TempData["shoppingList"] = shoppingList;
            return Redirect("/Orders/ShoppingCart");

        }
    }
}