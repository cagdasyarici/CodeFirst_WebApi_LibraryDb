using _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities
{
    public class LibraryDBContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public LibraryDBContext(DbContextOptions<LibraryDBContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
        }
    }
}
