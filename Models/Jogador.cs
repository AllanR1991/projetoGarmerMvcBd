using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projetoGarmerMvcBd.Models
{
    public class Jogador
    {
        /*Data Notation validator C#*/
        [Key]//Data Annotation - IdJogador
        public int IdJogador { get; set; }
        
        public string Nome { get; set; }
        
        public string Email { get; set; }
        
        public string Senha { get; set; }
        
        
        [ForeignKey("Equipe")]//Data Notation referenciando a tabela ForeignKey.
        public int IdEquipe { get; set; }
        
        
    }
}