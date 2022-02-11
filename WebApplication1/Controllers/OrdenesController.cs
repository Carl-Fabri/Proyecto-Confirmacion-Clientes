using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class OrdenesController : Controller
    {
        // Controladores del Apartado Index
        // GET: Reciviendo Datos
        public ActionResult Index()
        {     
            List<OrdenesViewModel> ListaOrdenes;
            using (NorthwindEntidad BD = new NorthwindEntidad())
            {
                
                ListaOrdenes = (from d in BD.SP_MOSTRARORDENES() //En este apartado establecemos la llamada de los datos desde el procedimiento almacenado de SP_MostrarOrdenes
                                select new OrdenesViewModel
                                {
                                    OrderID = d.OrderID.GetValueOrDefault(),
                                    OrderDate = (DateTime)d.OrderDate.GetValueOrDefault(),
                                    ContactName = d.ContactName,
                                    Phone = d.Phone
                                }).ToList();
            }
            return View(ListaOrdenes);//Aqui establecemos que la lista sea enviada a la vista
        }
        #region

        #endregion
        // Controladores del Apartado Editar

        public ActionResult Editar(int id) //Dentro de los controladores instanciamos la variable ID que usaremos para traer nuestros respectivos datos
        {
            //En esta parta establecemos los Productos
            OrdersTablaViewModel md = new OrdersTablaViewModel();
            using (NorthwindEntidad BD = new NorthwindEntidad())
            {
                //Con esta instancia crearemos una lista
                List<ProductosViewModel> ListaProductos;
                decimal TotalPrice = 0;
                ///Con este codigo buscamos la informacion de las tabla que necesitemos
                var TablaSeleccionada = BD.Orders.Find(id);
                var OrdenSeleccionada = BD.Customers.Find(TablaSeleccionada.CustomerID);


                md.OrderID = TablaSeleccionada.OrderID;
                md.ConfirmeDate = (DateTime)TablaSeleccionada.ConfirmeDate.GetValueOrDefault();
                md.Feedback = TablaSeleccionada.Feedback;
                md.IsConfirmed = (bool)TablaSeleccionada.IsConfirmed.GetValueOrDefault();


                ListaProductos = (from pd in BD.SP_MOSTRAPRODUCTOS(id)
                       select new ProductosViewModel
                       {
                           ProductID = (int)pd.ProductID,
                           ProductoName = pd.ProductName,
                           Quantity = (int)pd.Quantity,
                           UnitPrice = String.Format("{0:C}", (decimal)pd.UnitPrice),
                           FinalPrice = String.Format("{0:C}", Convert.ToDecimal((decimal)pd.UnitPrice * (int)pd.Quantity)),
                       }).ToList();

                foreach (var prod in BD.SP_MOSTRAPRODUCTOS(id))
                {
                    TotalPrice +=  Convert.ToDecimal( prod.UnitPrice * prod.Quantity);
                }
                ViewBag.NumeroPedido = md.OrderID;
                ViewBag.NombreCliente = OrdenSeleccionada.ContactName;
                ViewBag.TotalFinal = String.Format("{0:C}", TotalPrice);
                ViewBag.DatosProductos = ListaProductos;//Con esto podremos visualizar la lista desde nuestra vista
            }
            return View(md);//Aqui establecemos que el objeto sea enviada a la vista
        }

        [HttpPost]
        public ActionResult Editar(OrdersTablaViewModel md)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (NorthwindEntidad BD = new NorthwindEntidad())
                    {
                        var TablaSeleccionada = BD.Orders.Find(md.OrderID);
                        
                        TablaSeleccionada.ConfirmeDate = DateTime.Now;
                        TablaSeleccionada.Feedback = md.Feedback;
                        TablaSeleccionada.IsConfirmed = md.IsConfirmed;

                        //Con este modificar los datos
                        BD.Entry(TablaSeleccionada).State = System.Data.Entity.EntityState.Modified;
                        BD.SaveChanges();
                    }
                    return Redirect("~/Ordenes/");
                }
                return View(md);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}