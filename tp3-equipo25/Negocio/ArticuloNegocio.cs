using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>(); // crea una instancia de lista de Articulos
            AccesoDB datos = new AccesoDB();

            /// Seteo consulta de todos los elementos de la tabla integrada

            try// manejo de excepciones al intentar acceder a DB
            {
                datos.setQuery("Select A.Id,Codigo,Nombre,Precio,A.Descripcion, M.Id as 'MarcasId', M.Descripcion as 'MarcasDescripcion', C.Descripcion as 'CategoriasDescripcion', C.Id as 'CategoriasId' from Articulos A left join Marcas M on M.Id=A.IdMarca left join Categorias C on C.Id =A.IdCategoria");
                datos.executeReader();
                // la seleccion esta en Lector
                while ((datos.Reader.Read())) // Devuelve valor booleano y va cambiando el cursor
                {
                    Articulo aux = new Articulo(); // Crea una instancia de variable Articulo
                    aux.Id = (int)datos.Reader["Id"]; // Carga la Variable Articulo con los datos de la base de datos
                    aux.Codigo = (string)datos.Reader["Codigo"];                 //aux.= lector.GetInt32();
                    aux.Nombre = (string)datos.Reader["Nombre"];
                    aux.Descripcion = (string)datos.Reader["Descripcion"]; // validaciones sobre descripcion y otros
                    aux.Precio = Math.Round(Convert.ToDecimal(datos.Reader["Precio"]), 2); /// Ver tema del float y el casteo de money
                    aux.Marca = new Marca();
                    if (!(datos.Reader["MarcasId"] is DBNull))
                    aux.Marca.Id = (int)datos.Reader["MarcasId"];
                    if (!(datos.Reader["MarcasDescripcion"] is DBNull))
                    aux.Marca.Descripcion = (string)datos.Reader["MarcasDescripcion"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Reader["CategoriasDescripcion"] is DBNull))
                    aux.Categoria.Descripcion = (string)datos.Reader["CategoriasDescripcion"];
                                    
                    if (!(datos.Reader["CategoriasId"] is DBNull))
                    aux.Categoria.Id = (int)datos.Reader["Categoriasid"];
                
					//Cargar Imágenes
					ImagenNegocio imagenNegocio = new ImagenNegocio();
					aux.Imagenes = imagenNegocio.listar(aux.Id.ToString());

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

        //FILTRAR PRODUCTOS
        public List<Articulo> Filtrar (string Busqueda, string Filtroprimario, string Categoria, string Marca) /// reutilizarlas y combinarlas
        {
            // pasar las comparaciones a toupper
            List<Articulo> lista = new List<Articulo>(); // crea una instancia de lista de Articulos
            AccesoDB datos = new AccesoDB();
            
            string select = "Select A.Id,Codigo,Nombre,Precio, A.Descripcion, M.Id as 'MarcasId', M.Descripcion as 'MarcasDescripcion', C.Descripcion as 'CategoriasDescripcion', C.Id as 'CategoriasId' from Articulos A left join Marcas M on M.Id=A.IdMarca left join Categorias C on C.Id =A.IdCategoria ";
            string Restriccion="";
            /// Seteo consulta de todos los elementos de la tabla integrada
            try// manejo de excepciones al intentar acceder a DB
            {

                if ((Busqueda == "Ingrese busqueda..." || Busqueda == string.Empty) && Filtroprimario == "Filtros disponibles")
                {
                    if (Categoria == "Categorias" && Marca == "Marcas")
                    {
                        return listar();
                    }
                    else if(Categoria != "Categorias" && Marca == "Marcas")
                    {
                        Restriccion = "where C.Descripcion like '" + Categoria + "'";
                     }
                    
                    else if(Categoria == "Categorias" && Marca != "Marcas")
                    {
                        Restriccion = "where M.Descripcion like '" + Marca + "'";
                    }
                    else
                    {
                        Restriccion = "where M.Descripcion like '"+Marca+"' and C.Descripcion like '" + Categoria + "'";

                    }
                    
                }


                if (Filtroprimario == "Nombre" || Filtroprimario == "Codigo" || Filtroprimario == "Descripcion")
                    {
                        if (Filtroprimario == "Descripcion") // SI ES DESCRIPCION HAY QUE CAMBIAR LA QUERY
                        {
                            Restriccion = "where A.Descripcion like '%" + Busqueda + "%'";
                            // SI CATEGORIAS O MARCAS CAMBIO EL TEXTO AGREGA RESTRICCION        
                            if (Categoria != "Categorias")
                            {

                                Restriccion += " and C.Descripcion like '" + Categoria + "'";

                            }
                            if (Marca != "Marcas")
                            {

                                Restriccion += " and M.Descripcion like '" + Marca + "'";

                            }

                        }
                        else // SI NO ES DESCRIPCION SE USA EL NOMBRE DEL FILTRO EN LA QUERY
                        {

                            Restriccion = "where " + Filtroprimario + " like '%" + Busqueda + "%'";

                            // SI CATEGORIAS O MARCAS CAMBIO EL TEXTO AGREGA RESTRICCION
                            if (Categoria != "Categorias")
                            {

                                Restriccion += " and C.Descripcion like '" + Categoria + "'";

                            }
                            if (Marca != "Marcas")
                            {

                                Restriccion += " and M.Descripcion like '" + Marca + "'";

                            }

                        }


                    }
                    else if (Filtroprimario == "Precio mayor a")
                    {

                        Restriccion += "where Precio > " + Busqueda;

                        if (Categoria != "Categorias")
                        {

                            Restriccion += " and C.Descripcion like '" + Categoria + "'";

                        }
                        if (Marca != "Marcas")
                        {

                            Restriccion += " and M.Descripcion like '" + Marca + "'";

                        }

                    }
                    else if (Filtroprimario == "Precio menor a")
                    {

                        Restriccion += "where Precio < " + Busqueda;
                        if (Categoria != "Categorias")
                        {

                            Restriccion += " and C.Descripcion like '" + Categoria + "'";

                        }
                        if (Marca != "Marcas")
                        {

                            Restriccion += " and M.Descripcion like '" + Marca + "'";

                        }
                    }


                    datos.setQuery(select + Restriccion);
                    datos.executeReader();
                    while ((datos.Reader.Read())) 
                    {
                        Articulo aux = new Articulo(); 
                    if (!(datos.Reader["id"] is DBNull))
                        aux.Id = (int)datos.Reader["Id"];
                    if (!(datos.Reader["id"] is DBNull))
                        aux.Codigo = (string)datos.Reader["Codigo"];
                    if (!(datos.Reader["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Reader["Nombre"];
                    if (!(datos.Reader["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Reader["Descripcion"];
                    if (!(datos.Reader["Precio"] is DBNull))
                        aux.Precio = Math.Round(Convert.ToDecimal(datos.Reader["Precio"]), 2);

                    aux.Marca = new Marca();
                    if (!(datos.Reader["MarcasId"] is DBNull))
                        aux.Marca.Id = (int)datos.Reader["MarcasId"];
                    if (!(datos.Reader["MarcasDescripcion"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Reader["MarcasDescripcion"];

                    aux.Categoria = new Categoria();
                        if (!(datos.Reader["CategoriasDescripcion"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Reader["CategoriasDescripcion"];

                    if (!(datos.Reader["CategoriasId"] is DBNull))
                  
                      aux.Categoria.Id = (int)datos.Reader["Categoriasid"];
                                             
					 
                    
                    //Cargar Imágenes
                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    aux.Imagenes = imagenNegocio.listar(aux.Id.ToString());

					    lista.Add(aux);// Agrega cada variable a la lista
                    }

                    return lista;// devuelve la lista

                
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
           
              
       
        public int agregar(Articulo articulo)
        {
            AccesoDB datoSQL = new AccesoDB();

            try
            {
                datoSQL.setQuery
                 (
                    $"INSERT INTO ARTICULOS " +
                    $"(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                    $"VALUES('{articulo.Codigo}', '{articulo.Nombre}', '{articulo.Descripcion}', {articulo.Marca.Id}, {articulo.Categoria.Id}, {articulo.Precio.ToString(new CultureInfo("en-US"))}) " +
					"SELECT SCOPE_IDENTITY()"
                );

                return datoSQL.executeScalar();

                //if (datoSQL.executeNonQuery())
                //{
                //    datoSQL.closeConnection();
                //    return true;
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


        //MODIFICAR PRODUCTOS
        public bool modificar(Articulo articulo)
        {
            AccesoDB datoSQL = new AccesoDB();
            try
            {
                datoSQL.setQuery(
                "UPDATE ARTICULOS " +
                $"SET Codigo = '{articulo.Codigo}', Nombre = '{articulo.Nombre}', Descripcion = '{articulo.Descripcion}', " +
                $"Precio = {articulo.Precio.ToString(new CultureInfo("en-US"))}, IdMarca = {articulo.Marca.Id}, IdCategoria = {articulo.Categoria.Id} " +
                $"WHERE Id = {articulo.Id}"
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

        //TODO: ELIMINAR PRODUCTO
        public bool eliminar(int Id)
        {
            AccesoDB datos = new AccesoDB(); 
            try
            {
               
                datos.setQuery("DELETE FROM ARTICULOS WHERE Id = @id");
                datos.setParameter("@id", Id);

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



