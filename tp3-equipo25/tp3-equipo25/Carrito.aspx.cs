using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace tp3_equipo25
{
    public partial class Carrito : System.Web.UI.Page
    {
        public List<Carrito> listaCarritos = new List<Carrito>();

        protected void Page_Load(object sender, EventArgs e)
        {
            dgvCarrito.DataSource = listaCarritos;
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