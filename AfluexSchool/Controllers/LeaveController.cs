using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AfluexSchool.Models;
using System.Data;
using AfluexSchool.Filter;

namespace AfluexSchool.Controllers
{
    public class LeaveController : AdminBaseController
    {
        // GET: Leave

        #region Approved Declined List

        public ActionResult LeaveList(Leave model)
        {

            #region ddlhelclass+
            try
            {
                Student obj = new Student();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlSection = new List<SelectListItem>();
            ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlSection = ddlSection;

            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;

            List<Leave> list = new List<Leave>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Session = Session["SessionId"].ToString();
            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string s = r["IsApproved"].ToString();
                    if (s != "Pending")
                    {
                        Leave obj = new Leave();

                        obj.Reason = r["Reason"].ToString();
                        obj.FromDate = r["FromDate"].ToString();
                        obj.ToDate = r["ToDate"].ToString();
                        obj.StudentName = r["StudentName"].ToString();
                        obj.Status = r["IsApproved"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.Pk_StudentID = r["FK_StudentID"].ToString();
                        obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                        obj.Description = r["Description"].ToString();
                        list.Add(obj);
                    }
                }
                model.listStudent = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSearch")]
        [ActionName("LeaveList")]
        public ActionResult SearchLeave(Leave model)
        {
            List<Leave> list = new List<Leave>();
            if (model.Status == "0") { model.Status = null; }
            if (model.PK_ClassID == "0") { model.PK_ClassID = null; }
            if (model.Fk_SectionID == "0") { model.Fk_SectionID = null; }
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");


            #region ddlhelclass+
            try
            {
                Student obj = new Student();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlsection
            try
            {

                Master obj = new Master();
                obj.Pk_ClassId = model.PK_ClassID;
                if (obj.Pk_ClassId != null)
                {
                    int count4 = 0;
                    List<SelectListItem> ddlSection = new List<SelectListItem>();
                    DataSet ds4 = obj.GettingSectionList();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds4.Tables[0].Rows)
                        {
                            if (count4 == 0)
                            {
                                ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                            }
                            ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                            count4 = count4 + 1;
                        }
                    }

                    ViewBag.ddlSection = ddlSection;



                }
                else
                {

                    List<SelectListItem> ddlsection = new List<SelectListItem>();
                    ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                    ViewBag.ddlSection = ddlsection;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion



            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;
            model.Session = Session["SessionId"].ToString();
            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string s = r["IsApproved"].ToString();
                    if (s != "Pending")
                    {
                        Leave obj = new Leave();

                        obj.Reason = r["Reason"].ToString();
                        obj.FromDate = r["FromDate"].ToString();
                        obj.ToDate = r["ToDate"].ToString();
                        obj.StudentName = r["StudentName"].ToString();
                        obj.Status = r["isApproved"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.Pk_StudentID = r["FK_StudentID"].ToString();
                        obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                        obj.Description = r["Description"].ToString();
                        list.Add(obj);
                    }
                }
                model.listStudent = list;
            }


            return View(model);
        }

        public ActionResult GetSectionByClass(string Fk_ClassID)
        {
            Leave model = new Leave();
            try
            {

                model.PK_ClassID = Fk_ClassID;

                List<SelectListItem> ddlSection = new List<SelectListItem>();

                DataSet ds = model.GetSectionByClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }

                model.ddlSection = ddlSection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Pending Leave

        public ActionResult PendingLeave(Leave model)
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Leave Approval");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission", "Home");
            }
            if (!objPermission.IsUpdate)
            {
                ViewBag.IsEdit = "none";
            }
            else
            {
                ViewBag.IsEdit = "";
            }
            if (!objPermission.IsDelete)
            {
                ViewBag.IsDelete = "none";
            }
            else
            {
                ViewBag.IsDelete = "";
            }
            #endregion
            // model.PK_TeacherID = Session["PK_TeacherID"].ToString();
            model.Status = "Pending";
            List<Leave> listq = new List<Leave>();
            #region ddlhelclass+
            try
            {
                Student obj = new Student();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlSection = new List<SelectListItem>();
            ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlSection = ddlSection;

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Session = Session["SessionId"].ToString();
            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Leave obj = new Leave();

                    obj.Reason = r["Reason"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.Status = r["IsApproved"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Pk_StudentID = r["FK_StudentID"].ToString();
                    obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                    obj.Description = r["Description"].ToString();
                    listq.Add(obj);
                }
                model.listStudent = listq;
            }
            return View(model);
        }


        [HttpPost]
        [OnAction(ButtonName = "ApproveLeave")]
        [ActionName("PendingLeave")]
        public ActionResult ApprovePendingLeave(Leave model)
        {
            string noofrows = Request["hdRows"].ToString();

            string chkselect = "";

            for (int i = 1; i < int.Parse(noofrows); i++)
            {
                try
                {

                    if (Request["chkSelect_ " + i].ToString() == "Checked")
                    {
                        model.UpdatedBy = Session["Pk_AdminId"].ToString();
                        model.PK_StdntLeaveID = Request["PK_StdntLeaveID_ " + i].ToString();
                        model.Description = Request["Description_ " + i].ToString();
                        model.Pk_StudentID = Request["Pk_StudentID_ " + i].ToString();
                        model.Status = "Approved";
                        model.Session = Session["SessionId"].ToString();
                        DataSet ds = model.UpdatingStudentLeaveAplcn();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                TempData["PendingLeave"] = "Approved Successfully";
                                model.Result = "1";
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                            {
                                TempData["PendingLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                return View(model);
                            }
                        }
                    }
                }
                catch { chkselect = "0"; }

            }
            return RedirectToAction("PendingLeave");
        }


        [HttpPost]
        [OnAction(ButtonName = "DeclineLeave")]
        [ActionName("PendingLeave")]
        public ActionResult DeclinePendingLeave(Leave model)
        {
            string noofrows = Request["hdRows"].ToString();

            string chkselect = "";

            for (int i = 1; i < int.Parse(noofrows); i++)
            {
                try
                {

                    if (Request["chkSelect_ " + i].ToString() == "Checked")
                    {
                        model.UpdatedBy = Session["Pk_AdminId"].ToString();
                        model.PK_StdntLeaveID = Request["PK_StdntLeaveID_ " + i].ToString();
                        model.Description = Request["Description_ " + i].ToString();
                        model.Pk_StudentID = Request["Pk_StudentID_ " + i].ToString();
                        model.Status = "Declined";
                        model.Session = Session["SessionId"].ToString();
                        DataSet ds = model.UpdatingStudentLeaveAplcn();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                TempData["PendingLeave"] = "Declined Successfully";
                                model.Result = "1";
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                            {
                                TempData["PendingLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                return View(model);
                            }
                        }
                    }
                }
                catch { chkselect = "0"; }

            }
            return RedirectToAction("PendingLeave");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSearch12")]
        [ActionName("PendingLeave")]
        public ActionResult SearchPendingLeave(Leave model)
        {
            List<Leave> list = new List<Leave>();
            if (model.Status == "0") { model.Status = null; }
            if (model.PK_ClassID == "0") { model.PK_ClassID = null; }
            if (model.Fk_SectionID == "0") { model.Fk_SectionID = null; }
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");


            #region ddlhelclass+
            try
            {
                Student obj = new Student();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlsection
            try
            {

                Master obj = new Master();
                obj.Pk_ClassId = model.PK_ClassID;
                if (obj.Pk_ClassId != null)
                {
                    int count4 = 0;
                    List<SelectListItem> ddlSection = new List<SelectListItem>();
                    DataSet ds4 = obj.GettingSectionList();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds4.Tables[0].Rows)
                        {
                            if (count4 == 0)
                            {
                                ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                            }
                            ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                            count4 = count4 + 1;
                        }
                    }

                    ViewBag.ddlSection = ddlSection;



                }
                else
                {

                    List<SelectListItem> ddlsection = new List<SelectListItem>();
                    ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                    ViewBag.ddlSection = ddlsection;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
            model.Session = Session["SessionId"].ToString();
            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string s = r["IsApproved"].ToString();
                    if (s == "Pending")
                    {
                        Leave obj = new Leave();

                        obj.Reason = r["Reason"].ToString();
                        obj.FromDate = r["FromDate"].ToString();
                        obj.ToDate = r["ToDate"].ToString();
                        obj.StudentName = r["StudentName"].ToString();
                        obj.Status = r["isApproved"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.Pk_StudentID = r["FK_StudentID"].ToString();
                        obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                        obj.Description = r["Description"].ToString();
                        list.Add(obj);
                    }
                }
                model.listStudent = list;
            }

            return View(model);
        }

        public ActionResult GetSecByCla(string Fk_ClassID)
        {
            Leave model = new Leave();
            try
            {

                model.PK_ClassID = Fk_ClassID;

                List<SelectListItem> ddlSection = new List<SelectListItem>();

                DataSet ds = model.GetSectionByClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }

                model.ddlSection = ddlSection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}