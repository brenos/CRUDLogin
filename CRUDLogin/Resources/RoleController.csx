using {0}.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace {1}.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var roles = Roles.GetAllRoles().GetEnumerator();
            List<RoleModel> listRegras = new List<RoleModel>();
            while (roles.MoveNext())
            {
                RoleModel role = new RoleModel()
                {
                    Regra = roles.Current.ToString()
                };
                listRegras.Add(role);
            }
            return View(listRegras);
        }

        //
        // GET: /Role/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(string role = "")
        {
            RoleModel r = new RoleModel()
            {
                Regra = role
            };
            return View(r);
        }

        //
        // GET: /Role/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Regra)
        {
            if (ModelState.IsValid)
            {
                if (!Roles.RoleExists(Regra))
                {
                    Roles.CreateRole(Regra);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Role/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(string role = "")
        {
            RoleModel r = new RoleModel()
            {
                Regra = role
            };
            return View(r);
        }

        //
        // POST: /Role/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RoleModel role)
        {
            if (!role.Regra.Equals("Administrador"))
            {
                if (Roles.GetUsersInRole(role.Regra).Length <= 0)
                {
                    Roles.DeleteRole(role.Regra);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}
