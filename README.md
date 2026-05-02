# interactive-learning

`interactive-learning` est une application web interactive pour apprendre C#, SQL / SQL Server et PHP / Symfony avec des cours progressifs, un editeur Monaco, des corrections automatiques, des XP, des niveaux, des badges, des Monstres intermediaires et un Boss Final par parcours.

Le nom public du projet est `interactive-learning`. Les dossiers backend conservent encore le nom technique historique `CSharpInteractive.*` pour eviter un renommage de namespace plus large.

## Structure

```text
frontend/                       Next.js + TypeScript + TailwindCSS + Monaco Editor
backend/CSharpInteractive.Api/  ASP.NET Core Web API + EF Core SQLite + Roslyn
backend/CSharpInteractive.Api.Tests/  Tests backend xUnit
backend/legacy-express/         Ancien prototype Express archive
PLAN.md                         Architecture et plan d'implementation
TODO.md                         Checklist d'avancement
```

## Installation et lancement

### Prerequis

- Node.js 20 ou plus recent
- npm
- SDK .NET 8

SQL Server Docker est optionnel. Le projet fonctionne sans lui grace au moteur SQL pedagogique en memoire.

Le parcours PHP / Symfony ne requiert pas de runtime PHP local en v1: les exercices sont valides par criteres statiques pedagogiques cote backend.

### Lancer le projet

La maniere recommandee est de lancer le backend et le frontend dans deux terminaux separes depuis la racine du depot.

Avant de commencer, verifier que les ports sont libres :

```bash
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5000/api/courses
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:3000
```

Si une commande retourne `200`, le service correspondant tourne deja.

#### Option A - Lancement local avec le SDK .NET

Terminal 1 - API backend :

```bash
cd backend/CSharpInteractive.Api
dotnet restore
dotnet run
```

L'API demarre sur `http://localhost:5000` et expose ses routes sous `http://localhost:5000/api`.

Terminal 2 - application web :

```bash
cd frontend
npm install
npm run dev
```

Le package npm du frontend s'appelle `interactive-learning`.

Ouvrir ensuite `http://localhost:3000` dans le navigateur.

Par defaut, le frontend appelle l'API sur `http://localhost:5000/api`. Si l'API utilise une autre URL, lancer le frontend avec :

```bash
NEXT_PUBLIC_API_BASE_URL=http://localhost:5000/api npm run dev
```

#### Option B - Backend via Docker si `dotnet` n'est pas installe

Utiliser cette option si `dotnet --info` ne fonctionne pas sur la machine, mais que Docker est disponible.

Depuis la racine du depot :

```bash
docker rm -f interactive-learning-api 2>/dev/null || true
docker create \
  --name interactive-learning-api \
  -w /src/CSharpInteractive.Api \
  -p 5000:5000 \
  -e ASPNETCORE_URLS=http://0.0.0.0:5000 \
  mcr.microsoft.com/dotnet/sdk:8.0 \
  sh -c 'dotnet restore /src/CSharpInteractive.sln && dotnet run --no-restore'
docker cp backend/. interactive-learning-api:/src
docker start interactive-learning-api
```

Attendre quelques secondes, puis verifier :

```bash
curl -s http://localhost:5000/api/courses
```

La reponse doit lister les trois parcours `csharp-foundations`, `sqlserver-foundations` et `php-symfony`.

Dans un deuxieme terminal, lancer le frontend :

```bash
cd frontend
npm install
NEXT_PUBLIC_API_BASE_URL=http://localhost:5000/api npm run dev
```

Ouvrir ensuite `http://localhost:3000`.

Pour voir les logs du backend Docker :

```bash
docker logs -f interactive-learning-api
```

Pour arreter le backend Docker :

```bash
docker rm -f interactive-learning-api
```

### Lancement rapide apres installation

Une fois les dependances restaurees, les commandes quotidiennes sont :

```bash
cd backend/CSharpInteractive.Api
dotnet run
```

```bash
cd frontend
npm run dev
```

## Frontend

Le frontend est une application Next.js situee dans `frontend/`. Monaco est configure selon le parcours actif: C#, SQL ou PHP.

Commandes utiles :

```bash
cd frontend
npm run dev
npm run lint
npm run build
```

## Backend

Le backend actif est une API ASP.NET Core situee dans `backend/CSharpInteractive.Api/`.

Commandes utiles :

```bash
cd backend/CSharpInteractive.Api
dotnet restore
dotnet run
```

L'API expose Swagger en environnement de developpement.

La base SQLite `interactive-learning.db` est creee automatiquement via EF Core `EnsureCreated` au premier demarrage.

## Parcours disponibles

1. C#.
2. SQL / SQL Server.
3. PHP / Symfony.

Chaque parcours possede ses modules, lecons, exercices, Monstres intermediaires, badges, progression dans le parcours, XP et contribution au niveau global. Les modules suivants restent verrouilles tant que le Monstre intermediaire du module precedent n'est pas reussi.

Le parcours PHP / Symfony reste strictement centre sur PHP et Symfony. Git, Docker, Jira, PhpStorm, MySQL et NoSQL ne sont pas des modules du parcours. Doctrine est aborde uniquement comme composant de persistance Symfony.

## SQL Server pedagogique

