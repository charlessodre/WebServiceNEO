using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRepositorio<T, id>
    {
        List<T> ListAll();
        T GetbyID(id id);

        bool Update(T item);

        bool UpdateOrInsert(T item);

        bool Insert(T item);

        bool Delete(id id);

        bool DeleteItem(T item);

        List<T> Buscar(T criterio);
    }
}
