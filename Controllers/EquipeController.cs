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
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        Context acessoBd = new Context();

        [Route("Listar")]// http://localhost/Equipe/Listar 
        //rota que chama o index
        public IActionResult Index()
        {
            ViewBag.Login = HttpContext.Session.GetString("UserName");
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

        [Route("Cadastar")]
        public IActionResult Cadastrar(IFormCollection form)
        {

            /*Instancia um objeto Equipe*/
            Equipe novaEquipe = new Equipe();

            /*Atribuição de valores recebidos do formulario.*/
            novaEquipe.Name = form["Nome"].ToString();
            // novaEquipe.Imagem = form["Imagem"].ToString();

            /*
        Aqui estava chegando como string(não queremos assim)
        novaEquipe.Imagem = form["Imagem"].ToString();

        Inicio da logica do upload da imagem
        */

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Equipes");

                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/",folder,file.FileName);

                //Using para que a instrução dentro dele seja encerrado assim que for executada
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;

            }else{
                novaEquipe.Imagem = "padrao.png";
            }

            /*Adiciona objeto na tabela do banco de dados*/
            acessoBd.Add(novaEquipe);
            /*Salva alterações no banco de dados*/
            acessoBd.SaveChanges();
            /*Atualiza a lista, !!! Testar sem essa atualização*/
            // ViewBag.Equipe = acessoBd.Equipe.ToList(); não necessario  
            /*Retorna para o Local chamando a toda de Listar(método Index)*/
            return LocalRedirect("~/Equipe/Listar");
        }
        /*Sempre que for usar um parametro em uma função deve ter tambem um parametro na rota.*/
        [Route("Excluir/{id}")]
        public IActionResult Excluir (int id){
            /*FirstOrDefault(e => e.IdEquipe == id) retornar o primeiro objeto que encontrar.*/
            Equipe equipeEncontrada = acessoBd.Equipe.FirstOrDefault(e => e.IdEquipe == id);
            acessoBd.Remove(equipeEncontrada);
            acessoBd.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar (int id){
            ViewBag.Login = HttpContext.Session.GetString("UserName");

            Equipe equipeEncontrada = acessoBd.Equipe.First(x=> x.IdEquipe == id);
            ViewBag.Equipe = equipeEncontrada;
            return View("Alterar");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar (IFormCollection form){
            //objeto com os dados do formulario antigo
                Equipe equipe= new Equipe();
  
                equipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

                equipe.Name= form["Name"].ToString();
                
               
                if (form.Files.Count > 0)
            {
            
                 var file = form.Files[0];
                
                var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var pathCaminho = Path.Combine(folder, file.FileName);

                
                using (var stream =new FileStream(pathCaminho, FileMode.Create))
                {
                    file.CopyTo(stream);

                }

                  equipe.Imagem =file.FileName;
            }

                 else
                {
                    equipe.Imagem="padrao.png";
                }

                Equipe equipeEncontrada = acessoBd.Equipe.First(x=> x.IdEquipe == equipe.IdEquipe);

                equipeEncontrada.Name= equipe.Name;
                equipeEncontrada.Imagem=equipe.Imagem;

                acessoBd.Equipe.Update(equipeEncontrada);
                acessoBd.SaveChanges();

                return LocalRedirect("~/Equipe/Listar");

            }
    }
}