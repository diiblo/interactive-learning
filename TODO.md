# interactive-learning - TODO

## Phase 1 - Socle technique existant

- [x] Verifier et retablir les droits d'acces en ecriture sur le dossier du projet.
- [ ] Installer le SDK .NET localement si `dotnet` est indisponible hors Docker.
- [x] Frontend Next.js + TypeScript + TailwindCSS.
- [x] Monaco Editor installe et integre.
- [x] Backend ASP.NET Core Web API.
- [x] SQLite + Entity Framework Core.
- [x] Execution C# via Roslyn.
- [x] Correction automatique par tests structures.
- [x] XP, niveaux, badges et deblocage progressif.
- [x] Endpoints principaux: profil, cours, lecons, progression, Boss Final.
- [x] Tests backend valides via Docker.
- [x] Lint et build frontend valides.
- [x] Ajouter SQL Server LocalDB ou SQL Server Docker pour le parcours SQL.
- [x] Ajouter un service d'execution SQL controlee.
- [x] Ajouter un service de securite SQL.

## Phase 2 - Alignement du modele pedagogique

- [x] Ajouter ou simuler le champ `FinalCorrection` pour chaque lecon.
- [x] Ajouter ou simuler un resume de concept pour chaque lecon.
- [x] Ajouter ou simuler les erreurs frequentes et conseils associes.
- [x] Verifier que les DTOs exposent toute la structure pedagogique obligatoire.
- [x] Verifier que le frontend affiche titre, objectif, explication, exemple, exercice, starter code, tests, feedback, correction finale et XP.

## Phase 3 - Structure pedagogique complete

- [x] Parcours C#.
- [x] Module 1 - Fondations.
- [x] Hello World.
- [x] Variables.
- [x] Types: `int`, `string`, `bool`, `double`.
- [x] Operateurs.
- [x] Test intermediaire du module.
- [x] Module 2 - Controle du flux.
  - [x] `if` / `else`.
  - [x] `switch`.
  - [x] `for`.
  - [x] `while`.
  - [x] `foreach`.
  - [x] Test intermediaire du module.
- [x] Module 3 - Methodes.
  - [x] Creer une methode.
  - [x] Parametres.
  - [x] Valeur de retour.
  - [x] Scope.
  - [x] Surcharge.
  - [x] Test intermediaire du module.
- [x] Module 4 - POO de base.
  - [x] Classes.
  - [x] Objets.
  - [x] Proprietes.
  - [x] Constructeurs.
  - [x] Test intermediaire du module.
- [x] Module 5 - POO avancee.
  - [x] Heritage.
  - [x] Polymorphisme.
  - [x] Interfaces.
  - [x] `public` / `private` / `protected`.
  - [x] Test intermediaire du module.
- [x] Module 6 - Structures de donnees.
  - [x] Tableaux.
  - [x] `List<T>`.
  - [x] `Dictionary<TKey, TValue>`.
  - [x] LINQ.
  - [x] Test intermediaire du module.
- [x] Module 7 - Gestion des erreurs.
  - [x] `try` / `catch`.
  - [x] Exceptions.
  - [x] Nullables.
  - [x] Test intermediaire du module.
- [x] Module 8 - Base de donnees.
  - [x] Bases relationnelles.
  - [x] Entity Framework Core.
  - [x] `DbContext`.
  - [x] CRUD.
  - [x] Test intermediaire du module.
- [x] Parcours SQL / SQL Server.
  - [x] Module 1 - Fondations SQL.
  - [x] Module 2 - Trier et filtrer les donnees.
  - [x] Module 3 - Agregation.
  - [x] Module 4 - Jointures.
  - [x] Module 5 - Modification des donnees.
  - [x] Module 6 - Modelisation relationnelle.
  - [x] Module 7 - SQL Server avance.
  - [x] Module 8 - Projet pratique SQL Server.
  - [x] Boss Final SQL e-commerce.

## Phase 4 - Module 1 complet

