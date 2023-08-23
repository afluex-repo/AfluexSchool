using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfluexSchool.Models
{
    public class DashBoard:Common
    {
        public List<DashBoard> lststudent { get; set; }
        public List<DashBoard> lstteacher { get; set; }
        public string ImagePath { get; set; }
        public string Fk_ParentId { get; set; }
        public string Name { get; set; }
        public string Qualification { get;  set; }
        public string Status { get; set; }
        public string Total { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string AadhaarCard { get; set; }
        public string PermanentAddress { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string FatherOcc { get;  set; }
        public string MotherOcc { get;  set; }
        public string RegistrationNo { get; set; }
         public string Notice { get; set; }
         public string Session { get; set; }
        public DataSet GetDashBoardDetails()
        {
            SqlParameter[] Param ={
                                    new SqlParameter("@Fk_SessionId",Session),

                                    };
            DataSet ds = Connection.ExecuteQuery("GetDashBoradDetailsForAdmin",Param);
            return ds;
        }
        public DataSet GetDashBoardDetailsForParent()
        {
            SqlParameter[] Param ={
                                    new SqlParameter("@Fk_ParentId",Fk_ParentId),
                                     
                                    };
            DataSet ds = Connection.ExecuteQuery("GetDashBoardDetailsForParent", Param);
            return ds;
        }
    }
}