using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class AulaApplication : ApplicationBase<Dim_Aula, string>
    {
        public IRepositorioAula repositorioAula { get; set; }
        public AulaApplication() 
        {
            this.repositorio = repositorioAula;
        }
        
    }
}
