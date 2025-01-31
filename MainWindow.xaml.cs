using System.Windows;
using System.Windows.Media;
using TuristickaAgencija.Views;
using TuristickaAgencija.Properties;

namespace TuristickaAgencija
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new AranzmanView();
        }

        private void OpenAranzman(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            AranzmanMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new AranzmanView();
        }

        private void OpenRezervacija(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            RezervacijaMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new RezervacijaView();
        }

        private void OpenKlijent(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            KlijentMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new KlijentView();
        }

        private void OpenUsluge(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            UslugeMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new UslugaView();
        }

        private void OpenPrevoz(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            PrevozMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new PrevozView();
        }

        private void OpenSmjestaj(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            SmjestajMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new SmjestajView();
        }

        private void OpenZaposleni(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            ZaposleniMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new ZaposleniView();
        }

        private void OpenOpcije(object sender, RoutedEventArgs e)
        {
            SetAllNull();
            OpcijeMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default.SelectedPrimaryColor));
            MainContent.Content = new OpcijeView();
        }

        /// <summary>
        /// Resetuje pozadinu svih stavki menija na podrazumevanu vrednost.
        /// </summary>
        private void SetAllNull()
        {
            AranzmanMenuItem.Background = null;
            RezervacijaMenuItem.Background = null;
            KlijentMenuItem.Background = null;
            UslugeMenuItem.Background = null;
            PrevozMenuItem.Background = null;
            SmjestajMenuItem.Background = null;
            ZaposleniMenuItem.Background = null;
            OpcijeMenuItem.Background = null;
        }
    }
}
