# interactive-learning - Plan produit et pedagogique

## 1. Architecture technique

Objectif: application web interactive, progressive et gamifiee pour apprendre C#, SQL / SQL Server et PHP / Symfony depuis zero jusqu'a un niveau pratique.

Stack:

- Frontend: Next.js, TypeScript, TailwindCSS.
- Editeur: Monaco Editor configure pour C#, SQL et PHP.
- Backend: ASP.NET Core Web API.
- Execution C#: Roslyn en memoire avec capture console, diagnostics et timeout.
- Execution SQL: requetes SQL Server controlees cote backend sur base isolee ou reinitialisable.
- Validation PHP / Symfony: validation statique pedagogique par criteres, snippets et structures Symfony attendues.
- Donnees applicatives: SQLite avec Entity Framework Core.
- Donnees pedagogiques SQL: SQL Server LocalDB ou SQL Server Docker.
- Acces DB SQL: Entity Framework Core ou ADO.NET selon les besoins de correction.
- Progression: XP global, niveaux globaux, badges par parcours, verrouillage progressif des chapitres, Boss Final par parcours.

Parcours obligatoires:

1. C#.
2. SQL / SQL Server.
3. PHP / Symfony.

Flux principal:

1. Le frontend charge le profil, les parcours disponibles, la carte du cours et la lecon active.
2. L'apprenant lit une courte lecon, modifie le code dans Monaco, puis lance le code.
3. Pour C#, le backend compile et execute le code via Roslyn.
4. Pour SQL, le backend verifie la securite de la requete puis l'execute sur une base pedagogique isolee.
5. Pour PHP / Symfony, le backend valide les structures demandees sans executer un vrai projet Symfony.
6. La soumission execute les tests automatiques de la lecon.
7. Si les tests passent, le backend attribue XP, badges et deblocages dans le parcours concerne.
8. Le frontend rafraichit la progression visible et le niveau global.

Progression par modules:

- Chaque module de chaque parcours se termine par un Monstre intermediaire.
- Le module suivant reste verrouille tant que le Monstre intermediaire du module actuel n'est pas reussi.
- Le Boss Final d'un parcours reste verrouille tant que les lecons et monstres requis du parcours ne sont pas termines.

Securite SQL obligatoire:

- Interdire par defaut `DROP DATABASE`, `DROP TABLE` hors exercice prevu, `TRUNCATE`, `ALTER SERVER`, `EXEC` non autorise, acces aux tables systeme, requetes multi-statements non prevues et commandes dangereuses SQL Server.
- Autoriser explicitement les operations necessaires par lecon.
- Reinitialiser ou isoler la base d'exercice avant chaque correction.
- Limiter le temps d'execution et la taille des resultats.

Flux de correction SQL:

1. Charger le scenario de base de donnees de depart.
2. Valider la requete contre la politique de securite de la lecon.
3. Executer la requete dans SQL Server.
4. Verifier syntaxe, colonnes attendues, nombre de lignes, resultat retourne et effets pour `INSERT` / `UPDATE` / `DELETE`.
5. Si les tests passent, le backend attribue XP, badges et deblocages.

Contraintes v1:

- Profil local unique, sans authentification.
- Contenu pedagogique seed dans SQLite.
- Execution Roslyn suffisante pour apprentissage local, pas une sandbox de production.
- SQL Server pedagogique doit rester isole des donnees applicatives.
- L'ancien backend Express reste archive dans `backend/legacy-express`.

## 2. Arborescence du projet

