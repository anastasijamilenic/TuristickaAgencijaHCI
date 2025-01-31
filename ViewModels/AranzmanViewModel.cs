using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using TuristickaAgencija.Data;
using TuristickaAgencija.Converters;

namespace TuristickaAgencija.ViewModels
{
    internal class AranzmanViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Aranzman> Aranzmani { get; set; }
        public Aranzman NewAranzman { get; set; }
        private Aranzman _selectedAranzman;
        public bool IsAdmin { get; set; }

        public Aranzman SelectedAranzman
        {
            get => _selectedAranzman;
            set
            {
                if (_selectedAranzman != value)
                {
                    _selectedAranzman = value;
                    OnPropertyChanged(nameof(SelectedAranzman));
                }
            }
        }

        public ICommand AddAranzmanCommand { get; set; }
        public ICommand EditAranzmanCommand { get; set; }
        public ICommand DeleteAranzmanCommand { get; set; }

        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;



        public AranzmanViewModel()
        {
            Context = new ApplicationDbContext();
            IsAdmin = CurrentUser.CurrentZaposleni?.IsAdmin ?? false;
            var aranzmani = Context.Aranzmans
                .Include(a => a.PrevozIdPrevozaNavigation)
                .Include(a => a.SmjestajIdSmjestajaNavigation)
                .Include(a => a.Rezervacijas)
                .ToList();

            Aranzmani = new ObservableCollection<Aranzman>(aranzmani);

            NewAranzman = new Aranzman();

            AddAranzmanCommand = new RelayCommand(AddAranzman);
            EditAranzmanCommand = new RelayCommand(EditAranzman);
            DeleteAranzmanCommand = new RelayCommand(DeleteAranzman);
        }

        public void AddAranzman()
        {
            try
            {
                // Setting fixed values for VodicIdVodica and OsiguranjeIdOsiguranja
                NewAranzman.VodicIdVodica = 1; // Postavi vrednost za Vodica (ako uvek koristiš 1)
                NewAranzman.OsiguranjeIdOsiguranja = 1;
                NewAranzman.PrevozIdPrevoza = 1;
                NewAranzman.SmjestajIdSmjestaja = 1;// Postavi vrednost za Osiguranje (ako uvek koristiš 1)

                // Setting navigation properties (for Prevoz, Smjestaj, etc.)
                NewAranzman.PrevozIdPrevozaNavigation = Context.Prevozs.FirstOrDefault(p => p.IdPrevoza == NewAranzman.PrevozIdPrevoza);
                NewAranzman.SmjestajIdSmjestajaNavigation = Context.Smjestajs.FirstOrDefault(s => s.IdSmjestaja == NewAranzman.SmjestajIdSmjestaja);

                // Add the new Aranzman to the context and save changes
                Context.Aranzmans.Add(NewAranzman);
                Context.SaveChanges();

                // Add to collection for display
                Aranzmani.Add(NewAranzman);
                NewAranzman = new Aranzman();
                OnPropertyChanged(nameof(NewAranzman));
            }
            catch (DbUpdateException dbEx)
            {
                // Handling the specific DbUpdateException
                MessageBox.Show($"Database update error: {dbEx.Message}\nInner exception: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handling general exceptions
                MessageBox.Show($"Error adding Aranžman: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        public void EditAranzman()
        {
            // Prvo prikazivanje poruke sa pitanjem da li ste sigurni
            var result = MessageBox.Show("Are you sure you want to edit this Aranžman?",
                                         "Confirm Edit", MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var existingAranzman = Context.Aranzmans
                        .Include(a => a.PrevozIdPrevozaNavigation)
                        .Include(a => a.SmjestajIdSmjestajaNavigation)
                        .FirstOrDefault(a => a.IdAranzmana == SelectedAranzman.IdAranzmana);

                    if (existingAranzman != null)
                    {
                        // Update the fields
                        existingAranzman.NazivDestinacije = SelectedAranzman.NazivDestinacije;
                        existingAranzman.Cijena = SelectedAranzman.Cijena;
                        existingAranzman.Termin = SelectedAranzman.Termin;
                        existingAranzman.Trajanje = SelectedAranzman.Trajanje;
                        existingAranzman.PrevozIdPrevoza = SelectedAranzman.PrevozIdPrevoza;
                        existingAranzman.SmjestajIdSmjestaja = SelectedAranzman.SmjestajIdSmjestaja;

                        // Update navigation properties
                        existingAranzman.PrevozIdPrevozaNavigation = Context.Prevozs.FirstOrDefault(p => p.IdPrevoza == SelectedAranzman.PrevozIdPrevoza);
                        existingAranzman.SmjestajIdSmjestajaNavigation = Context.Smjestajs.FirstOrDefault(s => s.IdSmjestaja == SelectedAranzman.SmjestajIdSmjestaja);

                        Context.SaveChanges();

                        // Update the display list
                        var index = Aranzmani.IndexOf(SelectedAranzman);
                        if (index >= 0)
                        {
                            Aranzmani[index] = existingAranzman;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing Aranžman: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Optionally, notify the user that the edit was canceled
                MessageBox.Show("Edit operation was canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        public void DeleteAranzman()
        {
            if (SelectedAranzman != null)
            {
                // Prvo prikazivanje poruke sa pitanjem da li ste sigurni
                var result = MessageBox.Show("Are you sure you want to delete this Aranžman?",
                                             "Confirm Deletion", MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var existingAranzman = Context.Aranzmans
                            .FirstOrDefault(a => a.IdAranzmana == SelectedAranzman.IdAranzmana);

                        if (existingAranzman != null)
                        {
                            Context.Aranzmans.Remove(existingAranzman);
                            Context.SaveChanges();
                            Aranzmani.Remove(SelectedAranzman);
                            SelectedAranzman = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting Aranžman: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
