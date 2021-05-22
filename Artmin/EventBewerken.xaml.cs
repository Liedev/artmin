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
using System.Data.Entity;
using Artmin_DAL;
using System.Globalization;
using Syncfusion.Windows.Controls;

namespace Artmin
{
    /// <summary>
    /// Interaction logic for EventBewerken.xaml
    /// </summary>
    public partial class EventBewerken : Window //Lieven
    {
        public EventBewerken()
        {
            InitializeComponent();
        }
        Event eventOphalen = new Event();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Eventtype> eventtype = new List<Eventtype>();
            eventtype = DatabaseOperations.OphalenEventTypes();
            if (eventtype != null)
            {
                cmbType.ItemsSource = eventtype;
            }
            else
            {
                MessageBox.Show("Kon de eventtypes niet binnen laden. Gelieve later opnieuw te proberen.");
            }
            if (Eventgegevens.EventId > 0)
            {
                eventOphalen = DatabaseOperations.OphalenEventViaId(Eventgegevens.EventId);

                if (eventOphalen != null)
                {
                    txtEventNaam.Text = eventOphalen.Eventnaam;
                    cmbType.SelectedItem = eventOphalen.Eventtype;
                    dpDatum.Text = eventOphalen.Datum.ToString();
                    sftStartUur.Value = eventOphalen.Startuur;
                    sftEindUur.Value = eventOphalen.Einduur;

                }
                else
                {
                    MessageBox.Show("Kon uw event niet vinden. Gelieve later opnieuw te proberen.");
                }
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = "";
            foutmeldingen += Valideer("cmbType");
            foutmeldingen += Valideer("dbDatum");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Event _event = new Event();
                _event.Eventnaam = txtEventNaam.Text;
                Eventtype eventtypeOphalen = cmbType.SelectedItem as Eventtype;
                _event.EventtypeID = eventtypeOphalen.EventtypeID;
                _event.Datum = dpDatum.SelectedDate.Value;
                DateTime startuur = sftStartUur.Value.ToDateTime();
                DateTime einduur = sftEindUur.Value.ToDateTime();
                _event.Startuur = startuur.ToString("HH:mm");
                _event.Einduur = einduur.ToString("HH:mm");
                if (_event.IsGeldig())
                {
                    if (Eventgegevens.EventId > 0)
                    {
                        _event.EventID = eventOphalen.EventID;
                        if (DatabaseOperations.UpdateEvent(_event) > 0)
                        {
                            MessageBox.Show("Uw event is aangepast.");
                            ResetEnAfsluiten();
                        }
                        else
                        {
                            MessageBox.Show("Kan uw event niet aanpassen. Gelieve later nog eens te proberen");
                        }
                    }
                    else
                    {
                        if (DatabaseOperations.ToevoegenEvent(_event) > 0)
                        {
                            MessageBox.Show("Uw event is toegevoegd.");
                            ResetEnAfsluiten();
                        }
                        else
                        {
                            MessageBox.Show("Kan uw event niet toevoegen. Gelieve later nog eens te proberen");
                        }
                    }
                }
                else
                {
                    MessageBox.Show(_event.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        public string Valideer(string kolomnaam)
        {
            if (kolomnaam == "cmbType" && cmbType.SelectedItem == null)
            {
                return "Gelieve een eventtype te selecteren" + Environment.NewLine;
            }
            if (kolomnaam == "dbDatum" && dpDatum.SelectedDate == null)
            {
                return "Gelieve een datum te selecteren" + Environment.NewLine;
            }
            return "";
        }
        public void ResetEnAfsluiten()
        {
            Eventgegevens.EventId = 0;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnEventsOverzicht_Click(object sender, RoutedEventArgs e)
        {
            ResetEnAfsluiten();
        }
    }
}
