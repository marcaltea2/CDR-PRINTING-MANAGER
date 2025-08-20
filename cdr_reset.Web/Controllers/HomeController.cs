using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cdr_reset.Service.Services;
using cdr_reset.Core.Models;
using archiesolutions.common;
using archiesolutions.UAM.BusinessEntities;
using PagedList;
using cdr_reset.Web.Models;
using System.Data;

namespace cdr_reset.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home

        #region Dashboard
         [OutputCache(Duration = 60)]
        public ActionResult Dashboard(int? page, string searchTerm, string selectedBusinessUnit)
        {
            GetConfig();

            User login = (User)Session["User"];
            PermissionCollection permissions = null;

            if (login != null)
            {
                App app = archiesolutions.UAM.Bll.AppManager.GetItemByCode(login.Login, Config.ApplicationCode);
                if (app != null)
                {
                    Config.ConnectionString = app.ConnectionString;
                    var printLogsService = new PrintLogsService(Config.ConnectionString);

                    // Get business units
                    var businessUnits = GetBusinessUnits(printLogsService, login.Login);

                    List<PrintLog> printlogs = new List<PrintLog>();
                    if (!string.IsNullOrEmpty(selectedBusinessUnit) && selectedBusinessUnit != "Select Business Unit")
                    {
                        // Get all print logs
                        printlogs = printLogsService.GetAllPrintLogs(login.Login).ToList();
                        Session["PrintLogs"] = printlogs;

                        // Apply filters
                        printlogs = FilterByBusinessUnit(printLogsService, printlogs, selectedBusinessUnit);
                        printlogs = FilterBySearchTerm(printLogsService, printlogs, searchTerm);
                    }

                    // Pagination
                    const int pageSize = 9;
                    int pageNumber = (page ?? 1);
                    IPagedList<PrintLog> pagedList = printlogs.ToPagedList(pageNumber, pageSize);

                    var viewModel = new DashboardViewModel
                    {
                        PrintLogs = pagedList,
                        BusinessUnits = businessUnits
                    };

                    return View(viewModel);
                }

                Session["ConnectionString"] = Config.ConnectionString;
                Session["Permissions"] = permissions;
            }

            return View();
        }
        #endregion

        #region GetBusinessUnits
        private List<BusinessUnitName> GetBusinessUnits(PrintLogsService printLogsService, string login)
        {
            return printLogsService.GetAllBusinessUnits(login).ToList();
        }
        #endregion

        #region FilterBySearchTerm
        private List<PrintLog> FilterBySearchTerm(PrintLogsService printLogsService, List<PrintLog> printlogs, string searchTerm)
        {
            return printLogsService.FilterBySearchTerm(printlogs, searchTerm).ToList();
        }
        #endregion

        #region FilterByBusinessUnit
        private List<PrintLog> FilterByBusinessUnit(PrintLogsService printLogsService, List<PrintLog> printlogs, string selectedBusinessUnit)
        {
            return printLogsService.FilterByBusinessUnit(printlogs, selectedBusinessUnit).ToList();
        }
        #endregion

        #region GetPrintLog

        public ActionResult GetPrintLog(string documentNum)
        {
            User login = (User)Session["User"];

            if(login != null)
            {
                //var printLogService = new PrintLogsService(Config.ConnectionString);
                //var SelectedPrintLog = printLogService.GetPrintLogById(documentNum, login.Login);

                var printlogs = (List<PrintLog>)Session["PrintLogs"];
                var SelectedPrintLog = printlogs.FirstOrDefault(c => c.document_num == documentNum);

                return PartialView("PopupModal", SelectedPrintLog);
            }

            return PartialView("PopupModal", null);
        }

        #endregion

        #region UpdatePrintLog

        [HttpPost]
        public ActionResult UpdatePrintLog(string documentNum)
        {
            User login = (User)Session["User"];

            if (login != null)
            {
                var printLogService = new PrintLogsService(Config.ConnectionString);
                var SelectedPrintLog = printLogService.GetPrintLogById(documentNum, login.Login);

                printLogService.UpdatePrintLog(SelectedPrintLog);
            }

            return Json(new { success = true, message = "Print log updated successfully." });
        }

        #endregion

        #region DeletePrintLog
        public ActionResult DeletePrintLog(string documentNum)
        {
            User login = (User)Session["User"];

            if(login != null)
            {
                var printLogService = new PrintLogsService(Config.ConnectionString);

                printLogService.DeletePrintLog(documentNum);
            }

            return Json(new { success = true, message = "Print log deleted successfully." });
        }

        #endregion

        #region Logout

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            HttpContext.Response.RemoveOutputCacheItem("/Home/Dashboard");
            System.Web.Security.FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account"); // change if you use another login controller
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