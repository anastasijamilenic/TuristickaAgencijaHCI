using System;
using System.Collections.Generic;
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
    internal class PrevozViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Prevoz> Prevozi { get; set; }
        public Prevoz NewPrevoz { get; set; }
        private Prevoz _selectedPrevoz;


        public Prevoz SelectedPrevoz
        {
            get => _selectedPrevoz;
            set
            {
                if (_selectedPrevoz != value)
                {
                    _selectedPrevoz = value;
                    OnPropertyChanged(nameof(SelectedPrevoz));
                }
            }
        }

        public ICommand AddPrevozCommand { get; set; }
        public ICommand EditPrevozCommand { get; set; }
        public ICommand DeletePrevozCommand { get; set; }

        public bool IsAdmin { get; set; }
        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public PrevozViewModel()
        {
            Context = new ApplicationDbContext();
            var prevozi = Context.Prevozs.ToList();
            Prevozi = new ObservableCollection<Prevoz>(prevozi);
            IsAdmin = CurrentUser.CurrentZaposleni?.IsAdmin ?? false;

            NewPrevoz = new Prevoz();
            _selectedPrevoz = NewPrevoz;

            AddPrevozCommand = new RelayCommand(AddPrevoz);
            EditPrevozCommand = new RelayCommand(EditPrevoz);
            DeletePrevozCommand = new RelayCommand(DeletePrevoz);
        }

        public void AddPrevoz()
        {
            if (!string.IsNullOrEmpty(NewPrevoz.TipPrevoza) && !string.IsNullOrEmpty(NewPrevoz.NazivKompanije))
            {
                try
                {
                    Context.Prevozs.Add(NewPrevoz);
                    Context.SaveChanges();

                    Prevozi.Add(NewPrevoz);
                    NewPrevoz = new Prevoz();
                    OnPropertyChanged(nameof(NewPrevoz));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Prevoz: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void EditPrevoz()
        {
            if (SelectedPrevoz != null)
            {
                try
                {
                    var existingPrevoz = Context.Prevozs.FirstOrDefault(p => p.IdPrevoza == SelectedPrevoz.IdPrevoza);

                    if (existingPrevoz != null)
                    {
                        existingPrevoz.TipPrevoza = SelectedPrevoz.TipPrevoza;
                        existingPrevoz.NazivKompanije = SelectedPrevoz.NazivKompanije;
                        existingPrevoz.Cijena = SelectedPrevoz.Cijena;

                        Context.SaveChanges();

                        var index = Prevozi.IndexOf(SelectedPrevoz);
                        if (index >= 0)
                        {
                            Prevozi[index] = existingPrevoz;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing Prevoz: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void DeletePrevoz()
        {
            if (SelectedPrevoz != null)
            {
                try
                {
                    var existingPrevoz = Context.Prevozs.FirstOrDefault(p => p.IdPrevoza == SelectedPrevoz.IdPrevoza);

                    if (existingPrevoz != null)
                    {
                        Context.Prevozs.Remove(existingPrevoz);
                        Context.SaveChanges();

                        Prevozi.Remove(SelectedPrevoz);
                        SelectedPrevoz = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Prevoz: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
