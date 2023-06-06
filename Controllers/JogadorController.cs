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
            ViewBag.Jogador = acessoBd.Jogador.ToList();
            ViewBag.Equipe = acessoBd.Equipe.ToList();
            return View();
        }

        [Route("Cadastrar")]

        public IActionResult Cadastrar(IFormCollection form)
        {
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
            Jogador jogador = acessoBd.Jogador.First(j =>j.IdJogador == id);
            acessoBd.Jogador.Remove(jogador);
            acessoBd.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar (int id){
            Jogador jogadorEncontrada = acessoBd.Jogador.First(x=> x.IdJogador == id);
            ViewBag.Jogador = jogadorEncontrada;
            ViewBag.Equipe = acessoBd.Equipe.ToList();
            return View("Alterar");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar (IFormCollection form){
            Equipe equipe = new Equipe();
            equipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());
            equipe.Name = form["Name"].ToString();
            if(form.Files.Count > 0 ){
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder,file.FileName);

                using(var stream = new FileStream(path, FileMode.Create)){
                    file.CopyTo(stream);
                }

                equipe.Imagem =file.FileName;
            }else{
                equipe.Imagem = "padrao.png";
            }

            Equipe equipeEncontrada = acessoBd.Equipe.First(x => x.IdEquipe == equipe.IdEquipe);

            equipeEncontrada.Name = equipe.Name;
            equipeEncontrada.Imagem = equipe.Imagem;

            acessoBd.Equipe.Update(equipeEncontrada);

            acessoBd.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}