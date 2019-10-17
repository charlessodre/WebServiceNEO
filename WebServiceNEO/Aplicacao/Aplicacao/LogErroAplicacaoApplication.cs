using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class LogErroAplicacaoApplication : ApplicationBase<LogErroAplicacao, Int32>
    {
        //public IServiceLogErroAplicacao serviceogErroAplicacao { get; set; }

        public IRepositorioLogErroAplicacao repositorioLogErroAplicacao { get; set; }
        public LogErroAplicacaoApplication() 
        {
            this.repositorio = repositorioLogErroAplicacao;
        }
        
    }
}
