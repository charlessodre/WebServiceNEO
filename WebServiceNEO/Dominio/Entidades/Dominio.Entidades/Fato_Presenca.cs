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
    
    public partial class Fato_Presenca
    {
        public string id_aula { get; set; }
        public string id_aluno_preceptor { get; set; }
        public string id_turma { get; set; }
        public string idmatricula_aluno { get; set; }
        public string realizada_presenca { get; set; }
        public Nullable<System.DateTime> datahora_presenca { get; set; }
        public Nullable<int> qtd_atividades_disponiveis { get; set; }
        public Nullable<int> qtd_atividades_realizadas { get; set; }
        public Nullable<System.DateTime> data_insercao { get; set; }
        public Nullable<System.DateTime> data_atualizacao { get; set; }
    
        public virtual Dim_Aluno_Preceptor Dim_Aluno_Preceptor { get; set; }
        public virtual Dim_Aula Dim_Aula { get; set; }
    }
}
