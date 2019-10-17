using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class AppConfigUtil
    {
        /// <summary>
        /// Atualiza uma chave no arquivo de configuração.
        /// </summary>
        /// <param name="chave">Chave</param>
        /// <param name="valor">Valor</param>
        public static void AtualizarSecaoAppSettings(string chave, string valor)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Add an Application Setting.
            config.AppSettings.Settings.Remove(chave);
            config.AppSettings.Settings.Add(chave, valor);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");

        }

        /// <summary>
        /// Altera uma chave e cria uma cópia do arquivo de Configuração da aplicação.
        /// </summary>

        /// <param name="pathDestino">Destino da cópia criada</param>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        public static void CriarCopiaAppConfig(string pathDestino, string nomeArquivo)
        {
            string novoArquivo = pathDestino + "\\" + nomeArquivo + ".config";

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.SaveAs(novoArquivo, ConfigurationSaveMode.Modified);

        }

        /// <summary>
        /// Obtêm o valor da chave do AppSettings
        /// </summary>
        /// <param name="chave">Chave</param>
        /// <returns>Retorna no valor da chave</returns>
        public static string ObterValorAppSettings(string chave)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return config.AppSettings.Settings[chave].Value;

        }

        /// <summary>
        /// Obtêm a ConnectionString da chave especificada
        /// </summary>
        /// <param name="chave">Chave</param>
        /// <returns>Retorna no valor da chave</returns>
        public static string ObterConnectionString(string chave)
        {
            return ConfigurationManager.ConnectionStrings[chave].ConnectionString;

        }

    }

}
