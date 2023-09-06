using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApplicationStore.Models;

namespace WebApplicationStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string CadenaConexion;

        public HomeController(ILogger<HomeController> logger, AccesoDatos cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Curso> cursos = new List<Curso>();

            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                using (SqlCommand command = new SqlCommand("ObtenerPreciosCursos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fecha", DateTime.Now.Date);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Curso curso = new Curso();
                            curso.Id = (int)reader["Id"];
                            curso.Nombre = reader["Nombre"].ToString();
                            curso.Descripcion = reader["Descripcion"].ToString();
                            curso.RutaImagen = "/Images/" + reader["Imagen"].ToString();
                            curso.Precios = new List<Precio>();

                            Precio precio = new Precio();
                            precio.PrecioCurso = (decimal)reader["Precio"];
                            curso.Precios.Add(precio);

                            cursos.Add(curso);
                        }
                    }
                }
            }

            return View(cursos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}