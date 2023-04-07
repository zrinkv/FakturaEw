using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakturaMVC.Models;

namespace FakturaMVC.Controllers
{
    [Authorize]
    public class StavkaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stavka
        public ActionResult Index()
        {
            return View(db.Stavke.Where(x=>x.Izbrisano == false).ToList());
        }

        // GET: Stavka/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavke.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        public JsonResult GetStavkuById(int id)
        {
            Stavka stavka = db.Stavke.Find(id);
            return Json(stavka, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult CreateNewStavku(int id, decimal cijena=0, int kolicina=1, decimal poreznaStopa = 1.1m)
        {
            Stavka stavka = db.Stavke.Find(id);

            FakturaStavka fakturaStavka = new FakturaStavka();
            fakturaStavka.FakturaStavkaId = 0;
            fakturaStavka.StavkaId = stavka.StavkaId;
            fakturaStavka.Opis = stavka.Naziv;
            fakturaStavka.Cijena = cijena;
            fakturaStavka.Kolicina = kolicina;
            fakturaStavka.UkupnaCijenaBezPoreza = cijena * kolicina;
            fakturaStavka.Porez = poreznaStopa * 100 - 100;

            List<FakturaStavka> fakturaStavkaList = new List<FakturaStavka>();
            if (TempData["stavkeFaktureTD"] != null)
            {
                fakturaStavkaList = TempData["stavkeFaktureTD"] as List<FakturaStavka>;

                if (fakturaStavkaList.Find(x => x.StavkaId == id) != null)
                {                    
                    TempData["stavkeFaktureTD"] = fakturaStavkaList;
                    return null;
                }
                else
                    fakturaStavkaList.Add(fakturaStavka);
                TempData["stavkeFaktureTD"] = fakturaStavkaList;
            }
            else
            {
                fakturaStavkaList.Add(fakturaStavka);
                TempData["stavkeFaktureTD"] = fakturaStavkaList;
            }

            return PartialView("~/Views/Shared/_StavkeFakture.cshtml", fakturaStavka);
        }

        public ActionResult DeleteStavku(int id)
        {            
            List<FakturaStavka> fakturaStavkaList = new List<FakturaStavka>();
            if (TempData["stavkeFaktureTD"] != null)
            {
                fakturaStavkaList = TempData["stavkeFaktureTD"] as List<FakturaStavka>;               
                if (fakturaStavkaList.Find(x => x.StavkaId == id) != null)
                {
                    fakturaStavkaList.Remove(fakturaStavkaList.Find(x => x.StavkaId == id));
                    TempData["stavkeFaktureTD"] = fakturaStavkaList;
                }
            }
            return null;
        }

        // GET: Stavka/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stavka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StavkaId,Naziv,Cijena")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Stavke.Add(stavka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stavka);
        }

        // GET: Stavka/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavke.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // POST: Stavka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StavkaId,Naziv,Cijena")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stavka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stavka);
        }

        // GET: Stavka/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavke.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // POST: Stavka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stavka stavka = db.Stavke.Find(id);
            stavka.Izbrisano = true;
            db.Stavke.AddOrUpdate(stavka);
            db.SaveChanges();
            return RedirectToAction("Index");
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
