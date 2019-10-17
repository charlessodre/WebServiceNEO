using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class TratarDadosUtil
    {

        #region Tratar dado para  ser enviado ao banco.

        public static int TratarBooleanoBD(bool pValor)
        {
            if (pValor == false)
                return 0;
            else
                return 1;
        }

        public static object TratarNumeroBD(double pValor, bool pPermiteNull)
        {
            if (pPermiteNull == true)
                return TratarDadosUtil.TratarNumeroBD(pValor);
            else
                return pValor;

        }

        public static object TratarNumeroBD(double pValor)
        {
            if (pValor > 0)
                return pValor;
            else
                return DBNull.Value;
        }

        public static object TratarDecimalBD(decimal pValor)
        {
            if (pValor > 0)
                return pValor;
            else
                return DBNull.Value;
        }

        public static object TratarStringBD(string pValor)
        {
            if (!string.IsNullOrEmpty(pValor))
                return pValor;
            else
                return DBNull.Value;
        }

        public static object TratarDataBD(DateTime pValor)
        {
            try
            {
                if (pValor != DateTime.MinValue & pValor != null)
                    return pValor;
                else
                    return DBNull.Value;

            }
            catch (Exception ex)
            {
                throw new Exception("Data inválida! " + ex.Message);
            }
        }

        #endregion

        #region Tratar dados Nulos e vazios

        public static bool ValorEstaNulo(object pValor)
        {
            return pValor == null;
        }

        public static bool ValorNaoNuloOuVazio(object pValor)
        {
            bool retorno = true;

            if (pValor.ToString().Trim().Equals(string.Empty) || TratarDadosUtil.ValorEstaNulo(pValor))
                retorno = false;

            return retorno;
        }



        #endregion

        #region Tratar

       

        #endregion
    }

}
