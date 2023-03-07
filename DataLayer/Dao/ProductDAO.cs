using DataLayer.EF;
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
        public int Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public Product GetByID(int id)
        {
            return db.Products.SingleOrDefault(x => x.ID == id);
        }
        public IEnumerable<Product> ListPaging(string searchString, int page, int pageSize)
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
            return model.OrderBy(x => x.Create_at).ToPagedList(page, pageSize);
        }
        public void Update(Product entity)
        {
            var products = db.Products.Find(entity.ID);
            if (products != null)
            {
                products.ProductName = entity.ProductName;
                products.Slug = entity.Slug;
                products.Update_at = DateTime.Now;
                products.Price = entity.Price;
                products.Path = entity.Path;
                products.NumberViews = entity.NumberViews;
                products.Status = entity.Status;
                products.Detail = entity.Detail;
                db.SaveChanges();
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
