using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Reports : Common
    {
        public string SessionName { get; set; }
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string ReceiptNo { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string BankDetails { get; set; }
        public List<Reports> lstfeedata { get; set; }
        public string DueDate { get; set; }
        public string InstallemntNo { get; set; }
        public string InstallmentAmt { get; set; }
        public string PaidAmount { get; set; }
        public string FeeTypeName { get; set; }
        public string Pk_StudentID { get; set; }
        public string MaximumMarks { get; set; }
        public string ObtainMarks { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string StudentName { get; set; }
        public string ExamTypeName { get; set; }
        public string Fk_SectionID { get; set; }
        public string Fk_ClassID { get; set; }
        public List<SelectListItem> ddlsection { get; set; }
        public string pkid { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string Status { get; set; }
        public string Fk_ParentId { get; set; }
        public string Session { get; set; }
        public string ActionName { get; set; }
        public string TransactionDate { get; set; }
        public string CreatedBy { get; set; }
        public string TransactionBy { get; set; }
        public string Remark { get; set; }
        public string Mobile { get; set; }
        public string ConcessionFee { get; set; }
        public string FinalAmount { get; set; }
        public string AdmissionFee { get; set; }
        public string ExaminationFee { get; set; }
        public string Computerfee { get; set; }
        public string Otherfee { get; set; }
        public string Totalfee { get; set; }
        public string DueAmt { get; set; }
        public string RegistrationAmt { get; set; }
        public string TuitionFee { get; set; }
        public string TotalPaid { get; set; }
        public string PaidStudent { get; set; }
        public string UnpaidStudent { get; set; }
        public string TotalStudent { get; set; }
        public string FatherName { get; set; }
        public string Balance { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
        public string FeeAmount { get; set; }
        public string LateFee { get; set; }
        public string Discount { get; set; }
        public string DeletedOn { get; set; }
        public string FromDeletedDate { get; set; }
        public string ToDeletedDate { get; set; }

        public List<Reports> listStudent { get; set; }
        public string HomeworkDate { get; set; }
        public string HomeworkBy { get; set; }

        public string HomeWorkHTML { get; set; }
        public string HomeWorkID { get; set; }
        public string SubjectID { get; set; }
        public string TeacherName { get; set; }
        public List<Reports> listEnquiry { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FormNo { get; set; }
        public string Pk_EnquiryId { get; set; }
        public string PreviousSchool { get; set; }
        public string PK_TeacherID { get; set; }
        public string HomeworkFile { get; set; }

        public List<SelectListItem> ddlSubjectName { get; set; }

        public DataSet GetFeeReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Fk_ParentId", Fk_ParentId),
                                      new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                      new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                      new SqlParameter("@ReceiptNo",ReceiptNo),
                                      new SqlParameter("@Fk_SessionId",Session),

            };
            DataSet ds = Connection.ExecuteQuery("GetFeeReport", para);
            return ds;

        }
        public DataSet PrintReceipt()
        {
            SqlParameter[] para = { new SqlParameter("@ReceiptNo", ReceiptNo) };
            DataSet ds = Connection.ExecuteQuery("PrintReceipt", para);
            return ds;

        }
        public DataSet GetStudentList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_StudentID",Pk_StudentID),

                                };
            DataSet ds = Connection.ExecuteQuery("GetStudentList", para);
            return ds;
        }
        public DataSet StudentMarksheet()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_SectionId",Fk_SectionID),
                                    new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                    new SqlParameter("@Fk_StudentId",Pk_StudentID),
                                    new SqlParameter("@Fk_ExamTypeId",ExamTypeName),
                                    new SqlParameter("@Fk_ParentId",Fk_ParentId),
                                     new SqlParameter("@Fk_SessionId",Session),

                                };
            DataSet ds = Connection.ExecuteQuery("Marksheet", para);
            return ds;
        }
        public DataSet PrintMarksheet()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_SectionId",Fk_SectionID),
                                    new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                    new SqlParameter("@Fk_StudentId",Pk_StudentID),
                                    new SqlParameter("@Fk_ExamTypeId",pkid),
                                };
            DataSet ds = Connection.ExecuteQuery("PrintMarksheet", para);
            return ds;
        }

        public DataSet BindClass()
        {
            DataSet ds = Connection.ExecuteQuery("GetClassList");
            return ds;
        }
        public DataSet ExamTypeList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_ExamTypeId", pkid) };
            DataSet ds = Connection.ExecuteQuery("GetExamtype", para);
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


        public DataSet ActionList()
        {

            DataSet ds = Connection.ExecuteQuery("ActionList");
            return ds;
        }

        public DataSet TransactionLog()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@ActionName",ActionName),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),

                                };
            DataSet ds = Connection.ExecuteQuery("GetTransactionLog", para);
            return ds;
        }
        public DataSet FeeTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("FeeTypeList");
            return ds;
        }
        public DataSet GetConcessionReport()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@LoginID",LoginId),
                                    new SqlParameter("@FeeTypeName",FeeTypeName),
                                    new SqlParameter("@Monthname",MonthName),
                                    new SqlParameter("@Fk_SessionId",Session),

                                };
            DataSet ds = Connection.ExecuteQuery("ConcessionFeeReport", para);
            return ds;
        }
        public DataSet DueReportClassWise()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Date",FromDate),

                                    new SqlParameter("@Fk_SessionId",SessionName),

                                };
            DataSet ds = Connection.ExecuteQuery("GetDueReportClassWise", para);
            return ds;
        }
        public DataSet DueReport()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Date",FromDate),
                                    new SqlParameter("@Fk_SessionId",SessionName),
                                };
            DataSet ds = Connection.ExecuteQuery("GetDueReportDetail", para);
            return ds;
        }
        public DataSet DeleteFee()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@ReceiptNo",ReceiptNo),
                                    new SqlParameter("@DeletedBy",DeletedBy),


                                };
            DataSet ds = Connection.ExecuteQuery("DeleteFee", para);
            return ds;
        }
        public DataSet DeletedFeeReport()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@StudentName",StudentName ),
                                    new SqlParameter("@LoginID",LoginId),
                                     new SqlParameter("@RecieptNo",ReceiptNo),
                                    new SqlParameter("@PaymentFromDate",FromDate),
                                     new SqlParameter("@PaymentToDate",ToDate),
                                    new SqlParameter("@DeletedFromDate",FromDeletedDate),
                                     new SqlParameter("@DeletedToDate",ToDeletedDate),
                                         new SqlParameter("@Fk_SessionId",SessionName),

                                };
            DataSet ds = Connection.ExecuteQuery("DeletedFeeReport", para);
            return ds;
        }
        public DataSet MultipleStudentReport()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_ParentID",pkid ),
                                    new SqlParameter("@Fk_SessionID",SessionName),
                                     new SqlParameter("@LoginID",LoginId),
                                    new SqlParameter("@ParentName",ParentName),
                                    
                                };
            DataSet ds = Connection.ExecuteQuery("MultipleStudentReport", para);
            return ds;
        }

        public DataSet HomeworkList()
        {
            SqlParameter[] para ={

                                  new SqlParameter("@FromDate",FromDate),
                                      new SqlParameter("@ToDate",ToDate),
                                       new SqlParameter("@HomeworkBy",HomeworkBy),
                                      new SqlParameter("@AddedBy",AddedBy),
                                        new SqlParameter("@Fk_SessionId",Session),

                                        new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                         new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                         new SqlParameter("@FK_TeacherID",PK_TeacherID),
                                          new SqlParameter("@Fk_SubjectId",SubjectID),
                                            new SqlParameter("@HomeworkFile",HomeworkFile)

                               };
            DataSet ds = Connection.ExecuteQuery("HomeWorkList", para);
            return ds;
        }

        public DataSet GetEnquiryList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_SessionId",SessionName),

                             };
            DataSet ds = Connection.ExecuteQuery("EnquiryList",para);
            return ds;
        }

        public DataSet GettingTeacherList()
        {
            SqlParameter[] Param ={
                                      new SqlParameter("@PK_TeacherID",PK_TeacherID)
                                   };
            DataSet ds = Connection.ExecuteQuery("TeacherList", Param);
            return ds;
        }

        public DataSet GetSubjectNameBySection()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_ClassId", Fk_ClassID),
                                       new SqlParameter("@Fk_SectionId",  Fk_SectionID),
                                           new SqlParameter("@Fk_SessionId",SessionName),
            };

            DataSet ds = Connection.ExecuteQuery("GetSubjectNameBySection", para);
            return ds;
        }

        public DataSet GetSectionByClass()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_ClassID",Fk_ClassID)
                                };
            DataSet ds = Connection.ExecuteQuery("GetSectionByClass", para);
            return ds;
        }

        public DataSet DeletingEnquiry()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_EnquiryId",Pk_EnquiryId),
                                    new SqlParameter("@DeletedBy",DeletedBy),


                                };
            DataSet ds = Connection.ExecuteQuery("DeleteEnquiry", para);
            return ds;
        }


    }
}