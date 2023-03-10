using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataLayer.EF
{
    public partial class NTQDBContext : DbContext
    {
        public NTQDBContext()
            : base("name=NTQDBContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
