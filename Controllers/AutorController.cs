using GestionBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionBiblioteca.Controllers
{
    public class AutorController : Controller
    {
        // GET: MantenimientoAutor
        public ActionResult Index()
        {
            var mantenimiento = new MantenimientoAutor();
            return View(mantenimiento.ListarTodos());
        }

        // GET: MantenimientoAutor/Details/5
        public ActionResult Details(string id)
        {
            var mantenimiento = new MantenimientoAutor();
            return View(mantenimiento.Consultar(id));
        }

        // GET: MantenimientoAutor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantenimientoAutor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                var mantenimiento = new MantenimientoAutor();
                mantenimiento.Ingresar(autor);
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: MantenimientoAutor/Edit/5
        public ActionResult Edit(string id)
        {
            var mantenimiento = new MantenimientoAutor();
            return View(mantenimiento.Consultar(id));
        }

        // POST: MantenimientoAutor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Autor autor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mantenimiento = new MantenimientoAutor();

                    // Asegurarse de que el ID se respete (por si el campo oculto no se llena)
                    autor.CodAutor = id;

                    mantenimiento.Modificar(autor);
                    return RedirectToAction(nameof(Index));
                }
                return View(autor);
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoAutor/Delete/5
        public ActionResult Delete(string id)
        {
            var mantenimiento = new MantenimientoAutor();
            return View(mantenimiento.Consultar(id));
        }

        // POST: MantenimientoAutor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var mantenimiento = new MantenimientoAutor();
                mantenimiento.Borrar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
