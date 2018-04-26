using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KertomanLaskenta
{
    class Program
    {

        public static void Main(string[] args)
        {
            while (true)
            {
                int number = lueKokonaisluku();
                if (number < 0)
                {
                    break;
                }
            }
            Console.WriteLine("Ohjelma lopetetaan.");
        }

        public static int laskeKertoma(int luku)
        {
            int kertoma = 1;
            for (int i = 1; i <= luku; i++)
            {
                kertoma *= i;
            }
            return kertoma;
        }

        public static int lueKokonaisluku()
        {
            Console.Write("Anna kokonaisluku: ");
            int number;
            var input = Console.ReadLine();
            if (int.TryParse(input, out number))
            {
                if (number > 0)
                {
                    tulostaLuvunKertoma(number);
                    return 0;
                }
                else if (number == 0)
                {
                    Console.WriteLine("0! = 1");
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("Virheellinen syöte.");
                return 0;
            }
        }

        public static void tulostaLuvunKertoma(int number)
        {
            if (number != 0)
            {
                Console.WriteLine("{0}! = {1}", number, laskeKertoma(number));
            }
        }
    }
}
