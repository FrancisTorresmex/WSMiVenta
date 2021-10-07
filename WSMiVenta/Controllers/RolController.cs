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
    public class RolController : ControllerBase
    {
        private IRolService _role; //objeto

        public RolController(IRolService role)
        {
            this._role = role;
        }

        [HttpPost]
        public IActionResult Add(RolRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                var role = _role.Add(model);   //llamo a mi metodo add             
                if (role == null) return BadRequest("El nombre del rol ya existe");                
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut]
        public IActionResult Edit(RolRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                _role.Edit(model);
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
