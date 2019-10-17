using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioFatPresencaEF : Repositorio<Fato_Presenca>, IRepositorioFatPresenca
    {


        public Fato_Presenca GetbyID(string id)
        {
            throw new NotImplementedException();
        }

        public Fato_Presenca GetByName(string nome)
        {
            throw new NotImplementedException();
        }

        public Fato_Presenca GetbyIDs(string idAluno, string idAula, string idTurma, string idMatricula)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.FatPresenca.Where(x => x.id_aula == idAula && x.id_aluno_preceptor == idAluno && x.id_turma == idTurma && x.idmatricula_aluno == idMatricula).FirstOrDefault();
            }
        }

        public List<Fato_Presenca> Buscar(Fato_Presenca criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.FatPresenca.Where(x => x.id_aula == criterio.id_aula && x.id_aluno_preceptor == criterio.id_aluno_preceptor).OrderBy(x => x.id_aula);
                return query.ToList();

            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }


    }
}
