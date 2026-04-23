    using BibliotekaBel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    namespace TestikBeloi
    {
        [TestClass]
        public class CalculatorEngineTests
        {
            [TestMethod]
            public void Sum()
            {
                double a = 5, b = 3, expected = 8;
                double result = CalculatorEngine.Execute(a, '+', b);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void Minys()
            {
                double a = 10, b = 4, expected = 6;
                double result = CalculatorEngine.Execute(a, '-', b);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void ymnog()
            {
                double a = 7, b = 6, expected = 42;
                double result = CalculatorEngine.Execute(a, '*', b);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void delen()
            {
                double a = 15, b = 3, expected = 5;
                double result = CalculatorEngine.Execute(a, '/', b);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void delenie_na_0()
            {
                try
                {
                    double a = 10, b = 0;
                    CalculatorEngine.Execute(a, '/', b);
                    Assert.Fail("Ожидалось исключение");
                }
                catch (DivideByZeroException)
                {

                    Assert.IsTrue(true);
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Деление на 0,  было выброшено: {ex.GetType().Name}");
                }
            }
            [TestMethod]
            public void stepen()
            {
                double a = 2, b = 4, expected = 16;
                double result = CalculatorEngine.Execute(a, '^', b);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void stepen_mal_chisla()
            {
                double a = 0.5, b = 6, expected = 0.015625;
                double result = CalculatorEngine.Execute(a, '^', b);
                Assert.AreEqual(expected, result, 0.0001);
            }
        [TestMethod]
        public void skobki()
        {
            double a = CalculatorEngine.Execute(2, '+', 3);
            double b = CalculatorEngine.Execute(a, '*', 4);
            double expected = 20;
            Assert.AreEqual(expected, b);
        }
        [TestMethod]
            public void GetPriority_ReturnsCorrectPriority()
            {
                Assert.AreEqual(3, CalculatorEngine.GetPriority('^'));
                Assert.AreEqual(2, CalculatorEngine.GetPriority('*'));
                Assert.AreEqual(2, CalculatorEngine.GetPriority('/'));
                Assert.AreEqual(1, CalculatorEngine.GetPriority('+'));
                Assert.AreEqual(1, CalculatorEngine.GetPriority('-'));
                Assert.AreEqual(0, CalculatorEngine.GetPriority('x'));
            }

            [TestMethod]
            public void Execute_NegativeNumbers_ReturnsCorrectResult()
            {
                double a = -5, b = 3, expected = -2;
                double result = CalculatorEngine.Execute(a, '+', b);
                Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void Execute_DecimalNumbers_ReturnsCorrectResult()
            {
                double a = 2.5, b = 1.5, expected = 4.0;
                double result = CalculatorEngine.Execute(a, '+', b);
                Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void Execute_ComplexExpression_ReturnsCorrectResult()
            {
                double step1 = CalculatorEngine.Execute(2, '+', 3);
                double result = CalculatorEngine.Execute(step1, '*', 4);
                double expected = 20;
                Assert.AreEqual(expected, result);
            }
        }
    }
