using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioModuloEF : Repositorio<Dim_Modulo>, IRepositorioModulo
    {


        public Dim_Modulo GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Modulo.Where(x => x.id_modulo == id).FirstOrDefault();
            }
        }

        public Dim_Modulo GetByName(string nome)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Modulo.Where(x => x.nome_modulo.ToUpper() == nome.ToUpper()).FirstOrDefault();
            }
        }

        public List<Dim_Modulo> Buscar(Dim_Modulo criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Modulo.Where(x => x.nome_modulo == criterio.nome_modulo).OrderBy(x => x.nome_modulo);
                return query.ToList();

            }
        }



        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
