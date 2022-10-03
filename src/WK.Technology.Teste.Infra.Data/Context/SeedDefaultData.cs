using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Infra.Data.FakeData.Category;
using WK.Technology.Teste.Infra.Data.FakeData.Product;

namespace WK.Technology.Teste.Infra.Data.Context
{
    public static class SeedDefaultData
    {
        public static void SeedCategory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new CategoryFaker().Generate(10));
        }

        public static void SeedProduct(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new ProductFaker().Generate(10));
        }
    }
}
