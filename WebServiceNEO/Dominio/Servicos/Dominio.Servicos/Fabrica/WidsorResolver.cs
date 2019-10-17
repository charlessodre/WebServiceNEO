using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Domain.Service.Fabrica
{
    public class WindsorResolver
    {
        private static WindsorResolver instance;
        private static object initLock = new object();
        static WindsorContainer container;
        private static WindsorResolver GetFabrica()
        {
            lock (initLock)
            {
                if (instance == null)
                {
                    container = new WindsorContainer(new XmlInterpreter());
                    //LoadConfigurationLocal();

                    instance = new WindsorResolver();
                }
                return instance;
            }
        }
        public static object CreateInstance(Type tipo)
        {
            GetFabrica();
            object obj = container.Resolve(tipo);
            return obj;
        }
    }
}
