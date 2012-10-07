using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    public class KaartSpel : ICloneable, IEnumerable<Kaart>
    {
        public delegate void bijLaatsteKaartGetrokkenVanSoortEventHandler(object sender, KaartEventArgs e);
        public event bijLaatsteKaartGetrokkenVanSoortEventHandler laatsteKaart;

        private Dictionary<Types, int> aantalKaartenVanType;
        public List<Kaart> Kaarten { get; private set; }
        public int AantalKaarten { get; private set; }

        protected void LaatsteKaartGetrokken(KaartEventArgs e) {
            if (laatsteKaart != null) { 
                //Er zijn methodes op deze event geregistreert.
                laatsteKaart(this, e);
            }
        }

        public Kaart this[int i]
        {
            get
            {
                return Kaarten[i];
            }

        }

        public Kaart this[Types t, Waardes w]
        {
            get
            {
                bool b = false;
                int goedeKaart = -1;
                for (int i = 0; i < AantalKaarten && !b; i++)
                {
                    if (Kaarten[i].Type == t && Kaarten[i].Waarde == w)
                    {
                        b = true;
                        goedeKaart = i;
                    }
                }
                if (goedeKaart > -1) { return Kaarten[goedeKaart]; }
                else { return null; }
            }
        }

        public KaartSpel()
        {
            //Maak een gewoon Kaartenspel aan.
            aantalKaartenVanType = new Dictionary<Types, int>();
            int aantalWaardes = Enum.GetValues(typeof(Waardes)).Length; //Anders berekent die het 4 (of meer) keer in de lus wat overbodig is.
            Kaarten = new List<Kaart>(52);
            foreach (Types t in Enum.GetValues(typeof(Types)))
            {
                foreach (Waardes w in Enum.GetValues(typeof(Waardes)))
                {
                    Kaarten.Add(new Kaart(t, w, false));
                }
                aantalKaartenVanType.Add(t, aantalWaardes);

            }
            AantalKaarten = 52;
        }

        public KaartSpel(IEnumerable<Kaart> kn) 
        {
            //Zet kaarten over naar een List.
            Kaarten = new List<Kaart>(kn.Count() + 5);
            AantalKaarten = kn.Count();
            foreach (Kaart k in kn)
            {
                Kaarten.Add(k);
                aantalKaartenVanType[k.Type]++;
            }
        }

        public void VoegKaartToe(Kaart k) //Voeg Exceptions toe!
        {
            Kaarten.Add(k);
            aantalKaartenVanType[k.Type]++;
            AantalKaarten++;
        }

        public void VoegKaartenToe(IEnumerable<Kaart> kn) //Voeg Exceptions toe!
        {
            foreach (Kaart k in kn)
            {
                Kaarten.Add(k);
                aantalKaartenVanType[k.Type]++;
            }
            AantalKaarten += kn.Count();
        }

        public Kaart TrekRandomKaart()
        {
            Random r = new Random();
            //TODO: Checken of er wel kaarten zijn :d
            int g = r.Next(0, Kaarten.Count());
            Kaart k = Kaarten.ElementAt(g);
            Kaarten.RemoveAt(g);
            aantalKaartenVanType[k.Type]--;
            kijkOfLaatsteKaart(k);
            return k;
        }

        public void kijkOfLaatsteKaart(Kaart kaart)
        {
            Types type = (Types) 123; //Gewoon een niet valid enum type opgeven zodat ie straks faalt
            foreach (Types t in aantalKaartenVanType.Keys) {
                if (aantalKaartenVanType[t] == 0) {
                    KaartEventArgs k = new KaartEventArgs(kaart);
                    LaatsteKaartGetrokken(k);
                    type = t;
                }
            }
            if (Enum.IsDefined(typeof(Types), type)) //Hierzo, als er geen enkele soort op is.
            {
                aantalKaartenVanType[type]--;
            }
            
        }

        public Kaart TrekBovensteKaart()
        {
            Kaart k = Kaarten.ElementAt(0);
            Kaarten.RemoveAt(0);
            aantalKaartenVanType[k.Type]--;
            kijkOfLaatsteKaart(k);
            return k;
        }

        public Kaart TrekKaart(int index)
        {
            if (index >= 0 && index < Kaarten.Count())
            {
                Kaart k = Kaarten.ElementAt(index);
                Kaarten.RemoveAt(index);
                aantalKaartenVanType[k.Type]--;
                kijkOfLaatsteKaart(k);
                return k;
            }
            else { return null; }

        }

        public void SchudKaartSpel()
        {
            int c = Kaarten.Count();
            //Random.Next(x,y) is [x, y[
            Random r = new Random();
            List<Kaart> tmp = new List<Kaart>(c);
            while (c > 0)
            {
                int j = r.Next(0, c);
                tmp.Add(Kaarten.ElementAt(j));
                Kaarten.RemoveAt(j);
                c--;
            }
            Kaarten = tmp;
        }


        public object Clone()
        {
            List<Kaart> copyList = new List<Kaart>(AantalKaarten);
            foreach (Kaart k in Kaarten)
            {
                copyList.Add((Kaart)k.Clone());
            }

            return new KaartSpel(copyList);
        }

        public void Sort(IComparer<Kaart> comp)
        {
            if (comp == null)
            {
                Kaarten.Sort();
            }
            else
            {
                Kaarten.Sort(comp);
            }

        }

        public IEnumerator<Kaart> GetEnumerator() //Want we willen ook gewoon foreach(Kaart k in kaartspel) {}
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
