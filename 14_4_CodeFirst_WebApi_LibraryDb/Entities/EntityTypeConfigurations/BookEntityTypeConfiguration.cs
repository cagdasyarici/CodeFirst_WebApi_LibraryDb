using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations
{
    public class BookEntityTypeConfiguration:BaseEntityTypeConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
