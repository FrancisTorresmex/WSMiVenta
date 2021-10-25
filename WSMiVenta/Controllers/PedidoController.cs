using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;
using Microsoft.EntityFrameworkCore;
using WSMiVenta.Services;
//using System.Linq;

namespace WSMiVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private IPedidoService _orders;

        public PedidoController(IPedidoService orders)
        {
            _orders = orders;
        }

        //Ver pedidos (admin)
        [HttpGet("Administrador")]
        public IActionResult getPedidos(int pag)
        {
          ResponseGeneral response = new ResponseGeneral();

            try
            {
                response.Data = _orders.getOrdersAdmin(pag);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Ok(response);
        }



        //Ver pedidos (usuario)
        [HttpGet("Usuario")]
        public IActionResult getPedio(int id, int pag)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                var lst = _orders.getOrdersUser(id, pag);
                if(lst == null)
                {
                    return BadRequest("Lista vacia");
                }
                response.Data = lst;
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }


}

