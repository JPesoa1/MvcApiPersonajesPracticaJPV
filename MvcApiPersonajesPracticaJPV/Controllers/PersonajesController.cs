using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesPracticaJPV.Models;
using MvcApiPersonajesPracticaJPV.Services;

namespace MvcApiPersonajesPracticaJPV.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonaje service;

        public PersonajesController(ServicePersonaje service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }
        public async Task<IActionResult> Details(int id)
        {
            Personaje personajes = await this.service.FindPersonajeAsync(id);
            return View(personajes);
        }


        public async Task<IActionResult> Create()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje.Nombre, personaje.Imagen, personaje.IdSerie);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);

            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync(personaje.IdPersoanje,personaje.Nombre, personaje.Imagen, personaje.IdSerie);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);

            return RedirectToAction("Index");
        }

    }
}
