using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfluexSchool.Models
{
    public class Home : Common
    {
        public string Email { get; set; }
        public string Pk_AdminId { get; set; }
        public DataTable PermissionDBSet { get; set; }
        public string UserType { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string Url { get; set; }
        public string Icons { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuId { get; set; }
        public List<Home> lstMenu { get; set; }
        public List<Home> lstsubmenu { get; set; }
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public string Session { get; set; }
        public string SessionId { get; set; }
        public string ParentName { get; set; }
        public string Pk_ParentID { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string StudentName { get; set; }
        public string Fk_ClassID { get; set; }
        public string FormNo { get; set; }
        public string Image { get; set; }
        public string Fk_SessionId { get; set; }
        public string PreviousSchool { get; set; }

        public DataSet GetUsersType()
        {
            DataSet ds = Connection.ExecuteQuery("GetUsersType");
            return ds;
        }
        public DataSet GetSession()
        {
            DataSet ds = Connection.ExecuteQuery("GetSession");
            return ds;
        }
        public DataSet Login()
        {

            SqlParameter[] para =
                            {
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@Password",Password),
                                       new SqlParameter("@UserType",UserType),

                            };
            DataSet ds = Connection.ExecuteQuery("Login", para);
            return ds;
        }
        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = Connection.ExecuteQuery("GetMenuUserWise", para);
            return ds;
        }
        public static Home GetMenus(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Url = r["Url"].ToString();
                        obj.Icons = r["icons"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }

        #region ForgotPassword

        public DataSet GettingPassword()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UserType",UserType),
                new SqlParameter("@LoginId",LoginId),
                 new SqlParameter("@MobileNo",MobileNo)
            };
            DataSet ds = Connection.ExecuteQuery("GetPassword", para);
            return ds;
        }

        public DataSet ValidatingForgotDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UserType",UserType),
                new SqlParameter("@LoginId",LoginId),
                 new SqlParameter("@MobileNo",MobileNo)
            };
            DataSet ds = Connection.ExecuteQuery("GetPassword", para);
            return ds;
        }

        #endregion

        public DataSet SaveParentEnquiry()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Address",Address),
                                    new SqlParameter("@StudentName",StudentName),
                                    new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                    new SqlParameter("@FormNo",FormNo),
                                    new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Fk_SessionId",Fk_SessionId),
                                       new SqlParameter("@Email",Email),
                                       new SqlParameter("@Image",Image),
                                       new SqlParameter("@PreviousSchool",PreviousSchool),
                                };
            DataSet ds = Connection.ExecuteQuery("Enquiry", para);
            return ds;

        }
    }
}