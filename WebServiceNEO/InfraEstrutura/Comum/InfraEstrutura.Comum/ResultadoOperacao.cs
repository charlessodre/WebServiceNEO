using System.Collections;

namespace Infrastructure.Common
{
    public class ResultadoOperacao
    {
        #region Construtores

        #endregion

        #region Atributos

        private string _mensagem;
        private Enumeradores.Resultados _resultado;
        private IList _objetosList;
        private Enumeradores.TipoOperacao _tipoOperacao;

        
        #endregion

        #region Propriedades

        /// <summary>
        /// Lê e escreve o tipo da operação realizada.
        /// </summary>
        public Enumeradores.TipoOperacao TipoOperacao
        {
            get { return _tipoOperacao; }
            set { _tipoOperacao = value; }
        }


        /// <summary>
        /// Lê e escreve uma lista de objetos não genérica.
        /// </summary>
        public IList ListaObjetos
        {
            get { return _objetosList; }
            set { _objetosList = value; }
        }

        /// <summary>
        /// Lê e Escreve a mensagem.
        /// </summary>
        public string Mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value; }
        }

        /// <summary>
        /// Lê e Escreve o resultado da operação.
        /// </summary>
        public Enumeradores.Resultados Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }

        #endregion

        #region Métodos

        #endregion

    }
}
