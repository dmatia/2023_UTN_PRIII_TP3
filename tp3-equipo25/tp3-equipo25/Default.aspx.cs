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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArticuloNegocio articulosNegocio = new ArticuloNegocio();
                Session.Add("ListaArticulos", articulosNegocio.listar());
                RepCards.DataSource = Session["ListaArticulos"];
                RepCards.DataBind();
                CargarDropdowns();

            }
            Tipodebusqueda();
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

            DDLOrdenar.Items.Add("Organizar por");
            DDLOrdenar.Items.Add("Nombre ascendente");
            DDLOrdenar.Items.Add("Nombre descendente");
            DDLOrdenar.Items.Add("Precio ascendente");
            DDLOrdenar.Items.Add("Precio descendente");

        }

        bool CheckbusquedaAvanzada()
        {
            return ChkBusquedaAvanzada.Checked;
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
            Session.Add("ListafiltradaDefault", Listafiltrada);

            RepCards.DataSource = Listafiltrada;
            RepCards.DataBind();
        }



        protected void TxtBusquedaRapida_TextChanged(object sender, EventArgs e)
        {

            
            List<Articulo> Listafiltrada = new List<Articulo>();
           
           if (TxtBusquedaRapida.Text.Count()>0) {
                Listafiltrada = ((List<Articulo>)Session["ListaArticulos"]).FindAll(x =>  x.Descripcion.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()) || x.Nombre.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()));
            }
            else
            {
                              
                    Listafiltrada = (List<Articulo>)Session["ListaArticulos"];
                
            }
            
            Session.Add("ListafiltradaDefault", Listafiltrada);


            RepCards.DataSource = Listafiltrada;
            RepCards.DataBind();
        }

        protected void BtnBusquedaRapida_Click(object sender, EventArgs e)
        {
            List<Articulo> Listafiltrada = new List<Articulo>();

            if (TxtBusquedaRapida.Text.Count() > 0)
            {
                Listafiltrada = ((List<Articulo>)Session["ListaArticulos"]).FindAll(x => x.Descripcion.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()) || x.Nombre.ToUpper().Contains(TxtBusquedaRapida.Text.ToUpper()));
            }
            else
            {

                Listafiltrada = (List<Articulo>)Session["ListaArticulos"];

            }
            Session.Add("ListafiltradaDefault", Listafiltrada);

            RepCards.DataSource = Listafiltrada;
            RepCards.DataBind();
        }
    }
}
   