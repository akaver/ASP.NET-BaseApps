using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    class Program
    {

        delegate string ConvertMethod(int n);

        static void Main(string[] args)
        {

            for (var i = 0; i <= 5; i++)
            {
                Console.WriteLine($"Plain: Number is {i} and it is {ConvertToNumber(i)}!");
                //Console.WriteLine("Number is "+i+" and it is "+ConvertToNumber(i)+"!");
                //Console.WriteLine("Number is {0} and it is {1}!",i, ConvertToNumber(i));
            }

            ConvertMethod convMeth = ConvertToNumber;
            for (var i = 0; i <= 5; i++)
            {
                Console.WriteLine($"Delegate: Number is {i} and it is {convMeth(i)}!");

            }
            // Func<Parameeter1, Parameeter2, Parameeter...., tagastustüüp>

            Func<int, string> convMethFunc = ConvertToNumber;
            for (var i = 0; i <= 5; i++)
            {
                Console.WriteLine($"Func: Number is {i} and it is {convMethFunc(i)}!");

            }


            Func<int, string> convMethFuncAnon = delegate (int number)
            {
                switch (number)
                {
                    case 0:
                        return "nothing";
                    case 1:
                        return "one";
                    case 2:
                        return "two";
                    default:
                        return "many";
                }
            };

            for (var i = 0; i <= 5; i++)
            {
                Console.WriteLine($"Func anonymous: Number is {i} and it is {convMethFuncAnon(i)}!");
            }

            Func<int, int, string> convMethFuncLambda = ((number1, number2) => (number1 + number2).ToString());
            Func<string> convMethFuncLambda0parameetrit = (() => "0 parameetrit");
            Func<int, string> convMethFuncLambda1parameeter = (number1 => (number1).ToString());
            for (var i = 0; i <= 5; i++)
            {
                Console.WriteLine($"Func anonymous: Number is {i} and 2x is {convMethFuncLambda(i,i)}!");
            }


        }

        private static string ConvertToNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return "nothing";
                case 1:
                    return "one";
                case 2:
                    return "two";
                default:
                    return "many";
            }
        }
    }
}
