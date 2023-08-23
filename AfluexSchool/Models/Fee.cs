using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfluexSchool.Models
{
    public class Fee : Common
    {
        public DataTable dtTable { get; set; }
        public List<Fee> lstconsession { get; set; }

        public string FeeTypeName { get; set; }
        public string PK_FeeTypeID { get; set; }
        public List<Fee> lstFeeTypeList { get; set; }
        public List<Fee> lstfeesdetails { get; set; }
        public List<Fee> lstpaidfee { get; set; }
        public string Pk_FeeStructureId { get; set; }
        public string Pk_SessionId { get; set; }
        public string ClassName { get; set; }
        public string Amount { get; set; }
        public string SessionName { get; set; }
        public string LoginId { get; set; }
        public string Month { get; set; }
        public string ParentName { get; set; }
        public string ParentMobile { get; set; }
        public string ParenteEmailId { get; set; }
        public string SectionName { get; set; }
        public string RegistrationNo { get; set; }
        public string BranchName { get; set; }
        public string ParentLoginId { get; set; }
        public string Medium { get; set; }
        public string PaymentMode { get; set; }


        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string PaymentDate { get; set; }
        public string DueDate { get; set; }
        public string InstallemntNo { get; set; }
        public string InstallmentAmt { get; set; }
        public string AmountWithoutFine { get; set; }
        public string Pk_FeesDeatilsId { get; set; }
        public string IsTickTrue { get; set; }

        public string LateFee { get; set; }
        public string PaidAmount { get; set; }
        public string PK_ClassID { get; set; }
        public string discountflag { get; set; }
        public string IsDaily { get; set; }
        public string IsPaid { get; set; }
        public string Feb { get; set; }
        public string Apr { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string Aug { get; set; }
        public string Sep { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Dec { get; set; }
        public string Mar { get; set; }
        public string Jan { get; set; }
        public string TotalAmount { get; set; }
        public string ConsessionFee { get; set; }
        public string Discount { get; set; }
        public string Password { get; set; }
        public string PreviousBalance { get; set; }
        public List<Fee> lstmonth { get; set; }
        public string detail { get; set; }
        public string LOGID { get; set; }
        public string Fk_SessionId { get; set; }
        public DataSet InsertingFeeType()
        {

            SqlParameter[] Param ={ new SqlParameter("@FeeTypeName",FeeTypeName),
                                      new SqlParameter("@AddedBy",AddedBy)
                                    };
            DataSet ds = Connection.ExecuteQuery("AddFeeType", Param);
            return ds;
        }

        public DataSet ViewFeeType()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FeeTypeID", PK_FeeTypeID) };
            DataSet ds = Connection.ExecuteQuery("FeeTypeList", para);
            return ds;

        }



        public DataSet DeleteFeeType()
        {
            SqlParameter[] Param ={ new SqlParameter("@PK_FeeTypeID",PK_FeeTypeID),
                                    new SqlParameter("@DeletedBy",DeletedBy)
                             };
            DataSet ds = Connection.ExecuteQuery("DeleteFeeType", Param);
            return ds;
        }

        public DataSet UpdateFee()
        {
            SqlParameter[] param = { new SqlParameter("@PK_FeeTypeID", PK_FeeTypeID),
                                       new SqlParameter("@FeeTypeName", FeeTypeName),
                                        new SqlParameter("@UpdatedBy", UpdatedBy)

        };
            DataSet ds = Connection.ExecuteQuery("UpdateFeeType", param);
            return ds;
        }

        public DataSet GetClassList()
        {
            DataSet ds = Connection.ExecuteQuery("GetClassList");
            return ds;
        }

        #region fee strc
        public DataSet SaveFeeStructure()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_ClassId",ClassName),
                                      new SqlParameter("@Fk_FeeTypeId",FeeTypeName),
                                      new SqlParameter("@Jan",Jan),
                                      new SqlParameter("@Feb",Feb),
                                      new SqlParameter("@Mar",Mar),
                                      new SqlParameter("@Apr",Apr),
                                      new SqlParameter("@May",May),
                                      new SqlParameter("@June",June),
                                      new SqlParameter("@July",July),
                                      new SqlParameter("@Aug",Aug),
                                      new SqlParameter("@Sep",Sep),
                                      new SqlParameter("@Oct",Oct),
                                      new SqlParameter("@Nov",Nov),
                                      new SqlParameter("@Dec",Dec),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Fk_SessionId",Fk_SessionId)
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertFeeStructure", para);
            return ds;
        }
        public DataSet GetFeeStructureList()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_ClassId", ClassName) ,
            new SqlParameter("@Fk_SessionId", Fk_SessionId)};
            DataSet ds = Connection.ExecuteQuery("GetFeeStructure", para);
            return ds;
        }


        public DataSet DeleteFeeStructure()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_FeeStructureId",Pk_FeeStructureId),
                                      new SqlParameter("@DeletedBy",DeletedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteFeeStructure", para);
            return ds;
        }

        public DataSet UpdateFeeStructure()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_FeeStructureId",Pk_FeeStructureId),

                                      new SqlParameter("@Fk_ClassId",ClassName),
                                      new SqlParameter("@Fk_FeeTypeId",FeeTypeName),
                                      new SqlParameter("@Amount",Amount),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateFeeStructure", para);
            return ds;
        }

        #endregion

        public DataSet GetDetailsForExaminationFees()
        {
            SqlParameter[] param = {
                new SqlParameter("@LoginId", LoginId),

                                    };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForExaminationFees", param);
            return ds;
        }

        public DataSet SaveAdmissionFee()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@PaymentDate",PaymentDate),
                                      new SqlParameter("@PaymentMode",PaymentMode),
                                      new SqlParameter("@TransactionNo",TransactionNumber),
                                       new SqlParameter("@TransactionDate",TransactionDate),
                                        new SqlParameter("@BankName",BankName),
                                         new SqlParameter("@BankBranch",BankBranch),
                                          new SqlParameter("@AddedBy",AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveAdmissionFee", para);
            return ds;
        }
        public DataSet GetDetailsForFees()
        {
            SqlParameter[] param = { new SqlParameter("@LoginId", LoginId),

                                    };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForFees", param);
            return ds;
        }
        public DataSet GetDetailsForFeesMonth()
        {
            SqlParameter[] param = {
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Month", MonthId),
                                    new SqlParameter("@Login", LOGID),
                                    new SqlParameter("@Fk_SessionID", Fk_SessionId)
                                    };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForFeesMonth", param);
            return ds;
        }
        public DataSet GetFineAMount()
        {
            SqlParameter[] param = {
                                        new SqlParameter("@PK_ClassID", PK_ClassID),
                                        new SqlParameter("@DueDate", DueDate),
                                        new SqlParameter("@PaymentDate", PaymentDate),

                                    };
            DataSet ds = Connection.ExecuteQuery("GetFineAmount", param);
            return ds;
        }
        public DataSet SaveFee()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@Month",MonthId),
                                      new SqlParameter("@PaymentDate",PaymentDate),
                                      new SqlParameter("@PaymentMode",PaymentMode),
                                      new SqlParameter("@TransactionNo",TransactionNumber),
                                      new SqlParameter("@TransactionDate",TransactionDate),
                                      new SqlParameter("@BankName",BankName),
                                      new SqlParameter("@BankBranch",BankBranch),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@LateFee",LateFee),
                                      new SqlParameter("@PaidAmount",PaidAmount),
                                      new SqlParameter("@Discount",Discount),
                                        new SqlParameter("@Fk_SessionId",Fk_SessionId),
                                      new SqlParameter("@dtStudentConsession",dtTable),
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveInstallmentFee", para);
            return ds;
        }
        public DataSet CheckPassword()
        {
            SqlParameter[] param = { new SqlParameter("@Password", Password),

                                    };
            DataSet ds = Connection.ExecuteQuery("CheckTransPassword", param);
            return ds;
        }
    }
}