# DiplomaApp
Aplikacja stworzona do przygotowywania PDF'ów z imieniem i nazwiska zawodnika. Program pobiera dane z bazy danych i na podstawie pobranych rekordów generuje dyplomy.

W tym celu wykorzystany jest zewnętrzny serwer.

Jak działa aplikacja?
1. Wpierw łączymy się z bazą danych (dane są wpisane w apkę, żeby nie było problemów z łączeniem). Baza jest akurat fikcyjna, utworzona na cele tego projektu.
2. Wybieram plik PDF, któr posłuży nam jako tło dyplomu (plik uczestnictwo.pdf).
2a. Po otworzeniu pliku, pojawi się na podglądzie label "imię i nazwisko", przesuwam je w dowolne miejsce, w którym ma się pojawić imię i nazwisko uczestnika na dyplomie.
3. Wybieramy folder, w którym będą przechowywane dyplomy.
4. Generujemy pliki PDF.

Jako szansę rozwoju projektu widzę dodanie automatycznego wysyłania dyplomów na maile.
