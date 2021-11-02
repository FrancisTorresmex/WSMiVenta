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
    [Authorize] //solo los que tengan token (inicio se sesón), pueden hacer esta petición
    [ApiController]
    public class VentaController : ControllerBase
    {
        private IVentaService _sale;

        public VentaController(IVentaService sale)
        {
            this._sale = sale;
        }

        //Agregar
        [HttpPost]
        [Authorize(Roles = "normal")]
        public IActionResult Add(VentaRequest model)
        {
            ResponseGeneral response = new ResponseGeneral(); 

            try
            {
                _sale.Add(model);
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
