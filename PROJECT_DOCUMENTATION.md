# Documentation complete du projet interactive-learning

## 1. Vision du produit

`interactive-learning` est une application web d'apprentissage par la pratique. Elle propose des parcours progressifs pour apprendre C#, SQL / SQL Server et PHP / Symfony avec:

- des lecons courtes et actionnables;
- un editeur Monaco integre;
- une execution ou validation immediate du code;
- une correction automatique par criteres;
- une progression XP / niveaux / badges;
- des Monstres intermediaires pour valider un module;
- un Boss Final par parcours pour consolider les acquis.

L'objectif pedagogique est de garder l'apprenant actif: la lecon donne le contexte, l'exercice fixe l'objectif, l'editeur reste a l'apprenant, et la correction indique quoi ameliorer sans injecter la solution.

## 2. Principes pedagogiques

La pedagogie du projet suit un cycle simple:

1. Comprendre: l'apprenant lit un objectif, une explication courte, un exemple et les erreurs frequentes.
2. Pratiquer: l'apprenant ecrit lui-meme le code dans l'editeur.
3. Executer: l'apprenant lance son code pour observer le resultat.
4. Soumettre: le backend applique des tests visibles et caches selon la lecon.
5. Progresser: une reussite attribue XP, badges et deverrouillages.
6. Consolider: les Monstres intermediaires et Boss Finals forcent la synthese de plusieurs notions.

Les solutions finales restent cote serveur. Le frontend ne doit pas afficher une correction complete dans une lecon normale. Les indices peuvent orienter, mais ils ne doivent pas remplacer l'effort de production.

## 3. Architecture generale

```text
frontend/                       Next.js, TypeScript, TailwindCSS, Monaco
backend/CSharpInteractive.Api/  API ASP.NET Core, EF Core, moteurs pedagogiques
backend/CSharpInteractive.Api.Tests/  Tests xUnit
backend/legacy-express/         Ancien prototype archive
```

### Frontend

Le frontend est une application Next.js. Le composant principal `AppShell` orchestre:

- la liste des cours;
- la carte de progression;
- le panneau de contenu pedagogique;
- l'editeur Monaco;
- la console d'execution;
- le feedback de correction;
- la carte de competences;
- la file de revisions;
- les badges, XP et informations SQL.

Le frontend appelle l'API via `frontend/src/lib/api-client.ts`. Les DTO TypeScript sont dans `frontend/src/types/api.ts`.

### Backend

Le backend expose des routes REST sous `/api`:

- `/api/courses`: catalogue et carte de progression;
- `/api/lessons`: lecons C# et PHP/Symfony;
- `/api/sql/lessons`: lecons SQL et schema pedagogique;
- `/api/intermediate-bosses`: Monstres intermediaires;
- `/api/boss-final`: Boss Final C#;
- `/api/profile` et `/api/progress`: profil, badges et progression.
- `/api/skills`: catalogue de competences et progression par competence;
- `/api/reviews/due`: revisions arrivees a echeance.

Les donnees pedagogiques sont seedees dans `backend/CSharpInteractive.Api/Data/SeedData.cs`.

## 4. Systeme de progression

Chaque lecon a un statut:

- `Locked`: non accessible;
- `Available`: accessible;
- `Started`: tentative effectuee mais non validee;
- `Completed`: lecon reussie.

La progression repose sur:

- XP par lecon;
- niveau calcule a partir de l'XP total;
- badges par regles simples;
- deverrouillage sequentiel des lecons;
- Monstre intermediaire requis pour passer certains modules;
- Boss Final deverrouille apres les prerequisites du parcours.
- progression par competence;
- repetition espacee simple.

### Progression par competence

Le projet suit aussi une progression transversale par competence. Une competence decrit une capacite pedagogique precise, par exemple `csharp-variables`, `sql-joins` ou `symfony-routing`.

Chaque lecon est reliee a une a trois competences via `LessonSkill`. Apres chaque soumission, le backend met a jour `UserSkillProgress`:

- `MasteryPercent`: niveau de maitrise entre 0 et 100;
- `SuccessfulAttempts`: nombre de reussites;
- `FailedAttempts`: nombre d'echecs;
- `NextReviewAt`: prochaine revision conseillee;
- `Status`: `New`, `Learning`, `Fragile`, `Solid`, `Mastered` ou `ReviewDue`.

Les seuils sont simples:

