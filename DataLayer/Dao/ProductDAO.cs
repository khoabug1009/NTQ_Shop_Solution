﻿using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ProductDAO
    {
        NTQDBContext db = null;
        public ProductDAO()
        {
            db = new NTQDBContext();
        }
        public int Insert(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return product.ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateView(int id)
        {
            try
            {
                var product = db.Products.SingleOrDefault(x => x.ID == id);
                product.NumberViews = product.NumberViews + 1;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public Product GetByID(int id)
        {
            try
            {
                return db.Products.SingleOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public IEnumerable<Product> ListPaging(string searchString, int page, int pageSize)
        {
            try
            {

                IQueryable<Product> model = db.Products;
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString) || x.Slug.Contains(searchString)).OrderByDescending(x => x.NumberViews);
                    if (model == null)
                    {
                        return null;
                    }
                }
                return model.OrderBy(x => x.Create_at ).Where(x => x.Status == 1).
                    ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public IEnumerable<Product> ListPagingall(string searchString, int page, int pageSize)
        {
            try
            {

                IQueryable<Product> model = db.Products;
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString) || x.Slug.Contains(searchString)).OrderByDescending(x => x.NumberViews);
                    if (model == null)
                    {
                        return null;
                    }
                }
                return model.OrderBy(x => x.Create_at).
                    ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void Update(Product product)
        {
            try
            {
                var products = db.Products.Find(product.ID);
                if (products != null)
                {
                    products.ProductName = product.ProductName;
                    products.Slug = product.Slug;
                    products.Update_at = DateTime.Now;
                    products.Price = product.Price;
                    products.Path = product.Path;
                    products.NumberViews = product.NumberViews;
                    products.Status = product.Status;
                    products.Detail = product.Detail;
                    products.Trending = product.Trending;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public bool Delete(int id)
        {

            using (var context = new NTQDBContext())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    return false;
                }
                product.Status = 0;
                product.Delete_at = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
                }
            }

        }
    }
}
