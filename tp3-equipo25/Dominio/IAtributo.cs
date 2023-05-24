namespace Dominio
{
    public interface IAtributo
    {
        int Id { get; set; }
        string Descripcion { get; set; }
        string ToString();
    }
}
