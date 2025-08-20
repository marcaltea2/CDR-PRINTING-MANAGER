using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cdr_reset.Core.Models;
using cdr_reset.Repository.Context;
using archiesolutions.common;
using System.Reflection;

namespace cdr_reset.Repository.Repositories
{
    public class PrintLogsRepository
    {
        private readonly CDR_Context _context;

        public PrintLogsRepository(string connectionString)
        {
            _context = new CDR_Context(connectionString);
        }

        public IEnumerable<PrintLog> GetAllPrintLogs(string login)
        {
            return _context.PrintLogs.SqlQuery("dbo.usp_GetBusinessUnitName @login", new SqlParameter("@login", login)).ToList();
        }

        public IEnumerable<BusinessUnitName> GetAllBusinessUnits(string login)
        {
            return _context.BusinessUnits.SqlQuery("dbo.usp_GetBusinessUnitList @login", new SqlParameter("@login", login)).ToList();
        }

        public IEnumerable<PrintLog> FilterBySearchTerm(IEnumerable<PrintLog> printlogs, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return printlogs;

            return printlogs.Where(pl => pl.document_num.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public IEnumerable<PrintLog> FilterByBusinessUnit(IEnumerable<PrintLog> printlogs, string selectedBusinessUnit)
        {
            if (string.IsNullOrEmpty(selectedBusinessUnit) || selectedBusinessUnit == "AllPlants")
                return printlogs;

            return printlogs.Where(pl => pl.business_unit == selectedBusinessUnit);
        }

        public PrintLog GetPrintLogById(string documentNum, string login)
        {
            var existingLog = _context.PrintLogs.SqlQuery("dbo.usp_GetBusinessUnitName @login", new SqlParameter("@login", login)).FirstOrDefault(c => c.document_num == documentNum);
            return existingLog ?? null;
        }

        public void UpdatePrintLog(PrintLog printLog)
        {
            var existingLog = _context.PrintLogsTable.FirstOrDefault(c => c.document_num == printLog.document_num);

            if (existingLog != null)
            {
                existingLog.times_printed = 1;
                existingLog.re_printed_date = null;
                existingLog.re_printed_by = null;
                _context.SaveChanges();
            }
        }

        public void DeletePrintLog(string documentNum)
        {
            var existingLog = _context.PrintLogsTable.FirstOrDefault(c => c.document_num == documentNum);
            if (existingLog != null)
            {
                _context.PrintLogsTable.Remove(existingLog);
                _context.SaveChanges();
            }
        }
    }
}
