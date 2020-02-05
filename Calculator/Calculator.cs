using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Calc
    {
        public StringBuilder Expression { get; } = new StringBuilder();

        public bool AddElement(string element)
        {
            bool is_result = false;
            if(element == "=")
            {
                element+=Evaluate();
                is_result = true;
            }
            Expression.Append(element);
            return is_result;
        }
        public double Evaluate(StringBuilder expression = null)
        {
            if (expression == null)
            {
                expression = Expression;
            }
            double result = 0;
            Regex numreg = new Regex(@"[\d.]+");
            Match match = numreg.Match(expression.ToString());
            double newval;
            result += double.Parse(match.Value);

            while (match.NextMatch().Success)
            {
                newval = double.Parse(match.NextMatch().Value);
                result = Operation(result, newval, expression[match.Index + match.Length]);
                match = match.NextMatch();
            }
            return result;
        }

        public double Operation(double left, double right, char operation)
        {
            double result;
            switch (operation)
            {
                case ('+'):
                    result = left + right; break;
                case ('-'):
                    result = left - right; break;
                case ('/'):
                    result = left / right; break;
                case (':'):
                    result = left / right; break;
                case ('*'):
                    result = left * right; break;
                case ('x'):
                    result = left * right; break;
                default: throw new Exception("Explicit operation char");
            }
            return result;
        }

        public StringBuilder Clear()
        {
            return Expression.Clear();
        }
    }
}
