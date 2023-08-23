using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using AfluexSchool.Filter;
using System.IO;

namespace AfluexSchool.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            Session.Abandon();
            List<SelectListItem> ddlUserType = new List<SelectListItem>();
            try
            {
                Home obj1 = new Home();
                int count = 0;
               
                DataSet ds1 = obj1.GetUsersType();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlUserType.Add(new SelectListItem { Text = "Select User Type", Value = "0" });
                        }
                        ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["Pk_UserTypeID"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlUserType = ddlUserType;
            }
            catch (Exception ex)
            {
                ddlUserType.Add(new SelectListItem { Text = "Select User Type", Value = "0" });
                ViewBag.ddlUserType = ddlUserType;
                TempData["LoginError"] = ex.Message;
            }
            return View();
        }

        public ActionResult LoginAction(Home model)
        {
            string Controller = "";
            string FormName = "";
            try
            {
                DataSet dsLogin = model.Login();
                if (dsLogin != null && dsLogin.Tables[0].Rows.Count > 0)
                {
                    if (dsLogin.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        
                        if (dsLogin.Tables[0].Rows[0]["UserType"].ToString() == "Parent")
                        {
                            Session["Pk_ParentID"] = dsLogin.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                            Session["LoginID"] = dsLogin.Tables[0].Rows[0]["LoginID"].ToString();
                            Session["Name"] = dsLogin.Tables[0].Rows[0]["ParentName"].ToString();
                            Session["UserImage"] = "../../AdminPanelcss/assets/img/user1.png";
                            Controller = "ParentPanel";
                            FormName = "DashBoard";
                        }
                        else if (dsLogin.Tables[0].Rows[0]["UserType"].ToString() == "Teacher")
                        {
                            Session["PK_TeacherID"] = dsLogin.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                            Session["LoginID"] = dsLogin.Tables[0].Rows[0]["LoginID"].ToString();
                            Session["Name"] = dsLogin.Tables[0].Rows[0]["Name"].ToString();
                            Session["UserImage"] = dsLogin.Tables[0].Rows[0]["ImagePath"].ToString();
                            if (dsLogin.Tables[1].Rows.Count > 0)
                            {
                                Session["ClassID"] = dsLogin.Tables[1].Rows[0]["Fk_ClassId"].ToString();
                                Session["SectionID"] = dsLogin.Tables[1].Rows[0]["Fk_SectionId"].ToString();

                            }
                            Controller = "TeacherLogin";
                            FormName = "Dashboard";
                        }
                        else if (dsLogin.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["LoginError"] = dsLogin.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            Controller = "Home";
                            FormName = "Login";
                        }

                       
                    }
                    else if (dsLogin.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["LoginError"] = dsLogin.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        Controller = "Home";
                        FormName = "Login";
                    }
                }
                else
                {
                    TempData["LoginError"] = "Invalid LoginId and Password";
                    Controller = "Home";
                    FormName = "Login";
                }
            }
            catch (Exception ex)
            {
                TempData["LoginError"] = ex.Message;
                Controller = "Home";
                FormName = "Login";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult AdminLogin()
        {
            Home obj1 = new Home();
            //Session.Abandon();
            List<SelectListItem> ddlsession = new List<SelectListItem>();
            try
            {
              
                int count = 0;

                DataSet ds1 = obj1.GetSession();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            obj1.Session = ds1.Tables[2].Rows[0]["Pk_SessionId"].ToString();
                        }
                        ddlsession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlsession = ddlsession;
                obj1.Session = ds1.Tables[2].Rows[0]["Pk_SessionId"].ToString();
                obj1.SessionId = ds1.Tables[2].Rows[0]["SessionName"].ToString();
            }
            catch (Exception ex)
            {
                ddlsession.Add(new SelectListItem { Text = "Select User Type", Value = "0" });
                ViewBag.ddlUserType = ddlsession;
                TempData["LoginError"] = ex.Message;
            }
            return View(obj1);
        }


        public ActionResult LoginActionAdmin(Home model)
        {
            string Controller = "";
            string FormName = "";
            try
            {
                model.UserType = "1";
                DataSet dsLogin = model.Login();
                if (dsLogin != null && dsLogin.Tables[0].Rows.Count > 0)
                {
                    if (dsLogin.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (dsLogin.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            Session["PK_AdminID"] = dsLogin.Tables[0].Rows[0]["PK_UserID"].ToString();
                            Session["RoleName"] = dsLogin.Tables[0].Rows[0]["RoleName"].ToString();
                            Session["UserType"] = dsLogin.Tables[0].Rows[0]["RoleName"].ToString();
                            Session["FullName"] = dsLogin.Tables[0].Rows[0]["FirstName"].ToString();
                            Session["UserImage"] = dsLogin.Tables[0].Rows[0]["UserImage"].ToString();
                            Session["SessionId"] = model.Session;
                            Session["SessionName"] = model.SessionId;
                            model.PermissionDBSet = dsLogin.Tables[1];
                            Sessions.PermissionDBSet = model.PermissionDBSet;

                            Controller = "Admin";
                            FormName = "Dashboard";
                        }

                       
                        else if (dsLogin.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["LoginError"] = dsLogin.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            Controller = "Home";
                            FormName = "AdminLogin";
                        }


                    }
                    else if (dsLogin.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["LoginError"] = dsLogin.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        Controller = "Home";
                        FormName = "AdminLogin";
                    }
                }
                else
                {
                    TempData["LoginError"] = "Invalid LoginId and Password";
                    Controller = "Home";
                    FormName = "AdminLogin";
                }
            }
            catch (Exception ex)
            {
                TempData["LoginError"] = ex.Message;
                Controller = "Home";
                FormName = "AdminLogin";
            }
            return RedirectToAction(FormName, Controller);
        }
        public virtual PartialViewResult Menu()
        {
            Home Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Home)Session["_Menu"];
            }
            else
            {

                Menu = Home.GetMenus(Session["PK_AdminID"].ToString(), Session["RoleName"].ToString()); // pass employee id here
                Session["_Menu"] = Menu;
            }
            return PartialView("_Menu", Menu);
        }

        #region ForgotPassword

        public ActionResult ForgotPassword(Home model)
        {
            try
            {
                Home obj1 = new Home();
                int count = 0;
                List<SelectListItem> ddlUserType = new List<SelectListItem>();
                DataSet ds1 = obj1.GetUsersType();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlUserType.Add(new SelectListItem { Text = "Select User Type", Value = "0" });
                        }
                        ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["Pk_UserTypeID"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlUserType = ddlUserType;
            }
            catch (Exception ex)
            {
                TempData["ForgotPassword"] = ex.Message;
            }
            return View(model);
        }
        
    
        public ActionResult GetPassword(string UserType, string LoginId, string MobileNo)
        {
            Home model = new Home();
            model.UserType = UserType;
            model.LoginId = LoginId;
            model.MobileNo = MobileNo;
            SendSMS obj =new SendSMS();
            string Status = "";
            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("Name", typeof(string));
            dtSMS.Columns.Add("Mobile", typeof(string));
            dtSMS.Columns.Add("Status", typeof(string));
            DataSet ds = model.GettingPassword();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    string pass = "Dear "+ ds.Tables[0].Rows[0]["Name"].ToString() + " ,Your Password is :" + ds.Tables[0].Rows[0]["Password"].ToString();
                    BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, model.MobileNo, pass);

                    if (model.MobileNo.Length < 10)
                    {
                        Status = "Failed";
                    }
                    else if (model.MobileNo.Length > 10)
                    {
                        Status = "Failed";
                    }
                    else
                    {
                        Status = "Send";
                    }

                    dtSMS.Rows.Add(ds.Tables[0].Rows[0]["Name"].ToString(), model.MobileNo, Status);
                    obj.dtSMS = dtSMS;
                    obj.AddedBy = "1";
                    obj.SMS = pass;
                    obj.TotalSMS = "1";
                    obj.SMSTemplateText = "Forget Password";
                    DataSet ds1 = obj.SaveSMSData();
                    model.Result = "1";

                }
                else if(ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["ForgotPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    
        public ActionResult GetOtp(string UserType, string LoginId, string MobileNo)
        {
            Home model = new Home();
            model.UserType = UserType;
            model.LoginId = LoginId;
            model.MobileNo = MobileNo;
            Random rnd = new Random();
            string otpass = rnd.Next(1111, 9999).ToString();
            try
            {
                DataSet ds = model.GettingPassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                        model.OTP = otpass;
                        BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, model.MobileNo, otpass);
                        model.Result = "1"; 
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                       
                    }

                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Website
        public ActionResult Index()
        {
            

            return View();
        }
        public ActionResult aboutus()
        {
            

            return View();
        }

        public ActionResult Contactus()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult addmission()
        {
            ViewBag.Message = "Your admission page.";

            return View();
        }
        public ActionResult commingsoon()
        {
            ViewBag.Message = "Your commingsoon page.";

            return View();
        }
        public ActionResult gallery()
        {
            ViewBag.Message = "Your gallery page.";

            return View();
        }

        public ActionResult staff()
        {
            ViewBag.Message = "Your gallery page.";

            return View();
        }
     

        #endregion Website

        public ActionResult NoPermission()
        {
            return View();
        }

        public ActionResult Enquiry(Home model)
        {
            List<Home> list = new List<Home>();

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

        [HttpPost]
        [ActionName("Enquiry")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveandPrintParentEnquiry(HttpPostedFileBase photo, Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    obj.Image = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(photo.FileName);
                    photo.SaveAs(Path.Combine(Server.MapPath(obj.Image)));
                }
                obj.AddedBy = "0";
                DataSet ds = obj.SaveParentEnquiry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["Enquiry"] = "Enquiry is Saved successfully";
                        FormName = "ThankYou";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["ParentEnquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Enquiry";
                        Controller = "Home";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ParentEnquiry"] = ex.Message;
                FormName = "Enquiry";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ThankYou(Home model)
        {
            return View(model);
        }
    }
}