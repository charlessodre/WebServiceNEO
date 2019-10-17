using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioAlunoEF : Repositorio<Dim_Aluno_Preceptor>, IRepositorioAluno
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

        public Dim_Aluno_Preceptor GetbyID(string id)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Aluno.Where(x => x.id_aluno_preceptor == id).FirstOrDefault();
            }
        }

        public Dim_Aluno_Preceptor GetbyIDs(string idAluno, string idMatricula, string idTurma)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Aluno.Where(x => x.id_aluno_preceptor == idAluno && x.idmatricula_aluno == idMatricula && x.id_turma == idTurma).FirstOrDefault();
            }
        }

        public List<Dim_Aluno_Preceptor> Buscar(Dim_Aluno_Preceptor criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Aluno.Where(x => x.nome_aluno_preceptor == criterio.nome_aluno_preceptor).OrderBy(x => x.nome_aluno_preceptor);
                return query.ToList();

            }
        }




    }
}
