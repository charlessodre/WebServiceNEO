using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class RegistroLog
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void RegistarLogInfo(string mensagemInfo)
        {
            _logger.Info(mensagemInfo);
        }

        public static void RegistarLogErro(string mensagemErro, Exception ex)
        {
            _logger.ErrorException(mensagemErro, ex);
        }
    }
}
