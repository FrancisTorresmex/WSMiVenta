using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;
using WSMiVenta.Services;

namespace WSMiVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        public IEntregaService _entrega;

        public EntregaController(IEntregaService entrega)
        {
            this._entrega = entrega;
        }

        //Editar estado de entrega de articulos false(0) = pendeinte, true(1) = entregado (admin)
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult deliveryStatus(EntregaRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                _entrega.deliveryStatus(model);
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
