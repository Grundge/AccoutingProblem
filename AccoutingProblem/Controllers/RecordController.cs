using AccoutingProblem.Implementations;
using AccoutingProblem.Interfaces;
using AccoutingProblem.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AccoutingProblem.Controllers
{
    public class RecordController : Controller
    {
        private IRecordRepository _recordRepository;

        public RecordController()
        {
            _recordRepository = new RecordRepository(new AccountingContext());
        }

        public RecordController(IRecordRepository repository)
        {
            _recordRepository = repository;
        }

        // GET: Record
        public ActionResult Records()
        {
            var records = from r in _recordRepository.GetRecords()
                          select r;
            return View(records);
        }

        public ActionResult UploadRecord()
        {
            return View();
        }

        // POST: Record file
        [HttpPost]
        public ActionResult UploadRecord(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/records"), fileName);
                file.SaveAs(path);

                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx" || Path.GetExtension(file.FileName).ToLower() == ".xls")
                {
                    Record record = LoadFile(file.InputStream);
                    return View(record);
                    //Thread thread = new Thread(() => LoadFile(file.InputStream));
                    //thread.Start();
                }
            }

            return RedirectToAction("UploadRecord");
        }

        private Record LoadFile(Stream file)
        {
            try
            {
                if (file != null)
                {
                    XLWorkbook Workbook;
                    try//incase if the file is corrupt
                    {
                        Workbook = new XLWorkbook(file);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, $"Check your file. {ex.Message}");
                        return null;
                    }
                    IXLWorksheet WorkSheet = null;
                    try//incase if the sheet you are looking for is not found
                    {
                        WorkSheet = Workbook.Worksheet("Sheet1");

                    }
                    catch
                    {
                        ModelState.AddModelError(String.Empty, "Sheet1 not found!");
                        return null;
                    }
                    WorkSheet.FirstRow().Delete();//if you want to remove ist row

                    Record record = new Record()
                    {
                        Created = DateTime.Now,
                        IsDeleted = false,
                        RecordName = Guid.NewGuid().ToString(),
                        Transactions = new List<Transaction>(),
                        Deleted = null,
                        Updated = null,
                    };

                    record = _recordRepository.InsertRecord(record);
                    _recordRepository.Save();

                    foreach (var row in WorkSheet.RowsUsed())
                    {
                        record.Transactions.Add(new Transaction()
                        {
                            Account = row.Cell(1).Value.ToString(),
                            Description = row.Cell(2).Value.ToString(),
                            CurrencyCode = row.Cell(3).Value.ToString(),
                            Amount = double.Parse(row.Cell(4).Value.ToString()),
                            TransactionError = TransactionError.None
                        });
                        _recordRepository.UpdateRecord(record);
                        _recordRepository.Save();
                        ////do something here
                        //row.Cell(1).Value.ToString();//Get ist cell. 1 represent column number
                        //Debug.WriteLine(row.Cell(1).Value.ToString());
                        //Debug.WriteLine(row.Cell(2).Value.ToString());
                        //Debug.WriteLine(row.Cell(3).Value.ToString());
                        //Debug.WriteLine(row.Cell(4).Value.ToString());
                    }
                    

                    return record;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}