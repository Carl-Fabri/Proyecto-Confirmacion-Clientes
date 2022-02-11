using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class ProductosViewModel
    {
        public int ProductID { get; set; }
        public string ProductoName  { get; set; }
        public int Quantity { get; set; }
        public String UnitPrice { get; set; }
        public String FinalPrice { get; set; }
    }
}