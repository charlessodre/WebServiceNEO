using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RepositorioTurmaEF : Repositorio<Dim_Turma>, IRepositorioTurma
    {


        public Dim_Turma GetbyID(string id)
        {

            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Turma.Where(x => x.id_turma == id).FirstOrDefault();
            }
        }

        public Dim_Turma GetByName(string nome)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {
                return context.Turma.Where(x => x.nome_turma.ToUpper() == nome.ToUpper()).FirstOrDefault();
            }
        }

        public List<Dim_Turma> Buscar(Dim_Turma criterio)
        {
            using (WSNEOEntities context = new WSNEOEntities())
            {

                var query = context.Turma.Where(x => x.nome_turma == criterio.nome_turma).OrderBy(x => x.nome_turma);
                return query.ToList();

            }
        }


        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
