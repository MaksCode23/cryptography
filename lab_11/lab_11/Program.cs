using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Policy;

namespace lab_11
{
    internal class Program
    {

        static Random random = new Random();
        static BigInteger modInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger t = m;

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
        static BigInteger GenQ(int size, BigInteger  p)
        {
            BigInteger q;

            do
            {
                q = Genrandbitsize(size);
            } while (!Miller_Rabin(q, 1) || (p % q != 0));       
            return q;
        }
        static BigInteger GenP(int size)
        {
            BigInteger p;
            do
            {
                p = Genrandbitsize(size);
            } while (!Miller_Rabin(p, 5));

            return p;
        }
        static BigInteger Genrandsizepq(BigInteger size)
        {
            byte[] data = new byte[size.ToByteArray().Length];
            random.NextBytes(data);
            BigInteger result = new BigInteger(data);
            result = BigInteger.Abs(result);
            return result;
        }
        static BigInteger GenrandAK(BigInteger q)
        {
            return Genrandsizepq(q - 1) + 1; 
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
        static BigInteger Genrandbitsize(int sizebit)
        {
            int size = sizebit / 8;
            byte[] data = new byte[size];
            random.NextBytes(data);
            BigInteger result = new BigInteger(data);
            result = BigInteger.Abs(result);
            return result;
        }
        static BigInteger HashSha1(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] hash;
            using (SHA1 sha1 = SHA1.Create())
            {
                hash = sha1.ComputeHash(data);
            }
            
            BigInteger h = new BigInteger(hash);
            return h;
        }
        static BigInteger FindGenerator(BigInteger p, BigInteger q)
        {
            for (BigInteger i = 2; i < p - 1; i++)
            {
                BigInteger gq = BigInteger.ModPow(i, q, p);
                BigInteger exp = (p - 1) / q;
                BigInteger gp = BigInteger.ModPow(i, exp, p);

                if (gq != 1 && gp != 1)
                {
                    return i;
                }
            }

            return 0;
        }
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            
            int t = 0;
            int sizebit = 512 + 64 * t;
            BigInteger p = GenP(sizebit);
            //BigInteger p = 124540019;
            Console.WriteLine("p: " + p);
            BigInteger q = GenQ(160,p);
            // BigInteger q = GenQ(160,p);
            //BigInteger q = 17389;
            Console.WriteLine("q: " + q);

            BigInteger g = FindGenerator(p, q);
            
            //BigInteger g = 110217528;

            Console.WriteLine("g: " + g);

            BigInteger exp = BigInteger.Divide(BigInteger.Subtract(p, 1), q);

            Console.WriteLine(" ( p - 1 ) / q: " + exp);

            BigInteger a = BigInteger.ModPow(g, exp, p);
            Console.WriteLine("a: " + a);

            BigInteger aint = GenrandAK(q);
            //BigInteger aint = 12496;
            Console.WriteLine("aint : " + aint);

            BigInteger y = BigInteger.ModPow(a, aint, p);
            Console.WriteLine("y: " + y);

            Console.WriteLine("public keys: (p , q ,a ,y) private key : ( aint ) ");

            string text = "Текст";
            Console.WriteLine("text: " + text);

            BigInteger h = BigInteger.Abs( HashSha1(text));
            //BigInteger h = 5246;
            Console.WriteLine("Хеш : " + h);  

            BigInteger k,r,s,kinv;

            do
            {
                k = GenrandAK(q);
               //k = 9557;
                kinv = modInverse(k, q);
                r = BigInteger.Remainder(BigInteger.ModPow(a, k, p), q);
                s = BigInteger.Remainder(BigInteger.Multiply(kinv, BigInteger.Add(h, BigInteger.Multiply(aint, r))), q);


            } while (k >= q && r > q && s > q);

            
            Console.WriteLine("k: "  + k);
            Console.WriteLine("r: " + r);
            Console.WriteLine("k ^ -1 mod q : " + kinv);  

            Console.WriteLine("s: " + s);

            BigInteger w = modInverse(s, q);
            Console.WriteLine("w: " + w);

            BigInteger u1 = BigInteger.Remainder(BigInteger.Multiply(w, h), q);
            Console.WriteLine("u1: " + u1);

            BigInteger u2 = BigInteger.Remainder(BigInteger.Multiply(r, w), q);
            Console.WriteLine("u2: " + u2);


            BigInteger u = ((BigInteger.ModPow(a, u1, p) * BigInteger.ModPow(y, u2, p)) % p) % q;
            //BigInteger u = ((Pow(a, u1) * Pow(y,u2))%p)%q;
            Console.WriteLine("u: " + u);

            if(u == r)
            {
                Console.WriteLine("r: " + r + " = " + "u: " + u);
            }
            else
            {
                Console.WriteLine("r: " + r + " != " + "u: " + u);
            }

        }
    }
}
