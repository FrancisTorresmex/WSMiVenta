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
        
        List<PedidoRequest> searchOrderAdmin(int id);

        List<PedidoRequest> searchPendingAdmin(bool delivery, int pag); //true = entregado, false = pendiente 

        List<PedidoRequest> getOrdersUser(int id, int pag);

        List<PedidoRequest> searchOrderUser(int idUsuario, int idVenta);

        List<PedidoRequest> searchPendingUser(int idUser, bool delivery, int pag); //true = entregado, false = pendiente 
    }
}
