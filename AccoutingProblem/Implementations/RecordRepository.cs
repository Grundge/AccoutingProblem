using AccoutingProblem.Interfaces;
using AccoutingProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccoutingProblem.Implementations
{
    public class RecordRepository : IRecordRepository, IDisposable
    {
        private AccountingContext _context;
        private bool _disposed = false;

        public RecordRepository(AccountingContext context)
        {
            _context = context;
        }

        public void DeleteRecord(int recordId)
        {
            Record record = _context.Records.Find(recordId);
            _context.Records.Remove(record);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Record GetRecordById(int recordId)
        {
            return _context.Records.Find(recordId);
        }

        public IEnumerable<Record> GetRecords()
        {
            return _context.Records.ToList();
        }

        public void InsertRecord(Record record)
        {
            _context.Records.Add(record);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateRecord(int recordId)
        {
            throw new NotImplementedException();
        }
    }
}