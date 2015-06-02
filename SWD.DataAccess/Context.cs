using SWD.DataAccess.Entites;
using SWD.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.DataAccess
{
    class Context : DbContext
    {
        public Context()
            : base("DefaultConnection")
        {

        }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<FormulaElementary> FormulaElementaries { get; set; }
        public DbSet<Fact> Facts { get; set; }
    }
}
