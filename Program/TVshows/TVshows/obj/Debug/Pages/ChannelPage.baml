   M S B A M L   `   `   `  ����B  >TVshows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TVshows.PagesTVshows.Pages  5          TVshows.Pages.ChannelPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:TVshows.Pages     5        N� Title$  ChannelPage��5	       N� 
Background �#3A5154 � 	Resources 5         listTemplate �� CornerRadius �� Padding ��Name  listPanelTemplate��%�  &	        &	 �    �� 5   
   .R�5      �� $ 10��6   ���#3A5154$��2q�5      $��10q�5      $	��280��5      ._�5      ]� ���#929AA2.+�5      �� $ 5��6   ���#3A5154$��2q�5      $ 10q�5      ._�5      �� $ 	ImageLike��6    $��60��5       $��60��5       ���#3A5154$
��Left=�5       5      �� 5       $��Right=�6!   $��5 0 0 0q�5!   !   .+�5"      �� $
��Bold"�5#   $   $
��Wrap=�5$   $   $��18(�5%       $
��Left=�5&   $   ��5"   $   �� *NameCh+5&   $   �� 5'      $
��Wrap=�5(   $   $��16(�5)   $   $
��Left=�5*   $   $��0 5 0 0q�5+   $   ��5'   $   �� *DescriptionCh+5+   $   5,      5-      5/      50      �� 51   
   .R�52      � 53      54      .��56      �  � ColumnDefinitions 57   
   �� 58      $
��409* �6   �� 59      $
��Auto �6   �� 5:      $	��250 �6   $	��250��6+    � RowDefinitions 5<   
   �� 5=      $	��10* �6   �� 5>      $	��90* �6   .+�5@   
   ]� $��0��6   $��0��5A      $��3��5B      .+�5C      �� -   5         ��Name$ DeleteChannelButton��5C      $��Удалить��5D      $��32��5E      $	��110��5F      ���#929AA2$��16(�5H      $
��20 0q�5I      $��Inter)�5J      $��Right=�5K      $
��Bold"�5L      $y�Visible=�5M      5N      �� 5O      -   5        $ AddChannelButton��5O      $��Добавить��5P      $��32��5Q      $	��110��5R      ���#929AA2$��16(�5T      $��Inter)�5U      $��Right=�5V      $
��Bold"�5W      $y�Visible=�5X      5Y      �� 5Z      $��Right=�6   5\      �� 5]   
   -   5        	 ��Name$	 ChannelsStackPanel��5]      $��0��5^      $��3��5_      $��1��5`      ���#B9B0B2.+�5c      �� -   5        
 ��Name$
 ChnlsListBox��5c      #��[  5d      #��[ 5e      ���#B9B0B2$��Disabled=�5h      5j      �� 5k   
   -   5         ��Name$ DialogGridSplitter��5k      $��1��5l      $��1��5m      $��Center=�5n      $��3��5o      $y�Hidden=�5p      �� 5q   
   -   5         ��Name$ DialogScrollViewer��5q      $��2��5r      $��1��5s      $y�Hidden=�6$   .��5t       � -   5          �Name$ DialogFrame��5t      5u      5v      5x      