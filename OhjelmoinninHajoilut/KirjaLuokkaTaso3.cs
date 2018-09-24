using System;

namespace Kirjasto
{
    public class Kirja
    {
        public string Koodi { get; set; }
        public string Tekija { get; set; }
        public string Nimi { get; set; }
        public double Hinta { get; set; }

        public Kirja()
        {
            Console.Write("Anna kirjan koodi: ");
            Koodi = Console.ReadLine();
            Console.Write("Anna kirjan nimi: ");
            Nimi = Console.ReadLine();
            Console.Write("Anna kirjan tekijä: ");
            Tekija = Console.ReadLine();
            Console.Write("Anna kirjan hinta: ");
            Hinta = double.Parse(Console.ReadLine());
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

    public static class Program
    {
        public static void Main(string[] args)
        {
            Kirja[] bookArray = new Kirja[100];
            int bookCount = 0;

            while (true)
            {
                char command = AskCommand();

                switch (command)
                {
                    case 'l':
                        // lisää kirja taulukkoon
                        var book = new Kirja();
                        bookArray[bookCount] = book;
                        bookCount++;
                        break;

                    case 'p':
                        // poista kirja taulukosta
                        break;

                    case 'h':
                        // hae kirjan tiedot koodin, nimen tai tekijän mukaan
                        break;

                    case 't':
                        // tulosta taulukon kirjat
                        foreach (Kirja item in bookArray)
                        {
                            if (item != null)
                            {
                                Console.WriteLine(item.HaeKirjanTiedot());
                            }
                        }
                        break;

                    case 'q':
                        // lopeta ohjelma
                        return;

                    default:
                        break;
                }
            }
        }

        public static char AskCommand()
        {
            Console.WriteLine("Anna valintasi (l, p, h, t, q): ");
            string input = Console.ReadLine();
            char command = input[0];

            return command;
        }
    }
}
