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

                   DdlMarca.Items.Add("Marca");
                foreach (Marca aux in Listamarcas)
                {
                DdlMarca.Items.Add(aux.Descripcion);
                }

        }

        protected void TxtPreciomax_TextChanged(object sender, EventArgs e)
        {
          
        }


        protected void BtnBusqueda_Click(object sender, EventArgs e)
        {

            List<Articulo> Listafiltrada = (List<Articulo>)Session["ListaArticulos"];

            
            if(DdlCategoria.SelectedIndex > 0 )
            {
                Listafiltrada.RemoveAll(x => !x.Categoria.Descripcion.ToUpper().Contains(DdlCategoria.SelectedItem.ToString().ToUpper()));

            }
            if (DdlMarca.SelectedIndex > 0)
            {
                Listafiltrada.RemoveAll(x => !x.Marca.Descripcion.ToUpper().Contains(DdlMarca.SelectedItem.ToString().ToUpper()));

            }
            
            if (TxtBusqueda.Text.Length > 0)
            {
                if (ChkCheckDescripcion.Checked)
                {
                    Listafiltrada.RemoveAll(x => !(x.Descripcion.ToUpper().Contains(TxtBusqueda.Text.ToUpper()) || x.Nombre.ToUpper().Contains(TxtBusqueda.Text.ToUpper())));
                }
                else {
                    Listafiltrada.RemoveAll(x => !x.Nombre.ToUpper().Contains(TxtBusqueda.Text.ToUpper()));
                }
            }

            if (TxtPreciomin.Text != string.Empty && TxtPreciomax.Text != string.Empty)
            {
                decimal precioMaximo;
                decimal precioMinimo;
                if (decimal.TryParse(TxtPreciomin.Text, out precioMinimo) && decimal.TryParse(TxtPreciomax.Text, out precioMaximo))
                {
                    Listafiltrada.RemoveAll(x => x.Precio < precioMinimo && x.Precio > precioMaximo);
                }
                
            }
            else if (TxtPreciomax.Text != string.Empty)
            {
                decimal precioMaximo;
                if (decimal.TryParse(TxtPreciomin.Text, out precioMaximo))
                    Listafiltrada.RemoveAll(x => x.Precio > precioMaximo);
            }
            else
            {
                decimal precioMinimo;
                if (decimal.TryParse(TxtPreciomin.Text, out precioMinimo))
                Listafiltrada.RemoveAll(x => x.Precio < precioMinimo);

            }
            RepCards.DataSource = Listafiltrada;
            RepCards.DataBind();
        }
        
        

       
    }
}
   