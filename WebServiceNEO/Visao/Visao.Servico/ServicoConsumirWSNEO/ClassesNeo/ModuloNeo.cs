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
    public class ModuloNeo : BaseNeo
    {
        IList<Dim_Modulo> listaModulos;
        Application.ModuloApplication app = new Application.ModuloApplication();

        public ModuloNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Modulo modulo = null;



            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaModulos = new List<Dim_Modulo>();

            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaModulos.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    modulo = null;

                    string id_modulo = base.CodigoParaCampoChaveNulo.ToString();
                    string nome_modulo = string.Empty;

                    Nullable<DateTime> datainicio_modulo = null;
                    Nullable<DateTime> datafim_modulo = null;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_modulo"]))
                            id_modulo = Convert.ToString(item["id_modulo"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["datainicio_modulo"]))
                            datainicio_modulo = Convert.ToDateTime(item["datainicio_modulo"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["datafim_modulo"]))
                            datafim_modulo = Convert.ToDateTime(item["datafim_modulo"]);

                        nome_modulo = item["nome_modulo"].ToString();

                        modulo = (Dim_Modulo)this.BuscarObjetoBD(id_modulo);

                        if (modulo == null)
                        {
                            modulo = new Dim_Modulo();
                            modulo.id_modulo = id_modulo;
                            modulo.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                modulo.nome_modulo.ToLower().Equals(nome_modulo.ToLower()) &&
                                (modulo.datainicio_modulo == datainicio_modulo) &&
                                (modulo.datafim_modulo == datafim_modulo)

                                )
                                continue;

                            modulo.data_atualizacao = dataCarga;
                        }

                        modulo.nome_modulo = nome_modulo;
                        modulo.datainicio_modulo = datainicio_modulo;
                        modulo.datafim_modulo = datafim_modulo;


                        this.listaModulos.Add(modulo);
                    }
                    catch (Exception ex)
                    {
                        if (modulo != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(modulo.GetType().Name, this.FormartarDadosRegistro(modulo), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_modulo = " + id_modulo, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Modulo item in this.listaModulos)
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

        private string FormartarDadosRegistro(Dim_Modulo obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {9}",
                                                        Constantes.TabelaSTGModulo,
                                                        "id_modulo", obj.id_modulo,
                                                        "nome_modulo", obj.nome_modulo,
                                                        "datainicio_modulo", obj.datainicio_modulo,
                                                        "datafim_modulo", obj.datafim_modulo);
        }


        private Dim_Modulo CriarObjInicial()
        {
            Dim_Modulo modulo = new Dim_Modulo();

            modulo.id_modulo = base.CodigoParaCampoChaveNulo.ToString();
            modulo.nome_modulo = base.CodigoParaCampoChaveNulo.ToString();
            modulo.datainicio_modulo = Constantes.DataInicialCampoNulo;
            modulo.datafim_modulo = Constantes.DataInicialCampoNulo;

            return modulo;
        }
    }
}
