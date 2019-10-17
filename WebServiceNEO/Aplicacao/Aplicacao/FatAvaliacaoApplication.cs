using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class FatAvaliacaoApplication : ApplicationBase<Fato_Avaliacao, string>
    {
        public IRepositorioFatAvaliacao repositorioAvaliacao { get; set; }
        public FatAvaliacaoApplication() 
        {
            this.repositorio = repositorioAvaliacao;
        }
        
    }
}
