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
    public class UserDao
    {
        NTQDBContext db = null;
        public UserDao()
        {
            db=new NTQDBContext();
        }
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        /// <summary>
        /// Tìm kiếm User theo Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
        public User GetByUsername(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public IEnumerable<User> ListPaging(string searchString, bool roleFilter,  int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Email.Contains(searchString));
                if (model == null)
                {
                    return null;
                }
            }
            if (roleFilter )
            {
                model = model.Where(x => x.Role == 1);
            }

            /* if (statusFilter.HasValue)
             {
                 model = model.Where(u => u.Status == statusFilter);               
             }
             */
            return model.OrderBy(x => x.Create_at).ToPagedList(page, pageSize);
        }
        public void Update(User entity)
        {
            var user = db.Users.Find(entity.ID);
            if(user != null)
            {
                user.UserName = entity.UserName;
                user.PassWord = entity.PassWord;
                user.Update_at = DateTime.Now;
                db.SaveChanges();
            }
        }
        public bool Delete(int id)
        {
            using (var context = new NTQDBContext())
            {
                var user = context.Users.Find(id);
                if (user == null)
                {
                    return false;
                }
                user.Status = 0;
                user.Delete_at = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
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
        public int GetByCodition(string Username, string Email)
        {
            var checkUser =  db.Users.SingleOrDefault(x => x.UserName == Username);
            var checkEmail = db.Users.SingleOrDefault(x => x.Email == Email);
            if(checkUser == null && checkEmail == null)
            {
                return 0;
            }else if(checkUser != null && checkEmail == null)
            {
                return 1;
            }else
            {
                return -1;
            }

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string email,string password)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == 0)
                {
                    return -1;
                }
                else
                {
                    if(result.PassWord==password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

    }
}
