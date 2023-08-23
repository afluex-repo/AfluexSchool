using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Master : Common
    {
        #region Properties
        public string ISDailyValue { get; set; }
        public string SessionName { get; set; }
        public string Pk_SessionId { get; set; }
        public string ClassName { get; set; }
        public string Pk_ClassId { get; set; }
        public string PK_SectionId { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string Pk_SubjectId { get; set; }
        public string isDeleted { get; set; }
        public string BranchName { get; set; }
        public string PK_FineID { get; set; }
        public string Amount { get; set; }
        public string DepartmentName { get; set; }
        public string PK_DepartmentID { get; set; }
        public string IsDaily { get; set; }
        public string LeaveName { get; set; }
        public string VehicleType { get; set; }
        public string PK_LeaveID { get; set; }
        public string Pk_AssignId { get; set; }
        public string LeaveCount { get; set; }
        public string Status { get; set; }
        public string PK_TeacherID { get; set; }
        public string LoginID { get; set; }
        public string Name { get; set; }
        public string Pk_SubjecttoTeacherId { get; set; }
        public string ReligionName { get; set; }
        public string PK_ReligionID { get; set; }
        public string PK_NoticeId { get; set; }
        public string PK_VehicleMasterID { get; set; }
        public string PK_VehicleTypeID { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public string NoticeName { get; set; }
        public List<Master> sessionLst { get; set; }
        public List<Master> sectionLst { get; set; }
        public List<Master> classLst { get; set; }
        public List<Master> subjectLst { get; set; }
        public List<Master> BranchLst { get; set; }
        public List<Master> DepartmentLst { get; set; }
        public List<Master> FineList { get; set; }
        public List<Master> LeaveList { get; set; }
        public DataTable dsSubject { get; set; }
        public List<SelectListItem> ddlSectionName { get; set; }
        public List<Master> Religionlist { get; set; }
        public List<Master> Noticelist { get; set; }
        public List<Master> VehicleList { get; set; }
        public List<SelectListItem> ddlVehicleNo { get; set; }
        public string Type { get; set; }
        public string Pk_StudentID { get; set; }
        public List<SelectListItem> ddlStudent { get; set; }
        public string Pk_AlotVehicleID { get; set; }
        public string SelectedValue { get; set; }
        public string StudentName { get; set; }
        public List<Master> StudentList { get; set; }
        public string Daily { get; set; }
        public string NoticeBy { get; set; }
        public string Address { get; set; }
        public string Fk_ClassID { get; set; }
        public string Fk_SectionID { get; set; }
        public string Pk_AssignClassTeacherId { get; set; }
        public List<Master> assignClassTeacherList { get; set; }
        public List<Master> VehicleTypelist { get; set; }
        public string AreaName { get; set; }
        public string PK_AreaMasterID { get; set; }
        public List<Master> Arealist { get; set; }
        public string RouteNo { get; set; }
        public string PickupTime { get; set; }
        public string DropTime { get; set; }
        public string PK_RouteId { get; set; }
        public string PK_AllotId { get; set; }
        public List<SelectListItem> ddlArea { get; set; }
        public string Syllabus { get; set; }
        public string SyllabusID { get; set; }
        public string TimeTable { get; set; }
        public string TimeTableID { get; set; }
        public string HolidayID { get; set; }
        public string HolidayName { get; set; }
        public string HolidayDate { get; set; }
        #endregion

        #region Session
        public DataSet SavingSession()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@SessionName",SessionName)
            };
            DataSet ds = Connection.ExecuteQuery("SaveSession", para);
            return ds;
        }

        public DataSet GettingSession()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_SessionId",Pk_SessionId),
                new SqlParameter("@SessionName",SessionName)
            };
            DataSet ds = Connection.ExecuteQuery("GetSession", para);
            return ds;
        }

        public DataSet UpdatingSession()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_SessionId",Pk_SessionId),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@SessionName",SessionName)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateSession", para);
            return ds;
        }

        public DataSet DeletingSession()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_SessionId",Pk_SessionId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteSession", para);
            return ds;
        }
        #endregion

        #region Class

        public DataSet SavingClass()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@ClassName",ClassName)
            };
            DataSet ds = Connection.ExecuteQuery("SaveClass", para);
            return ds;
        }

        public DataSet GettingClass()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ClassId",Pk_ClassId)
            };
            DataSet ds = Connection.ExecuteQuery("GetClassList", para);
            return ds;
        }

        public DataSet UpdatingClass()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_ClassID",Pk_ClassId),
            new SqlParameter("@ClassName",ClassName),
            new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateClass", para);
            return ds;
        }

        public DataSet DeletingClass()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                new SqlParameter("@PK_ClassId",Pk_ClassId)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteClass", para);
            return ds;
        }
        #endregion

        #region Section

        public DataSet SavingSection()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@SectionName",SectionName),
                 new SqlParameter("@FK_ClassId",Pk_ClassId),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("SaveSection", para);
            return ds;
        }

        public DataSet GettingSectionList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_SectionID",PK_SectionId),
                new SqlParameter("@Pk_ClassID",Pk_ClassId)
            };
            DataSet ds = Connection.ExecuteQuery("GetSectionList", para);
            return ds;
        }

        public DataSet UpdatingSection()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_ClassId",Pk_ClassId),
                new SqlParameter("@SectionName",SectionName),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@PK_SectionId",PK_SectionId)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateSection", para);
            return ds;
        }

        public DataSet DeletingSection()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_SectionId",PK_SectionId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteSection", para);
            return ds;
        }
        #endregion

        #region Subject

        public DataSet SavingSubject()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@SubjectName",SubjectName),
                new SqlParameter("@AddedBy",AddedBy),
            };
            DataSet ds = Connection.ExecuteQuery("SaveSubjectMaster", para);
            return ds;
        }

        public DataSet gettingSubjectMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@SubjectName",SubjectName),
                new SqlParameter("@Pk_SubjectId",Pk_SubjectId)
            };
            DataSet ds = Connection.ExecuteQuery("getSubjectMaster", para);
            return ds;
        }

        public DataSet UpdatingSubjectMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_SubjectId",Pk_SubjectId),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@SubjectName",SubjectName)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateSubjectMaster", para);
            return ds;
        }

        public DataSet DeletingSubjectMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                new SqlParameter("@Pk_SubjectId",Pk_SubjectId)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteSubjectMaster", para);
            return ds;
        }
        #endregion

        #region Branch

        public DataSet SavingBranch()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@BranchName",BranchName)
            };
            DataSet ds = Connection.ExecuteQuery("SaveBranch", para);
            return ds;
        }

        public DataSet GettingBranch()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BranchID",Pk_BranchID)
            };
            DataSet ds = Connection.ExecuteQuery("GetBranchList", para);
            return ds;
        }

        public DataSet UpdatingBranch()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@BranchName",BranchName),
                new SqlParameter("@Pk_BranchID",Pk_BranchID)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateBranch", para);
            return ds;
        }

        public DataSet DeletingBranch()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                new SqlParameter("@Pk_BranchID",Pk_BranchID)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteBranch", para);
            return ds;
        }
        #endregion

        #region Department

        public DataSet SavingDepartment()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@DepartmentName",DepartmentName),
            };
            DataSet ds = Connection.ExecuteQuery("SaveDepartment", para);
            return ds;
        }

        public DataSet GettingDepartment()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_DepartmentID",PK_DepartmentID),
                new SqlParameter("@DepartmentName",DepartmentName)
            };
            DataSet ds = Connection.ExecuteQuery("GetDepartmentList", para);
            return ds;
        }

        public DataSet UpdatingDepartment()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_DepartmentID",PK_DepartmentID),
                  new SqlParameter("@DepartmentName",DepartmentName),
                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateDepartment", para);
            return ds;
        }

        public DataSet DeletingDepartment()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                 new SqlParameter("@PK_DepartmentID",PK_DepartmentID)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteDepartment", para);
            return ds;
        }
        #endregion

        #region Fine Master

        public DataSet SavingFineMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@IsDaily",IsDaily),
                new SqlParameter("@Amount",Amount)
            };
            DataSet ds = Connection.ExecuteQuery("SaveFineMaster", para);
            return ds;
        }

        public DataSet GettingFineMasterList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@PK_FineID",PK_FineID),
                 new SqlParameter("@IsDaily",IsDaily),
                 new SqlParameter("@Amount",Amount)
            };
            DataSet ds = Connection.ExecuteQuery("FineMasterList", para);
            return ds;
        }

        public DataSet UpdatingFineMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("PK_FineID",PK_FineID),
                new SqlParameter("Fk_ClassId",Pk_ClassId),
                new SqlParameter("Amount",Amount),
                new SqlParameter("IsDaily",IsDaily),
                new SqlParameter("UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateFineMaster", para);
            return ds;
        }

        public DataSet DeletingFineMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_FineID",PK_FineID),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteFineMaster", para);
            return ds;
        }
        #endregion

        #region Leave

        public DataSet SavingLeave()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@LeaveCount",LeaveCount),
                new SqlParameter("@LeaveName",LeaveName)
            };
            DataSet ds = Connection.ExecuteQuery("SaveLeave", para);
            return ds;
        }

        public DataSet GettingLeaveList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_LeaveID",PK_LeaveID),
                new SqlParameter("@LeaveCount",LeaveCount),
                new SqlParameter("@LeaveName",LeaveName),
            };
            DataSet ds = Connection.ExecuteQuery("LeaveList", para);
            return ds;
        }

        public DataSet UpdatingLeave()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@LeaveName",LeaveName),
                new SqlParameter("@LeaveCount",LeaveCount),
                new SqlParameter("@PK_LeaveID",PK_LeaveID)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateLeave", para);
            return ds;
        }

        public DataSet DeletingLeave()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_LeaveID",PK_LeaveID),
                 new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteLeave", para);
            return ds;
        }

        #endregion

        #region AssignSubjecttoClass

        public DataSet SavingAssignSubject()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@Fk_SectionId",PK_SectionId),
                 new SqlParameter("@Fk_SubjectID",Pk_SubjectId),
                 new SqlParameter("@Pk_AssignId",Pk_AssignId),
                 new SqlParameter("@Status",Status),
                    new SqlParameter("@Fk_SessionId",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("SaveAssignSubject", para);
            return ds;
        }

        public DataSet GettingAssignSubject()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@Fk_SectionId",PK_SectionId),
                  new SqlParameter("@Fk_SessionId",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("GetSubjectNameBySection", para);
            return ds;
        }

        #endregion

        #region AssignSubjecttoTeacher

        public DataSet GettingTeacherList()
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


        public DataSet GettingSubjectNameByTeacher()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@Fk_SectionId",PK_SectionId),
                new SqlParameter("@FK_TeacherID",PK_TeacherID),
                new SqlParameter("@Fk_SessionID",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("GetSubjectNameByTeacher", para);
            return ds;
        }

        public DataSet SavingAssignTeacher()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@Fk_SectionId",PK_SectionId),
                new SqlParameter("@Fk_SubjectID",Pk_SubjectId),
                new SqlParameter("@FK_TeacherID",PK_TeacherID),
                new SqlParameter("@Pk_SubjecttoTeacherId",Pk_SubjecttoTeacherId),
                new SqlParameter("@Status",Status),
                new SqlParameter("@AddedBy",AddedBy),
                 new SqlParameter("@Fk_SessionId",SessionName),

            };
            DataSet ds = Connection.ExecuteQuery("SaveAssignTeacher", para);
            return ds;
        }
        #endregion

        #region Religion

        public DataSet SavingReligion()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@ReligionName",ReligionName)
            };
            DataSet ds = Connection.ExecuteQuery("SaveReligion", para);
            return ds;
        }

        public DataSet GettingReligionList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_ReligionID",PK_ReligionID)
            };
            DataSet ds = Connection.ExecuteQuery("GetReligion", para);
            return ds;
        }

        public DataSet UpdatingReligion()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@ReligionName",ReligionName),
                new SqlParameter("@PK_ReligionID",PK_ReligionID)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateReligion", para);
            return ds;
        }

        public DataSet DeletingReligion()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                new SqlParameter("@PK_ReligionID",PK_ReligionID),
            };
            DataSet ds = Connection.ExecuteQuery("DeleteReligion", para);
            return ds;
        }


        #endregion

        #region Notice

        public DataSet SavingNotice()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@FK_SectionId",PK_SectionId),
                new SqlParameter("@NoticeName",NoticeName),
                    new SqlParameter("@NoticeBy",NoticeBy),
                     new SqlParameter("@Fk_SessionId",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("SaveNoticeMaster", para);
            return ds;
        }
        public DataSet SaveNotice()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_ClassId",Fk_ClassID),
                new SqlParameter("@FK_SectionId",Fk_SectionID),
                new SqlParameter("@NoticeName",NoticeName),
                    new SqlParameter("@NoticeBy",NoticeBy),
            };
            DataSet ds = Connection.ExecuteQuery("SaveNoticeMaster", para);
            return ds;
        }
        public DataSet GettingNoticeList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_NoticeId",PK_NoticeId),
                new SqlParameter("@AddedBy",AddedBy),
                  new SqlParameter("@Fk_SessionId",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("GetNoticeList", para);
            return ds;
        }

        public DataSet UpdatingNotice()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@PK_NoticeId",PK_NoticeId),
                new SqlParameter("@FK_ClassId",Pk_ClassId),
                new SqlParameter("@FK_SectionId",PK_SectionId),
                new SqlParameter("NoticeName",NoticeName)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateNotice", para);
            return ds;
        }
        public DataSet UpdateNotice()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@PK_NoticeId",PK_NoticeId),
                new SqlParameter("@FK_ClassId",Fk_ClassID),
                new SqlParameter("@FK_SectionId",Fk_SectionID),
                new SqlParameter("NoticeName",NoticeName)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateNotice", para);
            return ds;
        }
        public DataSet DeletingNotice()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                 new SqlParameter("@PK_NoticeId",PK_NoticeId)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteNotice", para);
            return ds;
        }


        #endregion

        #region VehicleMaster

        public DataSet GettingVehicleType()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_VehicleTypeID",PK_VehicleTypeID)
            };
            DataSet ds = Connection.ExecuteQuery("GetVehicleType", para);
            return ds;
        }

        public DataSet SavingVehicle()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@FK_VehicleTypeID",PK_VehicleTypeID),
                new SqlParameter("@DriverContactNo",DriverContactNo),
                new SqlParameter("@DriverName",DriverName),
                new SqlParameter("@VehicleNo",VehicleNo),
                new SqlParameter("@Address",Address),
                new SqlParameter("@PinCode",PinCode),
                new SqlParameter("@State",State),
                new SqlParameter("@City",City)
            };
            DataSet ds = Connection.ExecuteQuery("SaveVehicle", para);
            return ds;
        }

        public DataSet GettingVehicleList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_VehicleMasterID",PK_VehicleMasterID),
                new SqlParameter("@FK_VehicleTypeID",PK_VehicleTypeID)
            };
            DataSet ds = Connection.ExecuteQuery("GetVehicleList", para);
            return ds;
        }

        public DataSet UpdatingVehicle()
        {
            SqlParameter[] para =
            {
               new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@FK_VehicleTypeID",PK_VehicleTypeID),
                new SqlParameter("@DriverContactNo",DriverContactNo),
                new SqlParameter("@DriverName",DriverName),
                new SqlParameter("@VehicleNo",VehicleNo),
                new SqlParameter("@PK_VehicleMasterID",PK_VehicleMasterID),
                new SqlParameter("@Address",Address),
                new SqlParameter("@PinCode",PinCode),
                new SqlParameter("@State",State),
                new SqlParameter("@City",City)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateVehicle", para);
            return ds;
        }

        public DataSet DeletingVehicle()
        {
            SqlParameter[] para =
            {
            new SqlParameter("@DeletedBy", DeletedBy),
            new SqlParameter("@PK_VehicleMasterID", PK_VehicleMasterID)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteVehicle", para);
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para ={
                                       new SqlParameter ("@PinCode",PinCode),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }

        #endregion

        #region Alot Vehicle
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

        public DataSet GetStudentList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_StudentID",Pk_StudentID),
                                    new SqlParameter("@Fk_SectionID",PK_SectionId),
                                    new SqlParameter("@Fk_ClassID",Pk_ClassId),
                                          new SqlParameter("@Session",SessionName),
                                };
            DataSet ds = Connection.ExecuteQuery("GetStudentList", para);
            return ds;
        }

        public DataSet SavingAlotVehicle()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@VehicleNo",VehicleNo),
                new SqlParameter("@DriverName",DriverName),
                new SqlParameter("@DriverContactNo",DriverContactNo),
                new SqlParameter("@FK_TeacherID",PK_TeacherID),
                new SqlParameter("@Fk_StudentID",Pk_StudentID),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@FK_RouteId",PK_RouteId),
                 new SqlParameter("@FK_AreaMasterID",PK_AreaMasterID),
                     new SqlParameter("@SessionName",SessionName),


        };
            DataSet ds = Connection.ExecuteQuery("SaveAlotVehicle", para);
            return ds;
        }

        public DataSet GettingVehicleDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ClassID",Pk_ClassId),
                 new SqlParameter("@Fk_SectionID",PK_SectionId),
                 new SqlParameter("@Type",Type),
                   new SqlParameter("@Fk_SessionId",SessionName),
                 
            };
            DataSet ds = Connection.ExecuteQuery("GetVehicleDetails", para);
            return ds;
        }

        #endregion

        #region AssignClassTeacher

        public DataSet SavingAssignClassTeacher()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_TeacherID",PK_TeacherID),
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                new SqlParameter("@Fk_SectionId",PK_SectionId),
                new SqlParameter("@AddedBy",AddedBy),
                   new SqlParameter("@SessionName",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("SaveAssignClassTeacher", para);
            return ds;
        }

        public DataSet GettingAssignClassTeacherList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_AssignClassTeacherId",Pk_AssignClassTeacherId),
                new SqlParameter("@Fk_ClassId",Pk_ClassId),
                 new SqlParameter("@FK_TeacherID",PK_TeacherID),
                new SqlParameter("@Fk_SectionId",PK_SectionId),
                 new SqlParameter("@Fk_SessionID",SessionName),

        };
            DataSet ds = Connection.ExecuteQuery("GetAssignClassTeacherList", para);
            return ds;
        }

        public DataSet DeletingAssignClassTeacher()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_AssignClassTeacherId",Pk_AssignClassTeacherId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteAssignClassTeacher", para);
            return ds;
        }

        #endregion

        #region VehicleType

        public DataSet SavingVehicleType()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@VehicleType",VehicleType)
            };
            DataSet ds = Connection.ExecuteQuery("SaveVehicleType", para);
            return ds;
        }

        public DataSet UpdatingVehicleType()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_VehicleTypeID",PK_VehicleTypeID),
                new SqlParameter("@VehicleType",VehicleType),
                new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateVehicleType", para);
            return ds;
        }

        public DataSet DeletingVehicleType()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_VehicleTypeID",PK_VehicleTypeID),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteVehicleType", para);
            return ds;
        }
        #endregion

        #region AreaMaster

        public DataSet SavingAreaMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@CreatedBy",AddedBy),
                new SqlParameter("@AreaName",AreaName),
                new SqlParameter("@Amount",Amount)
            };
            DataSet ds = Connection.ExecuteQuery("SaveAreaMaster", para);
            return ds;
        }

        public DataSet GettingAreaList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_AreaMasterID",PK_AreaMasterID)
            };
            DataSet ds = Connection.ExecuteQuery("GetAreaList", para);
            return ds;
        }

        public DataSet UpdatingAreaMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@PK_AreaMasterID",PK_AreaMasterID),
                new SqlParameter("@AreaName",AreaName),
                new SqlParameter("@Amount",Amount)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateAreaMaster", para);
            return ds;
        }

        public DataSet DeletingAreaMaster()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",DeletedBy),
                new SqlParameter("@PK_AreaMasterID",PK_AreaMasterID)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteAreaMaster", para);
            return ds;
        }

        #endregion

        #region Route

        public DataSet SavingRoute()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@RouteNo",RouteNo),
                new SqlParameter("@PickupTime",PickupTime),
                new SqlParameter("@DropTime",DropTime),
                new SqlParameter("@FK_VehicleTypeID",PK_VehicleTypeID),
                new SqlParameter("@FK_VehicleMasterID",PK_VehicleMasterID),
                new SqlParameter("@FK_AreaMasterID",PK_AreaMasterID),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("SaveRoute", para);
            return ds;
        }

        public DataSet GettingRoute()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RouteId",PK_RouteId)
            };
            DataSet ds = Connection.ExecuteQuery("GetRoute", para);
            return ds;
        }

        public DataSet GettingAreaByRoute()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RouteId",PK_RouteId),
                 new SqlParameter("@PK_AreaMasterID",PK_AreaMasterID)
            };
            DataSet ds = Connection.ExecuteQuery("GetAreaAmountByRoute", para);
            return ds;
        }
        public DataSet DeleteRouteList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RouteId",PK_RouteId),
                 new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteRouteList", para);
            return ds;
        }
        public DataSet AllotVehicleList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_AlotVehicleID",PK_AllotId),

            };
            DataSet ds = Connection.ExecuteQuery("AllotVehicleList", para);
            return ds;
        }
        public DataSet DeleteAllotVehicle()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_AllotId",PK_AllotId),
                 new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteAllotVehicle", para);
            return ds;
        }
        public DataSet RouteWiseTransportReport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@RouteNo",RouteNo),
                 new SqlParameter("@VehicleNo",VehicleNo)
            };
            DataSet ds = Connection.ExecuteQuery("RouteWiseTransportReport", para);
            return ds;
        }
        public DataSet RouteViewDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RouteId",PK_RouteId),

            };
            DataSet ds = Connection.ExecuteQuery("ViewRouteDetails", para);
            return ds;
        }
        #endregion

        #region Syllabus
        public DataSet SaveSyllabus()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@Fk_ClassID",Fk_ClassID),
                 new SqlParameter("@Fk_SectionID",Fk_SectionID),
                  new SqlParameter("@Syllabus",Syllabus),
                   new SqlParameter("@AddedBy",AddedBy),
                      new SqlParameter("@SessionID",SessionName),
                   
            };
            DataSet ds = Connection.ExecuteQuery("SaveSyllabus", para);
            return ds;
        }
        public DataSet SyllabusList()
        {
            SqlParameter[] para =
             {
              
                      new SqlParameter("@SessionID",SessionName),

            };
            DataSet ds = Connection.ExecuteQuery("SyllabusList",para);
            return ds;
        }
        public DataSet DeletingSyllabus()
        {
            SqlParameter[] para =
           {
                 new SqlParameter("@SyllabusID",SyllabusID),
                
                   new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteSyllabus", para);
            return ds;
        }

        #endregion
        #region TimeTable
        public DataSet SaveTimeTable()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@Fk_ClassID",Fk_ClassID),
                 new SqlParameter("@Fk_SectionID",Fk_SectionID),
                  new SqlParameter("@TimeTable",TimeTable),
                   new SqlParameter("@AddedBy",AddedBy),
                   new SqlParameter("@SessionID",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("SaveTimeTable", para);
            return ds;
        }
        public DataSet TimeTableList()
        {

            SqlParameter[] para =
             {

                      new SqlParameter("@SessionID",SessionName),

            };
            DataSet ds = Connection.ExecuteQuery("TimeTableList",para);
            return ds;
        }
        public DataSet DeletingTimeTable()
        {
            SqlParameter[] para =
           {
                 new SqlParameter("@TimeTableID",TimeTableID),

                   new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteTimeTable", para);
            return ds;
        }
        public DataSet SyllabusListForParent()
        {
            SqlParameter[] para =
           {
                 new SqlParameter("@Pk_ParentID",AddedBy), 
                   new SqlParameter("@StudentId",Name)
            };
            DataSet ds = Connection.ExecuteQuery("GetSyllabusForParent", para);
            return ds;
        }
        public DataSet TimeTableListsForParent()
        {
            SqlParameter[] para =
           {
                 new SqlParameter("@Pk_ParentID",AddedBy),
                   new SqlParameter("@StudentId",Name)
            };
            DataSet ds = Connection.ExecuteQuery("GetTimeTableForParent", para);
            return ds;
        }

        #endregion
        #region Holiday

        public DataSet SaveHoliday()
        {
            SqlParameter[] para =
            {

                 new SqlParameter("@HolidayDate",HolidayDate),
                  new SqlParameter("@HolidayName",HolidayName),
                   new SqlParameter("@AddedBy",AddedBy),
                     new SqlParameter("@SessionName",SessionName),
            };
            DataSet ds = Connection.ExecuteQuery("AddHoliday", para);
            return ds;
        }
        public DataSet UpdateHoliday()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@PK_HolidayId",HolidayID),
                 new SqlParameter("@HolidayDate",HolidayDate),
                  new SqlParameter("@HolidayName",HolidayName),
                   new SqlParameter("@AddedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateHoliday", para);
            return ds;
        }
        public DataSet HolidayList()
        {

            SqlParameter[] para =
             {

                      new SqlParameter("@SessionID",SessionName),

            };
            DataSet ds = Connection.ExecuteQuery("HolidayList",para);
            return ds;
        }
        public DataSet DeletingHoliday()
        {
            SqlParameter[] para =
           {
                 new SqlParameter("@PK_HolidayId",HolidayID),
                   new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteHoliday", para);
            return ds;
        }

        #endregion
    }
}