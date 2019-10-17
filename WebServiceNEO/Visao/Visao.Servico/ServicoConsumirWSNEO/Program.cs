using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicoConsumirWSNEO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if (!DEBUG)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ServicoConsumirWSNEO() 
            };
            ServiceBase.Run(ServicesToRun);
#else

            // Debug code: Permite debugar um código sem se passar por um Windows Service.

            // Defina qual método deseja chamar no inicio do Debug (ex. MetodoRealizaFuncao)

            // Depois de debugar basta compilar em Release e instalar para funcionar normalmente.

            ServicoConsumirWSNEO service = new ServicoConsumirWSNEO();

            // Chamada do seu método para Debug.

            service.InicioParaDebug();

            // Coloque sempre um breakpoint para o ponto de parada do seu código.

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#endif

        }
    }
}
