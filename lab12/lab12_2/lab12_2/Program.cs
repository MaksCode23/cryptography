using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12_2
{
    internal class Program
    {
        static Random rand = new Random();
        static void FindY(int x, int p, int xi, out int[] M, out bool chk)
        {
            M = new int[2];
            chk = false;
            for (int i = 0; i <= p; i++)
            {
                int y = (int)Math.Pow(i, 2);
                if (Mod(y, p) == Mod(x, p))
                {
                    chk = true;
                    M[0] = xi;
                    M[1] = i;
                    break;
                } 
            }    
        }
        static int [] Points(int a, int b, int p)
        {
            int[] M;
            bool chk;
            do
            {
                int x = rand.Next(p + 1);
                int xr = (x + a * x + b);
                FindY(xr, p, x, out M, out chk);

            } while (!chk);
            return M;
        }
        static int Mod(int a, int b)
        {
            int result = a % b;
            return result < 0 ? result + b : result;
        }
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
        static int[] Gn(int Qx, int Qy, int a, int p,int n)
        {
            int Qxt = Qx, Qyt = Qy; int i = 2;
            int[] Q = new int[2]; 
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
                if(n+1 == i)
                {
                    Q[0] = Qxt; Q[1] = Qyt;
                    return Q;
                    break;
                }
            } while (Qx != Qxt);
            return Q;
        }
        static int Findn(int Qx, int Qy, int a, int p)
        {
            int Qxt = Qx, Qyt = Qy; int i = 2;

            do
            {
                i++;
                int m;
                if (Qx == Qxt)
                {
                    int md = Mod((3 * (int)Math.Pow(Qx, 2) + a), p);
                    int mdil = Mod((2 * Qy), p);
                    m = Mod((modInverse(mdil, p) * md), p);
                    Qxt = Mod(((int)Math.Pow(m, 2) - (2 * Qx)), p);
                    Qyt = Mod((-m * (Qxt - Qx) - Qy), p);
                }
                else
                {
                    int md = Mod((Qy - Qyt), p);
                    int mdil = Mod((Qx - Qxt), p);
                    m = Mod((modInverse(mdil, p) * md), p);
                    Qxt = Mod(((int)Math.Pow(m, 2) - Qx - Qxt), p);
                    Qyt = Mod((-m * (Qxt - Qx) - Qy), p);
                }

                if (Qx == Qxt)
                {
                    break;
                } 
            } while (Qx != Qxt);
            return i;
        }
        static void Main(string[] args)
        {
            int a = 1;
            int b = 1;
            int p = 23;
            int []M = Points(a, b, p);
            Console.WriteLine("Повiдомлення: M (" + M[0] + "," + M[1] + ")");
            int Qx = 17 , Qy = 20;
            int nO = Findn(Qx, Qy, a, p);
            Console.WriteLine("n: " + nO);
            int d = rand.Next(nO - 3) + 2;
            Console.WriteLine("Секретний ключ d: " + d);
            int []Q = Gn(Qx, Qy, a, p, d);
            Console.WriteLine("Публiчний ключ: Q (" + Q[0] + "," + Q[1] + ")");
            int k = rand.Next(nO - 2) + 2;
            Console.WriteLine("Випадкове  число k: " + k);
            int []R = Gn(Qx, Qy, a, p, k);
            Console.WriteLine("Точка R (" + R[0] + "," + R[1] + ")");
            int[] T = Gn(Q[0], Q[1], a, p, k);
            Console.WriteLine("Точка T (" + T[0] + "," + T[1] + ")");
            int C1 = Mod((M[0] + T[0]), p);
            int C2 = Mod((M[1] + T[0]), p);
            Console.WriteLine("Зашифроване повiдомлення: (" + C1 + "," + C2 + ")");
            int[] Tr = Gn(R[0], R[1], a, p, d);
            Console.WriteLine("Точка T знайдена через d (" + Tr[0] + "," + Tr[1] + ")");
            int M1 = Mod((C1 - Tr[0]), p);
            int M2 = Mod((C2 - Tr[0]), p);
            Console.WriteLine("Розшифроване повiдомлення: (" + M1 + "," + M2 + ")");
        }
    }
}
