using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetoGarmerMvcBd.Models
{
    public class Equipe
    {
        [Key]//Data Annotation - IdJogador
        public int IdEquipe { get; set; }

        public string Name { get; set; }

        public string Imagem { get; set; }

        //Icollection é uma referencia para enchergar a class jogadores
        //Referencia que a classe equipe vai ter acesso
        //a collection Jogador.
        public ICollection<Jogador> Jogador { get; set; }

    }
}