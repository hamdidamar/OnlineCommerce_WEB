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

        // GET: Accounts/Edit/5
        /*public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "ID", "Name", accounts.AccountTypeID);
            ViewBag.ID = new SelectList(db.Companies, "ID", "Name", accounts.ID);
            return View(accounts);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,AccountTypeID")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "ID", "Name", accounts.AccountTypeID);
            ViewBag.ID = new SelectList(db.Companies, "ID", "Name", accounts.ID);
            return View(accounts);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            db.Accounts.Remove(accounts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
