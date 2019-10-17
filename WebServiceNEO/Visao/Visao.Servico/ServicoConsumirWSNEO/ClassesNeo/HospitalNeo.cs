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
    public class HospitalNeo : BaseNeo
    {
        IList<Dim_Hospital> listaHospitais;
        Application.HospitalApplication app = new Application.HospitalApplication();

        public HospitalNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {

        }

        public override void EfetuarLeituraXml()
        {
            Dim_Hospital hospital = null;



            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaHospitais = new List<Dim_Hospital>();

            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaHospitais.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    hospital = null;

                    string id_hospital = base.CodigoParaCampoChaveNulo.ToString();
                    string nome_hospital = string.Empty;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_hospital"]))
                            id_hospital = Convert.ToString(item["id_hospital"]);

                        nome_hospital = item["nome_hospital"].ToString();

                        hospital = (Dim_Hospital)this.BuscarObjetoBD(id_hospital);

                        if (hospital == null)
                        {
                            hospital = new Dim_Hospital();
                            hospital.id_hospital = id_hospital;
                            hospital.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (hospital.nome_hospital.ToLower().Equals(nome_hospital.ToLower()))
                                continue;

                            hospital.data_atualizacao = dataCarga;
                        }

                        hospital.nome_hospital = nome_hospital;


                        this.listaHospitais.Add(hospital);
                    }
                    catch (Exception ex)
                    {
                        if (hospital != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(hospital.GetType().Name, this.FormartarDadosRegistro(hospital), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_hospital = " + id_hospital, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Hospital item in this.listaHospitais)
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

        private string FormartarDadosRegistro(Dim_Hospital obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4}",
                                                        Constantes.TabelaSTGHospital, "id_hospital", obj.id_hospital,
                                                       "nome_hospital", obj.nome_hospital);
        }

        private Dim_Hospital CriarObjInicial()
        {
            Dim_Hospital hospital = new Dim_Hospital();

            hospital.nome_hospital = base.CodigoParaCampoChaveNulo.ToString();
            hospital.id_hospital = base.CodigoParaCampoChaveNulo.ToString();

            return hospital;
        }
    }
}
