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
    /// Interaction logic for LocatieToevoegen.xaml
    /// </summary>
    public partial class LocatieToevoegen : Window
    {
        public LocatieToevoegen()
        {
            InitializeComponent();
        }
        //Stijn Beckers - r0795483
        Locatie locatie = new Locatie();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Eventgegevens.EventId = 1; //hard gecodeerd
            Event @event = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);
            lblNaamEvenement.Content = $"{@event.Eventnaam}";//Naam van event inladen in label links bovenaan
            btnTerugNaarLocatieSelecteren.Content = "< " + @event.Eventnaam;
        }


        private void BtnTerugNaarLocatieSelectie_Click(object sender, RoutedEventArgs e)
        {
            LocatieSelectie locatieSelectie = new LocatieSelectie();
            locatieSelectie.Show();
            this.Close();
        }

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("txtNummer");
            foutmeldingen += Valideer("txtPostcode");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {//een locatie toevoegen            
                locatie.Naam = txtNaamLocatie.Text;
                locatie.Straat = txtStraat.Text;
                locatie.Huisnr = int.Parse(txtNummer.Text);
                locatie.Postcode = int.Parse(txtPostcode.Text);
                locatie.Gemeente = txtGemeente.Text;
                locatie.Manager = txtManager.Text;
                locatie.Email = txtEmail.Text;
                locatie.Telefoon = txtTelefoon.Text;
                //locatie toevoegen als ze geldig is
                if (locatie.IsGeldig())
                {
                    int ok = DatabaseOperations.ToevoegenLocatie(locatie);
                    if (ok <= 0)
                    {
                        MessageBox.Show("Het toevoegen van de locatie is niet gelukt!");
                    }
                    else
                    {
                        MessageBox.Show("Het toevoegen van de locatie is gelukt!");
                        LocatieSelectie locatieSelectie = new LocatieSelectie();
                        locatieSelectie.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(locatie.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        private string Valideer(string veldnaam)
        {
            if (veldnaam == "txtNummer" && !int.TryParse(txtNummer.Text, out int huisnr))
            {
                return "Geef een huisnummer in !" + Environment.NewLine;
            }
            if (veldnaam == "txtPostcode" && !int.TryParse(txtPostcode.Text, out int postcode))
            {
                return "Geef een postcode in !" + Environment.NewLine;
            }
            return "";
        }
    }
}
