using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            try
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
                    TempData["success"] = "Create succsess";
                    return RedirectToAction("Index", "ListProduct");
                }
                return View();
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
                var dao = new ProductDAO();
                var temp = dao.GetByID(id);
                bool trend;
                bool status;
                if (temp.Trending == true)
                {
                    trend = true;
                }
                else
                {
                    trend = false;
                }
                if (temp.Status == 1)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

                var product = new ProductModel
                {
                    ProductName = temp.ProductName,
                    Slug = temp.Slug,
                    Detail = temp.Detail,
                    NumberViews = temp.NumberViews,
                    Price = temp.Price,
                    Trending = trend,
                    Path = temp.Path,
                    Status = status                
                };

                return View(product);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        [HttpPost]
        public ActionResult Update(ProductModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new ProductDAO();
                    bool temp;
                    int status;
                    if (model.Trending)
                    {
                        temp = true;
                    }
                    else
                    {
                        temp = false;
                    }
                    if (model.Status)
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }
                    var product = new Product
                    {
                        ID = model.ID,
                        ProductName = model.ProductName,
                        Slug = model.Slug,
                        Detail = model.Detail,
                        NumberViews = model.NumberViews,
                        Trending = temp,
                        Price = model.Price,
                        Path = model.Path,
                        Status = status
                    };
                    dao.Update(product);
                    TempData["success"] = "Update succsee";
                    return RedirectToAction("Index", "ListProduct");
                }
                return View("Update");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        public ActionResult Delete(int id)
        {
            try
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
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            try
            {
                var dao = new ProductDAO();
                var temp = dao.GetByID(id);
                var product = new ProductModel
                {
                    ProductName = temp.ProductName,
                    Slug = temp.Slug,
                    Detail = temp.Detail,
                    NumberViews = temp.NumberViews,
                    Price = temp.Price,
                    Path = temp.Path
                };
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if (sessionUser != null) { ViewBag.UserID = sessionUser.UserID; }
                ViewBag.ListReview = new ReviewDao().GetAllReview(id);
                dao.UpdateView(id);
                return View(product);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
            }
        }
        [HttpPost]
        public JsonResult AddNewReview(int productid, int userid, string title)
        {
            try
            {
                var dao = new ReviewDao();
                Review review = new Review();
                review.UserID = userid;
                review.ProductID = productid;
                review.Title = title;
                review.Status = 0;
                review.Create_at = DateTime.Now;
                bool addReview = dao.InsertReview(review);
                if (addReview)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch
            {
                return Json(new { status = false });
            }
        }
        [HttpPost]
        public ActionResult AddToWishlist(int productID)
        {
            var dao = new WishListDao();
            dao.Insert();
            return View();
        }
    }
}