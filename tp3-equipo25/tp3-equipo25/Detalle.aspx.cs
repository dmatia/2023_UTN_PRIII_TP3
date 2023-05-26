using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace tp3_equipo25
{
	public partial class Detalle : Page
	{
		public Articulo articulo { get; set; }

		public DateTime dia { get; set; }

		private List<Carrito> carrito;
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				dia = DateTime.Today.AddDays(1);
				CargarArticulo();
			}
		}

		private void CargarArticulo()
		{
			articulo = (Articulo)Session["DetalleArticulo"];
		}

		protected void AgregarCarrito(object sender, EventArgs e)
		{
			//Cargamos session de carrito si existe. Si no se crea un nuevo objeto.
			carrito = (List<Carrito>)Session["carrito"] != null ? (List<Carrito>)Session["carrito"] : new List<Carrito>();

			if (carrito.Count > 0)
			{
				//Buscamos el indice del articulo en el carrito
				int indexArticulo = carrito.FindIndex(x => x.Articulo.Id == articulo.Id);

				if (indexArticulo >= 0)
				{
					//Si el articulo está en el carrito, le sumamos la cantidad nueva
					carrito[indexArticulo].Cantidad += Convert.ToInt32(tb_cantidad.Text);

					//Guardamos carrito en session
					Session["carrito"] = carrito;
					//Response.Redirect("Carrito.aspx", false); <-- DESCOMENTAR CUANDO EL ARCHIVO ESTE CREADO

					return;
				}

			}

			//Si el artículo no se encuentra en el carrito, se agrega uno nuevo
			Carrito nuevoArticulo = new Carrito();
			nuevoArticulo.Articulo = articulo;
			nuevoArticulo.Cantidad = 1;
			carrito.Add(nuevoArticulo);

			//Guardamos carrito en session
			Session["carrito"] = carrito;
			//Response.Redirect("Carrito.aspx", false); <-- DESCOMENTAR CUANDO EL ARCHIVO ESTE CREADO
		}
	}
}