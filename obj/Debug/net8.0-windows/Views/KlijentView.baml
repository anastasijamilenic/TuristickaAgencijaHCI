   M S B A M L   `   `   `  ����M  ITuristickaAgencija, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TuristickaAgencija.ViewModelsTuristickaAgencija.ViewModels  5      *    $TuristickaAgencija.Views.KlijentView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :vm+clr-namespace:TuristickaAgencija.ViewModels     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � DataContext  5
      4  .TuristickaAgencija.ViewModels.KlijentViewModel  5   
   .��5      ]�    MaterialDesignBackground#���   6   .+�5   
   � #���   6   $	��Top=�6G   .+�5      �� .@�5      ��   PregledKlijenata#���  6   .��5      � $	��Top=�6    � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $��* �6,   .+�5      &   System.Windows.Controls.DataGrid  $��1��6#      AutoGenerateColumns$ False��5   #      
IsReadOnly$
 True��5   #      CanUserAddRows$ False��5    #   $��Stretch=�5!   #   $	��Top=�5"   #   $��10q�5#   #    �� SelectedItem 5   #   �� *SelectedKlijent+ �� Mode$ TwoWay=���5   #   �� *
Klijenti+   Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%   "     	KlijentID,  &System.Windows.Controls.DataGridColumn   Header# �  65   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6y   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6Z   �� *
IdKlijenta+6y     5&   "     Ime# �  65   $	 * 6l   
 6T   �� *Ime+6l     5'   "     Prezime# �  65   $	 * 6t   
 6X   �� *	Prezime+6t     5(   "    	 Email# �  65   $	 * 6p   
 6V   �� *Email+6p     5)   "     Telefon# �  65   $	 * 6y   
 6X   �� *BrojTelefona+6y   5+      5,      5-      �� 50        DodavanjeKlijenta#���  6   .��51      � $	��Top=�6   .+�52      ��  �� Orientation$ Vertical=�6%   $��10q�6<   .+�53      ��   ImeKlijenta)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  6&   $	��350��54   k   $��0,5,0,15q�6w   ��6&   �� *NewKlijent.Ime+ �� UpdateSourceTrigger$ PropertyChanged=�6w   �� 55      # �  6&   $	��350��56   o   $��0,5,0,15q�6{   ��6&   �� *NewKlijent.Prezime+$ PropertyChanged=�6{   �� 57      # �  6&   $	��350��58   m   $��0,5,0,15q�6y   ��6&   �� *NewKlijent.Email+$ PropertyChanged=�6y   �� 59      # �  6&   $	��350��5:   t   $��0,5,0,15q�6�   ��6&   �� *NewKlijent.BrojTelefona+$ PropertyChanged=�6�   �� 5;       	 DodajKlijenta#��� 	 6%   $	��200��6u   $��40��6�   $��0,20,0,0q�6�   ��6O   �� *AddKlijentCommand+6�   5<      5=      5>      �� 5A       
 IzmjenaBrisanjeKlijenta#��� 
 6   .��5B      � $	��Top=�6    5C      �� 5D      $
��Auto �6,   �� 5E      $��* �6,   .+�5I        $��1��6#   $ False��5J   #   $ False��5M   #   $��Stretch=�5N   #   $	��Top=�5O   #   $��10q�5P   #    5K   #   �� *SelectedKlijent+$ TwoWay=���5L   #   �� *
Klijenti+ 5Q        5R   "   # �  65   $	 * 6y   
 6Z   �� *
IdKlijenta+6y     5S   "   # �  65   $	 * 6l   
 6T   �� *Ime+6l     5T   "   # �  65   $	 * 6t   
 6X   �� *	Prezime+6t     5U   "   # �  65   $	 * 6p   
 6V   �� *Email+6p     5V   "   # �  65   $	 * 6y   
 6X   �� *BrojTelefona+6y   5X      �� 5Z      $ 
Horizontal=�6%   $��20,0,0,0q�6>   $��Center=�6P   .+�5[      ��   Izmijeni#���  6%   $	��100��6q   $��40��6}   $��	10,0,10,0q�6�   ��6J   �� *EditKlijentCommand+6�   �� 5\       
 Obrisi#���  6%   $	��100��6q   $��40��6}   ��6H   �� *DeleteKlijentCommand+6}   5]      5^      5_      5`      5a      5b      5c      