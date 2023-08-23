using AfluexSchool.Models;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AfluexSchool.Controllers
{
    public class ExportController : AdminBaseController
    {
        // GET: Export
        public void _export(DataTable dt, string fname)
        {
            if (dt.Rows.Count > 0)
            {

                string filename = fname + DateTime.Now.ToString("ddMMyyyyhhMMss") + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                if (fname == "ParentEnquiry")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_ParentEnquiryID");
                        dt.Columns.Remove("Fk_ClassId");
                        dt.Columns.Remove("AddedOn");
                        dt.Columns.Remove("IsActive");
                        dt.Columns.Remove("AlternateNumber");
                        dt.Columns.Remove("Email");
                        dt.Columns.Remove("CorrespondenceAddress");
                        dt.Columns.Remove("CorrespondencePinCode");
                        dt.Columns.Remove("CorrespondencState");
                        dt.Columns.Remove("Fk_ReligionID");
                        dt.Columns.Remove("CorrespondencCity");
                        dt.Columns.Remove("Fk_FatherOccupationID");
                        dt.Columns.Remove("Fk_MotherOccupationID");
                        dt.Columns.Remove("MotherName");
                        dt.Columns.Remove("Pincode");
                        dt.Columns.Remove("State");
                        dt.Columns.Remove("City");
                        dt.Columns.Remove("FirstName");
                        dt.Columns.Remove("RecDt");

                    }
                    catch { }
                }
                else if (fname == "StudentList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_StudentID");
                        dt.Columns.Remove("Fk_BranchID");
                        dt.Columns.Remove("Fk_ClassID");
                        dt.Columns.Remove("SessionName");
                        dt.Columns.Remove("Fk_SessionID");
                        dt.Columns.Remove("Medium");
                        dt.Columns.Remove("Fk_MotherOccupationID");
                        dt.Columns.Remove("Fk_FatherOccupationID");
                        dt.Columns.Remove("Age");
                        dt.Columns.Remove("Fk_ReligionID");
                        dt.Columns.Remove("StudentPhoto");
                        dt.Columns.Remove("BirthCetificate");
                        dt.Columns.Remove("AddedOn");
                        dt.Columns.Remove("FK_RouteId");
                        dt.Columns.Remove("FK_AreaMasterID");
                        dt.Columns.Remove("VehicleNo");
                        dt.Columns.Remove("DriverName");
                        dt.Columns.Remove("DriverContactNo");
                        dt.Columns.Remove("Amount");
                        dt.Columns.Remove("RouteNo");
                        dt.Columns.Remove("AreaName");
                        dt.Columns.Remove("BranchName");
                        dt.Columns.Remove("ParentName");
                        dt.Columns.Remove("Fk_SectionID");
                        dt.Columns.Remove("Password");
                    }
                    catch { }
                }
                else if (fname == "TeacherList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_TeacherID");
                        dt.Columns.Remove("FK_BranchID");
                        dt.Columns.Remove("FK_ReligionID");
                        dt.Columns.Remove("ImagePath");

                    }
                    catch { }
                }
                else if (fname == "SMSTemplate")
                {
                    try
                    {
                        dt.Columns.Remove("PK_TemplateId");


                    }
                    catch { }
                }
                else if (fname == "SessionList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_SessionId");
                        dt.Columns.Remove("isDeleted");


                    }
                    catch { }
                }
                else if (fname == "ClassList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_ClassId");



                    }
                    catch { }
                }
                else if (fname == "SectionList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_SectionID");
                        dt.Columns.Remove("Fk_ClassID");


                    }
                    catch { }
                }
                else if (fname == "SubjectList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_SubjectId");



                    }
                    catch { }
                }
                else if (fname == "DepartmentList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_DepartmentID");



                    }
                    catch { }
                }
                else if (fname == "FineMaster")
                {
                    try
                    {
                        dt.Columns.Remove("Fk_ClassId");

                        dt.Columns.Remove("IsDaily");
                        dt.Columns.Remove("PK_FineID");


                    }
                    catch { }
                }
                else if (fname == "NoticeList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_NoticeId");

                        dt.Columns.Remove("Pk_ClassId");
                        dt.Columns.Remove("PK_SectionId");


                    }
                    catch { }
                }
                else if (fname == "HolidayList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_HolidayId");




                    }
                    catch { }
                }
                else if (fname == "VehicleTypeList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_VehicleTypeID");

                    }
                    catch { }
                }
                else if (fname == "VehicleList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_VehicleMasterID");
                        dt.Columns.Remove("FK_VehicleTypeID");


                    }
                    catch { }
                }
                else if (fname == "AreaMasterList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_AreaMasterID");

                    }
                    catch { }
                }
                else if (fname == "RouteList")
                {
                    try
                    {
                        dt.Columns.Remove("PK_RouteId");
                        dt.Columns.Remove("FK_VehicleTypeID");
                        dt.Columns.Remove("FK_VehicleMasterID");

                    }
                    catch { }
                }
                else if (fname == "AllotVehicleList")

                {
                    try
                    {
                        dt.Columns.Remove("FK_TeacherID");
                        dt.Columns.Remove("Fk_StudentID");
                        dt.Columns.Remove("FK_SessionID");
                        dt.Columns.Remove("FK_RouteId");
                        dt.Columns.Remove("FK_AreaMasterID");
                        dt.Columns.Remove("Pk_AlotVehicleID");

                    }
                    catch { }
                }
                else if (fname == "UserList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_AdminId");
                        dt.Columns.Remove("UserImage");
                        dt.Columns.Remove("PANImage");
                        dt.Columns.Remove("AadharImage");
                        dt.Columns.Remove("Fk_BranchId");
                        dt.Columns.Remove("FK_RoleID");


                    }
                    catch { }
                }
                else if (fname == "HomeworkList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_HomeworkID");
                        dt.Columns.Remove("HomeworkFile");
                    }
                    catch { }
                }
                else if (fname == "ClassTeacherList")
                {
                    try
                    {
                        dt.Columns.Remove("Pk_AssignClassTeacherId");
                        dt.Columns.Remove("FK_TeacherID");
                        dt.Columns.Remove("Fk_ClassId");
                        dt.Columns.Remove("Fk_SectionId");
                    }
                    catch { }
                }
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //string style = @" .text { mso-number-format:\@; }  ";
                string style = @"<style> td { mso-number-format:\@; } </style> ";

                Response.Clear();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter s_Write = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter h_write = new HtmlTextWriter(s_Write);
                dgGrid.ShowHeader = true;
                dgGrid.RenderControl(h_write);
                string headerTable = @"<table style='width: 100%;'>
                <tr><td><table border='0' style='border-collapse:
                collapse;width:573pt;max-width:100%;border-spacing: 0;box-sizing: border-box'>
                <colgroup><col style='width:54pt'/><col style='mso-width-source:userset;mso-width-alt:13952;width:327pt' width='436' />
                <col style='mso-width-source:userset;mso-width-alt:8192;width:192pt' width='256' />
                </colgroup><tr><td colspan='4' style='width: 327pt;color: black;font-size: 14.0pt;font-weight: 700;
                font-style: normal;text-decoration: none;font-family:Times New Roman,serif;text-align: center;
                vertical-align: middle;white-space: normal;border: .5pt solid windowtext;padding: 0px;background: #B4C6E7;'><span style='font-variant-ligatures: normal;font-variant-caps: normal;orphans: 2;
                widows: 2;-webkit-text-stroke-width: 0px;text-decoration-style: initial;text-decoration-color: initial'>" + Common.SoftwareDetails.CompanyName + "<br />"
                + "Registration / Affilation No. " + Common.SoftwareDetails.AffliateNo + "</span></td><td style='width: 192pt;color: black;"
                + "font-size: 14.0pt;font-weight: 700;font-style: normal;text-decoration: none;font-family:Times New Roman, serif;text-align: center;vertical-align: middle;white-space: normal;border: .5pt solid windowtext;"
                + "padding: 0px;background: #B4C6E7;' colspan='4'>" + fname + "</td></tr></table></td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></table>";
                Response.Write(headerTable);
                Response.Write(style);
                Response.Write(s_Write.ToString());
                Response.End();
            }

        }

        public void _pdf(DataTable dt, string fname)
        {
            string filename = fname + DateTime.Now.ToString("ddMMyyyyhhMMss") + ".pdf";

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
             "attachment;filename=" + filename + "");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView grd = new GridView();
            if (fname == "ParentEnquiry")
            {
                try
                {
                    dt.Columns.Remove("Pk_ParentEnquiryID");
                    dt.Columns.Remove("Fk_ClassId");
                    dt.Columns.Remove("AddedOn");
                    dt.Columns.Remove("IsActive");
                    dt.Columns.Remove("AlternateNumber");
                    dt.Columns.Remove("Email");
                    dt.Columns.Remove("CorrespondenceAddress");
                    dt.Columns.Remove("CorrespondencePinCode");
                    dt.Columns.Remove("CorrespondencState");
                    dt.Columns.Remove("Fk_ReligionID");
                    dt.Columns.Remove("CorrespondencCity");
                    dt.Columns.Remove("Fk_FatherOccupationID");
                    dt.Columns.Remove("Fk_MotherOccupationID");
                    dt.Columns.Remove("MotherName");
                    dt.Columns.Remove("Pincode");
                    dt.Columns.Remove("State");
                    dt.Columns.Remove("City");
                    dt.Columns.Remove("FirstName");
                    dt.Columns.Remove("RecDt");
                }
                catch { }
            }
            else if (fname == "StudentList")
            {
                try
                {
                    dt.Columns.Remove("Pk_StudentID");
                    dt.Columns.Remove("Fk_BranchID");
                    dt.Columns.Remove("Fk_ClassID");
                    dt.Columns.Remove("SessionName");
                    dt.Columns.Remove("Fk_SessionID");
                    dt.Columns.Remove("Medium");
                    dt.Columns.Remove("Fk_MotherOccupationID");
                    dt.Columns.Remove("Fk_FatherOccupationID");
                    dt.Columns.Remove("Age");
                    dt.Columns.Remove("Fk_ReligionID");
                    dt.Columns.Remove("StudentPhoto");
                    dt.Columns.Remove("BirthCetificate");
                    dt.Columns.Remove("AddedOn");
                    dt.Columns.Remove("FK_RouteId");
                    dt.Columns.Remove("FK_AreaMasterID");
                    dt.Columns.Remove("VehicleNo");
                    dt.Columns.Remove("DriverName");
                    dt.Columns.Remove("DriverContactNo");
                    dt.Columns.Remove("Amount");
                    dt.Columns.Remove("RouteNo");
                    dt.Columns.Remove("AreaName");
                    dt.Columns.Remove("BranchName");
                    dt.Columns.Remove("ParentName");
                    dt.Columns.Remove("Fk_SectionID");
                    dt.Columns.Remove("Password");
                }
                catch { }
            }
            else if (fname == "TeacherList")
            {
                try
                {
                    dt.Columns.Remove("PK_TeacherID");
                    dt.Columns.Remove("FK_BranchID");
                    dt.Columns.Remove("FK_ReligionID");
                    dt.Columns.Remove("ImagePath");

                }
                catch { }
            }
            else if (fname == "SMSTemplate")
            {
                try
                {
                    dt.Columns.Remove("PK_TemplateId");


                }
                catch { }
            }
            else if (fname == "SessionList")
            {
                try
                {
                    dt.Columns.Remove("Pk_SessionId");
                    dt.Columns.Remove("isDeleted");


                }
                catch { }
            }
            else if (fname == "ClassList")
            {
                try
                {
                    dt.Columns.Remove("Pk_ClassId");



                }
                catch { }
            }
            else if (fname == "SectionList")
            {
                try
                {
                    dt.Columns.Remove("PK_SectionID");
                    dt.Columns.Remove("Fk_ClassID");


                }
                catch { }
            }
            else if (fname == "SubjectList")
            {
                try
                {
                    dt.Columns.Remove("Pk_SubjectId");



                }
                catch { }
            }
            else if (fname == "DepartmentList")
            {
                try
                {
                    dt.Columns.Remove("PK_DepartmentID");



                }
                catch { }
            }
            else if (fname == "FineMaster")
            {
                try
                {
                    dt.Columns.Remove("Fk_ClassId");

                    dt.Columns.Remove("IsDaily");
                    dt.Columns.Remove("PK_FineID");


                }
                catch { }
            }
            else if (fname == "NoticeList")
            {
                try
                {
                    dt.Columns.Remove("PK_NoticeId");

                    dt.Columns.Remove("Pk_ClassId");
                    dt.Columns.Remove("PK_SectionId");


                }
                catch { }
            }
            else if (fname == "HolidayList")
            {
                try
                {
                    dt.Columns.Remove("PK_HolidayId");

                }
                catch { }
            }
            else if (fname == "VehicleTypeList")
            {
                try
                {
                    dt.Columns.Remove("PK_VehicleTypeID");

                }
                catch { }
            }
            else if (fname == "VehicleList")
            {
                try
                {
                    dt.Columns.Remove("PK_VehicleMasterID");
                    dt.Columns.Remove("FK_VehicleTypeID");


                }
                catch { }
            }
            else if (fname == "AreaMasterList")
            {
                try
                {
                    dt.Columns.Remove("PK_AreaMasterID");

                }
                catch { }
            }
            else if (fname == "RouteList")
            {
                try
                {
                    dt.Columns.Remove("PK_RouteId");
                    dt.Columns.Remove("FK_VehicleTypeID");
                    dt.Columns.Remove("FK_VehicleMasterID");

                }
                catch { }
            }
            else if (fname == "AllotVehicleList")
            {
                try
                {
                    dt.Columns.Remove("FK_TeacherID");
                    dt.Columns.Remove("Fk_StudentID");
                    dt.Columns.Remove("FK_SessionID");
                    dt.Columns.Remove("FK_RouteId");
                    dt.Columns.Remove("FK_AreaMasterID");
                    dt.Columns.Remove("Pk_AlotVehicleID");

                }
                catch { }
            }

            else if (fname == "UserList")
            {
                try
                {
                    dt.Columns.Remove("Pk_AdminId");
                    dt.Columns.Remove("UserImage");
                    dt.Columns.Remove("PANImage");
                    dt.Columns.Remove("AadharImage");
                    dt.Columns.Remove("Fk_BranchId");
                    dt.Columns.Remove("FK_RoleID");

                }
                catch { }
            }
            else if (fname == "HomeworkList")
            {
                try
                {
                    dt.Columns.Remove("Pk_HomeworkID");
                    dt.Columns.Remove("HomeworkFile");
                }
                catch { }
            }
            else if (fname == "ClassTeacherList")
            {
                try
                {
                    dt.Columns.Remove("Pk_AssignClassTeacherId");
                    dt.Columns.Remove("FK_TeacherID");
                    dt.Columns.Remove("Fk_ClassId");
                    dt.Columns.Remove("Fk_SectionId");
                }
                catch { }
            }
            grd.DataSource = dt;
            grd.DataBind();
            grd.AllowPaging = false;

            grd.HeaderRow.Style.Add("background-color", "#8ECBCA");
            int i = grd.Rows.Count;
            grd.Rows[i - 1].Style.Add("background-color", "#8ECBCA");
            grd.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            string headerTable = @"<table style='width: 100%;'>
                <tr><td><table border='0' style='border-collapse:
                collapse;width:573pt;max-width:100%;border-spacing: 0;box-sizing: border-box'>
                <colgroup><col style='width:54pt'/><col style='mso-width-source:userset;mso-width-alt:13952;width:327pt' width='436' />
                <col style='mso-width-source:userset;mso-width-alt:8192;width:192pt' width='256' />
                </colgroup><tr><td colspan='14' style='width: 327pt;color: black;font-size: 14.0pt;font-weight: 700;
                font-style: normal;text-decoration: none;font-family:Times New Roman,serif;text-align: center;
                vertical-align: middle;white-space: normal;border: .5pt solid windowtext;padding: 0px;background: #B4C6E7;'><span style='font-variant-ligatures: normal;font-variant-caps: normal;orphans: 2;
                widows: 2;-webkit-text-stroke-width: 0px;text-decoration-style: initial;text-decoration-color: initial'>" + Common.SoftwareDetails.CompanyName + "<br />"
                + "Registration / Affilation No. " + Common.SoftwareDetails.AffliateNo + "</span></td><td style='width: 192pt;color: black;"
                + "font-size: 14.0pt;font-weight: 700;font-style: normal;text-decoration: none;font-family:Times New Roman, serif;text-align: center;vertical-align: middle;white-space: normal;border: .5pt solid windowtext;"
                + "padding: 0px;background: #B4C6E7;' colspan='14'>" + fname + "</td></tr></table></td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></table>";
            //htmlparser.Parse(new StringReader(headerTable));
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
        }

        public void _word(DataTable dt, string fname)
        {

            string filename = fname + DateTime.Now.ToString("ddMMyyyyhhMMss") + ".doc";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView grd = new GridView();
            if (fname == "ParentEnquiry")
            {
                try
                {
                    dt.Columns.Remove("Pk_ParentEnquiryID");
                    dt.Columns.Remove("Fk_ClassId");
                    dt.Columns.Remove("AddedOn");
                    dt.Columns.Remove("IsActive");
                    dt.Columns.Remove("AlternateNumber");
                    dt.Columns.Remove("Email");
                    dt.Columns.Remove("CorrespondenceAddress");
                    dt.Columns.Remove("CorrespondencePinCode");
                    dt.Columns.Remove("CorrespondencState");
                    dt.Columns.Remove("Fk_ReligionID");
                    dt.Columns.Remove("CorrespondencCity");
                    dt.Columns.Remove("Fk_FatherOccupationID");
                    dt.Columns.Remove("Fk_MotherOccupationID");
                    dt.Columns.Remove("MotherName");
                    dt.Columns.Remove("Pincode");
                    dt.Columns.Remove("State");
                    dt.Columns.Remove("City");
                    dt.Columns.Remove("FirstName");
                    dt.Columns.Remove("RecDt");
                }
                catch { }
            }
            else if (fname == "StudentList")
            {
                try
                {
                    dt.Columns.Remove("Pk_StudentID");
                    dt.Columns.Remove("Fk_BranchID");
                    dt.Columns.Remove("Fk_ClassID");
                    dt.Columns.Remove("SessionName");
                    dt.Columns.Remove("Fk_SessionID");
                    dt.Columns.Remove("Medium");
                    dt.Columns.Remove("Fk_MotherOccupationID");
                    dt.Columns.Remove("Fk_FatherOccupationID");
                    dt.Columns.Remove("Age");
                    dt.Columns.Remove("Fk_ReligionID");
                    dt.Columns.Remove("StudentPhoto");
                    dt.Columns.Remove("BirthCetificate");
                    dt.Columns.Remove("AddedOn");
                    dt.Columns.Remove("FK_RouteId");
                    dt.Columns.Remove("FK_AreaMasterID");
                    dt.Columns.Remove("VehicleNo");
                    dt.Columns.Remove("DriverName");
                    dt.Columns.Remove("DriverContactNo");
                    dt.Columns.Remove("Amount");
                    dt.Columns.Remove("RouteNo");
                    dt.Columns.Remove("AreaName");
                    dt.Columns.Remove("BranchName");
                    dt.Columns.Remove("ParentName");
                    dt.Columns.Remove("Fk_SectionID");
                    dt.Columns.Remove("Password");
                }
                catch { }
            }
            else if (fname == "TeacherList")
            {
                try
                {
                    dt.Columns.Remove("PK_TeacherID");
                    dt.Columns.Remove("FK_BranchID");
                    dt.Columns.Remove("FK_ReligionID");
                    dt.Columns.Remove("ImagePath");

                }
                catch { }
            }
            else if (fname == "SMSTemplate")
            {
                try
                {
                    dt.Columns.Remove("PK_TemplateId");


                }
                catch { }
            }
            else if (fname == "SessionList")
            {
                try
                {
                    dt.Columns.Remove("Pk_SessionId");
                    dt.Columns.Remove("isDeleted");


                }
                catch { }
            }
            else if (fname == "ClassList")
            {
                try
                {
                    dt.Columns.Remove("Pk_ClassId");



                }
                catch { }
            }
            else if (fname == "SectionList")
            {
                try
                {
                    dt.Columns.Remove("PK_SectionID");
                    dt.Columns.Remove("Fk_ClassID");


                }
                catch { }
            }
            else if (fname == "SubjectList")
            {
                try
                {
                    dt.Columns.Remove("Pk_SubjectId");



                }
                catch { }
            }
            else if (fname == "DepartmentList")
            {
                try
                {
                    dt.Columns.Remove("PK_DepartmentID");



                }
                catch { }
            }
            else if (fname == "FineMaster")
            {
                try
                {
                    dt.Columns.Remove("Fk_ClassId");

                    dt.Columns.Remove("IsDaily");
                    dt.Columns.Remove("PK_FineID");


                }
                catch { }
            }
            else if (fname == "NoticeList")
            {
                try
                {
                    dt.Columns.Remove("PK_NoticeId");

                    dt.Columns.Remove("Pk_ClassId");
                    dt.Columns.Remove("PK_SectionId");


                }
                catch { }
            }
            else if (fname == "HolidayList")
            {
                try
                {
                    dt.Columns.Remove("PK_HolidayId");




                }
                catch { }
            }
            else if (fname == "VehicleTypeList")
            {
                try
                {
                    dt.Columns.Remove("PK_VehicleTypeID");

                }
                catch { }
            }
            else if (fname == "VehicleList")
            {
                try
                {
                    dt.Columns.Remove("PK_VehicleMasterID");
                    dt.Columns.Remove("FK_VehicleTypeID");


                }
                catch { }
            }
            else if (fname == "AreaMasterList")
            {
                try
                {
                    dt.Columns.Remove("PK_AreaMasterID");

                }
                catch { }
            }
            else if (fname == "RouteList")
            {
                try
                {
                    dt.Columns.Remove("PK_RouteId");
                    dt.Columns.Remove("FK_VehicleTypeID");
                    dt.Columns.Remove("FK_VehicleMasterID");

                }
                catch { }
            }
            else if (fname == "AllotVehicleList")
            {
                try
                {
                    dt.Columns.Remove("FK_TeacherID");
                    dt.Columns.Remove("Fk_StudentID");
                    dt.Columns.Remove("FK_SessionID");
                    dt.Columns.Remove("FK_RouteId");
                    dt.Columns.Remove("FK_AreaMasterID");
                    dt.Columns.Remove("Pk_AlotVehicleID");

                }
                catch { }
            }

            else if (fname == "UserList")
            {
                try
                {
                    dt.Columns.Remove("Pk_AdminId");
                    dt.Columns.Remove("UserImage");
                    dt.Columns.Remove("PANImage");
                    dt.Columns.Remove("AadharImage");
                    dt.Columns.Remove("Fk_BranchId");
                    dt.Columns.Remove("FK_RoleID");

                }
                catch { }
            }
            else if (fname == "HomeworkList")
            {
                try
                {
                    dt.Columns.Remove("Pk_HomeworkID");
                    dt.Columns.Remove("HomeworkFile");
                }
                catch { }
            }
            else if (fname == "ClassTeacherList")
            {
                try
                {
                    dt.Columns.Remove("Pk_AssignClassTeacherId");
                    dt.Columns.Remove("FK_TeacherID");
                    dt.Columns.Remove("Fk_ClassId");
                    dt.Columns.Remove("Fk_SectionId");
                }
                catch { }
            }
            grd.DataSource = dt;
            grd.DataBind();
            grd.AllowPaging = false;
            grd.HeaderRow.Style.Add("background-color", "#8ECBCA");
            int i = grd.Rows.Count;
            grd.Rows[i - 1].Style.Add("background-color", "#8ECBCA");
            grd.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            Response.Clear();
        }



        public ActionResult ExportToExcel(string id, string PageName)
        {

            string filename = PageName;
            if (Session["dt"] != null)
            {
                DataTable dtt = (DataTable)Session["dt"];
                if (id == "excel")
                {
                    _export(dtt, filename);
                }
                else if (id == "pdf")
                {
                    _pdf(dtt, filename);
                }
                else if (id == "word")
                {
                    _word(dtt, filename);
                }
                else
                {

                }

            }
            return null;
        }
    }
}