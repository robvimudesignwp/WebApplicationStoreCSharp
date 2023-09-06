using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplicationStore.Models;

namespace WebApplicationStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private string CadenaConexion;

        public AccountController(ILogger<AccountController> logger, AccesoDatos cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            _logger = logger;
        }
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CrearUsuario(Cliente c)
        {
            //Validamos datos del formulario
            if (string.IsNullOrEmpty(c.Email))
            {
                ModelState.AddModelError("email", "El campo email es obligatorio");
            }
            if (string.IsNullOrEmpty(c.Clave))
            {
                ModelState.AddModelError("clave", "El campo clave es obligatorio");
            }

            //Si los datos son correctos procedemos a registrar el nuevo usuario
            if (ModelState.IsValid)
            {
				//registrar el nuevo usuario en la DDBB
				using (SqlConnection connection = new SqlConnection(CadenaConexion))
                {
					using (SqlCommand command = new SqlCommand("ClienteRegistro", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@email", c.Email);
                        command.Parameters.AddWithValue("@clave", c.Clave);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

				}

                //Redirigimos a la página de inicio
                return RedirectToAction("Index", "Home");

			}

            //Si los datos no son válidos, volver a mostrar el formulario con los errores de validación
            return View("CrearUsuario");
        }
    }
}
