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
using Artmin_DAL;

namespace Artmin
{
    /// <summary>
    /// Interaction logic for NotitieOverzicht.xaml
    /// </summary>
    public partial class NotitieOverzicht : Window
    { //Scherm gemaakt door: Jarno Peeters - R0670336
        public NotitieOverzicht()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Event eventNaam = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);
            lblNaamEvenement.Content = $"{eventNaam.Eventnaam}";//Naam van event inladen
            List<Notitie> notities = DatabaseOperations.OphalenNotitiesViaEventId(Eventgegevens.EventId);
            datagridNotitieEvenement.ItemsSource = notities;
        }
        private void btnAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (datagridNotitieEvenement.SelectedItem is Notitie notitie)
            {
                NotitieGegevens.NotitieId = notitie.NotitieID;
                NotitieBewerken notitieBewerken = new NotitieBewerken(notitie.NotitieID);
                notitieBewerken.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een notitie");
            }
        }
        private string Valideer(string columnName)
        {
            if (columnName == "datagridNotitieEvenement" && datagridNotitieEvenement.SelectedItem == null)
            {
                return "Selecteer een notitie!" + Environment.NewLine;
            }
            return "";
        }
        private void btnTerugNaarVorigScherm_Click(object sender, RoutedEventArgs e)
        {
            EventDetail eventDetail = new EventDetail();
            eventDetail.Show();
            this.Close();
        }
        private void btnNotitieToevoegen_Click_1(object sender, RoutedEventArgs e)
        {
            NotitieGegevens.NotitieId = 0;

            NotitieBewerken notitieBewerken = new NotitieBewerken();
            notitieBewerken.Show();
            this.Close();
        }
        private void btnVerwijderen_Click_1(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("datagridNotitieEvenement");
            if (string.IsNullOrEmpty(foutmelding))
            {
                if (datagridNotitieEvenement.SelectedItem is Notitie notitie)
                {
                    int ok = DatabaseOperations.VerwijderenNotitie(notitie);
                    if (ok <= 0)
                    {
                        MessageBox.Show("Notitie is niet verwijderd");
                    }
                    else
                    {
                        datagridNotitieEvenement.ItemsSource = DatabaseOperations.OphalenNotitiesViaEventId(notitie.EventID);
                        MessageBox.Show("Uw notitie is verwijderd");
                    }
                }
            }
            else
            {
                MessageBox.Show(foutmelding);
            }
        }
    }
}
