using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ohjelmoinnin_perusteet
{
    class Program
    {

        static void Main(string[] args)
        {
            int rows;
            int columns;

            rows = int.Parse(Console.ReadLine()) +1;
            columns = int.Parse(Console.ReadLine());
            string[] columnNames = new string[columns];

            for (int i = 0; i < columnNames.Length; i++)
            {
                columnNames[i] = Console.ReadLine();
            }

            string[,] taulukko = new string[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (row == 0)
                    {
                        taulukko[0, column] = columnNames[column];
                    }
                    else
                    {
                        taulukko[row, column] = ((row + 4) * 10 / (column +1)).ToString();
                    }
                }
            }

            Console.WriteLine("Taulukon koko: 4 x 3");
            Console.WriteLine("Taulukon tyyppi: System.Int32[,]");
            Console.WriteLine("Taulukko otsikoineen tulostettuna:");
            PrintTaulukko(rows, columns, taulukko);
            Console.ReadLine();
        }

        private static void PrintTaulukko(int rows, int columns, string[,] taulukko)
        {
            string[,] tulostettavaTaulukko = taulukko;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (row == 0)
                    {
                        Console.Write(" {0}", tulostettavaTaulukko[row, column]);
                    }
                    else
                    {
                        Console.Write("  {0}", tulostettavaTaulukko[row, column]);
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
