namespace Dominio
{
    public class Categoria : IAtributo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }

        public Categoria()
        {
            this.Id = -1;
            this.Descripcion = string.Empty;
        }
    }
}
