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
    internal class SmjestajViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Smjestaj> Smjestaji { get; set; }
        public Smjestaj NewSmjestaj { get; set; }
        private Smjestaj _selectedSmjestaj;
        public bool IsAdmin { get; set; }

        public Smjestaj SelectedSmjestaj
        {
            get => _selectedSmjestaj;
            set
            {
                if (_selectedSmjestaj != value)
                {
                    _selectedSmjestaj = value;
                    OnPropertyChanged(nameof(SelectedSmjestaj));
                }
            }
        }

        public ICommand AddSmjestajCommand { get; set; }
        public ICommand EditSmjestajCommand { get; set; }
        public ICommand DeleteSmjestajCommand { get; set; }

        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SmjestajViewModel()
        {
            Context = new ApplicationDbContext();
            IsAdmin = CurrentUser.CurrentZaposleni?.IsAdmin ?? false;

            Smjestaji = new ObservableCollection<Smjestaj>(Context.Smjestajs.ToList());

            NewSmjestaj = new Smjestaj();

            AddSmjestajCommand = new RelayCommand(AddSmjestaj);
            EditSmjestajCommand = new RelayCommand(EditSmjestaj);
            DeleteSmjestajCommand = new RelayCommand(DeleteSmjestaj);
        }

        public void AddSmjestaj()
        {
            if (!string.IsNullOrEmpty(NewSmjestaj.NazivSmjestaja) && !string.IsNullOrEmpty(NewSmjestaj.Lokacija))
            {
                try
                {
                    Context.Smjestajs.Add(NewSmjestaj);
                    Context.SaveChanges();

                    Smjestaji.Add(NewSmjestaj);
                    NewSmjestaj = new Smjestaj();
                    OnPropertyChanged(nameof(NewSmjestaj));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Smještaj: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void EditSmjestaj()
        {
            if (SelectedSmjestaj != null)
            {
                try
                {
                    var existingSmjestaj = Context.Smjestajs.FirstOrDefault(s => s.IdSmjestaja == SelectedSmjestaj.IdSmjestaja);

                    if (existingSmjestaj != null)
                    {
                        existingSmjestaj.NazivSmjestaja = SelectedSmjestaj.NazivSmjestaja;
                        existingSmjestaj.VrstaSmjestaja = SelectedSmjestaj.VrstaSmjestaja;
                        existingSmjestaj.Lokacija = SelectedSmjestaj.Lokacija;
                        existingSmjestaj.BrojZvjezdica = SelectedSmjestaj.BrojZvjezdica;
                        existingSmjestaj.Pogodnosti = SelectedSmjestaj.Pogodnosti;
                        existingSmjestaj.Cijena = SelectedSmjestaj.Cijena;

                        Context.SaveChanges();

                        var index = Smjestaji.IndexOf(SelectedSmjestaj);
                        if (index >= 0)
                        {
                            Smjestaji[index] = existingSmjestaj;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing Smještaj: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void DeleteSmjestaj()
        {
            if (SelectedSmjestaj != null)
            {
                try
                {
                    var existingSmjestaj = Context.Smjestajs.FirstOrDefault(s => s.IdSmjestaja == SelectedSmjestaj.IdSmjestaja);

                    if (existingSmjestaj != null)
                    {
                        Context.Smjestajs.Remove(existingSmjestaj);
                        Context.SaveChanges();
                        Smjestaji.Remove(SelectedSmjestaj);
                        SelectedSmjestaj = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Smještaj: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
