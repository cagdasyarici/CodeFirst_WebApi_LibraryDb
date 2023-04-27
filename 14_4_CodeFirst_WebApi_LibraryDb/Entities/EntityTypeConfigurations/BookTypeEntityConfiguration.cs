using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations
{
    public class BookTypeEntityConfiguration:BaseEntityTypeConfiguration<BookType>
    {
        public override void Configure(EntityTypeBuilder<BookType> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Book).WithMany(x => x.BookTypes).HasForeignKey(x => x.BookID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Type).WithMany(x => x.BookTypes).HasForeignKey(x => x.TypeID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
