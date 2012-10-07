using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    public class KaartComparer : IComparer<Kaart>
    {
        private Types[] rij;
        public KaartComparer(Types[] volgordeTypes)
        {
            rij = volgordeTypes;
        }

        private int PlaatsInArray(Types t)
        {
            int place = -1;
            bool found = false;
            for (int i = 0; i < rij.Length && !found; i++) {
                if (t == rij[i]) {
                    place = i;
                }
            }
            return place;
        }

        public int Compare(Kaart x, Kaart y)
        {
            int plaatsX = PlaatsInArray(x.Type);
            int plaatsY = PlaatsInArray(y.Type);
            if (plaatsX > plaatsY && plaatsY >= 0) { return 1; }
            else if (plaatsX > plaatsY && plaatsY < 0) { return -1; }
            else if (plaatsX < plaatsY && plaatsX >= 0) { return -1; }
            else if (plaatsX < plaatsY && plaatsX < 0) { return 1; }
            else
            {
                //Types zijn gelijk, dus bvb 2 Harten -> Kijken naar de waarde van kaart.
                if ((int)x.Waarde > (int)y.Waarde) { return 1; }
                else if ((int)x.Waarde < (int)y.Waarde) { return -1; }
                return 0;
            }
        }
    }
}
