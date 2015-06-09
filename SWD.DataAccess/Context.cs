using SWD.DataAccess.Entites;
using SWD.Model;
using System;
using System.Data.Entity;

namespace SWD.DataAccess
{
    public class Context : DbContext
    {
        public Context()
            : base("DefaultConnection")
        {

        }
        public DbSet<FormulaElementary> FormulaElementaries { get; set; }
        public DbSet<Fact> Facts { get; set; }
    }
}
