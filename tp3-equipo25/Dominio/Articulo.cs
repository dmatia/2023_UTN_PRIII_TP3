using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public Decimal Precio { get; set; }
        public List<Imagen> Imagenes { get; set; }

        public Articulo()
        {
            this.Id = 0;
            this.Codigo = "";
            this.Nombre = "";
            this.Descripcion = "";
            this.Marca = new Marca();
            this.Categoria = new Categoria();
            this.Precio = 0;
        }
    }
}
