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
using System.ComponentModel.Design;
using tp3_equipo25.Layouts;
using System.Configuration;

namespace tp3_equipo25
{
    public partial class CarritoWeb : System.Web.UI.Page
    {
        public List<Carrito> ListaCarrito { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["carrito"] != null)
            {
                cargarGridView();
            }
            if (!IsPostBack)
            {
                ResetearImagen2();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //valores de fila
                string quantityStr = DataBinder.Eval(e.Row.DataItem, "Cantidad").ToString();
                string priceStr = DataBinder.Eval(e.Row.DataItem, "Articulo.Precio").ToString();

                // valores a tipos numéricos
                int cantidad = int.Parse(quantityStr);
                decimal precio = decimal.Parse(priceStr);

                // Calcular el subtotal
                decimal subtotal = cantidad * precio;

                // acá busco la columna de subtotales
                Label lblSubtotal = (Label)e.Row.FindControl("lblSubtotal");

                // se agrega el subtotal calculado como texto de la celda
                lblSubtotal.Text = subtotal.ToString("C");
            }
        }

        public void cargarGridView()
        {
            ListaCarrito = (List<Carrito>)Session["carrito"];
            dgvCarrito.DataSource = ListaCarrito;
            dgvCarrito.DataBind();
            if (ListaCarrito != null)
            {
                cargarTotal();
                cargarTotalItems();

            }
        }

        protected void cargarTotalItems()
        {
            // crea una nueva fila
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            // crea una nueva celda
            TableCell totalCarrito = new TableCell();
            // tamaño en columnas de la celda
            totalCarrito.ColumnSpan = dgvCarrito.Columns.Count + 1;

            int total = 0;

            // suma al total la cantidad de items por carrito al total
            foreach (Carrito carrito in ListaCarrito)
            {
                total += (carrito.Cantidad);
            }

            //se agrega el total como texto de la celda creada
            totalCarrito.Text = "Tenés " + total + " items en tu carrito";
            // estilos de la fila creada
            totalCarrito.Attributes.Add("style", "background-color:#7FB3D5; text-align: left");

            // se agrega la celda a la fila
            gv.Cells.Add(totalCarrito);

            // Session agrega la fila al dgv
            this.dgvCarrito.Controls[0].Controls.AddAt(0, gv);

            //Cambia el contador del navegador
            Nav.carrito = total.ToString();

        }

        protected void cargarTotal()
        {
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell totalCarrito = new TableCell();
            totalCarrito.ColumnSpan = dgvCarrito.Columns.Count + 1;
            Decimal total = 0;
            foreach (Carrito carrito in ListaCarrito)
            {
                total += (carrito.Articulo.Precio * carrito.Cantidad);
            }

            totalCarrito.Text = "TOTAL: " + string.Format("{0:C}", Convert.ToDecimal(total));
            totalCarrito.Attributes.Add("style", "font-family: 'Bebas-Neue', sans-serif; text-align: right; background-color:#7FB3D5; font-weight: bold; font-size: 1.2rem");
            gv.Cells.Add(totalCarrito);
            this.dgvCarrito.Controls[0].Controls.AddAt(dgvCarrito.Rows.Count + 1, gv);
        }

        protected void btnAcción(object sender, EventArgs e)
        {
            // Me traigo el sender y la fila para saber el indice de la lisra que busco, luego veo qué comando me manda  a llamar
            Button Accion = (Button)sender;
            GridViewRow fila = (GridViewRow)Accion.NamingContainer;
            int filaIndice = fila.RowIndex;
            Carrito carrito = ListaCarrito[filaIndice];

            switch (Accion.CommandName)
            {
                case "Agregar":
                    Agregar(carrito, filaIndice);
                    break;

                case "Quitar":
                    Quitar(carrito, filaIndice);
                    break;

                case "Ver":
                    Ver(carrito, filaIndice);
                    break;

                case "Borrar":
                    borrarCarritoDeLista(carrito);
                    break;

                case "Detalle":
                    Detalle(filaIndice);
                    break;

                default:
                    break;
            }


        }

        protected void Agregar(Carrito carrito, int filaIndice)
        {
            // Aumento la cantidad y actualizo la lista local y la de sesión
            carrito.Cantidad++;
            Session["carrito"] = ListaCarrito;
            dgvCarrito.DataBind();
            cargarGridView();
        }

        protected void Quitar(Carrito carrito, int filaIndice)
        {
            if (filaIndice != -1 && filaIndice < ListaCarrito.Count)
            {
                // Disminuyo cantidad
                carrito.Cantidad--;
                // Si el carrito queda vacío, lo borro la lista de carritos y reseteo la imagen2. Si no queda vacío, lo actualizo en lista y en sesión 
                if (carrito.Cantidad == 0)
                {
                    borrarCarritoDeLista(carrito);
                    ResetearImagen2();
                }
                else
                {
                    Session["carrito"] = ListaCarrito;
                    dgvCarrito.DataBind();
                    cargarGridView();
                }
            }
        }

        protected void Ver(Carrito carrito, int filaIndice)
        {
            if (filaIndice == -1 || ListaCarrito[filaIndice].Articulo.Imagenes[0].UrlImagen == "")
            {
                ResetearImagen2();
            }
            else
            {
                Image2.ImageUrl = ListaCarrito[filaIndice].Articulo.Imagenes[0].UrlImagen;
            }
        }

        protected void Detalle(int filaIndice)
        {
            List<Carrito> articulos = (List<Carrito>)Session["carrito"];
            Articulo articulo = articulos[filaIndice].Articulo;
            Session.Add("DetalleArticulo", articulo);
            Response.Redirect("Detalle.aspx", false);
        }

        protected void ResetearImagen2()
        {
            Image2.ImageUrl = "https://d3ugyf2ht6aenh.cloudfront.net/stores/872/502/products/carro-compras-111-51d754b8f31ee398d316701805488150-640-0.webp";
        }

        protected void borrarCarritoDeLista(Carrito carrito)
        {
            ListaCarrito.Remove(carrito);
            if (ListaCarrito.Count == 0)
            {
                Session["carrito"] = null;
                ListaCarrito = null;
            }
            cargarGridView();

        }

        protected void btnBorrarCarrito_Click(object sender, EventArgs e)
        {
            Session["carrito"] = null;
            cargarGridView();
        }
    }
}