using Microsoft.AspNetCore.Mvc;
using ContactoDb.Datos;
using ContactoDb.Models;

namespace ContactoDb.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        ContactoDatos contactoDatos = new ContactoDatos();

        //Holamundo Ya termine
        public IActionResult Listar()
        {
            var lista = contactoDatos.ListarContacto();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Guardar() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ContactoModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = contactoDatos.GuardarContacto(model);
            if (respuesta)
                return RedirectToAction("Listar");
            else 
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Editar(int IdContacto) 
        {
            ContactoModel contacto = contactoDatos.ObtenerContacto(IdContacto); 
            return View(contacto);
        }
        [HttpPost]
        public IActionResult Editar(ContactoModel model)
        {
            var resultado = contactoDatos.EditarContacto(model);
            if (resultado)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Eliminar(int idContacto)
        {
            var contacto = contactoDatos.ObtenerContacto(idContacto);
            return View(contacto);
        }
        [HttpPost]
        public IActionResult Eliminar(ContactoModel model)
        {
            var respuesta = contactoDatos.EliminarContacto(model);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
        
    }
}
