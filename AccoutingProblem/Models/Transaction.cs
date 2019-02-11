using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccoutingProblem.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
        public bool IsValid { get; set; }
        public TransactionError TransactionError { get; set; }
    }
}