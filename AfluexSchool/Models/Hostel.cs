using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AfluexSchool.Models
{
    public class Hostel : Common
    {
        #region Props
        public string Pk_HostelTypeId { get; set; }
        public string HostelName { get; set; }
        public string Pk_HostelId { get; set; }
        public string TypeName { get; set; }
        public List<Hostel> lstHostel { get; set; }
        public string Pk_HostelFloorId { get;   set; }
        public string FloorName { get;   set; }
        public List<SelectListItem> lstHostelName { get; set; }
        public List<SelectListItem> lstFloor { get; set; }
         public List<SelectListItem> ddlHostelRoom { get; set; }
        public string Pk_HostelRoomId { get;   set; }
        public string FromRoom { get;  set; }
        public string ToRoom { get;  set; }
        public string Pk_BedId { get;   set; }
        public string BedCapacity { get;   set; }
         public string Amount { get; set; }

        public List<SelectListItem> ddlsection { get; set; }
        public List<SelectListItem> ddlstudent { get; set; }
        public string Pk_StudentID { get; set; }
        public string Fk_SectionID { get; set; }
        public string Fk_ClassID { get; set; }
        public string Pk_HostelAllotmentId { get;   set; }
        public bool IsMonthly { get; set; }
        public string Session { get; set; }

        #endregion

        #region Hostel Entry
        public DataSet HostelTypeList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelMasterId", Pk_HostelTypeId) };
            DataSet ds = Connection.ExecuteQuery("HostelTypeList", para);
            return ds;
        }

        public DataSet SaveHostel()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_HostelMasterId", Pk_HostelTypeId),
                new SqlParameter("@HostelName", HostelName),
                new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("InsertHostel", para);
            return ds;
        }

        public DataSet GetHostelList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelId", Pk_HostelId),
                 new SqlParameter("@Fk_HostelTYpeId", Pk_HostelTypeId),
            };
            DataSet ds = Connection.ExecuteQuery("GetHostelEntryList", para);
            return ds;
        }

        public DataSet DeletedHostel()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelId", Pk_HostelId),
                new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteHostelEntry", para);
            return ds;
        }

        public DataSet UpdateHostel()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelId", Pk_HostelId),
                new SqlParameter("@Fk_HostelMasterId", Pk_HostelTypeId),
                new SqlParameter("@HostelName", HostelName),
                new SqlParameter("@UpdatedBy", UpdatedBy) };
            DataSet ds = Connection.ExecuteQuery("UpdateHostel", para);
            return ds;
        }
        #endregion

        #region hostel floor
        
        public DataSet SaveHostelFloor()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_HostelMasterId", Pk_HostelTypeId),
                                      new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                      new SqlParameter("@FloorName", FloorName),
                                      new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertHostelFloor", para);
            return ds;
        }

        public DataSet GetHostelFloorList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelFloorId", Pk_HostelFloorId),
                 new SqlParameter("@Fk_HostelId", Pk_HostelId),
                  new SqlParameter("@Fk_HostelTypeId", Pk_HostelTypeId),

            };
            DataSet ds = Connection.ExecuteQuery("GetHostelFloorList", para);
            return ds;
        }

        public DataSet DeleteHostelFloor()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelFloorId", Pk_HostelFloorId), new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteHostelFloor", para);
            return ds;
        }
        public DataSet UpdateHostelFloor()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelFloorId", Pk_HostelFloorId),
                                      new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                      new SqlParameter("@Fk_HostelId", Pk_HostelId),
                                      new SqlParameter("@FloorName", FloorName),
                                      new SqlParameter("@UpdatedBy", UpdatedBy) };
            DataSet ds = Connection.ExecuteQuery("UpdateHostelFloor", para);
            return ds;
        }
        #endregion

        #region Hostel Room

        public DataSet SaveHostelRoom()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                      new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                       new SqlParameter("@Fk_HostelFloorId",Pk_HostelFloorId),
                                      new SqlParameter("@FromRoom",FromRoom),
                                      new SqlParameter("@ToRoom",ToRoom),
                                      new SqlParameter("@AddedBy",AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertHostelRoom", para);
            return ds;
        }

        public DataSet GetHostelRoomList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelRoomId", Pk_HostelRoomId) };
            DataSet ds = Connection.ExecuteQuery("GetHostelRoomList", para);
            return ds;
        }

        public DataSet DeleteHostelRoom()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelRoomId", Pk_HostelRoomId), new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteHostelRoom", para);
            return ds;
        }

        public DataSet UpdateHostelRoom()
        {

            SqlParameter[] para ={
                                    new SqlParameter("@Pk_HostelRoomId",Pk_HostelRoomId),
                                    new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                    new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                    new SqlParameter("@Fk_HostelFloorId",Pk_HostelFloorId),
                                    new SqlParameter("@FromRoom",FromRoom),
                                    //new SqlParameter("@ToRoom",ToRoom),
                                    new SqlParameter("@UpdatedBy",UpdatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("UpdateHostelRoom", para);
            return ds;
        }
        #endregion

        #region Bed
        public DataSet GetRoomNumberByFloorName()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_HostelFloorId", Pk_HostelFloorId) };
            DataSet ds = Connection.ExecuteQuery("GetHostelRoomList", para);
            return ds;
        }

        public DataSet SaveBedCapacity()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                      new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                      new SqlParameter("@Fk_HostelFloorId",Pk_HostelFloorId),
                                      new SqlParameter("@Fk_HostelRoomId",Pk_HostelRoomId),
                                       new SqlParameter("@BedCapacity",BedCapacity),
                                      new SqlParameter("@AddedBy",AddedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("InsertBedCapacity", para);
            return ds;
        }

        public DataSet BedCapacityList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_BedRoomId", Pk_BedId) };
            DataSet ds = Connection.ExecuteQuery("GetBedRoomList", para);
            return ds;
        }

        public DataSet DeleteBedCapacity()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_BedRoomId", Pk_BedId),
                                      new SqlParameter("@DeletedBy", DeletedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteHostelBedRoom", para);
            return ds;
        }

        public DataSet UpdateBedCapacity()
        {
            SqlParameter[] para ={
                                      new SqlParameter("@Pk_BedRoomId",Pk_BedId),
                                   new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                      new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                      new SqlParameter("@Fk_HostelFloorId",Pk_HostelFloorId),
                                      new SqlParameter("@Fk_HostelRoomId",Pk_HostelRoomId),
                                       new SqlParameter("@BedCapacity",BedCapacity),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("UpdateHostelBedRoom", para);
            return ds;
        }

        #endregion

        #region allot room 
       

        public DataSet SaveHostelAllotment()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                      new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                      new SqlParameter("@Fk_StudentID",Pk_StudentID),
                                      new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                      new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                      new SqlParameter("@Fk_HostelFloorId",Pk_HostelFloorId),
                                      new SqlParameter("@Fk_HostelRoomId",Pk_HostelRoomId),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Amount",Amount),
                                         new SqlParameter("@Fk_SessionId",Session),

                                  };
            DataSet ds = Connection.ExecuteQuery("InsertHostelAllotment", para);
            return ds;
        }

        public DataSet HostelAllotmentList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelAllotmentId", Pk_HostelAllotmentId) ,

                new SqlParameter("@Session", Session) ,
            };
            DataSet ds = Connection.ExecuteQuery("GetHostelAllotmentList", para);
            return ds;

        }

        public DataSet DeleteHostelAllotment()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_HostelAllotmentId", Pk_HostelAllotmentId),
                new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteHostelAllotment", para);
            return ds;
        }

        public DataSet UpdateHostelAllotment()
        {

            SqlParameter[] para ={
                                    new SqlParameter("@Pk_HostelAllotmentId",Pk_HostelAllotmentId),
                                     new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                      new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                    new SqlParameter("@Fk_StudentId",Pk_StudentID),
                                    new SqlParameter("@Fk_HostelMasterId",Pk_HostelTypeId),
                                    new SqlParameter("@Fk_HostelId",Pk_HostelId),
                                    new SqlParameter("@Fk_HostelFloorId",Pk_HostelFloorId),
                                    new SqlParameter("@Fk_HostelRoomId",Pk_HostelRoomId),
                                    new SqlParameter("@UpdatedBy",UpdatedBy),
                                      new SqlParameter("@Amount",Amount),
                                         
                                };
            DataSet ds = Connection.ExecuteQuery("UpdateHostelAllotment", para);
            return ds;

        }

        public DataSet GetStudentBySection()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@Fk_SectionID",Fk_SectionID),
                                   new SqlParameter("@Fk_ClassID",Fk_ClassID),
                                     new SqlParameter("@Session",Session),
                               };
            DataSet ds = Connection.ExecuteQuery("GetStudentList", para);
            return ds;
        }
        #endregion

    }
}