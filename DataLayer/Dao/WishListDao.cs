using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class WishListDao
    {
        NTQDBContext db = null;
        public WishListDao()
        {
            db = new NTQDBContext();
        }
        public void Insert()
        {

        }
    }
}