- [x] Remplacer la lecon Hello World actuelle par la version pedagogique complete du plan.
- [x] Ajouter une correction finale pour Hello World.
- [x] Ajouter un test automatique verifiant au moins deux appels a `Console.WriteLine`.
- [x] Enrichir la lecon Variables.
- [x] Ajouter la lecon Types.
- [x] Ajouter la lecon Operateurs.
- [x] Ajouter le test intermediaire de Fondations.
- [x] Verifier que les deblocages du Module 1 fonctionnent.

## Phase 5 - Boss Final RPG

- [x] Remplacer le Boss Final actuel par une mini-application console de gestion d'inventaire RPG.
- [x] Verifier la classe `Item`.
- [x] Verifier l'ajout d'item.
- [x] Verifier l'affichage de l'inventaire.
- [x] Verifier la suppression d'item.
- [x] Verifier le calcul de valeur totale.
- [x] Verifier la gestion des erreurs utilisateur.
- [x] Verifier variables, conditions, boucles, methodes, classes, objets, `List<T>`, exceptions et logique metier simple.
- [x] Attribuer le badge final apres reussite.

## Phase 6 - Parcours SQL / SQL Server

- [x] Ajouter un deuxieme cours `sqlserver-foundations`.
- [x] Ajouter une progression independante SQL.
- [x] Ajouter des badges propres au parcours SQL.
- [x] Verifier que le niveau global cumule C# + SQL.
- [x] Ajouter les DTOs SQL.
- [x] Ajouter les endpoints `GET /api/sql/lessons/{lessonId}`.
- [x] Ajouter les endpoints `POST /api/sql/lessons/{lessonId}/run`.
- [x] Ajouter les endpoints `POST /api/sql/lessons/{lessonId}/submit`.
- [x] Ajouter l'endpoint `POST /api/sql/lessons/{lessonId}/reset`.
- [x] Ajouter les endpoints Boss Final SQL via `GET/POST /api/sql/lessons/{lessonId}`.
- [x] Ajouter `SqlSafetyService`.
- [x] Bloquer `DROP DATABASE`.
- [x] Bloquer `DROP TABLE` hors exercice prevu.
- [x] Bloquer `TRUNCATE`.
- [x] Bloquer `ALTER SERVER`.
- [x] Bloquer `EXEC` non autorise.
- [x] Bloquer l'acces aux tables systeme.
- [x] Bloquer les requetes multi-statements non prevues.
- [x] Bloquer les commandes dangereuses SQL Server.
- [x] Ajouter `SqlExecutionService`.
- [x] Ajouter le mode SQL Server Docker dans `SqlExecutionService`.
- [x] Ajouter `SqlCorrectionService`.
- [x] Verifier syntaxe SQL.
- [x] Verifier colonnes attendues.
- [x] Verifier nombre de lignes attendu.
- [x] Verifier resultats attendus.
- [x] Verifier effets `INSERT` / `UPDATE` / `DELETE`.
- [x] Ajouter `SqlResultGrid` cote frontend.
- [x] Ajouter `SqlSchemaPanel` cote frontend.
- [x] Ajouter `SqlSafetyNotice` cote frontend.
- [x] Ajouter `CourseSwitcher` C# / SQL.

## Phase 7 - Module SQL 1 complet

- [x] Creer le scenario SQL `StoreBasics`.
- [x] Ajouter la table `Categories`.
- [x] Ajouter la table `Products`.
- [x] Ajouter les donnees de depart du catalogue.
- [x] Ajouter la lecon SQL "Qu'est-ce qu'une base de donnees relationnelle ?".
- [x] Ajouter la lecon SQL "Tables, lignes, colonnes".
- [x] Ajouter la lecon SQL "Types de donnees SQL Server".
- [x] Ajouter la lecon SQL `SELECT`.
- [x] Ajouter la lecon SQL `WHERE`.
- [x] Ajouter le test intermediaire SQL "Catalogue lisible".
- [x] Valider que `SELECT` retourne les colonnes attendues.
- [x] Valider que `WHERE` filtre les lignes attendues.
- [x] Valider que les operations interdites sont refusees.
- [x] Valider XP, badges et deblocages SQL Module 1.

## Phase 7.1 - Module SQL 2 complet

