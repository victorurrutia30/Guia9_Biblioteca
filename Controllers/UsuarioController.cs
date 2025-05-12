using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionBiblioteca.Models;

namespace GestionBiblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            // Definimos un objeto de tipo "MantenimientoUsuario"
            MantenimientoUsuario Musuario = new MantenimientoUsuario();

            // Llamamos al método "ListarTodos" y se lo pasamos a la Vista
            // El método "ListarTodos" retorna un objeto Lista de tipo "Usuario"
            return View(Musuario.ListarTodos());

        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            // Definimos un objeto de tipo "MantenimientoUsuario"
            MantenimientoUsuario Musuario = new MantenimientoUsuario();

            // Definimos un objeto de tipo "Usuario"
            // Le asignamos el objeto que retorna el método "Consultar" en la tabla "Usuarios"
            Usuario user = Musuario.Consultar(id);

            // Retornamos la vista con los datos del registro indicado
            return View(user);

        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MantenimientoUsuario Musuario = new MantenimientoUsuario();
                    // Definimos un objeto de tipo "Usuario"
                    Usuario user = new Usuario
                    {
                        CodUsuario = collection["codUsuario"],
                        Nombre = collection["nombre"],
                        Username = collection["username"],
                        Password = collection["password"],
                        FechaCreacion = collection["fechaCreacion"]
                    };

                    // Llamamos al método "Ingresar"
                    Musuario.Ingresar(user);

                    // Invocamos acción "Index"
                    return RedirectToAction("Index");
                }
                else
                {
                    // Si el modelo no es válido, retornamos a la vista de creación
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            // Definimos un objeto de tipo "MantenimientoUsuario"
            MantenimientoUsuario Musuario = new MantenimientoUsuario();

            // Definimos un objeto de tipo "Usuario"
            // Le asignamos el objeto que retorna el método "Consultar" en la tabla "Usuarios"
            Usuario user = Musuario.Consultar(id);

            // Retornamos la vista con los datos del registro indicado
            return View(user);

        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)  // Si todas las validaciones se cumplen
                {
                    // Definimos un objeto de tipo "MantenimientoUsuario"
                    MantenimientoUsuario Musuario = new MantenimientoUsuario();

                    // Definimos un objeto de tipo "Usuario"
                    Usuario user = new Usuario
                    {
                        CodUsuario = id,
                        Nombre = usuario.Nombre,
                        Username = usuario.Username,
                        Password = usuario.Password,
                        FechaCreacion = usuario.FechaCreacion
                    };

                    // Llamamos al método "Modificar"
                    Musuario.Modificar(user);

                    // Invocamos acción "Index"
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            // Definimos un objeto de tipo "MantenimientoUsuario"
            MantenimientoUsuario Musuario = new MantenimientoUsuario();

            // Definimos un objeto de tipo "Usuario"
            // Le asignamos el objeto que retorna el método "Consultar" en la tabla "Usuarios"
            Usuario user = Musuario.Consultar(id);

            // Retornamos la vista con los datos del registro indicado
            return View(user);

        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // Definimos un objeto de tipo "MantenimientoUsuario"
                MantenimientoUsuario Musuario = new MantenimientoUsuario();

                // Llamamos al método "Borrar"
                Musuario.Borrar(id);

                // Invocamos acción "Index"
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
    }
}
