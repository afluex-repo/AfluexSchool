using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AfluexSchool.Models;
using AfluexSchool.Filter;
using System.Data;
using System.IO;

namespace AfluexSchool.Controllers
{
    public class MasterController : AdminBaseController
    {
        // GET: Master

        #region Session
        public ActionResult SessionMaster(string Pk_SessionId)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Add Session");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission", "Home");
            }
            else
            {


            }
            #endregion
            Master model = new Master();
            model.Pk_SessionId = Pk_SessionId;
            if (Pk_SessionId != null)
            {
                DataSet ds = model.GettingSession();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Pk_SessionId = ds.Tables[0].Rows[0]["Pk_SessionId"].ToString();
                    model.SessionName = ds.Tables[0].Rows[0]["SessionName"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveSession")]
        [ActionName("SessionMaster")]
        public ActionResult SavingSessionMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingSession();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SavingSessionMaster"] = "Session Saved";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SavingSessionMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SavingSessionMaster"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SessionMaster");
        }

        public ActionResult SessionList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Session List");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission", "Home");
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
            List<Master> lst = new List<Master>();

            DataSet ds = model.GettingSession();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SessionName = r["SessionName"].ToString();
                    obj.Pk_SessionId = r["Pk_SessionId"].ToString();
                    obj.isDeleted = r["isDeleted"].ToString();
                    lst.Add(obj);
                }
                model.sessionLst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateSession")]
        [ActionName("SessionMaster")]
        public ActionResult UpdatingSession(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.Pk_SessionId = model.Pk_SessionId;
                DataSet ds = model.UpdatingSession();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SessionList"] = "Session Updated";
                    }
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SessionList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SavingSessionMaster"] = ex.Message;
                return View(model);

            }
            return RedirectToAction("SessionList");
        }

        public ActionResult DeletingSession(string Pk_SessionId)
        {
            Master model = new Master();
            try
            {

                model.DeletedBy = Session["Pk_AdminId"].ToString();
                model.Pk_SessionId = Pk_SessionId;
                DataSet ds = model.DeletingSession();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SessionList"] = "New Session Activated";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SessionList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SessionList"] = ex.Message;
                return View(model);

            }
            return RedirectToAction("SessionList");
        }

        #endregion

        #region Class

        public ActionResult Class(string Pk_ClassId)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Add Class");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission","Home");
            }
            else
            {


            }
            #endregion
            Master model = new Master();
            model.Pk_ClassId = Pk_ClassId;
            if (Pk_ClassId != null)
            {
                DataSet ds = model.GettingClass();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.ClassName = ds.Tables[0].Rows[0]["ClassName"].ToString();
                    model.Pk_ClassId = ds.Tables[0].Rows[0]["Pk_ClassId"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveClass")]
        [ActionName("Class")]
        public ActionResult SavingClass(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingClass();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Class"] = "Class Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Class"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("Class");
        }

        public ActionResult ClassList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Class List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ClassId = r["PK_ClassID"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    list.Add(obj);
                }
                model.classLst = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateClass")]
        [ActionName("Class")]
        public ActionResult UpdateClass(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.Pk_ClassId = model.Pk_ClassId;
                DataSet ds = model.UpdatingClass();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["ClassList"] = "Class Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Class"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ClassList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("ClassList");
        }

        public ActionResult DeletingClass(string Pk_ClassId)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.Pk_ClassId = Pk_ClassId;
            DataSet ds = model.DeletingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["ClassList"] = "Class Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["ClassList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("ClassList");
        }
        #endregion

        #region Section

        public ActionResult Section(string PK_SectionId)
        {
            Master model = new Master();
            List<Master> list = new List<Master>();
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();


            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                    }
                    ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["Pk_ClassId"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }

            if (PK_SectionId != null)
            {
                model.PK_SectionId = PK_SectionId;
                DataSet ds1 = model.GettingSectionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    model.PK_SectionId = ds1.Tables[0].Rows[0]["PK_SectionID"].ToString();
                    model.ClassName = ds1.Tables[0].Rows[0]["ClassName"].ToString();
                    model.Pk_ClassId = ds1.Tables[0].Rows[0]["Fk_ClassID"].ToString();
                    model.SectionName = ds1.Tables[0].Rows[0]["SectionName"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveSection")]
        [ActionName("Section")]
        public ActionResult SavingSection(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();

                #region Bind Class
                int count1 = 0;
                List<SelectListItem> ddlClassName = new List<SelectListItem>();
                DataSet ds1 = model.GettingClass();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                        }
                        ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                        count1 = count1 + 1;
                    }
                    ViewBag.ddlClassName = ddlClassName;
                }
                #endregion

                DataSet ds = model.SavingSection();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Section"] = "Section Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Section"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Section"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Section");
        }

        public ActionResult SectionList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Section List");
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

            List<Master> list = new List<Master>();
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();


            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                    }
                    ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["Pk_ClassId"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }

            DataSet ds1 = model.GettingSectionList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds1.Tables[0];
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_SectionId = r["PK_SectionID"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.Pk_ClassId = r["Fk_ClassID"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    list.Add(obj);
                }
                model.sectionLst = list;
            }

            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateSection")]
        [ActionName("Section")]
        public ActionResult UpdatingSection(Master model)
        {
            try
            {

                #region Bind Class
                int count1 = 0;
                List<SelectListItem> ddlClassName = new List<SelectListItem>();
                DataSet ds1 = model.GettingClass();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                        }
                        ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                        count1 = count1 + 1;
                    }
                    ViewBag.ddlClassName = ddlClassName;
                }
                #endregion
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_SectionId = model.PK_SectionId;
                DataSet ds = model.UpdatingSection();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SectionList"] = "Section Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Section"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SectionList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SectionList");
        }

        public ActionResult DeleteSection(string PK_SectionId)
        {
            Master model = new Master();
            model.PK_SectionId = PK_SectionId;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingSection();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["SectionList"] = "Section Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["SectionList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("SectionList");
        }
        #endregion

        #region Subject Master
        public ActionResult SubjectMaster(string Pk_SubjectId)
        {
            Master model = new Master();
            model.Pk_SubjectId = Pk_SubjectId;
            if (Pk_SubjectId != null)
            {
                DataSet ds = model.gettingSubjectMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Pk_SubjectId = ds.Tables[0].Rows[0]["Pk_SubjectId"].ToString();
                    model.SubjectName = ds.Tables[0].Rows[0]["SubjectName"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "SaveSubjectMaster")]
        [ActionName("SubjectMaster")]
        public ActionResult SavingSubjectMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingSubject();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SubjectMaster"] = "Subject Master saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SubjectMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SubjectMaster"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SubjectMaster");
        }

        public ActionResult SubjectMasterList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Subject List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.gettingSubjectMaster();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_SubjectId = r["Pk_SubjectId"].ToString();
                    obj.SubjectName = r["SubjectName"].ToString();
                    list.Add(obj);
                }
                model.subjectLst = list;
            }
            return View(model);


        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateSubjectMaster")]
        [ActionName("SubjectMaster")]
        public ActionResult UpdateSubjectMaster(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.Pk_SubjectId = model.Pk_SubjectId;
                DataSet ds = model.UpdatingSubjectMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SubjectMasterList"] = "Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SubjectMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SubjectMasterList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SubjectMasterList");
        }

        public ActionResult DeletingSubjectMaster(string Pk_SubjectId)
        {
            Master model = new Master();
            model.Pk_SubjectId = Pk_SubjectId;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingSubjectMaster();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["SubjectMasterList"] = "Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["SubjectMasterList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("SubjectMasterList");
        }

        #endregion

        #region Branch

        public ActionResult Branch(string Pk_BranchID)
        {
            Master model = new Master();
            model.Pk_BranchID = Pk_BranchID;
            if (Pk_BranchID != null)
            {
                DataSet ds = model.GettingBranch();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    model.Pk_BranchID = ds.Tables[0].Rows[0]["Pk_BranchID"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveBranch")]
        [ActionName("Branch")]
        public ActionResult SaveBranch(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingBranch();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("Branch");
        }

        public ActionResult BranchList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Branch List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingBranch();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Pk_BranchID = r["Pk_BranchID"].ToString();
                    list.Add(obj);
                }
                model.BranchLst = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateBranch")]
        [ActionName("Branch")]
        public ActionResult UpdateBranch(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.Pk_BranchID = model.Pk_BranchID;
                DataSet ds = model.UpdatingBranch();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["BranchList"] = "Branch Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BranchList"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("BranchList");
        }

        public ActionResult DeleteBranch(string Pk_BranchID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.Pk_BranchID = Pk_BranchID;
            DataSet ds = model.DeletingBranch();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["BranchList"] = "Branch Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["BranchList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("BranchList");
        }
        #endregion

        #region Department

        public ActionResult Department(string PK_DepartmentID)
        {
            Master model = new Master();
            model.PK_DepartmentID = PK_DepartmentID;
            if (PK_DepartmentID != null)
            {
                DataSet ds = model.GettingDepartment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_DepartmentID = ds.Tables[0].Rows[0]["PK_DepartmentID"].ToString();
                    model.DepartmentName = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveDepartment")]
        [ActionName("Department")]
        public ActionResult SaveDepartment(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingDepartment();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Department"] = "Department Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Department"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Department"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Department");
        }

        public ActionResult DepartmentList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Department List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingDepartment();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DepartmentName = r["DepartmentName"].ToString();
                    obj.PK_DepartmentID = r["PK_DepartmentID"].ToString();
                    list.Add(obj);
                }
                model.DepartmentLst = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateDepartment")]
        [ActionName("Department")]
        public ActionResult UpdateDepartment(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_DepartmentID = model.PK_DepartmentID;
                DataSet ds = model.UpdatingDepartment();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["DepartmentList"] = "Department Successfully Updated";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Department"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DepartmentList"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("DepartmentList");
        }

        public ActionResult DeleteDepartment(string PK_DepartmentID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.PK_DepartmentID = PK_DepartmentID;
            DataSet ds = model.DeletingDepartment();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["DepartmentList"] = "Department Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["DepartmentList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("DepartmentList");
        }
        #endregion

        #region FineMaster

        public ActionResult FineMaster(string PK_FineID)
        {
            Master model = new Master();
            model.PK_FineID = PK_FineID;
            List<Master> list = new List<Master>();
            #region Bind Class

            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();


            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["Pk_ClassId"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion
            if (PK_FineID != null)
            {
                DataSet ds1 = model.GettingFineMasterList();
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    model.PK_FineID = ds1.Tables[0].Rows[0]["PK_FineID"].ToString();
                    model.Pk_ClassId = ds1.Tables[0].Rows[0]["Fk_ClassId"].ToString();
                    model.ClassName = ds1.Tables[0].Rows[0]["ClassName"].ToString();
                    model.IsDaily = ds1.Tables[0].Rows[0]["IsDaily"].ToString();
                    model.Amount = ds1.Tables[0].Rows[0]["Amount"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "SaveFineMaster")]
        [ActionName("FineMaster")]
        public ActionResult SaveFineMaster(Master model)
        {
            try
            {
                if (model.IsDaily == "on")
                {
                    model.IsDaily = "1";
                }
                else
                {
                    model.IsDaily = "0";

                }
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingFineMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["FineMaster"] = "Fine Master Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["FineMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FineMaster"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("FineMaster");
        }

        public ActionResult FineMasterList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Fine List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingFineMasterList();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ClassId = r["Fk_ClassId"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.IsDaily = r["IsDaily"].ToString();
                    obj.Daily = r["Daily"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PK_FineID = r["PK_FineID"].ToString();
                    list.Add(obj);
                }
                model.FineList = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateFineMaster")]
        [ActionName("FineMaster")]
        public ActionResult UpdateFineMaster(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_FineID = model.PK_FineID;
                model.IsDaily = model.ISDailyValue;

                #region Bind Class

                int count1 = 0;
                List<SelectListItem> ddlClassName = new List<SelectListItem>();


                DataSet ds1 = model.GettingClass();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClassName.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["Pk_ClassId"].ToString() });
                        count1 = count1 + 1;
                    }
                    ViewBag.ddlClassName = ddlClassName;
                }
                #endregion
                DataSet ds = model.UpdatingFineMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["FineMasterList"] = "Fine Master Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["FineMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FineMasterList"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("FineMasterList");
        }

        public ActionResult DeleteFineMaster(string PK_FineID)
        {
            Master model = new Master();
            model.PK_FineID = PK_FineID;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingFineMaster();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["FineMasterList"] = "Fine Master Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["FineMasterList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("FineMasterList");
        }
        #endregion

        #region Leave

        public ActionResult Leave(string PK_LeaveID)
        {
            Master model = new Master();
            model.PK_LeaveID = PK_LeaveID;
            if (PK_LeaveID != null)
            {
                DataSet ds = model.GettingLeaveList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_LeaveID = ds.Tables[0].Rows[0]["PK_LeaveID"].ToString();
                    model.LeaveCount = ds.Tables[0].Rows[0]["LeaveCount"].ToString();
                    model.LeaveName = ds.Tables[0].Rows[0]["LeaveName"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "SaveLeave")]
        [ActionName("Leave")]
        public ActionResult SaveLeave(Master model)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.SavingLeave();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Leave"] = "Leave Saved Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["Leave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("Leave");
        }

        public ActionResult LeaveList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Leave List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingLeaveList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LeaveCount = r["LeaveCount"].ToString();
                    obj.LeaveName = r["LeaveName"].ToString();
                    obj.PK_LeaveID = r["PK_LeaveID"].ToString();
                    list.Add(obj);
                }
                model.LeaveList = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateLeave")]
        [ActionName("Leave")]
        public ActionResult UpdateLeave(Master model)
        {
            model.UpdatedBy = Session["Pk_AdminId"].ToString();
            model.PK_LeaveID = model.PK_LeaveID;
            DataSet ds = model.UpdatingLeave();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["LeaveList"] = "Leave Updated Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["LeaveList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("LeaveList");
        }

        public ActionResult DeleteLeave(string PK_LeaveID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.PK_LeaveID = PK_LeaveID;
            DataSet ds = model.DeletingLeave();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["LeaveList"] = "Leave Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["LeaveList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("LeaveList");
        }
        #endregion

        #region AssignSubjecttoClass

        public ActionResult AssignSubjecttoClass(Master model)
        {

            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlSectionName = ddlSectionName;
            //int count1 = 0;
            //DataSet ds1 = model.GettingSectionList();
            //if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds1.Tables[0].Rows)
            //    {
            //        if (count1 == 0)
            //        {
            //            ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            //        }
            //        ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
            //        count1 = count1 + 1;
            //    }
            //    ViewBag.ddlSectionName = ddlSectionName;
            //}

            #endregion


            return View(model);
        }

        public ActionResult GetSectionByClass(string Pk_ClassId)
        {
            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            Master model = new Master();
            model.Pk_ClassId = Pk_ClassId;
            DataSet ds = model.GettingSectionList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                }
                model.ddlSectionName = ddlSectionName;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSave")]
        [ActionName("AssignSubjecttoClass")]
        public ActionResult SaveAssignSubject(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.PK_SectionId = model.PK_SectionId;
                model.Pk_ClassId = model.Pk_ClassId;
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                DataTable dtstudent = new DataTable();
                dtstudent.Columns.Add("SubjectName", typeof(string));
                dtstudent.Columns.Add("Pk_SubjectId", typeof(string));
                dtstudent.Columns.Add("Status", typeof(string));
                dtstudent.Columns.Add("Pk_AssignId", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["chkSelect_ " + i];
                    if (chk == "Checked")
                    {
                        model.Status = "1";
                    }
                    else
                    {
                        model.Status = "0";
                    }

                    model.Pk_SubjectId = Request["Pk_SubjectId_ " + i].ToString();
                    model.Pk_AssignId = Request["Pk_AssignId_ " + i].ToString();
                    DataSet ds = new DataSet();
                    model.SessionName = Session["SessionId"].ToString();
                    ds = model.SavingAssignSubject();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["AssignSubjecttoClass"] = "Subject Asigned Successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["AssignSubjecttoClass"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return View(model);
                        }
                    }

                }
                model.dsSubject = dtstudent;
            }
            catch (Exception ex)
            {
                TempData["AssignSubjecttoClass"] = ex.Message;
                return View(model);
            }


            return RedirectToAction("AssignSubjecttoClass");
        }

        [HttpPost]
        [OnAction(ButtonName = "GetAssignList")]
        [ActionName("AssignSubjecttoClass")]
        public ActionResult GetAssignSubject(string Pk_ClassId, string PK_SectionId)
        {

            List<Master> list = new List<Master>();
            Master model = new Master();

            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion
            model.Pk_ClassId = Pk_ClassId;

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            int count1 = 0;
            DataSet ds1 = model.GettingSectionList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                    }
                    ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                    count1 = count1 + 1;
                }
                ViewBag.ddlSectionName = ddlSectionName;
            }

            #endregion
            model.PK_SectionId = PK_SectionId;
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds2 = model.GettingAssignSubject();

            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_AssignId = r["Pk_AssignId"].ToString();
                    obj.Pk_ClassId = r["Fk_ClassId"].ToString();
                    obj.PK_SectionId = r["Fk_SectionId"].ToString();
                    obj.Pk_SubjectId = r["Fk_SubjectID"].ToString();
                    obj.SubjectName = r["SubjectName"].ToString();
                    obj.Status = r["Status"].ToString();
                    list.Add(obj);
                }
                model.subjectLst = list;
            }

            return View(model);
        }

        #endregion

        #region AssignSubjecttoTeacher

        public ActionResult AssignSubjecttoTeacher(Master model)
        {

            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlSectionName = ddlSectionName;
            #endregion

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


        [HttpPost]
        [OnAction(ButtonName = "GetSubjectNameByTeacher")]
        [ActionName("AssignSubjecttoTeacher")]
        public ActionResult GetSubjectNameByTeacher(string PK_TeacherID, string Pk_ClassId, string PK_SectionId)
        {

            List<Master> list = new List<Master>();
            Master model = new Master();
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
            model.PK_TeacherID = PK_TeacherID;
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion
            model.Pk_ClassId = Pk_ClassId;


            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            int count1 = 0;
            DataSet ds1 = model.GettingSectionList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                    }
                    ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                    count1 = count1 + 1;
                }
                ViewBag.ddlSectionName = ddlSectionName;
            }

            #endregion
            model.PK_SectionId = PK_SectionId;
            model.SessionName= Session["SessionId"].ToString();
            DataSet ds3 = model.GettingSubjectNameByTeacher();

            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_SubjecttoTeacherId = r["Pk_SubjecttoTeacherId"].ToString();
                    obj.Pk_ClassId = r["Fk_ClassId"].ToString();
                    obj.PK_SectionId = r["Fk_SectionId"].ToString();
                    obj.Pk_SubjectId = r["Fk_SubjectID"].ToString();
                    obj.SubjectName = r["SubjectName"].ToString();
                    obj.Status = r["Status"].ToString();
                    list.Add(obj);
                }
                model.subjectLst = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSave")]
        [ActionName("AssignSubjecttoTeacher")]
        public ActionResult SaveAssignTeacher(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.PK_SectionId = model.PK_SectionId;
                model.Pk_ClassId = model.Pk_ClassId;
                model.PK_TeacherID = model.PK_TeacherID;
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                DataTable dtstudent = new DataTable();
                dtstudent.Columns.Add("SubjectName", typeof(string));
                dtstudent.Columns.Add("Pk_SubjectId", typeof(string));
                dtstudent.Columns.Add("Status", typeof(string));
                dtstudent.Columns.Add("Pk_SubjecttoTeacherId", typeof(string));
                dtstudent.Columns.Add("PK_TeacherID", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["chkSelect_ " + i];
                    if (chk == "Checked")
                    {
                        model.Status = "1";
                    }
                    else
                    {
                        model.Status = "0";
                    }
                    model.Pk_SubjectId = Request["Pk_SubjectId_ " + i].ToString();
                    model.Pk_SubjecttoTeacherId = Request["Pk_SubjecttoTeacherId_ " + i].ToString();
                    DataSet ds = new DataSet();
                     model.SessionName =  Session["SessionId"].ToString();
                    ds = model.SavingAssignTeacher();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["AssignSubjecttoTeacher"] = "Subject Asigned Successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["AssignSubjecttoTeacher"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return View(model);
                        }
                    }
                }
                model.dsSubject = dtstudent;
            }
            catch (Exception ex)
            {
                TempData["AssignSubjecttoTeacher"] = ex.Message;
                return View(model);
            }


            return RedirectToAction("AssignSubjecttoTeacher");
        }
        #endregion

        #region Religion

        public ActionResult Religion(string PK_ReligionID)
        {
            Master model = new Master();
            if (PK_ReligionID != null)
            {
                model.PK_ReligionID = PK_ReligionID;
                DataSet ds = model.GettingReligionList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_ReligionID = ds.Tables[0].Rows[0]["PK_ReligionID"].ToString();
                    model.ReligionName = ds.Tables[0].Rows[0]["ReligionName"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveReligion")]
        [ActionName("Religion")]
        public ActionResult SaveReligion(Master model)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.SavingReligion();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Religion"] = "Religion Saved Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["Religion"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("Religion");
        }

        public ActionResult ReligionList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Religion List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingReligionList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_ReligionID = r["PK_ReligionID"].ToString();
                    obj.ReligionName = r["ReligionName"].ToString();
                    list.Add(obj);
                }
                model.Religionlist = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateReligion")]
        [ActionName("Religion")]
        public ActionResult UpdateReligion(Master model)
        {
            model.UpdatedBy = Session["Pk_AdminId"].ToString();
            model.PK_ReligionID = model.PK_ReligionID;
            DataSet ds = model.UpdatingReligion();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["ReligionList"] = "Religion Updated Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["ReligionList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("ReligionList");
        }

        public ActionResult DeleteReligion(string PK_ReligionID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.PK_ReligionID = PK_ReligionID;
            DataSet ds = model.DeletingReligion();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["ReligionList"] = "Religion Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["ReligionList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("ReligionList");
        }
        #endregion

        #region NoticeMaster

        public ActionResult NoticeMaster(string PK_NoticeId)
        {
            Master model = new Master();
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion
            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            if (PK_NoticeId != null)
            {

                model.PK_NoticeId = PK_NoticeId;
                DataSet ds2 = model.GettingNoticeList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    model.PK_NoticeId = ds2.Tables[0].Rows[0]["PK_NoticeId"].ToString();
                    model.NoticeName = ds2.Tables[0].Rows[0]["NoticeName"].ToString();
                    model.Pk_ClassId = ds2.Tables[0].Rows[0]["FK_ClassId"].ToString();

                    #region Section


                    int count1 = 0;
                    DataSet ds1 = model.GettingSectionList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count1 == 0)
                            {
                                ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                            }
                            ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                            count1 = count1 + 1;
                        }

                    }
                    ViewBag.ddlSectionName = ddlSectionName;
                    #endregion
                    model.PK_SectionId = ds2.Tables[0].Rows[0]["FK_SectionId"].ToString();
                }
            }
            else
            {


                ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                ViewBag.ddlSectionName = ddlSectionName;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveNotice")]
        [ActionName("NoticeMaster")]
        public ActionResult SaveNotice(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.NoticeBy = "Admin";
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.SavingNotice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["NoticeMaster"] = "Notice Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["NoticeMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["NoticeMaster"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("NoticeMaster");
        }


        public ActionResult NoticeList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Notice List");
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

            List<Master> list = new List<Master>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.GettingNoticeList();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"]=ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_NoticeId = r["PK_NoticeId"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Pk_ClassId = r["FK_ClassId"].ToString();
                    obj.PK_SectionId = r["FK_SectionId"].ToString();
                    obj.NoticeName = r["NoticeName"].ToString();
                    list.Add(obj);
                }
                model.Noticelist = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateNotice")]
        [ActionName("NoticeMaster")]
        public ActionResult UpdateNotice(Master model)
        {
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            int count1 = 0;
            DataSet ds1 = model.GettingSectionList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                    }
                    ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                    count1 = count1 + 1;
                }
                ViewBag.ddlSectionName = ddlSectionName;
            }

            #endregion
            model.UpdatedBy = Session["Pk_AdminId"].ToString();
            model.PK_NoticeId = model.PK_NoticeId;
            DataSet ds2 = model.UpdatingNotice();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["NoticeList"] = "Notice Updated Successfully";
                }
                else if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["NoticeMaster"] = ds2.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("NoticeList");
        }

        public ActionResult DeleteNotice(string PK_NoticeId)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.PK_NoticeId = PK_NoticeId;
            DataSet ds = model.DeletingNotice();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["NoticeList"] = "Notice Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["NoticeList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("NoticeList");
        }
        #endregion

        #region VehicleMaster

        public ActionResult VehicleMaster(string PK_VehicleMasterID)
        {
            Master model = new Master();
            #region Bind VehicleType

            int count = 0;
            List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
            DataSet ds = model.GettingVehicleType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
                    }
                    ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                    count = count + 1;

                }
                ViewBag.ddlVehicleType = ddlVehicleType;
            }

            #endregion

            if (PK_VehicleMasterID != null)
            {
                model.PK_VehicleMasterID = PK_VehicleMasterID;
                DataSet ds1 = model.GettingVehicleList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    model.PK_VehicleMasterID = ds1.Tables[0].Rows[0]["PK_VehicleMasterID"].ToString();
                    model.PK_VehicleTypeID = ds1.Tables[0].Rows[0]["FK_VehicleTypeID"].ToString();
                    model.VehicleType = ds1.Tables[0].Rows[0]["VehicleType"].ToString();
                    model.VehicleNo = ds1.Tables[0].Rows[0]["VehicleNo"].ToString();
                    model.DriverContactNo = ds1.Tables[0].Rows[0]["DriverContactNo"].ToString();
                    model.DriverName = ds1.Tables[0].Rows[0]["DriverName"].ToString();
                    model.Address = ds1.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds1.Tables[0].Rows[0]["PinCode"].ToString();
                    model.City = ds1.Tables[0].Rows[0]["City"].ToString();
                    model.State = ds1.Tables[0].Rows[0]["State"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveVehicle")]
        [ActionName("VehicleMaster")]
        public ActionResult SaveVehicle(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.PK_VehicleTypeID = model.PK_VehicleTypeID;
                #region Bind VehicleType

                int count1 = 0;
                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
                DataSet ds1 = model.GettingVehicleType();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
                        }
                        ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                        count1 = count1 + 1;

                    }
                    ViewBag.ddlVehicleType = ddlVehicleType;
                }

                #endregion

                DataSet ds = model.SavingVehicle();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["VehicleMaster"] = "Vehicle Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["VehicleMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["VehicleMaster"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("VehicleMaster");
        }

        public ActionResult VehicleList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Vehicle List");
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

            List<Master> list = new List<Master>();
            DataSet ds = model.GettingVehicleList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_VehicleMasterID = r["PK_VehicleMasterID"].ToString();
                    obj.PK_VehicleTypeID = r["FK_VehicleTypeID"].ToString();
                    obj.VehicleType = r["VehicleType"].ToString();
                    obj.VehicleNo = r["VehicleNo"].ToString();
                    obj.DriverContactNo = r["DriverContactNo"].ToString();
                    obj.DriverName = r["DriverName"].ToString();
                    list.Add(obj);
                }
                model.VehicleList = list;
            }
            else
            {
                Session["dt"] = ds.Tables[0];
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateVehicle")]
        [ActionName("VehicleMaster")]
        public ActionResult UpdateVehicle(Master model)
        {
            try
            {
                #region Bind VehicleType

                int count = 0;
                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
                DataSet ds = model.GettingVehicleType();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
                        }
                        ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                        count = count + 1;

                    }
                    ViewBag.ddlVehicleType = ddlVehicleType;
                }

                #endregion
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_VehicleMasterID = model.PK_VehicleMasterID;
                DataSet ds1 = model.UpdatingVehicle();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["VehicleList"] = "Vehicle Updated Successfully";
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["VehicleMaster"] = ds1.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["VehicleList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("VehicleList");
        }

        public ActionResult DeleteVehicle(string PK_VehicleMasterID)
        {
            Master model = new Master();
            model.PK_VehicleMasterID = PK_VehicleMasterID;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingVehicle();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["VehicleList"] = "Vehicle Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["VehicleList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("VehicleList");
        }

        public ActionResult GetStateCity(string PinCode)
        {
            Master obj = new Master();
            obj.PinCode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["StateName"].ToString();
                obj.City = ds.Tables[0].Rows[0]["CityName"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Alot Vehicle

        public ActionResult AlotVehicle(Master model)
        {

            #region Bind Route


            List<SelectListItem> ddlRoute = new List<SelectListItem>();
            DataSet ds6 = model.GettingRoute();
            ddlRoute.Add(new SelectListItem { Value = "0", Text = "Select Route" });
            if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds6.Tables[0].Rows)
                {

                    ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["PK_RouteId"].ToString() });


                }

            }
            ViewBag.ddlRoute = ddlRoute;
            #endregion

            #region Bind VehicleType

            int count = 0;
            List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
            ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
            DataSet ds = model.GettingVehicleType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                    count = count + 1;

                }

            }
            ViewBag.ddlVehicleType = ddlVehicleType;
            #endregion


            #region Bind VehicleNo
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
            ddlVehicleNo.Add(new SelectListItem { Text = "Select Vehicle", Value = "0" });
            ViewBag.ddlVehicleNo = ddlVehicleNo;

            #endregion

            #region Bind Type
            List<SelectListItem> ddltype = Common.BindType();
            ViewBag.ddltype = ddltype;
            #endregion Bind Type


            #region Bind Class
            int count4 = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds4 = model.GettingClass();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count4 = count4 + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlSectionName = ddlSectionName;

            #endregion

            #region Bind Area
            List<SelectListItem> ddlArea = new List<SelectListItem>();
            ddlArea.Add(new SelectListItem { Text = "Select Area", Value = "0" });
            ViewBag.ddlArea = ddlArea;
            #endregion

            return View(model);
        }

        public ActionResult GetVehicleByType(string PK_VehicleTypeID)
        {
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
            Master model = new Master();
            model.PK_VehicleTypeID = PK_VehicleTypeID;
            DataSet ds = model.GettingVehicleList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ddlVehicleNo.Add(new SelectListItem { Text = r["VehicleNo"].ToString(), Value = r["PK_VehicleMasterID"].ToString() });
                }
                model.ddlVehicleNo = ddlVehicleNo;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArea(string PK_RouteId)
        {
            List<SelectListItem> ddlArea = new List<SelectListItem>();

            Master model = new Master();
            model.PK_RouteId = PK_RouteId;
            DataSet ds = model.GettingAreaByRoute();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ddlArea.Add(new SelectListItem { Text = r["AreaName"].ToString(), Value = r["PK_AreaMasterID"].ToString() });
                }
                model.ddlArea = ddlArea;

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehicleDetails(string PK_RouteId, string PK_AreaMasterID)
        {
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();

            Master model = new Master();
            model.PK_AreaMasterID = PK_AreaMasterID;
            model.PK_RouteId = PK_RouteId;
            DataSet ds = model.GettingAreaByRoute();
            if (ds != null && ds.Tables.Count > 0)
            {
                model.VehicleNo = ds.Tables[0].Rows[0]["VehicleNo"].ToString();
                model.DriverContactNo = ds.Tables[0].Rows[0]["DriverContactNo"].ToString();
                model.DriverName = ds.Tables[0].Rows[0]["DriverName"].ToString();

                model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDriverNameandContact(string PK_VehicleMasterID)
        {
            Master model = new Master();
            model.PK_VehicleMasterID = PK_VehicleMasterID;
            if (PK_VehicleMasterID != null)
            {
                DataSet ds = model.GettingVehicleList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.DriverContactNo = ds.Tables[0].Rows[0]["DriverContactNo"].ToString();
                    model.DriverName = ds.Tables[0].Rows[0]["DriverName"].ToString();
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentList(string Pk_ClassId, string PK_SectionId)
        {
            List<SelectListItem> ddlStudent = new List<SelectListItem>();
            Master model = new Master();
            model.Pk_ClassId = Pk_ClassId;
            model.PK_SectionId = PK_SectionId;
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.GetStudentList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ddlStudent.Add(new SelectListItem { Text = r["StudentName"].ToString(), Value = r["Pk_StudentID"].ToString() });
                }
                model.ddlStudent = ddlStudent;
            }

            #region Bind VehicleType

            int count = 0;
            List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
            DataSet ds4 = model.GettingVehicleType();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
                    }
                    ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                    count = count + 1;

                }
                ViewBag.ddlVehicleType = ddlVehicleType;
            }

            #endregion


            #region Bind VehicleNo
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
            ddlVehicleNo.Add(new SelectListItem { Text = "Select Vehicle", Value = "0" });
            ViewBag.ddlVehicleNo = ddlVehicleNo;

            #endregion
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveAlotVehicle")]
        [ActionName("AlotVehicle")]
        public ActionResult SaveAlotVehicle(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();

                string hdrows = Request["hdRows"].ToString();
                string chkselect = "";
                string PK_Teacher = "";
                string Pk_Class = "";
                string PK_Section = "";
                string Pk_Student = "";
                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    try
                    {
                        chkselect = Request["chkSelect_ " + i].ToString();

                        PK_Teacher = Request["PK_TeacherID_ " + i].ToString();
                        Pk_Class = Request["Pk_ClassId_ " + i].ToString();
                        PK_Section = Request["PK_SectionId_ " + i].ToString();
                        Pk_Student = Request["Pk_StudentID_ " + i].ToString();

                        model.PK_TeacherID = PK_Teacher;
                        model.Pk_StudentID = Pk_Student;
                        model.DriverName = model.DriverName;
                        model.DriverContactNo = model.DriverContactNo;
                        model.PK_VehicleMasterID = model.PK_VehicleMasterID;
                        model.VehicleNo = model.VehicleNo;
                        model.Amount = model.Amount;
                        model.PK_RouteId = model.PK_RouteId;
                        model.PK_AreaMasterID = model.PK_AreaMasterID;
                        model.SessionName = Session["SessionId"].ToString();
                        DataSet ds = model.SavingAlotVehicle();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                TempData["AlotVehicle"] = "Vehicle Alotted Saved Successfully";
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                            {
                                TempData["AlotVehicle"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                return View(model);
                            }
                        }
                    }
                    catch { chkselect = "0"; }
                }
            }
            catch (Exception ex)
            {
                TempData["AlotVehicle"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("AlotVehicle");
        }

        [HttpPost]
        [ActionName("AlotVehicle")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetDetails(Master model)
        {

            List<Master> lst = new List<Master>();
            if (model.PK_SectionId == "0") { model.PK_SectionId = null; }
            if (model.PK_TeacherID == "0") { model.PK_TeacherID = null; }
            if (model.PK_VehicleMasterID == "0") { model.PK_VehicleMasterID = null; }
            if (model.PK_VehicleTypeID == "0") { model.PK_VehicleTypeID = null; }
            if (model.Pk_StudentID == "0") { model.Pk_StudentID = null; }
            if (model.Pk_ClassId == "0") { model.Pk_ClassId = null; }
            if (model.PK_AreaMasterID == "0") { model.PK_AreaMasterID = null; }
            if (model.PK_RouteId == "0") { model.PK_RouteId = null; }
            model.PK_TeacherID = model.PK_TeacherID;
            model.Pk_ClassId = model.Pk_ClassId;
            model.PK_SectionId = model.PK_SectionId;


            Master obj = new Master();
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds1 = obj.GettingClass();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion


            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            int count1 = 0;
            obj.Pk_ClassId = model.Pk_ClassId;
            DataSet ds2 = obj.GettingSectionList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                    }
                    ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                    count1 = count1 + 1;
                }
                ViewBag.ddlSectionName = ddlSectionName;
            }

            #endregion
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.GettingVehicleDetails();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master ob = new Master();

                    if (model.Type == "Student")
                    {
                        ob.StudentName = dr["StudentName"].ToString();
                        ob.Pk_ClassId = dr["Fk_ClassID"].ToString();
                        ob.PK_SectionId = dr["Fk_SectionID"].ToString();
                        ob.Pk_StudentID = dr["Pk_StudentID"].ToString();
                        ob.Type = dr["Type"].ToString();

                    }
                    else if (model.Type == "Staff")
                    {
                        ob.Name = dr["Name"].ToString();
                        ob.PK_TeacherID = dr["PK_TeacherID"].ToString();
                        ob.Type = dr["Type"].ToString();
                    }
                    lst.Add(ob);
                }
                model.StudentList = lst;
            }

            #region Bind VehicleType

            int count2 = 0;
            List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
            ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
            DataSet ds4 = model.GettingVehicleType();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {

                    ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                    count2 = count2 + 1;

                }

            }
            ViewBag.ddlVehicleType = ddlVehicleType;
            #endregion


            #region Bind VehicleNo
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
            ddlVehicleNo.Add(new SelectListItem { Text = "Select Vehicle", Value = "0" });
            ViewBag.ddlVehicleNo = ddlVehicleNo;

            #endregion

            #region Bind Type
            List<SelectListItem> ddltype = Common.BindType();
            ViewBag.ddltype = ddltype;
            #endregion Bind Type

            #region Bind Route

            int count6 = 0;
            List<SelectListItem> ddlRoute = new List<SelectListItem>();
            ddlRoute.Add(new SelectListItem { Value = "0", Text = "Select Route" });
            DataSet ds6 = model.GettingRoute();
            if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds6.Tables[0].Rows)
                {

                    ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["PK_RouteId"].ToString() });
                    count6 = count6 + 1;

                }

            }
            ViewBag.ddlRoute = ddlRoute;
            #endregion

            #region Bind Area
            List<SelectListItem> ddlArea = new List<SelectListItem>();
            ddlArea.Add(new SelectListItem { Text = "Select Area", Value = "0" });
            ViewBag.ddlArea = ddlArea;
            #endregion

            return View(model);
        }

        public ActionResult AllotVehicleList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Allot Vehicle List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.AllotVehicleList();

            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_AllotId = r["Pk_AlotVehicleID"].ToString();
                    obj.RouteNo = r["RouteNo"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.AreaName = r["AreaName"].ToString();
                    obj.VehicleNo = r["VehicleNo"].ToString();
                    obj.DriverName = r["DriverName"].ToString();
                    obj.DriverContactNo = r["DriverContactNo"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.Name = r["Name"].ToString();

                    list.Add(obj);
                }
                model.Arealist = list;
            }


            return View(model);
        }


        public ActionResult DeleteAllotVehicle(string PK_AllotId)
        {
            Master model = new Master();
            model.PK_AllotId = PK_AllotId;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeleteAllotVehicle();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["DeleteAllotVehicle"] = "Vehicle Allotment Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["DeleteAllotVehicle"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("AllotVehicleList");
        }
        #endregion

        #region AssignClassTeacher

        public ActionResult AssignClassTeacher(Master model)
        {

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
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlSectionName = ddlSectionName;
            #endregion

            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveAssignClassTeacher")]
        [ActionName("AssignClassTeacher")]
        public ActionResult SaveAssignClassTeacher(Master model)
        {
            try
            {

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.SavingAssignClassTeacher();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["AssignClassTeacher"] = "Class Assigned to Teacher Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["AssignClassTeacher"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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

                        #region Bind Class
                        int count = 0;
                        List<SelectListItem> ddlClassName = new List<SelectListItem>();
                        DataSet ds1 = model.GettingClass();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds1.Tables[0].Rows)
                            {
                                if (count == 0)
                                {
                                    ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                                }
                                ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                                count = count + 1;
                            }
                            ViewBag.ddlClassName = ddlClassName;
                        }
                        #endregion
                        #region Section 
                        List<SelectListItem> ddlSectionName = new List<SelectListItem>();
                        int count1 = 0;
                        DataSet ds3 = model.GettingSectionList();
                        if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds3.Tables[0].Rows)
                            {
                                if (count1 == 0)
                                {
                                    ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                                }
                                ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                                count1 = count1 + 1;
                            }
                            ViewBag.ddlSectionName = ddlSectionName;
                        }
                        #endregion
                        return RedirectToAction("AssignClassTeacher");
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["AssignClassTeacher"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("AssignClassTeacher");
        }

        public ActionResult AssignClassTeacherList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Assign Class Teacher List");
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
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion

            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlSectionName = ddlSectionName;
            #endregion


            List<Master> list = new List<Master>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds4 = model.GettingAssignClassTeacherList();
            if (ds4 != null && ds4.Tables.Count > 0)
            {
                Session["dt"] = ds4.Tables[0];
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_AssignClassTeacherId = r["Pk_AssignClassTeacherId"].ToString();
                    obj.PK_TeacherID = r["FK_TeacherID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Pk_ClassId = r["Fk_ClassId"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.PK_SectionId = r["Fk_SectionId"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    list.Add(obj);
                }
                model.assignClassTeacherList = list;
            }
            return View(model);
        }

        public ActionResult DeleteAssignClassTeacher(string Pk_AssignClassTeacherId)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.Pk_AssignClassTeacherId = Pk_AssignClassTeacherId;
            DataSet ds = model.DeletingAssignClassTeacher();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["AssignClassTeacherList"] = "Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["AssignClassTeacherList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("AssignClassTeacherList");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnsearch")]
        [ActionName("AssignClassTeacherList")]
        public ActionResult SearchAssignClassTeacher(Master model)
        {

            if (model.Pk_ClassId == "0") { model.Pk_ClassId = null; }
            if (model.PK_TeacherID == "0") { model.PK_TeacherID = null; }
            if (model.PK_SectionId == "0") { model.PK_SectionId = null; }

            model.PK_TeacherID = model.PK_TeacherID;
            model.Pk_ClassId = model.Pk_ClassId;
            model.PK_SectionId = model.PK_SectionId;



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
            #region Bind Class
            int count = 0;
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet ds = model.GettingClass();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Value = "0", Text = "Select Class" });
                    }
                    ddlClassName.Add(new SelectListItem { Value = r["Pk_ClassId"].ToString(), Text = r["ClassName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlClassName = ddlClassName;
            }
            #endregion
            #region Set Permission
            Permissions objPermission = new Permissions("Assign Class Teacher List");
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
            #region Section

            List<SelectListItem> ddlSectionName = new List<SelectListItem>();
            int count1 = 0;
            model.Pk_ClassId = model.Pk_ClassId;
            DataSet ds3 = model.GettingSectionList();
            if (ds3 != null && ds3.Tables.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSectionName.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                    }
                    ddlSectionName.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionId"].ToString() });
                    count1 = count1 + 1;
                }
                ViewBag.ddlSectionName = ddlSectionName;
            }
            #endregion

            List<Master> list = new List<Master>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds4 = model.GettingAssignClassTeacherList();
            if (ds4 != null && ds4.Tables.Count > 0)
            {
                Session["dt"] = ds4.Tables[0];
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    Master obj1 = new Master();
                    obj1.Pk_AssignClassTeacherId = r["Pk_AssignClassTeacherId"].ToString();
                    obj1.PK_TeacherID = r["FK_TeacherID"].ToString();
                    obj1.Name = r["Name"].ToString();
                    obj1.Pk_ClassId = r["Fk_ClassId"].ToString();
                    obj1.ClassName = r["ClassName"].ToString();
                    obj1.PK_SectionId = r["Fk_SectionId"].ToString();
                    obj1.SectionName = r["SectionName"].ToString();
                    list.Add(obj1);
                }
                model.assignClassTeacherList = list;

            }


            return View(model);
        }
        #endregion

        #region VehicleType

        public ActionResult VehicleType(string PK_VehicleTypeID)
        {
            Master model = new Master();
            model.PK_VehicleTypeID = PK_VehicleTypeID;
            if (PK_VehicleTypeID != null)
            {
                DataSet ds = model.GettingVehicleType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.VehicleType = ds.Tables[0].Rows[0]["VehicleType"].ToString();
                    model.PK_VehicleTypeID = ds.Tables[0].Rows[0]["PK_VehicleTypeID"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveVehicleType")]
        [ActionName("VehicleType")]
        public ActionResult SaveVehicleType(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingVehicleType();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["VehicleType"] = "Vehicle Type Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["VehicleType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["VehicleType"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("VehicleType");
        }

        public ActionResult VehicleTypeList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Vehicle Type List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingVehicleType();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.VehicleType = r["VehicleType"].ToString();
                    obj.PK_VehicleTypeID = r["PK_VehicleTypeID"].ToString();
                    list.Add(obj);
                }
                model.VehicleTypelist = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateVehicleType")]
        [ActionName("VehicleType")]
        public ActionResult UpdateVehicleType(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_VehicleTypeID = model.PK_VehicleTypeID;
                DataSet ds = model.UpdatingVehicleType();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["VehicleTypeList"] = "Vehicle Type Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["VehicleType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return RedirectToAction("VehicleType");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["VehicleTypeList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("VehicleTypeList");
        }

        public ActionResult DeleteVehicleType(string PK_VehicleTypeID)
        {
            Master model = new Master();
            model.PK_VehicleTypeID = PK_VehicleTypeID;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingVehicleType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["VehicleTypeList"] = "Vehicle Type Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["VehicleTypeList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("VehicleTypeList");
        }
        #endregion

        #region AreaMaster

        public ActionResult AreaMaster(string PK_AreaMasterID)
        {
            Master model = new Master();
            model.PK_AreaMasterID = PK_AreaMasterID;
            if (PK_AreaMasterID != null)
            {
                DataSet ds = model.GettingAreaList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_AreaMasterID = ds.Tables[0].Rows[0]["PK_AreaMasterID"].ToString();
                    model.AreaName = ds.Tables[0].Rows[0]["AreaName"].ToString();
                    model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveAreaMaster")]
        [ActionName("AreaMaster")]
        public ActionResult SaveAreaMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingAreaMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["AreaMaster"] = "Area   Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["AreaMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AreaMaster"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("AreaMaster");
        }

        public ActionResult AreaMasterList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Area Master List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingAreaList();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.AreaName = r["AreaName"].ToString();
                    obj.PK_AreaMasterID = r["PK_AreaMasterID"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    list.Add(obj);
                }
                model.Arealist = list;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "UpdateAreaMaster")]
        [ActionName("AreaMaster")]
        public ActionResult UpdateAreaMaster(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.PK_AreaMasterID = model.PK_AreaMasterID;
                DataSet ds = model.UpdatingAreaMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["AreaMasterList"] = "Area   Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["AreaMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return RedirectToAction("AreaMaster");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AreaMasterList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("AreaMasterList");
        }

        public ActionResult DeleteAreaMaster(string PK_AreaMasterID)
        {
            Master model = new Master();
            model.PK_AreaMasterID = PK_AreaMasterID;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeletingAreaMaster();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["AreaMasterList"] = "Area Master Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["AreaMasterList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("AreaMasterList");
        }
        #endregion

        #region Route

        public ActionResult Route()
        {
            Master model = new Master();

            List<Master> list = new List<Master>();
            DataSet ds2 = model.GettingAreaList();
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.AreaName = r["AreaName"].ToString();
                    obj.PK_AreaMasterID = r["PK_AreaMasterID"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    list.Add(obj);
                }
                model.Arealist = list;
            }

            #region Bind VehicleType

            int count = 0;
            List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
            DataSet ds = model.GettingVehicleType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
                    }
                    ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                    count = count + 1;

                }
                ViewBag.ddlVehicleType = ddlVehicleType;
            }

            #endregion


            #region Bind VehicleNo
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
            ddlVehicleNo.Add(new SelectListItem { Text = "Select Vehicle", Value = "0" });
            ViewBag.ddlVehicleNo = ddlVehicleNo;

            #endregion

            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "SaveRoute")]
        [ActionName("Route")]
        public ActionResult SaveRoute(Master model)
        {


            #region Bind VehicleType

            int count = 0;
            List<SelectListItem> ddlVehicleType = new List<SelectListItem>();
            DataSet ds2 = model.GettingVehicleType();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVehicleType.Add(new SelectListItem { Value = "0", Text = "Select Vehicle Type" });
                    }
                    ddlVehicleType.Add(new SelectListItem { Text = r["VehicleType"].ToString(), Value = r["PK_VehicleTypeID"].ToString() });
                    count = count + 1;

                }
                ViewBag.ddlVehicleType = ddlVehicleType;
            }

            #endregion


            #region Bind VehicleNo
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();
            ddlVehicleNo.Add(new SelectListItem { Text = "Select Vehicle", Value = "0" });
            ViewBag.ddlVehicleNo = ddlVehicleNo;

            #endregion

            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                string hdrows = Request["hdRows"].ToString();
                string chkselect = "";

                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    try
                    {

                        if (Request["chkSelect_ " + i].ToString() == "on")
                        {
                            model.DropTime = Request["DropTime_ " + i].ToString();
                            model.PickupTime = Request["PickupTime_ " + i].ToString();
                            model.PK_AreaMasterID = Request["PK_AreaMasterID_ " + i].ToString();

                            DataSet ds = model.SavingRoute();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    TempData["Route"] = "Route Saved Successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    TempData["Route"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    return RedirectToAction("Route");
                                }
                            }
                        }

                    }
                    catch { chkselect = "0"; }
                }
            }
            catch (Exception ex)
            {
                TempData["Route"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("Route");
        }

        public ActionResult RouteList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Route List");
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
            List<Master> list = new List<Master>();
            DataSet ds = model.GettingRoute();
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RouteId = r["PK_RouteId"].ToString();
                    obj.RouteNo = r["RouteNo"].ToString();
                    obj.VehicleType = r["VehicleType"].ToString();
                    obj.VehicleNo = r["VehicleNo"].ToString();
                    obj.DriverName = r["DriverName"].ToString();
                    obj.DriverContactNo = r["DriverContactNo"].ToString();
                    list.Add(obj);
                }
                model.Arealist = list;
            }

            return View(model);
        }
        public ActionResult DeleteRouteList(string PK_RouteId)
        {
            Master model = new Master();
            model.PK_RouteId = PK_RouteId;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeleteRouteList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["RouteList"] = "Route Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["RouteList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("RouteList");
        }
        public ActionResult RouteViewDetails(string PK_RouteId)
        {
            Master model = new Master();
            List<Master> list = new List<Master>();
            model.PK_RouteId = PK_RouteId;


            DataSet ds = model.RouteViewDetails();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.RouteNo = r["RouteNo"].ToString();
                    obj.VehicleType = r["VehicleType"].ToString();
                    obj.AreaName = r["AreaName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PickupTime = r["PickupTime"].ToString();
                    obj.DropTime = r["DropTime"].ToString();
                    list.Add(obj);
                }
                model.Arealist = list;
            }

            return View(model);


        }
        #endregion

        #region Syllabus
        public ActionResult Syllabus(Master model)
        {
            #region ddlhelclass
            try
            {
               
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = model.GetClass();
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
            return View();
        }
        [HttpPost]
        [ActionName("Syllabus")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSyllabus(IEnumerable<HttpPostedFileBase> Syllabus, Master obj)
        {
             
            string FormName = "";
            string Controller = "";
            
            try
            {
                {
                    foreach (var file in Syllabus)
                    {
                        if (file != null && file.ContentLength > 0)
                        { 
                                obj.Syllabus = "/Syllabus/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                                file.SaveAs(Path.Combine(Server.MapPath(obj.Syllabus)));
                            
                        }
                       
                    }
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.SaveSyllabus();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    { 
                        TempData["Syllabus"] = "Syllabus saved successfully";
                        FormName = "Syllabus";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Syllabus"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Syllabus";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Syllabus"] = ex.Message;
                FormName = "Syllabus";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult SyllabusList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Syllabus List");
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
            List<Master> list = new List<Master>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.SyllabusList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SyllabusID = r["PK_SyllabusId"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Syllabus = r["Syllabus"].ToString();
                    list.Add(obj);
                }
                model.classLst = list;
            }
            return View(model);
        }
        public ActionResult DeletingSyllabus(string SyllabusID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.SyllabusID = SyllabusID;
            DataSet ds = model.DeletingSyllabus();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["SyllabusList"] = "Syllabus Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["SyllabusList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("SyllabusList");
        }
        #endregion

        #region TimeTable

        public ActionResult TimeTable(Master model)
        {
            #region ddlhelclass
            try
            {

                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = model.GetClass();
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
            return View();
        }
        [HttpPost]
        [ActionName("TimeTable")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveTimeTable(IEnumerable<HttpPostedFileBase> TimeTable, Master obj)
        {

            string FormName = "";
            string Controller = "";

            try
            {
                {
                    foreach (var file in TimeTable)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            obj.TimeTable = "/TimeTable/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.TimeTable)));

                        }

                    }
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.SaveTimeTable();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["TimeTable"] = "TimeTable saved successfully";
                        FormName = "TimeTable";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["TimeTable"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "TimeTable";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["TimeTable"] = ex.Message;
                FormName = "TimeTable";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult TimeTableList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("TimeTable List");
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
            List<Master> list = new List<Master>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.TimeTableList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.TimeTableID = r["PK_TimeTableId"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.TimeTable = r["TimeTable"].ToString();
                    list.Add(obj);
                }
                model.classLst = list;
            }
            return View(model);
        }
        public ActionResult DeletingTimeTable(string TimeTableID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.TimeTableID = TimeTableID;
            DataSet ds = model.DeletingTimeTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["TimeTableList"] = " TimeTable Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["TimeTableList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("TimeTableList");
        }

        #endregion

        #region Holiday 
        public ActionResult Holiday(string HolidayID)
        {

            Master model = new Master();
            model.HolidayID = HolidayID;
            if (HolidayID != null)
            {
                DataSet ds = model.HolidayList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.HolidayName = ds.Tables[0].Rows[0]["HolidayName"].ToString();
                    model.HolidayDate = ds.Tables[0].Rows[0]["HolidayDate"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Holiday")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveHoliday(Master obj)
        {

            string FormName = "";
            string Controller = "";

            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.HolidayDate = string.IsNullOrEmpty(obj.HolidayDate) ? null : Common.ConvertToSystemDate(obj.HolidayDate, "dd/MM/yyyy");
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.SaveHoliday();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Holiday"] = "Holiday saved successfully";
                        FormName = "Holiday";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Holiday"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Holiday";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Holiday"] = ex.Message;
                FormName = "Holiday";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult HolidayList(Master model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Holiday List");
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
            List<Master> list = new List<Master>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.HolidayList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.HolidayID = r["PK_HolidayId"].ToString();
                    obj.HolidayName = r["HolidayName"].ToString();
                    obj.HolidayDate = r["HolidayDate"].ToString();

                    list.Add(obj);
                }
                model.classLst = list;
            }
            else
            {
                Session["dt"] = ds.Tables[0];
            }
            return View(model);
        }
        public ActionResult DeletingHoliday(string HolidayID)
        {
            Master model = new Master();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.HolidayID = HolidayID;
            DataSet ds = model.DeletingHoliday();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["HolidayList"] = " Holiday Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["HolidayList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("HolidayList");
        }
        [HttpPost]
        [OnAction(ButtonName = "Update")]
        [ActionName("Holiday")]
        public ActionResult UpdateHoliday(Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.HolidayID = model.HolidayID;
                model.HolidayDate = string.IsNullOrEmpty(model.HolidayDate) ? null : Common.ConvertToSystemDate(model.HolidayDate, "dd/MM/yyyy");
                DataSet ds = model.UpdateHoliday();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["HolidayList"] = "Holiday Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["HolidayList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HolidayList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("HolidayList");
        }
        #endregion
        
        #region Complains
        public ActionResult GetAllMessages()
        {

            Parent newdata = new Parent();
            List<Parent> lst1 = new List<Parent>();

            DataSet ds11 = newdata.GetAllMessages();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Parent Obj = new Parent();
                    Obj.Pk_MessageId = r["Pk_MessageId"].ToString();
                    Obj.Fk_UserId = r["Fk_UserId"].ToString();
                    Obj.MemberName = r["Name"].ToString();
                    Obj.MessageTitle = r["MessageTitle"].ToString();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Message = r["Message"].ToString();
                    Obj.cssclass = r["cssclass"].ToString();

                    lst1.Add(Obj);
                }
                newdata.listparent = lst1;
            }


            return View(newdata);
        }

        public ActionResult SaveMessages(string Message, string MessageBy, string Fk_UserId, string Pk_MessageId)
        {
            Parent obj = new Parent();
            try
            {
                obj.Message = Message;
                obj.MessageBy = MessageBy;
                obj.Fk_UserId = Fk_UserId;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_MessageId = Pk_MessageId;
                DataSet ds = obj.SaveMessage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Result = "1";
                    }
                    else
                    {
                        obj.Result = "Message Not Send";
                    }
                }
                else
                {
                    obj.Result = "Message Not Send";
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        #endregion
        
    }
}




