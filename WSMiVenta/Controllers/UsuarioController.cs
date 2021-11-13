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
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _user;

        public UsuarioController(IUsuarioService user)
        {
            this._user = user;
        }

        //Modificar nombre de usuario
        [HttpPut("Update/Name")]
        [Authorize(Roles = "normal")]
        public IActionResult UpdateName([FromBody] UpdateName model)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                _user.EditName(model);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;                
            }
            return Ok(response);
        }


        //Modificar contraseña de usuario
        [HttpPut("Update/Password")]
        [Authorize(Roles = "normal")]
        public IActionResult UpdatePassword([FromBody] UpdatePassword model)
        {
            ResponseGeneral response = new ResponseGeneral();

            try
            {
                _user.EditPassword(model);
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
