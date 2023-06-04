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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;

namespace tp3_equipo25
{
    public partial class CarritoWeb : System.Web.UI.Page
    {
        public List<Carrito> ListaCarrito { get; set; }
        bool Usado;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["carrito"] != null)
            {
                ActualizarDgv();

            }
            if (!IsPostBack)
            {
                CargarDropdowns();
                ResetearImagen2();
            }
            Cuponera();
        }

        protected void ActualizarDgv()
        {
            if (Session["carrito"] != null)
            {
                ListaCarrito = (List<Carrito>)Session["carrito"];
                dgvCarrito.DataSource = ListaCarrito;
                dgvCarrito.DataBind();

                CargarTotales();
            }
        }

        protected void dgvCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void CargarTotales()
        {
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell totalCarrito = new TableCell();
            totalCarrito.ColumnSpan = dgvCarrito.Columns.Count + 1;
            totalCarrito.Text = "tenés " + CargarTotalItems() + " item(s) en tu carrito         | TOTAL: " + string.Format("{0:C}", Convert.ToDecimal(CargarTotal()));
            totalCarrito.Attributes.Add("style", "font-family: 'Bebas-Neue', sans-serif; text-align: right; background-color:#fff; font-weight: bold;");
            gv.Cells.Add(totalCarrito);
            this.dgvCarrito.Controls[0].Controls.AddAt(dgvCarrito.Rows.Count + 1, gv);
            Nav.carrito = CargarTotalItems().ToString();
        }

        protected Decimal CargarTotal()
        {
            Decimal total = 0;
            foreach (Carrito carrito in ListaCarrito)
            {
                total += (carrito.Articulo.Precio * carrito.Cantidad);
            }
            return total;
        }

        public int CargarTotalItems()
        {
           int total = 0;
            foreach (Carrito carrito in ListaCarrito)
            {
                total += (carrito.Cantidad);
            }
            return total;
        }
  
        public void CargarDropdowns()
        {
            DDLOrdenar.Items.Add("Organizar por");
            DDLOrdenar.Items.Add("Cantidad ascendente");
            DDLOrdenar.Items.Add("Cantidad descendente");
            DDLOrdenar.Items.Add("Precio ascendente");
            DDLOrdenar.Items.Add("Precio descendente");

        }

        public void DDLOrdenar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLOrdenar.SelectedIndex != 0)
            {
                if (DDLOrdenar.SelectedIndex == 1)
                    ListaCarrito = ListaCarrito.OrderBy(x => x.Cantidad).ToList();
                if (DDLOrdenar.SelectedIndex == 2)
                    ListaCarrito = ListaCarrito.OrderByDescending(x => x.Cantidad).ToList();
                if (DDLOrdenar.SelectedIndex == 3)
                    ListaCarrito = ListaCarrito.OrderBy(x => x.Articulo.Precio).ToList();
                if (DDLOrdenar.SelectedIndex == 4)
                    ListaCarrito = ListaCarrito.OrderByDescending(x => x.Articulo.Precio).ToList();
                Session["carrito"] = ListaCarrito;
                ActualizarDgv();
               
               // CargarTotales();
            }

        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            Button btnVer = (Button)sender;
            int rowIndex = int.Parse(btnVer.CommandArgument);
            Image2.ImageUrl = ListaCarrito[rowIndex].Articulo.Imagenes[0].UrlImagen;
        }



        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            if (Usado == false)
            {
                if (txbxCupon.Text.ToUpper() == "KLOSTER")
                {
                    AplicarCupon();
                    Usado = true;
                    Session.Add("Usado", Usado);
                    Cuponera();
                    txbxCupon.Text = "Descuento aplicado!";
                }
                else
                {
                    txbxCupon.Text = "Cupón Inválido. Ingresá el código nuevamente.";
                }
            }
        }

        protected void AplicarCupon()
        {
            foreach (var carrito in ListaCarrito)
            {
                carrito.Articulo.Precio -= Decimal.Multiply(carrito.Articulo.Precio, (decimal)0.10);
            }

            ActualizarDgv();
        }

        protected void Cuponera()
        {
            if (Session["Usado"] == null)
                Usado = false;
            else if ((bool)Session["Usado"] == true)
            {
                txbxCupon.Text = "Tu carrito tiene un descuento aplicado ;)";
                txbxCupon.Enabled = false;
                btnCupon.Enabled = false;
                btnCupon.BackColor = Color.Gray;
            }
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int rowIndex = int.Parse(btnEliminar.CommandArgument);
            Carrito carrito = ListaCarrito[rowIndex];
            BorrarCarritoDeLista(carrito);
        }

        protected void BorrarCarritoDeLista(Carrito carrito)
        {
            ListaCarrito.Remove(carrito);
            if (ListaCarrito.Count == 0)
            {
                BorrarCarrito();
            }
            else
            {
                ResetearImagen2();
                ActualizarDgv();
            }
           

        }

        protected void BorrarCarrito()
        {
            ListaCarrito = null;
            Session["carrito"] = null;
            Session["Usado"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnBorrarCarrito_Click(object sender, EventArgs e)
        {
            BorrarCarrito();
        }


        protected void ResetearImagen2()
        {
            Image2.ImageUrl = "https://d3ugyf2ht6aenh.cloudfront.net/stores/872/502/products/carro-compras-111-51d754b8f31ee398d316701805488150-640-0.webp";
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
                    BorrarCarritoDeLista(carrito);
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
            ActualizarDgv();
        }

        protected void Quitar(Carrito carrito, int filaIndice)
        {
            try
            {
                if (filaIndice != -1 && filaIndice < ListaCarrito.Count)
                {
                    // Disminuyo cantidad
                    carrito.Cantidad--;
                    // Si el carrito queda vacío, lo borro la lista de carritos y reseteo la imagen2. Si no queda vacío, lo actualizo en lista y en sesión 
                    if (carrito.Cantidad == 0)
                    {
                        BorrarCarritoDeLista(carrito);
                        ResetearImagen2();
                        Session["carrito"] = ListaCarrito;
                        dgvCarrito.DataBind();
                        ActualizarDgv();
                    }
                    else
                    {
                        Session["carrito"] = ListaCarrito;
                        dgvCarrito.DataBind();
                        ActualizarDgv(); ;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
    }

}