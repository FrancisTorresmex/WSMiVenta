using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public interface IUsuarioService
    {
        public void EditName(UpdateName model);

        public void EditPassword(UpdatePassword model);
    }
}
