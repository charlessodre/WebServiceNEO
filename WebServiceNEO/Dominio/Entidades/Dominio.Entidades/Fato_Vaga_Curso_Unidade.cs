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
    
    public partial class Fato_Vaga_Curso_Unidade
    {
        public int id_vaga_curso_unidade { get; set; }
        public string id_hospital { get; set; }
        public string id_curso { get; set; }
        public string tipo_aluno { get; set; }
        public System.DateTime dt_vigencia_inicio { get; set; }
        public Nullable<System.DateTime> dt_vigencia_fim { get; set; }
        public Nullable<int> qtd_vagas { get; set; }
        public System.DateTime data_insercao { get; set; }
        public Nullable<System.DateTime> data_atualizacao { get; set; }
    }
}
