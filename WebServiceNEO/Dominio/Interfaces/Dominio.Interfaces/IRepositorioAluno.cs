using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;

namespace Domain.Interfaces
{
    public interface IRepositorioAluno : IRepositorio<Dim_Aluno_Preceptor, string>
    {
        Dim_Aluno_Preceptor GetbyIDs(string idAluno, string idMatricula, string idTurma);
    }
}
