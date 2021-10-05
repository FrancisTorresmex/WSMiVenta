using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public interface IClienteService
    {        
        public void Add(ClienteRequest model);

        public void Edit(ClienteRequest model);

        public void Delete(int id);
    }
}
