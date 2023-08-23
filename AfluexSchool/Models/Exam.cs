using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Exam : Common
    {
        #region Props
        public string ExamTypeName { get; set; }
        public string Pk_ExamTypeId { get; set; }
        public List<Exam> lstExamType { get; set; }
        public string Pk_MaxMarksId { get; set; }
        public string ClassName { get; set; }
        public string MaxMarksTest { get; set; }
        public string MaxMarksExam { get; set; }
        public List<Exam> lstMaxMarks { get; set; }
        public string Pk_StudentMarksId { get; set; }
        public string StudentName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string ObtainMarks { get; set; }
        public string Marks { get; set; }
        public List<SelectListItem> ddlsection { get; set; }
        public List<SelectListItem> ddlSubjectName { get; set; }
        public List<SelectListItem> ddlstudent { get; set; }
        public List<Exam> ddlStudentMarks { get; set; }
        public List<Exam> lststudent { get; set; }
        public string Fk_SectionId { get; set; }
        public string Fk_ClassID { get; set; }
        public string SubjectID { get; set; }
        public string Pk_StudentID { get; set; }
        public string MaxMarks { get; set; }
        public string ObtainedMarks { get; set; }
        public DataTable dsStudentAttendance { get; set; }
         public string MinMarks { get; set; }
         public string Session { get; set; }
        #endregion

        #region ExamType

        public DataSet SaveExamType()
        {
            SqlParameter[] para = { new SqlParameter("@ExamTypeName", ExamTypeName), new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("InsertExamType", para);
            return ds;
        }

        public DataSet ExamTypeList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_ExamTypeId", Pk_ExamTypeId) };
            DataSet ds = Connection.ExecuteQuery("GetExamtype", para);
            return ds;
        }
        public DataSet DeleteExamType()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_ExamTypeId", Pk_ExamTypeId), new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteExamType", para);
            return ds;
        }

        public DataSet UpdateExamType()
        {
            SqlParameter[] para ={new SqlParameter("@Pk_ExamTypeId",Pk_ExamTypeId),
                                     new SqlParameter("@ExamTypeName",ExamTypeName),
                                     new SqlParameter("@UpdatedBy",UpdatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("UpdateExamType", para);
            return ds;
        }

        #endregion

        #region MaxMarks


        public DataSet BindClass()
        {
            DataSet ds = Connection.ExecuteQuery("GetClassList");
            return ds;
        }



        public DataSet SaveMaxMarks()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_ClassId",ClassName),
                                      new SqlParameter("@Fk_ExamTypeId",ExamTypeName),
                                      new SqlParameter("@MaxMarksTest",MinMarks),
                                      new SqlParameter("@MaxMarksExam",MaxMarksExam),
                                      new SqlParameter("@AddedBy",AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertMaximumMarks", para);
            return ds;
        }

        public DataSet GetMaxMarksList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_MaxMarksId", Pk_MaxMarksId) };

            DataSet ds = Connection.ExecuteQuery("GetMaxMarksList", para);
            return ds;
        }
        public DataSet DeleteMaxMarks()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_MaxMarksId", Pk_MaxMarksId), new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteMaxMarks", para);
            return ds;
        }

        public DataSet UpdateMaxMarks()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_MaxMarksId", Pk_MaxMarksId),
                                      new SqlParameter("@Fk_ClassId",ClassName),
                                      new SqlParameter("@Fk_ExamTypeId",ExamTypeName),
                                      new SqlParameter("@MaxMarksTest",MinMarks),
                                      new SqlParameter("@MaxMarksExam",MaxMarksExam),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateMaxMarks", para);
            return ds;
        }
        #endregion

        #region Student Marks
        public DataSet GetStudentBySection()
        {
            SqlParameter[] para ={

                                   new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                   new SqlParameter("@Fk_SectionID",Fk_SectionId),
                                   new SqlParameter("@Fk_ExamTypeID",ExamTypeName),
                                        new SqlParameter("@Fk_SessionId",Session),

                                  
        };
            DataSet ds = Connection.ExecuteQuery("GetMaxMarksAndStudentName", para);
            return ds;
        }
        public DataSet GetSubjectNameBySection()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_ClassId", Fk_ClassID),
                                       new SqlParameter("@Fk_SectionId",  Fk_SectionId),
                                           new SqlParameter("@Fk_SessionId",  Session),

            };

            DataSet ds = Connection.ExecuteQuery("GetSubjectNameBySection", para);
            return ds;
        }

        public DataSet StudentMarksList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_StudentMarksId", Pk_StudentMarksId),
                new SqlParameter("@Fk_ClassId", Fk_ClassID),
                new SqlParameter("@Fk_SectionId", Fk_ClassID),
                new SqlParameter("@Fk_SubjectId", SubjectID),
                new SqlParameter("@Fk_ExamTypeId", ExamTypeName),
                    new SqlParameter("@Fk_SessionId", Session  ),
                
            };
            DataSet ds = Connection.ExecuteQuery("StudentMarksList", para);
            return ds;
        }
        public DataSet SaveStudentMarks()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                      new SqlParameter("@Fk_SectionId",Fk_ClassID),
                                      new SqlParameter("@Fk_SubjectId",SubjectID),
                                      new SqlParameter("@Fk_ExamTypeId",ExamTypeName),
                                      new SqlParameter("@StudentAttendance",dsStudentAttendance),
                                      new SqlParameter("@AddedBy",AddedBy),
                                       new SqlParameter("@SessionName", Session  ),
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertStudentSubjectMarks", para);
            return ds;
        }
        #endregion
    }
}