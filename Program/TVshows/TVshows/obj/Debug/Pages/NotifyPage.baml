   M S B A M L   `   `   `  ����B  >TVshows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TVshows.PagesTVshows.Pages  5          TVshows.Pages.NotifyPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:TVshows.Pages     5        N� Title$  	ShowsPage��5	       N� 
Background �#3A5154 � 	Resources 5         listTemplate �� CornerRadius  listPanelTemplate��%1  &	        &	 �    �� 5   
   .R�5      �� $ 10��6   ���#3A5154$��2q�5      $��10q�5      $	��300��5      $��80��5      ._�5      ]� ���#929AA2.+�5      �� $��Center=�6!   $��Center=�5   !   $
��Left=�5   !   .+�5      �� $��Center=�5       $��Center=�5       $��Center=�5       $��Center=�5       $
��Bold"�5       ��5       �� *tStart+5       �� 5      $��Center=�5        $��Center=�5!       $��Center=�5"       $��Center=�5#       ��5       �� *dStart+5#       5$      �� 5&      $
��Left=�6!   $��5 0 0 0q�5'   !   .+�5(      ]� .+�5)      �� $��Началось: ��6$   $
��Left=�5*       �� 5+      $
��Bold"�5,       $
��Left=�5-       ��5+   $   �� *NameSh+5-       5.      ]� 5/      .+�50      �� $��На канале: ��6$   $
��Left=�51       �� 52      $
��Left=�53   $   $��0 5 0 0q�54   $   ��52   $   �� *NameCh+54   $   55      56      57      59      5:      �� 5;   
   .R�5<      � 5=      5>      .��5@      �  � ColumnDefinitions 5A   
   �� 5B      $
��100* �6    � RowDefinitions 5D   
   �� 5E      $	��10* �6   �� 5F      $	��90* �6   .+�5H   
   �� $��0��6   $��0��5I       �� Orientation$ 
Horizontal=�5J      .+�5K      �� -   5         ��Name$ SetReadedButton��5K      $-��'Пометить прочитанным��5L      $��32��5M      $	��238��5N      ���#929AA2$��16(�5P      $��	20 0 10 0q�5Q      $��Inter)�5R      $
��Bold"�5S      5T      5U      �� 5V   
   -   5         ��Name$ ShowsStackPanel��5V      $��0��5W      $��2��5X      $��1��5Y      ���#B9B0B2.+�5[      �� -   5        	 ��Name$	 NotifyListBox��5[      ���#B9B0B2#��[  5]      #��[ 5^      $��Disabled=�5_      5`      5a      5b      