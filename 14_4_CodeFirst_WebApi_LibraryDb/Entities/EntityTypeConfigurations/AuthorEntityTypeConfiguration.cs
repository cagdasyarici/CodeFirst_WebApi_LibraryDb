using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations
{
    public class AuthorEntityTypeConfiguration:BaseEntityTypeConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.FirstName).IsRequired();
            builder.Property(x=>x.LastName).IsRequired();
        }
    }
}
