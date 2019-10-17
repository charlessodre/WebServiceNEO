using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioEspecialidadeEF : Repositorio<Dim_Especialidade>, IRepositorioEspecialidade
    {
        //public Dim_Curso GetByName(string nome)
        //{
        //    using (WSNEOEntities context = new WSNEOEntities())
        //    {
        //        return context.Curso.Where(x => x.nome_curso.ToUpper() == nome.ToUpper()).FirstOrDefault();
        //    }
        //}


        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Dim_Especialidade GetbyID(string id)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Especialidade.Where(x => x.id_especialidade == id).FirstOrDefault();
            }
        }

        public List<Dim_Especialidade> Buscar(Dim_Especialidade criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Especialidade.Where(x => x.nome_especialidade == criterio.nome_especialidade).OrderBy(x => x.nome_especialidade);
                return query.ToList();

            }
        }


    }
}
