using Application;
using Domain.Entity;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;


namespace ServicoConsumirWSNEO
{
    public abstract class BaseNeo
    {

        public BaseNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
        {
            this.enderecoWebServiceREST = enderecoWebServiceREST;
            this.parametrosWS = parametrosWS;
            this.valorChave = chave;
            this.valorAcao = acao;
            this.codigoParaCampoChaveNulo = codigoParaCampoChaveNulo;
            this.wsTimeOutLeituraXML = wsTimeOutLeituraXML;
            this.dataInicioBuscaYYmmDD = dataInicioBuscaYYmmDD;
            this.dataFimBuscaYYmmDD = dataFimBuscaYYmmDD;
        }

        #region Variáveis Locais

        private string enderecoWebServiceREST;
        private string[] parametrosWS;
        private string valorChave;
        private string valorAcao;
        private int codigoParaCampoChaveNulo;
        private int quantidadeDiasBusca;
        private string dataInicioBuscaYYmmDD;
        private string dataFimBuscaYYmmDD;
        private int wsTimeOutLeituraXML;

        public int CodigoParaCampoChaveNulo
        {
            get { return codigoParaCampoChaveNulo; }
            // set { codigoParaCampoChaveNulo = value; }
        }

        #endregion

        #region Propriedades

        public string DataFimBuscaYYmmDD
        {
            get { return dataFimBuscaYYmmDD; }
            //set { dataFimBuscaYYmmDD = value; }
        }
        public string DataInicioBuscaYYmmDD
        {
            get { return dataInicioBuscaYYmmDD; }
            //set { dataInicioBuscaYYmmDD = value; }
        }


        public int QuantidadeDiasBusca
        {
            get { return quantidadeDiasBusca; }
            //set { quantidadeDiasBusca = value; }
        }
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

        public abstract void EfetuarLeituraXml();

        public abstract void SalvarDadosXmlBD();

        public abstract BaseNeoClass BuscarObjetoBD(string id);
        //public abstract string FormartarDadosRegistro(BaseNeoClass obj);

        #endregion

        #region Métodos Privados

        private StringReader ObterDadosWS()
        {
            StringReader retorno = null;

            try
            {
                retorno = HttpUtil.HttpPostStringReader(this.enderecoWebServiceREST, this.parametrosWS, new string[4] { valorAcao, valorChave, this.dataInicioBuscaYYmmDD, this.DataFimBuscaYYmmDD }, this.wsTimeOutLeituraXML);
            }
            catch (Exception ex)
            {
                BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, Mensagem.WSFalhaObterXml, ex);
            }

            return retorno;
        }

        #endregion

        #region Métodos Virtuais

        public virtual DataSet ImportarXmltoDataSet()
        {
            DataSet dsStaging = new DataSet();

            try
            {
                dsStaging.ReadXml(XmlReader.Create(this.ObterDadosWS()));
            }
            catch (Exception ex)
            {
                BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, Mensagem.XMLImportarDataSetErro, ex);
            }

            return dsStaging;
        }

        public virtual string FormatarMensagemLog(string mensagem)
        {
            return string.Format("{0} [{1}: {2} até {3}]", mensagem, Mensagem.BuscaDadosPeriodo, this.dataInicioBuscaYYmmDD, this.dataFimBuscaYYmmDD);
        }


        #endregion

        #region Métodos Estáticos

        public static void SalvarLogErroAplicacao(Exception excecao)
        {
            SalvarLogErroAplicacao(string.Empty, excecao);
        }

        public static void SalvarLogErroAplicacao(string mensagem, Exception excecao)
        {
            SalvarLogErroAplicacao(string.Empty, mensagem, excecao);
        }

        public static void SalvarLogErroAplicacao(string nomeObjeto, string mensagem, Exception excecao)
        {
            SalvarLogErroAplicacao(nomeObjeto, string.Empty, mensagem, excecao);
        }
        public static void SalvarLogErroAplicacao(string nomeObjeto, string dadosRegistro, string mensagem, Exception excecao)
        {
            LogErroAplicacao log = new LogErroAplicacao();

            try
            {
                LogErroAplicacaoApplication logApp = new LogErroAplicacaoApplication();


                if (excecao.InnerException != null)
                {
                    if (excecao.InnerException.InnerException != null)
                        if (excecao.InnerException.InnerException.InnerException != null)
                            log.Excecao = excecao.InnerException.InnerException.InnerException.Message;
                        else
                            log.Excecao = excecao.InnerException.InnerException.Message;
                    else
                        log.Excecao = excecao.InnerException.Message;
                }

                if (excecao.GetType().Name == "DbEntityValidationException")
                {
                    System.Data.Entity.Validation.DbEntityValidationException obj = (System.Data.Entity.Validation.DbEntityValidationException)excecao;

                    if (obj.EntityValidationErrors != null && obj.EntityValidationErrors.ToList().Count > 0)
                    {
                        System.Data.Entity.Validation.DbEntityValidationResult erros = obj.EntityValidationErrors.ToList()[0];

                        if (erros != null && erros.ValidationErrors != null && erros.ValidationErrors.Count > 0)
                        {
                            System.Data.Entity.Validation.DbValidationError erro = erros.ValidationErrors.ToList()[0];

                            log.Excecao = erro.ErrorMessage;
                        }
                    }
                }

                if (!dadosRegistro.Equals(string.Empty))
                    log.DadosRegistro = dadosRegistro;

                if (!nomeObjeto.Equals(string.Empty))
                    log.NomeObjeto = nomeObjeto;

                if (!mensagem.Equals(string.Empty))
                    log.MensagemErro = mensagem + " - " + excecao.Message;

                log.Data = DateTime.Now;
                log.Origem = excecao.Source;
                log.CodExcecao = excecao.HResult;

                logApp.Insere(log);

                RegistroLog.RegistarLogErro(Mensagem.ErroSistema + " | Objeto: " + log.NomeObjeto + "| Dados Registro: " + log.DadosRegistro + "| MensagemErro: " + log.MensagemErro + " | Excecao: " + log.Excecao, excecao);
            }
            catch (Exception ex)
            {
                GravarAquivoErro(Mensagem.ErroSistema + " | Objeto: " + log.NomeObjeto + "| Dados Registro: " + log.DadosRegistro + "| MensagemErro: " + log.MensagemErro + " | Excecao: " + log.Excecao, ex);
            }

        }


        public static void GravarAquivoErro(string mensagemErro, Exception excecao)
        {
            try
            {
                RegistroLog.RegistarLogErro(mensagemErro, excecao);
            }
            catch (Exception ex)
            {
                throw new Exception(Mensagem.ErroFatalSistema + " | " + mensagemErro + "( " + ex.Message + " )", ex);
            }
        }

        #endregion

    }
}
