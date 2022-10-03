using AutoMapper;
using WK.Technology.Teste.Domain.Mapper;

namespace WK.Technology.Teste.Tests
{
    public class MappingFixture
    {
        public IMapper Mapper { get; }
        public IConfigurationProvider ConfigurationProvider { get; }

        public MappingFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<CategoryProfile>();
            });
            Mapper = ConfigurationProvider.CreateMapper();
        }

        [Fact(DisplayName = "Valid Configuration")]
        [Trait("MapperConfiguration", "MapperFixture")]
        public void Should_Have_Valid_Configuration()
        {
            ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    [CollectionDefinition("MappingCollection")]
    public class MappingCollection : ICollectionFixture<MappingFixture>
    {

    }
}
