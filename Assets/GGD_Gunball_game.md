<div style="font-size: 38px">Game Design Document - Gunball</div><br>
<div style="font-size: 16px">Douwe Westerdijk - Rowan Pijnakker - Tarik Kurt - Jesper Rottiné</div>

---

### Inhoudsopgave
- [Inleiding](#inleiding)
- [Gameplay](#gameplay)
- [User Stories](#user-stories)
- [Visuals](#visuals)
- [Overige informatie](#overige-informatie)

### Inleiding

*Gunball* is een singleplayer game waar je met verschillende wapens goals moet scoren door tegen een bal aan te schieten. De speler krijgt een punt per goal. Na elke goal wordt de bal weer terug geplaatst in het spel en moet de speler opnieuw scoren. Er zijn een aantal verschillende wapens waarmee de speler kan spelen, ieder met zijn voor en nadelen.

### Gameplay

- **Snelle overview**

    De speler moet met verschillende wapens een bal het doel in schieten. De speler heeft een pistool, shotgun en een sniper. Het pistool schiet snel, maar heeft minder kracht dan de andere wapens. De shotgun kan de bal snel vooruit schieten, maar moet lang herladen, en de sniper doet veel schade en is accuraat, maar schiet traag.

- **Movement**

    De speler kan rondlopen, crouchen, sprinten, en springen. Met de muis kan de speler rondkijken en zijn wapens richten en met de linker en rechtermuis toetsen kan hij richten of schieten. De speler kan ook BHoppen voor geadvanceerde bewegingen.

- **De wapens**

    Er zijn drie wapen in *Gunball*:

    - Pistool<br>
        Het pistool heeft gemiddelde kracht en schiet gemiddeld snel. Het pistool heeft een magazijn van 16 kogels en kan semi-auto of full-auto schieten.

    - Shotgun<br>
        De shotgun heeft een magazijn van 6 shells. Elke shell schiet 6 aparte pellets, deze hebben een willekeurige spread. Door de spread zal de bal niet altijd exact bewegen in de richtig waar je richt.

    - Sniper<br>
        De sniper heeft veel kracht en is zeer accuraat, maar het heeft ook een trage fire rate. De sniper heeft een magazijn van 1 kogel.

- **De bal**

    De bal is ongeveer de grootte van een yogabal. De bal kan via de grond, muur en het plafond stuiteren en zo in het doel belanden. De speler kan de bal ook simpelweg het doel induwen.

### User Stories

- Als speler wil ik verschillende wapens kunnen gebruiken zodat ik kan experimenteren met verschillende speelstijlen.

- Als speler wil ik punten kunnen scoren door goals te maken.

- Als speler wil ik de bal kunnen zien stuiteren op verschillende oppervlakken.

- Als speler wil ik mijn wapen kunnen herladen zodat ik strategisch moet nadenken over wanneer ik schiet.

- Als speler wil ik kunnen rondlopen, crouchen, sprinten en springen zodat ik vrij kan bewegen en verschillende tactieken kan gebruiken.

- Als speler wil ik een duidelijke interface hebben die mijn score en munitie toont zodat ik altijd op de hoogte ben van mijn status in het spel.

### Audiovisual

- **De map**

    Onze map is geïnspireerd op de map van het spel *Rocket League*. Het is een rechthoekige oppervlakte met aan een kant een doel. Om dit rechthoek zit een vlakke muur die overgaat in een koepel als plafond.

- **De speler**

    *Gunball* wordt gespeeld vanuit een first-person perspectief, de speler heeft geen skin, de speler heeft een capsule-shaped hitbox.

- **De bal**

    De bal is gekleurd en ongeveer de helft zo groot als de speler.

- **De wapens**

    De wapens hebben hun eigen modellen en lijken op een generieke versie van wat ze horen te zijn.<br>
    Elk wapen heeft zijn eigen schiet en reload geluidseffect die afspeelt tijdens het schieten / herladen.

### Overige Informatie

- **AI**

    Er is geen AI in dit spel. De speler is het enige karakter op in het spel, en de bal heeft geen eigen AI.

- **Menu scherm**

    *Gunball* heeft geen start of menu scherm. Wanneer je het spel opstart zit je meteen in het spel.