```text
interactive-learning/
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── layout.tsx
│   │   │   ├── page.tsx
│   │   │   ├── learn/[lessonId]/page.tsx
│   │   │   ├── boss-final/page.tsx
│   │   │   └── globals.css
│   │   ├── components/
│   │   │   ├── app-shell.tsx
│   │   │   ├── course-sidebar.tsx
│   │   │   ├── lesson-content.tsx
│   │   │   ├── code-editor.tsx
│   │   │   ├── run-console.tsx
│   │   │   ├── feedback-panel.tsx
│   │   │   ├── progress-header.tsx
│   │   │   ├── xp-level-card.tsx
│   │   │   ├── badge-grid.tsx
│   │   │   ├── lesson-unlock-card.tsx
│   │   │   └── boss-final-workspace.tsx
│   │   ├── lib/
│   │   │   ├── api-client.ts
│   │   │   └── lesson-state.ts
│   │   └── types/
│   │       ├── api.ts
│   │       └── learning.ts
│   └── package.json
├── backend/
│   ├── CSharpInteractive.Api/
│   │   ├── Controllers/
│   │   │   ├── CoursesController.cs
│   │   │   ├── LessonsController.cs
│   │   │   ├── BossFinalController.cs
│   │   │   └── SqlLessonsController.cs
│   │   ├── Contracts/
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── SeedData.cs
│   │   │   └── SqlScenarioSeedData.cs
│   │   ├── Models/
│   │   ├── Services/
│   │   │   ├── RoslynExecutionService.cs
│   │   │   ├── SqlExecutionService.cs
│   │   │   ├── SqlSafetyService.cs
│   │   │   ├── SqlCorrectionService.cs
│   │   │   ├── ProgressService.cs
│   │   │   └── UnlockService.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── CSharpInteractive.Api.Tests/
│   ├── CSharpInteractive.sln
│   └── legacy-express/
├── PLAN.md
├── TODO.md
└── README.md
```

## 3. Modele de donnees

### Course

- `Id`
- `Slug`
- `Title`
- `Description`
- `Language`: `csharp`, `sqlserver` ou `php-symfony`
- `SortOrder`

### Chapter

- `Id`
- `CourseId`
- `Title`
- `Description`
- `SortOrder`
- `RequiredXp`

### Lesson

Chaque lecon doit porter toute la structure pedagogique obligatoire:

- `Id`
- `ChapterId`
- `Slug`
- `Title`
- `Objective`
- `Explanation`
- `ExampleCode`
- `ExercisePrompt`
- `StarterCode`
- `SuccessFeedback`
- `FailureFeedback`
- `FinalCorrection`
- `ConceptSummary`
- `XpReward`
- `SortOrder`
- `IsBossPrerequisite`
- `IsBossFinal`
- `LessonKind`: `CSharp` ou `Sql`
- `LessonKind`: `CSharp`, `Sql` ou `PhpSymfony`

Extension pedagogique recommandee pour une v2 du modele:

- `CommonMistakes`: erreurs frequentes pour enrichir le feedback.

### IntermediateBoss

- `Id`
- `ModuleId`
- `Title`
- `Objective`
- `Instructions`
- `StarterCode`
- `ExpectedResult`
- `ValidationRules`
- `Hints`
- `Solution`
- `XpReward`
- `IsRequiredToUnlockNextModule`

## Parcours PHP / Symfony

Contraintes:

- Le parcours se concentre uniquement sur PHP et Symfony.
- Pas de modules dedies a Git, Docker, Jira, PhpStorm, MySQL ou NoSQL.
- Doctrine est aborde uniquement dans le cadre Symfony.

Modules:

1. Fondations PHP: syntaxe, variables, types, conditions, boucles, fonctions.
2. PHP oriente objet: classes, objets, proprietes, methodes, constructeurs, encapsulation.
3. Bases de Symfony: structure projet, routes, controllers, responses, Twig, parametres.
4. Formulaires Symfony: creation, validation, contraintes, erreurs, soumission.
5. Doctrine avec Symfony: entites, repositories, migrations, relations simples, CRUD.
6. Architecture Symfony: services, injection de dependances, configuration, separation controller/service, bonnes pratiques.
7. Securite Symfony: authentification, utilisateurs, roles, routes protegees, autorisations simples.
8. Projet pratique Symfony: mini-application MVC avec routes, controllers, Twig, formulaires, Doctrine, services et securite simple.

Boss Final PHP / Symfony:

- Mini-application Symfony de gestion de produits.
- Validation attendue: PHP de base, POO, routes, controllers, Twig, formulaires, validation, Doctrine, services, securite simple et architecture propre.

### LessonTest

