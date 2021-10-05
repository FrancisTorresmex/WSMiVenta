﻿using Microsoft.AspNetCore.Http;
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
        public IActionResult GetProduct()
        {
            ResponseGeneral response = new ResponseGeneral(); 

            try
            {
                using (MiVentaContext db = new MiVentaContext())
                {
                    var lst = db.Productos.OrderBy(i => i.Id).ToList();
                    response.Success = 1;
                    response.Data = lst;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        //Agregar
        [HttpPost]
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