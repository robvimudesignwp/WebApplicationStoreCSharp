namespace WebApplicationStore.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public List<Precio> Precios { get; set; }
    }
}