- 0-25%: `New`;
- 26-59%: `Learning`;
- 60-84%: `Solid`;
- 85-100%: `Mastered`;
- plusieurs echecs sur une competence faible: `Fragile`;
- revision arrivee a echeance: `ReviewDue`.

La repetition espacee est volontairement simple en v1: J+1, J+3, J+7 ou J+14 selon la maitrise.

Entites principales:

- `Skill`: competence pedagogique globale, rattachee a un langage de cours.
- `LessonSkill`: liaison entre une lecon et une competence, avec un poids.
- `UserSkillProgress`: progression de l'apprenant pour une competence.
- `LessonHint`: indice progressif lie a une lecon.

Les competences sont seedees dans `SeedData.cs`. Les lecons existantes sont liees automatiquement par heuristique de slug/langage afin de couvrir tout le catalogue actuel sans reecrire chaque lecon manuellement. Pour les nouvelles lecons, une liaison explicite reste preferable.

Les badges seedes incluent notamment:

- Premier run;
- 100 XP;
- Boss Final;
- Premier SELECT;
- Premier script PHP;
- Produit Symfony.

Le modele de badge supporte aussi des regles ciblees par parcours via `RuleCourseLanguage`, avec les types `CompleteLessonInCourse` et `CompleteBossFinalInCourse`. Cela evite qu'un badge SQL ou PHP soit attribue par une progression C# globale.

## 5. Moteurs pedagogiques

Le backend contient trois familles d'execution/validation.

### C#

Le moteur C# utilise Roslyn en memoire. Il compile le code, execute le point d'entree, capture la sortie console et renvoie diagnostics, sortie et duree.

Le squelette neutre de l'editeur C# contient uniquement:

```csharp
using System;

// Ecris ton code ici
```

Il ne contient pas de solution d'exercice.

### SQL / SQL Server

Le parcours SQL utilise un moteur pedagogique controle. Il expose un schema, applique des regles de securite et valide:

- colonnes attendues;
- nombre de lignes;
- sortie textuelle;
- snippets requis;
- snippets interdits.

SQL Server Docker est optionnel: le projet peut fonctionner avec le moteur pedagogique en memoire.

### PHP / Symfony

La v1 ne lance pas un runtime PHP. Elle applique une validation statique pedagogique:

- code non vide;
- balise PHP placee correctement;
- presence de snippets ou concepts attendus;
- interdiction de contournements simples.

## 6. Refactoring modulaire

Le backend applique maintenant un registre de langages via `ILearningLanguageHandler`.

Chaque langage declare:

- son identifiant de parcours (`CourseLanguage`);
- le langage Monaco (`EditorLanguage`);
- comment executer ou valider du code;
- comment soumettre une lecon;
- comment valider les regles d'un Monstre intermediaire.

Handlers existants:

- `CSharpLearningLanguageHandler`;
- `SqlServerLearningLanguageHandler`;
- `PhpSymfonyLearningLanguageHandler`.

Le service `LearningLanguageService` selectionne le bon handler depuis le langage du cours. Les controleurs ne contiennent plus la logique metier `if C# / SQL / PHP`; ils deleguent au handler.

Le service `CourseCatalogService` construit le catalogue et la carte de progression. Le controleur `CoursesController` reste limite a HTTP.

Cette separation respecte mieux les principes SOLID:

- Single Responsibility: controleurs HTTP, catalogue, progression, execution et validation sont separes.
- Open/Closed: ajouter un langage revient a ajouter un handler, sans modifier les controleurs existants.
- Liskov Substitution: tous les handlers respectent le meme contrat.
- Interface Segregation: le contrat expose seulement les operations necessaires aux parcours.
- Dependency Inversion: les controleurs dependent d'abstractions de service, pas des moteurs concrets.

## 7. Cadrage local et limites volontaires

Le projet est concu en priorite pour un usage personnel/local. Les travaux recents privilegient la qualite pedagogique, la progression gamifiee, les feedbacks et la modularite du contenu.

Ne sont pas des priorites de cette version:

- authentification complexe;
- multi-utilisateur avance;
- sandboxing production;
- durcissement de securite production;
- scaling horizontal;
- orchestration de workers d'execution.

Etat persistant actuel en usage local:

- profil utilisateur;
- progression;
- badges;
- cours et lecons;
- tentatives et meilleurs codes.
- progression par competence;
- echeances de revision.

Si le projet devait devenir multi-utilisateur ou public, il faudrait alors traiter separement:

