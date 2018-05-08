using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wenskaart.Model
{
    public class Kleur
    {
        public SolidColorBrush Borstel { get; set; }
        public string KleurNaam { get; set; }
        public Kleur(SolidColorBrush borstel, string kleur)
        {
            Borstel = borstel;
            KleurNaam = kleur;
        }
    }
}
