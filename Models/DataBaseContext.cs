using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Models
{
    public class DataBaseContext: Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
       

        public DataBaseContext():base()
        {

        }
       
        public DataBaseContext
            (Microsoft.EntityFrameworkCore.DbContextOptions<DataBaseContext> options):base(options)
        {
           
        }

        public Microsoft.EntityFrameworkCore.DbSet<Pie> Pies { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Catagory> Catagories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrderDetails> OrderDetails { get; set; }


        protected override void OnConfiguring
                            (Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                   ("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BethanysPieShopDataBase;Data Source=.");
        }

        protected override  void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seed categories
                        
            modelBuilder.Entity<Catagory>()
                .HasData(new Catagory { CatagoryName = Resources.DataDictionary.PieCategory1 });

            modelBuilder.Entity<Catagory>()
                .HasData(new Catagory { CatagoryName = Resources.DataDictionary.PieCategory2 });

            modelBuilder.Entity<Catagory>()
                .HasData(new Catagory { CatagoryName = Resources.DataDictionary.PieCategory3 });
                      
            //RelationShips
                       
            modelBuilder.Entity<Order>()
                .HasOne<User>(currentOrder => currentOrder.User)
                .WithMany(currentUser => currentUser.Orders)
                .HasForeignKey(currentOrder => currentOrder.UserId);

            modelBuilder.Entity<Pie>()
                .HasOne<Catagory>(currentPie => currentPie.PieCatagory)
                .WithMany(currentCatagory => currentCatagory.Pies)
                .HasForeignKey(currentPie => currentPie.CatagoryId);

            modelBuilder.Entity<OrderDetails>()
               .HasOne<Pie>(currentOrderDetails => currentOrderDetails.Pie)
               .WithMany(currentPie => currentPie.OrderDetails)
               .HasForeignKey(currentOrderDetails => currentOrderDetails.PieId);

            modelBuilder.Entity<OrderDetails>()
               .HasOne<Order>(currentOrderDetails => currentOrderDetails.Order)
               .WithMany(currentOrder => currentOrder.OrderDetails)
               .HasForeignKey(currentOrderDetails => currentOrderDetails.OrderId);

            //some Features
            modelBuilder.Entity<User>()
                .HasIndex(current => current.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(current => current.Password)
               .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(current => current.EMail)
               .IsUnique();
        }
    }
}
