//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fato_Plantao
    {
        public string id_plantao { get; set; }
        public string id_aluno_preceptor { get; set; }
        public string idmatricula_aluno { get; set; }
        public string id_turma { get; set; }
        public Nullable<System.DateTime> escalainicio_plantao { get; set; }
        public Nullable<System.DateTime> escalafim_plantao { get; set; }
        public Nullable<System.DateTime> marcacaoinicio_plantao { get; set; }
        public Nullable<System.DateTime> marcacaofim_plantao { get; set; }
        public Nullable<decimal> duracao_plantao { get; set; }
        public Nullable<decimal> efetivo_plantao { get; set; }
        public string abono_ds { get; set; }
        public string abono_falta { get; set; }
        public string abono_bolsa { get; set; }
        public Nullable<System.DateTime> data_insercao { get; set; }
        public Nullable<System.DateTime> data_atualizacao { get; set; }
    
        public virtual Dim_Aluno_Preceptor Dim_Aluno_Preceptor { get; set; }
    }
}
