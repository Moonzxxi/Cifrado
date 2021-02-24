using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cifrado
{
    class Program
    {
        //Cifrado cesar
        public static String abc = "abcdefghijklmopqrstuwxyzABCDEFGHIJKLMOPQRSTUWXYZ";

        static string cph(string mes, int despl)
        {
            string cif = "";
            if (despl>0 && despl<abc.Length)
            {
                for (int i = 0; i < mes.Length; i++)
                {
                    int posicion = PosAbc(mes[i]);
                    if (posicion!=-1)
                    {
                        int pos = posicion + despl;
                        while (pos>=abc.Length )
                        {
                            pos = pos - abc.Length;
                        }
                        cif += abc[pos];
                    }
                    else
                    {
                        cif += mes[i];
                    }
                }
            }
            return cif;
        }

        static int PosAbc(char caracter)
        {
            for (int i = 0; i < abc.Length; i++)
            {
                if (caracter==abc[i])
                {
                    return i;
                }
            }
            return -1;
        }
       //-------------------------------------------------------------------------------------------------------//
        static void Main(string[] args)
        {
            int Select;

            Console.WriteLine("Saludos...");
            Thread.Sleep(1000);
            
            Console.Clear();
            String menu = @"
             -------------------------------------------------------
               Seleccione el cifrado a usar:
             -------------------------------------------------------
                1. Cifrado Cesar
                2. Cifrado Vigenere
             -------------------------------------------------------";
            Console.WriteLine(menu);
            
            Console.WriteLine("");
            Console.Write("Su seleccion: ");
           
            Select = int.Parse(Console.ReadLine());
            Thread.Sleep(500);

            Console.WriteLine("Espere un momento...");
            Thread.Sleep(1000);

            Console.Clear();

            switch (Select)
            {
                case 1: //Cifrado Cesar
                    Console.WriteLine("Cifrado Cesar");
                    Console.WriteLine();
                    Thread.Sleep(700);

                    String mens = "";
                    String cipher;

                    Console.WriteLine("Escriba el mensaje: ");
                    mens = Console.ReadLine();
                    Thread.Sleep(700);

                    Console.WriteLine("------------------------------------------------------");

                    cipher = cph(mens, 3);
                    Console.WriteLine("Mensaje cifrado: {0}", cipher);
                    Console.WriteLine("------------------------------------------------------");

                    Console.ReadKey();
                    break;

                case 2://Cifrado Vigenere

                    Console.WriteLine("Cifrado Vigenere");
                    Console.WriteLine();
                    Thread.Sleep(700);

                    
                    string clearText = "";

                    Console.Write("Inserte el mensaje a cifrar: ");
                    clearText = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Mensaje Original: " + clearText);
                    Console.WriteLine();

                    List<char> alphabet =
                        Enumerable.Range('a', 'z' - 'a' + 1)
                        .Select(x => (char)x).ToList();

                    char[][] tabulaRecta = new char['z' - 'a' + 1][];
                    for (int i = 0; i < tabulaRecta.Length; i++)
                    {
                        tabulaRecta[i] = alphabet.ToArray();
                        var first = alphabet.First();
                        alphabet.Remove(first);
                        alphabet.Insert(alphabet.Count, first);
                    }

                    string keyword = "";
                    Console.Write("Inserte su llave: ");
                    keyword = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine("Llave: " + keyword);

                    Thread.Sleep(700);
                    Console.Clear();

                    Console.WriteLine("------------------------------------------------------");
                    string cipherText = Cipher(clearText, tabulaRecta, keyword);
                    Console.WriteLine("Mensaje Cifrado: {0}", cipherText);

                    string decipherText = Decipher(cipherText, tabulaRecta, keyword);
                    Console.WriteLine("Mensaje descifrado: {0}", decipherText);
                    Console.WriteLine("------------------------------------------------------");
                    Console.ReadKey();

                    break;
            }
            Console.ReadKey();
        }
      //------------------------------------------------------------------------------------------------------------//
        //metodos Vigenere

        private static string GrowToTextSize(int length, string keyword)
        {
            string result = keyword;

            int idx = 0;
            while (result.Length < length)
            {
                result += keyword[idx++];

                if (idx >= length)
                {
                    idx = 0;
                }
            }

            return result;
        }

        private static char[][] TransposeMatrix(char[][] matrix)
        {
            char[][] result = new char[matrix[0].Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new char[matrix.Length];
            }

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    result[col][row] = matrix[row][col];
                }
            }

            return result;
        }

        private static int IndexOf(char[] array, char toFind)
        {
            int result = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == toFind)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        private static string Cipher(
            string clearText, char[][] tabulaRecta, string keyword)
        {
            string result = string.Empty;

            keyword = GrowToTextSize(clearText.Length, keyword);

            for (int i = 0; i < clearText.Length; i++)
            {
                int row = clearText[i] - 'a';
                int col = keyword[i] - 'a';

                result += tabulaRecta[row][col];
            }

            return result;
        }

        private static string Decipher(
            string cipherText, char[][] tabulaRecta, string keyword)
        {
            string result = string.Empty;

            keyword = GrowToTextSize(cipherText.Length, keyword);
            tabulaRecta = TransposeMatrix(tabulaRecta);

            for (int i = 0; i < cipherText.Length; i++)
            {
                int row = keyword[i] - 'a';
                int col = IndexOf(tabulaRecta[row], cipherText[i]);

                result += tabulaRecta[0][col];
            }

            return result;
        }
    }
}
