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
    public class TurmaNeo : BaseNeo
    {
        IList<Dim_Turma> listaTurmas;
        Application.TurmaApplication app = new Application.TurmaApplication();

        public TurmaNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Turma turma = null;

            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaTurmas = new List<Dim_Turma>();

            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaTurmas.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {
                    turma = null;

                    string id_turma = base.CodigoParaCampoChaveNulo.ToString();
                    string id_curso = base.CodigoParaCampoChaveNulo.ToString();
                    string id_hospital = base.CodigoParaCampoChaveNulo.ToString();
                    string id_modulo = base.CodigoParaCampoChaveNulo.ToString();

                    string nome_turma = string.Empty;

                    try
                    {
                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_turma"]))
                            id_turma = Convert.ToString(item["id_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_curso"]))
                            id_curso = Convert.ToString(item["id_curso"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_hospital"]))
                            id_hospital = Convert.ToString(item["id_hospital"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_modulo"]))
                            id_modulo = Convert.ToString(item["id_modulo"]);

                        nome_turma = item["nome_turma"].ToString();

                        turma = (Dim_Turma)this.BuscarObjetoBD(id_turma);

                        if (turma == null)
                        {
                            turma = new Dim_Turma();
                            turma.id_turma = id_turma;
                            turma.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                    turma.nome_turma.ToLower().Equals(nome_turma.ToLower()) &&
                                    turma.id_curso == id_curso &&
                                    turma.id_hospital == id_hospital &&
                                    turma.id_modulo == id_modulo
                                )
                                continue;

                            turma.data_atualizacao = dataCarga;
                        }

                        turma.nome_turma = nome_turma;
                        turma.id_curso = id_curso;
                        turma.id_hospital = id_hospital;
                        turma.id_modulo = id_modulo;


                        this.listaTurmas.Add(turma);
                    }
                    catch (Exception ex)
                    {
                        if (turma != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(turma.GetType().Name, this.FormartarDadosRegistro(turma), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_turma = " + id_turma, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Turma item in this.listaTurmas)
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

        private string FormartarDadosRegistro(Dim_Turma obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10}",
                                                        Constantes.TabelaSTGTurma,
                                                        "id_turma", obj.id_turma,
                                                        "id_curso", obj.id_curso,
                                                        "id_hospital", obj.id_hospital,
                                                        "id_modulo", obj.id_modulo,
                                                       "nome_turma", obj.nome_turma);
        }

        private Dim_Turma CriarObjInicial()
        {
            Dim_Turma turma = new Dim_Turma();

            turma.id_turma = base.CodigoParaCampoChaveNulo.ToString();
            turma.id_curso = base.CodigoParaCampoChaveNulo.ToString();
            turma.id_hospital = base.CodigoParaCampoChaveNulo.ToString();
            turma.id_modulo = base.CodigoParaCampoChaveNulo.ToString();
            turma.nome_turma = base.CodigoParaCampoChaveNulo.ToString();

            return turma;
        }
    }
}
