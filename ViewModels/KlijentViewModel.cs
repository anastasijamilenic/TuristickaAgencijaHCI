using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Data;

namespace TuristickaAgencija.ViewModels
{
    internal class KlijentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Klijent> Klijenti { get; set; }
        public Klijent NewKlijent { get; set; }
        private Klijent _selectedKlijent;
        public bool IsAdmin { get; set; }

        public Klijent SelectedKlijent
        {
            get => _selectedKlijent;
            set
            {
                if (_selectedKlijent != value)
                {
                    _selectedKlijent = value;
                    OnPropertyChanged(nameof(SelectedKlijent));
                }
            }
        }

        public ICommand AddKlijentCommand { get; set; }
        public ICommand EditKlijentCommand { get; set; }
        public ICommand DeleteKlijentCommand { get; set; }

        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public KlijentViewModel()
        {
            Context = new ApplicationDbContext();
            var klijenti = Context.Klijents.ToList();
            Klijenti = new ObservableCollection<Klijent>(klijenti);

            NewKlijent = new Klijent();

            AddKlijentCommand = new RelayCommand(AddKlijent);
            EditKlijentCommand = new RelayCommand(EditKlijent);
            DeleteKlijentCommand = new RelayCommand(DeleteKlijent);
        }

        public void AddKlijent()
        {
            if (!string.IsNullOrEmpty(NewKlijent.Ime) && !string.IsNullOrEmpty(NewKlijent.Prezime) && !string.IsNullOrEmpty(NewKlijent.Email))
            {
                try
                {
                    Context.Klijents.Add(NewKlijent);
                    Context.SaveChanges();

                    Klijenti.Add(NewKlijent);
                    NewKlijent = new Klijent();
                    OnPropertyChanged(nameof(NewKlijent));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Klijent: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void EditKlijent()
        {
            if (SelectedKlijent != null)
            {
                try
                {
                    var existingKlijent = Context.Klijents.FirstOrDefault(k => k.IdKlijenta == SelectedKlijent.IdKlijenta);

                    if (existingKlijent != null)
                    {
                        // Update the fields
                        existingKlijent.Ime = SelectedKlijent.Ime;
                        existingKlijent.Prezime = SelectedKlijent.Prezime;
                        existingKlijent.BrojTelefona = SelectedKlijent.BrojTelefona;
                        existingKlijent.Email = SelectedKlijent.Email;
                        existingKlijent.DatumRodjenja = SelectedKlijent.DatumRodjenja;

                        Context.SaveChanges();

                        // Update the display list
                        var index = Klijenti.IndexOf(SelectedKlijent);
                        if (index >= 0)
                        {
                            Klijenti[index] = existingKlijent;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing Klijent: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void DeleteKlijent()
        {
            if (SelectedKlijent != null)
            {
                try
                {
                    var existingKlijent = Context.Klijents.FirstOrDefault(k => k.IdKlijenta == SelectedKlijent.IdKlijenta);

                    if (existingKlijent != null)
                    {
                        Context.Klijents.Remove(existingKlijent);
                        Context.SaveChanges();
                        Klijenti.Remove(SelectedKlijent);
                        SelectedKlijent = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Klijent: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
