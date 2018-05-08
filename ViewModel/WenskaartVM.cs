using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wenskaart.View;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Controls;
using System.Reflection;



namespace Wenskaart.ViewModel
{
    public class WenskaartVM : ViewModelBase
    {
        private Model.Wenskaart wenskaart;
        public WenskaartVM(Model.Wenskaart dewenskaart)
        {
            wenskaart = dewenskaart;
        }
        #region Properties
        public BitmapImage Achtergrond
        {
            get
            {
                return wenskaart.Achtergrond;
            }
            set
            {
                wenskaart.Achtergrond = value;
                RaisePropertyChanged("Achtergrond");
            }
        }
        public string Wens
        {
            get
            {
                return wenskaart.Wens;
            }
            set
            {
                wenskaart.Wens = value;
                RaisePropertyChanged("Wens");
            }
        }
        public FontFamily Lettertype
        {
            get
            {
                return wenskaart.Lettertype;
            }
            set
            {
                wenskaart.Lettertype = value;
                RaisePropertyChanged("Lettertype");
            }
        }
        public int LetterGrootte
        {
            get
            {
                return wenskaart.LetterGrootte;
            }
            set
            {
                wenskaart.LetterGrootte = value;
                RaisePropertyChanged("LetterGrootte");
            }
        }
        public string Status
        {
            get
            {
                return wenskaart.Status;
            }
            set
            {
                wenskaart.Status = value;
                RaisePropertyChanged("Status");
            }
        }
        public Boolean RechterStack
        {
            get
            { return wenskaart.RechterStack; }
            set
            {
                wenskaart.RechterStack = value;
                RaisePropertyChanged("RechterStack");
            }
        }

        private ObservableCollection<KleurVM> kleurlijst = new ObservableCollection<KleurVM>();
        public ObservableCollection<KleurVM> Kleurlijst
        {
            get
            {
                return kleurlijst;
            }
            set
            {
                kleurlijst = value;
                RaisePropertyChanged("Kleurlijst");
            }
        }
        private ObservableCollection<BalVM> ballen = new ObservableCollection<BalVM>();
        public ObservableCollection<BalVM> Ballen
        {
            get
            {
                return ballen;
            }
            set
            {
                ballen = value;
                RaisePropertyChanged("Ballen");
            }
        }

        #endregion
        #region RelayCommands

        public RelayCommand KerstCommand
        { get { return new RelayCommand(KerstKaart); } }
        private void KerstKaart()
        {
            Reset();
            Achtergrond = new BitmapImage(new Uri("images/kerstkaart.jpg", UriKind.Relative));
            RechterStack = true;
        }
        public RelayCommand GeboorteCommand
        { get { return new RelayCommand(geboorte); } }
        private void geboorte()
        {
            Reset();
            Achtergrond = new BitmapImage(new Uri("images/geboortekaart.jpg", UriKind.Relative));
            RechterStack = true;
        }
        public RelayCommand NieuwCommand
        { get { return new RelayCommand(Reset); } }


