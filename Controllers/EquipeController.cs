using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projetoGarmerMvcBd.Infra;

namespace projetoGarmerMvcBd.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        Context acessoBd = new Context();

        [Route("Listar")]// http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            /*acessoBd.Equipe.ToList() => Retorna os dados da equipe*/
            /*ViewBag.Equipe => bag de equipe*/
            ViewBag.Equipe = acessoBd.Equipe.ToList();
            /*Retorna a view da equipe (tela)*/
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}