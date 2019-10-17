using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class ModuloApplication : ApplicationBase<Dim_Modulo, string>
    {
        public IRepositorioModulo repositorioModulo { get; set; }

        public ModuloApplication() 
        {
            this.repositorio = repositorioModulo;
        }
        
    }
}
