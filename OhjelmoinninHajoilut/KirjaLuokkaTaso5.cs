using System;
using System.Collections.Generic;

namespace Kirjakauppa
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

    public class Kirjasto
    {
        private Kirja[] BookArray { get; set; }
        private int BookCount { get; set; }

        public Kirjasto()
        {
            BookArray = new Kirja[100];
        }

        public void Lisaa(string code = "0", string name = "-",
                          string author = "-", double price = 0.0)
        {
            var book = new Kirja();
            BookArray[BookCount] = book;
            BookCount++;
        }

        public void Poista(string code)
        {
            var searchArray = BookArray;
            for (int index = 0; index < BookCount; index++)
            {
                if (BookArray[index] != null)
                {
                    if (BookArray[index].Koodi == code)
                    {
                        List<Kirja> bookList = new List<Kirja>(searchArray);
                        bookList.RemoveAt(index);
                        BookArray = bookList.ToArray();
                    }
                }
            }
            BookCount--;
        }

        public Kirja HaeKirjaKoodilla(string code)
        {
            foreach (Kirja book in BookArray)
            {
                if (book != null)
                {
                    if (book.Koodi == code)
                    {
                        return book;
                    }
                }
            }
            return null;
        }

        public Kirja HaeKirjaNimella(string name)
        {
            foreach (Kirja book in BookArray)
            {
                if (book != null)
                {
                    if (book.Nimi == name)
                    {
                        return book;
                    }
                }
            }
            return null;
        }

        public Kirja HaeKirjaTekijalla(string author)
        {
            foreach (Kirja book in BookArray)
            {
                if (book != null)
                {
                    if (book.Tekija == author)
                    {
                        return book;
                    }
                }
            }
            return null;
        }

        public string HaeKirjojenTiedot(string format = "{0}: {1}, {2}, {3} euroa")
        {
            foreach (Kirja book in BookArray)
            {
                if (book != null)
                {
                    Console.WriteLine(book.HaeKirjanTiedot());
                }
            }
            return null;
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var Library = new Kirjasto();
            char command;

            while (true)
            {
                command = AskCommand();

                switch (command)
                {
                    case 'l':
                        // lisää kirja taulukkoon
                        Library.Lisaa();
                        break;

                    case 'p':
                        Console.Write("Anna kirjan koodi: ");
                        string code = Console.ReadLine();
                        Library.Poista(code);
                        break;

                    case 'h':
                        // hae kirjan tiedot koodin, nimen tai tekijän mukaan
                        Console.Write("Hakutekijä(k, n, t): ");
                        char searchCommand = Console.ReadLine()[0];
                        Console.Write("Hakuehto: ");
                        string searchBy = Console.ReadLine();

                        if (searchCommand == 'k')
                        {
                            Kirja book = Library.HaeKirjaKoodilla(searchBy);
                            Console.WriteLine(book.HaeKirjanTiedot());
                        }

                        else if (searchCommand == 'n')
                        {
                            Kirja book = Library.HaeKirjaNimella(searchBy);
                            Console.WriteLine(book.HaeKirjanTiedot());
                        }

                        else if (searchCommand == 't')
                        {
                            Kirja book = Library.HaeKirjaTekijalla(searchBy);
                            Console.WriteLine(book.HaeKirjanTiedot());
                        }

                        break;

                    case 't':
                        // tulosta taulukon kirjat
                        Library.HaeKirjojenTiedot();
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
            Console.Write("Anna valintasi (l, p, h, t, q): ");
            string input = Console.ReadLine();
            char command = input[0];

            return command;
        }
    }
}
