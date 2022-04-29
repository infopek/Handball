
# Handball

## Description
Small handball simulation written in C# for a second semester project in university. No inputs for this project. The task itself is written in Hungarian, everything else is in English.

## Task
Egy olyan programot kell készítenünk, amely segít szimulálni egy teljes kézilabda
meccs menetét.

Ehhez szükséges osztályok és interfészek:
<u>**IJátékos** interfész:</u>
- Egy játékost ír le, legyen egy **név**, **életkor** és egy **csapat** tulajdonsága.
- Ezen felül rendelkezzen még **erő**, **gyorsaság** és **állóképesség**
tulajdonságokkal.

<u>Minden **pozíciónak** külön osztály:</u> *(lövő, szélső, irányító, beálló, kapus)*
- Az összes osztály megvalósítja az **Ijátékos** interfészt.
- Ezen felül a kapusnak hozzon létre egy **védés** tulajdonságot, ami a kapus
sikeres védéseit számolja.
- Valamint hozzon létre a többi pozíciónak egy **gól** tulajdonságot, ami pedig a
lőtt gólok számát tárolja.

<u>**Csapat** osztály</u>:
- Rendelkezik egy név tulajdonsággal, valamint egy játékosokból álló tömbbel.
- Legyen lehetőség felvenni, törölni játékosokat, illetve listázni őket.
- Egy csapatban minimum 10 játékosnak kell lennie, maximum pedig 16 lehet.
- Mivel több csapatot is létrehozhatunk, ezért ezeket tároljuk el egy saját
készítésű láncolt listában név szerint rendezve.

Ezután hozza létre a **Mérkőzés** osztályt, ahol készítse el a mérkőzés szimulálásához
szükséges metódusokat és tulajdonságokat:
*(Egy mérkőzést két csapat játszik, a jatéktéren egy csapatból legfeljebb 7 játékos tartózkodhat,
amelyből 1 játékosnak kötelezően kapusnak kell lennie.)*
1. Hozzon létre mindkét csapat számára egy **cserepad** és **játéktér** tömböt.

2. Legyen egy metódus, amivel el lehet kezdeni a mérkőzést. A metódus vizsgálja,
hogy mindkét csapat megfelelő létszámmal van-e jelen. Ha nem akkor dobjunk
el egy <u>saját készítésű kivételt</u>. Majd mindkét csapat játékosok tömbjéből
válasszunk ki 7-7 játékost, akik a kezdőcsapatot fogják alkotni.

3. Erre készítsünk egy külön metódust, amely <u>visszalépéses keresés</u> segítségével
meghatározza a játékosok **erő**, **gyorsaság** és **állóképesség** tulajdonságaik
alapján, hogy melyik 7 játékos fog bekerülni a kezdőcsapatba.
A lényeg, hogy minél nagyobb legyen a <u>tulajdonságok összértéke</u>. Másodlagos
szempont, hogy minden pozícót ki kell tölteni. Egyszerre a pályán egy
csapatból 1 kapus, 1 beálló, 1 irányító, 2 szélső és 2 lővő tartózkodhat.
*(**Fontos**, hogy a kiválasztott játékosok nem szerepelhetnek a cserepad
tömbben.)*

4. Ezután készítsük el a **Szimuláció** metódust. Ennek az elején kell majd meghívni
a **Start** metódust. Egy mérkőzés 60 másodpercből fog állni, a mérkőzés
eseményei *(Pl.: Gól, védés, sérülés, sárga lap, piros lap)* véletlenszerűen fognak
előfordulni. Egy ciklusban vizsgáljuk, hogy elértük-e a 60 másodpercet és
mielőtt növelnénk a ciklusváltozót várunk 1 másodpercet.
*(System.Threading.Thread.Sleep(1000)*

5. Minden 15.-ik másodpercben 3 játékost kell cserélni. Véletlenszerűen a
válasszuk ki a játéktéren lévők közül a cseréket. Majd a cserepadról
<u>visszalépéses keresés</u> segítségével azt a 3 embert válasszuk ki, akikkel a
játokosok <u>tulajdonságainak az összértéke</u> a legnagyobb lesz.
**Fontos**, hogy például a játéktérről lecserélt játékosok már ne szerepeljenek a
játéktér tömbben. Ugyanez igaz fordítva is.
6. Ha egy játékos gólt lőtt vagy a kapus védett egy lövést, akkor növeljük a hozzá
tartozó számlálót eggyel.

7. Minden mérkőzésen történt eseményt *(Pl.: Gól, védés, sérülés, sárga lap, piros
lap)*, írjon ki a konzolra eseménykezeléssel.
8. Valamint használjon kivételkezelést az előforduló hibák esetén. *(Pl.: Nincs elég
játékos, létezik már ilyen nevű csapat, stb.)*

## Requirements
- C# 8.0
