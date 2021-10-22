using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Tools
{
    //public class CustomRoleAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    //{
    //    private string _role;

    //    public CustomRoleAuthorizeAttribute(string role)
    //    {
    //        this._role = role;
    //    }

    //    //Método obligatorio de IAsyncAuthorizationFilter
    //    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    //    {
    //        var service = (IAuthorizationService)context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService));

    //        var roleRequirement = new RoleRequirement();

    //    }
    //}
}
