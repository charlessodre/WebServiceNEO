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
    public class AlunoNeo : BaseNeo
    {
        IList<Dim_Aluno_Preceptor> listaAlunos;
        Application.AlunoApplication app = new Application.AlunoApplication();

        public AlunoNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Aluno_Preceptor aluno = null;


            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaAlunos = new List<Dim_Aluno_Preceptor>();

            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaAlunos.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    aluno = null;

                    Nullable<DateTime> dataMatriculaAluno = null;
                    Nullable<DateTime> dataNascAluno = null;
                    Nullable<DateTime> dataInativacao = null;
                    string idEspecialidade = base.CodigoParaCampoChaveNulo.ToString();
                    string idTurma = base.CodigoParaCampoChaveNulo.ToString();
                    string idAluno = base.CodigoParaCampoChaveNulo.ToString();
                    string idMatricula = base.CodigoParaCampoChaveNulo.ToString();
                    int alunoSituacao = 0;

                    try
                    {

                        string nomeAluno = item["nome_aluno_preceptor"].ToString().Trim();
                        string cpfAluno = item["cpf_aluno"].ToString();
                        string generoAluno = item["genero_aluno"].ToString().Trim();
                        string tipoAluno = item["tipo_aluno"].ToString().Trim();

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["idmatricula_aluno"]))
                            idMatricula = Convert.ToString(item["idmatricula_aluno"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aluno_preceptor"]))
                            idAluno = Convert.ToString(item["id_aluno_preceptor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["datamatricula_aluno"]))
                            dataMatriculaAluno = DateTime.Parse(Convert.ToDateTime(item["datamatricula_aluno"]).ToString(Constantes.FormatoDataHora));


                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["datanasc_aluno"]))
                            dataNascAluno = DateTime.Parse(Convert.ToDateTime(item["datanasc_aluno"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_especialidade"]))
                            idEspecialidade = Convert.ToString(item["id_especialidade"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_turma"]))
                            idTurma = Convert.ToString(item["id_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ALUNO_situacao"]))
                            alunoSituacao = Convert.ToInt32(item["ALUNO_situacao"]);

                        IFormatProvider culture = new CultureInfo(Constantes.CulturaInfo, true);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["DATA_inativacao"]))
                            dataInativacao = DateTime.Parse(Convert.ToDateTime(item["DATA_inativacao"]).ToString(Constantes.FormatoDataHora));

                        aluno = this.BuscarAlunoBD(idAluno, idMatricula, idTurma);

                        if (aluno == null)
                        {
                            aluno = new Dim_Aluno_Preceptor();
                            aluno.id_aluno_preceptor = idAluno;
                            aluno.idmatricula_aluno = idMatricula;
                            aluno.id_turma = idTurma;
                            aluno.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                aluno.nome_aluno_preceptor.ToLower().Equals(nomeAluno.ToLower().Trim()) &&
                                aluno.cpf_aluno.ToLower().Equals(cpfAluno.ToLower().Trim()) &&
                                aluno.tipo_aluno.ToLower().Equals(tipoAluno.ToLower().Trim()) &&
                                aluno.genero_aluno.ToLower().Equals(generoAluno.ToLower().Trim()) &&
                                aluno.datamatricula_aluno == dataMatriculaAluno &&
                                aluno.datanasc_aluno == dataNascAluno &&
                                aluno.id_especialidade == idEspecialidade &&
                                aluno.data_inativacao == dataInativacao &&
                                aluno.aluno_situacao == alunoSituacao
                                )

                                continue;

                            aluno.data_atualizacao = dataCarga;
                        }

                        aluno.nome_aluno_preceptor = nomeAluno;
                        aluno.cpf_aluno = cpfAluno;
                        aluno.genero_aluno = generoAluno;
                        aluno.datamatricula_aluno = dataMatriculaAluno;
                        aluno.datanasc_aluno = dataNascAluno;
                        aluno.tipo_aluno = tipoAluno;
                        aluno.id_especialidade = idEspecialidade;
                        aluno.data_inativacao = dataInativacao;
                        aluno.aluno_situacao = alunoSituacao;



                        this.listaAlunos.Add(aluno);
                    }
                    catch (Exception ex)
                    {
                        if (aluno != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(aluno.GetType().Name, this.FormartarDadosRegistro(aluno), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name,
    string.Format("id_aluno_preceptor = {0} | id_turma = {1} | idmatricula_aluno = {2}", idAluno, idTurma, idMatricula), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Aluno_Preceptor item in this.listaAlunos)
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
            throw new NotImplementedException();
            //return this.app.GetbyID(id);
        }
        public Dim_Aluno_Preceptor BuscarAlunoBD(string idAluno, string idMatricula, string idTurma)
        {
            return this.app.repositorioAluno.GetbyIDs(idAluno, idMatricula, idTurma);
        }


        private string FormartarDadosRegistro(Dim_Aluno_Preceptor obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12} | {13} = {14} | {15} = {16} | {17} = {18} | {19} = {20}",
                                                        Constantes.TabelaSTGAluno,
                                                        "id_aluno_preceptor", obj.id_aluno_preceptor,
                                                        "id_especialidade", obj.id_especialidade,
                                                        "id_turma", obj.id_turma,
                                                        "nome_aluno_preceptor", obj.nome_aluno_preceptor,
                                                        "idmatricula_aluno", obj.idmatricula_aluno,
                                                        "cpf_aluno", obj.cpf_aluno,
                                                        "genero_aluno", obj.genero_aluno,
                                                        "datamatricula_aluno", obj.datamatricula_aluno,
                                                        "datanasc_aluno", obj.datanasc_aluno,
                                                        "tipo_aluno", obj.tipo_aluno);

        }

        private Dim_Aluno_Preceptor CriarObjInicial()
        {
            Dim_Aluno_Preceptor aluno = new Dim_Aluno_Preceptor();

            aluno.id_aluno_preceptor = base.CodigoParaCampoChaveNulo.ToString();
            aluno.id_especialidade = base.CodigoParaCampoChaveNulo.ToString();
            aluno.id_turma = base.CodigoParaCampoChaveNulo.ToString();
            aluno.nome_aluno_preceptor = base.CodigoParaCampoChaveNulo.ToString();
            aluno.idmatricula_aluno = base.CodigoParaCampoChaveNulo.ToString();
            aluno.cpf_aluno = base.CodigoParaCampoChaveNulo.ToString();
            //aluno.genero_aluno = base.CodigoParaCampoChaveNulo.ToString();
            //aluno.tipo_aluno = base.CodigoParaCampoChaveNulo.ToString();
            aluno.datamatricula_aluno = Constantes.DataInicialCampoNulo;
            aluno.datanasc_aluno = Constantes.DataInicialCampoNulo;

            return aluno;
        }
    }
}
