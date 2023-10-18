using ApiRest.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using entityesLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using bussineslayer;
using entityesLayer.interfaces;
using bussinesLayer;
using dataAccesLayer;

namespace ApiRest.Controllers
{
    [Route("api/sales/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SaleController : Controller
    {
        private ventasBL VentasBL = new ventasBL();

        [HttpGet]
        public async Task<IEnumerable<ventas>> Get()
        {
            return await VentasBL.ObtainAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ventas products)
        {
            try
            {
                await VentasBL.NewSale(products);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
