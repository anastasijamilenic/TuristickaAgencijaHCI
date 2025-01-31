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

namespace TuristickaAgencija.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Zaposleni> ZaposleniList { get; set; }
        public Zaposleni LoggedInZaposleni { get; set; }
        public Zaposleni NewKorisnik { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ApplicationDbContext Context { get; set; }
        public bool IsAdmin { get; set; }

        public RegisterViewModel()
        {
            Context = new ApplicationDbContext();
            var zaposleni = Context.Zaposlenis.ToList();
            ZaposleniList = new ObservableCollection<Zaposleni>(zaposleni);

            LoggedInZaposleni = null; // Or set a default value
            NewKorisnik = new Zaposleni();

            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        public RegisterViewModel(Zaposleni loggedInZaposleni)
        {
            Context = new ApplicationDbContext();
            var zaposleni = Context.Zaposlenis.ToList();
            ZaposleniList = new ObservableCollection<Zaposleni>(zaposleni);

            LoggedInZaposleni = loggedInZaposleni;
            NewKorisnik = new Zaposleni();

            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private bool CanRegister()
        {
            // Provera da li je prijavljeni korisnik admin
            return LoggedInZaposleni != null && LoggedInZaposleni.IsAdmin;
        }

        private void Register()
        {
            if (!string.IsNullOrWhiteSpace(NewKorisnik.Ime) && !string.IsNullOrWhiteSpace(NewKorisnik.Lozinka))
            {
                bool isUsernameExists = ZaposleniList.Any(z => z.Ime == NewKorisnik.Ime);

                if (!isUsernameExists)
                {
                    // Hashovanje lozinke
                    string hashedPass = HashPassword(NewKorisnik.Lozinka);
                    NewKorisnik.Lozinka = hashedPass;

                    // Postavljanje role za novog korisnika (npr. običan korisnik)
                    NewKorisnik.IsAdmin = false;
                    Context.Zaposlenis.Add(NewKorisnik);
                    Context.SaveChanges();

                    // Resetovanje forme
                    NewKorisnik = new Zaposleni();
                    OnPropertyChanged(nameof(NewKorisnik));

                    MessageBox.Show("Korisnik uspešno registrovan!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Korisničko ime već postoji!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Popunite sva obavezna polja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
