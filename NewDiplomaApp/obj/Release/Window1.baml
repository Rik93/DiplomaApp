   M S B A M L   `   `   `  ���� H  DNewDiplomaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DiplomaApp
DiplomaApp      DiplomaApp.Window1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:DiplomaApp       � Title$  Logowanie z bazą danych��$	��282��$	��417�� � 
ResizeMode$ NoResize=�.��� .+�� $��36,21,28,10q� � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��2* � � ColumnDefinitions �� $��9* ��� $	��14* �.+��� -    ��Name$ labelServerName��$��Nazwa serwera:��$
��Left=�$��34,0,0,0q�$	��Top=�$��26��$��92���� -    ��Name$ 
serverName��$
��Left=�$��23��$��10,3,0,0q� �� TextWrapping$
 Wrap=�$��sql7.freesqldatabase.com��$	��Top=�$	��190��$��1��{�. �� -   $ labelDbName��$��Nazwa bazy danych:��$
��Left=�$��9,3,0,0q�$	��Top=�$��1��$��26��$	��117���� -   $ dbName��$
��Left=�$��23��$��10,4,0,0q�$
 Wrap=�$��
sql7123454��$	��Top=�$	��190��$��1��$��1��{�. �� -   $ userName��$
��Left=�$��23��$��10,3,0,0q�$
 Wrap=�$��
sql7123454��$	��Top=�$	��190��$��1��$��2���� -   $ labelUsername��$��Nazwa użytkownika:��$
��Left=�$��9,0,0,0q�$	��Top=�$��2��$��26��$	��117���� -   $ labelPassword��$��Hasło:��$
��Left=�$��83,0,0,0q�$	��Top=�$��3��$��26��$��43���� -    ��Name$ connectButton��$��Połącz z bazą��$
��Left=�$��	10,22,0,0q�$	��Top=�$��95��$��5��$��20��$��1���� -	   $ cancelButton��$��Anuluj��$
��Left=�$��
125,22,0,0q�$	��Top=�$��75��$��5��$��20��$��1���� -
    ��Name$ checkBox��$��Odsłonięte znaki��$
��Left=�$��	11,10,0,0q�$	��Top=�$��1��$��4��$��15��$	��108���� -   $ visiblePassword��$
��Left=�$��23��$��10,4,0,0q�$
 Wrap=�$��TextBox��$	��Top=�$	��190��$��1��$��3��$y�Hidden=�G� -   	 G�Name$	 hiddenPassword��$
��Left=�$��10,4,0,0q�$��3��$	��Top=�$	��190��
 J� RenderTransformOrigin$
 -5.625,0.667&�$��23��$��1�� G� Password$ 
MHpvTL54TC��