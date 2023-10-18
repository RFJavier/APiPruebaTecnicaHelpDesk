using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityesLayer
{
    public class ventas
    {
        [Key]
        public int idventas { get; set; }

        public int stocksale { get; set; }
        
        public decimal totalsale { get; set; }

        [DataType(DataType.Date)]   
        public DateTime datesale { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
        //[NotMapped]
        //public string itemssale { get; set; }

        [NotMapped]
        public List<products> itemssale { get; set; }
    }
}
