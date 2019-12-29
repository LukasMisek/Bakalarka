# Bakalarka
Zatím funkční:
  - Samotný algoritmus
    - Nalezení nejkratší cesty
    - Algoritmus je nyní možné pusit pro libovolnou kombinaci měst
    - Mesta a objednavky maji jsou nyni tvoreny pro kazdy algoritmus zvlast (vytvorim 1x seznam objednavek a pro kazdou polozku v seznamu objednavek pustim algoritmus. Algoritmus potom pocita pouze s mesty, ktere jsou v objednavce)
    - Mesta se jiz nemuzou jmenovat "´´\/ a podobne znaky, ktere mohou zpusobit, ze vznikne netisknutelny retezec
    - Program nyni vypise skupiny objednavek a je schopen spocitat nejkratsi vzdalenosti. Nasledne oznaci nejlepsi ksupinu
    - Program má nyní k dispozici souřadnice měst (X a Y). Tyto souřadnice jsou posuny od rovníku (Longtitude a Latitude)
https://en.wikipedia.org/wiki/Longitude a https://en.wikipedia.org/wiki/Latitude
    - Seznam měst je použit z existujícího projektu 33dbdd na GitHub.com https://github.com/33bcdd/souradnice-mest
    - Přidána třída InputOutput. V této třídě jsou metody pro práci se soubory (načítání dat a ukládání)
    - Přidána třída Functions (pomocné funkce)
________________________________________________________________________________________________________________
    - Algoritmus je nyní možné pustit pro libovolnou sekvenci měst s různými parametry. Tohle je za cenu rychlosti, což je poměrně znát.
Bude potřeba to poupravit, protože nebudu chtít pustit algoritmus s různými parametry pro různé cesty.
________________________________________________________________________________________________________________
  - Jedinec:
    - Tvorba Jedince podle Třídy Cities
    - Křížení (2 rodiče)
    - Mutace (Záměna 2 genů)
    - FixMe (Odstranění duplicit)
  - Populace:
    - Tvorba z Jedinců
    - Turnaj - Selekce
    - Zlepšení populace
    - Get/Set
  - Cities
    - Načtení ze souboru
    - Tvorba náhodná
    - Uložení do souboru
  - Orders
    - Tvorba ze třídy Cities
    - Vytvoření s ohledem na Maximální kapacitu auta
    - Vytvoření kombinací měst
  - InputOutput
    - Načtení názvů měst a souřadnic ze vstupních souborů
    - Zároveň jsou vygenerovány soubory pro jednotlivé kraje (jménoKraje.txt)

Nutno dodělat:
  - Je potřeba celý program hezky zaobalit
    - Jsem schopen vygenerovat z měst kombinaci cest => je potřeba pro každou cestu umožnit spustit algoritmus HOTOVO
    - Třída/logika pro nahrávání vstupů. Inicializace a potom start algoritmu. HOTOVO
    - Je nutne nacitat mesta ze souboru a pracovat pouze s jednim objektem HOTOVO
      - Bude se pracovat s tabulkou allCitiesTable a použiji k filtraci sloupce Okres a Kraj
    - Musim vychazet pouze z jedne matice vzdalenosti -> budu tvorit podmatice podle prvku, ktere mam HOTOVO
      - Budu vycházet z tabulky, která bude ve třídě Controller. Následně budu počítat vzdálenost mezi 2 body. Matice vzdáleností je příliš velká.
    - Jiné kódování pro názvy měst (je nutno mít více než A-Za-z měst (místo charu bude použit string) nebo Byte? HOTOVO
    - Výpočty pomocí souřadnic a matice vzdáleností HOTOVO
    - Mozna bude vhodne jinak reprezentovat mesta. Takto jsem omezen na max cca 86 zakazniku (chary) HOTOVO
    - Nutno použít nové názvy (přímé názvy, nebo HEX reprezentace) HOTOVO
    ________________________________________________________________________________________________________________
    - Opět sjednotit Algoritmus a jeho parametry. Tohle bude stejné pro všechny 
    ________________________________________________________________________________________________________________
    - ZKULTURNIT ZKULTURNIT

  - GUI
    - Celé interface a celý program
    - Menu se vstupními parametry (města, počty generací ...)
    - Obrazovka s vykreslováním
    - Export dat a obrazu
    - Průběžné vykreslování
    - Finální podoba
    - Uživatelské ovládání  
  
  - 2. varianta tvorby rozvrhu (Volitelné)
    - Mám seznam měst a seznam priorit (kam jdu nejprve)
