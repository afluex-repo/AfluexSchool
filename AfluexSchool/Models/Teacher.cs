using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Teacher : Common
    {
        public string Password { get; set; }
        public string ActionType { get; set; }
        public string Address { get; set; }
        public string BranchName { get; set; }
        public string Category { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string EmailID { get; set; }
        public string Experience { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string LastExperience { get; set; }
        public string LastSchool { get; set; }
        public string LoginID { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string Pk_ReligionId { get; set; }
        public string PK_TeacherID { get; set; }
        public string Qualification { get; set; }
        public string Religion { get; set; }
        public List<Teacher> lstTeacherList { get; set; }
        public List<Teacher> listStudent { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Pk_StudentID { get; set; }
        public string Description { get; set; }
        public string PK_StdntLeaveID { get; set; }
        public List<SelectListItem> ddlStatus { get; set; }
        public List<SelectListItem> ddlSection { get; set; }
        public string PK_ClassID { get; set; }
        public string Fk_SectionID { get; set; }
        public string Session { get; set; }
        public DataSet GetReligion()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_ReligionId",Pk_ReligionId)
                                };
            DataSet ds = Connection.ExecuteQuery("GetReligion", para);
            return ds;
        }
        public DataSet GetTeacherList()
        {
            SqlParameter[] Param ={
                                      new SqlParameter("@PK_TeacherID",PK_TeacherID),
                                      new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@TeacherName", Name),
                                      new SqlParameter("@FromDate",FromDate),
                                      new SqlParameter("@ToDate",ToDate)
                                   };
            DataSet ds = Connection.ExecuteQuery("TeacherList", Param);
            return ds;
        }

        public DataSet InsertTeacherRecord()
        {
            SqlParameter[] Para ={
                                     new SqlParameter("@Name",Name),
                                     new SqlParameter("@FatherName",FatherName),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@pincode",PinCode),
                                     new SqlParameter("@City",City),
                                     new SqlParameter("@State",State),
                                     new SqlParameter("@DOB",DOB),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@FK_ReligionID",Religion),
                                     new SqlParameter("@Category",Category),
                                   new SqlParameter("@LastSchool",LastSchool),
                                   new SqlParameter("@LastExperience",LastExperience),
                                     new SqlParameter("@DOJ",DOJ ),
                                     new SqlParameter("@Qualification",Qualification),
                                      new SqlParameter("@EmailID",EmailID),
                                     new SqlParameter("@FK_BranchID",BranchName),
                                     new SqlParameter("@Experience",Experience),
                                     new SqlParameter("@Image",Image),
                                     new SqlParameter("@MobileNo",MobileNo),

                                     new SqlParameter("AddedBy",AddedBy)

                                 };
            DataSet ds = Connection.ExecuteQuery("InsertTeacher", Para);
            return ds;
        }
        public DataSet UpdateTeacherRecord()
        {
            SqlParameter[] Param ={
                                      new SqlParameter("@PK_TeacherID",PK_TeacherID),
                                       new SqlParameter("@Name",Name),
                                       new SqlParameter("@FatherName",FatherName),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@pincode",PinCode),
                                   new SqlParameter("@EmailID",EmailID),
                                     new SqlParameter("@DOB",DOB),
                                       new SqlParameter("@LastSchool",LastSchool),
                                         new SqlParameter("@LastExperience",LastExperience),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@FK_ReligionID",Religion),
                                     new SqlParameter("@Category",Category),
                                     new SqlParameter("@DOJ",DOJ),
                                     new SqlParameter("Qualification",Qualification),
                                     new SqlParameter("@Experience",Experience),
                                     new SqlParameter("@FK_BranchID",BranchName),
                                     new SqlParameter("@MobileNo",MobileNo),
                                     new SqlParameter("@Image",Image),

                                     new SqlParameter("@UpdatedBy",UpdatedBy)
                                   };
            DataSet ds = Connection.ExecuteQuery("UpdateTeacherRecord", Param);
            return ds;
        }

        public DataSet DeleteTeacherRecord()
        {
            SqlParameter[] Param ={new SqlParameter("@PK_TeacherID",PK_TeacherID),
                                      new SqlParameter("@DeletedBy",DeletedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteTeacherRecord ", Param);
            return ds;
        }



        public DataSet LeaveListParent()
        {
            SqlParameter[] para ={
                                     new SqlParameter("@FK_ParentID",AddedBy),
                                     new SqlParameter("@FK_StudentID",Pk_StudentID),
                                     new SqlParameter("@FromDate",FromDate),
                                     new SqlParameter("@ToDate",ToDate),
                                     new SqlParameter("@Status",Status),
                                     new SqlParameter("@StudentName",StudentName),
                                     new SqlParameter("@Fk_ClassID",PK_ClassID),
                                     new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                     new SqlParameter("@FK_TeacherID",PK_TeacherID)

                               };
            DataSet ds = Connection.ExecuteQuery("TeacherStudentsLeaveApplication", para);
            return ds;

        }

        public DataSet UpdatingStudentLeaveAplcn()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@PK_StdntLeaveID",PK_StdntLeaveID),
                                    new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                     new SqlParameter("@Description",Description),
                                      new SqlParameter("@UpdatedBy",UpdatedBy),
                                      new SqlParameter("@Status",Status),
                                          new SqlParameter("@SessionID",Session),
                               };
            DataSet ds = Connection.ExecuteQuery("UpdateStudentLeaveApplication", para);
            return ds;
        }

        public DataSet GettingStatus()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Status",Status)
                               };
            DataSet ds = Connection.ExecuteQuery("SearchLeaveStatus", para);
            return ds;
        }

        public DataSet GetSectionByClass()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_ClassID",PK_ClassID),
                                };
            DataSet ds = Connection.ExecuteQuery("GetSectionByClass", para);
            return ds;
        }

        public DataSet GetSectionList()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@PK_SectionID",Fk_SectionID)
                               };
            DataSet ds = Connection.ExecuteQuery("GetSectionList", para);
            return ds;
        }

        public DataSet GetClassList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@PK_ClassID",PK_ClassID),
                                    new SqlParameter("@TeacherID",PK_TeacherID),

                                };

            DataSet ds = Connection.ExecuteQuery("GetClassList", para);
            return ds;
        }
    }
}