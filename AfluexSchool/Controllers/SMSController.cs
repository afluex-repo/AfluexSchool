using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class SMSController : AdminBaseController
    {

        #region Send SMS

        public ActionResult SendSMS()
        {
            ViewBag.SchoolName = Common.SoftwareDetails.CompanyName;
            #region Bind SMSTemplate
            Common obj1 = new Common();

            List<SelectListItem> ddlsms = new List<SelectListItem>();
            ddlsms.Add(new SelectListItem { Text = "Select SMS Template", Value = "0" });
            DataSet ds1 = obj1.GetSMSTemplate();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlsms.Add(new SelectListItem { Text = r["TemplateName"].ToString(), Value = r["Msg"].ToString() });

                }
            }

            ViewBag.ddlsms = ddlsms;
            #endregion Bind SMSTemplate

            #region Bind Type
            List<SelectListItem> ddltype = Common.BindTypeForSMS();
            ViewBag.ddltype = ddltype;
            #endregion Bind Type

            #region Bind Class


            List<SelectListItem> ddlclass = new List<SelectListItem>();
            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
            ds1 = obj1.GetClass();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });

                }
            }

            ViewBag.ddlclass = ddlclass;
            #endregion Bind Class

            #region Bind Section


            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            
            ViewBag.ddlsection = ddlsection;
            #endregion Bind Section

            #region BindStaff
            Teacher obj = new Teacher();
            List<SelectListItem> ddlstaff = new List<SelectListItem>();
            ddlstaff.Add(new SelectListItem { Text = "Select Teacher", Value = "0" });
            ds1 = obj.GetTeacherList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlstaff.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_TeacherID"].ToString() });

                }
            }

            ViewBag.ddlstaff = ddlstaff;
            #endregion BindStaff
            return View();
        }
        [HttpPost]
        [ActionName("SendSMS")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetDetails(SendSMS obj)
        {
            ViewBag.SchoolName = Common.SoftwareDetails.CompanyName;
            
            obj.Fk_StaffId = obj.Fk_StaffId == "0" ? null : obj.Fk_StaffId;
            obj.Fk_ClassId = obj.Fk_ClassId == "0" ? null : obj.Fk_ClassId;
            obj.Fk_SectionId = obj.Fk_SectionId == "0" ? null : obj.Fk_SectionId;
            obj.Fk_SessionId = Session["SessionId"].ToString();
            List<SendSMS> list = new List<SendSMS>();
            DataSet ds = obj.GetSMSData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        SendSMS obj112 = new SendSMS();
                        obj112.Name = r["Name"].ToString();
                        obj112.MobileNo = r["MobileNo"].ToString();
                        obj112.Fk_ClassId = r["ClassName"].ToString();
                        obj112.Fk_SectionId = r["SectionName"].ToString();
                         obj112.FatherName = r["FatherName"].ToString();
                        list.Add(obj112);

                    }
                    obj.lstsmsdata = list;
                }
            }
            #region Bind SMSTemplate
            Common obj1 = new Common();

            List<SelectListItem> ddlsms = new List<SelectListItem>();
            ddlsms.Add(new SelectListItem { Text = "Select SMS Template", Value = "0" });
            DataSet ds1 = obj1.GetSMSTemplate();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlsms.Add(new SelectListItem { Text = r["TemplateName"].ToString(), Value = r["Msg"].ToString() });

                }
            }

            ViewBag.ddlsms = ddlsms;
            #endregion Bind SMSTemplate

            #region Bind Type
            List<SelectListItem> ddltype = Common.BindTypeForSMS();
            ViewBag.ddltype = ddltype;
            #endregion Bind Type

            #region Bind Class


            List<SelectListItem> ddlclass = new List<SelectListItem>();
            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
            ds1 = obj1.GetClass();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });

                }
            }

            ViewBag.ddlclass = ddlclass;
            #endregion Bind Class

            #region Bind Section


            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ds1 = obj.GetSection();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                }
            }

            ViewBag.ddlsection = ddlsection;
            #endregion Bind Section

            #region BindStaff
            Teacher obj11 = new Teacher();
            List<SelectListItem> ddlstaff = new List<SelectListItem>();
            ddlstaff.Add(new SelectListItem { Text = "Select Teacher", Value = "0" });
            ds1 = obj11.GetTeacherList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    ddlstaff.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_TeacherID"].ToString() });

                }
            }

            ViewBag.ddlstaff = ddlstaff;
            #endregion BindStaff
            return View(obj);

        }

        [HttpPost]
        [ActionName("SendSMS")]
        [OnAction(ButtonName = "SendSMS")]
        public ActionResult SendSMS(SendSMS obj)
        {
            Session["MobileNo"] = null;
            obj.TotalSMS = obj.TotalSMS.Replace("SMS", "");
            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("Name", typeof(string));
            dtSMS.Columns.Add("Mobile", typeof(string));
            dtSMS.Columns.Add("Status", typeof(string));
            string Hdrows = Request["Hdrows"].ToString();

            for (int i = 0; i <= int.Parse(Hdrows); i++)
            {
                string chk = Request["chk_" + i];
                string mobile = Request["Mbile_" + i];
                string name = Request["Name_" + i];
                string Status = "";
                if (chk == "on")
                {
                    if (mobile.Length < 10)
                    {
                        Status = "Failed";
                    }
                    else if (mobile.Length > 10)
                    {
                        Status = "Failed";
                    }
                    else
                    {
                        Status = "Send";
                    }
                    dtSMS.Rows.Add(name, mobile, Status);
                    string message = obj.SMS + ' ' + Common.SoftwareDetails.CompanyName;
                    BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, mobile, message);
                    
                }
                
            }
            
            obj.dtSMS = dtSMS;
            obj.AddedBy = Session["Pk_AdminId"].ToString();

            DataSet ds = obj.SaveSMSData();
            TempData["SMSMsg"] = "Message Send Successfully.";
            return RedirectToAction("SendSMS");

        }

        [HttpPost]
        [ActionName("SendSMS")]
        [OnAction(ButtonName = "SendSMS1")]
        public ActionResult SendSMS1(SendSMS obj)
        {
            Session["MobileNo"] = null;
            obj.TotalSMS = obj.TotalSMS.Replace("SMS", "");
            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("Name", typeof(string));
            dtSMS.Columns.Add("Mobile", typeof(string));
            dtSMS.Columns.Add("Status", typeof(string));
            string Hdrows = Request["Hdrows"].ToString();

            for (int i = 0; i <= int.Parse(Hdrows); i++)
            {
                string chk = Request["chk_" + i];
                string mobile = Request["Mbile_" + i];
                string name = Request["Name_" + i];
                string Status = "";
                if (chk == "on")
                {
                    if (mobile.Length < 10)
                    {
                        Status = "Failed";
                    }
                    else if (mobile.Length > 10)
                    {
                        Status = "Failed";
                    }
                    else
                    {
                        Status = "Send";
                    }
                    dtSMS.Rows.Add(name, mobile, Status);
                    string message = obj.SMS + ' ' + Common.SoftwareDetails.CompanyName;
                    BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, mobile, message);
                    
                }
                
            }

            obj.dtSMS = dtSMS;
            obj.AddedBy = Session["Pk_AdminId"].ToString();

            DataSet ds = obj.SaveSMSData();
            TempData["SMSMsg"] = "Message Send Successfully.";
            return RedirectToAction("SendSMS");

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

        #endregion

        #region SMS Template

        public ActionResult SMSTemplate(string PK_TemplateId)
        {
            SendSMS model = new SendSMS();
            if (PK_TemplateId != null)
            {
                model.PK_TemplateId = PK_TemplateId;
                DataSet ds = model.GettingTemplateList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.TemplateName = ds.Tables[0].Rows[0]["TemplateName"].ToString();
                    model.Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                    model.PK_TemplateId = ds.Tables[0].Rows[0]["PK_TemplateId"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveSMSTemplate")]
        [ActionName("SMSTemplate")]
        public ActionResult SaveSMSTemplate(SendSMS model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingSMSTemplate();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SMSTemplate"] = "SMS Template Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SMSTemplate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SMSTemplate"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SMSTemplate");
        }

        public ActionResult SMSTemplateList(SendSMS model)
        {
            List<SendSMS> list = new List<SendSMS>();
            DataSet ds = model.GettingTemplateList();
            if(ds!=null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SendSMS obj = new SendSMS();
                    obj.TemplateName = r["TemplateName"].ToString();
                    obj.Msg = r["Msg"].ToString();
                    obj.PK_TemplateId = r["PK_TemplateId"].ToString();
                    list.Add(obj);
                }
                model.TemplateList = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateSMSTemplate")]
        [ActionName("SMSTemplate")]
        public ActionResult UpdateSMSTemplate(SendSMS model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_TemplateId = model.PK_TemplateId;
                DataSet ds = model.UpdatingSMSTemplate();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SMSTemplateList"] = " SMS Template updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SMSTemplate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SMSTemplate"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("SMSTemplateList");
        }

        public ActionResult DeleteSMSTemplate(string PK_TemplateId)
        {
            SendSMS model = new SendSMS();
            model.PK_TemplateId = PK_TemplateId;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingSMSTemplate();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["SMSTemplateList"] = " SMS Template Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["SMSTemplateList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("SMSTemplateList");
        }

        #endregion
    }
}