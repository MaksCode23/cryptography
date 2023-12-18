using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LabC1
{
    internal class Program{
        static void Main(string[] args)
        {
            Console.SetWindowSize(130, 30);
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            bool showMenu = true;
            string[] alf = {"А","Б","В","Г","Ґ","Д","Е","Є","Ж","З","И","І","Ї","Й","К","Л","М","Н","О","П","Р",
        "С","Т","У","Ф","Х","Ц","Ч","Ш","Щ","Ь","Ю","Я"};

            while (true) {
                if (showMenu) {
                Console.ForegroundColor = ConsoleColor.Green;
                for (int i = 0; i < alf.Length; i++)
                {
                Console.Write("|"+alf[i]+""+i);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");

                Console.WriteLine("Оберіть дію:");
                Console.WriteLine("1. Зашифрувати текст");
                Console.WriteLine("2. Розшифрувати текст");

                Console.WriteLine();
                }
                ConsoleKeyInfo cki = Console.ReadKey(intercept: true);
                switch (cki.Key) {
                    case ConsoleKey.D1:
                        showMenu = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Введіть текст: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        string text = Console.ReadLine().ToUpper();  
                        int[] resultnumtext = textnum(alf, text);
                        foreach (int item in resultnumtext)
                        {
                            Console.Write(item + "|");
                        }

                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Введіть ключ: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        string key = Console.ReadLine().ToUpper();
                        int[] resultnumkey = keynum(alf, key, text.Length);
                        foreach (int item in resultnumkey)
                        {
                            Console.Write(item + "|");
                        }

                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Зашифрований текст");
                        Console.ForegroundColor = ConsoleColor.Green;
                        int[] resultencryptednum = encryptednum(resultnumkey, resultnumtext);
                        foreach (int item in resultencryptednum)
                        {
                            Console.Write(item + "|");
                        }
                        Console.WriteLine();
                        Console.WriteLine(textencryption(resultencryptednum, alf));
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("4. Очистити");
                        break;

                    case ConsoleKey.D2:
                        showMenu = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Введіть зашифрований текст: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        string encryptedtext = Console.ReadLine().ToUpper();
                        int[] resultnumenctext = textnum(alf, encryptedtext);
                        foreach (int item in resultnumenctext)
                        {
                            Console.Write(item + "|");
                        }

                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Введіть ключ: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        string decryptedkey = Console.ReadLine().ToUpper();
                        int[] resultnumdeckey = keynum(alf, decryptedkey, encryptedtext.Length);
                        foreach (int item in resultnumdeckey)
                        {
                            Console.Write(item + "|");
                        }
                        
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Розшифрований текст");
                        Console.ForegroundColor = ConsoleColor.Green;
                        int[] resultdecryptednum = decryptednum(resultnumdeckey, resultnumenctext);
                        foreach (int item in resultdecryptednum)
                        {
                            Console.Write(item + "|");
                        }

                        Console.WriteLine();

                        Console.WriteLine(textdecryption(resultdecryptednum, alf));
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("4. Очистити"); 
                        break;

                    case ConsoleKey.D4:
                        Console.Clear();
                        showMenu = true;
                        break;

                    default:
                        showMenu = false;
                        break;
                }
            }
        }

        static int[] textnum(string[] alf, string text)
        {
            int[] resultnumtext = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
            for(int j = 0; j < alf.Length; j++)
                {
                    if (text[i].ToString() == alf[j])
                    {
                        resultnumtext[i] = j;
                    } 
                }
            }
            return resultnumtext;
        }
        static int[] keynum(string[] alf, string key, int textlength)
        {
            int[] resultnumkey = new int[textlength];
            for (int i = 0; i < textlength; i++)
            {
                int keyIndex = i % key.Length;
                for (int j = 0; j < alf.Length; j++)
                {
                    if (key[keyIndex].ToString() == alf[j])
                    {
                        resultnumkey[i] = j;
                    }
                }
            }
            return resultnumkey;
        }
        static int[] encryptednum(int[] keynum, int[]textnum)
        {
            int[] resultencryptednum = new int[textnum.Length];
            for(int i = 0; i < resultencryptednum.Length; i++)
            {
                resultencryptednum[i] = (keynum[i] + textnum[i]) % 33;
            }
            return resultencryptednum;
        }
        static string textencryption(int[] ecryptednum, string[] alf) {
            string encryptedtext = "";
            for (int i = 0; i < ecryptednum.Length; i++)
            {
            for(int j = 0; j < alf.Length; j++)
                {
                    if (ecryptednum[i] == j) { 
                    encryptedtext += alf[j].ToString();   
                    }
                    
                }
            }
            return encryptedtext;
        }
        static int[] decryptednum(int[] decryptedkeynum, int[] encryptedtextnum)
        {
            int[] resultdecryptednum = new int[encryptedtextnum.Length];
            for (int i = 0; i < resultdecryptednum.Length; i++)
            {
                resultdecryptednum[i] = (encryptedtextnum[i] - decryptedkeynum[i] + 33) % 33;
            }
            return resultdecryptednum;
        }
        static string textdecryption(int[] decryptednum, string[] alf)
        {
            string decryptedtext = "";
            for (int i = 0; i < decryptednum.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (decryptednum[i] == j)
                    {
                        decryptedtext += alf[j].ToString();
                    }

                }
            }
            return decryptedtext;
        }
    }
    }

