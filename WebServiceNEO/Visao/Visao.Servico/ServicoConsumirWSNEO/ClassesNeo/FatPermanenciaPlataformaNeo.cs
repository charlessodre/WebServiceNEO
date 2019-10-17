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
    public class FatPermanenciaPlataformaNeo : BaseNeo
    {
        IList<Fato_Permanencia_Plataforma> listaPermanenciaPlataforma;
        Application.FatPermanenciaPlataformaApplication app = new Application.FatPermanenciaPlataformaApplication();

        public FatPermanenciaPlataformaNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Fato_Permanencia_Plataforma permanenciaPlataforma = null;

            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaPermanenciaPlataforma = new List<Fato_Permanencia_Plataforma>();

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    permanenciaPlataforma = null;

                    string idAluno = base.CodigoParaCampoChaveNulo.ToString();
                    string idTurma = base.CodigoParaCampoChaveNulo.ToString();
                    string idMatricula = base.CodigoParaCampoChaveNulo.ToString();

                    DateTime dataEntradaPlataforma = Constantes.DataInicialCampoNulo;
                    Nullable<DateTime> dataSaidaPlataforma = null;

                    int tempoPermanenciaPlataforma = base.CodigoParaCampoChaveNulo;

                    try
                    {
                        string tipoAluno = item["PRECEPTOR_aluno"].ToString().Trim();


                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_aluno"]))
                            idAluno = Convert.ToString(item["ID_aluno"]);


                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["DATA_entrada"]))
                            dataEntradaPlataforma = DateTime.Parse(Convert.ToDateTime(item["DATA_entrada"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["DATA_saida"]))
                            dataSaidaPlataforma = DateTime.Parse(Convert.ToDateTime(item["DATA_saida"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["TEMPO_permanencia"]))
                            tempoPermanenciaPlataforma = Convert.ToInt32(item["TEMPO_permanencia"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_turma"]))
                            idTurma = Convert.ToString(item["ID_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_matricula"]))
                            idMatricula = Convert.ToString(item["ID_matricula"]);

                        permanenciaPlataforma = this.BuscarPermanenciaPlataforma(idAluno, dataEntradaPlataforma);

                        if (permanenciaPlataforma == null)
                        {
                            permanenciaPlataforma = new Fato_Permanencia_Plataforma();
                            permanenciaPlataforma.id_aluno_preceptor = idAluno;
                            permanenciaPlataforma.id_turma = idTurma;
                            permanenciaPlataforma.idmatricula_aluno = idMatricula;
                            permanenciaPlataforma.data_entrada = dataEntradaPlataforma;

                            permanenciaPlataforma.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                    permanenciaPlataforma.data_saida == dataSaidaPlataforma &&
                                    permanenciaPlataforma.tipo_aluno == tipoAluno &&
                                    permanenciaPlataforma.tempo_permanencia == tempoPermanenciaPlataforma
                                )
                                continue;

                            permanenciaPlataforma.data_atualizacao = dataCarga;
                        }

                        permanenciaPlataforma.id_turma = idTurma;
                        permanenciaPlataforma.idmatricula_aluno = idMatricula;
                        permanenciaPlataforma.data_saida = dataSaidaPlataforma;
                        permanenciaPlataforma.tipo_aluno = tipoAluno;
                        permanenciaPlataforma.tempo_permanencia = tempoPermanenciaPlataforma;

                        this.listaPermanenciaPlataforma.Add(permanenciaPlataforma);

                    }
                    catch (Exception ex)
                    {
                        if (permanenciaPlataforma != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(permanenciaPlataforma.GetType().Name, this.FormartarDadosRegistro(permanenciaPlataforma), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name,
    string.Format("id_aluno_preceptor = {0} |  idmatricula_aluno = {1} | id_turma = {2} | data_entrada = {3}", idAluno, idTurma, idMatricula, dataEntradaPlataforma), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Fato_Permanencia_Plataforma item in this.listaPermanenciaPlataforma)
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

        public Fato_Permanencia_Plataforma BuscarPermanenciaPlataforma(string idAluno, DateTime dataEntrada)
        {
            return this.app.repositorioPermanenciaPlataforma.GetbyIDs(idAluno, dataEntrada);
        }

        private string FormartarDadosRegistro(Fato_Permanencia_Plataforma obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12}",
                                                        Constantes.TabelaSTGFatPermanenciaPlataforma,
                                                        "id_aluno_preceptor", obj.id_aluno_preceptor,
                                                        "data_entrada", obj.data_entrada,
                                                        "data_saida", obj.data_saida,
                                                        "tempo_permanencia", obj.tempo_permanencia,
                                                        "id_turma", obj.id_turma,
                                                        "idmatricula_aluno", obj.idmatricula_aluno
                                                        );





        }
    }
}
