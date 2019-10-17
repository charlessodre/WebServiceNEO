using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;

namespace Domain.Interfaces
{
    public interface IRepositorioFatPermanenciaPlataforma : IRepositorio<Fato_Permanencia_Plataforma, string>
    {
        Fato_Permanencia_Plataforma GetbyIDs(string idAluno, DateTime dataEntrada);
    }
}
