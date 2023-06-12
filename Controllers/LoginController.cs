using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projetoGarmerMvcBd.Infra;
using projetoGarmerMvcBd.Models;

namespace projetoGarmerMvcBd.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [Route("Login")]
        public IActionResult Index(){
            /*Dar acesso a variavel Global*/
            ViewBag.Login = HttpContext.Session.GetString("UserName");
            return View();
        }

        [TempData]
        public string Mensagem {get;set;}
        Context acessoBd = new Context();

        [Route("Logar")]
        public IActionResult Logar (IFormCollection form){
            string email = form["Email"].ToString();
            string senha = form["Senha"].ToString();

            Jogador jogadorValido =  acessoBd.Jogador.FirstOrDefault(jogador => jogador.Email == email && jogador.Senha == senha);
            
           
            //Aqui precisamos implementar a seção
            //https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/app-state?view=aspnetcore-7.0
            if(jogadorValido != null){
                /*Variavel Global*/
                HttpContext.Session.SetString("UserName",jogadorValido.Nome);
                // Mensagem = "Logado com sucesso.";
                return LocalRedirect("~/");
            }

            Mensagem = "Dados Inválidos.";
            

            return LocalRedirect("~/Login/Login");
        }

        [Route("Logout")]

        public  IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");

            return LocalRedirect("~/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}