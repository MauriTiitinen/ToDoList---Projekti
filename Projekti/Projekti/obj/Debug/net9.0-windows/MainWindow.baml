   M S B A M L   `   `   `  ����C  ?Projekti, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ProjektiProjekti  5   	       Projekti.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Projekti     5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��800��6)   .��5	      � ���	#FFE4DBBC � ColumnDefinitions 5
   
   �� 5      $
��219* �6   �� 5      $
��181* �6   .+�5   
   �� -   5         ��Name$ Kello��5      $
��Left=�6   $��270,357,0,0q�6:   $	��Top=�6O   $	��104��6g   $��33��6s   $��1��6   �� 5   
   -   5        $ Paiva��5      $��14(�6   $
��Left=�6-   $��237,401,0,10q�6H   $	��115��6^   $��1��6j   �� 5   
   $��
TO-DO LIST��6   $
��Left=�6%   $��47��6@   $��
133,23,0,0q�6L   $	��Top=�6`   $	��142��6x   $��24(�6�   ���	#FFBBB7B74  .System.Windows.Automation.AutomationProperties   IsOffscreenBehavior$ Onscreen=�6�   $
��Bold"�6�   �� 5   
   $��Kesken olevat tehtävät��6   $
��Left=�61   $��24��6L   $��
139,75,0,0q�6X   $	��Top=�6l   $	��261��6�   �� 5   
   $��Valmiit tehtävät��6   $
��Left=�6+   $��29��6F   $��	160,0,0,0q�6R   $��Center=�6e   $	��174��6�   �� 5   
   -   5         ��Name$ poista��5      $��Poista��6!   $
��Left=�62   $��60��6M   $��133,408,0,0q�6Y   $	��Top=�6n   $��73��6�   ��6�   Z� �� 5   
   -   5        $��kesken��5      $��20,100,20,257q�6     J� RenderTransformOrigin$ 0.5,0.5&�67   ��6W   Z� z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.285Q�6&   T� 5      5      ��5      �� 5      .R�5      �� -   5         d� 	IsChecked$ FalseY�5   3   ��6   �� 6E   5       5"      �� 5#   
   -   5        $��valmiit��5#      $��20,255,20,94q�6!   ��67   Z� ��5$      �� 5%      .R�5&      �� -   5        $
 TrueY�5&   3   ��6   �� 6D   5'      5)      �� 5*   
   $=��7Valitse tehtävä klikkaamalla
 ja paina poista nappia��6   $
��Left=�6X   $��47��6s   $��217,421,0,0q�6   $	��Top=�6�   $	��176��6�   �� 5+   
   -   5        $ ��lisää uusi 
tehtävä
��5+      $
��Left=�6B   $��60��6]   $��
26,408,0,0q�6i   $	��Top=�6}   $��77��6�   ��6�   Z� �� 5-   
   -	   5        $ SettingsButton��5-      $	��☰��6)   $��99��5.      $��90��6   $��Right=�5/      $	��Top=�6-   $��0,9,10,0q�50      
���   �$��24(�52      $��1��6   ��51      Z� ��52   /   Z� �� 53   
   -
   5        $ ilmoitus��53      $��1��6#   $��Button��63   $
��Left=�6D   $��68��6_   $��
91,228,0,0q�6k   $	��Top=�6   $��78��6�   6�   5:      5;      