using ApiRest.Auth;
using bussinesLayer;
using entityesLayer;
using entityesLayer.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using bussineslayer;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace ApiRest.Controllers
{
    [Route("api/products/")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class productsController : Controller
    {
        private productsBL ProductsBL = new productsBL();
      
        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public productsController(productsBL _ProductsBL, JwtAuthenticationService pAuthService)
        {

            authService = pAuthService;
        }
        //************************************************

        [HttpGet]
        public async Task<IEnumerable<products>> Get()
        {
            return await ProductsBL.ObtainAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<products> Get(int id)
        {
            products products = new products();        
            products.idproduct = id;
            return await ProductsBL.ObtainByIdAsync(products);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] products products)
        {
            await ProductsBL.CreateAsync(products);
            return Ok();
        }

        [HttpPost("search")]
        public async Task<List<products>> search([FromBody] SearchProduct pproducts)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strproducts = JsonSerializer.Serialize(pproducts);
            products products = JsonSerializer.Deserialize<products>(strproducts, option);
            var Products = await ProductsBL.SearchAsync(products);
            Products.ForEach(s => s.ProductCategories = null); // Evitar la redundacia de datos
            return Products;

        }

        [HttpPut("{idproduct}")]
        public async Task<ActionResult> Put(int idproduct, [FromBody] products pproducts)
        {
            try
            {
                if (pproducts.idproduct == idproduct)
                await ProductsBL.ModifyAsync(pproducts);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }     
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                products products = new products();
                products.idproduct = id;
                await ProductsBL.DeleteAsync(products);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
