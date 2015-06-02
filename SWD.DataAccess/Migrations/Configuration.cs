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

        protected override void Seed(SWD.DataAccess.Context context)
        {
            var elementaries = new List<FormulaElementary>() { 
                new FormulaElementary(1, "Prędkość <=130 km/h", false, FormulaElementaryType.Enter),
                new FormulaElementary(2, "kierowca otrzymuje mandat", false, FormulaElementaryType.Exit),
                new FormulaElementary(3, "Prędkość >180 km/h", false, FormulaElementaryType.Enter),
                new FormulaElementary(4, "Występuje niebezpieczeństwo", false, FormulaElementaryType.Other),
                new FormulaElementary(5, "moc silnika jest poniżej 70 KM", true, FormulaElementaryType.Enter)
            };

            context.FormulaElementaries.AddOrUpdate(p => p.Name, elementaries.ToArray());

            var facts = new List<Fact>() {
                new Fact(expression: Expression.Parse("1˅2")),
                new Fact(expression: Expression.Parse("3>4")),
                new Fact(expression: Expression.Parse("5>!3"))
            };

            context.Facts.AddOrUpdate(facts.ToArray());
            
        }
    }
}
