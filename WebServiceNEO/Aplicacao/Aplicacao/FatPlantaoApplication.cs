using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class FatPlantaoApplication : ApplicationBase<Fato_Plantao, string>
    {
        public IRepositorioFatPlantao repositorioPlantao { get; set; }
        public FatPlantaoApplication() 
        {
            this.repositorio = repositorioPlantao;
        }
        
    }
}
