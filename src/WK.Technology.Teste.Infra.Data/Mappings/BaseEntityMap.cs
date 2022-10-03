using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Base;

namespace WK.Technology.Teste.Infra.Data.Mappings
{
    public abstract class BaseEntityMap<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<long>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder
                .HasKey(x => x.Id)
                .HasName($"PK_{typeof(TEntity).Name}");

            builder
                .Property(usuario => usuario.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasComment("Identificador exclusivo e universal para a entidade");

            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(30)
                .HasColumnType("varchar(36)")
                .IsRequired()
                .HasComment("Registro do usuário que cadastrou a entidade");

            builder
                .Property(usuario => usuario.CreatedOn)
                .HasColumnType("timestamp")
                .IsRequired()
                .HasComment("Registro de data/hora de quando o usuário cadastrou a entidade");

            builder
                .Property(x => x.UpdatedBy)
                .HasMaxLength(30)
                .HasColumnType("varchar(36)")
                .HasComment("Registro do usuário que realizou a última alteração na entidade");

            builder
                .Property(usuario => usuario.UpdatedOn)
                .HasColumnType("timestamp")
                .HasComment("Registro de data/hora da última atualização da entidade");
        }
    }
}
