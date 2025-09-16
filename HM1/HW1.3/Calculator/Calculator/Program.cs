using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double num1 = double.Parse(args[0]);
            double num2 = double.Parse(args[1]);
            string operation = args[2];
            double result = 0;
            switch (operation) {
                case "+": result = num1 + num2; break;
                case "-": result = num1 - num2; break;
                case "*": result = num1 * num2; break;
                case "/": result = num1 / num2; break;
            }
            Console.WriteLine(result);
        }
    }
}
