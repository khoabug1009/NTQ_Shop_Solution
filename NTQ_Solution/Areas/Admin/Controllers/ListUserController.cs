using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListUserController : BaseController
    {
        // GET: Admin/ListUser
        public ActionResult Index(string searchString, bool roleFilter = false , bool statustrue = false, bool statusfalse = false, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListPaging(searchString, roleFilter, page, pageSize);
            if (roleFilter)
            {
                model = model.Where(x => x.Role == 1).ToPagedList(page, pageSize);
            }
            if (statustrue)
            {
                model = model.Where(x => x.Status == 1).ToPagedList(page, pageSize);
            }
            if (statusfalse)
            {
                model = model.Where(x => x.Status == 0).ToPagedList(page, pageSize);
            }
            ViewBag.SearchString = searchString;
            ViewBag.RoleFilter = roleFilter;
            ViewBag.StatusTrue = statustrue;
            ViewBag.StatusFalse = statusfalse;
            return View(model);
        }
    }
}