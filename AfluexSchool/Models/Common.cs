using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Common
    {
    

        public string AddedBy { get; set; }
    
        public string Pk_BranchID { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string EncrptNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
         public string DeletedBy { get; set; }
        public string PinCode { get;  set; }
        public string State { get; set; }
        public string City { get; set; }
        public string MonthName { get; set; }
        public string MonthId { get; set; }
        public string IsPaid { get; set; }
         public string IDLoginId { get; set; }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }

        }

        public DataSet GetSMSTemplate()
        {

            DataSet ds = Connection.ExecuteQuery("GetSmstemplate");
            return ds;
        }
        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }
        public DataSet GetClass()
        {
            DataSet ds = Connection.ExecuteQuery("GetClassList");
            return ds;
        }
        public DataSet GetStaffList()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetStaffList");
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
        
        public static List<SelectListItem> BindType()
        {
            List<SelectListItem> BindType = new List<SelectListItem>();
            BindType.Add(new SelectListItem { Text = "Select Type", Value = "0" });
            BindType.Add(new SelectListItem { Text = "Teacher", Value = "Staff" });
            BindType.Add(new SelectListItem { Text = "Student", Value = "Student" });
            return BindType;
        }
        public static List<SelectListItem> BindTypeForSMS()
        {
            List<SelectListItem> BindTypeSMS = new List<SelectListItem>();
            BindTypeSMS.Add(new SelectListItem { Text = "Select Type", Value = "0" });
            BindTypeSMS.Add(new SelectListItem { Text = "Teacher", Value = "Staff" });
            BindTypeSMS.Add(new SelectListItem { Text = "Student", Value = "Student" });
            BindTypeSMS.Add(new SelectListItem { Text = "Transport Student", Value = "Transport" });
            return BindTypeSMS;
        }
        public DataSet UserTypeList()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetUsersType");
            return ds;
        }

        public DataSet GetPaymentMode()
        {

            DataSet ds = Connection.ExecuteQuery("GetPaymentMode");
            return ds;
        }
        public DataSet GetMonth()
        {


            SqlParameter[] para ={
                                   new SqlParameter("@LoginId",IDLoginId)
                               };
            DataSet ds = Connection.ExecuteQuery("GetMonth",para);
            return ds;
        }
        public static List<SelectListItem> TransferType()
        {
            List<SelectListItem> StatusType = new List<SelectListItem>();

            StatusType.Add(new SelectListItem { Text = "Migrate", Value = "Migrate" });
            StatusType.Add(new SelectListItem { Text = "Close", Value = "Close" });
            StatusType.Add(new SelectListItem { Text = "Fail", Value = "Fail" });
            return StatusType;
        }
        public class SoftwareDetails
        {
            public static string CompanyName = "Afluex Educational Demo";
            public static string CompanyAddress = "D-54 Vibhuti Khand Lucknow LUCKNOW";
            public static string Pin1 = "271001";
            public static string State1 = "U.P";
            public static string City1 = "LUCKNOW";
            public static string ContactNo = "(+91) 7310000413";
            public static string LandLine = "";
            public static string Website = "http://apsglobeschool.org/";
            public static string EmailID = "info@afluex.com";
            public static string AffliateNo = "17102-06/2018";
        }
        public class SMSCredential
        {
            public static string UserName = "apsglobe";
            public static string Password = "1234567";
            public static string SenderId = "APSGLB";
        }

        public static List<SelectListItem> Status()
        {
            List<SelectListItem> ddlStatus = new List<SelectListItem>();
            ddlStatus.Add(new SelectListItem { Text = "Select Status", Value = "0" });
            ddlStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            ddlStatus.Add(new SelectListItem { Text = "Declined", Value = "Declined" });
            return ddlStatus;
        }
        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = Connection.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}