- `Id`
- `LessonId`
- `Name`
- `TestType`
- `ExpectedOutput`
- `RequiredSnippet`
- `HiddenCode`
- `ExpectedColumns`
- `ExpectedRowCount`
- `ExpectedResultJson`
- `ExpectedAffectedRows`
- `ForbiddenSnippet`
- `AllowedStatementKinds`
- `SortOrder`

Types de tests:

- `ExpectedOutput`: verifie une sortie console.
- `RequiredSnippet`: verifie une construction syntaxique attendue.
- `HiddenCode`: ajoute un test cache au code utilisateur.
- `MinSnippetCount`: verifie un nombre minimum d'occurrences.
- `SqlExpectedResult`: verifie le resultat retourne par une requete SQL.
- `SqlExpectedColumns`: verifie les colonnes attendues.
- `SqlExpectedRowCount`: verifie le nombre de lignes.
- `SqlExpectedMutation`: verifie les effets d'un `INSERT`, `UPDATE` ou `DELETE`.
- `SqlForbiddenOperation`: verifie l'absence d'operations interdites.

Extension recommandee:

- `ExactOutput`: sortie complete stricte.
- `ForbiddenSnippet`: interdit une solution trop contournee.
- `DiagnosticHint`: message pedagogique specifique au test.

### SqlScenario

Scenario de base de donnees de depart pour les lecons SQL.

- `Id`
- `LessonId`
- `Name`
- `SetupScript`
- `ResetScript`
- `AllowedStatementKinds`
- `AllowMultipleStatements`
- `SortOrder`

### SqlExecutionAttempt

Trace optionnelle pour debug local et feedback.

- `Id`
- `UserProfileId`
- `LessonId`
- `Query`
- `Success`
- `Diagnostics`
- `DurationMs`
- `CreatedAt`

### Progression et gamification

`UserProfile`:

- `Id`
- `DisplayName`
- `TotalXp`
- `Level`
- `CurrentCourseId`
- `CreatedAt`
- `UpdatedAt`

`LessonProgress`:

- `Id`
- `UserProfileId`
- `LessonId`
- `CourseId`
- `Status`: `Locked`, `Available`, `Started`, `Completed`
- `BestCode`
- `LastOutput`
- `Attempts`
- `CompletedAt`
- `EarnedXp`

`Badge`:

- `Id`
- `CourseId`: nul pour badge global, renseigne pour badge C# ou SQL
- `Slug`
- `Name`
- `Description`
- `IconName`
- `RuleType`: `CompleteLessons`, `TotalXp`, `CompleteBossFinal`
- `RuleValue`

`UserBadge`:

- `Id`
- `UserProfileId`
- `BadgeId`
- `EarnedAt`

## 4. Endpoints backend

Base URL: `/api`

### Profil et progression

- `GET /api/profile`: profil local, XP, niveau, badges, progression globale.
- `PUT /api/profile`: modification du nom d'affichage.
- `GET /api/progress`: lecons terminees, XP, niveau, badges, prochains deblocages.

### Cours

- `GET /api/courses`: liste des cours disponibles.
- `GET /api/courses/{courseId}/map`: modules, lecons, etats verrouilles/deverrouilles, XP requis, Boss Final.
- `GET /api/courses/by-slug/{slug}/map`: acces direct au parcours `csharp-foundations` ou `sqlserver-foundations`.

### Lecons

- `GET /api/lessons/{lessonId}`: contenu complet d'une lecon.
- `POST /api/lessons/{lessonId}/run`: compile et execute le code sans valider.
- `POST /api/lessons/{lessonId}/submit`: execute, corrige, attribue XP, badges et deblocages.

### Lecons SQL

- `GET /api/sql/lessons/{lessonId}`: contenu complet, schema de depart, tables disponibles, consignes SQL.
- `POST /api/sql/lessons/{lessonId}/run`: valide la securite puis execute la requete SQL sans attribuer XP.
- `POST /api/sql/lessons/{lessonId}/submit`: execute, corrige resultat/effets, attribue XP, badges et deblocages SQL.
- `POST /api/sql/lessons/{lessonId}/reset`: reinitialise la base d'exercice de la lecon.

### Boss Final

