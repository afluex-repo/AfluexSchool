using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Student : Common
    {

        #region props

        [AllowHtml]
        public string HomeWorkHTML { get; set; }
        public string HomeWorkID { get; set; }



        public string studentName { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string Dateofbirth { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Branch { get; set; }
        public string Medium { get; set; }
        public string AadhaarCard { get; set; }
        // public string Pk_BranchID { get; set; }
        public string PermanentAddress { get; set; }
        public string Post { get; set; }
        public string LandMark { get; set; }
        public string AlternateNumber { get; set; }
        public string PreviousSchool { get; set; }
        public string PreviousClass { get; set; }
        public string StudentPhoto { get; set; }
        public string BirthCetificate { get; set; }
        public string SessionName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string PK_SectionID { get; set; }
        public List<Student> listStudent { get; set; }
        public string Pk_StudentID { get; set; }
        public string LoginID { get; set; }


        public string ParentName { get; set; }
        public string ParentLogin_ID { get; set; }
        public List<SelectListItem> ddlsection { get; set; }
        public string Fk_SectionID { get; set; }
        public string Fk_ClassID { get; set; }
        public string PK_OccupationID { get; set; }
        public string Pk_ReligionId { get; set; }
        public string Age { get; set; }
        public string correspondenceAddress { get; set; }
        public string correspondencPinCode { get; set; }
        public string correspondencState { get; set; }
        public string correspondencCity { get; set; }

        public string StudenLoginID { get; set; }

        public string RegistrationDate { get; set; }
        public string AttendanceDate { get; set; }
        public string Status { get; set; }
        public string Fk_ParentId { get; set; }
        public DataTable dsStudentAttendance { get; set; }
        public string Pk_StudentAttendanceID { get; set; }
        public string AttendanceBy { get; set; }
        public string TeacherID { get; set; }
        public List<SelectListItem> ddlSubjectName { get; set; }
        public string SubjectID { get; set; }
        public string HomeworkDate { get; set; }
        public string HomeworkBy { get; set; }

        public string AreaName { get; set; }
        public string PK_AreaMasterID { get; set; }
        public List<Master> Arealist { get; set; }
        public string RouteNo { get; set; }
        public string PickupTime { get; set; }
        public string DropTime { get; set; }
        public string PK_RouteId { get; set; }
        public List<SelectListItem> ddlArea { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public string Amount { get; set; }

        public string Reason { get; set; }

        public string FromDateDOB { get; set; }
        public string ToDateDOB { get; set; }
        public string HomeworkFile { get; set; }
        #endregion

        public DataSet GetSubjectNameBySection()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_ClassId", Fk_ClassID),
                                       new SqlParameter("@Fk_SectionId",  Fk_SectionID),
                                        new SqlParameter("@TeacherID",TeacherID),
                                           new SqlParameter("@Fk_SessionId",SessionName),
            };

            DataSet ds = Connection.ExecuteQuery("GetSubjectNameBySection", para);
            return ds;
        }

        public DataSet GetClassOfClassTeacher()
        {
            SqlParameter[] para ={

                                    new SqlParameter("@TeacherID",TeacherID),

                                };

            DataSet ds = Connection.ExecuteQuery("GetClassOfClassTeacher", para);
            return ds;
        }
        public DataSet GetClassList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@PK_ClassID",Fk_ClassID),
                                    new SqlParameter("@TeacherID",TeacherID),

                                };

            DataSet ds = Connection.ExecuteQuery("GetClassList", para);
            return ds;
        }



        public DataSet SaveStudentRegistration()
        {
            SqlParameter[] para ={

                                   new SqlParameter("ParentLogin_ID",ParentLogin_ID),
                                   new SqlParameter("@Branch",Branch),
                                   new SqlParameter("@ClassName",Fk_ClassID),
                                   new SqlParameter("@SectionName",Fk_SectionID),
                                   new SqlParameter("@studentName",studentName),
                                   new SqlParameter("@FatherName",FatherName),
                                   new SqlParameter("@Fk_FatherOccupationID",FatherOccupation),
                                   new SqlParameter("@MotherName",MotherName),
                                   new SqlParameter("@Fk_MotherOccupationID",MotherOccupation),
                                   new SqlParameter("@Dateofbirth",Dateofbirth),
                                   new SqlParameter("@Age",Age),
                                   new SqlParameter("@Gender",Gender),
                                   new SqlParameter("@Category",Category),
                                   new SqlParameter("@Fk_ReligionID",Religion),
                                   new SqlParameter("@Nationality",Nationality),
                                   new SqlParameter("@Medium",Medium),
                                   new SqlParameter("@AadhaarCard",AadhaarCard),
                                   new SqlParameter("@StudentPhoto",StudentPhoto),
                                   new SqlParameter("@BirthCetificate",BirthCetificate),
                                   new SqlParameter("@PermanentAddress",PermanentAddress),
                                   new SqlParameter("@PinCode",PinCode),
                                   new SqlParameter("@State",State),
                                   new SqlParameter("@City",City),
                                   new SqlParameter("@CorrespondenceAddress",correspondenceAddress),
                                   new SqlParameter("@CorrespondencPinCode",correspondencPinCode),
                                   new SqlParameter("@CorrespondencState",correspondencState),
                                   new SqlParameter("@CorrespondencCity",correspondencCity),
                                   new SqlParameter("@Mobile",Mobile),
                                   new SqlParameter("@AlternateNumber",AlternateNumber),
                                   new SqlParameter("@Email",Email),
                                   new SqlParameter("@PreviousSchool",PreviousSchool),
                                   new SqlParameter("@PreviousClass",PreviousClass),
                                   new SqlParameter("@AddedBy",AddedBy),
                                   new SqlParameter("@FK_RouteId",PK_RouteId),
                                   new SqlParameter("@FK_AreaMasterID",PK_AreaMasterID),
                                   new SqlParameter("@VehicleNo",VehicleNo),
                                    new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@DriverName",DriverName),
                                    new SqlParameter("@DriverContactNo",DriverContactNo),
                                          new SqlParameter("@SessionName",SessionName),


                               };
            DataSet ds = Connection.ExecuteQuery("StudentRegistration", para);
            return ds;
        }

        public DataSet GetStudentList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                    new SqlParameter("@LoginID",LoginID),
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@StudenLoginID",StudenLoginID),
                                    new SqlParameter("@studentName",studentName),
                                    new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                    new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                        new SqlParameter("@ParentID",Fk_ParentId),
                                          new SqlParameter("@Session",SessionName),
                                };
            DataSet ds = Connection.ExecuteQuery("GetStudentList", para);
            return ds;
        }

        public DataSet UpdateStudentRegistration()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                   new SqlParameter("@Branch",Branch),
                                   new SqlParameter("@ClassName",Fk_ClassID),
                                   new SqlParameter("@SectionName",Fk_SectionID),
                                   new SqlParameter("@studentName",studentName),
                                   new SqlParameter("@FatherName",FatherName),
                                   new SqlParameter("@Fk_FatherOccupationID",FatherOccupation),
                                   new SqlParameter("@MotherName",MotherName),
                                   new SqlParameter("@Fk_MotherOccupationID",MotherOccupation),
                                   new SqlParameter("@Dateofbirth",Dateofbirth),
                                   new SqlParameter("@Age",Age),
                                   new SqlParameter("@Gender",Gender),
                                   new SqlParameter("@Category",Category),
                                   new SqlParameter("@Fk_ReligionID",Religion),
                                   new SqlParameter("@Nationality",Nationality),
                                   new SqlParameter("@Medium",Medium),
                                   new SqlParameter("@AadhaarCard",AadhaarCard),
                                   new SqlParameter("@StudentPhoto",StudentPhoto),
                                   new SqlParameter("@BirthCetificate",BirthCetificate),
                                   new SqlParameter("@PermanentAddress",PermanentAddress),
                                   new SqlParameter("@Pincode",PinCode),
                                   new SqlParameter("@State",State),
                                   new SqlParameter("@City",City),
                                   new SqlParameter("@CorrespondenceAddress",correspondenceAddress),
                                   new SqlParameter("@CorrespondencPinCode",correspondencPinCode),
                                   new SqlParameter("@CorrespondencState",correspondencState),
                                   new SqlParameter("@CorrespondencCity",correspondencCity),
                                   new SqlParameter("@Mobile",Mobile),
                                   new SqlParameter("@AlternateNumber",AlternateNumber),
                                   new SqlParameter("@Email",Email),
                                   new SqlParameter("@PreviousSchool",PreviousSchool),
                                   new SqlParameter("@PreviousClass",PreviousClass),
                                   new SqlParameter("@UpdatedBy",UpdatedBy),
                                    new SqlParameter("@FK_RouteId",PK_RouteId),
                                   new SqlParameter("@FK_AreaMasterID",PK_AreaMasterID),
                                   new SqlParameter("@VehicleNo",VehicleNo),
                                    new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@DriverName",DriverName),
                                    new SqlParameter("@DriverContactNo",DriverContactNo),

                               };
            DataSet ds = Connection.ExecuteQuery("UpdateStudent", para);
            return ds;
        }


        public DataSet DeleteStudent()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                   new SqlParameter("@DeletedBy",DeletedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("DeleteStudent", para);
            return ds;

        }

        public DataSet GetParentdetailByLoginID()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@ParentLogin_ID",ParentLogin_ID)
                               };
            DataSet ds = Connection.ExecuteQuery("GetParentDetail", para);
            return ds;
        }

        public DataSet GetSectionByClass()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                      new SqlParameter("@TeacherID",TeacherID),
                                };
            DataSet ds = Connection.ExecuteQuery("GetSectionByClass", para);
            return ds;
        }


        public DataSet GetOccupation()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@PK_OccupationID",PK_OccupationID)
                                };
            DataSet ds = Connection.ExecuteQuery("GetOccupation", para);
            return ds;
        }

        public DataSet GetOccupationMother()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@PK_OccupationID",PK_OccupationID)
                                };
            DataSet ds = Connection.ExecuteQuery("GetOccupationMother", para);
            return ds;
        }

        public DataSet GetReligion()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_ReligionId",Pk_ReligionId)
                                };
            DataSet ds = Connection.ExecuteQuery("GetReligion", para);
            return ds;
        }
        public DataSet SessionList()
        {
            DataSet ds = Connection.ExecuteQuery("GetSession");
            return ds;
        }
        public DataSet BranchList()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_BranchID",Pk_BranchID)
                               };
            DataSet ds = Connection.ExecuteQuery("GetBranchList", para);
            return ds;
        }
        public DataSet GetSectionList()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@PK_SectionID",PK_SectionID)
                               };
            DataSet ds = Connection.ExecuteQuery("GetSectionList", para);
            return ds;
        }
        public DataSet GetStudentBySection()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                   new SqlParameter("@Fk_ClassID",Fk_ClassID)
                               };
            DataSet ds = Connection.ExecuteQuery("GetStudentBySection", para);
            return ds;
        }
        public DataSet StudentGenrateRollNumber()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                    new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                    new SqlParameter("@AddedBy",AddedBy),
                                     new SqlParameter("@Fk_SessionId",SessionName),

                                };
            DataSet ds = Connection.ExecuteQuery("GenerateStudentRollNumber", para);
            return ds;

        }

        public DataSet SaveAttendance()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@StudentAttendance",dsStudentAttendance),
                                   new SqlParameter("@FK_ClassId",Fk_ClassID),
                                   new SqlParameter("@FK_SectionId",Fk_SectionID),
                                   new SqlParameter("@AttendanceDate",AttendanceDate),
                                   new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@AttendanceBy",AttendanceBy),
                                          new SqlParameter("@Session",SessionName),
                               };
            DataSet ds = Connection.ExecuteQuery("InsertStudentAttendance", para);
            return ds;
        }
        public DataSet GetCategory()
        {

            DataSet ds = Connection.ExecuteQuery("CategoryList");
            return ds;
        }
        public DataSet GetMedium()
        {

            DataSet ds = Connection.ExecuteQuery("MediumList");
            return ds;
        }
        public DataSet GetGender()
        {

            DataSet ds = Connection.ExecuteQuery("GenderList");
            return ds;
        }

        public DataSet GetStudentAttendanceDetail()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@FormDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                   new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                   new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                     new SqlParameter("@Status",Status),
                                      new SqlParameter("@Fk_ParentId",Fk_ParentId),
                                       new SqlParameter("@LoginId",StudenLoginID),
                                         new SqlParameter("@FK_SessionID",SessionName),
                               };
            DataSet ds = Connection.ExecuteQuery("GetStudentAttendanceDetail", para);
            return ds;
        }

        public DataSet StudentReport()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                    new SqlParameter("@LoginID",LoginID),
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@StudenLoginID",StudenLoginID),
                                    new SqlParameter("@studentName",studentName),
                                    new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                    new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                     new SqlParameter("@Session",SessionName),

                                      new SqlParameter("@Religion",Religion),
                                    new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@Category",Category),
                                      new SqlParameter("@FromDOB",FromDateDOB),
                                     new SqlParameter("@ToDOB",ToDateDOB),


                                };
            DataSet ds = Connection.ExecuteQuery("StudentReport", para);
            return ds;
        }


        #region TransferStudent

        public List<SelectListItem> dropdnSection { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string ParentPass { get;  set; }

        public DataSet GetSession()
        {

            DataSet ds = Connection.ExecuteQuery("GetSession");
            return ds;

        }
        public DataSet GetStudentListForTransfer()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                    new SqlParameter("@LoginID",LoginID),
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@StudenLoginID",StudenLoginID),
                                    new SqlParameter("@studentName",studentName),
                                    new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                    new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                      new SqlParameter("@Fk_SessionId",SessionName),

                                };
            DataSet ds = Connection.ExecuteQuery("GetStudentListForTransfer", para);
            return ds;
        }
        public DataSet TransferStudent()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Status",Status),
                                   new SqlParameter("@Pk_StudentId",Pk_StudentID),
                                   new SqlParameter("@Fk_SessionId",SessionName),
                                   new SqlParameter("@Fk_ClassId",Class),
                                   new SqlParameter("@Fk_SectionId",Section),
                                   new SqlParameter("@AddedBy",AddedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("TransferStudent", para);
            return ds;
        }
        #endregion

        #region Homework
        public DataSet SaveHomework()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                   new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                   new SqlParameter("@SubjectID",SubjectID),
                                   new SqlParameter("@HomeworkDate",HomeworkDate),
                                   new SqlParameter("@StudentPhoto",StudentPhoto),
                                    new SqlParameter("@AddedBy",AddedBy),
                                     new SqlParameter("@HomeworkBy",HomeworkBy),
                                      new SqlParameter("@HomeWorkHTML",HomeWorkHTML),
                                        new SqlParameter("@Fk_SessionId",SessionName),
                               };
            DataSet ds = Connection.ExecuteQuery("Homework", para);
            return ds;
        }
        public DataSet GetHomework()
        {
            SqlParameter[] para ={

                                    new SqlParameter("@Pk_ParentID",AddedBy),

                               };
            DataSet ds = Connection.ExecuteQuery("GetHomeworkForParent", para);
            return ds;
        }
        public DataSet HomeworkList()
        {
            SqlParameter[] para ={

                                    new SqlParameter("@FromDate",FromDate),
                                      new SqlParameter("@ToDate",ToDate),
                                       new SqlParameter("@HomeworkBy",HomeworkBy),
                                      new SqlParameter("@AddedBy",AddedBy),
                                        new SqlParameter("@Fk_SessionId",SessionName),

                                        new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                         new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                         new SqlParameter("@FK_TeacherID",TeacherID),
                                          new SqlParameter("@Fk_SubjectId",SubjectID)
                                          //new SqlParameter("@HomeworkFile",HomeworkFile)

                               };
            DataSet ds = Connection.ExecuteQuery("HomeWorkList", para);
            return ds;
        }


        #endregion

        #region transport for student

        public DataSet GettingRoute()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RouteId",PK_RouteId)
            };
            DataSet ds = Connection.ExecuteQuery("GetRoute", para);
            return ds;
        }

        #endregion
        public DataSet GetStateCityByPincodecorres()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@pincode",correspondencPinCode)
                               };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;

        }
        public DataSet DeleteHomework()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_HomeworkID",HomeWorkID),
                                    new SqlParameter("@DeletedBy",DeletedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("DeleteHomework", para);
            return ds;

        }
        public DataSet DeleteHomeworkByTeacher()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_HomeworkID",HomeWorkID),
                                    new SqlParameter("@DeletedBy",DeletedBy)
                               };
            DataSet ds = Connection.ExecuteQuery("DeleteHomeworkByTeacher", para);
            return ds;

        }

        public DataSet SaveLeave()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_ParentID",AddedBy),
                                    new SqlParameter("@AddedBy",AddedBy),
                                     new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                    new SqlParameter("@FromDate",FromDate),
                                     new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@Reason",Reason),
                               };
            DataSet ds = Connection.ExecuteQuery("ApplyLeaveForStudentByParent", para);
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
                                     new SqlParameter("@StudentName",studentName),
                                     new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                     new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                     new SqlParameter("@Sessionid",SessionName),
                               };
            DataSet ds = Connection.ExecuteQuery("StudentLeaveApplicationList", para);
            return ds;

        }

        public DataSet NewStudentOnExistingParent()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_ParentEnquiryID",Fk_ParentId),

                                   new SqlParameter("@LoginID",LoginID),
                                    new SqlParameter("@MobileNo",Mobile),
                                   new SqlParameter("@Name",DisplayName),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                  new SqlParameter("@IsActive",Status),

                               };
            DataSet ds = Connection.ExecuteQuery("ParentEnquiryList", para);
            return ds;

        }
    }
}