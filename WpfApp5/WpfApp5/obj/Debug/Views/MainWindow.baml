   M S B A M L   `   `   `  ����e  aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089wTclr-namespace:System.Windows.Controls.Ribbon;assembly=System.Windows.Controls.RibbonSystem.Windows.Controls.Ribbon  5   	   B >WpfApp5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp5WpfApp5 5   	      WpfApp5.MainWindow   5   	   _ribbonTclr-namespace:System.Windows.Controls.Ribbon;assembly=System.Windows.Controls.Ribbon  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp5     5   	   U QMaterialDesignThemes.Wpf, Version=3.1.0.948, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          �Name$  Window��5   	    � Title$ Specialization Manager��5
   	   $	��950��6(   $
��1600��68      MaterialDesignBody#���   5      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � AllowsTransparency .  � WindowStyle$
 None=�6R    � WindowStartupLocation$ CenterScreen=�6e    � 	Resources 5       WpfApp5.MainViewModel  tvm%  WpfApp5.InverseBooleanConverter  inv �� 
TargetType  hasItems	 �� Triggers
 P� Property P� Value �� Property �� Value �� DataType  WpfApp5.SearchViewModel  WpfApp5.SearchControl  WpfApp5.HomeViewModel  WpfApp5.HomeControl WpfApp5.ProfileViewModel	  WpfApp5.ProfileControl!
  WpfApp5.StatisticsViewModel  WpfApp5.StatisticsControl  Slider_menu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  Slider_menu_reverse��%0  &	       &	      &	 &     (�� �     5       *, +)(�� �     5$      *, +)(��     5(      *, +)(�� G    5,      *,
 +)&	 y    &	       5   
   6     5   
   6(   �� 5   
   " ��6   	 5      P� 5      
 �@��HasItems .  . �5      ��  � y�$ 	Collapsed=�63   5      5      �� 5    
   "  6   .R�5!        5"      �� 5$   
   "  6   .R�5%        5&      �� 5(   
   "  6   .R�5)      	  5*      �� 5,   
   " 
 6   .R�5-        5.      �� 50   
   .��51      U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6,   $ 
stackPanel��6�   .\�52        $ 0:0:0.5��6'   $ 90.702Q�69   53      U� 54      $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6,   $ 
stackPanel��6�   .\�55        $ 0:0:0.5��6'   $ -0.54Q�69   56      57      �� 58   
   .��59      U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6,   $ 
stackPanel��6�   .\�5:      �� $ 0:0:0.5��6'   $ 0Q�69   5;      U� 5<      $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6,   $ 
stackPanel��6�   .\�5=      �� $ 0:0:0.5��6'   $ 0Q�69   5>      5?      5@       � Triggers 5A      :� 5B   
    :� 
SourceName$ 
stackPanel��6    :� RoutedEvent$ 
MouseEnter��6/   .[�5C      �� #��[ 6   5D      :� 5E   
   $ 
stackPanel��6   $ 
MouseLeave��6/   .[�5F      �� #��[ 6   5G      .��5I      � ���	#FFF2F2F2 � ColumnDefinitions 5J   
   �� 5K      $
��0.7* �6   6.   �� 5L      $��3* �6   6,   �� 5M      $��3* �6   6,   �� 5N      $��* �6   6+    � RowDefinitions 5P   
   �� 5Q      $	��40* �6   �� 5R      $	��33* �6   �� 5S      $	��33* �6   �� 5T      $	��33* �6   �� 5U      $	��10* �6   �� 5V      $��* �6   .+�5Z   
   � $��4��6   
������� 5[      �� 5\      $��* �6#   6/   �� 5]      $��* �6#   6/   �� 5^      $��* �6#   6/   �� 5_      $��* �6#   6/   .+�5a      �� $��1��6   $��0��6,   ��5b      �� 5c       �� ImageSource$ ../Images/logo.jpg��6!   5e      �� 5g      $��Center=�6   $��Center=�63   $��Specialiseringsplaneraren��6P   $��1��6q   $��2��6�   $��36(�6�   $��Impact)�6�   5h      %  MaterialDesignThemes.Wpf.Badged  5�   
    c� OverflowMode$ AsNeeded=�5�         CornerRadius$ 5��5�      $��3��6   $��Right=�6,   $��Bottom=�6H   $��0,0,10,0q�6c     ControlzEx.BadgedEx   Badge 5�      �� *
NumCourses+ �� Source# [ .��5�      ��  � ToolTip$ Antal Kurser��5�      #��[" �5�      ��5�   $   �� *NavigateStatisticsCommand+# [ .��5�      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	Bookshelf=�5�      $��25��6    $��25��6+   5�      5�      �� 5�   
   $��3��6    �� Orientation$ 
Horizontal=�6%   $��Right=�6>   .+�5�      �� 
�������$	��Top=�6(   $��Center=�6@   ��6]   �� *MinimizeCommand+# [ ��6�   ��   �� ElementName$  Window��.��5�        $ WindowMinimize=�6*   $��Center=�6@   $��Center=�6]   5�      �� 5�      
�������$	��Top=�6(   $��Right=�6@   ��6\   �� *CloseCommand+# [ ��6�   �� $  Window��.��5�        $ WindowClose=�6*   $��Center=�6=   $��Center=�6Z   5�      5�      �� 5�   
   -   5        ! ��Name$! 
stackPanel��5�      $��1��6)   $��5��66   ���	#FF0595D6$	��120��6^   $
��Left=�6j   $��	-90,0,0,0q�6�   " J� RenderTransformOrigin$" 0.5,0.5&�6�   z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      T� 5�      5�      .+�5�      �� $��Stretch=�6Z   $��Center=�6   $��45��6�   ��6   �� *NavigateHomeCommand+# [ .��5�      �� $ 
Horizontal=�6   $��	0,0,-14,0q�66   .+�5�      �� $��	Startsida��6   $��Impact)�60     5�      $
 Home=�6.   $��25��6:   $��25��6E   $��14,0,0,0q�6Q   5�      5�      �� 5�      $��1��6   �� 5�      $��Stretch=�6]   $��Center=�6�   $��45��6�   ��6   �� *NavigateProfileCommand+# [ .��5�      �� $ 
Horizontal=�6   $��	0,0,-14,0q�66   .+�5�      �� $��Profil��6   $
��Left=�6-   $��65��6O   $��Impact)�6Z     5�      $ Person=�6.   $��25��6<   $��25��6G   $��10,0,0,0q�6S   $" 
-0.08,0.56&�6e   5�      5�      �� 5�      $��1��6   �� 5�      $��Stretch=�6\   $��Center=�6�   $��45��6�   ��6   �� *NavigateSearchCommand+# [ .��5�      �� $ 
Horizontal=�6   $��	0,0,-14,0q�66   .+�5�      �� $
��Sök��6   $
��Left=�6*   $��65��6L   $��Impact)�6W     5�      $ Search=�6.   $��25��6<   $��25��6G   $��10,0,0,0q�6S   5�      5�      �� 5�      $��Stretch=�6   $��Center=�6:   $��45��6\   ��6h   �� *NavigateStatisticsCommand+# [ .��5�      �� $ 
Horizontal=�6   $��	0,0,-14,0q�66   .+�5�      �� $��Mina val��6   $
��Left=�6/   $��65��6Q   $��Impact)�6\     5�      $ 	ChartLine=�6.   $��25��6?   $��25��6J   $��10,0,0,0q�6V   $" 
-0.08,0.56&�6h   5�      5�      5�      �� 5�   
   $��1��6   $��1��6)   $��4��66   $��3��6G   ��6[   �� *selectedViewModel+# [ 6�   5�      5�      