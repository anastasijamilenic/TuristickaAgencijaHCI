
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using TuristickaAgencija.Properties;

namespace TuristickaAgencija.Views
{
    /// <summary>
    /// Interaction logic for OpcijeView.xaml
    /// </summary>
    public partial class OpcijeView : UserControl
    {
        public OpcijeView()
        {
            InitializeComponent();

            if (TuristickaAgencija.Properties.Settings.Default.Lang == "rs" || TuristickaAgencija.Properties.Settings.Default.Lang == "en")
            {
                SetLang(TuristickaAgencija.Properties.Settings.Default.Lang);
            }
        }

        private void ChangeThemeLight(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor((Color)ColorConverter.ConvertFromString("#C988B0"), BaseTheme.Light);
            SaveTheme("Light", (Color)ColorConverter.ConvertFromString("#C988B0"));
        }

        private void ChangeThemeDark(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor(Color.FromRgb(242, 174, 184), BaseTheme.Dark);

            SaveTheme("Dark", Color.FromRgb(242, 174, 184));
        }

        private void ChangeThemeMid(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor((Color)ColorConverter.ConvertFromString("#AEF2E8"), BaseTheme.Dark);
            SaveTheme("Dark", (Color)ColorConverter.ConvertFromString("#AEF2E8"));
        }

        private void ChangeFontSmall(object sender, RoutedEventArgs e)
        {
            TuristickaAgencija.Properties.Settings.Default.FontSize = 13;
            TuristickaAgencija.Properties.Settings.Default.Save();
        }

        private void ChangeFontMid(object sender, RoutedEventArgs e)
        {
            TuristickaAgencija.Properties.Settings.Default.FontSize = 16;
            TuristickaAgencija.Properties.Settings.Default.Save();
        }

        private void ChangeFontBig(object sender, RoutedEventArgs e)
        {
            TuristickaAgencija.Properties.Settings.Default.FontSize = 18;
            TuristickaAgencija.Properties.Settings.Default.Save();
        }

        private void ChangeFontDefault(object sender, RoutedEventArgs e)
        {
            TuristickaAgencija.Properties.Settings.Default.FontFamilyName = "Arial";
            TuristickaAgencija.Properties.Settings.Default.Save();
        }

        private void ChangeFontConsolas(object sender, RoutedEventArgs e)
        {
            TuristickaAgencija.Properties.Settings.Default.FontFamilyName = "Calibri";
            TuristickaAgencija.Properties.Settings.Default.Save();
        }

        private static void SetPrimaryColor(Color color, BaseTheme bt)
        {
            PaletteHelper paletteHelper = new PaletteHelper();

            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(bt);
            theme.SetPrimaryColor(color);

            paletteHelper.SetTheme(theme);
        }

        private void SaveTheme(string baseTheme, Color primaryColor)
        {
            TuristickaAgencija.Properties.Settings.Default.SelectedTheme = baseTheme;
            TuristickaAgencija.Properties.Settings.Default.SelectedPrimaryColor = primaryColor.ToString();
            TuristickaAgencija.Properties.Settings.Default.Save();
        }

        private void ChangeLanguageRs(object sender, RoutedEventArgs e)
        {
            SetLang("rs");
        }

        private void ChangeLanguageEn(object sender, RoutedEventArgs e)
        {
            SetLang("en");
        }

        private void SetLang(String lang)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            var languageResourceEn = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(r => r.Source != null && r.Source.ToString().Contains("Dictionary-en.xaml"));

            var languageResourceRs = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(r => r.Source != null && r.Source.ToString().Contains("Dictionary-rs.xaml"));

            if (languageResourceEn != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(languageResourceEn);
            }

            if (languageResourceRs != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(languageResourceRs);
            }

            ResourceDictionary resdict = new ResourceDictionary()
            {
                Source = new Uri($"/Resources/Dictionary-{lang}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(resdict);
            TuristickaAgencija.Properties.Settings.Default.Lang = lang;
            TuristickaAgencija.Properties.Settings.Default.Save();
        }
    }
}
