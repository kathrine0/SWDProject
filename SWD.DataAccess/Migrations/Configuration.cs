namespace SWD.DataAccess.Migrations
{
    using SWD.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SWD.DataAccess.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context context)
        {
            var elementaries = new List<FormulaElementary>() { 
                new FormulaElementary(1, "Prędkość", "<=", "130", false, FormulaElementaryType.Enter),
                new FormulaElementary(2, "kierowca otrzymuje mandat", false, FormulaElementaryType.Exit),
                new FormulaElementary(3, "Prędkość >180 km/h", ">", "180", false, FormulaElementaryType.Enter),
                new FormulaElementary(4, "Występuje niebezpieczeństwo", false, FormulaElementaryType.Exit),
                new FormulaElementary(5, "moc silnika", "<", "70 KM", true, FormulaElementaryType.Exit)
            };

            context.FormulaElementaries.AddOrUpdate(elementaries.ToArray());
            context.SaveChanges();

            var facts = new List<Fact>() {
                new Fact{ ExpressionString = "1˅2"},
                new Fact{ ExpressionString = "3>4"},
                new Fact{ ExpressionString = "5>!3"}
            };

            context.Facts.AddOrUpdate(facts.ToArray());
            context.SaveChanges();
        }
    }
}
