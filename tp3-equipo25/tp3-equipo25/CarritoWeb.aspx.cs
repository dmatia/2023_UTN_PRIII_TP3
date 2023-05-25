using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace tp3_equipo25
{
    public partial class CarritoWeb : System.Web.UI.Page
    {
        public List<Carrito> ListaCarrito { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Articulo art1 = new Articulo(); art1.Id = 1;
            Articulo art2 = new Articulo(); art2.Id = 2;
            Articulo art3 = new Articulo(); art3.Id = 3;
            Articulo art4 = new Articulo(); art4.Id = 4;

            Carrito car1 = new Carrito();
            Carrito car2 = new Carrito();
            Carrito car3 = new Carrito();
            Carrito car4 = new Carrito();

            car1.Articulo = art1; car1.Cantidad = 1;
            car2.Articulo = art2; car2.Cantidad = 1;
            car3.Articulo = art3; car3.Cantidad = 1;
            car4.Articulo = art4; car4.Cantidad = 1;
            ListaCarrito = new List<Carrito>();
            ListaCarrito.Add(car1);
            ListaCarrito.Add(car2);
            ListaCarrito.Add(car3);
            ListaCarrito.Add(car4);

            dgvCarrito.DataSource = ListaCarrito;
            dgvCarrito.DataBind();
        }

        protected void dgvCarrito_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvCarrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCarrito.PageIndex = e.NewPageIndex;
            dgvCarrito.DataBind();
        }
    }
}