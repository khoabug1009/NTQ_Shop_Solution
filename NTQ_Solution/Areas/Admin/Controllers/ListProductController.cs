using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListProductController : BaseController
    {
        // GET: Admin/ListProduct
        public ActionResult Index(string searchString,bool trending = false, int page =1, int pageSize = 10)
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
    }
}