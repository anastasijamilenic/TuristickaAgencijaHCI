   M S B A M L   `   `   `  ����M  ITuristickaAgencija, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TuristickaAgencija.ViewModelsTuristickaAgencija.ViewModels  5      +    %TuristickaAgencija.Views.AranzmanView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :vm+clr-namespace:TuristickaAgencija.ViewModels     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � DataContext  5
      5  /TuristickaAgencija.ViewModels.AranzmanViewModel  5   
   .��5      ]�    MaterialDesignBackground#���   6   .+�5   
   � #���   6   $	��Top=�6G   .+�5      �� .@�5      ��   PregledAranzmana#���  6   .��5      � $	��Top=�6    � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $��* �6,   .+�5      &   System.Windows.Controls.DataGrid  $��1��6#      AutoGenerateColumns$ False��5   #      
IsReadOnly$
 True��5   #      CanUserAddRows$ False��5   #   $��Stretch=�5   #   $	��Top=�5   #   $��10q�5    #    �� SelectedItem 5   #   �� *SelectedAranzman+ �� Mode$ TwoWay=���5   #   �� *	Aranzmani+   Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"   "     
AranzmanID,  &System.Windows.Controls.DataGridColumn   Header# �  65   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6{   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6[   �� *IdAranzmana+6{     5#   "     Destinacija# �  65   $	 * 6�   
 6\   �� *NazivDestinacije+6�     5$   "    
 Termin# �  65   $	 * 6�   
 6W   �� *Termin+ �� StringFormat$ 
dd.MM.yyyy��6�     5%   "     Trajanje# �  65   $	 * 6v   
 6Y   �� *
Trajanje+6v     5&   "    
 Cijena# �  65   $	 * 6r   
 6W   �� *Cijena+6r   5(      5)      5*      �� 5,        DodavanjeAranzmana#���  6   {�6H   �� *	IsAdmin+.��5-      � $	��Top=�6   .+�5/      ��  �� Orientation$ Vertical=�6%   $��10q�6<   .+�50      ��   DestinacijaAranzmana)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  6&   $	��350��6�   $��0,5,0,15q�6�   ��6n   �� *NewAranzman.NazivDestinacije+ �� UpdateSourceTrigger$ PropertyChanged=�6�   (  "System.Windows.Controls.DatePicker  51       	 TerminAranzmana# � 	 6)   $	��350��53      $��0,5,0,15q�6      SelectedDate 52      �� *NewAranzman.Termin+$ TwoWay=�53      �� 55      # �  6&   $	��350��6�   $��0,5,0,15q�6�   ��6b   �� *NewAranzman.Trajanje+$ PropertyChanged=�6�   �� 56      # �  6&   $	��350��6�   $��0,5,0,15q�6�   ��6`   �� *NewAranzman.Cijena+$ PropertyChanged=�6�   �� 57       
 DodajAranzman#��� 
 6%   $	��200��6v   $��40��6�   $��0,20,0,0q�6�   ��6O   �� *AddAranzmanCommand+6�   58      59      5:      �� 5<        IzmjenaBrisanjeAranzmana#���  6   {�6N   �� *	IsAdmin+.��5=      � $	��Top=�6    5>      �� 5?      $
��Auto �6,   �� 5@      $��* �6,   .+�5C        $��1��6#   $ False��5D   #   $ False��5G   #   $��Stretch=�5H   #   $	��Top=�5I   #   $��10q�5J   #    5E   #   �� *SelectedAranzman+$ TwoWay=���5F   #   �� *	Aranzmani+ 5K        5L   "   # �  65   $	 * 6{   
 6[   �� *IdAranzmana+6{     5M   "   # �  65   $	 * 6�   
 6\   �� *NazivDestinacije+6�     5N   "   # �  65   $	 * 6�   
 6W   �� *Termin+$ 
dd.MM.yyyy��6�     5O   "   # �  65   $	 * 6v   
 6Y   �� *
Trajanje+6v     5P   "   # �  65   $	 * 6r   
 6W   �� *Cijena+6r   5R      �� 5T      $ 
Horizontal=�6%   $��20,0,0,0q�6>   $��Center=�6P   .+�5U      ��   Izmijeni#���  6%   $	��100��6r   $��40��6~   $��	10,0,10,0q�6�   ��6J   �� *EditAranzmanCommand+6�   �� 5V       
 Obrisi#���  6%   $	��100��6r   $��40��6~   ��6H   �� *DeleteAranzmanCommand+6~   5W      5X      5Y      5Z      5[      5\      5]      