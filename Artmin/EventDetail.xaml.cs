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
    /// Interaction logic for EventDetail.xaml
    /// </summary>
    public partial class EventDetail : Window//Lieven
    {
        public EventDetail()
        {
            InitializeComponent();
        }
        Event nieuwEvent = new Event();
        List<ToDo> ophalenToDos = new List<ToDo>();
        private void btnEventsOverzicht_Click(object sender, RoutedEventArgs e)
        {
            ResetEnAfsluiten();
        }
        public void ResetEnAfsluiten()
        {
            Eventgegevens.EventId = 0;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ophalenToDos = DatabaseOperations.OphalenToDosViaEventId(Eventgegevens.EventId);
            nieuwEvent = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);
            if (nieuwEvent != null)
            {
                txtblEventNaam.Text = nieuwEvent.Eventnaam;
                txtblEventType.Text = nieuwEvent.Eventtype.Naam;
                txtbAantalNotities.Text = $"{nieuwEvent.Notities.Count.ToString()} notities";
                int todototaal = 0, afgewerktetodo = 0;
                foreach (var item in ophalenToDos)
                {
                    if (item.Afgewerkt == true)
                    {
                        afgewerktetodo++;
                    }
                    todototaal++;
                }
                if (todototaal > 0)
                {
                    txtbAantalToDosTeDoen.Text = $"{afgewerktetodo.ToString()}/{todototaal.ToString()} voltooid";
                    VisibilityXAanVUit(lblToDoX,lblToDoV);
                }
                else
                {
                    txtbAantalToDosTeDoen.Text = "Niets te doen";
                    VisibilityVAanXUit(lblToDoX, lblToDoV);
                }
                if (nieuwEvent.Locatie == null)
                {
                    VisibilityXAanVUit(lblLocatieX, lblLocatieV);
                    
                }
                else
                {
                    txtbPlaatsnaam.Text = nieuwEvent.Locatie.Naam;
                    txtbGemeente.Text = nieuwEvent.Locatie.Gemeente;
                    VisibilityVAanXUit(lblLocatieX, lblLocatieV);
                }
                if (nieuwEvent.Klant == null)
                {
                    VisibilityXAanVUit(lblKlantX, lblKlantV);
                }
                else
                {
                    txtbBedrijfsnaam.Text = nieuwEvent.Klant.Naam;
                    txtbNaam.Text = nieuwEvent.Klant.Contactnaam;
                    VisibilityVAanXUit(lblKlantX, lblKlantV);
                }
                int aantalArtiesten = nieuwEvent.Notities.Count;
                if (aantalArtiesten == 1)
                {
                    txtbArtiesten.Text = "1 artiest";
                    VisibilityVAanXUit(lblArtiestenX, lblArtiestenV);
                }
                else
                {
                    txtbArtiesten.Text = $"{nieuwEvent.Notities.Count.ToString()} artiesten";
                    if (aantalArtiesten == 0)
                    {
                        VisibilityXAanVUit(lblArtiestenX, lblArtiestenV);
                    }
                    else
                    {
                        VisibilityVAanXUit(lblArtiestenX, lblArtiestenV);
                    }
                }        
            }
            
        }
        private void VisibilityXAanVUit(Label labelX, Label labelY)
        {
            labelX.Visibility = Visibility.Visible;
            labelY.Visibility = Visibility.Collapsed;
        }
        private void VisibilityVAanXUit(Label labelX, Label labelY)
        {
            labelX.Visibility = Visibility.Collapsed;
            labelY.Visibility = Visibility.Visible;
        }

        private void BtnNotitie_Click(object sender, RoutedEventArgs e)
        {
            NotitieOverzicht notitieOverzicht = new NotitieOverzicht();
            notitieOverzicht.Show();
            this.Close();
        }

        private void btnToDo_Click(object sender, RoutedEventArgs e)
        {
            ToDoOverzicht toDoOverzicht = new ToDoOverzicht();
            toDoOverzicht.Show();
            this.Close();
        }

        private void btnLocatie_Click(object sender, RoutedEventArgs e)
        {
            LocatieSelectie locatieSelectie = new LocatieSelectie();
            locatieSelectie.Show();
            this.Close();
        }

        private void btnKlant_Click(object sender, RoutedEventArgs e)
        {
            KlantSelectie klantSelectie = new KlantSelectie();
            klantSelectie.Show();
            this.Close();
        }

        private void btnArtiesten_Click(object sender, RoutedEventArgs e)
        {
            ArtiestenOverzicht artiestenOverzicht = new ArtiestenOverzicht();
            artiestenOverzicht.Show();
            this.Close();
        }
    }
}
