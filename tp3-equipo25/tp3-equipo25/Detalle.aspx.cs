using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace tp3_equipo25
{
	public partial class Detalle : Page
	{
		public Articulo articulo { get; set; }

		public List<Articulo> articulosRelacionados { get; set; }

		public DateTime dia { get; set; }

		private List<Carrito> carrito;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				dia = DateTime.Today.AddDays(1);
				CargarArticulo();
				CargarArticulosRelacionados();
			}
		}

		private void CargarArticulo()
		{
			articulo = (Articulo)Session["DetalleArticulo"];
		}

		private void CargarCarrito()
		{
			//Cargamos session de carrito si existe. Si no se crea un nuevo objeto.
			carrito = (List<Carrito>)Session["carrito"] != null ? (List<Carrito>)Session["carrito"] : new List<Carrito>();
		}

		protected void AgregarCarrito(object sender, EventArgs e)
		{

			CargarCarrito();
			CargarArticulo();

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
					Response.Redirect("CarritoWeb.aspx", false);

					return;
				}

			}

			//Si el artículo no se encuentra en el carrito, se agrega uno nuevo
			Carrito nuevoArticulo = new Carrito();
			nuevoArticulo.Articulo = articulo;
			nuevoArticulo.Cantidad = Convert.ToInt32(tb_cantidad.Text);
			carrito.Add(nuevoArticulo);

			//Guardamos carrito en session
			Session["carrito"] = carrito;
			Response.Redirect("CarritoWeb.aspx", false);
		}

		private void CargarArticulosRelacionados()
		{
			if (articulo != null)
			{
				List<Articulo> articulosLista = ((List<Articulo>)Session["ListaArticulos"]);
				articulosRelacionados = articulosLista.FindAll(X => X.Categoria.Descripcion == articulo.Categoria.Descripcion && X.Id != articulo.Id);

				repeaterArticulosRelacionados.DataSource = articulosRelacionados;
				repeaterArticulosRelacionados.DataBind();
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