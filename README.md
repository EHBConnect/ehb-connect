
# EhB Connect

## 1. Projectbeschrijving

EhB Connect is een IT-project van studenten Toegepaste Informatica aan de Erasmushogeschool Brussel (EhB), academiejaar 2026–2027.

Het doel is studenten en docenten van verschillende opleidingen en campussen met elkaar in contact te brengen op basis van hun competenties.

EhB Connect wordt ontwikkeld als een responsive webapplicatie, bruikbaar op laptop en smartphone.

## 2. Kernfunctionaliteiten

De applicatie voorziet de volgende functionaliteiten:

- Registratie en authenticatie.
- Aanmaken en beheren van gebruikersprofielen.
- Selecteren van aangeboden en gezochte competenties.
- Publiceren van projectoproepen, hulpvragen en hulpaanbiedingen.
- Zoeken en filteren op competenties.
- Automatisch voorstellen van geschikte matches.
- Contactverzoeken versturen en beantwoorden.
- Samenwerkingsafspraken bevestigen.

Matching gebeurt op basis van een eenvoudig, regelgebaseerd algoritme. Er wordt geen AI gebruikt voor het berekenen van matches.

Deze functionaliteiten worden stapsgewijs ontwikkeld en zijn nog niet allemaal geïmplementeerd.

## 3. Projectstatus

Het project bevindt zich momenteel in ontwikkeling.

De actuele status kan worden gevolgd via GitHub en het Trello-taskboard.

## 4. Technologieën

### 4.1 Ontwikkelomgeving

| Onderdeel | Versie / technologie |
|---|---|
| IDE | Visual Studio 2026 |
| Node.js | v22.23.4 |
| npm | 10.9.9 |
| Angular CLI | 20.1.x volgens package.json |
| .NET SDK | .NET 10 vereist |
| Versiebeheer | Git en GitHub |
| Projectplanning | Trello |
| UI/UX-ontwerp | Figma |
| Diagrammen | draw.io |

De Node.js- en npm-versies hierboven zijn de versies die lokaal door het projectteam worden gebruikt.

De exacte Angular CLI-versie kan worden gecontroleerd met:

```bash
npx ng version
```

De lokaal geïnstalleerde versie kan afwijken van de versie in de projectconfiguratie.

### 4.2 Frontend

De frontend is gebouwd met Angular.

| Onderdeel | Technologie / vaststelling |
|---|---|
| Framework | Angular 20.1.x |
| Programmeertaal | TypeScript 5.8.x |
| Routing | Angular Router |
| HTTP-communicatie | Angular HttpClient |
| Frontend build | `npm run build` of `ng build` |
| Ontwikkelserver | `npm start` |
| HTTPS | Geconfigureerd voor lokale ontwikkeling |
| Testframework | Jasmine en Karma |
| Frontend-backend proxy | Proxyconfiguratie aanwezig |
| Styling | CSS |
| Architectuur | Single Page Application (SPA) |

De frontend bevindt zich in:

`ehb-connect.client/`

De Angular-applicatie maakt gebruik van componenten, routing en services om met de backend te communiceren.

### 4.3 Backend

De backend wordt ontwikkeld met ASP.NET Core.

| Onderdeel | Technologie / vaststelling |
|---|---|
| Framework | ASP.NET Core |
| .NET-versie | .NET 10 |
| Programmeertaal | C# |
| API-architectuur | REST API |
| Gegevensformaat | JSON |
| API-documentatie | OpenAPI |
| Database | Relationele SQL-database (gepland) |
| ORM | Entity Framework Core (gepland) |
| Authenticatie | ASP.NET Core Identity (gepland) |
| Matching | Regelgebaseerde MatchingService (gepland) |

De backend bevindt zich in:

`ehb-connect.Server/`

De backend is verantwoordelijk voor de businesslogica, authenticatie, autorisatie, gegevensverwerking en matching.

## 5. Architectuur

EhB Connect gebruikt een klassieke client-serverarchitectuur.

```text
               Gebruiker
                   |
                   v
          Angular Frontend
            (TypeScript)
                   |
                   | HTTP / JSON
                   v
          ASP.NET Core API
                (C#)
                   |
                   v
          Entity Framework Core
               (gepland)
                   |
                   v
          Relationele database
               (gepland)
```

De frontend verzorgt de gebruikersinterface en communiceert via REST-endpoints met de backend.

De backend verwerkt aanvragen, voert de businesslogica uit en beheert de gegevens.

De database zal onder andere gebruikers, competenties, projecten en contactverzoeken bevatten.

## 6. Repositorystructuur

.... 

### Belangrijkste onderdelen

**ehb-connect.client**

Bevat de Angular-applicatie, frontendcomponenten, routing, styling en configuratie.

**ehb-connect.Server**

Bevat de ASP.NET Core-backend en de configuratie van de Web API.

**docs**

