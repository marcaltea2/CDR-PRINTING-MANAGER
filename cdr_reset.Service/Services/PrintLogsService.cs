using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cdr_reset.Core.Models;
using cdr_reset.Repository.Repositories;
using System.Data.Entity;

namespace cdr_reset.Service.Services
{
    public class PrintLogsService
    {
        private readonly PrintLogsRepository _printLogsRepository;

        public PrintLogsService(string connectionString)
        {
            _printLogsRepository = new PrintLogsRepository(connectionString);
        }

        public IEnumerable<PrintLog> GetAllPrintLogs(string login)
        {
            return _printLogsRepository.GetAllPrintLogs(login);
        }

        public IEnumerable<BusinessUnitName> GetAllBusinessUnits(string login)
        {
            return _printLogsRepository.GetAllBusinessUnits(login);
        }

        public IEnumerable<PrintLog> FilterBySearchTerm(IEnumerable<PrintLog> printlogs, string searchTerm)
        {
            return _printLogsRepository.FilterBySearchTerm(printlogs, searchTerm);
        }

        public IEnumerable<PrintLog> FilterByBusinessUnit(IEnumerable<PrintLog> printlogs, string selectedBusinessUnit)
        {
            return _printLogsRepository.FilterByBusinessUnit(printlogs, selectedBusinessUnit);
        }

        public PrintLog GetPrintLogById(string documentNum, string login)
        {
            return _printLogsRepository.GetPrintLogById(documentNum, login);
        }

        public void UpdatePrintLog(PrintLog printLog)
        {
             _printLogsRepository.UpdatePrintLog(printLog);
        }

        public void DeletePrintLog(string documentNum)
        {
             _printLogsRepository.DeletePrintLog(documentNum);
        }

    }
}
