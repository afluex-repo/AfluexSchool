using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class UserPermission : Common
    {
        public string Designation { get; set; }
        public string AdminName { get; set; }
        public string BranchName { get; set; }
        public string Branch { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string Pk_AdminId { get; set; }
        public string LoginId { get; set; }
        public List<SelectListItem> lstListDes { get; set; }
        public List<UserPermission> lstUser { get; set; }

        public string Fk_UserTypeId { get; set; }
        public string Fk_UserId { get; set; }
        public string Fk_FormTypeId { get; set; }
        public string Fk_FormId { get; set; }
        public string FormView { get; set; }
        public string FormSave { get; set; }
        public string FormUpdate { get; set; }
        public string FormDelete { get; set; }
        public string FormName { get; set; }

        public bool IsSaveValue { get; set; }
        public bool IsUpdateValue { get; set; }
        public bool IsSelectValue { get; set; }
        public bool IsDeleteValue { get; set; }
        public string SelectedValue { get; set; }

        public DataTable UserTypeFormPermisssion { get; set; }
        public List<UserPermission> lstpermission = new List<UserPermission>();
        public string UserImage { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string PANImage { get; set; }
        public string AddharNo { get; set; }
        public string AadharImage { get; set; }


        public DataSet GettingPermissionByName()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Designation)

            };
            DataSet ds = Connection.ExecuteQuery("GetPermissionByName", para);
            return ds;
        }
        public DataSet GetFormPermission()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@fk_userid", Fk_UserId),
                                      new SqlParameter("@Pk_FormTypeId", Fk_FormTypeId) };
            DataSet ds = Connection.ExecuteQuery("GetPemissionData", para);
            return ds;
        }
        public DataSet SavePermisssion()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UserTypeFormPermisssion", UserTypeFormPermisssion),
                                      new SqlParameter("@CreatedBy", AddedBy),
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@Fk_FormTypeId", Fk_FormTypeId)
                                  };
            DataSet ds = Connection.ExecuteQuery("SetFormPermission", para);
            return ds;
        }
        public DataSet SavingAdmin()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_BranchId",Branch),
                new SqlParameter("@Name",Name),
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@EmailId",EmailId),
                new SqlParameter("@Password",Password),
                new SqlParameter("@UserType",UserType),
                  new SqlParameter("@UserImage",UserImage),
                  new SqlParameter("@Address",Address),
                  new SqlParameter("@Pincode",PinCode),
                  new SqlParameter("@State",State),
                  new SqlParameter("@City",City),
                  new SqlParameter("@PAN",PAN),
                  new SqlParameter("@PANImage",PANImage),
                  new SqlParameter("@AddharNo",AddharNo),
                  new SqlParameter("@AadharImage",AadharImage),

            };
            DataSet ds = Connection.ExecuteQuery("SaveAdmin", para);
            return ds;
        }

        public DataSet GettingUserList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_AdminId",Pk_AdminId),
                new SqlParameter("@LoginId",LoginId),


                new SqlParameter("@Name",Name),

            };
            DataSet ds = Connection.ExecuteQuery("GetUserList", para);
            return ds;
        }



        public DataSet UpdatingUser()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_AdminId",Pk_AdminId),
                new SqlParameter("@Fk_BranchId",Branch),
                new SqlParameter("@Name",Name),
                new SqlParameter("@EmailId",EmailId),
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                 new SqlParameter("@FK_RoleID",UserType),

                  new SqlParameter("@UserImage",UserImage),
                  new SqlParameter("@Address",Address),
                  new SqlParameter("@Pincode",PinCode),
                  new SqlParameter("@State",State),
                  new SqlParameter("@City",City),
                  new SqlParameter("@PAN",PAN),
                  new SqlParameter("@PANImage",PANImage),
                  new SqlParameter("@AddharNo",AddharNo),
                  new SqlParameter("@AadharImage",AadharImage),
            };
            DataSet ds = Connection.ExecuteQuery("UpdateUser", para);
            return ds;
        }

        public DataSet DeletingUser()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_AdminId",Pk_AdminId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteUser", para);
            return ds;
        }


    }
}