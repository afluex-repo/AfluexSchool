using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class StudentController : AdminBaseController
    {

        #region Student Registration
        public ActionResult GetStateCityByPincode(string PinCode)
        {
            Parent model = new Parent();
            try
            {

                model.PinCode = PinCode;



                DataSet ds = model.GetStateCityByPincode();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    model.State = ds.Tables[0].Rows[0]["StateName"].ToString();
                    model.City = ds.Tables[0].Rows[0]["CityName"].ToString();
                    model.Result = "Yes";
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateCityByPincodeCorres(string correspondencPinCode)
        {
            Student model = new Student();
            try
            {
                model.correspondencPinCode = correspondencPinCode;

                DataSet ds = model.GetStateCityByPincodecorres();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    model.correspondencState = ds.Tables[0].Rows[0]["StateName"].ToString();
                    model.correspondencCity = ds.Tables[0].Rows[0]["CityName"].ToString();
                    model.Result = "Yes";
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudentRegistration(Student model, string Pk_StudentID, string Pk_ParentID)
        {
            #region For Parent Enquiry
            if (Pk_ParentID != null)
            {
                DataSet dssess = new DataSet();
                Student objnew = new Student();
                DataSet dssession = new DataSet();
                try
                {

                    objnew.Fk_ParentId = Pk_ParentID;
                    DataSet ds4 = objnew.NewStudentOnExistingParent();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {

                        objnew.studentName = ds4.Tables[0].Rows[0]["StudentName"].ToString();
                        objnew.ParentLogin_ID = ds4.Tables[0].Rows[0]["LoginId"].ToString();
                        objnew.ParentName = ds4.Tables[0].Rows[0]["ParentName"].ToString();
                        objnew.Fk_ClassID = ds4.Tables[0].Rows[0]["Fk_ClassId"].ToString();
                        objnew.Mobile = ds4.Tables[0].Rows[0]["Mobile"].ToString();
                        objnew.FatherName = ds4.Tables[0].Rows[0]["ParentName"].ToString();
                        objnew.FatherOccupation = ds4.Tables[0].Rows[0]["Fk_FatherOccupationID"].ToString();
                        objnew.MotherName = ds4.Tables[0].Rows[0]["MotherName"].ToString();
                        objnew.MotherOccupation = ds4.Tables[0].Rows[0]["Fk_MotherOccupationID"].ToString();
                        objnew.PermanentAddress = ds4.Tables[0].Rows[0]["Address"].ToString();
                        objnew.PinCode = ds4.Tables[0].Rows[0]["Pincode"].ToString();
                        objnew.State = ds4.Tables[0].Rows[0]["State"].ToString();
                        objnew.City = ds4.Tables[0].Rows[0]["City"].ToString();
                        objnew.correspondenceAddress = ds4.Tables[0].Rows[0]["CorrespondenceAddress"].ToString();
                        objnew.correspondencPinCode = ds4.Tables[0].Rows[0]["CorrespondencePinCode"].ToString();
                        objnew.correspondencState = ds4.Tables[0].Rows[0]["CorrespondencState"].ToString();
                        objnew.correspondencCity = ds4.Tables[0].Rows[0]["CorrespondencCity"].ToString();
                        objnew.AlternateNumber = ds4.Tables[0].Rows[0]["AlterNateNumber"].ToString();
                        objnew.Email = ds4.Tables[0].Rows[0]["Email"].ToString();
                        objnew.Religion = ds4.Tables[0].Rows[0]["Fk_ReligionID"].ToString();

                    }
                    #region ddlhelclass
                    try
                    {
                        Student obj = new Student();
                        int count = 0;
                        List<SelectListItem> ddlclass = new List<SelectListItem>();
                        DataSet ds1 = obj.GetClassList();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds1.Tables[0].Rows)
                            {
                                if (count == 0)
                                {
                                    ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                                }
                                ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                                count = count + 1;
                            }
                        }

                        ViewBag.ddlclass = ddlclass;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion
                    List<SelectListItem> ddlSection = new List<SelectListItem>();
                    ddlSection.Add(new SelectListItem { Text = "Select Section", Value = "0" });

                    DataSet ds5 = objnew.GetSectionByClass();
                    if (ds5 != null && ds5.Tables.Count > 0)
                    {
                        foreach (DataRow r in ds5.Tables[0].Rows)
                        {

                            ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                        }
                    }

                    ViewBag.ddlsection = ddlSection;

                    #region ddlhelSession
                    try
                    {
                        Student obj1 = new Student();
                        int count1 = 0;
                        List<SelectListItem> ddlsession = new List<SelectListItem>();
                        dssess = obj1.SessionList();
                        if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in dssess.Tables[0].Rows)
                            {
                                if (count1 == 0)
                                {
                                    ddlsession.Add(new SelectListItem { Text = "Select Session", Value = "0" });
                                }
                                ddlsession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                                count1 = count1 + 1;
                            }
                        }

                        ViewBag.ddlsession = ddlsession;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region ddlhelBranch
                    try
                    {
                        Student obj1 = new Student();
                        int count1 = 0;
                        List<SelectListItem> ddlBranch = new List<SelectListItem>();
                        DataSet dsbr = obj1.BranchList();
                        if (dsbr != null && dsbr.Tables.Count > 0 && dsbr.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in dsbr.Tables[0].Rows)
                            {
                                if (count1 == 0)
                                {
                                    ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                                }
                                ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                                count1 = count1 + 1;
                            }
                        }

                        ViewBag.ddlBranch = ddlBranch;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region ddlheloccupation
                    try
                    {
                        Student obj = new Student();
                        int count = 0;
                        List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                        DataSet ds1 = obj.GetOccupation();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds1.Tables[0].Rows)
                            {
                                if (count == 0)
                                {
                                    ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                                }
                                ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                                count = count + 1;
                            }
                        }

                        ViewBag.ddlOccupation = ddlOccupation;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region ddlheloccupationmother
                    try
                    {
                        Student obj = new Student();
                        int count = 0;
                        List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                        DataSet ds1 = obj.GetOccupation();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds1.Tables[0].Rows)
                            {
                                if (count == 0)
                                {
                                    ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                                }
                                ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                                count = count + 1;
                            }
                        }

                        ViewBag.ddlOccupationmother = ddlOccupation;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region ddlhelReligion
                    try
                    {
                        Student obj = new Student();
                        int count = 0;
                        List<SelectListItem> ddlReligion = new List<SelectListItem>();
                        DataSet ds1 = obj.GetReligion();
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
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region ddlCategory
                    try
                    {
                        Student obj = new Student();
                        int countcat = 0;
                        List<SelectListItem> ddlCategory = new List<SelectListItem>();
                        DataSet ds1 = obj.GetCategory();
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
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion
                    #region ddlMedium
                    try
                    {
                        Student obj = new Student();
                        int countmed = 0;
                        List<SelectListItem> ddlMedium = new List<SelectListItem>();
                        DataSet ds1 = obj.GetMedium();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds1.Tables[0].Rows)
                            {
                                if (countmed == 0)
                                {
                                    ddlMedium.Add(new SelectListItem { Text = "Select Medium", Value = "0" });
                                }
                                ddlMedium.Add(new SelectListItem { Text = r["Medium"].ToString(), Value = r["Medium"].ToString() });
                                countmed = countmed + 1;
                            }
                        }

                        ViewBag.ddlMedium = ddlMedium;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion
                    #region ddlGender
                    try
                    {
                        Student obj = new Student();
                        int countgen = 0;
                        List<SelectListItem> ddlGender = new List<SelectListItem>();
                        DataSet ds1 = obj.GetGender();
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
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion


                    #region Bind Route


                    List<SelectListItem> ddlRoute = new List<SelectListItem>();
                    ddlRoute.Add(new SelectListItem { Value = "0", Text = "Select Route" });
                    DataSet ds6 = model.GettingRoute();
                    if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds6.Tables[0].Rows)
                        {

                            ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["PK_RouteId"].ToString() });


                        }

                    }
                    ViewBag.ddlRoute = ddlRoute;
                    #endregion

                    List<SelectListItem> ddlArea = new List<SelectListItem>();

                    Master model1 = new Master();
                    model1.PK_RouteId = objnew.PK_RouteId;
                    DataSet ds = model1.GettingAreaByRoute();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            ddlArea.Add(new SelectListItem { Text = r["AreaName"].ToString(), Value = r["PK_AreaMasterID"].ToString() });
                        }
                        ViewBag.ddlArea = ddlArea;

                    }
                    objnew.Fk_ParentId = Pk_ParentID;
                    objnew.Branch = "1";
                    objnew.Nationality = "Indian";
                    objnew.Medium = "English";
                    objnew.SessionName = dssess.Tables[2].Rows[0]["Pk_SessionId"].ToString();
                    return View(objnew);
                }
                catch (Exception ex)
                {

                }



            }
            #endregion For Parent Enquiry
            #region ForEdit
            model.Pk_StudentID = Pk_StudentID;
            if (model.Pk_StudentID != null)
            {


                Student objstudent = new Student();

                try
                {
                    objstudent.Pk_StudentID = model.Pk_StudentID;
                    objstudent.SessionName = Session["SessionId"].ToString();
                    DataSet ds4 = objstudent.GetStudentList();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        objstudent.Pk_StudentID = ds4.Tables[0].Rows[0]["Pk_StudentID"].ToString();
                        objstudent.studentName = ds4.Tables[0].Rows[0]["StudentName"].ToString();
                        objstudent.LoginID = ds4.Tables[0].Rows[0]["LoginID"].ToString();
                        objstudent.ParentLogin_ID = ds4.Tables[0].Rows[0]["ParentLogin_ID"].ToString();
                        objstudent.ParentName = ds4.Tables[0].Rows[0]["ParentName"].ToString();
                        objstudent.Branch = ds4.Tables[0].Rows[0]["Fk_BranchID"].ToString();
                        objstudent.Fk_ClassID = ds4.Tables[0].Rows[0]["Fk_ClassID"].ToString();
                        objstudent.Fk_SectionID = ds4.Tables[0].Rows[0]["Fk_SectionID"].ToString();
                        objstudent.SessionName = ds4.Tables[0].Rows[0]["Fk_SessionID"].ToString();
                        objstudent.Mobile = ds4.Tables[0].Rows[0]["Mobile"].ToString();
                        objstudent.Medium = ds4.Tables[0].Rows[0]["Medium"].ToString();
                        objstudent.FatherName = ds4.Tables[0].Rows[0]["FatherName"].ToString();
                        objstudent.FatherOccupation = ds4.Tables[0].Rows[0]["Fk_FatherOccupationID"].ToString();
                        objstudent.MotherName = ds4.Tables[0].Rows[0]["MotherName"].ToString();
                        objstudent.MotherOccupation = ds4.Tables[0].Rows[0]["Fk_MotherOccupationID"].ToString();
                        objstudent.Dateofbirth = ds4.Tables[0].Rows[0]["DateOfBirth"].ToString();
                        objstudent.Age = ds4.Tables[0].Rows[0]["Age"].ToString();
                        objstudent.Gender = ds4.Tables[0].Rows[0]["Gender"].ToString();
                        objstudent.Category = ds4.Tables[0].Rows[0]["Category"].ToString();
                        objstudent.Religion = ds4.Tables[0].Rows[0]["Fk_ReligionID"].ToString();
                        objstudent.Nationality = ds4.Tables[0].Rows[0]["Nationality"].ToString();
                        objstudent.AadhaarCard = ds4.Tables[0].Rows[0]["AadhaarCard"].ToString();
                        objstudent.StudentPhoto = ds4.Tables[0].Rows[0]["StudentPhoto"].ToString();
                        objstudent.BirthCetificate = ds4.Tables[0].Rows[0]["BirthCetificate"].ToString();
                        objstudent.PermanentAddress = ds4.Tables[0].Rows[0]["PermanentAddress"].ToString();
                        objstudent.PinCode = ds4.Tables[0].Rows[0]["Pincode"].ToString();
                        objstudent.State = ds4.Tables[0].Rows[0]["State"].ToString();
                        objstudent.City = ds4.Tables[0].Rows[0]["City"].ToString();
                        objstudent.correspondenceAddress = ds4.Tables[0].Rows[0]["CorrespondenceAddress"].ToString();
                        objstudent.correspondencPinCode = ds4.Tables[0].Rows[0]["CorrespondencePinCode"].ToString();
                        objstudent.correspondencState = ds4.Tables[0].Rows[0]["CorrespondencState"].ToString();
                        objstudent.correspondencCity = ds4.Tables[0].Rows[0]["CorrespondencCity"].ToString();
                        objstudent.AlternateNumber = ds4.Tables[0].Rows[0]["AlterNateNumber"].ToString();
                        objstudent.Email = ds4.Tables[0].Rows[0]["Email"].ToString();
                        objstudent.PreviousSchool = ds4.Tables[0].Rows[0]["PreviousSchool"].ToString();
                        objstudent.PreviousClass = ds4.Tables[0].Rows[0]["PreviousClass"].ToString();

                        objstudent.PK_RouteId = ds4.Tables[0].Rows[0]["FK_RouteId"].ToString();
                        objstudent.PK_AreaMasterID = ds4.Tables[0].Rows[0]["FK_AreaMasterID"].ToString();
                        objstudent.VehicleNo = ds4.Tables[0].Rows[0]["VehicleNo"].ToString();
                        objstudent.DriverName = ds4.Tables[0].Rows[0]["DriverName"].ToString();
                        objstudent.DriverContactNo = ds4.Tables[0].Rows[0]["DriverContactNo"].ToString();
                        objstudent.Amount = ds4.Tables[0].Rows[0]["Amount"].ToString();



                    }
                }
                catch (Exception ex)
                {
                    TempData["Student"] = ex.Message;
                }

                #region ddlhelclass
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlclass = new List<SelectListItem>();
                    DataSet ds1 = obj.GetClassList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                            }
                            ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlclass = ddlclass;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
                List<SelectListItem> ddlSection = new List<SelectListItem>();
                ddlSection.Add(new SelectListItem { Text = "Select Section", Value = "0" });

                DataSet ds5 = objstudent.GetSectionList();
                if (ds5 != null && ds5.Tables.Count > 0)
                {
                    foreach (DataRow r in ds5.Tables[0].Rows)
                    {

                        ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }

                ViewBag.ddlsection = ddlSection;

                #region ddlhelSession
                try
                {
                    Student obj1 = new Student();
                    int count1 = 0;
                    List<SelectListItem> ddlsession = new List<SelectListItem>();
                    DataSet dssess = obj1.SessionList();
                    if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dssess.Tables[0].Rows)
                        {
                            if (count1 == 0)
                            {
                                ddlsession.Add(new SelectListItem { Text = "Select Session", Value = "0" });
                            }
                            ddlsession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                            count1 = count1 + 1;
                        }
                    }

                    ViewBag.ddlsession = ddlsession;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlhelBranch
                try
                {
                    Student obj1 = new Student();
                    int count1 = 0;
                    List<SelectListItem> ddlBranch = new List<SelectListItem>();
                    DataSet dsbr = obj1.BranchList();
                    if (dsbr != null && dsbr.Tables.Count > 0 && dsbr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsbr.Tables[0].Rows)
                        {
                            if (count1 == 0)
                            {
                                ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                            }
                            ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                            count1 = count1 + 1;
                        }
                    }

                    ViewBag.ddlBranch = ddlBranch;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlheloccupation
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                    DataSet ds1 = obj.GetOccupation();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                            }
                            ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlOccupation = ddlOccupation;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlheloccupationmother
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                    DataSet ds1 = obj.GetOccupation();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                            }
                            ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlOccupationmother = ddlOccupation;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlhelReligion
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlReligion = new List<SelectListItem>();
                    DataSet ds1 = obj.GetReligion();
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlCategory
                try
                {
                    Student obj = new Student();
                    int countcat = 0;
                    List<SelectListItem> ddlCategory = new List<SelectListItem>();
                    DataSet ds1 = obj.GetCategory();
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
                #region ddlMedium
                try
                {
                    Student obj = new Student();
                    int countmed = 0;
                    List<SelectListItem> ddlMedium = new List<SelectListItem>();
                    DataSet ds1 = obj.GetMedium();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (countmed == 0)
                            {
                                ddlMedium.Add(new SelectListItem { Text = "Select Medium", Value = "0" });
                            }
                            ddlMedium.Add(new SelectListItem { Text = r["Medium"].ToString(), Value = r["Medium"].ToString() });
                            countmed = countmed + 1;
                        }
                    }

                    ViewBag.ddlMedium = ddlMedium;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
                #region ddlGender
                try
                {
                    Student obj = new Student();
                    int countgen = 0;
                    List<SelectListItem> ddlGender = new List<SelectListItem>();
                    DataSet ds1 = obj.GetGender();
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion


                #region Bind Route


                List<SelectListItem> ddlRoute = new List<SelectListItem>();
                ddlRoute.Add(new SelectListItem { Value = "0", Text = "Select Route" });
                DataSet ds6 = model.GettingRoute();
                if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds6.Tables[0].Rows)
                    {

                        ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["PK_RouteId"].ToString() });


                    }

                }
                ViewBag.ddlRoute = ddlRoute;
                #endregion

                List<SelectListItem> ddlArea = new List<SelectListItem>();

                Master model1 = new Master();
                model1.PK_RouteId = objstudent.PK_RouteId;
                DataSet ds = model1.GettingAreaByRoute();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlArea.Add(new SelectListItem { Text = r["AreaName"].ToString(), Value = r["PK_AreaMasterID"].ToString() });
                    }
                    ViewBag.ddlArea = ddlArea;
                     
                }
                return View(objstudent);
            }
            #endregion ForEdit


            else
            {
                DataSet dssession = new DataSet();
                #region ddlhelclass
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlclass = new List<SelectListItem>();
                    DataSet ds1 = obj.GetClassList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                            }
                            ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlclass = ddlclass;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
                ViewBag.ddlsection = ddlsection;

                #region ddlhelSession
                try
                {
                    Student obj1 = new Student();
                    int count1 = 0;
                    List<SelectListItem> ddlsession = new List<SelectListItem>();
                    dssession = obj1.SessionList();
                    if (dssession != null && dssession.Tables.Count > 0 && dssession.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dssession.Tables[0].Rows)
                        {
                            if (count1 == 0)
                            {
                                ddlsession.Add(new SelectListItem { Text = "Select Session", Value = "0" });
                            }
                            ddlsession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                            count1 = count1 + 1;
                        }
                    }

                    ViewBag.ddlsession = ddlsession;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlhelBranch
                try
                {
                    Student obj1 = new Student();
                    int count1 = 0;
                    List<SelectListItem> ddlBranch = new List<SelectListItem>();
                    DataSet ds = obj1.BranchList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            if (count1 == 0)
                            {
                                ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                            }
                            ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                            count1 = count1 + 1;
                        }
                    }

                    ViewBag.ddlBranch = ddlBranch;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion


                #region ddlheloccupation
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                    DataSet ds1 = obj.GetOccupation();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                            }
                            ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlOccupation = ddlOccupation;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlheloccupationmother
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                    DataSet ds1 = obj.GetOccupation();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                            }
                            ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlOccupationmother = ddlOccupation;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion


                #region ddlhelReligion
                try
                {
                    Student obj = new Student();
                    int count = 0;
                    List<SelectListItem> ddlReligion = new List<SelectListItem>();
                    DataSet ds1 = obj.GetReligion();
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region ddlCategory
                try
                {
                    Student obj = new Student();
                    int countcat = 0;
                    List<SelectListItem> ddlCategory = new List<SelectListItem>();
                    DataSet ds1 = obj.GetCategory();
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
                #region ddlMedium
                try
                {
                    Student obj = new Student();
                    int countmed = 0;
                    List<SelectListItem> ddlMedium = new List<SelectListItem>();
                    DataSet ds1 = obj.GetMedium();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (countmed == 0)
                            {
                                ddlMedium.Add(new SelectListItem { Text = "Select Medium", Value = "0" });
                            }
                            ddlMedium.Add(new SelectListItem { Text = r["Medium"].ToString(), Value = r["Medium"].ToString() });
                            countmed = countmed + 1;
                        }
                    }

                    ViewBag.ddlMedium = ddlMedium;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
                #region ddlGender
                try
                {
                    Student obj = new Student();
                    int countgen = 0;
                    List<SelectListItem> ddlGender = new List<SelectListItem>();
                    DataSet ds1 = obj.GetGender();
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                #region Bind Route


                List<SelectListItem> ddlRoute = new List<SelectListItem>();
                ddlRoute.Add(new SelectListItem { Value = "0", Text = "Select Route" });
                DataSet ds6 = model.GettingRoute();
                if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds6.Tables[0].Rows)
                    {

                        ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["PK_RouteId"].ToString() });


                    }

                }
                ViewBag.ddlRoute = ddlRoute;
                #endregion
                #region Bind Area
                List<SelectListItem> ddlArea = new List<SelectListItem>();
                ddlArea.Add(new SelectListItem { Text = "Select Area", Value = "0" });
                ViewBag.ddlArea = ddlArea;
                #endregion
                model.Branch = "1";
                model.Nationality = "Indian";
                model.Medium = "English";
                model.SessionName = dssession.Tables[2].Rows[0]["Pk_SessionId"].ToString();
                return View(model);
            }

        }
        public ActionResult GetArea(string PK_RouteId)
        {
            List<SelectListItem> ddlArea = new List<SelectListItem>();

            Master model = new Master();
            model.PK_RouteId = PK_RouteId;
            DataSet ds = model.GettingAreaByRoute();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ddlArea.Add(new SelectListItem { Text = r["AreaName"].ToString(), Value = r["PK_AreaMasterID"].ToString() });
                }
                model.ddlArea = ddlArea;

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehicleDetails(string PK_RouteId, string PK_AreaMasterID)
        {
            List<SelectListItem> ddlVehicleNo = new List<SelectListItem>();

            Master model = new Master();
            model.PK_AreaMasterID = PK_AreaMasterID;
            model.PK_RouteId = PK_RouteId;
            DataSet ds = model.GettingAreaByRoute();
            if (ds != null && ds.Tables.Count > 0)
            {
                model.VehicleNo = ds.Tables[0].Rows[0]["VehicleNo"].ToString();
                model.DriverContactNo = ds.Tables[0].Rows[0]["DriverContactNo"].ToString();
                model.DriverName = ds.Tables[0].Rows[0]["DriverName"].ToString();

                model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDriverNameandContact(string PK_VehicleMasterID)
        {
            Master model = new Master();
            model.PK_VehicleMasterID = PK_VehicleMasterID;
            if (PK_VehicleMasterID != null)
            {
                DataSet ds = model.GettingVehicleList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.DriverContactNo = ds.Tables[0].Rows[0]["DriverContactNo"].ToString();
                    model.DriverName = ds.Tables[0].Rows[0]["DriverName"].ToString();
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("StudentRegistration")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult SaveStudentRegistration(IEnumerable<HttpPostedFileBase> StudentFiles, Student obj)
        {
            DataTable dtSMS = new DataTable();
            SendSMS objsms = new SendSMS();
            dtSMS.Columns.Add("Name", typeof(string));
            dtSMS.Columns.Add("Mobile", typeof(string));
            dtSMS.Columns.Add("Status", typeof(string));
            string FormName = "";
            string Controller = "";
            int count = 0;
            try
            {
                {
                    foreach (var file in StudentFiles)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            if (count == 0)
                            {
                                obj.StudentPhoto = "/Students/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                                file.SaveAs(Path.Combine(Server.MapPath(obj.StudentPhoto)));
                            }
                            if (count == 1)
                            {
                                obj.BirthCetificate = "/BirthCetificate/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                                file.SaveAs(Path.Combine(Server.MapPath(obj.BirthCetificate)));
                            }

                        }
                        count++;
                    }
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Dateofbirth = string.IsNullOrEmpty(obj.Dateofbirth) ? null : Common.ConvertToSystemDate(obj.Dateofbirth, "dd/MM/yyyy");
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.SaveStudentRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        try
                        {
                            string sms = "Student Registered Sucessfully.  Your Login Id and password for parent login is :" + ds.Tables[1].Rows[0]["ParentLogin"].ToString() + " and Password is :" + ds.Tables[1].Rows[0]["ParentPassword"].ToString();
                            BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, obj.Mobile, sms);
                            string Status = "";
                            if (obj.Mobile.Length < 10)
                            {
                                Status = "Failed";
                            }
                            else if (obj.Mobile.Length > 10)
                            {
                                Status = "Failed";
                            }
                            else
                            {
                                Status = "Send";
                            }

                            dtSMS.Rows.Add(obj.studentName, obj.Mobile, Status);
                            objsms.dtSMS = dtSMS;
                            objsms.AddedBy = Session["Pk_AdminId"].ToString();
                            objsms.SMS = sms;
                            objsms.TotalSMS = "1";
                            objsms.SMSTemplateText = "Registration";
                            DataSet ds1 = objsms.SaveSMSData();
                        }
                        catch { }

                        TempData["SaveStudent"] = "Student registered successfully";
                        FormName = "StudentRegistration";
                        Controller = "Student";
                    }
                    else
                    {
                        TempData["SaveStudent"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "StudentRegistration";
                        Controller = "Student";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SaveStudent"] = ex.Message;
                FormName = "StudentRegistration";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult StudentRegistrationList(Student model)
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Student List");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission", "Home");
            }
            if (!objPermission.IsUpdate)
            {
                ViewBag.IsEdit = "none";
            }
            else
            {
                ViewBag.IsEdit = "";
            }
            if (!objPermission.IsDelete)
            {
                ViewBag.IsDelete = "none";
            }
            else
            {
                ViewBag.IsDelete = "";
            }
            #endregion
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlsection = ddlsection;

            List<Student> list = new List<Student>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
                model.Fk_SectionID = model.Fk_SectionID == "0" ? null : model.Fk_SectionID;
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.GetStudentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["dt"] = ds.Tables[0];
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                        obj.LoginID = r["LoginID"].ToString();
                        obj.SessionName = r["SessionName"].ToString();
                        obj.Fk_ClassID = r["ClassName"].ToString();
                        obj.Fk_SectionID = r["SectionName"].ToString();
                        obj.StudenLoginID = r["LoginID"].ToString();
                        obj.studentName = r["StudentName"].ToString();
                        obj.Medium = r["Medium"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.AddedOn = r["AddedOn"].ToString();
                        obj.ParentName = r["ParentName"].ToString();
                        obj.ParentLogin_ID = r["ParentLogin_ID"].ToString();
                        obj.ParentPass = r["ParentPass"].ToString();
                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("StudentRegistrationList")]
        [OnAction(ButtonName = "btnsearch")]
        public ActionResult StudentRegistrationListBy(Student model)
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Student List");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission", "Home");
            }
            if (!objPermission.IsUpdate)
            {
                ViewBag.IsEdit = "none";
            }
            else
            {
                ViewBag.IsEdit = "";
            }
            if (!objPermission.IsDelete)
            {
                ViewBag.IsDelete = "none";
            }
            else
            {
                ViewBag.IsDelete = "";
            }
            #endregion
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlsection = ddlsection;

            List<Student> list = new List<Student>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
                model.Fk_SectionID = model.Fk_SectionID == "0" ? null : model.Fk_SectionID;
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.GetStudentList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["dt"] = ds.Tables[0];
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                        obj.LoginID = r["LoginID"].ToString();
                        obj.SessionName = r["SessionName"].ToString();
                        obj.Fk_ClassID = r["ClassName"].ToString();
                        obj.Fk_SectionID = r["SectionName"].ToString();
                        obj.StudenLoginID = r["LoginID"].ToString();
                        obj.studentName = r["StudentName"].ToString();
                        obj.Medium = r["Medium"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.ParentName = r["ParentName"].ToString();
                        obj.ParentLogin_ID = r["ParentLogin_ID"].ToString();
                        obj.ParentPass = r["ParentPass"].ToString();
                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("StudentRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateStudentRegistration(IEnumerable<HttpPostedFileBase> StudentFiles, Student obj)
        {
            string FormName = "";
            string Controller = "";
            int count = 0;
            try
            {
                foreach (var file in StudentFiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        if (count == 0)
                        {
                            obj.StudentPhoto = "/Students/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.StudentPhoto)));
                        }
                        if (count == 1)
                        {
                            obj.BirthCetificate = "/BirthCetificate/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.BirthCetificate)));
                        }

                    }
                    count++;
                }
                obj.Dateofbirth = string.IsNullOrEmpty(obj.Dateofbirth) ? null : Common.ConvertToSystemDate(obj.Dateofbirth, "dd/MM/yyyy");
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();


                DataSet ds = obj.UpdateStudentRegistration();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Student"] = "Student Record Updated successfully .";
                        FormName = "StudentRegistrationList";
                        Controller = "Student";
                    }
                    else
                    {

                        //obj.Pk_StudentID = Session["pk_studentId"].ToString();
                        Session["studentid"] = obj.Pk_StudentID;
                        TempData["SaveStudent"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        #region ddlhelclass
                        try
                        {
                            Student obj1 = new Student();
                            int countst = 0;
                            List<SelectListItem> ddlclass = new List<SelectListItem>();
                            DataSet ds1 = obj.GetClassList();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in ds1.Tables[0].Rows)
                                {
                                    if (countst == 0)
                                    {
                                        ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                                    }
                                    ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                                    countst = countst + 1;
                                }
                            }

                            ViewBag.ddlclass = ddlclass;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion
                        List<SelectListItem> ddlSection = new List<SelectListItem>();
                        ddlSection.Add(new SelectListItem { Text = "Select Section", Value = "0" });

                        DataSet ds5 = obj.GetSectionList();
                        if (ds5 != null && ds5.Tables.Count > 0)
                        {
                            foreach (DataRow r in ds5.Tables[0].Rows)
                            {

                                ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                            }
                        }

                        ViewBag.ddlsection = ddlSection;

                        #region ddlhelSession
                        try
                        {
                            Student obj1 = new Student();
                            int count1 = 0;
                            List<SelectListItem> ddlsession = new List<SelectListItem>();
                            DataSet dssess = obj1.SessionList();
                            if (dssess != null && dssess.Tables.Count > 0 && dssess.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in dssess.Tables[0].Rows)
                                {
                                    if (count1 == 0)
                                    {
                                        ddlsession.Add(new SelectListItem { Text = "Select Session", Value = "0" });
                                    }
                                    ddlsession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                                    count1 = count1 + 1;
                                }
                            }

                            ViewBag.ddlsession = ddlsession;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region ddlhelBranch
                        try
                        {
                            Student obj1 = new Student();
                            int count1 = 0;
                            List<SelectListItem> ddlBranch = new List<SelectListItem>();
                            DataSet dsbr = obj1.BranchList();
                            if (dsbr != null && dsbr.Tables.Count > 0 && dsbr.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in dsbr.Tables[0].Rows)
                                {
                                    if (count1 == 0)
                                    {
                                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                                    }
                                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["Pk_BranchID"].ToString() });
                                    count1 = count1 + 1;
                                }
                            }

                            ViewBag.ddlBranch = ddlBranch;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region ddlheloccupation
                        try
                        {
                            //  Student obj = new Student();
                            int countocc = 0;
                            List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                            DataSet ds1 = obj.GetOccupation();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in ds1.Tables[0].Rows)
                                {
                                    if (countocc == 0)
                                    {
                                        ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                                    }
                                    ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                                    countocc = countocc + 1;
                                }
                            }

                            ViewBag.ddlOccupation = ddlOccupation;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region ddlheloccupationmother
                        try
                        {
                            // Student obj = new Student();
                            int countocm = 0;
                            List<SelectListItem> ddlOccupation = new List<SelectListItem>();
                            DataSet ds1 = obj.GetOccupation();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in ds1.Tables[0].Rows)
                                {
                                    if (countocm == 0)
                                    {
                                        ddlOccupation.Add(new SelectListItem { Text = "Select Occupation", Value = "0" });
                                    }
                                    ddlOccupation.Add(new SelectListItem { Text = r["OccupationName"].ToString(), Value = r["PK_OccupationID"].ToString() });
                                    countocm = countocm + 1;
                                }
                            }

                            ViewBag.ddlOccupationmother = ddlOccupation;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region ddlhelReligion
                        try
                        {
                            // Student obj = new Student();
                            int countrel = 0;
                            List<SelectListItem> ddlReligion = new List<SelectListItem>();
                            DataSet ds1 = obj.GetReligion();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in ds1.Tables[0].Rows)
                                {
                                    if (countrel == 0)
                                    {
                                        ddlReligion.Add(new SelectListItem { Text = "Select Religion", Value = "0" });
                                    }
                                    ddlReligion.Add(new SelectListItem { Text = r["ReligionName"].ToString(), Value = r["Pk_ReligionId"].ToString() });
                                    countrel = countrel + 1;
                                }
                            }

                            ViewBag.ddlReligion = ddlReligion;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region ddlCategory
                        try
                        {
                            // Student obj = new Student();
                            int countcat = 0;
                            List<SelectListItem> ddlCategory = new List<SelectListItem>();
                            DataSet ds1 = obj.GetCategory();
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
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion
                        #region ddlMedium
                        try
                        {
                            ///   Student obj = new Student();
                            int countmed = 0;
                            List<SelectListItem> ddlMedium = new List<SelectListItem>();
                            DataSet ds1 = obj.GetMedium();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in ds1.Tables[0].Rows)
                                {
                                    if (countmed == 0)
                                    {
                                        ddlMedium.Add(new SelectListItem { Text = "Select Medium", Value = "0" });
                                    }
                                    ddlMedium.Add(new SelectListItem { Text = r["Medium"].ToString(), Value = r["Medium"].ToString() });
                                    countmed = countmed + 1;
                                }
                            }

                            ViewBag.ddlMedium = ddlMedium;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion
                        #region ddlGender
                        try
                        {
                            //  Student obj = new Student();
                            int countgen = 0;
                            List<SelectListItem> ddlGender = new List<SelectListItem>();
                            DataSet ds1 = obj.GetGender();
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
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion


                        #region Bind Route


                        List<SelectListItem> ddlRoute = new List<SelectListItem>();
                        ddlRoute.Add(new SelectListItem { Value = "0", Text = "Select Route" });
                        DataSet ds6 = obj.GettingRoute();
                        if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds6.Tables[0].Rows)
                            {

                                ddlRoute.Add(new SelectListItem { Text = r["RouteNo"].ToString(), Value = r["PK_RouteId"].ToString() });


                            }

                        }
                        ViewBag.ddlRoute = ddlRoute;
                        #endregion

                        List<SelectListItem> ddlArea = new List<SelectListItem>();

                        Master model1 = new Master();
                        model1.PK_RouteId = obj.PK_RouteId;
                        DataSet dsr = model1.GettingAreaByRoute();
                        if (dsr != null && dsr.Tables.Count > 0)
                        {
                            foreach (DataRow r in dsr.Tables[0].Rows)
                            {
                                ddlArea.Add(new SelectListItem { Text = r["AreaName"].ToString(), Value = r["PK_AreaMasterID"].ToString() });
                            }
                            ViewBag.ddlArea = ddlArea;

                        }
                        return View(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
                FormName = "StudentRegistration";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult DeleteStudent(string Pk_StudentID, string DeletedBy)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                Student obj = new Student();
                obj.Pk_StudentID = Pk_StudentID;
                obj.DeletedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.DeleteStudent();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Student"] = "Student Record Deleted successfully";
                        FormName = "StudentRegistrationList";
                        Controller = "Student";
                    }
                    else
                    {
                        TempData["Studenterror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "StudentRegistrationList";
                        Controller = "Student";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Studenterror"] = ex.Message;
                FormName = "StudentRegistrationList";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetParentListByLoginID(string ParentLogin_ID)
        {
            Student model = new Student();
            try
            {

                model.ParentLogin_ID = ParentLogin_ID;

                List<Student> listparent = new List<Student>();

                DataSet ds = model.GetParentdetailByLoginID();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    model.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "Yes";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSectionByClass(string Fk_ClassID)
        {
            Student model = new Student();
            try
            {

                model.Fk_ClassID = Fk_ClassID;

                List<SelectListItem> ddlsection = new List<SelectListItem>();

                DataSet ds = model.GetSectionByClass();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                    }
                }

                model.ddlsection = ddlsection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrintIDCard(string Pk_StudentID)
        {
            Student model = new Student();
            model.Pk_StudentID = Pk_StudentID;
            if (model.Pk_StudentID != null)
            { 
                Student objstudent = new Student(); 
                try
                {
                    objstudent.Pk_StudentID = model.Pk_StudentID;
                    objstudent.SessionName = Session["SessionId"].ToString();
                    DataSet ds4 = objstudent.GetStudentList();
                    if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                    {
                        objstudent.Pk_StudentID = ds4.Tables[0].Rows[0]["Pk_StudentID"].ToString();
                        objstudent.studentName = ds4.Tables[0].Rows[0]["StudentName"].ToString();
                        objstudent.LoginID = ds4.Tables[0].Rows[0]["LoginID"].ToString();
                        objstudent.ParentLogin_ID = ds4.Tables[0].Rows[0]["ParentLogin_ID"].ToString();
                        objstudent.ParentName = ds4.Tables[0].Rows[0]["ParentName"].ToString();
                        objstudent.Branch = ds4.Tables[0].Rows[0]["Fk_BranchID"].ToString();
                        objstudent.ClassName = ds4.Tables[0].Rows[0]["ClassName"].ToString();
                        objstudent.SectionName = ds4.Tables[0].Rows[0]["SectionName"].ToString(); 
                        objstudent.Mobile = ds4.Tables[0].Rows[0]["Mobile"].ToString(); 
                        objstudent.FatherName = ds4.Tables[0].Rows[0]["FatherName"].ToString(); 
                        objstudent.MotherName = ds4.Tables[0].Rows[0]["MotherName"].ToString();  
                        objstudent.Dateofbirth = ds4.Tables[0].Rows[0]["DateOfBirth"].ToString(); 
                        objstudent.StudentPhoto = ds4.Tables[0].Rows[0]["StudentPhoto"].ToString(); 
                        objstudent.PermanentAddress = ds4.Tables[0].Rows[0]["PermanentAddress"].ToString();
                        objstudent.PinCode = ds4.Tables[0].Rows[0]["Pincode"].ToString();
                        objstudent.State = ds4.Tables[0].Rows[0]["State"].ToString();
                        objstudent.City = ds4.Tables[0].Rows[0]["City"].ToString();
                        objstudent.RegistrationDate= ds4.Tables[0].Rows[0]["RegistrationNo"].ToString();

                        ViewBag.LandLine = Common.SoftwareDetails.LandLine;
                        ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                        ViewBag.Website = Common.SoftwareDetails.Website;
                        ViewBag.EmailID = Common.SoftwareDetails.EmailID;
                        ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;
                        ViewBag.State = Common.SoftwareDetails.State1;
                        ViewBag.Pin1 = Common.SoftwareDetails.Pin1;
                        ViewBag.AffliateNo = Common.SoftwareDetails.AffliateNo;
                    }
                }
                catch (Exception ex)
                {
                    TempData["Student"] = ex.Message;
                }
  
                return View(objstudent);
            }
            
            return View();
        }

        #endregion

        #region Generate Roll Number

        public ActionResult GenrateStudentRollNumber(Student model)
        {

            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "Select Section", Value = "0" });
            ViewBag.ddlsection = ddlsection;


            return View(model);
        }

        [HttpPost]
        [ActionName("GenrateStudentRollNumber")]
        [OnAction(ButtonName = "btngetdetail")]
        public ActionResult GetStudentBySection(Student model, string Fk_ClassID, string Fk_SectionID)
        {
            Student obj = new Student();

            List<Student> list = new List<Student>();
            try
            {
                model.Fk_ClassID = Fk_ClassID;
                model.Fk_SectionID = Fk_SectionID;
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.GetStudentList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student objroll = new Student();
                        objroll.Pk_StudentID = r["Pk_StudentID"].ToString();
                        objroll.DisplayName = r["StudentName"].ToString();
                        objroll.LoginID = r["LoginID"].ToString();
                        objroll.ClassName = r["ClassName"].ToString();
                        objroll.SessionName = r["SessionName"].ToString();
                        objroll.SectionName = r["SectionName"].ToString();
                        objroll.Mobile = r["Mobile"].ToString();
                        objroll.Medium = r["Medium"].ToString();


                        list.Add(objroll);
                    }
                    model.listStudent = list;
                }

                #region ddlhelclass
                try
                {

                    int count = 0;
                    List<SelectListItem> ddlclass = new List<SelectListItem>();
                    DataSet ds1 = obj.GetClassList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                            }
                            ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlclass = ddlclass;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion

                int count1 = 0;
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                obj.Fk_ClassID = Fk_ClassID;
                DataSet ds2 = obj.GetSectionByClass();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlsection.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlsection = ddlsection;

            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("GenrateStudentRollNumber")]
        [OnAction(ButtonName = "btnsave")]

        public ActionResult StudentGenrateNumber(Student obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.StudentGenrateRollNumber();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["StudentGenrateRollNumber"] = "Roll No. Generated successfully";
                        FormName = "GenrateStudentRollNumber";
                        Controller = "Student";
                    }
                    else
                    {
                        TempData["StudentGenrateRollNumber"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "GenrateStudentRollNumber";
                        Controller = "Student";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["StudentGenrateRollNumber"] = ex.Message;
                FormName = "GenrateStudentRollNumber";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Student Attendance

        public ActionResult StudentAttendance()
        {
            Student model = new Student();
            model.AttendanceDate = DateTime.Now.ToString("dd/MM/yyyy");
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;


            return View(model);
        }
        [HttpPost]
        [ActionName("StudentAttendance")]
        [OnAction(ButtonName = "btndetail")]
        public ActionResult StudentAttendance(Student model)
        {
            model.AttendanceDate = Common.ConvertToSystemDate(model.AttendanceDate, "dd/MM/yyyy");
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlsection
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                DataSet ds1 = obj.GetSectionList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                        }
                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlsection = ddlsection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            List<Student> list = new List<Student>();
            model.SessionName = Session["SessionId"].ToString();
            DataSet ds = model.GetStudentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Student obj = new Student();
                    obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                    obj.DisplayName = r["StudentName"].ToString();
                    obj.LoginID = r["LoginID"].ToString();
                    obj.Fk_ClassID = r["Fk_ClassID"].ToString();
                    obj.Fk_SectionID = r["Fk_SectionID"].ToString();
                    obj.ClassName = r["ClassName"].ToString();
                    obj.SessionName = r["SessionName"].ToString();
                    obj.SectionName = r["SectionName"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Medium = r["Medium"].ToString();
                    //obj.Status = r["Status"].ToString();


                    list.Add(obj);
                }
                model.listStudent = list;
            }
            else
            {
                TempData["StudentAttendance"] = "No record found.";
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("StudentAttendance")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveAttendance(Student obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string studentid = "";
                string chk = "";
                DataTable dtstudent = new DataTable();
                obj.AttendanceDate = Common.ConvertToSystemDate(obj.AttendanceDate, "dd/MM/yyyy");
                dtstudent.Columns.Add("Pk_StudentID", typeof(string));
                dtstudent.Columns.Add("Status", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["chkSelect_ " + i];
                    if (chk == "on")
                    {
                        obj.Status = "P";

                    }
                    else
                    {

                        try
                        {
                            string str2 = "Dear Parent,Your ward " + Request["hdStudentName_ " + i].ToString() + " is absent on ." + obj.AttendanceDate;
                            BLSMS.SendSMS2(Common.SMSCredential.UserName, Common.SMSCredential.Password, Common.SMSCredential.SenderId, Request["hdMobile_ " + i].ToString(), str2);
                            SendSMS objsms = new SendSMS();
                            string Status = "";
                            DataTable dtSMS = new DataTable();
                            dtSMS.Columns.Add("Name", typeof(string));
                            dtSMS.Columns.Add("Mobile", typeof(string));
                            dtSMS.Columns.Add("Status", typeof(string));

                            if (Request["hdMobile_ " + i].Length < 10)
                            {
                                Status = "Failed";
                            }
                            else if (Request["hdMobile_ " + i].Length > 10)
                            {
                                Status = "Failed";
                            }
                            else
                            {
                                Status = "Send";
                            }

                            dtSMS.Rows.Add(Request["hdStudentName_ " + i].ToString(), Request["hdMobile_ " + i].ToString(), Status);
                            objsms.dtSMS = dtSMS;
                            objsms.AddedBy = Session["Pk_AdminId"].ToString();
                            objsms.SMS = str2;
                            objsms.TotalSMS = "1";
                            objsms.SMSTemplateText = "Student Attendance";
                            DataSet ds1 = objsms.SaveSMSData();
                        }
                        catch { }
                        obj.Status = "A";
                    }

                    studentid = Request["hdStudentID_ " + i].ToString();

                    dtstudent.Rows.Add(studentid, obj.Status);

                }
                obj.dsStudentAttendance = dtstudent;

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.SaveAttendance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["StudentAttendance"] = " Student attendance  is successfully save";
                        FormName = "StudentAttendance";
                        Controller = "Student";
                    }
                    else
                    {
                        TempData["StudentAttendance"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "StudentAttendance";
                        Controller = "Student";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["StudentAttendance"] = ex.Message;
                FormName = "StudentAttendance";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Transfer Student

        public ActionResult TransferStudent(Student model)
        {

            List<SelectListItem> StatusType = Common.TransferType();
            ViewBag.StatusType = StatusType;
            List<Reports> lst = new List<Reports>();

            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;



            #region  class
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> classddl = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            classddl.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        classddl.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.classddl = classddl;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> dropdnSection = new List<SelectListItem>();
            dropdnSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.dropdnSection = dropdnSection;


            #region ddlSession
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet ds1 = obj.GetSession();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[1].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSession = ddlSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            return View(model);

        }

        [HttpPost]
        [ActionName("TransferStudent")]
        [OnAction(ButtonName = "btnsearch")]
        public ActionResult StudentListBy(Student model)
        {

            List<SelectListItem> StatusType = Common.TransferType();
            ViewBag.StatusType = StatusType;
            List<Reports> lst = new List<Reports>();

            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "Select Class", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlSection = new List<SelectListItem>();
            ddlSection.Add(new SelectListItem { Text = "Select Section", Value = "0" });

            DataSet ds5 = model.GetSectionByClass();
            if (ds5 != null && ds5.Tables.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {

                    ddlSection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });

                }
            }

            ViewBag.ddlsection = ddlSection;


            #region  class
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> classddl = new List<SelectListItem>();
                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            classddl.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        classddl.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.classddl = classddl;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> dropdnSection = new List<SelectListItem>();
            dropdnSection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.dropdnSection = dropdnSection;

            #region ddlSession
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlSession = new List<SelectListItem>();
                DataSet ds1 = obj.GetSession();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[1].Rows)
                    {
                        if (count == 0)
                        {
                            ddlSession.Add(new SelectListItem { Text = "--Select Session--", Value = "0" });
                        }
                        ddlSession.Add(new SelectListItem { Text = r["SessionName"].ToString(), Value = r["Pk_SessionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlSession = ddlSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            List<Student> list = new List<Student>();
            try
            {
                model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
                model.Fk_SectionID = model.Fk_SectionID == "0" ? null : model.Fk_SectionID;
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.GetStudentListForTransfer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.Pk_StudentID = r["Pk_StudentID"].ToString();
                        obj.LoginID = r["LoginID"].ToString();
                        obj.SessionName = r["SessionName"].ToString();
                        obj.Fk_ClassID = r["ClassName"].ToString();
                        obj.Fk_SectionID = r["SectionName"].ToString();
                        obj.StudenLoginID = r["LoginID"].ToString();
                        obj.studentName = r["StudentName"].ToString();
                        obj.Medium = r["Medium"].ToString();
                        obj.Mobile = r["Mobile"].ToString();

                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["TransferStudent"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("TransferStudent")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveTransferStudent(Student obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                int count = 0;
                string noofrows = Request["hdRows"].ToString();

                string studentid = "";
                string chk = "";


                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    count = 1;
                    chk = Request["checkBoxId_ " + i];
                    if (chk == "on")
                    {
                        obj.Pk_StudentID = Request["hdStudentID_ " + i].ToString();
                        obj.AddedBy = Session["Pk_AdminId"].ToString();

                        DataSet ds = obj.TransferStudent();
                        if (ds != null && ds.Tables.Count > 0)
                        {

                        }

                    }

                }
                if (count == 0)
                {
                    TempData["TransferStudent"] = "Please select atleast one student.";
                    FormName = "TransferStudent";
                    Controller = "Student";
                }
                else
                {
                    TempData["TransferStudent"] = "Student Transfer Successfully";
                    FormName = "TransferStudent";
                    Controller = "Student";
                }



            }
            catch (Exception ex)
            {
                TempData["TransferStudent"] = ex.Message;
                FormName = "TransferStudent";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region homework

        public ActionResult Homework()
        {
            Student model = new Student();
            #region ddlhelclass
            try
            {
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlclass = new List<SelectListItem>();

                DataSet ds1 = obj.GetClassList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlclass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlclass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlclass = ddlclass;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            List<SelectListItem> ddlsection = new List<SelectListItem>();
            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
            ViewBag.ddlsection = ddlsection;

            List<SelectListItem> ddlSubjectName = new List<SelectListItem>();
            ddlSubjectName.Add(new SelectListItem { Text = "--Select Subject Name--", Value = "0" });
            ViewBag.ddlSubjectName = ddlSubjectName;
            return View(model);
        }

        [HttpPost]
        [ActionName("Homework")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult SaveHomework(IEnumerable<HttpPostedFileBase> StudentFiles, Student obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {

                foreach (var file in StudentFiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.StudentPhoto = "/Homework/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.StudentPhoto)));
                    }
                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.HomeworkDate = string.IsNullOrEmpty(obj.HomeworkDate) ? null : Common.ConvertToSystemDate(obj.HomeworkDate, "dd/MM/yyyy");
                obj.HomeworkBy = "Admin";
                obj.SessionName = Session["SessionId"].ToString();
                DataSet ds = obj.SaveHomework();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        //try
                        //{
                        //    string str2 = BLSchoolDemo.Registration(ds.Tables[0].Rows[0]["StudentName"].ToString(), ds.Tables[0].Rows[0]["LoginID"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                        //    BLSchoolDemo.SendSMS(obj.Mobile, str2);
                        //}
                        //catch { }

                        TempData["Homework"] = "Homework assigned successfully";
                        FormName = "Homework";
                        Controller = "Student";
                    }
                    else
                    {
                        TempData["Homework"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Homework";
                        Controller = "Student";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Homework"] = ex.Message;
                FormName = "Homework";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult GetSubjectNameBySection(string FK_ClassID, string Fk_SectionID)
        {
            Student model = new Student();
            try
            {

                model.Fk_SectionID = Fk_SectionID;
                model.Fk_ClassID = FK_ClassID;

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
                model.ddlSubjectName = ddlSection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HomeworkList(Student model)
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Student Homework List");
            if (!objPermission.IsView)
            {
                return RedirectToAction("NoPermission", "Home");
            }
            if (!objPermission.IsUpdate)
            {
                ViewBag.IsEdit = "none";
            }
            else
            {
                ViewBag.IsEdit = "";
            }
            if (!objPermission.IsDelete)
            {
                ViewBag.IsDelete = "none";
            }
            else
            {
                ViewBag.IsDelete = "";
            }
            #endregion
            List<Student> list = new List<Student>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                //model.Fk_ClassID = model.Fk_ClassID == "0" ? null : model.Fk_ClassID;
                //model.Fk_SectionID = model.Fk_SectionID == "0" ? null : model.Fk_SectionID;
                model.SessionName = Session["SessionId"].ToString();
                DataSet ds = model.HomeworkList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["dt"] = ds.Tables[0];
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Student obj = new Student();
                        obj.HomeWorkID = r["Pk_HomeworkID"].ToString();
                        obj.HomeWorkHTML = r["HomeworkText"].ToString();
                        obj.StudentPhoto = r["HomeworkFile"].ToString();
                        obj.HomeworkDate = r["HomeworkDate"].ToString();
                        obj.ClassName = r["ClassName"].ToString();
                        obj.SubjectID = r["SubjectName"].ToString();
                        obj.SectionName = r["SectionName"].ToString();
                        obj.HomeworkBy = r["HomeworkBy"].ToString();

                        list.Add(obj);

                    }
                    model.listStudent = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult DeleteHomework(string HomeWorkID)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                Student obj = new Student();
                obj.HomeWorkID = HomeWorkID;
                obj.DeletedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.DeleteHomework();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["HomeworkList"] = " Homeworkd Deleted successfully";
                        FormName = "HomeworkList";
                        Controller = "Student";
                    }
                    else
                    {
                        TempData["HomeworkList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "HomeworkList";
                        Controller = "Student";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HomeworkList"] = ex.Message;
                FormName = "HomeworkList";
                Controller = "Student";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion




    }
}