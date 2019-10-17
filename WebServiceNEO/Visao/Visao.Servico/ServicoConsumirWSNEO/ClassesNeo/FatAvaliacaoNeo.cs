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
    public class FatAvaliacaoNeo : BaseNeo
    {
        IList<Fato_Avaliacao> listaAvaliacoes;
        Application.FatAvaliacaoApplication app = new Application.FatAvaliacaoApplication();

        public FatAvaliacaoNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Fato_Avaliacao avaliacao = null;



            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaAvaliacoes = new List<Fato_Avaliacao>();

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    avaliacao = null;

                    string idAluno = base.CodigoParaCampoChaveNulo.ToString();
                    string idAvaliacao = base.CodigoParaCampoChaveNulo.ToString();
                    string idModulo = base.CodigoParaCampoChaveNulo.ToString();
                    string idProfessor = base.CodigoParaCampoChaveNulo.ToString();

                    string idTurma = base.CodigoParaCampoChaveNulo.ToString();
                    string idMatricula = base.CodigoParaCampoChaveNulo.ToString();

                    string resultado = string.Empty;

                    Nullable<decimal> nota1Avaliacao = null;
                    Nullable<decimal> nota2Avaliacao = null;
                    Nullable<decimal> notaaf2Avaliacao = null;
                    Nullable<decimal> nota3Avaliacao = null;
                    Nullable<decimal> nota4Recuperacao = null;
                    Nullable<decimal> notaFinalAvaliacao = null;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aluno_preceptor"]))
                            idAluno = Convert.ToString(item["id_aluno_preceptor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_avaliacao"]))
                            idAvaliacao = Convert.ToString(item["id_avaliacao"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_modulo"]))
                            idModulo = Convert.ToString(item["id_modulo"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_professor"]))
                            idProfessor = Convert.ToString(item["id_professor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["nota1_avaliacao"]))
                            nota1Avaliacao = Convert.ToDecimal(item["nota1_avaliacao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["nota2_avaliacao"]))
                            nota2Avaliacao = Convert.ToDecimal(item["nota2_avaliacao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["NOTAAF2_avaliacao"]))
                            notaaf2Avaliacao = Convert.ToDecimal(item["NOTAAF2_avaliacao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo));


                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["nota3_avaliacao"]))
                            nota3Avaliacao = Convert.ToDecimal(item["nota3_avaliacao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["NOTARECUPERACAO_avaliacao"]))
                            nota4Recuperacao = Convert.ToDecimal(item["NOTARECUPERACAO_avaliacao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["notafinal_avaliacao"]))
                            notaFinalAvaliacao = Convert.ToDecimal(item["notafinal_avaliacao"], CultureInfo.GetCultureInfo(Constantes.CulturaInfo));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["RESULTADO"]))
                            resultado = Convert.ToString(item["RESULTADO"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_turma"]))
                            idTurma = Convert.ToString(item["ID_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_matricula"]))
                            idMatricula = Convert.ToString(item["ID_matricula"]);

                        avaliacao = this.BuscarAvaliacao(idAluno, idAvaliacao);

                        if (avaliacao == null)
                        {
                            avaliacao = new Fato_Avaliacao();
                            avaliacao.id_aluno_preceptor = idAluno;
                            avaliacao.id_avaliacao = idAvaliacao;

                            avaliacao.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                    avaliacao.id_turma == idTurma &&
                                    avaliacao.idmatricula_aluno == idMatricula &&
                                    avaliacao.id_modulo == idModulo &&
                                    avaliacao.id_professor == idProfessor &&
                                    avaliacao.nota1_avaliacao == nota1Avaliacao &&
                                    avaliacao.nota2_avaliacao == nota2Avaliacao &&
                                    avaliacao.notaaf2_avaliacao == notaaf2Avaliacao &&
                                    avaliacao.nota3_avaliacao == nota3Avaliacao &&
                                     avaliacao.nota4_recuperacao == nota4Recuperacao &&
                                    avaliacao.notafinal_avaliacao == notaFinalAvaliacao &&
                                    avaliacao.resultado.Trim().ToUpper().Equals(resultado.Trim().ToUpper())
                                )
                                continue;

                            avaliacao.data_atualizacao = dataCarga;
                        }

                        avaliacao.id_turma = idTurma;
                        avaliacao.idmatricula_aluno = idMatricula;
                        avaliacao.id_modulo = idModulo;
                        avaliacao.id_professor = idProfessor;
                        avaliacao.resultado = resultado;
                        avaliacao.nota1_avaliacao = nota1Avaliacao;
                        avaliacao.nota2_avaliacao = nota2Avaliacao;
                        avaliacao.notaaf2_avaliacao = notaaf2Avaliacao;
                        avaliacao.nota3_avaliacao = nota3Avaliacao;
                        avaliacao.nota4_recuperacao = nota4Recuperacao;
                        avaliacao.notafinal_avaliacao = notaFinalAvaliacao;



                        this.listaAvaliacoes.Add(avaliacao);
                    }
                    catch (Exception ex)
                    {
                        if (avaliacao != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(avaliacao.GetType().Name, this.FormartarDadosRegistro(avaliacao), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name,
    string.Format("id_aluno_preceptor = {0} | id_avaliacao = {1} ", idAluno, idAvaliacao), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Fato_Avaliacao item in this.listaAvaliacoes)
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

        public Fato_Avaliacao BuscarAvaliacao(string idAluno, string id_avaliacao)
        {
            return this.app.repositorioAvaliacao.GetbyIDs(idAluno, id_avaliacao);
        }

        private string FormartarDadosRegistro(Fato_Avaliacao obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12} | {13} = {14} | {15} = {16}| {17} = {18} | {19} = {20} | {21} = {22} | {23} = {24} | {25} = {26}",
                                                        Constantes.TabelaSTGAvaliacao,
                                                        "id_aluno_preceptor", obj.id_aluno_preceptor,
                                                        "id_avaliacao", obj.id_avaliacao,
                                                        "id_turma", obj.id_turma,
                                                        "idmatricula_aluno", obj.idmatricula_aluno,
                                                        "id_modulo", obj.id_modulo,
                                                        "id_professor", obj.id_professor,
                                                        "nota1_avaliacao", obj.nota1_avaliacao,
                                                        "nota2_avaliacao", obj.nota2_avaliacao,
                                                        "notaaf2_avaliacao", obj.notaaf2_avaliacao,
                                                        "nota3_avaliacao", obj.nota3_avaliacao,
                                                        "nota4_recuperacao", obj.nota4_recuperacao,
                                                        "notafinal_avaliacao", obj.notafinal_avaliacao,
                                                         "resultado", obj.resultado);





        }
    }
}
