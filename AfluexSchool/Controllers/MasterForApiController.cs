using AfluexSchool.Models;
using APSSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

                DataSet dsResult = objParameters.GetHomeworkForParent();
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

                                    Subject = row1["SubjectName"].ToString(),
                                    HomeworkText = row1["HomeworkText"].ToString(),
                                    SectionName = row1["HomeworkDate"].ToString(),
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
                                    MonthName = row1["MonthName"].ToString(),

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
            if (objParameters.Pk_StudentId == "" || objParameters.Pk_StudentId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass StudentId";
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
        #endregion Sylabus

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
            if (objParameters.Pk_StudentId == "" || objParameters.Pk_StudentId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass StudentId";
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
                                    Subject = row1["SubjectName"].ToString(),
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
            if (objParameters.PK_ClassID == "" || objParameters.PK_ClassID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Pass Class";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.PK_SectionId == "" || objParameters.PK_SectionId == null)
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
                            foreach (DataRow row1  in (dsResult.Tables[1].Rows))
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





    }
}