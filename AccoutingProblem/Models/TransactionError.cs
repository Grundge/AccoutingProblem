using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccoutingProblem.Models
{
    public enum TransactionError
    {
        None,
        InvalidIso4217,
        InvalidAmount,
        EmptyName,
        EmptyDescription
    }
}