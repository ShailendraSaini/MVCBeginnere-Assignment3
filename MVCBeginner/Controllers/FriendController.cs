using MVCBeginner.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Web.Mvc;

namespace MVCBeginner.Controllers
{
    public class FriendController : Controller
    {
        private string _sConnString = string.Empty;
        private MySqlConnection _mySqlConnection = null;

        public FriendController()
        {
            _sConnString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            _mySqlConnection = new MySqlConnection(_sConnString);
            _mySqlConnection.Open();
        }
        // GET: Friend
        public ActionResult Index()
        {
            List<Friend> friendList = new List<Friend>();
            Friend friend = new Friend();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("GetAllFriends", _mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                friendList.Add(new Friend
                                {
                                    FriendID = Convert.ToInt32(sdr["FriendID"]),
                                    FriendName = sdr["FriendName"].ToString(),
                                    Place = sdr["Place"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return View(friendList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Friend friend)
        {
            bool Status = false;
            string sMessage = "";
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("IsFriendExists", _mySqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", friend.FriendID);
                        object nVal = cmd.ExecuteScalar();
                        if (Convert.ToInt32(nVal) > 0)
                        {
                            ModelState.AddModelError(Properties.Resources.FriendExists, Properties.Resources.FriendExistsErrMsg);
                            return View(friend);
                        }
                    }
                    using (MySqlCommand cmd = new MySqlCommand("InsertFriend", _mySqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", friend.FriendID);
                        cmd.Parameters.AddWithValue("@FName", friend.FriendName);
                        cmd.Parameters.AddWithValue("@PlaceVal", friend.Place);
                        int nVal = cmd.ExecuteNonQuery();
                        if (nVal > 0)
                        {
                            sMessage = Properties.Resources.FriendAdded;
                            Status = true;
                        }
                        else
                        {
                            sMessage = Properties.Resources.FriendFailed;
                        }
                    }
                }
                catch (Exception) {
                }
            }
            else
            {
                sMessage = Properties.Resources.InvalidRequest;
            }
            ViewBag.Message = sMessage;
            ViewBag.Status = Status;
            return View(friend);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            Friend friend = new Friend();
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (MySqlCommand cmd = new MySqlCommand("GetFriendInfo", _mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                friend.FriendID = Convert.ToInt32(sdr["FriendID"]);
                                friend.FriendName = sdr["FriendName"].ToString();
                                friend.Place = sdr["Place"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return View(friend);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection)
        {
            string sMessage = string.Empty;
            bool bStatus = false;
            Friend friend = new Friend();
            if (ModelState.IsValid)
            {
                friend.FriendID = Convert.ToInt32(formCollection["FriendId"]);
                friend.FriendName = formCollection["FriendName"];
                friend.Place = formCollection["Place"];
                #region Update to Database
                using (MySqlCommand cmd = new MySqlCommand("UpdateFriendInfo", _mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", friend.FriendID);
                    cmd.Parameters.AddWithValue("@FName", friend.FriendName);
                    cmd.Parameters.AddWithValue("@PlaceVal", friend.Place);

                    int nVal = cmd.ExecuteNonQuery();
                    if (nVal > 0)
                    {
                        sMessage = Properties.Resources.FriendUpdated;
                        bStatus = true;
                    }
                    else
                    {
                        sMessage = Properties.Resources.FriendUpdateFailed;
                    }
                }
                #endregion
                ViewBag.Message = sMessage;
                ViewBag.Status = bStatus;
            }
            else
            {
                sMessage = Properties.Resources.InvalidRequest;
            }
            return View(friend);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int? Id)
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (MySqlCommand cmd = new MySqlCommand("DeleteFriend", _mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    int nVal = cmd.ExecuteNonQuery();
                    if (nVal > 0)
                        return RedirectToAction("Index", "Friend");
                }
            }
            catch (Exception) { }
            return View();
        }
    }
}