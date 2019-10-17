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
    public class ProfessorNeo : BaseNeo
    {
        IList<Dim_Professor> listaProfessores;
        Application.ProfessorApplication app = new Application.ProfessorApplication();

        public ProfessorNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Professor professor = null;


            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaProfessores = new List<Dim_Professor>();

            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaProfessores.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {
                    professor = null;

                    Nullable<DateTime> datanascimento_professor = null;
                    string id_especialidade = base.CodigoParaCampoChaveNulo.ToString();
                    string id_professor = base.CodigoParaCampoChaveNulo.ToString();

                    try
                    {

                        string nome_professor = item["nome_professor"].ToString().Trim();
                        //string cpf_professor = item["cpf_professor"].ToString();
                        string genero_professor = item["genero_professor"].ToString().Trim();

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_professor"]))
                            id_professor = Convert.ToString(item["id_professor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["datanascimento_professor"]))
                            datanascimento_professor = Convert.ToDateTime(item["datanascimento_professor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_especialidade"]))
                            id_especialidade = Convert.ToString(item["id_especialidade"]);

                        professor = (Dim_Professor)this.BuscarObjetoBD(id_professor);

                        if (professor == null)
                        {
                            professor = new Dim_Professor();
                            professor.id_professor = id_professor;
                            professor.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                // professor.cpf_professor.ToLower().Equals(cpf_professor.ToLower().Trim()) &&
                                professor.genero_professor.ToLower().Equals(genero_professor.ToLower().Trim()) &&
                                professor.nome_professor.ToLower().Equals(nome_professor.ToLower().Trim()) &&
                                professor.id_especialidade == id_especialidade &&
                                (professor.datanascimento_professor.HasValue == datanascimento_professor.HasValue) &&
                                datanascimento_professor.HasValue &&
                                professor.datanascimento_professor.Value.ToString(Constantes.FormatoDataHora).Equals(datanascimento_professor.Value.ToString(Constantes.FormatoDataHora)
                                ))

                                continue;
                            professor.data_atualizacao = dataCarga;
                        }

                        // professor.cpf_professor = cpf_professor;
                        professor.datanascimento_professor = datanascimento_professor;
                        professor.genero_professor = genero_professor;
                        professor.nome_professor = nome_professor;
                        professor.id_especialidade = id_especialidade;


                        this.listaProfessores.Add(professor);
                    }
                    catch (Exception ex)
                    {
                        if (professor != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(professor.GetType().Name, this.FormartarDadosRegistro(professor), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_professor = " + id_professor, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Professor item in this.listaProfessores)
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

        private string FormartarDadosRegistro(Dim_Professor obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12} | {13} = {14}",
                                                     Constantes.TabelaSTGProfessor,
                                                    "id_professor", obj.id_professor,
                                                    "nome_professor", obj.nome_professor,
                                                    "cpf_professor", obj.cpf_professor,
                                                    "datanascimento_professor", obj.datanascimento_professor,
                                                    "genero_professor", obj.genero_professor,
                                                    "nome_professor", obj.nome_professor,
                                                    "id_especialidade", obj.id_especialidade);
        }

        private Dim_Professor CriarObjInicial()
        {
            Dim_Professor professor = new Dim_Professor();

            professor.id_especialidade = base.CodigoParaCampoChaveNulo.ToString();
            professor.id_professor = base.CodigoParaCampoChaveNulo.ToString();
            professor.nome_professor = base.CodigoParaCampoChaveNulo.ToString();
            //professor.genero_professor = base.CodigoParaCampoChaveNulo.ToString();
            professor.cpf_professor = base.CodigoParaCampoChaveNulo.ToString();
            professor.datanascimento_professor = Constantes.DataInicialCampoNulo;

            return professor;
        }
    }
}
