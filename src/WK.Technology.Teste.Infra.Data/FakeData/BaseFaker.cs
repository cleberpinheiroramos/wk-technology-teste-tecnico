using Bogus;

namespace WK.Technology.Teste.Infra.Data.FakeData
{
    public class BaseFaker<T> : Faker<T> where T : class
    {
        public BaseFaker()
        {
            Locale = "pt_BR";
        }
    }
}
