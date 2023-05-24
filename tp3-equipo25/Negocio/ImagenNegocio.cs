using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ImagenNegocio
    {

        // METODO LISTAR - Busca por articulo 
        public List<Imagen> listar(string IdArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDB datos = new AccesoDB();

            try
            {
                datos.setQuery("SELECT Id, IdArticulo, ImagenUrl FROM Imagenes WHERE IdArticulo = " + IdArticulo);
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (Int32)datos.Reader["Id"];
                    aux.IdArticulo = (int)datos.Reader["IdArticulo"];
                    aux.UrlImagen = (string)datos.Reader["ImagenUrl"];
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


		//AGREGAR IMAGEN
		public int agregar(Imagen imagen)
		{
			AccesoDB datoSQL = new AccesoDB();

			try
			{
				datoSQL.setQuery
				 (
					$"INSERT INTO IMAGENES " +
					$"(IdArticulo,ImagenUrl) " +
					$"VALUES('{imagen.IdArticulo}', '{imagen.UrlImagen}')" +
					"SELECT SCOPE_IDENTITY()"
				);

				return datoSQL.executeScalar();

				//if (datoSQL.executeNonQuery())
				//{
				//	datoSQL.closeConnection();
				//	return true;
				//}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datoSQL.closeConnection();
			}

			//return false;
		}


		//ELIMINAR IMAGEN
		public bool borrar(int id)
		{
			AccesoDB datoSQL = new AccesoDB();

			try
			{
				datoSQL.setQuery
				 (
					$"DELETE FROM IMAGENES " +
					$"WHERE Id = '{id}'"
				);

				if (datoSQL.executeNonQuery())
				{
					datoSQL.closeConnection();
					return true;
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datoSQL.closeConnection();
			}

			return false;
		}

	}
}