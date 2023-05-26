using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;


namespace tp3_equipo25
{
    public partial class CarritoWeb : System.Web.UI.Page
    {
        public List<Carrito> ListaCarrito { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            /* if (Session["ListaCarritos"] == null)
             {
                 ListaCarrito = Session["ListaCarritos"];
             }*/

            Imagen img = new Imagen(); img.IdArticulo = 1; img.UrlImagen = "https://img.freepik.com/foto-gratis/bandera-argentina_1401-57.jpg";
            Imagen img1 = new Imagen(); img1.IdArticulo = 1; img1.UrlImagen = "https://img.freepik.com/fotos-premium/bandera-provincia-buenos-aires-argentina-ondeando-coleccion-pancartas-ilustracion-3d_118047-9003.jpg";
            Imagen img2 = new Imagen(); img2.IdArticulo = 1; img2.UrlImagen = "https://i.pinimg.com/736x/04/8c/3b/048c3b66b1d9bb5f1091860732cdf9c8.jpg";



            Articulo art1 = new Articulo(); art1.Id = 1; art1.Nombre = "Art1"; art1.Imagenes = new List<Imagen>(); art1.Imagenes.Add(img); art1.Precio = 20;
            Articulo art2 = new Articulo(); art2.Id = 2; art2.Nombre = "Art2"; art2.Imagenes = new List<Imagen>(); art2.Imagenes.Add(img1); art2.Precio = 150;
            Articulo art3 = new Articulo(); art3.Id = 3; art3.Nombre = "Art3"; art3.Imagenes = new List<Imagen>(); art3.Imagenes.Add(img2); art3.Precio = 30;
            Articulo art4 = new Articulo(); art4.Id = 4; art4.Nombre = "Art4"; art4.Imagenes = new List<Imagen>(); art4.Imagenes.Add(img); art4.Precio = 15;



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

            Image2.ImageUrl = ListaCarrito[0].Articulo.Imagenes[0].UrlImagen;  
            
        }

        protected void dgvCarrito_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvCarrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCarrito.PageIndex = e.NewPageIndex;
            dgvCarrito.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
        }
        protected void grd_Pre(object sender, EventArgs e)
        {
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell totalCarrito = new TableCell();
            totalCarrito.ColumnSpan = 5;
            Decimal total = 0;
            foreach(Carrito carrito in ListaCarrito) {
                total += (carrito.Articulo.Precio * carrito.Cantidad); 
            }
            totalCarrito.Text = "Total: $" + total;
            totalCarrito.Attributes.Add("style", "text-align:center");
            gv.Cells.Add(totalCarrito);
            this.dgvCarrito.Controls[0].Controls.AddAt(6, gv);
        }
    }
}