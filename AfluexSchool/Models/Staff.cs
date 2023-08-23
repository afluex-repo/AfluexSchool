using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfluexSchool.Models
{
    public class Staff : Common
    {
        #region props

        public string Designation { get; set; }
        public string PK_StaffID { get; set; }
        public string PK_StaffDesignationID { get; set; }
        public List<Staff> lstSDesignationList { get; set; }
        public string Pk_ReligionId { get;  set; }
        public string Name { get;  set; }
        public string FatherName { get;  set; }
        public string DOB { get;  set; }
        public string Gender { get;  set; }
        public string Category { get;  set; }
        public string Religion { get;  set; }
        public string Qualification { get;  set; }
        public string Experience { get;  set; }
        public string Address { get;  set; }
        public string MobileNo { get;  set; }
        public string Image { get;  set; }
        public string DOJ { get;  set; }
        public string EmailID { get;  set; }
        public string BranchName { get;  set; }
        public string LoginID { get;  set; }

        #endregion

        #region staffdesignations
        public DataSet InsertStaffDesignation()
        {
            SqlParameter[] param = {new SqlParameter("@Designation",Designation),
                                      new SqlParameter("@AddedBy",AddedBy)   };
            DataSet ds = Connection.ExecuteQuery("InsertStaffDesignation", param);
            return ds;
        }

        public DataSet StaffDesignationList()
        {
            SqlParameter[] param = { new SqlParameter("@PK_StaffDesignationID", PK_StaffDesignationID) };
            DataSet ds = Connection.ExecuteQuery("StaffDesignationList", param);
            return ds;
        }

        public DataSet DeleteStaffDesignation()
        {
            SqlParameter[] param = { new SqlParameter("@PK_StaffDesignationID", PK_StaffDesignationID),
                                     new SqlParameter("@DeletedBy",DeletedBy)};
            DataSet ds = Connection.ExecuteQuery("DeleteStaffDesignation", param);
            return ds;
        }

        public DataSet UpdateStaffDesignation()
        {
            SqlParameter[] param = { new SqlParameter("@PK_StaffDesignationID", PK_StaffDesignationID),
                                   new SqlParameter("@Designation",Designation),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = Connection.ExecuteQuery("UpdateStaffDesignation", param);
            return ds;
        }
        #endregion

        #region staff
        public DataSet GetReligion()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_ReligionId",Pk_ReligionId)
                                };
            DataSet ds = Connection.ExecuteQuery("GetReligion", para);
            return ds;
        }
        public DataSet InsertStaffRecord()
        {
            SqlParameter[] Param ={
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@FatherName",FatherName),
                                      new SqlParameter("@DOB",DOB),
                                      new SqlParameter("@Gender",Gender),
                                      new SqlParameter("@Category",Category),
                                      new SqlParameter("@FK_ReligionID",Religion),
                                      new SqlParameter("@Qualification",Qualification),
                                      new SqlParameter("@Experience",Experience),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@pincode",PinCode),
                                      new SqlParameter("@State",State),
                                      new SqlParameter("@City",City),
                                      new SqlParameter("@MobileNo",MobileNo),
                                       new SqlParameter("@Image",Image),
                                       new SqlParameter("@EmailID",EmailID),
                                        new SqlParameter("@DOJ",DOJ),
                                      new SqlParameter("@FK_StaffDesignationID",Designation),
                                      new SqlParameter("@FK_BranchID",BranchName),
                                      new SqlParameter("@AddedBy",AddedBy)
                                   };
            DataSet ds = Connection.ExecuteQuery("InsertStaffRecord", Param);

            return ds;
        }
        
        public DataSet GetStaffList()
        {
            SqlParameter[] Param = { new SqlParameter("@PK_StaffID", PK_StaffID) ,
                                    new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@FromDate",FromDate),
                                      new SqlParameter("@ToDate",ToDate)
                                   };
            DataSet ds = Connection.ExecuteQuery("GetStaffList", Param);
            return ds;
        }

       
        public DataSet DeleteStaffList()
        {
            SqlParameter[] Param = { new SqlParameter("@PK_StaffID", PK_StaffID),
                                   new SqlParameter("@DeletedBy",DeletedBy)};
            DataSet ds = Connection.ExecuteQuery("DeleteStaffList", Param);
            return ds;
        }

        public DataSet UpdateStaffRecord()
        {
            SqlParameter[] Param = {  new SqlParameter("@PK_StaffID",PK_StaffID),
                                       new SqlParameter("@Name",Name),
                                       new SqlParameter("@FatherName",FatherName),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@pincode",PinCode),
                                   new SqlParameter("@EmailID",EmailID),
                                     new SqlParameter("@DOB",Common.ConvertToSystemDate(DOB,"dd/MM/yyyy")),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@FK_ReligionID",Religion),
                                     new SqlParameter("@Category",Category),
                                     new SqlParameter("@DOJ",Common.ConvertToSystemDate( DOJ,"dd/MM/yyyy")),
                                     new SqlParameter("Qualification",Qualification),
                                     new SqlParameter("@Experience",Experience),
                                     new SqlParameter("@FK_BranchID",BranchName),
                                     new SqlParameter("@MobileNo",MobileNo),
                                     new SqlParameter("@Image",Image),
                                   new SqlParameter("@FK_StaffDesignationID",Designation),
                                     new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = Connection.ExecuteQuery("UpdateStaffRecord", Param);
            return ds;
        }
        #endregion
    }
}