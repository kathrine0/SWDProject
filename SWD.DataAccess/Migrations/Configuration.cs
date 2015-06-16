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
                /*
                new FormulaElementary(1, "1",false,FormulaElementaryType.Enter),
                new FormulaElementary(2, "2",false,FormulaElementaryType.Enter),
                new FormulaElementary(3, "3",false,FormulaElementaryType.Enter),
                new FormulaElementary(4, "4",false,FormulaElementaryType.Enter),
                new FormulaElementary(5, "5",false,FormulaElementaryType.Enter),
                new FormulaElementary(6, "6",false,FormulaElementaryType.Enter),
                new FormulaElementary(7, "7",false,FormulaElementaryType.Enter),
                new FormulaElementary(8, "8",false,FormulaElementaryType.Enter),
                new FormulaElementary(9, "9",false,FormulaElementaryType.Exit),
                new FormulaElementary(10, "10",false,FormulaElementaryType.Exit)
                */
                new FormulaElementary(1, "Płeć", "=", "mężczyzna", true, FormulaElementaryType.Enter), //!=  kobieta
                new FormulaElementary(2, "Lubi", "=", "dżunglę", true, FormulaElementaryType.Enter),
                new FormulaElementary(3, "Lubi", "=", "góry", true, FormulaElementaryType.Enter),
                new FormulaElementary(4, "Lubi", "=", "morze", true, FormulaElementaryType.Enter),
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

               
                new FormulaElementary(17, "Kierunek", "=", "Włochy", false, FormulaElementaryType.Other),
                new FormulaElementary(18, "Kierunek", "=", "Hiszpania", false, FormulaElementaryType.Other),
                new FormulaElementary(19, "Kierunek", "=", "Polska", false, FormulaElementaryType.Other),
                new FormulaElementary(20, "Kierunek", "=", "Grecja", false, FormulaElementaryType.Other),
                new FormulaElementary(21, "Kierunek", "=", "Austria", false, FormulaElementaryType.Other),
                new FormulaElementary(22, "Kierunek", "=", "Czechy", false, FormulaElementaryType.Other),
                new FormulaElementary(23, "Kierunek", "=", "Cypr", false, FormulaElementaryType.Other),
                new FormulaElementary(24, "Kierunek", "=", "Słowacja", false, FormulaElementaryType.Other),
                new FormulaElementary(25, "Kierunek", "=", "Indie", false, FormulaElementaryType.Other),
                new FormulaElementary(26, "Kierunek", "=", "Malezja", false, FormulaElementaryType.Other),

                new FormulaElementary(27, "Wycieczka", "=", "Śródziemnomorska", false, FormulaElementaryType.Exit),
                new FormulaElementary(28, "Wycieczka", "=", "Krajowa", false, FormulaElementaryType.Exit),
                new FormulaElementary(29, "Wycieczka", "=", "Kontynentalna", false, FormulaElementaryType.Exit),
                new FormulaElementary(30, "Wycieczka", "=", "Dalekomorska", false, FormulaElementaryType.Exit)
               
            };

            context.FormulaElementaries.AddOrUpdate(elementaries.ToArray());
            context.SaveChanges();

            var facts = new List<Fact>() {
              
                new Fact{ ExpressionString = "(12v13v15)>!(21v22v24)"},
                new Fact{ ExpressionString = "(!2)>(!25^!26)"},
                new Fact{ ExpressionString = "!1>!26"},
                
                new Fact{ ExpressionString = "27>(17^18^20^23)"},
                new Fact{ ExpressionString = "28>19"},
                new Fact{ ExpressionString = "29>(21^22^24"},
                new Fact{ ExpressionString = "30>(25^26)"},
                

               
            };

            context.Facts.AddOrUpdate(facts.ToArray());
            context.SaveChanges();
        }
    }
}
