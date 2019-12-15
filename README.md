# Bakalarka
Zatím funkční:
  - Samotný algoritmus
    - Nalezení nejkratší cesty
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
    
Nutno dodělat:
  - Algoritmus potřebuje třídu Cities, ze které vygeneruje města a na těch hledá nejkratší cestu
    - Algoritmus musí přebírat vstup (města a z těch vytvořit matici vzdáleností a seznam měst) Array => List
    - OOP OBECNOST, PREPOUZITELNOST
  
  - GUI
    - Průběžné vykreslování
    - Finální podoba
    - Uživatelské ovládání  
  
  - 2. varianta tvorby rozvrhu (Volitelné)
    - Mám seznam měst a seznam priorit (kam jdu nejprve)
