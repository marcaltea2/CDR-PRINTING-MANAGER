using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using cdr_reset.Core.Models;

namespace cdr_reset.Repository.Context
{
    public class CDR_Context : DbContext
    {
        public CDR_Context(string connectionString) : base(connectionString)
        {
        }

        public DbSet<PrintLog> PrintLogs { get; set; }
        public DbSet<PrintLogTable> PrintLogsTable { get; set; }
        public DbSet<BusinessUnitName> BusinessUnits { get; set; }
    }
}
