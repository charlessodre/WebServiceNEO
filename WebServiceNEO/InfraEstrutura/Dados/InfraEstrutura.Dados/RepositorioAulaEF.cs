using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioAulaEF : Repositorio<Dim_Aula>, IRepositorioAula
    {

        public Dim_Aula GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Aula.Where(x => x.id_aula == id).FirstOrDefault();
            }
        }

        public Dim_Aula GetByName(string nome)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Aula.Where(x => x.titulo_aula.ToUpper() == nome.ToUpper()).FirstOrDefault();
            }
        }

        public List<Dim_Aula> Buscar(Dim_Aula criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Aula.Where(x => x.titulo_aula == criterio.titulo_aula).OrderBy(x => x.titulo_aula);
                return query.ToList();

            }
        }


        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
