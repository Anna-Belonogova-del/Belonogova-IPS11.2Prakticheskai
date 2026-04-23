using Belonogova2prBib;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Belonogova2WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbExpression.Clear();
            tbResult.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textButton = ((Button)sender).Content.ToString();
            tbExpression.Text += textButton;
            tbResult.Clear();
        }


        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            tbExpression.Clear();
            tbResult.Clear();
        }

        private void Button_Equals(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbExpression.Text))
                {
                    tbResult.Text = "Введите выражение";
                    return;
                }

                double result = CalculateExpression(tbExpression.Text);
                tbResult.Text = result.ToString();
            }
            catch (DivideByZeroException)
            {
                tbResult.Text = "Ошибка: деление на ноль";
            }
            catch (Exception ex)
            {
                tbResult.Text = "Ошибка: " + ex.Message;
            }
        }


        private double CalculateExpression(string expression)
        {

            expression = expression.Replace(',', '.');

            List<string> tokens = TokenizeExpression(expression);

            List<string> postfix = ConvertToPostfix(tokens);

            return EvaluatePostfix(postfix);
        }

        private List<string> TokenizeExpression(string expression)
        {
            List<string> tokens = new List<string>();
            string number = "";

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (char.IsDigit(c) || c == '.')
                {
                    number += c;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^')
                {
                    if (number != "")
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    tokens.Add(c.ToString());
                }
                else
                {
                    throw new ArgumentException($"Недопустимый символ: {c}");
                }
            }

            if (number != "")
            {
                tokens.Add(number);
            }

            return tokens;
        }

        private List<string> ConvertToPostfix(List<string> tokens)
        {
            List<string> output = new List<string>();
            Stack<string> operators = new Stack<string>();

            foreach (string token in tokens)
            {

                if (double.TryParse(token, out _))
                {
                    output.Add(token);
                }

                else if (IsOperator(token[0]))
                {
                    while (operators.Count > 0 &&
                    IsOperator(operators.Peek()[0]) &&
                    GetPriority(operators.Peek()[0]) >= GetPriority(token[0]))
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
            }

            while (operators.Count > 0)
            {
                output.Add(operators.Pop());
            }

            return output;
        }
        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
        }
        private int GetPriority(char op)
        {
            return CalculatorEngine.GetPriority(op);
        }

        private double EvaluatePostfix(List<string> postfix)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in postfix)
            {

                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }

                else if (token.Length == 1 && IsOperator(token[0]))
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Некорректное выражение");
                    }

                    double b = stack.Pop();
                    double a = stack.Pop();
                    char op = token[0];

                    double result = CalculatorEngine.Execute(a, op, b);
                    stack.Push(result);
                }
            }

            if (stack.Count != 1)
            {
                throw new ArgumentException("Некорректное выражение");
            }

            return stack.Pop();
        }
    }
}
