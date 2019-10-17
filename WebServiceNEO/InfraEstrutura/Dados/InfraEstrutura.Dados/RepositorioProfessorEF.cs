using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioProfessorEF : Repositorio<Dim_Professor>, IRepositorioProfessor
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

        public Dim_Professor GetbyID(string id)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Professor.Where(x =>x.id_professor == id).FirstOrDefault();
            }
        }

        public List<Dim_Professor> Buscar(Dim_Professor criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Professor.Where(x => x.nome_professor == criterio.nome_professor).OrderBy(x => x.nome_professor);
                return query.ToList();

            }
        }


    }
}
