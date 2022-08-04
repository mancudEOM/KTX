using Microsoft.EntityFrameworkCore;
namespace KTX.Models




{
    public class KtxDbContext : DbContext
    {
        public KtxDbContext(DbContextOptions<KtxDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Khởi tạo quan hệ 1-n cho User và RelativeUser ( half )
            modelBuilder.Entity<User>().HasMany(x => x.RelativeUsers).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().Navigation(x => x.RelativeUsers).UsePropertyAccessMode(PropertyAccessMode.Property);


            //Khởi tạo quan hệ 1-1 cho User và HistoryRent ( half )
            modelBuilder.Entity<User>().HasOne(t => t.HistoryRent).WithOne(p => p.User).HasForeignKey<HistoryRent>(t => t.UserId);
            modelBuilder.Entity<User>().Navigation(c => c.HistoryRent).UsePropertyAccessMode(PropertyAccessMode.Property);


            //Khởi tạo quan hệ 1 - n cho HistoryRent và Rent(half )
            modelBuilder.Entity<HistoryRent>().HasMany(x => x.Rents).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HistoryRent>().Navigation(x => x.Rents).UsePropertyAccessMode(PropertyAccessMode.Property);


            //Khởi tạo quan hệ 1-n cho Room và User ( half )
            modelBuilder.Entity<Room>().HasMany(x => x.Users).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Room>().Navigation(x => x.Users).UsePropertyAccessMode(PropertyAccessMode.Property);

            //Khởi tạo quan hệ 1 - n cho HistoryBill và Bill(half )
            modelBuilder.Entity<HistoryBill>().HasMany(x => x.Bills).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HistoryBill>().Navigation(x => x.Bills).UsePropertyAccessMode(PropertyAccessMode.Property);


            //Khởi tạo quan hệ 1-1 cho Room và HistoryBill ( half )
            modelBuilder.Entity<Room>().HasOne(t => t.HistoryBill).WithOne(p => p.Room).HasForeignKey<HistoryBill>(t => t.RoomId);
            modelBuilder.Entity<Room>().Navigation(c => c.HistoryBill).UsePropertyAccessMode(PropertyAccessMode.Property);

            //Khởi tạo quan hệ 1-n cho ListPost và Post ( half )

            modelBuilder.Entity<ListPost>().HasMany(x => x.Posts).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ListPost>().Navigation(x => x.Posts).UsePropertyAccessMode(PropertyAccessMode.Property);


        }

        public DbSet<User>? Users { get; set; } = null!;
        public DbSet<Room>? Rooms { get; set; } = null!;
        public DbSet<RelativeUser>? RelativeUsers { get; set; } = null!;
        public DbSet<HistoryRent>? HistoryRents { get; set; } = null!;
        public DbSet<Bill>? Bills { get; set; } = null!;
        public DbSet<HistoryBill>? HistoryBills { get; set; } = null!;
        public DbSet<Rent>? Rents { get; set; } = null!;
        public DbSet<Post>? Posts { get; set; } = null!;
        public DbSet<ListPost>? ListPosts { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
    }
}