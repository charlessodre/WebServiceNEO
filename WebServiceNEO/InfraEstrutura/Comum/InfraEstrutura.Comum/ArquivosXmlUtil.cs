using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace Infrastructure.Common
{
    public static class ArquivosXmlUtil
    {
        public static void CriarArquivoXML(string nomeArquivo, string pathArquivo , string elementoRaiz)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(pathArquivo + "\\" + nomeArquivo +".xml", null);

                writer.WriteStartDocument(); 
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement(elementoRaiz);
                writer.WriteEndElement();
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
