using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class Constantes
    {
        /// <summary>
        /// Milissegundos 300000 = 5 minutos
        /// </summary>
        public static int TempoEsperaConversao { get { return 300000; } }

        public static int CodigoParaCriarObjInicial { get { return -1; } }

        public static DateTime DataInicialCampoNulo { get { return new DateTime(1900, 01, 01); } }

        public static string CulturaInfo { get { return"pt-BR"; } }

        
        public static string TabelaSTGModulo { get { return "Modulo"; } }
        public static string TabelaSTGHospital { get { return "Hospital"; } }
        public static string TabelaSTGCurso { get { return "Curso"; } }
        public static string TabelaSTGTurma { get { return "Turma"; } }
        public static string TabelaSTGAluno { get { return "Aluno"; } }
        public static string TipoArquivoRJ { get { return "Professor"; } }
        public static string TabelaSTGAula { get { return "Aula"; } }
        public static string TabelaSTGPlantao { get { return "Plantao"; } }
        public static string TabelaSTGAvaliacao { get { return "Avaliacao"; } }
        public static string TabelaSTGPresenca { get { return "Presenca"; } }
        public static string TabelaSTGEspecialidade { get { return "Especialidade"; } }

        public static string TabelaSTGProfessor { get { return "Professor"; } }

        public static string TabelaSTGFatPermanenciaPlataforma { get { return "Permanencia"; } }


        public static string FormatoDataHora { get { return "dd/MM/yyyy HH:mm:ss"; } }
        public static string FormatoData { get { return "dd/MM/yyyy"; } }

        public static string FormatoDataBuscaWS { get { return "yyyy-MM-dd"; } }

        public static string ChaveAppFrequenciaExecucao { get { return "FrequenciaExecucao"; } }
        public static string ChaveAppHoraExecucao { get { return "HoraExecucao"; } }
        public static string ChaveAppMinutoExecucao { get { return "MinutoExecucao"; } }
        public static string ChaveAppExecucaoAcadaMinutos { get { return "ExecucaoAcadaMinutos"; } }
        public static string ChaveAppWSTimeOutLeituraXML { get { return "WSTimeOutLeituraXML"; } }
        

        public static string ChaveAppEnderecoWebServiceREST { get { return "EnderecoWebServiceREST"; } }
        public static string ChaveAppParametrosWS { get { return "ParametrosWS"; } }
        public static string ChaveAppValorParametroChave { get { return "ValorParametroChave"; } }
        public static string ChaveAppValoresParametroAcao { get { return "ValoresParametroAcao"; } }
        public static string ChaveAppCodigoParaCampoChaveNulo { get { return "CodigoParaCampoChaveNulo"; } }
        public static string ChaveAppQuantidadeDiasBusca { get { return "QuantidadeDiasBusca"; } }
         public static string ChaveAppBuscaIniciaDataAtualSistema { get { return "BuscaIniciaDataAtualSistema"; } }
        public static string ChaveAppDataInicioBuscaYYmmDD { get { return "DataInicioBuscaYYmmDD"; } }
        public static string ChaveAppDataFimDiasBuscaYYmmDD { get { return "DataFimBuscaYYmmDD"; } }


        /// <summary>
        /// Formar o TimeSpan em 0 horas 0 minutos 0 segundos
        /// </summary>
        /// <param name="timeSpan">TimeSpan</param>
        /// <returns>retorna o TimeSpan formatado</returns>
        public static string FormartarTimeSpan(TimeSpan timeSpan)
        {
            return string.Format("{0:%h} horas {0:%m} minutos {0:%s} segundos", timeSpan);
        }

    }


}
