using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using static AfluexSchool.Models.Common;

namespace AfluexSchool.Controllers
{
    public class TeacherLoginController : TeacherBaseController
    {
        // GET: TeacherLogin
        public ActionResult Dashboard()
        {
            return View();
        }

        #region Student Attendance

        public ActionResult StudentAttendanceByTeacher()
        {
            Student model = new Student();
            model.AttendanceDate = DateTime.Now.ToString("dd/MM/yyyy");
            model.TeacherID = Session["PK_TeacherID"].ToString();
            DataSet ds = model.GetClassOfClassTeacher();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Fk_ClassID = ds.Tables[0].Rows[0]["Fk_ClassId"].ToString();
                model.Fk_SectionID = ds.Tables[0].Rows[0]["Fk_SectionId"].ToString();
                model.ClassName = ds.Tables[0].Rows[0]["ClassName"].ToString();
                model.SectionName = ds.Tables[0].Rows[0]["SectionName"].ToString();
            }

            return View(model);
        }
        [HttpPost]
        [ActionName("StudentAttendanceByTeacher")]
        [OnAction(ButtonName = "btndetail")]
        public ActionResult StudentAttendance(Student model)
        {
            model.AttendanceDate = Common.ConvertToSystemDate(model.AttendanceDate, "dd/MM/yyyy");

            List<Student> list = new List<Student>();
            model.Fk_ClassID = model.Fk_ClassID == null ? "0" : model.Fk_ClassID;
            model.Fk_SectionID = model.Fk_SectionID == null ? "0" : model.Fk_SectionID;
            DataSet ds = model.GetStudentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Student obj = new Student();
                    obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                    obj.DisplayName = r["StudentName"].ToString();
                    obj.LoginID = r["LoginID"].ToString();
                    obj.Fk_ClassID = r["Fk_ClassID"].ToString();
                    obj.Fk_SectionID = r["Fk_SectionID"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SessionName = r["SessionName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Medium = r["Medium"].ToString();
                    //obj.Status = r["Status"].ToString();


                    list.Add(obj);
                }
                model.listStudent = list;
            }
            else
            {

            }

            return View(model);
        }

