using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Converters;
using TuristickaAgencija.Data;

namespace TuristickaAgencija.ViewModels
{
    internal class ZaposleniViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Zaposleni> ZaposleniList { get; set; }
        public Zaposleni NewZaposleni { get; set; }
        private Zaposleni _selectedZaposleni;
        public bool IsAdmin { get; set; }

        public Zaposleni SelectedZaposleni
        {
            get => _selectedZaposleni;
            set
            {
                if (_selectedZaposleni != value)
                {
                    _selectedZaposleni = value;
                    OnPropertyChanged(nameof(SelectedZaposleni));
                }
            }
        }

        public ICommand AddZaposleniCommand { get; set; }
        public ICommand EditZaposleniCommand { get; set; }
        public ICommand DeleteZaposleniCommand { get; set; }

        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ZaposleniViewModel()
        {
            Context = new ApplicationDbContext();
            IsAdmin = CurrentUser.CurrentZaposleni?.IsAdmin ?? false;

            // Ažuriranje lozinki u bazi ako nisu hashirane
            HashExistingPasswords();
            var zaposleni = Context.Zaposlenis.Include(z => z.IdPoslovniceNavigation).ToList();

            ZaposleniList = new ObservableCollection<Zaposleni>(zaposleni);
            NewZaposleni = new Zaposleni();

            AddZaposleniCommand = new RelayCommand(AddZaposleni);
            EditZaposleniCommand = new RelayCommand(EditZaposleni);
            DeleteZaposleniCommand = new RelayCommand(DeleteZaposleni);
        }

        private void HashExistingPasswords()
        {
            using (var context = new ApplicationDbContext())
            {
                var zaposleniList = context.Zaposlenis.ToList();
                bool changesMade = false;

                foreach (var zaposleni in zaposleniList)
                {
                    if (!zaposleni.Lozinka.Contains("=") || zaposleni.Lozinka.Length < 44) // Provera da li lozinka izgleda hashirano
                    {
                        zaposleni.Lozinka = LoginViewModel.HashPassword(zaposleni.Lozinka);
                        changesMade = true;
                    }
                }

                if (changesMade)
                {
                    context.SaveChanges();
                }
            }
        }

        public void AddZaposleni()
        {
            if (!string.IsNullOrEmpty(NewZaposleni.Ime) && !string.IsNullOrEmpty(NewZaposleni.Prezime))
            {
                try
                {
                    // Hashiramo lozinku prije spremanja
                    NewZaposleni.Lozinka = LoginViewModel.HashPassword(NewZaposleni.Lozinka);
                    NewZaposleni.IdPoslovnice = 1;

                    Context.Zaposlenis.Add(NewZaposleni);
                    Context.SaveChanges();
                    ZaposleniList.Add(NewZaposleni);
                    NewZaposleni = new Zaposleni();
                    OnPropertyChanged(nameof(NewZaposleni));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public void EditZaposleni()
        {
            if (SelectedZaposleni != null)
            {
                try
                {
                    var existingZaposleni = Context.Zaposlenis.FirstOrDefault(z => z.IdZaposlenog == SelectedZaposleni.IdZaposlenog);

                    if (existingZaposleni != null)
                    {
                        existingZaposleni.Ime = SelectedZaposleni.Ime;
                        existingZaposleni.Prezime = SelectedZaposleni.Prezime;
                        existingZaposleni.Plata = SelectedZaposleni.Plata;
                        existingZaposleni.Pozicija = SelectedZaposleni.Pozicija;
                        existingZaposleni.IsAdmin = SelectedZaposleni.IsAdmin;

                        // Hashiraj lozinku samo ako je unesena nova vrijednost
                        if (!string.IsNullOrEmpty(SelectedZaposleni.Lozinka) && SelectedZaposleni.Lozinka != existingZaposleni.Lozinka)
                        {
                            existingZaposleni.Lozinka = LoginViewModel.HashPassword(SelectedZaposleni.Lozinka);
                        }

                        Context.SaveChanges();

                        var index = ZaposleniList.IndexOf(SelectedZaposleni);
                        if (index >= 0)
                        {
                            ZaposleniList[index] = existingZaposleni;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        public void DeleteZaposleni()
        {
            if (SelectedZaposleni != null)
            {
                try
                {
                    var existingZaposleni = Context.Zaposlenis.FirstOrDefault(z => z.IdZaposlenog == SelectedZaposleni.IdZaposlenog);

                    if (existingZaposleni != null)
                    {
                        Context.Zaposlenis.Remove(existingZaposleni);
                        Context.SaveChanges();
                        ZaposleniList.Remove(SelectedZaposleni);
                        SelectedZaposleni = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}