using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Service.Fabrica;

namespace Application
{
    public class ApplicationBase<Tipo,ID>
    {
        public ApplicationBase() 
        {
            foreach (PropertyInfo propriedade in this.GetType().GetProperties()) 
            {
                if (propriedade.Name.ToUpper().Contains("REPOSITORIO") || propriedade.Name.ToUpper().Contains("SERVICE")) 
                {
                    Type tipo = propriedade.PropertyType;
                    object obj = WindsorResolver.CreateInstance(tipo);
                    propriedade.SetValue(this, obj);
                }
            }      
        }
        public IRepositorio<Tipo, ID> repositorio;
        public virtual bool Insere(Tipo item)
        {
            return repositorio.Insert(item);
        }
        public virtual bool Altera(Tipo item)
        {
            return repositorio.Update(item);
        }
        public virtual bool AlteraOuInsere(Tipo item)
        {
            return repositorio.UpdateOrInsert(item);
        }
        public List<Tipo> ListAll()
        {
            return repositorio.ListAll();
        }
        public bool Delete(Tipo item)
        {
            return repositorio.DeleteItem(item);
        }
        public bool Delete(ID id) 
        {
            return repositorio.Delete(id);
        }
        public Tipo GetbyID(ID id) 
        {
            return repositorio.GetbyID(id);
        }
        public List<Tipo> Buscar(Tipo criterio) 
        {
            return repositorio.Buscar(criterio);
        }

    }
}
