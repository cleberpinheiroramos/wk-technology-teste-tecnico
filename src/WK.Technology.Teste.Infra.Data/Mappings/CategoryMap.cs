using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Entities;

namespace WK.Technology.Teste.Infra.Data.Mappings
{
    public class CategoryMap : BaseEntityMap<Category>, IEntityTypeConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.ToTable("Categorys");

            builder
                .Property(x => x.Name)
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .HasMany(c => c.Products)
                .WithOne(c => c.Category);
        }
    }
}
