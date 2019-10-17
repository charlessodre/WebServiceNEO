using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioCursoEF : Repositorio<Dim_Curso>, IRepositorioCurso
    {


        public Dim_Curso GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Curso.Where(x => x.id_curso == id).FirstOrDefault();
            }
        }

        public Dim_Curso GetByName(string nome)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Curso.Where(x => x.nome_curso.ToUpper() == nome.ToUpper()).FirstOrDefault();
            }
        }

        public List<Dim_Curso> Buscar(Dim_Curso criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Curso.Where(x => x.nome_curso == criterio.nome_curso).OrderBy(x => x.nome_curso);
                return query.ToList();

            }
        }


        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
