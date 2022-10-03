using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Entities;

namespace WK.Technology.Teste.Infra.Data.Mappings
{
    public class ProductMap : BaseEntityMap<Product>, IEntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("Products");

            builder
                .Property(x => x.Name)
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .IsRequired()
                .HasComment("");

            builder
                .Property(x => x.Description)
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .IsRequired()
                .HasComment("");

            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(b => b.CategoryId);
        }
    }
}
