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
            if (Session["carrito"] != null)
            {
                //ListaCarrito = new List<Carrito>();
                ListaCarrito = (List<Carrito>)Session["carrito"];
                dgvCarrito.DataSource = ListaCarrito;
                dgvCarrito.DataBind();


                cargarTotal();
                cargarTotalItems();
                Image2.ImageUrl = "https://d3ugyf2ht6aenh.cloudfront.net/stores/872/502/products/carro-compras-111-51d754b8f31ee398d316701805488150-640-0.webp";
            }


            
            
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


        protected void cargarTotalItems()
        {
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell totalCarrito = new TableCell();
            totalCarrito.ColumnSpan = dgvCarrito.Columns.Count + 1;
            Decimal total = 0;
            foreach (Carrito carrito in ListaCarrito)
            {
                total += (carrito.Cantidad);
            }
            totalCarrito.Text = "Tenés " + total + " items en tu carrito";
            totalCarrito.Attributes.Add("style", "background-color:#7FB3D5; text-align: left");
            gv.Cells.Add(totalCarrito);
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

            totalCarrito.Text = "Total " +  string.Format("{0:C}", Convert.ToDecimal(total));
            totalCarrito.Attributes.Add("style", "text-align: right; background-color:#7FB3D5; fonte-weight: bold");
            gv.Cells.Add(totalCarrito);
            this.dgvCarrito.Controls[0].Controls.AddAt(dgvCarrito.Rows.Count + 1, gv);
        }
    }
}