        public RelayCommand SaveCommand { get { return new RelayCommand(Opslaan); } }
        private void Opslaan()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "Wenskaartje";
                dlg.Filter = "Wenskaarten |*.Test";
                dlg.DefaultExt = ".Test";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Achtergrond.UriSource);
                        bestand.WriteLine(Ballen.Count());
                        foreach (BalVM item in ballen)
                        {
                            bestand.WriteLine(item.BalKleur.ToString());
                            bestand.WriteLine(item.xPos.ToString());
                            bestand.WriteLine(item.yPos.ToString());
                        }
                        bestand.WriteLine(Wens);
                        bestand.WriteLine(Lettertype);
                        bestand.WriteLine(LetterGrootte);
                    }
                }
                Status = dlg.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er liep iets fout..." + ex.Message);
            }
        }
        public RelayCommand OpenCommand
        { get { return new RelayCommand(Openen); } }
        private void Openen()
        {

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.Filter = "Wenskaarten |*.Test";
                dlg.DefaultExt = ".Test";
                if (dlg.ShowDialog() == true)
                {
                    Reset();
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        BitmapImage Bi = new BitmapImage();
                        Bi = new BitmapImage(new Uri(bestand.ReadLine(), UriKind.Relative));
                        Achtergrond = Bi;
                        int aantalballen = int.Parse(bestand.ReadLine());
                        if (aantalballen != 0)
                        {
                            for (int i = 0; i < aantalballen; i++)
                            {
                                BrushConverter bc = new BrushConverter();
                                Brush kleur = (Brush)bc.ConvertFromString(bestand.ReadLine());
                                Model.Bal bal = new Model.Bal();
                                bal.BalKleur = kleur;
                                bal.xPos = double.Parse(bestand.ReadLine());
                                bal.yPos = double.Parse(bestand.ReadLine());
                                ballen.Add(new BalVM(bal));
                            }

                        }
                        Wens = bestand.ReadLine();
                        Lettertype = new FontFamily(bestand.ReadLine());
                        LetterGrootte = int.Parse(bestand.ReadLine());
                    }
                    Status = dlg.FileName;
                    RechterStack = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er liep iets fout" + ex.Message);
            }
        }
        public RelayCommand LoadedCommand { get { return new RelayCommand(Loaded); } }
        private void Loaded()
        {
            Reset();
            LaadKleuren();
        }
        private void LaadKleuren()
        {
            foreach (PropertyInfo info in typeof(Colors).GetProperties())
            {
                BrushConverter bc = new BrushConverter();
                SolidColorBrush deKleur = (SolidColorBrush)bc.ConvertFromString(info.Name);
                Model.Kleur kleur = new Model.Kleur(deKleur, info.Name);
                KleurVM kleurVM = new KleurVM(kleur);
                Kleurlijst.Add(kleurVM);
            }
        }
        private void Reset()
        {
            RechterStack = false;
            Status = "Nieuw";
            Achtergrond = null;
            LetterGrootte = 15;
            Wens = "Typ hier uw wens";
            Lettertype = new FontFamily("Arial");
            LaadKleuren();
            Ballen.Clear();
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
        public RelayCommand MeerCommand { get { return new RelayCommand(Meer); } }
        private void Meer()
        {
            if (LetterGrootte < 40) LetterGrootte += 1;
        }
        public RelayCommand MinderCommand { get { return new RelayCommand(Minder); } }
        private void Minder()
        {
            if (LetterGrootte > 10) LetterGrootte -= 1;
        }
        public RelayCommand AfdrukvoorbeeldCommand
        {
            get { return new RelayCommand(Afdrukvoorbeeld); }
        }
        private void Afdrukvoorbeeld()
        {
            MessageBox.Show("Afdrukvoorbeeld", "Wilt u afdrukken?", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
        }
        public RelayCommand<MouseEventArgs> MouseMoveCommand { get { return new RelayCommand<MouseEventArgs>(MouseMove); } }
        private void MouseMove(MouseEventArgs e)
        {
            Ellipse bal = new Ellipse();
            bal = (Ellipse)e.Source;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject Slepen = new DataObject("deBal", bal.Fill);
                try
                {
                    DragDrop.DoDragDrop(bal, Slepen, DragDropEffects.Move);
                }
                catch (Exception ex) { MessageBox.Show("Er liep iets fout..." + ex.Message); }
            }
        }
        public RelayCommand<DragEventArgs> DropCommand
        {
            get { return new RelayCommand<DragEventArgs>(Drop); }
        }
        private void Drop(DragEventArgs e)
        {
            Canvas canvas = new Canvas();
            canvas = (Canvas)e.Source;
            Model.Bal bal = new Model.Bal();
            bal.BalKleur = (Brush)e.Data.GetData("deBal");
            bal.xPos = e.GetPosition(canvas).X - 20;
            bal.yPos = e.GetPosition(canvas).Y - 20;
            BalVM balVM = new BalVM(bal);
            ballen.Add(balVM);
        }
        #endregion






















    }
}