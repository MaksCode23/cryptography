using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lab_12
{
    internal class Program
    {
        static int modInverse(int a, int m)
        {
            int m0 = m;
            int y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                int q = a / m;
                int t = m;

                m = a % m;
                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }
        static void FindY(int x, int p, int xi)
        {   
            for (int i = 0; i <= p; i++) { 
            int y = (int)Math.Pow(i, 2);
                if(Mod(y, p) == Mod(x, p))
                {                   
                    Console.WriteLine("E ("+ xi +","+ i + ")" +" та "+ "(" + xi +","+ -i + ")");
                }
            }
        }
        static void Points(int a, int b, int p)
        {
            Console.WriteLine($"Еліптична крива над полем GF({p}): y^2 = x^3 + {a}x + {b} mod {p}");
            Console.WriteLine("Точки: ");
            for ( int x = 0; x<=p; x++){
            int xr = (int)(Math.Pow(x, 3) + a * x + b);
            FindY(xr, p,x);
            }
        } 
        static void Gn(int Qx, int Qy, int a, int p)
        {
            int Qxt = Qx, Qyt = Qy; int i = 2;
            do
            {
                Console.WriteLine(i++);
                int m;
                if (Qx == Qxt)
                {
                    int md = Mod((3 * (int)Math.Pow(Qx, 2) + a), p);
                    int mdil = Mod((2 * Qy), p);
                    Console.WriteLine("m = " + md + " / " + mdil);
                    m = Mod((modInverse(mdil, p) * md), p);
                    Console.WriteLine("m = " + m);
                    Qxt = Mod(((int)Math.Pow(m, 2) - (2 * Qx)), p);
                    Qyt = Mod((-m * (Qxt - Qx) - Qy), p);
                    Console.WriteLine("Q: (" + Qxt + "," + Qyt + ")");
                }
                else
                {
                    int md = Mod((Qy - Qyt), p);
                    int mdil = Mod((Qx - Qxt), p);
                    Console.WriteLine("m = " + md + " / " + mdil);
                    m = Mod((modInverse(mdil, p) * md), p);
                    Console.WriteLine("m = " + m);
                    Qxt = Mod(((int)Math.Pow(m, 2) - Qx - Qxt), p);
                    Qyt = Mod((-m * (Qxt - Qx) - Qy), p);
                    Console.WriteLine("Q: (" + Qxt + "," + Qyt + ")");
                }

                if (Qx == Qxt)
                {
                    Console.WriteLine("n: " + i);
                }
            } while (Qx != Qxt);

        }
        static int Mod(int a, int b)
        {
            int result = a % b;
            return result < 0 ? result + b : result;
        }
        static void Main(string[] args)
        {
            
            int a = 1;
            int b = 1;
            int p = 23;
            //Points(a, b, p);
            Gn(17, 25, a, p);
        }
     
    }
}
