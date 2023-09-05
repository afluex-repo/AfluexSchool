using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class ParentPanelController : ParerntBaseController
    {
        // GET: ParentLogin
        public ActionResult DashBoard()
        {
            List<DashBoard> list = new List<DashBoard>();
            DashBoard newdata = new DashBoard();
            newdata.Fk_ParentId = Session["Pk_ParentID"].ToString();
            DataSet Ds = newdata.GetDashBoardDetailsForParent();
            if (Ds != null && Ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in Ds.Tables[0].Rows)
                {
                    DashBoard obj = new DashBoard();
                    obj.Name = r["StudentName"].ToString();
                    obj.ImagePath = r["StudentPhoto"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.DateOfBirth = r["DateOfBirth"].ToString();
                    obj.Gender = r["Gender"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.AadhaarCard = r["AadhaarCard"].ToString();
                    obj.PermanentAddress = r["PermanentAddress"].ToString();
                    obj.CorrespondenceAddress = r["CorrespondenceAddress"].ToString();
                    obj.FatherOcc = r["FatherOcc"].ToString();
                    obj.MotherOcc = r["MotherOcc"].ToString();
                    obj.RegistrationNo = r["RegistrationNo"].ToString();
                    list.Add(obj);

                }
                newdata.lststudent = list;
            }
            List<DashBoard> list1 = new List<DashBoard>();
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in Ds.Tables[1].Rows)
                {
                    DashBoard obj = new DashBoard();
                    obj.Notice = r["NoticeName"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();

                    list1.Add(obj);

                }
                newdata.lstteacher = list1;
            }
            return View(newdata);
        }

        #region AttendanceReport
        public ActionResult AttendanceReport()
        {
            #region ddlhelclass+
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
                model.Fk_ParentId = Session["Pk_ParentID"].ToString();

                DataSet ds = model.GetStudentAttendanceDetail();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.Pk_StudentAttendanceID = r["Pk_StudentAttID"].ToString();
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

        #region Fee
        public ActionResult FeeReport(Reports obj)
        {
            List<Reports> lst = new List<Reports>();
            try
            {
                obj.Fk_ParentId = Session["Pk_ParentID"].ToString();

                obj.Fk_ClassID = obj.Fk_ClassID == "0" ? null : obj.Fk_ClassID;
                obj.Fk_SectionID = obj.Fk_SectionID == "0" ? null : obj.Fk_SectionID;
                DataSet ds = obj.GetFeeReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Reports obj1 = new Reports();
                            obj1.Amount = r["Amount"].ToString();
                            obj1.StudentName = r["StudentName"].ToString();
                            obj1.ReceiptNo = r["ReceiptNo"].ToString();
                            obj1.PaymentDate = r["PaymentDate"].ToString();
                            obj1.PaymentMode = r["PaymentMode"].ToString();
                            obj1.BankDetails = r["BankDetails"].ToString();

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

            #region ddlhelclass+
            try
            {
                Student model = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = model.GetClassList();
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
            return View(obj);
        }


        public ActionResult PrintFeerecipt(string Id)
        {
            List<Reports> lst = new List<Reports>();
            Reports obj = new Reports();
            obj.ReceiptNo = Id;
            DataSet ds = obj.PrintReceipt();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                ViewBag.StudentId = ds.Tables[0].Rows[0]["LoginID"].ToString();
                ViewBag.ParentMobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.StudentName = ds.Tables[0].Rows[0]["StudentName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.TotalFee = ds.Tables[0].Rows[0]["TotalFee"].ToString();
                ViewBag.TotalFeeInWords = ds.Tables[0].Rows[0]["TotalFeeInWords"].ToString();

                ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                ViewBag.AmountInWords = ds.Tables[0].Rows[0]["AmountInWords"].ToString();
                ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                ViewBag.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                ViewBag.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                ViewBag.BankDetails = ds.Tables[0].Rows[0]["BankDetails"].ToString();

                ViewBag.LandLine = Common.SoftwareDetails.LandLine;
                ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                ViewBag.Website = Common.SoftwareDetails.Website;
                ViewBag.EmailID = Common.SoftwareDetails.EmailID;
                ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;

                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Reports obj1 = new Reports();
                    obj1.FeeTypeName = r["FeeTypeName"].ToString();
                    obj1.PaidAmount = r["PaidAmt"].ToString();
                    obj1.InstallmentAmt = r["InstallmentAmt"].ToString();
                    obj1.PaymentMode = r["PaymentMode"].ToString();
                    obj1.InstallemntNo = r["InstallemntNo"].ToString();
                    obj1.DueDate = r["DueDate"].ToString();
                    obj1.PaymentDate = r["PaymentDate"].ToString();
                    obj1.BankDetails = r["BankDetails"].ToString();
                    lst.Add(obj1);
                }
                obj.lstfeedata = lst;

            }
            return View(obj);
        }
        #endregion

        #region EditProfile
        public ActionResult EditProfile(Parent model)
        {
            try
            {
                model.Pk_ParentID = Session["Pk_ParentID"].ToString();
                DataSet ds = model.ParentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Pk_ParentID = ds.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                    model.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();

                }
            }
            catch (Exception ex)
            {
                TempData["Parent"] = ex.Message;
            }
            return View(model);
        }
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

        [HttpPost]
        [ActionName("EditProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateParentList(string Pk_ParentID, string ParentName, string Email, string Mobile, string PinCode, string Address, string State, string City)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Parent obj = new Parent();
                obj.Pk_ParentID = Pk_ParentID;
                obj.ParentName = ParentName;
                obj.Email = Email;
                obj.Mobile = Mobile;
                obj.Address = Address;
                obj.PinCode = PinCode;
                obj.State = State;
                obj.City = City;

                obj.UpdatedBy = Session["Pk_ParentID"].ToString();

                DataSet ds = obj.UpdateParentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["EditProfile"] = "Parent Record is Successfully updated";
                        FormName = "EditProfile";
                        Controller = "ParentLogin";
                    }
                    else
                    {
                        Session["Pk_ParentID"] = Pk_ParentID;
                        TempData["EditProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EditProfile";
                        Controller = "ParentLogin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EditProfile"] = ex.Message;
                FormName = "EditProfile";
                Controller = "ParentLogin";
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

                model.UpdatedBy = Session["Pk_ParentID"].ToString();
                model.Pk_ParentID = Session["Pk_ParentID"].ToString();
                DataSet ds = model.UpdateParentPassword();
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
                        Controller = "ParentPanel";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangePassword"] = ex.Message;
                FormName = "ChangePassword";
                Controller = "ParentPanel";
            }
            return RedirectToAction(FormName, Controller);

        }




        #endregion

        #region homewrok
        public ActionResult Homework(Student model)
        {

            List<Student> lst = new List<Student>();
            try
            {
                model.AddedBy = Session["Pk_ParentID"].ToString();
                DataSet ds = model.GetHomework();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        Student obj = new Student();

                        obj.studentName = r["SubjectName"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.AttendanceDate = r["HomeworkDate"].ToString();
                        obj.Status = r["HomeworkFile"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
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

        #region Syllabus

        public ActionResult SyllabusList(Master model)
        {
            List<Master> list = new List<Master>();
            model.PK_ParentId = Session["Pk_ParentID"].ToString();
            DataSet ds = model.SyllabusListForParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Syllabus = r["Syllabus"].ToString();
                    obj.Name = r["StudentName"].ToString();
                    list.Add(obj);
                }
                model.classLst = list;
            }
            return View(model);
        }


        #endregion

        #region TimeTable

        public ActionResult TimeTableList(Master model)
        {
            List<Master> list = new List<Master>();
            model.AddedBy = Session["Pk_ParentID"].ToString();
            DataSet ds = model.TimeTableListsForParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.TimeTable = r["TimeTable"].ToString();
                    obj.Name = r["StudentName"].ToString();
                    list.Add(obj);
                }
                model.classLst = list;
            }
            return View(model);
        }


        #endregion

        #region ApplyForLeave

        public ActionResult ApplyLeave(Student obj)
        {
            #region ddlStudent
            try
            {
                Student model = new Student();
                int count = 0;
                List<SelectListItem> ddlStudent = new List<SelectListItem>();
                model.Fk_ParentId = Session["Pk_ParentID"].ToString();
                DataSet ds1 = model.GetStudentList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlStudent.Add(new SelectListItem { Text = "--Select Studdent--", Value = "0" });
                        }
                        ddlStudent.Add(new SelectListItem { Text = r["StudentName"].ToString(), Value = r["Pk_StudentID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlStudent = ddlStudent;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            return View(obj);
        }

        [HttpPost]
        [OnAction(ButtonName = "Apply")]
        [ActionName("ApplyLeave")]
        public ActionResult SaveLeave(Student model)
        {
            model.AddedBy = Session["Pk_ParentID"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.SaveLeave();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["ApplyLeave"] = "Leave applied Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["ApplyLeave"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("ApplyLeave");
        }


        public ActionResult LeaveList(Student model)
        {
            List<Student> list = new List<Student>();
            model.AddedBy = Session["Pk_ParentID"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Student obj = new Student();

                    obj.Reason = r["Reason"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.studentName = r["StudentName"].ToString();
                    obj.Status = r["IsApproved"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    list.Add(obj);
                }
                model.listStudent = list;
            }
            return View(model);
        }

        #endregion

        #region Complain

        public ActionResult Complains(Parent model)
        {
            #region Messages

            model.Pk_ParentID = Session["Pk_ParentID"].ToString();

            List<Parent> lst1 = new List<Parent>();

            DataSet ds11 = model.GetAllMessages();

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
                model.listparent = lst1;
            }
            #endregion Messages
            return View(model);
        }
        public ActionResult SaveMessages(string Message, string MessageBy)
        {
            Parent obj = new Parent();
            try
            {
                obj.Message = Message;
                obj.MessageBy = MessageBy;
                obj.Fk_UserId = Session["Pk_ParentID"].ToString();
                obj.AddedBy = Session["Pk_ParentID"].ToString();
                obj.Pk_MessageId = "0";
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