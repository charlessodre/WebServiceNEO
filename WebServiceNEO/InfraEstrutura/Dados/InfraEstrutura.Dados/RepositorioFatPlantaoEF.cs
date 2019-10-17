using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioFatPlantaoEF : Repositorio<Fato_Plantao>, IRepositorioFatPlantao
    {


        public Fato_Plantao GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.FatPlantao.Where(x => x.id_plantao == id).FirstOrDefault();
            }
        }

        public Fato_Plantao GetByName(string nome)
        {
            throw new NotImplementedException();
        }

        public Fato_Plantao GetbyIDs(string idAluno, string idPlantao)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.FatPlantao.Where(x => x.id_plantao == idPlantao && x.id_aluno_preceptor == idAluno).FirstOrDefault();
            }
        }

        public List<Fato_Plantao> Buscar(Fato_Plantao criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.FatPlantao.Where(x => x.id_plantao == criterio.id_plantao && x.id_aluno_preceptor == criterio.id_aluno_preceptor).OrderBy(x => x.id_plantao);
                return query.ToList();

            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }


    }
}
