using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Wenskaart.ViewModel
{
    public class KleurVM: ViewModelBase
    {
        private Model.Kleur Kleur;
        public KleurVM(Model.Kleur kleur)
        {
            Kleur = kleur;
        }
        public SolidColorBrush Borstel
        {
            get
            {
                return Kleur.Borstel;
            }
            set
            {
                Kleur.Borstel = value;
                RaisePropertyChanged("Borstel");
            }
        }
        public string Kleurnaam
        {
            get
            {
                return Kleur.KleurNaam;
            }
            set
            {
                Kleur.KleurNaam = value;
                RaisePropertyChanged("Kleurnaam");
            }
        }
    }
}
