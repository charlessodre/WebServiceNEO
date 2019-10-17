using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    [Serializable]
    public class DominioGenerico
    {
        String codigo;
        String nome;
        bool selecionado;

        public String Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public bool Selecionado
        {
            get { return selecionado; }
            set { selecionado = value; }
        }
    }
}
