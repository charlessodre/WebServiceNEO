using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class AlunoApplication : ApplicationBase<Dim_Aluno_Preceptor, string>
    {
        public IRepositorioAluno repositorioAluno { get; set; }
        public AlunoApplication() 
        {
            this.repositorio = repositorioAluno;
        }
        
    }
}
