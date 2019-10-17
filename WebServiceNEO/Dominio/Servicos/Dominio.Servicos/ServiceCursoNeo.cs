using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;

namespace Domain.Service
{
    public class ServiceCursoNeo : ServiceBaseNeo
    {
        IList<Dim_Curso> listaCursos;
        public ServiceCursoNeo(string enderecoWebServiceREST, string[] parametrosWS, string chave, string acao)
            : base(enderecoWebServiceREST, parametrosWS, chave, acao)
        {


        }

        public override void EfetuarLeituraXml()
        {
            this.listaCursos = new List<Dim_Curso>();

            try
            {
                DataSet dsStaging = base.ImportarXmltoDataSet();

                foreach (DataRow item in dsStaging.Tables[1].Rows)
                {
                    Dim_Curso curso = new Dim_Curso();

                    curso.id_curso = Convert.ToInt32(item["id_curso"]);
                    curso.nome_curso = item["nome_curso"].ToString();
                    curso.data_atualizacao = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Constantes.TabelaSTGCurso + "." + Mensagem.XMLLeituraErro, ex.InnerException);
            }

        }

        public override void SalvarDadosXmlBD()
        {
            try
            {
                foreach (Dim_Curso item in  this.listaCursos)
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Constantes.TabelaSTGCurso + "." + Mensagem.ErroSalvarBD, ex.InnerException);
            }
        }
    }
}
