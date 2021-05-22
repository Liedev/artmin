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
    /// Interaction logic for LocatieSelectie.xaml
    /// </summary>
    public partial class LocatieSelectie : Window
    {
        public LocatieSelectie()
        {
            InitializeComponent();
        }
        //Stijn Beckers - r0795483
        Locatie locatie = new Locatie();
        Event eventnaam = new Event();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Locatie> locaties = DatabaseOperations.OphalenLocaties();
            cmbLocatie.ItemsSource = locaties;
            Eventgegevens.EventId = 1;
            eventnaam = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);
            if (eventnaam != null)
            {
                lblNaamEvenement.Content = $"{eventnaam.Eventnaam}";
                btnNaarEventOverzicht.Content = "< " + eventnaam.Eventnaam;             
                if (eventnaam.Locatie != null)
                {
                    Locatie locatie = DatabaseOperations.OphalenLocatieViaId(eventnaam.LocatieID.Value);
                    if (locatie != null)
                    {
                        txtNaamLocatie.Text = locatie.Naam;
                        txtStraatNummer.Text = locatie.Straat + " " + locatie.Huisnr;
                        txtManager.Text = locatie.Manager;
                        txtEmail.Text = locatie.Email;
                        txtPostcodeGemeente.Text = locatie.Postcode + " " + locatie.Gemeente;
                        txtTelefoon.Text = locatie.Telefoon;
                        cmbLocatie.SelectedItem = locatie;
                    }                    
                }
                else
                {
                    return;
                }

            }
            else
            {
                MessageBox.Show("Kon uw event niet vinden");
            }
            
        }

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (cmbLocatie.SelectedItem is Locatie selectedLocatie)
            {
                eventnaam.LocatieID = null;
                eventnaam.Locatie= null;

                int ok = DatabaseOperations.UpdateEvent(eventnaam);
                if (ok > 0)
                {
                    MessageBox.Show("De locatie van het evenement is verwijderd! ");
                    
                }
                else
                {
                    MessageBox.Show("De locatie van het evenement is niet verwijderd! ");
                }
            }
            else
            {
                MessageBox.Show("Het geselecteerde item is geen locatie");
            }
        }

        private void BtnNotitieToevoegen_Click(object sender, RoutedEventArgs e)
        {
            LocatieToevoegen locatieToevoegen = new LocatieToevoegen();
            locatieToevoegen.Show();
            this.Close();
        }

        private void CmbLocatie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLocatie.SelectedItem is Locatie locatie)
            {
                txtNaamLocatie.Text = locatie.Naam;
                txtStraatNummer.Text = locatie.Straat + " " + locatie.Huisnr;
                txtManager.Text = locatie.Manager;
                txtEmail.Text = locatie.Email;
                txtPostcodeGemeente.Text = locatie.Postcode + " " + locatie.Gemeente;
                txtTelefoon.Text = locatie.Telefoon;
            }
            else
            {
                MessageBox.Show("Selecteer eerst een locatie");
            }
        }

        private void BtnOplslaan_Click(object sender, RoutedEventArgs e)
        {
            // locatie toevoegen aan het event - update

            
                if (cmbLocatie.SelectedItem is Locatie selectedLocatie)
                {
                    eventnaam.LocatieID = selectedLocatie.LocatieID;
                    eventnaam.Locatie = selectedLocatie;

                    int ok = DatabaseOperations.UpdateEvent(eventnaam);
                    if (ok > 0)
                    {
                        MessageBox.Show("De locatie van het evenement is aangepast ! ");
                        EventDetail eventDetail = new EventDetail();
                        eventDetail.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("De locatie van het evenement is niet aangepast ! ");
                    }
                }
                else
                {
                    MessageBox.Show("Het geselecteerde item is geen locatie");
                }       
        }


        private void BtnNaarEventOverzicht_Click(object sender, RoutedEventArgs e)
        {
            EventDetail eventDetail = new EventDetail();
            eventDetail.Show();
            this.Close();
        }
    }
}
