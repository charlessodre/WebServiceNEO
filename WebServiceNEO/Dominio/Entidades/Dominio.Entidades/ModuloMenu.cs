using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entity
{
    [Serializable]
    public class ModuloMenu
    {
        Int32 codigo;
        Int32 codigoMenu;
        String codigoPai;
        String nomeModulo;
        Int32 sequencia;
        String urlAcesso;
        String ativo;
        String descricao;
        int privilegio;

        public int Privilegio
        {
            get { return privilegio; }
            set { privilegio = value; }
        }

        public Int32 Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public Int32 CodigoMenu
        {
            get { return codigoMenu; }
            set { codigoMenu = value; }
        }

        public String CodigoPai
        {
            get { return codigoPai; }
            set { codigoPai = value; }
        }

        public String NomeModulo
        {
            get { return nomeModulo; }
            set { nomeModulo = value; }
        }

        public Int32 Sequencia
        {
            get { return sequencia; }
            set { sequencia = value; }
        }

        public String UrlAcesso
        {
            get { return urlAcesso; }
            set { urlAcesso = value; }
        }

        public String Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
    }
}