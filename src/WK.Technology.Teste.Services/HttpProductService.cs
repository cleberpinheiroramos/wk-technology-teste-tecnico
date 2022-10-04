//using Arch.EntityFrameworkCore.UnitOfWork.Collections;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using WK.Technology.Teste.Domain.ViewModel.Product;
//using WK.Technology.Teste.Infra.Config;

//namespace WK.Technology.Teste.Infra.Provider
//{
//    public class HttpProdutoService : IHttpProdutoService
//    {
//        private readonly AppSettings _appSettings;
//        private readonly HttpClient _httpClient;

//        public HttpProdutoService(IOptions<AppSettings> config, HttpClient httpClient)
//        {
//            _appSettings = config.Value;
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri(_appSettings.ApiUrl);
//        }

//        public async Task<IPagedList<ProductViewModel>> GetProductsPaged()
//        {

//            var response = await _httpClient.GetAsync($"product");

//            var value = await response.Content.ReadAsStringAsync();

//            var result = JsonConvert.DeserializeObject<IPagedList<ProductViewModel>,>(value);

//            if (!response.IsSuccessStatusCode)
//                throw new Exception("Something is wrong");
//            return result;
//        }

//        public async Task<ProdutoViewModel> GetProductById(int id)
//        {
//            HttpClient httpClient = new HttpClient();

//            var response = await httpClient.GetAsync($"{_apiProdutoSettings.Url}produto/{id}");

//            var value = await response.Content.ReadAsStringAsync();

//            var result = JsonConvert.DeserializeObject<ProdutoViewModel>(value);

//            if (!response.IsSuccessStatusCode)
//                throw new Exception("Something is wrong");

//            return result;
//        }

//        public async Task<ProdutoViewModel> CreateProduct(ProdutoViewModel produto)
//        {
//            HttpClient httpClient = new HttpClient();

//            var request = new HttpRequestMessage
//            {
//                Method = HttpMethod.Post,
//                RequestUri = new Uri($"{_apiProdutoSettings.Url}produto/create"),
//                Content = new StringContent(JsonConvert.SerializeObject(produto), null, "application/json")
//            };

//            var response = httpClient.SendAsync(request).Result;

//            var value = await response.Content.ReadAsStringAsync();

//            var result = JsonConvert.DeserializeObject<ProdutoViewModel>(value);
//            if (!response.IsSuccessStatusCode)
//                throw new Exception("Something is wrong");
//            return result;
//        }

//        public async Task<ProdutoViewModel> UpdateProduct(ProdutoViewModel produto)
//        {
//            HttpClient httpClient = new HttpClient();

//            var request = new HttpRequestMessage
//            {
//                Method = HttpMethod.Put,
//                RequestUri = new Uri($"{_apiProdutoSettings.Url}produto/update"),
//                Content = new StringContent(JsonConvert.SerializeObject(produto), null, "application/json")
//            };

//            var response = httpClient.SendAsync(request).Result;

//            var value = await response.Content.ReadAsStringAsync();

//            var result = JsonConvert.DeserializeObject<ProdutoViewModel>(value);
//            if (!response.IsSuccessStatusCode)
//                throw new Exception("Something is wrong");
//            return result;
//        }

//        public async Task<List<ProdutoViewModel>> DeleteProduct(long produtoId)
//        {
//            HttpClient httpClient = new HttpClient();

//            var response = await httpClient.DeleteAsync($"{_apiProdutoSettings.Url}produto/delete/{produtoId}");

//            var value = await response.Content.ReadAsStringAsync();

//            var result = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(value);

//            if (!response.IsSuccessStatusCode)
//                throw new Exception("Something is wrong");

//            return result;
//        }


//    }
//}
