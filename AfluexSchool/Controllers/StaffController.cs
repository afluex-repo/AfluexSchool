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
    public class StaffController : AdminBaseController
    {
        // GET: Staff
        #region designation

        public ActionResult StaffDesignation(string PK_StaffDesignationID)
        {
            Staff model = new Staff();


            if (PK_StaffDesignationID != null)
            {

                model.PK_StaffDesignationID = PK_StaffDesignationID;

                DataSet ds = model.StaffDesignationList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_StaffDesignationID = ds.Tables[0].Rows[0]["PK_StaffDesignationID"].ToString();
                    model.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();

                }
            }


            return View(model);
        }



        [HttpPost]
        [ActionName("StaffDesignation")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult BranchMaster(Staff model, string AddedBy)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.InsertStaffDesignation();
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Designation"] = "Designation is Successfully Added";
                    }
                    else
                    {
                        TempData["Designation"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Designation"] = ex.Message;

            }
            return RedirectToAction("StaffDesignation");
        }



        public ActionResult StaffDesignationList(Staff model)
        {


            List<Staff> lst1 = new List<Staff>();

            DataSet ds = model.StaffDesignationList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Staff obj = new Staff();
                        obj.PK_StaffDesignationID = r["PK_StaffDesignationID"].ToString();
                        obj.Designation = r["Designation"].ToString();
                        Session["PK_StaffID"] = null;
                        lst1.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                model.lstSDesignationList = lst1;
            }
            return View(model);
        }


        public ActionResult DeleteStaffDesignation(string PK_StaffDesignationID)
        {
            try
            {
                Staff model = new Staff();
                model.PK_StaffDesignationID = PK_StaffDesignationID;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteStaffDesignation();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "")
                    {
                        TempData["StaffDesignationList"] = "Designation is Successfully Deleted";
                    }
                    else
                    {
                        TempData["StaffDesignationList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["StaffDesignationList"] = ex.Message;
            }
            return RedirectToAction("StaffDesignationList");
        }




        [HttpPost]
        [ActionName("StaffDesignation")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateStaff(string PK_StaffDesignationID, string Designation)
        {
            string FormName = "";
            string Controller = "";
            Staff obj = new Staff();
            try
            {
                obj.PK_StaffDesignationID = PK_StaffDesignationID;
                obj.Designation = Designation;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString(); ;
                DataSet ds = obj.UpdateStaffDesignation();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "StaffDesignationList";
                        Controller = "Staff";

                        TempData["StaffDesignationList"] = "Designation Updated Successfully!";
                    }
                    else
                    {
                        Session["PK_StaffDesignationID"] = PK_StaffDesignationID;
                        FormName = "StaffDesignationList";
                        Controller = "Staff";
                        TempData["StaffDesignationList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["StaffDesignationList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region Staff
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

        public ActionResult AddStaff(string PK_StaffID)
        {


            #region ddlBranch
            try
            {
                Staff obj1 = new Staff();
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
                Staff obj1 = new Staff();
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


            #region ddlDesignation
            try
            {
                Staff obj1 = new Staff();
                int count = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet ds1 = obj1.StaffDesignationList();
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



            Staff model = new Staff();
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
                    model.Religion = ds.Tables[0].Rows[0]["FK_ReligionID"].ToString();

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
                    model.BranchName = ds.Tables[0].Rows[0]["FK_BranchID"].ToString();
                    model.Designation = ds.Tables[0].Rows[0]["FK_StaffDesignationID"].ToString();

                }

            }

            return View(model);
        }

        [HttpPost]
        [ActionName("AddStaff")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult AddStaff(Staff obj, IEnumerable<HttpPostedFileBase> Image, string AddedBy)
        {
            try
            {
                foreach (var file in Image)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.Image = "../Staff/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
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
                DataSet ds = obj.InsertStaffRecord();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        //try
                        //{
                        //    string str2 = BLAPSSchool.StaffRegistration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginID"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                        //    BLAPSSchool.SendSMS(obj.MobileNo, str2);
                        //}
                        //catch { }

                        TempData["AddStaff"] = "Staff Record Is Successfully Added";
                    }
                    else
                    {
                        TempData["AddStaff"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AddStaff"] = ex.Message;
            }
            return RedirectToAction("AddStaff");
        }
         
        public ActionResult StaffList(Staff model)
        {
            List<Staff> lst1 = new List<Staff>();
            DataSet ds = model.GetStaffList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Staff obj = new Staff();
                        obj.PK_StaffID = r["PK_StaffID"].ToString();
                        obj.Name = r["Name"].ToString();

                        obj.DOB = r["DOB"].ToString();
                        obj.Gender = r["Gender"].ToString();
                        obj.Category = r["Category"].ToString();



                        obj.Address = r["Address"].ToString();


                        obj.MobileNo = r["MobileNo"].ToString();

                        obj.DOJ = r["DOJ"].ToString();
                        obj.LoginID = r["LoginID"].ToString();

                        Session["PK_StaffID"] = null;

                        lst1.Add(obj);


                    }

                }
                catch (Exception ex)
                {

                }
                model.lstSDesignationList = lst1;
            }
            return View(model);
        }

        public ActionResult DeleteStaffList(string PK_StaffID, string DeletedBy)
        {
            try
            {
                Staff model = new Staff();
                model.PK_StaffID = PK_StaffID;
                model.DeletedBy = Session["PK_AdminID"].ToString();
                DataSet ds = model.DeleteStaffList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "")
                    {
                        TempData["StaffList"] = "Staff Record Is Successfully Deleted";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["StaffList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["StaffList"] = ex.Message;
            }
            return RedirectToAction("StaffList");
        }

        [HttpPost]
        [ActionName("AddStaff")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateStaffList(Staff model, string PK_StaffID, string Designation, string BranchName,
            string Address, string PinCode, string FatherName, string DOB, string Gender, string Religion, string Category, string Qualification,
            string Experience, string MobileNo, string DOJ, string Image, string Name, string EmailID)
        {
            string FormName = "";
            string Controller = "";
            Staff obj = new Staff();
            try
            {
                model.PK_StaffID = PK_StaffID;
                model.Name = Name;
                model.Designation = Designation;
                model.BranchName = BranchName;
                model.Address = Address;
                model.PinCode = PinCode;
                model.FatherName = FatherName;
                model.DOB = DOB;
                model.Gender = Gender;
                model.Religion = Religion;
                model.Category = Category;
                model.Qualification = Qualification;
                model.Experience = Experience;
                model.MobileNo = MobileNo;
                model.DOJ = DOJ;
                model.Image = Image;
                model.EmailID = EmailID;
                model.UpdatedBy = Session["PK_AdminID"].ToString();
                DataSet ds = model.UpdateStaffRecord();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "StaffList";
                        Controller = "Staff";
                        TempData["StaffList"] = "Staff Record Is Successfully Updated";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        Session["PK_StaffID"] = PK_StaffID;
                        FormName = "AddStaff";
                        Controller = "Staff";
                        TempData["StaffList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["StaffList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion
    }
}