- `POST /api/boss-final/run`: execute le mini-projet final sans validation.
- `POST /api/boss-final/submit`: valide le mini-projet final et attribue badge/XP final.
- `POST /api/sql/boss-final/run`: execute le script e-commerce final sans validation.
- `POST /api/sql/boss-final/submit`: valide le Boss Final SQL Server.

## 5. Composants frontend

- `AppShell`: layout principal, navigation, zone cours, zone progression.
- `ProgressHeader`: XP, niveau, barre de progression, badges recents.
- `CourseSidebar`: modules, lecons, etats verrouilles, Boss Final.
- `CourseSwitcher`: selection du parcours C# ou SQL / SQL Server.
- `LessonContent`: titre, objectif, explication, exemple, exercice.
- `CodeEditor`: Monaco Editor C# ou SQL selon la lecon.
- `RunConsole`: sortie console, diagnostics Roslyn, duree d'execution.
- `SqlResultGrid`: table de resultats SQL, colonnes, lignes, nombre de lignes.
- `SqlSchemaPanel`: schema de base de depart, tables, colonnes et donnees utiles.
- `SqlSafetyNotice`: operations autorisees/interdites pour la lecon.
- `FeedbackPanel`: resultats de tests, feedback pedagogique, XP gagne.
- `XpLevelCard`: niveau courant, seuil suivant, progression XP.
- `BadgeGrid`: badges obtenus et badges verrouilles.
- `LessonUnlockCard`: prochaine lecon ou chapitre debloque.
- `BossFinalWorkspace`: enonce final, editeur, console, tests de synthese.
- `SqlBossFinalWorkspace`: editeur SQL, schema e-commerce, resultats, tests de synthese.

Ameliorations frontend a prevoir:

- Vue "Correction finale" affichee apres reussite.
- Indicateur de tentatives.
- Feedback par test avec conseil pedagogique.
- Resume de chapitre avec test intermediaire.

## 6. Structure pedagogique complete

### Structure commune C# et SQL

Chaque lecon contient obligatoirement:

1. Titre.
2. Objectif pedagogique.
3. Explication simple.
4. Exemple de code.
5. Exercice interactif.
6. Code de depart.
7. Tests automatiques.
8. Feedback en cas d'erreur.
9. Correction finale.
10. XP gagne.

Pour SQL, chaque lecon contient aussi:

1. Base de donnees de depart.
2. Requete attendue ou criteres de validation.
3. Colonnes attendues.
4. Nombre de lignes attendu.
5. Effets attendus pour `INSERT`, `UPDATE`, `DELETE`.
6. Operations interdites.

### Parcours C#

### Module 1 - Fondations

1. Hello World: afficher du texte avec `Console.WriteLine`.
2. Variables: stocker une valeur dans une variable.
3. Types: utiliser `int`, `string`, `bool`, `double`.
4. Operateurs: calculs, concatenation, comparaisons simples.
5. Test intermediaire: mini-programme de presentation avec score et statut.

### Module 2 - Controle du flux

1. if / else: executer un bloc selon une condition.
2. switch: choisir une action selon une valeur.
3. for: repeter un nombre connu de fois.
4. while: repeter tant qu'une condition reste vraie.
5. foreach: parcourir une collection.
6. Test intermediaire: calculateur de rang de joueur.

### Module 3 - Methodes

1. Creer une methode.
2. Parametres.
3. Valeur de retour.
4. Scope.
5. Surcharge.
6. Test intermediaire: boite a outils de calculs.

### Module 4 - POO de base

1. Classes.
2. Objets.
3. Proprietes.
4. Constructeurs.
5. Test intermediaire: modeliser un personnage simple.

### Module 5 - POO avancee

1. Heritage.
2. Polymorphisme.
3. Interfaces.
4. `public` / `private` / `protected`.
5. Test intermediaire: hierarchie de personnages.

### Module 6 - Structures de donnees

1. Tableaux.
2. `List<T>`.
3. `Dictionary<TKey, TValue>`.
4. LINQ.
5. Test intermediaire: inventaire simple avec recherche.

### Module 7 - Gestion des erreurs

1. `try` / `catch`.
2. Exceptions.
3. Nullables.
4. Test intermediaire: saisie robuste et valeurs absentes.

