using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal class Program
    {
       
        static void inverse_element2(int a, int n)
        {
            if (Gcd(a, n) != 1)
            {
                Console.WriteLine("-");
            }
            else
            {
                Console.WriteLine(Math.Pow(a,phi(n)-1)%n);
            }
        }
        static int phi(int m)
        {
            int res = 1;
            for (int i = 2; i < m; i++)
            {
                if (Gcd(m, i) == 1)
                {
                    res++;
                }   
            } 
            Console.WriteLine("phi ("+ m +") = " + res);
            return res;
        }
        static int Gcd(int a, int b) // найбільший спільний дільник
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
        static void inverse_element(int a, int n) {
            int d, x, y;
            Gcdex(a, n, out d, out x, out y);
            if (d != 1)
            {
                Console.WriteLine("-");
            }
            else
            {
                Console.WriteLine((x % n + n) % n); // x mod n
            }
            }
        static void Gcdex(int a,int b, out int d, out int x, out int y)
        {
            int x0 = 1, x1 = 0, y0 = 0, y1 = 1;
            int oa = a , ob = b ;
            while (b != 0)
            {
                int q = a / b;
                int temp = a;
                a = b;
                b = temp % b;

                int newX = x1;
                x1 = x0 - q * x1;
                x0 = newX;

                int newY = y1;
                y1 = y0 - q * y1;
                y0 = newY;
            }

            d = a;
            x = x0;
            y = y0;
            Console.WriteLine(d + " = " + ob + " * ( " + y + " ) + " + oa + " * ( " + x + " )");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            Console.WriteLine("4");
            ConsoleKeyInfo cki = Console.ReadKey(intercept: true);
            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Реалізувати у вигляді функції gcdex(a,b) ітераційний розширений алгоритм Евкліда пошуку трійки (d,x,y), де ax+by = d. \nПротестити на прикладі  a= 612 і b=342.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    int d, x, y;
                    Gcdex(612,342,out d, out x, out y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case ConsoleKey.D2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("2. Реалізувати у вигляді функції inverse_element(a,n) пошук розв'язку рівняння ax=1 (mod n), \nтобто знаходження мультиплікативноо оберненого елемента a^(-1) по модулю n, використовуючи gcdex(a,b).  \nПротестити на прикладі  a = 5 і  n=18.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    inverse_element(31, 832);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case ConsoleKey.D3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("3. Реалізувати у вигляді функції phi(m) обчислення значення функції Ейлера для заданого m");
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int i = 1; i < 10; i++)
                    {
                        phi(i);
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ConsoleKey.D4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("4. Реалізувати у вигляді функції inverse_element_2(a,p) знаходження мультиплікативного оберненого елемента a^(-1) по модулю числа n, використовуючи інший спосіб (теорему Ейлера). \nПротестити на прикладі  a= 5 і  n=18.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    inverse_element2(5,18);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }
    }
}
