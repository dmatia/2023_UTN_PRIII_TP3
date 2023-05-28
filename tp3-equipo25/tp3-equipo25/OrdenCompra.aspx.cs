using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace tp3_equipo25
{
	public partial class OrdenCompra : System.Web.UI.Page
	{
		public List<Carrito> carrito;
		
		public decimal precioFinal { get; set; }

		public int numeroCompra { get; set;  }

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				CargarCarrito();
				
				if(carrito != null)
				{
					CargarPrecioFinal();
					CargarRepeater();
					GenerarTokenCompra();
				}
			}
		}
		
		private void CargarCarrito()
		{
			carrito = (List<Carrito>)Session["carrito"];
		}
		
		private void CargarPrecioFinal()
		{
			foreach (Carrito item in carrito)
			{
				precioFinal += item.Articulo.Precio * item.Cantidad;
			}
		}

		private void CargarRepeater()
		{
			repeaterOrdenCompra.DataSource = carrito;
			repeaterOrdenCompra.DataBind();
		}

		protected void Comprar_Click(object sender, EventArgs e)
		{
			Session["Carrito"] = null;
			Response.Redirect("Default.aspx");
		}

		private void GenerarTokenCompra()
		{
			var random = new Random();
			numeroCompra = random.Next(minValue: 1, maxValue: 1000000);
		}
	}
}