### Module 8 - Base de donnees

1. Bases relationnelles.
2. Entity Framework Core.
3. `DbContext`.
4. CRUD.
5. Test intermediaire: mini repertoire persistant conceptuel.

### Boss Final - Inventaire RPG

Sujet: creer une mini-application console de gestion d'inventaire RPG.

Fonctionnalites obligatoires:

1. Creer une classe `Item`.
2. Ajouter un item.
3. Afficher l'inventaire.
4. Supprimer un item.
5. Calculer la valeur totale.
6. Gerer les erreurs utilisateur.

Competences verifiees:

- Variables.
- Conditions.
- Boucles.
- Methodes.
- Classes.
- Objets.
- `List<T>`.
- Exceptions.
- Logique metier simple.

Tests automatiques attendus:

- Presence d'une classe `Item`.
- Presence de proprietes nom, quantite et valeur.
- Utilisation d'une `List<Item>`.
- Ajout d'au moins deux items.
- Affichage de l'inventaire.
- Suppression d'un item.
- Calcul de valeur totale.
- Gestion d'erreur via `try` / `catch` ou exception explicite.

### Parcours SQL / SQL Server

#### Module 1 - Fondations SQL

1. Qu'est-ce qu'une base de donnees relationnelle ?
2. Tables, lignes, colonnes.
3. Types de donnees SQL Server.
4. `SELECT`.
5. `WHERE`.
6. Test intermediaire: lire et filtrer un catalogue de produits.

#### Module 2 - Trier et filtrer les donnees

1. `ORDER BY`.
2. `TOP`.
3. `DISTINCT`.
4. `LIKE`.
5. `IN`.
6. `BETWEEN`.
7. `IS NULL` / `IS NOT NULL`.
8. Test intermediaire: recherche avancee dans un catalogue.

#### Module 3 - Agregation

1. `COUNT`.
2. `SUM`.
3. `AVG`.
4. `MIN` / `MAX`.
5. `GROUP BY`.
6. `HAVING`.
7. Test intermediaire: statistiques de ventes.

#### Module 4 - Jointures

1. `INNER JOIN`.
2. `LEFT JOIN`.
3. `RIGHT JOIN`.
4. `FULL OUTER JOIN`.
5. Alias de tables.
6. Test intermediaire: commandes et clients.

#### Module 5 - Modification des donnees

1. `INSERT`.
2. `UPDATE`.
3. `DELETE`.
4. Transactions simples.
5. `ROLLBACK` / `COMMIT`.
6. Test intermediaire: corriger un stock produit.

#### Module 6 - Modelisation relationnelle

1. Cles primaires.
2. Cles etrangeres.
3. Contraintes.
4. Normalisation simple.
5. Relations 1-N et N-N.
6. Test intermediaire: modele commandes/produits.

#### Module 7 - SQL Server avance

1. Index.
2. Vues.
3. Procedures stockees.
4. Fonctions.
5. Variables T-SQL.
6. Test intermediaire: optimisation simple et vue metier.

#### Module 8 - Projet pratique SQL Server

1. Creation d'un schema complet.
2. Creation des tables.
3. Insertion de donnees.
4. Requetes metier.
5. Optimisation simple.
6. Test intermediaire: mini schema exploitable.

#### Boss Final SQL - Mini-boutique en ligne

Sujet: creer et interroger une base de donnees e-commerce simplifiee.

Tables minimales:

1. `Customers`.
2. `Products`.
3. `Orders`.
4. `OrderItems`.

Fonctionnalites attendues:

1. Creer les tables avec leurs contraintes.
2. Inserer des donnees de test.
3. Afficher les commandes avec le nom du client.
4. Calculer le total de chaque commande.
5. Afficher les meilleurs clients.
6. Mettre a jour le stock produit.
7. Supprimer proprement une commande avec ses lignes.
8. Utiliser une transaction.

Competences verifiees:

- Creation de tables.
- Cles primaires.
- Cles etrangeres.
- `INSERT`.
- `SELECT`.
- `WHERE`.
- `JOIN`.
- `GROUP BY`.
- `HAVING`.
- `UPDATE`.
- `DELETE` controle.
- Transaction simple.

