using DataLayer.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class HomeUserController : BaseController
    {
        // GET: Admin/HomeUser
        public ActionResult Index(string searchString, bool trending = false, int page = 1, int pageSize = 10)
        {
            try
            {
                var dao = new ProductDAO();
                var model = dao.ListPaging(searchString, page, pageSize);
                if (trending)
                {
                    model = model.Where(x => x.Trending == true).ToPagedList(page, pageSize);
                }
                ViewBag.SearchString = searchString;
                ViewBag.Trending = trending;
                return View(model);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
    }
}