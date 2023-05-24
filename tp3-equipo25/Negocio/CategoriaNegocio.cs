using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio : IAtributosNegocio
    {

        // METODO LISTAR TODAS LAS CATEGORIAS EN DB
        public List<IAtributo> listar()
        {
            List<IAtributo> lista = new List<IAtributo>();
            AccesoDB datos = new AccesoDB();

            try
            {
                datos.setQuery("SELECT Id, Descripcion FROM Categorias");
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (Int32)datos.Reader["Id"];
                    if (!(datos.Reader["Descripcion"] is DBNull))
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

        //  METODO AGREGAR CATEGORIA
        public bool agregar(IAtributo nueva)
        {
            AccesoDB datos = new AccesoDB();

            try
            {
                datos.setQuery("Insert into CATEGORIAS (Descripcion) values ('" + nueva.Descripcion + "')");
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

        // METODO MODIFICAR CATEGORIA
        public bool modificar(IAtributo modificar)
        {
            AccesoDB datos = new AccesoDB();

            try
            {
                datos.setQuery("Update CATEGORIAS set Descripcion = @desc WHERE Id = @id");
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

        // METODO ELIMINAR CATEGORIA
        public bool eliminar(IAtributo registro) { 
            AccesoDB datos = new AccesoDB();

            try
            {
                datos.setQuery("DELETE CATEGORIAS WHERE Id = " + registro.Id);
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