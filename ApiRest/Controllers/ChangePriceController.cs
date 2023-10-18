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
using dataAccesLayer;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;

namespace ApiRest.Controllers
{
    [Route("api/ChangePrice/")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChangePriceController : Controller
    {
        private productsBL ProductsBL = new productsBL();

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public ChangePriceController(productsBL _ProductsBL, JwtAuthenticationService pAuthService, ILogger<productsController> logger)
        {
            ProductsBL = _ProductsBL;
            authService = pAuthService;
       
        }
        //************************************************

        [HttpPut("{idproduct}")]
        public async Task<ActionResult> Put(int idproduct, [FromBody] products pproducts)
        {

                if (pproducts.idproduct == idproduct)
                _logger.Warn("Se cambio precio del producto con el Nombre: " + pproducts.productname.ToString() + " con el Codigo: " + pproducts.code);
                await ProductsBL.ModifyPriceAsync(pproducts);
                return Ok();
     
        }
    }
}
