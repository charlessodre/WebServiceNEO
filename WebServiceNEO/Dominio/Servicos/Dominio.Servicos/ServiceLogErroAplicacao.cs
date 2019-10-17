using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Interfaces;
using Domain.Entity;
using Infrastructure.Common;

namespace Domain.Service
{
    public class ServiceLogErroAplicacao : Service<LogErroAplicacao>, IServiceLogErroAplicacao
    {
        IRepositorioLogErroAplicacao repositorioLogErroAplicacao;
        public ServiceLogErroAplicacao(IRepositorioLogErroAplicacao repositorioLogErroAplicacao)
        {
            this.repositorioLogErroAplicacao = repositorioLogErroAplicacao;
        }

        

        public void Insert(Exception excecao)
        {
            this.Insert(string.Empty , excecao);
        }

        public void Insert(string mensagem, Exception excecao)
        {
            try
            {
                LogErroAplicacao log = new LogErroAplicacao();

                if (excecao.InnerException != null)
                    log.Excecao = excecao.InnerException.Message;

                log.Data = DateTime.Now;
                log.Mensagem = excecao.Message;

                if (!mensagem.Equals(string.Empty))
                    log.Mensagem = mensagem + " - " + excecao.Message;

                log.Origem = excecao.Source;
                log.CodExcecao = excecao.HResult;

                this.repositorioLogErroAplicacao.Insert(log);

                RegistroLog.RegistarLogErro(Mensagem.ErroSistema + "( " + log.Mensagem + " )", excecao);
            }
            catch (Exception ex)
            {
                GravarAquivoErro(Mensagem.ErroSalvarBD + "( " + ex.Message + " )", ex);
            }
        }



        public static void GravarAquivoErro(string mensagemErro, Exception excecao)
        {
            try
            {
                RegistroLog.RegistarLogErro(mensagemErro, excecao);
            }
            catch (Exception ex)
            {
                throw new Exception(Mensagem.ErroFatalSistema + "( " + ex.Message + " )", ex);
            }
        }


        
    }
}
