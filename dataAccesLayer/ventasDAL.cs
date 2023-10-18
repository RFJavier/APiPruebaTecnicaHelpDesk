using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entityesLayer;
using Microsoft.EntityFrameworkCore;

namespace dataAccesLayer
{
    public class ventasDAL
    {

        public async Task<int> NewSaleAsync(ventas venta)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                using (var transaction = bdContexto.Database.BeginTransaction())
                {
                    try
                    {
                        int stockVendido = 0;
                        decimal totalSale = 0;

                        foreach (var item in venta.itemssale)
                        {
                            var product = await bdContexto.products.FirstOrDefaultAsync(p => p.idproduct == item.idproduct);
                            if (product != null)
                            {
                                stockVendido += item.quantity;
                                product.quantity = product.quantity - stockVendido;
                                totalSale += item.quantity * product.price;
                            }
                        }
                        venta.stocksale = stockVendido;
                        venta.totalsale = totalSale;
                        venta.datesale = DateTime.Now;
                        bdContexto.ventas.Add(venta);
                        result = await bdContexto.SaveChangesAsync();
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    
                }
            }
        }

        public static async Task<List<ventas>> ObtenerVentas()
        {
            var ventas = new List<ventas>();
            using (var bdContexto = new dbContext())
            {
                ventas = await bdContexto.ventas.ToListAsync();
            }
            return ventas;
        }
    }
}
