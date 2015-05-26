using SWD.DataAccess.Entites;
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
    }
}
