using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LabC11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            string[] alf = {"А","Б","В","Г","Ґ","Д","Е","Є","Ж","З","И","І","Ї","Й","К","Л","М","Н","О","П","Р",
        "С","Т","У","Ф","Х","Ц","Ч","Ш","Щ","Ь","Ю","Я"};
            Console.WriteLine();
            char[,] MArray;
            char[] sortalfc; 
            string enctext;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Введіть стовпцевий ключ: ");
            string keycol = Console.ReadLine().ToUpper();
            Console.WriteLine("Введіть рядковий ключ: ");
            string keystring = Console.ReadLine().ToUpper();
            Console.WriteLine("Введіть текст: ");
            string text = Console.ReadLine().ToUpper();
            if (keycol.Length * keystring.Length < text.Length)
            {
                Console.WriteLine("Змініть ключі текст не поміщається в таблицю." + "\n" + "Символів доступно: "
                    + keycol.Length * keystring.Length + "\n" +
                    "Символів в тексті: " + text.Length);
                    Environment.Exit(0);
            }
            // Шифрування
            MArray = InitTbl(keystring, keycol, text);
            sortalfc = Sortkeycol(alf, MArray, keycol);
            Sortcoltbl(MArray, sortalfc); 
            PrintTbl(MArray);
            sortalfc = Sortkeyrow(alf, MArray, keystring);
            Sortrowtbl(MArray, sortalfc);
            PrintTbl(MArray);
            enctext = Printr(MArray,true);
            Console.WriteLine("|" + enctext + "|");

            //Дешифрування
            string sortkeystring = Sortdkey(alf, keystring);
            string sortkeycol = Sortdkey(alf, keycol);
            string formdectext = Formatdectext(keystring.Length, enctext); 
            MArray = InitTbl(sortkeystring, sortkeycol, formdectext);
            sortalfc = keystring.ToArray();
            Sortrowtbl(MArray, sortalfc);
            PrintTbl(MArray);
            sortalfc = keycol.ToArray();
            Sortcoltbl(MArray, sortalfc);
            PrintTbl(MArray);
            enctext = Printr(MArray, false);
            Console.WriteLine("|" + enctext + "|");
        }
        static string Formatdectext(int numrow, string enctext)
        {
            string formdectext = "";
            for (int i = 0; i < numrow; i++)
            {
                for (int j = i; j < enctext.Length; j += numrow)
                {
                    formdectext += enctext[j];
                }
            }
            return formdectext;
        }
        static string Sortdkey(string[] alf, string keys)
        {
            int[] numkeyc = new int[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (alf[j] == keys[i].ToString())
                    {
                        numkeyc[i] = j;
                    }
                }
            }
            Array.Sort(numkeyc);
            string sortdkey = "";
            for(int i = 0;i < keys.Length; i++)
            {
                for (int j = 0;j < alf.Length; j++)
                {
                    if (j == numkeyc[i])
                    {
                        sortdkey += alf[j].ToString();
                    }
                }
            }
            return sortdkey;
        }
        static string Printr(char[,] MArray, bool x)
        {
            string result = "";
            if (x) {
            for (int j = 1; j < MArray.GetLength(1); j++)
            {
                for (int i = 1; i < MArray.GetLength(0); i++)
                {
                    result += MArray[i, j];
                }
            } 
            } 
            else
            {
            for (int i = 1; i < MArray.GetLength(0); i++)
                {
                for (int j = 1; j < MArray.GetLength(1); j++)
                    {
                        result += MArray[i, j];
                    }
                }
            }
            return result;
        }
        static char[] Sortkeyrow(string[] alf, char[,] MArray, string keyrow)
        {
            int[] numkeyc = new int[keyrow.Length];
            for (int i = 0; i < keyrow.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (alf[j] == MArray[i+1, 0].ToString())
                    {
                        numkeyc[i] = j;
                    }
                }
            }
            Array.Sort(numkeyc);
            char[] sortalfc = new char[numkeyc.Length];
            for (int i = 0; i < keyrow.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (j == numkeyc[i])
                    {
                        sortalfc[i] = alf[j].ToCharArray()[0];
                    }
                }
            }
            return sortalfc;
        }
        static char[,] Sortrowtbl(char[,] MArray, char[] sortalfc)
        {
            char[,] tempcol = new char[sortalfc.Length , MArray.GetLength(1)];

            for (int i = 0; i < sortalfc.Length; i++)
            {
                for (int j = 1; j < MArray.GetLength(0); j++)
                {
                    if (MArray[j, 0] == sortalfc[i])
                    {
                        for (int k = 0; k < MArray.GetLength(1); k++)
                        {
                            tempcol[i, k] = MArray[j, k];
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < tempcol.GetLength(0); i++)
            {
                for (int j = 0; j < tempcol.GetLength(1); j++)
                {
                    MArray[i+1, j] = tempcol[i, j];
                }
            }
            return MArray;
        }
        static char[,] Sortcoltbl(char[,] MArray, char[] sortalfc)
        {
            char[,] tempcol = new char[MArray.GetLength(0), sortalfc.Length];

            for (int i = 0; i < sortalfc.Length; i++)
            {
                for (int j = 1; j < MArray.GetLength(1); j++)
                {
                    if (MArray[0, j] == sortalfc[i])
                    {
                        for (int k = 0; k < MArray.GetLength(0); k++)
                        {
                            tempcol[k, i] = MArray[k, j];
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < tempcol.GetLength(0); i++)
            {
                for (int j = 0; j < tempcol.GetLength(1); j++)
                {
                    MArray[i, j + 1] = tempcol[i, j];
                }
            }
            return MArray;
        }
        static char[] Sortkeycol(string[] alf, char[,] MArray, string keycol)
        {
            int[] numkeyc = new int[keycol.Length];
            for (int i = 0; i < keycol.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (alf[j] == MArray[0, i + 1].ToString())
                    {
                        numkeyc[i] = j;
                    }
                }
            }
            Array.Sort(numkeyc);
            char[] sortalfc = new char[numkeyc.Length];
            for (int i = 0; i < keycol.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (j == numkeyc[i])
                    {
                        sortalfc[i] = alf[j].ToCharArray()[0];
                    }
                }
            }
            return sortalfc;
        }
        static char [,] InitTbl(string keystring, string keycol, string text)
        {
            char [,] MArray = new char[keystring.Length + 1, keycol.Length + 1];

            for (int i = 0; i < MArray.GetLength(0); i++)
            {
                for (int j = 0; j < MArray.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    {
                        MArray[i, j] = ' ';
                    }
                    else if (i == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        MArray[i, j] = keycol[j - 1];
                    }
                    else if (j == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        MArray[i, j] = keystring[i - 1];
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        int textId = (i - 1) * keycol.Length + (j - 1);
                        MArray[i, j] = (textId < text.Length) ? text[textId] : ' ';
                    }
                    Console.Write(MArray[i, j] + "\t");
                }
                Console.WriteLine();
            }
            return MArray;
        }
        static void PrintTbl(char[,] MArray)
        {
            Console.WriteLine();
            for (int i = 0; i < MArray.GetLength(0); i++)
            {
                for (int j = 0; j < MArray.GetLength(1); j++)
                {
                    if (i == 0 || j == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.Write(MArray[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
    }

