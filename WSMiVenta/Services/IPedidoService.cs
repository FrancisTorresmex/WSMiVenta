using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public interface IPedidoService
    {
        List<PedidoRequest> getOrdersAdmin(int pag);

        List<PedidoRequest> getOrdersUser(int id, int pag);

        List<PedidoRequest> searchOrderAdmin(int id);

        List<PedidoRequest> searchOrderUser(int idVenta, int idUsuario);
    }
}
