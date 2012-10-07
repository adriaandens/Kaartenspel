using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Import
using KaartenLib;

namespace KaartConsoleApp
{
    class Program
    {
        private KaartSpel spelleke;

        public Program(KaartSpel ks)
        {
            spelleke = ks;
            spelleke.laatsteKaart += new KaartSpel.bijLaatsteKaartGetrokkenVanSoortEventHandler(LaatsteKaartGetrokken);
        }

        public void LaatsteKaartGetrokken(object sender, KaartEventArgs e)
        {
            Console.WriteLine("o hai, laatste kaart getrokken: {0}", e.LaatsteKaart.ToString());
        }



        static void Main(string[] args)
        {
            Kaart k1 = new Kaart(Types.Harten, Waardes.Aas, false);
            Console.WriteLine("Waardes manueel opvragen: {0} {1} ({2})", k1.Type, k1.Waarde, k1.KaartZichtbaar);
            Kaart k2 = new Kaart(Types.Harten, Waardes.Dame, false);
            Kaart k3 = new Kaart(Types.Schoppen, Waardes.Drie, false);
            Kaart k4 = new Kaart(Types.Klaveren, Waardes.Vijf, false);
            Kaart[] r = { k3, k2, k1, k4 };
            Console.WriteLine("Origineel");
            foreach (Kaart k in r)
            {
                Console.WriteLine(k.ToString());
            }

            Array.Sort(r);
            Console.WriteLine("Na sorteren (Harten, Ruiten, Schoppen en Klaveren)");
            foreach (Kaart k in r)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Met Comparer (Schoppen, Ruiten, Harten en Klaveren)");
            Types[] t = { Types.Schoppen, Types.Ruiten, Types.Harten, Types.Klaveren };
            KaartComparer c = new KaartComparer(t);
            Array.Sort(r, c);
            foreach (Kaart k in r)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Met Comparer (Ruiten, Schoppen, Klaveren)");
            Types[] z = { Types.Ruiten, Types.Schoppen, Types.Klaveren };
            KaartComparer e = new KaartComparer(z);
            Array.Sort(r, e);
            foreach (Kaart k in r)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Kaartspel");
            KaartSpel spel = new KaartSpel();
            foreach (Kaart k in spel.Kaarten)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Kaartspel geeft de vierde terug...");
            Console.WriteLine(spel[3]);
            Console.WriteLine("Kaartspel voeg Schoppen Vier toe");
            spel.VoegKaartToe(new Kaart(Types.Schoppen, Waardes.Vier, false));
            foreach (Kaart k in spel.Kaarten)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Kaartspel sorteren...");
            spel.Sort(null);
            foreach (Kaart k in spel.Kaarten)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Kaartspel sorteren met icomparer");
            Types[] p = { Types.Klaveren, Types.Ruiten, Types.Harten, Types.Schoppen };
            spel.Sort(new KaartComparer(p));
            foreach (Kaart k in spel.Kaarten)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine("Een event laten afvuren............................................");
            Program az = new Program(spel);
            for (int i = 0; i < 15; i++)
            {
                spel.TrekBovensteKaart();
            }
            foreach (Kaart k in spel.Kaarten)
            {
                Console.WriteLine(k.ToString());
            }
            Console.ReadKey();
        }
    }
}
