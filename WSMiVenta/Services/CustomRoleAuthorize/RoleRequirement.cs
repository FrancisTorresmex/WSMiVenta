using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;

namespace WSMiVenta.Services.CustomRoleAuthorize
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            this.Role = role;
        }

        public string Role { get; private set; }
    }

    public class CustomRoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {                
        protected override async Task HandleRequirementAsync (AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                //db.Usuarios.Join(db.Rols)

                //if (x.Equals("admin"));
            }
        }
    }
}
