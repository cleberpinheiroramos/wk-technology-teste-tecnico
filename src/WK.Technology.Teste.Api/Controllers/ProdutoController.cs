using Microsoft.AspNetCore.Mvc;
using WK.Technology.Teste.Infra.Provider;

namespace WK.Technology.Teste.WebApi.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IHttpProdutoService _httpProdutoService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
