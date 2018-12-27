using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WSApplicationWeb.Models;
using WSApplicationWeb.WSPersonas;


namespace WSApplicationWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<PersonaView> personas = new List<PersonaView>();

            using (WSPersonasClient cliente = new WSPersonasClient())
            {
                var lista = cliente.listarPersonas();
                foreach (var item in lista)
                {
                    personas.Add(new PersonaView
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Edad = item.Edad,
                        DocumentoIdentificacion = item.DocumentoIdentificacion
                    });
                }
            }
            return View(personas);
        }

        [HttpPost]
        public ActionResult Index(Personas persona)
        {
            using (WSPersonasClient cliente = new WSPersonasClient())
            {
                var result = cliente.NuevaPersona(persona);

                if(result != 0)
                {
                    ViewBag.Insertado = "Persona registrada correctamente";
                }
                else
                {
                    ViewBag.Error = "No fue posible registrar a esta persona";
                }

                
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarPersona(int id)
        {
            using (WSPersonasClient cliente = new WSPersonasClient())
            {
                var result = cliente.buscarPersona(id);
                PersonaView persona = new PersonaView();
                if (result != null)
                {                  
                    persona.Id = result.Id;
                    persona.Nombre = result.Nombre;
                    persona.Apellido = result.Apellido;
                    persona.Edad = result.Edad;
                    persona.DocumentoIdentificacion = result.DocumentoIdentificacion;
                }

                return View(persona);
            }
        }

        [HttpPost]
        public ActionResult EditarPersona(PersonaView persona)
        {
            using (WSPersonasClient cliente = new WSPersonasClient())
            {
                Personas WSPersona = new Personas();
                WSPersona.Id = persona.Id;
                WSPersona.Nombre = persona.Nombre;
                WSPersona.Apellido = persona.Apellido;
                WSPersona.Edad = persona.Edad;
                WSPersona.DocumentoIdentificacion = persona.DocumentoIdentificacion;

                var result = cliente.EditarPersona(WSPersona);

                if(result != 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(persona);
                }
            }
        }

        public ActionResult EliminarPersona(int? id)
        {
            using (WSPersonasClient cliente = new WSPersonasClient())
            {
                var result = cliente.buscarPersona(Convert.ToInt32(id));
                PersonaView persona = new PersonaView();
                if (result != null)
                {
                    persona.Id = result.Id;
                    persona.Nombre = result.Nombre;
                    persona.Apellido = result.Apellido;
                    persona.Edad = result.Edad;
                    persona.DocumentoIdentificacion = result.DocumentoIdentificacion;
                }

                return View(persona);
            }
        }

        [HttpPost]
        public ActionResult EliminarPersona(int id)
        {
            using (WSPersonasClient cliente = new WSPersonasClient())
            {
                var result = cliente.EliminarPersona(id);

                if(result != 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var resultado = cliente.buscarPersona(id);
                    PersonaView persona = new PersonaView();
                    if (resultado != null)
                    {
                        persona.Id = resultado.Id;
                        persona.Nombre = resultado.Nombre;
                        persona.Apellido = resultado.Apellido;
                        persona.Edad = resultado.Edad;
                        persona.DocumentoIdentificacion = resultado.DocumentoIdentificacion;
                    }

                    return View(persona);
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}