# Bakalarka
Zatím funkční:
  - Samotný algoritmus
    - Nalezení nejkratší cesty
    - Algoritmus je nyní možné pusit pro libovolnou kombinaci měst
    - Mesta a objednavky maji jsou nyni tvoreny pro kazdy algoritmus zvlast (vytvorim 1x seznam objednavek a pro kazdou polozku v seznamu objednavek pustim algoritmus. Algoritmus potom pocita pouze s mesty, ktere jsou v objednavce)
    - Mesta se jiz nemuzou jmenovat "´´\/ a podobne znaky, ktere mohou zpusobit, ze vznikne netisknutelny retezec
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
    
Nutno dodělat:
  - Je potřeba celý program hezky zaobalit
    - Jsem schopen vygenerovat z měst kombinaci cest => je potřeba pro každou cestu umožnit spustit algoritmus
    - Třída/logika pro nahrávání vstupů. Inicializace a potom start algoritmu.
    - Mozna bude vhodne jinak reprezentovat mesta. Takto jsem omezen na max cca 86 zakazniku (chary)
    - ZKULTURNIT
  
  - GUI
    - Průběžné vykreslování
    - Finální podoba
    - Uživatelské ovládání  
  
  - 2. varianta tvorby rozvrhu (Volitelné)
    - Mám seznam měst a seznam priorit (kam jdu nejprve)
