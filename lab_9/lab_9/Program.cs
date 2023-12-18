using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Net.Security;

namespace lab_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BigInteger p = GenPQ(1024);
            Console.WriteLine("p: \n" + p);

            BigInteger g = FindG(p);
            Console.WriteLine("g: \n" + g);

            BigInteger a = Genab(p);
            Console.WriteLine("a: \n" + a);

            BigInteger h = BigInteger.ModPow(g, a, p);
            Console.WriteLine("h: \n" + h);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Public key: ( " + p +","+ g +","+ h +" )");
            Console.WriteLine("Private key: ( " + p + "," + a +" )");
            Console.ForegroundColor = ConsoleColor.Yellow;

            BigInteger msg = 2023;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Текст: " + msg);
            Console.ForegroundColor = ConsoleColor.Yellow;

            BigInteger[] enctext = encrypt(g, h, p, msg);

            BigInteger decmsg = dectext(a, enctext, p);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Розшифрований текст: " + decmsg);
            Console.ForegroundColor = ConsoleColor.Yellow;

        }
        static Random rand = new Random();
        static BigInteger inv(BigInteger a, BigInteger b)
        {
            BigInteger x0 = 1, x1 = 0, y0 = 0, y1 = 1;
            BigInteger oa = a, ob = b;
            while (b != 0)
            {
                BigInteger q = a / b;
                BigInteger temp = a;
                a = b;
                b = temp % b;

                BigInteger newX = x1;
                x1 = x0 - q * x1;
                x0 = newX;

                BigInteger newY = y1;
                y1 = y0 - q * y1;
                y0 = newY;
            }
            BigInteger d = a;
            BigInteger x = x0;
            BigInteger y = y0;
            if (d == 1)
                return (x % ob + ob) % ob;
            return 0;
        }
        static BigInteger dectext(BigInteger a, BigInteger[] enctext, BigInteger p)
        {
            BigInteger c1 = enctext[0];
            BigInteger c2 = enctext[1];

            //(c1^a)^-1 mod p
            BigInteger s = inv(BigInteger.ModPow(c1, a, p), p);

            //c2 * s mod p
            BigInteger msg = (c2 * s) % p;
            return msg;
        }
        static BigInteger[] encrypt(BigInteger g, BigInteger h, BigInteger p, BigInteger msg)
        {
            BigInteger r = Genab(p);
            Console.WriteLine("r: \n" + r);
            BigInteger c1 = BigInteger.ModPow(g, r, p);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("C1: \n" + c1);
            BigInteger s = BigInteger.ModPow(h, r, p);
            BigInteger c2 = (msg * s) % p;
            Console.WriteLine("C2 \n" + c2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            BigInteger[] enctext = new BigInteger[2];
            enctext[0] = c1;
            enctext[1] = c2;
            return enctext;

        }
        static BigInteger FindG(BigInteger p)
        {
            BigInteger phi = p - 1;
            BigInteger g;
            while (true)
            {
                do
                {
                    byte[] bytes = new byte[160/8];
                    rand.NextBytes(bytes);
                    g = new BigInteger(bytes);

                } while (g < 1 || g > phi);
                if (BigInteger.ModPow(g, phi, p) == 1)
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
        static BigInteger Genab(BigInteger p)
        {
            BigInteger a;
            do
            {
                byte[] bytes = new byte[160/8];
                rand.NextBytes(bytes);
                a = new BigInteger(bytes);
            } while (a < 1 || a >= p - 1);
            return a;
        }
    }
}
