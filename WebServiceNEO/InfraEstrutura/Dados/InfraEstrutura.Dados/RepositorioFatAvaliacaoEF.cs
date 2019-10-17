using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioFatAvaliacaoEF : Repositorio<Fato_Avaliacao>, IRepositorioFatAvaliacao
    {


        public Fato_Avaliacao GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.FatAvaliacao.Where(x => x.id_avaliacao == id).FirstOrDefault();
            }
        }

        public Fato_Avaliacao GetByName(string nome)
        {
            throw new NotImplementedException();
        }

        public Fato_Avaliacao GetbyIDs(string idAluno, string id_avaliacao)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.FatAvaliacao.Where(x => x.id_avaliacao == id_avaliacao && x.id_aluno_preceptor == idAluno).FirstOrDefault();
            }
        }

        public List<Fato_Avaliacao> Buscar(Fato_Avaliacao criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.FatAvaliacao.Where(x => x.id_avaliacao == criterio.id_avaliacao && x.id_aluno_preceptor == criterio.id_aluno_preceptor).OrderBy(x => x.id_avaliacao);
                return query.ToList();

            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }


    }
}
