using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp3_equipo25.Layouts
{
	public partial class Nav : System.Web.UI.UserControl
	{
		public List<Carrito> listaCarrito { get; set; }
		public static string carrito { get; set; }
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				updateCarrito();
			}
		}

		private void updateCarrito()
		{
			listaCarrito = (List<Carrito>)Session["carrito"] != null ? (List<Carrito>)Session["carrito"] : new List<Carrito>();
			int cantidad = 0;

			foreach (var item in listaCarrito)
			{
				cantidad += item.Cantidad;
			}

			carrito = cantidad.ToString();
		}

		protected void BtnBusquedaRapida_Click(object sender, EventArgs e)
		{
			List<Articulo> Listafiltrada = new List<Articulo>();

			if (lbSearch.Text.Count() > 0)
			{
				Listafiltrada = ((List<Articulo>)Session["ListaArticulos"]).FindAll(x => x.Descripcion.ToUpper().Contains(lbSearch.Text.ToUpper()) || x.Nombre.ToUpper().Contains(lbSearch.Text.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(lbSearch.Text.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(lbSearch.Text.ToUpper()));

			}
			else
			{

				Listafiltrada = (List<Articulo>)Session["ListaArticulos"];

			}

			lbSearch.Text = "";
			Session.Add("ListafiltradaDefault", Listafiltrada);
			Response.Redirect("default.aspx");
		}
	}
}