
using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using static AfluexSchool.Models.Common;

namespace AfluexSchool.Controllers
{
    public class HRManagementController : AdminBaseController
    {
     //   GET: HRManagement
        #region EmpSalary
        public ActionResult EmployeeSalary(HRManagement model)
        {
            #region BindBranch

            int count = 0;
            List<SelectListItem> ddlbranch = new List<SelectListItem>();
            DataSet ds1 = model.GettingBranch();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlbranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlbranch = ddlbranch;
            
            #endregion BindBranch

            #region BindEmp
            List<SelectListItem> ddlEmployee = new List<SelectListItem>();
            ddlEmployee.Add(new SelectListItem { Text = "Select Employee", Value = "0" });
            ViewBag.ddlEmployee = ddlEmployee;
            #endregion BindEmp

            return View(model);
        }
        [HttpPost]
        [ActionName("EmployeeSalary")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeSalaryBy(HRManagement model)
        {

            List<HRManagement> lst = new List<HRManagement>();
            List<HRManagement> lst1 = new List<HRManagement>();

            DataSet ds0 = model.SalaryHeadList();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();
                    obj.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.IsAmtPer = r["IsAmtPer"].ToString();
                    obj.PaidAmount = r["Amount"].ToString();
                    obj.Value = r["Value"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }


            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[1].Rows)
                {
                    HRManagement obj = new HRManagement();
                    obj.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                    obj.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                    obj.SalaryHeadName = r["SalaryHeadName"].ToString();
                    obj.HeadNature = r["HeadNature"].ToString();
                    obj.IsAmtPer = r["IsAmtPer"].ToString();
                    obj.PaidAmount = r["Amount"].ToString();
                    obj.Value = r["Value"].ToString();
                    lst1.Add(obj);
                }
                model.lstList1 = lst1;
            }
            #region BindBranch

