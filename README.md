# TodoList

## Do Uruchomienia aplikacji proszę wykonać następujące kroki:

1. Pobrać i zainstalować lokalnie Docker.Desktop
    https://www.docker.com/products/docker-desktop/

2. Uruchomić Docker.Desktop

3. Ze strony https://hub.docker.com/r/microsoft/mssql-server pobrać najnowszy obraz Microsoft SQL Server.
   Aby to zrobić proszę uruchomić komendę w powershel lub cmd najlepiej będąc w trybie administratora:
   #### docker pull mcr.microsoft.com/mssql/server:2022-latest

4. Proszę uruchomić kontener poleceniem (również z powershel badz cmd):
   #### docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d --name sqlserver mcr.microsoft.com/mssql/server:2022-latest

 5. Następnie proszę uruchomić GitBash w folderze gdzie w którym ma zostać pobrane repozytorium
	(w przypadku braku GitBash mozna go pobrać https://git-scm.com/downloads)
	uruchomić komendę:
    #### git clone https://github.com/ortofone24/TodoList.git

  6. Następnie mozna otworzyc solucję projektu za pomocą Visual Studio 2022

  7. Przed uruchomieniem solucji w Visual Studio należy ustawić aby odpaliły się 2 projekty jednocześnie
	 - Klikając prawym przyciskiem na solucji wybrać properties
	 - wybrać Multiple startup projects i ustawić TodoList.Api -> Start oraz TodoList.Web->Start
	 - teraz mozemy uruchomić przycisk Start
	    w jednym oknie w tle odpali się api i wyswietli strona swagger a w drugim oknie będzie Aplikacja do użytkowania
    
![1](https://github.com/user-attachments/assets/ad939cf5-bf98-495b-a15a-4b27ef3e1a91)

![2](https://github.com/user-attachments/assets/129e074b-8207-46b1-a5c2-0d2af4959433)

![3](https://github.com/user-attachments/assets/7d47cc5b-1edf-4ec0-bcfc-37bd2abba184)


 8. Alternatywnie można uruchomić solucje w Visual Studio i ustawic TodoList.Api jako "Set As Startup Project" i uruchomić,
	A w drugiej instancji Visual Studio ustawic TodoList.Web jako "Set As Startup Project" i również uruchomić.

# Miłego testowania !!!
