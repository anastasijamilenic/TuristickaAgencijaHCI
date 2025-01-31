using CommunityToolkit.Mvvm.Input;
using TuristickaAgencija.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TuristickaAgencija.Views;
using MySqlX.XDevAPI;
using TuristickaAgencija.Converters;

namespace TuristickaAgencija.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Zaposleni> ZaposleniList { get; set; }
        public Zaposleni SelectedZaposleni { get; set; }
        public ICommand LoginCommand { get; set; }
        public ApplicationDbContext Context { get; set; }

        public LoginViewModel()
        {
            Context = new ApplicationDbContext();
            var zaposleni = Context.Zaposlenis.ToList(); 
            ZaposleniList = new ObservableCollection<Zaposleni>(zaposleni);
            HashExistingPasswords();
            SelectedZaposleni = new Zaposleni();
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SelectedZaposleni.Ime) && !string.IsNullOrWhiteSpace(SelectedZaposleni.Lozinka))
            {
                bool isUsernameExists = false;
                Zaposleni dbZaposleni = null;

                foreach (var zaposleni in ZaposleniList)
                {
                    if (zaposleni.Ime == SelectedZaposleni.Ime)
                    {
                        isUsernameExists = true;
                        dbZaposleni = zaposleni;
                        break;
                    }
                }

                if (isUsernameExists && dbZaposleni != null)
                {
                    string hashedPass = HashPassword(SelectedZaposleni.Lozinka);
                    // Debug ispis
                    System.Diagnostics.Debug.WriteLine($"Unesena hashirana lozinka: {hashedPass}");
                    System.Diagnostics.Debug.WriteLine($"Lozinka u bazi: {dbZaposleni.Lozinka}");

                    //TODO HASHIRATI U BAZI Lozinku
                    if (hashedPass == dbZaposleni.Lozinka)
                    {

                        CurrentUser.CurrentZaposleni = dbZaposleni;
                        // Koristi isAdmin za provjeru administratorskih privilegija
                        if (dbZaposleni.IsAdmin)
                        {
                            MessageBox.Show("Dobrodošli, administratoru!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Dobrodošli, korisniče!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();

                        if (parameter is Window window)
                        {
                            window.Close();
                        }
                    }
                    else
                    {
                        string message = (string)Application.Current.Resources["InvalidPasswordMessage"];
                        string title = (string)Application.Current.Resources["NotificationTitle"];

                        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    string message = (string)Application.Current.Resources["IncorrectUsername"];
                    string title = (string)Application.Current.Resources["NotificationTitle"];

                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        private void HashExistingPasswords()
        {
            var zaposleniList = Context.Zaposlenis.ToList();
            bool changesMade = false;

            foreach (var zaposleni in zaposleniList)
            {
                if (!zaposleni.Lozinka.Contains("=") || zaposleni.Lozinka.Length < 44) // Provjera da li lozinka izgleda hashirano
                {
                    zaposleni.Lozinka = HashPassword(zaposleni.Lozinka);
                    changesMade = true;
                }
            }

            if (changesMade)
            {
                Context.SaveChanges();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
