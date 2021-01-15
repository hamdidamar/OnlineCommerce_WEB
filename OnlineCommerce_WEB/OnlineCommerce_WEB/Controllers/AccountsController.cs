using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineCommerce_WEB.Models.EntityFramework;
using System.Web.UI;
using OnlineCommerce_WEB.ApiEntities;
using OnlineCommerce_WEB.Apis;
using System.Windows;

namespace OnlineCommerce_WEB.Controllers
{
    public class AccountsController : Controller
    {
        private OnlineCommerceEntities db = new OnlineCommerceEntities();

        // GET: Accounts
        /*public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.AccountTypes).Include(a => a.Companies);
            return View(accounts.ToList());
        }*/
        
        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountTypeID = 2;
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,AccountTypeID")] Accounts accounts)
        {
            var username = (from user in db.Accounts
                           where user.Username == accounts.Username
                           select user).Any();

            if (!username) // Daha önce kullanılmış mı kontrolü
            {
                if (ModelState.IsValid)
                {

                    SmtpMailEntity mailEntity = new SmtpMailEntity
                    {
                        senderEMailAdress = "anonimepostam99@gmail.com",
                        receiverEMailAdress = accounts.Username,
                        senderMailUsername = "anonimepostam99@gmail.com",
                        senderMailPassword = "a1999_1997",
                        subject = "Registered E Commerce",
                        body = "Thanks for be registered E Commerce Family"
                        + Environment.NewLine
                        + "Your username: " + accounts.Username
                        +Environment.NewLine
                        + "Your password: " + accounts.Password
                        + Environment.NewLine 
                        +"Love,"
                        +Environment.NewLine +
                        "E Commerce Family"
                    };

                    SmtpMailApi smtpMailApi = SmtpMailApi.Instance;
                    smtpMailApi.SendEMail(mailEntity);


                    accounts.AccountTypeID = 2;

                    Encryption enc = new Encryption();
                    accounts.Password = enc.Encrypt(accounts.Password); // Şifreleme işlemi yapılarak veri tabanına ekliyoruz.

                    db.Accounts.Add(accounts);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                Response.Write("<script>alert('Başka bir kullanıcı adı giriniz.')</script>"); // Hata Mesajı
            }
            

            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "ID", "Name", accounts.AccountTypeID);
            ViewBag.ID = new SelectList(db.Companies, "ID", "Name", accounts.ID);
            return View(accounts);
        }

        [HttpGet]
        public ActionResult ForgotMyPassword()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ForgotMyPassword([Bind(Include = "ID,Username,Password,AccountTypeID")] Accounts accounts)
        {

            try
            {
                var password = (from user in db.Accounts
                                where user.Username == accounts.Username
                                select user.Password).FirstOrDefault();

                Encryption enc = new Encryption();


                SmtpMailEntity mailEntity = new SmtpMailEntity
                {
                    senderEMailAdress = "anonimepostam99@gmail.com",
                    receiverEMailAdress = accounts.Username,
                    senderMailUsername = "anonimepostam99@gmail.com",
                    senderMailPassword = "a1999_1997",
                    subject = "Forgot Password From E Commerce",
                    body = "E Commerce Forgot Password"
                            + Environment.NewLine
                            + "Your password: " + enc.Decrypt(password)
                            + Environment.NewLine
                            + "Love,"
                            + Environment.NewLine +
                            "E Commerce Family"
                };

                SmtpMailApi smtpMailApi = SmtpMailApi.Instance;
                smtpMailApi.SendEMail(mailEntity);

                Response.Write("<script>alert('Şifreniz gönderilmiştir')</script>");

                return RedirectToAction("Index", "Login");

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Response.Write("<script>alert('Şifreniz gönderilememiştir')</script>");
               
            }
            return RedirectToAction("ForgotMyPassword", "Accounts");


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
