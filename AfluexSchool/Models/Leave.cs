using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Leave : Common
    {
        public List<Leave> listStudent { get; set; }
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
                                       new SqlParameter("@Sessionid",Session),
                               };
            DataSet ds = Connection.ExecuteQuery("StudentLeaveApplicationList", para);
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

    }
}