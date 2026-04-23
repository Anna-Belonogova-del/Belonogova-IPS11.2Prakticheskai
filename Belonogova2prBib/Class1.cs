using System;
namespace Belonogova2prBib
{
    public static class CalculatorEngine
    {
        public static double Execute(double n, char op, double k)
        {
            switch (op)
            {
                case '+': return n + k;
                case '-': return n - k;
                case '*': return n * k;
                case '/':
                    if (k == 0) throw new DivideByZeroException("Деление на ноль невозможно");
                    return n / k;
                case '^': return Math.Pow(n, k);
                default: throw new ArgumentException($"Недопустимый оператор: {op}");
            }
        }

        public static int GetPriority(char op)
        {
            return op switch
            {
                '^' => 3,
                '*' or '/' => 2,
                '+' or '-' => 1,
                _ => 0
            };
        }
    }
}