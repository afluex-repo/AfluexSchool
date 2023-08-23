using AfluexSchool.Filter;
using AfluexSchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace AfluexSchool.Controllers
{
    public class HostelController : AdminBaseController
    {
        #region Hostel Entry

        public ActionResult HostelEntry(string Pk_HostelId)
        {
            Hostel objd = new Hostel();
            try
            {
                #region ddlBindHostel
                Hostel obj = new Hostel();
                int count = 0;
                List<SelectListItem> ddlHostelType = new List<SelectListItem>();
                DataSet ds = obj.HostelTypeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlHostelType.Add(new SelectListItem { Text = "--Select HostelType--", Value = "0" });
                        }
                        ddlHostelType.Add(new SelectListItem { Text = r["TypeName"].ToString(), Value = r["Pk_HostelTypeId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlHostelType = ddlHostelType;
                #endregion
            }
            catch (Exception)
            {
            }


            if (Pk_HostelId != null)
            {
                #region ddlBindHostel
                Hostel obj = new Hostel();
                int count = 0;
                List<SelectListItem> ddlHostelType = new List<SelectListItem>();
                DataSet ds = obj.HostelTypeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlHostelType.Add(new SelectListItem { Text = "--Select HostelType--", Value = "0" });
                        }
                        ddlHostelType.Add(new SelectListItem { Text = r["TypeName"].ToString(), Value = r["Pk_HostelTypeId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlHostelType = ddlHostelType;
                #endregion

                try
                {
                    objd.Pk_HostelId = Pk_HostelId;
                    DataSet dsd = objd.GetHostelList();
                    if (dsd != null && dsd.Tables.Count > 0 && dsd.Tables[0].Rows.Count > 0)
                    {
                        objd.Pk_HostelId = dsd.Tables[0].Rows[0]["Pk_HostelId"].ToString();
                        objd.Pk_HostelTypeId = dsd.Tables[0].Rows[0]["Fk_HostelTYpeId"].ToString();
                        objd.HostelName = dsd.Tables[0].Rows[0]["HostelName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["HostelFloorMaster"] = ex.Message;
                }
                return View(objd);
            }



            else
            {
                return View(objd);
            }


        }


        [HttpPost]
        [ActionName("HostelEntry")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveHostel(Hostel obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveHostel();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "HostelEntry";
                        Controller = "Hostel";
                        TempData["HostelEntry"] = "Hostel Saved Successfully";
                    }
                    else
                    {
                        FormName = "GetHostelList";
                        Controller = "Hostel";
                        TempData["HostelEntryError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FormName = "GetHostelList";
                Controller = "Hostel";
                TempData["HostelEntryError"] = ex.Message;
            }
            return RedirectToAction("HostelEntry");
        }

        public ActionResult HostelList()
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Hostel List");
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
            Hostel model = new Hostel();
            List<Hostel> lst1 = new List<Hostel>();

            DataSet ds = model.GetHostelList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Hostel obj = new Hostel();
                    obj.Pk_HostelId = r["Pk_HostelId"].ToString();
                    obj.TypeName = r["TypeName"].ToString();
                    obj.HostelName = r["HostelName"].ToString();
                    Session["Pk_HostelId"] = null;
                    lst1.Add(obj);
                }
                model.lstHostel = lst1;
            }
            return View(model);
        }

        public ActionResult DeleteHostel(string Pk_HostelId)
        {

            try
            {
                Hostel model = new Hostel();
                model.Pk_HostelId = Pk_HostelId;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeletedHostel();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["HostelList"] = "  Deleted SuccessFully!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["HostelList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelList"] = ex.Message;
            }
            return RedirectToAction("HostelList");
        }


        [HttpPost]
        [ActionName("HostelEntry")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateHostel(string Pk_HostelId, string Pk_HostelTypeId, string HostelName)
        {
            Hostel obj = new Hostel();
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Pk_HostelId = Pk_HostelId;
                obj.Pk_HostelTypeId = Pk_HostelTypeId;
                obj.HostelName = HostelName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateHostel();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "HostelList";
                        Controller = "Hostel";
                        TempData["HostelList"] = "  Updated Successfully";
                    }
                    else
                    {
                        Session["Pk_HostelId"] = Pk_HostelId;
                        FormName = "HostelEntry";
                        Controller = "Hostel";
                        TempData["HostelList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                FormName = "HostelEntry";
                Controller = "Hostel";
                TempData["HostelList"] = ex.Message;
            }

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region Hostel Floor
        public ActionResult HostelFloor(string Pk_HostelFloorId)
        {
            Hostel obj1 = new Hostel();
            try
            {
                #region ddlBindHostel
                Hostel obj = new Hostel();
                int count = 0;
                List<SelectListItem> ddlHostelType = new List<SelectListItem>();
                DataSet ds = obj.HostelTypeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlHostelType.Add(new SelectListItem { Text = "--Select Hostel Type--", Value = "0" });
                        }
                        ddlHostelType.Add(new SelectListItem { Text = r["TypeName"].ToString(), Value = r["Pk_HostelTypeId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlHostelType = ddlHostelType;
                #endregion
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                ddlHostelName.Add(new SelectListItem { Text = "Select Hostel Name", Value = "0" });
                ViewBag.ddlHostelName = ddlHostelName;


            }
            catch (Exception)
            { }


            if (Pk_HostelFloorId != null)
            {
                #region ddlBindHostelName
                Hostel obja = new Hostel();
                int countg = 0;
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                DataSet ds4 = obja.GetHostelList();

                if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds4.Tables[0].Rows)
                    {
                        if (countg == 0)
                        {
                            ddlHostelName.Add(new SelectListItem { Text = "--Select Hostel Name--", Value = "0" });
                        }
                        ddlHostelName.Add(new SelectListItem { Text = r["HostelName"].ToString(), Value = r["Pk_HostelId"].ToString() });
                        countg = countg + 1;
                    }
                }

                ViewBag.ddlHostelName = ddlHostelName;
                #endregion



                try
                {
                    obj1.Pk_HostelFloorId = Pk_HostelFloorId;
                    DataSet ds = obj1.GetHostelFloorList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj1.Pk_HostelFloorId = ds.Tables[0].Rows[0]["Pk_HostelFloorId"].ToString();
                        obj1.Pk_HostelTypeId = ds.Tables[0].Rows[0]["Fk_HostelTypeId"].ToString();
                        obj1.Pk_HostelId = ds.Tables[0].Rows[0]["Fk_HostelId"].ToString();
                        obj1.FloorName = ds.Tables[0].Rows[0]["FloorName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData[" HostelFloorMaster"] = ex.Message;
                }
                return View(obj1);
            }
            else
            {
                return View(obj1);
            }

        }

        public ActionResult GetHostelNameByHostelType(string Pk_HostelTypeId)
        {
            Hostel model = new Hostel();
            try
            {

                model.Pk_HostelTypeId = Pk_HostelTypeId;

                List<SelectListItem> ddlHostelName = new List<SelectListItem>();

                DataSet ds = model.GetHostelList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlHostelName.Add(new SelectListItem { Text = r["HostelName"].ToString(), Value = r["Pk_HostelId"].ToString() });

                    }
                }

                //ViewBag.ddlHostelName = ddlHostelName;
                model.lstHostelName = ddlHostelName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("HostelFloor")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveHostelFloor(Hostel obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveHostelFloor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["HostelFloor"] = "Save Successfully";
                    }
                    else
                    {
                        TempData["HostelFloorError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelFloorError"] = ex.Message;
            }
            return RedirectToAction("HostelFloor");
        }

        public ActionResult GetHostelFloorList()
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Floor List");
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
            Hostel model = new Hostel();
            List<Hostel> lst1 = new List<Hostel>();

            DataSet ds = model.GetHostelFloorList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Hostel obj = new Hostel();
                    obj.Pk_HostelFloorId = r["Pk_HostelFloorId"].ToString();
                    obj.TypeName = r["TypeName"].ToString();
                    obj.HostelName = r["HostelName"].ToString();
                    obj.FloorName = r["FloorName"].ToString();
                    Session["Pk_HostelFloorId"] = null;
                    lst1.Add(obj);
                }
                model.lstHostel = lst1;
            }
            return View(model);
        }
        public ActionResult DeleteHostelFloor(string Pk_HostelFloorId)
        {
            try
            {
                Hostel model = new Hostel();
                model.Pk_HostelFloorId = Pk_HostelFloorId;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteHostelFloor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GetHostelFloorList"] = "Delete SuccessFully!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["HostelFloorError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelFloorError"] = ex.Message;
            }
            return RedirectToAction("GetHostelFloorList");
        }

        [HttpPost]
        [ActionName("HostelFloor")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateHostelFloor(string Pk_HostelFloorId, string Pk_HostelId, string Pk_HostelTypeId, string FloorName)
        {
            string FormName = "";
            string Controller = "";
            Hostel obj = new Hostel();
            try
            {
                obj.Pk_HostelFloorId = Pk_HostelFloorId;
                obj.Pk_HostelId = Pk_HostelId;
                obj.Pk_HostelTypeId = Pk_HostelTypeId;
                obj.FloorName = FloorName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateHostelFloor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "GetHostelFloorList";
                        Controller = "Hostel";
                        TempData["GetHostelFloorList"] = "Update Successfully!";
                    }
                    else
                    {
                        Session["Pk_HostelFloorId"] = Pk_HostelFloorId;
                        FormName = "HostelFloor";
                        Controller = "Hostel";
                        TempData["GetHostelFloorList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["GetHostelFloorList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region Hostel Room 
        public ActionResult HostelRoom(string Pk_HostelRoomId)
        {
            Hostel objroom = new Hostel();
            try
            {
                #region ddlBindHostel
                Hostel obj = new Hostel();
                int count = 0;
                List<SelectListItem> ddlHostelType = new List<SelectListItem>();
                DataSet ds = obj.HostelTypeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlHostelType.Add(new SelectListItem { Text = "--Select Hostel Type--", Value = "0" });
                        }
                        ddlHostelType.Add(new SelectListItem { Text = r["TypeName"].ToString(), Value = r["Pk_HostelTypeId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlHostelType = ddlHostelType;
                #endregion
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                ddlHostelName.Add(new SelectListItem { Text = "Select Hostel Name", Value = "0" });
                ViewBag.ddlHostelName = ddlHostelName;

                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();
                ddlHostelFloor.Add(new SelectListItem { Text = "Select Floor Number", Value = "0" });
                ViewBag.ddlHostelFloor = ddlHostelFloor;


            }
            catch (Exception)
            { }


            if (Pk_HostelRoomId != null)
            {
                #region ddlBindHostelName
                Hostel obja = new Hostel();
                int countg = 0;
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                DataSet ds4 = obja.GetHostelList();

                if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds4.Tables[0].Rows)
                    {
                        if (countg == 0)
                        {
                            ddlHostelName.Add(new SelectListItem { Text = "--Select Hostel Name--", Value = "0" });
                        }
                        ddlHostelName.Add(new SelectListItem { Text = r["HostelName"].ToString(), Value = r["Pk_HostelId"].ToString() });
                        countg = countg + 1;
                    }
                }

                ViewBag.ddlHostelName = ddlHostelName;
                #endregion


                #region ddlBindHostelFloor
                Hostel objs = new Hostel();
                int countf = 0;
                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();
                DataSet ds2 = objs.GetHostelFloorList();

                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (countf == 0)
                        {
                            ddlHostelFloor.Add(new SelectListItem { Text = "--Select Floor No.--", Value = "0" });
                        }
                        ddlHostelFloor.Add(new SelectListItem { Text = r["FloorName"].ToString(), Value = r["Pk_HostelFloorId"].ToString() });
                        countf = countf + 1;
                    }
                }

                ViewBag.ddlHostelFloor = ddlHostelFloor;
                #endregion


                try
                {
                    objroom.Pk_HostelRoomId = Pk_HostelRoomId;
                    DataSet ds = objroom.GetHostelRoomList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objroom.Pk_HostelRoomId = ds.Tables[0].Rows[0]["Pk_HostelRoomId"].ToString();
                        objroom.TypeName = ds.Tables[0].Rows[0]["Fk_HostelMasterId"].ToString();
                        objroom.HostelName = ds.Tables[0].Rows[0]["Fk_HostelId"].ToString();
                        objroom.FloorName = ds.Tables[0].Rows[0]["Fk_HostelFloorId"].ToString();
                        objroom.FromRoom = ds.Tables[0].Rows[0]["FromRoom"].ToString();
                        // obj.ToRoom = ds.Tables[0].Rows[0]["ToRoom"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData[" HostelRoomError"] = ex.Message;
                }
                return View(objroom);
            }
            else
            {
                return View(objroom);
            }

        }

        [HttpPost]
        [ActionName("HostelRoom")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveHostelRoom(Hostel obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveHostelRoom();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["HostelRoom"] = "Room Save Successfully";
                    }
                    else
                    {
                        TempData["HostelRoom"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelRoom"] = ex.Message;
            }
            return RedirectToAction("HostelRoom");
        }

        public ActionResult GetHostelRoomList()
        {
            #region Set Permission
            Permissions objPermission = new Permissions("Room List");
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
            Hostel model = new Hostel();
            List<Hostel> lst = new List<Hostel>();

            DataSet ds = model.GetHostelRoomList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Hostel obj = new Hostel();
                    obj.Pk_HostelRoomId = r["Pk_HostelRoomId"].ToString();
                    obj.TypeName = r["TypeName"].ToString();
                    obj.HostelName = r["HostelName"].ToString();
                    obj.FloorName = r["FloorName"].ToString();
                    obj.FromRoom = r["FromRoom"].ToString();
                    obj.ToRoom = r["ToRoom"].ToString();
                    Session["Pk_HostelRoomId"] = null;
                    lst.Add(obj);
                }
                model.lstHostel = lst;
            }
            return View(model);
        }
        public ActionResult DeleteHostelRoom(string Pk_HostelRoomId)
        {
            try
            {
                Hostel model = new Hostel();
                model.Pk_HostelRoomId = Pk_HostelRoomId;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteHostelRoom();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GetHostelRoomList"] = "Room Deleted Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["GetHostelRoomList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["GetHostelRoomList"] = ex.Message;
            }
            return RedirectToAction("GetHostelRoomList");
        }
        [HttpPost]
        [ActionName("HostelRoom")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateHostelRoom(string Pk_HostelRoomId, string TypeName, string HostelName, string FloorName, string FromRoom, string ToRoom)
        {
            string FormName = "";
            string Controller = "";
            Hostel obj = new Hostel();
            try
            {
                obj.Pk_HostelRoomId = Pk_HostelRoomId;
                obj.TypeName = TypeName;
                obj.HostelName = HostelName;
                obj.FloorName = FloorName;
                obj.FromRoom = FromRoom;
                // obj.ToRoom = ToRoom;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateHostelRoom();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "GetHostelRoomList";
                        Controller = "HostelRoomMaster";
                        TempData["HostelRoom"] = "Update Successfully!";
                    }
                    else
                    {
                        Session["Pk_HostelRoomId"] = Pk_HostelRoomId;
                        FormName = "HostelRoom";
                        Controller = "HostelRoomMaster";
                        TempData["HostelRoomError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                TempData["HostelRoomError"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }



        public ActionResult GetFloorNameByHostelName(string Pk_HostelTypeId, string Pk_HostelId)
        {
            Hostel model = new Hostel();
            try
            {

                model.Pk_HostelTypeId = Pk_HostelTypeId;
                model.Pk_HostelId = Pk_HostelId;
                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();

                DataSet ds = model.GetHostelFloorList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlHostelFloor.Add(new SelectListItem { Text = r["FloorName"].ToString(), Value = r["Pk_HostelFloorId"].ToString() });

                    }
                }

                model.lstFloor = ddlHostelFloor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Bedroom
        public ActionResult BedCapacity(string Pk_BedId)
        {
            Hostel obj = new Hostel();
            try
            {
                #region ddlBindHostel
                Hostel obj1 = new Hostel();
                int count1 = 0;
                List<SelectListItem> ddlHostel = new List<SelectListItem>();
                DataSet ds1 = obj1.HostelTypeList();

                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlHostel.Add(new SelectListItem { Text = "--Select Hostel Type--", Value = "0" });
                        }
                        ddlHostel.Add(new SelectListItem { Text = r["TypeName"].ToString(), Value = r["Pk_HostelTypeId"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlHostel = ddlHostel;
                #endregion
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                ddlHostelName.Add(new SelectListItem { Text = "--Select Hostel Name--", Value = "0" });
                ViewBag.ddlHostelName = ddlHostelName;

                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();
                ddlHostelFloor.Add(new SelectListItem { Text = "--Select Floor Number--", Value = "0" });
                ViewBag.ddlHostelFloor = ddlHostelFloor;

                List<SelectListItem> ddlHostelroom = new List<SelectListItem>();
                ddlHostelroom.Add(new SelectListItem { Text = "--Select Hostel Room Number--", Value = "0" });
                ViewBag.ddlHostelroom = ddlHostelroom;
            }
            catch (Exception)
            { }


            if (Pk_BedId != null)
            {
                #region ddlBindHostelName
                Hostel obja = new Hostel();
                int countg = 0;
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                DataSet ds4 = obja.GetHostelList();

                if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds4.Tables[0].Rows)
                    {
                        if (countg == 0)
                        {
                            ddlHostelName.Add(new SelectListItem { Text = "--Select Hostel Name--", Value = "0" });
                        }
                        ddlHostelName.Add(new SelectListItem { Text = r["HostelName"].ToString(), Value = r["Pk_HostelId"].ToString() });
                        countg = countg + 1;
                    }
                }

                ViewBag.ddlHostelName = ddlHostelName;
                #endregion


                #region ddlBindHostelFloor
                Hostel objs = new Hostel();
                int countf = 0;
                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();
                DataSet ds2 = objs.GetHostelFloorList();

                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (countf == 0)
                        {
                            ddlHostelFloor.Add(new SelectListItem { Text = "--Select Floor No.--", Value = "0" });
                        }
                        ddlHostelFloor.Add(new SelectListItem { Text = r["FloorName"].ToString(), Value = r["Pk_HostelFloorId"].ToString() });
                        countf = countf + 1;
                    }
                }

                ViewBag.ddlHostelFloor = ddlHostelFloor;
                #endregion

                #region ddlBindHostelRoom
                Hostel obj2 = new Hostel();
                int count2 = 0;
                List<SelectListItem> ddlHostelroom = new List<SelectListItem>();
                DataSet ds3 = objs.GetHostelRoomList();

                if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds3.Tables[0].Rows)
                    {
                        if (count2 == 0)
                        {
                            ddlHostelroom.Add(new SelectListItem { Text = "--Select Room--", Value = "0" });
                        }
                        ddlHostelroom.Add(new SelectListItem { Text = r["FromRoom"].ToString(), Value = r["Pk_HostelRoomId"].ToString() });
                        count2 = count2 + 1;
                    }
                }

                ViewBag.ddlHostelroom = ddlHostelroom;
                #endregion


                try
                {
                    obj.Pk_BedId = Pk_BedId;
                    DataSet ds = obj.BedCapacityList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_BedId = ds.Tables[0].Rows[0]["Pk_BedRoomId"].ToString();
                        obj.Pk_HostelTypeId = ds.Tables[0].Rows[0]["Fk_HostelTypeId"].ToString();
                        obj.Pk_HostelId = ds.Tables[0].Rows[0]["Fk_HostelId"].ToString();
                        obj.Pk_HostelFloorId = ds.Tables[0].Rows[0]["Fk_HostelFloorId"].ToString();
                        obj.Pk_HostelRoomId = ds.Tables[0].Rows[0]["Fk_HostelRoomId"].ToString();
                        obj.BedCapacity = ds.Tables[0].Rows[0]["BedCapacity"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData[" BedCapacityError"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(obj);
            }

        }

        public ActionResult RoomNumberByFloor(string Pk_HostelFloorId)
        {

            Hostel model = new Hostel();
            try
            {

                model.Pk_HostelFloorId = Pk_HostelFloorId;

                List<SelectListItem> ddlHostelRoom = new List<SelectListItem>();

                DataSet ds = model.GetRoomNumberByFloorName();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlHostelRoom.Add(new SelectListItem { Text = r["FromRoom"].ToString(), Value = r["Pk_HostelRoomId"].ToString() });

                    }
                }

                //ViewBag.ddlHostelName = ddlHostelName;
                model.ddlHostelRoom = ddlHostelRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("BedCapacity")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveBedCapacity(Hostel obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveBedCapacity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BedCapacity"] = "Save Successfully";
                    }
                    else
                    {
                        TempData["BedCapacity"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BedCapacity"] = ex.Message;
            }
            return RedirectToAction("BedCapacity");
        }

        public ActionResult BedCapacityList()
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Bed Capacity List");
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
            Hostel model = new Hostel();
            List<Hostel> BedCpacity = new List<Hostel>();

            DataSet ds = model.BedCapacityList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Hostel obj = new Hostel();
                    obj.Pk_BedId = r["Pk_BedRoomId"].ToString();
                    obj.TypeName = r["TypeName"].ToString();
                    obj.HostelName = r["HostelName"].ToString();
                    obj.FloorName = r["FloorName"].ToString();
                    obj.FromRoom = r["FromRoom"].ToString();
                    obj.BedCapacity = r["BedCapacity"].ToString();
                    Session["Pk_BedRoomId"] = null;

                    BedCpacity.Add(obj);
                }
                model.lstHostel = BedCpacity;
            }
            return View(model);

        }

        public ActionResult DeleteBedCapacity(string Pk_BedId)
        {

            try
            {
                Hostel model = new Hostel();
                model.Pk_BedId = Pk_BedId;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteBedCapacity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BedCapacityList"] = "Deleted SuccessFully!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BedCapacityList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BedCapacityList"] = ex.Message;
            }
            return RedirectToAction("BedCapacityList");
        }

        [ActionName("BedCapacity")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateHostelAllotment(string Pk_BedId, string Pk_HostelTypeId, string Pk_HostelId, string Pk_HostelFloorId, string Pk_HostelRoomId, string BedCapacity)
        {
            string FormName = "";
            string Controller = "";
            Hostel obj = new Hostel();
            try
            {
                obj.Pk_BedId = Pk_BedId;
                obj.Pk_HostelTypeId = Pk_HostelTypeId;
                obj.Pk_HostelId = Pk_HostelId;
                obj.Pk_HostelFloorId = Pk_HostelFloorId;
                obj.Pk_HostelRoomId = Pk_HostelRoomId;
                obj.BedCapacity = BedCapacity;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateBedCapacity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "BedCapacityList";
                        Controller = "Hostel";
                        TempData["BedCapacityList"] = "Update Successfully!";
                    }
                    else
                    {
                        Session["Pk_BedRoomId"] = Pk_BedId;
                        FormName = "BedCapacity";
                        Controller = "Hostel";
                        TempData["BedCapacityList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BedCapacityList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region Room Allotment

        public ActionResult HostelAllotment(string Pk_HostelAllotmentId)
        {
            Hostel objp = new Hostel();
            try
            {
                #region ddlBindClass
                Student objclass = new Student();
                int countClass = 0;
                List<SelectListItem> ddlClass = new List<SelectListItem>();
                DataSet dsClass = objclass.GetClassList();

                if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsClass.Tables[0].Rows)
                    {
                        if (countClass == 0)
                        {
                            ddlClass.Add(new SelectListItem { Text = "--Select Class--", Value = "0" });
                        }
                        ddlClass.Add(new SelectListItem { Text = r["ClassName"].ToString(), Value = r["PK_ClassID"].ToString() });
                        countClass = countClass + 1;
                    }
                }

                ViewBag.ddlClass = ddlClass;
                #endregion
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                ViewBag.ddlsection = ddlsection;


                List<SelectListItem> ddlStudent = new List<SelectListItem>();
                ddlStudent.Add(new SelectListItem { Text = "--Select Student--", Value = "0" });
                ViewBag.ddlStudent = ddlStudent;


                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                ddlHostelName.Add(new SelectListItem { Text = "--Select Hostel--", Value = "0" });
                ViewBag.ddlHostelName = ddlHostelName;



                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();
                ddlHostelFloor.Add(new SelectListItem { Text = "--Select Floor--", Value = "0" });
                ViewBag.ddlHostelFloor = ddlHostelFloor;

                List<SelectListItem> ddlHostelRoom = new List<SelectListItem>();
                ddlHostelRoom.Add(new SelectListItem { Text = "--Select Room--", Value = "0" });
                ViewBag.ddlHostelRoom = ddlHostelRoom;

                #region ddlBindHostel
                Hostel obj1 = new Hostel();
                int count1 = 0;
                List<SelectListItem> ddlHostel = new List<SelectListItem>();
                DataSet ds1 = obj1.HostelTypeList();

                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlHostel.Add(new SelectListItem { Text = "--Select Hostel Type--", Value = "0" });
                        }
                        ddlHostel.Add(new SelectListItem { Text = r["TypeName"].ToString(), Value = r["Pk_HostelTypeId"].ToString() });
                        count1 = count1 + 1;
                    }
                }

                ViewBag.ddlHostel = ddlHostel;
                #endregion


            }
            catch (Exception)
            { }

            if (Pk_HostelAllotmentId != null)
            {
                #region ddlBindSection
                Student objSection = new Student();
                int countSection = 0;
                List<SelectListItem> ddlsection = new List<SelectListItem>();
                DataSet dsSection = objSection.GetSectionList();

                if (dsSection != null && dsSection.Tables.Count > 0 && dsSection.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsSection.Tables[0].Rows)
                    {
                        if (countSection == 0)
                        {
                            ddlsection.Add(new SelectListItem { Text = "--Select Section--", Value = "0" });
                        }
                        ddlsection.Add(new SelectListItem { Text = r["SectionName"].ToString(), Value = r["PK_SectionID"].ToString() });
                        countSection = countSection + 1;
                    }
                }

                ViewBag.ddlsection = ddlsection;
                #endregion

                #region ddlBindStudent
                Student obj = new Student();
                int count = 0;
                List<SelectListItem> ddlStudent = new List<SelectListItem>();
                DataSet dsStudent = obj.GetStudentList();

                if (dsStudent != null && dsStudent.Tables.Count > 0 && dsStudent.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsStudent.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlStudent.Add(new SelectListItem { Text = "--Select StudentName--", Value = "0" });
                        }
                        ddlStudent.Add(new SelectListItem { Text = r["StudentName"].ToString(), Value = r["Pk_StudentID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlStudent = ddlStudent;
                #endregion
                #region ddlBindHostelName

                Hostel obja = new Hostel();
                int countg = 0;
                List<SelectListItem> ddlHostelName = new List<SelectListItem>();
                DataSet ds4 = obja.GetHostelList();

                if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds4.Tables[0].Rows)
                    {
                        if (countg == 0)
                        {
                            ddlHostelName.Add(new SelectListItem { Text = "--Select Hostel Name--", Value = "0" });
                        }
                        ddlHostelName.Add(new SelectListItem { Text = r["HostelName"].ToString(), Value = r["Pk_HostelId"].ToString() });
                        countg = countg + 1;
                    }
                }

                ViewBag.ddlHostelName = ddlHostelName;
                #endregion

                #region ddlBindHostelFloor
                Hostel objs = new Hostel();
                int countf = 0;
                List<SelectListItem> ddlHostelFloor = new List<SelectListItem>();
                DataSet ds2 = objs.GetHostelFloorList();

                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (countf == 0)
                        {
                            ddlHostelFloor.Add(new SelectListItem { Text = "--Select Floor No.--", Value = "0" });
                        }
                        ddlHostelFloor.Add(new SelectListItem { Text = r["FloorName"].ToString(), Value = r["Pk_HostelFloorId"].ToString() });
                        countf = countf + 1;
                    }
                }

                ViewBag.ddlHostelFloor = ddlHostelFloor;
                #endregion

                #region ddlBindHostelRoom
                Hostel obj2 = new Hostel();
                int count2 = 0;
                List<SelectListItem> ddlHostelroom = new List<SelectListItem>();

                DataSet ds3 = obj2.GetHostelRoomList();

                if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds3.Tables[0].Rows)
                    {
                        if (count2 == 0)
                        {
                            ddlHostelroom.Add(new SelectListItem { Text = "--Select Room No.--", Value = "0" });
                        }
                        ddlHostelroom.Add(new SelectListItem { Text = r["FromRoom"].ToString(), Value = r["Pk_HostelRoomId"].ToString() });
                        count2 = count2 + 1;
                    }
                }

                ViewBag.ddlHostelroom = ddlHostelroom;
                #endregion
              
                try
                {
                    objp.Pk_HostelAllotmentId = Pk_HostelAllotmentId;
                    objp.Session = Session["SessionId"].ToString();
                    DataSet ds = objp.HostelAllotmentList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.Pk_HostelAllotmentId = ds.Tables[0].Rows[0]["Pk_HostelAllotmentId"].ToString();
                        objp.Fk_ClassID = ds.Tables[0].Rows[0]["Fk_ClassId"].ToString();
                        objp.Fk_SectionID = ds.Tables[0].Rows[0]["Fk_SectionId"].ToString();
                        objp.Pk_StudentID = ds.Tables[0].Rows[0]["Fk_StudentId"].ToString();
                        objp.Pk_HostelTypeId = ds.Tables[0].Rows[0]["Fk_HostelTypeId"].ToString();
                        objp.Pk_HostelId = ds.Tables[0].Rows[0]["Fk_HostelId"].ToString();
                        objp.Pk_HostelFloorId = ds.Tables[0].Rows[0]["Fk_HostelFloorId"].ToString();
                        objp.Pk_HostelRoomId = ds.Tables[0].Rows[0]["Fk_HostelRoomId"].ToString();
                        objp.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        objp.IsMonthly =Convert.ToBoolean( ds.Tables[0].Rows[0]["IsMonthly"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    TempData[" HostelAllotmentError"] = ex.Message;
                }
                return View(objp);
            }
            else
            {
                return View(objp);
            }

        }

        [HttpPost]
        [ActionName("HostelAllotment")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveHostelAllotment(Hostel obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Session = Session["SessionId"].ToString();
                DataSet ds = obj.SaveHostelAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["HostelAllotment"] = "Save Successfully";
                    }
                    else
                    {
                        TempData["HostelAllotment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelAllotment"] = ex.Message;
            }
            return RedirectToAction("HostelAllotment");
        }

        public ActionResult HostelAllotmentList()
        {

            #region Set Permission
            Permissions objPermission = new Permissions("Hostel Room Allotment List");
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
            Hostel model = new Hostel();
            List<Hostel> lstAllotment = new List<Hostel>();
            model.Session = Session["SessionId"].ToString();
            DataSet ds = model.HostelAllotmentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Hostel obj = new Hostel();
                    obj.Pk_HostelAllotmentId = r["Pk_HostelAllotmentId"].ToString();
                    obj.Fk_ClassID = r["ClassName"].ToString();
                    obj.Fk_SectionID = r["SectionName"].ToString();
                    obj.Pk_StudentID = r["StudentName"].ToString();
                    obj.TypeName = r["TypeName"].ToString();
                    obj.HostelName = r["HostelName"].ToString();
                    obj.FloorName = r["FloorName"].ToString();
                    obj.FromRoom = r["FromRoom"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    Session["Pk_HostelAllotmentId"] = null;

                    lstAllotment.Add(obj);
                }
                model.lstHostel = lstAllotment;
            }
            return View(model);
        }

        public ActionResult DeleleHostelAllotment(string Pk_HostelAllotmentId)
        {
            try
            {
                Hostel model = new Hostel();
                model.Pk_HostelAllotmentId = Pk_HostelAllotmentId;
                model.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteHostelAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["HostelAllotmentList"] = "Deleted SuccessFully!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["HostelAllotmentList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelAllotmentList"] = ex.Message;
            }
            return RedirectToAction("HostelAllotmentList");
        }
        [HttpPost]
        [ActionName("HostelAllotment")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateHostelAllotment(string Pk_HostelAllotmentId, string Fk_ClassID, string Fk_SectionID, string Pk_StudentID, string Pk_HostelTypeId, string Pk_HostelId, string Pk_HostelFloorId, string Pk_HostelRoomId,string Amount)
        {
            string FormName = "";
            string Controller = "";
            Hostel obj = new Hostel();
            try
            {
                obj.Pk_HostelAllotmentId = Pk_HostelAllotmentId;
                obj.Fk_ClassID = Fk_ClassID;
                obj.Fk_SectionID = Fk_SectionID;
                obj.Pk_StudentID = Pk_StudentID;
                obj.Pk_HostelTypeId = Pk_HostelTypeId;
                obj.Pk_HostelId = Pk_HostelId;
                obj.Pk_HostelFloorId = Pk_HostelFloorId;
                obj.Pk_HostelRoomId = Pk_HostelRoomId;
                obj.Amount = Amount;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateHostelAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "HostelAllotmentList";
                        Controller = "Hostel";
                        TempData["HostelAllotmentList"] = "Update Successfully!";
                    }
                    else
                    {
                        Session["Pk_HostelAllotmentId"] = Pk_HostelAllotmentId;
                        FormName = "HostelAllotment";
                        Controller = "Hostel";
                        TempData["HostelAllotmentList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["HostelAllotmentList"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetStudentBySection(string Fk_ClassID, string Fk_SectionID)
        {

            Hostel model = new Hostel();
            try
            {
                model.Fk_ClassID = Fk_ClassID;
                model.Fk_SectionID = Fk_SectionID;
                List<SelectListItem> ddlstudent = new List<SelectListItem>();
                model.Session = Session["SessionId"].ToString();
                DataSet ds = model.GetStudentBySection();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlstudent.Add(new SelectListItem { Text = r["StudentName"].ToString(), Value = r["Pk_StudentID"].ToString() });

                    }
                }
  
                model.ddlstudent = ddlstudent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}