        [HttpPost]
        [ActionName("StudentAttendanceByTeacher")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveAttendance(Student obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string studentid = "";
                string chk = "";
                DataTable dtstudent = new DataTable();
                obj.AttendanceDate = Common.ConvertToSystemDate(obj.AttendanceDate, "dd/MM/yyyy");
                dtstudent.Columns.Add("Pk_StudentID", typeof(string));
                dtstudent.Columns.Add("Status", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["chkSelect_ " + i];
                    if (chk == "on")
                    {
                        obj.Status = "P";

                    }
                    else
                    {

                        //try
                        //{
                        //    string str2 = BLAPSSchool.StudentAttendance(Request["hdStudentName_ " + i].ToString(), obj.AttendanceDate, Request["hdMobile_ " + i].ToString());
                        //    BLAPSSchool.SendSMS(Request["hdMobile_ " + i].ToString(), str2);

                        //}
                        //catch { }
                        obj.Status = "A";
                    }

                    studentid = Request["hdStudentID_ " + i].ToString();

                    dtstudent.Rows.Add(studentid, obj.Status);

                }
                obj.dsStudentAttendance = dtstudent;

                obj.AddedBy = Session["PK_TeacherID"].ToString();
                obj.AttendanceBy = "Teacher";
                DataSet ds = obj.SaveAttendance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["StudentAttendance"] = " Student attendance  is successfully save";
                        FormName = "StudentAttendanceByTeacher";
                        Controller = "TeacherLogin";
                    }
                    else
                    {
                        TempData["StudentAttendance"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "StudentAttendanceByTeacher";
                        Controller = "TeacherLogin";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["StudentAttendance"] = ex.Message;
                FormName = "StudentAttendanceByTeacher";
                Controller = "TeacherLogin";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Homework
        public ActionResult GetSectionByClass(string Fk_ClassID)
        {
            Student model = new Student();
            try
            {

                model.Fk_ClassID = Fk_ClassID;

                List<SelectListItem> ddlsection = new List<SelectListItem>();
                model.TeacherID = Session["PK_TeacherID"].ToString();
                DataSet ds = model.GetSectionByClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[1].Rows)
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
        public ActionResult GetSubjectNameBySection(string FK_ClassID, string Fk_SectionID)
        {
            Student model = new Student();
            try
            {

                model.Fk_SectionID = Fk_SectionID;
                model.Fk_ClassID = FK_ClassID;
                model.TeacherID = Session["PK_TeacherID"].ToString();
                List<SelectListItem> ddlSection = new List<SelectListItem>();

                DataSet ds = model.GetSubjectNameBySection();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Fk_SubjectID"].ToString() });

                    }
                }
                
                model.ddlSubjectName = ddlSection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Homework()
        {
            Student model = new Student();
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                obj.TeacherID = Session["PK_TeacherID"].ToString();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[1].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }
                else
                {
                    ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
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

            List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
            ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject Name--", Value = "0" });
            ViewBag.ddlSubjectName = ddlSubjectName;
            return View(model);
        }
        [HttpPost]
        [ActionName("Homework")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult SaveHomework(IEnumerable<HttpPostedFileBase> StudentFiles, Student obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {

                foreach (var file in StudentFiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.StudentPhoto = "/Homework/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.StudentPhoto)));
                    }
                }

                obj.AddedBy = Session["PK_TeacherID"].ToString();
                obj.HomeworkDate = string.IsNullOrEmpty(obj.HomeworkDate) ? null : Common.ConvertToSystemDate(obj.HomeworkDate, "dd/MM/yyyy");
                obj.HomeworkBy = "Teacher";
                DataSet ds = obj.SaveHomework();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        //try
                        //{
                        //    string str2 = BLAimsInterCollege.Registration(ds.Tables[0].Rows[0]["StudentName"].ToString(), ds.Tables[0].Rows[0]["LoginID"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                        //    BLAimsInterCollege.SendSMS(obj.Mobile, str2);
                        //}
                        //catch { }

                        TempData["Homework"] = "Homework assigned successfully";
                        FormName = "Homework";
                        Controller = "TeacherLogin";
                    }
                    else
                    {
                        TempData["Homework"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Homework";
                        Controller = "TeacherLogin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Homework"] = ex.Message;
                FormName = "Homework";
                Controller = "TeacherLogin";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult HomeworkList(Student model)
        {
            List<Student> list = new List<Student>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.HomeworkBy = "Teacher";
                model.AddedBy = Session["PK_TeacherID"].ToString();
                DataSet ds = model.HomeworkList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.HomeWorkID = r["Pk_HomeworkID"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
                        obj.StudentPhoto = r["HomeworkFile"].ToString();
                        obj.HomeworkDate = r["HomeworkDate"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SubjectID = r["SubjectName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.HomeworkBy = r["HomeworkBy"].ToString();

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
            Teacher objclass = new Teacher();
            int countClass = 0;
            objclass.PK_TeacherID = Session["PK_TeacherID"].ToString();
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet dsClass = objclass.GetClassList();

            if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[1].Rows.Count>0)
            {
                foreach (DataRow r in dsClass.Tables[1].Rows)
                {
                    if (countClass == 0)
                    {
                        ddlClassName.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                    }
                    ddlClassName.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                    countClass = countClass + 1;
                }
            }
            else
            {
                ddlClassName.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
            }

            ViewBag.ddlClassName = ddlClassName;
            #endregion
            List<SelectListItem> ddlSection = new List<SelectListItem>();
            ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlSection = ddlSection;

            List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
            ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject--", Value = "0" });
            ViewBag.ddlSubjectName = ddlSubjectName;

            return View(model);
        }

        [HttpPost]
        [ActionName("HomeworkList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchHomeworkReport(Student model)
        {
            List<Student> list = new List<Student>();
            try
            {

                model.TeacherID = Session["PK_TeacherID"].ToString();
                if (model.Fk_ClassID == "0")
                {
                    model.Fk_ClassID = null;
                }
                if (model.Fk_SectionID == "0")
                {
                    model.Fk_SectionID = null;
                }
                if (model.TeacherID == "0")
                {
                    model.TeacherID = null;
                }
                if (model.SubjectID == "0")
                {
                    model.SubjectID = null;
                }
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds5 = model.HomeworkList();
                if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds5.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.HomeWorkID = r["Pk_HomeworkID"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
                        obj.StudentPhoto = r["HomeworkFile"].ToString();
                        obj.HomeworkDate = r["HomeworkDate"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SubjectID = r["SubjectName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.HomeworkBy = r["HomeworkBy"].ToString();
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
            Teacher objclass = new Teacher();
            int countClass = 0;
            objclass.PK_TeacherID = Session["PK_TeacherID"].ToString();
            List<SelectListItem> ddlClassName = new List<SelectListItem>();
            DataSet dsClass = objclass.GetClassList();

            if (dsClass != null && dsClass.Tables.Count > 0)
            {
                foreach (DataRow r in dsClass.Tables[1].Rows)
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

            model.Fk_ClassID = model.Fk_ClassID;

            List<SelectListItem> ddlsection = new List<SelectListItem>();
            
            if (model.Fk_ClassID == null)
            {
                ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            }
            else
            {
                model.TeacherID = Session["PK_TeacherID"].ToString();
                DataSet ds = model.GetSectionByClass();
                int count = 0;
                if (ds != null && ds.Tables.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        if (count == 0)
                        {
                            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                        }
                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                        count = count + 1;

                    }
                }
            }
            

                ViewBag.ddlsection = ddlsection;


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
        public ActionResult DeleteHomework(string HomeWorkID)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                Student obj = new Student();
                obj.HomeWorkID = HomeWorkID;
                obj.DeletedBy = Session["PK_TeacherID"].ToString();

                DataSet ds = obj.DeleteHomeworkByTeacher();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["HomeworkList"] = " Homework Deleted successfully";
                        FormName = "HomeworkList";
                        Controller = "TeacherLogin";
                    }
                    else
                    {
                        TempData["HomeworkList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "HomeworkList";
                        Controller = "Student";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HomeworkList"] = ex.Message;
                FormName = "HomeworkList";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region notice

        public ActionResult NoticeMaster(string PK_NoticeId)
        {
            Master model = new Master();

            if (PK_NoticeId != null)
            {
                model.PK_NoticeId = PK_NoticeId;
                DataSet ds2 = model.GettingNoticeList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    model.PK_NoticeId = ds2.Tables[0].Rows[0]["PK_NoticeId"].ToString();
                    model.NoticeName = ds2.Tables[0].Rows[0]["NoticeName"].ToString();
                    model.Fk_ClassID = ds2.Tables[0].Rows[0]["FK_ClassId"].ToString();
                    model.Fk_SectionID = ds2.Tables[0].Rows[0]["FK_SectionId"].ToString();
                }
                Student obj1 = new Student();
                #region ddlhelclass
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlclass = new List<SelectListItem>();
                    obj.TeacherID = Session["PK_TeacherID"].ToString();
                    DataSet ds1 = obj.GetClassList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[1].Rows)
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
                List<SelectListItem> ddlSection = new List<SelectListItem>();
                ddlSection.Add(new SelectListItem { Text = "Select Section", Value = "0" });

                DataSet ds5 = obj1.GetSectionList();
                if (ds5 != null && ds5.Tables.Count > 0)
                {
                    foreach (DataRow r in ds5.Tables[0].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }

                ViewBag.ddlsection = ddlSection;
                return View(model);
            }
            else
            {

                #region ddlhelclass
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlclass = new List<SelectListItem>();
                    obj.TeacherID = Session["PK_TeacherID"].ToString();
                    DataSet ds1 = obj.GetClassList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[1].Rows)
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
                return View(model);
            }

        }

        [HttpPost]
        [OnAction(ButtonName = "SaveNotice")]
        [ActionName("NoticeMaster")]
        public ActionResult SaveNotice(Master model)
        {
            try
            {
                model.AddedBy = Session["PK_TeacherID"].ToString();
                model.NoticeBy = "Teacher";
                DataSet ds = model.SaveNotice();
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
            List<Master> list = new List<Master>();
            model.AddedBy = Session["PK_TeacherID"].ToString();
            DataSet ds = model.GettingNoticeList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
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
            model.UpdatedBy = Session["PK_TeacherID"].ToString();

            DataSet ds = model.UpdateNotice();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["NoticeList"] = "Notice Updated Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["NoticeList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(model);
                }
            }
            return RedirectToAction("NoticeList");
        }

        public ActionResult DeleteNotice(string PK_NoticeId)
        {
            Master model = new Master();
            model.DeletedBy = Session["PK_TeacherID"].ToString();
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

        #region EditProfile
        public ActionResult EditProfile(Teacher model)
        {

            #region ddlBranch
            try
            {
                Teacher obj1 = new Teacher();
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet ds1 = obj1.BranchList();
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
                Teacher obj1 = new Teacher();
                int count = 0;
                List<SelectListItem> ddlReligion = new List<SelectListItem>();
                DataSet ds1 = obj1.GetReligion();
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


            model.PK_TeacherID = Session["PK_TeacherID"].ToString();


            DataSet ds = model.GetTeacherList();
            if (ds != null && ds.Tables.Count > 0)
            {
                model.PK_TeacherID = ds.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                model.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                model.Religion = ds.Tables[0].Rows[0]["FK_ReligionID"].ToString();
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
                model.BranchName = ds.Tables[0].Rows[0]["FK_BranchID"].ToString();


            }


            return View(model);
        }

        [HttpPost]
        [ActionName("EditProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateTeacherList(Teacher model, string PK_TeacherID, string BranchName,
           string Address, string pincode, string FatherName, string DOB, string Gender, string Religion, string Category, string Qualification,
           string Experience, string MobileNo, string DOJ, IEnumerable<HttpPostedFileBase> Image, string Name, string EmailID,
           string LastExperience, string LastSchool)
        {
            string FormName = "";
            string Controller = "";
            Teacher obj = new Teacher();
            try
            {
                foreach (var file in Image)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.Image = "../Teacher/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.Image)));
                    }
                    else
                    {
                        obj.Image = "../../img/no-profile.jpeg";
                    }
                }
                model.PK_TeacherID = PK_TeacherID;
                model.Name = Name;
                model.FatherName = FatherName;
                model.BranchName = BranchName;
                model.Address = Address;
                model.PinCode = pincode;
                model.FatherName = FatherName;
                model.DOB = DOB;
                model.Gender = Gender;
                model.Religion = Religion;
                model.Category = Category;
                model.Qualification = Qualification;
                model.LastExperience = LastExperience;
                model.LastSchool = LastSchool;
                model.Experience = Experience;
                model.MobileNo = MobileNo;
                model.DOJ = DOJ;

                model.EmailID = EmailID;
                model.UpdatedBy = Session["PK_TeacherID"].ToString();
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.DOJ = string.IsNullOrEmpty(obj.DOJ) ? null : Common.ConvertToSystemDate(obj.DOJ, "dd/MM/yyyy");
                DataSet ds = model.UpdateTeacherRecord();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "EditPtofile";
                        Controller = "TeacherLogin";
                        TempData["TeacherEditProfile"] = "Record updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        FormName = "EditPtofile";
                        Controller = "TeacherLogin";
                        TempData["TeacherEditProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["TeacherEditProfile"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region ChangePassword

        public ActionResult ChangePassword(ChangePassword model)
        {

            return View(model);
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SAveChangePassword(ChangePassword model)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                model.UpdatedBy = Session["PK_TeacherID"].ToString();

                DataSet ds = model.UpdateTeacherPassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangePassword"] = "Password is changed successfully";
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["ChangePassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePassword";
                        Controller = "TeacherLogin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangePassword"] = ex.Message;
                FormName = "ChangePassword";
                Controller = "TeacherLogin";
            }
            return RedirectToAction(FormName, Controller);

        }




        #endregion

        #region TeacherAttendanceReport
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
            model.EmployeeCode = Session["LoginID"].ToString();
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

                    lst.Add(obj);
                }
                model.lstList = lst;
            }

            return View(model);
        }

        #endregion

        #region SalarySlip
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
            model.EmployeeID = Session["PK_TeacherID"].ToString();
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
                    obj.NetSalary = r["NetSalary"].ToString();
                    obj.MonthName = r["MonthName"].ToString();
                    obj.Year = r["Year"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);
        }

        public ActionResult PrintSalarySlip(string Pk_PaidSalId, string EmployeeID)
        {
            HRManagement model = new HRManagement();

            List<HRManagement> lst = new List<HRManagement>();
            List<HRManagement> lst1 = new List<HRManagement>();
            model.Pk_PaidSalId = Pk_PaidSalId;
            model.EmployeeID = EmployeeID;
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

        public ActionResult GetStateCityByPincode(string PinCode)
        {
            Parent model = new Parent();
            try
            {

                model.PinCode = PinCode;

                DataSet ds = model.GetStateCityByPincode();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    model.State = ds.Tables[0].Rows[0]["StateName"].ToString();
                    model.City = ds.Tables[0].Rows[0]["CityName"].ToString();
                    model.Result = "Yes";
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #region Approve DEcline List

        public ActionResult LeaveList(Teacher model)
        {
            model.PK_TeacherID = Session["PK_TeacherID"].ToString();
            List<Teacher> listq = new List<Teacher>();
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

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string s = r["IsApproved"].ToString();
                    if (s != "Pending")
                    {
                        Teacher obj = new Teacher();

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
                }
                model.listStudent = listq;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSearch12")]
        [ActionName("LeaveList")]
        public ActionResult SearchLeave(Teacher model)
        {
            List<Teacher> list = new List<Teacher>();
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
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[1].Rows)
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

            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string s = r["IsApproved"].ToString();
                    if (s != "Pending")
                    {
                        Teacher obj = new Teacher();

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

        public ActionResult GetSecByClass(string Fk_ClassID)
        {
            Teacher model = new Teacher();
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

        #region PendingLeave

        public ActionResult PendingLeave(Teacher model)
        {

            model.PK_TeacherID = Session["PK_TeacherID"].ToString();
            model.Status = "Pending";
            List<Teacher> listq = new List<Teacher>();
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

            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Teacher obj = new Teacher();

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
        public ActionResult ApprovePendingLeave(Teacher model)
        {
            string noofrows = Request["hdRows"].ToString();

            string chkselect = "";

            for (int i = 1; i < int.Parse(noofrows); i++)
            {
                try
                {

                    if (Request["chkSelect_ " + i].ToString() == "Checked")
                    {
                        model.UpdatedBy = Session["PK_TeacherID"].ToString();
                        model.PK_StdntLeaveID = Request["PK_StdntLeaveID_ " + i].ToString();
                        model.Description = Request["Description_ " + i].ToString();
                        model.Pk_StudentID = Request["Pk_StudentID_ " + i].ToString();
                        model.Status = "Approved";
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
        public ActionResult DeclinePendingLeave(Teacher model)
        {
            string noofrows = Request["hdRows"].ToString();

            string chkselect = "";

            for (int i = 1; i < int.Parse(noofrows); i++)
            {
                try
                {

                    if (Request["chkSelect_ " + i].ToString() == "Checked")
                    {
                        model.UpdatedBy = Session["PK_TeacherID"].ToString();
                        model.PK_StdntLeaveID = Request["PK_StdntLeaveID_ " + i].ToString();
                        model.Description = Request["Description_ " + i].ToString();
                        model.Pk_StudentID = Request["Pk_StudentID_ " + i].ToString();
                        model.Status = "Declined";
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
        public ActionResult SearchPendingLeave(Teacher model)
        {
            List<Teacher> list = new List<Teacher>();
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

            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string s = r["IsApproved"].ToString();
                    if (s == "Pending")
                    {
                        Teacher obj = new Teacher();

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
            Teacher model = new Teacher();
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

        public ActionResult HomeWorkView(string StudentPhoto, string HomeWorkID)
        {
            Student model = new Student();
            model.HomeWorkID = HomeWorkID;
            model.HomeworkFile = StudentPhoto;
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