Le parcours SQL fonctionne par defaut avec un moteur pedagogique en memoire. Pour utiliser SQL Server Docker :

```bash
docker run -e ACCEPT_EULA=Y \
  -e MSSQL_SA_PASSWORD=Your_strong_password123 \
  -p 14333:1433 \
  --name interactive-learning-sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Puis lancer l'API avec :

```bash
SqlLearning__UseSqlServer=true \
SqlLearning__ConnectionString="Server=localhost,14333;Database=master;User Id=sa;Password=Your_strong_password123;TrustServerCertificate=True;Encrypt=False" \
dotnet run
```

Si l'API tourne elle aussi dans Docker, utiliser `host.docker.internal` a la place de `localhost` dans la chaine de connexion.

Le scenario `StoreBasics` est reinitialise automatiquement avant chaque execution SQL du Module 1.

Pour repartir d'une base vide en developpement :

```bash
cd backend/CSharpInteractive.Api
rm -f interactive-learning.db
dotnet run
```

## Validation

```bash
cd frontend
npm run lint
npm run build
```

```bash
cd backend/CSharpInteractive.Api
dotnet build
```

```bash
cd backend
dotnet test CSharpInteractive.sln
```

## Creation de contenu

Les contenus pedagogiques sont seeds dans `backend/CSharpInteractive.Api/Data/SeedData.cs`. Une lecon doit garder un slug stable, un titre clair, un objectif, une explication courte, un exemple, un exercice, un starter code, des feedbacks de reussite/echec, une correction finale, un resume de concept, des erreurs frequentes et une recompense XP.

Strategie par module :

- Commencer par les notions atomiques du module.
- Terminer chaque module par une lecon de synthese nommee `Test intermediaire du module`.
- Donner des exercices qui produisent une sortie observable dans la console ou une requete SQL verifiable.
- Ajouter au moins un test structurel par notion importante avec `RequiredSnippet`, `MinSnippetCount`, sortie attendue ou test SQL.
- Garder les corrections finales executables telles quelles.

## Ajouter une lecon C#

1. Ajouter une entree `Lesson(...)` dans le chapitre C# concerne dans `SeedData.cs`.
2. Renseigner `conceptSummary`, `commonMistakes` si le message par defaut ne suffit pas, et `finalCorrection`.
3. Ajouter des tests avec `Output`, `Snippet` ou `Count`. Pour une methode pure, `HiddenCode` peut tester un appel supplementaire.
4. Verifier que le `starterCode` compile ou donne une erreur pedagogique simple a corriger.
5. Relancer `dotnet build` et les tests backend.

Si la base SQLite locale existe deja, supprimer `backend/CSharpInteractive.Api/interactive-learning.db` pour reseeder le contenu.

## Ajouter une lecon SQL

1. Ajouter la lecon dans le parcours `sqlserver-foundations`.
2. Definir ou reutiliser un scenario SQL dans `SqlExecutionService`.
3. Configurer les tests avec colonnes attendues, nombre de lignes, sorties, mutations ou snippets interdits.
4. Autoriser explicitement les types d'instructions necessaires dans le scenario ou la logique de securite.
5. Tester la requete en mode moteur pedagogique et, si possible, avec SQL Server Docker.

## Ajouter une lecon PHP / Symfony

1. Ajouter une entree `PhpLesson(...)` dans le chapitre PHP / Symfony concerne dans `SeedData.cs`.
2. Renseigner titre, objectif, explication, exemple, exercice, starter code, feedback, resume, erreurs frequentes, correction finale masquee et XP.
3. Ajouter des tests avec `Required`, `Output`, `Count` ou snippets interdits selon le besoin.
4. Garder le contenu centre sur PHP / Symfony; ne pas ajouter de modules Git, Docker, Jira, PhpStorm, MySQL ou NoSQL.
5. Pour Symfony, valider les structures attendues: routes, controllers, Twig, formulaires, validation, Doctrine, services ou securite selon la lecon.

## Politique de securite SQL

La securite SQL est deny-by-default pour les operations dangereuses. `SqlSafetyService` bloque notamment `DROP DATABASE`, `TRUNCATE`, `ALTER SERVER`, l'acces aux tables systeme, les procedures non autorisees et les multi-statements hors exercices controles.

Les lecons doivent autoriser uniquement les operations necessaires. Les operations de modification (`INSERT`, `UPDATE`, `DELETE`) doivent rester encadrees par des tests de correction, des `WHERE` obligatoires quand c'est pertinent, et une reinitialisation du scenario avant execution ou correction.

## Limite v1

L'execution C# utilise Roslyn en memoire avec timeout et capture console. Ce n'est pas une sandbox durcie pour executer du code non fiable en production.

Le parcours SQL bloque les commandes dangereuses et autorise progressivement les lectures, jointures, agregations, modifications controlees, objets SQL pedagogiques et scripts du Boss Final. Pour une utilisation multi-utilisateur, chaque tentative SQL doit rester isolee ou reinitialisee.

Le parcours PHP / Symfony utilise une validation statique pedagogique. Il verifie la presence des structures demandees, mais n'execute pas une application Symfony complete.

## Ancien prototype Express

Le backend Express initial est conserve dans `backend/legacy-express/` pour reference. Le backend actif est `backend/CSharpInteractive.Api/`.
