using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;

namespace Domain.Interfaces
{
    public interface IRepositorioFatPlantao : IRepositorio<Fato_Plantao, string>
    {
        Fato_Plantao GetbyIDs(string idAluno, string idPlantao);
    }
}
