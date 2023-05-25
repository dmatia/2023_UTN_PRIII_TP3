using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp3_equipo25
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if(!IsPostBack)
			{
				ArticuloNegocio articulosNegocio = new ArticuloNegocio();
				Session.Add("ListaArticulos", articulosNegocio.listar());
				RepCards.DataSource = Session["ListaArticulos"];
				RepCards.DataBind();
			}
		}

        protected void BtnDetalle_Click(object sender, EventArgs e)
        {
			//Guardamos Articulo en Session
			int ArticuloId = int.Parse(((Button)sender).CommandArgument);
			Articulo articulo = ((List<Articulo>)Session["ListaArticulos"]).Find(x => x.Id == ArticuloId);

			Session.Add("DetalleArticulo", articulo);
			Response.Redirect("Detalle.aspx", false);
		}
    }
}