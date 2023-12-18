using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace lab_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BigInteger p = GenPQ(8);
            Console.WriteLine("p: \n" +p);

            BigInteger g = FindG(p);
            Console.WriteLine("g: \n" + g);

            BigInteger Srna = Genab(p);
            Console.WriteLine("a: \n" + Srna);
            BigInteger Srnb ;
            do
            {
                Srnb = Genab(p);

            } while (Srnb == Srna);
            Console.WriteLine("b: \n" + Srnb);

            BigInteger A = AB(g, p, Srna);
            Console.WriteLine("A: \n" + A);

            BigInteger B = AB(g, p, Srnb);
            Console.WriteLine("B: \n" + B);

            BigInteger KA = K(B, Srna, p);
            Console.WriteLine("KA: \n" + KA);

            BigInteger KB = K(A, Srnb, p);
            Console.WriteLine("KB: \n" + KB);

        }
        static BigInteger K(BigInteger AB, BigInteger ab, BigInteger p)
        {
            return BigInteger.ModPow(AB, ab, p);
        }
        static BigInteger AB(BigInteger g, BigInteger p, BigInteger ab)
        {
            return BigInteger.ModPow(g, ab, p);
        }
         static Random rand = new Random();
        static BigInteger Genab(BigInteger p)
        {
            BigInteger a;

            do
            {
                byte[] bytes = new byte[p.ToByteArray().Length];
                rand.NextBytes(bytes);
                a = new BigInteger(bytes);
            } while (a < 2 || a >= p - 2);
            return a;
        } 
        static BigInteger FindG(BigInteger p)
        {
            BigInteger phi = p - 1;
            BigInteger g;
            Random rand = new Random();
             while (true)
             {
                do
                {
                    byte[] bytes = new byte[p.ToByteArray().Length];
                    rand.NextBytes(bytes);
                    g = new BigInteger(bytes);
                    
                } while (g < 2 || g > phi);
                 if(BigInteger.ModPow(g, phi,p) == 1)
                {
                    return g;
                }
             }
            
        }
        static bool Miller_Rabin(BigInteger n, int k)
        {

            if (n <= 3 || n % 2 == 0)
            {
                return false;
            }
            else
            {
                BigInteger d = n - 1;
                BigInteger s = 0;
                while (d % 2 == 0)
                {
                    d /= 2;
                    s++;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;


                Random rand = new Random();
                BigInteger a;
                bool chk = false;

                for (int i = 0; i < k; i++)
                {
                    do
                    {
                        byte[] bytes = new byte[n.ToByteArray().Length];
                        rand.NextBytes(bytes);
                        a = new BigInteger(bytes);
                    } while (a < 2 || a >= n - 2);


                    BigInteger x = BigInteger.ModPow(a, d, n);

                    if (x == 1 || x == n - 1)
                    {
                        chk = true;
                        continue;
                    }
                    if (s == 1 && x != n - 1)
                    {
                        chk = false;
                    }

                    for (int r = 1; r < s; r++)
                    {
                        x = BigInteger.ModPow(x, 2, n);

                        if (x == n - 1)
                        {
                            chk = true;
                        }
                        else
                        {
                            chk = false;
                        }
                    }

                }
                if (chk)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
        }
        static BigInteger GenPQ(int size)
        {
            Random rand = new Random();
            BigInteger a;
            do
            {
                byte[] bytes = new byte[size / 8];
                rand.NextBytes(bytes);
                a = new BigInteger(bytes);
            } while (!Miller_Rabin(a, 5) || a.ToByteArray().Length != (size / 8));
            return a;
        }
    }

}
       

      
