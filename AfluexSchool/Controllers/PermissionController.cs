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
    public class PermissionController : AdminBaseController
    {

        #region UserRegistration
        public ActionResult UserRegistration(string Pk_AdminId)
        {
            UserPermission model = new UserPermission();
            Common obj = new Common();
            #region BindBranch

            int count1 = 0;
            List<SelectListItem> ddlbranch1 = new List<SelectListItem>();
            DataSet ds2 = obj.BranchList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlbranch1.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch1.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchId"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlbranch1 = ddlbranch1;

            #endregion BindBranch


            #region BindUserType

           
            List<SelectListItem> ddlusertype = new List<SelectListItem>();
            ddlusertype.Add(new SelectListItem { Text = "Select UserType", Value = "0" });
            ds2 = obj.UserTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[1].Rows)
                {
                  
                       
                    
                    ddlusertype.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlusertype = ddlusertype;

            #endregion BindUserType
            if (Pk_AdminId != null)
            {
               

                model.Pk_AdminId = Pk_AdminId;
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
                    TempData["AdminLogin"] = ex.Message;
                }
            }
            return View(model);
        }



        [HttpPost]
        [OnAction(ButtonName = "saveUser")]
        [ActionName("UserRegistration")]
        public ActionResult SaveUser(UserPermission model, IEnumerable<HttpPostedFileBase> Files)
        {
            int count = 0;
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                Random rnd = new Random();
                string pass = rnd.Next(111111, 999999).ToString();
                model.Password = (pass);
                 
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

               

                DataSet ds = model.SavingAdmin();

                string sms = "User Registered Sucessfully.  Your Login Id and password for User login is :" + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and Password is :" + ds.Tables[0].Rows[0]["Password"].ToString();
                BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, model.MobileNo, sms);


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["AdminLogin"] = "User Registered successfully . UserId :" + ds.Tables[0].Rows[0]["LoginId"].ToString() + "   Password  : " + ds.Tables[0].Rows[0]["Password"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["AdminLogin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AdminLogin"] = ex.Message;
                 
            }
            return RedirectToAction("UserRegistration");
        }

        public ActionResult UserList(UserPermission model)
        {
           
            #region BindBranch
            Common obj1 = new Common();
           


            int count = 0;
            List<SelectListItem> ddlbranch = new List<SelectListItem>();
            DataSet ds1 = obj1.BranchList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlbranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlbranch = ddlbranch;
           

            #endregion BindBranch

            List<UserPermission> lst = new List<Models.UserPermission>();
            DataSet ds = model.GettingUserList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserPermission obj = new UserPermission();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Branch = r["Fk_BranchId"].ToString();
                    obj.EmailId = r["EmailId"].ToString();
                    obj.MobileNo = r["MobileNo"].ToString();
                    obj.Pk_AdminId = r["Pk_AdminId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = r["Password"].ToString();
                    obj.UserImage = r["UserImage"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PinCode = r["Pincode"].ToString();
                    obj.State = r["State"].ToString();
                    obj.City = r["City"].ToString();
                    obj.PAN = r["PAN"].ToString();
                    obj.PANImage = r["PANImage"].ToString();
                    obj.AddharNo = r["AddharNo"].ToString();
                    obj.AadharImage = r["AadharImage"].ToString();
                    obj.UserType = r["UserType"].ToString();
                    lst.Add(obj);
                }
                model.lstUser = lst;
            }

            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "updateUser")]
        [ActionName("UserRegistration")]
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
                model.Pk_AdminId = model.Pk_AdminId;
                DataSet ds = model.UpdatingUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["UserList"] = "User Updated Successfully.";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["UserList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["UserList"] = ex.Message;
            }
           

            return RedirectToAction("UserList");
        }

        public ActionResult DeleteUser(string Pk_AdminId)
        {
            UserPermission model = new UserPermission();
            model.DeletedBy = Session["Pk_AdminId"].ToString();
            model.Pk_AdminId = Pk_AdminId;
            DataSet ds = model.DeletingUser();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["UserList"] = "User Deleted";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["UserList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("UserList");
        }

        [HttpPost]
        [OnAction(ButtonName = "searchUser")]
        [ActionName("UserList")]
        public ActionResult SearchUser(UserPermission model)
        {
            #region BindBranch
            Common obj3 = new Common();
            
            int count2 = 0;
            List<SelectListItem> ddlbranch = new List<SelectListItem>();
            DataSet ds2 = obj3.BranchList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r1 in ds2.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlbranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlbranch.Add(new SelectListItem { Text = r1["BranchName"].ToString(), Value = r1["Pk_BranchId"].ToString() });
                    count2 = count2 + 1;
                }
            }

            ViewBag.ddlbranch = ddlbranch;
           

            #endregion BindBranch

            List<UserPermission> lst = new List<UserPermission>();
            model.Branch = model.Branch == "0" ? null : model.Branch;

            DataSet ds = model.GettingUserList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["dt"] = ds.Tables[0];
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    UserPermission obj = new UserPermission();
                    obj.MobileNo = r["MobileNo"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Branch = r["Fk_BranchId"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = r["Password"].ToString();
                    obj.EmailId = r["EmailId"].ToString();
                    obj.Pk_AdminId = r["Pk_AdminId"].ToString();
                    lst.Add(obj);
                }
                model.lstUser = lst;
            }
            return View(model);


        }
        #endregion

        public ActionResult SetPermission(UserPermission model)
        {

            DataSet ds1 = new DataSet();

            #region ddlformtype
            Common obj = new Common();
            List<SelectListItem> ddlformtype = new List<SelectListItem>();
            ds1 = obj.BindFormTypeMaster();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                { ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() }); }
            }

            ViewBag.ddlformtype = ddlformtype;

            #endregion
            #region ddluser

            List<SelectListItem> ddluser = new List<SelectListItem>();
            UserPermission emp = new UserPermission();
            ds1 = emp.GettingUserList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[1].Rows)
                { ddluser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() }); }
            }
            else
            {
                ddluser.Add(new SelectListItem { Text = "Select User", Value = "0" });
            }

            ViewBag.ddluser = ddluser;

            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("SetPermission")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPermission(UserPermission obj)
        {
            UserPermission model = new UserPermission();
            List<UserPermission> lst = new List<UserPermission>();
            DataSet ds = obj.GetFormPermission();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserPermission ob = new UserPermission();
                    ob.FormName = dr["FormName"].ToString();
                    ob.IsSelectValue = Convert.ToBoolean(dr["FormView"].ToString());
                    ob.IsUpdateValue = Convert.ToBoolean(dr["FormUpdate"].ToString());
                    ob.IsDeleteValue = Convert.ToBoolean(dr["FormDelete"].ToString());
                    if (ob.IsSelectValue == false)
                    {
                        ob.SelectedValue = "";
                    }
                    else
                    {
                        ob.SelectedValue = "checked";
                    }
                    if (ob.IsUpdateValue == false)
                    {
                        ob.FormUpdate = "";
                    }
                    else
                    {
                        ob.FormUpdate = "checked";
                    }
                    if (ob.IsDeleteValue == false)
                    {
                        ob.FormDelete = "";
                    }
                    else
                    {
                        ob.FormDelete = "checked";
                    }
                    ob.IsSaveValue = Convert.ToBoolean(dr["FormSave"].ToString());
                    
                    ob.Fk_FormId = dr["PK_FormId"].ToString();
                    ob.Fk_FormTypeId = dr["pk_formtypeid"].ToString();
                    ob.Fk_UserId = dr["Fk_UserId"].ToString();
                    lst.Add(ob);
                }
                model.lstpermission = lst;
            }
            DataSet ds1 = new DataSet();

            #region ddlformtype
            Common obj1 = new Common();
            List<SelectListItem> ddlformtype = new List<SelectListItem>();
            ds1 = obj1.BindFormTypeMaster();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                { ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() }); }
            }

            ViewBag.ddlformtype = ddlformtype;

            #endregion
            #region ddluser

            List<SelectListItem> ddluser = new List<SelectListItem>();
            UserPermission emp = new UserPermission();
            ds1 = emp.GettingUserList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[1].Rows)
                { ddluser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() }); }
            }

            ViewBag.ddluser = ddluser;

            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("SetPermission")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePermission(UserPermission obj)
        {
            string hdrows = Request["hdRows"].ToString();
            string chkSave = "";
            string chkupdate = "";
            string chkdelete = "";
            string chkselect = "";
            string hdfformtypeid = "";
            string hdfformid = "";
            string hdfloginid = "";
            DataTable dtpermission = new DataTable();

            dtpermission.Columns.Add("Fk_FormTypeId");
            dtpermission.Columns.Add("Fk_FormId");
            dtpermission.Columns.Add("IsSave");
            dtpermission.Columns.Add("IsUpdate");
            dtpermission.Columns.Add("IsDelete");
            dtpermission.Columns.Add("IsSelect");
            for (int i = 1; i < int.Parse(hdrows); i++)
            {

                try
                {
                    chkselect = Request["chkSelect_ " + i].ToString(); 
                   
                }
                catch { chkselect = "0"; }
                try
                {
                    chkupdate = Request["chkEdit_ " + i].ToString();
                   
                }
                catch
                {
                    chkupdate = "0";
                }
                try
                {
                    chkdelete = Request["chkDelete_ " + i].ToString();
                }
                catch
                {
                    chkdelete = "0";
                }
                hdfformtypeid = Request["hdFormtypeId_ " + i].ToString();
                hdfformid = Request["hdFormId_ " + i].ToString();
                hdfloginid = Request["hdLoginid_ " + i].ToString();
               
                dtpermission.Rows.Add(hdfformtypeid, hdfformid,   chkselect == "on" ? "1" : "0", "0", chkupdate == "on" ? "1" : "0", chkdelete == "on" ? "1" : "0");

            }

            obj.UserTypeFormPermisssion = dtpermission;
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            obj.Fk_UserId = hdfloginid;
            obj.Fk_FormTypeId = hdfformtypeid;
            DataSet ds = obj.SavePermisssion();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Permission"] = "Permission set successfully.";
                }
                else
                {
                    TempData["Permission"] = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
            }

            return RedirectToAction("SetPermission");

        }

    }
}