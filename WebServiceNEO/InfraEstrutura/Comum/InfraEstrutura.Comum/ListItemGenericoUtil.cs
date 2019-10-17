using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class ListItemGenericoUtil
    {
        private object id;
        private object nome;

        public ListItemGenericoUtil(object id, object nome)
        {
            this.id = id;
            this.nome = nome;
        }

        public object Id { get { return id; } set { id = value; } }
        public object Nome { get { return nome; } set { nome = value; } }


    }
}
