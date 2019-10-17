using System;
using System.Runtime.InteropServices;

namespace Infrastructure.Common
{
    public static class Enumeradores
    {
        #region Tipo de Operação

        /// <summary>
        /// Tipo de Operaçõão 
        /// </summary>
        public enum TipoOperacao : short
        {
            Alteracao = 1,
            Cadastro = 2,
            Exclusao = 3,
            AlteracaoSenha = 4,
            Consulta = 5,
            Nenhuma = 6
        }


        /// <summary>
        /// Converter a string para o Enum do tipo correspondente.
        /// </summary>
        /// <param name="pString">String do Tipo</param>
        public static TipoOperacao ConverterStringTipoOperacao(string pString)
        {
            //
            switch (pString)
            {
                case "Alteracao":
                    return TipoOperacao.Alteracao;
                case "Cadastro":
                    return TipoOperacao.Cadastro;
                case "Exclusao":
                    return TipoOperacao.Exclusao;
                case "AlteracaoSenha":
                    return TipoOperacao.AlteracaoSenha;
                case "Consulta":
                    return TipoOperacao.Consulta;
                default:
                    return TipoOperacao.Nenhuma;
            }
        }


        #endregion

        #region Status

        /// <summary>
        /// Status
        /// </summary>
        public enum Status : short
        {
            Ativo = 1,
            Inativo = 2,
            Todos = 3

        }

        /// <summary>
        /// Converter um object para o Enum do Status correspondente.
        /// </summary>
        /// <param name="pObject">Object do Status</param>
        public static Status ConverterObject2Status(object pObject)
        {
            try
            {
                short status = Convert.ToInt16(pObject);
                return Enumeradores.ConverterShort2Status(status);
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("Erro ao Converter o Status. Erro: " + ex.Message);
            }


        }

        /// <summary>
        /// Converter o numero(shot) para o Enum do Status correspondente.
        /// </summary>
        /// <param name="pShort">pShort do Status</param>
        public static Status ConverterShort2Status(short pShort)
        {
            switch (pShort)
            {
                case 1:
                    return Status.Ativo;
                case 2:
                    return Status.Inativo;
                default:
                    return Status.Todos;
            }

        }

        /// <summary>
        /// Converter o Enum do Status para short.
        /// </summary>
        /// <param name="pString">String do Status</param>
        public static short ConverterStatus2Short(Status pStatus)
        {
            switch (pStatus)
            {
                case Status.Ativo:
                    return 1;
                case Status.Inativo:
                    return 2;
                default:
                    return 3;
            }

        }

        /// <summary>
        /// Converter a string para o Enum do Status correspondente.
        /// </summary>
        /// <param name="pString">String do Status</param>
        public static Status ConverterString2Status(string pString)
        {
            switch (pString)
            {
                case "Ativo":
                    return Status.Ativo;
                case "Inativo":
                    return Status.Inativo;
                default:
                    return Status.Todos;
            }
        }

        #endregion

        #region Resultados

        /// <summary>
        /// Enum que define o resultado da operação.
        /// </summary>
        public enum Resultados
        {
            Sucesso,
            Duplicado,
            FKConstraint,
            Erro,
            SemConexao
        }

        #endregion

        // Summary:
        //     Specifies the day of the week.
        [Serializable]
        [ComVisible(true)]
        public enum DiaDaSemana
        {
            AllDays = -1,

            // Summary:
            //     Indicates Sunday.
            Sunday = 0,
            //
            // Summary:
            //     Indicates Monday.
            Monday = 1,
            //
            // Summary:
            //     Indicates Tuesday.
            Tuesday = 2,
            //
            // Summary:
            //     Indicates Wednesday.
            Wednesday = 3,
            //
            // Summary:
            //     Indicates Thursday.
            Thursday = 4,
            //
            // Summary:
            //     Indicates Friday.
            Friday = 5,
            //
            // Summary:
            //     Indicates Saturday.
            Saturday = 6,
        }



        /// <summary>
        /// Converter um object para o Enum do Status correspondente.
        /// </summary>
        /// <param name="pObject">Object do Status</param>
        public static DiaDaSemana ConverterObject2DiaDaSemana(object pObject)
        {
            try
            {
                short status = Convert.ToInt16(pObject);
                return Enumeradores.ConverterShort2DiaDaSemana(status);
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("Erro ao Converter o DiaDaSemana. Erro: " + ex.Message);
            }


        }

        public static DiaDaSemana ConverterShort2DiaDaSemana(short pShort)
        {
            switch (pShort)
            {
                case 1:
                    return DiaDaSemana.Sunday;
                case 2:
                    return DiaDaSemana.Monday;
                case 3:
                    return DiaDaSemana.Tuesday;
                case 4:
                    return DiaDaSemana.Wednesday;
                case 5:
                    return DiaDaSemana.Thursday;
                case 6:
                    return DiaDaSemana.Friday;
                case 7:
                    return DiaDaSemana.Saturday;
                default:
                    return DiaDaSemana.AllDays;
            }
        }

    }
}
