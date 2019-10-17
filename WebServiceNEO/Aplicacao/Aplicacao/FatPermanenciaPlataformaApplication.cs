using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class FatPermanenciaPlataformaApplication : ApplicationBase<Fato_Permanencia_Plataforma, string>
    {
        public IRepositorioFatPermanenciaPlataforma repositorioPermanenciaPlataforma { get; set; }
        public FatPermanenciaPlataformaApplication() 
        {
            this.repositorio = repositorioPermanenciaPlataforma;
        }
        
    }
}
