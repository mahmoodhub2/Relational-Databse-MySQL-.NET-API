   M S B A M L   `   `   `  ����C  ?R_D_Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:R_D_GameR_D_Game  5   	       R_D_Game.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:R_D_Game     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��307* �6   �� 5      $
��486* �6   .+�5   
   �� -   5         ��Name$ 	nameInput��5      $
��Left=�6%   $��23��6@   $��6,18,0,0q�6L    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��120��6�   �� 5   
   -   5        $��Enter your name��5      $
��Left=�6+   $��
149,20,0,0q�6F   $	��Top=�6Z   $	��104��6r   6~   �� 5   
   -   5        $��Question��5   '   $y�hidden=�6   $
��Left=�67   $��66��6R   $��6,80,0,0q�6^   $
 Wrap=�6p   $�� ��6�   $	��Top=�6�   $	��247��6�   �� 5   
   -   5        $��correct_answer��5   C   $y�hidden=�6   $�� ��6Z   $
��Left=�6e   $��145,238,0,0q�6�   $	��Top=�6�   $	��114��6�   �� 5   
   -   5        $��potential_answer1��5   A   $y�hidden=�6   $�� ��6[   $
��Left=�6f   $��
10,197,0,0q�6�   $	��Top=�6�   $	��114��6�   �� 5   
   -   5        $��	winOrLose��5      $
��Left=�6%   $��9.667,0,0,-0.333q�6@   $
��Wrap=�6Z   $	��Top=�6n   $	��421��6�   $	��476��6�   $��1��6�   �� 5   
   -   5         ��Name$ errName��5   (   $y�hidden=�6   $
��Left=�69   $��	10,46,0,0q�6T   $
��Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��116��6�   �� 5   
   -   5        $ Score��5      $y�hidden=�6#   $
��Left=�67   $��
20,264,0,0q�6R   $
��Wrap=�6f   $	��Top=�6z   $��69��6�   $	��170��6�   �� 5   
   -	   5        $��reviewWinners��5   A   $y�hidden=�6   $��Review Board��6V   $
��Left=�6m   $��
10,303,0,0q�6�   $	��Top=�6�   $	��243��6�   �� 5   
   -
   5         ��Name$ potential_answer2��5   A   $y�hidden=�6   $�� ��6]   $
��Left=�6h   $��145,197,0,0q�6�   $	��Top=�6�   $	��114��6�   �� 5   
   -   5        $ potential_answer3��5   A   $y�hidden=�6   $�� ��6]   $
��Left=�6h   $��
12,238,0,0q�6�   $	��Top=�6�   $	��112��6�   �� 5   
   -   5        $ Timer��5      $
��Left=�6#   $��109,162,0,0q�6>   $
��Wrap=�6S   $�� ��6g   $	��Top=�6o   5      5      