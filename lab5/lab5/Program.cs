using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal class Program
    {
        static byte Mul02(byte val)
        {
            byte x = (byte)(val << 1);// множення на 02 
            if ((val & 0x80) != 0) // x^7 | 10000000 != 0 
            x ^= 0x1B; // x  XOR  m(x) = x^8 + x^4 + x^3 + x + 1
            return x;
        }
        static byte Mul03(byte val)
        {
            byte x = Mul02(val); //  BF * 02 
            x ^= val; // BF * 02 + BF
            return x;
        }
            
            static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. D4 * 02");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0:X2}", Mul02(0xD4) );
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. BF * 03");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0:X2}", Mul03(0xBF) );
        }
    }
}
