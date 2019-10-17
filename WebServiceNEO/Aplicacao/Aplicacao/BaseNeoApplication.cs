using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class BaseNeoApplication : ApplicationBase<BaseNeo, string>
    {
         public IServiceBaseNeo serviceBaseNeo { get; set; }
         public BaseNeoApplication() 
        {
           
        }
    }
}
