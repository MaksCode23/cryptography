using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7_1
{
    internal class Program
    {
        static void Miller_Rabin(int n, int k)
        {

            if (n <= 3 || n % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вхідні дані: n>3, непарне натуральне число. ");
            }
            else
            {   // n − 1= 2^s * d , де s — натуральне число, d — непарне натуральне число
                BigInteger d = n - 1;
                BigInteger s = 0;   
                while (d % 2 == 0)
                {
                    d /= 2;
                    s++;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("n = " + n + "\n"+"d = " + d + "\n" + "s = " + s + "\n"); ;
                Random rand = new Random();
                bool chk = false;
                for (int i = 0; i < k; i++)
                {
                    Console.WriteLine("________________________");
                    Console.WriteLine();
                    BigInteger a = rand.Next(2, n - 2);
                    Console.WriteLine("a = " + a);
                    BigInteger x = BigInteger.ModPow(a, d, n);
                    Console.WriteLine("x = " + x);
                    if (x == 1 || x == n - 1)
                    {
                        chk = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ймовірно просте");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        continue;
                    }
                    if (s == 1 && x != n - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Складене");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    for (int r = 1; r < s; r++)
                    {
                        x = BigInteger.ModPow(x, 2,n);
                        Console.WriteLine("x = " + x);
                        if (x == n - 1)
                        {
                            chk = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Ймовірно просте");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else
                        {

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Складене");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        }
                    
                }
                if (chk)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nЙмовірно просте з ймовірністю " + (1 - Math.Pow(0.25, k))*100 + "%");// помилка 1/4 ^ k
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Miller_Rabin(13, 3);


        }
    }     
    
}
