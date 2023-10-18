using dataAccesLayer;
using entityesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bussinesLayer
{
    public class ventasBL
    {
        private ventasDAL ventaDAL;

        public ventasBL()
        {
            ventaDAL = new ventasDAL();
        }

        public async Task<List<ventas>> ObtainAllAsync()
        {
            return await ventasDAL.ObtenerVentas();
        }

        //public List<ventas> ObtenerVentas()
        //{
        //    return ventaDAL.ObtenerVentas();
        //}

        public async Task<int> NewSale(ventas pventas)
        {
            // Aplicar reglas de negocio si es necesario
            return await ventaDAL.NewSaleAsync(pventas);
        }
    }
}

