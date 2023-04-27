using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities.EntityTypeConfigurations
{
    public class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x=>x.ID).IsRequired().UseIdentityColumn();
        }
    }
}
