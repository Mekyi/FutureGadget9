using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Globalization;

namespace Tapahtumat
{
    class Program
    {

        public static void Main(string[] args)
        {
            while (true)
            {
                string valinta = LueValinta();
                switch (valinta)
                {
                    case "":
                        return;
                    case "l":
                        string tapahtuma = LueTapahtuma();
                        TallennaTapahtuma("tapahtumat.txt", GetDateTime(), tapahtuma);
                        break;

                    case "t":
                        TulostaTapahtumat("tapahtumat.txt");
                        break;

                    case "p":
                        PoistaTapahtumat("tapahtumat.txt");
                        break;

                    default:
                        break;
                }
            }
        }

        public static string LueValinta()
        {
            Console.Write("Anna valintasi: ");
            string valinta = Console.ReadLine();
            return valinta;
        }

        public static string LueTapahtuma()
        {
            Console.Write("Anna tapahtumarivi: ");
            string tapahtuma = Console.ReadLine();
            return tapahtuma;
        }

        public static void TallennaTapahtuma(string fileName, string aika, string tapahtuma)
        {
            StreamWriter fileIn;

            if (!File.Exists(fileName))
            {
                fileIn = File.CreateText(fileName);
                fileIn.Close();
            }

            fileIn = File.AppendText(fileName);
            fileIn.WriteLine("{0}: {1}", aika, tapahtuma);
            fileIn.Close();
        }

        public static void PoistaTapahtumat(string fileName)
        {
            File.Delete(fileName);
        }

        public static void TulostaTapahtumat(string fileName)
        {
            if (File.Exists(fileName))
            {
                Console.Write(File.ReadAllText(fileName));
            }
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString(CultureInfo.GetCultureInfo("fi-FI"));
        }
    }
}
