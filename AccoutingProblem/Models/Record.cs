using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccoutingProblem.Models
{
    public class Record
    {
        public int RecordId { get; set; }
        public string RecordUrl { get; set; }
        public string RecordName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}