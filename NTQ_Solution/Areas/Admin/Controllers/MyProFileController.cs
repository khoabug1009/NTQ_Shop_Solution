using DataLayer.Dao;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class MyProFileController : BaseController
    {
        // GET: Admin/MyProFile
        public ActionResult Index()
        {
            var dao = new UserDao();
            var session = (NTQ_Solution.Common.UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
            var user = dao.GetById(session.UserID);
            bool role;
            if(user.Role == 1)
            {
                role = true;
            }
            else
            {
                role = false;
            }
            var result = new RigisterModel
            {
                ID = user.ID,
                Email = user.Email,
                Password = user.PassWord,
                Role = role,
                Username = user.UserName
            };
            return View(result);
        }
    }
}