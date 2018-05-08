using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace Wenskaart.Model
{
   public class Wenskaart
    {
        public BitmapImage Achtergrond { get; set; }
        public string Wens { get; set; }
        public FontFamily Lettertype { get; set; }
        public int LetterGrootte { get; set; }
        public String Status { get; set; }
        public Boolean RechterStack { get; set; }
    }
}