## 7. Plan d'implementation etapes validables

### Etape 1 - Geler la structure pedagogique

- Aligner `SeedData` sur les 8 modules obligatoires.
- Donner un slug stable a chaque lecon.
- Ajouter les tests intermediaires comme lecons de synthese.
- Conserver le Boss Final comme derniere lecon verrouillee.

Validation:

- La carte du cours affiche les 8 modules.
- Chaque lecon a titre, objectif, explication, exemple, exercice, starter code, tests, feedback, correction, XP.

### Etape 2 - Enrichir le modele de contenu

- Ajouter `FinalCorrection`, `ConceptSummary`, `CommonMistakes` si necessaire.
- Adapter DTOs et frontend.
- Garder la compatibilite avec les champs existants.

Validation:

- Une lecon peut afficher une correction finale.
- Le build backend et frontend passe.

### Etape 3 - Reecrire le Module 1 complet

- Produire les 4 lecons et le test intermediaire.
- Ajouter tests automatiques solides.
- Ajouter feedback specifique par erreur courante.

Validation:

- Le Module 1 est jouable de bout en bout.
- XP, deblocage et badges restent fonctionnels.

### Etape 4 - Modules 2 et 3

- Ajouter controle du flux et methodes.
- Utiliser davantage de tests `HiddenCode`.
- Introduire les premiers exercices de synthese.

Validation:

- Les apprenants peuvent resoudre des exercices sans copier uniquement la sortie attendue.

### Etape 5 - Modules 4 et 5

- Ajouter POO de base et avancee.
- Tester classes, objets, constructeurs, heritage, interfaces.

Validation:

- Les tests verifient la structure du code et le comportement.

### Etape 6 - Modules 6 et 7

- Ajouter collections, LINQ, erreurs, exceptions, nullables.
- Preparer les notions necessaires au Boss Final.

Validation:

- Les exercices utilisent des donnees plus proches de petits cas pratiques.

### Etape 7 - Module 8

- Enseigner les concepts EF Core sans executer une vraie base par le code apprenant si la sandbox ne le permet pas.
- Corriger via snippets, code cache et sorties attendues.

Validation:

- L'apprenant comprend entite, contexte et CRUD.

### Etape 8 - Boss Final RPG

- Remplacer le Boss Final actuel par l'inventaire RPG complet.
- Ajouter tests de synthese.
- Attribuer badge final.

Validation:

- Le Boss Final reste verrouille jusqu'aux prerequis.
- La completion finale attribue XP et badge.

### Etape 9 - Validation globale

- `npm run lint`
- `npm run build`
- `dotnet test`
- Tests manuels: succes, erreur compilation, test echoue, XP, niveaux, badges, Boss Final.

### Etape 10 - Ajouter le parcours SQL / SQL Server

- Ajouter le cours `sqlserver-foundations`.
- Ajouter les entites ou champs SQL necessaires: scenario, scripts, criteres de validation.
- Ajouter `SqlSafetyService`, `SqlExecutionService`, `SqlCorrectionService`.
- Ajouter les endpoints `/api/sql/...`.
- Ajouter le mode SQL dans Monaco et le rendu `SqlResultGrid`.

Validation:

- Le frontend affiche deux parcours: C# et SQL / SQL Server.
- La progression SQL est independante de C#.
- Le niveau global cumule C# + SQL.

### Etape 11 - Module SQL 1 complet

- Creer le scenario de depart `StoreBasics`.
- Ajouter les lecons: relationnel, tables/lignes/colonnes, types SQL Server, `SELECT`, `WHERE`, test intermediaire.
- Ajouter tests automatiques sur colonnes, lignes, resultats et securite.

Validation:

- Les requetes `SELECT` et `WHERE` s'executent dans SQL Server.
- Les operations interdites sont bloquees.
- Les deblocages et XP SQL fonctionnent.

## 8. Premier module complet - Lecon 1: Hello World

### Titre

Hello World

### Objectif pedagogique

Comprendre qu'un programme C# peut afficher du texte dans la console avec `Console.WriteLine`.

