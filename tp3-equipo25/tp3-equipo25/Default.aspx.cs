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
            // Para que el textbox de busqueda avanzada tome por default el boton correspondiente al apretar enter
            TxtBusqueda.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('BtnBusqueda').click();return false;}} else {return true}; "); 
            if (!IsPostBack)
			{
				//Se carga en session el estado del menu busqueda avanzada
                bool botonBusquedaAvanzada=false;
				Session.Add("BusquedaAvanzada", botonBusquedaAvanzada);

				// Se listan articulos y guardan en session
                ArticuloNegocio articulosNegocio = new ArticuloNegocio();
				Session.Add("ListaArticulos", articulosNegocio.listar());

				
				if (Session["ListafiltradaDefault"] == null)
				{
					// Si no hubo busqueda previa se carga el total
					RepCards.DataSource = Session["ListaArticulos"];
					RepCards.DataBind();
				}
				else
				{
					// Cuando vuelve de otra pagina tiene su busqueda todavia en default
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
			// Buscamos el articulo en la Lista con el command Argument
			int ArticuloId = int.Parse(((Button)sender).CommandArgument);
			Articulo articulo = ((List<Articulo>)Session["ListaArticulos"]).Find(x => x.Id == ArticuloId);

			// Cargamos el articulo en session y redirigimos a Pagina detalle
			Session.Add("DetalleArticulo", articulo);
			Response.Redirect("Detalle.aspx", false);
		}

		public List<Articulo> OrdenarLista(List<Articulo> listafiltrada, string SeleccionDDL)
		{
            
	
            List<Articulo> Listaordenada = new List<Articulo>();
			if (SeleccionDDL == "Nombre < A - Z >")
			{
				return Listaordenada = listafiltrada.OrderBy(x => x.Nombre).ToList();
			}
			if (SeleccionDDL == "Nombre < Z - A >")
			{
				return Listaordenada = listafiltrada.OrderByDescending(x => x.Nombre).ToList();
			}
			if (SeleccionDDL == "Mayor Precio")
			{
                return Listaordenada = listafiltrada.OrderByDescending(x => x.Precio).ToList();
            }
			if (SeleccionDDL == "Menor Precio")
			{				
                return Listaordenada = listafiltrada.OrderBy(x => x.Precio).ToList();
            }
			return listafiltrada;
		}

		public void CargarDropdowns()
		{
			// Lista categorias y las carga en el DDL con foreach
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
			DDLOrdenar.Items.Add("Nombre < A - Z >");
			DDLOrdenar.Items.Add("Nombre < Z - A >");
			DDLOrdenar.Items.Add("Mayor Precio");
			DDLOrdenar.Items.Add("Menor Precio");

		}

		public bool CheckbusquedaAvanzada()
		{
			return (bool)Session["BusquedaAvanzada"];
		}


		public void BusquedaAvanzada()
		{
			// Filtro de busqueda por remocion

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
				// Si lo campos estan vacios se recarga la lista
                Listafiltrada = new List<Articulo>((List<Articulo>)Session["ListaArticulos"]);

            }

            RepCards.DataSource = OrdenarLista(Listafiltrada, DDLOrdenar.SelectedItem.ToString());
            Session.Add("ListafiltradaDefault", Listafiltrada);
            RepCards.DataBind();


        }


        public void Tipodebusqueda()
		{
			// Si Checkbusqueda avanzada se inhabilita el Input de busqueda. El boton de busqueda rapida funciona tambien conla busqueda avanzada
			TxtBusquedaRapida.Enabled = !CheckbusquedaAvanzada();
		    if (CheckbusquedaAvanzada()) TxtBusquedaRapida.Attributes["placeholder"] = string.Empty;
		}

		protected void BtnBusqueda_Click(object sender, EventArgs e)
		{
			BusquedaAvanzada();
      
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
            // Filta la lista cada vez se selecciona un nuevo indice.
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
			//informa a session el estado del menu busqueda avanzada
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
			// Si Busqueda avanzada esta activo realiza la busqueda del menu busqueda avanzada

			if (CheckbusquedaAvanzada())
			{
				BusquedaAvanzada();

			}
			else { 
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
}
