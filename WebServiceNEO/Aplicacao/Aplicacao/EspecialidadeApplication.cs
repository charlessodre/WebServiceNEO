using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class EspecialidadeApplication : ApplicationBase<Dim_Especialidade, string>
    {
        public IRepositorioEspecialidade repositorioEspecialidade { get; set; }
        public EspecialidadeApplication() 
        {
            this.repositorio = repositorioEspecialidade;
        }
        
    }
}
