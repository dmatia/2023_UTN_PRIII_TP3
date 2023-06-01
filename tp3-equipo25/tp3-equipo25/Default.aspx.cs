using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Runtime.Remoting.Messaging;

namespace tp3_equipo25
{
	public partial class Default : System.Web.UI.Page
	{
		private List<Articulo> Listafiltrada;
       
        protected void Page_Load(object sender, EventArgs e)
		{
            TxtBusqueda.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('BtnBusqueda').click();return false;}} else {return true}; "); // PARA QUE EL TEXTBOX DE BUSQUEDA AVANZADA TOME POR DEFAULT EL BOTON CORRESPONDIENTE AL HACER ENTER
            if (!IsPostBack)
			{
                bool botonBusquedaAvanzada=false;
                Session.Add("BusquedaAvanzada", botonBusquedaAvanzada);
                ArticuloNegocio articulosNegocio = new ArticuloNegocio();
				Session.Add("ListaArticulos", articulosNegocio.listar());

				if (Session["ListafiltradaDefault"] == null)
				{
					RepCards.DataSource = Session["ListaArticulos"];
					RepCards.DataBind();
				}
				else
				{
					Listafiltrada = (List<Articulo>)Session["ListafiltradaDefault"];
					RepCards.DataSource = Listafiltrada;
					RepCards.DataBind();
					Session["ListafiltradaDefault"] = null;

				}
				              
                CargarDropdowns();

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

		public List<Articulo> OrdenarLista(List<Articulo> listafiltrada, string SeleccionDDL)
		{

			List<Articulo> Listaordenada = new List<Articulo>();
			if (SeleccionDDL == "Nombre ascendente")
			{
				return Listaordenada = listafiltrada.OrderBy(x => x.Nombre).ToList();
			}
			if (SeleccionDDL == "Nombre descendente")
			{
				return Listaordenada = listafiltrada.OrderByDescending(x => x.Nombre).ToList();
			}
			if (SeleccionDDL == "Precio ascendente")
			{
				return Listaordenada = listafiltrada.OrderBy(x => x.Precio).ToList();
			}
			if (SeleccionDDL == "Precio descendente")
			{
				return Listaordenada = listafiltrada.OrderByDescending(x => x.Precio).ToList();
			}
			return listafiltrada;
		}

		public void CargarDropdowns()
		{
			CategoriaNegocio CategoriaNegocio = new CategoriaNegocio();
			List<IAtributo> Listacategorias = new List<IAtributo>();
			Listacategorias = CategoriaNegocio.listar();
			DdlCategoria.Items.Add("Elige una Categoria");
			foreach (Categoria aux in Listacategorias)
			{
				DdlCategoria.Items.Add(aux.Descripcion);

			}

			MarcaNegocio MarcaNegocio = new MarcaNegocio();
			List<IAtributo> Listamarcas = new List<IAtributo>();
			Listamarcas = MarcaNegocio.listar();

			DdlMarca.Items.Add("Elige una Marca");
			foreach (Marca aux in Listamarcas)
			{
				DdlMarca.Items.Add(aux.Descripcion);
			}

			DDLOrdenar.Items.Add("Organizar por");
			DDLOrdenar.Items.Add("Nombre ascendente");
			DDLOrdenar.Items.Add("Nombre descendente");
			DDLOrdenar.Items.Add("Precio ascendente");
			DDLOrdenar.Items.Add("Precio descendente");

		}

		public bool CheckbusquedaAvanzada()
		{
			return (bool)Session["BusquedaAvanzada"];
		}





        public void Tipodebusqueda()
		{
			TxtBusquedaRapida.Enabled = !CheckbusquedaAvanzada();
			BtnBusquedaRapida.Enabled = !CheckbusquedaAvanzada();	
            TxtBusquedaRapida.Visible = !CheckbusquedaAvanzada();	
            BtnBusquedaRapida.Visible = !CheckbusquedaAvanzada();
            if (CheckbusquedaAvanzada()) TxtBusquedaRapida.Attributes["placeholder"] = string.Empty;
		}

		protected void BtnBusqueda_Click(object sender, EventArgs e)
		{
			List<Articulo> Listafiltrada = new List<Articulo>((List<Articulo>)Session["ListaArticulos"]);
			bool Camposnovacios = false;

			if (DdlCategoria.SelectedIndex > 0)
			{
				Listafiltrada.RemoveAll(x => !x.Categoria.Descripcion.ToUpper().Contains(DdlCategoria.SelectedItem.ToString().ToUpper()));
				Camposnovacios = true;
			}
			if (DdlMarca.SelectedIndex > 0)
			{
				Listafiltrada.RemoveAll(x => !x.Marca.Descripcion.ToUpper().Contains(DdlMarca.SelectedItem.ToString().ToUpper()));
				Camposnovacios = true;
			}

			if (TxtBusqueda.Text.Length > 0)
			{
				if (ChkCheckDescripcion.Checked)
				{
					Listafiltrada.RemoveAll(x => !(x.Descripcion.ToUpper().Contains(TxtBusqueda.Text.ToUpper()) || x.Nombre.ToUpper().Contains(TxtBusqueda.Text.ToUpper())));
				}
				else
				{
					Listafiltrada.RemoveAll(x => !x.Nombre.ToUpper().Contains(TxtBusqueda.Text.ToUpper()));
				}
				Camposnovacios = true;
			}

			if (TxtPreciomin.Text != string.Empty && TxtPreciomax.Text != string.Empty)
			{
				decimal precioMaximo;
				decimal precioMinimo;
				if (decimal.TryParse(TxtPreciomin.Text, out precioMinimo) && decimal.TryParse(TxtPreciomax.Text, out precioMaximo))
				{
					Listafiltrada.RemoveAll(x => x.Precio < precioMinimo || x.Precio > precioMaximo);
					Camposnovacios = true;
				}
			}
			else if (TxtPreciomax.Text != string.Empty)
			{
				decimal precioMaximo;
				if (decimal.TryParse(TxtPreciomax.Text, out precioMaximo))
				{
					Listafiltrada.RemoveAll(x => x.Precio > precioMaximo);
					Camposnovacios = true;
				}
			}
			else if (TxtPreciomin.Text != string.Empty)
			{
				decimal precioMinimo;
				if (decimal.TryParse(TxtPreciomin.Text, out precioMinimo))
				{
					Listafiltrada.RemoveAll(x => x.Precio < precioMinimo);
					Camposnovacios = true;
				}
			}

			if (!Camposnovacios)
			{

				Listafiltrada = new List<Articulo>((List<Articulo>)Session["ListaArticulos"]);

			}
			RepCards.DataSource = OrdenarLista(Listafiltrada, DDLOrdenar.SelectedItem.ToString());
			Session.Add("ListafiltradaDefault", Listafiltrada);
			RepCards.DataBind();
		}



		protected void TxtBusquedaRapida_TextChanged(object sender, EventArgs e)
		{


			List<Articulo> Listafiltrada = new List<Articulo>();

			if (TxtBusquedaRapida.Text.Count() > 0)
			{
				Listafiltrada = ((List<Articulo>)Session["ListaArticulos"]).FindAll(x => x.Descripcion.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()) || x.Nombre.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()));
			}
			else
			{

				Listafiltrada = (List<Articulo>)Session["ListaArticulos"];

			}

