using CarteiraSafra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Acao> Acoes { get; set; }
        public DbSet<Operacao> Operacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db");

    }
}
