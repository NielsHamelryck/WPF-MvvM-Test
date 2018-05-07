using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Wenskaart.ViewModel
{

    public class WenskaartVM : ViewModelBase
    {
        private Model.Wenskaart wenskaart;
        public WenskaartVM(Model.Wenskaart dewenskaart)
        {
            wenskaart = dewenskaart;
        }
        public BitmapImage KaartImage
        {
            get
            {
                return wenskaart.Kaart;
            }
            set
            {
                wenskaart.Kaart = value;
                RaisePropertyChanged("Kaart");
            }
        }
        public RelayCommand KerstCommand { get { return new RelayCommand(Kerst); } }
        private void Kerst()
        {
            KaartImage = new BitmapImage(new Uri(@"D:\kerstkaart.jpg"));
        }














        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(NieuweBon); }
        }
        private void NieuweBon()
        {
            //Define new variables
        }
























        public RelayCommand CloseCommand
        {
            get { return new RelayCommand(CloseKaart); }
        }
        private void CloseKaart()
        {
            Application.Current.MainWindow.Close();
        }
        public RelayCommand<CancelEventArgs> AfsluitenEvent
        {
            get { return new RelayCommand<CancelEventArgs>(Afsluiten); }
        }
        private void Afsluiten(CancelEventArgs e)
        {
            if (MessageBox.Show("Afsluiten", "Wilt u afsluiten?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) e.Cancel = true;
        }
    }
}