using System;
using System.Collections.Generic;

namespace VerkkoKirjasto
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

        public Kirja(string name, string author, string code, double price)
        {
            Koodi = code;
            Tekija = author;
            Nimi = name;
            Hinta = price;
        }

        public virtual string HaeKirjanTiedot(string format = "{0}: {1}, {2}, {3} euroa")
        {
            return string.Format("{0}: {1}, {2}, {3} euroa", Tekija, Nimi, Koodi, Hinta);
        }
    }

    public class Romaani : Kirja
    {
        public int Ika { get; set; }
        public string Alue { get; set; }

        public Romaani(string name, string author, string code, double price, int age, string area) 
            : base(name, author, code, price)
        {
            Ika = age;
            Alue = area;
        }

        public override string HaeKirjanTiedot(string format = "{0}: {1}, {2}, {3}, {4} vuotta, {5} euroa")
        {
            return string.Format("{0}: {1}, {2}, {3} vuotta, {4}, {5} euroa", Tekija, Nimi, Koodi, Ika, Alue, Hinta);
        }
    }

    public class Oppikirja : Kirja
    {
        public string Taso { get; set; }
        public string Ala { get; set; }

        public Oppikirja(string name, string author, string code, double price, string level, string field) : base(name, author, code, price)
        {
            Taso = level;
            Ala = field;
        }

        public override string HaeKirjanTiedot(string format = "{0}: {1}, {2}, {3}, {4}, {5} euroa")
        {
            return string.Format("{0}: {1}, {2}, {3}, {4}, {5} euroa", Tekija, Nimi, Koodi, Taso, Ala, Hinta);
        }
    }

    public class Kirjasto
    {
        List<Kirja> booklist = new List<Kirja>();

        public Kirjasto()
        {

        }

        // Add
        public void LisaaKirja(Kirja k)
        {
            booklist.Add(k);
        }

        // Remove

        public bool PoistaKirja(int koodi)
        {
            bool bookRemoved = false;

            foreach (Kirja kirja in booklist)
            {
                if (int.Parse(kirja.Koodi) == koodi)
                {
                    booklist.Remove(kirja);
                    bookRemoved = true;
                    return bookRemoved;
                }
            }
            return bookRemoved;
        }

        public bool PoistaKirja(string kms)
        {
            return true;
        }

        public void PoistaKirjat()
        {
            booklist.Clear();
        }

        // Search
        public Kirja HaeKirja(int koodi)
        {

            foreach (Kirja kirja in booklist)
            {
                if (int.Parse(kirja.Koodi) == koodi)
                {
                    return kirja;
                }
            }
            return null;
        }

        public Kirja HaeKirja(string nimi)
        {
            //string kirjanTiedot;

            //foreach (Kirja kirja in booklist)
            //{
            //    kirjanTiedot = kirja.HaeKirjanTiedot();
            //}
            return null;
        }

        public List<Kirja> HaeKirjat(string hakuehto)
        {
            List<Kirja> foundBooks = new List<Kirja>();
            string kirjanTiedot;

            foreach (Kirja kirja in booklist)
            {
                kirjanTiedot = kirja.HaeKirjanTiedot();
                if (kirjanTiedot.Contains(hakuehto))
                {
                    foundBooks.Add(kirja);
                }
            }

            return booklist;
        }

        public string HaeKirjojenTiedot(string format = "{0}: {1}, {2}, {3}, {4} vuotta, {5} euroa")
        {
            foreach (Kirja kirja in booklist)
            {
                Console.WriteLine(kirja.HaeKirjanTiedot());
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
                        //Console.Write("Lisäätkö romaanin vai oppikirjan: ");
                        char searchCommand = Console.ReadLine()[0];

                        if (searchCommand == 'r')
                        {
                            string nimi = Console.ReadLine();
                            string tekija = Console.ReadLine();
                            string koodi = Console.ReadLine();
                            double hinta = double.Parse(Console.ReadLine());
                            int ika = int.Parse(Console.ReadLine());
                            string alue = Console.ReadLine();

                            Romaani novel = new Romaani(nimi, tekija, koodi, hinta, ika, alue);
                            Library.LisaaKirja(novel);
                        }

                        else if (searchCommand == 'o')
                        {
                            string nimi = Console.ReadLine();
                            string tekija = Console.ReadLine();
                            string koodi = Console.ReadLine();
                            double hinta = double.Parse(Console.ReadLine());
                            string koulutustaso = Console.ReadLine();
                            string ala = Console.ReadLine();

                            Oppikirja textbook = new Oppikirja(nimi, tekija, koodi, hinta, koulutustaso, ala);
                            Library.LisaaKirja(textbook);
                        }
                        break;

                    case 'p':
                        //Console.Write("Anna kirjan koodi: ");
                        int code = int.Parse(Console.ReadLine());
                        Library.PoistaKirja(code);
                        break;

                    case 'h':
                        // hae kirjan tiedot koodin tai hakuehdon mukaan
                        //Console.Write("Anna kirjan koodi tai hakuehto: ");
                        string searchBy = Console.ReadLine();
                        int id;
                        bool isNumeric = int.TryParse(searchBy, out id);

                        if (isNumeric)
                        {
                            Kirja book = Library.HaeKirja(id);
                            Console.WriteLine(book.HaeKirjanTiedot());
                        }

                        else
                        {
                            List<Kirja> booklist = Library.HaeKirjat(searchBy);
                            foreach (Kirja kirja in booklist)
                            {
                                Console.WriteLine(kirja.HaeKirjanTiedot());
                            }

                        }
                        Console.Write("Anna komento: ");
                        break;

                    case 't':
                        // tulosta taulukon kirjat
                        Library.HaeKirjojenTiedot();
                        //Console.Write("Anna komento: ");
                        break;

                    case 'a':
                        // tyhjennä kirjasto
                        Library.PoistaKirjat();
                        Console.Write("Anna komento: ");
                        break;

                    case 'q':
                        // lopeta ohjelma
                        Console.Write("Anna komento: ");
                        return;

                    default:
                        break;
                }
            }
        }

        public static char AskCommand()
        {
            //Console.Write("Anna komento: ");
            string input = Console.ReadLine();
            char command = input[0];

            return command;
        }
    }
}
