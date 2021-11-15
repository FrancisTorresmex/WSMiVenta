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
using Microsoft.AspNetCore.Authorization;
//using System.Linq;

namespace WSMiVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //debe tener sesión iniciada
    public class PedidoController : ControllerBase
    {
        private IPedidoService _orders;

        public PedidoController(IPedidoService orders)
        {
            _orders = orders;
        }

        //Ver pedidos (admin)
        [HttpGet("Administrador")]
        [Authorize(Roles = "admin")]
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


        [HttpGet("Administrador/Search/Orders")]
        [Authorize(Roles = "admin")]
        public IActionResult searchAdmin(int id)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                response.Data = _orders.searchOrderAdmin(id);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        //Buscar entregas segun estado entregado o pendiente (admin)
        [HttpGet("Administrador/Search/Orders/Delivery")]
        [Authorize(Roles = "admin")]
        public IActionResult searchDeliveryAdmin(bool delivery, int pag)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                response.Data = _orders.searchPendingAdmin(delivery, pag);
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
        [Authorize(Roles = "normal")]
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


        [HttpGet("Usuario/Search/Orders")]
        [Authorize(Roles = "normal")]
        public IActionResult searchUser(int idUsuario, int idVenta)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                response.Data = _orders.searchOrderUser(idUsuario, idVenta);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        //Buscar entregas segun estado entregado o pendiente (usuario normal)
        [HttpGet("Usuario/Search/Orders/Delivery")]
        [Authorize(Roles = "normal")]
        public IActionResult searchDeliveryUser(int idUser, bool delivery, int pag)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                response.Data = _orders.searchPendingUser(idUser, delivery, pag);
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

