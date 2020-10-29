using OnlineCommerce_WEB.Models;
using OnlineCommerce_WEB.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCommerce_WEB.Controllers
{
    public class CustomerController : Controller
    {
        OnlineCommerceEntities db = new OnlineCommerceEntities();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customers entity)
        {
            try
            {
                var customer = (from c in db.Customers where c.AccountID == CurrentLoginEntity.ID select c).FirstOrDefault();
                if (customer == null)
                {
                    Customers c = new Customers();
                    c.Name = entity.Name;
                    c.Surname = entity.Surname;
                    c.Phone = entity.Phone;
                    c.Mail = entity.Mail;
                    c.Address = entity.Address;
                    c.AccountID = CurrentLoginEntity.ID;
                    CurrentLoginEntity.Name = entity.Name;

                    db.Customers.Add(c);
                }
                else
                {
                    customer.Name = entity.Name;
                    customer.Surname = entity.Surname;
                    customer.Phone = entity.Phone;
                    customer.Mail = entity.Mail;
                    customer.Address = entity.Address;
                    customer.AccountID = CurrentLoginEntity.ID;
                    CurrentLoginEntity.Name = customer.Name;
                }


                
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
