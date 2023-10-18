using ApiRest.Auth;
using bussinesLayer;
using entityesLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest.Controllers
{
    [Route("api/productviewanonimous/")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [AllowAnonymous]
    public class ProductViewAnonimousController : Controller
    {
        private productsBL ProductsBL = new productsBL();

        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public ProductViewAnonimousController( JwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }

        [HttpGet]
        public async Task<IEnumerable<products>> Get()
        {

            return await ProductsBL.ObtainAllAsync();
        }
    }
}
