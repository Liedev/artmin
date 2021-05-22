using Artmin_DAL;
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
using System.Data.Entity;
using Artmin_Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace Artmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // Lieven
    {
        public MainWindow()
        {
            InitializeComponent();           
 
        }
        private ObservableCollection<Event> events;
        List<Event> ophalenEvents = new List<Event>();
        string msg = "";
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ophalenEvents = DatabaseOperations.OphalenEvents();
            events = new ObservableCollection<Event>(ophalenEvents);
            if (events != null)
            {
                icItemscontrolEvents.ItemsSource = events;
            }
            else
            {
                MessageBox.Show("Kon uw events niet laden. Gelieve later nog eens te proberen.");
            }
            
        }

        private void btnEventToevoegen_Click(object sender, RoutedEventArgs e)
        {
            Eventgegevens.EventId = 0;
            EventBewerkenOpenen();
        }

        private void btnInstellingen_Click(object sender, RoutedEventArgs e)
        {
            msg = "Kan uw event niet bewerken, gelieve later nogmaals te proberen";
            IdOphalen(sender, ref msg);
            if (string.IsNullOrWhiteSpace(msg))
            {
                EventBewerkenOpenen();
            }
        }

        private void EventBewerkenOpenen()
        {
            EventBewerken eventBewerken = new EventBewerken();
            eventBewerken.Show();
            this.Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            msg = "Kon uw event details niet vinden. Gelieve later nogmaals te proberen.";
            IdOphalen(sender, ref msg);
            if (string.IsNullOrWhiteSpace(msg))
            {
                EventDetail eventDetail = new EventDetail();
                eventDetail.Show();
                this.Close();
            }
        }

        private static void IdOphalen(object sender, ref string msg)
        {
            var eventIDViaTag = ((Button)sender).Tag;
            if (eventIDViaTag != null)
            {
                Eventgegevens.EventId = Convert.ToInt32(eventIDViaTag);
                msg = "";
            }
            else
            {
                MessageBox.Show(msg);
            }
            
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            msg = "Uw event kon niet verwijdert worden";
            IdOphalen(sender, ref msg);
            if (string.IsNullOrWhiteSpace(msg))
            {
                Event teVerwijderenEvent = new Event();
                teVerwijderenEvent = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);
                if (teVerwijderenEvent != null)
                {
                    if (teVerwijderenEvent.Artiesten.Count == 0 &&
                        teVerwijderenEvent.Klant == null &&
                        teVerwijderenEvent.Locatie == null &&
                        teVerwijderenEvent.Notities.Count == 0 &&
                        teVerwijderenEvent.ToDos.Count == 0)
                    {
                        if (DatabaseOperations.VerwijderenEvent(teVerwijderenEvent) > 0)
                        {
                            events.Remove(teVerwijderenEvent);
                            MessageBox.Show("Event is succesvol verwijderd.");                     

                        }
                        else
                        {
                            MessageBox.Show("Uw event kon niet verwijdert worden");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uw event kon niet verwijdert worden omdat er nog gegevens aan vast hangen. Gelieve deze eerst te verwijderen");
                    }
                }
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

            LocatieSelectie locatieSelectie = new LocatieSelectie();
            locatieSelectie.Show();
            this.Close();
            
        }
    }
}
