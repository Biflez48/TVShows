   M S B A M L   `   `   `  ����B  >TVshows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TVshows.PagesTVshows.Pages  5          TVshows.Pages.ShowsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:TVshows.Pages     5        N� Title$  	ShowsPage��5	       N� 
Background �#3A5154 � 	Resources 5         listTemplate �� CornerRadius!   TVshows.Pages.HeartCheckBox!   TVshows.Pages.HeartCheckBox   MouseLeftButtonUp   	IsChecked �� Path  listPanelTemplate��%�  &	        &	 O    �� 5   
   .R�5      �� $ 10��6   ���#3A5154$��2q�5      $��10q�5      $	��275��5      $��80��5      ._�5      ]� ���#929AA2.+�5      �� $
��Bold"�5      $
��Left=�5      ��5      �� *tDurationSh+5      �� 5      $
��Left=�6!   $��5 0 0 0q�5   !   .+�5      �� $
��Bold"�5       $
��Left=�5       ��5       �� *NameSh+5       �� 5      $
��Wrap=�5   $   $
��Left=�5    $   $��0 5 0 0q�5!   $   ��5   $   �� *DescrioptionSh+5!   $   5"        5#      # HeartCheckBox_MouseLeftButtonUp5$   *   $��45��5%   "   $��45��5&   "    5#   *   �� *AddFavorite+5&   "   �� 5'      $
��Bold"�5(      $��Right=�5)      ��5'      �� $ Categories.NameCat�5)      �� 5*      $��Right=�6   5+      5-      5.      �� 5/   
   .R�50      � 51      52      .��54      �  � ColumnDefinitions 55   
   �� 56      $
��409* �6   �� 57      $
��Auto �6   �� 58      $	��250 �6   $	��250��6+    � RowDefinitions 5:   
   �� 5;      $	��10* �6   �� 5<      $	��90* �6   .+�5>   
   �� $��0��6   $��0��5?      $��3��5@      	 �� Orientation$	 
Horizontal=�5A      .+�5B      �� -   5        
 ��Name$
 AddShowButton��5B      $��Добавить��5C      $��32��5D      $	��109��5E      ���#929AA2$��16(�5G      $��	20 0 10 0q�5H      $��Inter)�5I      $
��Bold"�5J      6'   �� 5K      -   5        $
 EditShowButton��5K      $��Изменить��5L      $��32��5M      $	��109��5N      ���#929AA2$��16(�5P      $
��10 0q�5Q      $��Inter)�5R      $
��Bold"�5S      6'   �� 5T      -   5        $
 DeleteShowButton��5T      $��Удалить��5U      $��32��5V      $	��109��5W      ���#929AA2$��16(�5Y      $
��10 0q�5Z      $��Inter)�5[      $
��Bold"�5\      6'   5]      �� 5^   
   -   5         ��Name$ ShowsStackPanel��5^      $��0��5_      $��3��5`      $��1��5a      ���#B9B0B2.+�5c      �� -   5         ��Name$ ShowListBox��5c      ���#B9B0B2#��[  5e      #��[ 5f      $��Disabled=�5g      5i      �� 5j   
   -   5         ��Name$ DialogGridSplitter��5j      $��1��5k      $��1��5l      $��Center=�5m      $��3��5n      $y�Hidden=�5o      �� 5p   
   -   5         ��Name$ DialogScrollViewer��5p      $��2��5q      $��1��5r      $y�Hidden=�6$   .��5s       � -   5          �Name$ DialogFrame��5s      5t      5u      5v      