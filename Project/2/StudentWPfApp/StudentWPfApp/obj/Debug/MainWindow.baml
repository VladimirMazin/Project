   M S B A M L   `   `   `  ����H  DStudentWPfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:StudentWPfAppStudentWPfApp  5   	       StudentWPfApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:StudentWPfApp     5   	     � Title$.  (Форма для регистрации��5   	   $	��310��6'   $	��600��64    � 
ResizeMode$ NoResize=�6@    � Icon$ Снимок14.png��6V   .��5	      � .+�5
   
   � -   5         �Name$ RegistrationGrid��5
      $y�Visible=�6)   .+�5      �� -   5         ��Name$ ChooseTestTextBox��5      $��Выбрать тест��60   $
��Left=�6G   $��	25,0,0,75q�6b   $��Bottom=�6u   $��45��6�   $	��210��6�   $��Calibri)�6�   $��22(�6�   $��3q�6�   ���	#FF7AAEF36�   �� 5      -   5         ��Name$ NameTextBox��5      $
��Left=�6,   $��
340,19,0,0q�6G    �� TextWrapping$
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $	��220��6�   $��34��6�   $��Calibri)�6�   $��22(�6�   �� 5      $%��Введите своё имя:��6   $
��Left=�60   $��	25,17,0,0q�6K   $	��Top=�6^   $��34��6v   $	��190��6�   $��Calibri)�6�   $��22(�6�   �� 5      -   5        $ StartTestTextBox��5      $+��%Начать тестирование��6/   $��	25,0,0,17q�6M   $��Calibri)�6`   $��22(�6u   $��50��6�   $��Bottom=�6�   $
��Left=�6�   $	��210��6�   $��3q�6�   ���	#FF7AAEF36  �� 5      $-��'Введите свою фамилию:��6   $
��Left=�64   $��	25,57,0,0q�6O   $	��Top=�6b   $��34��6z   $	��270��6�   $��22(�6�   $��Calibri)�6�   �� 5      -   5        $ SurnameTextBox��5      $
��Left=�6.   $��
340,59,0,0q�6I   $
 Wrap=�6]   $�� ��6q   $	��Top=�6y   $	��220��6�   $��34��6�   $��22(�6�   $��Calibri)�6�   �� 5      $:��4Введите свою учебную группу:��6   $
��Left=�6;   $��
25,101,0,0q�6V   $	��Top=�6j   $��34��6�   $	��305��6�   $��22(�6�   $��Calibri)�6�   �� 5      -   5        $ GroupTextBox��5      $
��Left=�6,   $��340,101,0,0q�6G   $
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��220��6�   $��34��6�   $��22(�6�   $��Calibri)�6�   �� 5      -   5         ��Name$ TestNameLabel��5      $��Выбран тест: ��6+   $��Right=�6C   $��	0,0,25,17q�6_   $��Bottom=�6r   $	��102��6�   $	��320��6�   $��22(�6�   $��Calibri)�6�   5      � 5   
   -   5        $ QuastionGrid��5      $y�Hidden=�6%   .+�5      �� $��Label��6   $
��Left=�6$   $��	35,27,0,0q�6?   $	��Top=�6R   $��Calibri)�6j   $��22(�6   5      5      5      