### Explication simple

En C#, `Console.WriteLine` ecrit une ligne de texte dans la console. Le texte doit etre place entre guillemets. Une instruction C# se termine generalement par un point-virgule.

Points cles:

- `Console` represente la console.
- `WriteLine` signifie "ecrire une ligne".
- Le texte affiche est une chaine de caracteres.
- Le point-virgule termine l'instruction.

### Exemple de code

```csharp
using System;

Console.WriteLine("Bonjour C#");
```

Sortie attendue:

```text
Bonjour C#
```

### Exercice interactif

Ecris un programme qui affiche exactement:

```text
Hello, World!
Je commence C#
```

### Code de depart

```csharp
using System;

Console.WriteLine("Hello, World!");
// Ajoute une deuxieme ligne ici
```

### Tests automatiques

1. La sortie contient `Hello, World!`.
2. La sortie contient `Je commence C#`.
3. Le code utilise `Console.WriteLine`.
4. Le code contient au moins deux appels a `Console.WriteLine`.

### Feedback en cas d'erreur

- Si le code ne compile pas: verifie les guillemets et le point-virgule.
- Si la premiere ligne manque: garde `Console.WriteLine("Hello, World!");`.
- Si la deuxieme ligne manque: ajoute un second `Console.WriteLine`.
- Si le texte est different: respecte exactement les majuscules, espaces et ponctuation.

### Correction finale

```csharp
using System;

Console.WriteLine("Hello, World!");
Console.WriteLine("Je commence C#");
```

### XP gagne

25 XP

### Deblocage

La lecon "Variables" devient disponible apres reussite.

## 9. Premier module SQL complet - SELECT et WHERE

### Scenario de base de donnees de depart

Base pedagogique: `StoreBasics`.

Tables:

```sql
CREATE TABLE Categories (
    Id INT PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL
);

CREATE TABLE Products (
    Id INT PRIMARY KEY,
    Name NVARCHAR(120) NOT NULL,
    CategoryId INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    IsActive BIT NOT NULL,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
```

Donnees de depart:

```sql
INSERT INTO Categories (Id, Name) VALUES
(1, N'Books'),
(2, N'Games'),
(3, N'Hardware');

INSERT INTO Products (Id, Name, CategoryId, Price, Stock, IsActive) VALUES
(1, N'C# Basics', 1, 29.90, 15, 1),
(2, N'SQL Server Guide', 1, 34.50, 8, 1),
(3, N'RPG Dice Set', 2, 12.00, 30, 1),
(4, N'Mechanical Keyboard', 3, 89.99, 5, 1),
(5, N'Archived Mouse', 3, 19.90, 0, 0);
```

Operations autorisees pour le Module 1:

- `SELECT` uniquement.
- Une seule requete par execution.
- Aucun acces aux tables systeme.

Operations interdites:

- `INSERT`, `UPDATE`, `DELETE`.
- `DROP`, `TRUNCATE`, `ALTER`, `EXEC`.
- Multi-statements hors exercice prevu.

### Lecon SQL 1 - Qu'est-ce qu'une base de donnees relationnelle ?

Objectif pedagogique: comprendre qu'une base relationnelle organise les donnees en tables reliees.

Explication simple: une table ressemble a un tableau. Chaque ligne represente un element, chaque colonne represente une information. Une base relationnelle peut relier plusieurs tables avec des identifiants.

Exemple de requete:

```sql
SELECT Name
FROM Categories;
```

Exercice interactif: affiche les noms des categories.

Requete de depart:

```sql
-- Affiche les noms des categories
SELECT ...
```

Criteres de validation:

- Colonnes attendues: `Name`.
- Nombre de lignes attendu: 3.
- Resultats attendus: `Books`, `Games`, `Hardware`.
- Operations autorisees: `SELECT`.

Feedback en cas d'erreur:

- Si la table manque: utilise `FROM Categories`.
- Si la colonne manque: selectionne `Name`.
- Si une commande interdite apparait: garde seulement une requete `SELECT`.

Correction finale:

```sql
SELECT Name
FROM Categories;
```

XP gagne: 25 XP.

