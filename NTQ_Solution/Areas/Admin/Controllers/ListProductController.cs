using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListProductController : BaseController
    {
        // GET: Admin/ListProduct
        public ActionResult Index(string searchString,bool trending = false, int page =1, int pageSize = 2)
        {
            try
            {
                var dao = new ProductDAO();
                var model = dao.ListPagingall(searchString, page, pageSize);
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