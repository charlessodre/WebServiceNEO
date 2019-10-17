using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Domain.Entity;

    public partial class WSNEOEntities : DbContext
    {
        public WSNEOEntities()
            : base("name=STAGING_NEO_FUNRIOEntities")
        {
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Dim_Curso> Curso { get; set; }
        public DbSet<Dim_Aluno_Preceptor> Aluno { get; set; }
        public DbSet<Dim_Especialidade> Especialidade { get; set; }
        public DbSet<Dim_Turma> Turma { get; set; }
        public DbSet<Dim_Professor> Professor { get; set; }
        public DbSet<Dim_Modulo> Modulo { get; set; }
        public DbSet<Dim_Hospital> Hospital { get; set; }
        public DbSet<Dim_Aula> Aula { get; set; }
        public DbSet<Fato_Presenca> FatPresenca { get; set; }
        public DbSet<Fato_Plantao> FatPlantao { get; set; }
        public DbSet<Fato_Avaliacao> FatAvaliacao { get; set; }
        public DbSet<Fato_Permanencia_Plataforma> FatPermanenciaPlataforma { get; set; }
      

    }
}
