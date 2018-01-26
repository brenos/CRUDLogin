using {0}.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace {1}.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var usuarios = Membership.GetAllUsers().GetEnumerator();
            List<LoginModel> listaUsuarios = new List<LoginModel>();
            while (usuarios.MoveNext())
            {
                var usuario = (MembershipUser)usuarios.Current;
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                listaUsuarios.Add(l);
            }
            return View(listaUsuarios);
        }

        //
        // GET: /Account/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(string username = "")
        {
            if (User.IsInRole("Administrador"))
            {
                var usuario = Membership.GetUser(username);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                return View(l);
            }
            else if (username.Equals(User.Identity.Name))
            {
                var usuario = Membership.GetUser(username);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                return View(l);
            }
            return RedirectToAction("Index", "Home");
        }
        
        //
        // GET: /Account/Create
        //[AllowAnonymous]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            //Roles.CreateRole("Administrador");
            return View();
        }

        //
        // POST: /Account/Create
        //[AllowAnonymous]
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginModel usuario, string role)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status;
                MembershipUser newUser =  Membership.CreateUser(usuario.UserName, usuario.Password, usuario.Email, "Password Question", "Password Answer ", true, out status);
                if (newUser == null)
                {
                    //Mensagem de erro
                }
                else
                {
                    newUser.Comment = usuario.Name;
                    Membership.UpdateUser(newUser);
                    Roles.AddUserToRole(newUser.UserName, role);
                    //Mensagem de sucesso
                    return RedirectToAction("Details", new { username = usuario.UserName });
                }
            }
            return View(usuario);
        }

        
        //
        // GET: /Account/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(string username = "")
        {
            if (User.IsInRole("Administrador"))
            {
                var usuario = Membership.GetUser(username);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                return View(l);
            }
            else if (username.Equals(User.Identity.Name))
            {
                var usuario = Membership.GetUser(username);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                return View(l);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(LoginModel pUsuario, string role)
        {
            if (User.IsInRole("Administrador"))
            {
                if (ModelState.IsValid)
                {
                    var usuario = Membership.GetUser(pUsuario.UserName);
                    usuario.Email = pUsuario.Email;
                    usuario.Comment = pUsuario.Name;
                    Membership.UpdateUser(usuario);
                    foreach (var roleFor in Roles.GetAllRoles())
                    {
                        if (Roles.FindUsersInRole(roleFor, usuario.UserName).Length > 0)
                        {
                            Roles.RemoveUserFromRole(usuario.UserName, roleFor);
                        }
                    }
                    Roles.AddUserToRole(usuario.UserName, role);
                    //Mensagem de sucesso
                    return RedirectToAction("Details", new { username = usuario.UserName });
                }
                //Mensagem de erro
                return View(pUsuario);
            }
            else if (pUsuario.UserName.Equals(User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    var usuario = Membership.GetUser(pUsuario.UserName);
                    usuario.Email = pUsuario.Email;
                    usuario.Comment = pUsuario.Name;
                    Membership.UpdateUser(usuario);
                    foreach (var roleFor in Roles.GetAllRoles())
                    {
                        if (Roles.FindUsersInRole(roleFor, usuario.UserName).Length > 0)
                        {
                            Roles.RemoveUserFromRole(usuario.UserName, roleFor);
                        }
                    }
                    Roles.AddUserToRole(usuario.UserName, role);
                    //Mensagem de sucesso
                    return RedirectToAction("Details", new { username = usuario.UserName });
                }
                //Mensagem de erro
                return View(pUsuario);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Titulo/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(string username = "")
        {
            var usuario = Membership.GetUser(username);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            LoginModel l = new LoginModel()
            {
                Email = usuario.Email,
                UserName = usuario.UserName,
                Name = usuario.Comment
            };
            return View(l);
        }

        //
        // POST: /Titulo/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string username = "")
        {
            if (Membership.DeleteUser(username))
            {
                //Mensagem de sucesso
                return RedirectToAction("Index");
            }
            else
            {
                //Mensagem de erro
                return RedirectToAction("Delete", new { username = username });
            }
        }

        //
        // GET: /Account/ResetPassword
        //[AllowAnonymous]
        public ActionResult ResetPassword(string username)
        {
            if (User.IsInRole("Administrador"))
            {
                var usuario = Membership.GetUser(username);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                return View(l);
            }
            else if (username.Equals(User.Identity.Name))
            {
                var usuario = Membership.GetUser(username);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                return View(l);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string username, string newPass)
        {
            var usuario = Membership.GetUser(username);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            string oldPassword = usuario.ResetPassword("Password Answer ");
            if (usuario.ChangePassword(oldPassword, newPass))
            {
                LoginModel l = new LoginModel()
                {
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    Name = usuario.Comment
                };
                //Mensagem de sucesso
                return RedirectToAction("Details", new { username = usuario.UserName });
            }
            else
            {
                //Mensagem de erro
                return RedirectToAction("ResetPassword", new { username = username });
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // check if all required fields are set
            if (ModelState.IsValid)
            {
                // authenticate user
                var success = Membership.ValidateUser(model.UserName, model.Password);

                if (success)
                {
                    // set authentication cookie
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    return RedirectToAction("Index", "Menus");
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Usuário / Senha incorreto(s).");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Account/Logoff
        public ActionResult Logoff()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult Forgot()
        {
            return View();
        }

    //
    // POST: Account/Forgot?UserId=?
    //[HttpPost]
    //[AllowAnonymous]
    //[Authorize]
    //public ActionResult Forgot(string UserId, string UserEmail)
    //{
    //    Criar estrutura para enviar senha por email.
    //}

    public ActionResult InstalarSistema()
    {
        if (!Roles.RoleExists("Administrador"))
        {
            //Criar a regra admin
            Roles.CreateRole("Administrador");

            //Adicionar Usuário admim
            MembershipCreateStatus status;
            MembershipUser newUser = Membership.CreateUser({2}, {3}, {4}, "Password Question", "Password Answer ", true, out status);
            if (newUser == null)
            {
                //Mensagem de Erro
            }
            else
            {
                newUser.Comment = "Administrador";
                Membership.UpdateUser(newUser);
                Roles.AddUserToRole(newUser.UserName, "Administrador");
            }
            //Mensagem de sucesso: "Sistema instalado com sucesso!"
        }
        else
        {
            //Mensagem de erro: "Sistema já instalado!"

        }
        return RedirectToAction("Login", "Account", new { returnUrl = "Home/Index" });
    }
}
