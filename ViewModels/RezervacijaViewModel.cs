using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TuristickaAgencija.Data;
using TuristickaAgencija.ViewModels;


namespace TuristickaAgencija.ViewModels
{
    internal class RezervacijaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Rezervacija> Rezervacije { get; set; }
        public Rezervacija NewRezervacija { get; set; }
        private Rezervacija _selectedRezervacija;

        public Rezervacija SelectedRezervacija
        {
            get => _selectedRezervacija;
            set
            {
                if (_selectedRezervacija != value)
                {
                    _selectedRezervacija = value;
                    OnPropertyChanged(nameof(SelectedRezervacija));
                }
            }
        }

        public ICommand AddRezervacijaCommand { get; set; }
        public ICommand EditRezervacijaCommand { get; set; }
        public ICommand DeleteRezervacijaCommand { get; set; }

        public ApplicationDbContext Context { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RezervacijaViewModel()
        {
            Context = new ApplicationDbContext();
            var rezervacije = Context.Rezervacijas
                .Include(r => r.IdKlijentaNavigation)
                .Include(r => r.IdAranzmanaNavigation)// Učitavamo povezane podatke
                .ToList();

            Rezervacije = new ObservableCollection<Rezervacija>(rezervacije);

            NewRezervacija = new Rezervacija();

            AddRezervacijaCommand = new RelayCommand(AddRezervacija);
            EditRezervacijaCommand = new RelayCommand(EditRezervacija);
            DeleteRezervacijaCommand = new RelayCommand(DeleteRezervacija);
        }

        public void AddRezervacija()
        {
            
                try
                {
                    // Postavljanje fiksnih vrednosti za NacinPlacanja i Poslovnicu
                    NewRezervacija.NacinPlacanjaIdNacinaPlacanja = 1;
                    NewRezervacija.PoslovnicaIdPoslovnice = 1;

                    // Povezivanje navigacijskih svojstava (Klijent, Aranzman, itd.)
                    NewRezervacija.IdKlijentaNavigation = Context.Klijents.FirstOrDefault(k => k.IdKlijenta == NewRezervacija.IdKlijenta);
                    NewRezervacija.IdAranzmanaNavigation = Context.Aranzmans.FirstOrDefault(a => a.IdAranzmana == NewRezervacija.IdAranzmana);
                    NewRezervacija.IdZaposlenog = 1;

                    // Provera da li Klijent i Aranžman postoje u bazi
                    var klijentExists = Context.Klijents.Any(k => k.IdKlijenta == NewRezervacija.IdKlijenta);
                    var aranzmanExists = Context.Aranzmans.Any(a => a.IdAranzmana == NewRezervacija.IdAranzmana);

                    if (!klijentExists || !aranzmanExists)
                    {
                        MessageBox.Show("Klijent ili Aranžman nisu validni.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Dodavanje nove rezervacije u context
                    Context.Rezervacijas.Add(NewRezervacija);
                    Context.SaveChanges();

                    // Dodavanje u kolekciju za prikaz
                    Rezervacije.Add(NewRezervacija);
                    NewRezervacija = new Rezervacija();
                    OnPropertyChanged(nameof(NewRezervacija));
                }
                catch (DbUpdateException dbEx)
                {
                    // Obrađivanje greške vezane za bazu podataka
                    MessageBox.Show($"Greška pri ažuriranju baze podataka: {dbEx.Message}\nInner exception: {dbEx.InnerException?.Message}", "Greška u bazi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    // Generalna obrada izuzetaka
                    MessageBox.Show($"Došlo je do greške: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        


        public void EditRezervacija()
        {
            if (SelectedRezervacija != null)
            {
                try
                {
                    var existingRezervacija = Context.Rezervacijas
                        .Include(r => r.IdKlijentaNavigation)
                        .FirstOrDefault(r => r.IdRezervacije == SelectedRezervacija.IdRezervacije);

                    if (existingRezervacija != null)
                    {
                        existingRezervacija.IdKlijenta = SelectedRezervacija.IdKlijenta;
                        existingRezervacija.DatumRezervacije = SelectedRezervacija.DatumRezervacije;
                        existingRezervacija.UkupnaCijena = SelectedRezervacija.UkupnaCijena;

                        // Ažuriramo navigacijsko polje ako je promijenjen klijent
                        existingRezervacija.IdKlijentaNavigation = Context.Klijents.FirstOrDefault(k => k.IdKlijenta == SelectedRezervacija.IdKlijenta);

                        Context.SaveChanges();

                        // Ažuriramo prikaz
                        var index = Rezervacije.IndexOf(SelectedRezervacija);
                        if (index >= 0)
                        {
                            Rezervacije[index] = existingRezervacija;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteRezervacija()
        {
            if (SelectedRezervacija != null)
            {
                try
                {
                    var existingRezervacija = Context.Rezervacijas
                        .FirstOrDefault(r => r.IdRezervacije == SelectedRezervacija.IdRezervacije);

                    if (existingRezervacija != null)
                    {
                        Context.Rezervacijas.Remove(existingRezervacija);
                        Context.SaveChanges();
                        Rezervacije.Remove(SelectedRezervacija);
                        SelectedRezervacija = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
