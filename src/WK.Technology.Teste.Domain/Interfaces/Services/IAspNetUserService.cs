using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WK.Technology.Teste.Domain.Interfaces.Services
{
    public interface IAspNetUserService
    {
        string Name
        {
            get;
        }

        Guid GetUserId();

        string GetUserEmail();

        bool IsAutenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetUserClaims();

        HttpContext GetHttpContext();
    }
}
