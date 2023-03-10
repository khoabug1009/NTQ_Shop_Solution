using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            try
            {
                var dao = new UserDao();
                var session = (NTQ_Solution.Common.UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                var user = dao.GetById(session.UserID);
                bool role;
                if (user.Role == 1)
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
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var dao = new UserDao();
                var temp = dao.GetById(id);
                bool role;
                if (temp.Role == 1)
                {
                    role = true;
                }
                else
                {
                    role = false;
                }
                var user = new RigisterModel
                {
                    ID = temp.ID,
                    Email = temp.Email,
                    Username = temp.UserName,
                    Password = temp.PassWord,
                    Role = role
                };
                return View(user);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        [HttpPost]
        public ActionResult Update(RigisterModel model)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    if (model.Password != model.ComfimPassword)
                    {
                        ModelState.AddModelError("", "ComfirmPassword chưa chính xác");
                    }
                    else
                    {
                        var dao = new UserDao();
                        int temp;
                        if (model.Role)
                        {
                            temp = 1;
                        }
                        else
                        {
                            temp = 0;
                        }
                        var user = new User
                        {
                            ID = model.ID,
                            UserName = model.Username,
                            PassWord = model.Password,
                            Role = temp
                        };
                        dao.Update(user);
                        TempData["success"] = "Update succsess";
                        return RedirectToAction("Index", "MyProFile");
                    }

                }
                return View(model);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        public ActionResult Logout()
        {
            try
            {
                
                Session.Clear();

                
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã có lỗi xảy ra, vui lòng thử lại sau " + ex.Message;
                return View("Index");
            }
        }
    }
}