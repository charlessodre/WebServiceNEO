using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Infrastructure.Common
{
    public static class HttpUtil
    {
        public static string HttpPost(string url, string[] paramName, string[] paramVal, int timeout = 100000)
        {
            return HttpPostStringReader(url, paramName, paramVal,timeout).ToString();
        }

        public static StringReader HttpPostStringReader(string url, string[] paramName, string[] paramVal, int timeout = 100000)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.Timeout = timeout;
            req.ContentType = "application/x-www-form-urlencoded";


            StringBuilder paramz = new StringBuilder();
            for (int i = 0; i < paramName.Length; i++)
            {
                paramz.Append(paramName[i]);
                paramz.Append("=");
                paramz.Append(HttpUtility.UrlEncode(paramVal[i]));
                paramz.Append("&");
            }

            byte[] formData = UTF8Encoding.UTF8.GetBytes(paramz.ToString());
            req.ContentLength = formData.Length;

            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            StringReader result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(CultureInfo.GetCultureInfo(Constantes.CulturaInfo).TextInfo.ANSICodePage));
                result = new StringReader(reader.ReadToEnd());
            }

            return result;
        }

    }
}
