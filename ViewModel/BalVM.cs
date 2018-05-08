using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Media;

namespace Wenskaart.ViewModel
{
    public class BalVM : ViewModelBase
    {
        private Model.Bal Bal;
        public BalVM(Model.Bal bal)
        {
            Bal = bal;
        }
        public Brush BalKleur
        {
            get
            {
                return Bal.BalKleur;
            }
            set
            {
                Bal.BalKleur = value;
                RaisePropertyChanged("BalKleur");
            }
        }
        public double xPos
        {
            get
            {
                return Bal.xPos;
            }
            set
            {
                Bal.xPos = value;
                RaisePropertyChanged("xPos");
            }
        }
        public double yPos
        {
            get
            {
                return Bal.yPos;
            }
            set
            {
                Bal.yPos = value;
                RaisePropertyChanged("yPos");
            }
        }
    }
}
