namespace WebApplicationStore.Models
{
    public class Precio
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioCurso { get; set; }
    }
}
