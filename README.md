# Calc2

Analys av programmet: 
// Analys av meny
Jag valde att bygga mitt program med en huvudmeny ist�llet f�r att st�lla fr�gor som anv�ndaren ska svara (Y/N) p�. Jag tycker att det blir
enklare att navigera i programmet d� och man �r inte lika l�st till ett statiskt fl�de. Det jag skulle kunnat g�ra �r att �ndra s� att man
ist�llet navigerar med piltangenterna och enter, d� hade jag inte beh�vt skriva ut "Press 1", "Press 2" etc, utan ist�llet hade jag d� kunnat
highlighta det valet som mark�ren �r p�. Det hade jag l�st s� h�r:

Skapa en array av menyval och n�r man skriver ut menyn skriver anv�nder man choice f�r att skriva ut aktuellt index i en annan f�rg
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

		case ConsoleKey.Enter: k�ra alternativ choice fr�n menyn och breaka fr�n while-loopen.
		break;
	}
}

Det beh�vs �ven ett if statement f�r att kontrollera s� att choice inte blir h�gre en antalet menyval, och �ven l�gga in en wraparound s� att 
om man st�r l�ngst ner i menyn och trycker down arrow s� hamnar man l�ngst upp i menyn.
-------------------------------------------------------------------------------------------------------------------------------------------
// Metoder
Jag f�rs�kt bryta ner allting i metoder f�r att koden skulle bli mer l�ttl�st, men jag tror att jag hade kunnat g�ra det b�ttre. Speciellt 
metoden som heter Calculate tror jag hade varit bra att bryta ner i mindre delar, jag tror att den kan vara lite sv�r att l�sa f�r n�gon
som tittar p� koden f�r f�rsta g�ngen. 

Med facit i hand tror jag att jag borde ha gjort en class d�r jag hade alla metoder f�r att det skulle bli mindre kod i Program.cs. Det skulle
�ven underl�tta med en class (eller flera), n�r man ska bygga vidare p� programmet i framtiden. Min plan �r att refactor koden efter inl�mning
f�r att jag inte ska skapa n�gra problem som jag inte hinner l�sa.

-------------------------------------------------------------------------------------------------------------------------------------------
F�rslag p� vidareutveckling:
* L�gga till en meny f�r avancerad matematik. Tex potenser, r�kna roten av, area p� diverse figurer. Detta skulle vara ganska enkelt att
l�gga till. Jag hade b�rjat med att l�gga till menyvalet i huvudmenyn, och sedan hade jag skapat en undermeny d�r man v�ljer vilken
typ av operation man vill g�ra. F�r att r�kna ut potenser skulle jag anv�nda mig av Math.Pow(number, exponent) och f�r roten skulle jag 
anv�nvda Math.Sqrt()-metoden. 
F�r att r�kna ut arean hade jag f�rst l�tit anv�ndaren v�lja vilken typ av figur som ska ber�knas, och sedan bett anv�ndaren om input
beroende p� vad som kr�vs f�r formen. Jag hade anv�nt mig av en metod f�r varje figur Tex:

Kvadrat:
Mata in en sidas l�ngd
returnera "Arean p� en kvadrat d�r ena sidan �r x cm, �r x * x cm2".

Triangle:
Mata in basen och h�jden
returnera "Arean p� en triangel d�r base �r x cm och h�jden �r y cm, �r (x * y) / 2 cm2".

Cirkel
Mata in cirkelns radie
returnera "Arean p� en cirkel d�r radien �r x cm, �r x * x * Math.PI cm2"

* N�sta funktion som jag skulle vilja prova att l�gga till �r en enkel grafritare. Tanken �r att anv�ndaren d� ska kunna mata in en formel
som sedan ritar ut en graf. Jag vet inte riktigt hur jag ska l�sa det visuellt s� d�rf�r har jag inte testat att implementera detta �nnu.

* Det skulle �ven vara bra att f�rs�ka spara historiken till en fil, s� att inte historiken f�rsvinner n�r man st�nger av programmet.
D� skulle man kunna ge anv�ndaren rensa historiken n�r som helst, och �ven l�gga till en maxgr�ns, tex att det inte f�r finns fler en 50st 
utr�kningar i historiken, och n�r man g�r nya utr�kningar s� f�rsvinner den �ldsta i listan och den nya l�ggs till.

* Det skulle vara bra att l�gga till st�d f�r parenteser om det �r n�got speciellt matematiskt problem som ska l�sas.

-------------------------------------------------------------------------------------------------------------------------------------------
�ver lag �r jag n�jd med programmet d� jag lyckades fixa s� att anv�ndaren kan mata in hur m�nga tal och operatorer som den �nskar, p� en rad
utan att beh�va t�nka p� prioriteringsregler. Jag har under projektet l�rt mig mycket som jag kommer ha nytta av fram�ver!