- [x] Ajouter la lecon SQL `ORDER BY`.
- [x] Ajouter la lecon SQL `TOP`.
- [x] Ajouter la lecon SQL `DISTINCT`.
- [x] Ajouter la lecon SQL `LIKE`.
- [x] Ajouter la lecon SQL `IN`.
- [x] Ajouter la lecon SQL `BETWEEN`.
- [x] Ajouter la lecon SQL `IS NULL / IS NOT NULL`.
- [x] Ajouter le test intermediaire SQL "filtre precis".
- [x] Etendre l'execution SQL controlee pour `ORDER BY`.
- [x] Etendre l'execution SQL controlee pour `TOP`.
- [x] Etendre l'execution SQL controlee pour `DISTINCT`.
- [x] Etendre l'execution SQL controlee pour `LIKE`.
- [x] Etendre l'execution SQL controlee pour `IN`.
- [x] Etendre l'execution SQL controlee pour `BETWEEN`.
- [x] Etendre l'execution SQL controlee pour `IS NULL / IS NOT NULL`.
- [x] Valider le Module SQL 2 sur SQL Server Docker.

## Phase 7.2 - Module SQL 3 complet

- [x] Ajouter la lecon SQL `COUNT`.
- [x] Ajouter la lecon SQL `SUM`.
- [x] Ajouter la lecon SQL `AVG`.
- [x] Ajouter la lecon SQL `MIN / MAX`.
- [x] Ajouter la lecon SQL `GROUP BY`.
- [x] Ajouter la lecon SQL `HAVING`.
- [x] Ajouter le test intermediaire SQL "indicateurs catalogue".
- [x] Etendre l'execution SQL controlee pour `COUNT`.
- [x] Etendre l'execution SQL controlee pour `SUM`.
- [x] Etendre l'execution SQL controlee pour `AVG`.
- [x] Etendre l'execution SQL controlee pour `MIN / MAX`.
- [x] Etendre l'execution SQL controlee pour `GROUP BY`.
- [x] Etendre l'execution SQL controlee pour `HAVING`.
- [x] Valider le Module SQL 3 sur SQL Server Docker.

## Phase 7.3 - Module SQL 4 complet

- [x] Ajouter la lecon SQL `INNER JOIN`.
- [x] Ajouter la lecon SQL `LEFT JOIN`.
- [x] Ajouter la lecon SQL `RIGHT JOIN`.
- [x] Ajouter la lecon SQL `FULL OUTER JOIN`.
- [x] Ajouter la lecon SQL `Alias de tables`.
- [x] Ajouter le test intermediaire SQL "catalogue enrichi".
- [x] Autoriser les requetes `SELECT` avec jointures en mode SQL Server Docker.
- [x] Ajouter un fallback controle pour les jointures `Products` / `Categories`.
- [x] Valider le Module SQL 4 sur SQL Server Docker.

## Phase 7.4 - Module SQL 5 complet

- [x] Ajouter la lecon SQL `INSERT`.
- [x] Ajouter la lecon SQL `UPDATE`.
- [x] Ajouter la lecon SQL `DELETE`.
- [x] Ajouter la lecon SQL `Transactions simples`.
- [x] Ajouter la lecon SQL `ROLLBACK / COMMIT`.
- [x] Ajouter le test intermediaire SQL "correction de stock".
- [x] Autoriser les multi-statements uniquement pour modifications ou transactions controlees.
- [x] Bloquer `UPDATE` sans `WHERE`.
- [x] Bloquer `DELETE` sans `WHERE`.
- [x] Valider `INSERT`, `UPDATE`, `DELETE`, `COMMIT` et `ROLLBACK` sur SQL Server Docker.

## Phase 7.5 - Module SQL 6 complet

- [x] Ajouter la lecon SQL `Cles primaires`.
- [x] Ajouter la lecon SQL `Cles etrangeres`.
- [x] Ajouter la lecon SQL `Contraintes`.
- [x] Ajouter la lecon SQL `Normalisation simple`.
- [x] Ajouter la lecon SQL `Relations 1-N et N-N`.
- [x] Ajouter le test intermediaire SQL "mini-modele cours".
- [x] Autoriser `CREATE TABLE` pour les scripts pedagogiques controles.
- [x] Reinitialiser les tables de modelisation avant chaque execution SQL Server.
- [x] Conserver le blocage de `DROP TABLE`, `TRUNCATE`, `EXEC` et tables systeme.

