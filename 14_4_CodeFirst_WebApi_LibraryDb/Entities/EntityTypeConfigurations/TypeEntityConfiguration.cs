using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations
{
    public class TypeEntityConfiguration:BaseEntityTypeConfiguration<Type>
    {
        public override void Configure(EntityTypeBuilder<Type> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
