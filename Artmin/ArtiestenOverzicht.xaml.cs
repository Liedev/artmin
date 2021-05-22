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
using System.Windows.Shapes;

namespace Artmin
{
    /// <summary>
    /// Interaction logic for ArtiestenOverzicht.xaml
    /// </summary>
    public partial class ArtiestenOverzicht : Window // Lieven
    {
        public ArtiestenOverzicht()
        {
            InitializeComponent();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            EventDetail eventDetail = new EventDetail();
            eventDetail.Show();
            this.Close();
        }
    }
}
