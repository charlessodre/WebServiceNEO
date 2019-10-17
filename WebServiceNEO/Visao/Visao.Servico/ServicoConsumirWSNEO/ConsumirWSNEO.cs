using Application;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Common;
using Quartz;
using Quartz.Impl;
using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace ServicoConsumirWSNEO
{
    public partial class ServicoConsumirWSNEO : ServiceBase, IJob
    {
        private IScheduler scheduler = null;
        private static bool emExecucao = false;

        private string enderecoWebServiceREST;
        private string valorParametroChave;
        private string[] parametrosWS;
        private string[] metodosAcao;

        private int diaAgendamento;
        private int hora;
        private int minuto;
        private int executarAcadaMinuto;
        private int quantidadeDiasBusca = 0;
        private bool buscaIniciaDataAtualSistema = true;

        private DateTime dataInicioBusca = DateTime.Today;
        private DateTime dataFimBusca = DateTime.Today;
        private int wsTimeOutLeituraXML;


        private int codigoParaCampoChaveNulo;

        public ServicoConsumirWSNEO()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RegistroLog.RegistarLogInfo("##################################################################################");

            RegistroLog.RegistarLogInfo(Mensagem.ServicoInicio);

            this.CarregarConfiguracoes();
            this.DefinirAgendamentoExecucao();

            RegistroLog.RegistarLogInfo(Mensagem.ServicoFim);

            RegistroLog.RegistarLogInfo("##################################################################################");

        }

        public void InicioParaDebug()
        {
            try
            {

                RegistroLog.RegistarLogInfo(Mensagem.ServicoInicio);

                this.CarregarConfiguracoes();
                //this.DefinirAgendamentoExecucao();

                this.ObterDadosWs();

                RegistroLog.RegistarLogInfo(Mensagem.ServicoFim);

            }
            catch (Exception ex)
            {
                BaseNeo.SalvarLogErroAplicacao(ex);
            }
        }

        protected override void OnStop()
        {
            this.scheduler.Clear();
            RegistroLog.RegistarLogInfo(Mensagem.ServicoStop);
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                RegistroLog.RegistarLogInfo("***************************************************************************************************************");

                if (emExecucao)
                {
                    RegistroLog.RegistarLogInfo(Mensagem.ServicoInicio);
                    RegistroLog.RegistarLogInfo(Mensagem.ServicoUltimaExecucao + " (" + context.FireTimeUtc.Value.LocalDateTime.ToString(Constantes.FormatoDataHora) + ")");
                    RegistroLog.RegistarLogInfo(Mensagem.ServicoProximaExecucao + " (" + context.NextFireTimeUtc.Value.LocalDateTime.ToString(Constantes.FormatoDataHora) + ")");
                }
                else
                {
                    this.CarregarConfiguracoes();

                    //Inicia a o tratamento dos arquivos.
                    this.IniciarExecucao(context.FireTimeUtc.Value.LocalDateTime, context.NextFireTimeUtc.Value.LocalDateTime);

                }
                RegistroLog.RegistarLogInfo("***************************************************************************************************************");

            }
            catch (Exception ex)
            {
                BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, Mensagem.ServicoErro, ex);
            }
        }

        private void DefinirAgendamentoExecucao()
        {
            RegistroLog.RegistarLogInfo(Mensagem.SistemaAgendamentoInicio);

            ISchedulerFactory schedFact = new StdSchedulerFactory();
            ITrigger trigger;

            this.scheduler = schedFact.GetScheduler();

            IJobDetail job = JobBuilder.Create<ServicoConsumirWSNEO>()
                .WithIdentity("JobServico", "groupServico")
                .Build();


            if (this.executarAcadaMinuto > 0)
            {
                trigger = TriggerBuilder.Create()
                                      .StartNow()
                                      .WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForever(this.executarAcadaMinuto))
                                      .Build();
            }
            else
            {
                CronScheduleBuilder cronScheduleBuilder;

                Enumeradores.DiaDaSemana dia = Enumeradores.ConverterObject2DiaDaSemana(this.diaAgendamento);

                if (dia == Enumeradores.DiaDaSemana.AllDays)
                    cronScheduleBuilder = CronScheduleBuilder.DailyAtHourAndMinute(this.hora, this.minuto);
                else
                    cronScheduleBuilder = CronScheduleBuilder.WeeklyOnDayAndHourAndMinute((DayOfWeek)dia, this.hora, this.minuto);

                trigger = TriggerBuilder.Create()
                                            .StartNow()
                                            .WithSchedule(cronScheduleBuilder)
                                            .Build();
            }

            this.scheduler.ScheduleJob(job, trigger);
            this.scheduler.Start();

            RegistroLog.RegistarLogInfo(Mensagem.ServicoProximaExecucao + " - " + trigger.GetNextFireTimeUtc().Value.LocalDateTime.ToString(Constantes.FormatoDataHora));

            RegistroLog.RegistarLogInfo(Mensagem.SistemaAgendamentoFim);

        }

        private void CarregarConfiguracoes()
        {

            RegistroLog.RegistarLogInfo(Mensagem.ArqConfiObtendoInfo);

            this.enderecoWebServiceREST = AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppEnderecoWebServiceREST);

            this.parametrosWS = AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppParametrosWS).Split(';');
            this.metodosAcao = AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppValoresParametroAcao).Split(';');

            this.valorParametroChave = AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppValorParametroChave);

            this.diaAgendamento = Convert.ToInt32(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppFrequenciaExecucao));
            this.hora = int.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppHoraExecucao));
            this.minuto = int.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppMinutoExecucao));

            this.executarAcadaMinuto = int.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppExecucaoAcadaMinutos));

            this.codigoParaCampoChaveNulo = int.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppCodigoParaCampoChaveNulo));


            this.wsTimeOutLeituraXML = int.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppWSTimeOutLeituraXML));

            this.quantidadeDiasBusca = int.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppQuantidadeDiasBusca));

            this.buscaIniciaDataAtualSistema = bool.Parse(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppBuscaIniciaDataAtualSistema));

            if (!this.buscaIniciaDataAtualSistema)
            {
                if (TratarDadosUtil.ValorNaoNuloOuVazio(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppDataInicioBuscaYYmmDD)))
                    this.dataInicioBusca = Convert.ToDateTime(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppDataInicioBuscaYYmmDD));

                if (TratarDadosUtil.ValorNaoNuloOuVazio(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppDataFimDiasBuscaYYmmDD)))
                    this.dataFimBusca = Convert.ToDateTime(AppConfigUtil.ObterValorAppSettings(Constantes.ChaveAppDataFimDiasBuscaYYmmDD));

            }

            RegistroLog.RegistarLogInfo(Mensagem.ArqConfiObterInfoSucesso);
        }

        private void IniciarExecucao(DateTime ultimaExcucao, DateTime proximaExecucao)
        {
            emExecucao = true;

            Stopwatch tempoExecucao = new Stopwatch();

            tempoExecucao.Start();

            RegistroLog.RegistarLogInfo(Mensagem.ServicoInicio);

            try
            {
                this.ObterDadosWs();
            }
            catch (Exception ex)
            {
                BaseNeo.SalvarLogErroAplicacao(this.GetType().Name, Mensagem.ServicoErro, ex);
            }


            RegistroLog.RegistarLogInfo("-----------------------------------------------------------------------------------------------");

            RegistroLog.RegistarLogInfo(Mensagem.ServicoUltimaExecucao + " - " + ultimaExcucao.ToString(Constantes.FormatoDataHora));
            RegistroLog.RegistarLogInfo(Mensagem.ServicoProximaExecucao + " - " + proximaExecucao.ToString(Constantes.FormatoDataHora));

            RegistroLog.RegistarLogInfo("-----------------------------------------------------------------------------------------------");

            tempoExecucao.Stop();

            RegistroLog.RegistarLogInfo(Mensagem.ServicoTempoExecucao + " - " + tempoExecucao.Elapsed.ToString());

            emExecucao = false;

            RegistroLog.RegistarLogInfo("-----------------------------------------------------------------------------------------------");

        }

        private void ObterDadosWs()
        {
            DateTime dataInicioTemp;
            DateTime dataFimTemp;

            if (this.buscaIniciaDataAtualSistema)
            {
                dataInicioTemp = DateTime.Today.AddDays(-this.quantidadeDiasBusca);
                dataFimTemp = DateTime.Today;
            }
            else
            {
                dataInicioTemp = this.dataInicioBusca;
                dataFimTemp = dataInicioTemp.AddDays(this.quantidadeDiasBusca);
            }

            while (dataInicioTemp < this.dataFimBusca)
            {
                Stopwatch tempoExecucao = new Stopwatch();

                tempoExecucao.Start();

                RegistroLog.RegistarLogInfo("----------------------------------------------------------------------------------------");
                RegistroLog.RegistarLogInfo(Mensagem.BuscaDadosPeriodoInicio + " Data Início: (" + dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS) + ") Data Fim: (" + dataFimTemp.ToString(Constantes.FormatoDataBuscaWS) + ")");


                foreach (string item in this.metodosAcao)
                {
                    if (item.Contains(Constantes.TabelaSTGCurso))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoCurso);
                        this.SalvarXmlBD(new CursoNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));

                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGEspecialidade))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoEspecialidade);
                        this.SalvarXmlBD(new EspecialidadeNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGModulo))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoModulo);
                        this.SalvarXmlBD(new ModuloNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }


                    if (item.Contains(Constantes.TabelaSTGHospital))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoHospital);
                        this.SalvarXmlBD(new HospitalNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGProfessor))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoProfessor);
                        this.SalvarXmlBD(new ProfessorNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGTurma))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoTurma);
                        this.SalvarXmlBD(new TurmaNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGAluno))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoAluno);
                        this.SalvarXmlBD(new AlunoNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }


                    if (item.Contains(Constantes.TabelaSTGAula))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoAula);
                        this.SalvarXmlBD(new AulaNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }


                    if (item.Contains(Constantes.TabelaSTGPresenca))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoPresenca);
                        this.SalvarXmlBD(new FatPresencaNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGPlantao))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoPlantao);
                        this.SalvarXmlBD(new FatPlantaoNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGAvaliacao))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoAvaliacao);
                        this.SalvarXmlBD(new FatAvaliacaoNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }

                    if (item.Contains(Constantes.TabelaSTGFatPermanenciaPlataforma))
                    {
                        RegistroLog.RegistarLogInfo(Mensagem.NeoPermanenciaPlataforma);
                        this.SalvarXmlBD(new FatPermanenciaPlataformaNeo(this.enderecoWebServiceREST, this.parametrosWS, valorParametroChave, item, this.codigoParaCampoChaveNulo, dataInicioTemp.ToString(Constantes.FormatoDataBuscaWS), dataFimTemp.ToString(Constantes.FormatoDataBuscaWS), this.wsTimeOutLeituraXML));
                        continue;
                    }
                }

                tempoExecucao.Stop();

                RegistroLog.RegistarLogInfo(Mensagem.BuscaDadosTempoExecucao + " - " + tempoExecucao.Elapsed.ToString());
                                
                dataInicioTemp = dataFimTemp.AddDays(1);
                dataFimTemp = dataInicioTemp.AddDays(this.quantidadeDiasBusca);
            }

        }

        private void SalvarXmlBD(BaseNeo baseNeo)
        {
            RegistroLog.RegistarLogInfo(Mensagem.WSLeituraDadosInicio);

            RegistroLog.RegistarLogInfo(Mensagem.XmltoDataSetInicio);
            baseNeo.EfetuarLeituraXml();

            RegistroLog.RegistarLogInfo(Mensagem.XmltoDataSetFim);

            RegistroLog.RegistarLogInfo(Mensagem.XmlSalvarDadosBDInicio);
            baseNeo.SalvarDadosXmlBD();

            RegistroLog.RegistarLogInfo(Mensagem.XmlSalvarDadosBDFim);

            RegistroLog.RegistarLogInfo(Mensagem.WSLeituraDadosFim);
        }
    }
}
