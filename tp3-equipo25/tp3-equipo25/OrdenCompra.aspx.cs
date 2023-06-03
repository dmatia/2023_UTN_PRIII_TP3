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
				CargarDropDownList();


				if (carrito != null)
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

		private void CargarDropDownList()
		{
	
			modoEnvio.DataBind();
			modoEnvio.Items.Add(new ListItem("Envío gratis a tu domicilio", "0"));
			modoEnvio.Items.Add(new ListItem("Retiro gratis en sucursal", "1"));
			lbModoEnvio.Text = modoEnvio.Items[0].Text + ". Llega el " + DateTime.Today.AddDays(1).ToString("dddd", new System.Globalization.CultureInfo("es-MX"));

			modoPago.DataBind();
			modoPago.Items.Add(new ListItem("Tarjeta de Crédito", "0"));
			modoPago.Items.Add(new ListItem("Tarjeta de Débito", "1"));
			modoPago.Items.Add(new ListItem("Grupo 25 Crédito", "2"));
			modoPago.Items.Add(new ListItem("Efectivo", "3"));
			lbModoPago.Text = modoPago.Items[0].Text;

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

		protected void modoPago_Change(object sender, EventArgs e)
		{
			int index = modoPago.SelectedIndex;
			string opcion = modoPago.SelectedItem.Text;
			lbModoPago.Text = opcion;

		}

		protected void modoEnvio_Change(object sender, EventArgs e)
		{
			int index = modoEnvio.SelectedIndex;
			string opcion = modoEnvio.SelectedItem.Text;

			if (index == 0)
				lbModoEnvio.Text = opcion + ". Llega el " + DateTime.Today.AddDays(1).ToString("dddd", new System.Globalization.CultureInfo("es-MX"));
			else
				lbModoEnvio.Text = opcion;

		}
	}
}