			Session.Add("ListafiltradaDefault", Listafiltrada);
			RepCards.DataSource = OrdenarLista(Listafiltrada, DDLOrdenar.SelectedItem.ToString());
			RepCards.DataBind();
		}

	
		public void DDLOrdenar_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DDLOrdenar.SelectedIndex != 0)
			{
				List<Articulo> Listaactual = new List<Articulo>();
				if ((List<Articulo>)Session["ListafiltradaDefault"] != null)
				{
					Listaactual = ((List<Articulo>)Session["ListafiltradaDefault"]);

				}
				else
				{
					Listaactual = (List<Articulo>)Session["ListaArticulos"];
				}

				RepCards.DataSource = OrdenarLista(Listaactual, DDLOrdenar.SelectedItem.ToString());
				RepCards.DataBind();
			}

		}

             public void BtnBusquedaAvanzada_Click(object sender, EventArgs e)
        {
            if ((bool)Session["BusquedaAvanzada"])
            {
                bool aux = false;
                Session.Add("BusquedaAvanzada", aux);
                Tipodebusqueda();
            }
            else
            {
                bool aux = true;
                Session.Add("BusquedaAvanzada", aux);
                Tipodebusqueda();
            }

        }

        protected void BtnBusquedaRapida_Click(object sender, EventArgs e)
        {
            // Listafiltrada = new List<Articulo>();

            if (TxtBusquedaRapida.Text.Count() > 0)
            {
                Listafiltrada = ((List<Articulo>)Session["ListaArticulos"]).FindAll(x => x.Descripcion.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()) || x.Nombre.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()));

            }
            else
            {

                Listafiltrada = (List<Articulo>)Session["ListaArticulos"];

            }

            RepCards.DataSource = OrdenarLista(Listafiltrada, DDLOrdenar.SelectedItem.ToString());
            Session.Add("ListafiltradaDefault", Listafiltrada);
            RepCards.DataBind();
        }
    }
}
