using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Infra.Data.Mappings;

namespace WK.Technology.Teste.Infra.Data.Context
{
    public class ContextWkTechnology : DbContext
    {
        private readonly IAspNetUserService _aspNetUserService;

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }

        public ContextWkTechnology(DbContextOptions<ContextWkTechnology> options) : base(options)
        {
            _aspNetUserService = this.GetService<IAspNetUserService>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

            var loggerFactory = this.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("app");

            try
            {
                //modelBuilder.SeedDefaultRoles();
                //modelBuilder.SeedSuperAdmin();

#if DEBUG
                modelBuilder.SeedCategory();
                modelBuilder.SeedProduct();
#endif

#if RELEASE

#endif

                logger.LogInformation("Finished Seeding Default Data");
                logger.LogInformation("Application Starting");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred seeding the DB");
            }

            base.OnModelCreating(modelBuilder);
        }

        public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedOn") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedOn").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedOn").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedBy") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedBy").CurrentValue = _aspNetUserService.GetUserId().ToString().Substring(0,29);
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedBy").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedOn") != null))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedOn").CurrentValue = DateTime.Now;
                    entry.Property("UpdatedOn").IsModified = true;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedBy") != null))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedBy").CurrentValue = _aspNetUserService.GetUserId().ToString().Substring(0, 29);
                    entry.Property("UpdatedBy").IsModified = true;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);
        }
    }
}
