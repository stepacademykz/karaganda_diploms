   M S B A M L   `   `   `  ����F  BquestEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:questEditorquestEditor  5   	       questEditor.Results   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:questEditor     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Результаты��5	   	   $	��500��6   $	��950��6)   .��5
      �  � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      $��35 �6   6*   �� 5      $��35 �6   6*   �� 5      $��10 �6   6*   �� 5      $��35 �6   6*   �� 5      $��10 �6   6*   �� 5      $��35 �6   6*   �� 5      $��10 �6   6*   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��	dgResults��5   j   $��0��6   $��0��6    $��3��60   $
��Left=�6A   $��16(�6\      
IsReadOnly . 5      �� 5   
   $��3��6   $��0��6   $��20(�6-   $#��Введите фамилию��6;   6W   �� 5   
   -   5         ��Name$
 find��5   =   $��4��6   $��0��6   $��20(�6/   $�� ��6K   6U   �� 5   
   -   5         ��Name$ 
nameSearch��5   .   $��6��6   $��0��6   $��20(�6B   $��
Найти��6P   6{   �� 5   
   -   5        $ findAll��5   .   $��8��6   $��0��6   $��20(�6?   $!��Все результаты��6M   6~   5       5!      