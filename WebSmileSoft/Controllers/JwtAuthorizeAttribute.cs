using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebSmileSoft.Controllers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _role;

        public CustomAuthorizeAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != _role)
            {
                context.Result = new ForbidResult(); //
            }
        }
    }

}