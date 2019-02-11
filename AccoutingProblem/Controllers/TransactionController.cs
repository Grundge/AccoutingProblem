using AccoutingProblem.Implementations;
using AccoutingProblem.Interfaces;
using AccoutingProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccoutingProblem.Controllers
{
    public class TransactionController : Controller
    {
        private IRecordRepository _recordRepository;

        public TransactionController()
        {
            _recordRepository = new RecordRepository(new AccountingContext());
        }

        public TransactionController(IRecordRepository repository)
        {
            _recordRepository = repository;
        }

        // GET: Transaction
        public ActionResult Index(int recordId)
        {
            var transactions = from t in _recordRepository.GetRecordById(recordId).Transactions
                          select t;
            return View(transactions);
        }
    }
}