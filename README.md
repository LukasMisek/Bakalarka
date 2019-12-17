# Bakalarka
Zatím funkční:
  - Samotný algoritmus
    - Nalezení nejkratší cesty
    - Algoritmus je nyní možné pusit pro libovolnou kombinaci měst
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
  
  - GUI
    - Průběžné vykreslování
    - Finální podoba
    - Uživatelské ovládání  
  
  - 2. varianta tvorby rozvrhu (Volitelné)
    - Mám seznam měst a seznam priorit (kam jdu nejprve)
