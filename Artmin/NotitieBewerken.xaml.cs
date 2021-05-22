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
    /// Interaction logic for NotitieBewerken.xaml
    /// </summary>
    public partial class NotitieBewerken : Window
    {     //Scherm gemaakt door: Jarno Peeters - R0670336
        Notitie notitie = new Notitie();
        public NotitieBewerken()
        {
            InitializeComponent();
        }
        public NotitieBewerken(int notitieId)
        {
            InitializeComponent();
            notitie = DatabaseOperations.OphalenNotitieViaId(NotitieGegevens.NotitieId);
            txtNotitieNaamAanpassen.Text = notitie.Titel;
            txtNotitieOmschrijvingAanpassen.Text = notitie.Omschrijving;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Event eventNaam = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);
            lblNaamEvenement.Content = $"{eventNaam.Eventnaam}";//Naam van event inladen
        }
        private void btnTerugNaarVorigScherm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Uw notitie is niet aangepast!");
            NotitieOverzicht notitieOverzicht = new NotitieOverzicht();
            notitieOverzicht.Show();
            this.Close();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (NotitieGegevens.NotitieId == 0)
            { //Notitie toevoegen
                string foutmelding = Valideer("Titel" + "Omschrijving");
                if (string.IsNullOrWhiteSpace(foutmelding))
                {
                    notitie.Titel = txtNotitieNaamAanpassen.Text;
                    notitie.Omschrijving = txtNotitieOmschrijvingAanpassen.Text;
                    notitie.EventID = Eventgegevens.EventId;
                    if (notitie.IsGeldig())
                    {
                        int ok = DatabaseOperations.ToevoegenNotitie(notitie);
                        if (ok <= 0)
                        {
                            MessageBox.Show("Notitie is NIET toegevoegd!");
                        }
                        else
                        {
                            NotitieOverzicht notitieOverzicht = new NotitieOverzicht();
                            notitieOverzicht.Show();
                            this.Close();
                            MessageBox.Show("Uw notitie is toegevoegd!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(notitie.Error);
                    }
                }
                else
                {
                    MessageBox.Show(foutmelding);
                }
            }
            else
            { //Notitie aanpassen
                string foutmelding = Valideer("Titel" + "Omschrijving");
                if (string.IsNullOrWhiteSpace(foutmelding))
                {
                    notitie.Titel = txtNotitieNaamAanpassen.Text;
                    notitie.Omschrijving = txtNotitieOmschrijvingAanpassen.Text;
                    if (notitie.IsGeldig())
                    {
                        int ok = DatabaseOperations.AanpassenNotitie(notitie);
                        if (ok > 0)
                        {
                            NotitieOverzicht notitieOverzicht = new NotitieOverzicht();
                            notitieOverzicht.Show();
                            this.Close();
                            MessageBox.Show("Uw notitie is aangepast!");
                        }
                        else
                        {
                            MessageBox.Show("Notitie is niet aangepast!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(notitie.Error);
                    }
                }
                else
                {
                    MessageBox.Show(foutmelding);
                }
            }
        }
        private string Valideer(string columnName)
        {
            if (columnName == "Titel" && string.IsNullOrWhiteSpace(txtNotitieNaamAanpassen.Text))
            {
                return "Titel moet ingevuld zijn!";
            }
            if (columnName == "Omschrijving" && string.IsNullOrWhiteSpace(txtNotitieOmschrijvingAanpassen.Text))
            {
                return "De omschrijving moet ingevuld zijn!";
            }
            return "";
        }
    }
}