            int count = 0;
            List<SelectListItem> ddlbranch = new List<SelectListItem>();
            DataSet ds12 = model.GettingBranch();
            if (ds12 != null && ds12.Tables.Count > 0 && ds12.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds12.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlbranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlbranch = ddlbranch;
              
            #endregion BindBranch
             
            #region Bind Emp
            List<SelectListItem> lstListEmp = new List<SelectListItem>();
            model.Branch = model.Branch != "0" ? model.Branch : null;
            DataSet DS = model.GetEmployeeByBranch();
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in DS.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        lstListEmp.Add(new SelectListItem { Text = "Select Employee", Value = "0" });
                    }
                    lstListEmp.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_TeacherID"].ToString() });
                    count = count + 1;

                }
            }


            ViewBag.ddlEmployee = lstListEmp;
            #endregion BindEmp
            return View(model);
        }

        [HttpPost]
        [ActionName("EmployeeSalary")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSalary(HRManagement obj)
        {
            string FormName = "";
            string Controller = "";


            try
            {

                string noofrows = Request["hdrows"].ToString();
                string headid = "";
                string amt = "";

                DataTable dtEarning = new DataTable();
                dtEarning.Columns.Add("FK_SalaryHeadID", typeof(string));
                dtEarning.Columns.Add("Amount", typeof(string));



                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    headid = Request["ESalHeadId_ " + i].ToString();
                    amt = Request["txtEarAmt " + i].ToString() == "" ? "0" : Request["txtEarAmt " + i].ToString();

                    dtEarning.Rows.Add(headid, amt);
                }

                //=================
                string noofrows1 = Request["hdrows1"].ToString();
                string headid1 = "";
                string amt1 = "";


                DataTable dtDeduction = new DataTable();
                dtDeduction.Columns.Add("FK_SalaryHeadID", typeof(string));
                dtDeduction.Columns.Add("Amount", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows1) - 1; i++)
                {
                    headid1 = Request["DSalHeadId_ " + i].ToString();
                    amt1 = Request["txtDedAmt " + i].ToString() == "" ? "0" : Request["txtDedAmt " + i].ToString();


                    dtDeduction.Rows.Add(headid1, amt1);
                }

              //  obj.EmployeeID = Session["EmpID"].ToString();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtQualification = dtEarning;
                obj.dtWorkExp = dtDeduction;
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEmployeeSalaryDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmpSal"] = " Employee Salary Saved successfully !";

                    }
                    else
                    {
                        TempData["EmpSal"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EmpSal"] = ex.Message;
            }
            FormName = "EmployeeSalary";
            Controller = "HRManagement";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetEmployeeByBranch(string Branch)
        {
            HRManagement obj = new HRManagement();
            #region Bind
            List<SelectListItem> lstListEmp = new List<SelectListItem>();
            obj.Branch = Branch;

            DataSet DS = obj.GetEmployeeByBranch();
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in DS.Tables[0].Rows)
                {

                    lstListEmp.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["PK_TeacherID"].ToString() });

                }
            }
            obj.lstListEmp = lstListEmp;


            #endregion Bind 

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PublishSalary(HRManagement model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("PublishSalary")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PublishSalaryBy(HRManagement model)
        {

            List<HRManagement> lst = new List<HRManagement>();
            model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/MM/yyyy");
            DataSet ds0 = model.PublishSalaryBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();

                    obj.EmployeeID = r["PK_EmployeeID"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    
                    obj.Basic = r["BS"].ToString();
                    obj.HRA = r["HRA"].ToString();
                    obj.MA = r["MA"].ToString();
                    obj.PA = r["PA"].ToString();
                    obj.CA = r["CA"].ToString();
                    obj.PF = r["PF"].ToString();
                    obj.ExtraWork = r["ExtraWork"].ToString();
                    obj.Incentive = r["Incentive"].ToString();
                    obj.OtherPay = r["OtherPay"].ToString();
                    obj.TotalIncome = r["TotalIncome"].ToString();
                    obj.ContributionTosociety = r["ContributionTosociety"].ToString();
                    obj.Advance = r["Advanced"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Insurance = r["Insurance"].ToString();
                    obj.Other = r["Other"].ToString();
                    obj.TotalDeduction = r["TotalDeduction"].ToString();
                    obj.MonthName = r["MonthName"].ToString();
                    obj.Year = r["Year"].ToString();

                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PublishSalary")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePublishSalary(HRManagement obj)
        {
            try
            {

                string noofrows = Request["hdRows"].ToString();


                string chk = "";
                DataTable dtemployee = new DataTable();
                obj.PublishDate = string.IsNullOrEmpty(obj.PublishDate) ? null : Common.ConvertToSystemDate(obj.PublishDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["checkBoxId_ " + i];
                    if (chk == "on")
                    {
                        obj.EmployeeID = Request["Pk_EmployeeID_ " + i].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.SavePublishSalary();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                TempData["PublishSalary"] = " Salary Published successfully";
                            }
                            else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                TempData["PublishSalary"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                        }
                        else
                        {
                            TempData["PublishSalary"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                TempData["PublishSalary"] = ex.Message;
            }
            return RedirectToAction("PublishSalary", "HRManagement");
        }

        public ActionResult SalaryPayment(HRManagement model)
        {
            #region BindAccount
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            ddlAccountHead.Add(new SelectListItem { Text = "Select Account", Value = "0" });
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion BindAccount
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("SalaryPayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SalaryPaymentBy(HRManagement model)
        {

            List<HRManagement> lst = new List<HRManagement>();
            model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/MM/yyyy");
            DataSet ds0 = model.SalaryPaymentBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();

                    obj.EmployeeID = r["PK_TeacherID"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString(); 
                    obj.Basic = r["BS"].ToString();
                    obj.HRA = r["HRA"].ToString();
                    obj.MA = r["MA"].ToString();
                    obj.PA = r["PA"].ToString();
                    obj.CA = r["CA"].ToString();
                    obj.PF = r["PF"].ToString();
                    obj.ExtraWork = r["ExtraWork"].ToString();
                    obj.Incentive = r["Incentice"].ToString();
                    obj.OtherPay = r["OtherPay"].ToString();
                    obj.TotalIncome = r["TotalIncome"].ToString();
                    obj.ContributionTosociety = r["ContributionTosociety"].ToString();
                    obj.Advance = r["Advanced"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Insurance = r["Insurance"].ToString();
                    obj.Other = r["Other"].ToString();
                    obj.TotalDeduction = r["TotalDeduction"].ToString();
                    obj.MonthName = r["MonthName"].ToString();
                    obj.Year = r["Year"].ToString();
                    obj.NetSalary = r["NetSalary"].ToString();
                     obj.SalaryDate = r["PayDate"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            #region BindAccount
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            ddlAccountHead.Add(new SelectListItem { Text = "Select Account", Value = "0" });
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion BindAccount
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }
         
        [HttpPost]
        [ActionName("SalaryPayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSalaryPayment(HRManagement obj)
        {
            string FormName = "";
            string Controller = "";


            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.ChequeDate = string.IsNullOrEmpty(obj.ChequeDate) ? null : Common.ConvertToSystemDate(obj.ChequeDate, "dd/MM/yyyy");
                obj.PublishDate = string.IsNullOrEmpty(obj.PublishDate) ? null : Common.ConvertToSystemDate(obj.PublishDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveSalaryPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SalaryPayment"] = " Salary Payment Saved successfully !";

                    }
                    else
                    {
                        TempData["SalaryPayment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SalaryPayment"] = ex.Message;
            }
            FormName = "SalaryPayment";
            Controller = "HRManagement";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetAccountHeadByBranch(string PK_AccountHead)
        {
            HRManagement obj = new HRManagement();
            #region BindAccount
            List<SelectListItem> lstList = new List<SelectListItem>();

            obj.PK_AccountHead = PK_AccountHead;

            DataSet DS = obj.GetAccountHeadByBranch();
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in DS.Tables[0].Rows)
                {

                    lstList.Add(new SelectListItem { Text = dr["HeadName"].ToString(), Value = dr["Pk_HeadId"].ToString() });

                }
            }
            obj.lstListACCHD = lstList;


            #endregion BindAccount

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeSalarySlip(HRManagement model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("EmployeeSalarySlip")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeSalarySlipBy(HRManagement model)
        {

            List<HRManagement> lst = new List<HRManagement>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds0 = model.EmployeeSalarySlipBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();
                    obj.Pk_PaidSalId = r["Pk_PaidSalId"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.Basic = r["BasicSalary"].ToString();
                    obj.HRA = r["HouseRent"].ToString();
                    obj.MA = r["Medical"].ToString();
                    obj.PA = r["ProfDevAllowance"].ToString();
                    obj.CA = r["Conveyance"].ToString();
                    obj.PF = r["ProfDevAllowance"].ToString();
                    obj.ExtraWork = r["ExtraWork"].ToString();
                    obj.Incentive = r["Incentice"].ToString();
                    obj.OtherPay = r["OtherPay"].ToString();

                    obj.TotalIncome = r["TotalIncome"].ToString();

                    obj.ContributionTosociety = r["ContributionTosociety"].ToString();
                    obj.Advance = r["Advanced"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Insurance = r["Insurance"].ToString();
                    obj.Other = r["Other"].ToString();
                    obj.TotalDeduction = r["TotalDeduction"].ToString();
                    obj.MonthName = r["MonthName"].ToString();
                    obj.Year = r["Year"].ToString();
                    obj.NetSalary = r["NetSalary"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        public ActionResult PrintSalarySlip(string Pk_PaidSalId )
        {
            HRManagement model = new HRManagement();

            List<HRManagement> lst = new List<HRManagement>();
            List<HRManagement> lst1 = new List<HRManagement>();
            model.Pk_PaidSalId = Pk_PaidSalId;
           
            DataSet ds0 = model.EmployeeSalarySlipBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    model.EmployeeID = ds0.Tables[0].Rows[0]["FK_EmpID"].ToString();
                    model.EmployeeCode = ds0.Tables[0].Rows[0]["EmployeeCode"].ToString();
                    model.EmployeeName = ds0.Tables[0].Rows[0]["EmployeeName"].ToString();
                    model.TotalIncome = ds0.Tables[0].Rows[0]["TotalIncome"].ToString();
                    model.TotalDeduction = ds0.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    model.NetSalary = ds0.Tables[0].Rows[0]["NetSalary"].ToString(); 
                    model.MonthName = ds0.Tables[0].Rows[0]["MonthName"].ToString();
                    model.Year = ds0.Tables[0].Rows[0]["Year"].ToString();
                   
                  
                    model.Basic = ds0.Tables[0].Rows[0]["BasicSalary"].ToString();
                    model.HRA = ds0.Tables[0].Rows[0]["HouseRent"].ToString();
                    model.MA = ds0.Tables[0].Rows[0]["Medical"].ToString();
                    model.PA = ds0.Tables[0].Rows[0]["ProfDevAllowance"].ToString();
                    model.CA = ds0.Tables[0].Rows[0]["Conveyance"].ToString();
                    model.PF = ds0.Tables[0].Rows[0]["ProfDevAllowance"].ToString();
                    model.ExtraWork = ds0.Tables[0].Rows[0]["ExtraWork"].ToString();
                    model.Incentive = ds0.Tables[0].Rows[0]["Incentice"].ToString();
                    model.OtherPay = ds0.Tables[0].Rows[0]["OtherPay"].ToString();
                    model.ContributionTosociety = ds0.Tables[0].Rows[0]["ContributionTosociety"].ToString();
                    model.Advance = ds0.Tables[0].Rows[0]["Advanced"].ToString();
                    model.TDS = ds0.Tables[0].Rows[0]["TDS"].ToString();
                    model.Insurance = ds0.Tables[0].Rows[0]["Insurance"].ToString();
                    model.Other = ds0.Tables[0].Rows[0]["Other"].ToString();



                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = SoftwareDetails.Pin1;
                    ViewBag.State1 = SoftwareDetails.State1;
                    ViewBag.City1 = SoftwareDetails.City1;
                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
                    ViewBag.LandLine = SoftwareDetails.LandLine;
                    ViewBag.Website = SoftwareDetails.Website;
                    ViewBag.EmailID = SoftwareDetails.EmailID;
                }
            }
            return View(model);
        }
        #endregion

        #region  EmployeeAttendance
        public ActionResult EmployeeAttendance(HRManagement model)
        {
            #region BindBranch

            int count = 0;
            List<SelectListItem> ddlbranch = new List<SelectListItem>();
            DataSet ds1 = model.GettingBranch();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlbranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlbranch = ddlbranch;

            model.Branch = model.Branch != "0" ? model.Branch : null;
            


            #endregion BindBranch
            return View(model);
        }
        [HttpPost]
        [ActionName("EmployeeAttendance")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmployeeAttendanceBy(HRManagement model)
        {

            List<HRManagement> lst = new List<HRManagement>();

            DataSet ds0 = model.GetEmployeeByBranch();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();

                    obj.EmployeeID = r["PK_TeacherID"].ToString();
                    obj.EmployeeCode = r["LoginID"].ToString();
                    obj.EmployeeName = r["Name"].ToString();

                    lst.Add(obj);
                }
                model.lstList = lst;
            }


            #region BindBranch

            int count = 0;
            List<SelectListItem> ddlbranch = new List<SelectListItem>();
            DataSet ds1 = model.GettingBranch();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlbranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlbranch = ddlbranch;
            
                model.Branch = model.Branch != "0" ? model.Branch : null;
          
            #endregion BindBranch

            return View(model);
        }

        [HttpPost]
        [ActionName("EmployeeAttendance")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveAttendance(HRManagement obj)
        {
            string FormName = "";
            string Controller = "";


            obj.AttendanceDate = string.IsNullOrEmpty(obj.AttendanceDate) ? null : Common.ConvertToSystemDate(obj.AttendanceDate, "dd/MM/yyyy");

            try
            {

                string noofrows = Request["hdRows"].ToString();
                string chk = "";


                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["checkBoxId_ " + i];
                    if (chk == "on")
                    {
                        obj.InTime = Request["txtintime " + i].ToString();
                        obj.OutTime = Request["txtouttime " + i].ToString();
                        obj.EmployeeID = Request["empid " + i].ToString();
                        obj.AddedBy = Session["Pk_AdminId"].ToString();
                        DataSet ds = obj.SaveEmployeeAttendance();
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                TempData["EmployeeAttendance"] = "   Attendance Saved successfully !";

                            }
                            else
                            {
                                TempData["EmployeeAttendance"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["EmployeeAttendance"] = ex.Message;
            }
            FormName = "EmployeeAttendance";
            Controller = "HRManagement";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult AttendanceReport(HRManagement model)
        {

            return View(model);
        }
        [HttpPost]
        [ActionName("AttendanceReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AttendanceReportBy(HRManagement model)
        {

            List<HRManagement> lst = new List<HRManagement>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds0 = model.AttendanceReport();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();

                    obj.AttendanceDate = r["AttendanceDate"].ToString();
                    obj.InTime = r["InTime"].ToString();
                    obj.OutTime = r["OutTime"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }

            return View(model);
        }

        #endregion



    }
}




