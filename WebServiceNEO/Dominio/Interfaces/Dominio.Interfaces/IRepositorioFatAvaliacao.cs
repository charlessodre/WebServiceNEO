using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entity;

namespace Domain.Interfaces
{
    public interface IRepositorioFatAvaliacao : IRepositorio<Fato_Avaliacao, string>
    {
        Fato_Avaliacao GetbyIDs(string idAluno, string idAvaliacao);
    }
}
