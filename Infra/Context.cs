using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projetoGarmerMvcBd.Models;

namespace projetoGarmerMvcBd.Infra
{
    /*Herdar uma classe nativa, contendo as configuraçoes para o banco de dados.*/
    public class Context:DbContext 
    {
        /*Obrigatorios ter um construtor vazio*/
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options){

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured){
                /*String de conexão com o bando*/
                /*so esta tendo o sql pois foram os pacotes que instalamos.*/
                /*Data Source => nome do usuario / servidor do gerenciador do banco*/
                /*initial catalog = gamerMvcBd => nome do banco de dados*/
                /*Integrated Security = true => autenticação pelo windows*/
                /*TrustServerCertificate = true => autenticação pelo windows*/


                /***********************************************************/

                /*
                Autenicação pelo SqlServer
                
                User Id = "Nome do usuario de login"
                pwd = "senha do seu usuario"
                */
                
                //Comando para acessaro BD em casa.
                optionsBuilder.UseSqlServer("Data Source = ALLANR1991\\SQLEXPRESS; Initial Catalog = gamerMvcBdManha ; User Id = sa; pwd = 123456; TrustServerCertificate = true");
                //Comando para acessar o BD no Senai
                //optionsBuilder.UseSqlServer("Data Source = NOTE06-S15; Initial Catalog = gamerMvcBdManha ; User Id = sa; pwd = Senai@134; TrustServerCertificate = true");
            }
            
        }

        public DbSet<Jogador> Jogador{get;set;}
        public DbSet<Equipe> Equipe{get;set;}

    }
}