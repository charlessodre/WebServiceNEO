using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class TurmaApplication : ApplicationBase<Dim_Turma, string>
    {
        public IRepositorioTurma repositorioTurma { get; set; }
        public TurmaApplication() 
        {
            this.repositorio = repositorioTurma;
        }
        
    }
}
