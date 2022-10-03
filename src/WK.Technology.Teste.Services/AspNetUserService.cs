using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Infra.Extensions;

namespace WK.Technology.Teste.Services
{
    public class AspNetUserService : IAspNetUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public string Name => _accessor.HttpContext!.User.Identity!.Name;

        public AspNetUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid GetUserId()
        {
            if (!IsAutenticated())
            {
                return Guid.Empty;
            }

            return Guid.Parse(_accessor.HttpContext!.User.GetUserId());
        }

        public string GetUserEmail()
        {
            if (!IsAutenticated())
            {
                return "";
            }

            return _accessor.HttpContext!.User.GetUserEmail();
        }

        public bool IsAutenticated()
        {
            return _accessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext!.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            return _accessor.HttpContext!.User.Claims;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
}
