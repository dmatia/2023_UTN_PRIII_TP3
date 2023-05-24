namespace Dominio
{
    public class Marca : IAtributo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }

        public Marca()
        {
            this.Id = -1;
            this.Descripcion = string.Empty;
        }
    }
}
