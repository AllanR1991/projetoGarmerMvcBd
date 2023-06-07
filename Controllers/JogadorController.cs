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
    [Route("[Controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context acessoBd = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Login = HttpContext.Session.GetString("UserName");
            
            ViewBag.Jogador = acessoBd.Jogador.ToList();
            ViewBag.Equipe = acessoBd.Equipe.ToList();
            return View();
        }

        [Route("Cadastrar")]

        public IActionResult Cadastrar(IFormCollection form)
        {
            /*Obter os dados do formúlario e execultar*/
            Jogador novoJogador = new Jogador();
            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            acessoBd.Jogador.Add(novoJogador);
            acessoBd.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Excluir")]

        public IActionResult Excluir (int id)
        {
            /*Pesquisa o Id no banco de dados*/
            Jogador jogador = acessoBd.Jogador.First(j =>j.IdJogador == id);
            /*acessa o banco de dados do jogador e remove o objeto encontrado acima.*/
            acessoBd.Jogador.Remove(jogador);
            acessoBd.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar (int id){

            ViewBag.Login = HttpContext.Session.GetString("UserName");

            /*Pesquisa o id no banco de dados*/
            Jogador jogadorEncontrada = acessoBd.Jogador.First(x=> x.IdJogador == id);
            /*Armazena o objeto Jogador na viewbag para usar.*/
            /*Viewbag varivel local*/
            ViewBag.Jogador = jogadorEncontrada;
            ViewBag.Equipe = acessoBd.Equipe.ToList();
            return View("Alterar");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar (IFormCollection form){

            /*Obtem os dados do formulario*/
            Jogador jogador = new Jogador();
            jogador.IdJogador = int.Parse(form["IdJogador"].ToString());
            jogador.Nome = form["Nome"].ToString();
            jogador.Email = form["Email"].ToString();
            jogador.Senha = form["Senha"].ToString();
            jogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());
            
            Jogador jogadorEncontrado = acessoBd.Jogador.First(x => x.IdJogador == jogador.IdJogador);

            jogadorEncontrado.Nome = jogador.Nome;
            jogadorEncontrado.Email = jogador.Email;
            jogadorEncontrado.Senha = jogador.Senha;
            jogadorEncontrado.IdEquipe = jogador.IdEquipe;

            acessoBd.Jogador.Update(jogadorEncontrado);

            acessoBd.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}