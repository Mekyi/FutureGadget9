using System;
using System.Collections.Generic;

namespace kertotaulu
{
    static class kertotaulu
    {
        static int rivi = 1;
        static int sarake = 1;
        static int[,] taulukko = null;

        public static void Main(string[] args)
        {
            while (true)
            {
                LueRivitJaSarakkeet(out rivi, out sarake);
                if (rivi < 1 || sarake < 1)
                {
                    break;
                }
                taulukko = LuoTaulukko(rivi, sarake);
                TulostaTaulukko(taulukko);
            }
        }

        public static void LueRivitJaSarakkeet(out int rivi, out int sarake)
        {
            Console.WriteLine("Anna taulun rivien määrä:");
            try
            {
                rivi = int.Parse(Console.ReadLine());
            }
            catch
            {
                rivi = 0;
            }
            Console.WriteLine("Anna taulun sarakkeiden määrä:");
            try
            {
               sarake = int.Parse(Console.ReadLine());
            }
            catch
            {
                sarake = 0;
            }

        }

        public static int[,] LuoTaulukko(int rowCount, int columnCount)
        {
            taulukko = new int[rowCount, columnCount];

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    taulukko[rowIndex, columnIndex] = (rowIndex + 1) * (columnIndex + 1);
                }
            }

            return taulukko;
        }

        public static void TulostaTaulukko(int[,] taulu)
        {
            for (int rowNumber = 1; rowNumber < taulu.GetLength(1)+1; rowNumber++)
            {
                if (rowNumber < taulu.GetLength(1))
                {
                    Console.Write("{0} ", rowNumber);
                }

                else
                {
                    Console.Write(rowNumber);
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            for (int rowIndex = 0; rowIndex < taulu.GetLength(0); rowIndex++)
            {
                Console.Write(rowIndex+1);

                for (int columnIndex = 0; columnIndex < taulu.GetLength(1); columnIndex++)
                {
                    Console.Write(" {0}", taulu[rowIndex, columnIndex]);
                }

                Console.WriteLine();

            }

            Console.WriteLine();
        }
    }
}
