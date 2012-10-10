using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    class KaartEnumerator : IEnumerator<Kaart>
    {
        private readonly KaartSpel spel;
        private int i = -1;

        public KaartEnumerator(KaartSpel ks) {
            spel = ks;
        }

        public Kaart Current
        {
            get { return spel.Kaarten[i]; }
        }

        public void Dispose()
        {
            //Whatevah
        }

        public bool MoveNext()
        {
            i++;
            if (i >= spel.Kaarten.Count) {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            i = -1;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
