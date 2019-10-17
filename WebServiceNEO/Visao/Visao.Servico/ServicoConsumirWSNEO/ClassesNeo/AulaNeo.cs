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
    public class AulaNeo : BaseNeo
    {
        IList<Dim_Aula> listaAulas;
        Application.AulaApplication app = new Application.AulaApplication();

        public AulaNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Aula aula = null;



            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaAulas = new List<Dim_Aula>();


            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaAulas.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    aula = null;

                    string tituloAula = string.Empty;
                    string tipoAula = string.Empty;
                    Nullable<DateTime> dataInicioAula = null;
                    Nullable<DateTime> dataFimAula = null;

                    string idAula = base.CodigoParaCampoChaveNulo.ToString();
                    string idTurma = base.CodigoParaCampoChaveNulo.ToString();
                    string idProfessor = base.CodigoParaCampoChaveNulo.ToString();
                    int qtdAtividadesAula = base.CodigoParaCampoChaveNulo;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aula"]))
                            idAula = Convert.ToString(item["id_aula"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_turma"]))
                            idTurma = Convert.ToString(item["id_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_professor"]))
                            idProfessor = Convert.ToString(item["id_professor"]);


                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["data_aula"]))
                            dataInicioAula = DateTime.Parse(Convert.ToDateTime(item["data_aula"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["DATAFIM_aula"]))
                            dataFimAula = DateTime.Parse(Convert.ToDateTime(item["DATAFIM_aula"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["atividades_aula"]))
                            qtdAtividadesAula = Convert.ToInt32(item["atividades_aula"]);

                        tituloAula = item["titulo_aula"].ToString();
                        tipoAula = item["tipo_aula"].ToString();

                        aula = (Dim_Aula)this.BuscarObjetoBD(idAula);

                        if (aula == null)
                        {
                            aula = new Dim_Aula();
                            aula.id_aula = idAula;
                            aula.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                aula.titulo_aula.ToLower().Equals(tituloAula.ToLower()) &&
                                aula.tipo_aula.ToLower().Equals(tipoAula.ToLower()) &&
                                aula.atividades_aula == qtdAtividadesAula &&
                                aula.id_professor == idProfessor &&
                                aula.id_turma == idTurma &&
                                aula.data_aula == dataInicioAula &&
                                aula.data_fim_aula == dataFimAula)

                                continue;

                            aula.data_atualizacao = dataCarga;
                        }

                        aula.titulo_aula = tituloAula;
                        aula.tipo_aula = tipoAula;
                        aula.atividades_aula = qtdAtividadesAula;
                        aula.data_aula = dataInicioAula;
                        aula.data_fim_aula = dataFimAula;
                        aula.id_turma = idTurma;
                        aula.id_professor = idProfessor;



                        this.listaAulas.Add(aula);
                    }
                    catch (Exception ex)
                    {
                        if (aula != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(aula.GetType().Name, this.FormartarDadosRegistro(aula), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_aula = " + idAula, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Aula item in this.listaAulas)
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

        private string FormartarDadosRegistro(Dim_Aula obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12} | {13} = {14}| {15} = {16}",
                                                        Constantes.TabelaSTGAula,
                                                        "id_aula", obj.id_aula,
                                                        "titulo_aula", obj.titulo_aula,
                                                        "tipo_aula", obj.tipo_aula,
                                                        "id_turma", obj.id_turma,
                                                        "id_professor", obj.id_professor,
                                                        "atividades_aula", obj.atividades_aula,
                                                        "data_aula", obj.data_aula,
                                                        "data_fim_aula", obj.data_fim_aula);

        }

        private Dim_Aula CriarObjInicial()
        {
            Dim_Aula aula = new Dim_Aula();

            aula.atividades_aula = base.CodigoParaCampoChaveNulo;
            aula.id_turma = base.CodigoParaCampoChaveNulo.ToString();
            aula.id_professor = base.CodigoParaCampoChaveNulo.ToString();
            aula.id_aula = base.CodigoParaCampoChaveNulo.ToString();
            aula.titulo_aula = base.CodigoParaCampoChaveNulo.ToString();
            //aula.tipo_aula = base.CodigoParaCampoChaveNulo.ToString();
            aula.data_aula = Constantes.DataInicialCampoNulo;
            //aula.data_fim_aula = Constantes.DataInicialCampoNulo;

            return aula;
        }

    }
}
