using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Domain.Entity
{
    public partial class Usuario
    {
        public string EnderecoLocalizacaoAtual
        {
            get
            {
               
                    return "";
               
            }
        }
        public double? LatitudeAtual
        {
            get
            {

               
                    return null;
               
            }
        }
        public double? LongitudeAtual
        {
            get
            {
                
                    return null;
                
            }
        }
        //public Localizacao LocalizacaoAtual
        //{
        //    get
        //    {
        //        Localizacao localizacao = this.Localizacao.FirstOrDefault();
        //        return localizacao;
        //    }
        //}
    }
}
