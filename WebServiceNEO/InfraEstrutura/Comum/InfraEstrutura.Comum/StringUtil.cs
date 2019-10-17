using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class StringUtil
    {
        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        public static string CamelCaseTransform(string text) 
        {
            StringBuilder textoTransformed = new StringBuilder();
            char[] textArray = text.ToCharArray();
            for (int i = 0; i < textArray.Length; i++)
            {
                if (i <= 0)
                {
                    textoTransformed.Append(textArray[0].ToString().ToUpper());
                }
                else 
                {
                    if (textArray[i - 1] == ' ')
                    {
                        textoTransformed.Append(textArray[i].ToString().ToUpper());
                    }
                    else 
                    {
                        textoTransformed.Append(textArray[i].ToString().ToLower());
                    }
                }
            }
            return textoTransformed.ToString();
        }
        public static string TransformImageName(string texto,string extensao) 
        {
            string imageName = (RemoveAccents(texto)).Replace(" ", "-").ToLower() + "." + extensao;
            return imageName;
        }
        public static bool IsEquivalent(string origem, string destino) 
        {
            string textoTransformedOrigem = "";
            string textoTransformedDestino = "";
            textoTransformedOrigem = RemoveAccents(origem);
            textoTransformedOrigem = CamelCaseTransform(textoTransformedOrigem);
            textoTransformedDestino = RemoveAccents(destino);
            textoTransformedDestino = CamelCaseTransform(textoTransformedDestino);

            if (textoTransformedOrigem.Equals(textoTransformedDestino))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
