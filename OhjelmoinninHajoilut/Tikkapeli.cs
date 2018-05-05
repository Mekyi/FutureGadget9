using System;
using System.Collections.Generic;
using System.IO;

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

        // Kutsutaan luokan laajuisesti toimivat muuttujat.
        private static string kilpailunNimi;                            // kilpailun nimi. Käytetään myös tiedostonimessä
        private static List<string> osallistujat = new List<string>();  // lista osallistujista
        private static string[,] tulokset = null;                       // tulokset säilytetään kaksiulotteisessa taulukossa
        private static char komento;                                    // käyttäjältä saatu komentokehote

        public static void Main(string[] args)
        {
            KysyKilpailunNimi();

            while (true)
            {
                KysyKomento();  // Käyttäjältä kysytään komento aina loopin alkaessa

                switch (komento)  // Suoritetaan metodeja perustuen käyttäjän antamaan komentoon
                {
                    case 'u':  // uusi peli
                        KysyKilpailunNimi();
                        KysyOsallistujat();  // Täyttää osallistujat listan osallistujien nimillä
                        LuoTaulukko(osallistujat);
                        PelaaPelia();
                        break;

                    case 'j':  // jatka peliä aiemmin annetun kilpailunimen perusteella
                        if (!PeliKesken()) // tarkista onko peli kesken. Mikäli ei ole, kysy mahdollisia lisäkilpailijoita
                        {
                            Console.Write("Anna henkilön nimi: ");
                            string uusiPelaaja = Console.ReadLine();
                            if (uusiPelaaja != "")
                            {
                                osallistujat.Add(uusiPelaaja);
                            }
                        }
                        haeTiedostosta(kilpailunNimi);
                        JatkaPelia();
                        tallennaTiedostoon(kilpailunNimi, tulokset);
                        Console.Write("(u)usi, (j)atka, (h)eittojärjestys, (t)ulokset, (o)sallistujat tai (l)opeta: ");
                        break;

                    case 'h':  // tulosta tulokset heittojärjestyksessä
                        if (tulokset == null)
                        {
                            //Console.WriteLine("Ei vielä tuloksia!");
                        }
                        else
                        {
                            tulosta(tulokset, 0);
                            Console.Write("(u)usi, (j)atka, (h)eittojärjestys, (t)ulokset, (o)sallistujat tai (l)opeta: ");
                        }
                        break;

                    case 'o':  // tulosta tulokset aakkosjärjestyksessä
                        if (tulokset == null)
                        {
                            //Console.WriteLine("Ei vielä tuloksia!");
                        }
                        else
                        {
                            tulosta(tulokset, 1);
                            Console.Write("(u)usi, (j)atka, (h)eittojärjestys, (t)ulokset, (o)sallistujat tai (l)opeta: ");
                        }
                        break;

                    case 't':  // tulosta tulokset pistejärjestyksessä
                        if (tulokset == null)
                        {
                            //Console.WriteLine("Ei vielä tuloksia!");
                        }
                        else
                        {

                            tulosta(tulokset, 2);
                            Console.Write("(u)usi, (j)atka, (h)eittojärjestys, (t)ulokset, (o)sallistujat tai (l)opeta: ");
                        }
                        break;

                    case 'l':  // lopeta
                        tallennaTiedostoon(kilpailunNimi, tulokset);
                        return;

                    default:
                        //Console.WriteLine("Komentoa ei tunnistettu.");
                        break;
                }
            }
        }

        private static void JatkaPelia()
        {
            int heitot = int.Parse(tulokset[0, 3]); // tarkista millä kieroksella mennään
            for (int i = heitot; i < 3; i++)  // loopataan kierrosten läpi rajoittaen peli kolmeen kierrokseen
            {
                for (int pelaaja = 0; pelaaja < tulokset.GetLength(0); pelaaja++) // käydään jokaisen pelaajan vuoro läpi
                {
                    Console.Write("Heittovuoro {0} pelaaja {1} - {2} pistemäärä: ", (i + 1), (pelaaja + 1), (tulokset[pelaaja, 1]));
                    string piste = Console.ReadLine();

                    if (piste == "")  // keskeytä peli
                    {
                        return;
                    }
                    else
                    {
                        if (pelaaja % 2 != 0)
                        {
                            if (int.Parse(piste) > int.Parse(tulokset[1, 2]))
                            {
                                tulokset[1, 2] = piste;
                                tulokset[1, 3] = (int.Parse(tulokset[1, 3]) + 1).ToString();
                            }
                        }
                        else
                        {
                            if (int.Parse(piste) > int.Parse(tulokset[0, 2]))
                            {
                                tulokset[0, 2] = piste;
                                tulokset[0, 3] = (int.Parse(tulokset[0, 3]) + 1).ToString();
                            }
                        }
                    }
                }

            }
        }

        private static bool PeliKesken()
        {
            // Onko ensimmäisellä heittovuorolla olevalla pelaajalla heittoja?
            if (int.Parse(tulokset[0,3]) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void PelaaPelia()
        {
            int heitot = int.Parse(tulokset[0, 3]); // tarkista millä kieroksella mennään
            for (int i = heitot; i < 3; i++)  // loopataan kierrosten läpi rajoittaen peli kolmeen kierrokseen
            {
                for (int pelaaja = 0; pelaaja < tulokset.GetLength(0); pelaaja++) // käydään jokaisen pelaajan vuoro läpi
                {
                    //Console.Write("Heittovuoro {0} pelaaja {1} - {2} pistemäärä: ", (i + 1), (pelaaja + 1), (tulokset[pelaaja, 1]));
                    string piste = Console.ReadLine();

                    if (piste == "")  // keskeytä peli
                    {
                        return;
                    }
                    else
                    {
                        if (pelaaja % 2 != 0)
                        {
                            if (int.Parse(piste) > int.Parse(tulokset[1, 2]))
                            {
                                tulokset[1, 2] = piste;
                                tulokset[1, 3] = (int.Parse(tulokset[1, 3]) +1).ToString();
                            }
                        }
                        else
                        {
                            if (int.Parse(piste) > int.Parse(tulokset[0, 2]))
                            {
                                tulokset[0, 2] = piste;
                                tulokset[0, 3] = (int.Parse(tulokset[0, 3]) +1).ToString();
                            }
                        }
                    }
                }

            }
        }

        private static void LuoTaulukko(List<string> osallistujat)
        {
            // Luodaan taulukko ilmoittautuneiden pelaajien perusteella
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

            tulokset = tulosTaulukko;
            tallennaTiedostoon(kilpailunNimi, tulokset);
        }

        private static void KysyOsallistujat()
        {
            string kilpailijanNimi;

            for (int i = 0; i < 10; i++)
            {
                //Console.Write("Anna henkilön nimi: ");
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
            //Console.WriteLine("");
        }

        private static void KysyKilpailunNimi()
        {
            string nimi = Console.ReadLine();
            kilpailunNimi = nimi + ".txt";
        }

        private static void KysyKomento()
        {
            string input = "";
            //Console.WriteLine("(u)usi, (j)atka, (h)eittojärjestys, (t)ulokset, (o)sallistujat tai (l)opeta: ");
            while (input == "")
            {
                input = (Console.ReadLine());
            }
            komento = input[0];
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
                    for (int rowIndex = 0; rowIndex < tulokset.GetLength(0); rowIndex++)
                    {
                        sarakeSisalto.Add(tulokset[rowIndex, per]);
                    }
                    sarakeSisalto.Sort();

                    for (int rowIndex = 0; rowIndex < tulokset.GetLength(0); rowIndex++)
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
            return lajittelutaulukko;
        }

        public static void tulosta(string[,] tulokset, int jar)
        {
            string[,] tuloksetLajiteltu;
            // tulostus halutussa järjestyksessä
            switch (jar)
            {
                case 0:  // heittojärjestys (tulokset[,0])
                    //Console.WriteLine("Tikkakilpailu - Heittojärjestys");
                    Console.WriteLine("Heittojärjestys Nimi                Tulos     Heitot ");
                    tuloksetLajiteltu = lajittele(tulokset, 0);
                    for (int rivi = 0; rivi < tulokset.GetLength(0); rivi++)
                    {
                        Console.WriteLine("{0}               {1}               {2}         {3}",
                            tuloksetLajiteltu[rivi,0], tuloksetLajiteltu[rivi,1],
                            tuloksetLajiteltu[rivi,2], tuloksetLajiteltu[rivi,3]);
                    }

                    break;

                case 1:  // aakkosjärjestys (tulokset[,1])
                    Console.WriteLine("Tikkakilpailu - Osallistujat");
                    Console.WriteLine("Nimi                Heittojärjestys Tulos     Heitot ");
                    tuloksetLajiteltu = lajittele(tulokset, 1);
                    for (int rivi = 0; rivi < tulokset.GetLength(0); rivi++)
                    {
                        Console.WriteLine("{0}              {1}               {2}        {3} ",
                            tuloksetLajiteltu[rivi, 1], tuloksetLajiteltu[rivi, 0],
                            tuloksetLajiteltu[rivi, 2], tuloksetLajiteltu[rivi, 3]);
                    }
                    break;

                default:  // tulosjärjestys oletuksena (tulokset[,2])
                    //Console.WriteLine("Tikkakilpailu - Tulokset");
                    Console.WriteLine("Tulos     Nimi                Heittojärjestys Heitot ");
                    tuloksetLajiteltu = lajittele(tulokset, 2);
                    for (int rivi = 0; rivi < tulokset.GetLength(0); rivi++)
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
            return tulokset;
        }
    }
}
