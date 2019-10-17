using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces;

namespace Application
{
    public class HospitalApplication : ApplicationBase<Dim_Hospital, string>
    {
        public IRepositorioHospital repositorioHospital { get; set; }

        public HospitalApplication() 
        {
            this.repositorio = repositorioHospital;
        }
        
    }
}
