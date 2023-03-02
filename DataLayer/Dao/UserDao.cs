using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
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
        public IEnumerable<User> ListPaging(int page, int pageSize)
        {
            return db.Users.OrderBy(x => x.Create_at).ToPagedList(page, pageSize);
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
        public void Delete(int id)
        {
            var user = db.Users.SingleOrDefault(x => x.ID == id);
            if(user.Status == 1)
            {
                user.Status = 0;
            }else
            {
                user.Status = 1;
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
