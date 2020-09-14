namespace MVCBeginner.Controllers
{
    using MVCBeginner.DAL;
    using MVCBeginner.Models;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;

    /// <summary>
    ///     Home Controller
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        ///     GET: Home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string sUser = User.Identity.Name;
            List<UserEdit> userList = null;
            bool bStatus = false;
            if (string.IsNullOrEmpty(sUser))
            {
                return RedirectToAction("Login", "User");
            }
            if (IsAdmin(sUser))
            {
                userList = SelectAllUsers();
                bStatus = true;
            }
            ViewBag.User = sUser;
            ViewBag.Status = bStatus;
            return View(userList);
        }

        /// <summary>
        ///     POST: Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int? Id)
        {
            if (ModelState.IsValid)
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string sUserName = User.Identity.Name;
                if (!string.IsNullOrEmpty(sUserName))
                {
                    using (DB db = new DB())
                    {
                        if (db.DeleteUser(Id))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View();
        }

        /// <summary>
        ///     Method use for Select All Users
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public List<UserEdit> SelectAllUsers()
        {
            List<UserEdit> userList = new List<UserEdit>();
            string sUserName = User.Identity.Name;
            UserEdit userProfile = new UserEdit();
            if (!string.IsNullOrEmpty(sUserName))
            {
                using (DB db = new DB())
                {
                    userList = db.GetAllUsers();
                }
            }
            return userList;
        }

        #region Non Action

        /// <summary>
        ///     Method checks wether user is Admin or not
        /// </summary>
        /// <param name="sEmailId"></param>
        /// <returns></returns>
        [NonAction]
        public bool IsAdmin(string sEmailId)
        {
            bool IsAdmin = false;
            using (DB db = new DB())
            {
                IsAdmin = db.VerifyAdmin(sEmailId);
            }
            return IsAdmin;
        }
        #endregion
    }
}