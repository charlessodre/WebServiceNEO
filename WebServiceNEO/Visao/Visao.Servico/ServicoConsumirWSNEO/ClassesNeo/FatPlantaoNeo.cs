using Domain.Entity;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace ServicoConsumirWSNEO
{
    public class FatPlantaoNeo : BaseNeo
    {
        IList<Fato_Plantao> listaPlantoes;
        Application.FatPlantaoApplication app = new Application.FatPlantaoApplication();

        public FatPlantaoNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Fato_Plantao plantao = null;

            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaPlantoes = new List<Fato_Plantao>();

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {
                    plantao = null;

                    string idAluno = base.CodigoParaCampoChaveNulo.ToString();
                    string idPlantao = base.CodigoParaCampoChaveNulo.ToString();

                    string idTurma = null;
                    string idMatricula = null;

                    Nullable<decimal> duracao_plantao = base.CodigoParaCampoChaveNulo;
                    Nullable<decimal> efetivo_plantao = base.CodigoParaCampoChaveNulo;

                    Nullable<DateTime> escalaInicioPlantao = null;
                    Nullable<DateTime> escalaFimPlantao = null;
                    Nullable<DateTime> marcacaoInicioPlantao = null;
                    Nullable<DateTime> marcacaoFimPlantao = null;


                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_plantao"]))
                            idPlantao = Convert.ToString(item["id_plantao"]);

                        string abonoDs = Convert.ToString(item["ABONO"]);
                        string abonoBolsa = Convert.ToString(item["ABONOBOLSA"]);
                        string abonoFalta = Convert.ToString(item["ABONOFALTA"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aluno_preceptor"]))
                            idAluno = Convert.ToString(item["id_aluno_preceptor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["duracao_plantao"]))
                            duracao_plantao = decimal.Round(Convert.ToDecimal(item["duracao_plantao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo)), 2);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["efetivo_plantao"]))
                            efetivo_plantao = decimal.Round(Convert.ToDecimal(item["efetivo_plantao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo)), 2);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["escalainicio_plantao"]))
                            escalaInicioPlantao = DateTime.Parse(Convert.ToDateTime(item["escalainicio_plantao"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ESCALAFIM_plantao"]))
                            escalaFimPlantao = DateTime.Parse(Convert.ToDateTime(item["ESCALAFIM_plantao"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["marcacaoinicio_plantao"]))
                            marcacaoInicioPlantao = DateTime.Parse(Convert.ToDateTime(item["marcacaoinicio_plantao"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["marcacaofim_plantao"]))
                            marcacaoFimPlantao = DateTime.Parse(Convert.ToDateTime(item["marcacaofim_plantao"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_turma"]))
                            idTurma = Convert.ToString(item["ID_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_matricula"]))
                            idMatricula = Convert.ToString(item["ID_matricula"]);

                        plantao = this.BuscarPlantao(idAluno, idPlantao);

                        if (plantao == null)
                        {
                            plantao = new Fato_Plantao();
                            plantao.id_aluno_preceptor = idAluno;
                            plantao.id_plantao = idPlantao;

                            plantao.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                    plantao.id_turma == idTurma &&
                                    plantao.idmatricula_aluno == idMatricula &&
                                    plantao.escalainicio_plantao == escalaInicioPlantao &&
                                    plantao.escalafim_plantao == escalaFimPlantao &&
                                    plantao.marcacaoinicio_plantao == marcacaoInicioPlantao &&
                                    plantao.marcacaofim_plantao == marcacaoFimPlantao &&
                                    plantao.duracao_plantao == duracao_plantao &&
                                    plantao.efetivo_plantao == efetivo_plantao.Value &&
                                    plantao.abono_bolsa == abonoBolsa &&
                                    plantao.abono_falta == abonoFalta &&
                                    plantao.abono_ds.Trim().ToUpper().Equals(abonoDs.Trim().ToUpper())
                                )
                                continue;

                            plantao.data_atualizacao = dataCarga;
                        }

                        plantao.id_turma = idTurma;
                        plantao.idmatricula_aluno = idMatricula;
                        plantao.escalainicio_plantao = escalaInicioPlantao;
                        plantao.escalafim_plantao = escalaFimPlantao;
                        plantao.marcacaoinicio_plantao = marcacaoInicioPlantao;
                        plantao.marcacaofim_plantao = marcacaoFimPlantao;
                        plantao.duracao_plantao = duracao_plantao;
                        plantao.efetivo_plantao = efetivo_plantao.Value;
                        plantao.abono_ds = abonoDs;
                        plantao.abono_bolsa = abonoBolsa;
                        plantao.abono_falta = abonoFalta;

                        this.listaPlantoes.Add(plantao);

                    }
                    catch (Exception ex)
                    {
                        if (plantao != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(plantao.GetType().Name, this.FormartarDadosRegistro(plantao), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, string.Format("id_plantao = {0} | id_aluno_preceptor = {1}", idPlantao, idAluno), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Fato_Plantao item in this.listaPlantoes)
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
            //return this.app.GetbyID(id);
            throw new NotImplementedException();
        }

        public Fato_Plantao BuscarPlantao(string idAluno, string idPlantao)
        {
            return this.app.repositorioPlantao.GetbyIDs(idAluno, idPlantao);
        }

        private string FormartarDadosRegistro(Fato_Plantao obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12} | {13} = {14} | {15} = {16}| {17} = {18} | {19} = {20}| {21} = {22} | {23} = {24}| {25} = {26}",
                                                        Constantes.TabelaSTGPlantao,
                                                        "id_aluno_preceptor", obj.id_aluno_preceptor,
                                                        "id_plantao", obj.id_plantao,
                                                        "id_turma", obj.id_turma,
                                                        "idmatricula_aluno", obj.idmatricula_aluno,
                                                        "escalainicio_plantao", obj.escalainicio_plantao,
                                                        "escalafim_plantao", obj.escalafim_plantao,
                                                        "marcacaoinicio_plantao", obj.marcacaoinicio_plantao,
                                                        "marcacaofim_plantao", obj.marcacaofim_plantao,
                                                        "duracao_plantao", obj.duracao_plantao,
                                                        "efetivo_plantao", obj.efetivo_plantao,
                                                        "abono_ds", obj.abono_ds,
                                                        "abono_falta", obj.abono_falta,
                                                        "abono_bolsa", obj.abono_bolsa);



        }
    }
}
