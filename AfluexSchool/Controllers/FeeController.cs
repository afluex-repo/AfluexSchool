using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class FeeController : AdminBaseController
    {
        // GET: Fee

        #region FeeType
        public ActionResult FeeType(string PK_FeeTypeID)
        {

            Fee model = new Fee();
            if (PK_FeeTypeID != null)
            {

                model.PK_FeeTypeID = PK_FeeTypeID;

                DataSet ds = model.ViewFeeType();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_FeeTypeID = ds.Tables[0].Rows[0]["PK_FeeTypeID"].ToString();
                    model.FeeTypeName = ds.Tables[0].Rows[0]["FeeTypeName"].ToString();

                }
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("FeeType")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult FeeTypeMaster(Fee model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.InsertingFeeType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Fee"] = "Fee Type Saved Successfully";
                    }
                    else
                    {
                        TempData["Fee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;

            }
            return RedirectToAction("FeeType");

        }

        public ActionResult FeeTypeList(Fee model)
        {
            List<Fee> lst1 = new List<Fee>();
            DataSet ds = model.ViewFeeType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Fee obj = new Fee();
                    obj.PK_FeeTypeID = r["PK_FeeTypeID"].ToString();
                    obj.FeeTypeName = r["FeeTypeName"].ToString();
                    Session["PK_FeeTypeID"] = null;
                    lst1.Add(obj);
                }
                model.lstFeeTypeList = lst1;
            }



            return View(model);
        }

        public ActionResult DeleteFeeType(string PK_FeeTypeID)
        {
            Fee model = new Fee();
            try
            {

                model.PK_FeeTypeID = PK_FeeTypeID;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteFeeType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "")
                    {
                        TempData["Fee"] = " Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Fee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Fee"] = ex.Message;
            }
            return RedirectToAction("FeeTypeList");
        }

        [HttpPost]
        [ActionName("FeeType")]
        [OnAction(ButtonName = "btnupdate")]
        public ActionResult UpdateFeeType(string PK_FeeTypeID, string FeeTypeName)
        {
            string FormName = "";
            string Controller = "";
            Fee model = new Fee();
            try
            {
                model.PK_FeeTypeID = PK_FeeTypeID;
                model.FeeTypeName = FeeTypeName;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateFee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "FeeTypeList";
                        Controller = "Fee";
                        TempData["FeeTypeList"] = "Update successfully";
                    }
                    else
                    {
                        //Session["PK_FeeTypeID"] = PK_FeeTypeID;
                        FormName = "FeeType";
                        Controller = "Fee";
                        TempData["FeeTypeList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FeeTypeList"] = ex.Message;
                FormName = "FeeType";
                Controller = "Fee";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Fee structure
        public ActionResult FeeStructure(string Pk_FeeStructureId)
        {

            Fee obj = new Fee();

            #region ddlBindClass
            Fee obj1 = new Fee();
            int count = 0;
            List<SelectListItem> ddlClass = new List<SelectListItem>();
            DataSet ds = obj.GetClassList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {

                        ddlClass.Add(new SelectListItem { Text = " Select", Value = "0" });

                    }


                    ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlClass = ddlClass;
            #endregion





            return View(obj);
        }

        [HttpPost]
        [ActionName("FeeStructure")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFeeStructure(Fee obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();
                obj.Fk_SessionId = Session["SessionId"].ToString();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    obj.FeeTypeName = Request["PK_FeeTypeID_" + i];
                    obj.Jan = Request["txtJan_" + i] == "" ? "0" : Request["txtJan_" + i];
                    obj.Feb = Request["txtFeb_" + i] == "" ? "0" : Request["txtFeb_" + i];
                    obj.Mar = Request["txtMar_" + i] == "" ? "0" : Request["txtMar_" + i];
                    obj.Apr = Request["txtApr_" + i] == "" ? "0" : Request["txtApr_" + i];
                    obj.May = Request["txtMay_" + i] == "" ? "0" : Request["txtMay_" + i];
                    obj.June = Request["txtJune_" + i] == "" ? "0" : Request["txtJune_" + i];
                    obj.July = Request["txtJuly_" + i] == "" ? "0" : Request["txtJuly_" + i];
                    obj.Aug = Request["txtAug_" + i] == "" ? "0" : Request["txtAug_" + i];
                    obj.Sep = Request["txtSep_" + i] == "" ? "0" : Request["txtSep_" + i];
                    obj.Oct = Request["txtOct_" + i] == "" ? "0" : Request["txtOct_" + i];
                    obj.Nov = Request["txtNov_" + i] == "" ? "0" : Request["txtNov_" + i];
                    obj.Dec = Request["txtDec_" + i] == "" ? "0" : Request["txtDec_" + i];
                    DataSet ds = obj.SaveFeeStructure();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["FeeStructure"] = "Fee Structure Saved Successfully";
                        }
                        else
                        {
                            TempData["FeeStructure"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                TempData["FeeStructure"] = ex.Message;
            }
            return RedirectToAction("FeeStructure");
        }
        [HttpPost]
        [ActionName("FeeStructure")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetFeeStructure(Fee obj)
        {
            #region ddlBindClass
            Fee obj1 = new Fee();
            int count = 0;
            List<SelectListItem> ddlClass = new List<SelectListItem>();
            DataSet ds111 = obj.GetClassList();

            if (ds111 != null && ds111.Tables.Count > 0 && ds111.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds111.Tables[0].Rows)
                {
                    if (count == 0)
                    {

                        ddlClass.Add(new SelectListItem { Text = " Select", Value = "0" });

                    }


                    ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlClass = ddlClass;
            #endregion
            List<Fee> lst1 = new List<Fee>();
            try
            {
                obj.Fk_SessionId = Session["SessionId"].ToString();
                DataSet ds = obj.GetFeeStructureList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Fee obj1111 = new Fee();
                        obj1111.PK_FeeTypeID = r["Fk_FeeTypeId"].ToString();
                        obj1111.FeeTypeName = r["FeeTypeName"].ToString();
                        obj1111.Jan = r["Jan"].ToString();
                        obj1111.Feb = r["Feb"].ToString();
                        obj1111.Mar = r["Mar"].ToString();
                        obj1111.Apr = r["Apr"].ToString();
                        obj1111.May = r["May"].ToString();
                        obj1111.June = r["June"].ToString();
                        obj1111.July = r["July"].ToString();
                        obj1111.Aug = r["Aug"].ToString();
                        obj1111.Sep = r["Sep"].ToString();
                        obj1111.Oct = r["Oct"].ToString();
                        obj1111.Nov = r["Nov"].ToString();
                        obj1111.Dec = r["Dec"].ToString();
                        lst1.Add(obj1111);


                    }
                    obj.lstFeeTypeList = lst1;
                }

            }
            catch (Exception ex)
            {
                TempData["FeeStructure"] = ex.Message;
            }
            return View(obj);
        }
        public ActionResult FeeStructureList()
        {
            Fee model = new Fee();
            List<Fee> lst = new List<Fee>();

            DataSet ds = model.GetFeeStructureList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Fee obj = new Fee();
                        obj.Pk_FeeStructureId = r["Pk_FeeStructureId"].ToString();
                        obj.Pk_SessionId = r["Fk_SessionId"].ToString();
                        obj.SessionName = r["SessionName"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.FeeTypeName = r["FeeTypeName"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        Session["Pk_FeeStructureId"] = null;

                        lst.Add(obj);
                    }

                }
                catch (Exception ex)
                {
                    model.Result = ex.Message;
                }

                model.lstFeeTypeList = lst;
            }
            return View(model);
        }

        public ActionResult DeleteFeeStructure(string Pk_FeeStructureId)
        {
            try
            {
                Fee model = new Fee();
                model.Pk_FeeStructureId = Pk_FeeStructureId;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteFeeStructure();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["FeeStructure"] = "Deleted SuccessFully!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["FeeStructureError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FeeStructureError"] = ex.Message;
            }
            return RedirectToAction("FeeStructure");
        }
        [HttpPost]
        [ActionName("FeeStructure")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFeeStructure(string Pk_FeeStructureId, string ClassName, string FeeTypeName, string Amount)
        {
            string FormName = "";
            string Controller = "";
            Fee obj = new Fee();
            try
            {
                obj.Pk_FeeStructureId = Pk_FeeStructureId;

                obj.ClassName = ClassName;
                obj.FeeTypeName = FeeTypeName;
                obj.Amount = Amount;

                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateFeeStructure();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "FeeStructureList";
                        Controller = "FeeStructure";
                        TempData["FeeStructure"] = "Updated Successfully!";
                    }
                    else
                    {
                        Session["Pk_FeeStructureId"] = Pk_FeeStructureId;
                        FormName = "FeeStructure";
                        Controller = "FeeStructure";
                        TempData["FeeStructureError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FeeStructureError"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }


        #endregion

        //#region AdmissionFee
        //public ActionResult StudentFee()
        //{
        //    Common obj = new Common();
        //    #region BindPaymentmode


        //    List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //    ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //    DataSet ds2 = obj.GetPaymentMode();
        //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds2.Tables[0].Rows)
        //        {



        //            ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

        //        }
        //    }

        //    ViewBag.ddlpaymentmode = ddlpaymentmode;

        //    #endregion BindPaymentmode

        //    return View();
        //}
        //[HttpPost]
        //[ActionName("StudentFee")]
        //[OnAction(ButtonName = "GetDeatils")]
        //public ActionResult GetStudentFee(Fee obj)
        //{
        //    #region BindPaymentmode


        //    List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //    ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //    DataSet ds2 = obj.GetPaymentMode();
        //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds2.Tables[0].Rows)
        //        {



        //            ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

        //        }
        //    }

        //    ViewBag.ddlpaymentmode = ddlpaymentmode;

        //    #endregion BindPaymentmode

        //    List<Fee> lst = new List<Fee>();
        //    try
        //    {

        //        DataSet ds = obj.GetDetailsForExaminationFees();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                foreach (DataRow r in ds.Tables[0].Rows)
        //                {
        //                    Fee obj1 = new Fee();
        //                    obj1.FeeTypeName = r["FeeTypeName"].ToString();
        //                    obj1.Amount = r["Amount"].ToString();

        //                    lst.Add(obj1);
        //                }
        //                obj.lstfeesdetails = lst;
        //                ViewBag.Total = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
        //                obj.DisplayName = ds.Tables[1].Rows[0]["StudentName"].ToString();
        //                obj.LoginId = ds.Tables[1].Rows[0]["LoginId"].ToString();
        //                obj.ParentName = ds.Tables[1].Rows[0]["ParentName"].ToString();
        //                obj.ParentMobile = ds.Tables[1].Rows[0]["ParentMobile"].ToString();
        //                obj.ParentLoginId = ds.Tables[1].Rows[0]["ParentLoginId"].ToString();
        //                obj.ParenteEmailId = ds.Tables[1].Rows[0]["ParenteEmailId"].ToString();
        //                obj.ClassName = ds.Tables[1].Rows[0]["ClassName"].ToString();
        //                obj.SectionName = ds.Tables[1].Rows[0]["SectionName"].ToString();
        //                obj.Medium = ds.Tables[1].Rows[0]["Medium"].ToString();
        //                obj.RegistrationNo = ds.Tables[1].Rows[0]["RegistrationNo"].ToString();
        //                obj.BranchName = ds.Tables[1].Rows[0]["BranchName"].ToString();


        //            }
        //            else
        //            {
        //                TempData["Fee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Fee"] = ex.Message;
        //    }
        //    return View(obj);
        //}

        //[HttpPost]
        //[ActionName("StudentFee")]
        //[OnAction(ButtonName = "btnSave")]
        //public ActionResult SaveAdmisionFee(Fee obj)
        //{
        //    #region BindPaymentmode


        //    List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //    ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //    DataSet ds2 = obj.GetPaymentMode();
        //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds2.Tables[0].Rows)
        //        {



        //            ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

        //        }
        //    }

        //    ViewBag.ddlpaymentmode = ddlpaymentmode;

        //    #endregion BindPaymentmode

        //    try
        //    {
        //        obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
        //        obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
        //        obj.AddedBy = Session["Pk_AdminId"].ToString();
        //        DataSet ds = obj.SaveAdmissionFee();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                TempData["Fee"] = "Admission Fees Saved Successfully.";

        //                try
        //                {
        //                    string str2 = "Thanks, you have deposited the admission fee of your ward , of amount "  + ds.Tables[0].Rows[0]["Amount"].ToString();
        //                    BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, ds.Tables[0].Rows[0]["Mobile"].ToString(), str2);

        //                }
        //                catch { }
        //            }
        //            else
        //            {
        //                TempData["Fee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Fee"] = ex.Message;
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        //[ActionName("StudentFee")]
        //[OnAction(ButtonName = "btnSaveandPrint")]
        //public ActionResult SaveandPrintAdmisionFee(Fee obj)
        //{
        //    string FormName = "";
        //    string Controller = "";
        //    #region BindPaymentmode


        //    List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //    ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //    DataSet ds2 = obj.GetPaymentMode();
        //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds2.Tables[0].Rows)
        //        {



        //            ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

        //        }
        //    }

        //    ViewBag.ddlpaymentmode = ddlpaymentmode;

        //    #endregion BindPaymentmode

        //    try
        //    {
        //        obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
        //        obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
        //        obj.AddedBy = Session["Pk_AdminId"].ToString();
        //        DataSet ds = obj.SaveAdmissionFee();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //             //   TempData["Fee"] = "Admission Fees Saved Successfully.";
        //                Session["ReceiptNo"] = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
        //                TempData["Fee"] = "Admission Fees Saved Successfully" + " with Receipt No.: " + ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
        //                try
        //                {
        //                    string str2 = "Thanks, you have deposited the admission fee of your ward , of amount " + ds.Tables[0].Rows[0]["Amount"].ToString();
        //                    BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, ds.Tables[0].Rows[0]["Mobile"].ToString(), str2);

        //                }
        //                catch { }
        //                FormName = "PrintFeerecipt";
        //                Controller = "AdminReports";
        //            }
        //            else
        //            {
        //                TempData["Fee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                FormName = "PrintFeerecipt";
        //                Controller = "Fee";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Fee"] = ex.Message;
        //        FormName = "StudentFee";
        //        Controller = "Fee";
        //    }
        //    return RedirectToAction(FormName, Controller);
        //}



        //#endregion AdmissionFee
        public ActionResult GetStudentList()
        {
            Student obj = new Student();
            List<Student> lst = new List<Student>();
            obj.SessionName = Session["SessionId"].ToString();
            DataSet ds = obj.GetStudentList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Student objList = new Student();
                    objList.studentName = dr["StudentNameForFee"].ToString();
                    objList.LoginID = dr["LoginID"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        #region Fees
        public ActionResult Fees()
        {
            Session["MonthId"] = null;
            Session["MonthIdValidate"] = null;
            Session["LastMonth"] = null;
            Fee obj = new Fee();
            Session["InstallemntNo"] = null;
          
            #region BindPaymentmode


            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {



                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;
            obj.PaymentMode = "1";
            #endregion BindPaymentmode
            #region BindMonth


            List<Fee> lst = new List<Fee>();

            DataSet ds22 = obj.GetMonth();
            if (ds22 != null && ds22.Tables.Count > 0 && ds22.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds22.Tables[1].Rows)
                {
                    Fee obj1 = new Fee();
                    obj1.IsPaid = r["IsPaid"].ToString();
                    obj1.MonthId = r["MonthId"].ToString();

                    obj1.MonthName = r["MonthName"].ToString();
                    lst.Add(obj1);
                }
                obj.lstmonth = lst;
            }



            #endregion BindMonth
            obj.IDLoginId = null;
            obj.MonthId = null;
            obj.LoginId = null;
            return View(obj);
        }
        [HttpPost]
        [ActionName("Fees")]
        [OnAction(ButtonName = "GetDeatils")]
        public ActionResult GetFees(Fee obj)
        {
            #region BindPaymentmode


            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {



                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion BindPaymentmode
            #region BindMonth


            List<Fee> lst1 = new List<Fee>();

            DataSet ds22 = obj.GetMonth();
            if (ds22 != null && ds22.Tables.Count > 0 && ds22.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds22.Tables[1].Rows)
                {
                    Fee obj1 = new Fee();
                    obj1.IsPaid = r["IsPaid"].ToString();
                    obj1.MonthId = r["MonthId"].ToString();

                    obj1.MonthName = r["MonthName"].ToString();
                    lst1.Add(obj1);
                }
                obj.lstmonth = lst1;
            }



            #endregion BindMonth

            return View(obj);
        }

        public ActionResult GetLatefee(string LateFee, string DueDate, string PaymentDate, string PK_ClassID, string InstallmentAmt, string InstallemntNo)
        {
            Fee obj = new Fee();
            if (PaymentDate == "")
            {
                obj.Result = "Please enter payment date first.";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            obj.InstallmentAmt = InstallmentAmt;

            obj.DueDate = Common.ConvertToSystemDate(DueDate, "dd/MM/yyyy");
            obj.PaymentDate = Common.ConvertToSystemDate(PaymentDate, "dd/MM/yyyy");
            obj.PK_ClassID = PK_ClassID;
            DataSet ds = obj.GetFineAMount();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    obj.LateFee = ds.Tables[0].Rows[0]["FineAmount"].ToString();
                    obj.IsDaily = ds.Tables[0].Rows[0]["IsDaily"].ToString();
                    obj.Result = "1";
                }
                else
                {
                    obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConcatenateMonth(string monthid, string LoginID, string Login, string CheckboxStatus)
        {

            Fee obj = new Fee();
            if (monthid == "-1")
            {
                if (CheckboxStatus == "checked")
                {
                    Session["MonthId"] = ",4,5,6,7,8,9,10,11,12,1,2,3,-1,";
                }
                else
                {
                    Session["MonthId"] = "0";
                }
            }
            else
            {
                if (monthid != "0")
                {
                    if (CheckboxStatus == "checked")
                    {

                        if (int.Parse(Session["LastMonth"].ToString()) == 12)
                        {
                            Session["LastMonth"] = 0;
                        }
                        int lastmonth = int.Parse(Session["LastMonth"].ToString()) + 1;

                        if (lastmonth != int.Parse(monthid))
                        {
                            obj.Result = "3";
                            obj.MonthId = monthid;
                            obj.ErrorMessage = "Please select month in Sequence Order";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            Session["LastMonth"] = int.Parse(Session["LastMonth"].ToString()) + 1;
                            if (int.Parse(Session["LastMonth"].ToString()) == 12)
                            {
                                Session["LastMonth"] = 0;
                            }
                        }


                    }
                    else
                    {
                        if (int.Parse(Session["LastMonth"].ToString()) == int.Parse(monthid))
                        {
                            if (int.Parse(Session["LastMonth"].ToString()) == 1)
                            {
                                Session["LastMonth"] = 13;
                            }

                            Session["LastMonth"] = int.Parse(Session["LastMonth"].ToString()) - 1;
                        }
                        else
                        {

                            obj.Result = "4";
                            obj.MonthId = monthid;
                            obj.ErrorMessage = "Please Uncheck in Sequence Order By Last.";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
            }
            if (Session["MonthId"] == null || Session["MonthId"] == "")
            {
                Session["MonthId"] = null;
                Session["MonthId"] = ',' + monthid + ',';
            }
            else
            {
                if (Session["MonthId"].ToString().Contains(',' + monthid + ','))
                {
                    if (monthid != "-1")
                    {
                        Session["MonthId"] = Session["MonthId"].ToString().Replace(',' + monthid + ',', ",");
                    }

                }
                else
                {
                    Session["MonthId"] = ',' + monthid + Session["MonthId"].ToString();
                }
            }
            obj.MonthId = Session["MonthId"].ToString();
            if ((LoginID == null || LoginID == "") && (Login == ""))
            {
                obj.Result = "2";
                obj.ErrorMessage = "Invalid LoginId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            List<Fee> lst = new List<Fee>();
            List<Fee> lstconsession = new List<Fee>();
            List<Fee> PaidFee = new List<Fee>();
            try
            {
                obj.LoginId = LoginID;
                obj.LOGID = Login;
                obj.Fk_SessionId = Session["SessionId"].ToString();
                DataSet ds = obj.GetDetailsForFeesMonth();
                if (ds != null && ds.Tables.Count == 1)
                {
                   
                    obj.Result = "2";
                    obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Session["LastMonth"] == null)
                    {
                        Session["LastMonth"] = ds.Tables[3].Rows[0]["LastMonth"].ToString();
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow s in ds.Tables[2].Rows)
                            {
                                Fee obj2 = new Fee();
                                obj2.IsPaid = s["IsPaid"].ToString();
                                obj2.MonthId = s["MonthId"].ToString();

                                obj2.MonthName = s["MonthName"].ToString();
                                obj2.IsTickTrue = s["activetype"].ToString();
                                PaidFee.Add(obj2);

                            }
                            obj.lstpaidfee = PaidFee;
                        }

                        obj.PK_ClassID = ds.Tables[1].Rows[0]["PK_ClassID"].ToString();
                        obj.DisplayName = ds.Tables[1].Rows[0]["StudentName"].ToString();
                        obj.LoginId = ds.Tables[1].Rows[0]["LoginId"].ToString();
                        obj.PaymentDate = ds.Tables[1].Rows[0]["PaymentDate"].ToString();
                        obj.ParentName = ds.Tables[1].Rows[0]["ParentName"].ToString();
                        obj.ParentMobile = ds.Tables[1].Rows[0]["ParentMobile"].ToString();
                        obj.ParentLoginId = ds.Tables[1].Rows[0]["ParentLoginId"].ToString();
                        obj.ParenteEmailId = ds.Tables[1].Rows[0]["ParenteEmailId"].ToString();
                        obj.ClassName = ds.Tables[1].Rows[0]["ClassName"].ToString();
                        obj.SectionName = ds.Tables[1].Rows[0]["SectionName"].ToString();
                        obj.Medium = ds.Tables[1].Rows[0]["Medium"].ToString();
                        obj.RegistrationNo = ds.Tables[1].Rows[0]["RegistrationNo"].ToString();
                        obj.BranchName = ds.Tables[1].Rows[0]["BranchName"].ToString();
                        obj.PreviousBalance = ds.Tables[1].Rows[0]["PreviousBalance"].ToString();
                        obj.LateFee = ds.Tables[5].Rows[0]["FineAmount"].ToString();
                        obj.Result = "1";
                        obj.detail = "0";

                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                foreach (DataRow r in ds.Tables[0].Rows)
                                {
                                    Fee obj1 = new Fee();
                                    obj1.DueDate = r["DueDate"].ToString();
                                    obj1.FeeTypeName = r["FeeTypeName"].ToString();
                                    obj1.InstallmentAmt = r["InstallmentAmt"].ToString();



                                    lst.Add(obj1);
                                }

                                obj.lstfeesdetails = lst;

                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    foreach (DataRow r in ds.Tables[4].Rows)
                                    {
                                        Fee obj1 = new Fee();
                                        obj1.InstallmentAmt = r["Consession"].ToString();
                                        obj1.FeeTypeName = r["FeeTypeName"].ToString();
                                        obj1.MonthId = r["DueDate"].ToString();
                                        obj1.MonthName = r["MonthName"].ToString();
                                        obj1.PK_FeeTypeID = r["PK_FeeTypeID"].ToString();
                                        lstconsession.Add(obj1);
                                    }
                                    obj.lstconsession = lstconsession;

                                }

                                obj.DueDate = ds.Tables[0].Rows[0]["DueDate"].ToString();
                                obj.InstallemntNo = ds.Tables[0].Rows[0]["InstallemntNo"].ToString();
                                obj.InstallmentAmt = ds.Tables[0].Rows[0]["InstallmentAmt"].ToString();
                                obj.TotalAmount = (double.Parse(ds.Tables[0].Compute("sum(InstallmentAmt)", "").ToString()) + double.Parse(obj.LateFee) + double.Parse(obj.PreviousBalance)).ToString("0.00");

                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    obj.ConsessionFee = (double.Parse(ds.Tables[4].Compute("sum(Consession)", "").ToString())).ToString("0.00");
                                    obj.PaidAmount = ((double.Parse(ds.Tables[0].Compute("sum(InstallmentAmt)", "").ToString()) + double.Parse(obj.LateFee) + double.Parse(obj.PreviousBalance)) - double.Parse(obj.ConsessionFee)).ToString("0.00");

                                }
                                else
                                {
                                    obj.PaidAmount = ((double.Parse(ds.Tables[0].Compute("sum(InstallmentAmt)", "").ToString()) + double.Parse(obj.LateFee) + double.Parse(obj.PreviousBalance))).ToString("0.00");
                                    obj.ConsessionFee = "0";
                                }
                                obj.detail = "1";
                            }
                            obj.Discount = "0";
                            obj.Result = "1";


                        }
                        //else
                        //{
                        //    obj.Result = "0";  //ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        //}
                    }
                    else
                    {
                        obj.Result = "0";
                    }
                }
            }


            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("Fees")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFees(Fee obj)
        {
            #region BindPaymentmode


            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {



                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion BindPaymentmode
            #region BindMonth


            List<Fee> lst = new List<Fee>();

            DataSet ds22 = obj.GetMonth();
            if (ds22 != null && ds22.Tables.Count > 0 && ds22.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds22.Tables[1].Rows)
                {
                    Fee obj1 = new Fee();
                    obj1.IsPaid = r["IsPaid"].ToString();
                    obj1.MonthId = r["MonthId"].ToString();

                    obj1.MonthName = r["MonthName"].ToString();
                    lst.Add(obj1);
                }
                obj.lstmonth = lst;
            }



            #endregion BindMonth
            try
            {
                obj.LoginId = string.IsNullOrEmpty(obj.LoginId) ? obj.IDLoginId : obj.LoginId;
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Fk_SessionId = Session["SessionId"].ToString();
                if(obj.discountflag=="1")
                {
                    obj.Discount = obj.Discount;
                }
                else
                {
                    obj.Discount = "0";
                }
                string noofrows = "0";
                if (Request["hdrows"] != null)
                {
                    noofrows = Request["hdrows"].ToString();
                }


                string feetypeid = "";
                string amnt = "";
                string monthid = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("Fk_FeeTypeId ", typeof(string));
                dtst.Columns.Add("Month", typeof(string));
                dtst.Columns.Add("Amount", typeof(string));



                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {

                    feetypeid = Request["PK_FeeTypeID " + i].ToString();
                    amnt = Request["InstallmentAmt " + i].ToString();
                    monthid = Request["MonthId " + i].ToString();
                    dtst.Rows.Add(feetypeid, monthid, amnt);

                }


                obj.dtTable = dtst;
                DataSet ds = obj.SaveFee();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["ReceiptNo"] = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                        TempData["InstFee"] = " Fees Saved Successfully.";
                        try
                        {
                            decimal amt = decimal.Parse(obj.PaidAmount);
                            string str2 = "Thanks, you have deposited fee of your ward  " +obj.DisplayName+" ,  of amount " + amt.ToString();
                            BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, ds.Tables[0].Rows[0]["Mobile"].ToString(), str2);

                        }
                        catch { }
                        return RedirectToAction("PrintFeerecipt", "AdminReports");
                    }
                    else
                    {
                        TempData["InstFee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["InstFee"] = ex.Message;
            }
            return View(obj);
        }



        #endregion Fees


        public ActionResult CheckPassword(string Password)
        {
            Fee obj = new Fee();
            obj.Password = Password;
            DataSet ds = obj.CheckPassword();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Result = "1";
            }
            else
            {
                obj.Result = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}