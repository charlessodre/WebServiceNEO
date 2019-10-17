using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class ProfessorApplication : ApplicationBase<Dim_Professor, string>
    {
        public IRepositorioProfessor repositorioProfessor { get; set; }
        public ProfessorApplication() 
        {
            this.repositorio = repositorioProfessor;
        }
        
    }
}
