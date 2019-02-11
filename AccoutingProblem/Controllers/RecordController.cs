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
        public ActionResult Index()
        {
            var records = from r in _recordRepository.GetRecords()
                          select r;
            return View(records);
        }

        // POST: Record file
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/records"), fileName);
                file.SaveAs(path);

                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx" || Path.GetExtension(file.FileName).ToLower() == ".xls")
                {
                    Thread thread = new Thread(() => LoadFile(file.InputStream));
                    thread.Start();
                }
            }

            return RedirectToAction("Index");
        }

        private void LoadFile(Stream file)
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
                        return;
                    }
                    IXLWorksheet WorkSheet = null;
                    try//incase if the sheet you are looking for is not found
                    {
                        WorkSheet = Workbook.Worksheet("Sheet1");

                    }
                    catch
                    {
                        ModelState.AddModelError(String.Empty, "Sheet1 not found!");
                        return;
                    }
                    WorkSheet.FirstRow().Delete();//if you want to remove ist row

                    foreach (var row in WorkSheet.RowsUsed())
                    {
                        //do something here
                        row.Cell(1).Value.ToString();//Get ist cell. 1 represent column number
                        Debug.WriteLine(row.Cell(1).Value.ToString());
                        Debug.WriteLine(row.Cell(2).Value.ToString());
                        Debug.WriteLine(row.Cell(3).Value.ToString());
                        Debug.WriteLine(row.Cell(4).Value.ToString());
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}