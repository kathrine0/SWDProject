using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWD.Model.Test
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void FactTest1()
        {
            Fact fact = null;

            FormulaElementary lubieMorze = new FormulaElementary("1");
            FormulaElementary lubieGory = new FormulaElementary("2");
            FormulaElementary lubieChodzic = new FormulaElementary("3");
            FormulaElementary lubieCieplo = new FormulaElementary("4");
            FormulaElementary jadeDoRzymu = new FormulaElementary("5");

            Expression expression = new Expression(lubieGory, Operations.Or, lubieMorze);        
            
            Expression express = new Expression(lubieChodzic, Operations.And, lubieCieplo);

            expression.AddRight(Operations.And, express);
            expression.AddRight(Operations.Implication, jadeDoRzymu);
            Console.Out.Write(expression.ToString());
            fact = new Fact(expression);

            Assert.IsNotNull(fact);

        }

        [TestMethod]
        public void ParseTest1()
        {
            var exp = Expression.Parse("(5˄(1˅2)˄4)>3");
            Assert.Equals(exp.ToString(true), "(((5 ˄ (1 ˅ 2)) ˄ 4) > 3)");
        }

        [TestMethod]
        public void ParseTest2()
        {
            var exp = Expression.Parse("!(2˄2˅(2˄2))˄2");
            Console.Out.WriteLine(exp.ToString(true));
            
        }

        [TestMethod]
        public void ParseTest3()
        {
            var exp = Expression.Parse("!(1˄2)");
            Console.Out.WriteLine(exp.ToString(true));
        }

        [TestMethod]
        public void ParseTest4()
        {
            var exp = Expression.Parse("(1˄2)˄!(1˄2)");
            Console.Out.WriteLine(exp.ToString(true));
        }


        [TestMethod]
        public void Test1()
        {
            FormulaElementary a1 = new FormulaElementary(1, "Prędkość <=130 km/h", false, FormulaElementaryType.Enter);
            FormulaElementary a2 = new FormulaElementary(2, "kierowca otrzymuje mandat", false, FormulaElementaryType.Exit);
            FormulaElementary a3 = new FormulaElementary(3, "Prędkość >180 km/h", false, FormulaElementaryType.Enter);
            FormulaElementary a4 = new FormulaElementary(4, "Występuje niebezpieczeństwo", false, FormulaElementaryType.Other);
            FormulaElementary a5 = new FormulaElementary(5, "moc silnika jest poniżej 70 KM", true, FormulaElementaryType.Enter);

            Fact f1 = new Fact(1, Expression.Parse("1˅2"));
            Fact f2 = new Fact(2, Expression.Parse("3>4"));
            Fact f3 = new Fact(3, Expression.Parse("5>!3"));

            var exp = Expression.Parse("5>!3");
            Console.Out.WriteLine(exp.ToString(true));
        }
    }
}
