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
    [Route("api/[controller]")]
    [Authorize] //solo los registrados pueden acceder
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private IProductoService _product; //asignación de tipo IProductoService

        public ProductoController(IProductoService product)
        {
            this._product = product;
        }

        //Ver todo
        [HttpGet]
        public IActionResult GetProduct(int pag)
        {
            ResponseGeneral response = new ResponseGeneral(); 

            try
            {
                response.Data = _product.Get(pag);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        //Agregar
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(ProductoRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();            

            try
            {
                _product.Add(model);                
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
        [Authorize(Roles = "admin")]
        public IActionResult Edit(ProductoRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();            

            try
            {
                _product.Edit(model);
                response.Success = 1;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            ResponseGeneral response = new ResponseGeneral();            

            try
            {
                _product.Delete(id);
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