- remplacer SQLite par une base partagee robuste;
- isoler les tentatives utilisateur par compte;
- stocker les sessions et profils avec authentification reelle;
- externaliser les logs;
- eviter toute dependance a un fichier local par instance;
- limiter strictement l'execution de code utilisateur;
- utiliser une file ou un service dedie pour les executions longues.

Ces sujets restent hors perimetre de l'update pedagogique actuelle.

## 7.1 Feedback structure et indices

Chaque soumission peut maintenant retourner un `SubmissionFeedbackDto` en plus du feedback texte historique:

- resume;
- points reussis;
- points a corriger;
- categorie d'erreur;
- trois indices progressifs;
- competences liees;
- suggestions de lecons a reviser.

Les lecons normales ne renvoient pas la correction finale. Les indices restent progressifs et ne remplacent pas la solution.

Les Monstres intermediaires et Boss peuvent retourner un `BossResultDto` avec score global, scores par competence, forces, faiblesses et revisions conseillees.

DTO principaux:

- `SubmissionFeedbackDto`: feedback pedagogique structure.
- `RelatedSkillDto`: competence liee a la soumission, avec maitrise actuelle.
- `BossResultDto`: rapport global de boss ou monstre.
- `SkillResultDto`: score d'une competence dans un boss.
- `SkillProgressDto`: etat d'une competence dans la carte de competences.

Categories d'erreurs supportees:

- `SyntaxError`;
- `MissingRequiredSnippet`;
- `ForbiddenSnippetUsed`;
- `WrongOutput`;
- `WrongLogic`;
- `EmptyCode`;
- `CompilationError`;
- `RuntimeError`;
- `PartialSolution`;
- `HardcodedSolution`;
- `Unknown`.

Endpoints ajoutes:

- `GET /api/skills`;
- `GET /api/skills/progress`;
- `GET /api/skills/progress/{courseLanguage}`;
- `GET /api/reviews/due`;
- `POST /api/progress/review-completed`.

### Flux d'une soumission

1. Le controleur charge la lecon accessible.
2. Le handler de langage execute ou valide le code.
3. Le service de correction produit les `TestResultDto`.
4. `ProgressService` met a jour XP, statut de lecon, badges et deverrouillages.
5. `SkillProgressService` met a jour les competences liees.
6. La reponse conserve les champs historiques de `SubmitResultDto` et ajoute `structuredFeedback`.
7. Si la soumission concerne un Monstre ou Boss, la reponse peut aussi contenir `bossResult`.

### UI associee

Le frontend affiche:

- resume de feedback;
- points reussis;
- points a corriger;
- indice 1 visible immediatement;
- boutons pour reveler les indices suivants;
- competences concernees;
- score par competence apres Monstre/Boss;
- carte de competences du parcours actif;
- indication de revision due.

## 8. Parcours C# Fondations

Objectif: apprendre C# par des exercices progressifs de console, puis construire un mini-projet.

Modules:

- Module 1 - Fondations: Hello World, variables, types, operateurs, interpolation.
- Module 2 - Controle du flux: conditions, switch, boucles `for`, `while`, accumulation, `foreach`.
- Module 3 - Methodes: signatures, retours, parametres, surcharge, portee, composition.
- Module 4 - POO de base: classes, objets, proprietes, constructeurs, encapsulation.
- Module 5 - POO avancee: heritage, polymorphisme, abstractions, interfaces, protection des invariants.
- Module 6 - Structures de donnees: tableaux, listes, dictionnaires, LINQ simple, transformations.
- Module 7 - Gestion des erreurs: validations, exceptions, `try/catch`, erreurs metier.
- Module 8 - Base de donnees: modelisation relationnelle, EF Core, entites, contexte, requetes.
- Boss Final: mini-projet de synthese C#.

## 9. Parcours SQL / SQL Server

Objectif: apprendre a lire, filtrer, agreger, relier et modifier des donnees SQL.

Modules:

- Module 1 - Fondations SQL: `SELECT`, colonnes, alias, `WHERE`, filtres simples.
- Module 2 - Trier et filtrer les donnees: `ORDER BY`, `TOP`, `DISTINCT`, `LIKE`, `IN`, `BETWEEN`, `IS NULL`.
- Module 3 - Agregation: `COUNT`, `SUM`, `AVG`, `MIN`, `MAX`, `GROUP BY`, `HAVING`.
- Module 4 - Jointures: `INNER`, `LEFT`, `RIGHT`, `FULL OUTER`, alias et jointures filtrees.
- Module 5 - Modification des donnees: `INSERT`, `UPDATE`, `DELETE`, transactions et protections.
- Module 6 - Modelisation relationnelle: cles primaires, cles etrangeres, contraintes, relations N-N.
- Module 7 - SQL Server avance: vues, procedures, fonctions, index et requetes reutilisables.
- Module 8 - Projet pratique SQL Server: mini-systeme e-commerce, clients, commandes, lignes, totaux.
- Boss Final SQL: mini-boutique e-commerce complete.

