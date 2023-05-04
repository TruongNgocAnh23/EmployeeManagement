using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyApiNetCore6.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> opt): base(opt)
        {

        }

        #region DbSet
        public DbSet<Book>? Books { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<RegularPayment>? RegularPayment { get; set; }
        public DbSet<Employee>? Employee { get; set; }
        public DbSet<TimeSheet>? Timesheet { get; set; }
        #endregion
    }
}
