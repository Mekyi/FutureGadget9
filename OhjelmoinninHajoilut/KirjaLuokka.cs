using System;
using System.Collections.Generic;

namespace Kirja
{
    public class Kirja
    {
        public string Koodi { get; set; }
        public string Tekija { get; set; }
        public string Nimi { get; set; }
        public double Hinta { get; set; }

        public Kirja()
        {

        }

        public Kirja(string code, string author, string name, double price)
        {
            Koodi = code;
            Tekija = author;
            Nimi = name;
            Hinta = price;
        }


        public string HaeKirjanTiedot(string format)
        {
            string bookInfo = string.Format(format, Tekija, Nimi, Koodi, Hinta);
            return bookInfo;
        }


    }

        static class Program
    {
        public static void Main(string[] args)
        {
            var firstBook = new Kirja();
            Console.WriteLine("Anna ensimmäisen kirjan nimi: ");
            firstBook.Nimi = Console.ReadLine();
            Console.WriteLine("Anna ensimmäisen kirjan tekijä: ");
            firstBook.Tekija = Console.ReadLine();
            Console.WriteLine("Anna ensimmäisen kirjan koodi: ");
            firstBook.Koodi = Console.ReadLine();
            Console.WriteLine("Anna ensimmäisen kirjan hinta: ");
            firstBook.Hinta = double.Parse(Console.ReadLine());

            var secondBook = new Kirja();
            Console.WriteLine("Anna toisen kirjan nimi: ");
            secondBook.Nimi = Console.ReadLine();
            Console.WriteLine("Anna toisen kirjan tekijä: ");
            secondBook.Tekija = Console.ReadLine();
            Console.WriteLine("Anna toisen kirjan koodi: ");
            secondBook.Koodi = Console.ReadLine();
            Console.WriteLine("Anna toisen kirjan hinta: ");
            secondBook.Hinta = double.Parse(Console.ReadLine());

            Console.WriteLine("Kirjojen tiedot kalliimmasta halvempaan:");

            string formatType = "{0}: {1}, {2}, {3} euroa";

            if (firstBook.Hinta > secondBook.Hinta)
            {
                Console.WriteLine(firstBook.HaeKirjanTiedot(formatType));
                Console.WriteLine(secondBook.HaeKirjanTiedot(formatType));
            }
            else
            {
                Console.WriteLine(secondBook.HaeKirjanTiedot(formatType));
                Console.WriteLine(firstBook.HaeKirjanTiedot(formatType));
            }

        }
    }
}
