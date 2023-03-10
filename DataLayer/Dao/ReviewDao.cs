using DataLayer.EF;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ReviewDao
    {
        NTQDBContext db = null;
        public ReviewDao()
        {
            db = new NTQDBContext();
        }
        public bool InsertReview(Review entity)
        {
            db.Reviews.Add(entity);
            db.SaveChanges();
            return true;
        }
        public List<ReviewModel> GetAllReview(int productID)
        {
            try
            {
                var model = (from a in db.Reviews
                             join b in db.Users
                             on a.UserID equals b.ID
                             where  a.ProductID == productID
                             select new
                             {
                                 ID = a.ID,
                                 UserID = a.UserID,
                                 ProductID = a.ProductID,
                                 Title = a.Title,
                                 UserName = b.UserName,
                                 Create_at = a.Create_at
                             }).AsEnumerable().Select(x => new ReviewModel()
                             {
                                 ID = x.ID,
                                 UserID = x.UserID,
                                 ProductsID = x.ProductID,
                                 Title = x.Title,
                                 UserName = x.UserName,
                                 CreateAt = x.Create_at
                                 
                             });
                return model.OrderByDescending(y => y.ID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
