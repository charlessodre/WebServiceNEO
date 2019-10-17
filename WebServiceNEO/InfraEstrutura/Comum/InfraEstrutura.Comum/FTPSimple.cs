using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    /*****************************************************************
       
 *****************************************************************/
    public class FTPSimple
    {
        private string servidorFTP = null;
        private string usuario = null;
        private string senha = null;

        /* Construct Object */
        public FTPSimple(string pServidorFTP, string pUsuario, string pSenha)
        {
            servidorFTP = pServidorFTP;
            usuario = pUsuario;
            senha = pSenha;
        }


        /// <summary>
        /// Listar conteúdo do Diretório: arquivos e nomes somente
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório</param>
        /// <returns>Lista de Arquivos e Diretórios</returns>
        public string[] ListarDiretorioSimples(string pPathDiretorioFTP)
        {

            StringBuilder resultado = new StringBuilder();
            FtpWebRequest requisicaoFTP;

            try
            {
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP));
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                requisicaoFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse response = requisicaoFTP.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    resultado.Append(line);
                    resultado.Append("\n");
                    line = reader.ReadLine();
                }
                resultado.Remove(resultado.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return resultado.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Listar conteúdo do Diretório detalhado (Name, Size, Created, etc.)
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório</param>
        /// <returns>Lista de Arquivos e Diretórios</returns>
        public string[] ListarDiretorioDetalhado(string pPathDiretorioFTP)
        {

            StringBuilder resultado = new StringBuilder();
            FtpWebRequest requisicaoFTP;

            try
            {
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP));
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                requisicaoFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = requisicaoFTP.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);

                string line = reader.ReadLine();
                while (line != null)
                {
                    resultado.Append(line);
                    resultado.Append("\n");
                    line = reader.ReadLine();
                }
                resultado.Remove(resultado.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return resultado.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Download do arquivo.
        /// </summary>
        /// <param name="pDiretorioDestino">Diretório destino do arquivo.</param>
        /// <param name="pNomeArquivoFTP">Nome do arquivo no Servidor FTP.</param>
        /// <param name="pPathDiretorioFTP">Caminho do diretório no Servidor FTP.</param>

        public void Download(string pPathDiretorioFTP, string pNomeArquivoFTP, string pDiretorioDestino)
        {
            FtpWebRequest requisicaoFTP;
            FileStream outputStream;
            string arquivoLocal = pDiretorioDestino + "\\" + pNomeArquivoFTP;

            try
            {
                outputStream = new FileStream(arquivoLocal, FileMode.Create);

                if (pPathDiretorioFTP.Trim().Length > 0)
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP + "/" + pNomeArquivoFTP));
                else
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeArquivoFTP));



                requisicaoFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int lerContador;
                byte[] buffer = new byte[bufferSize];

                lerContador = ftpStream.Read(buffer, 0, bufferSize);
                while (lerContador > 0)
                {
                    outputStream.Write(buffer, 0, lerContador);
                    lerContador = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                File.Delete(arquivoLocal);
                throw ex;
            }
        }

        /// <summary>
        /// Upload do arquivo.
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório FTP</param>
        /// <param name="pNomeArquivo">Nome do arquiv.</param>
        public void Upload(string pPathDiretorioFTP, string pNomeArquivo)
        {

            FileInfo _arquivoInfo = new FileInfo(pNomeArquivo);

            string uri = "ftp://" + servidorFTP + "/" + _arquivoInfo.Name;

            FtpWebRequest requisicaoFTP;

            // Cria um objeto FtpWebRequest a partir da Uri fornecida
            if (pPathDiretorioFTP.Trim().Length > 0)
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP + "/" + _arquivoInfo.Name));
            else
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + _arquivoInfo.Name));

            // Fornece as credenciais de WebPermission
            requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);

            // Por padrão KeepAlive é true, 
            requisicaoFTP.KeepAlive = false;

            // Especifica o comando a ser executado
            requisicaoFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Especifica o tipo de dados a ser transferido
            requisicaoFTP.UseBinary = true;

            // Notifica o servidor seobre o tamanho do arquivo a enviar
            requisicaoFTP.ContentLength = _arquivoInfo.Length;

            // Define o tamanho do buffer para 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int _tamanhoConteudo;

            // Abre um stream (System.IO.FileStream) para o arquivo a ser enviado
            FileStream fs = _arquivoInfo.OpenRead();

            try
            {
                // Stream  para o qual o arquivo a ser enviado será escrito
                Stream strm = requisicaoFTP.GetRequestStream();

                // Lê a partir do arquivo stream, 2k por vez
                _tamanhoConteudo = fs.Read(buff, 0, buffLength);

                // ate o conteudo do stream terminar
                while (_tamanhoConteudo != 0)
                {
                    // Escreve o conteudo a partir do arquivo para o stream FTP 
                    strm.Write(buff, 0, _tamanhoConteudo);
                    _tamanhoConteudo = fs.Read(buff, 0, buffLength);
                }

                // Fecha o stream a requisição
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtém o tamanho de um arquivo.
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório FTP</param>
        /// <param name="pNomeArquivo">Nome do arquivo.</param>
        /// <returns>Tamanho do arquivo.</returns>
        public long ObterTamanhoArquivo(string pPathDiretorioFTP, string pNomeArquivo)
        {
            FtpWebRequest requisicaoFTP;
            long _tamanhoArquivo = 0;

            try
            {
                if (pPathDiretorioFTP.Trim().Length > 0)
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP + "/" + pNomeArquivo));
                else
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeArquivo));

                requisicaoFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                _tamanhoArquivo = response.ContentLength;

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _tamanhoArquivo;
        }


        /// <summary>
        /// Obtém a data de modificação de um arquivo.
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório FTP</param>
        /// <param name="pNomeArquivo">Nome do arquivo.</param>
        /// <returns>Data de modificação.</returns>
        public DateTime ObterDataModificacaoArquivo(string pPathDiretorioFTP, string pNomeArquivo)
        {
            FtpWebRequest requisicaoFTP;
            DateTime _dataModificacaoArquivo;

            try
            {
                if (pPathDiretorioFTP.Trim().Length > 0)
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP + "/" + pNomeArquivo));
                else
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeArquivo));

                requisicaoFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                _dataModificacaoArquivo = response.LastModified;

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _dataModificacaoArquivo;
        }

        /// <summary>
        /// Renomeio o um arquivo
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório FTP</param>
        /// <param name="pNomeAtualArquivo">Nome atual do arquivo.</param>
        /// <param name="pNovoNomeArquivo">Novo nome do arquivo.</param>
        public void Renomear(string pPathDiretorioFTP, string pNomeAtualArquivo, string pNovoNomeArquivo)
        {
            FtpWebRequest requisicaoFTP;
            try
            {
                if (pPathDiretorioFTP.Trim().Length > 0)
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP + "/" + pNomeAtualArquivo));
                else
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeAtualArquivo));

                requisicaoFTP.Method = WebRequestMethods.Ftp.Rename;
                requisicaoFTP.RenameTo = pNovoNomeArquivo;
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cria um diretório no servidor FTP.
        /// </summary>
        /// <param name="pPathDiretorioFTP">Caminho do diretório FTP</param>
        ///  <param name="pNomeDiretorio">Nome do Diretório</param>
        public void CriarDiretorio(string pPathDiretorioFTP, string pNomeDiretorio)
        {
            FtpWebRequest requisicaoFTP;
            try
            {

                if (pPathDiretorioFTP.Trim().Length > 0)
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pPathDiretorioFTP + "/" + pNomeDiretorio));
                else
                    requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeDiretorio));


                requisicaoFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                requisicaoFTP.UseBinary = true;
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove um arquivo
        /// </summary>
        /// <param name="pNomeArquivo">Nome do arquivo.</param>
        public void DeletarArquivo(string pNomeArquivo)
        {
            try
            {

                FtpWebRequest requisicaoFTP;
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeArquivo));

                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                requisicaoFTP.KeepAlive = false;
                requisicaoFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string resultado = String.Empty;
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);

                resultado = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove um diretório.
        /// </summary>
        /// <param name="pNomeDiretorio">Nome do diretório.</param>
        public void DeletarDiretorio(string pNomeDiretorio)
        {
            try
            {

                FtpWebRequest requisicaoFTP;
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + servidorFTP + "/" + pNomeDiretorio));

                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);
                requisicaoFTP.KeepAlive = false;
                requisicaoFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                string resultado = String.Empty;
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();

                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);

                resultado = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
