using AfluexSchool.Models;
using APSSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AfluexSchool.Models.Common;

namespace APSSchool.Controllers
{
    public class MasterForApiController : Controller
    {
        // GET: MasterForApi


        public ActionResult Index()
        {
            return View();
        }
        #region Login
        public ActionResult Login(Login objParameters)
        {
            Login obj = new Login();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Password == "" || objParameters.Password == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.Password = (objParameters.Password);
                DataSet dsResult = objParameters.LoginAction();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        objParameters.Pk_ParentID = dsResult.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                        DataSet dsResult1 = objParameters.SaveDeviceDetails();
                        obj.Status = "0";
                        obj.Pk_ParentID = dsResult.Tables[0].Rows[0]["Pk_ParentID"].ToString();
                        obj.LoginId = dsResult.Tables[0].Rows[0]["LoginID"].ToString();
                        obj.ParentName = dsResult.Tables[0].Rows[0]["ParentName"].ToString();


                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Login

        #region Dashboard
        public ActionResult DashBoardDetails(DashBoard1 objParameters)
        {
            DashBoard1 obj = new DashBoard1();
            List<StudentData> datalist = new List<StudentData>();
            try
            {
                DataSet dsResult = objParameters.GetDashBoardDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {
                            obj.lstdashboarddata = datalist;
                        }
                        List<StudentDetails> objstudent = new List<StudentDetails>();
                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new StudentDetails
                                {
                                    RollNo = row1["RollNo"].ToString(),
                                    StudentName = row1["StudentName"].ToString(),
                                    State = row1["State"].ToString(),
                                    City = row1["City"].ToString(),
                                    StudentPhoto = row1["StudentPhoto"].ToString(),
                                    ClassName = row1["ClassName"].ToString(),
                                    SectionName = row1["SectionName"].ToString(),
                                    DateOfBirth = row1["DateOfBirth"].ToString(),
                                    Gender = row1["Gender"].ToString(),
                                    Mobile = row1["Mobile"].ToString(),
                                    FatherOcc = row1["FatherOcc"].ToString(),
                                    MotherOcc = row1["MotherOcc"].ToString(),
                                    PermanentAddress = row1["PermanentAddress"].ToString(),
                                    CorrespondenceAddress = row1["CorrespondenceAddress"].ToString(),
                                    Pk_StudentId = row1["Pk_StudentId"].ToString(),

                                });
                            }
                            datalist.Add(new StudentData
                            {
                                Title = "Student Details",
                                StudentDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Dashboard

        #region Attendance
        public ActionResult Attendance(Attendance objParameters)
        {
            Attendance obj = new Attendance();
            List<Data1> datalist = new List<Data1>();
            List<Data> datalist1 = new List<Data>();
            DataSet dsResult = objParameters.GetAttendanceDetails();

            if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
            {
                if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    obj.Status = "0";
                    obj.TotalPresent = dsResult.Tables[2].Rows[0]["TotalPresent"].ToString();
                    obj.TotalAbsent = dsResult.Tables[3].Rows[0]["TotalAbsent"].ToString();
                    obj.TotalLeave = dsResult.Tables[4].Rows[0]["TotalLeave"].ToString();
                    foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                    {


                        obj.AttendanceList = datalist;
                        obj.MonthList = datalist1;


                    }

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<MonthDetails> objrecentjoined = new List<MonthDetails>();

                        {
                            #region CategoryDetails
                            for (int i = 0; i <= dsResult.Tables[1].Rows.Count - 1; i++)
                            {
                                List<AttendanceDetails> objsub = new List<AttendanceDetails>();
                                objrecentjoined.Add(new MonthDetails

                                {
                                    MonthName = dsResult.Tables[1].Rows[i]["MonthName"].ToString(),
                                    MonthId = dsResult.Tables[1].Rows[i]["MonthId"].ToString(),



                                });
                                for (int j = 0; j <= dsResult.Tables[0].Rows.Count - 1; j++)
                                {

                                    if (dsResult.Tables[1].Rows[i]["MonthId"].ToString() == dsResult.Tables[0].Rows[j]["Month"].ToString())
                                    {
                                        objsub.Add(new AttendanceDetails

                                        {
                                            Status = dsResult.Tables[0].Rows[j]["Status"].ToString(),
                                            Day = dsResult.Tables[0].Rows[j]["Day"].ToString(),
                                            AttendanceDate = dsResult.Tables[0].Rows[j]["AttendanceDate"].ToString(),


                                        });
                                    }
                                }
                                objrecentjoined[i].AttendanceDetails = objsub;
                            }
                            datalist.Add(new Data1
                            {

                                MonthDetails = objrecentjoined,


                            });
                            #endregion
                        }

                    }
                }
                else
                {
                    obj.Status = "1";
                }


            }

            else
            {

                obj.Status = "1";
                obj.TotalPresent = dsResult.Tables[2].Rows[0]["TotalPresent"].ToString();
                obj.TotalAbsent = dsResult.Tables[3].Rows[0]["TotalAbsent"].ToString();
                obj.TotalLeave = dsResult.Tables[4].Rows[0]["TotalLeave"].ToString();

                return Json(obj, JsonRequestBehavior.AllowGet);

            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion Attendance

        #region HomeWork
        public ActionResult HomeWork(HomeWork objParameters)
        {
            HomeWork obj = new HomeWork();
            List<HomeWorkData> datalist = new List<HomeWorkData>();
            try
            {
                objParameters.FromDate = string.IsNullOrEmpty(objParameters.FromDate) ? null : Common.ConvertToSystemDate(objParameters.FromDate, "dd/MM/yyyy");
                objParameters.ToDate = string.IsNullOrEmpty(objParameters.ToDate) ? null : Common.ConvertToSystemDate(objParameters.ToDate, "dd/MM/yyyy");
                DataSet dsResult = objParameters.GetHomeworkForParent();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        obj.Message = "Homework List Fetched Successfully.";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lsthomeworkDetails = datalist;


                        }
                        List<HomeWorkDetails> objstudent = new List<HomeWorkDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new HomeWorkDetails

                                {
                                    StudentName = row1["StudentName"].ToString(),
                                    Pk_StudentID = row1["Pk_StudentID"].ToString(),
                                    ClassName = row1["ClassName"].ToString(),
                                    Pk_ClassID = row1["Pk_ClassID"].ToString(),
                                    SectionName = row1["SectionName"].ToString(),
                                    Pk_SectionID = row1["Pk_SectionID"].ToString(),
                                    SubjectName = row1["SubjectName"].ToString(),
                                    Pk_SubjectID = row1["Pk_SubjectID"].ToString(),
                                    HomeworkText = row1["HomeworkText"].ToString(),
                                    HomeworkDate = row1["HomeworkDate"].ToString(),
                                    HomeworkFile = row1["HomeworkFile"].ToString(),


                                });
                            }
                            datalist.Add(new HomeWorkData
                            {
                                Title = "HomeWork Details",
                                HomeWorkDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion HomeWork

        #region Sylabus
        public ActionResult Sylabus(Sylabus objParameters)
        {
            Sylabus obj = new Sylabus();
            List<SylabusData> datalist = new List<SylabusData>();
            try
            {

                DataSet dsResult = objParameters.GetSylabusDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstSylabusDetails = datalist;


                        }
                        List<SylabusDetails> objstudent = new List<SylabusDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new SylabusDetails

                                {


                                    Syllabus = row1["Syllabus"].ToString(),


                                });
                            }
                            datalist.Add(new SylabusData
                            {
                                Title = "Sylabus Details",
                                SylabusDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Sylabus


        #region FeeReport
        public ActionResult FeeReport(FeeReport objParameters)
        {
            FeeReport obj = new FeeReport();
            List<FeeReportData> datalist = new List<FeeReportData>();
            try
            {

                DataSet dsResult = objParameters.GetFeeReport();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstfeedetails = datalist;


                        }
                        List<FeeReportDetails> objstudent = new List<FeeReportDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new FeeReportDetails

                                {


                                    Amount = row1["Amount"].ToString(),
                                    ReceiptNo = row1["ReceiptNo"].ToString(),
                                    PaymentDate = row1["PaymentDate"].ToString(),
                                    PaymentMode = row1["PaymentMode"].ToString(),
                                    BankDetails = row1["BankDetails"].ToString(),
                                    StudentName = row1["StudentName"].ToString(),

                                });
                            }
                            datalist.Add(new FeeReportData
                            {
                                Title = "Fee Details",
                                FeeReportDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion FeeReport

        #region TimeTable
        public ActionResult TimeTable(TimeTable objParameters)
        {
            TimeTable obj = new TimeTable();
            List<TimeTableData> datalist = new List<TimeTableData>();
            try
            {

                DataSet dsResult = objParameters.GetTimeTableForParent();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lsttimetabledetails = datalist;


                        }
                        List<TimeTableDetails> objstudent = new List<TimeTableDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new TimeTableDetails

                                {


                                    TimeTable = row1["TimeTable"].ToString(),
                                    StudentName = row1["StudentName"].ToString(),


                                });
                            }
                            datalist.Add(new TimeTableData
                            {
                                Title = "TimeTable Details",
                                TimeTableDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Sylabus

        #region ApplyLeave
        public ActionResult ApplyLeave(Leave1 objParameters)
        {
            Leave1 obj = new Leave1();
            if (objParameters.Pk_StudentID == "" || objParameters.Pk_StudentID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass StudentID";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.FromDate == "" || objParameters.FromDate == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter From Date";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.ToDate == "" || objParameters.ToDate == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter To Date";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Reason == "" || objParameters.Reason == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Reason";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.FromDate = string.IsNullOrEmpty(objParameters.FromDate) ? null : Common.ConvertToSystemDate(objParameters.FromDate, "dd/MM/yyyy");
                objParameters.ToDate = string.IsNullOrEmpty(objParameters.ToDate) ? null : Common.ConvertToSystemDate(objParameters.ToDate, "dd/MM/yyyy");

                DataSet dsResult = objParameters.SaveLeave();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "Leave Applied Successfully";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ApplyLeave

        #region LeaveList
        public ActionResult LeaveList(LeaveList objParameters)
        {
            LeaveList obj = new LeaveList();
            List<LeaveListData> datalist = new List<LeaveListData>();
            try
            {

                DataSet dsResult = objParameters.StudentLeaveApplicationList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstleavelist = datalist;


                        }
                        List<LeaveListDetails> objstudent = new List<LeaveListDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new LeaveListDetails

                                {


                                    Reason = row1["Reason"].ToString(),
                                    FromDate = row1["FromDate"].ToString(),
                                    ToDate = row1["ToDate"].ToString(),
                                    Status = row1["IsApproved"].ToString(),


                                });
                            }
                            datalist.Add(new LeaveListData
                            {
                                Title = "Leave Details",
                                LeaveListDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ChangePassword
        public ActionResult ChangePassword(ChangePas objParameters)
        {
            ChangePas obj = new ChangePas();
            if (objParameters.OldPassword == "" || objParameters.OldPassword == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter OldPassword";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.NewPassword == "" || objParameters.NewPassword == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter New Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Pk_ParentId == "" || objParameters.Pk_ParentId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please pass ParentId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {

                DataSet dsResult = objParameters.ChangeParentPassword();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "Password Changed Successfully";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ChangePassword

        #region BirthDay
        public ActionResult BirthDay(BirthDay objParameters)
        {
            BirthDay obj = new BirthDay();
            List<BirthDayData> datalist = new List<BirthDayData>();
            try
            {

                DataSet dsResult = objParameters.GetBirthday();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstBirthDaylist = datalist;


                        }
                        List<BirthDayDetails> objstudent = new List<BirthDayDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new BirthDayDetails

                                {


                                    StudentName = row1["StudentName"].ToString(),
                                    ClassName = row1["ClassName"].ToString(),
                                    StudentPhoto = row1["StudentPhoto"].ToString(),
                                    SectionName = row1["SectionName"].ToString(),


                                });
                            }
                            datalist.Add(new BirthDayData
                            {
                                Title = "BirthDay Details",
                                BirthDayDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion BirthDay

        #region Notice
        public ActionResult Notice(Notice objParameters)
        {
            Notice obj = new Notice();
            List<NoticeData> datalist = new List<NoticeData>();
            try
            {

                DataSet dsResult = objParameters.GetNotice();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstNoticelist = datalist;


                        }
                        List<NoticeDetails> objstudent = new List<NoticeDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new NoticeDetails

                                {


                                    NoticeDate = row1["AddedOn"].ToString(),

                                    NoticeName = row1["NoticeName"].ToString(),

                                });
                            }
                            datalist.Add(new NoticeData
                            {
                                Title = "Notice Details",
                                NoticeDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Notice

        #region Banner
        public ActionResult Banner(Banner objParameters)
        {
            Banner obj = new Banner();
            List<BannerData> datalist = new List<BannerData>();
            try
            {

                DataSet dsResult = objParameters.GetBanner();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstBannerlist = datalist;


                        }
                        List<BannerDetails> objstudent = new List<BannerDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new BannerDetails

                                {


                                    BannerName = row1["BannerName"].ToString(),



                                });
                            }
                            datalist.Add(new BannerData
                            {
                                Title = "Banner Details",
                                BannerDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Banner

        #region Complain
        public ActionResult Complain(ComplainBox objParameters)
        {
            ComplainBox obj = new ComplainBox();
            if (objParameters.Pk_ParentId == "" || objParameters.Pk_ParentId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass ParentId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            if (objParameters.Complain == "" || objParameters.Complain == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Complain";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {

                DataSet dsResult = objParameters.SaveComplain();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "Complain Add Successfully";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Complain

        #region ComplainList
        public ActionResult ComplainList(ComplainList objParameters)
        {
            ComplainList obj = new ComplainList();
            List<ComplainListData> datalist = new List<ComplainListData>();
            try
            {

                DataSet dsResult = objParameters.GetComplain();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstComplainList = datalist;


                        }
                        List<ComplainListDetails> objstudent = new List<ComplainListDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new ComplainListDetails

                                {


                                    ComplainDate = row1["ComplainDate"].ToString(),
                                    Complain = row1["Complain"].ToString(),
                                    ReplyDate = row1["ReplyDate"].ToString(),
                                    Reply = row1["Reply"].ToString(),


                                });
                            }
                            datalist.Add(new ComplainListData
                            {
                                Title = "Complain Details",
                                ComplainListDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Sylabus
        #region SaveDeviceDetails
        public ActionResult SaveDeviceDetails(DeviceDetails objParameters)
        {
            DeviceDetails obj = new DeviceDetails();

            try
            {

                DataSet dsResult = objParameters.SaveDeviceDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Status = "0";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion SaveDeviceDetails

        #region Logout
        public ActionResult Logout(Logout objParameters)
        {
            Logout obj = new Logout();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.DeviceId == "" || objParameters.DeviceId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Device Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {

                DataSet dsResult = objParameters.UpdateDeviceId();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Status = "0";

                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Logout

        #region Attendance
        public ActionResult AttendanceNew(Attendance objParameters)
        {
            Attendance obj = new Attendance();
            List<Data1> datalist = new List<Data1>();
            List<Data> datalist1 = new List<Data>();
            DataSet dsResult = objParameters.GetAttendanceDetailsNew();

            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                obj.Status = "0";
                obj.TotalPresent = dsResult.Tables[2].Rows[0]["TotalPresent"].ToString();
                obj.TotalAbsent = dsResult.Tables[3].Rows[0]["TotalAbsent"].ToString();
                obj.TotalLeave = dsResult.Tables[4].Rows[0]["TotalLeave"].ToString();
                foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                {


                    obj.AttendanceList = datalist;
                    obj.MonthList = datalist1;


                }

                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    List<MonthDetails> objrecentjoined = new List<MonthDetails>();

                    {
                        #region CategoryDetails
                        for (int i = 0; i <= dsResult.Tables[1].Rows.Count - 1; i++)
                        {
                            List<AttendanceDetails> objsub = new List<AttendanceDetails>();
                            objrecentjoined.Add(new MonthDetails

                            {
                                MonthName = dsResult.Tables[1].Rows[i]["MonthName"].ToString(),
                                MonthId = dsResult.Tables[1].Rows[i]["MonthId"].ToString(),



                            });
                            for (int j = 0; j <= dsResult.Tables[0].Rows.Count - 1; j++)
                            {

                                if (dsResult.Tables[1].Rows[i]["MonthId"].ToString() == dsResult.Tables[0].Rows[j]["Month"].ToString())
                                {
                                    objsub.Add(new AttendanceDetails

                                    {
                                        Status = dsResult.Tables[0].Rows[j]["Status"].ToString(),
                                        Day = dsResult.Tables[0].Rows[j]["Day"].ToString(),
                                        AttendanceDate = dsResult.Tables[0].Rows[j]["AttendanceDate"].ToString(),


                                    });
                                }
                            }
                            objrecentjoined[i].AttendanceDetails = objsub;
                        }
                        datalist.Add(new Data1
                        {

                            MonthDetails = objrecentjoined,


                        });
                        #endregion
                    }

                }


            }

            else
            {

                obj.TotalLeave = "1";


                return Json(obj, JsonRequestBehavior.AllowGet);

            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion Attendance

        #region TeacherLogin
        public ActionResult TeacherLogin(TeacherLogin objParameters)
        {
            TeacherLogin obj = new TeacherLogin();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Password == "" || objParameters.Password == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.Password = (objParameters.Password);
                DataSet dsResult = objParameters.TeacherLoginAction();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        objParameters.PK_TeacherID = dsResult.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                        DataSet dsResult1 = objParameters.SaveDeviceDetails();
                        obj.Status = "0";
                        obj.PK_TeacherID = dsResult.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                        obj.LoginId = dsResult.Tables[0].Rows[0]["LoginID"].ToString();
                        obj.TeacherName = dsResult.Tables[0].Rows[0]["Name"].ToString();
                        obj.ImagePath = dsResult.Tables[0].Rows[0]["ImagePath"].ToString();

                        if (dsResult.Tables[1].Rows.Count > 0)
                        {
                            obj.IsClassTeacher = "1";
                            obj.Fk_ClassId = dsResult.Tables[1].Rows[0]["Fk_ClassId"].ToString();
                            obj.Fk_SectionId = dsResult.Tables[1].Rows[0]["Fk_SectionId"].ToString();
                        }
                        else
                        {
                            obj.IsClassTeacher = "0";
                        }


                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion TeacherLogin


        #region TeacherHomeWork
        public ActionResult ClassForTeacher(GetClass objParameters)
        {
            GetClass obj = new GetClass();
            List<ClassData> datalist = new List<ClassData>();
            try
            {

                DataSet dsResult = objParameters.GetClassList();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {
                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstclassdetails = datalist;


                        }
                        List<ClassDetails> objstudent = new List<ClassDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objstudent.Add(new ClassDetails

                                {

                                    PK_ClassID = row1["PK_ClassID"].ToString(),
                                    ClassName = row1["ClassName"].ToString(),


                                });
                            }
                            datalist.Add(new ClassData
                            {
                                Title = "Class Details",
                                ClassDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SectionForTeacher(GetSection objParameters)
        {
            GetSection obj = new GetSection();
            List<SectionData> datalist = new List<SectionData>();
            try
            {

                DataSet dsResult = objParameters.GetSectionByClass();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {
                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstsectiondetails = datalist;


                        }
                        List<SectionDetails> objstudent = new List<SectionDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objstudent.Add(new SectionDetails

                                {

                                    PK_SectionId = row1["PK_SectionId"].ToString(),
                                    SectionName = row1["SectionName"].ToString(),


                                });
                            }
                            datalist.Add(new SectionData
                            {
                                Title = "Section Details",
                                SectionDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult SubjectForTeacher(GetSubject objParameters)
        {
            GetSubject obj = new GetSubject();
            List<SubjectData> datalist = new List<SubjectData>();
            try
            {

                DataSet dsResult = objParameters.GetSubjectNameBySection();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstSubjectdetails = datalist;


                        }
                        List<SubjectDetails> objstudent = new List<SubjectDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                if (row1["Status"].ToString() == "Checked")
                                {
                                    objstudent.Add(new SubjectDetails

                                    {

                                        SubjectName = row1["SubjectName"].ToString(),
                                        Fk_SubjectID = row1["Fk_SubjectID"].ToString(),


                                    });
                                }
                            }
                            datalist.Add(new SubjectData
                            {
                                Title = "Subject Details",
                                SubjectDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HomeWorkList(TeacherHomeWork objParameters)
        {
            TeacherHomeWork obj = new TeacherHomeWork();
            List<HomeWorkData> datalist = new List<HomeWorkData>();
            try
            {
                objParameters.FromDate = string.IsNullOrEmpty(objParameters.FromDate) ? null : Common.ConvertToSystemDate(objParameters.FromDate, "dd/MM/yyyy");
                objParameters.ToDate = string.IsNullOrEmpty(objParameters.ToDate) ? null : Common.ConvertToSystemDate(objParameters.ToDate, "dd/MM/yyyy");
                DataSet dsResult = objParameters.HomeworkList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lsthomeworkDetails = datalist;


                        }
                        List<HomeWorkDetails> objstudent = new List<HomeWorkDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new HomeWorkDetails

                                {
                                    Pk_HomeworkID = row1["Pk_HomeworkID"].ToString(),
                                    SubjectName = row1["SubjectName"].ToString(),
                                    ClassName = row1["ClassName"].ToString(),
                                    SectionName = row1["SectionName"].ToString(),
                                    HomeworkDate = row1["HomeworkDate"].ToString(),
                                    HomeworkFile = row1["HomeworkFile"].ToString(),

                                });
                            }
                            datalist.Add(new HomeWorkData
                            {
                                Title = "HomeWork Details",
                                HomeWorkDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteHomeWork(DeleteHomeWOrk objParameters)
        {
            DeleteHomeWOrk obj = new DeleteHomeWOrk();

            try
            {

                DataSet dsResult = objParameters.DeleteHomework();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Status = "0";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Home Work Not Deleted Please Contact to Admin.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion TeacherHomeWork


        #region StudentAttendance
        public ActionResult GetStudentForAttendance(StudentList objParameters)
        {
            StudentList obj = new StudentList();
            List<StudentListData> datalist = new List<StudentListData>();
            try
            {

                DataSet dsResult = objParameters.GetStudentList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstStudentList = datalist;


                        }
                        List<StudentListDetails> objstudent = new List<StudentListDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new StudentListDetails

                                {


                                    Mobile = row1["Mobile"].ToString(),
                                    StudentName = row1["StudentName"].ToString(),
                                    Pk_StudentID = row1["Pk_StudentID"].ToString(),


                                });
                            }
                            datalist.Add(new StudentListData
                            {
                                Title = "Student Details",
                                StudentListDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MarkAttendance(MarkAttendance obj)
        {
            try
            {
                DataTable dtstudent = new DataTable();
                obj.AttendanceDate = Common.ConvertToSystemDate(obj.AttendanceDate, "dd/MM/yyyy");
                dtstudent.Columns.Add("Pk_StudentID", typeof(string));
                dtstudent.Columns.Add("Status", typeof(string));
                for (int i = 0; i <= obj.lstMarkAttendance[0].MarkAttendancDetails.Count - 1; i++)
                {
                    if (obj.lstMarkAttendance[0].MarkAttendancDetails[i].Status == "P")
                    {
                        obj.Status = "P";
                    }
                    else
                    {
                        try
                        {
                            string str2 = "Dear Parent,Your ward " + obj.lstMarkAttendance[0].MarkAttendancDetails[i].StudentName + " is absent on ." + obj.AttendanceDate;
                            BLSMS.SendSMS2("", "", "", obj.lstMarkAttendance[0].MarkAttendancDetails[i].Mobile, str2);
                            SendSMS objsms = new SendSMS();
                            string Status = "";
                            DataTable dtSMS = new DataTable();
                            dtSMS.Columns.Add("Name", typeof(string));
                            dtSMS.Columns.Add("Mobile", typeof(string));
                            dtSMS.Columns.Add("Status", typeof(string));

                            if (obj.lstMarkAttendance[0].MarkAttendancDetails[i].Mobile.Length < 10)
                            {
                                Status = "Failed";
                            }
                            else if (obj.lstMarkAttendance[0].MarkAttendancDetails[i].Mobile.Length > 10)
                            {
                                Status = "Failed";
                            }
                            else
                            {
                                Status = "Send";
                            }

                            dtSMS.Rows.Add(obj.lstMarkAttendance[0].MarkAttendancDetails[i].StudentName, obj.lstMarkAttendance[0].MarkAttendancDetails[i].Mobile, Status);
                            objsms.dtSMS = dtSMS;
                            objsms.AddedBy = obj.AddedBy;
                            objsms.SMS = str2;
                            objsms.TotalSMS = "1";
                            objsms.SMSTemplateText = "Student Attendance";
                            DataSet ds1 = objsms.SaveSMSData();
                        }
                        catch { }
                        obj.Status = "A";
                    }


                    dtstudent.Rows.Add(obj.lstMarkAttendance[0].MarkAttendancDetails[i].Pk_StudentID, obj.Status);
                }
                obj.dsStudentAttendance = dtstudent;


                DataSet ds = obj.SaveAttendance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        MarkAttendance obj1 = new Models.MarkAttendance();
                        obj1.Status = "1";
                        return Json(obj1, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        MarkAttendance obj1 = new Models.MarkAttendance();
                        obj1.Status = "0";
                        obj1.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj1, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception ex)
            {
                MarkAttendance obj1 = new Models.MarkAttendance();
                obj1.Status = "0";
                obj1.ErrorMessage = ex.Message;
                return Json(obj1, JsonRequestBehavior.AllowGet);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion StudentAttendance

        #region NoticeForTeacher
        public ActionResult AddNotice(AddNotice objParameters)
        {
            AddNotice obj = new AddNotice();
            if (objParameters.PK_ClassID == "0")
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass Class";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.PK_SectionId == "0")
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass Section";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Notice == "" || objParameters.Notice == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Notice";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                DataSet dsResult = objParameters.SaveNotice();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "Notice saved successfull !!";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NoticeList(NoticeList objParameters)
        {
            NoticeList obj = new NoticeList();
            List<NoticeData> datalist = new List<NoticeData>();
            try
            {

                DataSet dsResult = objParameters.GetNotice();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {
                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {
                            obj.lstNoticeList = datalist;
                        }
                        List<NoticeDetails> objstudent = new List<NoticeDetails>();
                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objstudent.Add(new NoticeDetails
                                {
                                    NoticeName = row1["NoticeName"].ToString(),
                                    NoticeDate = row1["NoticeDate"].ToString(),
                                    PK_NoticeId = row1["PK_NoticeId"].ToString(),
                                });
                            }
                            datalist.Add(new NoticeData
                            {
                                Title = "Notice Details",
                                NoticeDetails = objstudent
                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteNotice(DeleteNotice objParameters)
        {
            DeleteNotice obj = new DeleteNotice();

            try
            {

                DataSet dsResult = objParameters.DeleteNoticeData();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Status = "0";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Notice Not Deleted Please Contact to Admin.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion NoticeForTeacher

        #region SelfAttendance
        public ActionResult SelfAttendance(SelfAttendance objParameters)
        {
            SelfAttendance obj = new SelfAttendance();
            List<AttendaneceData> datalist = new List<AttendaneceData>();
            try
            {
                objParameters.FromDate = string.IsNullOrEmpty(objParameters.FromDate) ? null : Common.ConvertToSystemDate(objParameters.FromDate, "dd/MM/yyyy");
                objParameters.ToDate = string.IsNullOrEmpty(objParameters.ToDate) ? null : Common.ConvertToSystemDate(objParameters.ToDate, "dd/MM/yyyy");
                DataSet dsResult = objParameters.GetAttendanceDetailsNew();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.AttendanceList = datalist;


                        }
                        List<SelfAttendanceDetails> objstudent = new List<SelfAttendanceDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new SelfAttendanceDetails

                                {


                                    AttendanceDate = row1["AttendanceDate"].ToString(),
                                    InTime = row1["InTime"].ToString(),
                                    OutTime = row1["OutTime"].ToString(),



                                });
                            }
                            datalist.Add(new AttendaneceData
                            {

                                AttendanceDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion SelfAttendance


        #region StudentListForLeaveApproval
        public ActionResult StudentListForLeaveApproval(LeaveList objParameters)
        {
            LeaveList obj = new LeaveList();
            List<LeaveListData> datalist = new List<LeaveListData>();
            try
            {
                DataSet dsResult = objParameters.TeacherStudentsLeaveApplication();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {
                            obj.lstleavelist = datalist;
                        }
                        List<LeaveListDetails> objstudent = new List<LeaveListDetails>();
                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new LeaveListDetails
                                {
                                    Reason = row1["Reason"].ToString(),
                                    FromDate = row1["FromDate"].ToString(),
                                    ToDate = row1["ToDate"].ToString(),
                                    Status = row1["IsApproved"].ToString(),
                                    PK_StdntLeaveID = row1["PK_StdntLeaveID"].ToString(),
                                });
                            }
                            datalist.Add(new LeaveListData
                            {
                                Title = "Leave Details",
                                LeaveListDetails = objstudent
                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ApproveLeave(ApproveLeaveForStudent objParameters)
        {
            ApproveLeaveForStudent obj = new ApproveLeaveForStudent();

            try
            {

                DataSet dsResult = objParameters.ApproveLeave();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Status = "0";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion StudentListForLeaveApproval


        #region ComplainListForTeacher
        public ActionResult ComplainListForTeacher(ComplainList objParameters)
        {
            ComplainList obj = new ComplainList();
            List<ComplainListData> datalist = new List<ComplainListData>();
            try
            {

                DataSet dsResult = objParameters.GetComplainForTecaher();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstComplainList = datalist;


                        }
                        List<ComplainListDetails> objstudent = new List<ComplainListDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new ComplainListDetails

                                {


                                    ComplainDate = row1["ComplainDate"].ToString(),
                                    Complain = row1["Complain"].ToString(),
                                    ReplyDate = row1["ReplyDate"].ToString(),
                                    Reply = row1["Reply"].ToString(),
                                    Pk_MessageId = row1["Pk_MessageId"].ToString(),

                                });
                            }
                            datalist.Add(new ComplainListData
                            {
                                Title = "Complain Details",
                                ComplainListDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReplyComplain(ReplyComplain objParameters)
        {
            ReplyComplain obj = new ReplyComplain();

            try
            {

                DataSet dsResult = objParameters.ReplyComplainByTeacher();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Status = "0";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ComplainListForTeacher


        #region ApplyLeave
        public ActionResult ApplyLeaveForTeacher(ApplyLeave objParameters)
        {
            ApplyLeave obj = new ApplyLeave();
            if (objParameters.Pk_TeacherId == "" || objParameters.Pk_TeacherId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass TecherId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.FromDate == "" || objParameters.FromDate == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter From Date";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.ToDate == "" || objParameters.ToDate == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter To Date";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Reason == "" || objParameters.Reason == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Reason";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.FromDate = Common.ConvertToSystemDate(objParameters.FromDate, "dd/MM/yyyy");
                objParameters.ToDate = Common.ConvertToSystemDate(objParameters.ToDate, "dd/MM/yyyy");
                DataSet dsResult = objParameters.SaveLeave();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "Leave Applied Successfully";



                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ApplyLeave

        #region LeaveList
        public ActionResult LeaveListForTeacher(LeaveListForTeacher objParameters)
        {
            LeaveListForTeacher obj = new LeaveListForTeacher();
            List<LeaveListData> datalist = new List<LeaveListData>();
            try
            {

                DataSet dsResult = objParameters.TeacherLeaveApplicationList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstleavelist = datalist;


                        }
                        List<LeaveListDetails> objstudent = new List<LeaveListDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new LeaveListDetails

                                {


                                    Reason = row1["Remark"].ToString(),
                                    FromDate = row1["FromDate"].ToString(),
                                    ToDate = row1["ToDate"].ToString(),
                                    Status = row1["LeaveStatus"].ToString(),


                                });
                            }
                            datalist.Add(new LeaveListData
                            {
                                Title = "Leave Details",
                                LeaveListDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Sylabus

        #region ExamType
        public ActionResult GetExamType(ExamType objParameters)
        {
            ExamType obj = new ExamType();
            List<ExamTypeData> datalist = new List<ExamTypeData>();
            try
            {

                DataSet dsResult = objParameters.GetExamType();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstexamdetails = datalist;


                        }
                        List<ExamDetails> objstudent = new List<ExamDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new ExamDetails

                                {

                                    Pk_ExamTypeId = row1["Pk_ExamTypeId"].ToString(),
                                    ExamTypeName = row1["ExamTypeName"].ToString(),


                                });
                            }
                            datalist.Add(new ExamTypeData
                            {
                                Title = "Exam Details",
                                ExamDetails = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ExamType

        public ActionResult SaveStudentMarks(StudentMarks obj)
        {
            try
            {
                DataTable dtstudent = new DataTable();

                dtstudent.Columns.Add("Pk_StudentID", typeof(string));
                dtstudent.Columns.Add("ObtainMarks", typeof(string));
                dtstudent.Columns.Add("MaximumMarks", typeof(string));
                for (int i = 0; i <= obj.lstMarks[0].StudentMarksDetails.Count - 1; i++)
                {

                    dtstudent.Rows.Add(obj.lstMarks[0].StudentMarksDetails[i].Pk_StudentID, obj.lstMarks[0].StudentMarksDetails[i].ObtainMarks, 0);
                }
                obj.dsStudentAttendance = dtstudent;


                DataSet ds = obj.InsertStudentSubjectMarks();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        StudentMarks obj1 = new Models.StudentMarks();
                        obj1.Status = "0";
                        return Json(obj1, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        StudentMarks obj1 = new Models.StudentMarks();
                        obj1.Status = "1";
                        obj1.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj1, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception ex)
            {
                StudentMarks obj1 = new Models.StudentMarks();
                obj1.Status = "1";
                obj1.ErrorMessage = ex.Message;
                return Json(obj1, JsonRequestBehavior.AllowGet);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSession(MarkSheet objParameters)
        {
            MarkSheet obj = new MarkSheet();
            List<Session> datalist = new List<Session>();
            try
            {
                DataSet dsResult = objParameters.SessionList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                    {


                        obj.lstsessiondetails = datalist;


                    }
                    List<SessionDetails> objstudent = new List<SessionDetails>();

                    {

                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            objstudent.Add(new SessionDetails

                            {

                                Pk_SessionId = row1["Pk_SessionId"].ToString(),
                                SessionName = row1["SessionName"].ToString(),


                            });
                        }
                        datalist.Add(new Session
                        {
                            Title = "Session",
                            SessionDetails = objstudent

                        });

                    }

                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetMarks(GetMarks objParameters)
        {
            GetMarks obj = new GetMarks();
            List<MarksData> datalist = new List<MarksData>();
            try
            {

                DataSet dsResult = objParameters.StudentMarksheetForParent();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                    {


                        obj.lstmarksdetails = datalist;


                    }
                    List<MarksDetails> objstudent = new List<MarksDetails>();

                    {

                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            objstudent.Add(new MarksDetails

                            {

                                MaximumMarks = row1["MaximumMarks"].ToString(),
                                ObtainMarks = row1["ObtainMarks"].ToString(),
                                ClassName = row1["ClassName"].ToString(),
                                SectionName = row1["SectionName"].ToString(),
                                StudentName = row1["StudentName"].ToString(),
                                ExamTypeName = row1["ExamTypeName"].ToString(),
                                Fk_ClassId = row1["Fk_ClassId"].ToString(),
                                Fk_SectionId = row1["Fk_SectionId"].ToString(),
                                Fk_StudentId = row1["Fk_StudentId"].ToString(),
                                Fk_ExamTypeId = row1["Fk_ExamTypeId"].ToString(),

                            });
                        }
                        datalist.Add(new MarksData
                        {
                            Title = "Marks",
                            MarksDetails = objstudent

                        });

                    }

                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintMarksheet(PrintMarkSheet objParameters)
        {
            PrintMarkSheet obj = new PrintMarkSheet();
            List<SubjectMarksData> datalist = new List<SubjectMarksData>();
            try
            {

                DataSet dsResult = objParameters.PrintMarksheetForParent();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    obj.ClassName = dsResult.Tables[0].Rows[0]["ClassName"].ToString();
                    obj.SectionName = dsResult.Tables[0].Rows[0]["SectionName"].ToString();
                    obj.StudentName = dsResult.Tables[0].Rows[0]["StudentName"].ToString();
                    obj.ParentName = dsResult.Tables[0].Rows[0]["ParentName"].ToString();
                    obj.ExamTypeName = dsResult.Tables[0].Rows[0]["ExamTypeName"].ToString();
                    obj.Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                    obj.Mobile = dsResult.Tables[0].Rows[0]["Mobile"].ToString();
                    obj.Address = dsResult.Tables[0].Rows[0]["Address"].ToString();
                    obj.Pincode = dsResult.Tables[0].Rows[0]["Pincode"].ToString();
                    obj.State = dsResult.Tables[0].Rows[0]["State"].ToString();
                    obj.City = dsResult.Tables[0].Rows[0]["City"].ToString();
                    obj.SessionName = dsResult.Tables[0].Rows[0]["SessionName"].ToString();
                    obj.RegistrationNo = dsResult.Tables[0].Rows[0]["RegistrationNo"].ToString();
                    foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                    {


                        obj.lstsubjectmarksdetails = datalist;


                    }
                    List<SubjectMarksDetails> objstudent = new List<SubjectMarksDetails>();

                    {

                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            objstudent.Add(new SubjectMarksDetails
                            {
                                MaximumMarks = row1["MaximumMarks"].ToString(),
                                ObtainMarks = row1["ObtainMarks"].ToString(),
                                SubjectName = row1["SubjectName"].ToString(),
                                SubjectCode = row1["SubjectCode"].ToString(),

                            });
                        }
                        datalist.Add(new SubjectMarksData
                        {
                            Title = "Marks",
                            listSubjectMarksDetails = objstudent

                        });

                    }

                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }

        #region HomeworkByTeacher
        public ActionResult SaveHomework(SaveHomeworkAPI obj, HttpPostedFileBase StudentFiles)
        {
            if (obj.Fk_ClassID == "0")
            {
                obj.Status = "1";
                obj.Message = "Please Select Class!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (obj.Fk_SectionID == "0")
            {
                obj.Status = "1";
                obj.Message = "Please Select Section!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (obj.SubjectID == "0")
            {
                obj.Status = "1";
                obj.Message = "Please Select Subject!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (obj.HomeworkDate == "" || obj.HomeworkDate == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Date!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (StudentFiles != null)
                {
                    obj.StudentFiles = "/Homework/" + Guid.NewGuid() + Path.GetExtension(StudentFiles.FileName);
                    StudentFiles.SaveAs(Path.Combine(Server.MapPath(obj.StudentFiles)));
                }
                //obj.AddedBy = Session["PK_TeacherID"].ToString();
                obj.HomeworkDate = string.IsNullOrEmpty(obj.HomeworkDate) ? null : Common.ConvertToSystemDate(obj.HomeworkDate, "dd/MM/yyyy");
                obj.HomeworkBy = "Teacher";
                DataSet ds = obj.SaveHomework();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                    {
                        SaveHomeworkAPI obj1 = new SaveHomeworkAPI();
                        obj1.Status = "0";
                        obj1.Message = "Homework Assigned successfully";
                        return Json(obj1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        SaveHomeworkAPI obj1 = new SaveHomeworkAPI();
                        obj1.Status = "1";
                        obj1.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj1, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveHomeworkAPI obj1 = new SaveHomeworkAPI();
                obj1.Status = "1";
                //obj1.Message = "Homework Not Assigned Successfully";
                obj1.Message = ex.Message;
                return Json(obj1, JsonRequestBehavior.AllowGet);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetHomeworkList(HomeworkListAPI model)
        {
            List<HomeworkListAPI> list = new List<HomeworkListAPI>();
            try
            {
                model.HomeworkBy = "Teacher";
                DataSet ds = model.HomeworkList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        HomeworkListAPI obj = new HomeworkListAPI();
                        obj.HomeWorkID = r["Pk_HomeworkID"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
                        obj.StudentPhoto = r["HomeworkFile"].ToString();
                        obj.HomeworkDate = r["HomeworkDate"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.Fk_ClassID = r["Pk_ClassID"].ToString();
                        obj.SubjectName = r["SubjectName"].ToString();
                        obj.SubjectID = r["Pk_SubjectID"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.Fk_SectionID = r["Pk_SectionID"].ToString();
                        obj.HomeworkBy = r["HomeworkBy"].ToString();

                        list.Add(obj);

                    }
                    model.listStudent = list;

                    model.Status = "0";
                    model.Message = "List Fetched.";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Status = "1";
                    model.Message = "List Not Fetched !!";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                model.Status = "1";
                model.Message = "List Not Fetched !!";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region TeacherAttendanceReport
        public ActionResult AttendanceReportBy(AttendenceReportAPI model)
        {
            List<AttendenceReportAPI> lst = new List<AttendenceReportAPI>();

            //model.EmployeeCode = Session["LoginID"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds0 = model.AttendanceReport();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    AttendenceReportAPI obj = new AttendenceReportAPI();

                    obj.AttendanceDate = r["AttendanceDate"].ToString();
                    obj.InTime = r["InTime"].ToString();
                    obj.OutTime = r["OutTime"].ToString();

                    lst.Add(obj);
                }
                model.lstList = lst;

                model.Status = "0";
                model.Message = "Attendence List Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Attendence List Not Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public ActionResult EmployeeSalarySlipBy(TeacherSalarySlipAPI model)
        {

            List<TeacherSalarySlipAPI> lst = new List<TeacherSalarySlipAPI>();
            //model.EmployeeID = Session["PK_TeacherID"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds0 = model.EmployeeSalarySlipBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    TeacherSalarySlipAPI obj = new TeacherSalarySlipAPI();
                    obj.Pk_PaidSalId = r["Pk_PaidSalId"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.Basic = r["BasicSalary"].ToString();
                    obj.HRA = r["HouseRent"].ToString();
                    obj.MA = r["Medical"].ToString();
                    obj.PA = r["ProfDevAllowance"].ToString();
                    obj.CA = r["Conveyance"].ToString();
                    obj.PF = r["ProfDevAllowance"].ToString();
                    obj.ExtraWork = r["ExtraWork"].ToString();
                    obj.Incentive = r["Incentice"].ToString();
                    obj.OtherPay = r["OtherPay"].ToString();
                    obj.TotalIncome = r["TotalIncome"].ToString();
                    obj.ContributionTosociety = r["ContributionTosociety"].ToString();
                    obj.Advance = r["Advanced"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Insurance = r["Insurance"].ToString();
                    obj.Other = r["Other"].ToString();
                    obj.TotalDeduction = r["TotalDeduction"].ToString();
                    obj.NetSalary = r["NetSalary"].ToString();
                    obj.MonthName = r["MonthName"].ToString();
                    obj.Year = r["Year"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;

                model.Status = "0";
                model.Message = "Salary Slip List Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Salary Slip List Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintSalarySlip(string Pk_PaidSalId, string EmployeeID)
        {
            SalarySlipPrintAPI model = new SalarySlipPrintAPI();

            List<SalarySlipPrintAPI> lst = new List<SalarySlipPrintAPI>();
            List<SalarySlipPrintAPI> lst1 = new List<SalarySlipPrintAPI>();
            model.Pk_PaidSalId = Pk_PaidSalId;
            model.EmployeeID = EmployeeID;
            DataSet ds0 = model.EmployeeSalarySlipBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    model.EmployeeID = ds0.Tables[0].Rows[0]["FK_EmpID"].ToString();
                    model.EmployeeCode = ds0.Tables[0].Rows[0]["EmployeeCode"].ToString();
                    model.EmployeeName = ds0.Tables[0].Rows[0]["EmployeeName"].ToString();
                    model.TotalIncome = ds0.Tables[0].Rows[0]["TotalIncome"].ToString();
                    model.TotalDeduction = ds0.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    model.NetSalary = ds0.Tables[0].Rows[0]["NetSalary"].ToString();
                    model.MonthName = ds0.Tables[0].Rows[0]["MonthName"].ToString();
                    model.Year = ds0.Tables[0].Rows[0]["Year"].ToString();

                    model.Basic = ds0.Tables[0].Rows[0]["BasicSalary"].ToString();
                    model.HRA = ds0.Tables[0].Rows[0]["HouseRent"].ToString();
                    model.MA = ds0.Tables[0].Rows[0]["Medical"].ToString();
                    model.PA = ds0.Tables[0].Rows[0]["ProfDevAllowance"].ToString();
                    model.CA = ds0.Tables[0].Rows[0]["Conveyance"].ToString();
                    model.PF = ds0.Tables[0].Rows[0]["ProfDevAllowance"].ToString();
                    model.ExtraWork = ds0.Tables[0].Rows[0]["ExtraWork"].ToString();
                    model.Incentive = ds0.Tables[0].Rows[0]["Incentice"].ToString();
                    model.OtherPay = ds0.Tables[0].Rows[0]["OtherPay"].ToString();
                    model.ContributionTosociety = ds0.Tables[0].Rows[0]["ContributionTosociety"].ToString();
                    model.Advance = ds0.Tables[0].Rows[0]["Advanced"].ToString();
                    model.TDS = ds0.Tables[0].Rows[0]["TDS"].ToString();
                    model.Insurance = ds0.Tables[0].Rows[0]["Insurance"].ToString();
                    model.Other = ds0.Tables[0].Rows[0]["Other"].ToString();

                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = SoftwareDetails.Pin1;
                    ViewBag.State1 = SoftwareDetails.State1;
                    ViewBag.City1 = SoftwareDetails.City1;
                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
                    ViewBag.LandLine = SoftwareDetails.LandLine;
                    ViewBag.Website = SoftwareDetails.Website;
                    ViewBag.EmailID = SoftwareDetails.EmailID;
                }
                model.Status = "0";
                model.Message = "Salary Slip Print.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Salary Slip Not Print.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        #region StudentAttendanceFilter
        public ActionResult StudentAttendanceFilter(StudentAttendanceFilter objParameters)
        {
            StudentAttendanceFilter obj = new StudentAttendanceFilter();
            List<StudentAttendanceData> datalist1 = new List<StudentAttendanceData>();
            try
            {

                DataSet dsResult = objParameters.GetStudentAttendanceDetail();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.ListStudent = datalist1;


                        }
                        List<StudentAttendanceDetails> objstudent = new List<StudentAttendanceDetails>();

                        {
                            #region Menu
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objstudent.Add(new StudentAttendanceDetails

                                {


                                    StudentName = row1["StudentName"].ToString(),
                                    ClassName = row1["ClassName"].ToString(),
                                    SectionName = row1["SectionName"].ToString(),
                                    AttendanceDate = row1["AttendanceDate"].ToString(),
                                    Status = row1["Status"].ToString(),


                                });
                            }
                            datalist1.Add(new StudentAttendanceData
                            {
                                Title = "Student Attendance Details",
                                lstStudents = objstudent

                            });
                            #endregion
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult PrintFeerecipt(PrintReceipt model)
        {
            List<PrintReceipt> lst = new List<PrintReceipt>();
            PrintReceipt obj = new PrintReceipt();
            try
            {
                DataSet ds = model.PrintReceipts();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    obj.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    obj.StudentId = ds.Tables[0].Rows[0]["LoginID"].ToString();
                    obj.ParentMobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    obj.StudentName = ds.Tables[0].Rows[0]["StudentName"].ToString();
                    obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    obj.TotalFee = ds.Tables[0].Rows[0]["TotalFee"].ToString();
                    obj.TotalFeeInWords = ds.Tables[0].Rows[0]["TotalFeeInWords"].ToString();

                    obj.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                    obj.AmountInWords = ds.Tables[0].Rows[0]["AmountInWords"].ToString();
                    obj.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    obj.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    obj.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    obj.BankDetails = ds.Tables[0].Rows[0]["BankDetails"].ToString();

                    obj.LandLine = Common.SoftwareDetails.LandLine;
                    obj.ContactNo = Common.SoftwareDetails.ContactNo;
                    obj.Website = Common.SoftwareDetails.Website;
                    obj.EmailID = Common.SoftwareDetails.EmailID;
                    obj.CompanyAddress = Common.SoftwareDetails.CompanyAddress;

                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        obj.Status = "0";
                        PrintReceipt obj1 = new PrintReceipt();
                        obj1.FeeTypeName = r["FeeTypeName"].ToString();
                        obj1.PaidAmount = r["PaidAmt"].ToString();
                        obj1.InstallmentAmt = r["InstallmentAmt"].ToString();
                        obj1.PaymentMode = r["PaymentMode"].ToString();
                        obj1.InstallemntNo = r["InstallemntNo"].ToString();
                        obj1.DueDate = r["DueDate"].ToString();
                        obj1.PaymentDate = r["PaymentDate"].ToString();
                        obj1.BankDetails = r["BankDetails"].ToString();
                        lst.Add(obj1);
                    }
                    obj.lstfeedata = lst;
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }




        public ActionResult TotalLeaveList(LeaveListAPI model)
        {

            List<LeaveListAPI> listq = new List<LeaveListAPI>();
            #region ddlhelclass+
            try
            {
                LeaveListAPI obj = new LeaveListAPI();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlSection = new List<SelectListItem>();
            ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlSection = ddlSection;


            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;


            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LeaveListAPI obj = new LeaveListAPI();

                    obj.Reason = r["Reason"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.Status = r["IsApproved"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Pk_StudentID = r["FK_StudentID"].ToString();
                    obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                    obj.Description = r["Description"].ToString();
                    listq.Add(obj);
                }
                model.listStudent = listq;
                model.Status = "0";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Leave List Not Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult TotalLeaves(SearchLeaveAPI model)
        {
            List<SearchLeaveAPI> list = new List<SearchLeaveAPI>();

            #region ddlhelclass+
            try
            {
                SearchLeaveAPI obj = new SearchLeaveAPI();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[1].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlsection
            try
            {

                SearchLeaveAPI obj = new SearchLeaveAPI();
                obj.Pk_ClassId = model.PK_ClassID;
                if (obj.Pk_ClassId != null)
                {
                    int count4 = 0;
                    List<SelectListItem> ddlSection = new List<SelectListItem>();
                    DataSet ds4 = obj.GettingSectionList();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds4.Tables[0].Rows)
                        {
                            if (count4 == 0)
                            {
                                ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                            }
                            ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                            count4 = count4 + 1;
                        }
                    }

                    ViewBag.ddlSection = ddlSection;



                }
                else
                {

                    List<SelectListItem> ddlsection = new List<SelectListItem>();
                    ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                    ViewBag.ddlSection = ddlsection;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;

            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SearchLeaveAPI obj = new SearchLeaveAPI();

                    obj.Reason = r["Reason"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.Status = r["isApproved"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Pk_StudentID = r["FK_StudentID"].ToString();
                    obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                    obj.Description = r["Description"].ToString();
                    list.Add(obj);
                }
                model.listStudent = list;

                model.Status = "0";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Search Leave List Not Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ApprovePendingLeave(ApproveLeaveAPI model)
        {
            //string noofrows = Request["hdRows"].ToString();

            //string chkselect = "";

            //for (int i = 1; i < int.Parse(noofrows); i++)
            //{
            try
            {

                model.Status = "Approved";
                DataSet ds = model.UpdatingStudentLeaveAplcn();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Status = "1";
                        model.Message = "Leave Approved Successfully";
                        return Json(model, JsonRequestBehavior.AllowGet);

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        model.Status = "1";
                        model.Message = "Leave Not Approved Successfully";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                }
                //}
            }
            catch
            {
                //chkselect = "0";
                model.Status = "1";
            }

            //}
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeclinePendingLeave(DeclineLeaveAPI model)
        {
            //string noofrows = Request["hdRows"].ToString();

            //string chkselect = "";

            //for (int i = 1; i < int.Parse(noofrows); i++)
            //{
            try
            {
                model.Status = "Declined";
                DataSet ds = model.UpdatingStudentLeaveAplcn();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Status = "0";
                        model.Message = "Leave Decline Successfully";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        model.Status = "1";
                        model.Message = "Leave Not Decline Successfully";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    // }
                }
            }
            catch
            {
                //chkselect = "0";
                model.Status = "1";
            }

            //}
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingLeave(PendingLeaveAPI model)
        {
            // model.PK_TeacherID = Session["PK_TeacherID"].ToString();
            model.Status = "Pending";
            List<PendingLeaveAPI> listq = new List<PendingLeaveAPI>();
            #region ddlhelclass+
            try
            {
                PendingLeaveAPI obj = new PendingLeaveAPI();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = obj.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlSection = new List<SelectListItem>();
            ddlSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlSection = ddlSection;

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.LeaveListParent();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PendingLeaveAPI obj = new PendingLeaveAPI();

                    obj.Reason = r["Reason"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.StudentName = r["StudentName"].ToString();
                    obj.Status = r["IsApproved"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Pk_StudentID = r["FK_StudentID"].ToString();
                    obj.PK_StdntLeaveID = r["PK_StdntLeaveID"].ToString();
                    obj.Description = r["Description"].ToString();
                    listq.Add(obj);
                }
                model.listStudent = listq;

                model.Status = "0";
                model.Message = "Pending Leave List Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Pending Leave List Not Fetched!!";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetClassList(GetClassAPI model)
        {
            try
            {
                //GetClassAPI obj = new GetClassAPI();
                List<GetClassAPI> listq = new List<GetClassAPI>();
                int count1 = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet ds2 = model.GetClassList();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[1].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;

                foreach (DataRow r in ds2.Tables[1].Rows)
                {
                    GetClassAPI obj = new GetClassAPI();

                    obj.ClassName = r["ClassName"].ToString();
                    obj.Fk_ClassID = r["PK_ClassID"].ToString();
                    listq.Add(obj);
                }
                model.listClass = listq;

                model.Status = "0";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSectionList(GetSectionAPI model)
        {
            try
            {
                List<GetSectionAPI> listq = new List<GetSectionAPI>();
                List<SelectListItem> ddlsection = new List<SelectListItem>();

                DataSet ds = model.GetSectionByClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {

                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }
                ViewBag.ddlsection = ddlsection;
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    GetSectionAPI obj = new GetSectionAPI();

                    obj.ClassName = r["ClassName"].ToString();
                    obj.Fk_ClassID = r["Fk_ClassID"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.PK_SectionId = r["PK_SectionID"].ToString();
                    listq.Add(obj);
                }
                model.listSection = listq;

                model.Status = "0";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSubjectNameBySection(GetSubjectAPI model)
        {
            try
            {
                List<GetSubjectAPI> listq = new List<GetSubjectAPI>();
                List<SelectListItem> ddlSection = new List<SelectListItem>();

                DataSet ds = model.GetSubjectNameBySection();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SubjectName"].ToString(), Value = r["Fk_SubjectID"].ToString() });

                    }
                }
                ViewBag.ddlSubjectName = ddlSection;

                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    GetSubjectAPI obj = new GetSubjectAPI();

                    obj.SubjectName = r["SubjectName"].ToString();
                    obj.Fk_SubjectID = r["Fk_SubjectID"].ToString();
                    listq.Add(obj);
                }
                model.listSection = listq;

                model.Status = "0";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveAttendance(SaveEmployeeAttendanceRequest Request, HttpPostedFileBase TeacherPhoto)
        {
            SaveEmployeeAttendanceResponse Response = new SaveEmployeeAttendanceResponse();
            Request.AttendanceDate = string.IsNullOrEmpty(Request.AttendanceDate) ? null : Common.ConvertToSystemDate(Request.AttendanceDate, "dd/MM/yyyy");
            try
            {
                if (TeacherPhoto != null)
                {
                    Request.TeacherPhoto = "/TeacherPunching/" + Guid.NewGuid() + Path.GetExtension(TeacherPhoto.FileName);
                    TeacherPhoto.SaveAs(Path.Combine(Server.MapPath(Request.TeacherPhoto)));
                }
                DataSet ds = Request.SaveEmployeeAttendance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Response.status = "0";
                        Response.Message = "   Punching Successfully !";
                    }
                    else
                    {
                        Response.status = "1";
                        Response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.status = "1";
                Response.Message = ex.Message;
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePunchOutAttendance(SaveEmployeeAttendancePunchoutRequest Request)
        {
            SaveEmployeeAttendancePunchoutResponse Response = new SaveEmployeeAttendancePunchoutResponse();
            Request.AttendanceDate = string.IsNullOrEmpty(Request.AttendanceDate) ? null : Common.ConvertToSystemDate(Request.AttendanceDate, "dd/MM/yyyy");
            try
            {
                DataSet ds = Request.SaveEmployeePunchoutAttendance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Response.status = "0";
                        Response.Message = "   PunchOut Successfully !";
                    }
                    else
                    {
                        Response.status = "1";
                        Response.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.status = "1";
                Response.Message = ex.Message;
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBranch(GetBranchAPI model)
        {
            List<GetBranchAPI> listq = new List<GetBranchAPI>();
            try
            {
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet ds1 = model.BranchList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                        }
                        ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlBranch = ddlBranch;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    GetBranchAPI obj = new GetBranchAPI();
                    obj.Pk_BranchID = r["Pk_BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    listq.Add(obj);
                }
                model.listBranch = listq;
                model.Status = "0";
                model.Message = "Branch Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetReligion(GetReligionAPI model)
        {
            List<GetReligionAPI> listq = new List<GetReligionAPI>();
            try
            {
                int count = 0;
                List<SelectListItem> ddlReligion = new List<SelectListItem>();
                DataSet ds1 = model.GetReligion();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlReligion.Add(new SelectListItem { Text = "Select Religion", Value = "0" });
                        }
                        ddlReligion.Add(new SelectListItem { Text = r["ReligionName"].ToString(), Value = r["Pk_ReligionId"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlReligion = ddlReligion;

                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    GetReligionAPI obj = new GetReligionAPI();
                    obj.Pk_ReligionId = r["PK_ReligionID"].ToString();
                    obj.ReligionName = r["ReligionName"].ToString();
                    listq.Add(obj);
                }
                model.listReligion = listq;

                model.Status = "0";
                model.Message = "Religion Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetCategory(GetCategoryAPI model)
        {
            List<GetCategoryAPI> listq = new List<GetCategoryAPI>();
            try
            {
                int countcat = 0;
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                DataSet ds1 = model.GetCategory();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (countcat == 0)
                        {
                            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                        }
                        ddlCategory.Add(new SelectListItem { Text = r["Category"].ToString(), Value = r["Category"].ToString() });
                        countcat = countcat + 1;
                    }
                }
                ViewBag.ddlCategory = ddlCategory;

                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    GetCategoryAPI obj = new GetCategoryAPI();
                    obj.PK_CategoryID = r["PK_CategoryID"].ToString();
                    obj.Category = r["Category"].ToString();
                    listq.Add(obj);
                }
                model.listCategory = listq;

                model.Status = "0";
                model.Message = "Category Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetGender(GetGenderAPI model)
        {
            List<GetGenderAPI> listq = new List<GetGenderAPI>();
            try
            {
                int countgen = 0;
                List<SelectListItem> ddlGender = new List<SelectListItem>();
                DataSet ds1 = model.GetGender();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (countgen == 0)
                        {
                            ddlGender.Add(new SelectListItem { Text = "Select Gender", Value = "0" });
                        }
                        ddlGender.Add(new SelectListItem { Text = r["Gender"].ToString(), Value = r["Gender"].ToString() });
                        countgen = countgen + 1;
                    }
                }
                ViewBag.ddlGender = ddlGender;

                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    GetGenderAPI obj = new GetGenderAPI();
                    obj.PK_GenderId = r["PK_GenderId"].ToString();
                    obj.Gender = r["Gender"].ToString();
                    listq.Add(obj);
                }
                model.listGender = listq;

                model.Status = "0";
                model.Message = "Gender Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetTeacherProfile(GetTeacherProfileAPI model)
        {
            DataSet ds = model.GetTeacherList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.PK_TeacherID = ds.Tables[0].Rows[0]["PK_TeacherID"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                model.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                model.Religion = ds.Tables[0].Rows[0]["FK_ReligionID"].ToString();
                model.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                model.Qualification = ds.Tables[0].Rows[0]["Qualification"].ToString();
                model.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                model.Image = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.LastExperience = ds.Tables[0].Rows[0]["LastExperience"].ToString();
                model.LastSchool = ds.Tables[0].Rows[0]["LastSchool"].ToString();
                model.PinCode = ds.Tables[0].Rows[0]["pincode"].ToString();
                model.City = ds.Tables[0].Rows[0]["City"].ToString();
                model.State = ds.Tables[0].Rows[0]["State"].ToString();
                model.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                model.DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                model.BranchName = ds.Tables[0].Rows[0]["FK_BranchID"].ToString();

                model.Status = "0";
                model.Message = "Teacher Details Fetched.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UpdateTeacherProfile(TeacherProfileUpdate model, HttpPostedFileBase UploadFile)
        {
            try
            {
                if (UploadFile != null)
                {
                    model.UploadFile = "../Teacher/" + Guid.NewGuid() + Path.GetExtension(UploadFile.FileName);
                    UploadFile.SaveAs(Path.Combine(Server.MapPath(model.UploadFile)));
                }
                model.PK_TeacherID = model.PK_TeacherID == "0" ? null : model.PK_TeacherID;
                model.Name = model.Name == "" ? null : model.Name;
                model.FatherName = model.FatherName == "" ? null : model.FatherName;
                model.Address = model.Address == "" ? null : model.Address;
                model.PinCode = model.PinCode == "" ? null : model.PinCode;
                model.EmailID = model.EmailID == "" ? null : model.EmailID;
                model.DOB = model.DOB == "" ? null : model.DOB;
                model.LastSchool = model.LastSchool == "" ? null : model.LastSchool;
                model.LastExperience = model.LastExperience == "" ? null : model.LastExperience;
                model.Gender = model.Gender == "" ? null : model.Gender;
                model.Religion = model.Religion == "" ? null : model.Religion;
                model.Category = model.Category == "" ? null : model.Category;
                model.DOJ = model.DOJ == "" ? null : model.DOJ;
                model.Qualification = model.Qualification == "" ? null : model.Qualification;
                model.Experience = model.Experience == "" ? null : model.Experience;
                model.BranchName = model.BranchName == "" ? null : model.BranchName;
                model.MobileNo = model.MobileNo == "" ? null : model.MobileNo;
                model.UploadFile = model.UploadFile == "" ? null : model.UploadFile;
                model.UpdatedBy = model.UpdatedBy == "0" ? null : model.UpdatedBy;
                DataSet ds = model.UpdateTeacherRecord();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                    {
                        model.Status = "0";
                        model.Message = "Teacher Details Updated Successfully.";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Status = "1";
                        model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
              
        ////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult GetAttenndaceList(GetAttenndaceListReqst model)
        {
            List<GetAttenndaceListRespons> lst = new List<GetAttenndaceListRespons>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds0 = model.GetAttenndaceList();
            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    GetAttenndaceListRespons obj = new GetAttenndaceListRespons();
                    obj.LoginID = r["LoginID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.FatherName = r["FatherName"].ToString();
                    obj.DOB = r["DOB"].ToString();
                    obj.Gender = r["Gender"].ToString();
                    obj.MobileNo = r["MobileNo"].ToString();
                    obj.EmailID = r["EmailID"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.AttendanceDate = r["AttendanceDate"].ToString();
                    obj.InTime = r["InTime"].ToString();
                    obj.OutTime = r["OutTime"].ToString();
                    obj.UploadFile = r["UploadFile"].ToString();
                    obj.Latitude = r["Latitude"].ToString();
                    obj.Longitude = r["Longitude"].ToString();
                    obj.PunchIn = r["PunchIn"].ToString();
                    obj.PunchOut = r["PunchOut"].ToString();
                    obj.OutLatitude = r["OutLatitude"].ToString();
                    obj.OutLongitude = r["OutLongitude"].ToString();
                    lst.Add(obj);
                }
                model.listAttenndace = lst;

                model.Status = "0";
                model.Message = "Record Found.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.Status = "1";
                model.Message = "Record Not Found.";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
    }
}