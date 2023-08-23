using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class ParentController : AdminBaseController
    {


        // GET: Parent
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
        public ActionResult ParentRegistration(string Pk_ParentID)
        {

            #region ddlheloccupation
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                DataSet ds1 = obj.GetOccupation();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                        }
                        ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlOccupation = ddlOccupation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlheloccupationmother
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                DataSet ds1 = obj.GetOccupation();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                        }
                        ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlOccupationmother = ddlOccupation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            if (Pk_ParentID != null)
            {
                Parent obj1 = new Parent();
                try
                {
                    obj1.Pk_ParentID = Pk_ParentID;
                    DataSet ds = obj1.ParentList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj1.Pk_ParentID = ds.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                        obj1.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                        obj1.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        obj1.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        obj1.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                        obj1.PinCode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                        obj1.State = ds.Tables[0].Rows[0]["State"].ToString();
                        obj1.City = ds.Tables[0].Rows[0]["City"].ToString();
                        obj1.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                        obj1.AadharNo = ds.Tables[0].Rows[0]["AadharNo"].ToString(); 
                        obj1.FatherOccupation = ds.Tables[0].Rows[0]["Fk_FatherOccupationID"].ToString();
                        obj1.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                        obj1.MotherOccupation = ds.Tables[0].Rows[0]["Fk_MotherOccupationID"].ToString();
                        obj1.correspondenceAddress = ds.Tables[0].Rows[0]["CorrespondenceAddress"].ToString();
                        obj1.correspondencPinCode = ds.Tables[0].Rows[0]["CorrespondencePinCode"].ToString();
                        obj1.correspondencState = ds.Tables[0].Rows[0]["CorrespondencState"].ToString();
                        obj1.correspondencCity = ds.Tables[0].Rows[0]["CorrespondencCity"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Parent"] = ex.Message;
                }
            
                return View(obj1);
            }


            else
            {
                return View();
            }

        }


        [HttpPost]
        [ActionName("ParentRegistration")]
        [OnAction(ButtonName = "btnsave")]

        public ActionResult SaveParentRegistration(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveParentRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        try
                        {
                            string login = " Your Login Id is :" + ds.Tables[0].Rows[0]["LoginID"].ToString();
                            string pass = " And Your Password is :" + ds.Tables[0].Rows[0]["Password"].ToString();
                            BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, obj.Mobile, login + pass);

                        }
                        catch { }
                        TempData["ParentReg"] = "Parent Inquiry is Saved successfully" + " Your LoginID is: " + ds.Tables[0].Rows[0]["LoginID"].ToString();
                        FormName = "ParentRegistration";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentReg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentRegistration";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentReg"] = ex.Message;
                FormName = "ParentRegistration";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("ParentRegistration")]
        [OnAction(ButtonName = "btnprint")]

        public ActionResult SaveandPrintParentRegistration(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveParentRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["ParentId"] = ds.Tables[0].Rows[0]["ParentId"].ToString();
                        TempData["ParentReg"] = "Parent Inquiry is Saved successfully" + " Your LoginID is: " + ds.Tables[0].Rows[0]["LoginID"].ToString();
                        FormName = "PrintReceipt";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentReg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentRegistration";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentReg"] = ex.Message;
                FormName = "ParentRegistration";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ParentList(Parent model)
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Parent List");
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
            List<Parent> list = new List<Parent>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.IsActive = model.IsActive == "All" ? null : model.IsActive;
                DataSet ds = model.ParentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Parent obj = new Parent();
                        obj.Pk_ParentID = r["Pk_ParentID"].ToString();
                        obj.ParentLogin_ID = r["LoginID"].ToString();
                        obj.ParentName = r["ParentName"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.PinCode = r["Pincode"].ToString();
                        obj.State = r["State"].ToString();
                        obj.City = r["City"].ToString();
                        obj.AddedOn = r["AddedOn"].ToString();
                        obj.PAN = r["PAN"].ToString();
                        obj.AadharNo = r["AadharNo"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.Password = r["Password"].ToString();
                        list.Add(obj);

                    }
                    model.listparent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Parent"] = ex.Message;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("ParentRegistration")]
        [OnAction(ButtonName = "btnUpdate")]

        public ActionResult UpdateParentList(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                 
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateParentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["ParentUpdate"] = "Parent Record is Successfully updated";
                        FormName = "ParentList";
                        Controller = "Parent";
                    }
                    else
                    {
                         
                        TempData["ParentUpdate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentList";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentUpdate"] = ex.Message;
                FormName = "ParentList";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult DeleteParentList(string Pk_ParentID, string DeletedBy)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                Parent obj = new Parent();
                obj.Pk_ParentID = Pk_ParentID;
                obj.DeletedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.DeleteParentList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ParentUpdate"] = "Parent Record Deleted is successfully";
                        FormName = "ParentList";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentUpdate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentList";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentUpdate"] = ex.Message;
                FormName = "ParentList";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PrintReceipt(string Pk_ParentID)
        {
            Parent model = new Parent();

            List<Parent> lst = new List<Parent>();
            List<Parent> lst1 = new List<Parent>();
            if (Session["ParentId"] != null)
            {
                model.Pk_ParentID = Session["ParentId"].ToString();
            }
            else
            {
                model.Pk_ParentID = Pk_ParentID;
            }
            DataSet ds0 = model.ParentEnquiryList();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {

                    
                    model.ParentLogin_ID = ds0.Tables[0].Rows[0]["LoginID"].ToString();
                    model.ParentName = ds0.Tables[0].Rows[0]["ParentName"].ToString();
                    model.Mobile = ds0.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Amount = ds0.Tables[0].Rows[0]["Amount"].ToString();
                    model.StudentName= ds0.Tables[0].Rows[0]["StudentName"].ToString();
                    model.Fk_ClassID = ds0.Tables[0].Rows[0]["ClassName"].ToString();
                    model.DisplayName = ds0.Tables[0].Rows[0]["FirstName"].ToString();
                    model.AddedOn = ds0.Tables[0].Rows[0]["RecDt"].ToString();   
                    model.FormNo=  ds0.Tables[0].Rows[0]["FormNo"].ToString();
                    ViewBag.LandLine = Common.SoftwareDetails.LandLine;
                    ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                    ViewBag.Website = Common.SoftwareDetails.Website;
                    ViewBag.EmailID = Common.SoftwareDetails.EmailID;
                    ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;
                    ViewBag.State = Common.SoftwareDetails.State1;
                    ViewBag.Pin1 = Common.SoftwareDetails.Pin1;
                    ViewBag.AffliateNo = Common.SoftwareDetails.AffliateNo;
                    
                }
            }
            return View(model);
        }


        #region Parent Enquiry

        public ActionResult ParentData(string MobileNo)
        {
            Parent model = new Parent();
            List<Parent> list = new List<Parent>();
            DataSet ds = model.GetEnquiryAmount();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();

            }
            Parent obj = new Parent();
            if (MobileNo != null)
            {
               
                try
                {
                    obj.MobileNo = MobileNo;
                    DataSet dsparent = obj.ParentEnquiryList();
                    if (dsparent != null && dsparent.Tables.Count > 0 && dsparent.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ParentID = dsparent.Tables[0].Rows[0]["Pk_ParentEnquiryID"].ToString();
                      
                        obj.Result = "1";
                        obj.ExistingMobile = dsparent.Tables[0].Rows[0]["Mobile"].ToString();
                        obj.ExistingAddress = dsparent.Tables[0].Rows[0]["Address"].ToString();
                        obj.ExistingParentName = dsparent.Tables[0].Rows[0]["ParentName"].ToString();
                        obj.ExistingAmount = model.Amount;

                    }
                    else
                    {
                        obj.Result = "0";
                    }
                }
                catch (Exception ex)
                {
                    obj.Result = "0";
                }
               
            }




            return Json(obj, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ActionName("ParentEnquiryNew")]
        [OnAction(ButtonName = "btnsave")]

        public ActionResult SaveParentEnquiry(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Amount = obj.FormNo == null ? "0" : obj.Amount;
                obj.Fk_SessionId = Session["SessionId"].ToString();
                DataSet ds = obj.SaveParentEnquiry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        try
                        {
                            string login = " Your Login Id is :" + ds.Tables[0].Rows[0]["LoginID"].ToString();
                           
                            BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, obj.Mobile, login   );

                        }
                        catch { }
                        TempData["ParentEnquiry"] = "Parent Inquiry is Saved successfully" + " Your LoginID is: " + ds.Tables[0].Rows[0]["LoginID"].ToString();
                        FormName = "ParentEnquiryNew";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentEnquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentEnquiryNew";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentEnquiry"] = ex.Message;
                FormName = "ParentEnquiryNew";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("ParentEnquiryNew")]
        [OnAction(ButtonName = "btnprint")]
        public ActionResult SaveandPrintParentEnquiry(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Fk_SessionId = Session["SessionId"].ToString();
                DataSet ds = obj.SaveParentEnquiry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["ParentId"] = ds.Tables[0].Rows[0]["ParentId"].ToString();
                        TempData["ParentEnquiry"] = "Parent Inquiry is Saved successfully" + " Your LoginID is: " + ds.Tables[0].Rows[0]["LoginID"].ToString();
                        FormName = "PrintReceipt";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentEnquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentEnquiryNew";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentEnquiry"] = ex.Message;
                FormName = "ParentEnquiryNew";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult GetEnquiryList(Parent model)
        {

            List<Parent> list = new List<Parent>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.IsActive = model.IsActive == "All" ? null : model.IsActive;
                DataSet ds = model.ParentEnquiryList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["dt"] = ds.Tables[0];
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Parent obj = new Parent();
                        obj.Pk_ParentID = r["Pk_ParentEnquiryID"].ToString();
                        obj.ParentLogin_ID = r["LoginID"].ToString();
                        obj.ParentName = r["ParentName"].ToString();
                        obj.StudentName = r["StudentName"].ToString();
                        obj.Fk_ClassID= r["ClassName"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                       obj.Amount= r["Amount"].ToString();
                        obj.AddedOn = r["AddedOn"].ToString();
                      

                        list.Add(obj);

                    }
                    model.listparent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Parent"] = ex.Message;
            }

            return View(model);
        }

        public ActionResult DeleteEnquiry(string PKID)
        {
            Parent model = new Parent();
            model.Pk_ParentID = PKID;
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeleteEnquiry();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["DeleteEnquiry"] = "Parent Enquiry Deleted Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["DeleteEnquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(); 
                }
            }
            return RedirectToAction("GetEnquiryList");
        }



        #endregion


        public ActionResult ParentEnQuiryNew()
        {
            Parent model = new Parent();
            List<Parent> list = new List<Parent>();
            DataSet ds = model.GetEnquiryAmount();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.ExistingAmount = ds.Tables[0].Rows[0]["Amount"].ToString();

            }
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
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
            return View(model);
        }

        public ActionResult GetParentList()
        {
            Parent obj = new Parent();
            List<Parent> lst = new List<Parent>();
            DataSet ds = obj.ParentList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Parent objList = new Parent();
                    objList.Mobile = dr["Mobile"].ToString();
                    objList.ParentName = dr["ParentName"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("ParentEnQuiryNew")]
        [OnAction(ButtonName = "btnSaveExisting")]

        public ActionResult SaveParentExisEnquiry(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Fk_SessionId = Session["SessionId"].ToString();
                DataSet ds = obj.SaveExistingParentEnquiry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["ParentEnquiry"] = "Parent Inquiry is Saved successfully" + " Your LoginID is: " + ds.Tables[0].Rows[0]["LoginID"].ToString();
                        FormName = "ParentEnQuiryNew";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentEnquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentEnQuiryNew";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentEnquiry"] = ex.Message;
                FormName = "ParentEnQuiryNew";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }


        [HttpPost]
        [ActionName("ParentEnQuiryNew")]
        [OnAction(ButtonName = "btnprintExisting")]
        public ActionResult SaveandExPrintParentEnquiry(Parent obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Fk_SessionId = Session["SessionId"].ToString();
                DataSet ds = obj.SaveExistingParentEnquiry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["ParentId"] = ds.Tables[0].Rows[0]["ParentId"].ToString();
                        TempData["ParentEnquiry"] = "Parent Inquiry is Saved successfully" + " Your LoginID is: " + ds.Tables[0].Rows[0]["LoginID"].ToString();
                        FormName = "PrintReceipt";
                        Controller = "Parent";
                    }
                    else
                    {
                        TempData["ParentEnquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ParentEnquiry";
                        Controller = "Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentEnquiry"] = ex.Message;
                FormName = "ParentEnquiry";
                Controller = "Parent";
            }
            return RedirectToAction(FormName, Controller);
        }


      
    }
}