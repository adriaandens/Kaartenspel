using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    public class KaartEventArgs: EventArgs
    {
        public Kaart LaatsteKaart { get; private set; }

        public KaartEventArgs(Kaart laatsteKaart) {
            LaatsteKaart = laatsteKaart;
        }
    }
}
