   M S B A M L   `   `   `  ����M  ITuristickaAgencija, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TuristickaAgencija.ViewModelsTuristickaAgencija.ViewModels  5      )    #TuristickaAgencija.Views.PrevozView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :vm+clr-namespace:TuristickaAgencija.ViewModels     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � DataContext  5
      3  -TuristickaAgencija.ViewModels.PrevozViewModel  5   
   .��5      ]�    MaterialDesignBackground#���   6   .+�5   
   � #���   6   .+�5      �� .@�5      ��   PregledPrevoza#���  6   .��5      � $	��Top=�6    � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $��* �6,   .+�5      &   System.Windows.Controls.DataGrid  $��1��6#      AutoGenerateColumns$ False��5   #      
IsReadOnly$
 True��5   #      CanUserAddRows$ False��5   #   $	��250��5   #   $   HorizontalScrollBarVisibility$
 Auto=�5    #   "   VerticalScrollBarVisibility$
 Auto=�5!   #   $��14(�5"   #   $��10q�5#   #    �� SelectedItem 5   #   �� *SelectedPrevoz+ �� Mode$ TwoWay=���5   #   �� *	Prevozi+	   Columns	 5$      0  *System.Windows.Controls.DataGridTextColumn  5%   "     PrevozID,  &System.Windows.Controls.DataGridColumn
   Header#
 �  65      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6w   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6Y   �� *	IdPrevoza+6w     5&   "     
TipPrevoza#
 �  65   $ * 6z    6[   �� *
TipPrevoza+6z     5'   "     NazivKompanije#
 �  65   $ * 6�    6_   �� *NazivKompanije+6�     5(   "    
 Cijena#
 �  65   $ * 6r    6W   �� *Cijena+6r   5*      5+      5,      �� 5/        DodavanjePrevoza#���  6   {�6F   �� *	IsAdmin+.��50      � $��0,20,0,0q�6   .+�51      ��  �� Orientation$ Vertical=�6%   $��0,0,20,0q�6<   .+�52      �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  6&   $	��350��53   &   $��0,5,0,15q�54   &   ��55   &   �� *NewPrevoz.TipPrevoza+ �� UpdateSourceTrigger$ PropertyChanged=��� 57      # �  6&   $	��350��58   &   $��0,5,0,15q�59   &   ��5:   &   �� *NewPrevoz.NazivKompanije+$ PropertyChanged=��� 5<      # �  6&   $	��350��5=   &   $��0,5,0,15q�5>   &   ��5?   &   �� *NewPrevoz.Cijena+$ PropertyChanged=��� 5A        DodajPrevoz#���  6%   $	��200��5B   %   $��40��5C   %   $��0,20,0,0q�5E   %   ��5D   %   �� *AddPrevozCommand+5E   %   5F      5G      5H      �� 5K        IzmjenaBrisanjePrevoza#���  6   {�6L   �� *	IsAdmin+.��5L      � $	��Top=�6    5M      �� 5N      $
��Auto �6,   �� 5O      $��* �6,   .+�5S        $��1��6#   $ False��5T   #   $ False��5W   #   $��Stretch=�5X   #   $	��Top=�5Y   #   $��10q�5Z   #    5U   #   �� *SelectedPrevoz+$ TwoWay=���5V   #   �� *	Prevozi+	 5[        5\   "   #
 �  65   $ * 6w    6Y   �� *	IdPrevoza+6w     5]   "   #
 �  65   $ * 6z    6[   �� *
TipPrevoza+6z     5^   "   #
 �  65   $ * 6�    6_   �� *NazivKompanije+6�     5_   "   #
 �  65   $ * 6r    6W   �� *Cijena+6r   5a      �� 5c      $ 
Horizontal=�6%   $��20,0,0,0q�6>   $��Center=�6P   .+�5d      ��  	 Izmijeni#��� 	 6%   $	��100��5f   %   $��40��5g   %   $��	10,0,10,0q�5h   %   ��5e   %   �� *EditPrevozCommand+5h   %   �� 5i       

 Obrisi#��� 
 6%   $	��100��5k   %   $��40��5l   %   ��5j   %   �� *DeletePrevozCommand+5l   %   5m      5n      5o      5p      5q      5r      5s      