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
    public class CursoNeo : BaseNeo
    {
        IList<Dim_Curso> listaCursos;
        Application.CursoApplication app = new Application.CursoApplication();

        public CursoNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Dim_Curso curso = null;


            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaCursos = new List<Dim_Curso>();


            if (base.CodigoParaCampoChaveNulo == Constantes.CodigoParaCriarObjInicial)
            {
                this.listaCursos.Add(this.CriarObjInicial());
            }

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {
                    curso = null;

                    string idCurso = base.CodigoParaCampoChaveNulo.ToString();
                    string nomeCurso = string.Empty;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_curso"]))
                            idCurso = Convert.ToString(item["id_curso"]);

                        nomeCurso = item["nome_curso"].ToString();

                        curso = (Dim_Curso)this.BuscarObjetoBD(idCurso);

                        if (curso == null)
                        {
                            curso = new Dim_Curso();
                            curso.id_curso = idCurso;
                            curso.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (curso.nome_curso.ToLower().Equals(nomeCurso.ToLower()))
                                continue;

                            curso.data_atualizacao = dataCarga;
                        }

                        curso.nome_curso = nomeCurso;


                        this.listaCursos.Add(curso);
                    }
                    catch (Exception ex)
                    {
                        if (curso != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(curso.GetType().Name, this.FormartarDadosRegistro(curso), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, "id_curso = " + idCurso, base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Dim_Curso item in this.listaCursos)
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

        private string FormartarDadosRegistro(Dim_Curso obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4}",
                                                        Constantes.TabelaSTGCurso, "id_curso", obj.id_curso,
                                                       "nome_curso", obj.nome_curso);
        }

        private Dim_Curso CriarObjInicial()
        {
            Dim_Curso curso = new Dim_Curso();

            curso.id_curso = base.CodigoParaCampoChaveNulo.ToString();
            curso.nome_curso = base.CodigoParaCampoChaveNulo.ToString();

            return curso;
        }
    }
}
