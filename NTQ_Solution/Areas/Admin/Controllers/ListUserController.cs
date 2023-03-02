using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListUserController : Controller
    {
        // GET: Admin/ListUser
        public ActionResult Index(int page = 1, int pageSize = 1)
        {
            var dao = new UserDao();
            var model = dao.ListPaging(page, pageSize);
            return View(model);
        }
    }
}