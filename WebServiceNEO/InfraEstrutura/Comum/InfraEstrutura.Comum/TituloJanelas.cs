using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Common
{
    public static class TituloJanelas
    {
        #region Atenção

        public static string Atencao
        {
            get { return "Atenção"; }
        }

        public static string SairSistema
        {
            get { return "Sair do Sistema"; }
        }

        public static string TrocarUsuario
        {
            get { return "Trocar Usuário"; }
        }

        public static string ExcluirImagem
        {
            get { return "Excluir Imagem"; }
        }

        public static string ExcluirRegistro
        {
            get { return "Excluir Registro"; }
        }

        public static string CampoObrigatorio
        {
            get { return "Campo Obrigatório"; }
        }


        public static string DataInvalida
        {
            get { return "Data inválida"; }
        }

        #endregion

        #region Erro

        public static string ErroGeral
        {
            get { return "Erro"; }
        }

        public static string FalhaLogin
        {
            get { return "Falha ao efetuar o login"; }
        }
        #endregion

        #region Sucesso

        public static string SucessoGeral
        {
            get { return "Sucesso"; }
        }
        #endregion

        #region Cadastro

        public static string CadastroGeral
        {
            get { return "Cadastro"; }
        }
        public static string CadastroAluno
        {
            get { return "Cadastrar Aluno"; }
        }
        public static string CadastroPais
        {
            get { return "Cadastrar País"; }
        }
        public static string CadastroEstado
        {
            get { return "Cadastrar Estado"; }
        }
        public static string CadastroCidade
        {
            get { return "Cadastrar Cidade"; }
        }
        public static string CadastroProfissao
        {
            get { return "Cadastrar Profissão"; }
        }
        public static string CadastroEmpresa
        {
            get { return "Cadastrar Empresa"; }
        }

        public static string CadastroGrupoUsuario
        {
            get { return "Cadastrar Grupo Usuário"; }
        }

        public static string CadastroUsuario
        {
            get { return "Cadastrar Usuário"; }
        }

        public static string ReceberPagamento
        {
            get { return "Receber Pagamento"; }
        }

        public static string LancarContaReceber
        {
            get { return "Lançar conta a receber"; }
        }

        public static string ConsultarContasReceber
        {
            get { return "Consultar Contas a receber"; }
        }

        public static string RecebimentoConta
        {
            get { return "Recebimento de Contas"; }
        }
        #endregion

        #region Consulta


        public static string ConsultarAluno
        {
            get { return "Consultar Aluno"; }
        }


        public static string ConsultarUsuario
        {
            get { return "Consultar Usuário"; }
        }


        public static string ConsultarGrupoUsuario
        {
            get { return "Consultar Grupo Usuário"; }
        }

        public static string ConsultarEmpresa
        {
            get { return "Consultar Empresa"; }
        }

        public static string ConsultarProfissao
        {
            get { return "Consultar Profissão"; }
        }

        public static string ConsultarPais
        {
            get { return "Consultar País"; }
        }
        public static string ConsultarEstado
        {
            get { return "Consultar Estado"; }
        }
        public static string ConsultarCidade
        {
            get { return "Consultar Cidade"; }
        }

        #endregion

        #region Alteração

        public static string AlteracaoGeral
        {
            get { return "Alteração"; }
        }

        public static string AlterarAluno
        {
            get { return "Alterar Aluno"; }
        }

        public static string AlterarEmpresa
        {
            get { return "Alterar Empresa"; }
        }

        public static string AlterarGrupoUsuario
        {
            get { return "Alterar Grupo de Usuário"; }
        }

        public static string AlterarUsuario
        {
            get { return "Alterar Usuário"; }
        }

        #endregion

    }
}
