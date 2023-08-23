using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class TeacherController : AdminBaseController
    {
        // GET: Teacher
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
        public ActionResult Teacher(string PK_TeacherID, string actionview)
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

            }
            model.ActionType = actionview;
            return View(model);
        }

        [HttpPost]
        [ActionName("Teacher")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult AddTeacherMaster(Teacher obj, IEnumerable<HttpPostedFileBase> Image )
        {
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
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.DOJ = string.IsNullOrEmpty(obj.DOJ) ? null : Common.ConvertToSystemDate(obj.DOJ, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.InsertTeacherRecord();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        try
                        {
                            string sms = "Teacher Registered Sucessfully.  Your Login Id is :" + ds.Tables[0].Rows[0]["LoginID"].ToString() + " and Password is :" + ds.Tables[0].Rows[0]["Password"].ToString();
                            BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, obj.MobileNo, sms);

                        }
                        catch { }
                        TempData["Teacher"] = "Teacher Added Successfully. " + "   LoginID  : " + ds.Tables[0].Rows[0]["LoginID"].ToString() + "   Password  : " + ds.Tables[0].Rows[0]["Password"].ToString();
                    }
                    else
                    {
                        TempData["Teacher"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Teacher"] = ex.Message;

            }

            return RedirectToAction("Teacher");
        }
        public ActionResult TeacherList(Teacher model)
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Teacher List");
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
            List<Teacher> lst1 = new List<Teacher>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetTeacherList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
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
                         obj.Password = r["Password"].ToString();
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

        [HttpPost]
        [ActionName("TeacherList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TeacherListBy(Teacher model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Teacher List");
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
            List<Teacher> lst1 = new List<Teacher>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetTeacherList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
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
                        obj.Password = r["Password"].ToString();
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
        public ActionResult DeleteTeacherList(string PK_TeacherID, string DeletedBy)
        {
            try
            {
                Teacher model = new Teacher();
                model.PK_TeacherID = PK_TeacherID;
                model.DeletedBy = Session["PK_AdminID"].ToString();
                DataSet ds = model.DeleteTeacherRecord();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "")
                    {
                        TempData["TeacherList"] = "Successfully Deleted";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["TeacherList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["TeacherList"] = ex.Message;
            }
            return RedirectToAction("TeacherList");
        }

        [HttpPost]
        [ActionName("Teacher")]
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
                model.UpdatedBy = Session["PK_AdminID"].ToString();
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.DOJ = string.IsNullOrEmpty(obj.DOJ) ? null : Common.ConvertToSystemDate(obj.DOJ, "dd/MM/yyyy");
                DataSet ds = model.UpdateTeacherRecord();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "TeacherList";
                        Controller = "Teacher";
                        TempData["TeacherList"] = "Teacher record updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        FormName = "TeacherList";
                        Controller = "Teacher";
                        TempData["TeacherList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["TeacherList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }





    }
}