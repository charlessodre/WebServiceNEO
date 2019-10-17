
using System;
using System.Collections.Generic;
using System.Threading;

namespace Infrastructure.Common
{
    /// <summary>
    /// Cria uma nova agenda.
    /// </summary>
    /// <param name="horario"></param>
    public class Scheduler
    {
        private static Dictionary<string, Scheduler> SchedulePool = new Dictionary<string, Scheduler>();

        private System.Threading.Timer scheculetimer;
        private StartTime start;
        private TimerCallback MethodCallBack;
        private TimeSpan frequencia;
        private int maxexec = 0;

        /// <summary>
        /// Define ou Retorna o ID da tarefa
        /// </summary>
        public String ID { get; set; }
        /// <summary>
        /// Define ou Retorna a frequência com que a tarefa se repete após sua primeira execução.
        /// Utilize timespan(0) para não repetir
        /// </summary>
        public TimeSpan Frequencia { get { return frequencia; } set { frequencia = value; } }
        /// <summary>
        /// Retorna o número de vezes que que o método foi executado
        /// </summary>
        public int CountExec { get; set; }
        /// <summary>
        /// Define ou Retorna o número máximo de vezes para executar a tarefa
        /// </summary>
        public int MaxExec { get { return maxexec; } set { maxexec = value; } }
        /// <summary>
        /// Define ou Retorna o objeto Timer da tarefa
        /// </summary>
        public System.Threading.Timer ScheduleTimer { get { return scheculetimer; } set { scheculetimer = value; } }

        public Scheduler(TimerCallback Method)
        {
            MethodCallBack = Method;
        }

        public class StartTime
        {
            /// <summary>
            /// Dia da Semana: 0-Todos os dias, 1-Domingo, 2-Segunda, 3-Terça, 4-Quarta, 5-Quinta, 6-Sexta, 7-Sábado.
            /// </summary>
            public int Day { get; set; }
            public int Hour { get; set; }
            public int Minutes { get; set; }
        }

        /// <summary>
        /// Adiciona uma nova tarefa que será executada determidado horário
        /// </summary>
        /// <param name="State"></param>
        /// <param name="Execute"></param>
        public void AddSchedule(object State, StartTime horario)
        {
            start = horario;
            if (ID != null)
            {
                TimerCallback method = new TimerCallback(Loop);
                ScheduleTimer = new System.Threading.Timer(method, State, StartDelay(), Frequencia);

                SchedulePool.Add(ID, this);
            }
            else
            {
                throw new Exception("É preciso definir um ID para a tarefa!");
            }
        }

        /// <summary>
        /// Adiciona uma tarefa que será executada depois de um delay
        /// </summary>
        /// <param name="State"></param>
        /// <param name="Delay"></param>
        public void StartWithDelay(object State, TimeSpan Delay)
        {
            if (ID != null)
            {
                TimerCallback method = new TimerCallback(Loop);
                ScheduleTimer = new System.Threading.Timer(method, State, Delay, Frequencia);

                SchedulePool.Add(ID, this);
            }
            else
            {
                throw new Exception("É preciso definir um ID para a tarefa!");
            }
        }

        private void Loop(object State)
        {
            //Adicinona +1 à contagem de vezes que a tarefa foi executada
            CountExec++;
            if (MaxExec > 0)
            {
                if (CountExec > MaxExec)
                {
                    Dispose();
                    return;
                }
            }

            //Invoca o metedo
            MethodCallBack.Invoke(State);

            //Confere se a tarefa se repete. Se não repetir libera a memória
            if (Frequencia.Ticks == 0)
            {
                Dispose();
            }
        }

        /// <summary>
        /// Retorna um TimeSpan com o dalay inicial, baseado na data agendada
        /// </summary>
        /// <returns></returns>
        private TimeSpan StartDelay()
        {
            //Define os minutos antes de definir a hora, pois aqueles podem influenciar nestes
            int waitminute = GetMinutes();
            int waithour = GetHour();
            return new TimeSpan(waithour, waitminute, 0);
        }

        private Infrastructure.Common.Enumeradores.DiaDaSemana GetDay(int dayOfWeek)
        {

            switch (dayOfWeek)
            {
                case 1:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Sunday;
                case 2:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Monday;
                case 3:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Tuesday;
                case 4:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Wednesday;
                case 5:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Thursday;
                case 6:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Friday;
                case 7:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.Saturday;
                default:
                    return Infrastructure.Common.Enumeradores.DiaDaSemana.AllDays;
            }

        }

        private int GetMinutes()
        {
            if (start.Minutes > DateTime.Now.Minute)
            {
                return start.Minutes - DateTime.Now.Minute;
            }
            else
            {
                start.Hour--;
                return start.Minutes - DateTime.Now.Minute + 60;
            }
        }

        private int GetHour()
        {
            if (start.Hour > DateTime.Now.Hour)
            {
                return start.Hour - DateTime.Now.Hour;
            }
            else if (start.Hour < DateTime.Now.Hour)
            {
                return start.Hour - DateTime.Now.Hour + 24;
            }
            else
            {
                return 0;
            }
        }



        public void Dispose()
        {
            ScheduleTimer.Dispose();
            if (SchedulePool.ContainsKey(ID))
                SchedulePool.Remove(ID);
        }

        public class Control
        {
            /// <summary>
            /// Retorna a referencia para a tarefa
            /// </summary>
            /// <param name="ScheduleId"></param>
            /// <returns></returns>
            public static Scheduler GetTimer(String ScheduleId)
            {
                if (SchedulePool.ContainsKey(ScheduleId))
                {
                    return SchedulePool[ScheduleId];
                }
                else
                {
                    throw new Exception("A tarefa especificada para cancelar não existe");
                }
            }

            /// <summary>
            /// Cancela a tarefa atual
            /// </summary>
            /// <param name="ScheduleId"></param>
            public static void Cancel(String ScheduleId)
            {
                Scheduler schedule = GetTimer(ScheduleId);
                schedule.Dispose();
            }

            /// <summary>
            /// Adiciona um int à quantidade máxima de execuções. Se não houver limite, define um.
            /// </summary>
            /// <param name="ScheduleId"></param>
            /// <param name="Times"></param>
            public static void AddExec(String ScheduleId, int Times)
            {
                Scheduler schedule = GetTimer(ScheduleId);
                schedule.MaxExec += Times;
            }

            /// <summary>
            /// Executa o método agendado.
            /// </summary>
            /// <param name="ScheduleId"></param>
            /// <param name="State"></param>
            public static void Execute(String ScheduleId, object State)
            {
                Scheduler schedule = GetTimer(ScheduleId);
                schedule.MethodCallBack.Invoke(State);
            }
        }
    }

    //Como usar.
    //Scheduler sd = new Scheduler(tempo =>
    //                                { Meu MEtodo(); }
    //                             );

    //sd.ID = "Agendamento";
    //sd.AddSchedule(null, new Scheduler.StartTime() { Hour = 09, Minutes = 25 });

}