## 10. Parcours PHP / Symfony

Objectif: construire les bases PHP moderne et Symfony par validation statique guidee.

Modules:

- Module 1 - Fondations PHP: syntaxe, variables, types, conditions, boucles, fonctions.
- Modules generes PHP/Symfony: progression sur les concepts Symfony usuels selon les titres seedes.
- Boss Final PHP / Symfony: mini-application Symfony de gestion de produits.

Ce parcours est volontairement moins lie a un runtime en v1. Il pourra evoluer vers un runner PHP isole en ajoutant un nouveau handler ou en remplacant `PhpSymfonyLearningLanguageHandler`.

## 11. Ajouter un nouveau cours

Pour ajouter un cours dans le systeme actuel:

1. Ajouter le cours, les chapitres, les lecons, tests et corrections dans `SeedData.cs`.
2. Utiliser un `Language` stable sur le cours.
3. Ajouter ou reutiliser les competences `Skill` correspondant au parcours.
4. Lier chaque lecon a une a trois competences via `LessonSkill`.
5. Ajouter trois `LessonHint` progressifs par lecon.
6. Si le langage existe deja, reutiliser son handler.
7. Si le langage est nouveau, creer une classe qui implemente `ILearningLanguageHandler`.
8. Enregistrer le handler dans `Program.cs`.
9. Ajouter les tests backend du moteur et de la progression par competence.
10. Verifier le frontend: le DTO existant suffit si le handler expose un `EditorLanguage` supporte par Monaco.

## 12. Ajouter une lecon

Une lecon doit fournir:

- `slug` stable;
- titre clair;
- objectif;
- explication concise;
- exemple;
- exercice;
- starter code stocke cote serveur, sans obligation de l'afficher;
- feedback de reussite;
- feedback d'echec;
- XP;
- tests de validation;
- competences liees;
- indices progressifs;
- resume de concept;
- erreurs frequentes;
- correction finale cote serveur.

Les tests doivent rester pedagogiques: assez stricts pour corriger, mais pas au point de bloquer inutilement les solutions equivalentes.

Indices recommandes:

- niveau 1: orientation legere;
- niveau 2: indication concrete;
- niveau 3: guidage fort sans correction complete.

Exemple pour une lecon de variables:

- `Identifie d'abord la valeur a stocker.`
- `Tu dois utiliser une variable avant l'affichage.`
- `Declare une variable puis utilise-la dans Console.WriteLine.`

## 13. Lancement local

Backend local avec .NET:

```bash
cd backend/CSharpInteractive.Api
dotnet restore
dotnet run
```

Lancement complet via Docker Compose:

```bash
docker compose up
```

Ce mode lance l'API et le frontend avec des conteneurs standards:

- API: image locale `interactive-learning-api:dev`, construite depuis `backend/Dockerfile.dev`, port `5000`;
- frontend: image locale `interactive-learning-frontend:dev`, construite depuis `frontend/Dockerfile.dev`, port `3000`;
- dependances NuGet et npm installees pendant le build Docker;
- pas de bind mount requis entre l'hote et les conteneurs;
- base SQLite conservee dans le volume Docker `api-data`.

Le premier lancement peut avoir besoin d'internet pour telecharger les images Docker et remplir les caches de dependances. Apres ce premier lancement, le projet peut redemarrer hors ligne si les images locales et le cache de build Docker sont conserves. Apres une modification du code, reconstruire avec `docker compose build` ou `docker compose up --build`.

Frontend:

```bash
cd frontend
NEXT_PUBLIC_API_BASE_URL=http://localhost:5000/api npm run dev
```

URLs:

- Frontend: `http://localhost:3000`
- API: `http://localhost:5000/api`

## 14. Verification

Commandes utiles:

```bash
npm run build
dotnet test CSharpInteractive.sln
curl -s http://localhost:5000/api/courses
curl -s http://localhost:5000/api/skills
curl -s http://localhost:5000/api/skills/progress/csharp
curl -s http://localhost:5000/api/reviews/due
```

Si `dotnet` n'est pas disponible localement, executer les tests dans un conteneur .NET en copiant le backend dans le conteneur.
