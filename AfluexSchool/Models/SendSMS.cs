using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace AfluexSchool.Models
{
    public class SendSMS : Common
    {
        #region Properties

        public List<SendSMS> lstsmsdata { get; set; }
        public DataTable dtSMS { get; set; }
        public string SchoolName { get; set; }
        public string SMSTemplate { get; set; }
        public string SMSTemplateText { get; set; }
        public string SMS { get; set; }
        public string Type { get; set; }
        public string Fk_ClassId { get; set; }
        public string Fk_SectionId { get; set; }
        public string Fk_SessionId { get; set; }
        public string Fk_StaffId { get; set; }
        public string MessageCount { get; set; }
        public string TotalSMS { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
        public string PK_TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Msg { get; set; }
         public string FatherName { get; set; }
        public List<SendSMS> TemplateList { get; set; }
        #endregion

        #region SendSMS

        public DataSet GetSection()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_ClassID",Fk_ClassId)
                                };
            DataSet ds = Connection.ExecuteQuery("GetSectionByClass", para);
            return ds;
        }
        public DataSet GetSMSData()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Type",Type),
                                   new SqlParameter("@Fk_StaffId",Fk_StaffId),
                                   new SqlParameter("@Fk_ClassId",Fk_ClassId),
                                   new SqlParameter("@Fk_SectionId",Fk_SectionId),
                                   new SqlParameter("@Fk_SessionId",Fk_SessionId),
                                    new SqlParameter("@FatherName",FatherName),
                               };
            DataSet ds = Connection.ExecuteQuery("GetSMSData", para);
            return ds;
        }

        public DataSet SaveSMSData()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@dtSMS",dtSMS),
                                   new SqlParameter("@Message", SMS),
                                   new SqlParameter("@SMSCount",TotalSMS),
                                   new SqlParameter("@AddedBy",AddedBy),
                                   new SqlParameter("@MessageTemplate",SMSTemplateText),
                               };
            DataSet ds = Connection.ExecuteQuery("SaveSMSReport", para);
            return ds;
        }
        public DataSet GetSMSReport()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@Status", Status),
                               };
            DataSet ds = Connection.ExecuteQuery("GetSMSReport", para);
            return ds;
        }

        #endregion

        #region SMS Template

        public DataSet SavingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@TemplateName",TemplateName),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Msg",Msg)
            };
            DataSet ds = Connection.ExecuteQuery("InsertSMSTemplate", para);
            return ds;
        }

        public DataSet GettingTemplateList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_TemplateId",PK_TemplateId)
            };
            DataSet ds = Connection.ExecuteQuery("GetSmstemplate", para);
            return ds;
        }

        public DataSet UpdatingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@TemplateName",TemplateName),
                new SqlParameter("@AddedBy",UpdatedBy),
                  new SqlParameter("@pk_templateid",PK_TemplateId),
                new SqlParameter("@Msg",Msg)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateSMSTemplate", para);
            return ds;
        }

        public DataSet DeletingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@pk_templateid",PK_TemplateId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteSMSTemplate", para);
            return ds;
        }

        #endregion
    }
}