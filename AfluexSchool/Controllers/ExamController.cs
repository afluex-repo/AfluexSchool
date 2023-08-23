using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class ExamController : AdminBaseController
    {
        // GET: Exam
        #region Exam Type
        public ActionResult ExamType(string Pk_ExamTypeId)
        {
             
            if (Pk_ExamTypeId != null)
            {
                Exam obj = new Exam();
                try
                {
                    obj.Pk_ExamTypeId = Pk_ExamTypeId;
                    DataSet ds = obj.ExamTypeList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ExamTypeId = ds.Tables[0].Rows[0]["Pk_ExamTypeId"].ToString();
                        obj.ExamTypeName = ds.Tables[0].Rows[0]["ExamTypeName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["ExamType"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ActionName("ExamType")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveExamType(Exam obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveExamType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExamType"] = "Save Successfully";
                        FormName = "ExamType";
                        Controller = "Exam";
                    }
                    else
                    {
                        TempData["ExamType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ExamType";
                        Controller = "Exam";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ExamType"] = ex.Message;
                FormName = "ExamType";
                Controller = "Exam";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ExamTypeList(Exam model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Exam Type List");
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
            List<Exam> lst = new List<Exam>();

            DataSet ds = model.ExamTypeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Exam obj = new Exam();
                        obj.Pk_ExamTypeId = r["Pk_ExamTypeId"].ToString();
                        obj.ExamTypeName = r["ExamTypeName"].ToString();
                        Session["Pk_ExamTypeId"] = null;

                        lst.Add(obj);
                    }

                }
                catch (Exception ex)
                {
                    model.Result = ex.Message;
                }

                model.lstExamType = lst;
            }
            return View(model);
        }

        public ActionResult DeleteExamType(string Pk_ExamTypeId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Exam obj = new Exam();
                obj.Pk_ExamTypeId = Pk_ExamTypeId;
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteExamType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExamTypeList"] = "Deleted  Successfully";
                        FormName = "ExamTypeList";
                        Controller = "Exam";
                    }
                    else
                    {
                        TempData["ExamTypeList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ExamTypeList";
                        Controller = "Exam";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ExamTypeList"] = ex.Message;
                FormName = "ExamTypeList";
                Controller = "Exam";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("ExamType")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateExamType(string Pk_ExamTypeId, string ExamTypeName)
        {
            string FormName = "";
            string Controller = "";

            Exam obj = new Exam();
            try
            {
                obj.Pk_ExamTypeId = Pk_ExamTypeId;
                obj.ExamTypeName = ExamTypeName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateExamType();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "ExamTypeList";
                        Controller = "Exam";
                        TempData["ExamTypeList"] = "Updated Successfully";
                    }
                    else
                    {
                        Session["Pk_ExamTypeId"] = Pk_ExamTypeId;
                        FormName = "ExamType";
                        Controller = "Exam";
                        TempData["ExamTypeList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ExamTypeList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Maximum Marks
        public ActionResult MaxMarksMaster(string Pk_MaxMarksId)
        {
            try
            {
                #region ddlBindClass
                Exam obj = new Exam();
                int count = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds = obj.BindClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
                #endregion 
                 
                #region ddlBindExamType
                Exam objf = new Exam();
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

            }
            catch (Exception)
            {

            }
            if (Pk_MaxMarksId != null)
            {
                Exam obj = new Exam();
                try
                {
                    obj.Pk_MaxMarksId = Pk_MaxMarksId;
                    DataSet ds = obj.GetMaxMarksList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_MaxMarksId = ds.Tables[0].Rows[0]["Pk_MaxMarksId"].ToString();
                        obj.ClassName = ds.Tables[0].Rows[0]["Fk_ClassId"].ToString();
                        obj.ExamTypeName = ds.Tables[0].Rows[0]["Fk_ExamTypeId"].ToString();
                        obj.MinMarks = ds.Tables[0].Rows[0]["MinMarks"].ToString();
                        obj.MaxMarksExam = ds.Tables[0].Rows[0]["MaxMarksExam"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["MaxMarksMaster"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        [ActionName("MaxMarksMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveMaxMarks(Exam obj)
        {

            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveMaxMarks();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MaxMarksMaster"] = "Save Successfully!";
                        FormName = "MaxMarksMaster";
                        Controller = "Exam";
                    }
                    else
                    {
                        TempData["MaxMarksMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "MaxMarksMaster";
                        Controller = "Exam";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["MaxMarksMaster"] = ex.Message;
                FormName = "MaxMarksMaster";
                Controller = "Exam";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult MaxMarksList(Exam model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Maximum Marks List");
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
            List<Exam> lst = new List<Exam>();

            DataSet ds = model.GetMaxMarksList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Exam obj = new Exam();
                        obj.Pk_MaxMarksId = r["Pk_MaxMarksId"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.ExamTypeName = r["ExamTypeName"].ToString();
                        obj.MaxMarksTest = r["MinMarks"].ToString();
                        obj.MaxMarksExam = r["MaxMarksExam"].ToString();

                        lst.Add(obj);
                    }

                }
                catch (Exception ex)
                {
                    model.Result = ex.Message;
                }

                model.lstMaxMarks = lst;
            }
            return View(model);
        }
        public ActionResult DeleteMaxMarks(string Pk_MaxMarksId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Exam obj = new Exam();
                obj.Pk_MaxMarksId = Pk_MaxMarksId;
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteMaxMarks();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GetMaxMarksList"] = "Delete Category Successfully";
                        FormName = "MaxMarksList";
                        Controller = "Exam";
                    }
                    else
                    {
                        TempData["GetMaxMarksList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "MaxMarksList";
                        Controller = "Exam";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["GetMaxMarksList"] = ex.Message;
                FormName = "MaxMarksList";
                Controller = "Exam";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("MaxMarksMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateMaxMarks(Exam obj)
        {

            try
            {

                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateMaxMarks();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GetMaxMarksList"] = "Update Successfully!";
                    }
                    else
                    {
                        TempData["GetMaxMarksList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["GetMaxMarksList"] = ex.Message;
            }
            return RedirectToAction("MaxMarksList");
        }
        #endregion

        #region StudentMarks
      
        public ActionResult StudentMarks(string Pk_StudentMarksId)
        {
            try
            {
                #region ddlBindClass
                Exam objclass = new Exam();
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

                List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
                ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject Name--", Value = "0" });
                ViewBag.ddlSubjectName = ddlSubjectName;


                #region ddlBindExamType
                Exam objf = new Exam();
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

            }
            catch (Exception)
            { }


            if (Pk_StudentMarksId != null)
            {
                Exam obj = new Exam();
                try
                {
                    obj.Pk_StudentMarksId = Pk_StudentMarksId;
                    obj.Session = Session["SessionId"].ToString();
                    
                    DataSet ds = obj.StudentMarksList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Pk_StudentMarksId = ds.Tables[0].Rows[0]["Pk_StudentMarksId"].ToString();
                        obj.StudentName = ds.Tables[0].Rows[0]["Fk_StudentId"].ToString();
                        obj.ClassName = ds.Tables[0].Rows[0]["Fk_ClassId"].ToString();
                        obj.SectionName = ds.Tables[0].Rows[0]["Fk_SectionId"].ToString();
                        obj.SubjectName = ds.Tables[0].Rows[0]["Fk_SubjectId"].ToString();
                        obj.Marks = ds.Tables[0].Rows[0]["Marks"].ToString();
                        obj.ObtainMarks = ds.Tables[0].Rows[0]["ObtainMarks"].ToString();
                    }
                }
                catch (Exception)
                { }
            }
            return View();
        }


        [HttpPost]
        [ActionName("StudentMarks")]
        [OnAction(ButtonName = "btndetail")]
        public ActionResult StudentMarks(Exam model)
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
            
            #region ddlsubject
            try
            {

               Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
                // obj.Fk_ClassID = model.Fk_ClassID;
                // obj.Fk_SectionId = model.Fk_SectionId;
                DataSet ds1 = obj.gettingSubjectMaster();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSubjectName.Add(new SelectListItem { Text = "--Select subject--", Value = "0" });
                        }
                        ddlSubjectName.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Pk_SubjectId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSubjectName = ddlSubjectName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            
            #region ddlBindExamType
            Exam objf = new Exam();
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

            List<Exam> list = new List<Exam>();
            Exam obj1 = new Exam();
            model.Fk_ClassID = model.Fk_ClassID;
            model.Fk_SectionId = model.Fk_SectionId;
            model.ExamTypeName = model.ExamTypeName;
            model.Session = Session["SessionId"].ToString();
            DataSet ds = model.GetStudentBySection();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Exam obj = new Exam();
                    obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.MaxMarks = r["MaxMarks"].ToString();
                    //obj.Fk_ClassID=r["Fk_ClassID"].ToString();

                    list.Add(obj);
                }
                model.lststudent = list;
            }
            return View(model);
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
        public ActionResult GetSubjectNameBySection(string FK_ClassID, string Fk_SectionId)
        {
            Exam model = new Exam();
            try
            {

                model.Fk_SectionId = Fk_SectionId;
                model.Fk_ClassID = FK_ClassID;

                List<SelectListItem> ddlSection = new List<SelectListItem>();
                model.Session = Session["SessionId"].ToString();
                DataSet ds = model.GetSubjectNameBySection();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Fk_SubjectID"].ToString() });

                    }
                }

                ViewBag.ddlSubjectName = ddlSection;
                model.ddlSubjectName = ddlSection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("StudentMarks")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveStudentMarks(Exam obj)
        {

            string FormName = "";
            string Controller = "";
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string studentid = "";
                string obtainMarks = "";
                string SubjectId = "";
                string maxmarks = "";
                DataTable dtstudent = new DataTable();

                dtstudent.Columns.Add("Pk_StudentID", typeof(string));
                dtstudent.Columns.Add("Fk_subjectID", typeof(string));
                dtstudent.Columns.Add("ObtainMarks", typeof(string));
                dtstudent.Columns.Add("MaximumMarks", typeof(string));

                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    studentid = Request["hdStudentID_ " + i].ToString();
                    obtainMarks = Request["obtainMarks_ " + i].ToString();
                    maxmarks = Request["MaxMarks " + i].ToString();
                    SubjectId = obj.SubjectID;
                    dtstudent.Rows.Add(studentid, SubjectId, obtainMarks, maxmarks);

                }
                obj.dsStudentAttendance = dtstudent;

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Session = Session["SessionId"].ToString();
                DataSet ds = obj.SaveStudentMarks();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["StudentMarks"] = "Student Marks Added Successfully";
                        FormName = "StudentMarks";
                        Controller = "Exam";
                    }
                    else
                    {
                        TempData["StudentMarks"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "StudentMarks";
                        Controller = "Exam";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["StudentMarks"] = ex.Message;
                FormName = "StudentMarks";
                Controller = "Exam";
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult StudentMarksList(Exam model)
        {
            #region ddlBindClass
            Exam objclass = new Exam();
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

            List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
            ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject Name--", Value = "0" });
            ViewBag.ddlSubjectName = ddlSubjectName;


            #region ddlBindExamType
            Exam objf = new Exam();
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



            return View(model);
        }


        [HttpPost]
        [ActionName("StudentMarksList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult StudentMarksListBy(Exam model)
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

            #region ddlsubject
            try
            {

                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
                // obj.Fk_ClassID = model.Fk_ClassID;
                // obj.Fk_SectionId = model.Fk_SectionId;
                DataSet ds1 = obj.gettingSubjectMaster();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSubjectName.Add(new SelectListItem { Text = "--Select subject--", Value = "0" });
                        }
                        ddlSubjectName.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Pk_SubjectId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSubjectName = ddlSubjectName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlBindExamType
            Exam objf = new Exam();
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

            List<Exam> list = new List<Exam>();
            Exam obj1 = new Exam();
            model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
            model.Fk_SectionId = model.Fk_SectionId == "0" ? null : model.Fk_SectionId;
            model.SubjectID = model.SubjectID == "0" ? null : model.SubjectID;
            model.ExamTypeName = model.ExamTypeName == "0" ? null : model.ExamTypeName;
            model.Session= Session["SessionId"].ToString();
            DataSet ds = model.StudentMarksList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Exam obj = new Exam();
                    obj.Pk_StudentID = r["Pk_StudentMarksId"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.MaxMarks = r["MaximumMarks"].ToString(); 
                    obj.ObtainedMarks = r["ObtainMarks"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.SubjectName = r["SubjectName"].ToString();
                    obj.ExamTypeName = r["ExamTypeName"].ToString();
                   
 
                    list.Add(obj);
                }
                model.lststudent = list;
            }
            return View(model);

        }
        #endregion

    }
}