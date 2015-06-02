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
                new FormulaElementary(5, "moc silnika jest poniżej 70 KM", true, FormulaElementaryType.Enter),

                new FormulaElementary(6, "Wiek > 20", true, FormulaElementaryType.Other),
                new FormulaElementary(7, "Wiek > 30", true, FormulaElementaryType.Other),
                new FormulaElementary(8, "Wiek > 40", true, FormulaElementaryType.Other),
                new FormulaElementary(9, "Wiek > 50", true, FormulaElementaryType.Other),
                new FormulaElementary(10, "Wiek > 60", true, FormulaElementaryType.Other),
                new FormulaElementary(11, "Wiek > 70", true, FormulaElementaryType.Other),

                new FormulaElementary(20, "Obywatelstwo = Polskie", true, FormulaElementaryType.Enter),
                new FormulaElementary(21, "Obywatelstwo = Niemieckie", true, FormulaElementaryType.Enter),
                new FormulaElementary(21, "Obywatelstwo = Brytyjskie", true, FormulaElementaryType.Enter),
                new FormulaElementary(21, "Obywatelstwo = Amerykańskie", true, FormulaElementaryType.Enter),


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
