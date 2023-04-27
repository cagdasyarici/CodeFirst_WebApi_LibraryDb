using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations
{
    public class AuthorBookEntityTypeConfiguration:BaseEntityTypeConfiguration<AuthorBook>
    {
        public override void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            base.Configure(builder);
            builder.HasOne(x=>x.Author).
                WithMany(x=>x.AuthorBooks).
                HasForeignKey(x=>x.AuthorID).
                OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Book).
                WithMany(x=>x.AuthorBooks).
                HasForeignKey(x=>x.BookID).
                OnDelete(DeleteBehavior.Restrict);
        }
    }
}
