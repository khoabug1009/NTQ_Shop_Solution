using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
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
        public ActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                bool trend;
                if (model.Trending)
                {
                    trend = true;
                }
                else
                {
                    trend = false;
                }
                var product = new Product
                {
                    ProductName = model.ProductName,
                    Slug = model.Slug,
                    Detail = model.Detail,
                    Trending = trend,
                    Status = 1,
                    NumberViews = 0,
                    Price = model.Price,
                    Create_at = DateTime.Now,
                    Path = model.Path
                    };
                    dao.Insert(product);
                

            }
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var dao = new ProductDAO();
            var temp = dao.GetByID(id);
            bool trend;
            if(temp.Trending == true)
            {
                trend = true;
            }
            else
            {
                trend = false;
            }
            var product = new ProductModel {
                ProductName = temp.ProductName,
                Slug = temp.Slug,
                Detail = temp.Detail,
                NumberViews = temp.NumberViews,
                Price = temp.Price,
                Trending = trend,
                Path = temp.Path
            };
           
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                bool temp;
                if (model.Trending)
                {
                    temp = true;
                }
                else
                {
                    temp = false;
                }
                var product = new Product
                {
                    ID = model.ID,
                    ProductName = model.ProductName,
                    Slug = model.Slug,
                    Detail = model.Detail,
                    Trending = temp,
                    Price = model.Price,
                    Path = model.Path
                };
                dao.Update(product);
                return RedirectToAction("Index", "ListProduct");
            }
            return View("Update");
        }
        public ActionResult Delete(int id)
        {
            ProductDAO productDao = new ProductDAO();
            bool success = productDao.Delete(id)
;
            if (success)
            {
                TempData["DeleteUserMessage"] = "Xoá thành công";
            }
            else
            {
                TempData["DeleteUserMessage"] = "Xoá không thành công";
            }
            return RedirectToAction("Index", "ListProduct");
        }
    }
}