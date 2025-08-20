using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using archiesolutions.common;
using archiesolutions.UAM.BusinessEntities;
using cdr_reset.Web.Models;

namespace cdr_reset.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login LoginModel)
        {
            GetConfig();

            if (ModelState.IsValid)
            {
                if (GetUserName(LoginModel.Username) && VerifyPassword(LoginModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(LoginModel.Username, false);
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View();
        }

        #region GetUserName
        private bool GetUserName(string userName)
        {
            bool result = false;

            User user = archiesolutions.UAM.Bll.LoginManager.GetItem("Guest", userName);

            if (user != null)
            {
                if (!user.IsSuspend)
                {
                    result = true;
                    Session["User"] = user;
                }
            }

            return result;
        }

        #endregion

        #region VerifyPassword
        private bool VerifyPassword(string password)
        {
            User login = (User)Session["User"];
            PermissionCollection permissions = null;

            bool result = false;

            if (login != null)
            {
                if (archiesolutions.UAM.Bll.UserPasswordManager.ValidatePassword(login, password))
                {
                    App app = archiesolutions.UAM.Bll.AppManager.GetItemByCode(login.Login, Config.ApplicationCode);
                    if (app != null)
                    {
                        Config.ConnectionString = app.ConnectionString;

                        archiesolutions.UAM.Bll.LoginManager.MarkAsLoggedIn(login.Login, login.Id);
                        permissions = archiesolutions.UAM.Bll.PermissionManager.GetPermissions(login.Login, login.Id, app.Id);

                        result = true;
                    }
                }
            }

            Session["ConnectionString"] = Config.ConnectionString;
            Session["Permissions"] = permissions;

            return result;
        }
        #endregion

        #region GetConfig
        private void GetConfig()
        {
            Config.ApplicationCode = Configuration.ApplicationCode;
            Config.ConnectionString = Configuration.ConnectionString;
            Config.CommandTimeout = Configuration.CommandTimeout;
            Config.AuthenticationConnectionString = Configuration.ConnectionString;
        }
        #endregion
    }
}