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
    
    public partial class Dim_Especialidade
    {
        public Dim_Especialidade()
        {
            this.Dim_Aluno_Preceptor = new HashSet<Dim_Aluno_Preceptor>();
            this.Dim_Professor = new HashSet<Dim_Professor>();
        }
    
        public string id_especialidade { get; set; }
        public string nome_especialidade { get; set; }
        public Nullable<System.DateTime> data_insercao { get; set; }
        public Nullable<System.DateTime> data_atualizacao { get; set; }
    
        public virtual ICollection<Dim_Aluno_Preceptor> Dim_Aluno_Preceptor { get; set; }
        public virtual ICollection<Dim_Professor> Dim_Professor { get; set; }
    }
}
