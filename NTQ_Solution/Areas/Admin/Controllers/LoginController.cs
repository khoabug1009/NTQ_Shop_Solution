﻿using DataLayer.Dao;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Email, Encryptor.MD5Hash(model.Password));
                if(result==1)
                {
                    var user = dao.GetByEmail(model.Email);
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.UserName = user.UserName;
                    userSession.Email = user.Email;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    if(user.Role == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }else
                    {
                        return RedirectToAction("Index", "HomeUser");
                    }
                    
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài sản không tồn tại, sai email");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài sản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View("Index");

        }
    }
}