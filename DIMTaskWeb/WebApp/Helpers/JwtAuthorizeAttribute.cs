using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp.Helpers
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute() : base(typeof(JwtAuthorizeFilter))
        {
        }
    }

    public class JwtAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtToken = context.HttpContext.Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(jwtToken))
            {
                context.Result = new ForbidResult(); // Token yoksa erişimi engelle
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(jwtToken);
            var rolesClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (rolesClaim == null || !rolesClaim.Value.Contains("Admin"))
            {
                context.Result = new ForbidResult("You do not have permission to access this resource."); // Admin rolü yoksa erişimi engelle
            }
        }
    }
}