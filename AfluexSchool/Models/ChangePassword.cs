using System.Data;
using System.Data.SqlClient;

namespace AfluexSchool.Models
{
    public class ChangePassword : Common
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Pk_ParentID { get; set; }
        public string PK_TeacherID { get; set; }

        public DataSet UpdateParentPassword()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@OldPassword",OldPassword),
                                    new SqlParameter("@NewPassword",NewPassword),
                                     new SqlParameter("@Pk_ParentID",UpdatedBy),
                                       new SqlParameter("@UpdatedBy",Pk_ParentID)
                               };
            DataSet ds = Connection.ExecuteQuery("ChangeParentPassword", para);
            return ds;

        }
        public DataSet UpdateAdminPassword()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@OldPassword",OldPassword),
                                    new SqlParameter("@NewPassword",NewPassword),
                                     new SqlParameter("@Pk_AdminID",UpdatedBy),
                                       new SqlParameter("@UpdatedBy",UpdatedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("ChangeAdminPassword", para);
            return ds;

        }

        public DataSet UpdateTeacherPassword()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@OldPassword",OldPassword),
                                    new SqlParameter("@NewPassword",NewPassword),
                                     new SqlParameter("@PK_TeacherID",UpdatedBy),
                                       new SqlParameter("@UpdatedBy",UpdatedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("ChangeTeacherPassword", para);
            return ds;

        }
        public DataSet UpdateAdminTxnPassword()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@OldPassword",OldPassword),
                                    new SqlParameter("@NewPassword",NewPassword),
                                     new SqlParameter("@Pk_AdminID",UpdatedBy),
                                       new SqlParameter("@UpdatedBy",UpdatedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("ChangeTxnPassword", para);
            return ds;

        }
    }
}