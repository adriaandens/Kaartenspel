using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    //Indien je public weglaat is ie alleen vindbaar in hetzelfde project/namespace
    public class Kaart : IComparable<Kaart>, ICloneable
    {
        public Types Type { get; set; }
        public Waardes Waarde { get; set; }
        public bool KaartZichtbaar { get; set; }

        public Kaart(Types t, Waardes w, bool z)
        {
            Type = t;
            Waarde = w;
            KaartZichtbaar = z;
        }

        public int CompareTo(Kaart k)
        {
            if ((int)k.Type > (int)Type) { return -1; }
            else if ((int)k.Type < (int)Type) { return 1; }
            else
            {
                //Types zijn gelijk, dus bvb 2 Harten -> Kijken naar de waarde van kaart.
                if ((int)k.Waarde > (int)Waarde) { return -1; }
                else if ((int)k.Waarde < (int)Waarde) { return 1; }
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Kaart k = (Kaart)obj;
                return Type == k.Type && Waarde == k.Waarde && KaartZichtbaar == k.KaartZichtbaar;
            }
        }

        public override int GetHashCode()
        {
            return ((int)Type * 3147547) * ((int)Waarde * 47137);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2})", Type, Waarde, KaartZichtbaar);
        }

        public object Clone()
        {
            return new Kaart(this.Type, this.Waarde, this.KaartZichtbaar);
        }
    }
}