### Lecon SQL 2 - Tables, lignes, colonnes

Objectif pedagogique: lire plusieurs colonnes dans une table.

Explication simple: `SELECT` choisit les colonnes a afficher. `FROM` indique la table a lire.

Exemple de requete:

```sql
SELECT Id, Name
FROM Products;
```

Exercice interactif: affiche `Name`, `Price` et `Stock` depuis `Products`.

Base de donnees de depart: `StoreBasics`.

Criteres de validation:

- Colonnes attendues: `Name`, `Price`, `Stock`.
- Nombre de lignes attendu: 5.
- Table attendue: `Products`.

Correction finale:

```sql
SELECT Name, Price, Stock
FROM Products;
```

XP gagne: 30 XP.

### Lecon SQL 3 - Types de donnees SQL Server

Objectif pedagogique: reconnaitre les types SQL Server courants.

Explication simple: SQL Server utilise des types comme `INT`, `NVARCHAR`, `DECIMAL` et `BIT` pour representer entiers, texte, prix et booleens.

Exemple de requete:

```sql
SELECT Name, Price, IsActive
FROM Products;
```

Exercice interactif: affiche le nom, le prix et l'etat actif des produits.

Criteres de validation:

- Colonnes attendues: `Name`, `Price`, `IsActive`.
- Nombre de lignes attendu: 5.
- Resultats incluant `Archived Mouse` avec `IsActive = 0`.

Correction finale:

```sql
SELECT Name, Price, IsActive
FROM Products;
```

XP gagne: 35 XP.

### Lecon SQL 4 - SELECT

Objectif pedagogique: utiliser `SELECT` pour choisir exactement les donnees utiles.

Explication simple: eviter `SELECT *` aide a lire seulement les colonnes necessaires et a garder les resultats propres.

Exemple de requete:

```sql
SELECT Name, Price
FROM Products;
```

Exercice interactif: affiche uniquement `Name` et `Price` pour tous les produits.

Criteres de validation:

- Colonnes attendues: `Name`, `Price`.
- Colonnes interdites dans le resultat: `Id`, `CategoryId`, `Stock`, `IsActive`.
- Nombre de lignes attendu: 5.

Correction finale:

```sql
SELECT Name, Price
FROM Products;
```

XP gagne: 35 XP.

### Lecon SQL 5 - WHERE

Objectif pedagogique: filtrer les lignes avec une condition.

Explication simple: `WHERE` garde seulement les lignes qui respectent une condition. On peut filtrer sur un prix, un stock ou un booleen.

Exemple de requete:

```sql
SELECT Name, Price
FROM Products
WHERE Price >= 30;
```

Exercice interactif: affiche `Name`, `Price` et `Stock` des produits actifs dont le stock est superieur ou egal a 10.

Criteres de validation:

- Colonnes attendues: `Name`, `Price`, `Stock`.
- Nombre de lignes attendu: 2.
- Resultats attendus: `C# Basics`, `RPG Dice Set`.
- La requete doit contenir `WHERE`.
- La requete doit filtrer `IsActive = 1`.
- La requete doit filtrer `Stock >= 10`.

Correction finale:

```sql
SELECT Name, Price, Stock
FROM Products
WHERE IsActive = 1 AND Stock >= 10;
```

XP gagne: 40 XP.

### Test intermediaire SQL Module 1 - Catalogue lisible

Objectif pedagogique: combiner lecture de colonnes et filtrage simple.

Exercice interactif: affiche `Name`, `Price`, `Stock` des produits actifs coutant moins de 40 et encore en stock.

Criteres de validation:

- Colonnes attendues: `Name`, `Price`, `Stock`.
- Nombre de lignes attendu: 3.
- Resultats attendus: `C# Basics`, `SQL Server Guide`, `RPG Dice Set`.
- La requete utilise `SELECT`, `FROM Products`, `WHERE`, `IsActive = 1`, `Price < 40`, `Stock > 0`.

Correction finale:

```sql
SELECT Name, Price, Stock
FROM Products
WHERE IsActive = 1 AND Price < 40 AND Stock > 0;
```

XP gagne: 60 XP.
