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
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            DashBoard newdata = new DashBoard();
            newdata.Session= Session["SessionId"].ToString();
            DataSet Ds = newdata.GetDashBoardDetails();
            ViewBag.TotalParent = Ds.Tables[0].Rows[0]["TotalParent"].ToString();
            ViewBag.TotalStudent = Ds.Tables[1].Rows[0]["TotalStudent"].ToString();
            ViewBag.TotalStaff = Ds.Tables[2].Rows[0]["TotalStaff"].ToString();
            ViewBag.TotalTeacher = Ds.Tables[3].Rows[0]["TotalTeacher"].ToString();

            ViewBag.TotalSMS = Ds.Tables[4].Rows[0]["TotalSMS"].ToString();
            ViewBag.TotalSendSMS = Ds.Tables[4].Rows[0]["TotalSendSMS"].ToString();
            ViewBag.TotalUnSendSMS = Ds.Tables[4].Rows[0]["TotalUnSendSMS"].ToString();
            ViewBag.Balance = (decimal.Parse(Ds.Tables[4].Rows[0]["TotalSMS"].ToString()) - decimal.Parse(Ds.Tables[4].Rows[0]["TotalSendSMS"].ToString())).ToString();

            List<DashBoard> list = new List<DashBoard>();
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[8].Rows.Count > 0)
            {
                foreach (DataRow r in Ds.Tables[8].Rows)
                {
                    DashBoard obj = new DashBoard();
                    obj.Name = r["Name"].ToString();
                    obj.ImagePath = r["ImagePath"].ToString();
                    obj.Qualification = r["Qualification"].ToString();

                   
                    list.Add(obj);

                }
                newdata.lstteacher = list;
            }
           
            return View(newdata);
        }
        public ActionResult BindSMSChart()
        {
            List<DashBoard> dataList = new List<DashBoard>();

            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();
            newdata.Session = Session["SessionId"].ToString();
            DataSet Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[5].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[5].Rows)
                {
                    DashBoard details = new DashBoard();

                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());

                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindStaffChart()
        {
            List<DashBoard> dataList = new List<DashBoard>();

            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();
            newdata.Session = Session["SessionId"].ToString();
            DataSet Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                
                int count = 0;
                foreach (DataRow dr in Ds.Tables[6].Rows)
                {
                    DashBoard details = new DashBoard();

                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());

                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindParentChart()
        {
            List<DashBoard> dataList = new List<DashBoard>();

            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();
            newdata.Session = Session["SessionId"].ToString();
            DataSet Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[7].Rows)
                {
                    DashBoard details = new DashBoard();

                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());

                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }


        #region editprofile
        public ActionResult EditProfile(UserPermission model)
        {
            model.Pk_AdminId = Session["Pk_AdminId"].ToString();
            try
            {
                DataSet ds = model.GettingUserList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Branch = ds.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    model.UserType = ds.Tables[0].Rows[0]["FK_RoleID"].ToString();

                    model.UserImage = ds.Tables[0].Rows[0]["UserImage"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                    model.PANImage = ds.Tables[0].Rows[0]["PANImage"].ToString();
                    model.AddharNo = ds.Tables[0].Rows[0]["AddharNo"].ToString();
                    model.AadharImage = ds.Tables[0].Rows[0]["AadharImage"].ToString();


                }
            }
            catch (Exception ex)
            {
                TempData["EditProfil"] = ex.Message;
            }

            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnUpdate")]
        [ActionName("EditProfile")]
        public ActionResult UpdateUser(UserPermission model, IEnumerable<HttpPostedFileBase> Files)
        {
            try
            {
                int count = 0;
                
                {
                    foreach (var file in Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            if (count == 0)
                            {
                                model.UserImage = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                                file.SaveAs(Path.Combine(Server.MapPath(model.UserImage)));
                            }
                            if (count == 1)
                            {
                                model.PANImage = "/PANImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                                file.SaveAs(Path.Combine(Server.MapPath(model.PANImage)));
                            }
                            if (count == 2)
                            {
                                model.AadharImage = "/AadharImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                                file.SaveAs(Path.Combine(Server.MapPath(model.AadharImage)));
                            }
                        }
                        count++;
                    }
                }
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.Pk_AdminId = Session["Pk_AdminId"].ToString();


                DataSet ds = model.UpdatingUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EditProfil"] = " Updated Successfully.";
                       
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["EditProfil"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["EditProfil"] = ex.Message;
            }
          

            return RedirectToAction("EditProfile");
        }
        #endregion

        #region change pwd
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

                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                 
                DataSet ds = model.UpdateAdminPassword();
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
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangePassword"] = ex.Message;
                FormName = "ChangePassword";
                Controller = "Admin";
            }
            return RedirectToAction(FormName, Controller);

        }


        public ActionResult ChangeTxnPassword(ChangePassword model)
        {

            return View(model);
        }
        [HttpPost]
        [ActionName("ChangeTxnPassword")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SAveChangeTxnPassword(ChangePassword model)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdateAdminTxnPassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangeTxnPassword"] = "Transaction Password is changed successfully";
                        FormName = "ChangeTxnPassword";
                        Controller = "Admin";
                    }
                    else
                    {
                        TempData["ChangeTxnPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangeTxnPassword";
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangeTxnPassword"] = ex.Message;
                FormName = "ChangeTxnPassword";
                Controller = "Admin";
            }
            return RedirectToAction(FormName, Controller);

        }
        #endregion
    }
}