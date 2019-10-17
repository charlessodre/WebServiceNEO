using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioHospitalEF : Repositorio<Dim_Hospital>, IRepositorioHospital
    {
        public Dim_Hospital GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Hospital.Where(x =>  x.id_hospital == id).FirstOrDefault();
            }
        }

        public Dim_Hospital GetByName(string nome)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Hospital.Where(x => x.nome_hospital.ToUpper() == nome.ToUpper()).FirstOrDefault();
            }
        }

        public List<Dim_Hospital> Buscar(Dim_Hospital criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Hospital.Where(x => x.nome_hospital == criterio.nome_hospital).OrderBy(x => x.nome_hospital);
                return query.ToList();

            }
        }


        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
