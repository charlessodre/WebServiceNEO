using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class FatPresencaApplication : ApplicationBase<Fato_Presenca, string>
    {
        public IRepositorioFatPresenca repositorioPresenca { get; set; }
        public FatPresencaApplication() 
        {
            this.repositorio = repositorioPresenca;
        }
        
    }
}
