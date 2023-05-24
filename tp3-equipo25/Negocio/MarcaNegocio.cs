using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio : IAtributosNegocio
    {

        // METODO LISTAR TODAS LAS MARCAS EN DB
        public List<IAtributo> listar()
        {
            List<IAtributo> lista = new List<IAtributo>();
            AccesoDB datos = new AccesoDB();

            try
            {
                datos.setQuery("SELECT Id, Descripcion FROM Marcas");
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (Int32)datos.Reader["Id"];
                    if (datos.Reader["Descripcion"] != null)
                        aux.Descripcion = (string)datos.Reader["Descripcion"];
                    else
                        aux.Descripcion = "...";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.closeConnection();
            }
        }

		//  METODO AGREGAR MARCA
		public bool agregar(IAtributo nueva)
		{
			AccesoDB datos = new AccesoDB();

			try
			{
				datos.setQuery("Insert into MARCAS (Descripcion) values ('" + nueva.Descripcion + "')");
				if (datos.executeNonQuery())
					return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				datos.closeConnection();
			}
			return false;
		}

		// METODO MODIFICAR MARCA
		public bool modificar(IAtributo modificar)
		{
			AccesoDB datos = new AccesoDB();

			try
			{
				datos.setQuery("Update MARCAS set Descripcion = @desc WHERE Id = @id");
				datos.setParameter("@id", modificar.Id);
				datos.setParameter("@desc", modificar.Descripcion);

				if (datos.executeNonQuery())
					return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				datos.closeConnection();
			}
			return false;

		}

		// METODO ELIMINAR MARCA
		public bool eliminar(IAtributo registro)
		{
			AccesoDB datos = new AccesoDB();

			try
			{
				datos.setQuery("DELETE MARCAS WHERE Id = " + registro.Id);
				if (datos.executeNonQuery())
					return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				datos.closeConnection();
			}
			return false;
		}

	}
}