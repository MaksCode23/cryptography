using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection;


namespace lab_7_2
{
    internal class Program
    {
        static void Main(string[] args)
        {   int size = 512;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.SetWindowSize(155, 30);
            BigInteger p = GenPQ(size);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("p:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(p);
            BigInteger q;
            do
            {
                q = GenPQ(size);
            } while (q == p);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("q:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(q);
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("n phi:");
            Console.ForegroundColor = ConsoleColor.Green;
            BigInteger n_phi = (p-1) * (q-1);
            Console.WriteLine(n_phi);

            BigInteger n = p * q;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("n: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(n);

            BigInteger e = PKey(n_phi);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Public key (e): ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e);

            BigInteger d =  PrKey(e,n_phi);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Private key (d): ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(d);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Повідомлення: ");
            string text = "Лаба 7.2 RSA";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);

            BigInteger enctext = Encrypt(text, e, n);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Зашифроване повідомлення: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(enctext);

            string dectext  =  Decrypt(enctext,d,n);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Розшифроване повідомлення: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(dectext);

        }
        static BigInteger Encrypt(string text, BigInteger e, BigInteger n)
        {
            
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            BigInteger m = new BigInteger(bytes);
            return BigInteger.ModPow(m, e, n);
        }
        static string Decrypt(BigInteger enctext, BigInteger d, BigInteger n)
        {
            BigInteger decrypted = BigInteger.ModPow(enctext, d, n);
            byte[] bytes = decrypted.ToByteArray();
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }
        static BigInteger PrKey(BigInteger a, BigInteger b)
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
            if(d==1)
            return (x % ob + ob) % ob;
            return 0;
        }
    
    

        static BigInteger PKey(BigInteger n_phi)
        {
            Random random = new Random();
            BigInteger e;

            do
            {
                byte[] randomBytes = new byte[n_phi.ToByteArray().Length];
                random.NextBytes(randomBytes);
                e = new BigInteger(randomBytes);
            } while (e <= 2 || e >= n_phi - 1 || Gcd(e, n_phi) != 1);

            return e;
        }
        static BigInteger Gcd(BigInteger e, BigInteger n_phi) 
        {
            while (n_phi != 0)
            {
                BigInteger temp = n_phi;
                n_phi = e % n_phi;
                e = temp;
            }
            return e;
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
                byte[] bytes = new byte[size/8];
                rand.NextBytes(bytes);
                a = new BigInteger(bytes);
            } while (!Miller_Rabin(a, 5) || a.ToByteArray().Length != (size/8)); 
            return a;
        }
    }
}
