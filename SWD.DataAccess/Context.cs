using SWD.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
