using ProjetFinalEval;
using ProjetFinalEval.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MySql.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;


namespace ProjetFinalEval.Controllers
{
    public class CollaborateurPEController : Controller
    {
        private ApplicationUserManager _userManager;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");

            try
            {
                if (model.File.ContentLength > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("CustomError", "File est plus 2mb");
                    return View();
                }
                if (!(model.File.ContentType == "image/jpeg" || model.File.ContentType == "image/gif"))
                {
                    ModelState.AddModelError("CustomError", "File alloued for jpeg and gif");
                    return View();

                }
                byte[] data = new byte[model.File.ContentLength];
                model.File.InputStream.Read(data, 0, model.File.ContentLength);
                model.IMAGEPE = data;
                 if (ModelState.IsValid)
                {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                var resulte = await UserManager.CreateAsync(user, model.Password);

                var userToInsert =bd.aspnetusers.Where(i=>i.UserName==user.UserName).FirstOrDefault(); 
                    if(userToInsert != null){

                        var colpe = new collaborateurpe() { IdUser = userToInsert.Id, NOMPE = model.NOMPE, PRENOMPE = model.PRENOMPE, IDFONCTION = model.IDFONCTION, IMAGEPE = model.IMAGEPE, STATUT = model.STATUT };
                        if (resulte.Succeeded) {

                            await SignInAsync(user, isPersistent: false);
                            bd.collaborateurpe.Add(colpe);
                            bd.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }

                }
                 else if (!ModelState.IsValid)
                 {
                     ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
                     return View();
                 }
            
               
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
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
            aspnetusers ass = bd.aspnetusers.Find(col.IdUser);
            if (col == null)
                return HttpNotFound();
            return View(col);
        }

        // POST: CollaborateurPE/Edit/5
        [HttpPost]
        public ActionResult Edit(collaborateurpe pe)
        {
            int id=pe.IDCOLLABORATEURPE;
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            collaborateurpe col = bd.collaborateurpe.Find(id);
            aspnetusers ass = bd.aspnetusers.Find(col.IdUser);
              col.NOMPE = pe.NOMPE;
              col.PRENOMPE =pe.PRENOMPE ;
              col.STATUT = pe.STATUT;
              col.IDFONCTION = pe.IDFONCTION;
            
            if (pe.fonction != null)
            {
                col.fonction = pe.fonction;
            }
            if (pe.IMAGEPE != null)
            {
                col.IMAGEPE = pe.IMAGEPE;
            }
            


            try
            {
                if (ModelState.IsValid)
                {
                    bd.Entry(col).State = System.Data.Entity.EntityState.Modified;
                    
                    bd.Entry(ass).State = System.Data.Entity.EntityState.Modified;
                    
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
                aspnetusers ass = new aspnetusers();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    colpe = bd.collaborateurpe.Find(id);
                    ass = bd.aspnetusers.Find(colpe.IdUser);
                    if (colpe == null)
                        return HttpNotFound();


                    bd.collaborateurpe.Remove(colpe);
                    bd.aspnetusers.Remove(ass);
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
