//using Application;
using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;


namespace Domain.Service
{
    public abstract class ServiceBaseNeo : Service<BaseNeo>, IServiceBaseNeo
    {
        public ServiceBaseNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao)
        {
            this.enderecoWebServiceREST = enderecoWebServiceREST;
            this.parametrosWS = parametrosWS;
            this.valorChave = chave;
            this.valorAcao = acao;

        }

        #region Variáveis Locais

        private string enderecoWebServiceREST;
        private string[] parametrosWS;
        private string valorChave;
        private string valorAcao;
        private IServiceBaseNeo serviceBaseNeo;

        #endregion

        #region Propriedades

        public string ValoresParametroAcao
        {
            get { return valorAcao; }
            // set { valoresParametroAcao = value; }
        }

        public string ValorParametroChave
        {
            get { return valorChave; }
            //set { valorParametroChave = value; }
        }

        public string[] ParametrosWS
        {
            get { return parametrosWS; }
            //set { parametrosWS = value; }
        }

        public string EnderecoWebServiceREST
        {
            get { return enderecoWebServiceREST; }
            //set { enderecoWebServiceREST = value; }
        }

        #endregion

        #region Métodos Abstratos


        #endregion

        #region Métodos Privados

        private StringReader ObterDadosWS()
        {
            StringReader retorno = null;

            try
            {
                retorno = HttpUtil.HttpPostStringReader(this.enderecoWebServiceREST, this.parametrosWS, new string[2] { valorAcao, valorChave });
            }
            catch (Exception ex)
            {
                throw new Exception(Mensagem.WSFalhaObterXml, ex.InnerException);
            }

            return retorno;
        }

        #endregion

        #region Métodos Virtuais

        public virtual void ArgumentNotNull<T>(T arg, string name) where T : BaseNeo
        {

        }
        public virtual List<T> EfetuarLeituraXml() where T : BaseNeo
        { }


        public abstract void SalvarDadosXmlBD();

        public virtual DataSet ImportarXmltoDataSet()
        {
            DataSet dsStaging = new DataSet();

            try
            {
                dsStaging.ReadXml(XmlReader.Create(this.ObterDadosWS()));
            }
            catch (Exception ex)
            {
                throw new Exception(Mensagem.XMLImportarDataSetErro, ex.InnerException);
            }

            return dsStaging;
        }


        #endregion

        #region Métodos Estáticos


        #endregion



    }
}
