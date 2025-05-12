using GestionBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GestionBiblioteca.Controllers
{
    public class LibroController : Controller
    {
        // GET: LibroController
        public ActionResult Index()
        {
            MantenimientoLibro Mlibro = new MantenimientoLibro();
            var libros = Mlibro.ListarTodos();

            // Cargar autores asociados a cada libro
            foreach (var libro in libros)
            {
                libro.NombresAutores = Mlibro.ObtenerAutoresPorLibro(libro.CodLibro);
            }

            return View(libros);
        }

        // GET: LibroController/Details/5
        public ActionResult Details(string id)
        {
            MantenimientoLibro Mlibro = new MantenimientoLibro();
            var libro = Mlibro.Consultar(id);

            // Cargar autores asociados al libro
            libro.NombresAutores = Mlibro.ObtenerAutoresPorLibro(id);

            return View(libro);
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            // Enviar lista de autores para el <select multiple>
            ViewBag.Autores = new MantenimientoAutor().ListarTodos();
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MantenimientoLibro Mlibro = new MantenimientoLibro();

                    Libros libro = new Libros
                    {
                        CodLibro = collection["CodLibro"],
                        Titulo = collection["Titulo"],
                        Editorial = collection["Editorial"],
                        AnioPublicacion = string.IsNullOrEmpty(collection["AnioPublicacion"]) ? null : int.Parse(collection["AnioPublicacion"]),
                        ISBN = collection["ISBN"],
                        FechaIngreso = DateTime.Parse(collection["FechaIngreso"])
                    };

                    List<string> codAutores = collection["CodAutores"].ToString().Split(',').ToList();

                    Mlibro.Ingresar(libro, codAutores);
                    return RedirectToAction("Index");
                }

                ViewBag.Autores = new MantenimientoAutor().ListarTodos();
                return View();
            }
            catch
            {
                ViewBag.Autores = new MantenimientoAutor().ListarTodos();
                return View();
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(string id)
        {
            MantenimientoLibro Mlibro = new MantenimientoLibro();
            var libro = Mlibro.Consultar(id);

            ViewBag.Autores = new MantenimientoAutor().ListarTodos();
            ViewBag.AutoresSeleccionados = Mlibro.ObtenerAutoresPorLibro(id);

            return View(libro);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MantenimientoLibro Mlibro = new MantenimientoLibro();

                    Libros libro = new Libros
                    {
                        CodLibro = id,
                        Titulo = collection["Titulo"],
                        Editorial = collection["Editorial"],
                        AnioPublicacion = string.IsNullOrEmpty(collection["AnioPublicacion"]) ? null : int.Parse(collection["AnioPublicacion"]),
                        ISBN = collection["ISBN"],
                        FechaIngreso = DateTime.Parse(collection["FechaIngreso"])
                    };

                    List<string> codAutores = collection["CodAutores"].ToString().Split(',').ToList();

                    Mlibro.Modificar(libro, codAutores);
                    return RedirectToAction("Index");
                }

                ViewBag.Autores = new MantenimientoAutor().ListarTodos();
                ViewBag.AutoresSeleccionados = collection["CodAutores"].ToString().Split(',').ToList();
                return View();
            }
            catch
            {
                ViewBag.Autores = new MantenimientoAutor().ListarTodos();
                return View();
            }
        }

        // GET: LibroController/Delete/5
        public ActionResult Delete(string id)
        {
            MantenimientoLibro Mlibro = new MantenimientoLibro();
            var libro = Mlibro.Consultar(id);
            return View(libro);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                MantenimientoLibro Mlibro = new MantenimientoLibro();
                Mlibro.Borrar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
