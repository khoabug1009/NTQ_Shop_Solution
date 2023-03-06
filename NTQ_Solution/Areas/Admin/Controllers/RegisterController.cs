using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Admin/Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(RigisterModel rigisterModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.GetByCodition(rigisterModel.Username, rigisterModel.Email);
                if(result == 0)
                {
                    var user = new User { 
                        Email = rigisterModel.Email,
                        PassWord = rigisterModel.Password,
                        UserName = rigisterModel.Username,
                        Create_at = DateTime.Now,
                        Role = 0,
                        Status = 1
                    };
                    dao.Insert(user);
                    return RedirectToAction("Index", "Login");
                }
                else if(result == 1)
                {
                    ModelState.AddModelError("", "Username đã tồn tại");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
            }
            return View("Index");
        }
    }
}