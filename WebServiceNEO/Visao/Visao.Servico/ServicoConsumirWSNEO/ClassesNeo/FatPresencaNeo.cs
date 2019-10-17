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
    public class FatPresencaNeo : BaseNeo
    {
        IList<Fato_Presenca> listaPresencas;
        Application.FatPresencaApplication app = new Application.FatPresencaApplication();

        public FatPresencaNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao, int codigoParaCampoChaveNulo, string dataInicioBuscaYYmmDD, string dataFimBuscaYYmmDD, int wsTimeOutLeituraXML)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao, codigoParaCampoChaveNulo, dataInicioBuscaYYmmDD, dataFimBuscaYYmmDD, wsTimeOutLeituraXML)
        {


        }

        public override void EfetuarLeituraXml()
        {
            Fato_Presenca presenca = null;



            DataSet dsStaging = base.ImportarXmltoDataSet();

            this.listaPresencas = new List<Fato_Presenca>();

            if (dsStaging.Tables.Count == 2)
            {
                DateTime dataCarga = DateTime.Now;

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {

                    presenca = null;

                    string idAluno = base.CodigoParaCampoChaveNulo.ToString();
                    string idAula = base.CodigoParaCampoChaveNulo.ToString();
                    string idTurma = base.CodigoParaCampoChaveNulo.ToString();
                    string idMatricula = base.CodigoParaCampoChaveNulo.ToString();

                    int qtdAtividadesDisponiveis = 0;
                    int qtdAtividadesRealizadas = 0;
                   
                    Nullable<DateTime> dataHoraPresenca = null;

                    try
                    {

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aluno_preceptor"]))
                            idAluno = Convert.ToString(item["id_aluno_preceptor"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aula"]))
                            idAula = Convert.ToString(item["id_aula"]);

                         string realizadaPresenca = Convert.ToString(item["realizada_presenca"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["DATAHORA_presenca"]))
                            dataHoraPresenca = DateTime.Parse(Convert.ToDateTime(item["DATAHORA_presenca"]).ToString(Constantes.FormatoDataHora));

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["id_aula"]))
                            idAula = Convert.ToString(item["id_aula"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_turma"]))
                            idTurma = Convert.ToString(item["ID_turma"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ID_matricula"]))
                            idMatricula = Convert.ToString(item["ID_matricula"]);

                        if (TratarDadosUtil.ValorNaoNuloOuVazio(item["ATIVIDADES_assistidas"]))
                        {
                            string[] Atividades = Convert.ToString(item["ATIVIDADES_assistidas"]).Split('/');

                            qtdAtividadesRealizadas = Convert.ToInt32(Atividades[0]);
                            qtdAtividadesDisponiveis = Convert.ToInt32(Atividades[1]);
                        }

                        presenca = this.BuscarPresenca(idAluno, idAula, idTurma, idMatricula);

                        if (presenca == null)
                        {
                            presenca = new Fato_Presenca();
                            presenca.id_aluno_preceptor = idAluno;
                            presenca.id_aula = idAula;
                            presenca.id_turma = idTurma;
                            presenca.idmatricula_aluno = idMatricula;

                            presenca.data_insercao = dataCarga;
                        }
                        else
                        {
                            if (
                                presenca.datahora_presenca == dataHoraPresenca &&
                                presenca.realizada_presenca == realizadaPresenca &&
                                presenca.qtd_atividades_disponiveis == qtdAtividadesDisponiveis &&
                                presenca.qtd_atividades_realizadas == qtdAtividadesRealizadas
                                )
                                continue;

                            presenca.data_atualizacao = dataCarga;
                        }

                        presenca.realizada_presenca = realizadaPresenca;
                        presenca.datahora_presenca = dataHoraPresenca;
                        presenca.qtd_atividades_disponiveis = qtdAtividadesDisponiveis;
                        presenca.qtd_atividades_realizadas = qtdAtividadesRealizadas;


                        this.listaPresencas.Add(presenca);
                    }
                    catch (Exception ex)
                    {
                        if (presenca != null)
                        {
                            BaseNeo.SalvarLogErroAplicacao(presenca.GetType().Name, this.FormartarDadosRegistro(presenca), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
                        }
                        else
                        {
                            BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, string.Format("id_aluno_preceptor = {0} | id_aula = {1} | id_turma = {2} | idmatricula_aluno = {3}", idAluno, idAula, idTurma, idMatricula), base.FormatarMensagemLog(Mensagem.XMLLeituraErro), ex);
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
            foreach (Fato_Presenca item in this.listaPresencas)
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

        public Fato_Presenca BuscarPresenca(string idAluno, string idAula, string idTurma, string idMatricula)
        {
            return this.app.repositorioPresenca.GetbyIDs(idAluno, idAula, idTurma, idMatricula);
        }

        private string FormartarDadosRegistro(Fato_Presenca obj)
        {
            return string.Format("{0} | {1} = {2} | {3} = {4} | {5} = {6} | {7} = {8} | {9} = {10} | {11} = {12} | {13} = {14} | {15} = {16}",
                                                        Constantes.TabelaSTGPresenca,
                                                         "id_aluno_preceptor", obj.id_aluno_preceptor,
                                                         "id_aula", obj.id_aula,
                                                         "id_turma", obj.id_turma,
                                                         "idmatricula_aluno", obj.idmatricula_aluno,
                                                         "realizada_presenca", obj.realizada_presenca,
                                                         "datahora_presenca", obj.datahora_presenca,
                                                         "qtd_atividades_disponiveis", obj.qtd_atividades_disponiveis,
                                                         "qtd_atividades_relizadas", obj.qtd_atividades_realizadas);


        }
    }
}
