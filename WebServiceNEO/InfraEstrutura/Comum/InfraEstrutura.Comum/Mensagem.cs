using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Common
{
    public static class Mensagem
    {
        #region Mensagens NEO

        public static string NeoModulo
        {
            get { return "Leitura do dos dados da Tabela Módulo."; }
        }

        public static string NeoHospital
        {
            get { return "Leitura do dos dados da Tabela Hospital."; }
        }

        public static string NeoCurso
        {
            get { return "Leitura do dos dados da Tabela Curso."; }
        }

        public static string NeoTurma
        {
            get { return "Leitura do dos dados da Tabela Turma."; }
        }

        public static string NeoAluno
        {
            get { return "Leitura do dos dados da Tabela Aluno."; }
        }

        public static string NeoProfessor
        {
            get { return "Leitura do dos dados da Tabela Professor."; }
        }

        public static string NeoAula
        {
            get { return "Leitura do dos dados da Tabela Aula."; }
        }

        public static string NeoPlantao
        {
            get { return "Leitura do dos dados da Tabela Plantao."; }
        }

        public static string NeoAvaliacao
        {
            get { return "Leitura do dos dados da Tabela Avaliacao."; }
        }

        public static string NeoPresenca
        {
            get { return "Leitura do dos dados da Tabela Presenca."; }
        }

        public static string NeoEspecialidade
        {
            get { return "Leitura do dos dados da Tabela Especialidade."; }
        }

        public static string NeoPermanenciaPlataforma
        {
            get { return "Leitura do dos dados da Tabela Fato_Permanencia_Plataforma."; }
        }
        #endregion

        #region Mensagens Busca de Dados

        public static string BuscaDadosPeriodo
        {
            get { return "Período da Busca"; }
        }

        public static string BuscaDadosPeriodoInicio
        {
            get { return "Início da busca de dados para o período."; }
        }

        public static string BuscaDadosPeriodoFim
        {
            get { return "Fim da busca de dados para o período."; }
        }

        public static string BuscaDadosTempoExecucao
        {
            get { return "Tempo total de execução da busca de dados: "; }
        }

        #endregion

        #region Mensagens Sistema

        public static string SistemaErroLogar
        {
            get { return "Erro ao efetuar login no sistema."; }
        }

        public static string SistemaSair
        {
            get { return "Deseja realmente fechar o Sistema?"; }
        }

        public static string SistemaInicio { get { return "Iniciando a execução."; } }

        public static string SistemaFim
        {
            get { return "Fim da execução."; }
        }

        public static string SistemaAgendamentoInicio
        {
            get { return "Iniciando o Agendamento do sistema."; }
        }

        public static string SistemaAgendamentoFim
        {
            get { return "Finalizando o Agendamento do sistema."; }
        }

        public static string SistemaAguardandoExecucao
        {
            get { return "Aguardando Execução."; }
        }

        public static string SistemaEmExecucao
        {
            get { return "Sistema em Execução."; }
        }

        public static string SistemaUltimaExecucao
        {
            get { return "Última execução do sistema."; }
        }

        public static string SistemaProximaExecucao
        {
            get { return "Próxima execução do sistema."; }
        }

        public static string SistemaTempoExecucao
        {
            get { return "Tempo total de execução: "; }
        }



        #endregion

        #region Mensagens Serviço


        public static string ServicoEmExecucao
        {
            get { return "Serviço em Execução."; }
        }

        public static string ServicoUltimaExecucao
        {
            get { return "Última execução do Serviço."; }
        }

        public static string ServicoProximaExecucao
        {
            get { return "Próxima execução do Serviço."; }
        }

        public static string ServicoTempoExecucao
        {
            get { return "Tempo total de execução: "; }
        }

        public static string ServicoInicio
        {
            get { return "Início da execução do Serviço."; }
        }

        public static string ServicoFim
        {
            get { return "Fim da execução do Serviço."; }
        }

        public static string ServicoStop
        {
            get { return "O serviço foi parado."; }
        }

        public static string ServicoErro
        {
            get { return "Ocorreu um erro no Serviço."; }
        }

        #endregion

        #region Mensagens WebServices
        public static string WSFalhaObterDados
        {
            get { return "Falha ao obter dados do Web Service."; }
        }

        public static string WSFalhaObterXml
        {
            get { return "Falha ao obter o XML do Web Service."; }
        }

        public static string WSLeituraDadosInicio
        {
            get { return "Início da leitura dos dados do Web Service."; }
        }

        public static string WSLeituraDadosFim
        {
            get { return "Fim da leitura dos dados do Web Service."; }
        }


        #endregion

        #region Mensagens Arquivos Gerais

        public static string ArqInicioVerifiModica
        {
            get { return "Início da verificação dos arquivos modificados."; }
        }

        public static string ArqFimVerifiModica
        {
            get { return "Fim da verificação dos arquivos modificados."; }
        }

        public static string ArqBuscaIniciada
        {
            get { return "Busca de arquivos iniciada."; }
        }

        public static string ArqBuscaFinalizada
        {
            get { return "Busca de arquivos Finalizada."; }
        }

        public static string ArqNaoEncontrado
        {
            get { return "Nenhum arquivo encontrado!"; }
        }

        public static string ArqListagemFim
        {
            get { return "Listagem dos arquivos concluída!"; }
        }

        public static string ArqFalhaConversao
        {
            get { return "A conversão do arquivo falhou!"; }
        }

        public static string ArqSalvaInicioBD
        {
            get { return "Início do processo de salvar os arquivos no banco de dados."; }
        }

        public static string ArqSalvaFimBD
        {
            get { return "Fim do processo de salvar os arquivos no banco de dados."; }
        }

        public static string ArqConversaoInicio
        {
            get { return "Início da conversão dos arquivos."; }
        }

        public static string ArqConversaoFim
        {
            get { return "Fim da conversão dos arquivos."; }
        }

        public static string ArqVerifModificados
        {
            get { return "Verificando arquivos modificados."; }
        }
        
        public static string ArqVerifNovos
        {
            get { return "Verificando novos arquivos."; }
        }

        #endregion

        #region Mensagens Arquivos XML


        public static string XmltoDataSetInicio
        {
            get { return "Início da importação do XML para o DataSet."; }
        }
        public static string XmltoDataSetFim
        {
            get { return "Fim da importação do XML para o DataSet. ."; }
        }

        public static string XmlSalvarDadosBDInicio
        {
            get { return "Início do processo de salvar o XML no banco de dados."; }
        }

        public static string XmlSalvarDadosBDFim
        {
            get { return "Fim do processo de salvar o XML no banco de dados."; }
        }

        public static string XMLLeituraErro
        {
            get { return "Erro ao efetuar a leitura do arquivo XML."; }
        }

        public static string XMLImportarDataSetErro
        {
            get { return "Erro ao efetuar a importação do arquivo XML para o DataSet."; }
        }

        public static string XMLSemCabecalhoCorpo
        {
            get { return "O DataSet de leitura do XML só possui um DataTable. Provalvemente o XML está sem o cabeçalho ou o corpo da mensagem."; }
        }

        #endregion

        #region Mensagens Arquivos Configuração

        public static string ArqConfiAlteracaoSucesso
        {
            get { return "Arquivo de configurações alterado com sucesso!"; }
        }

        public static string ArqConfiObtendoInfo
        {
            get { return "Obtendo informações do Arquivo de configurações."; }
        }

        public static string ArqConfiObterInfoSucesso
        {
            get { return "Informações do Arquivo de configurações obtidas com sucesso!"; }
        }
        #endregion

        #region Mensagens Download

        public static string DownloadInicio
        {
            get { return "Download Iniciado."; }
        }
        public static string DownloadFim
        {
            get { return "Download Finalizado."; }
        }

        public static string DownloadFalha
        {
            get { return "Falha no Download dos itens."; }
        }

        #endregion

        #region Mensagens Diretórios

        public static string DiretorioInfoDestino
        {
            get { return "Informe um diretório de destino!"; }
        }

        #endregion

        #region Mensagens Erro

       

       

        public static string ErroExcluir
        {
            get { return "Erro ao Excluir o Registro."; }
        }

        public static string ErroConsultar
        {
            get { return "Erro ao consultar o(s) registro(s)."; }
        }

        public static string ErroAlterar
        {
            get { return "Erro ao Alterar o Registro."; }
        }

        public static string ErroInserir
        {
            get { return "Erro ao Inserir o Registro."; }
        }

        public static string ErroCarregar
        {
            get { return "Erro ao Carregar o item."; }
        }

        public static string ErroConstraint
        {
            get { return "Chave não encontrada. Erro de Constraint."; }
        }

        public static string ErroSalvarBD
        {
            get { return "Não foi possivel salvar as informações no banco de dados."; }
        }

        public static string ErroFatalSistema
        {
            get { return "Ocorreu um erro FATAL na aplicação."; }
        }

        public static string ErroSistema
        {
            get { return "Ocorreu um erro na aplicação."; }
        }


        #endregion

        #region Mensagens Aviso

        public static string ExcluidoSucesso
        {
            get { return "Registro excluido com sucesso."; }
        }

        public static string ConsultaSucesso
        {
            get { return "Consulta realizada com sucesso."; }
        }

        public static string AlteradoSucesso
        {
            get { return "Registro alterado com sucesso."; }
        }

        public static string InseridoSucesso
        {
            get { return "Registro inserido com sucesso."; }
        }

        public static string ConsultaVazia
        {
            get { return "A Consulta não retornou o registros."; }
        }

        public static string SenhaAlteradaSucesso
        {
            get { return "Senha Alterada com Sucesso!"; }
        }

        public static string UsuarioBloqueado
        {
            get { return "Usuário Bloqueado! Entre em contato com o administrador dos sistema."; }
        }

        public static string UsuarioDesbloqueado
        {
            get { return "Usuário Desbloqueado com sucesso!"; }
        }

        public static string UsuarioBloqueadoSucesso
        {
            get { return "Usuário Bloqueado com sucesso!"; }
        }

        public static string RegistroDuplicado
        {
            get { return "Já existe um registro cadastrado com esses dados."; }
        }

        public static string UsuarioInativo
        {
            get { return "Usuário desativado tentou logar."; }
        }

        public static string SemConexao
        {
            get { return "Não foi possível estabelecer conexão com o banco de dados."; }
        }




        #endregion

        #region Mensagens Validação

        public static string UsuarioInvalido
        {
            get { return "Dados do usuário inválidos!"; }
        }

        public static string CampoNomeObrigatorio
        {
            get { return "O preenchimento do nome é obrigatório."; }
        }

        #endregion

        #region Mensagens Cabeçalho

        public static string BoaNoite
        {
            get { return "Boa Noite!"; }
        }

        public static string BoaTarde
        {
            get { return "Boa Tarde!"; }
        }

        public static string BomDia
        {
            get { return "Bom dia!"; }
        }

        public static string BemVindo
        {
            get { return "Bem Vindo!"; }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Retorna a mensagem de acordo com o resultado e tipo da operação.
        /// </summary>
        /// <param name="pResultado">Enum do resultado da operação.</param>
        /// <param name="pTipoOperacao">Enum do tipo da operação.</param>
        /// <returns>Retorna a mensagem correspondente.</returns>
        public static string MensagemResultadoOperacao(Enumeradores.Resultados pResultado, Enumeradores.TipoOperacao pTipoOperacao)
        {
            switch (pResultado)
            {
                case Enumeradores.Resultados.Sucesso:
                    return Mensagem.MensagemSucessoOperacao(pTipoOperacao);
                case Enumeradores.Resultados.Duplicado:
                    return Mensagem.MensagemDuplicadoOperacao(pTipoOperacao);
                case Enumeradores.Resultados.FKConstraint:
                    return Mensagem.MensagemFKConstraintOperacao(pTipoOperacao);
                case Enumeradores.Resultados.Erro:
                    return Mensagem.MensagemErroOperacao(pTipoOperacao);
                case Enumeradores.Resultados.SemConexao:
                    return Mensagem.SemConexao;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Retorna a mensagem de acordo com o tipo da operação informando o sucesso da operação.
        /// </summary>
        /// <param name="pTipoOperacao">Enum do tipo da operação.</param>
        /// <returns>Retorna a mensagem correspondente.</returns>
        private static string MensagemSucessoOperacao(Enumeradores.TipoOperacao pTipoOperacao)
        {
            switch (pTipoOperacao)
            {
                case Enumeradores.TipoOperacao.Alteracao:
                    return Mensagem.AlteradoSucesso;
                case Enumeradores.TipoOperacao.Cadastro:
                    return Mensagem.InseridoSucesso;
                case Enumeradores.TipoOperacao.Exclusao:
                    return Mensagem.ExcluidoSucesso;
                case Enumeradores.TipoOperacao.AlteracaoSenha:
                    return Mensagem.SenhaAlteradaSucesso;
                case Enumeradores.TipoOperacao.Consulta:
                    return Mensagem.ConsultaSucesso;
                case Enumeradores.TipoOperacao.Nenhuma:
                default:
                    return string.Empty;
            }

        }

        /// <summary>
        /// Retorna a mensagem de acordo com o tipo da operação informando que um erro ocorreu.
        /// </summary>
        /// <param name="pTipoOperacao">Enum do tipo da operação.</param>
        /// <returns>Retorna a mensagem correspondente.</returns>
        private static string MensagemErroOperacao(Enumeradores.TipoOperacao pTipoOperacao)
        {
            switch (pTipoOperacao)
            {
                case Enumeradores.TipoOperacao.Alteracao:
                    return Mensagem.ErroAlterar;
                case Enumeradores.TipoOperacao.Cadastro:
                    return Mensagem.ErroInserir;
                case Enumeradores.TipoOperacao.Exclusao:
                    return Mensagem.ErroExcluir;
                case Enumeradores.TipoOperacao.AlteracaoSenha:
                    return Mensagem.ErroAlterar;
                case Enumeradores.TipoOperacao.Consulta:
                    return Mensagem.ErroConsultar;
                case Enumeradores.TipoOperacao.Nenhuma:
                default:
                    return string.Empty;
            }

        }

        /// <summary>
        /// Retorna a mensagem de acordo com o tipo da operação informando que o registro está duplicado.
        /// </summary>
        /// <param name="pTipoOperacao">Enum do tipo da operação.</param>
        /// <returns>Retorna a mensagem correspondente.</returns>
        private static string MensagemDuplicadoOperacao(Enumeradores.TipoOperacao pTipoOperacao)
        {
            switch (pTipoOperacao)
            {
                case Enumeradores.TipoOperacao.Alteracao:
                case Enumeradores.TipoOperacao.Cadastro:
                case Enumeradores.TipoOperacao.Exclusao:
                case Enumeradores.TipoOperacao.AlteracaoSenha:
                case Enumeradores.TipoOperacao.Nenhuma:
                default:
                    return Mensagem.RegistroDuplicado;
            }

        }

        /// <summary>
        /// Retorna a mensagem de acordo com o tipo da operação informando que um erro de FKConstraint.
        /// </summary>
        /// <param name="pTipoOperacao">Enum do tipo da operação.</param>
        /// <returns>Retorna a mensagem correspondente.</returns>
        private static string MensagemFKConstraintOperacao(Enumeradores.TipoOperacao pTipoOperacao)
        {
            switch (pTipoOperacao)
            {
                case Enumeradores.TipoOperacao.Alteracao:
                case Enumeradores.TipoOperacao.Cadastro:
                case Enumeradores.TipoOperacao.Exclusao:
                case Enumeradores.TipoOperacao.AlteracaoSenha:
                case Enumeradores.TipoOperacao.Nenhuma:
                default:
                    return Mensagem.ErroConstraint;
            }

        }


        #endregion
    }
}
