using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class OrdersTablaViewModel
    {
        public int OrderID { get; set;}
        [Required]
        [Display(Name = "Fecha de Confirmacion")]
        public DateTime ConfirmeDate { get; set; }
        [Required]
        [Display(Name = "Comentarios")]
        public string Feedback { get; set; }
        [Required]
        [Display(Name = "Confirmación")]
        public bool IsConfirmed { get; set; }

    }
}