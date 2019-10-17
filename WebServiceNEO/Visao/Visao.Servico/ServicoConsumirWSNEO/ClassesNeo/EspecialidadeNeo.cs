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
    public class EspecialidadeNeo : BaseNeo
    {
        IList<Dim_Especialidade> listaspecialidades;
        Application.EspecialidadeApplication app = new Application.EspecialidadeApplication();

        public EspecialidadeNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Especialidade especialidade = null;

            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaspecialidades = new List<Dim_Especialidade>();

            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaspecialidades.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {


                    especialidade = null;

                    string id_especialidade = base.CodigoParaCampoChaveNulo.ToString();
                    string nome_especialidade = string.Empty;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_especialidade"]))
                            id_especialidade = Convert.ToString(item["id_especialidade"]);

                        nome_especialidade = item["nome_especialidade"].ToString();

                        especialidade = (Dim_Especialidade)this.BuscarObjetoBD(id_especialidade);

                        if (especialidade == null)
                        {
                            especialidade = new Dim_Especialidade();
                            especialidade.id_especialidade = id_especialidade;
                            especialidade.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (especialidade.nome_especialidade.ToLower().Equals(nome_especialidade.ToLower()))
                                continue;

                            especialidade.data_atualizacao = dataCarga;
                        }

                        especialidade.nome_especialidade = nome_especialidade;


                        this.listaspecialidades.Add(especialidade);
                    }
                    catch (Exception ex)
                    {
                        if (especialidade != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(especialidade.GetType().Name, this.FormartarDadosRegistro(especialidade), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_especialidade = " + id_especialidade, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                    }
                }
            }
            else
            {
                BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, base.FormatarMensagemLog(Mensagem.XMLSemCabecalhoCorpo), new Exception(Mensagem.XMLSemCabecalhoCorpo));
            }



        }

        public override void SalvarDadosXmlBD()
        {
            foreach (Dim_Especialidade item in this.listaspecialidades)
            {
                try
                {
                    this.app.AlteraOuInsere(item);
                }
                catch (Exception ex)
                {
                    BaseNeo.SalvarLogErroAplicacao(item.GetType().Name, this.FormartarDadosRegistro(item), base.FormatarMensagemLog(Mensagem.ErroSalvarBD), ex);
                }
            }
        }

        public override BaseNeoClass BuscarObjetoBD(string id)
        {
            return this.app.GetbyID(id);
        }

        private string FormartarDadosRegistro(Dim_Especialidade obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4}",
                                                        Constantes.TabelaSTGEspecialidade, "id_especialidade", obj.id_especialidade,
                                                       "nome_especialidade", obj.nome_especialidade);
        }

        private Dim_Especialidade CriarObjInicial()
        {
            Dim_Especialidade especialidade = new Dim_Especialidade();

            especialidade.id_especialidade = base.CodigoParaCampoChaveNulo.ToString();
            especialidade.data_atualizacao = DateTime.Now;
            especialidade.nome_especialidade = base.CodigoParaCampoChaveNulo.ToString();

            return especialidade;
        }
    }
}
