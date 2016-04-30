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

namespace AppGestionEvaluation.Controllers
{
    public class CollaborateurTitulaireController : Controller
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
        bd_evaluationEntities3 bd = new bd_evaluationEntities3();
        // GET: Collaborateur
        public ActionResult Index()
        {
            return View(bd.collaborateurtitulaire.ToList());
        }

        // GET: Collaborateur/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Evaluateur = bd.collaborateurtitulaire.Where(m => m.IDCOLLABORATEURTITULAIRE == id).FirstOrDefault().FLAGEVAL;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collaborateurtitulaire CollT = bd.collaborateurtitulaire.Find(id);
            if (CollT == null)
            {
                return HttpNotFound();
            }
            return View(CollT);
        }
        [HttpGet]
        // GET: Collaborateur/Create
        public ActionResult Create()
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            return View();
        }

        // POST: Collaborateur/Create
        [HttpPost]
        public async Task<ActionResult> Create( RegisterViewModel model)
        {

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
                model.IMAGE = data;

                if (ModelState.IsValid)
                {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                var resulte = await UserManager.CreateAsync(user, model.Password);

                var userToInsert =bd.aspnetusers.Where(i=>i.UserName==user.UserName).FirstOrDefault(); 
                    if(userToInsert != null){

                        var CollT = new collaborateurtitulaire() { IdUser = userToInsert.Id, NOM = model.NOM, PRENOM = model.PRENOM, IDFONCTION = model.IDFONCTION, IMAGE = model.IMAGE, FLAGEVAL = model.FLAGEVAL };
                        if (resulte.Succeeded)
                        {

                            await SignInAsync(user, isPersistent: false);
                            bd.collaborateurtitulaire.Add(CollT);
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
            
            }
            catch (Exception ex)
            {
                ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
                string tst = ex.Message;
                return View();
            }
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            return View();
        }

        // GET: Collaborateur/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION", bd.collaborateurtitulaire.Find(id).fonction.IDFONCTION);
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            collaborateurtitulaire CollT = bd.collaborateurtitulaire.Find(id);
            aspnetusers asp = bd.aspnetusers.Find(CollT.IdUser);
            if (CollT == null)
                return HttpNotFound();
            return View(CollT);
        }

        // POST: Collaborateur/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id,collaborateurtitulaire CollT)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            collaborateurtitulaire col = bd.collaborateurtitulaire.Find(id);




            col.NOM = CollT.NOM;
            col.PRENOM = CollT.PRENOM;
            col.FLAGEVAL = CollT.FLAGEVAL;
            col.IDFONCTION = CollT.IDFONCTION;
            

            if (CollT.fonction != null)
            {
                col.fonction = CollT.fonction;
            }
            if (CollT.IMAGE != null)
            {
                col.IMAGE = CollT.IMAGE;
            }
            try
            {

                if (ModelState.IsValid)
                {
                    bd.Entry(col).State = System.Data.Entity.EntityState.Modified;

                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(CollT);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Collaborateur/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collaborateurtitulaire CollT = bd.collaborateurtitulaire.Find(id);
            if (CollT == null)
            {
                return HttpNotFound();
            }
            return View(CollT);
        }

        // POST: Collaborateur/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, collaborateurtitulaire CollTt,aspnetusers asp)
        {
            try
            {
                collaborateurtitulaire CollT = new collaborateurtitulaire();
                aspnetusers asps = new aspnetusers();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    CollT = bd.collaborateurtitulaire.Find(id);
                    asps = bd.aspnetusers.Find(CollTt.IdUser);
                    if (CollT == null && asps==null)
                        return HttpNotFound();
                    bd.collaborateurtitulaire.Remove(CollT);
                    bd.aspnetusers.Remove(asps);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(CollT);
            }
            catch
            {
                return View();
            }
        }
    }
}