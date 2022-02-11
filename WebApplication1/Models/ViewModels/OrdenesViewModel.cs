using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class OrdenesViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
    }
}