Bevat aanvullende projectdocumentatie, waaronder beveiligingsdocumentatie en technische beslissingen.

**ehb-connect.slnx**

Visual Studio-solution waarin frontend en backend worden samengebracht.

## 7. Lokaal ontwikkelen

### 7.1 Vereisten

Voor lokale ontwikkeling zijn minimaal nodig:

- Visual Studio 2026 met ondersteuning voor .NET- en webontwikkeling.
- .NET 10 SDK.
- Node.js v22.23.4.
- npm 10.9.9.
- Git.

### 7.2 Repository klonen

```bash
git clone https://github.com/EHBConnect/ehb-connect.git
```

Open vervolgens de solution:

`ehb-connect.slnx`

in Visual Studio 2026.

### 7.3 Frontend-afhankelijkheden installeren

Open een terminal in de frontendmap:

```bash
cd ehb-connect.client
npm install
```

Voor een reproduceerbare installatie op basis van het bestaande lockbestand kan ook `npm ci` worden gebruikt.

### 7.4 Frontend starten

```bash
npm start
```

Dit start de Angular-ontwikkelserver met de HTTPS-configuratie die voor de ASP.NET Core-integratie is voorzien.

Bij de eerste uitvoering kan het nodig zijn om het lokale HTTPS-ontwikkelcertificaat te configureren of te vertrouwen.

### 7.5 Frontend bouwen

```bash
npm run build
```

De gecompileerde frontend wordt opgeslagen in de geconfigureerde build-outputmap.

### 7.6 Unit tests uitvoeren

```bash
npm test
```

De frontend gebruikt hiervoor Jasmine en Karma.

### 7.7 Backend starten

De backend kan worden gestart vanuit Visual Studio met de geschikte debugconfiguratie.

De ASP.NET Core-backend en Angular-frontend zijn geïntegreerd in dezelfde Visual Studio-solution.

## 8. Git- en GitHub-workflow

Voor versiebeheer wordt GitHub gebruikt.

Ontwikkelaars werken niet rechtstreeks op de `main`-branch.

### Werkwijze

1. Haal de meest recente wijzigingen van `main` op.
2. Maak een nieuwe feature- of documentatiebranch.
3. Voer de wijzigingen uit.
4. Test de wijzigingen lokaal.
5. Commit met een duidelijke commitboodschap.
6. Push de branch naar GitHub.
7. Maak een pull request naar `main`.
8. Laat een ander teamlid de wijzigingen reviewen.
9. Merge na goedkeuring en de vereiste controles.

### Voorbeelden van branchnamen

```text
feature/login-backend
feature/profile-frontend
feature/matching
docs/project-readme
fix/login-validation
```

### Voorbeelden van commitboodschappen

```text
docs: add project README
feat: add student profile form
feat: implement skill management API
fix: correct login validation
test: add matching service unit tests
```

Pull requests worden gebruikt om wijzigingen te bespreken, te controleren en goed te keuren voordat ze op `main` terechtkomen.

## 9. Beveiliging

Bij de ontwikkeling wordt rekening gehouden met:

- Beveiligde authenticatie en autorisatie.
- HTTPS voor netwerkcommunicatie.
- Server-side validatie van gebruikersinvoer.
- Bescherming tegen SQL-injectie en XSS.
- Correct beheer van wachtwoorden en secrets.
- Minimale verwerking van persoonsgegevens.
- Veilige GitHub- en ontwikkelworkflows.
- GDPR/AVG-principes.

Voor de MVP wordt gewerkt met fictieve accounts en testgegevens.

De risico's en geplande beveiligingsmaatregelen worden beschreven in de afzonderlijke Security Risk Assessment.

## 10. Projectdocumentatie

Het project beschikt over aanvullende analysedocumentatie:

- Probleemanalyse en Design Thinking.
- Personas en user stories.
- Flowcharts en wireframes.
- MVP-afbakening.
- Agile backlog en acceptatiecriteria.
- Databaseontwerp en klassendiagram.
- API-documentatie.
- Security Risk Assessment.
- Technical Feasibility & Risk Assessment.
- Technology Impact Cycle Tool (TICT).

De projectdocumentatie wordt verder aangevuld tijdens de ontwikkeling.

## 11. Planning

Het project wordt ontwikkeld tijdens het academiejaar 2026–2027.

De voorziene deadline voor de MVP is **8 januari 2027**.

De ontwikkeling verloopt iteratief via korte sprints, waarbij frontend, backend en database stapsgewijs geïntegreerd worden.

## 12. Team

**IT-Project (WT) 2026–2027 – Groep 2**

Erasmushogeschool Brussel  
Graduaat Toegepaste Informatica

---

*Deze README wordt bijgewerkt naarmate de ontwikkeling van EhB Connect vordert. Onderdelen die als gepland worden aangeduid, zijn nog niet noodzakelijk geïmplementeerd.*
