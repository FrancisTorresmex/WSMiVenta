using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;
using WSMiVenta.Services;

namespace WSMiVenta.Controllers
{
    [ApiController]
    [Authorize] //solo los registrados pueden acceder
    [Route("api/[controller]")]    
    public class ClienteController : ControllerBase
    {
        private IClienteService _client; //asignación de tipo IClienteService

        public ClienteController(IClienteService client) 
        {
            this._client = client;
        }

        [HttpGet]
        public IActionResult GetClient()
        {
            ResponseGeneral response = new ResponseGeneral(); //objeto de respuesta gral
            
            try
            {
                using (MiVentaContext db = new MiVentaContext())
                {
                    var lst = db.Clientes.OrderByDescending(i => i.Id).ToList(); //enlistamos la tabla cliente del id mas alto al mas bajo
                    response.Success = 1; //le indicamos que salio bien todo
                    response.Data = lst; 
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                Console.WriteLine(ex.InnerException);
            }

            return Ok(response);
        }

        //añadir
        [HttpPost]
        public IActionResult Add(ClienteRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();            

            try
            {
                _client.Add(model);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Ok(response);
        }


        //Editar
        [HttpPut]
        public IActionResult Edit(ClienteRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();            

            try
            {
                _client.Edit(model);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }


        //eliminar
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ResponseGeneral response = new ResponseGeneral();            

            try
            {
                _client.Delete(id);
                response.Success = 1; //si se elimina ok
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;                
            }

            return Ok(response);
        }
    }
}
