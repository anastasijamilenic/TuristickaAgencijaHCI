-- Insert data into POSLOVNICA table
INSERT INTO POSLOVNICA (idPoslovnice, brojTelefona, adresa)
VALUES
(1, '0123456789', 'Adresa 1'),
(2, '0987654321', 'Adresa 2');

-- Insert data into ZAPOSLENI table
INSERT INTO ZAPOSLENI (ime, prezime, plata, pozicija, idPoslovnice, isAdmin, lozinka)
VALUES
('Marko', 'Markovic', 3000.00, 'Manager', 1, 1, 'password123'),
('Jovana', 'Jovanovic', 2500.00, 'Agent', 2, 0, 'password456');

-- Insert data into PREVOZ table
INSERT INTO PREVOZ (tipPrevoza, nazivKompanije, cijena)
VALUES
('Bus', 'BusCompany A', 100.00),
('Train', 'TrainCompany B', 150.00);

-- Insert data into SMJESTAJ table
INSERT INTO SMJESTAJ (nazivSmjestaja, vrstaSmjestaja, lokacija, brojZvjezdica, pogodnosti, cijena)
VALUES
('Hotel A', 'Hotel', 'City Center', '5', 'Wi-Fi, Pool', 200.00),
('Hotel B', 'Hotel', 'Beach Side', '4', 'Wi-Fi', 150.00);

-- Insert data into ARANZMAN table
INSERT INTO ARANZMAN (nazivDestinacije, cijena, termin, trajanje, VODIC_idVodica, OSIGURANJE_idOsiguranja, PREVOZ_idPrevoza, SMJESTAJ_idSmjestaja)
VALUES
('Paris', 1000.00, '2025-05-01', '7 days', 1, 1, 1, 1),
('Rome', 1200.00, '2025-06-15', '5 days', 2, 2, 2, 2);

-- Insert data into USLUGA table
INSERT INTO USLUGA (tipUsluge, cijena)
VALUES
('Excursion', 50.00),
('Insurance', 30.00);

-- Insert data into KLIJENT table
INSERT INTO KLIJENT (ime, prezime, brojTelefona, email, datumRodjenja)
VALUES
('Ana', 'Anic', '0912345678', 'ana@domain.com', '1990-08-15'),
('Ivan', 'Ivic', '0923456789', 'ivan@domain.com', '1985-03-25');

-- Insert data into NACIN_PLACANJA table
INSERT INTO NACIN_PLACANJA (idNacinaPlacanja, nazivNacinaPlacanja)
VALUES
(1, 'Credit Card'),
(2, 'Cash');

-- Insert data into REZERVACIJA table
INSERT INTO REZERVACIJA (datumRezervacije, ukupnaCijena, idKlijenta, idAranzmana, idZaposlenog, NACIN_PLACANJA_idNacinaPlacanja, POSLOVNICA_idPoslovnice)
VALUES
('2025-01-15', 1050.00, 1, 1, 1, 1, 1),
('2025-02-20', 1250.00, 2, 2, 2, 2, 2);

ALTER TABLE TERMIN
MODIFY COLUMN idTermina INT AUTO_INCREMENT;

-- Insert data into TERMIN table
INSERT INTO TERMIN (datumPocetka, datumKraja, brojSlobodnihMjesta, idAranzmana)
VALUES
('2025-05-01', '2025-05-07', 20, 1),
('2025-06-15', '2025-06-20', 15, 2);

-- Insert data into REZERVACIJA_has_USLUGA table
INSERT INTO REZERVACIJA_has_USLUGA (REZERVACIJA_idRezervacije, USLUGA_idUsluge)
VALUES
(1, 1),
(2, 2);


-- Insert data into POSLOVNICA table
INSERT INTO POSLOVNICA (idPoslovnice, brojTelefona, adresa)
VALUES
(3, '0112233445', 'Adresa 3'),
(4, '0223344556', 'Adresa 4');

-- Insert data into ZAPOSLENI table
INSERT INTO ZAPOSLENI (ime, prezime, plata, pozicija, idPoslovnice, isAdmin, lozinka)
VALUES
('Petar', 'Petrovic', 2800.00, 'Supervisor', 3, 0, 'password789'),
('Milica', 'Milic', 2600.00, 'Agent', 4, 0, 'password987');

-- Insert data into PREVOZ table
INSERT INTO PREVOZ (tipPrevoza, nazivKompanije, cijena)
VALUES
('Plane', 'Airline A', 500.00),
('Ferry', 'FerryCompany C', 80.00);

-- Insert data into SMJESTAJ table
INSERT INTO SMJESTAJ (nazivSmjestaja, vrstaSmjestaja, lokacija, brojZvjezdica, pogodnosti, cijena)
VALUES
('Resort C', 'Resort', 'Island', '5', 'Wi-Fi, Spa, Private Beach', 350.00),
('Hostel D', 'Hostel', 'Downtown', '3', 'Wi-Fi, Shared Kitchen', 80.00);

-- Insert data into ARANZMAN table
INSERT INTO ARANZMAN (nazivDestinacije, cijena, termin, trajanje, VODIC_idVodica, OSIGURANJE_idOsiguranja, PREVOZ_idPrevoza, SMJESTAJ_idSmjestaja)
VALUES
('London', 1400.00, '2025-07-10', '6 days', 3, 3, 3, 3),
('Dubai', 1600.00, '2025-09-05', '7 days', 4, 4, 4, 4);

-- Insert data into USLUGA table
INSERT INTO USLUGA (tipUsluge, cijena)
VALUES
('City Tour', 60.00),
('Dinner Package', 40.00);

-- Insert data into KLIJENT table
INSERT INTO KLIJENT (ime, prezime, brojTelefona, email, datumRodjenja)
VALUES
('Sara', 'Saric', '0934567890', 'sara@domain.com', '1995-11-20'),
('Nikola', 'Nikolic', '0945678901', 'nikola@domain.com', '1982-07-14');

-- Insert data into NACIN_PLACANJA table
INSERT INTO NACIN_PLACANJA (idNacinaPlacanja, nazivNacinaPlacanja)
VALUES
(3, 'Bank Transfer'),
(4, 'PayPal');

-- Insert data into REZERVACIJA table
INSERT INTO REZERVACIJA (datumRezervacije, ukupnaCijena, idKlijenta, idAranzmana, idZaposlenog, NACIN_PLACANJA_idNacinaPlacanja, POSLOVNICA_idPoslovnice)
VALUES
('2025-03-10', 1450.00, 3, 3, 3, 3, 3),
('2025-04-05', 1650.00, 4, 4, 4, 4, 4);

-- Insert data into TERMIN table
INSERT INTO TERMIN (datumPocetka, datumKraja, brojSlobodnihMjesta, idAranzmana)
VALUES
('2025-07-10', '2025-07-16', 25, 3),
('2025-09-05', '2025-09-12', 18, 4);

-- Insert data into REZERVACIJA_has_USLUGA table
INSERT INTO REZERVACIJA_has_USLUGA (REZERVACIJA_idRezervacije, USLUGA_idUsluge)
VALUES
(3, 1),
(4, 2);


