using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class CursoApplication : ApplicationBase<Dim_Curso, string>
    {
        public IRepositorioCurso repositorioCurso { get; set; }

        public CursoApplication() 
        {
            this.repositorio = repositorioCurso;
        }
        
    }
}
