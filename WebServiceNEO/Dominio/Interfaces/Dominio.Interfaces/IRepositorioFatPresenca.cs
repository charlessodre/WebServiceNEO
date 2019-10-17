using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;

namespace Domain.Interfaces
{
    public interface IRepositorioFatPresenca : IRepositorio<Fato_Presenca, string>
    {
        Fato_Presenca GetbyIDs(string idAluno, string idAula, string idTurma, string idMatricula);
    }
}
