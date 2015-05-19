using System;
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

            Expression expression = new Expression(lubieGory);        
            expression = expression.AddRight(Operations.Or, lubieMorze);

            Expression express = new Expression(lubieChodzic, Operations.And, lubieCieplo);

            expression = expression.AddRight(Operations.And, express);
            expression = expression.AddRight(Operations.Implication, jadeDoRzymu);
            Console.Out.Write(expression.ToString());
            fact = new Fact(expression);

            Assert.IsNotNull(fact);

        }
    }
}
