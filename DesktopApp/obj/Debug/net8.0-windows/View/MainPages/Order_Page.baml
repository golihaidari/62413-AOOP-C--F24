   M S B A M L   `   `   `  ����E  ADesktopApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DesktopApp.View.MainPagesDesktopApp.View.MainPages  5      =#clr-namespace:DesktopApp.ViewModelsDesktopApp.ViewModels  5      *    $DesktopApp.View.MainPages.Order_Page   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:DesktopApp.View.MainPages     5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,vm#clr-namespace:DesktopApp.ViewModels  5        N� Title$  
Order_Page��5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��0 0 0 30q�6;   .+�5      �� $��Stretch=�6   ���	#80808080$
��0.70��6E   $��	20 5 20 5q�6R   �� 5      $��Order history��6      	pageTitle#��[  6-   5      � 5   
   $��1��6   $��	20 0 20 5q�6    � ColumnDefinitions 5      �� 5      $	��250 �6#   �� 5      $��* �6#   .+�5"      � $��0��6   ���	#FFF1F1F1.+�5%      �� $��Orders��6   $��20(�6*   $��SemiBold"�68   $��	20 10 0 0q�6N   $	��Top=�6a   $
��Left=�6y   �� 5(      -   5        $��
20 40 20 0q�5(   l   $
��Left=�6�   ��6   �� *Orders+ �� View 5)      �� 5*      .P�5+      �� $��
Order Date��6-    �� Width$
 Auto��6l    �� DisplayMemberBinding 6A   �� *	OrderDate+6l   �� 5,      $��Status��6-   $
 Auto��6e    6=   �� *Status+6e   5-      5/      50      � 53      $��1��6   ���#FAF0E6.+�55      �� $��OrderDetails Items��6   $��20(�56      $��SemiBold"�57      $��	20 10 0 0q�58      $	��Top=�59      $��Center=�5:      �� 5=      -   5         ��Name$ orderItemListView��5=   +   $��1��6   $��
20 40 20 0q�6F    5>      �� 5?      .P�5@      �� $��	ProductId��6-   $
 Auto��6m    6@   �� *
Product.Id+6m   �� 5A      $
��Name��6-    6;   �� *Product.Name+�� 5B      $��Price��6-    6<   �� *Product.Price+�� 5C      $��Quantity��6-   $
 Auto��6i    6?   �� *
Quantity+6i   5D      5F      5G      5H      5I      5J      