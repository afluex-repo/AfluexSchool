using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        // GET: Reports
        #region Fee
        public ActionResult FeeReport(Reports obj)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Section List");
            if (Session["PK_AdminID"].ToString() == "1")
            { 
                ViewBag.IsDelete = "";
            }
            else
            {
                ViewBag.IsDelete = "none";
            }
            #endregion

            #region ddlSession
            try
            {
                Student objsess = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();

                DataSet dssess = objsess.SessionList();
                if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dssess.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSession = ddlSession;
                obj.Session = Session["SessionId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlBindClass
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlClass = new List<SelectListItem>();
            DataSet dsClass = objclass.BindClass();

            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                    }
                    ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    countClass = countClass + 1;
                }
            }

            ViewBag.ddlClass = ddlClass;
            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;
            return View(obj);
        }
        [HttpPost]
        [ActionName("FeeReport")]
        [OnAction(ButtonName = "GetDeatils")]
        public ActionResult GetFeeReport(Reports obj)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Section List");
            if (Session["PK_AdminID"].ToString() == "1")
            {
                ViewBag.IsDelete = "";
            }
            else
            {
                ViewBag.IsDelete = "none";
            }
            #endregion
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.Fk_ClassID = (obj.Fk_ClassID) == "0" ? null : obj.Fk_ClassID;
                obj.Fk_SectionID = (obj.Fk_SectionID) == "0" ? null : obj.Fk_SectionID;
                obj.Session = (obj.Session) == "0" ? null : obj.Session;
                DataSet ds = obj.GetFeeReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Reports obj1 = new Reports();
                            obj1.Amount = r["Amount"].ToString();
                            obj1.ReceiptNo = r["ReceiptNo"].ToString();
                            obj1.PaymentDate = r["PaymentDate"].ToString();
                            obj1.PaymentMode = r["PaymentMode"].ToString();
                            obj1.BankDetails = r["BankDetails"].ToString();
                            obj1.BankDetails = r["BankDetails"].ToString();
                            obj1.EncrptNo = Crypto.Encrypt(r["ReceiptNo"].ToString());
                            obj1.ClassName = r["ClassName"].ToString();
                            obj1.SectionName = r["SectionName"].ToString();
                            obj1.StudentName = r["StudentName"].ToString();
                            obj1.LoginId = r["LoginID"].ToString();

                            lst.Add(obj1);
                        }
                        obj.lstfeedata = lst;

                    }
                    else
                    {
                        TempData["FeeReport"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
            #region ddlSession
            try
            {
                Student objsess = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet dssess = objsess.SessionList();
                if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dssess.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }
                obj.Session = Session["SessionId"].ToString();
                ViewBag.ddlSession = ddlSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlBindClass
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlClass = new List<SelectListItem>();
            DataSet dsClass = objclass.BindClass();

            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                    }
                    ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    countClass = countClass + 1;
                }
            }

            ViewBag.ddlClass = ddlClass;
            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;
            return View(obj);
        }

        public ActionResult PrintFeerecipt(string Id)
        {
            List<Reports> lst = new List<Reports>();
            Reports obj = new Reports();
            if (Session["ReceiptNo"] != null)
            {
                obj.ReceiptNo = Session["ReceiptNo"].ToString();
            }
            else
            {
                obj.ReceiptNo = Id;
            }

            DataSet ds = obj.PrintReceipt();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.SessionName = ds.Tables[0].Rows[0]["SessionName"].ToString();
                ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                ViewBag.AddedOn = ds.Tables[0].Rows[0]["AddedOn"].ToString();
                ViewBag.RegistrationNo = ds.Tables[0].Rows[0]["RegistrationNo"].ToString();
                ViewBag.StudentName = ds.Tables[0].Rows[0]["StudentName"].ToString();
                ViewBag.ClassSection = ds.Tables[0].Rows[0]["ClassSection"].ToString();
                ViewBag.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                ViewBag.DateOfBirth = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.MonthName = ds.Tables[0].Rows[0]["MonthName"].ToString();
                ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                ViewBag.BankDetails = ds.Tables[0].Rows[0]["BankDetails"].ToString();
                ViewBag.TotalFee = ds.Tables[0].Rows[0]["TotalFee"].ToString();
                ViewBag.LateFee = ds.Tables[0].Rows[0]["LateFee"].ToString();
                ViewBag.ConcessionFee = ds.Tables[0].Rows[0]["ConcessionFee"].ToString();
                ViewBag.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                ViewBag.Balance = ds.Tables[0].Rows[0]["Balance"].ToString();
                ViewBag.PaidAmountInWords = ds.Tables[0].Rows[0]["PaidAmountInWords"].ToString();
                ViewBag.RecieptBy = ds.Tables[0].Rows[0]["RecieptBy"].ToString();
                ViewBag.Arrear = ds.Tables[0].Rows[0]["Arrear"].ToString();

                ViewBag.TotalAMount = (decimal.Parse(ViewBag.TotalFee) + decimal.Parse(ViewBag.LateFee) + decimal.Parse(ViewBag.Arrear))
                    - (decimal.Parse(ViewBag.ConcessionFee) + decimal.Parse(ViewBag.Discount));
                ViewBag.LandLine = Common.SoftwareDetails.LandLine;
                ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                ViewBag.Website = Common.SoftwareDetails.Website;
                ViewBag.EmailID = Common.SoftwareDetails.EmailID;
                ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;
                ViewBag.State = Common.SoftwareDetails.State1;
                ViewBag.Pin1 = Common.SoftwareDetails.Pin1;
                ViewBag.AffliateNo = Common.SoftwareDetails.AffliateNo;
                ViewBag.FinalAmtAfterCoon = decimal.Parse(ViewBag.TotalFee) - decimal.Parse(ViewBag.ConcessionFee);
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Reports obj1 = new Reports();
                    obj1.FeeTypeName = r["FeeTypeName"].ToString();
                    obj1.InstallmentAmt = r["InstallmentAmt"].ToString();
                    obj1.ConcessionFee = r["ConcessionFee"].ToString();
                    obj1.FinalAmount = r["FinalAmt"].ToString();
                    obj1.PaidAmount = r["PaidAmt"].ToString();


                    lst.Add(obj1);
                }

                obj.lstfeedata = lst;

            }

            return View(obj);

        }
        public ActionResult PrintFeereciptForReport(string Id)
        {
            List<Reports> lst = new List<Reports>();
            Reports obj = new Reports();
            
            
                obj.ReceiptNo = Id;
            

            DataSet ds = obj.PrintReceipt();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.SessionName = ds.Tables[0].Rows[0]["SessionName"].ToString();
                ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                ViewBag.AddedOn = ds.Tables[0].Rows[0]["AddedOn"].ToString();
                ViewBag.RegistrationNo = ds.Tables[0].Rows[0]["RegistrationNo"].ToString();
                ViewBag.StudentName = ds.Tables[0].Rows[0]["StudentName"].ToString();
                ViewBag.ClassSection = ds.Tables[0].Rows[0]["ClassSection"].ToString();
                ViewBag.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                ViewBag.DateOfBirth = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.MonthName = ds.Tables[0].Rows[0]["MonthName"].ToString();
                ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                ViewBag.BankDetails = ds.Tables[0].Rows[0]["BankDetails"].ToString();
                ViewBag.TotalFee = ds.Tables[0].Rows[0]["TotalFee"].ToString();
                ViewBag.LateFee = ds.Tables[0].Rows[0]["LateFee"].ToString();
                ViewBag.ConcessionFee = ds.Tables[0].Rows[0]["ConcessionFee"].ToString();
                ViewBag.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                ViewBag.Balance = ds.Tables[0].Rows[0]["Balance"].ToString();
                ViewBag.PaidAmountInWords = ds.Tables[0].Rows[0]["PaidAmountInWords"].ToString();
                ViewBag.RecieptBy = ds.Tables[0].Rows[0]["RecieptBy"].ToString();
                ViewBag.Arrear = ds.Tables[0].Rows[0]["Arrear"].ToString();

                ViewBag.TotalAMount = (decimal.Parse(ViewBag.TotalFee) + decimal.Parse(ViewBag.LateFee) + decimal.Parse(ViewBag.Arrear))
                    - (decimal.Parse(ViewBag.ConcessionFee) + decimal.Parse(ViewBag.Discount));
                ViewBag.LandLine = Common.SoftwareDetails.LandLine;
                ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                ViewBag.Website = Common.SoftwareDetails.Website;
                ViewBag.EmailID = Common.SoftwareDetails.EmailID;
                ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;
                ViewBag.State = Common.SoftwareDetails.State1;
                ViewBag.Pin1 = Common.SoftwareDetails.Pin1;
                ViewBag.AffliateNo = Common.SoftwareDetails.AffliateNo;
                ViewBag.FinalAmtAfterCoon = decimal.Parse(ViewBag.TotalFee) - decimal.Parse(ViewBag.ConcessionFee);
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Reports obj1 = new Reports();
                    obj1.FeeTypeName = r["FeeTypeName"].ToString();
                    obj1.InstallmentAmt = r["InstallmentAmt"].ToString();
                    obj1.ConcessionFee = r["ConcessionFee"].ToString();
                    obj1.FinalAmount = r["FinalAmt"].ToString();
                    obj1.PaidAmount = r["PaidAmt"].ToString();


                    lst.Add(obj1);
                }

                obj.lstfeedata = lst;

            }
           
            return View(obj);
        }

        public ActionResult DeleteFee(string id)
        {
            Reports model = new Reports();
            try
            {

                model.ReceiptNo = id;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteFee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DeleteFee"] = " Fee Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["DeleteFee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteFee"] = ex.Message;
            }
            return RedirectToAction("FeeReport");
        }
        #endregion

        #region DeletedFeeReport
        public ActionResult DeletedFeeReport()
        {
            return View();
        }
        [HttpPost]
        [ActionName("DeletedFeeReport")]
        [OnAction(ButtonName = "GetDeatils")]
        public ActionResult GetDeletedFeeReport(Reports obj)
        {
            
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                obj.FromDeletedDate = string.IsNullOrEmpty(obj.FromDeletedDate) ? null : Common.ConvertToSystemDate(obj.FromDeletedDate, "dd/MM/yyyy");
                obj.ToDeletedDate = string.IsNullOrEmpty(obj.ToDeletedDate) ? null : Common.ConvertToSystemDate(obj.ToDeletedDate, "dd/MM/yyyy");
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.DeletedFeeReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj1 = new Reports();
                        obj1.StudentName = r["StudentName"].ToString();
                        obj1.LoginId = r["LoginId"].ToString();
                        obj1.ReceiptNo = r["ReceiptNo"].ToString();
                        obj1.PaymentDate = r["PaymentDate"].ToString();
                        obj1.FeeAmount = r["FeeAmount"].ToString();
                        obj1.LateFee = r["LateFee"].ToString();
                        obj1.ConcessionFee = r["ConcessionFee"].ToString();
                        obj1.Discount = r["Discount"].ToString();
                        obj1.PaidAmount = r["PaidAmount"].ToString();
                        obj1.DeletedOn = r["DeletedOn"].ToString();
                        obj1.DeletedBy = r["DeletedBy"].ToString();


                        lst.Add(obj1);
                    }
                    obj.lstfeedata = lst;
                }

                else
                {

                }
               

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
           
            return View(obj);
        }
        #endregion
        
        #region Marksheet
        public ActionResult StudentMarksheet(Reports model)
        {
            #region ddlSession
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet dssess = obj.SessionList();
                if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dssess.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }
                obj.SessionName = Session["SessionId"].ToString();
                ViewBag.ddlSession = ddlSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlBindClass
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlClass = new List<SelectListItem>();
            DataSet dsClass = objclass.BindClass();

            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                    }
                    ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    countClass = countClass + 1;
                }
            }

            ViewBag.ddlClass = ddlClass;
            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;

            #region ddlBindExamType
            Reports objf = new Reports();
            int countd = 0;
            List<SelectListItem> ddlExamType = new List<SelectListItem>();
            DataSet ds1 = objf.ExamTypeList();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (countd == 0)
                    {
                        ddlExamType.Add(new SelectListItem { Text = "--Select ExamType--", Value = "0" });
                    }
                    ddlExamType.Add(new SelectListItem { Text = r["ExamTypeName"].ToString(), Value = r["Pk_ExamTypeId"].ToString() });
                    countd = countd + 1;
                }
            }

            ViewBag.ddlExamType = ddlExamType;
            #endregion

            #region ddlBindStudent
            Reports objst = new Reports();
            int countst = 0;
            List<SelectListItem> lstfeedata = new List<SelectListItem>();
            DataSet ds = objst.GetStudentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        lstfeedata.Add(new SelectListItem { Text = "--Select Employee--", Value = "0" });
                    }
                    lstfeedata.Add(new SelectListItem { Text = r["StudentName"].ToString(), Value = r["Pk_StudentID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.lstfeedata = lstfeedata;
            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("StudentMarksheet")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult StudentMaeksheetBy(Reports obj)
        {

            List<Reports> lst = new List<Reports>();
            try
            {
                obj.Fk_ClassID = (obj.Fk_ClassID) == "0" ? null : obj.Fk_ClassID;
                obj.Fk_SectionID = (obj.Fk_SectionID) == "0" ? null : obj.Fk_SectionID;
                obj.Pk_StudentID = (obj.Pk_StudentID) == "0" ? null : obj.Pk_StudentID;
                obj.ExamTypeName = (obj.ExamTypeName) == "0" ? null : obj.ExamTypeName;
                obj.SessionName = (obj.SessionName) == "0" ? null : obj.SessionName;
                DataSet dlsts = obj.StudentMarksheet();
                if (dlsts != null && dlsts.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in dlsts.Tables[0].Rows)
                    {
                        Reports obj1 = new Reports();
                        obj1.MaximumMarks = r["MaximumMarks"].ToString();
                        obj1.ObtainMarks = r["ObtainMarks"].ToString();
                        obj1.ClassName = r["ClassName"].ToString();
                        obj1.SectionName = r["SectionName"].ToString();
                        obj1.StudentName = r["StudentName"].ToString();
                        obj1.ExamTypeName = r["ExamTypeName"].ToString();
                        obj1.Fk_ClassID = r["Fk_ClassId"].ToString();
                        obj1.Fk_SectionID = r["Fk_SectionId"].ToString();
                        obj1.Pk_StudentID = r["Fk_StudentId"].ToString();
                        obj1.pkid = r["Fk_ExamTypeId"].ToString();

                        lst.Add(obj1);
                    }
                    obj.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
            #region ddlhelclass
            try
            {
                Reports objcl = new Reports();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = objcl.BindClass();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlsection
            try
            {
                Reports objsec = new Reports();
                int count = 0;
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                DataSet ds1 = objsec.GetSectionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                        }
                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlsection = ddlsection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlBindExamType
            Reports objf = new Reports();
            int countd = 0;
            List<SelectListItem> ddlExamType = new List<SelectListItem>();
            DataSet ds2 = objf.ExamTypeList();

            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countd == 0)
                    {
                        ddlExamType.Add(new SelectListItem { Text = "--Select ExamType--", Value = "0" });
                    }
                    ddlExamType.Add(new SelectListItem { Text = r["ExamTypeName"].ToString(), Value = r["Pk_ExamTypeId"].ToString() });
                    countd = countd + 1;
                }
            }

            ViewBag.ddlExamType = ddlExamType;
            #endregion  

            #region ddlBindStudent
            Reports objst = new Reports();
            int countst = 0;
            List<SelectListItem> lstfeedata = new List<SelectListItem>();
            DataSet ds = objst.GetStudentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        lstfeedata.Add(new SelectListItem { Text = "--Select Employee--", Value = "0" });
                    }
                    lstfeedata.Add(new SelectListItem { Text = r["StudentName"].ToString(), Value = r["Pk_StudentID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.lstfeedata = lstfeedata;
            #endregion       
            #region ddlSession
            try
            {
                Student objsess = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet dssess = objsess.SessionList();
                if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dssess.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }
                obj.SessionName = Session["SessionId"].ToString();
                ViewBag.ddlSession = ddlSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            return View(obj);
        }

        public ActionResult GetSectionByClass(string Fk_ClassID)
        {
            Student model = new Student();
            try
            {

                model.Fk_ClassID = Fk_ClassID;

                List<SelectListItem> ddlsection = new List<SelectListItem>();

                DataSet ds = model.GetSectionByClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }

                model.ddlsection = ddlsection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintMarksheet(string Fk_ClassID, string Fk_SectionID, string Pk_StudentID, string Pk_ExamTypeId)
        {
            Reports model = new Reports();

            try
            {
                List<Reports> lst = new List<Reports>();
                model.Fk_SectionID = Fk_SectionID;
                model.Fk_ClassID = Fk_ClassID;
                model.Pk_StudentID = Pk_StudentID;
                model.pkid = Pk_ExamTypeId;
                DataSet dsblock = model.PrintMarksheet();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    ViewBag.StudentName = dsblock.Tables[0].Rows[0]["StudentName"].ToString();
                    ViewBag.ParentName = dsblock.Tables[0].Rows[0]["ParentName"].ToString();
                    ViewBag.ClassName = dsblock.Tables[0].Rows[0]["ClassName"].ToString();
                    ViewBag.SectionName = dsblock.Tables[0].Rows[0]["SectionName"].ToString();
                    ViewBag.Email = dsblock.Tables[0].Rows[0]["Email"].ToString();
                    ViewBag.Mobile = dsblock.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.Address = dsblock.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.Pincode = dsblock.Tables[0].Rows[0]["Pincode"].ToString();
                    ViewBag.State = dsblock.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = dsblock.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.ExamType = dsblock.Tables[0].Rows[0]["ExamTypeName"].ToString();
                    ViewBag.ExamTypeName = dsblock.Tables[0].Rows[0]["ExamTypeName"].ToString();
                    ViewBag.SessionName = dsblock.Tables[0].Rows[0]["SessionName"].ToString();
                    ViewBag.RegistrationNo = dsblock.Tables[0].Rows[0]["RegistrationNo"].ToString();
                    ViewBag.CompanyName = Common.SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = Common.SoftwareDetails.Pin1;
                    ViewBag.State1 = Common.SoftwareDetails.State1;
                    ViewBag.City1 = Common.SoftwareDetails.City1;
                    ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                    ViewBag.LandLine = Common.SoftwareDetails.LandLine;

                }
                else
                {
                    model.Result = "No record found !";
                }
                if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[0].Rows)
                    {
                        Reports obj = new Reports();

                        obj.MaximumMarks = r["MaximumMarks"].ToString();
                        obj.ObtainMarks = r["ObtainMarks"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.SubjectName = r["SubjectName"].ToString();
                        obj.SubjectCode = r["SubjectCode"].ToString();
                        obj.Status = r["Status"].ToString();
                        if (obj.Status == "F")
                        {
                            ViewBag.Result = "Fail";
                        }
                        lst.Add(obj);
                    }
                    model.lstfeedata = lst;
                }
                ViewBag.MaximumMarks = double.Parse(dsblock.Tables[0].Compute("sum(MaximumMarks)", "").ToString()).ToString("n2");
                ViewBag.ObtainMarks = double.Parse(dsblock.Tables[0].Compute("sum(ObtainMarks)", "").ToString()).ToString("n2");
                ViewBag.Perc = ((decimal.Parse(ViewBag.ObtainMarks) * 100) / decimal.Parse(ViewBag.MaximumMarks)).ToString("n2");
            }
            catch (Exception ex)
            {

            }
            return View(model);


        }
        #endregion

        #region Student


        public ActionResult StudentReport(Student model)
        {



            #region ddlhelReligion
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlReligion = new List<SelectListItem>();
                DataSet ds1 = obj.GetReligion();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlReligion.Add(new SelectListItem { Text = "Select Religion", Value = "0" });
                        }
                        ddlReligion.Add(new SelectListItem { Text = r["ReligionName"].ToString(), Value = r["Pk_ReligionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlReligion = ddlReligion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlCategory
            try
            {
                Student obj = new Student();
                int countcat = 0;
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                DataSet ds1 = obj.GetCategory();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (countcat == 0)
                        {
                            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                        }
                        ddlCategory.Add(new SelectListItem { Text = r["Category"].ToString(), Value = r["Category"].ToString() });
                        countcat = countcat + 1;
                    }
                }

                ViewBag.ddlCategory = ddlCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            #region ddlGender
            try
            {
                Student obj = new Student();
                int countgen = 0;
                List<SelectListItem> ddlGender = new List<SelectListItem>();
                DataSet ds1 = obj.GetGender();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (countgen == 0)
                        {
                            ddlGender.Add(new SelectListItem { Text = "Select Gender", Value = "0" });
                        }
                        ddlGender.Add(new SelectListItem { Text = r["Gender"].ToString(), Value = r["Gender"].ToString() });
                        countgen = countgen + 1;
                    }
                }

                ViewBag.ddlGender = ddlGender;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlSession
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet ds1 = obj.SessionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSession = ddlSession;
                obj.SessionName = Session["SessionId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlsection = ddlsection;

            List<Student> list = new List<Student>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.FromDateDOB = string.IsNullOrEmpty(model.FromDateDOB) ? null : Common.ConvertToSystemDate(model.FromDateDOB, "dd/MM/yyyy");
                model.ToDateDOB = string.IsNullOrEmpty(model.ToDateDOB) ? null : Common.ConvertToSystemDate(model.ToDateDOB, "dd/MM/yyyy");
                model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
                model.Fk_SectionID = model.Fk_SectionID == "0" ? null : model.Fk_SectionID;
                model.SessionName = model.SessionName == "0" ? null : model.SessionName;
                model.Gender = model.Gender == "0" ? null : model.Gender;
                model.Religion = model.Religion == "0" ? null : model.Religion;
                model.Category = model.Category == "0" ? null : model.Category;
                DataSet ds = model.StudentReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                        obj.LoginID = r["LoginID"].ToString();
                        obj.SessionName = r["SessionName"].ToString();
                        obj.Fk_ClassID = r["ClassName"].ToString();
                        obj.Fk_SectionID = r["SectionName"].ToString();
                        obj.StudenLoginID = r["LoginID"].ToString();
                        obj.studentName = r["StudentName"].ToString();
                        obj.Medium = r["Medium"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.RegistrationDate = r["RegistrationNo"].ToString();
                        obj.Category = r["Category"].ToString();
                        obj.Gender = r["Gender"].ToString();
                        obj.Address = r["PermanentAddress"].ToString();
                        obj.FatherName = r["FatherName"].ToString();
                        obj.MotherName = r["MotherName"].ToString();
                        obj.Dateofbirth = r["DateOfBirth"].ToString();
                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult StudentViewProfile(Student model, string Pk_StudentID)
        {
            if (Pk_StudentID != null)
            {

                Student objstudent = new Student();

                try
                {
                    objstudent.Pk_StudentID = Pk_StudentID;
                    DataSet ds4 = objstudent.StudentReport();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        objstudent.Pk_StudentID = ds4.Tables[0].Rows[0]["Pk_StudentID"].ToString();
                        objstudent.studentName = ds4.Tables[0].Rows[0]["StudentName"].ToString();
                        objstudent.LoginID = ds4.Tables[0].Rows[0]["LoginID"].ToString();
                        objstudent.ParentLogin_ID = ds4.Tables[0].Rows[0]["ParentLogin_ID"].ToString();
                        objstudent.ParentName = ds4.Tables[0].Rows[0]["ParentName"].ToString();
                        objstudent.Branch = ds4.Tables[0].Rows[0]["BranchName"].ToString();
                        objstudent.ClassName = ds4.Tables[0].Rows[0]["ClassName"].ToString();
                        objstudent.SectionName = ds4.Tables[0].Rows[0]["SectionName"].ToString();
                        objstudent.SessionName = ds4.Tables[0].Rows[0]["SessionName"].ToString();
                        objstudent.Mobile = ds4.Tables[0].Rows[0]["Mobile"].ToString();
                        objstudent.Medium = ds4.Tables[0].Rows[0]["Medium"].ToString();
                        objstudent.FatherName = ds4.Tables[0].Rows[0]["FatherName"].ToString();
                        objstudent.FatherOccupation = ds4.Tables[0].Rows[0]["FOCC"].ToString();
                        objstudent.MotherName = ds4.Tables[0].Rows[0]["MotherName"].ToString();
                        objstudent.MotherOccupation = ds4.Tables[0].Rows[0]["MotheroCC"].ToString();
                        objstudent.Dateofbirth = ds4.Tables[0].Rows[0]["DateOfBirth"].ToString();
                        objstudent.Age = ds4.Tables[0].Rows[0]["Age"].ToString();
                        objstudent.Gender = ds4.Tables[0].Rows[0]["Gender"].ToString();
                        objstudent.Category = ds4.Tables[0].Rows[0]["Category"].ToString();
                        objstudent.Religion = ds4.Tables[0].Rows[0]["Fk_ReligionID"].ToString();
                        objstudent.Nationality = ds4.Tables[0].Rows[0]["Nationality"].ToString();
                        objstudent.AadhaarCard = ds4.Tables[0].Rows[0]["AadhaarCard"].ToString();
                        objstudent.StudentPhoto = ds4.Tables[0].Rows[0]["StudentPhoto"].ToString();
                        objstudent.BirthCetificate = ds4.Tables[0].Rows[0]["BirthCetificate"].ToString();
                        objstudent.PermanentAddress = ds4.Tables[0].Rows[0]["PermanentAddress"].ToString();
                        objstudent.PinCode = ds4.Tables[0].Rows[0]["Pincode"].ToString();
                        objstudent.State = ds4.Tables[0].Rows[0]["State"].ToString();
                        objstudent.City = ds4.Tables[0].Rows[0]["City"].ToString();
                        objstudent.correspondenceAddress = ds4.Tables[0].Rows[0]["CorrespondenceAddress"].ToString();
                        objstudent.correspondencPinCode = ds4.Tables[0].Rows[0]["CorrespondencePinCode"].ToString();
                        objstudent.correspondencState = ds4.Tables[0].Rows[0]["CorrespondencState"].ToString();
                        objstudent.correspondencCity = ds4.Tables[0].Rows[0]["CorrespondencCity"].ToString();
                        objstudent.AlternateNumber = ds4.Tables[0].Rows[0]["AlterNateNumber"].ToString();
                        objstudent.Email = ds4.Tables[0].Rows[0]["Email"].ToString();
                        objstudent.PreviousSchool = ds4.Tables[0].Rows[0]["PreviousSchool"].ToString();
                        objstudent.PreviousClass = ds4.Tables[0].Rows[0]["PreviousClass"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Student"] = ex.Message;
                }
                return View(objstudent);
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult StudentPrintPreview(Student model, string Pk_StudentID)
        {
            if (Pk_StudentID != null)
            {

                Student objstudent = new Student();

                try
                {
                    objstudent.Pk_StudentID = Pk_StudentID;
                    DataSet ds4 = objstudent.StudentReport();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.Pk_StudentID = ds4.Tables[0].Rows[0]["Pk_StudentID"].ToString();
                        ViewBag.studentName = ds4.Tables[0].Rows[0]["StudentName"].ToString();
                        ViewBag.LoginID = ds4.Tables[0].Rows[0]["LoginID"].ToString();
                        ViewBag.ParentLogin_ID = ds4.Tables[0].Rows[0]["ParentLogin_ID"].ToString();
                        ViewBag.ParentName = ds4.Tables[0].Rows[0]["ParentName"].ToString();
                        ViewBag.Branch = ds4.Tables[0].Rows[0]["BranchName"].ToString();
                        ViewBag.ClassName = ds4.Tables[0].Rows[0]["ClassName"].ToString();
                        ViewBag.SectionName = ds4.Tables[0].Rows[0]["SectionName"].ToString();
                        ViewBag.SessionName = ds4.Tables[0].Rows[0]["SessionName"].ToString();
                        ViewBag.Mobile = ds4.Tables[0].Rows[0]["Mobile"].ToString();
                        ViewBag.Medium = ds4.Tables[0].Rows[0]["Medium"].ToString();
                        ViewBag.FatherName = ds4.Tables[0].Rows[0]["FatherName"].ToString();
                        ViewBag.FatherOccupation = ds4.Tables[0].Rows[0]["FOCC"].ToString();
                        ViewBag.MotherName = ds4.Tables[0].Rows[0]["MotherName"].ToString();
                        ViewBag.MotherOccupation = ds4.Tables[0].Rows[0]["MotheroCC"].ToString();
                        ViewBag.Dateofbirth = ds4.Tables[0].Rows[0]["DateOfBirth"].ToString();
                        ViewBag.Age = ds4.Tables[0].Rows[0]["Age"].ToString();
                        ViewBag.Gender = ds4.Tables[0].Rows[0]["Gender"].ToString();
                        ViewBag.Category = ds4.Tables[0].Rows[0]["Category"].ToString();
                        ViewBag.Religion = ds4.Tables[0].Rows[0]["Fk_ReligionID"].ToString();
                        ViewBag.Nationality = ds4.Tables[0].Rows[0]["Nationality"].ToString();
                        ViewBag.AadhaarCard = ds4.Tables[0].Rows[0]["AadhaarCard"].ToString();
                        ViewBag.StudentPhoto = ds4.Tables[0].Rows[0]["StudentPhoto"].ToString();
                        ViewBag.BirthCetificate = ds4.Tables[0].Rows[0]["BirthCetificate"].ToString();
                        ViewBag.PermanentAddress = ds4.Tables[0].Rows[0]["PermanentAddress"].ToString();
                        ViewBag.PinCode = ds4.Tables[0].Rows[0]["Pincode"].ToString();
                        ViewBag.State = ds4.Tables[0].Rows[0]["State"].ToString();
                        ViewBag.City = ds4.Tables[0].Rows[0]["City"].ToString();
                        ViewBag.correspondenceAddress = ds4.Tables[0].Rows[0]["CorrespondenceAddress"].ToString();
                        ViewBag.correspondencPinCode = ds4.Tables[0].Rows[0]["CorrespondencePinCode"].ToString();
                        ViewBag.correspondencState = ds4.Tables[0].Rows[0]["CorrespondencState"].ToString();
                        ViewBag.correspondencCity = ds4.Tables[0].Rows[0]["CorrespondencCity"].ToString();
                        ViewBag.AlternateNumber = ds4.Tables[0].Rows[0]["AlterNateNumber"].ToString();
                        ViewBag.Email = ds4.Tables[0].Rows[0]["Email"].ToString();
                        ViewBag.PreviousSchool = ds4.Tables[0].Rows[0]["PreviousSchool"].ToString();
                        ViewBag.PreviousClass = ds4.Tables[0].Rows[0]["PreviousClass"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Student"] = ex.Message;
                }
                return View(objstudent);
            }
            else
            {
                return View(model);
            }

        }
        #endregion

        #region Teacher
        public ActionResult TeacherReport(Teacher model)
        {
            List<Teacher> lst1 = new List<Teacher>();
            DataSet ds = model.GetTeacherList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Teacher obj = new Teacher();
                        obj.PK_TeacherID = r["PK_TeacherID"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.DOB = r["DOB"].ToString();
                        obj.Gender = r["Gender"].ToString();
                        obj.Category = r["Category"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.MobileNo = r["MobileNo"].ToString();
                        obj.DOJ = r["DOJ"].ToString();
                        obj.LoginID = r["LoginID"].ToString();

                        lst1.Add(obj);

                    }

                }
                catch (Exception ex)
                {

                }
                model.lstTeacherList = lst1;
            }
            return View(model);
        }
        public ActionResult TeacherViewProfile(string PK_TeacherID)
        {

            Teacher model = new Teacher();
            if (PK_TeacherID != null)
            {
                model.PK_TeacherID = PK_TeacherID;
                DataSet ds = model.GetTeacherList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_TeacherID = ds.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                    model.Religion = ds.Tables[0].Rows[0]["Religion"].ToString();
                    model.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    model.Qualification = ds.Tables[0].Rows[0]["Qualification"].ToString();
                    model.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                    model.Image = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.LastExperience = ds.Tables[0].Rows[0]["LastExperience"].ToString();
                    model.LastSchool = ds.Tables[0].Rows[0]["LastSchool"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["pincode"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    model.DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();


                }

            }

            return View(model);
        }
        public ActionResult TeacherPrintView(Teacher model, string PK_TeacherID)
        {
            if (PK_TeacherID != null)
            {
                model.PK_TeacherID = PK_TeacherID;
                DataSet ds = model.GetTeacherList();
                try
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        ViewBag.PK_TeacherID = ds.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                        ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();

                        ViewBag.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                        ViewBag.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                        ViewBag.Category = ds.Tables[0].Rows[0]["Category"].ToString();

                        ViewBag.Religion = ds.Tables[0].Rows[0]["Religion"].ToString();
                        ViewBag.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                        ViewBag.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                        ViewBag.Qualification = ds.Tables[0].Rows[0]["Qualification"].ToString();
                        ViewBag.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                        ViewBag.Image = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                        ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        ViewBag.DateOfBirth = ds.Tables[0].Rows[0]["LastExperience"].ToString();
                        ViewBag.LastSchool = ds.Tables[0].Rows[0]["LastSchool"].ToString();
                        ViewBag.pincode = ds.Tables[0].Rows[0]["pincode"].ToString();
                        ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                        ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                        ViewBag.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        ViewBag.DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                        ViewBag.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();



                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return View(model);
        }
        #endregion

        #region AttendanceReport
        public ActionResult AttendanceReport()
        {
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;
            return View();
        }

        [HttpPost]
        [ActionName("AttendanceReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult StudentAttendanceFilter(Student model)
        {

            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            #region ddlsection
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                DataSet ds1 = obj.GetSectionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                        }
                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlsection = ddlsection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            List<Student> lst = new List<Student>();
            try
            {

                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
                model.Fk_SectionID = model.Fk_SectionID == "0" ? null : model.Fk_SectionID;
                model.Status = model.Status == "0" ? null : model.Status;
                model.SessionName = Session["SessionId"].ToString();

                DataSet ds = model.GetStudentAttendanceDetail();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();

                        obj.studentName = r["StudentName"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.AttendanceDate = r["AttendanceDate"].ToString();
                        obj.Status = r["Status"].ToString();
                        lst.Add(obj);
                    }
                    model.listStudent = lst;

                }

            }
            catch (Exception ex)
            {

            }

            return View(model);

        }
        #endregion

        #region Parent
        public ActionResult ParentReport(Parent model)
        {
            List<Parent> list = new List<Parent>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.ParentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Parent obj = new Parent();
                        obj.Pk_ParentID = r["Pk_ParentID"].ToString();
                        obj.ParentLogin_ID = r["LoginID"].ToString();
                        obj.ParentName = r["ParentName"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.Password = r["Password"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.PinCode = r["Pincode"].ToString();
                        obj.State = r["State"].ToString();
                        obj.City = r["City"].ToString();
                        obj.PAN = r["PAN"].ToString();
                        obj.AadharNo = r["AadharNo"].ToString();
                        list.Add(obj);

                    }
                    model.listparent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Parent"] = ex.Message;
            }

            return View(model);

        }

        public ActionResult ParentViewProfile(string Pk_ParentID)
        {
            if (Pk_ParentID != null)
            {
                Parent obj = new Parent();
                try
                {
                    obj.Pk_ParentID = Pk_ParentID;
                    DataSet ds = obj.ParentList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ParentID = ds.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                        obj.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                        obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        obj.PinCode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                        obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                        obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                        obj.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                        obj.AadharNo = ds.Tables[0].Rows[0]["AadharNo"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Parent"] = ex.Message;
                }
                return View(obj);
            }

            return View();
        }
        public ActionResult ParentPrintView(string Pk_ParentID, Parent model)
        {
            if (Pk_ParentID != null)
            {
                model.Pk_ParentID = Pk_ParentID;
                DataSet ds = model.ParentList();
                try
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        ViewBag.Pk_ParentID = ds.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                        ViewBag.ParentLogin_ID = ds.Tables[0].Rows[0]["LoginID"].ToString();

                        ViewBag.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                        ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();

                        ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        ViewBag.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                        ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                        ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                        ViewBag.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                        ViewBag.AadharNo = ds.Tables[0].Rows[0]["AadharNo"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(model);
        }




        #endregion

        #region Staff

        public ActionResult StaffReport(Staff model)
        {
            List<Staff> list = new List<Staff>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetStaffList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Staff obj = new Staff();
                    obj.LoginID = r["LoginID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PK_StaffID = r["PK_StaffID"].ToString();
                    obj.FatherName = r["FatherName"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Pk_BranchID = r["FK_BranchID"].ToString();
                    obj.Designation = r["Designation"].ToString();
                    obj.PK_StaffDesignationID = r["FK_StaffDesignationID"].ToString();
                    obj.City = r["City"].ToString();
                    obj.PinCode = r["pincode"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.DOB = r["DOB"].ToString();
                    obj.Gender = r["Gender"].ToString();
                    obj.Religion = r["Religion"].ToString();
                    obj.Pk_ReligionId = r["FK_ReligionID"].ToString();
                    obj.Category = r["Category"].ToString();
                    obj.DOJ = r["DOJ"].ToString();
                    obj.EmailID = r["EmailID"].ToString();
                    obj.Qualification = r["Qualification"].ToString();
                    obj.Experience = r["Experience"].ToString();
                    obj.Image = r["ImagePath"].ToString();
                    obj.MobileNo = r["MobileNo"].ToString();
                    list.Add(obj);
                }
                model.lstSDesignationList = list;
            }
            return View(model);
        }

        public ActionResult StaffViewProfile(string PK_StaffID)
        {
            Staff model = new Staff();
            #region ddlBranch
            try
            {

                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet ds1 = model.BranchList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                        }
                        ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlBranch = ddlBranch;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            #region ddlReligion
            try
            {
                int count = 0;
                List<SelectListItem> ddlReligion = new List<SelectListItem>();
                DataSet ds1 = model.GetReligion();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlReligion.Add(new SelectListItem { Text = "Select Religion", Value = "0" });
                        }
                        ddlReligion.Add(new SelectListItem { Text = r["ReligionName"].ToString(), Value = r["Pk_ReligionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlReligion = ddlReligion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            #region ddlCategory
            try
            {
                Student obj = new Student();
                int countcat = 0;
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                DataSet ds1 = obj.GetCategory();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (countcat == 0)
                        {
                            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                        }
                        ddlCategory.Add(new SelectListItem { Text = r["Category"].ToString(), Value = r["Category"].ToString() });
                        countcat = countcat + 1;
                    }
                }

                ViewBag.ddlCategory = ddlCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlGender
            try
            {
                Student obj = new Student();
                int countgen = 0;
                List<SelectListItem> ddlGender = new List<SelectListItem>();
                DataSet ds1 = obj.GetGender();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (countgen == 0)
                        {
                            ddlGender.Add(new SelectListItem { Text = "Select Gender", Value = "0" });
                        }
                        ddlGender.Add(new SelectListItem { Text = r["Gender"].ToString(), Value = r["Gender"].ToString() });
                        countgen = countgen + 1;
                    }
                }

                ViewBag.ddlGender = ddlGender;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            #region ddlDesignation
            try
            {

                int count = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet ds1 = model.StaffDesignationList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["Designation"].ToString(), Value = r["PK_StaffDesignationID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            if (PK_StaffID != null)
            {

                model.PK_StaffID = PK_StaffID;
                DataSet ds = model.GetStaffList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_StaffID = ds.Tables[0].Rows[0]["PK_StaffID"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                    model.Pk_ReligionId = ds.Tables[0].Rows[0]["FK_ReligionID"].ToString();
                    model.Religion = ds.Tables[0].Rows[0]["Religion"].ToString();
                    model.Qualification = ds.Tables[0].Rows[0]["Qualification"].ToString();
                    model.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["pincode"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    model.Image = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                    model.DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    model.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    model.Pk_BranchID = ds.Tables[0].Rows[0]["FK_BranchID"].ToString();
                    model.PK_StaffDesignationID = ds.Tables[0].Rows[0]["FK_StaffDesignationID"].ToString();
                    model.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();

                }

            }

            return View(model);
        }

        public ActionResult StaffPrintView(Staff model, string PK_StaffID)
        {
            if (PK_StaffID != null)
            {
                model.PK_StaffID = PK_StaffID;
                DataSet ds = model.GetStaffList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewBag.PK_StaffID = ds.Tables[0].Rows[0]["PK_StaffID"].ToString();
                    ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    ViewBag.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    ViewBag.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                    ViewBag.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    ViewBag.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                    ViewBag.Pk_ReligionId = ds.Tables[0].Rows[0]["FK_ReligionID"].ToString();
                    ViewBag.Religion = ds.Tables[0].Rows[0]["Religion"].ToString();
                    ViewBag.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                    ViewBag.Qualification = ds.Tables[0].Rows[0]["Qualification"].ToString();
                    ViewBag.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.PinCode = ds.Tables[0].Rows[0]["pincode"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    ViewBag.Image = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                    ViewBag.DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    ViewBag.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    ViewBag.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    ViewBag.Pk_BranchID = ds.Tables[0].Rows[0]["FK_BranchID"].ToString();
                    ViewBag.PK_StaffDesignationID = ds.Tables[0].Rows[0]["FK_StaffDesignationID"].ToString();
                    ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();

                }
            }
            return View(model);
        }
        #endregion

        #region SMSReport
        public ActionResult UsedSMS(SendSMS model, string Status)
        {

            List<SendSMS> list = new List<SendSMS>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.Status = string.IsNullOrEmpty(Status) ? null : Status;
                DataSet ds = model.GetSMSReport();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        SendSMS obj = new SendSMS();
                        obj.SMS = r["Message"].ToString();

                        obj.Name = r["Name"].ToString();
                        obj.MobileNo = r["MobileNo"].ToString();

                        obj.Status = r["SMSStatus"].ToString();
                        obj.AddedOn = r["AddedOn"].ToString();
                        obj.MessageCount = r["SMSCount"].ToString();
                        list.Add(obj);

                    }
                    model.lstsmsdata = list;
                }


            }
            catch (Exception ex)
            {
                TempData["StudentListError"] = ex.Message;
            }

            return View(model);
        }
        #endregion SMSReport

        #region  TransactionLog

        public ActionResult TransactionLog(Reports model)
        {
            #region ddlAction
            try
            {
                Reports obj = new Reports();
                int count = 0;
                List<SelectListItem> ddlAction = new List<SelectListItem>();
                DataSet ds1 = obj.ActionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlAction.Add(new SelectListItem { Text = "Select Action", Value = "0" });
                        }
                        ddlAction.Add(new SelectListItem { Text = r["ActionName"].ToString(), Value = r["ActionName"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlAction = ddlAction;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("TransactionLog")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult LogReportBy(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.ActionName = model.ActionName == "0" ? null : model.ActionName;
                DataSet dlsts = model.TransactionLog();
                if (dlsts != null && dlsts.Tables[0].Rows.Count > 0)
                {

                    Session["dt"] = dlsts.Tables[0];
                    foreach (DataRow r in dlsts.Tables[0].Rows)
                    {

                        Reports obj1 = new Reports();
                        obj1.ActionName = r["ActionName"].ToString();
                        obj1.Remark = r["Remark"].ToString();
                        obj1.TransactionDate = r["TransactionDate"].ToString();
                        obj1.TransactionBy = r["TransactionBy"].ToString();


                        lst.Add(obj1);
                    }
                    model.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {

            }
            #region ddlAction
            try
            {
                Reports obj = new Reports();
                int count = 0;
                List<SelectListItem> ddlAction = new List<SelectListItem>();

                DataSet ds1 = obj.ActionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlAction.Add(new SelectListItem { Text = "Select Action", Value = "0" });
                        }
                        ddlAction.Add(new SelectListItem { Text = r["ActionName"].ToString(), Value = r["ActionName"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlAction = ddlAction;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            return View(model);
        }
        #endregion

        #region DueInstallmentReport

        public ActionResult DueInstallmentReport()
        {

            return View();
        }


        #endregion

        #region RouteWiseTransportReport

        public ActionResult RouteWiseTransportReport(Master model)
        {

            #region ddlRoute
            try
            {
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlRoute = new List<SelectListItem>();
                DataSet ds1 = obj.GettingRoute();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlRoute.Add(new SelectListItem { Text = "Select Route", Value = "0" });
                        }
                        ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["RouteNo"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlRoute = ddlRoute;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            #region ddlVehicleNo
            try
            {
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
                DataSet ds1 = obj.GettingVehicleList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlVehicleNo.Add(new SelectListItem { Text = "Select VehicleNo", Value = "0" });
                        }
                        ddlVehicleNo.Add(new SelectListItem { Text = r["VehicleNo"].ToString(), Value = r["VehicleNo"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlVehicleNo = ddlVehicleNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            return View(model);
        }
        [HttpPost]
        [ActionName("RouteWiseTransportReport")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult RouteWiseTransportReportBy(Master model)
        {
            List<Master> lst = new List<Master>();
            try
            {
                model.RouteNo = model.RouteNo == "0" ? null : model.RouteNo;
                model.VehicleNo = model.VehicleNo == "0" ? null : model.VehicleNo;
                DataSet dlsts = model.RouteWiseTransportReport();
                if (dlsts != null && dlsts.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in dlsts.Tables[0].Rows)
                    {

                        Master obj1 = new Master();
                        obj1.RouteNo = r["RouteNo"].ToString();
                        obj1.AreaName = r["AreaName"].ToString();
                        obj1.VehicleNo = r["VehicleNo"].ToString();
                        obj1.VehicleType = r["VehicleType"].ToString();
                        obj1.Amount = r["Amount"].ToString();

                        lst.Add(obj1);
                    }
                    model.Arealist = lst;

                }

            }
            catch (Exception ex)
            {

            }
            #region ddlRoute
            try
            {
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlRoute = new List<SelectListItem>();
                DataSet ds1 = obj.GettingRoute();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlRoute.Add(new SelectListItem { Text = "Select Route", Value = "0" });
                        }
                        ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["RouteNo"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlRoute = ddlRoute;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            #region ddlVehicleNo
            try
            {
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
                DataSet ds1 = obj.GettingVehicleList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlVehicleNo.Add(new SelectListItem { Text = "Select VehicleNo", Value = "0" });
                        }
                        ddlVehicleNo.Add(new SelectListItem { Text = r["VehicleNo"].ToString(), Value = r["VehicleNo"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlVehicleNo = ddlVehicleNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            return View(model);
        }

        #endregion

        #region ConcessionReport

        public ActionResult ConcessionReport(Reports obj)
        {
            #region ddlSession
            try
            {
                Student objsess = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet dssess = objsess.SessionList();
                if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dssess.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });

                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSession = ddlSession;
                obj.SessionName = dssess.Tables[2].Rows[0]["Pk_SessionId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlfeetype
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlfeetype = new List<SelectListItem>();
            DataSet dsClass = objclass.FeeTypeList();
            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlfeetype.Add(new SelectListItem { Text = "--Select Fee Type--", Value = "0" });
                    }
                    ddlfeetype.Add(new SelectListItem { Text = r["FeeTypeName"].ToString(), Value = r["FeeTypeName"].ToString() });
                    countClass = countClass + 1;
                }
            }
            ViewBag.ddlfeetype = ddlfeetype;
            #endregion

            #region ddlmonth 
            List<SelectListItem> ddlmonth = new List<SelectListItem>();
            DataSet dss = objclass.GetMonth();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                ddlmonth.Add(new SelectListItem { Text = "--Select month--", Value = "0" });
                foreach (DataRow r in dss.Tables[0].Rows)
                {

                    ddlmonth.Add(new SelectListItem { Text = r["MonthName"].ToString(), Value = r["MonthName"].ToString() });
                }
            }
            ViewBag.ddlmonth = ddlmonth;
            #endregion

            return View(obj);
        }
        [HttpPost]
        [ActionName("ConcessionReport")]
        [OnAction(ButtonName = "GetDeatils")]
        public ActionResult GetConcessionReport(Reports obj)
        {

            List<Reports> lst = new List<Reports>();
            try
            {
                obj.MonthName = (obj.MonthName) == "0" ? null : obj.MonthName;
                obj.FeeTypeName = (obj.FeeTypeName) == "0" ? null : obj.FeeTypeName;
                obj.SessionName = (obj.SessionName) == "0" ? null : obj.SessionName;
                DataSet ds = obj.GetConcessionReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj1 = new Reports();
                        obj1.StudentName = r["StudentName"].ToString();
                        obj1.LoginId = r["LoginID"].ToString();
                        obj1.FeeTypeName = r["FeeTypeName"].ToString();
                        obj1.ConcessionFee = r["ConcessionFee"].ToString();
                        obj1.MonthName = r["Monthname"].ToString();
                        obj1.ClassName = r["ClassName"].ToString();
                        obj1.SectionName = r["SectionName"].ToString();
                        lst.Add(obj1);
                    }
                    obj.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
            #region ddlSession
            try
            {
                Student objsess = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet dssess = objsess.SessionList();
                if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dssess.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            obj.SessionName = dssess.Tables[2].Rows[0]["Pk_SessionId"].ToString();
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSession = ddlSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlfeetype
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlfeetype = new List<SelectListItem>();
            DataSet dsClass = objclass.FeeTypeList();
            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlfeetype.Add(new SelectListItem { Text = "--Select Fee Type--", Value = "0" });
                    }
                    ddlfeetype.Add(new SelectListItem { Text = r["FeeTypeName"].ToString(), Value = r["FeeTypeName"].ToString() });
                    countClass = countClass + 1;
                }
            }
            ViewBag.ddlfeetype = ddlfeetype;
            #endregion

            #region ddlmonth 
            List<SelectListItem> ddlmonth = new List<SelectListItem>();
            DataSet dss = objclass.GetMonth();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                ddlmonth.Add(new SelectListItem { Text = "--Select month--", Value = "0" });
                foreach (DataRow r in dss.Tables[0].Rows)
                {

                    ddlmonth.Add(new SelectListItem { Text = r["MonthName"].ToString(), Value = r["MonthName"].ToString() });
                }
            }
            ViewBag.ddlmonth = ddlmonth;
            #endregion
            return View(obj);
        }

        #endregion

        #region DueReport 
        public ActionResult DueReportClassWise(Reports obj)
        {
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.DueReportClassWise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        Reports obj1 = new Reports();
                        obj1.ClassName = r["ClassName"].ToString();
                        obj1.RegistrationAmt = r["RegistrationAmt"].ToString();
                        obj1.AdmissionFee = r["AdmissionFee"].ToString();
                        obj1.TuitionFee = r["TuitionFee"].ToString();
                        obj1.ExaminationFee = r["ExaminationFee"].ToString();
                        obj1.Computerfee = r["Computerfee"].ToString();
                        obj1.Otherfee = r["Otherfee"].ToString();
                        obj1.Totalfee = r["Totalfee"].ToString();
                        obj1.TotalPaid = r["TotalPaid"].ToString();
                        obj1.DueAmt = r["DueAmt"].ToString();

                        obj1.TotalStudent = r["TotalStudent"].ToString();
                        obj1.PaidStudent = r["PaidStudent"].ToString();
                        obj1.UnpaidStudent = r["UnpaidStudent"].ToString();


                        lst.Add(obj1);
                    }
                    ViewBag.TotalStudent = double.Parse(ds.Tables[0].Compute("sum(TotalStudent)", "").ToString()).ToString("n2");
                    ViewBag.TotalPaidStudent = double.Parse(ds.Tables[0].Compute("sum(PaidStudent)", "").ToString()).ToString("n2");
                    ViewBag.TotalUnpaidStudent = double.Parse(ds.Tables[0].Compute("sum(UnpaidStudent)", "").ToString()).ToString("n2");
                    ViewBag.TotalRegistrationAmt = double.Parse(ds.Tables[0].Compute("sum(RegistrationAmt)", "").ToString()).ToString("n2");
                    ViewBag.TotalAdmissionFee = double.Parse(ds.Tables[0].Compute("sum(AdmissionFee)", "").ToString()).ToString("n2");
                    ViewBag.TotalTuitionFee = double.Parse(ds.Tables[0].Compute("sum(TuitionFee)", "").ToString()).ToString("n2");
                    ViewBag.TotalExaminationFee = double.Parse(ds.Tables[0].Compute("sum(ExaminationFee)", "").ToString()).ToString("n2");
                    ViewBag.TotalComputerfee = double.Parse(ds.Tables[0].Compute("sum(Computerfee)", "").ToString()).ToString("n2");
                    ViewBag.TotalOtherfee = double.Parse(ds.Tables[0].Compute("sum(Otherfee)", "").ToString()).ToString("n2");
                    ViewBag.TotalTotalfee = double.Parse(ds.Tables[0].Compute("sum(Totalfee)", "").ToString()).ToString("n2");
                    ViewBag.TotalTotalPaid = double.Parse(ds.Tables[0].Compute("sum(TotalPaid)", "").ToString()).ToString("n2");
                    ViewBag.TotalDueAmt = double.Parse(ds.Tables[0].Compute("sum(DueAmt)", "").ToString()).ToString("n2");
                    obj.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
            obj.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            return View(obj);



        }

        public ActionResult DueReport(Reports obj)
        {
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.DueReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        Reports obj1 = new Reports();
                        obj1.StudentName = r["StudentName"].ToString();
                        obj1.FatherName = r["FathersName"].ToString();
                        obj1.Mobile = r["Mobile"].ToString();
                        obj1.ClassName = r["ClassName"].ToString();
                        obj1.SectionName = r["SectionName"].ToString();
                        obj1.RegistrationAmt = r["RegistrationAmt"].ToString();
                        obj1.AdmissionFee = r["AdmissionFee"].ToString();
                        obj1.TuitionFee = r["TuitionFee"].ToString();
                        obj1.ExaminationFee = r["ExaminationFee"].ToString();
                        obj1.Computerfee = r["Computerfee"].ToString();
                        obj1.Otherfee = r["Otherfee"].ToString();
                        obj1.Totalfee = r["Totalfee"].ToString();
                        obj1.TotalPaid = r["PaidAmount"].ToString();
                        obj1.Balance = r["Balance"].ToString();

                        lst.Add(obj1);
                    }

                    ViewBag.TotalRegistrationAmt = double.Parse(ds.Tables[0].Compute("sum(RegistrationAmt)", "").ToString()).ToString("n2");
                    ViewBag.TotalAdmissionFee = double.Parse(ds.Tables[0].Compute("sum(AdmissionFee)", "").ToString()).ToString("n2");
                    ViewBag.TotalTuitionFee = double.Parse(ds.Tables[0].Compute("sum(TuitionFee)", "").ToString()).ToString("n2");
                    ViewBag.TotalExaminationFee = double.Parse(ds.Tables[0].Compute("sum(ExaminationFee)", "").ToString()).ToString("n2");
                    ViewBag.TotalComputerfee = double.Parse(ds.Tables[0].Compute("sum(Computerfee)", "").ToString()).ToString("n2");
                    ViewBag.TotalOtherfee = double.Parse(ds.Tables[0].Compute("sum(Otherfee)", "").ToString()).ToString("n2");
                    ViewBag.TotalTotalfee = double.Parse(ds.Tables[0].Compute("sum(Totalfee)", "").ToString()).ToString("n2");
                    ViewBag.TotalTotalPaid = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                    ViewBag.Balance = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");
                    obj.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
            obj.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            return View(obj);



        }
        #endregion

        #region MultipleStudentReport
        public ActionResult MultipleStudentReport()
        {
            return View();
        }
        [HttpPost]
        [ActionName("MultipleStudentReport")]
        [OnAction(ButtonName = "GetDeatils")]
        public ActionResult GetMultipleStudentReport(Reports obj)
        {
             
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.MultipleStudentReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj1 = new Reports();
                        obj1.pkid= r["Fk_ParentID"].ToString();
                        obj1.ParentName = r["ParentName"].ToString();
                        obj1.LoginId = r["LoginID"].ToString();
                        obj1.ReceiptNo = r["NoOfStudents"].ToString();
                        obj1.Mobile = r["Mobile"].ToString();
                      
                        lst.Add(obj1);
                    }
                    obj.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
         
            return View(obj);
        }

        public ActionResult GetMultipleStudentByParent(string pkid)
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.pkid = pkid;
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.MultipleStudentReport();
                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Reports obj1 = new Reports();
                        obj1.StudentName = r["StudentName"].ToString();
                        obj1.ClassName = r["ClassName"].ToString();
                        obj1.SectionName = r["SectionName"].ToString();
                        obj1.RegistrationAmt = r["RegistrationNo"].ToString();
                        obj1.LoginId = r["LoginID"].ToString();

                        lst.Add(obj1);
                    }
                    obj.lstfeedata = lst;

                }

            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }

            return View(obj);
        }

        #endregion

        #region HomeworkReport

        public ActionResult HomeworkReport(Reports model)
        {
            List<Reports> list = new List<Reports>();
            try
            {

                model.Session = Session["SessionId"].ToString();
                DataSet ds = model.HomeworkList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        Reports obj = new Reports();
                        obj.HomeWorkID = r["Pk_HomeworkID"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
                        obj.HomeworkDate = r["HomeworkDate"].ToString();
                        obj.HomeworkFile = r["HomeworkFile"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SubjectID = r["SubjectName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.HomeworkBy = r["HomeworkBy"].ToString();
                        obj.TeacherName = r["Name"].ToString();
                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }

            #region ddlBindClass
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet dsClass = objclass.BindClass();

            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                    }
                    ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    countClass = countClass + 1;
                }
            }

            ViewBag.ddlClassName = ddlClassName;
            #endregion
            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            ddlSectionName.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlSectionName = ddlSectionName;

            List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
            ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject--", Value = "0" });
            ViewBag.ddlSubjectName = ddlSubjectName;

            #region Bind Teacher
            List<SelectListItem> ddlTeacherName = new List<SelectListItem>();
            int count2 = 0;
            DataSet ds2 = model.GettingTeacherList();
            ddlTeacherName.Add(new SelectListItem { Text = "Select Teacher", Value = "0" });
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {

                    ddlTeacherName.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_TeacherID"].ToString() });

                }

            }
            ViewBag.ddlTeacherName = ddlTeacherName;

            #endregion
            return View(model);
        }

        public ActionResult GetSubjectNameBySection(string FK_ClassID, string Fk_SectionID)
        {
            Reports model = new Reports();
            try
            {

                model.Fk_SectionID = Fk_SectionID;
                model.Fk_ClassID = FK_ClassID;

                List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
                int count = 0;
                DataSet ds = model.GetSubjectNameBySection();

                if (ds != null && ds.Tables.Count > 0 )
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        ddlSubjectName.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Fk_SubjectID"].ToString() });

                    }
                }
                model.ddlSubjectName = ddlSubjectName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("HomeworkReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchHomeworkReport(Reports model)
        {
            List<Reports> list = new List<Reports>();
            try
            {

                model.Session = Session["SessionId"].ToString();
                if (model.Fk_ClassID == "0")
                {
                    model.Fk_ClassID = null;
                }
                if (model.Fk_SectionID == "0")
                {
                    model.Fk_SectionID = null;
                }
                if (model.PK_TeacherID == "0")
                {
                    model.PK_TeacherID = null;
                }
                if (model.SubjectID == "0")
                {
                    model.SubjectID = null;
                }
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.HomeworkList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.HomeWorkID = r["Pk_HomeworkID"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
                        obj.HomeworkDate = r["HomeworkDate"].ToString();
                        obj.HomeworkFile = r["HomeworkFile"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SubjectID = r["SubjectName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.HomeworkBy = r["HomeworkBy"].ToString();
                        obj.TeacherName = r["Name"].ToString();
                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }

            #region ddlBindClass
            Reports objclass = new Reports();
            int countClass = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet dsClass = objclass.BindClass();

            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[0].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                    }
                    ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    countClass = countClass + 1;
                }
            }

            ViewBag.ddlClassName = ddlClassName;
            #endregion

            #region ddlsection
            model.Fk_ClassID = model.Fk_ClassID;

            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            DataSet ds2 = model.GetSectionByClass();

            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow r in ds2.Tables[0].Rows)
                {

                    ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                }
            }

            ViewBag.ddlSectionName = ddlsection;

            #endregion

            #region Bind Teacher
            List<SelectListItem> ddlTeacherName = new List<SelectListItem>();
            int count2 = 0;
            Reports teclst = new Reports();
            DataSet ds3 = teclst.GettingTeacherList();
            ddlTeacherName.Add(new SelectListItem { Text = "Select Teacher", Value = "0" });
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {

                    ddlTeacherName.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_TeacherID"].ToString() });

                }

            }
            ViewBag.ddlTeacherName = ddlTeacherName;

            #endregion


            model.Fk_SectionID = model.Fk_SectionID;
            model.Fk_ClassID = model.Fk_ClassID;

            List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
            ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject--", Value = "0" });

            DataSet ds4 = model.GetSubjectNameBySection();

            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[1].Rows)
                {

                    ddlSubjectName.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Fk_SubjectID"].ToString() });

                }
            }
            ViewBag.ddlSubjectName = ddlSubjectName;


            return View(model);
        }

        #endregion

        #region EnquiryReport

        public ActionResult EnquiryReport(Reports model)
        {
            List<Reports> list = new List<Reports>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.GetEnquiryList();
            if(ds!=null && ds.Tables.Count > 0)
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ParentName = r["ParentName"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.StudentName  = r["StudentName"].ToString();
                    obj.Fk_ClassID = r["Fk_ClassId"].ToString();
                    obj.FormNo = r["FormNo"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Image = r["Image"].ToString();
                    obj.Pk_EnquiryId = r["Pk_EnquiryId"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.PreviousSchool = r["PreviousSchool"].ToString();
                    obj.AddedOn = r["AddedOn"].ToString();

                    list.Add(obj);
                }
                model.listEnquiry = list;
            }
            return View(model);
        }

        public ActionResult DeleteEnquiry(string Pk_EnquiryId)
        {
            Reports model = new Reports();
            model.Pk_EnquiryId = Pk_EnquiryId;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingEnquiry();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["EnquiryReport"] = "Enquiry Deleted Successfully";
                }
                else if(ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["EnquiryReport"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
                return RedirectToAction("EnquiryReport");
        }
        #endregion

        public ActionResult HomeWorkView(string HomeworkFile, string HomeWorkID)
        {
            Reports model = new Reports();
            model.HomeWorkID = HomeWorkID;
            model.HomeworkFile = HomeworkFile;
            DataSet ds = model.HomeworkList();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.HomeWorkID = ds.Tables[0].Rows[0]["Pk_HomeworkID"].ToString();
                    model.HomeworkFile = ds.Tables[0].Rows[0]["HomeworkFile"].ToString();
                    model.Result = "1";
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}