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
                new FormulaElementary(1, "Płeć", "=", "mężczyzna", true, FormulaElementaryType.Enter), //!=  kobieta
                
                new FormulaElementary(2, "Szczepienia", "=", "posiada", false, FormulaElementaryType.Enter),
                
                new FormulaElementary(3, "Lubi", "=", "góry", false, FormulaElementaryType.Enter),
                new FormulaElementary(4, "Lubi", "=", "morze", false, FormulaElementaryType.Enter),
                
                new FormulaElementary(5, "Forma wypoczynku", "=", "aktywna", false, FormulaElementaryType.Enter),
                
                new FormulaElementary(6, "Wiek", ">", "10", true, FormulaElementaryType.Enter),
                new FormulaElementary(7, "Wiek", ">", "20", true, FormulaElementaryType.Enter),
                new FormulaElementary(8, "Wiek", ">", "30", true, FormulaElementaryType.Enter),
                new FormulaElementary(9, "Wiek", ">", "40", true, FormulaElementaryType.Enter),
                new FormulaElementary(10, "Wiek", ">", "50", true, FormulaElementaryType.Enter),
                new FormulaElementary(11, "Wiek", ">", "60", true, FormulaElementaryType.Enter),

                new FormulaElementary(12, "Zainteresowania", "=", "kitesurfing", false, FormulaElementaryType.Enter),
                new FormulaElementary(13, "Zainteresowania", "=", "windsurfing", false, FormulaElementaryType.Enter),
                new FormulaElementary(14, "Zainteresowania", "=", "chodzenie po górach", false, FormulaElementaryType.Enter),
                new FormulaElementary(15, "Zainteresowania", "=", "plażing", false, FormulaElementaryType.Enter),
                new FormulaElementary(16, "Zainteresowania", "=", "zwiedzanie", false, FormulaElementaryType.Enter),

                new FormulaElementary(17, "Kierunek", "=", "Włochy", false, FormulaElementaryType.Exit),
                new FormulaElementary(18, "Kierunek", "=", "Hiszpania", false, FormulaElementaryType.Exit),
                new FormulaElementary(19, "Kierunek", "=", "Polska", false, FormulaElementaryType.Exit),
                new FormulaElementary(20, "Kierunek", "=", "Grecja", false, FormulaElementaryType.Exit),
                new FormulaElementary(21, "Kierunek", "=", "Austria", false, FormulaElementaryType.Exit),
                new FormulaElementary(22, "Kierunek", "=", "Czechy", false, FormulaElementaryType.Exit),
                new FormulaElementary(23, "Kierunek", "=", "Cypr", false, FormulaElementaryType.Exit),
                new FormulaElementary(24, "Kierunek", "=", "Słowacja", false, FormulaElementaryType.Exit),
                new FormulaElementary(25, "Kierunek", "=", "Indie", false, FormulaElementaryType.Exit),
                new FormulaElementary(26, "Kierunek", "=", "Malezja", false, FormulaElementaryType.Exit),
            };

            context.FormulaElementaries.AddOrUpdate(elementaries.ToArray());
            context.SaveChanges();

            var facts = new List<Fact>() {
                new Fact{ ExpressionString = "(12v13v15)>!(21v22v24)"},
                //new Fact{ ExpressionString = "(!1^15)>(17v18v20v23)"},
                new Fact{ ExpressionString = "(!2)>(!25^!26)"},
                //new Fact{ ExpressionString = "(3^!8)>14"},
                //new Fact{ ExpressionString = "(14^!9)>21"},
                //new Fact{ ExpressionString = "(14^9)>24"},
                //new Fact{ ExpressionString = "(5^!8)>12v13"},
                //new Fact{ ExpressionString = "(5^8)>16"},
                //new Fact{ ExpressionString = "(3)>(19v21v24)"},
                //new Fact{ ExpressionString = "4>(17v18v20v23v25v26)"},
            };

            context.Facts.AddOrUpdate(facts.ToArray());
            context.SaveChanges();
        }
    }
}
