using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class DirectorioUtil
    {

        /// <summary>
        /// Lista todos os arquivos de todos os diretórios e subdiretórios.
        /// </summary>
        /// <param name="pDiretorio">Diretório dos arquivos</param>
        /// <returns>Retorna os arquivos</returns>
        public static IList<FileInfo> ListarTodosArquivos(string pDiretorio)
        {
            DirectoryInfo Dir = new DirectoryInfo(pDiretorio);

            return Dir.GetFiles("*", SearchOption.AllDirectories).ToList();
        }

        public static FileInfo ObterArquivo(string pDiretorio, string pNomeArquivo)
        {
            DirectoryInfo Dir = new DirectoryInfo(pDiretorio);

            FileInfo file = Dir.GetFiles(pNomeArquivo, SearchOption.AllDirectories).FirstOrDefault();

            return file;
        }

        public static bool VerificarArquivoExiste(string pDiretorio, string pNomeArquivo)
        {
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(pDiretorio);

                FileInfo file = Dir.GetFiles(pNomeArquivo, SearchOption.AllDirectories).FirstOrDefault();

                return file.Exists;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Lista todos os Subdiretórios
        /// </summary>
        /// <param name="pDiretorio">Diretório Raiz</param>
        /// <returns>Retorna os subdiretórios.</returns>
        public static DirectoryInfo[] ListarSubDiretorios(string pDiretorio)
        {
            DirectoryInfo Dir = new DirectoryInfo(pDiretorio);

            DirectoryInfo[] diretorios = Dir.GetDirectories();

            return diretorios;
        }


        /// <summary>
        /// Lista todos os Subdiretórios
        /// </summary>
        /// <param name="pDiretorio">Diretório Raiz</param>
        /// <returns>Retorna os subdiretórios.</returns>
        public static void CriarDiretorio(string pDiretorio)
        {
            DirectoryInfo dir = new DirectoryInfo(pDiretorio);

            if (!dir.Exists)
                dir.Create();
        }



    }
}
