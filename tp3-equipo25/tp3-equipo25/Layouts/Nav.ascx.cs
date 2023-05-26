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
			updateCarrito();
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
	}
}