## Phase 7.6 - Module SQL 7 complet

- [x] Ajouter la lecon SQL `Index`.
- [x] Ajouter la lecon SQL `Vues`.
- [x] Ajouter la lecon SQL `Procedures stockees`.
- [x] Ajouter la lecon SQL `Fonctions`.
- [x] Ajouter la lecon SQL `Variables T-SQL`.
- [x] Ajouter le test intermediaire SQL "objet metier reutilisable".
- [x] Autoriser `CREATE INDEX`, `CREATE VIEW`, `CREATE PROCEDURE`, `CREATE FUNCTION` et `DECLARE` pour les scripts pedagogiques controles.
- [x] Autoriser `EXEC` uniquement pour la procedure pedagogique `GetActiveProducts`.
- [x] Reinitialiser les vues, procedures et fonctions avant chaque execution SQL Server.
- [x] Conserver le blocage de `DROP TABLE`, `TRUNCATE`, tables systeme et `EXEC` non autorise.

## Phase 7.7 - Module SQL 8 complet

- [x] Ajouter la lecon SQL `Creation d'un schema complet`.
- [x] Ajouter la lecon SQL `Creation des tables`.
- [x] Ajouter la lecon SQL `Insertion de donnees`.
- [x] Ajouter la lecon SQL `Requetes metier`.
- [x] Ajouter la lecon SQL `Optimisation simple`.
- [x] Ajouter le test intermediaire SQL "total des commandes".
- [x] Reinitialiser `Customers`, `Orders` et `OrderItems` avant chaque execution SQL Server.
- [x] Valider les creations de tables e-commerce.
- [x] Valider les insertions relationnelles.
- [x] Valider les jointures metier.
- [x] Valider l'agregation `SUM` + `GROUP BY`.
- [x] Valider l'index simple sur `Orders(CustomerId)`.

## Phase 8 - Boss Final SQL e-commerce

- [x] Creer le sujet Boss Final SQL.
- [x] Ajouter le Boss Final SQL dans la carte du parcours SQL.
- [x] Exposer le Boss Final SQL via les endpoints SQL existants.
- [x] Verifier la creation de `Customers`.
- [x] Verifier la creation de `Products`.
- [x] Verifier la creation de `Orders`.
- [x] Verifier la creation de `OrderItems`.
- [x] Verifier les cles primaires.
- [x] Verifier les cles etrangeres.
- [x] Verifier les `INSERT`.
- [x] Verifier `SELECT`.
- [x] Verifier `WHERE`.
- [x] Verifier `JOIN`.
- [x] Verifier `GROUP BY`.
- [x] Verifier `HAVING`.
- [x] Verifier `UPDATE`.
- [x] Verifier `DELETE` controle.
- [x] Verifier une transaction simple.

## Phase 9 - Tests et validation

- [x] Run backend build.
- [x] Run backend tests.
- [x] Run frontend lint.
- [x] Run frontend production build.
- [x] Tester manuellement une lecon reussie.
- [x] Tester manuellement le feedback d'erreur de compilation.
- [x] Tester manuellement le feedback de tests echoues.
- [x] Tester manuellement le gain XP.
- [x] Tester manuellement le changement de niveau.
- [x] Tester manuellement les badges.
- [x] Tester manuellement le deblocage du Boss Final.
- [x] Tester manuellement la completion du Boss Final RPG.
- [x] Tester manuellement une lecon SQL reussie.
- [x] Tester manuellement une erreur syntaxique SQL.
- [x] Tester manuellement une operation SQL interdite.
- [x] Tester manuellement le Boss Final SQL.

## Phase 10 - Documentation

- [x] Documenter les commandes de lancement.
- [x] Documenter les variables frontend.
- [x] Documenter les limites Roslyn.
- [x] Documenter le plan pedagogique complet dans `PLAN.md`.
- [x] Documenter le plan pedagogique SQL dans `PLAN.md`.
- [x] Documenter la strategie de creation de contenu par module.
- [x] Documenter comment ajouter une nouvelle lecon.
- [x] Documenter comment ajouter une lecon SQL.
- [x] Documenter la politique de securite SQL.
