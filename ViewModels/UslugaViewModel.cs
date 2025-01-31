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
    internal class UslugaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Usluga> Usluge { get; set; }
        public Usluga NewUsluga { get; set; }
        private Usluga _selectedUsluga;
        public bool IsAdmin { get; set; }

        public Usluga SelectedUsluga
        {
            get => _selectedUsluga;
            set
            {
                if (_selectedUsluga != value)
                {
                    _selectedUsluga = value;
                    OnPropertyChanged(nameof(SelectedUsluga));
                }
            }
        }

        public ICommand AddUslugaCommand { get; set; }
        public ICommand EditUslugaCommand { get; set; }
        public ICommand DeleteUslugaCommand { get; set; }

        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public UslugaViewModel()
        {
            Context = new ApplicationDbContext();
            IsAdmin = CurrentUser.CurrentZaposleni?.IsAdmin ?? false;

            Usluge = new ObservableCollection<Usluga>(Context.Uslugas.ToList());

            NewUsluga = new Usluga();

            AddUslugaCommand = new RelayCommand(AddUsluga);
            EditUslugaCommand = new RelayCommand(EditUsluga);
            DeleteUslugaCommand = new RelayCommand(DeleteUsluga);
        }

        public void AddUsluga()
        {
            if (!string.IsNullOrEmpty(NewUsluga.TipUsluge) && NewUsluga.Cijena > 0)
            {
                try
                {
                    Context.Uslugas.Add(NewUsluga);
                    Context.SaveChanges();
                    Usluge.Add(NewUsluga);
                    NewUsluga = new Usluga();
                    OnPropertyChanged(nameof(NewUsluga));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Usluga: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void EditUsluga()
        {
            if (SelectedUsluga != null)
            {
                try
                {
                    var existingUsluga = Context.Uslugas.FirstOrDefault(u => u.IdUsluge == SelectedUsluga.IdUsluge);
                    if (existingUsluga != null)
                    {
                        existingUsluga.TipUsluge = SelectedUsluga.TipUsluge;
                        existingUsluga.Cijena = SelectedUsluga.Cijena;
                        Context.SaveChanges();

                        var index = Usluge.IndexOf(SelectedUsluga);
                        if (index >= 0)
                        {
                            Usluge[index] = existingUsluga;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing Usluga: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void DeleteUsluga()
        {
            if (SelectedUsluga != null)
            {
                try
                {
                    var existingUsluga = Context.Uslugas.FirstOrDefault(u => u.IdUsluge == SelectedUsluga.IdUsluge);
                    if (existingUsluga != null)
                    {
                        Context.Uslugas.Remove(existingUsluga);
                        Context.SaveChanges();
                        Usluge.Remove(SelectedUsluga);
                        SelectedUsluga = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Usluga: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
