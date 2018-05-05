using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Tikkakilpailu
{
    class Program
    {
        // Säännöt:
        // - 5 tikkaa / vuoro
        // - 3 vuoroa / heittäjä
        // - heittäjiä max 10
        // - paras tulos jää voimaan
        // - tasapeli mahdollinen

        public static void Main(string[] args)
        {
            string kilpailunNimi;       // kilpailun nimi. Käytetään myös tiedostonimessä
            string[,] tulokset = null;  // tulokset säilytetään kaksiulotteisessa taulukossa
            List<string> osallistujat;  // lista osallistujista
            char komento;               // käyttäjältä saatu komentokehote

            //Console.WriteLine("Tikkakilpailu");
            kilpailunNimi = KysyKilpailunNimi();

            while (true)
            {
                komento = KysyKomento();

                switch (komento)
                {
                    case 'u':  // uusi peli
                        kilpailunNimi = KysyKilpailunNimi();
                        osallistujat = KysyOsallistujat();
                        tulokset = LuoTaulukko(osallistujat);
                        for (int i = 0; i < osallistujat.Count; i++)
                        {
                            string piste = Console.ReadLine();
                            if (piste == "")
                            {
                                break;
                            }
                            else
                            {
                                tulokset[i, 2] = (int.Parse(tulokset[i, 2]) + int.Parse(piste)).ToString();
                            }

                        }
                        break;

                    case 'j':  // jatka peliä
                        tulokset = haeTiedostosta(kilpailunNimi);
                        for (int i = 0; i < 4; i++)
                        {
                            string piste = Console.ReadLine();
                            if (piste == "")
                            {
                                break;
                            }
                            else
                            {
                                if (i % 2 != 0)
                                {
                                    if (int.Parse(piste) > int.Parse(tulokset[1, 2]))
                                    {
                                        tulokset[1, 2] = piste;
                                    }
                                }
                                else
                                {
                                    if (int.Parse(piste) > int.Parse(tulokset[0, 2]))
                                    {
                                        tulokset[0, 2] = piste;
                                    }
                                }
                            }

                        }
                        break;

                    case 'h':  // tulosta tulokset heittojärjestyksessä
                        if (tulokset == null)
                        {
                            Console.WriteLine("Ei vielä tuloksia!");
                        }
                        else
                        {
                            tulosta(tulokset, 0);
                        }
                        break;

                    case 'o':  // tulosta tulokset aakkosjärjestyksessä
                        if (tulokset == null)
                        {
                            Console.WriteLine("Ei vielä tuloksia!");
                        }
                        else
                        {
                            tulosta(tulokset, 1);
                        }
                        break;

                    case 't':  // tulosta tulokset pistejärjestyksessä
                        if (tulokset == null)
                        {
                            Console.WriteLine("Ei vielä tuloksia!");
                        }
                        else
                        {

                            tulosta(tulokset, 2);
                        }
                        break;

                    case 'l':  // lopeta
                        tallennaTiedostoon(kilpailunNimi, tulokset);
                        return;

                    default:
                        Console.WriteLine("Komentoa ei tunnistettu.");
                        break;
                }
                Console.ReadLine();
            }
        }

        private static string[,] LuoTaulukko(List<string> osallistujat)
        {
            string[,] tulosTaulukko = new string[osallistujat.Count,4];
            for (int rowIndex = 0; rowIndex < osallistujat.Count; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 4; columnIndex++)
                {
                    if (columnIndex == 0)
                    {
                        tulosTaulukko[rowIndex, columnIndex] = (rowIndex+1).ToString();
                    }
                    else if (columnIndex == 1)
                    {
                        tulosTaulukko[rowIndex, columnIndex] = osallistujat[rowIndex];
                    }
                    else if (columnIndex == 2)
                    {
                        tulosTaulukko[rowIndex, columnIndex] = "0";
                    }
                    else if (columnIndex == 3)
                    {
                        tulosTaulukko[rowIndex, columnIndex] = "0";
                    }
                    else
                    {
                        Console.WriteLine("Out of range");
                    }
                }
            }
            return tulosTaulukko;
        }

        private static List<string> KysyOsallistujat()
        {
            List<string> osallistujat = new List<string>();
            string kilpailijanNimi;

            for (int i = 0; i < 10; i++)
            {
                Console.Write("Anna henkilön nimi: ");
                kilpailijanNimi = Console.ReadLine();

                if (kilpailijanNimi == "")
                {
                    break;
                }
                else
                {
                    osallistujat.Add(kilpailijanNimi);
                }
            }
            Console.WriteLine("");
            return osallistujat;
        }

        private static string KysyKilpailunNimi()
        {
            string kilpailunNimi;

            Console.Write("Anna kilpailun nimi: ");
            kilpailunNimi = Console.ReadLine();
            string tiedostonNimi = kilpailunNimi + ".txt";
            return tiedostonNimi;
        }

        private static char KysyKomento()
        {
            char komento;
            Console.WriteLine("(u)usi, (j)atka, (h)eittojärjestys, (t)ulokset, (o)sallistujat tai (l)opeta: ");
            komento = char.Parse(Console.ReadLine());
            return komento;
        }

        public static string[,] lajittele(string[,] tulokset, int per)
        {
            string[,] lajittelutaulukko = new string[tulokset.Length, 4];
            List<String> sarakeSisalto = new List<string>();

            // lajittelu halutussa järjestyksessä

            switch (per)
            {
                case 0:  // heittojärjestys (tulokset[,0])
                    lajittelutaulukko = tulokset;
                    break;

                case 1:  // aakkosjärjestys (tulokset[,1])
                    for (int rowIndex = 0; rowIndex < 2; rowIndex++)
                    {
                        sarakeSisalto.Add(tulokset[rowIndex, per]);
                    }
                    sarakeSisalto.Sort();

                    for (int rowIndex = 0; rowIndex < 2; rowIndex++)
                    {
                        string tempNimi = sarakeSisalto[rowIndex];
                        for (int i = 0; i < sarakeSisalto.Count; i++)
                        {
                            if (tempNimi == tulokset[i, 1])
                            {
                                for (int columnIndex = 0; columnIndex < 4; columnIndex++)
                                {
                                    lajittelutaulukko[rowIndex, columnIndex] = tulokset[i, columnIndex];
                                }
                            }
                        }
                    }
                    break;

                default:  // pistejärjestys oletuksena (tulokset[,2])
                    if (int.Parse(tulokset[1,2]) > int.Parse(tulokset[0, 2]))
                    {
                        for (int i = 0; i < tulokset.GetLength(1); i++)
                        {
                            lajittelutaulukko[0, i] = tulokset[1, i];
                        }
                        for (int i = 0; i < tulokset.GetLength(1); i++)
                        {
                            lajittelutaulukko[1, i] = tulokset[0, i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < tulokset.GetLength(1); i++)
                        {
                            lajittelutaulukko[0, i] = tulokset[0, i];
                        }
                        for (int i = 0; i < tulokset.GetLength(1); i++)
                        {
                            lajittelutaulukko[1, i] = tulokset[1, i];
                        }
                    }
                    break;
            }
            //Console.WriteLine(lajittelutaulukko);
            return lajittelutaulukko;
        }

        public static void tulosta(string[,] tulokset, int jar)
        {
            string[,] tuloksetLajiteltu;
            // tulostus halutussa järjestyksessä
            switch (jar)
            {
                case 0:  // heittojärjestys (tulokset[,0])
                    Console.WriteLine("Tikkakilpailu - Heittojärjestys");
                    Console.WriteLine("Heittojärjestys Nimi                Tulos     Heitot ");
                    tuloksetLajiteltu = lajittele(tulokset, 0);
                    for (int rivi = 0; rivi < 2; rivi++)
                    {
                        Console.WriteLine("{0}        {1}               {2}               {3}",
                            tuloksetLajiteltu[rivi,0], tuloksetLajiteltu[rivi,1],
                            tuloksetLajiteltu[rivi,2], tuloksetLajiteltu[rivi,3]);
                    }
                    break;

                case 1:  // aakkosjärjestys (tulokset[,1])
                    Console.WriteLine("Tikkakilpailu - Osallistujat");
                    Console.WriteLine("Nimi                Heittojärjestys Tulos     Heitot ");
                    tuloksetLajiteltu = lajittele(tulokset, 1);
                    for (int rivi = 0; rivi < 2; rivi++)
                    {
                        Console.WriteLine("{0}              {1}               {2}        {3} ",
                            tuloksetLajiteltu[rivi, 1], tuloksetLajiteltu[rivi, 0],
                            tuloksetLajiteltu[rivi, 2], tuloksetLajiteltu[rivi, 3]);
                    }
                    break;

                default:  // tulosjärjestys oletuksena (tulokset[,2])
                    Console.WriteLine("Tikkakilpailu - Tulokset");
                    Console.WriteLine("Tulos     Nimi                Heittojärjestys Heitot ");
                    tuloksetLajiteltu = lajittele(tulokset, 2);
                    for (int rivi = 0; rivi < 2 ; rivi++)
                    {
                        Console.WriteLine("{0}        {1}               {2}               {3}",
                            tuloksetLajiteltu[rivi, 2], tuloksetLajiteltu[rivi, 1],
                            tuloksetLajiteltu[rivi, 0], tuloksetLajiteltu[rivi, 3]);

                    }
                    break;
            }
        }

        public static void tallennaTiedostoon(string tiedosto, string[,] tulokset)
        {
            // Pelaajatiedot tallennetaan seuraavaan muotoon: 2;jukka;6;3
            // (ilmoittautumisjärjestys, nimi, tulos, heittojen määrä)
            StreamWriter tulosTiedosto;

            if (!File.Exists(tiedosto))  // luo tiedosto jos sitä ei vielä ole
            {
                tulosTiedosto = File.CreateText(tiedosto);
                tulosTiedosto.Close();
            }

            else  // tyhjennä jo olemassa oleva tiedosto uudelleenkirjoitusta varten
            {
                File.WriteAllText(tiedosto, String.Empty);
            }


            // Kirjoita tulokset tekstitiedostoon:
            tulosTiedosto = File.AppendText(tiedosto);
            for (int rowIndex = 0; rowIndex < 2; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 4; columnIndex++)
                {
                    if (columnIndex == 0)
                    {
                        tulosTiedosto.Write("{0}", tulokset[rowIndex, columnIndex]);
                    }
                    else
                    {
                        tulosTiedosto.Write(";{0}", tulokset[rowIndex, columnIndex]);
                    }
                }
                tulosTiedosto.WriteLine("");  // vaihda riviä
            }
            tulosTiedosto.Close();
        }

        public static string[,] haeTiedostosta(string tiedosto)
        {
            // Pelaajatiedot haetaan tiedostosta peliä tai tuloksien katsomista varten.
            string[] tulosTiedosto = File.ReadAllLines(tiedosto);
            string[,] tulokset = new string[tulosTiedosto.Length, 4];

            for (int rowIndex = 0; rowIndex < tulosTiedosto.Length; rowIndex++)
            {
                string[] splittedRow = tulosTiedosto[rowIndex].Split(';');

                for (int columnIndex = 0; columnIndex < 4; columnIndex++)
                {
                    tulokset[rowIndex, columnIndex] = splittedRow[columnIndex];
                }
            }

            //// Testaa taulukon tulostus:
            //for (int i = 0; i < tulosTiedosto.Length; i++)
            //{
            //    for (int y = 0; y < 4; y++)
            //    {
            //        Console.Write(tulokset[i, y] + " ");
            //    }
            //    Console.WriteLine("");
            //}
            return tulokset;
        }
    }
}
