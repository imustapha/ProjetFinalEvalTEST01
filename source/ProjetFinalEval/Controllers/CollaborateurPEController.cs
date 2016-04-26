using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinalEval.Controllers
{
    public class CollaborateurPEController : Controller
    {
        private bd_evaluationEntities3 bd = new bd_evaluationEntities3();
        // GET: CollaborateurPE
        public ActionResult Index()
        {
            return View(bd.collaborateurpe.ToList());
        }

        // GET: CollaborateurPE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            collaborateurpe colpe = bd.collaborateurpe.Find(id);
            if (colpe == null)
                return HttpNotFound();


            return View(colpe);
        }

        // GET: CollaborateurPE/Create
        public ActionResult Create()
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            return View();
        }

        // POST: CollaborateurPE/Create
        [HttpPost]
        public ActionResult Create(collaborateurpe colpe)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");

            try
            {
                if (colpe.File.ContentLength > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("CustomError", "File est plus 2mb");
                    return View();
                }
                if (!(colpe.File.ContentType == "image/jpeg" || colpe.File.ContentType == "image/gif"))
                {
                    ModelState.AddModelError("CustomError", "File alloued for jpeg and gif");
                    return View();

                }
                byte[] data = new byte[colpe.File.ContentLength];
                colpe.File.InputStream.Read(data, 0, colpe.File.ContentLength);
                colpe.IMAGEPE = data;
                if (ModelState.IsValid)
                {
                    bd.collaborateurpe.Add(colpe);
                    int x = bd.SaveChanges();
                    return RedirectToAction("Index", bd.collaborateurpe.ToList());
                }
                return View();
            }
            catch (Exception ex)
            {
                string tst = ex.Message;
                return View();
            }

        }

        // GET: CollaborateurPE/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION", bd.collaborateurpe.Find(id).fonction.IDFONCTION);

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            collaborateurpe col = bd.collaborateurpe.Find(id);
            if (col == null)
                return HttpNotFound();
            return View(col);
        }

        // POST: CollaborateurPE/Edit/5
        [HttpPost]
        public ActionResult Edit(collaborateurpe col)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            try
            {
                if (ModelState.IsValid)
                {
                    bd.Entry(col).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return RedirectToAction("Index");


                }
                return View(col);
            }
            catch
            {
                return View();
            }
        }

        // GET: CollaborateurPE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collaborateurpe colpe = bd.collaborateurpe.Find(id);
            if (colpe == null)
            {
                return HttpNotFound();
            }
            return View(colpe);
        }

        // POST: CollaborateurPE/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, collaborateurpe col)
        {
            try
            {
                collaborateurpe colpe = new collaborateurpe();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    colpe = bd.collaborateurpe.Find(id);
                    if (colpe == null)
                        return HttpNotFound();


                    bd.collaborateurpe.Remove(colpe);
                    bd.SaveChanges();
                    return RedirectToAction("Index");

                } return View(colpe);
            }
            catch
            {
                return View();
            }
        }
    }
}
