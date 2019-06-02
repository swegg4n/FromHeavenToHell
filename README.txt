Spelet spelas med mus/tangentbord och en handkontroll eller med två handkontroll.
Spelet måste startas via "MainMenu"-scenen.

Om man vill ändra värden på spelarkaraktärerna eller fienderna så gör man det via deras prefabs.
Rummen hittar man enklast via "Main scene" om man vill ändra på dem.
Där finns "SerializedFields" för alla relevanta värden att ändra. De kommer då synas under det tillhörande scriptet.
Till exempel i "GameManager"-objektet i "Main Scene" under "Player Manager"-scriptet finns variablen
"Health" som ändrar spelarnas livvärde.

Efter spelet startas trycker man på "Play" för att spela. Då får man först välja hur karaktärerna ska styras.
Efter det startar man i startrummet. Om man trycker på 'P' öppnas pausmenyn där man kan navigera till 
"Game info" skärmen för att se relevant information om spelet.

Med mus/tangentbord styr man karaktären med 'w', 'a', 's', och 'd'. Man siktar med musen.
Med handkontroll styr man karaktären med vänster spak och siktet med höger spak.

Git repository:
https://github.com/swegg4n/FromHeavenToHell.git