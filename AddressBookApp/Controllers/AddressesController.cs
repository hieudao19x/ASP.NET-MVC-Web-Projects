using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AddressBookApp.Models;

namespace AddressBookApp.Controllers
{
    public class AddressesController : Controller
    {
        private AddressBookInfoEntities db = new AddressBookInfoEntities();

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.Group);
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Kana,ZipCode,PrefectureItem,StreetAddress,Telephone,Mail,Group_Id")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.Prefecture = address.PrefectureItem.ToString();
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", address.Group_Id);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            Prefectures p;
            if (Enum.TryParse(address.Prefecture, out p))
            {
                address.PrefectureItem = p;
            }
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", address.Group_Id);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Kana,ZipCode,PrefectureItem,StreetAddress,Telephone,Mail,Group_Id")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.Prefecture = address.PrefectureItem.ToString();
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", address.Group_Id);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search([Bind(Include = "Kana")] SearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Kana))
            {
                var list = db.Addresses.Where(item => item.Kana.IndexOf(model.Kana) == 0).ToList();
                model.Addresses = list;
            }
            else
            {
                model.Addresses = db.Addresses.ToList();
            }
            return View(model);
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
