# Calc2

Analys av programmet: 
// Analys av meny
Jag valde att bygga mitt program med en huvudmeny istället för att ställa frågor som användaren ska svara (Y/N) på. Jag tycker att det blir
enklare att navigera i programmet då och man är inte lika låst till ett statiskt flöde. Det jag skulle kunnat göra är att ändra så att man
istället navigerar med piltangenterna och enter, då hade jag inte behövt skriva ut "Press 1", "Press 2" etc, utan istället hade jag då kunnat
highlighta det valet som markören är på. Det hade jag löst så här:

Skapa en array av menyval och när man skriver ut menyn skriver använder man choice för att skriva ut aktuellt index i en annan färg
int choice = 0

var keyPress = Console.ReadKey(true);
while(true)
{
	switch (keyPress.Key)
	{
		case ConsoleKey.UpArrow: choice--; 
		break;

		case ConsoleKey.DownArrow: choice++;
		break;

		case ConsoleKey.Enter: köra alternativ choice från menyn och breaka från while-loopen.
		break;
	}
}

Det behövs även ett if statement för att kontrollera så att choice inte blir högre en antalet menyval, och även lägga in en wraparound så att 
om man står längst ner i menyn och trycker down arrow så hamnar man längst upp i menyn.
-------------------------------------------------------------------------------------------------------------------------------------------
// Metoder
Jag försökt bryta ner allting i metoder för att koden skulle bli mer lättläst, men jag tror att jag hade kunnat göra det bättre. Speciellt 
metoden som heter Calculate tror jag hade varit bra att bryta ner i mindre delar, jag tror att den kan vara lite svår att läsa för någon
som tittar på koden för första gången. 

Med facit i hand tror jag att jag borde ha gjort en class där jag hade alla metoder för att det skulle bli mindre kod i Program.cs. Det skulle
även underlätta med en class (eller flera), när man ska bygga vidare på programmet i framtiden. Min plan är att refactor koden efter inlämning
för att jag inte ska skapa några problem som jag inte hinner lösa.

-------------------------------------------------------------------------------------------------------------------------------------------
Förslag på vidareutveckling:
* Lägga till en meny för avancerad matematik. Tex potenser, räkna roten av, area på diverse figurer. Detta skulle vara ganska enkelt att
lägga till. Jag hade börjat med att lägga till menyvalet i huvudmenyn, och sedan hade jag skapat en undermeny där man väljer vilken
typ av operation man vill göra. För att räkna ut potenser skulle jag använda mig av Math.Pow(number, exponent) och för roten skulle jag 
använvda Math.Sqrt()-metoden. 
För att räkna ut arean hade jag först låtit användaren välja vilken typ av figur som ska beräknas, och sedan bett användaren om input
beroende på vad som krävs för formen. Jag hade använt mig av en metod för varje figur Tex:

Kvadrat:
Mata in en sidas längd
returnera "Arean på en kvadrat där ena sidan är x cm, är x * x cm2".

Triangle:
Mata in basen och höjden
returnera "Arean på en triangel där base är x cm och höjden är y cm, är (x * y) / 2 cm2".

Cirkel
Mata in cirkelns radie
returnera "Arean på en cirkel där radien är x cm, är x * x * Math.PI cm2"

* Nästa funktion som jag skulle vilja prova att lägga till är en enkel grafritare. Tanken är att användaren då ska kunna mata in en formel
som sedan ritar ut en graf. Jag vet inte riktigt hur jag ska lösa det visuellt så därför har jag inte testat att implementera detta ännu.

* Det skulle även vara bra att försöka spara historiken till en fil, så att inte historiken försvinner när man stänger av programmet.
Då skulle man kunna ge användaren rensa historiken när som helst, och även lägga till en maxgräns, tex att det inte får finns fler en 50st 
uträkningar i historiken, och när man gör nya uträkningar så försvinner den äldsta i listan och den nya läggs till.

* Det skulle vara bra att lägga till stöd för parenteser om det är något speciellt matematiskt problem som ska lösas.

-------------------------------------------------------------------------------------------------------------------------------------------
Över lag är jag nöjd med programmet då jag lyckades fixa så att användaren kan mata in hur många tal och operatorer som den önskar, på en rad
utan att behöva tänka på prioriteringsregler. Jag har under projektet lärt mig mycket som jag kommer ha nytta av framöver!