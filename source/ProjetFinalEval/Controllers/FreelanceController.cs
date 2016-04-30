using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinalEval.Controllers
{
    public class FreelanceController : Controller
    {
        bd_evaluationEntities3 bd = new bd_evaluationEntities3();
        // GET: Freelance
        public ActionResult Index()
        {
            return View(bd.freelance.ToList());
        }

        // GET: Freelance/Details/5
        public ActionResult Details(int? id)
        {
            if(id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            freelance free = bd.freelance.Find(id);
            return View(free);
        }

        // GET: Freelance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Freelance/Create
        [HttpPost]
        public ActionResult Create(freelance fre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    freelance free = new freelance { IDFREELANCE = fre.IDFREELANCE, NOMFL = fre.NOMFL, PRENOMFL = fre.PRENOMFL, EMAILFL = fre.EMAILFL, TELEPHONE = fre.TELEPHONE };

                        bd.freelance.Add(free);
                        bd.SaveChanges();
                        return RedirectToAction("Index");

                    
                }
                
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Freelance/Edit/5
        public ActionResult Edit(int? id)
        {if(id==null)
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        freelance free = bd.freelance.Find(id);
            if(free==null)
                return HttpNotFound();

            return View(free);
        }

        // POST: Freelance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, freelance free)
        {
            freelance fre = bd.freelance.Find(id);
            fre.NOMFL = free.NOMFL;
            fre.PRENOMFL = free.PRENOMFL;
            fre.EMAILFL = free.EMAILFL;
            fre.TELEPHONE = free.TELEPHONE;
            try
            { 
                if(ModelState.IsValid){
                    bd.Entry(fre).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }

                // TODO: Add update logic here


                return View(free);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Freelance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            freelance free = bd.freelance.Find(id);
            if (free == null)
            {
                return HttpNotFound();
            }
            return View(free);
        }

        // POST: Freelance/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, freelance free)
        {

            try
            {

                if(id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                if (free == null)
                    return HttpNotFound();
               free = bd.freelance.Find(id);
               bd.freelance.Remove(free);
               bd.SaveChanges();
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
