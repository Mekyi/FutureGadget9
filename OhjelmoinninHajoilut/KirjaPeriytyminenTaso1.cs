using System;
using System.Collections.Generic;

namespace Verkkokirjakauppa
{
    public class Kirja
    {
        public string Koodi { get; set; }
        public string Tekija { get; set; }
        public string Nimi { get; set; }
        public double Hinta { get; set; }

        public Kirja()
        {
            //Console.Write("Anna kirjan koodi: ");
            //Koodi = Console.ReadLine();
            //Console.Write("Anna kirjan nimi: ");
            //Nimi = Console.ReadLine();
            //Console.Write("Anna kirjan tekijä: ");
            //Tekija = Console.ReadLine();
            //Console.Write("Anna kirjan hinta: ");
            //Hinta = double.Parse(Console.ReadLine());
        }

        public Kirja(string code, string author, string name, double price)
        {
            Koodi = code;
            Tekija = author;
            Nimi = name;
            Hinta = price;
        }

        public string HaeKirjanTiedot(string format = "{0}: {1}, {2}, {3} euroa")
        {
            string bookInfo = string.Format(format, Tekija, Nimi, Koodi, Hinta);
            return bookInfo;
        }
    }

    public class Romaani : Kirja
    {
        public int Ika { get; set; }
        public string Alue { get; set; }

        public Romaani()
        {
            Console.Write("Anna romaanin nimi: ");
            Nimi = Console.ReadLine();
            Console.Write("Anna romaanin tekijä: ");
            Tekija = Console.ReadLine();
            Console.Write("Anna romaanin koodi: ");
            Koodi = Console.ReadLine();
            Console.Write("Anna romaanin hinta: ");
            Hinta = double.Parse(Console.ReadLine());
            Console.Write("Anna romaanin kohderyhmän ikä: ");
            Ika = int.Parse(Console.ReadLine());
            Console.Write("Anna romaanin aihealue: ");
            Alue = Console.ReadLine();
        }

        public new string HaeKirjanTiedot(string format = "{0}: {1}, {2}, {3}, {4} vuotta, {5} euroa")
        {
            string bookInfo = string.Format(format, Tekija, Nimi, Koodi, Alue, Ika, Hinta);
            return bookInfo;
        }
    }

    class Oppikirja : Kirja
    {
        public string Taso { get; set; }
        public string Ala { get; set; }

        public Oppikirja()
        {
            Console.Write("Anna oppikirjan nimi: ");
            Nimi = Console.ReadLine();
            Console.Write("Anna oppikirjan tekijä: ");
            Tekija = Console.ReadLine();
            Console.Write("Anna oppikirjan koodi: ");
            Koodi = Console.ReadLine();
            Console.Write("Anna oppikirjan hinta: ");
            Hinta = double.Parse(Console.ReadLine());
            Console.Write("Anna oppikirjan koulutustaso: ");
            Taso = Console.ReadLine();
            Console.Write("Anna oppikirjan tieteen ala: ");
            Ala = Console.ReadLine();
        }

        public new string HaeKirjanTiedot(string format = "{0}: {1}, {2}, {3}, {4}, {5} euroa")
        {
            string bookInfo = string.Format(format, Tekija, Nimi, Koodi, Ala, Taso, Hinta);
            return bookInfo;
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var novel = new Romaani();
            var textbook = new Oppikirja();
            Console.WriteLine("Kirjojen tiedot:");
            Console.WriteLine(novel.HaeKirjanTiedot());
            Console.WriteLine(textbook.HaeKirjanTiedot());
            Console.ReadLine();
        }
    }
}
