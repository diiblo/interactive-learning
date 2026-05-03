using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Data;

public static class SeedData
{
    public static async Task EnsureSeededAsync(AppDbContext db)
    {
        var seededFullCatalog = false;
        if (!await db.Courses.AnyAsync())
        {
            seededFullCatalog = true;
            var course = new Course
            {
                Slug = "csharp-foundations",
                Title = "C# Fondations",
                Description = "Un parcours progressif pour apprendre C# par la pratique.",
                Language = "csharp",
                SortOrder = 1,
                Chapters =
                [
                    Chapter("Module 1 - Fondations", "Premiers programmes, variables, types et operateurs.", 1, 0,
                    [
                        Lesson(
                            "hello-world",
                            "Hello World",
                            "Comprendre qu'un programme C# peut afficher du texte dans la console avec Console.WriteLine.",
                            "Console.WriteLine ecrit une ligne de texte dans la console. Le texte doit etre place entre guillemets. Une instruction C# se termine generalement par un point-virgule.",
                            "Console.WriteLine(\"Bonjour C#\");",
                            "Affiche exactement deux lignes: Hello, World! puis Je commence C#.",
                            """
                            using System;

                            Console.WriteLine("Hello, World!");
                            // Ajoute une deuxieme ligne ici
                            """,
                            "Parfait, ton premier programme C# affiche les deux lignes attendues.",
                            "Verifie les guillemets, les points-virgules et le texte exact a afficher.",
                            25,
                            1,
                            [
                                Output("Affiche Hello World", "Hello, World!"),
                                Output("Affiche la deuxieme ligne", "Je commence C#"),
                                Snippet("Utilise Console.WriteLine", "Console.WriteLine"),
                                Count("Utilise deux affichages", "Console.WriteLine", 2)
                            ],
                            conceptSummary: "Console.WriteLine affiche une ligne. Le texte va entre guillemets et chaque instruction se termine par un point-virgule.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("Hello, World!");
                            Console.WriteLine("Je commence C#");
                            """),
                        Lesson(
                            "variables",
                            "Variables",
                            "Stocker une information dans une variable et la reutiliser dans un affichage.",
                            "Une variable garde une valeur en memoire. En C#, on indique souvent son type avant son nom. Le type string sert a stocker du texte.",
                            "string hero = \"Ada\";\nConsole.WriteLine(hero);",
                            "Cree une variable name contenant Ada, puis affiche Bonjour Ada en utilisant la variable.",
                            """
                            using System;

                            string name = "...";
                            // Affiche Bonjour Ada en utilisant name
                            """,
                            "La variable est declaree et reutilisee dans l'affichage.",
                            "Declare string name avec la valeur Ada, puis utilise name dans Console.WriteLine.",
                            35,
                            2,
                            [
                                Snippet("Declare une variable string", "string name"),
                                Snippet("Stocke Ada", "\"Ada\""),
                                Snippet("Utilise la variable", "name"),
                                Output("Affiche le message attendu", "Bonjour Ada")
                            ],
                            conceptSummary: "Une variable a un nom, un type et une valeur. Elle evite de repeter une information en dur dans tout le programme.",
                            finalCorrection:
                            """
                            using System;

                            string name = "Ada";
                            Console.WriteLine("Bonjour " + name);
                            """),
                        Lesson(
                            "types",
                            "Types: int, string, bool, double",
                            "Utiliser les types de base pour representer des nombres, du texte et des valeurs vrai/faux.",
                            "C# utilise des types pour savoir quelle nature de valeur une variable contient. int stocke un entier, string du texte, bool vrai ou faux, double un nombre decimal.",
                            "int level = 3;\nstring name = \"Ada\";\nbool isReady = true;\ndouble score = 9.5;",
                            "Declare un joueur Ada de niveau 3, pret a jouer, avec un score de 9.5, puis affiche Ada niveau 3.",
                            """
                            using System;

                            int level = 0;
                            string name = "";
                            bool isReady = false;
                            double score = 0;

                            // Affiche Ada niveau 3
                            """,
                            "Les quatre types de base sont utilises correctement.",
                            "Verifie les types demandes: int, string, bool et double. Le texte attendu doit contenir Ada niveau 3.",
                            40,
                            3,
                            [
                                Snippet("Utilise int", "int level"),
                                Snippet("Utilise string", "string name"),
                                Snippet("Utilise bool", "bool isReady"),
                                Snippet("Utilise double", "double score"),
                                Output("Affiche Ada niveau 3", "Ada niveau 3")
                            ],
                            conceptSummary: "Le type decrit la forme d'une donnee. C# peut ainsi proteger le programme contre beaucoup d'erreurs.",
                            finalCorrection:
                            """
                            using System;

                            int level = 3;
                            string name = "Ada";
                            bool isReady = true;
                            double score = 9.5;

                            Console.WriteLine(name + " niveau " + level);
                            """),
                        Lesson(
                            "operators",
                            "Operateurs",
                            "Combiner des valeurs avec des calculs, des comparaisons et de la concatenation.",
                            "Les operateurs permettent de calculer avec +, -, *, /, de comparer avec >= ou ==, et de construire du texte avec +.",
                            "int total = 10 + 5;\nbool enough = total >= 12;\nConsole.WriteLine(\"Total: \" + total);",
                            "Calcule la valeur totale de 3 potions a 5 pieces, verifie si le total vaut au moins 15, puis affiche Total: 15 et Achat possible: True.",
                            """
                            using System;

                            int quantity = 3;
                            int price = 5;
                            int total = 0;
                            bool canBuy = false;

                            Console.WriteLine("Total: " + total);
                            Console.WriteLine("Achat possible: " + canBuy);
                            """,
                            "Les calculs, la comparaison et les affichages sont corrects.",
                            "Utilise quantity * price pour calculer total, puis une comparaison pour canBuy.",
                            45,
                            4,
                            [
                                Snippet("Multiplie quantite et prix", "quantity * price"),
                                Snippet("Compare le total", "total >= 15"),
                                Output("Affiche le total", "Total: 15"),
                                Output("Affiche la possibilite d'achat", "Achat possible: True")
                            ],
                            conceptSummary: "Un operateur transforme ou compare des valeurs. Le resultat peut etre un nombre, du texte ou un booleen.",
                            finalCorrection:
                            """
                            using System;

                            int quantity = 3;
                            int price = 5;
                            int total = quantity * price;
                            bool canBuy = total >= 15;

                            Console.WriteLine("Total: " + total);
                            Console.WriteLine("Achat possible: " + canBuy);
                            """),
                        Lesson(
                            "foundations-checkpoint",
                            "Test intermediaire: carte de joueur",
                            "Assembler affichage, variables, types et operateurs dans un petit programme lisible.",
                            "Un programme utile combine souvent plusieurs notions: des variables pour stocker les donnees, des operateurs pour calculer, puis Console.WriteLine pour afficher le resultat.",
                            "string player = \"Ada\";\nint points = 40 + 2;\nConsole.WriteLine(player + \" score \" + points);",
                            "Cree une carte joueur pour Ada: niveau 3, bonus 7, score final 37, actif True. Affiche Ada - score 37 puis Actif: True.",
                            """
                            using System;

                            string player = "Ada";
                            int level = 3;
                            int baseScore = 30;
                            int bonus = 0;
                            bool active = false;

                            // Calcule finalScore et affiche la carte joueur
                            """,
                            "Module 1 valide. Tu sais afficher, stocker, typer et calculer des valeurs simples.",
                            "Reprends chaque piece: variables, int/string/bool, addition, puis deux Console.WriteLine.",
                            60,
                            5,
                            [
                                Snippet("Utilise string", "string player"),
                                Snippet("Utilise int", "int level"),
                                Snippet("Utilise bool", "bool active"),
                                Snippet("Calcule le score final", "baseScore + bonus"),
                                Count("Affiche deux lignes", "Console.WriteLine", 2),
                                Output("Affiche le score final", "Ada - score 37"),
                                Output("Affiche le statut", "Actif: True")
                            ],
                            conceptSummary: "Ce test confirme que les briques de base peuvent etre combinees dans un mini-programme coherent.",
                            finalCorrection:
                            """
                            using System;

                            string player = "Ada";
                            int level = 3;
                            int baseScore = 30;
                            int bonus = 7;
                            bool active = true;
                            int finalScore = baseScore + bonus;

                            Console.WriteLine(player + " - score " + finalScore);
                            Console.WriteLine("Actif: " + active);
                            """)
                    ]),
                    Chapter("Module 2 - Controle du flux", "Prendre des decisions et repeter des actions.", 2, 50,
                    [
                        Lesson(
                            "if-else",
                            "if / else",
                            "Choisir entre deux chemins avec if et else.",
                            "Une instruction if execute un bloc quand une condition est vraie. Le bloc else s'execute quand elle est fausse.",
                            "int age = 18;\nif (age >= 18) Console.WriteLine(\"Majeur\");\nelse Console.WriteLine(\"Mineur\");",
                            "Avec age egal a 17, affiche Mineur en utilisant if et else.",
                            """
                            using System;

                            int age = 17;

                            // Ecris un if / else ici
                            """,
                            "La branche else est utilisee correctement.",
                            "Teste age >= 18 dans le if, puis affiche Mineur dans le else.",
                            45,
                            1,
                            [
                                Snippet("Utilise if", "if"),
                                Snippet("Utilise else", "else"),
                                Output("Affiche Mineur", "Mineur")
                            ],
                            conceptSummary: "if / else permet d'exprimer une decision binaire a partir d'une condition booleenne.",
                            finalCorrection:
                            """
                            using System;

                            int age = 17;

                            if (age >= 18)
                            {
                                Console.WriteLine("Majeur");
                            }
                            else
                            {
                                Console.WriteLine("Mineur");
                            }
                            """),
                        Lesson(
                            "switch",
                            "switch",
                            "Choisir un bloc selon une valeur precise.",
                            "switch compare une valeur a plusieurs cas. Chaque case decrit une reponse possible.",
                            "string role = \"admin\";\nswitch (role) { case \"admin\": Console.WriteLine(\"Acces complet\"); break; }",
                            "Avec rank egal a Silver, affiche Ligue intermediaire en utilisant switch.",
                            """
                            using System;

                            string rank = "Silver";

                            // Ecris un switch ici
                            """,
                            "Le switch choisit le bon cas.",
                            "Ajoute un case \"Silver\" qui affiche Ligue intermediaire.",
                            50,
                            2,
                            [
                                Snippet("Utilise switch", "switch"),
                                Snippet("Declare le case Silver", "case \"Silver\""),
                                Output("Affiche la ligue", "Ligue intermediaire")
                            ],
                            conceptSummary: "switch rend lisibles les choix bases sur une valeur discrete comme un texte, un nombre ou une enum.",
                            finalCorrection:
                            """
                            using System;

                            string rank = "Silver";

                            switch (rank)
                            {
                                case "Bronze":
                                    Console.WriteLine("Ligue debutant");
                                    break;
                                case "Silver":
                                    Console.WriteLine("Ligue intermediaire");
                                    break;
                                default:
                                    Console.WriteLine("Ligue inconnue");
                                    break;
                            }
                            """),
                        Lesson(
                            "for-loop",
                            "for",
                            "Repeter une action un nombre connu de fois.",
                            "Une boucle for contient une initialisation, une condition et une iteration. Elle est adaptee aux compteurs.",
                            "for (int i = 1; i <= 3; i++) Console.WriteLine(i);",
                            "Affiche Niveau 1, Niveau 2 et Niveau 3 avec une boucle for.",
                            """
                            using System;

                            // Ecris une boucle for ici
                            """,
                            "La boucle for produit la bonne sequence.",
                            "Utilise un compteur de 1 a 3 et concatene-le avec Niveau.",
                            50,
                            3,
                            [
                                Snippet("Utilise for", "for"),
                                Output("Affiche Niveau 1", "Niveau 1"),
                                Output("Affiche Niveau 2", "Niveau 2"),
                                Output("Affiche Niveau 3", "Niveau 3")
                            ],
                            conceptSummary: "for est ideal quand le nombre de repetitions est connu a l'avance.",
                            finalCorrection:
                            """
                            using System;

                            for (int level = 1; level <= 3; level++)
                            {
                                Console.WriteLine("Niveau " + level);
                            }
                            """),
                        Lesson(
                            "while-loop",
                            "while",
                            "Repeter une action tant qu'une condition reste vraie.",
                            "while teste la condition avant chaque tour. Il faut modifier une valeur dans la boucle pour eviter une boucle infinie.",
                            "int energy = 3;\nwhile (energy > 0) { Console.WriteLine(energy); energy--; }",
                            "Avec energy egal a 3, affiche Energie 3, Energie 2, Energie 1 avec while.",
                            """
                            using System;

                            int energy = 3;

                            // Ecris une boucle while ici
                            """,
                            "La boucle while s'arrete correctement.",
                            "Teste energy > 0 et diminue energy a chaque tour.",
                            50,
                            4,
                            [
                                Snippet("Utilise while", "while"),
                                Snippet("Diminue energy", "energy--"),
                                Output("Affiche Energie 3", "Energie 3"),
                                Output("Affiche Energie 2", "Energie 2"),
                                Output("Affiche Energie 1", "Energie 1")
                            ],
                            conceptSummary: "while convient quand on repete jusqu'a ce qu'une condition change.",
                            finalCorrection:
                            """
                            using System;

                            int energy = 3;

                            while (energy > 0)
                            {
                                Console.WriteLine("Energie " + energy);
                                energy--;
                            }
                            """),
                        Lesson(
                            "foreach-loop",
                            "foreach",
                            "Parcourir tous les elements d'une collection.",
                            "foreach donne chaque element d'une collection sans gerer soi-meme un index.",
                            "foreach (string name in names) Console.WriteLine(name);",
                            "Parcours la liste quests et affiche chaque quete.",
                            """
                            using System;
                            using System.Collections.Generic;

                            var quests = new List<string> { "Tutoriel", "Donjon", "Boss" };

                            // Ecris une boucle foreach ici
                            """,
                            "Chaque element de la liste est affiche.",
                            "Utilise foreach avec une variable quest et affiche quest.",
                            50,
                            5,
                            [
                                Snippet("Utilise foreach", "foreach"),
                                Output("Affiche Tutoriel", "Tutoriel"),
                                Output("Affiche Donjon", "Donjon"),
                                Output("Affiche Boss", "Boss")
                            ],
                            conceptSummary: "foreach simplifie le parcours des tableaux, listes et autres collections.",
                            finalCorrection:
                            """
                            using System;
                            using System.Collections.Generic;

                            var quests = new List<string> { "Tutoriel", "Donjon", "Boss" };

                            foreach (string quest in quests)
                            {
                                Console.WriteLine(quest);
                            }
                            """),
                        Lesson(
                            "flow-checkpoint",
                            "Test intermediaire du module",
                            "Combiner conditions et boucles dans un programme lisible.",
                            "Le controle du flux permet de decider, repeter et traiter plusieurs valeurs dans un ordre precis.",
                            "for (int i = 1; i <= 3; i++) if (i >= 2) Console.WriteLine(i);",
                            "Affiche les tours 1 a 3, puis indique Victoire quand score vaut au moins 30.",
                            """
                            using System;

                            int score = 30;

                            // Affiche Tour 1 a Tour 3 avec une boucle
                            // Puis affiche Victoire ou Rejouer avec if / else
                            """,
                            "Module 2 valide. Tu sais controler l'ordre d'execution.",
                            "Utilise une boucle pour les tours, puis un if / else sur score >= 30.",
                            65,
                            6,
                            [
                                Snippet("Utilise une boucle", "for"),
                                Snippet("Utilise if", "if"),
                                Snippet("Utilise else", "else"),
                                Output("Affiche Tour 1", "Tour 1"),
                                Output("Affiche Tour 2", "Tour 2"),
                                Output("Affiche Tour 3", "Tour 3"),
                                Output("Affiche Victoire", "Victoire")
                            ],
                            conceptSummary: "Les conditions et les boucles forment la base des programmes interactifs.",
                            finalCorrection:
                            """
                            using System;

                            int score = 30;

                            for (int turn = 1; turn <= 3; turn++)
                            {
                                Console.WriteLine("Tour " + turn);
                            }

                            if (score >= 30)
                            {
                                Console.WriteLine("Victoire");
                            }
                            else
                            {
                                Console.WriteLine("Rejouer");
                            }
                            """)
                    ]),
                    Chapter("Module 3 - Methodes", "Structurer le code en actions reutilisables.", 3, 120,
                    [
                        Lesson(
                            "create-method",
                            "Creer une methode",
                            "Regrouper une action reutilisable dans une methode.",
                            "Une methode est un bloc nomme que l'on peut appeler plusieurs fois. static void DireBonjour() declare une methode sans valeur de retour.",
                            "static void DireBonjour() { Console.WriteLine(\"Bonjour\"); }\nDireBonjour();",
                            "Cree une methode SayHello qui affiche Hello Method, puis appelle-la.",
                            """
                            using System;

                            static void SayHello()
                            {
                                // Affiche le message ici
                            }

                            SayHello();
                            """,
                            "La methode est declaree et appelee correctement.",
                            "Declare SayHello, affiche Hello Method dans la methode, puis appelle SayHello().",
                            55,
                            1,
                            [
                                Snippet("Declare SayHello", "static void SayHello"),
                                Snippet("Appelle SayHello", "SayHello()"),
                                Output("Affiche le message", "Hello Method")
                            ],
                            conceptSummary: "Une methode donne un nom a une action et evite de dupliquer du code.",
                            finalCorrection:
                            """
                            using System;

                            static void SayHello()
                            {
                                Console.WriteLine("Hello Method");
                            }

                            SayHello();
                            """),
                        Lesson(
                            "method-parameters",
                            "Parametres",
                            "Transmettre une valeur a une methode.",
                            "Un parametre est une variable declaree dans la signature d'une methode. L'appel fournit sa valeur.",
                            "static void Greet(string name) { Console.WriteLine(name); }\nGreet(\"Ada\");",
                            "Cree une methode ShowPlayer avec un parametre name et affiche Joueur: Ada.",
                            """
                            using System;

                            static void ShowPlayer(string name)
                            {
                                // Affiche Joueur: Ada avec name
                            }

                            ShowPlayer("Ada");
                            """,
                            "Le parametre est utilise dans l'affichage.",
                            "Concatene Joueur: avec le parametre name.",
                            60,
                            2,
                            [
                                Snippet("Declare un parametre", "string name"),
                                Snippet("Appelle avec Ada", "ShowPlayer(\"Ada\")"),
                                Output("Affiche le joueur", "Joueur: Ada")
                            ],
                            conceptSummary: "Les parametres rendent une methode configurable.",
                            finalCorrection:
                            """
                            using System;

                            static void ShowPlayer(string name)
                            {
                                Console.WriteLine("Joueur: " + name);
                            }

                            ShowPlayer("Ada");
                            """),
                        Lesson(
                            "return-value",
                            "Valeur de retour",
                            "Faire produire un resultat par une methode.",
                            "Une methode non void renvoie une valeur avec return. Le type de retour annonce la nature du resultat.",
                            "static int Add(int a, int b) { return a + b; }",
                            "Cree une methode AddBonus qui retourne score + bonus, puis affiche Score final: 42.",
                            """
                            using System;

                            static int AddBonus(int score, int bonus)
                            {
                                return 0;
                            }

                            int finalScore = AddBonus(35, 7);
                            Console.WriteLine("Score final: " + finalScore);
                            """,
                            "La methode retourne le bon resultat.",
                            "Remplace return 0 par return score + bonus.",
                            60,
                            3,
                            [
                                Snippet("Retourne un int", "static int AddBonus"),
                                Snippet("Utilise return", "return"),
                                Snippet("Additionne", "score + bonus"),
                                Output("Affiche le score", "Score final: 42")
                            ],
                            conceptSummary: "Une valeur de retour permet d'utiliser le resultat d'une methode ailleurs dans le programme.",
                            finalCorrection:
                            """
                            using System;

                            static int AddBonus(int score, int bonus)
                            {
                                return score + bonus;
                            }

                            int finalScore = AddBonus(35, 7);
                            Console.WriteLine("Score final: " + finalScore);
                            """),
                        Lesson(
                            "scope",
                            "Scope",
                            "Comprendre ou une variable est accessible.",
                            "Le scope est la zone du code ou un nom existe. Une variable declaree dans une methode n'est pas visible partout.",
                            "static void PrintName() { string name = \"Ada\"; Console.WriteLine(name); }",
                            "Declare message dans la methode ShowMessage et affiche Scope OK.",
                            """
                            using System;

                            static void ShowMessage()
                            {
                                // Declare message ici
                                Console.WriteLine(message);
                            }

                            ShowMessage();
                            """,
                            "La variable est declaree dans le bon scope.",
                            "Declare string message avant de l'utiliser dans ShowMessage.",
                            55,
                            4,
                            [
                                Snippet("Declare message", "string message"),
                                Snippet("Appelle la methode", "ShowMessage()"),
                                Output("Affiche Scope OK", "Scope OK")
                            ],
                            conceptSummary: "Declarer une variable pres de son usage rend le code plus clair et limite les erreurs.",
                            finalCorrection:
                            """
                            using System;

                            static void ShowMessage()
                            {
                                string message = "Scope OK";
                                Console.WriteLine(message);
                            }

                            ShowMessage();
                            """),
                        Lesson(
                            "overload",
                            "Surcharge",
                            "Declarer plusieurs methodes avec le meme nom et des parametres differents.",
                            "La surcharge permet d'avoir le meme nom de methode quand les signatures sont differentes.",
                            "static void Show(int value) { }\nstatic void Show(string value) { }",
                            "Cree deux methodes Describe dans PlayerPrinter: une pour string, une pour int. Affiche Nom: Ada puis Niveau: 3.",
                            """
                            using System;

                            PlayerPrinter.Describe("Ada");
                            PlayerPrinter.Describe(3);

                            class PlayerPrinter
                            {
                                // Ajoute les deux surcharges Describe
                            }
                            """,
                            "Les deux surcharges sont appelees correctement.",
                            "Declare Describe(string name) et Describe(int level).",
                            65,
                            5,
                            [
                                Snippet("Surcharge string", "Describe(string"),
                                Snippet("Surcharge int", "Describe(int"),
                                Output("Affiche le nom", "Nom: Ada"),
                                Output("Affiche le niveau", "Niveau: 3")
                            ],
                            conceptSummary: "La surcharge garde une intention commune tout en acceptant des formes de donnees differentes.",
                            finalCorrection:
                            """
                            using System;

                            PlayerPrinter.Describe("Ada");
                            PlayerPrinter.Describe(3);

                            class PlayerPrinter
                            {
                                public static void Describe(string name)
                                {
                                    Console.WriteLine("Nom: " + name);
                                }

                                public static void Describe(int level)
                                {
                                    Console.WriteLine("Niveau: " + level);
                                }
                            }
                            """),
                        Lesson(
                            "methods-checkpoint",
                            "Test intermediaire du module",
                            "Composer plusieurs methodes pour produire un resultat.",
                            "Un programme plus grand se lit mieux quand chaque methode porte une responsabilite claire.",
                            "static int Double(int value) { return value * 2; }",
                            "Cree FormatName et ComputeTotal, puis affiche Ada total 12.",
                            """
                            using System;

                            static string FormatName(string name)
                            {
                                return "";
                            }

                            static int ComputeTotal(int baseScore, int bonus)
                            {
                                return 0;
                            }

                            Console.WriteLine(FormatName("Ada") + " total " + ComputeTotal(7, 5));
                            """,
                            "Module 3 valide. Tes methodes cooperent correctement.",
                            "Retourne name depuis FormatName et baseScore + bonus depuis ComputeTotal.",
                            75,
                            6,
                            [
                                Snippet("Methode string", "static string FormatName"),
                                Snippet("Methode int", "static int ComputeTotal"),
                                Snippet("Utilise return", "return"),
                                Output("Affiche le total", "Ada total 12")
                            ],
                            conceptSummary: "Les methodes permettent de decouper une solution en petites etapes testables.",
                            finalCorrection:
                            """
                            using System;

                            static string FormatName(string name)
                            {
                                return name;
                            }

                            static int ComputeTotal(int baseScore, int bonus)
                            {
                                return baseScore + bonus;
                            }

                            Console.WriteLine(FormatName("Ada") + " total " + ComputeTotal(7, 5));
                            """)
                    ]),
                    Chapter("Module 4 - POO de base", "Creer des types metier simples.", 4, 180,
                    [
                        Lesson(
                            "classes",
                            "Classes",
                            "Declarer un modele d'objet avec class.",
                            "Une classe decrit les donnees et comportements communs a des objets.",
                            "class Player { public string Name = \"\"; }",
                            "Declare une classe Player et affiche Classe Player.",
                            """
                            using System;

                            Console.WriteLine("Classe Player");

                            // Declare la classe Player ici
                            """,
                            "La classe Player est presente.",
                            "Ajoute class Player apres les instructions principales.",
                            55,
                            1,
                            [
                                Snippet("Declare Player", "class Player"),
                                Output("Affiche Classe Player", "Classe Player")
                            ],
                            conceptSummary: "Une classe sert de plan pour creer des objets.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("Classe Player");

                            class Player
                            {
                            }
                            """),
                        Lesson(
                            "objects",
                            "Objets",
                            "Instancier une classe avec new.",
                            "Un objet est une instance concrete d'une classe. new cree cette instance en memoire.",
                            "var player = new Player();",
                            "Cree un objet Player et affiche Objet cree.",
                            """
                            using System;

                            // Cree un objet Player ici
                            Console.WriteLine("Objet cree");

                            class Player
                            {
                            }
                            """,
                            "L'objet est instancie.",
                            "Utilise new Player() avant l'affichage.",
                            55,
                            2,
                            [
                                Snippet("Instancie Player", "new Player()"),
                                Output("Affiche Objet cree", "Objet cree")
                            ],
                            conceptSummary: "Une classe est le plan; un objet est l'element utilisable cree depuis ce plan.",
                            finalCorrection:
                            """
                            using System;

                            var player = new Player();
                            Console.WriteLine("Objet cree");

                            class Player
                            {
                            }
                            """),
                        Lesson(
                            "properties",
                            "Proprietes",
                            "Stocker l'etat d'un objet dans une propriete.",
                            "Une propriete expose une donnee d'un objet avec get et set.",
                            "public string Name { get; set; } = \"\";",
                            "Ajoute une propriete Name a Player, donne-lui Ada, puis affiche Ada.",
                            """
                            using System;

                            var player = new Player();
                            player.Name = "Ada";
                            Console.WriteLine(player.Name);

                            class Player
                            {
                                // Ajoute Name ici
                            }
                            """,
                            "La propriete est lue et modifiee.",
                            "Ajoute public string Name { get; set; } = \"\"; dans Player.",
                            60,
                            3,
                            [
                                Snippet("Declare Name", "public string Name"),
                                Snippet("Utilise get set", "get; set;"),
                                Output("Affiche Ada", "Ada")
                            ],
                            conceptSummary: "Les proprietes representent l'etat public utile d'un objet.",
                            finalCorrection:
                            """
                            using System;

                            var player = new Player();
                            player.Name = "Ada";
                            Console.WriteLine(player.Name);

                            class Player
                            {
                                public string Name { get; set; } = "";
                            }
                            """),
                        Lesson(
                            "constructors",
                            "Constructeurs",
                            "Initialiser un objet au moment de sa creation.",
                            "Un constructeur porte le meme nom que la classe et prepare l'objet avec des valeurs initiales.",
                            "public Player(string name) { Name = name; }",
                            "Ajoute un constructeur Player(string name), cree Player(\"Ada\"), puis affiche Ada.",
                            """
                            using System;

                            var player = new Player("Ada");
                            Console.WriteLine(player.Name);

                            class Player
                            {
                                public string Name { get; set; } = "";

                                // Ajoute le constructeur ici
                            }
                            """,
                            "Le constructeur initialise l'objet.",
                            "Declare public Player(string name) et affecte Name = name.",
                            65,
                            4,
                            [
                                Snippet("Declare le constructeur", "public Player(string name)"),
                                Snippet("Affecte Name", "Name = name"),
                                Output("Affiche Ada", "Ada")
                            ],
                            conceptSummary: "Un constructeur garantit qu'un objet demarre avec un etat coherent.",
                            finalCorrection:
                            """
                            using System;

                            var player = new Player("Ada");
                            Console.WriteLine(player.Name);

                            class Player
                            {
                                public string Name { get; set; } = "";

                                public Player(string name)
                                {
                                    Name = name;
                                }
                            }
                            """),
                        Lesson(
                            "oop-basics-checkpoint",
                            "Test intermediaire du module",
                            "Creer une classe complete avec objet, proprietes et constructeur.",
                            "La POO de base rassemble une classe, un constructeur et des proprietes pour modeliser une entite.",
                            "var item = new Item(\"Potion\", 5);",
                            "Cree Item avec Name et Price, puis affiche Potion coute 5.",
                            """
                            using System;

                            var item = new Item("Potion", 5);
                            Console.WriteLine(item.Name + " coute " + item.Price);

                            // Declare Item ici
                            """,
                            "Module 4 valide. Ton objet porte un etat coherent.",
                            "Cree une classe Item avec Name, Price et un constructeur.",
                            80,
                            5,
                            [
                                Snippet("Declare Item", "class Item"),
                                Snippet("Propriete Name", "public string Name"),
                                Snippet("Propriete Price", "public int Price"),
                                Snippet("Constructeur", "public Item("),
                                Output("Affiche l'item", "Potion coute 5")
                            ],
                            conceptSummary: "Les objets rapprochent les donnees et le code qui les manipule.",
                            finalCorrection:
                            """
                            using System;

                            var item = new Item("Potion", 5);
                            Console.WriteLine(item.Name + " coute " + item.Price);

                            class Item
                            {
                                public string Name { get; set; }
                                public int Price { get; set; }

                                public Item(string name, int price)
                                {
                                    Name = name;
                                    Price = price;
                                }
                            }
                            """)
                    ]),
                    Chapter("Module 5 - POO avancee", "Specialiser et proteger les objets.", 5, 240,
                    [
                        Lesson(
                            "inheritance",
                            "Heritage",
                            "Creer une classe specialisee a partir d'une classe de base.",
                            "L'heritage permet a une classe derivee de reutiliser les membres d'une classe de base.",
                            "class Warrior : Character { }",
                            "Cree Warrior qui herite de Character et affiche Guerrier pret.",
                            """
                            using System;

                            var warrior = new Warrior();
                            warrior.Name = "Guerrier";
                            Console.WriteLine(warrior.Name + " pret");

                            class Character
                            {
                                public string Name { get; set; } = "";
                            }

                            // Declare Warrior ici
                            """,
                            "La classe derivee reutilise Name.",
                            "Declare class Warrior : Character.",
                            65,
                            1,
                            [
                                Snippet("Utilise heritage", "class Warrior : Character"),
                                Output("Affiche Guerrier pret", "Guerrier pret")
                            ],
                            conceptSummary: "L'heritage exprime une relation de specialisation.",
                            finalCorrection:
                            """
                            using System;

                            var warrior = new Warrior();
                            warrior.Name = "Guerrier";
                            Console.WriteLine(warrior.Name + " pret");

                            class Character
                            {
                                public string Name { get; set; } = "";
                            }

                            class Warrior : Character
                            {
                            }
                            """),
                        Lesson(
                            "polymorphism",
                            "Polymorphisme",
                            "Redefinir un comportement dans une classe derivee.",
                            "Avec virtual et override, une classe derivee peut remplacer une methode de la classe de base.",
                            "public virtual string Attack() => \"Base\";\npublic override string Attack() => \"Slash\";",
                            "Redefinis Attack dans Mage pour afficher Boule de feu.",
                            """
                            using System;

                            Character character = new Mage();
                            Console.WriteLine(character.Attack());

                            class Character
                            {
                                public virtual string Attack()
                                {
                                    return "Attaque";
                                }
                            }

                            class Mage : Character
                            {
                                // Redefinis Attack ici
                            }
                            """,
                            "L'appel utilise la version specialisee.",
                            "Ajoute public override string Attack() et retourne Boule de feu.",
                            70,
                            2,
                            [
                                Snippet("Methode virtual", "virtual"),
                                Snippet("Methode override", "override"),
                                Output("Affiche Boule de feu", "Boule de feu")
                            ],
                            conceptSummary: "Le polymorphisme permet d'appeler une abstraction et d'obtenir le comportement concret.",
                            finalCorrection:
                            """
                            using System;

                            Character character = new Mage();
                            Console.WriteLine(character.Attack());

                            class Character
                            {
                                public virtual string Attack()
                                {
                                    return "Attaque";
                                }
                            }

                            class Mage : Character
                            {
                                public override string Attack()
                                {
                                    return "Boule de feu";
                                }
                            }
                            """),
                        Lesson(
                            "interfaces",
                            "Interfaces",
                            "Definir un contrat que plusieurs classes peuvent respecter.",
                            "Une interface decrit ce qu'une classe doit fournir, sans imposer toute son implementation.",
                            "interface IPlayable { void Play(); }",
                            "Cree IDescribable et fais implementer Describe par Item pour afficher Objet rare.",
                            """
                            using System;

                            IDescribable item = new Item();
                            item.Describe();

                            // Declare IDescribable et Item ici
                            """,
                            "L'interface est implementee.",
                            "Declare interface IDescribable avec void Describe(), puis class Item : IDescribable.",
                            70,
                            3,
                            [
                                Snippet("Declare interface", "interface IDescribable"),
                                Snippet("Implemente interface", "class Item : IDescribable"),
                                Output("Affiche Objet rare", "Objet rare")
                            ],
                            conceptSummary: "Les interfaces decouplent le code de la classe concrete.",
                            finalCorrection:
                            """
                            using System;

                            IDescribable item = new Item();
                            item.Describe();

                            interface IDescribable
                            {
                                void Describe();
                            }

                            class Item : IDescribable
                            {
                                public void Describe()
                                {
                                    Console.WriteLine("Objet rare");
                                }
                            }
                            """),
                        Lesson(
                            "access-modifiers",
                            "public / private / protected",
                            "Controler l'acces aux membres d'une classe.",
                            "public expose un membre, private le garde interne a la classe, protected l'ouvre aux classes derivees.",
                            "private int health;\nprotected int armor;\npublic string Name { get; set; } = \"\";",
                            "Utilise public, private et protected dans Character, puis affiche Acces controle.",
                            """
                            using System;

                            Console.WriteLine("Acces controle");

                            class Character
                            {
                                // Ajoute public, private et protected ici
                            }
                            """,
                            "Les trois niveaux d'acces sont presents.",
                            "Ajoute un membre public, un private et un protected.",
                            60,
                            4,
                            [
                                Snippet("Utilise public", "public"),
                                Snippet("Utilise private", "private"),
                                Snippet("Utilise protected", "protected"),
                                Output("Affiche Acces controle", "Acces controle")
                            ],
                            conceptSummary: "Les modificateurs d'acces protegent l'encapsulation des objets.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("Acces controle");

                            class Character
                            {
                                public string Name { get; set; } = "";
                                private int health = 100;
                                protected int armor = 10;
                            }
                            """),
                        Lesson(
                            "advanced-oop-checkpoint",
                            "Test intermediaire du module",
                            "Combiner heritage, interface et polymorphisme.",
                            "Les abstractions orientee objet aident a faire evoluer un programme sans reecrire tous les appels.",
                            "IAction action = new Spell();\naction.Execute();",
                            "Cree IAction, Character et Rogue. Rogue herite de Character, implemente IAction et affiche Attaque furtive.",
                            """
                            using System;

                            IAction action = new Rogue();
                            action.Execute();

                            // Complete les types ici
                            """,
                            "Module 5 valide. Les abstractions cooperent.",
                            "Declare IAction, Character, puis Rogue : Character, IAction.",
                            85,
                            5,
                            [
                                Snippet("Declare interface", "interface IAction"),
                                Snippet("Declare Character", "class Character"),
                                Snippet("Herite et implemente", "class Rogue : Character, IAction"),
                                Output("Affiche Attaque furtive", "Attaque furtive")
                            ],
                            conceptSummary: "Heritage, interfaces et polymorphisme structurent les variations de comportement.",
                            finalCorrection:
                            """
                            using System;

                            IAction action = new Rogue();
                            action.Execute();

                            interface IAction
                            {
                                void Execute();
                            }

                            class Character
                            {
                                protected string Name { get; set; } = "Rogue";
                            }

                            class Rogue : Character, IAction
                            {
                                public void Execute()
                                {
                                    Console.WriteLine("Attaque furtive");
                                }
                            }
                            """)
                    ]),
                    Chapter("Module 6 - Structures de donnees", "Stocker et transformer plusieurs valeurs.", 6, 300,
                    [
                        Lesson(
                            "arrays",
                            "Tableaux",
                            "Stocker un nombre fixe de valeurs du meme type.",
                            "Un tableau garde plusieurs valeurs accessibles par index, en commencant a zero.",
                            "int[] scores = { 10, 20, 30 };",
                            "Cree un tableau scores contenant 10, 20, 30 et affiche 20.",
                            """
                            using System;

                            // Cree le tableau ici
                            Console.WriteLine(scores[1]);
                            """,
                            "Le tableau est utilise avec le bon index.",
                            "Declare int[] scores = { 10, 20, 30 };",
                            55,
                            1,
                            [
                                Snippet("Declare un tableau", "int[] scores"),
                                Snippet("Utilise l'index", "scores[1]"),
                                Output("Affiche 20", "20")
                            ],
                            conceptSummary: "Un tableau est simple et efficace quand la taille est connue.",
                            finalCorrection:
                            """
                            using System;

                            int[] scores = { 10, 20, 30 };
                            Console.WriteLine(scores[1]);
                            """),
                        Lesson(
                            "lists",
                            "List<T>",
                            "Ajouter et parcourir des valeurs dans une liste.",
                            "List<T> grandit dynamiquement et offre des methodes comme Add.",
                            "var names = new List<string>();\nnames.Add(\"Ada\");",
                            "Cree une List<string>, ajoute CSharp et Roslyn, puis affiche les deux valeurs.",
                            """
                            using System;
                            using System.Collections.Generic;

                            var tools = new List<string>();

                            // Ajoute et parcours les outils ici
                            """,
                            "La liste est remplie et parcourue.",
                            "Utilise Add puis foreach.",
                            60,
                            2,
                            [
                                Snippet("Utilise une liste", "List<string>"),
                                Count("Ajoute deux valeurs", ".Add(", 2),
                                Snippet("Utilise foreach", "foreach"),
                                Output("Affiche CSharp", "CSharp"),
                                Output("Affiche Roslyn", "Roslyn")
                            ],
                            conceptSummary: "List<T> est la collection generaliste la plus courante pour une suite modifiable.",
                            finalCorrection:
                            """
                            using System;
                            using System.Collections.Generic;

                            var tools = new List<string>();
                            tools.Add("CSharp");
                            tools.Add("Roslyn");

                            foreach (string tool in tools)
                            {
                                Console.WriteLine(tool);
                            }
                            """),
                        Lesson(
                            "dictionaries",
                            "Dictionary<TKey, TValue>",
                            "Associer une cle a une valeur.",
                            "Dictionary<TKey, TValue> retrouve rapidement une valeur a partir de sa cle.",
                            "var stock = new Dictionary<string, int>();\nstock[\"Potion\"] = 3;",
                            "Cree un dictionnaire stock avec Potion = 3, puis affiche Stock Potion: 3.",
                            """
                            using System;
                            using System.Collections.Generic;

                            // Cree le dictionnaire ici
                            Console.WriteLine("Stock Potion: " + stock["Potion"]);
                            """,
                            "La cle Potion donne la bonne valeur.",
                            "Declare Dictionary<string, int> et affecte Potion a 3.",
                            65,
                            3,
                            [
                                Snippet("Utilise Dictionary", "Dictionary<string, int>"),
                                Snippet("Cle Potion", "\"Potion\""),
                                Output("Affiche le stock", "Stock Potion: 3")
                            ],
                            conceptSummary: "Un dictionnaire est adapte aux recherches par identifiant, nom ou code.",
                            finalCorrection:
                            """
                            using System;
                            using System.Collections.Generic;

                            var stock = new Dictionary<string, int>();
                            stock["Potion"] = 3;
                            Console.WriteLine("Stock Potion: " + stock["Potion"]);
                            """),
                        Lesson(
                            "linq",
                            "LINQ",
                            "Filtrer ou transformer des collections avec des requetes C#.",
                            "LINQ fournit des methodes comme Where, Select et Sum pour travailler avec des collections.",
                            "var expensive = prices.Where(price => price > 10);",
                            "Filtre les scores superieurs ou egaux a 10, puis affiche 12 et 20.",
                            """
                            using System;
                            using System.Collections.Generic;
                            using System.Linq;

                            var scores = new List<int> { 5, 12, 20 };

                            // Filtre avec LINQ puis affiche
                            """,
                            "LINQ filtre les bonnes valeurs.",
                            "Utilise Where avec score >= 10 puis foreach.",
                            70,
                            4,
                            [
                                Snippet("Importe LINQ", "using System.Linq"),
                                Snippet("Utilise Where", ".Where("),
                                Output("Affiche 12", "12"),
                                Output("Affiche 20", "20")
                            ],
                            conceptSummary: "LINQ rend les transformations de collections expressives et composables.",
                            finalCorrection:
                            """
                            using System;
                            using System.Collections.Generic;
                            using System.Linq;

                            var scores = new List<int> { 5, 12, 20 };
                            var selectedScores = scores.Where(score => score >= 10);

                            foreach (int score in selectedScores)
                            {
                                Console.WriteLine(score);
                            }
                            """),
                        Lesson(
                            "data-structures-checkpoint",
                            "Test intermediaire du module",
                            "Choisir et combiner les bonnes collections.",
                            "Tableaux, listes, dictionnaires et LINQ couvrent la plupart des besoins de donnees en memoire.",
                            "var total = prices.Sum();",
                            "Stocke trois items, compte-les par categorie, filtre les categories avec au moins 2 items et affiche Potion: 2.",
                            """
                            using System;
                            using System.Collections.Generic;
                            using System.Linq;

                            var categories = new List<string> { "Potion", "Arme", "Potion" };

                            // Utilise un dictionnaire et LINQ pour afficher Potion: 2
                            """,
                            "Module 6 valide. Les collections sont combinees correctement.",
                            "Compte les categories dans un Dictionary, puis filtre avec Where.",
                            85,
                            5,
                            [
                                Snippet("Utilise List", "List<string>"),
                                Snippet("Utilise Dictionary", "Dictionary<string, int>"),
                                Snippet("Utilise LINQ", ".Where("),
                                Output("Affiche Potion", "Potion: 2")
                            ],
                            conceptSummary: "Les structures de donnees donnent une forme claire aux informations traitees.",
                            finalCorrection:
                            """
                            using System;
                            using System.Collections.Generic;
                            using System.Linq;

                            var categories = new List<string> { "Potion", "Arme", "Potion" };
                            var counts = new Dictionary<string, int>();

                            foreach (string category in categories)
                            {
                                if (!counts.ContainsKey(category))
                                {
                                    counts[category] = 0;
                                }

                                counts[category]++;
                            }

                            foreach (var entry in counts.Where(entry => entry.Value >= 2))
                            {
                                Console.WriteLine(entry.Key + ": " + entry.Value);
                            }
                            """)
                    ]),
                    Chapter("Module 7 - Gestion des erreurs", "Prevoir et traiter les cas invalides.", 7, 360,
                    [
                        Lesson(
                            "try-catch",
                            "try / catch",
                            "Intercepter une erreur pour garder le controle du programme.",
                            "try contient le code risque. catch reagit quand une exception est levee.",
                            "try { int.Parse(\"abc\"); } catch { Console.WriteLine(\"Erreur\"); }",
                            "Essaie de convertir abc en int et affiche Nombre invalide dans catch.",
                            """
                            using System;

                            string input = "abc";

                            // Ajoute try / catch ici
                            """,
                            "L'erreur est interceptee proprement.",
                            "Place int.Parse(input) dans try et affiche Nombre invalide dans catch.",
                            60,
                            1,
                            [
                                Snippet("Utilise try", "try"),
                                Snippet("Utilise catch", "catch"),
                                Output("Affiche Nombre invalide", "Nombre invalide")
                            ],
                            conceptSummary: "try / catch evite qu'une erreur prevue interrompe brutalement le programme.",
                            finalCorrection:
                            """
                            using System;

                            string input = "abc";

                            try
                            {
                                int.Parse(input);
                            }
                            catch
                            {
                                Console.WriteLine("Nombre invalide");
                            }
                            """),
                        Lesson(
                            "exceptions",
                            "Exceptions",
                            "Lever une erreur explicite quand une regle metier est violee.",
                            "throw signale une situation invalide. Une exception porte un message exploitable.",
                            "throw new InvalidOperationException(\"Action impossible\");",
                            "Si stock vaut 0, leve une InvalidOperationException puis affiche Stock vide via catch.",
                            """
                            using System;

                            int stock = 0;

                            try
                            {
                                // Leve une exception si stock vaut 0
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine("Stock vide");
                            }
                            """,
                            "L'exception metier est levee et interceptee.",
                            "Utilise throw new InvalidOperationException() dans un if.",
                            65,
                            2,
                            [
                                Snippet("Leve une exception", "throw new InvalidOperationException"),
                                Snippet("Intercepte le type", "catch (InvalidOperationException"),
                                Output("Affiche Stock vide", "Stock vide")
                            ],
                            conceptSummary: "Les exceptions permettent de rendre les erreurs explicites.",
                            finalCorrection:
                            """
                            using System;

                            int stock = 0;

                            try
                            {
                                if (stock == 0)
                                {
                                    throw new InvalidOperationException("Stock vide");
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine("Stock vide");
                            }
                            """),
                        Lesson(
                            "nullables",
                            "Nullables",
                            "Representer une valeur potentiellement absente.",
                            "Avec les annotations nullables, string? indique qu'une variable peut etre null.",
                            "string? name = null;\nConsole.WriteLine(name ?? \"Inconnu\");",
                            "Declare string? nickname = null et affiche Inconnu avec ??.",
                            """
                            using System;

                            // Declare nickname ici
                            Console.WriteLine(nickname ?? "Inconnu");
                            """,
                            "La valeur absente est geree.",
                            "Utilise string? nickname = null.",
                            55,
                            3,
                            [
                                Snippet("Declare nullable", "string? nickname"),
                                Snippet("Utilise coalescence", "??"),
                                Output("Affiche Inconnu", "Inconnu")
                            ],
                            conceptSummary: "Les nullables obligent a penser aux valeurs absentes au lieu de les subir.",
                            finalCorrection:
                            """
                            using System;

                            string? nickname = null;
                            Console.WriteLine(nickname ?? "Inconnu");
                            """),
                        Lesson(
                            "errors-checkpoint",
                            "Test intermediaire du module",
                            "Combiner validation, exception et valeur absente.",
                            "Un code robuste valide ses entrees, signale les erreurs et fournit des valeurs par defaut quand c'est pertinent.",
                            "string label = value ?? \"Default\";",
                            "Valide quantity = -1 avec une exception, intercepte-la, puis affiche Quantite invalide et Alias: Inconnu.",
                            """
                            using System;

                            int quantity = -1;
                            string? alias = null;

                            // Gere l'erreur et la valeur null ici
                            """,
                            "Module 7 valide. Les cas invalides sont geres.",
                            "Utilise try / catch, throw new ArgumentException et ??.",
                            80,
                            4,
                            [
                                Snippet("Utilise try", "try"),
                                Snippet("Leve ArgumentException", "throw new ArgumentException"),
                                Snippet("Utilise ??", "??"),
                                Output("Affiche Quantite invalide", "Quantite invalide"),
                                Output("Affiche Alias", "Alias: Inconnu")
                            ],
                            conceptSummary: "La gestion d'erreurs rend le comportement du programme explicite en cas de probleme.",
                            finalCorrection:
                            """
                            using System;

                            int quantity = -1;
                            string? alias = null;

                            try
                            {
                                if (quantity < 0)
                                {
                                    throw new ArgumentException("Quantite invalide");
                                }
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Quantite invalide");
                            }

                            Console.WriteLine("Alias: " + (alias ?? "Inconnu"));
                            """)
                    ]),
                    Chapter("Module 8 - Base de donnees", "Comprendre les bases relationnelles et EF Core.", 8, 420,
                    [
                        Lesson(
                            "relational-databases",
                            "Bases relationnelles",
                            "Comprendre tables, lignes, colonnes et relations.",
                            "Une base relationnelle organise les donnees en tables. Les cles relient les lignes entre elles.",
                            "Customers(Id, Name) -> Orders(CustomerId)",
                            "Affiche Table Products puis Cle primaire Id.",
                            """
                            using System;

                            // Affiche les deux lignes demandees
                            """,
                            "Les notions relationnelles sont identifiees.",
                            "Utilise deux Console.WriteLine avec les textes exacts.",
                            45,
                            1,
                            [
                                Output("Affiche table", "Table Products"),
                                Output("Affiche cle", "Cle primaire Id")
                            ],
                            conceptSummary: "Les tables stockent les entites; les cles primaires et etrangeres creent les relations.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("Table Products");
                            Console.WriteLine("Cle primaire Id");
                            """),
                        Lesson(
                            "entity-framework-core",
                            "Entity Framework Core",
                            "Comprendre le role d'un ORM dans une application C#.",
                            "Entity Framework Core mappe des classes C# vers des tables et genere les requetes de base.",
                            "db.Products.Add(product);\nawait db.SaveChangesAsync();",
                            "Affiche EF Core mappe Product vers Products.",
                            """
                            using System;

                            // Affiche le message demande
                            """,
                            "Le role d'EF Core est clair.",
                            "Affiche exactement EF Core mappe Product vers Products.",
                            45,
                            2,
                            [
                                Output("Affiche EF Core", "EF Core mappe Product vers Products")
                            ],
                            conceptSummary: "EF Core evite d'ecrire tout le SQL courant a la main dans une application C#.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("EF Core mappe Product vers Products");
                            """),
                        Lesson(
                            "dbcontext",
                            "DbContext",
                            "Identifier DbContext comme point d'acces aux donnees.",
                            "DbContext represente une session avec la base. Les DbSet representent les tables manipulees.",
                            "class AppDbContext : DbContext { public DbSet<Product> Products => Set<Product>(); }",
                            "Declare une classe AppDbContext et affiche DbContext pret.",
                            """
                            using System;

                            Console.WriteLine("DbContext pret");

                            // Declare AppDbContext ici
                            """,
                            "Le point d'acces aux donnees est nomme.",
                            "Declare class AppDbContext pour symboliser le contexte.",
                            50,
                            3,
                            [
                                Snippet("Declare AppDbContext", "class AppDbContext"),
                                Output("Affiche DbContext pret", "DbContext pret")
                            ],
                            conceptSummary: "DbContext centralise les lectures, ecritures et suivis de changements EF Core.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("DbContext pret");

                            class AppDbContext
                            {
                            }
                            """),
                        Lesson(
                            "crud",
                            "CRUD",
                            "Nommer les quatre operations de base sur les donnees.",
                            "CRUD signifie Create, Read, Update, Delete: creer, lire, modifier et supprimer.",
                            "Create -> Add, Read -> Query, Update -> Change, Delete -> Remove",
                            "Affiche Create, Read, Update et Delete chacun sur sa ligne.",
                            """
                            using System;

                            // Affiche les operations CRUD
                            """,
                            "Les quatre operations CRUD sont connues.",
                            "Utilise quatre Console.WriteLine.",
                            50,
                            4,
                            [
                                Count("Affiche quatre lignes", "Console.WriteLine", 4),
                                Output("Affiche Create", "Create"),
                                Output("Affiche Read", "Read"),
                                Output("Affiche Update", "Update"),
                                Output("Affiche Delete", "Delete")
                            ],
                            conceptSummary: "CRUD resume les actions principales d'une application orientee donnees.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("Create");
                            Console.WriteLine("Read");
                            Console.WriteLine("Update");
                            Console.WriteLine("Delete");
                            """),
                        Lesson(
                            "database-checkpoint",
                            "Test intermediaire du module",
                            "Relier les notions de base de donnees dans une mini-synthese.",
                            "Avant de coder l'acces aux donnees, il faut savoir nommer les tables, le contexte et les operations.",
                            "DbContext + DbSet + CRUD",
                            "Affiche Products via DbContext puis CRUD complet.",
                            """
                            using System;

                            // Affiche la synthese demandee
                            """,
                            "Module 8 valide. Tu connais le vocabulaire essentiel de l'acces aux donnees.",
                            "Affiche Products via DbContext et CRUD complet.",
                            70,
                            5,
                            [
                                Output("Affiche Products", "Products via DbContext"),
                                Output("Affiche CRUD", "CRUD complet")
                            ],
                            conceptSummary: "Ces notions preparent le passage entre code C#, modele objet et base relationnelle.",
                            finalCorrection:
                            """
                            using System;

                            Console.WriteLine("Products via DbContext");
                            Console.WriteLine("CRUD complet");
                            """)
                    ]),
                    Chapter("Boss Final", "Mini-projet de synthese.", 9, 500,
                    [
                        Lesson(
                            "boss-final",
                            "Boss Final RPG: inventaire",
                            "Construire une mini-application console de gestion d'inventaire RPG.",
                            "Le Boss Final valide le parcours C#: variables, conditions, boucles, methodes, classes, objets, List<T>, exceptions et logique metier simple.",
                            "var inventory = new List<Item>();\ninventory.Add(new Item(\"Potion\", 2, 5));\nConsole.WriteLine(inventory[0].Name);",
                            "Cree une classe Item et des methodes pour ajouter, afficher, supprimer et calculer la valeur totale d'un inventaire. Le programme doit aussi gerer proprement une suppression impossible.",
                            """
                            using System;
                            using System.Collections.Generic;
                            using System.Linq;

                            Console.WriteLine("Inventaire RPG");

                            var inventory = new List<Item>();

                            // Ajoute Potion x2 a 5 pieces et Epee x1 a 25 pieces
                            // Affiche l'inventaire
                            // Affiche Valeur totale: 35
                            // Supprime Potion
                            // Tente de supprimer Arc et affiche Erreur: objet introuvable

                            class Item
                            {
                                // Complete la classe
                            }
                            """,
                            "Boss Final vaincu. Ton inventaire RPG valide les bases C# pratiques.",
                            "Verifie que Item porte Name, Quantity et UnitPrice, que l'inventaire utilise List<Item>, et que la suppression impossible est geree avec try / catch.",
                            150,
                            1,
                            [
                                Output("Affiche le titre", "Inventaire RPG"),
                                Snippet("Declare Item", "class Item"),
                                Snippet("Utilise une liste d'items", "List<Item>"),
                                Snippet("Ajoute avec AddItem", "AddItem"),
                                Snippet("Supprime avec RemoveItem", "RemoveItem"),
                                Snippet("Calcule avec GetTotalValue", "GetTotalValue"),
                                Snippet("Gere les erreurs", "try"),
                                Snippet("Intercepte une exception", "catch"),
                                Output("Affiche Potion", "Potion x2 = 10"),
                                Output("Affiche Epee", "Epee x1 = 25"),
                                Output("Affiche total", "Valeur totale: 35"),
                                Output("Affiche suppression", "Supprime: Potion"),
                                Output("Affiche erreur", "Erreur: objet introuvable")
                            ],
                            conceptSummary: "Le Boss Final assemble le vocabulaire C# dans un programme console coherent avec donnees, operations et erreurs controlees.",
                            commonMistakes: "Oublier de multiplier quantite par prix, supprimer sans verifier l'existence de l'item, ou declarer Item sans constructeur rend l'inventaire fragile.",
                            finalCorrection:
                            """
                            using System;
                            using System.Collections.Generic;
                            using System.Linq;

                            Console.WriteLine("Inventaire RPG");

                            var inventory = new List<Item>();

                            AddItem(inventory, new Item("Potion", 2, 5));
                            AddItem(inventory, new Item("Epee", 1, 25));
                            PrintInventory(inventory);
                            Console.WriteLine("Valeur totale: " + GetTotalValue(inventory));
                            RemoveItem(inventory, "Potion");

                            try
                            {
                                RemoveItem(inventory, "Arc");
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine("Erreur: objet introuvable");
                            }

                            static void AddItem(List<Item> inventory, Item item)
                            {
                                inventory.Add(item);
                                Console.WriteLine("Ajoute: " + item.Name);
                            }

                            static void PrintInventory(List<Item> inventory)
                            {
                                foreach (Item item in inventory)
                                {
                                    Console.WriteLine(item.Name + " x" + item.Quantity + " = " + item.TotalValue);
                                }
                            }

                            static void RemoveItem(List<Item> inventory, string name)
                            {
                                Item? item = inventory.FirstOrDefault(item => item.Name == name);

                                if (item is null)
                                {
                                    throw new InvalidOperationException("Objet introuvable");
                                }

                                inventory.Remove(item);
                                Console.WriteLine("Supprime: " + name);
                            }

                            static int GetTotalValue(List<Item> inventory)
                            {
                                return inventory.Sum(item => item.TotalValue);
                            }

                            class Item
                            {
                                public string Name { get; }
                                public int Quantity { get; }
                                public int UnitPrice { get; }
                                public int TotalValue => Quantity * UnitPrice;

                                public Item(string name, int quantity, int unitPrice)
                                {
                                    Name = name;
                                    Quantity = quantity;
                                    UnitPrice = unitPrice;
                                }
                            }
                            """,
                            isBossFinal: true)
                    ])
                ]
            };

            var sqlCourse = new Course
            {
                Slug = "sqlserver-foundations",
                Title = "SQL / SQL Server",
                Description = "Un parcours progressif pour apprendre SQL Server avec des requetes controlees.",
                Language = "sqlserver",
                SortOrder = 2,
                Chapters =
                [
                    Chapter("Module 1 - Fondations SQL", "Lire des tables, choisir des colonnes et filtrer avec WHERE.", 1, 0,
                    [
                        Lesson(
                            "sql-relational-database",
                            "Qu'est-ce qu'une base de donnees relationnelle ?",
                            "Comprendre qu'une base relationnelle organise les donnees en tables reliees.",
                            "Une table ressemble a un tableau. Chaque ligne represente un element, chaque colonne represente une information. Une base relationnelle peut relier plusieurs tables avec des identifiants.",
                            "SELECT Name\nFROM Categories;",
                            "Affiche les noms des categories.",
                            """
                            -- Affiche les noms des categories
                            SELECT Name
                            FROM Categories;
                            """,
                            "La requete lit correctement la table Categories.",
                            "Utilise SELECT Name FROM Categories et garde une seule requete SELECT.",
                            25,
                            1,
                            [
                                SqlColumns("Retourne la colonne Name", "Name"),
                                SqlRows("Retourne les trois categories", 3),
                                Output("Contient Books", "Books"),
                                Output("Contient Games", "Games"),
                                Output("Contient Hardware", "Hardware"),
                                Forbidden("N'utilise pas SELECT *", "SELECT *")
                            ],
                            conceptSummary: "Une base relationnelle stocke des donnees dans des tables. SELECT lit des colonnes depuis une table.",
                            finalCorrection:
                            """
                            SELECT Name
                            FROM Categories;
                            """),
                        Lesson(
                            "sql-tables-rows-columns",
                            "Tables, lignes, colonnes",
                            "Lire plusieurs colonnes dans une table.",
                            "SELECT choisit les colonnes a afficher. FROM indique la table a lire. Chaque ligne retournee correspond a un enregistrement.",
                            "SELECT Id, Name\nFROM Products;",
                            "Affiche Name, Price et Stock depuis Products.",
                            """
                            SELECT Name, Price, Stock
                            FROM Products;
                            """,
                            "Les colonnes utiles du catalogue sont affichees.",
                            "Selectionne exactement Name, Price et Stock depuis Products.",
                            30,
                            2,
                            [
                                SqlColumns("Retourne Name, Price, Stock", "Name,Price,Stock"),
                                SqlRows("Retourne les cinq produits", 5),
                                Output("Contient C# Basics", "C# Basics"),
                                Required("Lit la table Products", "FROM Products"),
                                Forbidden("Evite SELECT *", "SELECT *")
                            ],
                            conceptSummary: "Une requete claire selectionne seulement les colonnes necessaires.",
                            finalCorrection:
                            """
                            SELECT Name, Price, Stock
                            FROM Products;
                            """),
                        Lesson(
                            "sql-server-data-types",
                            "Types de donnees SQL Server",
                            "Reconnaitre les types SQL Server courants dans une table.",
                            "SQL Server utilise INT pour les entiers, NVARCHAR pour le texte, DECIMAL pour les prix et BIT pour vrai/faux.",
                            "SELECT Name, Price, IsActive\nFROM Products;",
                            "Affiche le nom, le prix et l'etat actif des produits.",
                            """
                            SELECT Name, Price, IsActive
                            FROM Products;
                            """,
                            "Les colonnes texte, nombre decimal et booleen sont lues correctement.",
                            "Affiche Name, Price et IsActive depuis Products.",
                            35,
                            3,
                            [
                                SqlColumns("Retourne Name, Price, IsActive", "Name,Price,IsActive"),
                                SqlRows("Retourne les cinq produits", 5),
                                Output("Contient Archived Mouse", "Archived Mouse"),
                                Output("Contient au moins un produit inactif", "0"),
                                Forbidden("N'utilise pas de commande interdite", "DROP")
                            ],
                            conceptSummary: "Le type SQL Server indique la nature d'une colonne: texte, entier, decimal ou booleen.",
                            finalCorrection:
                            """
                            SELECT Name, Price, IsActive
                            FROM Products;
                            """),
                        Lesson(
                            "sql-select",
                            "SELECT",
                            "Utiliser SELECT pour choisir exactement les donnees utiles.",
                            "Eviter SELECT * aide a garder les resultats lisibles et a ne recuperer que les colonnes necessaires.",
                            "SELECT Name, Price\nFROM Products;",
                            "Affiche uniquement Name et Price pour tous les produits.",
                            """
                            SELECT Name, Price
                            FROM Products;
                            """,
                            "La requete selectionne uniquement les deux colonnes demandees.",
                            "La sortie doit contenir seulement Name et Price, sans SELECT *.",
                            35,
                            4,
                            [
                                SqlColumns("Retourne Name et Price", "Name,Price"),
                                SqlRows("Retourne les cinq produits", 5),
                                Output("Contient SQL Server Guide", "SQL Server Guide"),
                                Forbidden("N'utilise pas SELECT *", "SELECT *")
                            ],
                            conceptSummary: "SELECT controle la forme du resultat. Le choix des colonnes fait partie de la solution.",
                            finalCorrection:
                            """
                            SELECT Name, Price
                            FROM Products;
                            """),
                        Lesson(
                            "sql-where",
                            "WHERE",
                            "Filtrer les lignes avec une condition.",
                            "WHERE garde seulement les lignes qui respectent une condition. On peut combiner plusieurs conditions avec AND.",
                            "SELECT Name, Price\nFROM Products\nWHERE Price >= 30;",
                            "Affiche Name, Price et Stock des produits actifs dont le stock est superieur ou egal a 10.",
                            """
                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE IsActive = 1 AND Stock >= 10;
                            """,
                            "Le filtre WHERE retourne exactement les produits attendus.",
                            "Utilise WHERE avec IsActive = 1 et Stock >= 10.",
                            40,
                            5,
                            [
                                Required("Utilise WHERE", "WHERE"),
                                Required("Filtre les produits actifs", "IsActive = 1"),
                                Required("Filtre le stock", "Stock >= 10"),
                                SqlColumns("Retourne Name, Price, Stock", "Name,Price,Stock"),
                                SqlRows("Retourne deux produits", 2),
                                Output("Contient C# Basics", "C# Basics"),
                                Output("Contient RPG Dice Set", "RPG Dice Set")
                            ],
                            conceptSummary: "WHERE reduit les lignes retournees. AND permet de cumuler plusieurs conditions.",
                            finalCorrection:
                            """
                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE IsActive = 1 AND Stock >= 10;
                            """),
                        Lesson(
                            "sql-foundations-checkpoint",
                            "Test intermediaire: catalogue lisible",
                            "Combiner SELECT, choix de colonnes et WHERE dans une requete lisible.",
                            "Une requete metier selectionne les bonnes colonnes et filtre les lignes inutiles pour obtenir un resultat directement exploitable.",
                            "SELECT Name, Price, Stock\nFROM Products\nWHERE IsActive = 1;",
                            "Affiche Name, Price et Stock des produits actifs coutant moins de 40 et encore en stock.",
                            """
                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE IsActive = 1 AND Price < 40 AND Stock > 0;
                            """,
                            "Module SQL 1 valide. Tu sais lire une table et filtrer un resultat.",
                            "Combine IsActive = 1, Price < 40 et Stock > 0 dans WHERE.",
                            60,
                            6,
                            [
                                Required("Utilise WHERE", "WHERE"),
                                Required("Filtre actif", "IsActive = 1"),
                                Required("Filtre le prix", "Price < 40"),
                                Required("Filtre le stock", "Stock > 0"),
                                SqlColumns("Retourne Name, Price, Stock", "Name,Price,Stock"),
                                SqlRows("Retourne trois produits", 3),
                                Output("Contient C# Basics", "C# Basics"),
                                Output("Contient SQL Server Guide", "SQL Server Guide"),
                                Output("Contient RPG Dice Set", "RPG Dice Set")
                            ],
                            conceptSummary: "Ce test valide les bases de lecture SQL: table, colonnes, filtre et resultat attendu.",
                            finalCorrection:
                            """
                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE IsActive = 1 AND Price < 40 AND Stock > 0;
                            """)
                    ]),
                    Chapter("Module 2 - Trier et filtrer les donnees", "Ordonner les resultats et affiner les filtres SQL.", 2, 0,
                    [
                        Lesson(
                            "sql-order-by",
                            "ORDER BY",
                            "Trier les lignes retournees par une colonne.",
                            "ORDER BY place les lignes dans un ordre choisi. ASC trie du plus petit au plus grand, DESC du plus grand au plus petit.",
                            "SELECT Name, Price\nFROM Products\nORDER BY Price DESC;",
                            "Affiche Name et Price des produits, du plus cher au moins cher.",
                            """
                            SELECT Name, Price
                            FROM Products
                            ORDER BY Price DESC;
                            """,
                            "Les produits sont tries par prix decroissant.",
                            "Ajoute ORDER BY Price DESC a la requete.",
                            35,
                            1,
                            [
                                Required("Utilise ORDER BY", "ORDER BY"),
                                Required("Trie par prix descendant", "Price DESC"),
                                SqlColumns("Retourne Name et Price", "Name,Price"),
                                SqlRows("Retourne les cinq produits", 5),
                                Output("Premier produit cher present", "Mechanical Keyboard")
                            ],
                            conceptSummary: "ORDER BY ne change pas les donnees, seulement l'ordre d'affichage.",
                            finalCorrection:
                            """
                            SELECT Name, Price
                            FROM Products
                            ORDER BY Price DESC;
                            """),
                        Lesson(
                            "sql-top",
                            "TOP",
                            "Limiter le nombre de lignes retournees.",
                            "TOP se place juste apres SELECT et garde seulement les premieres lignes du resultat. Il est souvent combine avec ORDER BY.",
                            "SELECT TOP 3 Name, Stock\nFROM Products\nORDER BY Stock DESC;",
                            "Affiche les trois produits avec le plus grand stock.",
                            """
                            SELECT TOP 3 Name, Stock
                            FROM Products
                            ORDER BY Stock DESC;
                            """,
                            "La requete retourne seulement les trois meilleurs stocks.",
                            "Utilise TOP 3 avec ORDER BY Stock DESC.",
                            35,
                            2,
                            [
                                Required("Utilise TOP 3", "TOP 3"),
                                Required("Trie par stock", "ORDER BY Stock DESC"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne trois produits", 3),
                                Output("Contient RPG Dice Set", "RPG Dice Set")
                            ],
                            conceptSummary: "TOP limite le resultat. Sans ORDER BY, les premieres lignes ne sont pas garanties.",
                            finalCorrection:
                            """
                            SELECT TOP 3 Name, Stock
                            FROM Products
                            ORDER BY Stock DESC;
                            """),
                        Lesson(
                            "sql-distinct",
                            "DISTINCT",
                            "Supprimer les doublons dans une colonne.",
                            "DISTINCT retourne chaque valeur une seule fois. C'est utile pour lister les categories utilisees sans repetition.",
                            "SELECT DISTINCT CategoryId\nFROM Products;",
                            "Affiche les CategoryId presents dans Products sans doublon.",
                            """
                            SELECT DISTINCT CategoryId
                            FROM Products;
                            """,
                            "Chaque categorie apparait une seule fois.",
                            "Utilise SELECT DISTINCT CategoryId FROM Products.",
                            35,
                            3,
                            [
                                Required("Utilise DISTINCT", "DISTINCT"),
                                SqlColumns("Retourne CategoryId", "CategoryId"),
                                SqlRows("Retourne trois categories", 3),
                                Output("Contient 1", "1"),
                                Output("Contient 2", "2"),
                                Output("Contient 3", "3")
                            ],
                            conceptSummary: "DISTINCT reduit les doublons sur les colonnes selectionnees.",
                            finalCorrection:
                            """
                            SELECT DISTINCT CategoryId
                            FROM Products;
                            """),
                        Lesson(
                            "sql-like",
                            "LIKE",
                            "Filtrer du texte avec un motif.",
                            "LIKE compare du texte avec des jokers. Le symbole % remplace n'importe quelle suite de caracteres.",
                            "SELECT Name\nFROM Products\nWHERE Name LIKE '%SQL%';",
                            "Affiche les produits dont le nom contient SQL.",
                            """
                            SELECT Name
                            FROM Products
                            WHERE Name LIKE '%SQL%';
                            """,
                            "Le filtre texte retrouve le produit SQL.",
                            "Utilise WHERE Name LIKE '%SQL%'.",
                            35,
                            4,
                            [
                                Required("Utilise LIKE", "LIKE"),
                                Required("Cherche SQL", "%SQL%"),
                                SqlColumns("Retourne Name", "Name"),
                                SqlRows("Retourne un produit", 1),
                                Output("Contient SQL Server Guide", "SQL Server Guide")
                            ],
                            conceptSummary: "LIKE sert aux recherches textuelles simples. % signifie zero, un ou plusieurs caracteres.",
                            finalCorrection:
                            """
                            SELECT Name
                            FROM Products
                            WHERE Name LIKE '%SQL%';
                            """),
                        Lesson(
                            "sql-in",
                            "IN",
                            "Tester si une valeur appartient a une liste.",
                            "IN remplace plusieurs comparaisons avec OR lorsque tu veux accepter plusieurs valeurs possibles.",
                            "SELECT Name, CategoryId\nFROM Products\nWHERE CategoryId IN (1, 2);",
                            "Affiche Name et CategoryId des produits des categories 1 ou 2.",
                            """
                            SELECT Name, CategoryId
                            FROM Products
                            WHERE CategoryId IN (1, 2);
                            """,
                            "La requete garde uniquement les categories demandees.",
                            "Utilise WHERE CategoryId IN (1, 2).",
                            35,
                            5,
                            [
                                Required("Utilise IN", "IN"),
                                Required("Liste 1 et 2", "(1, 2)"),
                                SqlColumns("Retourne Name et CategoryId", "Name,CategoryId"),
                                SqlRows("Retourne trois produits", 3),
                                Output("Contient C# Basics", "C# Basics"),
                                Output("Contient RPG Dice Set", "RPG Dice Set")
                            ],
                            conceptSummary: "IN rend les filtres a plusieurs valeurs plus courts et plus lisibles.",
                            finalCorrection:
                            """
                            SELECT Name, CategoryId
                            FROM Products
                            WHERE CategoryId IN (1, 2);
                            """),
                        Lesson(
                            "sql-between",
                            "BETWEEN",
                            "Filtrer une valeur dans un intervalle inclusif.",
                            "BETWEEN garde les valeurs comprises entre deux bornes. Les bornes sont incluses.",
                            "SELECT Name, Price\nFROM Products\nWHERE Price BETWEEN 20 AND 40;",
                            "Affiche Name et Price des produits dont le prix est entre 20 et 40 inclus.",
                            """
                            SELECT Name, Price
                            FROM Products
                            WHERE Price BETWEEN 20 AND 40;
                            """,
                            "La requete retourne les produits dans la bonne fourchette de prix.",
                            "Utilise WHERE Price BETWEEN 20 AND 40.",
                            35,
                            6,
                            [
                                Required("Utilise BETWEEN", "BETWEEN"),
                                Required("Utilise la borne basse", "20"),
                                Required("Utilise la borne haute", "40"),
                                SqlColumns("Retourne Name et Price", "Name,Price"),
                                SqlRows("Retourne deux produits", 2),
                                Output("Contient C# Basics", "C# Basics"),
                                Output("Contient SQL Server Guide", "SQL Server Guide")
                            ],
                            conceptSummary: "BETWEEN inclut la borne basse et la borne haute.",
                            finalCorrection:
                            """
                            SELECT Name, Price
                            FROM Products
                            WHERE Price BETWEEN 20 AND 40;
                            """),
                        Lesson(
                            "sql-is-null",
                            "IS NULL / IS NOT NULL",
                            "Detecter les valeurs absentes.",
                            "Une valeur NULL signifie absence d'information. On utilise IS NULL ou IS NOT NULL, jamais = NULL.",
                            "SELECT Name, DiscontinuedAt\nFROM Products\nWHERE DiscontinuedAt IS NULL;",
                            "Affiche les produits encore actifs dont DiscontinuedAt est NULL.",
                            """
                            SELECT Name, DiscontinuedAt
                            FROM Products
                            WHERE DiscontinuedAt IS NULL;
                            """,
                            "La requete identifie les produits sans date d'archivage.",
                            "Utilise DiscontinuedAt IS NULL.",
                            35,
                            7,
                            [
                                Required("Utilise IS NULL", "IS NULL"),
                                Forbidden("N'utilise pas = NULL", "= NULL"),
                                SqlColumns("Retourne Name et DiscontinuedAt", "Name,DiscontinuedAt"),
                                SqlRows("Retourne quatre produits", 4),
                                Output("Contient C# Basics", "C# Basics"),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard")
                            ],
                            conceptSummary: "NULL ne se compare pas avec =. Utilise IS NULL ou IS NOT NULL.",
                            finalCorrection:
                            """
                            SELECT Name, DiscontinuedAt
                            FROM Products
                            WHERE DiscontinuedAt IS NULL;
                            """),
                        Lesson(
                            "sql-filtering-checkpoint",
                            "Test intermediaire: filtre precis",
                            "Combiner tri, limitation et filtre texte dans une requete utile.",
                            "Un rapport SQL combine souvent WHERE pour reduire les lignes, ORDER BY pour les classer et TOP pour garder les plus importantes.",
                            "SELECT TOP 2 Name, Price\nFROM Products\nWHERE IsActive = 1\nORDER BY Price DESC;",
                            "Affiche les deux produits actifs les plus chers avec Name et Price.",
                            """
                            SELECT TOP 2 Name, Price
                            FROM Products
                            WHERE IsActive = 1
                            ORDER BY Price DESC;
                            """,
                            "Module SQL 2 valide. Tu sais trier, limiter et filtrer plus finement.",
                            "Combine TOP 2, WHERE IsActive = 1 et ORDER BY Price DESC.",
                            60,
                            8,
                            [
                                Required("Utilise TOP 2", "TOP 2"),
                                Required("Utilise WHERE", "WHERE"),
                                Required("Filtre actif", "IsActive = 1"),
                                Required("Trie par prix", "ORDER BY Price DESC"),
                                SqlColumns("Retourne Name et Price", "Name,Price"),
                                SqlRows("Retourne deux produits", 2),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard"),
                                Output("Contient SQL Server Guide", "SQL Server Guide")
                            ],
                            conceptSummary: "Les clauses SQL se combinent dans un ordre precis: SELECT, FROM, WHERE, ORDER BY.",
                            finalCorrection:
                            """
                            SELECT TOP 2 Name, Price
                            FROM Products
                            WHERE IsActive = 1
                            ORDER BY Price DESC;
                            """)
                    ]),
                    Chapter("Module 3 - Agregation", "Calculer des indicateurs avec les fonctions d'agregation.", 3, 0,
                    [
                        Lesson(
                            "sql-count",
                            "COUNT",
                            "Compter les lignes d'un resultat.",
                            "COUNT(*) compte les lignes retournees par FROM et WHERE. Un alias rend la colonne lisible dans le resultat.",
                            "SELECT COUNT(*) AS ProductCount\nFROM Products;",
                            "Compte le nombre total de produits.",
                            """
                            SELECT COUNT(*) AS ProductCount
                            FROM Products;
                            """,
                            "Le total des produits est calcule.",
                            "Utilise COUNT(*) AS ProductCount depuis Products.",
                            35,
                            1,
                            [
                                Required("Utilise COUNT", "COUNT(*)"),
                                Required("Ajoute un alias", "AS ProductCount"),
                                SqlColumns("Retourne ProductCount", "ProductCount"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Compte cinq produits", "5")
                            ],
                            conceptSummary: "COUNT(*) retourne un compteur. Il produit une seule ligne sans GROUP BY.",
                            finalCorrection:
                            """
                            SELECT COUNT(*) AS ProductCount
                            FROM Products;
                            """),
                        Lesson(
                            "sql-sum",
                            "SUM",
                            "Additionner les valeurs numeriques d'une colonne.",
                            "SUM additionne les valeurs d'un groupe. Avec WHERE, tu peux additionner seulement les lignes utiles.",
                            "SELECT SUM(Stock) AS TotalStock\nFROM Products\nWHERE IsActive = 1;",
                            "Calcule le stock total des produits actifs.",
                            """
                            SELECT SUM(Stock) AS TotalStock
                            FROM Products
                            WHERE IsActive = 1;
                            """,
                            "Le stock total des produits actifs est calcule.",
                            "Utilise SUM(Stock) AS TotalStock avec WHERE IsActive = 1.",
                            35,
                            2,
                            [
                                Required("Utilise SUM", "SUM(Stock)"),
                                Required("Filtre les actifs", "IsActive = 1"),
                                SqlColumns("Retourne TotalStock", "TotalStock"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Stock total actif", "58")
                            ],
                            conceptSummary: "SUM sert aux totaux: stock, montant, quantite ou valeur cumulee.",
                            finalCorrection:
                            """
                            SELECT SUM(Stock) AS TotalStock
                            FROM Products
                            WHERE IsActive = 1;
                            """),
                        Lesson(
                            "sql-avg",
                            "AVG",
                            "Calculer une moyenne.",
                            "AVG calcule la moyenne des valeurs non nulles. Un filtre peut limiter les lignes prises en compte.",
                            "SELECT AVG(Price) AS AveragePrice\nFROM Products\nWHERE IsActive = 1;",
                            "Calcule le prix moyen des produits actifs.",
                            """
                            SELECT AVG(Price) AS AveragePrice
                            FROM Products
                            WHERE IsActive = 1;
                            """,
                            "Le prix moyen des produits actifs est calcule.",
                            "Utilise AVG(Price) AS AveragePrice avec WHERE IsActive = 1.",
                            35,
                            3,
                            [
                                Required("Utilise AVG", "AVG(Price)"),
                                Required("Filtre les actifs", "IsActive = 1"),
                                SqlColumns("Retourne AveragePrice", "AveragePrice"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Moyenne attendue", "41")
                            ],
                            conceptSummary: "AVG donne une tendance centrale. Le type decimal peut afficher plus ou moins de decimales selon SQL Server.",
                            finalCorrection:
                            """
                            SELECT AVG(Price) AS AveragePrice
                            FROM Products
                            WHERE IsActive = 1;
                            """),
                        Lesson(
                            "sql-min-max",
                            "MIN / MAX",
                            "Trouver la plus petite et la plus grande valeur.",
                            "MIN retourne la valeur minimale, MAX retourne la valeur maximale. Ces fonctions sont pratiques pour reperer des bornes.",
                            "SELECT MIN(Price) AS MinPrice, MAX(Price) AS MaxPrice\nFROM Products;",
                            "Affiche le prix minimum et le prix maximum du catalogue.",
                            """
                            SELECT MIN(Price) AS MinPrice, MAX(Price) AS MaxPrice
                            FROM Products;
                            """,
                            "Les bornes de prix du catalogue sont calculees.",
                            "Utilise MIN(Price) AS MinPrice et MAX(Price) AS MaxPrice.",
                            35,
                            4,
                            [
                                Required("Utilise MIN", "MIN(Price)"),
                                Required("Utilise MAX", "MAX(Price)"),
                                SqlColumns("Retourne MinPrice et MaxPrice", "MinPrice,MaxPrice"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Prix maximum", "89.99")
                            ],
                            conceptSummary: "MIN et MAX cherchent les extremums d'une colonne.",
                            finalCorrection:
                            """
                            SELECT MIN(Price) AS MinPrice, MAX(Price) AS MaxPrice
                            FROM Products;
                            """),
                        Lesson(
                            "sql-group-by",
                            "GROUP BY",
                            "Regrouper les lignes avant de calculer des agregats.",
                            "GROUP BY cree un groupe pour chaque valeur d'une colonne. Les fonctions comme COUNT ou SUM sont calculees par groupe.",
                            "SELECT CategoryId, COUNT(*) AS ProductCount\nFROM Products\nGROUP BY CategoryId;",
                            "Compte les produits par categorie.",
                            """
                            SELECT CategoryId, COUNT(*) AS ProductCount
                            FROM Products
                            GROUP BY CategoryId;
                            """,
                            "Chaque categorie obtient son compteur.",
                            "Utilise GROUP BY CategoryId avec COUNT(*) AS ProductCount.",
                            40,
                            5,
                            [
                                Required("Utilise GROUP BY", "GROUP BY CategoryId"),
                                Required("Utilise COUNT", "COUNT(*)"),
                                SqlColumns("Retourne CategoryId et ProductCount", "CategoryId,ProductCount"),
                                SqlRows("Retourne trois groupes", 3),
                                Output("Categorie 1", "1"),
                                Output("Deux produits", "2")
                            ],
                            conceptSummary: "GROUP BY transforme des lignes detaillees en groupes synthetiques.",
                            finalCorrection:
                            """
                            SELECT CategoryId, COUNT(*) AS ProductCount
                            FROM Products
                            GROUP BY CategoryId;
                            """),
                        Lesson(
                            "sql-having",
                            "HAVING",
                            "Filtrer les groupes apres agregation.",
                            "WHERE filtre les lignes avant le regroupement. HAVING filtre les groupes apres COUNT, SUM, AVG, MIN ou MAX.",
                            "SELECT CategoryId, COUNT(*) AS ProductCount\nFROM Products\nGROUP BY CategoryId\nHAVING COUNT(*) >= 2;",
                            "Affiche les categories qui contiennent au moins deux produits.",
                            """
                            SELECT CategoryId, COUNT(*) AS ProductCount
                            FROM Products
                            GROUP BY CategoryId
                            HAVING COUNT(*) >= 2;
                            """,
                            "Seuls les groupes assez grands sont conserves.",
                            "Utilise GROUP BY CategoryId puis HAVING COUNT(*) >= 2.",
                            40,
                            6,
                            [
                                Required("Utilise GROUP BY", "GROUP BY CategoryId"),
                                Required("Utilise HAVING", "HAVING"),
                                Required("Filtre les groupes", "COUNT(*) >= 2"),
                                SqlColumns("Retourne CategoryId et ProductCount", "CategoryId,ProductCount"),
                                SqlRows("Retourne deux groupes", 2),
                                Output("Categorie 1", "1"),
                                Output("Categorie 3", "3")
                            ],
                            conceptSummary: "HAVING sert aux conditions sur des agregats, par exemple COUNT(*) >= 2.",
                            finalCorrection:
                            """
                            SELECT CategoryId, COUNT(*) AS ProductCount
                            FROM Products
                            GROUP BY CategoryId
                            HAVING COUNT(*) >= 2;
                            """),
                        Lesson(
                            "sql-aggregation-checkpoint",
                            "Test intermediaire: indicateurs catalogue",
                            "Combiner WHERE, GROUP BY, SUM et HAVING pour produire un indicateur metier.",
                            "Un tableau de bord SQL regroupe les lignes, calcule des indicateurs, puis garde seulement les groupes significatifs.",
                            "SELECT CategoryId, SUM(Stock) AS TotalStock\nFROM Products\nWHERE IsActive = 1\nGROUP BY CategoryId\nHAVING SUM(Stock) >= 10;",
                            "Affiche le stock total par categorie active pour les categories ayant au moins 10 articles en stock.",
                            """
                            SELECT CategoryId, SUM(Stock) AS TotalStock
                            FROM Products
                            WHERE IsActive = 1
                            GROUP BY CategoryId
                            HAVING SUM(Stock) >= 10;
                            """,
                            "Module SQL 3 valide. Tu sais produire des indicateurs agreges.",
                            "Combine WHERE IsActive = 1, GROUP BY CategoryId et HAVING SUM(Stock) >= 10.",
                            65,
                            7,
                            [
                                Required("Utilise SUM", "SUM(Stock)"),
                                Required("Filtre les actifs", "IsActive = 1"),
                                Required("Regroupe par categorie", "GROUP BY CategoryId"),
                                Required("Filtre les groupes", "HAVING SUM(Stock) >= 10"),
                                SqlColumns("Retourne CategoryId et TotalStock", "CategoryId,TotalStock"),
                                SqlRows("Retourne deux groupes", 2),
                                Output("Stock categorie 1", "23"),
                                Output("Stock categorie 2", "30")
                            ],
                            conceptSummary: "Les agregations permettent de passer du detail a l'indicateur metier.",
                            finalCorrection:
                            """
                            SELECT CategoryId, SUM(Stock) AS TotalStock
                            FROM Products
                            WHERE IsActive = 1
                            GROUP BY CategoryId
                            HAVING SUM(Stock) >= 10;
                            """)
                    ]),
                    Chapter("Module 4 - Jointures", "Relier plusieurs tables avec JOIN.", 4, 0,
                    [
                        Lesson(
                            "sql-inner-join",
                            "INNER JOIN",
                            "Relier deux tables et garder seulement les lignes correspondantes.",
                            "INNER JOIN combine deux tables quand la condition ON trouve une correspondance. Ici, Product.CategoryId pointe vers Categories.Id.",
                            "SELECT p.Name AS ProductName, c.Name AS CategoryName\nFROM Products p\nINNER JOIN Categories c ON p.CategoryId = c.Id;",
                            "Affiche le nom de chaque produit avec le nom de sa categorie.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id;
                            """,
                            "Chaque produit est associe a sa categorie.",
                            "Utilise INNER JOIN Categories c ON p.CategoryId = c.Id.",
                            40,
                            1,
                            [
                                Required("Utilise INNER JOIN", "INNER JOIN"),
                                Required("Utilise ON", "ON p.CategoryId = c.Id"),
                                SqlColumns("Retourne ProductName et CategoryName", "ProductName,CategoryName"),
                                SqlRows("Retourne cinq produits", 5),
                                Output("Produit livre", "C# Basics"),
                                Output("Categorie Books", "Books")
                            ],
                            conceptSummary: "INNER JOIN retourne seulement les lignes qui ont une correspondance dans les deux tables.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id;
                            """),
                        Lesson(
                            "sql-left-join",
                            "LEFT JOIN",
                            "Garder toutes les lignes de la table de gauche.",
                            "LEFT JOIN retourne toutes les lignes de la table avant JOIN, meme si aucune ligne ne correspond a droite.",
                            "SELECT p.Name AS ProductName, c.Name AS CategoryName\nFROM Products p\nLEFT JOIN Categories c ON p.CategoryId = c.Id;",
                            "Affiche tous les produits avec leur categorie quand elle existe.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            LEFT JOIN Categories c ON p.CategoryId = c.Id;
                            """,
                            "Tous les produits restent visibles.",
                            "Utilise LEFT JOIN Categories c ON p.CategoryId = c.Id.",
                            40,
                            2,
                            [
                                Required("Utilise LEFT JOIN", "LEFT JOIN"),
                                Required("Utilise ON", "ON p.CategoryId = c.Id"),
                                SqlColumns("Retourne ProductName et CategoryName", "ProductName,CategoryName"),
                                SqlRows("Retourne cinq produits", 5),
                                Output("Contient Archived Mouse", "Archived Mouse")
                            ],
                            conceptSummary: "LEFT JOIN est utile pour garder la table principale complete.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            LEFT JOIN Categories c ON p.CategoryId = c.Id;
                            """),
                        Lesson(
                            "sql-right-join",
                            "RIGHT JOIN",
                            "Garder toutes les lignes de la table de droite.",
                            "RIGHT JOIN garde la table apres JOIN. Il est moins courant que LEFT JOIN, mais permet de raisonner depuis l'autre cote de la relation.",
                            "SELECT p.Name AS ProductName, c.Name AS CategoryName\nFROM Products p\nRIGHT JOIN Categories c ON p.CategoryId = c.Id;",
                            "Affiche les categories et les produits associes.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            RIGHT JOIN Categories c ON p.CategoryId = c.Id;
                            """,
                            "Les categories restent le cote conserve.",
                            "Utilise RIGHT JOIN Categories c ON p.CategoryId = c.Id.",
                            40,
                            3,
                            [
                                Required("Utilise RIGHT JOIN", "RIGHT JOIN"),
                                Required("Utilise ON", "ON p.CategoryId = c.Id"),
                                SqlColumns("Retourne ProductName et CategoryName", "ProductName,CategoryName"),
                                SqlRows("Retourne cinq lignes", 5),
                                Output("Contient Hardware", "Hardware")
                            ],
                            conceptSummary: "RIGHT JOIN est symetrique a LEFT JOIN, avec le cote conserve inverse.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            RIGHT JOIN Categories c ON p.CategoryId = c.Id;
                            """),
                        Lesson(
                            "sql-full-outer-join",
                            "FULL OUTER JOIN",
                            "Garder les lignes des deux cotes de la jointure.",
                            "FULL OUTER JOIN conserve les lignes de gauche et de droite, meme sans correspondance. Il sert a comparer deux ensembles.",
                            "SELECT p.Name AS ProductName, c.Name AS CategoryName\nFROM Products p\nFULL OUTER JOIN Categories c ON p.CategoryId = c.Id;",
                            "Affiche tous les produits et toutes les categories reliees.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            FULL OUTER JOIN Categories c ON p.CategoryId = c.Id;
                            """,
                            "La jointure conserve les deux cotes.",
                            "Utilise FULL OUTER JOIN Categories c ON p.CategoryId = c.Id.",
                            45,
                            4,
                            [
                                Required("Utilise FULL OUTER JOIN", "FULL OUTER JOIN"),
                                Required("Utilise ON", "ON p.CategoryId = c.Id"),
                                SqlColumns("Retourne ProductName et CategoryName", "ProductName,CategoryName"),
                                SqlRows("Retourne cinq lignes", 5),
                                Output("Contient Games", "Games")
                            ],
                            conceptSummary: "FULL OUTER JOIN combine les comportements LEFT et RIGHT.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            FULL OUTER JOIN Categories c ON p.CategoryId = c.Id;
                            """),
                        Lesson(
                            "sql-table-aliases",
                            "Alias de tables",
                            "Utiliser des alias courts pour rendre une jointure lisible.",
                            "Un alias comme p ou c evite de repeter les noms complets des tables. Les alias rendent les colonnes ambigues explicites.",
                            "SELECT p.Name AS ProductName, c.Name AS CategoryName\nFROM Products p\nINNER JOIN Categories c ON p.CategoryId = c.Id\nORDER BY p.Name;",
                            "Affiche les produits et categories avec les alias p et c, tries par nom de produit.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id
                            ORDER BY p.Name;
                            """,
                            "Les alias rendent la jointure claire et compacte.",
                            "Utilise les alias p et c, puis ORDER BY p.Name.",
                            40,
                            5,
                            [
                                Required("Alias p", "Products p"),
                                Required("Alias c", "Categories c"),
                                Required("Trie par p.Name", "ORDER BY p.Name"),
                                SqlColumns("Retourne ProductName et CategoryName", "ProductName,CategoryName"),
                                SqlRows("Retourne cinq produits", 5),
                                Output("Contient SQL Server Guide", "SQL Server Guide")
                            ],
                            conceptSummary: "Les alias clarifient les jointures et evitent les collisions de noms de colonnes.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id
                            ORDER BY p.Name;
                            """),
                        Lesson(
                            "sql-joins-checkpoint",
                            "Test intermediaire: catalogue enrichi",
                            "Combiner jointure, alias et filtre pour produire un resultat metier.",
                            "Une requete utile relie les tables, nomme clairement les colonnes et filtre les lignes qui interessent l'utilisateur.",
                            "SELECT p.Name AS ProductName, c.Name AS CategoryName, p.Price\nFROM Products p\nINNER JOIN Categories c ON p.CategoryId = c.Id\nWHERE p.IsActive = 1\nORDER BY p.Price DESC;",
                            "Affiche les produits actifs avec leur categorie et leur prix, du plus cher au moins cher.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName, p.Price
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id
                            WHERE p.IsActive = 1
                            ORDER BY p.Price DESC;
                            """,
                            "Module SQL 4 valide. Tu sais relier deux tables.",
                            "Combine INNER JOIN, alias p/c, WHERE p.IsActive = 1 et ORDER BY p.Price DESC.",
                            70,
                            6,
                            [
                                Required("Utilise INNER JOIN", "INNER JOIN"),
                                Required("Utilise les alias", "Products p"),
                                Required("Filtre les actifs", "p.IsActive = 1"),
                                Required("Trie par prix", "ORDER BY p.Price DESC"),
                                SqlColumns("Retourne ProductName, CategoryName, Price", "ProductName,CategoryName,Price"),
                                SqlRows("Retourne quatre produits actifs", 4),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard"),
                                Output("Contient SQL Server Guide", "SQL Server Guide")
                            ],
                            conceptSummary: "JOIN transforme des identifiants techniques en informations lisibles.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName, p.Price
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id
                            WHERE p.IsActive = 1
                            ORDER BY p.Price DESC;
                            """)
                    ]),
                    Chapter("Module 5 - Modification des donnees", "Ajouter, modifier, supprimer et proteger les changements.", 5, 0,
                    [
                        Lesson(
                            "sql-insert",
                            "INSERT",
                            "Ajouter une nouvelle ligne dans une table.",
                            "INSERT INTO indique la table et les colonnes a remplir. VALUES fournit les valeurs dans le meme ordre.",
                            "INSERT INTO Products (Id, Name, CategoryId, Price, Stock, IsActive)\nVALUES (6, N'Learning Mug', 1, 14.90, 12, 1);",
                            "Ajoute le produit Learning Mug puis affiche la ligne ajoutee.",
                            """
                            INSERT INTO Products (Id, Name, CategoryId, Price, Stock, IsActive)
                            VALUES (6, N'Learning Mug', 1, 14.90, 12, 1);

                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE Id = 6;
                            """,
                            "Le nouveau produit est insere et verifie.",
                            "Utilise INSERT INTO Products puis un SELECT de verification sur Id = 6.",
                            45,
                            1,
                            [
                                Required("Utilise INSERT", "INSERT INTO Products"),
                                Required("Ajoute Learning Mug", "Learning Mug"),
                                Required("Verifie avec SELECT", "SELECT"),
                                SqlColumns("Retourne Name, Price, Stock", "Name,Price,Stock"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient Learning Mug", "Learning Mug")
                            ],
                            conceptSummary: "INSERT cree une ligne. Un SELECT juste apres permet de verifier l'effet de l'ecriture.",
                            finalCorrection:
                            """
                            INSERT INTO Products (Id, Name, CategoryId, Price, Stock, IsActive)
                            VALUES (6, N'Learning Mug', 1, 14.90, 12, 1);

                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE Id = 6;
                            """),
                        Lesson(
                            "sql-update",
                            "UPDATE",
                            "Modifier une ligne existante avec une condition.",
                            "UPDATE change des valeurs. WHERE est indispensable pour limiter les lignes modifiees.",
                            "UPDATE Products\nSET Stock = 18\nWHERE Id = 2;",
                            "Mets le stock de SQL Server Guide a 18 puis affiche son nom et son stock.",
                            """
                            UPDATE Products
                            SET Stock = 18
                            WHERE Id = 2;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 2;
                            """,
                            "Le stock cible est modifie sans toucher aux autres produits.",
                            "Utilise UPDATE Products SET Stock = 18 WHERE Id = 2.",
                            45,
                            2,
                            [
                                Required("Utilise UPDATE", "UPDATE Products"),
                                Required("Utilise WHERE", "WHERE Id = 2"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Stock mis a jour", "18")
                            ],
                            conceptSummary: "UPDATE sans WHERE est dangereux. Les exercices imposent une clause WHERE.",
                            finalCorrection:
                            """
                            UPDATE Products
                            SET Stock = 18
                            WHERE Id = 2;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 2;
                            """),
                        Lesson(
                            "sql-delete",
                            "DELETE",
                            "Supprimer une ligne de facon controlee.",
                            "DELETE FROM retire des lignes. Comme UPDATE, il doit etre limite par WHERE.",
                            "DELETE FROM Products\nWHERE Id = 5;",
                            "Supprime Archived Mouse puis compte les produits restants.",
                            """
                            DELETE FROM Products
                            WHERE Id = 5;

                            SELECT COUNT(*) AS ProductCount
                            FROM Products;
                            """,
                            "Une seule ligne est supprimee et le total confirme l'effet.",
                            "Utilise DELETE FROM Products WHERE Id = 5 puis COUNT(*).",
                            45,
                            3,
                            [
                                Required("Utilise DELETE", "DELETE FROM Products"),
                                Required("Limite la suppression", "WHERE Id = 5"),
                                SqlColumns("Retourne ProductCount", "ProductCount"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Quatre produits restants", "4")
                            ],
                            conceptSummary: "DELETE supprime des lignes. La clause WHERE est une barriere de securite essentielle.",
                            finalCorrection:
                            """
                            DELETE FROM Products
                            WHERE Id = 5;

                            SELECT COUNT(*) AS ProductCount
                            FROM Products;
                            """),
                        Lesson(
                            "sql-simple-transaction",
                            "Transactions simples",
                            "Regrouper plusieurs modifications dans une unite logique.",
                            "Une transaction permet d'appliquer plusieurs changements ensemble. COMMIT valide les modifications.",
                            "BEGIN TRANSACTION;\nUPDATE Products SET Stock = Stock + 5 WHERE Id = 1;\nCOMMIT;",
                            "Ajoute 5 au stock de C# Basics dans une transaction validee, puis affiche son stock.",
                            """
                            BEGIN TRANSACTION;
                            UPDATE Products
                            SET Stock = Stock + 5
                            WHERE Id = 1;
                            COMMIT;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 1;
                            """,
                            "La transaction valide le nouveau stock.",
                            "Utilise BEGIN TRANSACTION, UPDATE, COMMIT puis SELECT.",
                            50,
                            4,
                            [
                                Required("Demarre une transaction", "BEGIN TRANSACTION"),
                                Required("Valide avec COMMIT", "COMMIT"),
                                Required("Met a jour le stock", "Stock = Stock + 5"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Stock valide", "20")
                            ],
                            conceptSummary: "COMMIT rend les changements definitifs dans la transaction.",
                            finalCorrection:
                            """
                            BEGIN TRANSACTION;
                            UPDATE Products
                            SET Stock = Stock + 5
                            WHERE Id = 1;
                            COMMIT;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 1;
                            """),
                        Lesson(
                            "sql-rollback-commit",
                            "ROLLBACK / COMMIT",
                            "Annuler ou confirmer les modifications d'une transaction.",
                            "ROLLBACK annule les changements depuis BEGIN TRANSACTION. C'est utile pour tester ou proteger une operation risquee.",
                            "BEGIN TRANSACTION;\nUPDATE Products SET Stock = 999 WHERE Id = 1;\nROLLBACK;",
                            "Tente de changer le stock de C# Basics a 999, annule avec ROLLBACK, puis verifie que le stock reste 15.",
                            """
                            BEGIN TRANSACTION;
                            UPDATE Products
                            SET Stock = 999
                            WHERE Id = 1;
                            ROLLBACK;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 1;
                            """,
                            "Le rollback annule la modification.",
                            "Utilise ROLLBACK et verifie que Stock vaut encore 15.",
                            50,
                            5,
                            [
                                Required("Demarre une transaction", "BEGIN TRANSACTION"),
                                Required("Annule avec ROLLBACK", "ROLLBACK"),
                                Required("Tente la valeur 999", "999"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Stock original conserve", "15")
                            ],
                            conceptSummary: "ROLLBACK remet la base dans l'etat precedent la transaction.",
                            finalCorrection:
                            """
                            BEGIN TRANSACTION;
                            UPDATE Products
                            SET Stock = 999
                            WHERE Id = 1;
                            ROLLBACK;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 1;
                            """),
                        Lesson(
                            "sql-modification-checkpoint",
                            "Test intermediaire: correction de stock",
                            "Combiner transaction, modification controlee et verification.",
                            "Une modification fiable se fait dans une transaction, cible une ligne precise et se verifie avec une requete de lecture.",
                            "BEGIN TRANSACTION;\nUPDATE Products SET Stock = Stock - 2 WHERE Id = 3;\nCOMMIT;",
                            "Dans une transaction, retire 2 au stock de RPG Dice Set, valide, puis affiche Name et Stock.",
                            """
                            BEGIN TRANSACTION;
                            UPDATE Products
                            SET Stock = Stock - 2
                            WHERE Id = 3;
                            COMMIT;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 3;
                            """,
                            "Module SQL 5 valide. Tu sais modifier et verifier une donnee.",
                            "Combine BEGIN TRANSACTION, UPDATE cible, COMMIT et SELECT.",
                            70,
                            6,
                            [
                                Required("Demarre une transaction", "BEGIN TRANSACTION"),
                                Required("Utilise UPDATE", "UPDATE Products"),
                                Required("Cible Id 3", "WHERE Id = 3"),
                                Required("Valide avec COMMIT", "COMMIT"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient RPG Dice Set", "RPG Dice Set"),
                                Output("Stock final", "28")
                            ],
                            conceptSummary: "Les modifications doivent etre ciblees, transactionnelles et verifiables.",
                            finalCorrection:
                            """
                            BEGIN TRANSACTION;
                            UPDATE Products
                            SET Stock = Stock - 2
                            WHERE Id = 3;
                            COMMIT;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Id = 3;
                            """)
                    ]),
                    Chapter("Module 6 - Modelisation relationnelle", "Structurer des tables fiables avec cles et contraintes.", 6, 0,
                    [
                        Lesson(
                            "sql-primary-keys",
                            "Cles primaires",
                            "Identifier chaque ligne avec une cle primaire stable.",
                            "Une cle primaire rend chaque ligne unique. Elle sert de reference pour retrouver ou relier une donnee.",
                            "CREATE TABLE Suppliers (\n    Id int NOT NULL PRIMARY KEY,\n    Name nvarchar(80) NOT NULL\n);",
                            "Cree la table Suppliers avec Id comme cle primaire, insere un fournisseur, puis affiche Id et Name.",
                            """
                            CREATE TABLE Suppliers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            INSERT INTO Suppliers (Id, Name)
                            VALUES (1, N'Northwind Supply');

                            SELECT Id, Name
                            FROM Suppliers
                            WHERE Id = 1;
                            """,
                            "La table possede une cle primaire et une ligne identifiable.",
                            "Utilise PRIMARY KEY sur Id, puis verifie avec SELECT Id, Name.",
                            50,
                            1,
                            [
                                Required("Cree Suppliers", "CREATE TABLE Suppliers"),
                                Required("Declare la cle primaire", "PRIMARY KEY"),
                                Required("Insere un fournisseur", "INSERT INTO Suppliers"),
                                SqlColumns("Retourne Id et Name", "Id,Name"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient Northwind Supply", "Northwind Supply")
                            ],
                            conceptSummary: "La cle primaire garantit l'identite d'une ligne et sert de point d'accroche aux relations.",
                            finalCorrection:
                            """
                            CREATE TABLE Suppliers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            INSERT INTO Suppliers (Id, Name)
                            VALUES (1, N'Northwind Supply');

                            SELECT Id, Name
                            FROM Suppliers
                            WHERE Id = 1;
                            """),
                        Lesson(
                            "sql-foreign-keys",
                            "Cles etrangeres",
                            "Relier deux tables avec une contrainte de reference.",
                            "Une cle etrangere stocke l'identifiant d'une ligne d'une autre table. SQL Server verifie que la reference existe.",
                            "CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)",
                            "Cree Suppliers et SupplierProducts, relie SupplierProducts a Products et Suppliers, puis affiche le lien insere.",
                            """
                            CREATE TABLE Suppliers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            CREATE TABLE SupplierProducts (
                                SupplierId int NOT NULL,
                                ProductId int NOT NULL,
                                CONSTRAINT PK_SupplierProducts PRIMARY KEY (SupplierId, ProductId),
                                CONSTRAINT FK_SupplierProducts_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id),
                                CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Suppliers (Id, Name)
                            VALUES (1, N'Northwind Supply');

                            INSERT INTO SupplierProducts (SupplierId, ProductId)
                            VALUES (1, 2);

                            SELECT SupplierId, ProductId
                            FROM SupplierProducts;
                            """,
                            "Les deux references sont valides et le lien est lisible.",
                            "Ajoute deux FOREIGN KEY: une vers Suppliers(Id), une vers Products(Id).",
                            55,
                            2,
                            [
                                Required("Cree la table de liaison", "CREATE TABLE SupplierProducts"),
                                Count("Declare deux cles etrangeres", "FOREIGN KEY", 2),
                                Required("Reference Products", "REFERENCES Products(Id)"),
                                Required("Reference Suppliers", "REFERENCES Suppliers(Id)"),
                                SqlColumns("Retourne SupplierId et ProductId", "SupplierId,ProductId"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Produit relie", "2")
                            ],
                            conceptSummary: "Une cle etrangere protege la coherence entre deux tables.",
                            finalCorrection:
                            """
                            CREATE TABLE Suppliers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            CREATE TABLE SupplierProducts (
                                SupplierId int NOT NULL,
                                ProductId int NOT NULL,
                                CONSTRAINT PK_SupplierProducts PRIMARY KEY (SupplierId, ProductId),
                                CONSTRAINT FK_SupplierProducts_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id),
                                CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Suppliers (Id, Name)
                            VALUES (1, N'Northwind Supply');

                            INSERT INTO SupplierProducts (SupplierId, ProductId)
                            VALUES (1, 2);

                            SELECT SupplierId, ProductId
                            FROM SupplierProducts;
                            """),
                        Lesson(
                            "sql-constraints",
                            "Contraintes",
                            "Utiliser NOT NULL, UNIQUE et CHECK pour proteger les donnees.",
                            "Les contraintes de table refusent les donnees invalides avant qu'elles n'entrent dans la base.",
                            "Code nvarchar(10) NOT NULL UNIQUE,\nCapacity int NOT NULL CHECK (Capacity > 0)",
                            "Cree Warehouses avec un code unique et une capacite positive, insere une ligne, puis affiche Code et Capacity.",
                            """
                            CREATE TABLE Warehouses (
                                Id int NOT NULL PRIMARY KEY,
                                Code nvarchar(10) NOT NULL UNIQUE,
                                Capacity int NOT NULL CHECK (Capacity > 0)
                            );

                            INSERT INTO Warehouses (Id, Code, Capacity)
                            VALUES (1, N'WH-A', 120);

                            SELECT Code, Capacity
                            FROM Warehouses
                            WHERE Code = N'WH-A';
                            """,
                            "La structure refuse les codes dupliques et les capacites invalides.",
                            "Utilise NOT NULL, UNIQUE et CHECK (Capacity > 0).",
                            50,
                            3,
                            [
                                Required("Utilise NOT NULL", "NOT NULL"),
                                Required("Utilise UNIQUE", "UNIQUE"),
                                Required("Utilise CHECK", "CHECK (Capacity > 0)"),
                                SqlColumns("Retourne Code et Capacity", "Code,Capacity"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient WH-A", "WH-A")
                            ],
                            conceptSummary: "Les contraintes placent les regles importantes directement dans le schema.",
                            finalCorrection:
                            """
                            CREATE TABLE Warehouses (
                                Id int NOT NULL PRIMARY KEY,
                                Code nvarchar(10) NOT NULL UNIQUE,
                                Capacity int NOT NULL CHECK (Capacity > 0)
                            );

                            INSERT INTO Warehouses (Id, Code, Capacity)
                            VALUES (1, N'WH-A', 120);

                            SELECT Code, Capacity
                            FROM Warehouses
                            WHERE Code = N'WH-A';
                            """),
                        Lesson(
                            "sql-simple-normalization",
                            "Normalisation simple",
                            "Separer les informations pour eviter les repetitions inutiles.",
                            "La normalisation consiste a placer chaque type d'information dans sa table. On garde l'identifiant pour relier les tables.",
                            "Products garde CategoryId au lieu de recopier le nom de categorie sur chaque produit.",
                            "Affiche les produits avec leur categorie sans recopier le nom de categorie dans Products.",
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id
                            ORDER BY p.Name;
                            """,
                            "Le nom de categorie vient de Categories, pas d'une duplication dans Products.",
                            "Utilise Products p, Categories c et la relation p.CategoryId = c.Id.",
                            45,
                            4,
                            [
                                Required("Utilise Products avec alias", "Products p"),
                                Required("Utilise Categories avec alias", "Categories c"),
                                Required("Relie les identifiants", "p.CategoryId = c.Id"),
                                SqlColumns("Retourne ProductName et CategoryName", "ProductName,CategoryName"),
                                SqlRows("Retourne cinq produits", 5),
                                Output("Categorie affichee", "Books")
                            ],
                            conceptSummary: "Une donnee stockee une seule fois est plus facile a corriger et a maintenir.",
                            finalCorrection:
                            """
                            SELECT p.Name AS ProductName, c.Name AS CategoryName
                            FROM Products p
                            INNER JOIN Categories c ON p.CategoryId = c.Id
                            ORDER BY p.Name;
                            """),
                        Lesson(
                            "sql-relationships",
                            "Relations 1-N et N-N",
                            "Modeliser une relation directe et une table de liaison.",
                            "Une relation 1-N utilise une cle etrangere dans la table enfant. Une relation N-N passe par une table de liaison avec deux cles etrangeres.",
                            "Products -> Categories est 1-N. SupplierProducts relie Suppliers et Products en N-N.",
                            "Cree un fournisseur et une table de liaison SupplierProducts, puis affiche le fournisseur et le produit relie.",
                            """
                            CREATE TABLE Suppliers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            CREATE TABLE SupplierProducts (
                                SupplierId int NOT NULL,
                                ProductId int NOT NULL,
                                CONSTRAINT PK_SupplierProducts PRIMARY KEY (SupplierId, ProductId),
                                CONSTRAINT FK_SupplierProducts_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id),
                                CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Suppliers (Id, Name)
                            VALUES (1, N'Northwind Supply');

                            INSERT INTO SupplierProducts (SupplierId, ProductId)
                            VALUES (1, 4);

                            SELECT s.Name AS SupplierName, p.Name AS ProductName
                            FROM SupplierProducts sp
                            INNER JOIN Suppliers s ON sp.SupplierId = s.Id
                            INNER JOIN Products p ON sp.ProductId = p.Id;
                            """,
                            "La table de liaison materialise la relation N-N.",
                            "Utilise SupplierProducts avec deux FOREIGN KEY et deux JOIN de lecture.",
                            60,
                            5,
                            [
                                Required("Cree SupplierProducts", "CREATE TABLE SupplierProducts"),
                                Count("Declare deux cles etrangeres", "FOREIGN KEY", 2),
                                Required("Joint Suppliers", "INNER JOIN Suppliers"),
                                Required("Joint Products", "INNER JOIN Products"),
                                SqlColumns("Retourne SupplierName et ProductName", "SupplierName,ProductName"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard")
                            ],
                            conceptSummary: "Une table de liaison transforme une relation N-N en deux relations 1-N controlables.",
                            finalCorrection:
                            """
                            CREATE TABLE Suppliers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            CREATE TABLE SupplierProducts (
                                SupplierId int NOT NULL,
                                ProductId int NOT NULL,
                                CONSTRAINT PK_SupplierProducts PRIMARY KEY (SupplierId, ProductId),
                                CONSTRAINT FK_SupplierProducts_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id),
                                CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Suppliers (Id, Name)
                            VALUES (1, N'Northwind Supply');

                            INSERT INTO SupplierProducts (SupplierId, ProductId)
                            VALUES (1, 4);

                            SELECT s.Name AS SupplierName, p.Name AS ProductName
                            FROM SupplierProducts sp
                            INNER JOIN Suppliers s ON sp.SupplierId = s.Id
                            INNER JOIN Products p ON sp.ProductId = p.Id;
                            """),
                        Lesson(
                            "sql-modeling-checkpoint",
                            "Test intermediaire: mini-modele cours",
                            "Construire un petit schema relationnel avec relation N-N.",
                            "Un modele relationnel complet combine cles primaires, cles etrangeres, table de liaison et requete de verification.",
                            "CREATE TABLE Students (...);\nCREATE TABLE Courses (...);\nCREATE TABLE StudentCourses (...);",
                            "Cree Students, Courses et StudentCourses, inscris Ada au cours SQL Server, puis affiche StudentName et CourseTitle.",
                            """
                            CREATE TABLE Students (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            CREATE TABLE Courses (
                                Id int NOT NULL PRIMARY KEY,
                                Title nvarchar(120) NOT NULL
                            );

                            CREATE TABLE StudentCourses (
                                StudentId int NOT NULL,
                                CourseId int NOT NULL,
                                CONSTRAINT PK_StudentCourses PRIMARY KEY (StudentId, CourseId),
                                CONSTRAINT FK_StudentCourses_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
                                CONSTRAINT FK_StudentCourses_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id)
                            );

                            INSERT INTO Students (Id, Name)
                            VALUES (1, N'Ada');

                            INSERT INTO Courses (Id, Title)
                            VALUES (1, N'SQL Server');

                            INSERT INTO StudentCourses (StudentId, CourseId)
                            VALUES (1, 1);

                            SELECT s.Name AS StudentName, c.Title AS CourseTitle
                            FROM StudentCourses sc
                            INNER JOIN Students s ON sc.StudentId = s.Id
                            INNER JOIN Courses c ON sc.CourseId = c.Id;
                            """,
                            "Module SQL 6 valide. Tu sais creer un schema relationnel simple.",
                            "Combine trois tables, cles primaires, deux cles etrangeres et une requete JOIN finale.",
                            80,
                            6,
                            [
                                Required("Cree Students", "CREATE TABLE Students"),
                                Required("Cree Courses", "CREATE TABLE Courses"),
                                Required("Cree StudentCourses", "CREATE TABLE StudentCourses"),
                                Count("Declare trois cles primaires", "PRIMARY KEY", 3),
                                Count("Declare deux cles etrangeres", "FOREIGN KEY", 2),
                                SqlColumns("Retourne StudentName et CourseTitle", "StudentName,CourseTitle"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient Ada", "Ada"),
                                Output("Contient SQL Server", "SQL Server")
                            ],
                            conceptSummary: "Un schema relationnel explicite les entites et les relations avant de manipuler les donnees.",
                            finalCorrection:
                            """
                            CREATE TABLE Students (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL
                            );

                            CREATE TABLE Courses (
                                Id int NOT NULL PRIMARY KEY,
                                Title nvarchar(120) NOT NULL
                            );

                            CREATE TABLE StudentCourses (
                                StudentId int NOT NULL,
                                CourseId int NOT NULL,
                                CONSTRAINT PK_StudentCourses PRIMARY KEY (StudentId, CourseId),
                                CONSTRAINT FK_StudentCourses_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
                                CONSTRAINT FK_StudentCourses_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id)
                            );

                            INSERT INTO Students (Id, Name)
                            VALUES (1, N'Ada');

                            INSERT INTO Courses (Id, Title)
                            VALUES (1, N'SQL Server');

                            INSERT INTO StudentCourses (StudentId, CourseId)
                            VALUES (1, 1);

                            SELECT s.Name AS StudentName, c.Title AS CourseTitle
                            FROM StudentCourses sc
                            INNER JOIN Students s ON sc.StudentId = s.Id
                            INNER JOIN Courses c ON sc.CourseId = c.Id;
                            """)
                    ]),
                    Chapter("Module 7 - SQL Server avance", "Creer des objets SQL Server reutilisables et optimiser les lectures.", 7, 0,
                    [
                        Lesson(
                            "sql-indexes",
                            "Index",
                            "Comprendre qu'un index accelere certaines recherches.",
                            "Un index est une structure maintenue par SQL Server pour retrouver plus vite des lignes sur une colonne souvent filtree ou triee.",
                            "CREATE INDEX IX_Products_Name ON Products(Name);",
                            "Cree un index sur Products(Name), puis recherche le produit SQL Server Guide.",
                            """
                            CREATE INDEX IX_Products_Name
                            ON Products(Name);

                            SELECT Name, Price
                            FROM Products
                            WHERE Name = N'SQL Server Guide';
                            """,
                            "L'index est cree et la requete cible le produit attendu.",
                            "Utilise CREATE INDEX IX_Products_Name ON Products(Name), puis un SELECT filtre par Name.",
                            50,
                            1,
                            [
                                Required("Cree un index", "CREATE INDEX IX_Products_Name"),
                                Required("Indexe Name", "ON Products(Name)"),
                                Required("Filtre par Name", "WHERE Name ="),
                                SqlColumns("Retourne Name et Price", "Name,Price"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient SQL Server Guide", "SQL Server Guide")
                            ],
                            conceptSummary: "Un index accelere les recherches frequentes, au prix d'un cout sur les ecritures.",
                            finalCorrection:
                            """
                            CREATE INDEX IX_Products_Name
                            ON Products(Name);

                            SELECT Name, Price
                            FROM Products
                            WHERE Name = N'SQL Server Guide';
                            """),
                        Lesson(
                            "sql-views",
                            "Vues",
                            "Creer une requete reutilisable sous forme de vue.",
                            "Une vue expose une requete comme une table logique. Elle simplifie les lectures recurrentes sans dupliquer les donnees.",
                            "CREATE VIEW ActiveProducts AS SELECT Name, Price FROM Products WHERE IsActive = 1;",
                            "Cree la vue ActiveProducts, puis lis les produits actifs depuis cette vue.",
                            """
                            CREATE VIEW ActiveProducts
                            AS
                            SELECT Name, Price
                            FROM Products
                            WHERE IsActive = 1;

                            SELECT Name, Price
                            FROM ActiveProducts
                            ORDER BY Price DESC;
                            """,
                            "La vue masque le filtre IsActive et retourne les produits vendables.",
                            "Utilise CREATE VIEW ActiveProducts AS, puis SELECT depuis ActiveProducts.",
                            55,
                            2,
                            [
                                Required("Cree une vue", "CREATE VIEW ActiveProducts"),
                                Required("Filtre les actifs", "WHERE IsActive = 1"),
                                Required("Lit la vue", "FROM ActiveProducts"),
                                SqlColumns("Retourne Name et Price", "Name,Price"),
                                SqlRows("Retourne quatre produits actifs", 4),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard")
                            ],
                            conceptSummary: "Une vue encapsule une requete de lecture et donne un nom metier au resultat.",
                            finalCorrection:
                            """
                            CREATE VIEW ActiveProducts
                            AS
                            SELECT Name, Price
                            FROM Products
                            WHERE IsActive = 1;

                            SELECT Name, Price
                            FROM ActiveProducts
                            ORDER BY Price DESC;
                            """),
                        Lesson(
                            "sql-stored-procedures",
                            "Procedures stockees",
                            "Regrouper une requete dans une procedure executee par nom.",
                            "Une procedure stockee place une logique SQL cote serveur. Elle peut recevoir des parametres, mais on commence ici par une lecture simple.",
                            "CREATE PROCEDURE GetActiveProducts AS SELECT Name FROM Products WHERE IsActive = 1;",
                            "Cree la procedure GetActiveProducts, execute-la, puis retourne les noms des produits actifs.",
                            """
                            CREATE PROCEDURE GetActiveProducts
                            AS
                            SELECT Name
                            FROM Products
                            WHERE IsActive = 1;

                            EXEC GetActiveProducts;
                            """,
                            "La procedure est creee puis executee pour retourner les produits actifs.",
                            "Utilise CREATE PROCEDURE GetActiveProducts et EXEC GetActiveProducts.",
                            60,
                            3,
                            [
                                Required("Cree une procedure", "CREATE PROCEDURE GetActiveProducts"),
                                Required("Execute la procedure", "EXEC GetActiveProducts"),
                                SqlColumns("Retourne Name", "Name"),
                                SqlRows("Retourne quatre produits actifs", 4),
                                Output("Contient C# Basics", "C# Basics")
                            ],
                            conceptSummary: "Une procedure stockee donne un nom a une operation SQL serveur controlee.",
                            finalCorrection:
                            """
                            CREATE PROCEDURE GetActiveProducts
                            AS
                            SELECT Name
                            FROM Products
                            WHERE IsActive = 1;

                            EXEC GetActiveProducts;
                            """),
                        Lesson(
                            "sql-functions",
                            "Fonctions",
                            "Creer une fonction scalaire reutilisable dans une requete.",
                            "Une fonction SQL retourne une valeur. Elle peut servir a centraliser un calcul simple utilise dans plusieurs requetes.",
                            "CREATE FUNCTION dbo.ApplyTax (@price decimal(10,2)) RETURNS decimal(10,2) AS BEGIN RETURN @price * 1.20 END;",
                            "Cree la fonction dbo.ApplyTax, puis calcule le prix taxe de C# Basics.",
                            """
                            CREATE FUNCTION dbo.ApplyTax (@price decimal(10,2))
                            RETURNS decimal(10,2)
                            AS
                            BEGIN
                                RETURN @price * 1.20
                            END;

                            SELECT Name, dbo.ApplyTax(Price) AS PriceWithTax
                            FROM Products
                            WHERE Id = 1;
                            """,
                            "La fonction applique le calcul attendu dans le SELECT.",
                            "Utilise CREATE FUNCTION dbo.ApplyTax et appelle dbo.ApplyTax(Price).",
                            60,
                            4,
                            [
                                Required("Cree une fonction", "CREATE FUNCTION dbo.ApplyTax"),
                                Required("Declare RETURNS", "RETURNS decimal"),
                                Required("Appelle la fonction", "dbo.ApplyTax(Price)"),
                                SqlColumns("Retourne Name et PriceWithTax", "Name,PriceWithTax"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Prix taxe calcule", "35.88")
                            ],
                            conceptSummary: "Une fonction est adaptee aux calculs reutilisables qui retournent une valeur.",
                            finalCorrection:
                            """
                            CREATE FUNCTION dbo.ApplyTax (@price decimal(10,2))
                            RETURNS decimal(10,2)
                            AS
                            BEGIN
                                RETURN @price * 1.20
                            END;

                            SELECT Name, dbo.ApplyTax(Price) AS PriceWithTax
                            FROM Products
                            WHERE Id = 1;
                            """),
                        Lesson(
                            "sql-tsql-variables",
                            "Variables T-SQL",
                            "Stocker une valeur temporaire dans un script SQL Server.",
                            "DECLARE cree une variable T-SQL locale au script. SET lui affecte une valeur, puis la variable peut etre utilisee dans une requete.",
                            "DECLARE @minStock int; SET @minStock = 10; SELECT Name FROM Products WHERE Stock >= @minStock;",
                            "Declare @minStock avec la valeur 10, puis affiche les produits dont le stock est au moins egal a cette valeur.",
                            """
                            DECLARE @minStock int;
                            SET @minStock = 10;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Stock >= @minStock
                            ORDER BY Stock DESC;
                            """,
                            "La variable pilote le filtre de stock.",
                            "Utilise DECLARE @minStock int, SET @minStock = 10 et WHERE Stock >= @minStock.",
                            50,
                            5,
                            [
                                Required("Declare une variable", "DECLARE @minStock int"),
                                Required("Affecte la valeur", "SET @minStock = 10"),
                                Required("Utilise la variable", "Stock >= @minStock"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne deux produits", 2),
                                Output("Contient RPG Dice Set", "RPG Dice Set")
                            ],
                            conceptSummary: "Une variable T-SQL rend un script plus lisible et evite les valeurs magiques repetees.",
                            finalCorrection:
                            """
                            DECLARE @minStock int;
                            SET @minStock = 10;

                            SELECT Name, Stock
                            FROM Products
                            WHERE Stock >= @minStock
                            ORDER BY Stock DESC;
                            """),
                        Lesson(
                            "sql-advanced-checkpoint",
                            "Test intermediaire: objet metier reutilisable",
                            "Combiner vue et requete metier pour produire une lecture reusable.",
                            "Un objet SQL Server utile encapsule une intention metier claire, puis les requetes applicatives s'appuient dessus.",
                            "CREATE VIEW ActiveProducts AS SELECT Name, Price FROM Products WHERE IsActive = 1;",
                            "Cree la vue ActiveProducts avec Name, Price et Stock, puis affiche les produits actifs dont le stock est inferieur a 10.",
                            """
                            CREATE VIEW ActiveProducts
                            AS
                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE IsActive = 1;

                            SELECT Name, Stock
                            FROM ActiveProducts
                            WHERE Stock < 10
                            ORDER BY Stock ASC;
                            """,
                            "Module SQL 7 valide. Tu sais creer un objet SQL Server reutilisable.",
                            "Combine CREATE VIEW ActiveProducts, un filtre IsActive = 1, puis une lecture sur Stock < 10.",
                            80,
                            6,
                            [
                                Required("Cree ActiveProducts", "CREATE VIEW ActiveProducts"),
                                Required("Filtre les actifs", "WHERE IsActive = 1"),
                                Required("Lit la vue", "FROM ActiveProducts"),
                                Required("Filtre le stock", "Stock < 10"),
                                SqlColumns("Retourne Name et Stock", "Name,Stock"),
                                SqlRows("Retourne deux produits", 2),
                                Output("Contient SQL Server Guide", "SQL Server Guide"),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard")
                            ],
                            conceptSummary: "Les objets SQL Server avances servent a donner un nom, une forme et un cadre aux requetes importantes.",
                            finalCorrection:
                            """
                            CREATE VIEW ActiveProducts
                            AS
                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE IsActive = 1;

                            SELECT Name, Stock
                            FROM ActiveProducts
                            WHERE Stock < 10
                            ORDER BY Stock ASC;
                            """)
                    ]),
                    Chapter("Module 8 - Projet pratique SQL Server", "Construire et interroger un mini-systeme e-commerce.", 8, 0,
                    [
                        Lesson(
                            "sql-complete-schema",
                            "Creation d'un schema complet",
                            "Concevoir les tables principales d'une mini-boutique.",
                            "Un schema complet liste les entites, leurs colonnes, leurs cles primaires et leurs relations avant d'inserer des donnees.",
                            "Customers represente les clients, Orders les commandes, OrderItems les lignes de commande.",
                            "Cree Customers, Orders et OrderItems avec leurs cles primaires et etrangeres, puis affiche les tables creees avec une requete de controle.",
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            SELECT Name
                            FROM Customers
                            WHERE Id = 1;
                            """,
                            "Les trois tables sont definies avec leurs relations.",
                            "Cree Customers, Orders et OrderItems avec PRIMARY KEY, FOREIGN KEY et les contraintes utiles.",
                            60,
                            1,
                            [
                                Required("Cree Customers", "CREATE TABLE Customers"),
                                Required("Cree Orders", "CREATE TABLE Orders"),
                                Required("Cree OrderItems", "CREATE TABLE OrderItems"),
                                Count("Declare trois cles primaires", "PRIMARY KEY", 3),
                                Count("Declare trois cles etrangeres", "FOREIGN KEY", 3),
                                Required("Controle les quantites", "CHECK (Quantity > 0)")
                            ],
                            conceptSummary: "Le schema fixe la structure et les regles avant les donnees.",
                            finalCorrection:
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );
                            """),
                        Lesson(
                            "sql-create-project-tables",
                            "Creation des tables",
                            "Creer les tables dans le bon ordre pour respecter les dependances.",
                            "On cree d'abord les tables parentes, puis les tables enfants qui contiennent des cles etrangeres.",
                            "CREATE TABLE Customers (...); CREATE TABLE Orders (...); CREATE TABLE OrderItems (...);",
                            "Cree les trois tables, insere un client de controle, puis affiche son nom et son email.",
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Customers (Id, Name, Email)
                            VALUES (1, N'Ada Lovelace', N'ada@example.com');

                            SELECT Name, Email
                            FROM Customers
                            WHERE Id = 1;
                            """,
                            "Les tables existent et acceptent une premiere ligne client.",
                            "Respecte l'ordre Customers, Orders, OrderItems, puis verifie Customers.",
                            55,
                            2,
                            [
                                Required("Cree Customers", "CREATE TABLE Customers"),
                                Required("Cree Orders", "CREATE TABLE Orders"),
                                Required("Cree OrderItems", "CREATE TABLE OrderItems"),
                                Required("Insere Ada", "Ada Lovelace"),
                                SqlColumns("Retourne Name et Email", "Name,Email"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Contient Ada", "Ada Lovelace")
                            ],
                            conceptSummary: "L'ordre de creation evite les references vers des tables qui n'existent pas encore.",
                            finalCorrection:
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Customers (Id, Name, Email)
                            VALUES (1, N'Ada Lovelace', N'ada@example.com');

                            SELECT Name, Email
                            FROM Customers
                            WHERE Id = 1;
                            """),
                        Lesson(
                            "sql-seed-project-data",
                            "Insertion de donnees",
                            "Remplir un schema relationnel avec des donnees coherentes.",
                            "Les insertions doivent respecter les cles etrangeres: creer les clients avant leurs commandes, puis les lignes de commande.",
                            "INSERT INTO Customers ... INSERT INTO Orders ... INSERT INTO OrderItems ...",
                            "Cree les tables, insere deux clients, deux commandes et trois lignes, puis compte les lignes de commande.",
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Customers (Id, Name, Email)
                            VALUES (1, N'Ada Lovelace', N'ada@example.com'),
                                   (2, N'Alan Turing', N'alan@example.com');

                            INSERT INTO Orders (Id, CustomerId, OrderedAt)
                            VALUES (1, 1, N'2026-04-01'),
                                   (2, 2, N'2026-04-02');

                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice)
                            VALUES (1, 1, 2, 1, 34.50),
                                   (2, 1, 3, 2, 12.00),
                                   (3, 2, 4, 1, 89.99);

                            SELECT COUNT(*) AS LineCount
                            FROM OrderItems;
                            """,
                            "Les donnees respectent les relations et les quantites attendues.",
                            "Insere dans l'ordre Customers, Orders, OrderItems, puis compte OrderItems.",
                            60,
                            3,
                            [
                                Count("Insere dans trois tables", "INSERT INTO", 3),
                                Required("Insere Ada", "Ada Lovelace"),
                                Required("Insere Alan", "Alan Turing"),
                                SqlColumns("Retourne LineCount", "LineCount"),
                                SqlRows("Retourne une ligne", 1),
                                Output("Trois lignes de commande", "3")
                            ],
                            conceptSummary: "Des donnees coherentes respectent les relations definies par le schema.",
                            finalCorrection:
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Customers (Id, Name, Email)
                            VALUES (1, N'Ada Lovelace', N'ada@example.com'),
                                   (2, N'Alan Turing', N'alan@example.com');

                            INSERT INTO Orders (Id, CustomerId, OrderedAt)
                            VALUES (1, 1, N'2026-04-01'),
                                   (2, 2, N'2026-04-02');

                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice)
                            VALUES (1, 1, 2, 1, 34.50),
                                   (2, 1, 3, 2, 12.00),
                                   (3, 2, 4, 1, 89.99);

                            SELECT COUNT(*) AS LineCount
                            FROM OrderItems;
                            """),
                        Lesson(
                            "sql-business-queries",
                            "Requetes metier",
                            "Relier commandes, clients et produits pour afficher une information utile.",
                            "Une requete metier transforme des identifiants techniques en informations lisibles pour l'utilisateur.",
                            "SELECT o.Id, c.Name FROM Orders o INNER JOIN Customers c ON o.CustomerId = c.Id;",
                            "Cree et remplis le mini-schema, puis affiche les commandes avec le nom du client et le nom du produit.",
                            """
                            CREATE TABLE Customers (Id int NOT NULL PRIMARY KEY, Name nvarchar(80) NOT NULL, Email nvarchar(120) NOT NULL UNIQUE);
                            CREATE TABLE Orders (Id int NOT NULL PRIMARY KEY, CustomerId int NOT NULL, OrderedAt nvarchar(20) NOT NULL, CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id));
                            CREATE TABLE OrderItems (Id int NOT NULL PRIMARY KEY, OrderId int NOT NULL, ProductId int NOT NULL, Quantity int NOT NULL CHECK (Quantity > 0), UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0), CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id), CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id));
                            INSERT INTO Customers (Id, Name, Email) VALUES (1, N'Ada Lovelace', N'ada@example.com'), (2, N'Alan Turing', N'alan@example.com');
                            INSERT INTO Orders (Id, CustomerId, OrderedAt) VALUES (1, 1, N'2026-04-01'), (2, 2, N'2026-04-02');
                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 1, 2, 1, 34.50), (2, 1, 3, 2, 12.00), (3, 2, 4, 1, 89.99);

                            SELECT o.Id AS OrderId, c.Name AS CustomerName, p.Name AS ProductName
                            FROM Orders o
                            INNER JOIN Customers c ON o.CustomerId = c.Id
                            INNER JOIN OrderItems oi ON oi.OrderId = o.Id
                            INNER JOIN Products p ON oi.ProductId = p.Id
                            ORDER BY o.Id;
                            """,
                            "Les commandes sont lisibles avec client et produit.",
                            "Utilise les jointures Orders, Customers, OrderItems et Products.",
                            65,
                            4,
                            [
                                Required("Joint Customers", "INNER JOIN Customers"),
                                Required("Joint OrderItems", "INNER JOIN OrderItems"),
                                Required("Joint Products", "INNER JOIN Products"),
                                SqlColumns("Retourne OrderId, CustomerName, ProductName", "OrderId,CustomerName,ProductName"),
                                SqlRows("Retourne trois lignes", 3),
                                Output("Contient Ada", "Ada Lovelace"),
                                Output("Contient Mechanical Keyboard", "Mechanical Keyboard")
                            ],
                            conceptSummary: "Une requete metier assemble plusieurs tables pour produire une information directement exploitable.",
                            finalCorrection:
                            """
                            SELECT o.Id AS OrderId, c.Name AS CustomerName, p.Name AS ProductName
                            FROM Orders o
                            INNER JOIN Customers c ON o.CustomerId = c.Id
                            INNER JOIN OrderItems oi ON oi.OrderId = o.Id
                            INNER JOIN Products p ON oi.ProductId = p.Id
                            ORDER BY o.Id;
                            """),
                        Lesson(
                            "sql-simple-optimization",
                            "Optimisation simple",
                            "Ajouter un index adapte a une requete frequente.",
                            "Un index sur une cle etrangere ou une colonne souvent filtree aide SQL Server a retrouver les lignes plus efficacement.",
                            "CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);",
                            "Cree et remplis le mini-schema, ajoute un index sur Orders(CustomerId), puis affiche les commandes d'Ada.",
                            """
                            CREATE TABLE Customers (Id int NOT NULL PRIMARY KEY, Name nvarchar(80) NOT NULL, Email nvarchar(120) NOT NULL UNIQUE);
                            CREATE TABLE Orders (Id int NOT NULL PRIMARY KEY, CustomerId int NOT NULL, OrderedAt nvarchar(20) NOT NULL, CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id));
                            CREATE TABLE OrderItems (Id int NOT NULL PRIMARY KEY, OrderId int NOT NULL, ProductId int NOT NULL, Quantity int NOT NULL CHECK (Quantity > 0), UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0), CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id), CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id));
                            INSERT INTO Customers (Id, Name, Email) VALUES (1, N'Ada Lovelace', N'ada@example.com'), (2, N'Alan Turing', N'alan@example.com');
                            INSERT INTO Orders (Id, CustomerId, OrderedAt) VALUES (1, 1, N'2026-04-01'), (2, 2, N'2026-04-02');
                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 1, 2, 1, 34.50), (2, 1, 3, 2, 12.00), (3, 2, 4, 1, 89.99);

                            CREATE INDEX IX_Orders_CustomerId
                            ON Orders(CustomerId);

                            SELECT o.Id AS OrderId, c.Name AS CustomerName
                            FROM Orders o
                            INNER JOIN Customers c ON o.CustomerId = c.Id
                            WHERE c.Name = N'Ada Lovelace';
                            """,
                            "L'index cible la relation la plus consultee.",
                            "Cree IX_Orders_CustomerId puis filtre les commandes d'Ada.",
                            55,
                            5,
                            [
                                Required("Cree l'index", "CREATE INDEX IX_Orders_CustomerId"),
                                Required("Indexe CustomerId", "ON Orders(CustomerId)"),
                                Required("Filtre Ada", "Ada Lovelace"),
                                SqlColumns("Retourne OrderId et CustomerName", "OrderId,CustomerName"),
                                SqlRows("Retourne une commande", 1),
                                Output("Contient Ada", "Ada Lovelace")
                            ],
                            conceptSummary: "Une optimisation simple part d'une requete reelle, puis ajoute un index sur la colonne de recherche ou de jointure.",
                            finalCorrection:
                            """
                            CREATE INDEX IX_Orders_CustomerId
                            ON Orders(CustomerId);

                            SELECT o.Id AS OrderId, c.Name AS CustomerName
                            FROM Orders o
                            INNER JOIN Customers c ON o.CustomerId = c.Id
                            WHERE c.Name = N'Ada Lovelace';
                            """),
                        Lesson(
                            "sql-project-checkpoint",
                            "Test intermediaire: total des commandes",
                            "Calculer un indicateur metier sur le mini-schema e-commerce.",
                            "Un total de commande combine jointures, calcul, GROUP BY et un alias clair pour etre exploitable.",
                            "SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal",
                            "Cree et remplis le mini-schema, puis affiche le total de chaque commande avec le nom du client.",
                            """
                            CREATE TABLE Customers (Id int NOT NULL PRIMARY KEY, Name nvarchar(80) NOT NULL, Email nvarchar(120) NOT NULL UNIQUE);
                            CREATE TABLE Orders (Id int NOT NULL PRIMARY KEY, CustomerId int NOT NULL, OrderedAt nvarchar(20) NOT NULL, CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id));
                            CREATE TABLE OrderItems (Id int NOT NULL PRIMARY KEY, OrderId int NOT NULL, ProductId int NOT NULL, Quantity int NOT NULL CHECK (Quantity > 0), UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0), CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id), CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id));
                            INSERT INTO Customers (Id, Name, Email) VALUES (1, N'Ada Lovelace', N'ada@example.com'), (2, N'Alan Turing', N'alan@example.com');
                            INSERT INTO Orders (Id, CustomerId, OrderedAt) VALUES (1, 1, N'2026-04-01'), (2, 2, N'2026-04-02');
                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 1, 2, 1, 34.50), (2, 1, 3, 2, 12.00), (3, 2, 4, 1, 89.99);

                            SELECT o.Id AS OrderId, c.Name AS CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal
                            FROM Orders o
                            INNER JOIN Customers c ON o.CustomerId = c.Id
                            INNER JOIN OrderItems oi ON oi.OrderId = o.Id
                            GROUP BY o.Id, c.Name
                            ORDER BY OrderTotal DESC;
                            """,
                            "Module SQL 8 valide. Tu sais construire et interroger un mini-systeme e-commerce.",
                            "Combine Customers, Orders, OrderItems, SUM, GROUP BY et ORDER BY.",
                            90,
                            6,
                            [
                                Required("Calcule un total", "SUM(oi.Quantity * oi.UnitPrice)"),
                                Required("Groupe par commande", "GROUP BY o.Id, c.Name"),
                                Required("Trie par total", "ORDER BY OrderTotal DESC"),
                                SqlColumns("Retourne OrderId, CustomerName, OrderTotal", "OrderId,CustomerName,OrderTotal"),
                                SqlRows("Retourne deux commandes", 2),
                                Output("Contient Alan", "Alan Turing"),
                                Output("Total Alan", "89.99"),
                                Output("Total Ada", "58.50")
                            ],
                            conceptSummary: "Un projet SQL pratique relie schema, donnees, jointures et agregats pour repondre a une question metier.",
                            finalCorrection:
                            """
                            SELECT o.Id AS OrderId, c.Name AS CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal
                            FROM Orders o
                            INNER JOIN Customers c ON o.CustomerId = c.Id
                            INNER JOIN OrderItems oi ON oi.OrderId = o.Id
                            GROUP BY o.Id, c.Name
                            ORDER BY OrderTotal DESC;
                            """)
                    ]),
                    Chapter("Boss Final SQL", "Mini-boutique e-commerce complete.", 9, 0,
                    [
                        Lesson(
                            "sql-boss-final-ecommerce",
                            "Boss Final SQL: mini-boutique e-commerce",
                            "Creer, modifier, nettoyer et interroger une base e-commerce simplifiee.",
                            "Ce Boss Final valide le parcours SQL complet: schema relationnel, contraintes, insertions, jointures, agregats, HAVING, mise a jour, suppression controlee et transaction.",
                            "SELECT c.Name, SUM(oi.Quantity * oi.UnitPrice) AS TotalSpent\nFROM Customers c\nINNER JOIN Orders o ON o.CustomerId = c.Id\nINNER JOIN OrderItems oi ON oi.OrderId = o.Id\nGROUP BY c.Name\nHAVING SUM(oi.Quantity * oi.UnitPrice) >= 50;",
                            "Cree Customers, Products, Orders et OrderItems. Insere des donnees, mets a jour un stock, supprime proprement une commande dans une transaction, puis affiche les meilleurs clients avec leur total d'achat.",
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Products (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(120) NOT NULL,
                                Price decimal(10,2) NOT NULL CHECK (Price >= 0),
                                Stock int NOT NULL CHECK (Stock >= 0)
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Customers (Id, Name, Email)
                            VALUES (1, N'Ada Lovelace', N'ada@example.com'),
                                   (2, N'Alan Turing', N'alan@example.com');

                            INSERT INTO Products (Id, Name, Price, Stock)
                            VALUES (1, N'SQL Server Guide', 34.50, 8),
                                   (2, N'RPG Dice Set', 12.00, 30),
                                   (3, N'Mechanical Keyboard', 89.99, 5);

                            INSERT INTO Orders (Id, CustomerId, OrderedAt)
                            VALUES (1, 1, N'2026-04-01'),
                                   (2, 2, N'2026-04-02'),
                                   (3, 1, N'2026-04-03');

                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice)
                            VALUES (1, 1, 1, 1, 34.50),
                                   (2, 1, 2, 2, 12.00),
                                   (3, 2, 3, 1, 89.99),
                                   (4, 3, 2, 1, 12.00);

                            UPDATE Products
                            SET Stock = Stock - 1
                            WHERE Id = 3;

                            BEGIN TRANSACTION;
                            DELETE FROM OrderItems
                            WHERE OrderId = 3;
                            DELETE FROM Orders
                            WHERE Id = 3;
                            COMMIT;

                            SELECT c.Name AS CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSpent
                            FROM Customers c
                            INNER JOIN Orders o ON o.CustomerId = c.Id
                            INNER JOIN OrderItems oi ON oi.OrderId = o.Id
                            GROUP BY c.Name
                            HAVING SUM(oi.Quantity * oi.UnitPrice) >= 50
                            ORDER BY TotalSpent DESC;
                            """,
                            "Boss Final SQL reussi. Tu sais construire et exploiter une mini-base e-commerce.",
                            "Le script doit creer les quatre tables, inserer des donnees, utiliser UPDATE, DELETE controle, transaction, JOIN, GROUP BY et HAVING.",
                            150,
                            1,
                            [
                                Required("Cree Customers", "CREATE TABLE Customers"),
                                Required("Cree Products", "CREATE TABLE Products"),
                                Required("Cree Orders", "CREATE TABLE Orders"),
                                Required("Cree OrderItems", "CREATE TABLE OrderItems"),
                                Count("Declare quatre cles primaires", "PRIMARY KEY", 4),
                                Count("Declare trois cles etrangeres", "FOREIGN KEY", 3),
                                Count("Insere dans les quatre tables", "INSERT INTO", 4),
                                Required("Utilise WHERE", "WHERE"),
                                Required("Utilise JOIN", "INNER JOIN"),
                                Required("Utilise GROUP BY", "GROUP BY"),
                                Required("Utilise HAVING", "HAVING"),
                                Required("Met a jour le stock", "UPDATE Products"),
                                Required("Supprime les lignes de commande", "DELETE FROM OrderItems"),
                                Required("Supprime une commande", "DELETE FROM Orders"),
                                Required("Demarre une transaction", "BEGIN TRANSACTION"),
                                Required("Valide la transaction", "COMMIT"),
                                SqlColumns("Retourne CustomerName et TotalSpent", "CustomerName,TotalSpent"),
                                SqlRows("Retourne deux meilleurs clients", 2),
                                Output("Contient Alan", "Alan Turing"),
                                Output("Total Alan", "89.99"),
                                Output("Contient Ada", "Ada Lovelace"),
                                Output("Total Ada", "58.50")
                            ],
                            conceptSummary: "Le Boss Final SQL assemble toutes les competences du parcours dans un script e-commerce coherent.",
                            finalCorrection:
                            """
                            CREATE TABLE Customers (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(80) NOT NULL,
                                Email nvarchar(120) NOT NULL UNIQUE
                            );

                            CREATE TABLE Products (
                                Id int NOT NULL PRIMARY KEY,
                                Name nvarchar(120) NOT NULL,
                                Price decimal(10,2) NOT NULL CHECK (Price >= 0),
                                Stock int NOT NULL CHECK (Stock >= 0)
                            );

                            CREATE TABLE Orders (
                                Id int NOT NULL PRIMARY KEY,
                                CustomerId int NOT NULL,
                                OrderedAt nvarchar(20) NOT NULL,
                                CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                            );

                            CREATE TABLE OrderItems (
                                Id int NOT NULL PRIMARY KEY,
                                OrderId int NOT NULL,
                                ProductId int NOT NULL,
                                Quantity int NOT NULL CHECK (Quantity > 0),
                                UnitPrice decimal(10,2) NOT NULL CHECK (UnitPrice >= 0),
                                CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                                CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                            );

                            INSERT INTO Customers (Id, Name, Email)
                            VALUES (1, N'Ada Lovelace', N'ada@example.com'),
                                   (2, N'Alan Turing', N'alan@example.com');

                            INSERT INTO Products (Id, Name, Price, Stock)
                            VALUES (1, N'SQL Server Guide', 34.50, 8),
                                   (2, N'RPG Dice Set', 12.00, 30),
                                   (3, N'Mechanical Keyboard', 89.99, 5);

                            INSERT INTO Orders (Id, CustomerId, OrderedAt)
                            VALUES (1, 1, N'2026-04-01'),
                                   (2, 2, N'2026-04-02'),
                                   (3, 1, N'2026-04-03');

                            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice)
                            VALUES (1, 1, 1, 1, 34.50),
                                   (2, 1, 2, 2, 12.00),
                                   (3, 2, 3, 1, 89.99),
                                   (4, 3, 2, 1, 12.00);

                            UPDATE Products
                            SET Stock = Stock - 1
                            WHERE Id = 3;

                            BEGIN TRANSACTION;
                            DELETE FROM OrderItems
                            WHERE OrderId = 3;
                            DELETE FROM Orders
                            WHERE Id = 3;
                            COMMIT;

                            SELECT c.Name AS CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSpent
                            FROM Customers c
                            INNER JOIN Orders o ON o.CustomerId = c.Id
                            INNER JOIN OrderItems oi ON oi.OrderId = o.Id
                            GROUP BY c.Name
                            HAVING SUM(oi.Quantity * oi.UnitPrice) >= 50
                            ORDER BY TotalSpent DESC;
                            """,
                            isBossFinal: true)
                    ])
                ]
            };

            var phpCourse = PhpSymfonyCourse();

            AttachIntermediateBosses(course);
            AttachIntermediateBosses(sqlCourse);
            AttachIntermediateBosses(phpCourse);

            db.Courses.AddRange(course, sqlCourse, phpCourse);
            db.Badges.AddRange(
                new Badge { Slug = "first-run", Name = "Premier run", Description = "Terminer une premiere lecon.", IconName = "play", RuleType = BadgeRuleType.CompleteLessons, RuleValue = 1 },
                new Badge { Slug = "hundred-xp", Name = "100 XP", Description = "Atteindre 100 points d'experience.", IconName = "star", RuleType = BadgeRuleType.TotalXp, RuleValue = 100 },
                new Badge { Slug = "boss-slayer", Name = "Boss Final", Description = "Reussir le mini-projet final.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinal, RuleValue = 1 },
                new Badge { Slug = "sql-first-select", Name = "Premier SELECT", Description = "Terminer une premiere lecon SQL.", IconName = "database", RuleType = BadgeRuleType.CompleteLessonInCourse, RuleValue = 1, RuleCourseLanguage = "sqlserver" },
                new Badge { Slug = "php-first-script", Name = "Premier script PHP", Description = "Terminer une premiere lecon PHP/Symfony.", IconName = "code", RuleType = BadgeRuleType.CompleteLessonInCourse, RuleValue = 1, RuleCourseLanguage = "php-symfony" },
                new Badge { Slug = "symfony-product-builder", Name = "Produit Symfony", Description = "Avancer dans le parcours PHP/Symfony.", IconName = "box", RuleType = BadgeRuleType.TotalXp, RuleValue = 250, RuleCourseLanguage = "php-symfony" },
                new Badge { Slug = "csharp-boss-final", Name = "Boss Final C#", Description = "Reussir le boss final C#.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinalInCourse, RuleValue = 1, RuleCourseLanguage = "csharp" },
                new Badge { Slug = "sql-boss-final", Name = "Boss Final SQL", Description = "Reussir le boss final SQL.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinalInCourse, RuleValue = 1, RuleCourseLanguage = "sqlserver" },
                new Badge { Slug = "php-boss-final", Name = "Boss Final PHP", Description = "Reussir le boss final PHP/Symfony.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinalInCourse, RuleValue = 1, RuleCourseLanguage = "php-symfony" });
        }

        if (!seededFullCatalog)
        {
            await EnsurePhpSymfonyCourseSeededAsync(db);
        }

        await EnsureIntermediateBossesSeededAsync(db);
        await db.SaveChangesAsync();
        await EnsureLearningMetadataSeededAsync(db);

        if (!await db.UserProfiles.AnyAsync())
        {
            db.UserProfiles.Add(new UserProfile { DisplayName = "Apprenant" });
        }

        await db.SaveChangesAsync();
    }

    private static async Task EnsureLearningMetadataSeededAsync(AppDbContext db)
    {
        var skillDefinitions = new (string Language, string Slug, string Name, string Description)[]
        {
            ("csharp", "csharp-variables", "Variables C#", "Declarer, initialiser et reutiliser des variables."),
            ("csharp", "csharp-types", "Types C#", "Choisir les types de base adaptes aux donnees."),
            ("csharp", "csharp-console-output", "Sortie console C#", "Afficher des informations avec Console.WriteLine."),
            ("csharp", "csharp-conditions", "Conditions C#", "Controler le flux avec if, else et switch."),
            ("csharp", "csharp-loops", "Boucles C#", "Repeter des traitements avec for, while et foreach."),
            ("csharp", "csharp-methods", "Methodes C#", "Decouper le code en actions reutilisables."),
            ("csharp", "csharp-classes", "Classes C#", "Modeliser des objets avec classes, proprietes et constructeurs."),
            ("csharp", "csharp-lists", "Listes C#", "Manipuler des collections ordonnees."),
            ("csharp", "csharp-dictionaries", "Dictionnaires C#", "Associer des cles et des valeurs."),
            ("csharp", "csharp-linq", "LINQ C#", "Interroger et transformer des collections."),
            ("csharp", "csharp-exceptions", "Exceptions C#", "Prevoir et traiter les erreurs."),
            ("csharp", "csharp-efcore", "EF Core", "Comprendre les entites, DbContext et requetes EF Core."),
            ("sqlserver", "sql-select", "SELECT SQL", "Lire des colonnes depuis une table."),
            ("sqlserver", "sql-where", "WHERE SQL", "Filtrer les lignes avec des conditions."),
            ("sqlserver", "sql-order-by", "ORDER BY SQL", "Trier les resultats."),
            ("sqlserver", "sql-aggregates", "Agregats SQL", "Calculer COUNT, SUM, AVG, MIN et MAX."),
            ("sqlserver", "sql-group-by", "GROUP BY SQL", "Regrouper les resultats."),
            ("sqlserver", "sql-joins", "Jointures SQL", "Relier plusieurs tables."),
            ("sqlserver", "sql-insert", "INSERT SQL", "Ajouter des donnees."),
            ("sqlserver", "sql-update", "UPDATE SQL", "Modifier des donnees."),
            ("sqlserver", "sql-delete", "DELETE SQL", "Supprimer des donnees prudemment."),
            ("sqlserver", "sql-modeling", "Modelisation SQL", "Structurer tables, cles et contraintes."),
            ("sqlserver", "sql-indexes", "Index SQL", "Optimiser les lectures avec des index."),
            ("php-symfony", "php-syntax", "Syntaxe PHP", "Ecrire un script PHP valide."),
            ("php-symfony", "php-variables", "Variables PHP", "Manipuler les variables PHP."),
            ("php-symfony", "php-functions", "Fonctions PHP", "Creer des fonctions PHP."),
            ("php-symfony", "php-arrays", "Tableaux PHP", "Utiliser les tableaux PHP."),
            ("php-symfony", "php-oop", "POO PHP", "Structurer le code avec classes et objets."),
            ("php-symfony", "symfony-routing", "Routing Symfony", "Declarer des routes."),
            ("php-symfony", "symfony-controller", "Controleurs Symfony", "Construire des controleurs."),
            ("php-symfony", "symfony-service", "Services Symfony", "Extraire la logique dans des services."),
            ("php-symfony", "symfony-doctrine", "Doctrine Symfony", "Modeliser et persister les entites."),
            ("php-symfony", "symfony-form", "Formulaires Symfony", "Gerer des formulaires."),
            ("php-symfony", "symfony-validation", "Validation Symfony", "Valider les donnees metier.")
        };

        foreach (var definition in skillDefinitions)
        {
            if (!await db.Skills.AnyAsync(skill => skill.Slug == definition.Slug))
            {
                db.Skills.Add(new Skill { CourseLanguage = definition.Language, Slug = definition.Slug, Name = definition.Name, Description = definition.Description });
            }
        }

        await db.SaveChangesAsync();

        var skills = await db.Skills.ToDictionaryAsync(skill => skill.Slug);
        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.LessonSkills)
            .Include(lesson => lesson.Hints)
            .Where(lesson => lesson.Chapter != null)
            .ToListAsync();
        var lessonPlans = BuildLessonPlans();

        foreach (var lesson in lessons)
        {
            if (lessonPlans.TryGetValue(lesson.Slug, out var plan))
            {
                foreach (var (slug, weight) in plan.Skills.Take(3))
                {
                    if (skills.TryGetValue(slug, out var skill) && lesson.LessonSkills.All(item => item.SkillId != skill.Id))
                    {
                        db.LessonSkills.Add(new LessonSkill { LessonId = lesson.Id, SkillId = skill.Id, Weight = weight });
                    }
                }

                if (lesson.Hints.Count == 0)
                {
                    var hints = plan.Hints.Count > 0 ? plan.Hints : DefaultLessonHints(lesson);
                    var level = 1;
                    foreach (var hint in hints)
                    {
                        db.LessonHints.Add(new LessonHint { LessonId = lesson.Id, HintLevel = level++, Content = hint });
                    }
                }

                continue;
            }

            foreach (var slug in InferSkillSlugs(lesson).Take(3))
            {
                if (skills.TryGetValue(slug, out var skill) && lesson.LessonSkills.All(item => item.SkillId != skill.Id))
                {
                    db.LessonSkills.Add(new LessonSkill { LessonId = lesson.Id, SkillId = skill.Id, Weight = 1 });
                }
            }

            if (lesson.Hints.Count == 0)
            {
                var level = 1;
                foreach (var hint in DefaultLessonHints(lesson))
                {
                    db.LessonHints.Add(new LessonHint { LessonId = lesson.Id, HintLevel = level++, Content = hint });
                }
            }
        }

        await db.SaveChangesAsync();
    }

    private sealed record LessonPlan(IReadOnlyList<(string Slug, int Weight)> Skills, IReadOnlyList<string> Hints);

    private static Dictionary<string, LessonPlan> BuildLessonPlans() => new(StringComparer.OrdinalIgnoreCase)
    {
        ["hello-world"] = new LessonPlan(
            [
                ("csharp-console-output", 2),
                ("csharp-variables", 1)
            ],
            [
                "Commence par un Console.WriteLine simple.",
                "Ajoute une deuxieme ligne avec un autre Console.WriteLine.",
                "Verifie le texte exact attendu, avec guillemets et point-virgule."
            ]),
        ["variables"] = new LessonPlan(
            [
                ("csharp-variables", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Identifie d'abord la valeur a stocker.",
                "Tu dois utiliser une variable avant l'affichage.",
                "Declare une variable puis utilise-la dans Console.WriteLine."
            ]),
        ["types"] = new LessonPlan(
            [
                ("csharp-types", 2),
                ("csharp-variables", 1)
            ],
            [
                "Chaque variable doit avoir un type adapte.",
                "Pense a int, string, bool, double.",
                "Affiche ensuite le texte demande avec ces valeurs."
            ]),
        ["operators"] = new LessonPlan(
            [
                ("csharp-conditions", 1),
                ("csharp-variables", 1),
                ("csharp-console-output", 1)
            ],
            [
                "Calcule d'abord le total avec une multiplication.",
                "Compare le total pour obtenir un booleen.",
                "Affiche ensuite les deux lignes demandees."
            ]),
        ["foundations-checkpoint"] = new LessonPlan(
            [
                ("csharp-variables", 1),
                ("csharp-types", 1),
                ("csharp-console-output", 1)
            ],
            [
                "Reprends les variables une a une.",
                "Calcule le score final avant d'afficher.",
                "Affiche exactement les deux lignes attendues."
            ]),
        ["if-else"] = new LessonPlan(
            [
                ("csharp-conditions", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Ecris un if avec une condition simple.",
                "Ajoute un else qui couvre le cas inverse.",
                "Affiche le texte exact attendu."
            ]),
        ["switch"] = new LessonPlan(
            [
                ("csharp-conditions", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Declare le switch sur la variable demandee.",
                "Ajoute un case pour la valeur specifique.",
                "Pense au break dans chaque case."
            ]),
        ["for-loop"] = new LessonPlan(
            [
                ("csharp-loops", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Initialise un compteur de 1 a 3.",
                "Affiche le numero a chaque tour.",
                "Verifie l'incrementation et la condition."
            ]),
        ["while-loop"] = new LessonPlan(
            [
                ("csharp-loops", 2),
                ("csharp-console-output", 1)
            ],
            [
                "La condition du while doit changer a chaque tour.",
                "Decremente la variable dans la boucle.",
                "Affiche l'energie avant de la modifier."
            ]),
        ["foreach-loop"] = new LessonPlan(
            [
                ("csharp-loops", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Parcours la liste avec foreach.",
                "Affiche chaque element de la collection.",
                "N'utilise pas de compteur manuel."
            ]),
        ["flow-checkpoint"] = new LessonPlan(
            [
                ("csharp-conditions", 1),
                ("csharp-loops", 1),
                ("csharp-console-output", 1)
            ],
            [
                "Commence par la boucle pour les tours.",
                "Ajoute un if / else pour la victoire.",
                "Affiche chaque ligne exactement comme demande."
            ]),
        ["create-method"] = new LessonPlan(
            [
                ("csharp-methods", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Declare une methode static void.",
                "Place l'affichage dans la methode.",
                "Appelle la methode apres sa declaration."
            ]),
        ["method-parameters"] = new LessonPlan(
            [
                ("csharp-methods", 2),
                ("csharp-variables", 1)
            ],
            [
                "Ajoute un parametre dans la signature.",
                "Utilise le parametre dans Console.WriteLine.",
                "Appelle la methode avec la valeur demandee."
            ]),
        ["return-value"] = new LessonPlan(
            [
                ("csharp-methods", 2),
                ("csharp-types", 1)
            ],
            [
                "Retourne le resultat du calcul.",
                "Utilise la valeur retournee ensuite.",
                "Affiche le texte exact attendu."
            ]),
        ["scope"] = new LessonPlan(
            [
                ("csharp-methods", 1),
                ("csharp-variables", 1)
            ],
            [
                "Declare la variable dans la methode.",
                "Affecte une valeur claire.",
                "Affiche la variable dans le meme bloc."
            ]),
        ["overload"] = new LessonPlan(
            [
                ("csharp-methods", 2),
                ("csharp-types", 1)
            ],
            [
                "Declare deux methodes avec le meme nom.",
                "Les parametres doivent differer.",
                "Affiche les deux messages attendus."
            ]),
        ["methods-checkpoint"] = new LessonPlan(
            [
                ("csharp-methods", 2),
                ("csharp-variables", 1)
            ],
            [
                "Retourne la bonne valeur dans chaque methode.",
                "Compose les appels dans l'affichage final.",
                "Verifie les valeurs attendues."
            ]),
        ["classes"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-console-output", 1)
            ],
            [
                "Declare une classe avec le mot-cle class.",
                "Laisse-la vide si demande.",
                "Garde l'affichage principal."
            ]),
        ["objects"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-variables", 1)
            ],
            [
                "Cree une instance avec new.",
                "Stocke-la dans une variable.",
                "Affiche ensuite le message."
            ]),
        ["properties"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-variables", 1)
            ],
            [
                "Declare une propriete avec get; set;.",
                "Assigne la valeur avant d'afficher.",
                "Lis la propriete dans l'affichage."
            ]),
        ["constructors"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-methods", 1)
            ],
            [
                "Ajoute un constructeur public.",
                "Assigne la propriete dans le constructeur.",
                "Instancie avec l'argument demande."
            ]),
        ["oop-basics-checkpoint"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-variables", 1)
            ],
            [
                "Declare la classe et ses proprietes.",
                "Ajoute un constructeur complet.",
                "Affiche le texte attendu."
            ]),
        ["inheritance"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-types", 1)
            ],
            [
                "Fais heriter Warrior de Character.",
                "Reutilise la propriete Name.",
                "Affiche le texte attendu."
            ]),
        ["polymorphism"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-methods", 1)
            ],
            [
                "Ajoute override sur Attack.",
                "Retourne le texte demande.",
                "Laisse la classe de base intacte."
            ]),
        ["interfaces"] = new LessonPlan(
            [
                ("csharp-classes", 1),
                ("csharp-methods", 1)
            ],
            [
                "Declare une interface avec une methode.",
                "Implemente-la dans la classe.",
                "Affiche le texte attendu."
            ]),
        ["access-modifiers"] = new LessonPlan(
            [
                ("csharp-classes", 1),
                ("csharp-variables", 1)
            ],
            [
                "Rends la propriete accessible avec public.",
                "Garde les champs prives si demandes.",
                "Affiche le resultat attendu."
            ]),
        ["advanced-oop-checkpoint"] = new LessonPlan(
            [
                ("csharp-classes", 2),
                ("csharp-methods", 1)
            ],
            [
                "Assemble heritage, override et interfaces.",
                "Respecte la structure de classe demandee.",
                "Verifie les sorties attendues."
            ]),
        ["arrays"] = new LessonPlan(
            [
                ("csharp-lists", 1),
                ("csharp-loops", 1)
            ],
            [
                "Declare un tableau avec les valeurs demandees.",
                "Parcours-le avec une boucle.",
                "Affiche chaque element."
            ]),
        ["lists"] = new LessonPlan(
            [
                ("csharp-lists", 2),
                ("csharp-loops", 1)
            ],
            [
                "Cree une List avec les elements.",
                "Ajoute ou modifie l'element demande.",
                "Affiche la liste."
            ]),
        ["dictionaries"] = new LessonPlan(
            [
                ("csharp-dictionaries", 2),
                ("csharp-lists", 1)
            ],
            [
                "Declare un Dictionary.",
                "Ajoute les paires cle/valeur.",
                "Affiche la valeur attendue."
            ]),
        ["linq"] = new LessonPlan(
            [
                ("csharp-linq", 2),
                ("csharp-lists", 1)
            ],
            [
                "Utilise une requete LINQ simple.",
                "Filtre ou projette selon la demande.",
                "Affiche le resultat attendu."
            ]),
        ["data-structures-checkpoint"] = new LessonPlan(
            [
                ("csharp-lists", 1),
                ("csharp-dictionaries", 1),
                ("csharp-linq", 1)
            ],
            [
                "Combine liste, dictionnaire et LINQ.",
                "Assure-toi que les structures sont remplies.",
                "Affiche le resultat attendu."
            ]),
        ["try-catch"] = new LessonPlan(
            [
                ("csharp-exceptions", 2),
                ("csharp-conditions", 1)
            ],
            [
                "Enveloppe le code dans try/catch.",
                "Capture l'exception demandee.",
                "Affiche le message attendu."
            ]),
        ["exceptions"] = new LessonPlan(
            [
                ("csharp-exceptions", 2),
                ("csharp-variables", 1)
            ],
            [
                "Lance une exception au bon endroit.",
                "Utilise throw avec le type demande.",
                "Affiche ou gere le message attendu."
            ]),
        ["nullables"] = new LessonPlan(
            [
                ("csharp-exceptions", 1),
                ("csharp-types", 1)
            ],
            [
                "Utilise le type nullable avec ?.",
                "Controle la valeur avant usage.",
                "Affiche le resultat attendu."
            ]),
        ["errors-checkpoint"] = new LessonPlan(
            [
                ("csharp-exceptions", 2),
                ("csharp-conditions", 1)
            ],
            [
                "Combine try/catch et validation.",
                "Laisse le programme stable.",
                "Affiche les messages demandes."
            ]),
        ["relational-databases"] = new LessonPlan(
            [
                ("csharp-efcore", 2),
                ("csharp-classes", 1)
            ],
            [
                "Pense entites et relations.",
                "Structure les classes de modele.",
                "Relie les tables comme demande."
            ]),
        ["entity-framework-core"] = new LessonPlan(
            [
                ("csharp-efcore", 2),
                ("csharp-classes", 1)
            ],
            [
                "Cree le DbContext.",
                "Ajoute les DbSet demandes.",
                "Verifie la configuration."
            ]),
        ["dbcontext"] = new LessonPlan(
            [
                ("csharp-efcore", 2),
                ("csharp-classes", 1)
            ],
            [
                "Declare un DbContext derive.",
                "Ajoute les DbSet utiles.",
                "Configure l'acces aux donnees."
            ]),
        ["crud"] = new LessonPlan(
            [
                ("csharp-efcore", 2),
                ("csharp-linq", 1)
            ],
            [
                "Ajoute une creation ou mise a jour.",
                "Utilise SaveChanges si demande.",
                "Affiche ou retourne le resultat attendu."
            ]),
        ["database-checkpoint"] = new LessonPlan(
            [
                ("csharp-efcore", 2),
                ("csharp-linq", 1)
            ],
            [
                "Relie les entites aux operations.",
                "Ecris la requete finale.",
                "Affiche le resultat attendu."
            ]),
        ["sql-relational-database"] = new LessonPlan(
            [
                ("sql-select", 2),
                ("sql-modeling", 1)
            ],
            [
                "Utilise SELECT et FROM sur la bonne table.",
                "Choisis uniquement la colonne demandee.",
                "Evite SELECT *."
            ]),
        ["sql-tables-rows-columns"] = new LessonPlan(
            [
                ("sql-select", 2),
                ("sql-modeling", 1)
            ],
            [
                "Liste chaque colonne demandee.",
                "Cible la table Products.",
                "Evite SELECT *."
            ]),
        ["sql-server-data-types"] = new LessonPlan(
            [
                ("sql-select", 2),
                ("sql-modeling", 1)
            ],
            [
                "Selectionne les colonnes Name, Price, IsActive.",
                "Garde une requete simple.",
                "Verifie le resultat."
            ]),
        ["sql-select"] = new LessonPlan(
            [
                ("sql-select", 2),
                ("sql-order-by", 1)
            ],
            [
                "Choisis seulement Name et Price.",
                "Garde la requete lisible.",
                "Evite SELECT *."
            ]),
        ["sql-where"] = new LessonPlan(
            [
                ("sql-where", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute WHERE avec les deux conditions.",
                "Garde les colonnes demandees.",
                "Verifie les deux produits attendus."
            ]),
        ["sql-foundations-checkpoint"] = new LessonPlan(
            [
                ("sql-where", 1),
                ("sql-select", 1),
                ("sql-order-by", 1)
            ],
            [
                "Selectionne les colonnes demandees.",
                "Filtre avec les conditions.",
                "Valide les lignes attendues."
            ]),
        ["sql-order-by"] = new LessonPlan(
            [
                ("sql-order-by", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute ORDER BY sur la colonne cible.",
                "Verifie l'ordre attendu.",
                "Evite SELECT *."
            ]),
        ["sql-top"] = new LessonPlan(
            [
                ("sql-order-by", 1),
                ("sql-select", 1)
            ],
            [
                "Utilise TOP avec le nombre demande.",
                "Trie si necessaire.",
                "Limite correctement les lignes."
            ]),
        ["sql-distinct"] = new LessonPlan(
            [
                ("sql-select", 1),
                ("sql-where", 1)
            ],
            [
                "Ajoute DISTINCT sur la bonne colonne.",
                "Garde une requete simple.",
                "Verifie les doublons."
            ]),
        ["sql-like"] = new LessonPlan(
            [
                ("sql-where", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise LIKE avec %.",
                "Garde la bonne colonne dans SELECT.",
                "Verifie les lignes attendues."
            ]),
        ["sql-in"] = new LessonPlan(
            [
                ("sql-where", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise IN avec les valeurs attendues.",
                "Selectionne les colonnes demandees.",
                "Verifie les lignes."
            ]),
        ["sql-between"] = new LessonPlan(
            [
                ("sql-where", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise BETWEEN pour le filtre.",
                "Selectionne les colonnes demandees.",
                "Verifie le resultat."
            ]),
        ["sql-is-null"] = new LessonPlan(
            [
                ("sql-where", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise IS NULL ou IS NOT NULL.",
                "Garde une requete claire.",
                "Verifie les lignes."
            ]),
        ["sql-filtering-checkpoint"] = new LessonPlan(
            [
                ("sql-where", 1),
                ("sql-order-by", 1),
                ("sql-select", 1)
            ],
            [
                "Applique les filtres successifs.",
                "Trie si demande.",
                "Valide le resultat final."
            ]),
        ["sql-count"] = new LessonPlan(
            [
                ("sql-aggregates", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise COUNT sur la colonne.",
                "Ajoute un alias si demande.",
                "Verifie le resultat."
            ]),
        ["sql-sum"] = new LessonPlan(
            [
                ("sql-aggregates", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise SUM sur la colonne.",
                "Ajoute un alias si besoin.",
                "Verifie le resultat."
            ]),
        ["sql-avg"] = new LessonPlan(
            [
                ("sql-aggregates", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise AVG sur la colonne.",
                "Ajoute un alias si besoin.",
                "Verifie le resultat."
            ]),
        ["sql-min-max"] = new LessonPlan(
            [
                ("sql-aggregates", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise MIN et MAX.",
                "Affiche les deux valeurs.",
                "Verifie le resultat."
            ]),
        ["sql-group-by"] = new LessonPlan(
            [
                ("sql-group-by", 2),
                ("sql-aggregates", 1)
            ],
            [
                "Ajoute GROUP BY sur la bonne colonne.",
                "Combine avec un agregat.",
                "Verifie les groupes."
            ]),
        ["sql-having"] = new LessonPlan(
            [
                ("sql-group-by", 2),
                ("sql-aggregates", 1)
            ],
            [
                "Ajoute HAVING avec l'agregat.",
                "Garde le GROUP BY.",
                "Verifie les groupes filtres."
            ]),
        ["sql-aggregation-checkpoint"] = new LessonPlan(
            [
                ("sql-aggregates", 1),
                ("sql-group-by", 1),
                ("sql-select", 1)
            ],
            [
                "Combine SELECT, GROUP BY et agregats.",
                "Filtre si necessaire.",
                "Verifie les valeurs finales."
            ]),
        ["sql-inner-join"] = new LessonPlan(
            [
                ("sql-joins", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute INNER JOIN avec la bonne cle.",
                "Selectionne les colonnes demandees.",
                "Verifie les lignes."
            ]),
        ["sql-left-join"] = new LessonPlan(
            [
                ("sql-joins", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute LEFT JOIN avec la bonne cle.",
                "Garde toutes les lignes de gauche.",
                "Verifie les resultats."
            ]),
        ["sql-right-join"] = new LessonPlan(
            [
                ("sql-joins", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute RIGHT JOIN si supporte.",
                "Garde toutes les lignes de droite.",
                "Verifie les resultats."
            ]),
        ["sql-full-outer-join"] = new LessonPlan(
            [
                ("sql-joins", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute FULL OUTER JOIN.",
                "Garde toutes les lignes des deux tables.",
                "Verifie les resultats."
            ]),
        ["sql-table-aliases"] = new LessonPlan(
            [
                ("sql-joins", 1),
                ("sql-select", 1)
            ],
            [
                "Ajoute des alias courts aux tables.",
                "Utilise-les dans la requete.",
                "Garde la lecture claire."
            ]),
        ["sql-joins-checkpoint"] = new LessonPlan(
            [
                ("sql-joins", 2),
                ("sql-select", 1)
            ],
            [
                "Combine jointures et selection.",
                "Verifie les resultats.",
                "Garde la requete lisible."
            ]),
        ["sql-insert"] = new LessonPlan(
            [
                ("sql-insert", 2),
                ("sql-select", 1)
            ],
            [
                "Utilise INSERT INTO avec les colonnes.",
                "Ajoute les valeurs demandees.",
                "Verifie la lecture finale."
            ]),
        ["sql-update"] = new LessonPlan(
            [
                ("sql-update", 2),
                ("sql-where", 1)
            ],
            [
                "Ajoute UPDATE avec SET.",
                "Ajoute un WHERE precis.",
                "Verifie la sortie finale."
            ]),
        ["sql-delete"] = new LessonPlan(
            [
                ("sql-delete", 2),
                ("sql-where", 1)
            ],
            [
                "Ajoute DELETE avec WHERE.",
                "Ne supprime pas trop de lignes.",
                "Verifie la sortie finale."
            ]),
        ["sql-simple-transaction"] = new LessonPlan(
            [
                ("sql-update", 1),
                ("sql-where", 1)
            ],
            [
                "Entoure les operations d'une transaction.",
                "Applique la modification demandee.",
                "Verifie le resultat."
            ]),
        ["sql-rollback-commit"] = new LessonPlan(
            [
                ("sql-update", 1),
                ("sql-where", 1)
            ],
            [
                "Ajoute COMMIT ou ROLLBACK.",
                "Garde la transaction coherente.",
                "Verifie la sortie."
            ]),
        ["sql-modification-checkpoint"] = new LessonPlan(
            [
                ("sql-update", 1),
                ("sql-delete", 1),
                ("sql-insert", 1)
            ],
            [
                "Combine insertion, mise a jour et suppression.",
                "Ajoute les WHERE necessaires.",
                "Verifie la sortie finale."
            ]),
        ["sql-primary-keys"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute la cle primaire.",
                "Verifie la definition de table.",
                "Garde la structure demandee."
            ]),
        ["sql-foreign-keys"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-joins", 1)
            ],
            [
                "Ajoute la cle etrangere.",
                "Verifie la reference.",
                "Garde la structure demandee."
            ]),
        ["sql-constraints"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute la contrainte demandee.",
                "Verifie la syntaxe.",
                "Garde la structure attendue."
            ]),
        ["sql-simple-normalization"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-joins", 1)
            ],
            [
                "Separe les entites correctement.",
                "Relie les tables.",
                "Verifie le resultat."
            ]),
        ["sql-relationships"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-joins", 1)
            ],
            [
                "Declare les relations.",
                "Ajoute les cles etrangeres.",
                "Verifie les jointures."
            ]),
        ["sql-modeling-checkpoint"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-joins", 1)
            ],
            [
                "Assemble schema et relations.",
                "Verifie les contraintes.",
                "Valide la requete finale."
            ]),
        ["sql-indexes"] = new LessonPlan(
            [
                ("sql-indexes", 2),
                ("sql-select", 1)
            ],
            [
                "Cree un index sur la colonne cible.",
                "Utilise la bonne syntaxe.",
                "Verifie la requete finale."
            ]),
        ["sql-views"] = new LessonPlan(
            [
                ("sql-select", 1),
                ("sql-order-by", 1)
            ],
            [
                "Cree une vue avec SELECT.",
                "Garde les colonnes attendues.",
                "Verifie la vue."
            ]),
        ["sql-stored-procedures"] = new LessonPlan(
            [
                ("sql-aggregates", 1),
                ("sql-select", 1)
            ],
            [
                "Declare une procedure stockee.",
                "Ajoute un SELECT simple.",
                "Verifie la sortie."
            ]),
        ["sql-functions"] = new LessonPlan(
            [
                ("sql-aggregates", 1),
                ("sql-select", 1)
            ],
            [
                "Cree une fonction SQL.",
                "Retourne la valeur demandee.",
                "Verifie le resultat."
            ]),
        ["sql-tsql-variables"] = new LessonPlan(
            [
                ("sql-select", 2)
            ],
            [
                "Declare la variable avec DECLARE.",
                "Affecte une valeur avec SET.",
                "Utilise-la dans la requete."
            ]),
        ["sql-advanced-checkpoint"] = new LessonPlan(
            [
                ("sql-indexes", 1),
                ("sql-aggregates", 1),
                ("sql-select", 1)
            ],
            [
                "Combine les notions avancees.",
                "Verifie les objets crees.",
                "Valide le resultat final."
            ]),
        ["sql-complete-schema"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-joins", 1)
            ],
            [
                "Cree le schema complet.",
                "Definis les cles et relations.",
                "Verifie les tables."
            ]),
        ["sql-create-project-tables"] = new LessonPlan(
            [
                ("sql-modeling", 2),
                ("sql-select", 1)
            ],
            [
                "Cree les tables demandees.",
                "Respecte les types.",
                "Verifie le schema."
            ]),
        ["sql-seed-project-data"] = new LessonPlan(
            [
                ("sql-insert", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute les donnees avec INSERT.",
                "Verifie les valeurs.",
                "Valide la lecture finale."
            ]),
        ["sql-business-queries"] = new LessonPlan(
            [
                ("sql-joins", 1),
                ("sql-aggregates", 1),
                ("sql-group-by", 1)
            ],
            [
                "Combine jointures et agregats.",
                "Ajoute le GROUP BY si besoin.",
                "Verifie les resultats."
            ]),
        ["sql-simple-optimization"] = new LessonPlan(
            [
                ("sql-indexes", 2),
                ("sql-select", 1)
            ],
            [
                "Ajoute un index adapte.",
                "Verifie la requete.",
                "Valide les resultats."
            ]),
        ["sql-project-checkpoint"] = new LessonPlan(
            [
                ("sql-joins", 1),
                ("sql-aggregates", 1),
                ("sql-select", 1)
            ],
            [
                "Assemble les requetes du projet.",
                "Verifie les resultats.",
                "Valide la sortie finale."
            ]),
        ["sql-boss-final-ecommerce"] = new LessonPlan(
            [
                ("sql-joins", 1),
                ("sql-aggregates", 1),
                ("sql-group-by", 1)
            ],
            [
                "Combine schema, donnees et requetes.",
                "Verifie les resultats attendus.",
                "Valide chaque section."
            ]),
        ["boss-final"] = new LessonPlan(
            [
                ("csharp-efcore", 1),
                ("csharp-linq", 1),
                ("csharp-classes", 1)
            ],
            [
                "Assemble les modules precedents.",
                "Verifie chaque etape du projet.",
                "Valide les sorties demandees."
            ]),
        ["php-symfony-boss-final-products"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-form", 1),
                ("symfony-doctrine", 1)
            ],
            [
                "Ajoute entite, routes et controller.",
                "Integre formulaire et validation.",
                "Pense au service et a la securite."
            ]),
        ["php-symfony-module-1-intermediate-boss"] = new LessonPlan(
            [
                ("php-syntax", 1),
                ("php-variables", 1),
                ("php-functions", 1)
            ],
            [
                "Reprends les bases PHP du module.",
                "Verifie la syntaxe, les variables et echo.",
                "Affiche le texte attendu."
            ]),
        ["php-syntax"] = new LessonPlan(
            [
                ("php-syntax", 2),
                ("php-variables", 1)
            ],
            [
                "Commence par la balise PHP.",
                "Utilise echo avec le texte exact.",
                "Ajoute le point-virgule."
            ]),
        ["php-variables"] = new LessonPlan(
            [
                ("php-variables", 2),
                ("php-syntax", 1)
            ],
            [
                "Declare $name avec Ada.",
                "Utilise echo pour afficher.",
                "Concatene le texte et la variable."
            ]),
        ["php-types"] = new LessonPlan(
            [
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "Declare les quatre variables.",
                "Assigne les bons types.",
                "Affiche le texte demande."
            ]),
        ["php-conditions"] = new LessonPlan(
            [
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "Ecris un if et un else.",
                "Teste la variable stock.",
                "Affiche le texte attendu."
            ]),
        ["php-loops"] = new LessonPlan(
            [
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "Utilise un for avec $i.",
                "Affiche dans la boucle.",
                "Verifie les trois lignes."
            ]),
        ["php-functions"] = new LessonPlan(
            [
                ("php-functions", 2),
                ("php-syntax", 1)
            ],
            [
                "Declare la fonction avec types.",
                "Retourne la chaine demandee.",
                "Appelle la fonction."
            ]),
        ["php-symfony-module-2-classes"] = new LessonPlan(
            [
                ("php-oop", 2),
                ("php-syntax", 1)
            ],
            [
                "Declare une classe PHP.",
                "Utilise le mot-cle class.",
                "Garde la structure simple."
            ]),
        ["php-symfony-module-2-objets"] = new LessonPlan(
            [
                ("php-oop", 2),
                ("php-syntax", 1)
            ],
            [
                "Instancie un objet avec new.",
                "Stocke-le dans une variable.",
                "Garde un code PHP valide."
            ]),
        ["php-symfony-module-2-proprietes"] = new LessonPlan(
            [
                ("php-oop", 2),
                ("php-variables", 1)
            ],
            [
                "Declare une propriete.",
                "Garde la visibilite demandee.",
                "Utilise la bonne syntaxe PHP."
            ]),
        ["php-symfony-module-2-methodes"] = new LessonPlan(
            [
                ("php-oop", 2),
                ("php-functions", 1)
            ],
            [
                "Declare une methode publique.",
                "Retourne la valeur attendue.",
                "Respecte les types."
            ]),
        ["php-symfony-module-2-constructeurs"] = new LessonPlan(
            [
                ("php-oop", 2),
                ("php-variables", 1)
            ],
            [
                "Ajoute __construct.",
                "Affecte les proprietes.",
                "Verifie la signature."
            ]),
        ["php-symfony-module-2-encapsulation"] = new LessonPlan(
            [
                ("php-oop", 2),
                ("php-functions", 1)
            ],
            [
                "Rends les proprietes privees.",
                "Expose un getter.",
                "Garde un code PHP valide."
            ]),
        ["php-symfony-module-3-structure-dun-projet-symfony"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Respecte la structure Symfony.",
                "Utilise les namespaces attendus.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-3-routes"] = new LessonPlan(
            [
                ("symfony-routing", 2),
                ("symfony-controller", 1)
            ],
            [
                "Ajoute l'attribut Route.",
                "Definis le chemin.",
                "Garde un controller valide."
            ]),
        ["php-symfony-module-3-controllers"] = new LessonPlan(
            [
                ("symfony-controller", 2),
                ("symfony-routing", 1)
            ],
            [
                "Etends AbstractController.",
                "Ajoute une methode d'action.",
                "Retourne une reponse."
            ]),
        ["php-symfony-module-3-responses"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Retourne une Response.",
                "Utilise le bon namespace.",
                "Garde un code coherent."
            ]),
        ["php-symfony-module-3-templates-twig"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Utilise la syntaxe Twig.",
                "Affiche une variable.",
                "Garde un template minimal."
            ]),
        ["php-symfony-module-3-parametres-de-route"] = new LessonPlan(
            [
                ("symfony-routing", 2),
                ("symfony-controller", 1)
            ],
            [
                "Ajoute un parametre dans la route.",
                "Utilise-le dans la methode.",
                "Retourne une reponse."
            ]),
        ["php-symfony-module-4-creation-de-formulaire"] = new LessonPlan(
            [
                ("symfony-form", 2),
                ("symfony-controller", 1)
            ],
            [
                "Cree le formulaire avec createForm.",
                "Utilise un type de formulaire.",
                "Garde la syntaxe Symfony."
            ]),
        ["php-symfony-module-4-validation"] = new LessonPlan(
            [
                ("symfony-validation", 2),
                ("symfony-form", 1)
            ],
            [
                "Ajoute une contrainte de validation.",
                "Utilise l'attribut Assert.",
                "Garde la propriete ciblee."
            ]),
        ["php-symfony-module-4-contraintes"] = new LessonPlan(
            [
                ("symfony-validation", 2),
                ("symfony-form", 1)
            ],
            [
                "Utilise une contrainte specifique.",
                "Garde la syntaxe d'attribut.",
                "Associe a la bonne propriete."
            ]),
        ["php-symfony-module-4-gestion-des-erreurs"] = new LessonPlan(
            [
                ("symfony-form", 1),
                ("symfony-validation", 1)
            ],
            [
                "Affiche form_errors dans Twig.",
                "Garde la syntaxe Twig.",
                "Cible le bon champ."
            ]),
        ["php-symfony-module-4-traitement-de-la-soumission"] = new LessonPlan(
            [
                ("symfony-form", 2),
                ("symfony-controller", 1)
            ],
            [
                "Utilise handleRequest.",
                "Teste isSubmitted et isValid.",
                "Garde le flux complet."
            ]),
        ["php-symfony-module-5-entites"] = new LessonPlan(
            [
                ("symfony-doctrine", 2),
                ("php-oop", 1)
            ],
            [
                "Ajoute l'attribut ORM Entity.",
                "Declare la classe.",
                "Garde les namespaces."
            ]),
        ["php-symfony-module-5-repositories"] = new LessonPlan(
            [
                ("symfony-doctrine", 2),
                ("php-oop", 1)
            ],
            [
                "Etends ServiceEntityRepository.",
                "Ajoute le constructeur.",
                "Garde un repository valide."
            ]),
        ["php-symfony-module-5-migrations"] = new LessonPlan(
            [
                ("symfony-doctrine", 2),
                ("php-syntax", 1)
            ],
            [
                "Declare une migration.",
                "Utilise AbstractMigration.",
                "Garde la structure attendue."
            ]),
        ["php-symfony-module-5-relations-simples"] = new LessonPlan(
            [
                ("symfony-doctrine", 2),
                ("php-oop", 1)
            ],
            [
                "Ajoute l'attribut ManyToOne.",
                "Declare la propriete.",
                "Garde la relation coherente."
            ]),
        ["php-symfony-module-5-crud-avec-doctrine"] = new LessonPlan(
            [
                ("symfony-doctrine", 2),
                ("php-oop", 1)
            ],
            [
                "Utilise persist et flush.",
                "Ajoute remove si demande.",
                "Garde un flux Doctrine clair."
            ]),
        ["php-symfony-module-6-services"] = new LessonPlan(
            [
                ("symfony-service", 2),
                ("php-oop", 1)
            ],
            [
                "Declare un service.",
                "Garde la classe simple.",
                "Utilise le bon namespace."
            ]),
        ["php-symfony-module-6-injection-de-dependances"] = new LessonPlan(
            [
                ("symfony-service", 2),
                ("php-oop", 1)
            ],
            [
                "Ajoute un constructeur.",
                "Injecte le service.",
                "Garde la syntaxe PHP 8."
            ]),
        ["php-symfony-module-6-configuration"] = new LessonPlan(
            [
                ("symfony-service", 1),
                ("php-syntax", 1)
            ],
            [
                "Ajoute la configuration service.",
                "Respecte le YAML.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-6-separation-controller---service"] = new LessonPlan(
            [
                ("symfony-service", 2),
                ("symfony-controller", 1)
            ],
            [
                "Appelle un service depuis le controller.",
                "Garde une methode claire.",
                "Retourne une reponse."
            ]),
        ["php-symfony-module-6-bonnes-pratiques-symfony"] = new LessonPlan(
            [
                ("symfony-service", 2),
                ("php-oop", 1)
            ],
            [
                "Utilise readonly si demande.",
                "Garde une classe simple.",
                "Respecte la syntaxe PHP."
            ]),
        ["php-symfony-module-7-authentification"] = new LessonPlan(
            [
                ("php-syntax", 1),
                ("symfony-controller", 1)
            ],
            [
                "Declare la classe d'authentification.",
                "Etends la bonne classe.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-7-utilisateurs"] = new LessonPlan(
            [
                ("php-oop", 1),
                ("php-syntax", 1)
            ],
            [
                "Implemente UserInterface.",
                "Declare la classe.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-7-roles"] = new LessonPlan(
            [
                ("php-syntax", 1),
                ("symfony-controller", 1)
            ],
            [
                "Retourne un tableau de roles.",
                "Utilise ROLE_USER.",
                "Garde un exemple simple."
            ]),
        ["php-symfony-module-7-protection-des-routes"] = new LessonPlan(
            [
                ("symfony-routing", 1),
                ("symfony-controller", 1)
            ],
            [
                "Ajoute l'attribut IsGranted.",
                "Utilise ROLE_USER.",
                "Garde la route protegee."
            ]),
        ["php-symfony-module-7-autorisations-simples"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Utilise denyAccessUnlessGranted.",
                "Cible ROLE_USER.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-8-mini-application-mvc"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Declare un controller Symfony.",
                "Ajoute une route.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-8-routes"] = new LessonPlan(
            [
                ("symfony-routing", 2),
                ("symfony-controller", 1)
            ],
            [
                "Ajoute une route dans le module.",
                "Utilise le bon nom.",
                "Garde la syntaxe Symfony."
            ]),
        ["php-symfony-module-8-controllers"] = new LessonPlan(
            [
                ("symfony-controller", 2),
                ("symfony-routing", 1)
            ],
            [
                "Ajoute un controller.",
                "Retourne une reponse.",
                "Garde le flux minimal."
            ]),
        ["php-symfony-module-8-templates-twig"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Utilise Twig pour afficher.",
                "Garde un template simple.",
                "Verifie la variable."
            ]),
        ["php-symfony-module-8-formulaires"] = new LessonPlan(
            [
                ("symfony-form", 2),
                ("symfony-controller", 1)
            ],
            [
                "Cree un formulaire Symfony.",
                "Utilise createForm.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-8-doctrine"] = new LessonPlan(
            [
                ("symfony-doctrine", 2),
                ("php-oop", 1)
            ],
            [
                "Utilise EntityManager.",
                "Ajoute persist et flush.",
                "Garde un flux Doctrine."
            ]),
        ["php-symfony-module-8-services"] = new LessonPlan(
            [
                ("symfony-service", 2),
                ("php-oop", 1)
            ],
            [
                "Declare un service.",
                "Injecte-le si besoin.",
                "Garde un exemple minimal."
            ]),
        ["php-symfony-module-8-securite-simple"] = new LessonPlan(
            [
                ("symfony-controller", 1),
                ("symfony-routing", 1)
            ],
            [
                "Protege la route.",
                "Utilise IsGranted.",
                "Garde un exemple minimal."
            ])
    };

    private static IReadOnlyList<string> DefaultLessonHints(Lesson lesson) =>
    [
        "Relis l'objectif et repere les mots-cles importants.",
        "Compare ton code aux criteres de validation affiches apres soumission.",
        $"Travaille directement la demande: {lesson.ExercisePrompt}"
    ];

    private static IReadOnlyList<string> InferSkillSlugs(Lesson lesson)
    {
        var slug = lesson.Slug.ToLowerInvariant();
        var language = lesson.Chapter?.Course?.Language;
        if (language == "sqlserver")
        {
            if (slug.Contains("join")) return ["sql-joins", "sql-select"];
            if (slug.Contains("group") || slug.Contains("having")) return ["sql-group-by", "sql-aggregates"];
            if (slug.Contains("count") || slug.Contains("sum") || slug.Contains("avg") || slug.Contains("min") || slug.Contains("max")) return ["sql-aggregates", "sql-select"];
            if (slug.Contains("order")) return ["sql-order-by", "sql-select"];
            if (slug.Contains("where") || slug.Contains("like") || slug.Contains("between") || slug.Contains("null") || slug.Contains("in")) return ["sql-where", "sql-select"];
            if (slug.Contains("insert")) return ["sql-insert", "sql-select"];
            if (slug.Contains("update")) return ["sql-update", "sql-where"];
            if (slug.Contains("delete")) return ["sql-delete", "sql-where"];
            if (slug.Contains("index")) return ["sql-indexes", "sql-select"];
            if (slug.Contains("table") || slug.Contains("key") || slug.Contains("constraint") || slug.Contains("model")) return ["sql-modeling", "sql-select"];
            if (slug.Contains("variable")) return ["sql-tsql-variables", "sql-select"];
            return ["sql-select"];
        }

        if (language == "php-symfony")
        {
            if (slug.Contains("route")) return ["symfony-routing", "symfony-controller"];
            if (slug.Contains("controller")) return ["symfony-controller", "symfony-routing"];
            if (slug.Contains("service")) return ["symfony-service", "php-oop"];
            if (slug.Contains("doctrine") || slug.Contains("entity")) return ["symfony-doctrine", "php-oop"];
            if (slug.Contains("form")) return ["symfony-form", "symfony-validation"];
            if (slug.Contains("validation")) return ["symfony-validation", "symfony-form"];
            if (slug.Contains("array")) return ["php-arrays", "php-syntax"];
            if (slug.Contains("function")) return ["php-functions", "php-syntax"];
            if (slug.Contains("variable")) return ["php-variables", "php-syntax"];
            if (slug.Contains("class") || slug.Contains("oop")) return ["php-oop", "php-syntax"];
            return ["php-syntax"];
        }

        if (slug.Contains("variable")) return ["csharp-variables", "csharp-console-output"];
        if (slug.Contains("type")) return ["csharp-types", "csharp-variables"];
        if (slug.Contains("if") || slug.Contains("condition") || slug.Contains("switch")) return ["csharp-conditions", "csharp-console-output"];
        if (slug.Contains("loop") || slug.Contains("for") || slug.Contains("while") || slug.Contains("foreach")) return ["csharp-loops", "csharp-console-output"];
        if (slug.Contains("method") || slug.Contains("parameter") || slug.Contains("return")) return ["csharp-methods", "csharp-types"];
        if (slug.Contains("class") || slug.Contains("object") || slug.Contains("constructor") || slug.Contains("property")) return ["csharp-classes", "csharp-types"];
        if (slug.Contains("list")) return ["csharp-lists", "csharp-loops"];
        if (slug.Contains("dictionary")) return ["csharp-dictionaries", "csharp-lists"];
        if (slug.Contains("linq")) return ["csharp-linq", "csharp-lists"];
        if (slug.Contains("exception") || slug.Contains("try") || slug.Contains("error")) return ["csharp-exceptions", "csharp-conditions"];
        if (slug.Contains("ef") || slug.Contains("database")) return ["csharp-efcore", "csharp-classes"];
        return ["csharp-console-output"];
    }

    private static IReadOnlyList<(int Level, string Content)> InferHints(Lesson lesson) =>
    [
        (1, "Relis l'objectif et repere les mots-cles importants."),
        (2, "Compare ton code aux criteres de validation affiches apres soumission."),
        (3, $"Travaille directement la demande: {lesson.ExercisePrompt}")
    ];

    private static Chapter Chapter(string title, string description, int sortOrder, int requiredXp, List<Lesson> lessons) =>
        new() { Title = title, Description = description, SortOrder = sortOrder, RequiredXp = requiredXp, Lessons = lessons };

    private static async Task EnsurePhpSymfonyCourseSeededAsync(AppDbContext db)
    {
        if (!await db.Courses.AnyAsync(course => course.Slug == "php-symfony"))
        {
            var phpCourse = PhpSymfonyCourse();
            AttachIntermediateBosses(phpCourse);
            db.Courses.Add(phpCourse);
        }

        if (!await db.Badges.AnyAsync(badge => badge.Slug == "php-first-script"))
        {
            db.Badges.Add(new Badge { Slug = "php-first-script", Name = "Premier script PHP", Description = "Terminer une premiere lecon PHP/Symfony.", IconName = "code", RuleType = BadgeRuleType.CompleteLessonInCourse, RuleValue = 1, RuleCourseLanguage = "php-symfony" });
        }

        if (!await db.Badges.AnyAsync(badge => badge.Slug == "symfony-product-builder"))
        {
            db.Badges.Add(new Badge { Slug = "symfony-product-builder", Name = "Produit Symfony", Description = "Avancer dans le parcours PHP/Symfony.", IconName = "box", RuleType = BadgeRuleType.TotalXp, RuleValue = 250, RuleCourseLanguage = "php-symfony" });
        }

        if (!await db.Badges.AnyAsync(badge => badge.Slug == "sql-first-select"))
        {
            db.Badges.Add(new Badge { Slug = "sql-first-select", Name = "Premier SELECT", Description = "Terminer une premiere lecon SQL.", IconName = "database", RuleType = BadgeRuleType.CompleteLessonInCourse, RuleValue = 1, RuleCourseLanguage = "sqlserver" });
        }

        if (!await db.Badges.AnyAsync(badge => badge.Slug == "csharp-boss-final"))
        {
            db.Badges.Add(new Badge { Slug = "csharp-boss-final", Name = "Boss Final C#", Description = "Reussir le boss final C#.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinalInCourse, RuleValue = 1, RuleCourseLanguage = "csharp" });
        }

        if (!await db.Badges.AnyAsync(badge => badge.Slug == "sql-boss-final"))
        {
            db.Badges.Add(new Badge { Slug = "sql-boss-final", Name = "Boss Final SQL", Description = "Reussir le boss final SQL.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinalInCourse, RuleValue = 1, RuleCourseLanguage = "sqlserver" });
        }

        if (!await db.Badges.AnyAsync(badge => badge.Slug == "php-boss-final"))
        {
            db.Badges.Add(new Badge { Slug = "php-boss-final", Name = "Boss Final PHP", Description = "Reussir le boss final PHP/Symfony.", IconName = "trophy", RuleType = BadgeRuleType.CompleteBossFinalInCourse, RuleValue = 1, RuleCourseLanguage = "php-symfony" });
        }
    }

    private static Course PhpSymfonyCourse() =>
        new()
        {
            Slug = "php-symfony",
            Title = "PHP / Symfony",
            Description = "Un parcours progressif centre sur PHP moderne et Symfony.",
            Language = "php-symfony",
            SortOrder = 3,
            Chapters =
            [
                Chapter("Module 1 - Fondations PHP", "Syntaxe PHP, variables, types, conditions, boucles et fonctions.", 1, 0,
                [
                    PhpLesson(
                        "php-syntax",
                        "Syntaxe PHP",
                        "Comprendre la forme minimale d'un script PHP et l'affichage avec echo.",
                        "Un fichier PHP commence souvent par <?php. Les instructions se terminent par un point-virgule. echo affiche une valeur.",
                        "<?php\necho \"Bonjour PHP\";",
                        "Cree un script qui affiche Bonjour PHP avec echo.",
                        """
                        <?php

                        // Affiche Bonjour PHP
                        """,
                        "Le script PHP contient la balise d'ouverture, echo et le texte attendu.",
                        "Place <?php au debut, puis utilise echo avec le texte exact.",
                        25,
                        1,
                        [
                            Required("Commence par la balise PHP", "<?php"),
                            Required("Utilise echo", "echo"),
                            Output("Contient Bonjour PHP", "Bonjour PHP")
                        ],
                        conceptSummary: "La syntaxe PHP repose sur une balise d'ouverture, des instructions et des points-virgules.",
                        finalCorrection:
                        """
                        <?php

                        echo "Bonjour PHP";
                        """),
                    PhpLesson(
                        "php-variables",
                        "Variables",
                        "Stocker une valeur dans une variable PHP et la reutiliser.",
                        "Une variable PHP commence par $. Elle peut contenir du texte, un nombre, un booleen ou une structure plus complexe.",
                        "<?php\n$name = \"Ada\";\necho $name;",
                        "Declare $name avec Ada, puis affiche Bonjour Ada.",
                        """
                        <?php

                        $name = "";
                        // Affiche Bonjour Ada
                        """,
                        "La variable est declaree et reutilisee dans l'affichage.",
                        "Une variable PHP commence par $. Concatene le texte avec la variable.",
                        35,
                        2,
                        [
                            Required("Declare $name", "$name"),
                            Required("Stocke Ada", "\"Ada\""),
                            Required("Utilise echo", "echo"),
                            Output("Contient Bonjour Ada", "Bonjour Ada")
                        ],
                        conceptSummary: "Une variable PHP porte un nom prefixe par $ et permet de reutiliser une valeur.",
                        finalCorrection:
                        """
                        <?php

                        $name = "Ada";
                        echo "Bonjour " . $name;
                        """),
                    PhpLesson(
                        "php-types",
                        "Types",
                        "Representer texte, entier, decimal et booleen en PHP.",
                        "PHP type les valeurs dynamiquement. Les bases utiles sont string, int, float et bool.",
                        "<?php\n$level = 3;\n$price = 9.99;\n$active = true;",
                        "Declare $name, $level, $price et $active, puis affiche Ada niveau 3.",
                        """
                        <?php

                        $name = "";
                        $level = 0;
                        $price = 0.0;
                        $active = false;
                        """,
                        "Les valeurs de base sont presentes et reutilisees.",
                        "Utilise une chaine, un entier, un decimal et un booleen.",
                        40,
                        3,
                        [
                            Required("Utilise $name", "$name"),
                            Required("Utilise $level", "$level"),
                            Required("Utilise $price", "$price"),
                            Required("Utilise $active", "$active"),
                            Required("Active le booleen", "true"),
                            Output("Contient Ada niveau 3", "Ada niveau 3")
                        ],
                        conceptSummary: "PHP sait manipuler plusieurs types meme sans annotation explicite.",
                        finalCorrection:
                        """
                        <?php

                        $name = "Ada";
                        $level = 3;
                        $price = 9.99;
                        $active = true;

                        echo $name . " niveau " . $level;
                        """),
                    PhpLesson(
                        "php-conditions",
                        "Conditions",
                        "Choisir un affichage selon une condition avec if et else.",
                        "if execute un bloc si une condition est vraie. else gere le cas contraire.",
                        "<?php\n$stock = 5;\nif ($stock > 0) { echo \"Disponible\"; }",
                        "Avec $stock = 0, affiche Rupture en utilisant if et else.",
                        """
                        <?php

                        $stock = 0;
                        // Ecris la condition ici
                        """,
                        "La condition gere correctement le stock vide.",
                        "Teste $stock > 0, puis affiche Rupture dans le else.",
                        45,
                        4,
                        [
                            Required("Utilise if", "if"),
                            Required("Utilise else", "else"),
                            Required("Teste le stock", "$stock > 0"),
                            Output("Contient Rupture", "Rupture")
                        ],
                        conceptSummary: "Une condition transforme une valeur booleenne en decision de programme.",
                        finalCorrection:
                        """
                        <?php

                        $stock = 0;

                        if ($stock > 0) {
                            echo "Disponible";
                        } else {
                            echo "Rupture";
                        }
                        """),
                    PhpLesson(
                        "php-loops",
                        "Boucles",
                        "Repeter une action avec une boucle PHP.",
                        "for repete un bloc un nombre connu de fois. foreach parcourt une liste.",
                        "<?php\nfor ($i = 1; $i <= 3; $i++) { echo $i; }",
                        "Utilise une boucle for pour produire Item 1, Item 2 et Item 3.",
                        """
                        <?php

                        // Boucle de 1 a 3
                        """,
                        "La boucle produit les trois elements attendus.",
                        "Utilise for, une variable $i et un echo dans la boucle.",
                        45,
                        5,
                        [
                            Required("Utilise for", "for"),
                            Required("Utilise $i", "$i"),
                            Required("Affiche dans la boucle", "echo"),
                            Output("Contient Item 1", "Item 1"),
                            Output("Contient Item 3", "Item 3")
                        ],
                        conceptSummary: "Une boucle evite de dupliquer les memes instructions.",
                        finalCorrection:
                        """
                        <?php

                        for ($i = 1; $i <= 3; $i++) {
                            echo "Item " . $i;
                        }
                        """),
                    PhpLesson(
                        "php-functions",
                        "Fonctions",
                        "Isoler une logique reutilisable dans une fonction PHP.",
                        "Une fonction porte un nom, peut recevoir des parametres et peut retourner une valeur.",
                        "<?php\nfunction formatName(string $name): string { return strtoupper($name); }",
                        "Cree formatProduct(string $name, int $price): string et retourne Product: Book - 12.",
                        """
                        <?php

                        function formatProduct(string $name, int $price): string
                        {
                            // Retourne le texte demande
                        }

                        echo formatProduct("Book", 12);
                        """,
                        "La fonction est typee, appelee et retourne le texte attendu.",
                        "Garde les parametres types et retourne une chaine concatenee.",
                        55,
                        6,
                        [
                            Required("Declare une fonction", "function formatProduct"),
                            Required("Type le nom", "string $name"),
                            Required("Type le prix", "int $price"),
                            Required("Retourne une valeur", "return"),
                            Output("Contient Product: Book - 12", "Product: Book - 12")
                        ],
                        conceptSummary: "Une fonction rend une logique nommee, testable et reutilisable.",
                        finalCorrection:
                        """
                        <?php

                        function formatProduct(string $name, int $price): string
                        {
                            return "Product: " . $name . " - " . $price;
                        }

                        echo formatProduct("Book", 12);
                        """)
                ]),
                PhpSymfonyModule(2, "PHP oriente objet", "Classes, objets, proprietes, methodes, constructeurs et encapsulation.",
                    ["Classes", "Objets", "Proprietes", "Methodes", "Constructeurs", "Encapsulation"]),
                PhpSymfonyModule(3, "Bases de Symfony", "Structure de projet, routes, controllers, responses, Twig et parametres.",
                    ["Structure d'un projet Symfony", "Routes", "Controllers", "Responses", "Templates Twig", "Parametres de route"]),
                PhpSymfonyModule(4, "Formulaires Symfony", "Creation, validation, contraintes, erreurs et traitement de soumission.",
                    ["Creation de formulaire", "Validation", "Contraintes", "Gestion des erreurs", "Traitement de la soumission"]),
                PhpSymfonyModule(5, "Doctrine avec Symfony", "Entites, repositories, migrations, relations simples et CRUD Doctrine.",
                    ["Entites", "Repositories", "Migrations", "Relations simples", "CRUD avec Doctrine"]),
                PhpSymfonyModule(6, "Architecture Symfony", "Services, injection de dependances, configuration et separation des responsabilites.",
                    ["Services", "Injection de dependances", "Configuration", "Separation controller / service", "Bonnes pratiques Symfony"]),
                PhpSymfonyModule(7, "Securite Symfony", "Authentification, utilisateurs, roles, protection des routes et autorisations.",
                    ["Authentification", "Utilisateurs", "Roles", "Protection des routes", "Autorisations simples"]),
                PhpSymfonyModule(8, "Projet pratique Symfony", "Mini-application MVC avec routes, controllers, Twig, formulaires, Doctrine, services et securite.",
                    ["Mini-application MVC", "Routes", "Controllers", "Templates Twig", "Formulaires", "Doctrine", "Services", "Securite simple"]),
                Chapter("Boss Final PHP / Symfony", "Mini-application Symfony de gestion de produits.", 9, 0,
                [
                    PhpLesson(
                        "php-symfony-boss-final-products",
                        "Boss Final PHP / Symfony: gestion de produits",
                        "Construire une mini-application Symfony de gestion de produits.",
                        "Le Boss Final assemble PHP, POO, routes, controllers, Twig, formulaires, validation, Doctrine, services et securite simple.",
                        "#[Route('/products')]\nfinal class ProductController extends AbstractController\n{\n}",
                        "Propose le code d'une mini-application Symfony qui gere des produits: liste, detail, ajout, edition, suppression, validation, service et route protegee.",
                        """
                        <?php

                        namespace App\Controller;

                        // Assemble ici les elements attendus du mini-projet Symfony.
                        """,
                        "Boss Final PHP / Symfony valide.",
                        "Ajoute les briques une par une: entite, routes, controller, formulaires, repository, service et securite.",
                        180,
                        1,
                        [
                            Required("Cree une entite Product", "class Product"),
                            Required("Utilise Doctrine ORM", "ORM\\Entity"),
                            Required("Declare des routes", "#[Route"),
                            Required("Utilise un controller", "AbstractController"),
                            Required("Affiche une liste", "index"),
                            Required("Affiche un detail", "show"),
                            Required("Ajoute un formulaire", "createForm"),
                            Required("Valide les donnees", "Assert\\"),
                            Required("Utilise un service", "ProductService"),
                            Required("Protege une route", "IsGranted")
                        ],
                        conceptSummary: "Une application Symfony propre se construit par responsabilites: entite, repository, controller, formulaire, service, templates et securite.",
                        finalCorrection:
                        """
                        <?php

                        namespace App\Controller;

                        use App\Entity\Product;
                        use App\Form\ProductType;
                        use App\Service\ProductService;
                        use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
                        use Symfony\Component\Routing\Attribute\Route;
                        use Symfony\Component\Security\Http\Attribute\IsGranted;

                        #[Route('/products')]
                        final class ProductController extends AbstractController
                        {
                            public function __construct(private ProductService $products) {}

                            #[Route('', name: 'product_index')]
                            public function index() {}

                            #[Route('/{id}', name: 'product_show')]
                            public function show(Product $product) {}

                            #[Route('/new', name: 'product_new')]
                            #[IsGranted('ROLE_USER')]
                            public function new() {}

                            #[Route('/{id}/edit', name: 'product_edit')]
                            public function edit(Product $product) {}

                            #[Route('/{id}/delete', name: 'product_delete')]
                            public function delete(Product $product) {}
                        }
                        """,
                        isBossFinal: true)
                ])
            ]
        };

    private static Chapter PhpSymfonyModule(int sortOrder, string title, string description, string[] lessonTitles) =>
        Chapter($"Module {sortOrder} - {title}", description, sortOrder, 0,
            lessonTitles.Select((lessonTitle, index) => PhpLesson(
                $"php-symfony-module-{sortOrder}-{Slugify(lessonTitle)}",
                lessonTitle,
                $"Comprendre et pratiquer: {lessonTitle}.",
                $"{lessonTitle} fait partie du socle PHP/Symfony du module. L'exercice valide les mots-cles et structures essentiels sans sortir du cadre PHP/Symfony.",
                PhpSymfonyExampleFor(lessonTitle),
                $"Complete le code pour montrer une utilisation correcte de: {lessonTitle}.",
                PhpSymfonyStarterFor(lessonTitle),
                "La notion est presente dans un code PHP/Symfony coherent.",
                "Ajoute les mots-cles attendus et garde le code centre sur Symfony.",
                45 + index * 5,
                index + 1,
                PhpSymfonyTestsFor(lessonTitle),
                conceptSummary: $"{lessonTitle} est une brique du parcours PHP/Symfony.",
                finalCorrection: PhpSymfonySolutionFor(lessonTitle))).ToList());

    private static Lesson PhpLesson(
        string slug,
        string title,
        string objective,
        string explanation,
        string exampleCode,
        string exercisePrompt,
        string starterCode,
        string successFeedback,
        string failureFeedback,
        int xpReward,
        int sortOrder,
        List<LessonTest> tests,
        string conceptSummary = "",
        string commonMistakes = "",
        string finalCorrection = "",
        bool isBossFinal = false) =>
        Lesson(
            slug,
            title,
            objective,
            explanation,
            exampleCode,
            exercisePrompt,
            starterCode,
            successFeedback,
            failureFeedback,
            xpReward,
            sortOrder,
            tests,
            conceptSummary,
            string.IsNullOrWhiteSpace(commonMistakes)
                ? "Verifie les noms PHP/Symfony demandes, les $, les namespaces, les attributs et les methodes attendues."
                : commonMistakes,
            finalCorrection,
            isBossFinal);

    private static string Slugify(string value) =>
        value.ToLowerInvariant()
            .Replace("'", "")
            .Replace("/", "-")
            .Replace(" ", "-")
            .Replace("<", "")
            .Replace(">", "")
            .Replace(":", "")
            .Replace("é", "e");

    private static string PhpSymfonyExampleFor(string lessonTitle) => lessonTitle switch
    {
        "Classes" => "<?php\nfinal class Product {}",
        "Objets" => "<?php\n$product = new Product();",
        "Proprietes" => "<?php\nprivate string $name;",
        "Methodes" => "<?php\npublic function getName(): string { return $this->name; }",
        "Constructeurs" => "<?php\npublic function __construct(private string $name) {}",
        "Encapsulation" => "<?php\nprivate int $price;\npublic function getPrice(): int { return $this->price; }",
        "Routes" or "Parametres de route" => "<?php\n#[Route('/products/{id}', name: 'product_show')]",
        "Controllers" => "<?php\nfinal class ProductController extends AbstractController {}",
        "Responses" => "<?php\nreturn new Response('OK');",
        "Templates Twig" => "{{ product.name }}",
        "Creation de formulaire" or "Traitement de la soumission" => "<?php\n$form = $this->createForm(ProductType::class, $product);",
        "Validation" or "Contraintes" => "<?php\n#[Assert\\NotBlank]\nprivate string $name;",
        "Gestion des erreurs" => "{{ form_errors(form.name) }}",
        "Entites" => "<?php\n#[ORM\\Entity]\nclass Product {}",
        "Repositories" => "<?php\nfinal class ProductRepository extends ServiceEntityRepository {}",
        "Migrations" => "<?php\nfinal class Version20260101000000 extends AbstractMigration {}",
        "Relations simples" => "<?php\n#[ORM\\ManyToOne]\nprivate ?Category $category = null;",
        "CRUD avec Doctrine" => "<?php\n$entityManager->persist($product);\n$entityManager->flush();",
        "Services" or "Separation controller / service" => "<?php\nfinal class ProductService {}",
        "Injection de dependances" => "<?php\npublic function __construct(private ProductService $service) {}",
        "Configuration" => "services:\n  App\\Service\\ProductService: ~",
        "Bonnes pratiques Symfony" => "<?php\nfinal readonly class ProductService {}",
        "Authentification" => "<?php\nclass LoginFormAuthenticator extends AbstractLoginFormAuthenticator {}",
        "Utilisateurs" => "<?php\nclass User implements UserInterface {}",
        "Roles" => "<?php\nreturn ['ROLE_USER'];",
        "Protection des routes" or "Autorisations simples" => "<?php\n#[IsGranted('ROLE_USER')]",
        _ => "<?php\n#[Route('/products')]\nfinal class ProductController extends AbstractController {}"
    };

    private static string PhpSymfonyStarterFor(string lessonTitle) =>
        $"<?php\n\n// Complete une implementation PHP/Symfony pour: {lessonTitle}\n";

    private static string PhpSymfonySolutionFor(string lessonTitle) => lessonTitle switch
    {
        "Classes" => "<?php\n\nfinal class Product\n{\n}\n",
        "Objets" => "<?php\n\n$product = new Product();\n",
        "Proprietes" => "<?php\n\nfinal class Product\n{\n    private string $name;\n    private int $price;\n}\n",
        "Methodes" => "<?php\n\nfinal class Product\n{\n    public function getName(): string\n    {\n        return $this->name;\n    }\n}\n",
        "Constructeurs" => "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name, private int $price) {}\n}\n",
        "Encapsulation" => "<?php\n\nfinal class Product\n{\n    private int $price;\n\n    public function getPrice(): int\n    {\n        return $this->price;\n    }\n}\n",
        "Routes" => "<?php\n\n#[Route('/products', name: 'product_index')]\npublic function index(): Response {}\n",
        "Controllers" => "<?php\n\nfinal class ProductController extends AbstractController\n{\n}\n",
        "Responses" => "<?php\n\nreturn new Response('OK');\n",
        "Templates Twig" => "<h1>{{ product.name }}</h1>\n",
        "Parametres de route" => "<?php\n\n#[Route('/products/{id}', name: 'product_show')]\npublic function show(int $id): Response {}\n",
        "Creation de formulaire" => "<?php\n\n$form = $this->createForm(ProductType::class, $product);\n",
        "Validation" => "<?php\n\n#[Assert\\NotBlank]\nprivate string $name;\n",
        "Contraintes" => "<?php\n\n#[Assert\\Positive]\nprivate int $price;\n",
        "Gestion des erreurs" => "{{ form_errors(form.name) }}\n",
        "Traitement de la soumission" => "<?php\n\n$form->handleRequest($request);\nif ($form->isSubmitted() && $form->isValid()) {}\n",
        "Entites" => "<?php\n\n#[ORM\\Entity]\nclass Product {}\n",
        "Repositories" => "<?php\n\nfinal class ProductRepository extends ServiceEntityRepository {}\n",
        "Migrations" => "<?php\n\nfinal class Version20260101000000 extends AbstractMigration {}\n",
        "Relations simples" => "<?php\n\n#[ORM\\ManyToOne]\nprivate ?Category $category = null;\n",
        "CRUD avec Doctrine" => "<?php\n\n$entityManager->persist($product);\n$entityManager->flush();\n$entityManager->remove($product);\n",
        "Services" => "<?php\n\nfinal class ProductService {}\n",
        "Injection de dependances" => "<?php\n\npublic function __construct(private ProductService $productService) {}\n",
        "Configuration" => "services:\n  App\\Service\\ProductService: ~\n",
        "Separation controller / service" => "<?php\n\n$product = $productService->create($data);\n",
        "Bonnes pratiques Symfony" => "<?php\n\nfinal readonly class ProductService {}\n",
        "Authentification" => "<?php\n\nclass LoginFormAuthenticator extends AbstractLoginFormAuthenticator {}\n",
        "Utilisateurs" => "<?php\n\nclass User implements UserInterface {}\n",
        "Roles" => "<?php\n\nreturn ['ROLE_USER'];\n",
        "Protection des routes" => "<?php\n\n#[IsGranted('ROLE_USER')]\npublic function account(): Response {}\n",
        "Autorisations simples" => "<?php\n\n$this->denyAccessUnlessGranted('ROLE_USER');\n",
        _ => "<?php\n\n#[Route('/products')]\nfinal class ProductController extends AbstractController {}\n"
    };

    private static List<LessonTest> PhpSymfonyTestsFor(string lessonTitle)
    {
        string[] expected = lessonTitle switch
        {
            "Classes" => ["class Product"],
            "Objets" => ["new Product"],
            "Proprietes" => ["private", "$"],
            "Methodes" => ["function"],
            "Constructeurs" => ["__construct"],
            "Encapsulation" => ["private", "public function"],
            "Structure d'un projet Symfony" => ["src", "templates"],
            "Routes" or "Parametres de route" => ["#[Route"],
            "Controllers" => ["AbstractController"],
            "Responses" => ["Response"],
            "Templates Twig" => ["{{", "}}"],
            "Creation de formulaire" or "Formulaires" => ["createForm"],
            "Validation" or "Contraintes" => ["Assert\\"],
            "Gestion des erreurs" => ["form_errors"],
            "Traitement de la soumission" => ["handleRequest", "isSubmitted", "isValid"],
            "Entites" => ["ORM\\Entity"],
            "Repositories" => ["Repository"],
            "Migrations" => ["AbstractMigration"],
            "Relations simples" => ["ORM\\ManyToOne"],
            "CRUD avec Doctrine" or "Doctrine" => ["persist", "flush"],
            "Services" => ["ProductService"],
            "Injection de dependances" => ["__construct", "ProductService"],
            "Configuration" => ["services:"],
            "Separation controller / service" => ["ProductService"],
            "Bonnes pratiques Symfony" => ["final"],
            "Authentification" => ["Authenticator"],
            "Utilisateurs" => ["UserInterface"],
            "Roles" => ["ROLE_USER"],
            "Protection des routes" => ["IsGranted"],
            "Autorisations simples" => ["denyAccessUnlessGranted"],
            "Mini-application MVC" => ["#[Route", "AbstractController"],
            "Securite simple" => ["ROLE_USER"],
            _ => ["<?php"]
        };

        return expected.Select((snippet, index) =>
        {
            var test = Required($"Contient {snippet}", snippet);
            test.SortOrder = index + 1;
            return test;
        }).ToList();
    }


    private static async Task EnsureIntermediateBossesSeededAsync(AppDbContext db)
    {
        var courses = await db.Courses
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.Tests)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.IntermediateBoss)
            .ToListAsync();

        foreach (var course in courses)
        {
            AttachIntermediateBosses(course);
        }
    }

    private static void AttachIntermediateBosses(Course course)
    {
        foreach (var chapter in course.Chapters.Where(chapter => !chapter.Title.Contains("Boss Final", StringComparison.OrdinalIgnoreCase)))
        {
            if (chapter.IntermediateBoss is not null)
            {
                continue;
            }

            chapter.IntermediateBoss = course.Language == "php-symfony" && chapter.SortOrder == 1
                ? PhpModuleOneIntermediateBoss()
                : course.Language == "sqlserver" && chapter.SortOrder == 1
                ? SqlModuleOneIntermediateBoss()
                : course.Language == "csharp" && chapter.SortOrder == 1
                    ? CSharpModuleOneIntermediateBoss()
                    : BuildIntermediateBossFromModule(course, chapter);
        }
    }

    private static IntermediateBoss PhpModuleOneIntermediateBoss() =>
        new()
        {
            Slug = "php-symfony-module-1-intermediate-boss",
            Title = "Monstre intermediaire - Fondations PHP",
            Objective = "Consolider syntaxe PHP, variables, types, conditions, boucles et fonctions.",
            Instructions = "Cree un script PHP simple qui declare des produits, utilise une fonction, parcourt une liste avec une boucle et affiche uniquement les produits en stock.",
            StarterCode =
            """
            <?php

            $products = [
                ["name" => "Book", "price" => 12, "stock" => 3],
                ["name" => "Mouse", "price" => 25, "stock" => 0],
            ];

            function formatProduct(array $product): string
            {
                // Retourne Product: Book - 12
            }

            // Parcours les produits et affiche seulement ceux en stock
            """,
            ExpectedResult = "Le script utilise une variable tableau, une fonction, une condition, une boucle et affiche Product: Book - 12.",
            XpReward = 85,
            IsRequiredToUnlockNextModule = true,
            ValidationRules =
            [
                BossSnippet("Commence par <?php", "<?php", 1),
                BossSnippet("Declare $products", "$products", 2),
                BossSnippet("Declare formatProduct", "function formatProduct", 3),
                BossSnippet("Type le parametre array", "array $product", 4),
                BossSnippet("Retourne une chaine", "return", 5),
                BossSnippet("Utilise une boucle", "foreach", 6),
                BossSnippet("Utilise une condition", "if", 7),
                BossSnippet("Teste le stock", "stock", 8),
                BossSnippet("Affiche le resultat", "echo", 9),
                BossOutput("Contient Product: Book - 12", "Product: Book - 12", 10)
            ],
            Hints =
            [
                BossHint("La fonction doit recevoir un produit et construire une chaine avec name et price.", 1),
                BossHint("La boucle foreach parcourt $products, puis un if filtre les produits dont stock est superieur a 0.", 2),
                BossHint("Dans le if, appelle formatProduct($product) et affiche son resultat avec echo.", 3)
            ],
            Solution =
            """
            <?php

            $products = [
                ["name" => "Book", "price" => 12, "stock" => 3],
                ["name" => "Mouse", "price" => 25, "stock" => 0],
            ];

            function formatProduct(array $product): string
            {
                return "Product: " . $product["name"] . " - " . $product["price"];
            }

            foreach ($products as $product) {
                if ($product["stock"] > 0) {
                    echo formatProduct($product);
                }
            }
            """
        };

    private static IntermediateBoss CSharpModuleOneIntermediateBoss() =>
        new()
        {
            Slug = "csharp-module-1-intermediate-boss",
            Title = "Monstre intermediaire - Fondations C#",
            Objective = "Prouver que tu sais assembler affichage console, variables, types et operateurs.",
            Instructions = "Cree une fiche de personnage pour Ada. Le programme doit utiliser Console.WriteLine, des variables typees, un calcul et une comparaison. Affiche exactement deux lignes: Ada - score 37 puis Pret: True.",
            StarterCode =
            """
            using System;

            string player = "Ada";
            int baseScore = 30;
            int bonus = 0;
            bool isReady = false;

            // Calcule finalScore, rends le joueur pret, puis affiche les deux lignes demandees.
            """,
            ExpectedResult = "Ada - score 37\nPret: True",
            XpReward = 80,
            IsRequiredToUnlockNextModule = true,
            ValidationRules =
            [
                BossSnippet("Utilise Console.WriteLine", "Console.WriteLine", 1),
                BossCount("Affiche deux lignes", "Console.WriteLine", 2, 2),
                BossSnippet("Declare une variable string", "string player", 3),
                BossSnippet("Utilise un entier bonus", "int bonus", 4),
                BossSnippet("Utilise un booleen", "bool isReady", 5),
                BossSnippet("Calcule le score avec un operateur", "baseScore + bonus", 6),
                BossOutput("Affiche le score final", "Ada - score 37", 7),
                BossOutput("Affiche le statut pret", "Pret: True", 8)
            ],
            Hints =
            [
                BossHint("Commence par mettre bonus a 7 et isReady a true.", 1),
                BossHint("Cree une variable finalScore avec baseScore + bonus avant les affichages.", 2),
                BossHint("La premiere ligne concatene player et finalScore. La deuxieme affiche le booleen isReady.", 3)
            ],
            Solution =
            """
            using System;

            string player = "Ada";
            int baseScore = 30;
            int bonus = 7;
            bool isReady = true;
            int finalScore = baseScore + bonus;

            Console.WriteLine(player + " - score " + finalScore);
            Console.WriteLine("Pret: " + isReady);
            """
        };

    private static IntermediateBoss SqlModuleOneIntermediateBoss() =>
        new()
        {
            Slug = "sql-module-1-intermediate-boss",
            Title = "Monstre intermediaire - Fondations SQL",
            Objective = "Prouver que tu sais lire une table, choisir les colonnes utiles et filtrer avec WHERE.",
            Instructions = "Ecris une requete qui affiche Name, Price et Stock des produits actifs, en stock, coutant moins de 40. La requete doit utiliser SELECT, FROM et WHERE, sans SELECT *.",
            StarterCode =
            """
            SELECT Name, Price, Stock
            FROM Products
            -- Ajoute le filtre ici
            """,
            ExpectedResult = "Trois lignes: C# Basics, SQL Server Guide et RPG Dice Set avec les colonnes Name, Price, Stock.",
            XpReward = 80,
            IsRequiredToUnlockNextModule = true,
            ValidationRules =
            [
                BossSnippet("Utilise SELECT", "SELECT", 1),
                BossSnippet("Lit la table Products", "FROM Products", 2),
                BossSnippet("Utilise WHERE", "WHERE", 3),
                BossSnippet("Filtre les produits actifs", "IsActive = 1", 4),
                BossSnippet("Filtre le prix", "Price < 40", 5),
                BossSnippet("Filtre le stock", "Stock > 0", 6),
                BossSqlColumns("Retourne Name, Price, Stock", "Name,Price,Stock", 7),
                BossSqlRows("Retourne trois produits", 3, 8),
                BossOutput("Contient C# Basics", "C# Basics", 9),
                BossOutput("Contient SQL Server Guide", "SQL Server Guide", 10),
                BossOutput("Contient RPG Dice Set", "RPG Dice Set", 11),
                BossForbidden("N'utilise pas SELECT *", "SELECT *", 12)
            ],
            Hints =
            [
                BossHint("La requete part de Products et selectionne seulement Name, Price et Stock.", 1),
                BossHint("Le WHERE doit combiner trois conditions avec AND.", 2),
                BossHint("Les conditions attendues portent sur IsActive, Price et Stock.", 3)
            ],
            Solution =
            """
            SELECT Name, Price, Stock
            FROM Products
            WHERE IsActive = 1 AND Price < 40 AND Stock > 0;
            """
        };

    private static IntermediateBoss BuildIntermediateBossFromModule(Course course, Chapter chapter)
    {
        var lesson = chapter.Lessons
            .Where(lesson => !lesson.IsBossFinal)
            .OrderByDescending(lesson => lesson.SortOrder)
            .First();

        return new IntermediateBoss
        {
            Slug = $"{course.Slug}-module-{chapter.SortOrder}-intermediate-boss",
            Title = $"Monstre intermediaire - {chapter.Title.Replace("Module " + chapter.SortOrder + " - ", "")}",
            Objective = $"Consolider les notions du {chapter.Title.ToLowerInvariant()}.",
            Instructions = $"{lesson.ExercisePrompt} Reussis cet exercice de consolidation pour deverrouiller le module suivant.",
            StarterCode = lesson.StarterCode,
            ExpectedResult = lesson.SuccessFeedback,
            XpReward = Math.Max(70, lesson.XpReward + 20),
            IsRequiredToUnlockNextModule = true,
            ValidationRules = lesson.Tests.Select(ToBossRule).ToList(),
            Hints =
            [
                BossHint(lesson.FailureFeedback, 1),
                BossHint(lesson.ConceptSummary, 2),
                BossHint("Relis les criteres de validation et corrige un point a la fois.", 3)
            ],
            Solution = lesson.FinalCorrection
        };
    }

    private static IntermediateBossValidationRule ToBossRule(LessonTest test) =>
        new()
        {
            Name = test.Name,
            TestType = test.TestType,
            ExpectedOutput = test.ExpectedOutput,
            RequiredSnippet = test.RequiredSnippet,
            HiddenCode = test.HiddenCode,
            MinCount = test.MinCount,
            ExpectedColumns = test.ExpectedColumns,
            ExpectedRowCount = test.ExpectedRowCount,
            SortOrder = test.SortOrder
        };

    private static Lesson Lesson(
        string slug,
        string title,
        string objective,
        string explanation,
        string exampleCode,
        string exercisePrompt,
        string starterCode,
        string successFeedback,
        string failureFeedback,
        int xpReward,
        int sortOrder,
        List<LessonTest> tests,
        string conceptSummary = "",
        string commonMistakes = "",
        string finalCorrection = "",
        bool isBossFinal = false) =>
        new()
        {
            Slug = slug,
            Title = title,
            Objective = objective,
            ConceptSummary = conceptSummary,
            CommonMistakes = string.IsNullOrWhiteSpace(commonMistakes)
                ? "Verifier le texte exact attendu, les points-virgules et les noms demandes par l'exercice."
                : commonMistakes,
            Explanation = explanation,
            ExampleCode = exampleCode,
            ExercisePrompt = exercisePrompt,
            StarterCode = starterCode,
            SuccessFeedback = successFeedback,
            FailureFeedback = failureFeedback,
            FinalCorrection = finalCorrection,
            XpReward = xpReward,
            SortOrder = sortOrder,
            IsBossFinal = isBossFinal,
            IsBossPrerequisite = !isBossFinal,
            Tests = tests
        };

    private static LessonTest Output(string name, string expectedOutput) =>
        new() { Name = name, TestType = LessonTestType.ExpectedOutput, ExpectedOutput = expectedOutput };

    private static LessonTest Snippet(string name, string requiredSnippet) =>
        new() { Name = name, TestType = LessonTestType.RequiredSnippet, RequiredSnippet = requiredSnippet };

    private static LessonTest Required(string name, string requiredSnippet) =>
        Snippet(name, requiredSnippet);

    private static LessonTest Count(string name, string requiredSnippet, int minCount) =>
        new() { Name = name, TestType = LessonTestType.MinSnippetCount, RequiredSnippet = requiredSnippet, MinCount = minCount };

    private static LessonTest SqlColumns(string name, string expectedColumns) =>
        new() { Name = name, TestType = LessonTestType.SqlExpectedColumns, ExpectedColumns = expectedColumns };

    private static LessonTest SqlRows(string name, int expectedRowCount) =>
        new() { Name = name, TestType = LessonTestType.SqlExpectedRowCount, ExpectedRowCount = expectedRowCount };

    private static LessonTest Forbidden(string name, string forbiddenSnippet) =>
        new() { Name = name, TestType = LessonTestType.SqlForbiddenSnippet, RequiredSnippet = forbiddenSnippet };

    private static IntermediateBossValidationRule BossOutput(string name, string expectedOutput, int sortOrder) =>
        new() { Name = name, TestType = LessonTestType.ExpectedOutput, ExpectedOutput = expectedOutput, SortOrder = sortOrder };

    private static IntermediateBossValidationRule BossSnippet(string name, string requiredSnippet, int sortOrder) =>
        new() { Name = name, TestType = LessonTestType.RequiredSnippet, RequiredSnippet = requiredSnippet, SortOrder = sortOrder };

    private static IntermediateBossValidationRule BossCount(string name, string requiredSnippet, int minCount, int sortOrder) =>
        new() { Name = name, TestType = LessonTestType.MinSnippetCount, RequiredSnippet = requiredSnippet, MinCount = minCount, SortOrder = sortOrder };

    private static IntermediateBossValidationRule BossSqlColumns(string name, string expectedColumns, int sortOrder) =>
        new() { Name = name, TestType = LessonTestType.SqlExpectedColumns, ExpectedColumns = expectedColumns, SortOrder = sortOrder };

    private static IntermediateBossValidationRule BossSqlRows(string name, int expectedRowCount, int sortOrder) =>
        new() { Name = name, TestType = LessonTestType.SqlExpectedRowCount, ExpectedRowCount = expectedRowCount, SortOrder = sortOrder };

    private static IntermediateBossValidationRule BossForbidden(string name, string forbiddenSnippet, int sortOrder) =>
        new() { Name = name, TestType = LessonTestType.SqlForbiddenSnippet, RequiredSnippet = forbiddenSnippet, SortOrder = sortOrder };

    private static IntermediateBossHint BossHint(string content, int sortOrder) =>
        new() { Content = content, SortOrder = sortOrder };
}
