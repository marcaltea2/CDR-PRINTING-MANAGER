using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using cdr_reset.Core.Models;

namespace cdr_reset.Web.Models
{
    public class DashboardViewModel
    {
        public IPagedList<PrintLog> PrintLogs { get; set; }
        public IEnumerable<BusinessUnitName> BusinessUnits { get; set; }
    }
}