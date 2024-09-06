Aplikacja SmartERP.ModuleEditor jest częścią systemu SmartERP i służy do tworzenia modułów i encji w obrębie systemu.

Aplikacja oprócz aplikacji głównej (SmartERP.ModuleEditor.ReactiveUI) stanowiącej interfejs UI wykonany przy użyciu platformy AvaloniaUI składa się z następujących autorskich bibliotek (stanowiących paczki NuGet dostępne na nuget.org):
- NiceToDev.CommandLine - biblioteka służąca do kolejkowania, uruchamiania i logowania poleceń wiersza poleceń
- NiceToDev.ProjectGenerator - biblioteka generująca rozwiązania i projekty w oparciu o obiekty klas SolutionInfo i ProjectInfo
- SmartERP.CommonTools - zestaw narzędzi przydatych w rozmaitych aplikacjach ASP.NET zawierający między innymi bazowe (uniwersalne) klasy dla repozytoriów i serwisów, obsługę konfiguracji, autoryzacji oraz podstawowe rozszerzenia podstawowych klas
- SmartERP.Development - biblioteka zawierająca podstawową logikę bazodanową związaną z obsługą aplikacji i jej podstawowych encji

Jak zacząć:
1. W klasie SmartERP.ModuleEditor.ReactiveUI.Static.DependencyResolver należy przy wywołaniu metody BuildServiceProvider wprowadzić Connection String do własnej bazy danych
2. Przeprowadzić migrację kontekstu z projektu SmartERP.Development.Database lub uruchomić skrypt SmartERP_Database_Create.sql z katalogu SQL tego projektu
3. Uruchomić aplikację utworzyć nowy moduł klikając w lewym górnym rogu przycisk Add
4. Wypełnić odpowiednio pola modułu 
5. Dodać encje klikając Add Entity, wypełniając pola encji, dodając pola przez Add Field a następnie zapisując klikając przycisk Save w prawym dolnym rogu okna
6. Krok można powtarzać aż do uzyskania satysfakcjonującej ilości encji
7. Kliknąć przycisk Save w prawym górnym rogu okna spowoduje zapisanie encji
8. Kliknąć przycisk Generate w lewym górnym rogu okna - spowoduje to wygenerowanie w katalogu z aplikacją (bin) w folderze Modules solucji oraz projektów wybranego modułu

Uwagi:
- trzeba uważnie wypełniać pola formularza, ponieważ nie mają one żadnej walidacji
- aplikacja ma charakter poglądowy - nie stanowi gotowego rozwiązania

Dalszy rozwój:
- aplikacja będzie rozwijana z użyciem innego interfejsu (ASP.NET lub NET MAUI) - stąd jej modułowa budowa i całość logiki umieszczona w odrębnym, niezależnym projekcie stanowiącym paczkę NuGet (SmartERP.Development) 
- aplikacja oprócz generowania bibliotek będzie generowała również API oraz Front End (najpewniej w technologii VueJS)



