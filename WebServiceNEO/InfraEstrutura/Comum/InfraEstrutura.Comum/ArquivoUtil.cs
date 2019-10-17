using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class ArquivoUtil
    {

        public static void ExcluirArquivo(string diretorio, string arquivo)
        {
            string localArquivo = Path.Combine(diretorio, arquivo);

            if (File.Exists(localArquivo))
                File.Delete(localArquivo);
        }
        public static void MergeTextFiles(string targetFileName, string sourcePath, string searchPattern = "*.*", int bufferSize = 0)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                sourcePath = Directory.GetCurrentDirectory();
            }
            if (targetFileName.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException("Diretório fonte especificado contém caracteres inválidos", "sourcePath");
            }
            if (string.IsNullOrEmpty(targetFileName))
            {
                throw new ArgumentException("Nome do arquivo destino precisa ser especificado", "targetFileName");
            }
            if (string.IsNullOrEmpty(targetFileName))
            {
                throw new ArgumentException("Nome do arquivo destino precisa ser especificado", "targetFileName");
            }

            var targetFullFileName = Path.Combine(sourcePath, targetFileName);


            if (bufferSize == 0)
            {
                File.Delete(targetFullFileName);

                foreach (var file in Directory.GetFiles(sourcePath, searchPattern))
                {
                    if (file != targetFullFileName)
                    {
                        File.AppendAllText(targetFullFileName, File.ReadAllText(file));
                    }
                }
            }
            else
            {
                using (var targetFile = File.Create(targetFullFileName, bufferSize))
                {

                    foreach (var file in Directory.GetFiles(sourcePath, searchPattern))
                    {
                        if (file != targetFullFileName)
                        {
                            using (var sourceFile = File.OpenRead(file))
                            {
                                var buffer = new byte[bufferSize];
                                int bytesRead;
                                while ((bytesRead = sourceFile.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    targetFile.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                }
            }
        }


    }
}
