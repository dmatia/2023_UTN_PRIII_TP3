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

namespace tp3_equipo25
{
    public partial class CarritoWeb : System.Web.UI.Page
    {
        public List<Carrito> ListaCarrito { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["carrito"] != null))
            {
                cargarGridView();
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
            
            Decimal total = 0;
            
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

            totalCarrito.Text = "Total " + string.Format("{0:C}", Convert.ToDecimal(total));
            totalCarrito.Attributes.Add("style", "text-align: right; background-color:#7FB3D5; fonte-weight: bold");
            gv.Cells.Add(totalCarrito);
            this.dgvCarrito.Controls[0].Controls.AddAt(dgvCarrito.Rows.Count + 1, gv);
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Button btnAgregar = (Button)sender;
            GridViewRow fila = (GridViewRow)btnAgregar.NamingContainer;
            int filaIndice = fila.RowIndex;

            // busco el carrito en la lista por el index de la lista
            Carrito carrito = ListaCarrito[filaIndice];

            // Aumentar cant
            carrito.Cantidad++;

            // actualizo el gridview 
            Session["carrito"] = ListaCarrito;
            dgvCarrito.DataBind();
            cargarTotal();
            cargarTotalItems();
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            Button btnAgregar = (Button)sender;
            GridViewRow row = (GridViewRow)btnAgregar.NamingContainer;
            int rowIndex = row.RowIndex;

            // Obtener el carrito correspondiente en la lista
            Carrito carrito = ListaCarrito[rowIndex];

            // Aumentar la cantidad del carrito
            if(carrito.Cantidad > 0)
            carrito.Cantidad--;
            else
            {
                // borrar carrito de la lista
            }

            // Actualizar el GridView
            Session["carrito"] = ListaCarrito;
            dgvCarrito.DataBind();
            cargarGridView();
        }

        public void cargarGridView()
        {
                ListaCarrito = (List<Carrito>)Session["carrito"];
                dgvCarrito.DataSource = ListaCarrito;
                dgvCarrito.DataBind();
                cargarTotal();
                cargarTotalItems();
                Image2.ImageUrl = "https://d3ugyf2ht6aenh.cloudfront.net/stores/872/502/products/carro-compras-111-51d754b8f31ee398d316701805488150-640-0.webp";
            
        }



    }
}