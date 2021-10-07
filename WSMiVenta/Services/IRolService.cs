using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public interface IRolService
    {
        Rol Add(RolRequest model);

        public void Edit(RolRequest model);
    }
}
