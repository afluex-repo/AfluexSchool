using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfluexSchool.Models
{
    public class Parent : Common
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ParentLogin_ID { get; set; }
        public string ParentName { get; set; }

        public string Pk_ParentID { get; set; }

        public List<Parent> listparent { get; set; }
        public string LoginID { get; set; }
        public string Name { get; set; }
        public string PAN { get; set; }
        public string AadharNo { get; set; }
        public string Amount { get; set; }
        public string IsActive { get; set; }
        public string MobileNo { get; set; }
        public string StudentName { get; set; }
        public string Fk_ClassID { get; set; }
        public string FormNo { get; set; }
        public string ExistingParentName { get; set; }
        public string ExistingMobile { get; set; }
        public string ExistingAddress { get; set; }
        public string ExistingStudentName { get; set; }
        public string ExistingFormNo { get; set; }
        public string ExistingFk_ClassID { get; set; }
        public string ExistingAmount { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string correspondenceAddress { get; set; }
        public string correspondencPinCode { get; set; }
        public string correspondencState { get; set; }
        public string correspondencCity { get; set; }

        public string Pk_MessageId { get; set; }
        public string Fk_UserId { get; set; }
        public string MemberName { get; set; }
        public string MessageTitle { get; set; }
        public string Message { get; set; }
        public string cssclass { get; set; }
        public string MessageBy { get; set; }
        public string Fk_SessionId { get; set; }
        public DataSet SaveParentRegistration()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@Email",Email),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Address",Address),
                                    new SqlParameter("@PinCode",PinCode),
                                    new SqlParameter("@State",State),
                                    new SqlParameter("@City",City),
                                    new SqlParameter("@AddedBy",AddedBy),
                                     new SqlParameter("@PAN",PAN),
                                    new SqlParameter("@AadharNo",AadharNo),

                                };
            DataSet ds = Connection.ExecuteQuery("ParentRegistration", para);
            return ds;

        }

        public DataSet ParentList()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_ParentID",Pk_ParentID),
                                   new SqlParameter("@LoginID",LoginID),
                                   new SqlParameter("@Name",Name),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@IsActive",IsActive),

                               };
            DataSet ds = Connection.ExecuteQuery("GetParentList", para);
            return ds;
        }
        public DataSet ParentEnquiryList()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Pk_ParentEnquiryID",Pk_ParentID),
                                   new SqlParameter("@LoginID",LoginID),
                                    new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@Name",Name),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                  new SqlParameter("@IsActive",IsActive),

                               };
            DataSet ds = Connection.ExecuteQuery("ParentEnquiryList", para);
            return ds;
        }
        public DataSet UpdateParentList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_ParentID",Pk_ParentID),
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@Email",Email),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Address",Address),
                                    new SqlParameter("@PinCode",PinCode),
                                    new SqlParameter("@State",State),
                                    new SqlParameter("@City",City),
                                    new SqlParameter("@UpdatedBy",UpdatedBy),
                                       new SqlParameter("@PAN",PAN),
                                    new SqlParameter("@AadharNo",AadharNo),
                                     new SqlParameter("@CorrespondenceAddress",correspondenceAddress),
                                   new SqlParameter("@CorrespondencPinCode",correspondencPinCode),
                                   new SqlParameter("@CorrespondencState",correspondencState),
                                   new SqlParameter("@CorrespondencCity",correspondencCity),
                                    new SqlParameter("@Fk_FatherOccupationID",FatherOccupation),
                                   new SqlParameter("@MotherName",MotherName),
                                   new SqlParameter("@Fk_MotherOccupationID",MotherOccupation),
                                };
            DataSet ds = Connection.ExecuteQuery("UpdateParent", para);
            return ds;

        }

        public DataSet DeleteParentList()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_ParentID",Pk_ParentID),
                                    new SqlParameter("@DeletedBy",DeletedBy)

                               };
            DataSet ds = Connection.ExecuteQuery("DeleteParent", para);
            return ds;
        }

        public DataSet GetStateCityByPincode()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@pincode",PinCode)
                               };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;

        }

        public DataSet SaveParentEnquiry()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Address",Address),
                                    new SqlParameter("@StudentName",StudentName),
                                    new SqlParameter("@Fk_ClassId",Fk_ClassID),
                                    new SqlParameter("@FormNo",FormNo),
                                    new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Fk_SessionId",Fk_SessionId),
                                };
            DataSet ds = Connection.ExecuteQuery("ParentEnquiry", para);
            return ds;

        }
        public DataSet SaveExistingParentEnquiry()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@ParentName",ExistingParentName),
                                    new SqlParameter("@Mobile",ExistingMobile),
                                    new SqlParameter("@Address",ExistingAddress),
                                    new SqlParameter("@StudentName",ExistingStudentName),
                                    new SqlParameter("@Fk_ClassId",ExistingFk_ClassID),

                                    new SqlParameter("@FormNo",ExistingFormNo),
                                    new SqlParameter("@Amount",ExistingAmount),
                                      new SqlParameter("@AddedBy",AddedBy),
                                       new SqlParameter("@Pk_ParentID",Pk_ParentID),
                                         new SqlParameter("@Fk_SessionId",Fk_SessionId),
                                };
            DataSet ds = Connection.ExecuteQuery("ParentEnquiryForExisting", para);
            return ds;

        }

        public DataSet GetEnquiryAmount()
        {

            DataSet ds = Connection.ExecuteQuery("GetEnquiryAmount");
            return ds;

        }
        public DataSet GetAllMessages()
        {
            SqlParameter[] para = { new SqlParameter("@ParentID", Pk_ParentID) };
            DataSet ds = Connection.ExecuteQuery("GetMessages", para);
            return ds;
        }
        public DataSet SaveMessage()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Message", Message),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@MessageBy", MessageBy),
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                          new SqlParameter("@Fk_MessageId", Pk_MessageId),
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertMessage", para);
            return ds;
        }
        public DataSet DeleteEnquiry()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_ParentID", Pk_ParentID),
                                      new SqlParameter("@DeletedBy", DeletedBy),
                                   
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteParentEnquiry", para);
            return ds;
        }

        public DataSet ParentProfileDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_ParentID", Pk_ParentID)
                                  };
            DataSet ds = Connection.ExecuteQuery("Parentprofiledetails", para);
            return ds;
        }

        public DataSet UpdateParentProfile()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Pk_ParentID",Pk_ParentID),
                                    new SqlParameter("@ParentName",ParentName),
                                    new SqlParameter("@Email",Email),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Address",Address),
                                    new SqlParameter("@PinCode",PinCode),
                                    new SqlParameter("@State",State),
                                    new SqlParameter("@City",City),
                                    new SqlParameter("@UpdatedBy",UpdatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("UpdateParentProfile", para);
            return ds;
        }


    }
}