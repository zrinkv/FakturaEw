using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FakturaMVC.Models;
using Microsoft.AspNet.Identity;

namespace FakturaMVC.Controllers
{
    [Authorize]   
    public class FakturaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ImportMany]
        public IEnumerable<Lazy<FakturaMEF.IPorez<decimal>, FakturaMEF.IPorezMetaData>> Porezi { get; private set; }        

        // GET: Faktura
        public ActionResult Index()
        {
            return View(db.Fakture.Where(x => x.Stornirano == false).Include(x=>x.StavkeList).ToList());
        }

        // GET: Faktura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakture.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        public JsonResult GetPoreznuStopuByDrzava(string drzava)
        {
            var Porez = (from p in Porezi
                         where p.Metadata.Naziv == drzava
                         select p.Value).FirstOrDefault();
            var PoreznaStopa = Porez.IzracunajPorez(1m);
            return Json(PoreznaStopa, JsonRequestBehavior.AllowGet);
        }

        // GET: Faktura/Create
        public ActionResult Create()
        {
            List<string> PorezList = new List<string>(from p in Porezi select p.Metadata.Naziv);
            ViewBag.PoreziDrzave = PorezList;            

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            Faktura faktura = new Faktura();
            string tempBroj = db.Fakture.Select(x => x.BrojFakture).ToList().Last();
            int ZadnjiBrojRacuna = int.Parse(tempBroj.Split('-')[0]) + 1;
            faktura.BrojFakture = ZadnjiBrojRacuna + "-" + DateTime.Now.Month.ToString("D2") + "/" + DateTime.Now.Year.ToString("D4").Substring(2);
            faktura.DatumStvaranja = DateTime.Now;
            faktura.DatumDospijeca = DateTime.Now;
            if (currentUser != null)
                faktura.StvarateljRacuna = currentUser.UserName;
            faktura.StavkeList = new List<FakturaStavka>();
            if (TempData["stavkeFaktureTD"] != null)
                TempData["stavkeFaktureTD"] = null;

            faktura.StavkeSelectList = db.Stavke.Where(x => x.Izbrisano == false).Select(x => new SelectListItem { Value = x.StavkaId.ToString(), Text = x.Naziv + ":" + x.Cijena }).ToList();

            return View(faktura);
        }

        // POST: Faktura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faktura faktura) //[Bind(Include = "FakturaId,BrojFakture,DatumStvaranja,DatumDospijeca,UkupnaCijenaBezPoreza,UkupnaCijenaSPorezom,StvarateljRacuna,NazivPrimateljaRacuna")]
        {
            List<string> PorezList = new List<string>(from p in Porezi select p.Metadata.Naziv);
            ViewBag.PoreziDrzave = PorezList;

            faktura.StavkeList = new List<FakturaStavka>();
            if (TempData["stavkeFaktureTD"] != null)
                faktura.StavkeList = TempData["stavkeFaktureTD"] as List<FakturaStavka>;

            if (ModelState.IsValid)
            {
                db.Fakture.Add(faktura);       
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            faktura.StavkeSelectList = db.Stavke.Select(x => new SelectListItem { Value = x.StavkaId.ToString(), Text = x.Naziv + ":" + x.Cijena }).ToList();
            return View(faktura);
        }       

        // GET: Faktura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakture.Find(id);            
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Faktura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faktura faktura = db.Fakture.Find(id);
            faktura.Stornirano = true;
            db.Fakture.AddOrUpdate(faktura);
            db.Configuration.ValidateOnSaveEnabled = false;
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
