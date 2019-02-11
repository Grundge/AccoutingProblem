using AccoutingProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccoutingProblem.Interfaces
{
    public interface IRecordRepository : IDisposable
    {
        IEnumerable<Record> GetRecords();
        Record GetRecordById(int recordId);
        Record InsertRecord(Record record);
        void DeleteRecord(int recordId);
        void UpdateRecord(Record record);
        void Save();
    }
}
