using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Interfaces;

namespace Domain.Service
{
    public abstract class Service <Tipo> where Tipo : class
    {
        protected IRepositorio<Tipo, int> repositorio;
        public virtual bool Insere(Tipo item) 
        {
            return repositorio.Insert(item);
        }
        public virtual bool Altera(Tipo item) 
        {
            return repositorio.Update(item);
        }
        public List<Tipo> ListarTodos() 
        {
            return repositorio.ListAll();
        }
        public bool Delete(Tipo item) 
        {
            return repositorio.DeleteItem(item);
        }
    }
}
