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
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RigisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.GetByCodition(model.Username, model.Email);
                int temp;
                if (model.Password != model.ComfimPassword)
                {
                    ModelState.AddModelError("", "ComfirmPassword chưa chính xác");
                }
                else if (result == 0)

                {
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
                        UserName = model.Username,
                        Email = model.Email,
                        PassWord = Encryptor.MD5Hash( model.Password),
                        Create_at = DateTime.Now,
                        Status = 1,
                        Role = temp
                };
                    dao.Insert(user);
                }
                else if (result == 1)
                {
                    ModelState.AddModelError("", "Username đã tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var dao = new UserDao();
            var temp = dao.GetById(id);
            bool role;
            if (temp.Role == 1)
            {
                role = true;
            }else if(temp.Role == 0)
            {
                role = false;
            }
            var user = new RigisterModel {
                ID = temp.ID,
                Email = temp.Email,
                Username = temp.UserName,
                Password = temp.PassWord,
            };
            return View(user);
        }
        [HttpPost]
        public ActionResult Update(RigisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var user = new User {
                    ID = model.ID,
                    UserName = model.Username,
                    PassWord = Encryptor.MD5Hash(model.Password),
                };
                dao.Update(user);
                return RedirectToAction("Index", "ListUser");
            }
            return View("Update");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            dao.Delete(id);
            return RedirectToAction("Index", "ListUser");
        }

    }
}