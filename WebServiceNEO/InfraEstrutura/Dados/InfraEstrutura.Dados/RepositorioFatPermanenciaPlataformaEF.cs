using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioFatPermanenciaPlataformaEF : Repositorio<Fato_Permanencia_Plataforma>, IRepositorioFatPermanenciaPlataforma
    {


        public Fato_Permanencia_Plataforma GetbyID(string id)
        {
            throw new NotImplementedException();
        }

        public Fato_Permanencia_Plataforma GetByName(string nome)
        {
            throw new NotImplementedException();
        }

        public Fato_Permanencia_Plataforma GetbyIDs(string idAluno, DateTime dataEntrada)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                //return context.FatPermanenciaPlataforma.Where(x => x.id_aluno_preceptor == idAluno && x.id_turma == idTurma && x.idmatricula_aluno == idMatricula).FirstOrDefault();

                return context.FatPermanenciaPlataforma.Where(x => x.id_aluno_preceptor == idAluno && x.data_entrada == dataEntrada).FirstOrDefault();
            }
        }

        public List<Fato_Permanencia_Plataforma> Buscar(Fato_Permanencia_Plataforma criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.FatPermanenciaPlataforma.Where(x => x.id_aluno_preceptor == criterio.id_aluno_preceptor).OrderBy(x => x.id_aluno_preceptor);
                return query.ToList();

            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }


    }
}
