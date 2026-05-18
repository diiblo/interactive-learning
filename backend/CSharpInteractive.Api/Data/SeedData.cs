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
                            "Affiche tous les produits actifs avec leur categorie et leur prix, du plus cher au moins cher. Ne limite pas la requete a quelques noms: Mechanical Keyboard et SQL Server Guide doivent apparaitre parmi les quatre lignes.",
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
                                SqlRows("Retourne les quatre produits actifs", 4),
                                Output("Mechanical Keyboard apparait dans les resultats", "Mechanical Keyboard"),
                                Output("SQL Server Guide apparait dans les resultats", "SQL Server Guide")
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
                            "Ajoute dans Products le produit Id 6, Name N'Learning Mug', CategoryId 1, Price 14.90, Stock 12, IsActive 1, puis affiche Name, Price et Stock de la ligne ajoutee avec WHERE Id = 6.",
                            """
                            INSERT INTO Products (Id, Name, CategoryId, Price, Stock, IsActive)
                            VALUES (6, N'Learning Mug', 1, 14.90, 12, 1);

                            SELECT Name, Price, Stock
                            FROM Products
                            WHERE Id = 6;
                            """,
                            "Le nouveau produit est insere et verifie.",
                            "Utilise INSERT INTO Products avec les colonnes Id, Name, CategoryId, Price, Stock, IsActive et les valeurs 6, N'Learning Mug', 1, 14.90, 12, 1, puis un SELECT de verification sur Id = 6.",
                            45,
                            1,
                            [
                                Required("Utilise INSERT", "INSERT INTO Products"),
                                Required("Ajoute Learning Mug", "Learning Mug"),
                                Required("Utilise l'Id 6", "6"),
                                Required("Utilise le prix 14.90", "14.90"),
                                Required("Utilise le stock 12", "12"),
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
                            "Cree la table Suppliers avec Id comme cle primaire et Name nvarchar(80) NOT NULL. Insere le fournisseur Id 1, Name N'Northwind Supply', puis affiche Id et Name avec WHERE Id = 1.",
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
                            "Utilise PRIMARY KEY sur Id, insere (1, N'Northwind Supply'), puis verifie avec SELECT Id, Name WHERE Id = 1.",
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
                            "Cree Suppliers et SupplierProducts. Insere Suppliers (1, N'Northwind Supply'), relie SupplierId 1 au ProductId 2 dans SupplierProducts, puis affiche SupplierId et ProductId.",
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
                            "Ajoute deux FOREIGN KEY: SupplierId vers Suppliers(Id), ProductId vers Products(Id). Insere le lien (SupplierId 1, ProductId 2).",
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
                            "Cree Warehouses avec Id PRIMARY KEY, Code nvarchar(10) NOT NULL UNIQUE et Capacity int NOT NULL CHECK (Capacity > 0). Insere (1, N'WH-A', 120), puis affiche Code et Capacity avec WHERE Code = N'WH-A'.",
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
                            "Utilise NOT NULL, UNIQUE et CHECK (Capacity > 0), puis insere WH-A avec Capacity 120.",
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
                            "Cree Suppliers et SupplierProducts. Insere Suppliers (1, N'Northwind Supply'), relie SupplierId 1 au ProductId 4, puis affiche SupplierName et ProductName avec des JOIN.",
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
                            "Utilise SupplierProducts avec deux FOREIGN KEY, insere le lien (1, 4), puis fais deux JOIN de lecture vers Suppliers et Products.",
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
                            "Cree Students, Courses et StudentCourses. Insere Students (1, N'Ada'), Courses (1, N'SQL Server'), inscris StudentId 1 au CourseId 1, puis affiche StudentName et CourseTitle.",
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
                            "Combine trois tables, cles primaires, deux cles etrangeres, les insertions Ada / SQL Server et une requete JOIN finale.",
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
                            "Cree Customers, Orders et OrderItems dans cet ordre. Insere le client Customers (1, N'Ada Lovelace', N'ada@example.com'), puis affiche Name et Email avec WHERE Id = 1.",
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
                            "Respecte l'ordre Customers, Orders, OrderItems, insere Ada Lovelace avec ada@example.com, puis verifie Customers.",
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
                            "Cree les tables, insere Customers Ada Lovelace et Alan Turing, Orders Id 1 pour Ada et Id 2 pour Alan, puis trois OrderItems. Termine avec COUNT(*) AS LineCount depuis OrderItems.",
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
                            "Insere dans l'ordre Customers, Orders, OrderItems avec les deux clients Ada/Alan et trois lignes de commande, puis compte OrderItems.",
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
                            "Cree et remplis le mini-schema avec Ada Lovelace, Alan Turing, deux commandes et trois lignes. Affiche OrderId, CustomerName et ProductName avec des JOIN vers Customers, OrderItems et Products.",
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
                            "Utilise les jointures Orders, Customers, OrderItems et Products, et retourne trois lignes avec les alias OrderId, CustomerName, ProductName.",
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
                            "Cree et remplis le mini-schema avec Ada Lovelace et Alan Turing. Ajoute l'index IX_Orders_CustomerId sur Orders(CustomerId), puis affiche OrderId et CustomerName des commandes d'Ada Lovelace.",
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
                            "Cree IX_Orders_CustomerId sur Orders(CustomerId), puis filtre WHERE c.Name = N'Ada Lovelace'.",
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
                            "Cree et remplis le mini-schema avec deux commandes: Ada totalise 58.50 et Alan 89.99. Affiche OrderId, CustomerName et OrderTotal avec SUM(oi.Quantity * oi.UnitPrice), GROUP BY o.Id, c.Name et ORDER BY OrderTotal DESC.",
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
                            "Combine Customers, Orders, OrderItems, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal, GROUP BY o.Id, c.Name et ORDER BY OrderTotal DESC.",
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
            var reactCourse = ReactCourse();
            var reactNativeCourse = ReactNativeCourse();
            var tailwindCourse = TailwindCssCourse();
            var cssCourse = CssCourse();
            var javascriptCourse = JavaScriptCourse();

            AttachIntermediateBosses(course);
            AttachIntermediateBosses(sqlCourse);
            AttachIntermediateBosses(phpCourse);
            AttachIntermediateBosses(reactCourse);
            AttachIntermediateBosses(reactNativeCourse);
            AttachIntermediateBosses(tailwindCourse);
            AttachIntermediateBosses(cssCourse);
            AttachIntermediateBosses(javascriptCourse);

            db.Courses.AddRange(course, sqlCourse, phpCourse, reactCourse, reactNativeCourse, tailwindCourse, cssCourse, javascriptCourse);
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
            await EnsureStaticCourseSeededAsync(db, ReactCourse());
            await EnsureStaticCourseSeededAsync(db, ReactNativeCourse());
            await EnsureStaticCourseSeededAsync(db, TailwindCssCourse());
            await EnsureStaticCourseSeededAsync(db, CssCourse());
            await EnsureStaticCourseSeededAsync(db, JavaScriptCourse());
        }

        await RemoveIntermediateCheckpointLessonsAsync(db);
        await RefreshExistingLessonGuidanceAsync(db);
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
            ("php-symfony", "php-types", "Types PHP", "Choisir les types PHP utiles aux donnees produit."),
            ("php-symfony", "php-strings", "Chaines PHP", "Composer et afficher du texte en PHP."),
            ("php-symfony", "php-conditions", "Conditions PHP", "Ecrire des decisions avec if et else."),
            ("php-symfony", "php-loops", "Boucles PHP", "Parcourir des donnees avec for et foreach."),
            ("php-symfony", "php-functions", "Fonctions PHP", "Creer des fonctions PHP."),
            ("php-symfony", "php-arrays", "Tableaux PHP", "Utiliser les tableaux PHP."),
            ("php-symfony", "php-array-functions", "Fonctions de tableaux PHP", "Transformer, filtrer et reduire des tableaux avec les fonctions natives."),
            ("php-symfony", "php-modern-types", "Types modernes PHP", "Utiliser strict types, nullable, union types et arguments nommes."),
            ("php-symfony", "php-functional-arrays", "Tableaux fonctionnels PHP", "Transformer, filtrer et reduire des tableaux."),
            ("php-symfony", "php-date-time", "Dates PHP", "Manipuler DateTimeImmutable."),
            ("php-symfony", "php-enums", "Enums PHP", "Modeliser des etats metier avec enum."),
            ("php-symfony", "php-readonly", "Readonly PHP", "Utiliser des proprietes immutables."),
            ("php-symfony", "php-oop", "POO PHP", "Structurer le code avec classes et objets."),
            ("php-symfony", "php-interfaces", "Interfaces PHP", "Definir des contrats avec interfaces."),
            ("php-symfony", "php-inheritance", "Heritage PHP", "Utiliser classes abstraites, extends et polymorphisme."),
            ("php-symfony", "php-composition", "Composition PHP", "Assembler des objets par dependances."),
            ("php-symfony", "php-exceptions", "Exceptions PHP", "Signaler une erreur metier avec une exception."),
            ("php-symfony", "php-namespaces", "Namespaces PHP", "Organiser les classes PHP avec des espaces de noms."),
            ("php-symfony", "php-composer", "Composer PHP", "Declarer dependances et scripts Composer."),
            ("php-symfony", "php-autoload", "Autoload PHP", "Configurer l'autoload PSR-4."),
            ("php-symfony", "php-standards", "Standards PHP", "Respecter les conventions PSR."),
            ("php-symfony", "php-project-structure", "Structure projet PHP", "Organiser src, public et tests."),
            ("php-symfony", "php-http", "HTTP PHP natif", "Lire GET, POST et SERVER."),
            ("php-symfony", "php-sessions", "Sessions PHP", "Utiliser session_start et SESSION."),
            ("php-symfony", "php-cookies", "Cookies PHP", "Creer un cookie avec setcookie."),
            ("php-symfony", "php-files", "Fichiers PHP", "Lire et ecrire des fichiers de donnees simples."),
            ("php-symfony", "php-json", "JSON PHP", "Retourner du JSON avec json_encode."),
            ("php-symfony", "php-pdo", "PDO PHP", "Acceder aux donnees avec PDO et requetes preparees."),
            ("php-symfony", "symfony-framework", "Framework Symfony", "Comprendre ce qu'un framework apporte par rapport a PHP natif."),
            ("php-symfony", "symfony-project-structure", "Structure Symfony", "Identifier les dossiers et responsabilites d'un projet Symfony."),
            ("php-symfony", "symfony-routing", "Routing Symfony", "Declarer des routes."),
            ("php-symfony", "symfony-controller", "Controleurs Symfony", "Construire des controleurs."),
            ("php-symfony", "symfony-response", "Responses Symfony", "Retourner des reponses HTTP avec Symfony."),
            ("php-symfony", "symfony-request", "Request Symfony", "Lire les donnees d'une requete HTTP avec l'objet Request."),
            ("php-symfony", "symfony-twig", "Twig Symfony", "Afficher les donnees dans des templates Twig."),
            ("php-symfony", "symfony-service", "Services Symfony", "Extraire la logique dans des services."),
            ("php-symfony", "symfony-dependency-injection", "Injection de dependances Symfony", "Recevoir les collaborateurs par constructeur dans Symfony."),
            ("php-symfony", "symfony-console", "Console Symfony", "Utiliser bin/console pour piloter le projet."),
            ("php-symfony", "symfony-doctrine", "Doctrine Symfony", "Modeliser et persister les entites."),
            ("php-symfony", "symfony-repository", "Repositories Symfony", "Regrouper les requetes Doctrine dans des repositories."),
            ("php-symfony", "symfony-form", "Formulaires Symfony", "Gerer des formulaires."),
            ("php-symfony", "symfony-validation", "Validation Symfony", "Valider les donnees metier."),
            ("php-symfony", "symfony-access-control", "Controle d'acces Symfony", "Proteger des actions avec des attributs d'autorisation."),
            ("php-symfony", "symfony-security", "Securite Symfony", "Proteger les routes et raisonner sur roles, utilisateurs et CSRF."),
            ("php-symfony", "symfony-api", "API Symfony", "Construire des endpoints JSON avec Symfony."),
            ("php-symfony", "symfony-crud", "CRUD Symfony", "Assembler les operations liste, detail, creation, edition et suppression."),
            ("php-symfony", "symfony-project-architecture", "Architecture projet Symfony", "Assembler controller, service, repository, entite, Twig et API dans un projet coherent."),
            ("react", "react-jsx", "JSX React", "Ecrire une interface avec JSX."),
            ("react", "react-components", "Composants React", "Decouper l'interface en composants."),
            ("react", "react-props", "Props React", "Transmettre des donnees aux composants."),
            ("react", "react-typescript-props", "Props TypeScript React", "Typer les props avec type ou interface."),
            ("react", "react-children", "Children React", "Composer des composants avec children."),
            ("react", "react-composition", "Composition React", "Assembler des composants reutilisables."),
            ("react", "react-state", "State React", "Gerer un etat local."),
            ("react", "react-events", "Evenements React", "Reagir aux actions utilisateur."),
            ("react", "react-controlled-inputs", "Inputs controles React", "Relier value et onChange a un state."),
            ("react", "react-conditional-rendering", "Rendu conditionnel React", "Afficher selon l'etat."),
            ("react", "react-lists", "Listes React", "Afficher des collections avec map et key."),
            ("react", "react-keys", "Keys React", "Donner des cles stables aux listes."),
            ("react", "react-forms", "Formulaires React", "Construire des formulaires controles."),
            ("react", "react-form-validation", "Validation de formulaires React", "Valider les saisies et afficher les erreurs."),
            ("react", "react-effects", "Effects React", "Synchroniser avec une API ou un effet."),
            ("react", "react-custom-hooks", "Hooks personnalises React", "Extraire une logique reusable."),
            ("react", "react-memoization", "Memoisation React", "Eviter les recalculs avec useMemo et useCallback."),
            ("react", "react-reducer", "Reducer React", "Structurer un etat complexe avec useReducer."),
            ("react", "react-context", "Context React", "Partager un etat transversal."),
            ("react", "react-routing", "Routing React simule", "Representer routes et parametres sans routeur reel."),
            ("react", "react-router", "Routing React", "Naviguer entre pages et parametres."),
            ("react", "react-api", "API React", "Charger des donnees distantes."),
            ("react", "react-loading-error", "Loading et erreurs React", "Gerer chargement et erreurs API."),
            ("react", "react-accessibility", "Accessibilite React", "Relier labels, aria et controles."),
            ("react", "react-performance", "Performance React", "Limiter les recalculs et rerenders."),
            ("react", "react-testing", "Tests React", "Tester composants et interactions."),
            ("react", "react-project", "Projet React", "Assembler un Product Manager."),
            ("react-native", "rn-core-components", "Composants RN", "Utiliser View, Text et composants natifs."),
            ("react-native", "rn-view-text", "View et Text RN", "Composer les ecrans avec View et Text."),
            ("react-native", "rn-styling", "Styles RN", "Styler avec StyleSheet."),
            ("react-native", "rn-stylesheet", "StyleSheet RN", "Centraliser les styles avec StyleSheet.create."),
            ("react-native", "rn-flexbox", "Flexbox RN", "Organiser les ecrans mobiles."),
            ("react-native", "rn-props-state", "Props et state RN", "Gerer donnees et interactions."),
            ("react-native", "rn-events", "Evenements RN", "Reagir aux interactions tactiles."),
            ("react-native", "rn-lists", "Listes RN", "Afficher des listes avec FlatList."),
            ("react-native", "rn-flatlist", "FlatList RN", "Configurer data, renderItem et keyExtractor."),
            ("react-native", "rn-forms", "Formulaires RN", "Saisir des donnees avec TextInput."),
            ("react-native", "rn-inputs", "Inputs RN", "Controler TextInput avec value et onChangeText."),
            ("react-native", "rn-navigation", "Navigation RN", "Naviguer entre ecrans."),
            ("react-native", "rn-navigation-params", "Params navigation RN", "Lire route.params dans les ecrans detail."),
            ("react-native", "rn-api", "API RN", "Charger des donnees distantes."),
            ("react-native", "rn-loading-error", "Loading et erreurs RN", "Gerer ActivityIndicator et messages d'erreur."),
            ("react-native", "rn-storage", "Stockage RN", "Persist local avec AsyncStorage."),
            ("react-native", "rn-platform", "Plateforme RN", "Adapter l'interface mobile."),
            ("react-native", "rn-safe-area", "Safe area RN", "Respecter les zones sures mobiles."),
            ("react-native", "rn-permissions", "Permissions RN", "Representer une demande de permission."),
            ("react-native", "rn-project", "Projet RN", "Assembler une Product App mobile."),
            ("tailwindcss", "tailwind-utility-first", "Utility-first Tailwind", "Composer une interface avec des classes utilitaires."),
            ("tailwindcss", "tailwind-layout", "Layout Tailwind", "Structurer une page avec container, position et overflow."),
            ("tailwindcss", "tailwind-spacing", "Espacement Tailwind", "Gerer padding, margin et gaps."),
            ("tailwindcss", "tailwind-typography", "Typographie Tailwind", "Styliser textes, titres et poids."),
            ("tailwindcss", "tailwind-colors", "Couleurs Tailwind", "Utiliser couleurs de fond et de texte."),
            ("tailwindcss", "tailwind-sizing", "Dimensions Tailwind", "Controler largeur et hauteur avec w-* et h-*."),
            ("tailwindcss", "tailwind-borders", "Bordures Tailwind", "Utiliser border, radius et couleurs de bordure."),
            ("tailwindcss", "tailwind-shadows", "Ombres Tailwind", "Donner de la profondeur avec shadow-*."),
            ("tailwindcss", "tailwind-flexbox", "Flexbox Tailwind", "Aligner et distribuer les elements avec flex."),
            ("tailwindcss", "tailwind-grid", "Grid Tailwind", "Construire des grilles responsives."),
            ("tailwindcss", "tailwind-flex-grid", "Flex et grid Tailwind", "Organiser les zones avec flexbox et CSS grid."),
            ("tailwindcss", "tailwind-responsive", "Responsive Tailwind", "Adapter mobile, tablette et desktop avec les prefixes."),
            ("tailwindcss", "tailwind-states", "Etats Tailwind", "Styliser hover, focus, active et disabled."),
            ("tailwindcss", "tailwind-transitions", "Transitions Tailwind", "Animer les changements avec transition et duration."),
            ("tailwindcss", "tailwind-dark-mode", "Dark mode Tailwind", "Preparer des variantes dark."),
            ("tailwindcss", "tailwind-components", "Composants Tailwind", "Construire boutons, cards, navbars et tableaux."),
            ("tailwindcss", "tailwind-forms", "Formulaires Tailwind", "Styliser inputs, labels et feedbacks."),
            ("tailwindcss", "tailwind-dashboard", "Dashboard Tailwind", "Assembler un Product Dashboard responsive."),
            ("css", "css-selectors", "Selecteurs CSS", "Cibler les elements avec des selecteurs."),
            ("css", "css-colors", "Couleurs CSS", "Appliquer couleurs de texte et de fond."),
            ("css", "css-typography", "Typographie CSS", "Regler tailles, poids et lisibilite."),
            ("css", "css-box-model", "Box model CSS", "Maitriser margin, padding, border et width."),
            ("css", "css-layout", "Layout CSS", "Organiser les blocs avec display, flex, grid et position."),
            ("css", "css-responsive", "Responsive CSS", "Adapter l'interface avec media queries."),
            ("css", "css-components", "Composants CSS", "Styliser boutons, cards, navbars et formulaires."),
            ("css", "css-flexbox", "Flexbox CSS", "Aligner et distribuer les elements avec flexbox."),
            ("css", "css-grid", "Grid CSS", "Construire des grilles responsives."),
            ("css", "css-positioning", "Positionnement CSS", "Positionner des elements dans une interface."),
            ("css", "css-media-queries", "Media queries CSS", "Adapter les styles selon la largeur."),
            ("css", "css-forms", "Formulaires CSS", "Styliser champs et actions de formulaire."),
            ("css", "css-project", "Projet CSS", "Assembler une page produit responsive."),
            ("javascript", "js-foundations", "Fondations JavaScript", "Manipuler variables, types, conditions et boucles."),
            ("javascript", "js-functions", "Fonctions JavaScript", "Ecrire fonctions et fonctions flechees."),
            ("javascript", "js-arrays-objects", "Tableaux et objets JS", "Transformer collections et objets."),
            ("javascript", "js-modern", "JavaScript moderne", "Utiliser template literals, destructuring, spread/rest et modules."),
            ("javascript", "js-dom", "DOM JavaScript", "Lire, creer et modifier des elements DOM."),
            ("javascript", "js-events", "Evenements JavaScript", "Reagir aux interactions utilisateur."),
            ("javascript", "js-async", "Asynchrone JS", "Utiliser promises, async/await et fetch."),
            ("javascript", "js-storage", "Stockage web", "Persister des donnees avec localStorage."),
            ("javascript", "js-variables", "Variables JS", "Declarer et reutiliser des valeurs."),
            ("javascript", "js-types", "Types JS", "Manipuler string, number, boolean et tableaux."),
            ("javascript", "js-conditions", "Conditions JS", "Controler le flux avec if/else."),
            ("javascript", "js-loops", "Boucles JS", "Parcourir des collections."),
            ("javascript", "js-arrays", "Arrays JS", "Manipuler listes et methodes de tableau."),
            ("javascript", "js-objects", "Objects JS", "Modeliser des donnees produit."),
            ("javascript", "js-modern-syntax", "Syntaxe moderne JS", "Utiliser template literals, destructuring, spread/rest et modules."),
            ("javascript", "js-dom-selection", "Selection DOM", "Selectionner les elements avec querySelector."),
            ("javascript", "js-dom-manipulation", "Manipulation DOM", "Creer, modifier et inserer des elements."),
            ("javascript", "js-forms", "Formulaires JS", "Traiter submit et donnees utilisateur."),
            ("javascript", "js-local-storage", "localStorage JS", "Persister et restaurer des donnees locales."),
            ("javascript", "js-fetch", "Fetch JS", "Charger des donnees avec fetch."),
            ("javascript", "js-project", "Projet JS", "Assembler une Product List interactive.")
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
        foreach (var plan in PhpSymfonyLessonPlans())
        {
            lessonPlans[plan.Key] = plan.Value;
        }
        foreach (var plan in PhpSupplementalLessonPlans())
        {
            lessonPlans[plan.Key] = plan.Value;
        }
        foreach (var plan in StaticSnippetLessonPlans())
        {
            lessonPlans[plan.Key] = plan.Value;
        }

        foreach (var lesson in lessons)
        {
            if (lessonPlans.TryGetValue(lesson.Slug, out var plan))
            {
                if (lesson.Chapter?.Course?.Language == "php-symfony")
                {
                    db.LessonHints.RemoveRange(lesson.Hints);
                    lesson.Hints.Clear();
                }

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
        ["php-arrays"] = new LessonPlan(
            [
                ("php-arrays", 2),
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "Cree le tableau avec les trois valeurs donnees.",
                "Lis le premier element avec l'index 0.",
                "Utilise count pour compter les elements."
            ]),
        ["php-loops"] = new LessonPlan(
            [
                ("php-arrays", 1),
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "Utilise un for avec $i.",
                "Affiche dans la boucle.",
                "Verifie les trois lignes."
            ]),
        ["php-while"] = new LessonPlan(
            [
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "La condition doit tester $stock.",
                "Diminue $stock dans le bloc.",
                "Verifie que la boucle peut s'arreter."
            ]),
        ["php-foreach"] = new LessonPlan(
            [
                ("php-arrays", 2),
                ("php-variables", 1),
                ("php-syntax", 1)
            ],
            [
                "Parcours $products avec foreach.",
                "Utilise une variable $product pour chaque element.",
                "Affiche la valeur courante dans la boucle."
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

    private static Dictionary<string, LessonPlan> PhpSymfonyLessonPlans() => new(StringComparer.OrdinalIgnoreCase)
    {
        ["php-syntax"] = PhpPlan("php-syntax", new[] { ("php-syntax", 2), ("php-strings", 1) }),
        ["php-variables"] = PhpPlan("php-variables", new[] { ("php-variables", 2), ("php-strings", 1) }),
        ["php-types"] = PhpPlan("php-types", new[] { ("php-types", 2), ("php-variables", 1) }),
        ["php-conditions"] = PhpPlan("php-conditions", new[] { ("php-conditions", 2), ("php-types", 1) }),
        ["php-arrays"] = PhpPlan("php-arrays", new[] { ("php-arrays", 2), ("php-loops", 1) }),
        ["php-associative-arrays"] = PhpPlan("php-associative-arrays", new[] { ("php-arrays", 2), ("php-types", 1) }),
        ["php-for"] = PhpPlan("php-for", new[] { ("php-loops", 2), ("php-variables", 1) }),
        ["php-foreach"] = PhpPlan("php-foreach", new[] { ("php-loops", 2), ("php-arrays", 1) }),
        ["php-functions"] = PhpPlan("php-functions", new[] { ("php-functions", 2), ("php-types", 1), ("php-strings", 1) }),

        ["php-oop-namespace"] = PhpPlan("php-oop-namespace", new[] { ("php-oop", 2), ("php-syntax", 1) }),
        ["php-oop-class"] = PhpPlan("php-oop-class", new[] { ("php-oop", 2), ("php-syntax", 1) }),
        ["php-oop-properties"] = PhpPlan("php-oop-properties", new[] { ("php-oop", 2), ("php-types", 1) }),
        ["php-oop-constructor"] = PhpPlan("php-oop-constructor", new[] { ("php-oop", 2), ("php-types", 1) }),
        ["php-oop-getters"] = PhpPlan("php-oop-getters", new[] { ("php-oop", 2), ("php-functions", 1) }),
        ["php-oop-business-method"] = PhpPlan("php-oop-business-method", new[] { ("php-oop", 2), ("php-conditions", 1) }),
        ["php-oop-exception"] = PhpPlan("php-oop-exception", new[] { ("php-exceptions", 2), ("php-oop", 1), ("php-conditions", 1) }),

        ["symfony-project-structure"] = PhpPlan("symfony-project-structure", new[] { ("symfony-project-structure", 2), ("symfony-controller", 1) }),
        ["symfony-controller"] = PhpPlan("symfony-controller", new[] { ("symfony-controller", 2), ("symfony-routing", 1), ("symfony-response", 1) }),
        ["symfony-route-index"] = PhpPlan("symfony-route-index", new[] { ("symfony-routing", 2), ("symfony-controller", 1), ("symfony-response", 1) }),
        ["symfony-response"] = PhpPlan("symfony-response", new[] { ("symfony-response", 2), ("symfony-controller", 1) }),
        ["symfony-json-response"] = PhpPlan("symfony-json-response", new[] { ("symfony-response", 2), ("symfony-controller", 1) }),
        ["symfony-route-parameter"] = PhpPlan("symfony-route-parameter", new[] { ("symfony-routing", 2), ("symfony-controller", 1), ("symfony-response", 1) }),

        ["symfony-render-template"] = PhpPlan("symfony-render-template", new[] { ("symfony-twig", 2), ("symfony-controller", 1), ("symfony-response", 1) }),
        ["symfony-twig-variable"] = PhpPlan("symfony-twig-variable", new[] { ("symfony-twig", 2) }),
        ["symfony-twig-loop"] = PhpPlan("symfony-twig-loop", new[] { ("symfony-twig", 2), ("php-loops", 1) }),
        ["symfony-twig-condition"] = PhpPlan("symfony-twig-condition", new[] { ("symfony-twig", 2), ("php-conditions", 1) }),
        ["symfony-twig-path"] = PhpPlan("symfony-twig-path", new[] { ("symfony-twig", 2), ("symfony-routing", 1) }),

        ["symfony-form-type"] = PhpPlan("symfony-form-type", new[] { ("symfony-form", 2), ("php-oop", 1) }),
        ["symfony-form-fields"] = PhpPlan("symfony-form-fields", new[] { ("symfony-form", 2), ("php-oop", 1) }),
        ["symfony-create-form"] = PhpPlan("symfony-create-form", new[] { ("symfony-form", 2), ("symfony-controller", 1) }),
        ["symfony-handle-request"] = PhpPlan("symfony-handle-request", new[] { ("symfony-form", 2), ("symfony-controller", 1), ("symfony-validation", 1) }),
        ["symfony-validation-constraints"] = PhpPlan("symfony-validation-constraints", new[] { ("symfony-validation", 2), ("symfony-form", 1) }),
        ["symfony-form-errors"] = PhpPlan("symfony-form-errors", new[] { ("symfony-form", 2), ("symfony-validation", 1), ("symfony-twig", 1) }),

        ["symfony-doctrine-entity"] = PhpPlan("symfony-doctrine-entity", new[] { ("symfony-doctrine", 2), ("php-oop", 1) }),
        ["symfony-doctrine-id"] = PhpPlan("symfony-doctrine-id", new[] { ("symfony-doctrine", 2), ("php-types", 1) }),
        ["symfony-doctrine-columns"] = PhpPlan("symfony-doctrine-columns", new[] { ("symfony-doctrine", 2), ("php-types", 1), ("symfony-validation", 1) }),
        ["symfony-doctrine-repository"] = PhpPlan("symfony-doctrine-repository", new[] { ("symfony-repository", 2), ("symfony-doctrine", 1) }),
        ["symfony-doctrine-query"] = PhpPlan("symfony-doctrine-query", new[] { ("symfony-repository", 2), ("symfony-doctrine", 1) }),
        ["symfony-doctrine-save"] = PhpPlan("symfony-doctrine-save", new[] { ("symfony-doctrine", 2), ("symfony-crud", 1) }),
        ["symfony-doctrine-delete"] = PhpPlan("symfony-doctrine-delete", new[] { ("symfony-doctrine", 2), ("symfony-crud", 1) }),

        ["symfony-service-class"] = PhpPlan("symfony-service-class", new[] { ("symfony-service", 2), ("php-oop", 1) }),
        ["symfony-service-repository-injection"] = PhpPlan("symfony-service-repository-injection", new[] { ("symfony-service", 2), ("symfony-repository", 1) }),
        ["symfony-service-method"] = PhpPlan("symfony-service-method", new[] { ("symfony-service", 2), ("symfony-repository", 1) }),
        ["symfony-controller-service-injection"] = PhpPlan("symfony-controller-service-injection", new[] { ("symfony-service", 2), ("symfony-controller", 1) }),
        ["symfony-controller-delegation"] = PhpPlan("symfony-controller-delegation", new[] { ("symfony-service", 2), ("symfony-controller", 1), ("symfony-twig", 1) }),

        ["symfony-project-product-list"] = PhpPlan("symfony-project-product-list", new[] { ("symfony-crud", 2), ("symfony-service", 1), ("symfony-twig", 1) }),
        ["symfony-project-product-show"] = PhpPlan("symfony-project-product-show", new[] { ("symfony-crud", 2), ("symfony-routing", 1), ("symfony-twig", 1) }),
        ["symfony-project-product-create"] = PhpPlan("symfony-project-product-create", new[] { ("symfony-crud", 2), ("symfony-form", 1), ("symfony-doctrine", 1) }),
        ["symfony-project-product-edit"] = PhpPlan("symfony-project-product-edit", new[] { ("symfony-crud", 2), ("symfony-form", 1), ("symfony-doctrine", 1) }),
        ["symfony-project-product-delete"] = PhpPlan("symfony-project-product-delete", new[] { ("symfony-crud", 2), ("symfony-doctrine", 1), ("symfony-routing", 1) }),
        ["symfony-project-product-validation"] = PhpPlan("symfony-project-product-validation", new[] { ("symfony-validation", 2), ("symfony-crud", 1) }),
        ["symfony-project-protected-route"] = PhpPlan("symfony-project-protected-route", new[] { ("symfony-access-control", 2), ("symfony-routing", 1), ("symfony-crud", 1) }),

        ["php-symfony-boss-final-products"] = PhpPlan("php-symfony-boss-final-products", new[] { ("symfony-crud", 2), ("symfony-doctrine", 2), ("symfony-form", 2) })
    };

    private static LessonPlan PhpPlan(string slug, IReadOnlyList<(string Slug, int Weight)> skills)
    {
        var spec = PhpSymfonyLessonSpecFor(slug);
        var firstCriteria = spec.RequiredSnippets.Take(3).ToList();
        return new LessonPlan(
            skills,
            [
                $"Commence par l'objectif principal: {spec.Objective}",
                firstCriteria.Count > 0 ? $"Ajoute d'abord: {string.Join(", ", firstCriteria)}." : "Pose d'abord la structure demandee.",
                $"Complete sans coller la correction: {spec.FailureFeedback}"
            ]);
    }

    private static PhpLessonSpec PhpSymfonyLessonSpecFor(string slug)
    {
        var spec = PhpNativeLessonSpecs().TryGetValue(slug, out var nativeSpec)
            ? nativeSpec
            : PhpSymfonyLessonSpecs.TryGetValue(slug, out var explicitSpec)
                ? explicitSpec
                : GeneratedSymfonyLessonSpecs().TryGetValue(slug, out var generatedSpec)
                    ? generatedSpec
                    : PhpSupplementalLessonSpec(slug);

        return EnrichInteractivePhpSymfonySpec(slug, spec);
    }

    private static PhpLessonSpec EnrichInteractivePhpSymfonySpec(string slug, PhpLessonSpec spec)
    {
        var isSymfony = slug.StartsWith("symfony-", StringComparison.OrdinalIgnoreCase) || slug == "php-symfony-boss-final-products";
        var family = isSymfony ? "Symfony Product Catalog" : "Product Catalog natif";
        var previous = isSymfony
            ? "Tu as deja vu le meme probleme en PHP natif: une requete arrive, le code decide quoi faire, puis une reponse repart."
            : "Tu construis progressivement les briques PHP natives qui serviront ensuite a comprendre Symfony.";

        var conceptSummary = spec.ConceptSummary.Contains("Situation", StringComparison.OrdinalIgnoreCase)
            ? spec.ConceptSummary
            : $"""
              Situation concrete:
              {family} avance par petites briques. Cette lecon part d'un besoin produit reel plutot que d'un mot-cle isole.

              Objectif de competence:
              {spec.Objective}

              Mini-recapitulatif:
              {spec.ConceptSummary}
              """;

        var explanation = spec.Explanation.Contains("Cours", StringComparison.OrdinalIgnoreCase)
            ? spec.Explanation
            : $"""
              Cours:
              {previous}

              Pourquoi cette notion existe:
              {spec.Explanation}

              Explication pas a pas:
              1. Identifie la responsabilite principale de la lecon.
              2. Repere les donnees manipulees dans le Product Catalog.
              3. Ecris un extrait court qui montre la notion dans du code exploitable.
              4. Verifie ensuite les criteres statiques comme une liste de preuves, pas comme une phrase a copier.

              Exemple commente:
              L'exemple ci-dessous reste volontairement court pour isoler la notion avant l'exercice.

              Lien avec le projet fil rouge:
              Cette etape rapproche le mini-projet d'un catalogue produit maintenable: domaine, HTTP, affichage, persistance ou organisation selon la lecon.
              """;

        var exercisePrompt = spec.ExercisePrompt.Contains("Question rapide", StringComparison.OrdinalIgnoreCase)
            ? spec.ExercisePrompt
            : $"""
              Situation concrete:
              Tu ajoutes une brique au {family}.

              Objectif de competence:
              {spec.Objective}

              Question rapide:
              Quel probleme cette notion evite-t-elle dans un catalogue produit qui grandit?

              Reponse attendue:
              Elle evite de disperser une responsabilite dans du code fragile. La notion donne une forme claire a une donnee, une route, une reponse, une vue, une regle ou une persistance.

              Manipulation guidee:
              1. Lis le starter code et garde sa structure.
              2. Ajoute d'abord la declaration principale attendue.
              3. Relie cette declaration au cas Product Catalog.
              4. Termine avec le retour, l'affichage ou la configuration demandee.

              Exercice principal:
              {spec.ExercisePrompt}

              Contraintes explicites:
              Le code doit contenir les elements attendus par la validation dans de vraies instructions.

              Sortie ou comportement attendu:
              {spec.SuccessFeedback}

              Validation automatique:
              La correction statique cherche une combinaison minimale: {string.Join(", ", spec.RequiredSnippets)}.

              Recapitulatif:
              Tu dois obtenir un extrait court, lisible et utile au Product Catalog.

              Lien avec le mini-projet:
              Cette lecon nourrit le meme fil rouge: construire un Product Catalog natif puis Symfony, sans sauter les etapes.
              """;

        var commonMistakes = spec.CommonMistakes.Contains("Erreur frequente", StringComparison.OrdinalIgnoreCase)
            ? spec.CommonMistakes
            : $"""
              Erreur frequente 1:
              Coller les mots attendus en commentaire au lieu de les utiliser dans le code.

              Erreur frequente 2:
              Repondre uniquement au resultat visible sans montrer la structure demandee.

              Erreur frequente 3:
              Oublier le lien avec le Product Catalog et produire un extrait trop generique.

              Conseil:
              {spec.CommonMistakes}
              """;

        return spec with
        {
            ConceptSummary = conceptSummary,
            Explanation = explanation,
            ExercisePrompt = exercisePrompt,
            CommonMistakes = commonMistakes
        };
    }

    private sealed record GeneratedSymfonyDefinition(
        string Title,
        string Objective,
        string[] RequiredSnippets,
        string FinalCorrection,
        IReadOnlyList<(string Slug, int Weight)> Skills,
        int XpReward = 60);

    private static Dictionary<string, PhpLessonSpec> GeneratedSymfonyLessonSpecs() =>
        GeneratedSymfonyLessonDefinitions().ToDictionary(
            item => item.Key,
            item =>
            {
                var definition = item.Value;
                return new PhpLessonSpec(
                    definition.Title,
                    definition.Objective,
                    $"Symfony prolonge le PHP natif: {definition.Objective}",
                    $"Cette lecon montre comment Symfony structure une responsabilite que tu pourrais coder a la main en PHP natif. Le Product Catalog gagne une convention stable, une API claire et un code plus facile a verifier.",
                    BuildGeneratedSymfonyExample(definition),
                    $"Applique cette notion au Product Catalog. Le code doit prouver la competence suivante: {definition.Objective}",
                    "<?php\n\n// TODO: complete cette brique Symfony du Product Catalog.\n",
                    "La brique Symfony est presente, reliee au Product Catalog et exploitable.",
                    $"Ajoute les elements attendus: {string.Join(", ", definition.RequiredSnippets.Take(5))}.",
                    definition.XpReward,
                    definition.RequiredSnippets,
                    "Ne laisse pas la notion sous forme de commentaire: elle doit apparaitre dans du code Symfony ou de configuration utilisable.",
                    definition.FinalCorrection);
            },
            StringComparer.OrdinalIgnoreCase);

    private static string BuildGeneratedSymfonyExample(GeneratedSymfonyDefinition definition) =>
        $"""
        <?php

        // Exemple independant pour isoler la notion.
        // Objectif: {definition.Objective}
        // La correction attendra ensuite une version appliquee au Product Catalog.
        """;

    private static Dictionary<string, GeneratedSymfonyDefinition> GeneratedSymfonyLessonDefinitions() => new(StringComparer.OrdinalIgnoreCase)
    {
        ["symfony-why-framework"] = S("Pourquoi un framework", "Expliquer pourquoi Symfony remplace des conventions PHP natives repetitives.", ["Symfony", "framework", "routing", "controller"], "<?php\n\n// Symfony fournit routing, controllers, services et configuration pour organiser Product Catalog.\n", [("symfony-framework", 2), ("symfony-project-structure", 1)], 20),
        ["symfony-public-index"] = S("public/index.php", "Comprendre le point d'entree HTTP unique d'une application Symfony.", ["public/index.php", "Kernel", "Request"], "<?php\n\n// public/index.php\nuse App\\Kernel;\nuse Symfony\\Component\\HttpFoundation\\Request;\n\n$kernel = new Kernel('dev', true);\n$request = Request::createFromGlobals();", [("symfony-framework", 1), ("symfony-request", 1), ("symfony-project-structure", 1)], 25),
        ["symfony-request-response-flow"] = S("Cycle Request Response", "Relier requete HTTP, controller Symfony et reponse.", ["Request", "Response", "Controller"], "<?php\n\nuse Symfony\\Component\\HttpFoundation\\Request;\nuse Symfony\\Component\\HttpFoundation\\Response;\n\npublic function index(Request $request): Response\n{\n    return new Response('Products');\n}", [("symfony-request", 2), ("symfony-response", 1)], 30),
        ["symfony-bin-console"] = S("bin/console", "Identifier le role de la console Symfony dans le projet.", ["bin/console", "debug:router", "make:"], "bin/console debug:router\nbin/console make:controller ProductController", [("symfony-console", 2), ("symfony-framework", 1)], 20),
        ["symfony-env-file"] = S("Fichier .env", "Comprendre les variables d'environnement du projet Symfony.", [".env", "APP_ENV", "DATABASE_URL"], "APP_ENV=dev\nDATABASE_URL=\"sqlite:///%kernel.project_dir%/var/data.db\"", [("symfony-framework", 1), ("symfony-project-structure", 1)], 20),

        ["symfony-controller-basic"] = S("Controller de base", "Creer une action controller qui retourne une Response.", ["Controller", "AbstractController", "Response", "return"], "<?php\n\nuse Symfony\\Bundle\\FrameworkBundle\\Controller\\AbstractController;\nuse Symfony\\Component\\HttpFoundation\\Response;\n\nfinal class ProductController extends AbstractController\n{\n    public function index(): Response\n    {\n        return new Response('Products');\n    }\n}", [("symfony-controller", 2), ("symfony-response", 1)], 35),
        ["symfony-request-query"] = S("Query string Symfony", "Lire un filtre GET avec l'objet Request.", ["Request", "query", "get", "search"], "<?php\n\npublic function index(Request $request): Response\n{\n    $search = $request->query->get('search', '');\n    return new Response($search);\n}", [("symfony-request", 2), ("symfony-controller", 1)], 35),
        ["symfony-request-post"] = S("POST Symfony", "Lire une donnee POST avec Request.", ["Request", "request", "get", "name"], "<?php\n\npublic function create(Request $request): Response\n{\n    $name = $request->request->get('name', '');\n    return new Response($name);\n}", [("symfony-request", 2), ("symfony-controller", 1)], 35),
        ["symfony-redirect"] = S("Redirection Symfony", "Rediriger vers la liste produits apres une action.", ["redirectToRoute", "product_index", "return"], "<?php\n\nreturn $this->redirectToRoute('product_index');", [("symfony-response", 1), ("symfony-routing", 1), ("symfony-controller", 1)], 35),
        ["symfony-not-found"] = S("404 Symfony", "Retourner une erreur 404 quand le produit est absent.", ["createNotFoundException", "throw", "Product"], "<?php\n\nif (!$product) {\n    throw $this->createNotFoundException('Product not found');\n}", [("symfony-controller", 1), ("symfony-response", 1)], 40),

        ["symfony-twig-include"] = S("Include Twig", "Extraire une carte produit reutilisable avec include.", ["include", "product/_card.html.twig", "product"], "{{ include('product/_card.html.twig', { product: product }) }}", [("symfony-twig", 2), ("symfony-project-architecture", 1)], 35),
        ["symfony-twig-layout"] = S("Layout Twig", "Utiliser un layout Twig commun.", ["extends", "base.html.twig", "block body"], "{% extends 'base.html.twig' %}\n{% block body %}\n    <h1>Products</h1>\n{% endblock %}", [("symfony-twig", 2), ("symfony-project-architecture", 1)], 35),
        ["symfony-twig-product-card"] = S("Carte produit Twig", "Afficher name, price et stock dans une carte produit Twig.", ["product.name", "product.price", "product.stock"], "<article>\n    <h2>{{ product.name }}</h2>\n    <p>{{ product.price }} euros</p>\n    <p>{{ product.stock }}</p>\n</article>", [("symfony-twig", 2), ("symfony-crud", 1)], 35),

        ["symfony-doctrine-why-orm"] = S("Pourquoi Doctrine ORM", "Comprendre comment Doctrine remplace le SQL repetitif pour les entites.", ["Doctrine", "Entity", "Repository"], "<?php\n\n// Doctrine mappe Product en table et ProductRepository regroupe les requetes.\n", [("symfony-doctrine", 2), ("symfony-repository", 1)], 35),
        ["symfony-doctrine-find"] = S("find avec Doctrine", "Charger un produit par id depuis le repository.", ["ProductRepository", "find", "$id"], "<?php\n\n$product = $productRepository->find($id);", [("symfony-repository", 2), ("symfony-doctrine", 1)], 45),
        ["symfony-doctrine-relations"] = S("Relation Doctrine simple", "Declarer une relation entre Product et Category.", ["ManyToOne", "Category", "private"], "<?php\n\n#[ORM\\ManyToOne]\nprivate ?Category $category = null;", [("symfony-doctrine", 2), ("php-oop", 1)], 50),

        ["symfony-form-why"] = S("Pourquoi Symfony Form", "Comparer formulaire Symfony et lecture manuelle de POST.", ["Form", "Request", "validation"], "<?php\n\n// Symfony Form lit Request, hydrate Product et expose les erreurs de validation.\n", [("symfony-form", 2), ("symfony-request", 1)], 35),
        ["symfony-form-product-create"] = S("Formulaire creation Product", "Assembler ProductType dans une action de creation.", ["createForm", "ProductType::class", "new Product", "handleRequest"], "<?php\n\n$product = new Product();\n$form = $this->createForm(ProductType::class, $product);\n$form->handleRequest($request);", [("symfony-form", 2), ("symfony-crud", 1)], 55),
        ["symfony-form-product-edit"] = S("Formulaire edition Product", "Reutiliser ProductType pour modifier un produit existant.", ["Product $product", "createForm", "handleRequest", "flush"], "<?php\n\n$form = $this->createForm(ProductType::class, $product);\n$form->handleRequest($request);\n$entityManager->flush();", [("symfony-form", 1), ("symfony-crud", 2)], 55),

        ["symfony-service-why"] = S("Pourquoi un service", "Comprendre pourquoi la logique metier sort du controller.", ["service", "controller", "ProductCatalogService"], "<?php\n\n// ProductCatalogService porte les cas d'usage; le controller gere HTTP.\n", [("symfony-service", 2), ("symfony-project-architecture", 1)], 40),
        ["symfony-dependency-injection"] = S("Injection de dependances", "Recevoir un service par constructeur au lieu de le creer avec new.", ["__construct", "private", "ProductCatalogService"], "<?php\n\npublic function __construct(private ProductCatalogService $products) {}\n", [("symfony-dependency-injection", 2), ("symfony-service", 1)], 45),
        ["symfony-dto"] = S("DTO Symfony", "Representer des donnees d'entree sans surcharger l'entite.", ["class", "ProductInput", "public string $name"], "<?php\n\nfinal class ProductInput\n{\n    public string $name = '';\n    public int $price = 0;\n}", [("symfony-service", 1), ("symfony-project-architecture", 1), ("php-oop", 1)], 45),
        ["symfony-command"] = S("Commande Symfony", "Creer une commande console pour inspecter le catalogue.", ["Command", "AsCommand", "execute"], "<?php\n\n#[AsCommand(name: 'app:products:list')]\nfinal class ListProductsCommand extends Command\n{\n    protected function execute(InputInterface $input, OutputInterface $output): int\n    {\n        return Command::SUCCESS;\n    }\n}", [("symfony-console", 2), ("symfony-service", 1)], 50),

        ["symfony-security-why"] = S("Pourquoi securiser", "Identifier les actions produit qui doivent etre protegees.", ["security", "ROLE_USER", "route"], "<?php\n\n// Les routes create, edit et delete du Product Catalog demandent un utilisateur connecte.\n", [("symfony-security", 2), ("symfony-crud", 1)], 35),
        ["symfony-user-entity"] = S("Entity User", "Declarer une entite utilisateur minimale.", ["class User", "UserInterface", "getUserIdentifier"], "<?php\n\nfinal class User implements UserInterface\n{\n    public function getUserIdentifier(): string { return $this->email; }\n}", [("symfony-security", 2), ("symfony-doctrine", 1)], 50),
        ["symfony-password-hashing"] = S("Hash password", "Hasher un mot de passe avant persistence.", ["UserPasswordHasherInterface", "hashPassword", "setPassword"], "<?php\n\n$hash = $passwordHasher->hashPassword($user, $plainPassword);\n$user->setPassword($hash);", [("symfony-security", 2), ("symfony-service", 1)], 50),
        ["symfony-roles"] = S("Roles Symfony", "Representer les roles d'un utilisateur.", ["getRoles", "ROLE_USER", "array_unique"], "<?php\n\npublic function getRoles(): array\n{\n    return array_unique([...$this->roles, 'ROLE_USER']);\n}", [("symfony-security", 2), ("php-arrays", 1)], 45),
        ["symfony-is-granted"] = S("isGranted", "Tester un droit dans un controller.", ["isGranted", "ROLE_ADMIN", "if"], "<?php\n\nif ($this->isGranted('ROLE_ADMIN')) {\n    return new Response('admin');\n}", [("symfony-security", 2), ("symfony-controller", 1)], 45),
        ["symfony-deny-access-unless-granted"] = S("denyAccessUnlessGranted", "Bloquer une action si le role manque.", ["denyAccessUnlessGranted", "ROLE_USER"], "<?php\n\n$this->denyAccessUnlessGranted('ROLE_USER');", [("symfony-security", 2), ("symfony-controller", 1)], 45),
        ["symfony-protected-route"] = S("Route protegee", "Proteger une route avec IsGranted.", ["IsGranted", "ROLE_USER", "#[Route"], "<?php\n\n#[Route('/products/new')]\n#[IsGranted('ROLE_USER')]\npublic function new(): Response\n{\n    return new Response('Protected');\n}", [("symfony-security", 2), ("symfony-routing", 1)], 50),
        ["symfony-csrf-basic"] = S("CSRF simple", "Verifier un token CSRF sur une action sensible.", ["isCsrfTokenValid", "csrf_token", "delete"], "<?php\n\nif ($this->isCsrfTokenValid('delete'.$product->getId(), $request->request->get('_token'))) {\n    $entityManager->remove($product);\n}", [("symfony-security", 2), ("symfony-form", 1)], 55),

        ["symfony-api-json-response"] = S("API JsonResponse", "Retourner une reponse JSON Symfony.", ["JsonResponse", "return new JsonResponse", "name"], "<?php\n\nreturn new JsonResponse(['name' => 'Book']);", [("symfony-api", 2), ("symfony-response", 1)], 45),
        ["symfony-api-list-products"] = S("API liste produits", "Retourner les produits disponibles en JSON.", ["#[Route('/api/products'", "JsonResponse", "findAvailable"], "<?php\n\n#[Route('/api/products', methods: ['GET'])]\npublic function list(ProductRepository $products): JsonResponse\n{\n    return new JsonResponse($products->findAvailable());\n}", [("symfony-api", 2), ("symfony-repository", 1)], 55),
        ["symfony-api-show-product"] = S("API detail produit", "Retourner un produit par id en JSON.", ["#[Route('/api/products/{id}'", "Product $product", "JsonResponse"], "<?php\n\n#[Route('/api/products/{id}', methods: ['GET'])]\npublic function show(Product $product): JsonResponse\n{\n    return new JsonResponse(['name' => $product->getName()]);\n}", [("symfony-api", 2), ("symfony-routing", 1)], 55),
        ["symfony-api-create-product"] = S("API creation produit", "Creer un produit depuis une requete JSON.", ["Request", "json_decode", "persist", "JsonResponse"], "<?php\n\n$data = json_decode($request->getContent(), true);\n$product = new Product($data['name'], $data['price']);\n$entityManager->persist($product);\n$entityManager->flush();\nreturn new JsonResponse(['status' => 'created'], 201);", [("symfony-api", 2), ("symfony-crud", 1)], 65),
        ["symfony-api-validation-errors"] = S("Erreurs validation API", "Retourner les erreurs de validation en JSON.", ["ValidatorInterface", "validate", "JsonResponse"], "<?php\n\n$errors = $validator->validate($product);\nif (count($errors) > 0) {\n    return new JsonResponse(['errors' => (string) $errors], 400);\n}", [("symfony-api", 1), ("symfony-validation", 2)], 60),
        ["symfony-api-status-codes"] = S("Status codes API", "Choisir un code HTTP adapte a la reponse.", ["JsonResponse", "201", "404"], "<?php\n\nreturn new JsonResponse(['status' => 'created'], 201);\nreturn new JsonResponse(['error' => 'Not found'], 404);", [("symfony-api", 2), ("symfony-response", 1)], 45),
        ["symfony-api-service-layer"] = S("API et service layer", "Deleguer la logique API au service ProductCatalogService.", ["ProductCatalogService", "createProduct", "JsonResponse"], "<?php\n\n$product = $catalog->createProduct($data['name'], $data['price']);\nreturn new JsonResponse($product->toArray(), 201);", [("symfony-api", 1), ("symfony-service", 2)], 65),

        ["symfony-project-product-service"] = S("Service projet Product", "Ajouter ProductCatalogService dans le projet vertical Symfony.", ["ProductCatalogService", "__construct", "ProductRepository"], "<?php\n\nfinal readonly class ProductCatalogService\n{\n    public function __construct(private ProductRepository $products) {}\n}", [("symfony-service", 2), ("symfony-project-architecture", 1)], 70),
        ["symfony-project-api-endpoint"] = S("Endpoint API projet", "Ajouter un endpoint JSON au Product Catalog complet.", ["#[Route('/api/products'", "JsonResponse", "ProductCatalogService"], "<?php\n\n#[Route('/api/products', methods: ['GET'])]\npublic function api(ProductCatalogService $catalog): JsonResponse\n{\n    return new JsonResponse($catalog->listAvailableProducts());\n}", [("symfony-api", 2), ("symfony-project-architecture", 1)], 75),
        ["symfony-project-clean-architecture"] = S("Architecture propre projet", "Separer controller, service, repository, entite et vues.", ["Controller", "ProductCatalogService", "ProductRepository", "Product", "templates/product"], "<?php\n\n// Controller -> ProductCatalogService -> ProductRepository -> Product\n// templates/product affiche les donnees sans requete Doctrine directe.\n", [("symfony-project-architecture", 2), ("symfony-service", 1), ("symfony-crud", 1)], 80)
    };

    private static GeneratedSymfonyDefinition S(
        string title,
        string objective,
        string[] requiredSnippets,
        string finalCorrection,
        IReadOnlyList<(string Slug, int Weight)> skills,
        int xpReward = 60) =>
        new(title, objective, requiredSnippets, finalCorrection, skills, xpReward);

    private sealed record PhpSupplementalDefinition(
        string Title,
        string Objective,
        string[] RequiredSnippets,
        string FinalCorrection,
        IReadOnlyList<(string Slug, int Weight)> Skills);

    private static PhpLessonSpec PhpSupplementalLessonSpec(string slug)
    {
        var definition = PhpSupplementalDefinitions()[slug];
        var requiredSnippets = string.Join(", ", definition.RequiredSnippets);
        return new PhpLessonSpec(
            definition.Title,
            definition.Objective,
            $"{definition.Title} introduit une notion PHP a reconnaitre, comprendre, puis utiliser dans un extrait de code lisible.",
            $"Dans un projet PHP, cette notion sert a rendre l'intention du code explicite avant d'arriver a Symfony. Commence par identifier la donnee manipulee, puis ecris une instruction PHP complete qui applique la notion. Les elements attendus par la validation sont: {requiredSnippets}. Ils ne sont pas la consigne a recopier, mais les traces minimales qui prouvent que la notion est vraiment utilisee.",
            "<?php\n\n$message = \"Exemple independant\";\necho $message;",
            $"""
            Mise en pratique:
            {definition.Objective}

            Ce que ton code doit montrer:
            Le code doit contenir les elements suivants dans de vraies instructions PHP: {requiredSnippets}.

            Validation:
            Les commentaires seuls ne suffisent pas; chaque element attendu doit apparaitre dans du code exploitable.
            """,
            "<?php\n\n// Complete l'exercice ici.",
            "La notion PHP demandee est presente dans un code coherent.",
            $"Ajoute les elements attendus dans du code PHP executable: {requiredSnippets}.",
            50,
            definition.RequiredSnippets,
            "Erreur frequente: traiter les mots attendus comme une liste a coller. Ecris d'abord le code qui a du sens, puis verifie que la notion apparait dans des instructions PHP completes.",
            definition.FinalCorrection);
    }

    private static Dictionary<string, LessonPlan> PhpSupplementalLessonPlans()
    {
        var plans = PhpSupplementalDefinitions().ToDictionary(
            item => item.Key,
            item => new LessonPlan(
                item.Value.Skills,
                [
                    $"Repere la notion principale: {item.Value.Title}.",
                    $"Ajoute les premiers elements attendus: {string.Join(", ", item.Value.RequiredSnippets.Take(3))}.",
                    "Complete un exemple minimal sans afficher la correction complete."
                ]),
            StringComparer.OrdinalIgnoreCase);

        foreach (var item in PhpNativeLessonDefinitions())
        {
            plans[item.Key] = new LessonPlan(item.Value.Skills, item.Value.Hints);
        }

        foreach (var item in GeneratedSymfonyLessonDefinitions())
        {
            plans[item.Key] = new LessonPlan(
                item.Value.Skills,
                [
                    $"Indice 1 - situation: applique `{item.Value.Title}` au Product Catalog Symfony.",
                    $"Indice 2 - strategie: commence par {string.Join(", ", item.Value.RequiredSnippets.Take(2))}, puis relie-le a la responsabilite Symfony.",
                    $"Indice 3 - verification: controle les criteres {string.Join(", ", item.Value.RequiredSnippets.Take(3))} dans du code exploitable."
                ]);
        }

        return plans;
    }

    private sealed record PhpNativeLessonDefinition(
        string Title,
        string Context,
        string Objective,
        string Inputs,
        string ExpectedOutput,
        string Constraints,
        string StarterCode,
        string[] RequiredSnippets,
        string FinalCorrection,
        IReadOnlyList<(string Slug, int Weight)> Skills,
        IReadOnlyList<string> Hints,
        int XpReward = 60);

    private static Dictionary<string, PhpLessonSpec> PhpNativeLessonSpecs() =>
        PhpNativeLessonDefinitions().ToDictionary(
            item => item.Key,
            item =>
            {
                var definition = item.Value;
                var prompt = $"""
                Situation concrete:
                {definition.Context}

                Objectif de competence:
                {definition.Objective}

                Question rapide:
                Quelle donnee de depart dois-tu utiliser pour obtenir la sortie attendue, et pourquoi ne faut-il pas coder le resultat final en dur?

                Reponse attendue:
                Il faut partir des entrees imposees ({definition.Inputs}) afin que le code reste modifiable, verifiable et reutilisable dans le Product Catalog.

                Manipulation guidee:
                1. Lis le starter sans supprimer les lignes deja fournies.
                2. Repere les donnees imposees.
                3. Ajoute la transformation PHP demandee par la lecon.
                4. Termine par la sortie ou la valeur attendue.

                Exercice principal:
                {definition.Objective}

                Entrees imposees:
                {definition.Inputs}

                Sortie attendue:
                {definition.ExpectedOutput}

                Validation automatique:
                {definition.Constraints}

                Recapitulatif:
                Tu dois obtenir un code court, coherent et relie au Product Catalog natif.

                Lien avec le mini-projet:
                Cette brique sera reutilisee dans le catalogue produits natif: variables, tableaux, fonctions, domaine objet, HTTP, JSON ou persistance selon le module.
                """;

                return new PhpLessonSpec(
                    definition.Title,
                    definition.Objective,
                    BuildPhpConceptSummary(definition),
                    BuildPhpExplanation(definition),
                    BuildPhpCommentedExample(definition),
                    prompt,
                    definition.StarterCode,
                    $"Competence validee: tu sais appliquer `{definition.Title}` dans le fil rouge Product Catalog.",
                    $"La solution est incomplete. Repars de la situation, puis verifie les elements attendus: {string.Join(", ", definition.RequiredSnippets.Take(4))}.",
                    definition.XpReward,
                    definition.RequiredSnippets,
                    BuildPhpCommonMistakes(definition),
                    definition.FinalCorrection);
            },
            StringComparer.OrdinalIgnoreCase);

    private static string BuildPhpConceptSummary(PhpNativeLessonDefinition definition) =>
        $"""
        Situation concrete:
        {definition.Context}

        Objectif de competence:
        {definition.Objective}

        Mini-recapitulatif:
        Cette etape ajoute une brique au Product Catalog natif. Elle transforme une intention metier en code PHP lisible, testable et reutilisable.
        """;

    private static string BuildPhpExplanation(PhpNativeLessonDefinition definition) =>
        $"""
        Cours:
        Avant d'ecrire le code, identifie trois choses: la donnee de depart, la transformation a appliquer et le resultat attendu. En PHP, une lecon utile ne consiste pas a placer un mot-cle au hasard: chaque instruction doit expliquer une petite decision du programme.

        Situation:
        {definition.Context}

        Donnees imposees:
        {definition.Inputs}

        Raisonnement:
        1. Repere les valeurs deja fournies ou a declarer.
        2. Applique la notion de la lecon pour transformer ces valeurs.
        3. Termine par une sortie, un retour ou une structure exploitable.

        Resultat vise:
        {definition.ExpectedOutput}

        Lien avec le projet:
        Dans le Product Catalog natif, cette notion servira ensuite a representer des produits, filtrer un catalogue, calculer un panier, exposer du JSON ou organiser le domaine.
        """;

    private static string BuildPhpCommentedExample(PhpNativeLessonDefinition definition) =>
        $"""
        <?php

        // Exemple independant: on part d'un produit simple.
        $product = ["name" => "Example", "price" => 10, "stock" => 1];

        // Idee de la lecon:
        // {definition.Objective}

        // On garde l'exemple separe de la correction pour comprendre la notion
        // avant de l'appliquer dans l'exercice.
        echo $product["name"];
        """;

    private static string BuildPhpCommonMistakes(PhpNativeLessonDefinition definition) =>
        $"""
        Erreur frequente 1:
        Coder directement `{definition.ExpectedOutput}` sans utiliser les donnees imposees.

        Erreur frequente 2:
        Ajouter les mots attendus en commentaire au lieu de les utiliser dans des instructions PHP.

        Erreur frequente 3:
        Oublier le lien metier: chaque ligne doit contribuer au Product Catalog, pas seulement satisfaire la validation statique.
        """;

    private static Dictionary<string, PhpNativeLessonDefinition> PhpNativeLessonDefinitions() => new(StringComparer.OrdinalIgnoreCase)
    {
        ["php-what-is-php"] = N("Qu'est-ce que PHP ?", "Tu decouvres le langage qui executera le backend du catalogue.", "Ecrire un premier fichier PHP qui affiche le role du serveur.", "Aucune variable fournie.", "`PHP prepare la reponse serveur`.", "Commencer par `<?php` et utiliser `echo` pour afficher la phrase.", "<?php\n\n// TODO: affiche le role de PHP dans le backend.\n", ["<?php", "echo", "PHP prepare la reponse serveur"], "<?php\n\necho \"PHP prepare la reponse serveur\";", [("php-syntax", 2), ("php-strings", 1)], 15),
        ["php-syntax-script"] = N("Script PHP minimal", "Tu crees le script d'accueil d'une boutique.", "Afficher exactement `Bienvenue dans Product Catalog`.", "Aucune variable fournie.", "`Bienvenue dans Product Catalog`.", "Commencer par `<?php`, utiliser `echo`, ne pas ecrire de HTML et terminer l'instruction par `;`.", "<?php\n\n// TODO: affiche le message d'accueil de la boutique.\n", ["<?php", "echo", "Bienvenue dans Product Catalog", ";"], "<?php\n\necho \"Bienvenue dans Product Catalog\";", [("php-syntax", 2), ("php-strings", 1)]),
        ["php-echo-output"] = N("Afficher avec echo", "Tu veux envoyer une petite reponse texte au navigateur.", "Utiliser `echo` pour afficher `Product Catalog`.", "Texte impose: Product Catalog.", "`Product Catalog`.", "Utiliser `echo`, une chaine et un point-virgule.", "<?php\n\n// TODO: affiche le nom du projet.\n", ["echo", "Product Catalog", ";"], "<?php\n\necho \"Product Catalog\";", [("php-syntax", 1), ("php-strings", 2)], 15),
        ["php-comments"] = N("Commentaires utiles", "Tu documentes une ligne qui initialise le catalogue.", "Ajouter un commentaire utile puis un echo executable.", "Message impose: Catalogue initialise.", "`Catalogue initialise`.", "Le commentaire doit aider a comprendre le code, puis `echo` doit faire le travail reel.", "<?php\n\n// TODO: ajoute un commentaire utile.\n// TODO: affiche le message.\n", ["//", "echo", "Catalogue initialise"], "<?php\n\n// Le catalogue demarre avec un message de verification serveur.\necho \"Catalogue initialise\";", [("php-syntax", 2), ("php-strings", 1)], 15),
        ["php-variables-product"] = N("Variables produit", "Tu developpes une fiche produit.", "Declarer `$name`, `$price`, `$stock`, puis afficher le prix du produit.", "`$name = \"Book\"`, `$price = 12`, `$stock = 3`.", "`Book coute 12 euros`.", "Utiliser les variables dans l'affichage et ne pas ecrire toute la phrase en dur.", "<?php\n\n// TODO: declare name, price et stock.\n// TODO: affiche la phrase avec les variables.\n", ["$name", "$price", "$stock", "\"Book\"", "12", "echo", "$name"], "<?php\n\n$name = \"Book\";\n$price = 12;\n$stock = 3;\n\necho $name . \" coute \" . $price . \" euros\";", [("php-variables", 2), ("php-strings", 1)]),
        ["php-strings-output"] = N("Chaines et sortie produit", "Tu construis la phrase visible sur une fiche produit.", "Composer une chaine avec variables et concatenation pour afficher un libelle produit.", "`$name = \"Book\"`, `$price = 12`.", "`Product: Book - 12 euros`.", "Utiliser `$name`, `$price`, l'operateur `.`, et `echo` sans ecrire toute la phrase finale en dur.", "<?php\n\n$name = \"Book\";\n$price = 12;\n\n// TODO: compose et affiche le libelle produit.\n", ["$name", "$price", ".", "echo"], "<?php\n\n$name = \"Book\";\n$price = 12;\n\necho \"Product: \" . $name . \" - \" . $price . \" euros\";", [("php-strings", 2), ("php-variables", 1)]),
        ["php-strings"] = N("Chaines et concatenation", "Tu prepares le libelle d'un produit saisi avec des espaces autour du nom.", "Nettoyer le nom `\" Book \"` avec `trim`, le stocker dans `$name`, puis afficher `Product: Book` avec une concatenation.", "`$rawName = \" Book \"`.", "`Product: Book`.", "Utiliser `$name`, appeler `trim` sur la valeur fournie, utiliser l'operateur de concatenation `.`, puis afficher le resultat avec `echo`.", "<?php\n\n$rawName = \" Book \";\n\n// TODO: nettoie $rawName dans $name.\n// TODO: affiche Product: suivi du nom nettoye avec une concatenation.\n", ["$name", "trim", ".", "echo"], "<?php\n\n$rawName = \" Book \";\n$name = trim($rawName);\n\necho \"Product: \" . $name;", [("php-strings", 2), ("php-variables", 1)]),
        ["php-numbers-price"] = N("Nombres et prix", "Tu calcules un prix TTC simple.", "Stocker un prix numerique puis calculer 20% de TVA.", "`$price = 10`.", "`12`.", "Utiliser un nombre, une multiplication et une variable de resultat.", "<?php\n\n$price = 10;\n\n// TODO: calcule le prix TTC.\n", ["$price", "$priceWithTax", "* 1.2", "echo"], "<?php\n\n$price = 10;\n$priceWithTax = $price * 1.2;\n\necho $priceWithTax;", [("php-types", 2), ("php-variables", 1)], 20),
        ["php-booleans-stock"] = N("Booleens et stock", "Tu dois representer si un produit est disponible.", "Declarer un booleen `$isAvailable` depuis le stock.", "`$stock = 3`.", "`Disponible`.", "Utiliser une comparaison, une variable booleenne et une condition.", "<?php\n\n$stock = 3;\n\n// TODO: calcule la disponibilite.\n", ["$isAvailable", "$stock > 0", "if", "Disponible"], "<?php\n\n$stock = 3;\n$isAvailable = $stock > 0;\n\nif ($isAvailable) {\n    echo \"Disponible\";\n}", [("php-types", 1), ("php-conditions", 1)], 20),
        ["php-types-order"] = N("Types scalaires", "Tu representes une commande simple.", "Declarer les donnees scalaires d'une commande et afficher son resume.", "`$orderNumber`, `$quantity`, `$unitPrice`, `$isPaid`.", "`Commande CMD-001 : 3 articles`.", "Utiliser une string, un int, un float, un bool et composer l'affichage depuis les variables.", "<?php\n\n// TODO: declare les donnees de commande.\n// TODO: affiche le resume.\n", ["$orderNumber", "$quantity", "$unitPrice", "$isPaid", "true||false", "CMD-001", "echo"], "<?php\n\n$orderNumber = \"CMD-001\";\n$quantity = 3;\n$unitPrice = 12.5;\n$isPaid = true;\n\necho \"Commande \" . $orderNumber . \" : \" . $quantity . \" articles\";", [("php-types", 2), ("php-variables", 1)]),
        ["php-condition-stock"] = N("Condition de stock", "Tu affiches la disponibilite d'un produit.", "Afficher `Disponible` si le stock est positif, sinon `Rupture`.", "`$stock = 0`.", "`Rupture`.", "Utiliser `if`, `else` et tester `$stock > 0`.", "<?php\n\n$stock = 0;\n\n// TODO: ajoute la condition de disponibilite.\n", ["if", "else", "$stock > 0", "Disponible", "Rupture"], "<?php\n\n$stock = 0;\n\nif ($stock > 0) {\n    echo \"Disponible\";\n} else {\n    echo \"Rupture\";\n}", [("php-conditions", 2), ("php-types", 1)]),
        ["php-condition-discount"] = N("Remise conditionnelle", "Tu calcules le prix final d'un panier.", "Appliquer 10% de remise si le total atteint 100 euros.", "`$total = 120`.", "`Total final : 108`.", "Utiliser `$discount`, une condition, un calcul, et ne pas afficher directement 108 sans calcul.", "<?php\n\n$total = 120;\n$discount = 0;\n\n// TODO: calcule la remise si necessaire.\n// TODO: calcule puis affiche le total final.\n", ["$total", "$discount", "if", ">= 100", "*", "echo"], "<?php\n\n$total = 120;\n$discount = 0;\n\nif ($total >= 100) {\n    $discount = $total * 0.10;\n}\n\n$finalTotal = $total - $discount;\necho \"Total final : \" . $finalTotal;", [("php-conditions", 2), ("php-variables", 1)]),
        ["php-match-order-status"] = N("Etat de commande avec match", "Tu traduis un statut technique en message client.", "Utiliser `match` pour convertir `$status` en message lisible.", "`$status = \"paid\"`.", "`Commande payee`.", "Prevoir pending, paid, cancelled et un default, puis afficher le message.", "<?php\n\n$status = \"paid\";\n\n// TODO: cree $message avec match.\n// TODO: affiche $message.\n", ["match", "$status", "\"paid\"||'paid'", "Commande payee", "default", "echo"], "<?php\n\n$status = \"paid\";\n\n$message = match ($status) {\n    \"pending\" => \"Commande en attente\",\n    \"paid\" => \"Commande payee\",\n    \"cancelled\" => \"Commande annulee\",\n    default => \"Statut inconnu\",\n};\n\necho $message;", [("php-conditions", 2), ("php-modern-types", 1)]),
        ["php-loop-invoice-lines"] = N("Boucle for sur lignes de facture", "Tu generes les lignes d'une facture courte.", "Afficher les lignes 1, 2 et 3 avec une boucle.", "Aucune liste fournie, seulement un compteur.", "`Ligne 1`, `Ligne 2`, `Ligne 3`.", "Utiliser `for`, demarrer a 1, aller jusqu'a 3, et afficher la variable de boucle.", "<?php\n\n// TODO: boucle de 1 a 3.\n", ["for", "$i = 1", "$i <= 3", "$i++", "echo", "$i"], "<?php\n\nfor ($i = 1; $i <= 3; $i++) {\n    echo \"Ligne \" . $i;\n}", [("php-loops", 2), ("php-variables", 1)]),
        ["php-while-stock-decrease"] = N("Boucle while et decrementation", "Tu simules une sortie de stock.", "Afficher le stock restant tant qu'il est positif.", "`$stock = 3`.", "`Stock restant : 3`, puis 2, puis 1.", "Utiliser `while`, decremente `$stock` et eviter une boucle infinie.", "<?php\n\n$stock = 3;\n\n// TODO: affiche puis decremente le stock.\n", ["while", "$stock > 0", "$stock--||$stock -= 1", "echo"], "<?php\n\n$stock = 3;\n\nwhile ($stock > 0) {\n    echo \"Stock restant : \" . $stock;\n    $stock--;\n}", [("php-loops", 2), ("php-conditions", 1)]),

        ["php-array-product-names"] = N("Noms de produits", "Tu initialises les produits visibles d'un catalogue.", "Creer un tableau de noms et afficher le premier produit avec le total.", "Produits imposes: Book, Pen, Mug.", "`Book` et `3`.", "Utiliser `$products[0]` et `count($products)`.", "<?php\n\n// TODO: cree le tableau products.\n// TODO: affiche le premier produit et le nombre total.\n", ["$products", "\"Book\"", "\"Pen\"", "\"Mug\"", "$products[0]", "count($products)"], "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\necho $products[0];\necho count($products);", [("php-arrays", 2), ("php-functions", 1)]),
        ["php-array-access-index"] = N("Acces par index", "Tu affiches le deuxieme produit de la liste catalogue.", "Lire un element de tableau avec son index.", "`$products = [\"Book\", \"Pen\", \"Mug\"]`.", "`Pen`.", "Utiliser l'index 1 et ne pas ecrire `Pen` directement dans echo.", "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\n// TODO: affiche le deuxieme produit.\n", ["$products[1]", "echo"], "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\necho $products[1];", [("php-arrays", 2), ("php-variables", 1)], 25),
        ["php-array-count-products"] = N("Compter les produits", "Tu veux afficher le nombre de produits du catalogue.", "Utiliser `count` pour compter les elements d'un tableau.", "`$products` contient 3 noms.", "`3 produits`.", "Utiliser `count($products)` dans la phrase.", "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\n// TODO: affiche le nombre de produits.\n", ["count($products)", "echo", "$products"], "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\necho count($products) . \" produits\";", [("php-arrays", 1), ("php-functions", 1)], 25),
        ["php-associative-product"] = N("Produit associatif", "Tu representes une fiche produit sans classe.", "Creer un tableau associatif produit et afficher son libelle prix.", "name => Book, price => 12, stock => 3.", "`Book - 12 euros`.", "Lire `name` et `price` depuis le tableau, pas depuis des variables separees.", "<?php\n\n$product = [\n    // TODO: name, price, stock\n];\n\n// TODO: affiche le libelle.\n", ["\"name\" => \"Book\"", "\"price\" => 12", "\"stock\" => 3", "$product[\"name\"]", "$product[\"price\"]"], "<?php\n\n$product = [\n    \"name\" => \"Book\",\n    \"price\" => 12,\n    \"stock\" => 3,\n];\n\necho $product[\"name\"] . \" - \" . $product[\"price\"] . \" euros\";", [("php-arrays", 2), ("php-types", 1)]),
        ["php-products-list"] = N("Liste de produits", "Tu construis une liste catalogue en tableaux associatifs.", "Creer 3 produits avec name, price, stock puis afficher chaque nom.", "Trois tableaux associatifs dans `$products`.", "Les trois noms de produits.", "Utiliser `foreach ($products as $product)` et `$product[\"name\"]`.", "<?php\n\n$products = [\n    // TODO: trois produits associatifs\n];\n\n// TODO: parcours la liste.\n", ["$products", "foreach", "as $product", "$product[\"name\"]", "echo"], "<?php\n\n$products = [\n    [\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3],\n    [\"name\" => \"Pen\", \"price\" => 2, \"stock\" => 10],\n    [\"name\" => \"Mug\", \"price\" => 8, \"stock\" => 0],\n];\n\nforeach ($products as $product) {\n    echo $product[\"name\"];\n}", [("php-arrays", 2), ("php-loops", 1)]),
        ["php-foreach-products"] = N("foreach produits", "Tu dois afficher chaque produit d'une liste sans gerer les index.", "Parcourir `$products` avec `foreach` et afficher chaque nom.", "`$products` contient des tableaux associatifs.", "Chaque nom de produit.", "Utiliser `foreach`, `as $product` et `$product[\"name\"]`.", "<?php\n\n$products = [[\"name\" => \"Book\"], [\"name\" => \"Pen\"]];\n\n// TODO: parcours la liste.\n", ["foreach", "as $product", "$product[\"name\"]", "echo"], "<?php\n\n$products = [[\"name\" => \"Book\"], [\"name\" => \"Pen\"]];\n\nforeach ($products as $product) {\n    echo $product[\"name\"];\n}", [("php-loops", 2), ("php-arrays", 1)], 30),
        ["php-filter-products-in-stock"] = N("Filtrer les produits en stock", "Tu affiches seulement les produits commandables.", "Parcourir la liste et afficher uniquement les produits avec stock positif.", "`$products` contient Book stock 3, Pen stock 0, Mug stock 5.", "`Book` et `Mug` uniquement.", "Utiliser `foreach`, `if`, `$product[\"stock\"] > 0`, et ne pas modifier le tableau initial.", "<?php\n\n$products = [\n    [\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3],\n    [\"name\" => \"Pen\", \"price\" => 2, \"stock\" => 0],\n    [\"name\" => \"Mug\", \"price\" => 8, \"stock\" => 5],\n];\n\n// TODO: affiche seulement les produits en stock.\n", ["foreach", "if", "$product[\"stock\"] > 0", "$product[\"name\"]"], "<?php\n\n$products = [\n    [\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3],\n    [\"name\" => \"Pen\", \"price\" => 2, \"stock\" => 0],\n    [\"name\" => \"Mug\", \"price\" => 8, \"stock\" => 5],\n];\n\nforeach ($products as $product) {\n    if ($product[\"stock\"] > 0) {\n        echo $product[\"name\"];\n    }\n}", [("php-arrays", 1), ("php-loops", 1), ("php-conditions", 1)]),
        ["php-compute-cart-total"] = N("Calcul total panier", "Tu calcules le montant d'une commande.", "Additionner price * quantity pour chaque ligne du panier.", "`$cart` contient des lignes avec `price` et `quantity`.", "Le total numerique calcule.", "Utiliser `$total = 0`, `foreach`, multiplication et `+=`.", "<?php\n\n$cart = [\n    [\"price\" => 12, \"quantity\" => 2],\n    [\"price\" => 5, \"quantity\" => 3],\n];\n\n$total = 0;\n// TODO: calcule le total.\n", ["$total = 0", "foreach", "$line[\"price\"]", "$line[\"quantity\"]", "*", "+="], "<?php\n\n$cart = [\n    [\"price\" => 12, \"quantity\" => 2],\n    [\"price\" => 5, \"quantity\" => 3],\n];\n\n$total = 0;\nforeach ($cart as $line) {\n    $total += $line[\"price\"] * $line[\"quantity\"];\n}\n\necho $total;", [("php-arrays", 1), ("php-loops", 1), ("php-variables", 1)]),
        ["php-array-map-product-labels"] = N("Labels avec array_map", "Tu prepares des labels pour une API ou un affichage.", "Transformer chaque produit en label `Name - price euros`.", "`$products` liste des produits associatifs.", "`Book - 12 euros` dans les labels.", "Utiliser `array_map`, une arrow function `fn`, et lire name/price.", "<?php\n\n$products = [[\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3]];\n\n// TODO: cree $labels avec array_map.\n", ["array_map", "fn", "$product", "$product[\"name\"]", "$product[\"price\"]"], "<?php\n\n$products = [[\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3]];\n\n$labels = array_map(fn($product) => $product[\"name\"] . \" - \" . $product[\"price\"] . \" euros\", $products);\nprint_r($labels);", [("php-array-functions", 1), ("php-functional-arrays", 2), ("php-arrays", 1)]),
        ["php-array-filter-available"] = N("Disponibles avec array_filter", "Tu prepares la liste filtrée d'un catalogue.", "Obtenir uniquement les produits avec stock positif.", "`$products` contient des stocks positifs et nuls.", "Un tableau `$availableProducts` sans rupture.", "Utiliser `array_filter`, `fn` et `$product[\"stock\"] > 0`.", "<?php\n\n$products = [[\"name\" => \"Book\", \"stock\" => 3], [\"name\" => \"Pen\", \"stock\" => 0]];\n\n// TODO: cree $availableProducts.\n", ["array_filter", "fn", "$product[\"stock\"] > 0"], "<?php\n\n$products = [[\"name\" => \"Book\", \"stock\" => 3], [\"name\" => \"Pen\", \"stock\" => 0]];\n\n$availableProducts = array_filter($products, fn($product) => $product[\"stock\"] > 0);\nprint_r($availableProducts);", [("php-array-functions", 1), ("php-functional-arrays", 2), ("php-arrays", 1)]),
        ["php-array-reduce-total"] = N("Total avec array_reduce", "Tu calcules un panier sans accumulateur externe mutable.", "Utiliser `array_reduce` pour calculer le total.", "`$cart` contient price et quantity.", "Le total numerique.", "Utiliser `$carry`, `$line`, price, quantity et multiplication.", "<?php\n\n$cart = [[\"price\" => 12, \"quantity\" => 2]];\n\n// TODO: calcule $total avec array_reduce.\n", ["array_reduce", "$carry", "$line", "$line[\"price\"]", "$line[\"quantity\"]"], "<?php\n\n$cart = [[\"price\" => 12, \"quantity\" => 2]];\n\n$total = array_reduce($cart, fn($carry, $line) => $carry + $line[\"price\"] * $line[\"quantity\"], 0);\necho $total;", [("php-array-functions", 1), ("php-functional-arrays", 2), ("php-arrays", 1)]),
        ["php-sort-products-by-price"] = N("Tri par prix", "Tu ranges le catalogue du moins cher au plus cher.", "Trier une liste de produits avec `usort` sur le prix.", "`$products` contient des prix differents.", "Liste triee par prix croissant.", "Utiliser `usort`, `fn`, et comparer `$a[\"price\"] <=> $b[\"price\"]`.", "<?php\n\n$products = [[\"name\" => \"Book\", \"price\" => 12], [\"name\" => \"Pen\", \"price\" => 2]];\n\n// TODO: trie par prix.\n", ["usort", "fn", "$a[\"price\"] <=> $b[\"price\"]"], "<?php\n\n$products = [[\"name\" => \"Book\", \"price\" => 12], [\"name\" => \"Pen\", \"price\" => 2]];\n\nusort($products, fn($a, $b) => $a[\"price\"] <=> $b[\"price\"]);\nprint_r($products);", [("php-array-functions", 2), ("php-arrays", 1)], 35),

        ["php-function-why"] = N("Pourquoi les fonctions", "Tu as deux affichages produit qui se ressemblent.", "Montrer qu'une fonction evite de dupliquer un format.", "Deux produits Book et Pen.", "Une fonction reutilisable.", "Declarer une fonction et l'appeler au moins deux fois.", "<?php\n\n// TODO: cree une fonction de formatage et reutilise-la.\n", ["function", "return", "formatProduct", "formatProduct("], "<?php\n\nfunction formatProduct(string $name, int $price): string\n{\n    return $name . \" - \" . $price . \" euros\";\n}\n\necho formatProduct(\"Book\", 12);\necho formatProduct(\"Pen\", 2);", [("php-functions", 2), ("php-strings", 1)], 30),
        ["php-function-format-product"] = N("Formatter un produit", "Tu extrais le formatage d'une fiche produit.", "Creer `formatProduct(array $product): string`.", "`$product = [\"name\" => \"Book\", \"price\" => 12]`.", "`Book - 12 euros` retourne par la fonction.", "Utiliser une fonction typee, `return`, name et price.", "<?php\n\n$product = [\"name\" => \"Book\", \"price\" => 12];\n\nfunction formatProduct(array $product): string\n{\n    // TODO\n}\n", ["function formatProduct", "array $product", ": string", "return", "$product[\"name\"]", "$product[\"price\"]"], "<?php\n\n$product = [\"name\" => \"Book\", \"price\" => 12];\n\nfunction formatProduct(array $product): string\n{\n    return $product[\"name\"] . \" - \" . $product[\"price\"] . \" euros\";\n}\n\necho formatProduct($product);", [("php-functions", 2), ("php-arrays", 1)]),
        ["php-function-parameters"] = N("Parametres de fonction", "Tu veux formater n'importe quel produit sans dependance externe.", "Recevoir `$name` et `$price` en parametres.", "Nom et prix passes a l'appel.", "`Book - 12 euros`.", "Utiliser deux parametres types et les reutiliser dans le retour.", "<?php\n\nfunction formatProduct(string $name, int $price): string\n{\n    // TODO\n}\n", ["string $name", "int $price", "return", "$name", "$price"], "<?php\n\nfunction formatProduct(string $name, int $price): string\n{\n    return $name . \" - \" . $price . \" euros\";\n}", [("php-functions", 2), ("php-types", 1)], 30),
        ["php-function-return"] = N("Retour de fonction", "Tu veux reutiliser un label produit ailleurs qu'a l'ecran.", "Retourner une chaine avec `return` au lieu de faire seulement `echo`.", "`$product = [\"name\" => \"Book\"]`.", "Une chaine retournee.", "Utiliser `return` dans la fonction puis echo a l'exterieur.", "<?php\n\nfunction productLabel(array $product): string\n{\n    // TODO\n}\n\n// TODO: affiche le retour.\n", ["function productLabel", ": string", "return", "echo productLabel"], "<?php\n\nfunction productLabel(array $product): string\n{\n    return \"Product: \" . $product[\"name\"];\n}\n\necho productLabel([\"name\" => \"Book\"]);", [("php-functions", 2), ("php-arrays", 1)], 30),
        ["php-function-is-available"] = N("Fonction disponibilite", "Tu nommes la regle de stock.", "Creer `isAvailable(array $product): bool`.", "`$product` contient une cle stock.", "`true` si stock > 0.", "Retourner une comparaison, ne pas faire un echo.", "<?php\n\nfunction isAvailable(array $product): bool\n{\n    // TODO\n}\n", ["function isAvailable", "array $product", ": bool", "return", "$product[\"stock\"] > 0"], "<?php\n\nfunction isAvailable(array $product): bool\n{\n    return $product[\"stock\"] > 0;\n}", [("php-functions", 2), ("php-conditions", 1)]),
        ["php-function-calculate-total"] = N("Fonction total panier", "Tu rends le calcul panier reutilisable.", "Creer `calculateCartTotal(array $cart): float`.", "`$cart` contient des lignes price/quantity.", "Le total du panier retourne.", "Utiliser `$total`, `foreach`, multiplication et `return $total`.", "<?php\n\nfunction calculateCartTotal(array $cart): float\n{\n    $total = 0;\n    // TODO\n}\n", ["function calculateCartTotal", "array $cart", ": float", "foreach", "return $total"], "<?php\n\nfunction calculateCartTotal(array $cart): float\n{\n    $total = 0;\n    foreach ($cart as $line) {\n        $total += $line[\"price\"] * $line[\"quantity\"];\n    }\n\n    return $total;\n}", [("php-functions", 2), ("php-arrays", 1)]),
        ["php-function-default-parameters"] = N("Parametres par defaut", "Tu formates un prix avec devise optionnelle.", "Creer `formatPrice(float $price, string $currency = \"EUR\"): string`.", "`$price` et devise optionnelle.", "`12 EUR` par defaut.", "Utiliser un parametre par defaut et un retour string.", "<?php\n\nfunction formatPrice(float $price, string $currency = \"EUR\"): string\n{\n    // TODO\n}\n", ["function formatPrice", "float $price", "string $currency =", ": string"], "<?php\n\nfunction formatPrice(float $price, string $currency = \"EUR\"): string\n{\n    return $price . \" \" . $currency;\n}", [("php-functions", 2), ("php-types", 1)]),
        ["php-function-named-arguments"] = N("Arguments nommes", "Tu appelles une fabrique produit de facon lisible.", "Appeler `createProduct` avec `name:`, `price:`, `stock:`.", "Fonction `createProduct` disponible.", "Un produit cree avec les valeurs imposees.", "Utiliser les arguments nommes PHP 8.", "<?php\n\nfunction createProduct(string $name, float $price, int $stock): array { return compact(\"name\", \"price\", \"stock\"); }\n\n// TODO: appelle createProduct avec arguments nommes.\n", ["name:", "price:", "stock:"], "<?php\n\nfunction createProduct(string $name, float $price, int $stock): array { return compact(\"name\", \"price\", \"stock\"); }\n\n$product = createProduct(name: \"Book\", price: 12, stock: 3);", [("php-functions", 1), ("php-modern-types", 2)]),
        ["php-closure-discount"] = N("Closure de remise", "Tu prepares une regle de remise injectable.", "Creer `$applyDiscount` qui recoit un prix et retourne le prix remise.", "`$price = 120`.", "`108` apres remise de 10%.", "Utiliser une closure `function`, `return` et une multiplication.", "<?php\n\n// TODO: cree $applyDiscount.\n", ["$applyDiscount", "function", "return", "*"], "<?php\n\n$applyDiscount = function (float $price): float {\n    return $price * 0.9;\n};\n\necho $applyDiscount(120);", [("php-functions", 2), ("php-modern-types", 1)]),
        ["php-arrow-function-tax"] = N("Arrow function TVA", "Tu appliques la TVA a un prix catalogue.", "Creer une arrow function qui ajoute 20% de TVA.", "`$price = 10`.", "`12`.", "Utiliser `fn`, `=>` et `* 1.2`.", "<?php\n\n// TODO: cree $withTax.\n", ["fn", "=>", "* 1.2"], "<?php\n\n$withTax = fn(float $price): float => $price * 1.2;\necho $withTax(10);", [("php-functions", 1), ("php-modern-types", 2)]),
        ["php-function-refactor-duplicated-code"] = N("Refactorer du code duplique", "Tu vois le meme format produit dans deux pages.", "Extraire la logique repetee dans une fonction `formatProduct`.", "Deux tableaux produits.", "Une fonction appelee pour chaque produit.", "Declarer la fonction une fois, retourner le label, puis l'appeler deux fois.", "<?php\n\n$productA = [\"name\" => \"Book\", \"price\" => 12];\n$productB = [\"name\" => \"Pen\", \"price\" => 2];\n\n// TODO: evite la duplication.\n", ["function formatProduct", "return", "formatProduct($productA)", "formatProduct($productB)"], "<?php\n\n$productA = [\"name\" => \"Book\", \"price\" => 12];\n$productB = [\"name\" => \"Pen\", \"price\" => 2];\n\nfunction formatProduct(array $product): string\n{\n    return $product[\"name\"] . \" - \" . $product[\"price\"] . \" euros\";\n}\n\necho formatProduct($productA);\necho formatProduct($productB);", [("php-functions", 2), ("php-arrays", 1)], 35),

        ["php-strict-types"] = N("Strict types", "Tu prepares un fichier metier strict.", "Ajouter `declare(strict_types=1);` et une fonction typee.", "Prix produit numerique.", "Une valeur retournee par une fonction typee.", "Placer declare apres `<?php` et typer parametre/retour.", "<?php\n\n// TODO: active strict_types et cree une fonction typee.\n", ["declare(strict_types=1);", "function", ":"], "<?php\n\ndeclare(strict_types=1);\n\nfunction normalizePrice(float $price): float\n{\n    return $price;\n}", [("php-modern-types", 2), ("php-types", 1)]),
        ["php-scalar-type-hints"] = N("Types scalaires en parametres", "Tu veux clarifier les donnees d'une fonction metier.", "Typer les parametres `string`, `float` et `int`.", "Nom, prix et stock d'un produit.", "Signature claire.", "Utiliser des types scalaires dans la signature.", "<?php\n\n// TODO: type les parametres.\n", ["function createProduct", "string $name", "float $price", "int $stock"], "<?php\n\nfunction createProduct(string $name, float $price, int $stock): array\n{\n    return compact(\"name\", \"price\", \"stock\");\n}", [("php-modern-types", 2), ("php-types", 1)], 35),
        ["php-return-types"] = N("Types de retour", "Tu veux garantir ce que renvoie une fonction prix.", "Ajouter un type de retour `: float`.", "`$price = 10`.", "Nombre flottant retourne.", "Utiliser `: float` et `return`.", "<?php\n\nfunction withTax(float $price)\n{\n    // TODO\n}\n", ["function withTax", ": float", "return"], "<?php\n\nfunction withTax(float $price): float\n{\n    return $price * 1.2;\n}", [("php-modern-types", 2), ("php-functions", 1)], 35),
        ["php-nullable-types"] = N("Types nullable", "Tu recherches un produit qui peut ne pas exister.", "Creer `findProductName(int $id): ?string`.", "`$id` de produit.", "Un nom ou `null`.", "Utiliser `?string` et `return null`.", "<?php\n\nfunction findProductName(int $id): ?string\n{\n    // TODO\n}\n", ["?string", "return null"], "<?php\n\nfunction findProductName(int $id): ?string\n{\n    return $id === 1 ? \"Book\" : null;\n}", [("php-modern-types", 2), ("php-types", 1)]),
        ["php-union-types"] = N("Union types", "Tu acceptes un identifiant venant d'une URL ou d'une base.", "Creer une fonction qui accepte `int|string $id`.", "`$id` peut etre 12 ou \"SKU-12\".", "Identifiant normalise.", "Utiliser `int|string` dans la signature.", "<?php\n\nfunction normalizeProductId(int|string $id): string\n{\n    // TODO\n}\n", ["int|string", "$id"], "<?php\n\nfunction normalizeProductId(int|string $id): string\n{\n    return (string) $id;\n}", [("php-modern-types", 2), ("php-types", 1)]),
        ["php-match-expression"] = N("Expression match moderne", "Tu traduis un statut court en libelle lisible.", "Utiliser `match` comme expression qui retourne une valeur.", "`$status = \"paid\"`.", "`Payee`.", "Assigner le resultat de `match` dans `$label`.", "<?php\n\n$status = \"paid\";\n\n// TODO: cree $label avec match.\n", ["$label", "match", "=>", "default"], "<?php\n\n$status = \"paid\";\n$label = match ($status) {\n    \"paid\" => \"Payee\",\n    \"pending\" => \"En attente\",\n    default => \"Inconnu\",\n};\n\necho $label;", [("php-modern-types", 2), ("php-conditions", 1)], 35),
        ["php-readonly-product"] = N("Produit readonly", "Tu crees un DTO produit immuable.", "Creer une classe avec proprietes `readonly`.", "Nom et prix recus au constructeur.", "Objet dont le nom ne change plus apres construction.", "Utiliser `readonly`, `public`, `string $name`.", "<?php\n\nfinal class ProductView\n{\n    // TODO\n}\n", ["readonly", "public", "string $name"], "<?php\n\nfinal class ProductView\n{\n    public function __construct(public readonly string $name, public readonly float $price) {}\n}", [("php-readonly", 2), ("php-modern-types", 1), ("php-oop", 1)]),
        ["php-enum-order-status"] = N("Enum OrderStatus", "Tu modelises les statuts d'une commande.", "Creer `enum OrderStatus: string` avec Pending, Paid, Cancelled.", "Statuts metier imposes.", "Une enum PHP valide.", "Utiliser les trois cases demandees.", "<?php\n\n// TODO: declare enum OrderStatus.\n", ["enum OrderStatus: string", "case Pending", "case Paid", "case Cancelled"], "<?php\n\nenum OrderStatus: string\n{\n    case Pending = \"pending\";\n    case Paid = \"paid\";\n    case Cancelled = \"cancelled\";\n}", [("php-enums", 2), ("php-modern-types", 1)]),
        ["php-match-with-enum"] = N("Match avec enum", "Tu transformes une enum en message client.", "Utiliser `match` avec `OrderStatus::Paid`.", "`$status = OrderStatus::Paid`.", "`Commande payee`.", "Couvrir Pending, Paid et default.", "<?php\n\n$status = OrderStatus::Paid;\n\n// TODO: cree $message avec match.\n", ["match", "OrderStatus::Paid", "OrderStatus::Pending", "default"], "<?php\n\n$status = OrderStatus::Paid;\n$message = match ($status) {\n    OrderStatus::Pending => \"Commande en attente\",\n    OrderStatus::Paid => \"Commande payee\",\n    default => \"Statut inconnu\",\n};", [("php-enums", 1), ("php-modern-types", 2)]),
        ["php-date-time-immutable"] = N("DateTimeImmutable", "Tu dates une commande sans muter l'objet.", "Creer une date de commande et la formatter.", "Date courante.", "Une date formatee.", "Utiliser `DateTimeImmutable` et `format`.", "<?php\n\n// TODO: cree $orderedAt et affiche la date.\n", ["DateTimeImmutable", "format"], "<?php\n\n$orderedAt = new DateTimeImmutable();\necho $orderedAt->format(\"Y-m-d\");", [("php-date-time", 2), ("php-modern-types", 1)]),
        ["php-string-functions-clean-input"] = N("Nettoyage de saisie", "Tu nettoies une recherche utilisateur.", "Nettoyer une saisie avec `trim`, `strtolower`, `str_contains`.", "`$search = \"  BOOK  \"`.", "Recherche normalisee et testee.", "Utiliser les trois fonctions demandees.", "<?php\n\n$search = \"  BOOK  \";\n\n// TODO: nettoie puis teste la saisie.\n", ["trim", "strtolower", "str_contains"], "<?php\n\n$search = \"  BOOK  \";\n$normalized = strtolower(trim($search));\n$containsBook = str_contains($normalized, \"book\");", [("php-strings", 2), ("php-functions", 1)]),
        ["php-null-coalescing"] = N("Null coalescing", "Tu lis un parametre optionnel de recherche.", "Utiliser `??` pour fournir une valeur par defaut.", "`$_GET[\"search\"]` peut manquer.", "Recherche vide par defaut.", "Utiliser `$_GET`, `??` et `$search`.", "<?php\n\n// TODO: lis search avec valeur par defaut.\n", ["$_GET", "??", "$search"], "<?php\n\n$search = $_GET[\"search\"] ?? \"\";\necho $search;", [("php-modern-types", 1), ("php-http", 1)], 35),
        ["php-spread-operator"] = N("Spread operator", "Tu ajoutes un produit a une liste sans modifier l'originale.", "Utiliser `...` pour creer un nouveau tableau.", "`$products` contient Book.", "Nouveau tableau avec Book et Pen.", "Utiliser `...$products` dans un tableau.", "<?php\n\n$products = [[\"name\" => \"Book\"]];\n$newProduct = [\"name\" => \"Pen\"];\n\n// TODO: cree $updatedProducts.\n", ["...$products", "$updatedProducts", "$newProduct"], "<?php\n\n$products = [[\"name\" => \"Book\"]];\n$newProduct = [\"name\" => \"Pen\"];\n\n$updatedProducts = [...$products, $newProduct];\nprint_r($updatedProducts);", [("php-modern-types", 1), ("php-arrays", 1)], 35),

        ["php-oop-why-objects"] = N("Pourquoi les objets", "Tu veux eviter de disperser les regles produit dans des tableaux partout.", "Expliquer la brique `Product` avec une classe simple.", "Nom, prix et stock d'un produit.", "Classe Product comme modele metier.", "Declarer une classe Product et montrer qu'elle regroupe les donnees.", "<?php\n\n// TODO: cree le modele Product minimal.\n", ["class Product", "private", "__construct"], "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name, private float $price, private int $stock) {}\n}", [("php-oop", 2), ("php-project-structure", 1)], 35),
        ["php-oop-product-class"] = N("Classe Product professionnelle", "Tu poses le modele objet du catalogue.", "Creer `final class Product` avec name, price, stock.", "Donnees produit typees.", "Classe avec proprietes privees.", "Utiliser les trois proprietes typees et privees.", "<?php\n\n// TODO: declare Product.\n", ["final class Product", "private string $name", "private float $price", "private int $stock"], "<?php\n\nfinal class Product\n{\n    private string $name;\n    private float $price;\n    private int $stock;\n}", [("php-oop", 2), ("php-types", 1)]),
        ["php-oop-properties"] = N("Proprietes Product", "Tu ajoutes les donnees internes du produit.", "Declarer des proprietes privees typees.", "name, price, stock.", "Etat interne protege.", "Utiliser `private` et des types scalaires.", "<?php\n\nfinal class Product\n{\n    // TODO: proprietes\n}\n", ["private string $name", "private float $price", "private int $stock"], "<?php\n\nfinal class Product\n{\n    private string $name;\n    private float $price;\n    private int $stock;\n}", [("php-oop", 2), ("php-types", 1)], 35),
        ["php-oop-constructor"] = N("Constructeur Product", "Tu veux creer un produit toujours initialise.", "Ajouter `__construct` pour recevoir name, price et stock.", "Trois valeurs obligatoires.", "Constructeur complet.", "Utiliser `__construct` et assigner les proprietes.", "<?php\n\nfinal class Product\n{\n    private string $name;\n    private float $price;\n    private int $stock;\n\n    // TODO: constructeur\n}\n", ["__construct", "$this->name", "$this->price", "$this->stock"], "<?php\n\nfinal class Product\n{\n    private string $name;\n    private float $price;\n    private int $stock;\n\n    public function __construct(string $name, float $price, int $stock)\n    {\n        $this->name = $name;\n        $this->price = $price;\n        $this->stock = $stock;\n    }\n}", [("php-oop", 2), ("php-functions", 1)], 35),
        ["php-oop-constructor-promotion"] = N("Constructeur promu", "Tu initialises Product proprement.", "Creer un constructeur avec promotion de proprietes.", "name, price, stock recus au constructeur.", "Objet Product initialise.", "Utiliser `public function __construct` et des parametres promus prives.", "<?php\n\nfinal class Product\n{\n    // TODO\n}\n", ["public function __construct", "private string $name", "private float $price", "private int $stock"], "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name, private float $price, private int $stock) {}\n}", [("php-oop", 2), ("php-modern-types", 1)]),
        ["php-oop-getters"] = N("Getters Product", "Tu exposes Product sans casser l'encapsulation.", "Ajouter `getName`, `getPrice`, `getStock`.", "Proprietes privees existantes.", "Valeurs accessibles par methodes.", "Chaque getter doit etre public et type.", "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name, private float $price, private int $stock) {}\n\n    // TODO: getters\n}\n", ["getName(): string", "getPrice(): float", "getStock(): int"], "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name, private float $price, private int $stock) {}\n    public function getName(): string { return $this->name; }\n    public function getPrice(): float { return $this->price; }\n    public function getStock(): int { return $this->stock; }\n}", [("php-oop", 2), ("php-functions", 1)]),
        ["php-oop-business-method"] = N("Methode metier Product", "Tu places la disponibilite dans le domaine.", "Ajouter `isAvailable(): bool`.", "Product contient `stock`.", "`true` si stock > 0.", "Utiliser `$this->stock > 0`.", "<?php\n\nfinal class Product\n{\n    public function __construct(private int $stock) {}\n\n    // TODO\n}\n", ["function isAvailable", ": bool", "$this->stock > 0"], "<?php\n\nfinal class Product\n{\n    public function __construct(private int $stock) {}\n    public function isAvailable(): bool { return $this->stock > 0; }\n}", [("php-oop", 2), ("php-conditions", 1)]),
        ["php-oop-encapsulation"] = N("Encapsulation", "Tu proteges l'etat interne de Product.", "Garder les proprietes privees et exposer des getters.", "Proprietes name et price.", "Acces controle par methodes.", "Utiliser `private` et `public function getName`.", "<?php\n\nfinal class Product\n{\n    // TODO: encapsule name.\n}\n", ["private string $name", "public function getName", "return $this->name"], "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name) {}\n\n    public function getName(): string\n    {\n        return $this->name;\n    }\n}", [("php-oop", 2), ("php-functions", 1)], 40),
        ["php-oop-guard-negative-price"] = N("Garde prix negatif", "Tu proteges les invariants produit.", "Lever une exception si le prix est negatif.", "`$price` recu au constructeur.", "Exception pour prix invalide.", "Utiliser `if`, `$price < 0`, `throw new`, `InvalidArgumentException`.", "<?php\n\nfinal class Product\n{\n    public function __construct(private float $price)\n    {\n        // TODO\n    }\n}\n", ["if", "$price < 0", "throw new", "InvalidArgumentException"], "<?php\n\nfinal class Product\n{\n    public function __construct(private float $price)\n    {\n        if ($price < 0) {\n            throw new InvalidArgumentException(\"Price cannot be negative\");\n        }\n    }\n}", [("php-exceptions", 2), ("php-oop", 1)]),
        ["php-oop-custom-exception"] = N("Exception metier prix", "Tu nommes explicitement une erreur domaine.", "Creer `InvalidProductPriceException`.", "Aucune entree runtime.", "Classe exception dediee.", "Etendre `Exception` ou `InvalidArgumentException`.", "<?php\n\n// TODO: declare l'exception metier.\n", ["class InvalidProductPriceException", "extends", "Exception||InvalidArgumentException"], "<?php\n\nclass InvalidProductPriceException extends InvalidArgumentException\n{\n}", [("php-exceptions", 2), ("php-oop", 1)]),
        ["php-oop-interface"] = N("Interface repository", "Tu definis le contrat de lecture catalogue.", "Creer `ProductRepositoryInterface` avec `findAvailable(): array`.", "Aucune implementation encore.", "Interface utilisable par un service.", "Utiliser `interface` et une methode publique typee.", "<?php\n\n// TODO: interface repository.\n", ["interface ProductRepositoryInterface", "public function findAvailable", ": array"], "<?php\n\ninterface ProductRepositoryInterface\n{\n    public function findAvailable(): array;\n}", [("php-interfaces", 2), ("php-oop", 1)]),
        ["php-oop-implementation"] = N("Repository en memoire", "Tu fournis une implementation testable du repository.", "Creer `InMemoryProductRepository implements ProductRepositoryInterface`.", "`$products` en memoire.", "Produits disponibles retournes.", "Utiliser `implements`, `findAvailable` et `array_filter`.", "<?php\n\nfinal class InMemoryProductRepository implements ProductRepositoryInterface\n{\n    // TODO\n}\n", ["implements ProductRepositoryInterface", "findAvailable", "array_filter"], "<?php\n\nfinal class InMemoryProductRepository implements ProductRepositoryInterface\n{\n    public function __construct(private array $products) {}\n    public function findAvailable(): array\n    {\n        return array_filter($this->products, fn(Product $product) => $product->isAvailable());\n    }\n}", [("php-interfaces", 1), ("php-oop", 1), ("php-array-functions", 1)]),
        ["php-oop-service-composition"] = N("Service par composition", "Tu separes cas d'usage et stockage.", "Creer `ProductCatalogService` qui recoit `ProductRepositoryInterface`.", "Repository injecte.", "Service deleguant au repository.", "Utiliser `__construct`, le type interface et `$this->`.", "<?php\n\nfinal class ProductCatalogService\n{\n    // TODO\n}\n", ["ProductCatalogService", "__construct", "ProductRepositoryInterface", "$this->"], "<?php\n\nfinal class ProductCatalogService\n{\n    public function __construct(private ProductRepositoryInterface $products) {}\n    public function listAvailableProducts(): array\n    {\n        return $this->products->findAvailable();\n    }\n}", [("php-composition", 2), ("php-interfaces", 1)]),
        ["php-oop-abstract-class"] = N("Classe abstraite formatter", "Tu prepares une famille de formatters.", "Creer `AbstractFormatter` avec methode abstraite `format`.", "Aucune donnee concrete.", "Contrat partiellement factorise.", "Utiliser `abstract class` et `abstract public function format`.", "<?php\n\n// TODO\n", ["abstract class", "abstract public function format"], "<?php\n\nabstract class AbstractFormatter\n{\n    abstract public function format(Product $product): string;\n}", [("php-inheritance", 2), ("php-oop", 1)]),
        ["php-oop-trait"] = N("Trait Timestampable", "Tu factorises une date de creation.", "Creer `TimestampableTrait`.", "Date de creation.", "Trait avec DateTimeImmutable.", "Utiliser `trait`, `createdAt`, `DateTimeImmutable`.", "<?php\n\n// TODO\n", ["trait TimestampableTrait", "createdAt", "DateTimeImmutable"], "<?php\n\ntrait TimestampableTrait\n{\n    private DateTimeImmutable $createdAt;\n\n    public function markCreated(): void\n    {\n        $this->createdAt = new DateTimeImmutable();\n    }\n}", [("php-oop", 1), ("php-date-time", 1)]),
        ["php-oop-try-catch"] = N("try/catch produit", "Tu geres une operation produit qui peut echouer.", "Encadrer une operation avec `try`, traiter l'erreur avec `catch`, puis executer un nettoyage avec `finally`.", "Une exception peut etre lancee pendant l'operation.", "Message d'erreur gere puis bloc final execute.", "Utiliser `try`, `catch` et `finally` dans de vraies instructions PHP.", "<?php\n\n// TODO: ajoute try, catch et finally.\n", ["try", "catch", "finally"], "<?php\n\ntry {\n    throw new Exception(\"Error\");\n} catch (Exception $exception) {\n    echo $exception->getMessage();\n} finally {\n    echo \"Done\";\n}", [("php-exceptions", 2), ("php-oop", 1)]),
        ["php-oop-composition-over-inheritance"] = N("Composition plutot qu'heritage", "Tu assembles un service avec un repository au lieu d'heriter de lui.", "Recevoir un collaborateur par constructeur.", "Repository produit.", "Service compose.", "Utiliser une propriete privee repository et deleguer un appel.", "<?php\n\nfinal class ProductCatalogService\n{\n    // TODO: compose avec un repository.\n}\n", ["__construct", "private ProductRepositoryInterface", "$this->products"], "<?php\n\nfinal class ProductCatalogService\n{\n    public function __construct(private ProductRepositoryInterface $products) {}\n\n    public function listAvailableProducts(): array\n    {\n        return $this->products->findAvailable();\n    }\n}", [("php-composition", 2), ("php-interfaces", 1)], 45),

        ["php-project-structure-src-public"] = N("Structure src/public", "Tu separes le code metier et le point d'entree web.", "Lister `src/`, `public/` et `composer.json`.", "Projet PHP natif.", "Structure minimale.", "Faire apparaitre les dossiers attendus.", "// TODO: indique la structure minimale.\n", ["src/", "public/", "composer.json"], "src/\npublic/\ncomposer.json", [("php-project-structure", 2), ("php-composer", 1)], 35),
        ["php-namespace-app-domain"] = N("Namespace App\\Domain", "Tu ranges Product dans le domaine applicatif.", "Creer une classe Product dans `namespace App\\Domain`.", "Fichier `src/Domain/Product.php` suppose.", "Classe Product namespacee.", "Declarer le namespace avant la classe.", "<?php\n\n// TODO: namespace et classe Product.\n", ["namespace App\\Domain", "class Product"], "<?php\n\nnamespace App\\Domain;\n\nclass Product\n{\n}", [("php-namespaces", 2), ("php-project-structure", 1)]),
        ["php-use-import"] = N("Import use", "Tu consommes Product depuis un autre namespace.", "Importer `App\\Domain\\Product` avec `use`.", "Classe Product existante.", "Instanciation avec `new Product`.", "Utiliser `use App\\Domain\\Product` puis `new Product`.", "<?php\n\nnamespace App\\Controller;\n\n// TODO: import et instanciation.\n", ["use App\\Domain\\Product", "new Product"], "<?php\n\nnamespace App\\Controller;\n\nuse App\\Domain\\Product;\n\n$product = new Product();", [("php-namespaces", 2), ("php-oop", 1)]),
        ["php-composer-json-minimal"] = N("composer.json PSR-4", "Tu initialises un petit projet PHP.", "Creer un `composer.json` minimal avec autoload PSR-4.", "Namespace `App\\` et dossier `src/`.", "JSON Composer valide.", "Contenir `autoload`, `psr-4`, `App\\\\` et `src/`.", "{\n  \"autoload\": {\n    // TODO\n  }\n}\n", ["\"autoload\"", "\"psr-4\"", "\"App\\\\\\\\\"", "\"src/\""], "{\n  \"autoload\": {\n    \"psr-4\": {\n      \"App\\\\\": \"src/\"\n    }\n  }\n}", [("php-composer", 2), ("php-autoload", 1)]),
        ["php-composer-autoload-psr4"] = N("Autoload Composer PSR-4", "Tu relies le namespace App au dossier src.", "Configurer `autoload.psr-4` dans composer.json.", "Namespace App et dossier src.", "Autoload coherent.", "Utiliser `autoload`, `psr-4`, `App\\\\` et `src/`.", "{\n  // TODO: autoload\n}\n", ["\"autoload\"", "\"psr-4\"", "\"App\\\\\\\\\"", "\"src/\""], "{\n  \"autoload\": {\n    \"psr-4\": {\n      \"App\\\\\": \"src/\"\n    }\n  }\n}", [("php-autoload", 2), ("php-composer", 1)], 40),
        ["php-psr4-directory-mapping"] = N("Mapping PSR-4", "Tu verifies la correspondance namespace/fichier.", "Associer `App\\Service\\ProductService` a `src/Service/ProductService.php`.", "Namespace et chemin imposes.", "Correspondance explicite.", "Mentionner `App\\Service` et `src/Service`.", "// TODO: indique le mapping PSR-4 attendu.\n", ["App\\Service", "src/Service"], "// App\\Service\\ProductService => src/Service/ProductService.php", [("php-autoload", 2), ("php-project-structure", 1)]),
        ["php-psr12-class-style"] = N("Style PSR-12", "Tu remets une classe dans une structure lisible.", "Corriger une classe avec namespace, use, class et methodes.", "Classe ProductService.", "Structure PSR-12 simple.", "Inclure `namespace`, `use`, `final class`, accolades.", "<?php\n\n// TODO: structure PSR-12.\n", ["namespace", "use", "final class", "{", "}"], "<?php\n\nnamespace App\\Service;\n\nuse App\\Domain\\Product;\n\nfinal class ProductService\n{\n    public function format(Product $product): string\n    {\n        return $product->getName();\n    }\n}", [("php-standards", 2), ("php-namespaces", 1)]),
        ["php-composer-require-package"] = N("composer require", "Tu ajoutes une dependance de test au projet.", "Declarer une dependance Composer dans `require-dev`.", "Package phpunit/phpunit.", "Bloc require-dev lisible.", "Utiliser `require-dev` et le nom du package.", "{\n  // TODO: dependance de test\n}\n", ["\"require-dev\"", "\"phpunit/phpunit\""], "{\n  \"require-dev\": {\n    \"phpunit/phpunit\": \"^10.0\"\n  }\n}", [("php-composer", 2), ("php-standards", 1)], 40),
        ["php-composer-scripts"] = N("Scripts Composer", "Tu ajoutes une commande de verification projet.", "Ajouter un script Composer `test`.", "composer.json existant.", "Bloc scripts avec test.", "Utiliser `\"scripts\"` et `\"test\"`.", "{\n  // TODO: scripts\n}\n", ["\"scripts\"", "\"test\""], "{\n  \"scripts\": {\n    \"test\": \"phpunit\"\n  }\n}", [("php-composer", 2), ("php-standards", 1)]),
        ["php-autoload-require-vendor"] = N("Charger vendor/autoload.php", "Tu demarres le point d'entree public du catalogue.", "Inclure l'autoloader Composer avec `require`.", "Fichier `vendor/autoload.php` genere par Composer.", "Classes App chargeables.", "Utiliser `require` et `vendor/autoload.php`.", "<?php\n\n// TODO: charge l'autoloader.\n", ["require", "vendor/autoload.php"], "<?php\n\nrequire __DIR__ . '/../vendor/autoload.php';", [("php-autoload", 2), ("php-composer", 1)], 40),

        ["php-http-request-response"] = N("Requete et reponse HTTP", "Tu observes le cycle minimal d'une page PHP.", "Lire une entree HTTP puis produire une reponse texte.", "URL `/products` simulee.", "Reponse Product Catalog.", "Utiliser `$_SERVER`, `REQUEST_URI` et `echo`.", "<?php\n\n// TODO: lis l'URI puis reponds.\n", ["$_SERVER", "REQUEST_URI", "echo"], "<?php\n\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/products\";\necho \"Product Catalog: \" . $uri;", [("php-http", 2), ("php-strings", 1)], 35),
        ["php-get-query-param"] = N("Parametre GET", "Tu lis un filtre de recherche.", "Lire `$_GET[\"search\"]` avec valeur par defaut.", "Requete `/products?search=book` simulee.", "Une variable `$search`.", "Utiliser `$_GET`, `??` et `\"search\"`.", "<?php\n\n// TODO: lis search.\n", ["$_GET", "??", "\"search\""], "<?php\n\n$search = $_GET[\"search\"] ?? \"\";\necho $search;", [("php-http", 2), ("php-variables", 1)]),
        ["php-post-form-data"] = N("Donnees POST", "Tu lis un formulaire produit.", "Lire `$_POST[\"name\"]` et `$_POST[\"price\"]`.", "Formulaire simule.", "Variables name et price.", "Utiliser `$_POST`, `\"name\"`, `\"price\"`.", "<?php\n\n// TODO: lis name et price.\n", ["$_POST", "\"name\"", "\"price\""], "<?php\n\n$name = $_POST[\"name\"] ?? \"\";\n$price = $_POST[\"price\"] ?? 0;", [("php-http", 2), ("php-variables", 1)]),
        ["php-server-request-method"] = N("Methode HTTP", "Tu distingues creation et lecture.", "Tester `$_SERVER[\"REQUEST_METHOD\"] === \"POST\"`.", "Serveur HTTP simule.", "Branche POST detectee.", "Utiliser `$_SERVER`, `REQUEST_METHOD`, `POST`.", "<?php\n\n// TODO: teste la methode.\n", ["$_SERVER", "REQUEST_METHOD", "POST"], "<?php\n\nif ($_SERVER[\"REQUEST_METHOD\"] === \"POST\") {\n    echo \"Create product\";\n}", [("php-http", 2), ("php-conditions", 1)]),
        ["php-server-request-uri"] = N("URI de requete", "Tu veux savoir quelle route a ete demandee.", "Lire `$_SERVER[\"REQUEST_URI\"]` avec une valeur par defaut.", "Serveur HTTP simule.", "Variable `$uri`.", "Utiliser `REQUEST_URI` et `??`.", "<?php\n\n// TODO: lis l'URI.\n", ["$_SERVER", "REQUEST_URI", "??", "$uri"], "<?php\n\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/\";\necho $uri;", [("php-http", 2), ("php-variables", 1)], 35),
        ["php-session-cart"] = N("Panier en session", "Tu initialises un panier visiteur.", "Demarrer une session et initialiser `$_SESSION[\"cart\"]`.", "Session PHP.", "Panier tableau initialise.", "Utiliser `session_start`, `$_SESSION`, `\"cart\"`.", "<?php\n\n// TODO: session et panier.\n", ["session_start", "$_SESSION", "\"cart\""], "<?php\n\nsession_start();\n$_SESSION[\"cart\"] = $_SESSION[\"cart\"] ?? [];", [("php-sessions", 2), ("php-http", 1)]),
        ["php-cookie-theme"] = N("Cookie theme", "Tu memorises une preference d'interface.", "Creer un cookie `theme` avec valeur `dark`.", "Navigateur HTTP.", "Cookie envoye.", "Utiliser `setcookie`, `\"theme\"`, `\"dark\"`.", "<?php\n\n// TODO: cree le cookie.\n", ["setcookie", "\"theme\"", "\"dark\""], "<?php\n\nsetcookie(\"theme\", \"dark\");", [("php-cookies", 2), ("php-http", 1)]),
        ["php-file-upload-validation"] = N("Validation upload", "Tu verifies qu'un fichier produit a ete envoye.", "Lire un fichier uploadé et verifier qu'il existe.", "`$_FILES[\"image\"]`.", "Branche fichier present.", "Utiliser `$_FILES`, `isset`, `tmp_name`.", "<?php\n\n// TODO: verifie le fichier uploadé.\n", ["$_FILES", "isset", "tmp_name"], "<?php\n\nif (isset($_FILES[\"image\"]) && isset($_FILES[\"image\"][\"tmp_name\"])) {\n    echo $_FILES[\"image\"][\"tmp_name\"];\n}", [("php-http", 2), ("php-files", 1)]),
        ["php-json-response-native"] = N("Reponse JSON native", "Tu exposes un produit a un client HTTP.", "Retourner une reponse JSON avec un produit.", "`$product = [\"name\" => \"Book\"]`.", "JSON produit.", "Utiliser `header`, `Content-Type: application/json`, `json_encode`.", "<?php\n\n$product = [\"name\" => \"Book\", \"price\" => 12];\n\n// TODO: header JSON et encodage.\n", ["header", "Content-Type: application/json", "json_encode"], "<?php\n\n$product = [\"name\" => \"Book\", \"price\" => 12];\nheader(\"Content-Type: application/json\");\necho json_encode($product);", [("php-http", 1), ("php-json", 2)]),
        ["php-http-status-code"] = N("Code HTTP", "Tu signales qu'un produit n'existe pas.", "Envoyer un code HTTP 404 avant une reponse JSON.", "Produit absent.", "Erreur JSON avec statut 404.", "Utiliser `http_response_code`, `404` et `json_encode`.", "<?php\n\n// TODO: retourne une erreur 404.\n", ["http_response_code", "404", "json_encode"], "<?php\n\nhttp_response_code(404);\necho json_encode([\"error\" => \"Product not found\"]);", [("php-http", 2), ("php-json", 1)], 40),
        ["php-basic-router"] = N("Mini-router natif", "Tu routes une API PHP minimale.", "Creer un mini-router basé sur `$_SERVER[\"REQUEST_URI\"]`.", "URI `/products`.", "Route products reconnue.", "Utiliser `REQUEST_URI`, `match` ou `switch`, `/products`.", "<?php\n\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/\";\n\n// TODO: route /products.\n", ["REQUEST_URI", "match||switch", "/products"], "<?php\n\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/\";\n$response = match ($uri) {\n    \"/products\" => [\"products\" => []],\n    default => [\"error\" => \"not_found\"],\n};\necho json_encode($response);", [("php-http", 2), ("php-conditions", 1)]),
        ["php-route-products-get-post"] = N("Route GET/POST /products", "Tu separes lecture et creation de produits.", "Router `GET /products` et `POST /products`.", "Methodes HTTP et URI.", "Deux branches routees.", "Utiliser `REQUEST_METHOD`, `REQUEST_URI`, `/products`, `GET`, `POST`.", "<?php\n\n$method = $_SERVER[\"REQUEST_METHOD\"] ?? \"GET\";\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/products\";\n\n// TODO: route GET et POST.\n", ["REQUEST_METHOD", "REQUEST_URI", "/products", "GET", "POST"], "<?php\n\n$method = $_SERVER[\"REQUEST_METHOD\"] ?? \"GET\";\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/products\";\n\nif ($uri === \"/products\" && $method === \"GET\") {\n    echo json_encode([\"products\" => []]);\n} elseif ($uri === \"/products\" && $method === \"POST\") {\n    echo json_encode([\"created\" => true]);\n}", [("php-http", 2), ("php-json", 1)], 45),

        ["php-json-encode-products"] = N("Encoder produits JSON", "Tu prepares une reponse API.", "Transformer une liste de produits en JSON.", "`$products` tableau associatif.", "JSON des produits.", "Utiliser `json_encode` et `$products`.", "<?php\n\n$products = [[\"name\" => \"Book\"]];\n\n// TODO: encode en JSON.\n", ["json_encode", "$products"], "<?php\n\n$products = [[\"name\" => \"Book\"]];\necho json_encode($products);", [("php-json", 2), ("php-arrays", 1)]),
        ["php-json-decode-products"] = N("Decoder produits JSON", "Tu recuperes des produits depuis une chaine JSON.", "Decoder une chaine JSON en tableau associatif.", "`$json = '[{\"name\":\"Book\"}]'`.", "Tableau associatif PHP.", "Utiliser `json_decode` avec `true`.", "<?php\n\n$json = '[{\"name\":\"Book\"}]';\n\n// TODO: decode en tableau associatif.\n", ["json_decode", "true"], "<?php\n\n$json = '[{\"name\":\"Book\"}]';\n$products = json_decode($json, true);", [("php-json", 2), ("php-arrays", 1)]),
        ["php-file-put-contents"] = N("Ecrire products.json", "Tu persistes un catalogue simple.", "Sauvegarder du JSON dans `products.json`.", "`$products` tableau.", "Fichier JSON ecrit.", "Utiliser `file_put_contents`, `products.json`, `json_encode`.", "<?php\n\n$products = [[\"name\" => \"Book\"]];\n\n// TODO: sauvegarde le JSON.\n", ["file_put_contents", "products.json", "json_encode"], "<?php\n\n$products = [[\"name\" => \"Book\"]];\nfile_put_contents(\"products.json\", json_encode($products));", [("php-files", 2), ("php-json", 1)]),
        ["php-file-get-contents"] = N("Lire products.json", "Tu relis un catalogue stocke en fichier.", "Lire `products.json` puis decoder le JSON.", "Fichier products.json.", "Tableau `$products`.", "Utiliser `file_get_contents` et `json_decode`.", "<?php\n\n// TODO: lis et decode products.json.\n", ["file_get_contents", "json_decode"], "<?php\n\n$json = file_get_contents(\"products.json\");\n$products = json_decode($json, true);", [("php-files", 2), ("php-json", 1)]),
        ["php-file-products-repository"] = N("Repository fichier JSON", "Tu encapsules la lecture fichier dans un repository.", "Creer une classe qui charge products.json.", "Fichier JSON de produits.", "Methode `findAll`.", "Utiliser `class`, `file_get_contents`, `json_decode` et `return`.", "<?php\n\nfinal class FileProductRepository\n{\n    // TODO: findAll\n}\n", ["class FileProductRepository", "file_get_contents", "json_decode", "return"], "<?php\n\nfinal class FileProductRepository\n{\n    public function findAll(): array\n    {\n        $json = file_get_contents(\"products.json\");\n        return json_decode($json, true);\n    }\n}", [("php-files", 1), ("php-json", 1), ("php-oop", 1)], 45),
        ["php-pdo-connection"] = N("Connexion PDO", "Tu prepares l'acces base de donnees.", "Creer une connexion PDO.", "`$dsn`, `$username`, `$password`.", "Objet PDO.", "Utiliser `new PDO`, `$dsn`, `$username`, `$password`.", "<?php\n\n$dsn = \"mysql:host=localhost;dbname=app\";\n$username = \"app\";\n$password = \"secret\";\n\n// TODO: cree PDO.\n", ["new PDO", "$dsn", "$username", "$password"], "<?php\n\n$dsn = \"mysql:host=localhost;dbname=app\";\n$username = \"app\";\n$password = \"secret\";\n$pdo = new PDO($dsn, $username, $password);", [("php-pdo", 2), ("php-project-structure", 1)]),
        ["php-pdo-exception-mode"] = N("Mode exception PDO", "Tu veux que les erreurs SQL soient visibles et capturables.", "Configurer PDO avec `PDO::ERRMODE_EXCEPTION`.", "Objet `$pdo`.", "Exceptions SQL actives.", "Utiliser `setAttribute`, `PDO::ATTR_ERRMODE`, `PDO::ERRMODE_EXCEPTION`.", "<?php\n\n// TODO: configure le mode erreur.\n", ["setAttribute", "PDO::ATTR_ERRMODE", "PDO::ERRMODE_EXCEPTION"], "<?php\n\n$pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);", [("php-pdo", 2), ("php-exceptions", 1)], 45),
        ["php-pdo-prepared-select"] = N("SELECT prepare PDO", "Tu lis un produit par identifiant sans concatener SQL.", "Preparer une requete SELECT avec parametre `:id`.", "`$id = 1`.", "Produit lu avec `fetch`.", "Utiliser `prepare`, `:id`, `execute`, `fetch`.", "<?php\n\n$id = 1;\n\n// TODO: prepare, execute, fetch.\n", ["prepare", ":id", "execute", "fetch"], "<?php\n\n$id = 1;\n$statement = $pdo->prepare(\"SELECT * FROM products WHERE id = :id\");\n$statement->execute([\"id\" => $id]);\n$product = $statement->fetch();", [("php-pdo", 2), ("php-http", 1)]),
        ["php-pdo-insert-product"] = N("INSERT prepare PDO", "Tu ajoutes un produit en base.", "Inserer un produit avec requete preparee.", "`name`, `price`.", "Produit insere.", "Utiliser `prepare`, `INSERT INTO products`, `:name`, `:price`, `execute`.", "<?php\n\n$name = \"Book\";\n$price = 12;\n\n// TODO: insert prepare.\n", ["prepare", "INSERT INTO products", ":name", ":price", "execute"], "<?php\n\n$name = \"Book\";\n$price = 12;\n$statement = $pdo->prepare(\"INSERT INTO products (name, price) VALUES (:name, :price)\");\n$statement->execute([\"name\" => $name, \"price\" => $price]);", [("php-pdo", 2), ("php-arrays", 1)]),
        ["php-pdo-update-stock"] = N("UPDATE stock PDO", "Tu mets a jour le stock apres une commande.", "Preparer une requete UPDATE avec `:id` et `:stock`.", "id produit et nouveau stock.", "Stock mis a jour.", "Utiliser `prepare`, `UPDATE products`, `:stock`, `:id`, `execute`.", "<?php\n\n$id = 1;\n$stock = 2;\n\n// TODO: update prepare.\n", ["prepare", "UPDATE products", ":stock", ":id", "execute"], "<?php\n\n$id = 1;\n$stock = 2;\n$statement = $pdo->prepare(\"UPDATE products SET stock = :stock WHERE id = :id\");\n$statement->execute([\"stock\" => $stock, \"id\" => $id]);", [("php-pdo", 2), ("php-variables", 1)], 50),
        ["php-pdo-delete-product"] = N("DELETE produit PDO", "Tu supprimes un produit par identifiant.", "Preparer une requete DELETE avec `:id`.", "`$id = 1`.", "Produit supprime.", "Utiliser `prepare`, `DELETE FROM products`, `:id`, `execute`.", "<?php\n\n$id = 1;\n\n// TODO: delete prepare.\n", ["prepare", "DELETE FROM products", ":id", "execute"], "<?php\n\n$id = 1;\n$statement = $pdo->prepare(\"DELETE FROM products WHERE id = :id\");\n$statement->execute([\"id\" => $id]);", [("php-pdo", 2), ("php-variables", 1)], 50),
        ["php-pdo-repository"] = N("Repository PDO", "Tu regroupes les requetes SQL produit dans une classe.", "Creer `PdoProductRepository` avec une dependance PDO.", "Objet PDO injecte.", "Repository avec findById.", "Utiliser `__construct`, `PDO`, `prepare`, `fetch`.", "<?php\n\nfinal class PdoProductRepository\n{\n    // TODO\n}\n", ["class PdoProductRepository", "__construct", "PDO", "prepare", "fetch"], "<?php\n\nfinal class PdoProductRepository\n{\n    public function __construct(private PDO $pdo) {}\n\n    public function findById(int $id): array|false\n    {\n        $statement = $this->pdo->prepare(\"SELECT * FROM products WHERE id = :id\");\n        $statement->execute([\"id\" => $id]);\n        return $statement->fetch();\n    }\n}", [("php-pdo", 2), ("php-oop", 1)], 60),
        ["php-pdo-exceptions"] = N("PDO exceptions", "Tu rends les erreurs SQL visibles.", "Configurer PDO en mode exception.", "Objet `$pdo`.", "Mode erreur exception.", "Utiliser `PDO::ATTR_ERRMODE` et `PDO::ERRMODE_EXCEPTION`.", "<?php\n\n// TODO: configure PDO.\n", ["PDO::ATTR_ERRMODE", "PDO::ERRMODE_EXCEPTION"], "<?php\n\n$pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);", [("php-pdo", 2), ("php-exceptions", 1)]),

        ["php-boss-final-native-product-catalog"] = N("Boss Final PHP : Product Catalog natif", "Tu construis un mini backend PHP natif de catalogue produits.", "Assembler domaine, repository, service, HTTP, JSON et persistance.", "Routes GET /products et POST /products, produits persistables en fichier ou PDO.", "Un backend structure retournant du JSON.", "Inclure strict types, namespace, Product, repository, service, routes HTTP, JSON, persistence et gestion d'exceptions.", "<?php\n\ndeclare(strict_types=1);\n\nnamespace App;\n\n// TODO: Product, repository, service, HTTP, persistence.\n", ["declare(strict_types=1);", "namespace", "class Product", "__construct", "private", "getName", "isAvailable", "throw new", "interface ProductRepositoryInterface", "findAvailable", "findById", "save", "ProductCatalogService", "__construct", "$_SERVER[\"REQUEST_METHOD\"]", "$_SERVER[\"REQUEST_URI\"]", "json_encode", "header", "prepare||file_put_contents", "try", "catch"], """
<?php

declare(strict_types=1);

namespace App;

use InvalidArgumentException;

class Product
{
    public function __construct(private int $id, private string $name, private float $price, private int $stock)
    {
        if ($price < 0) {
            throw new InvalidArgumentException('Invalid price');
        }
    }

    public function getName(): string { return $this->name; }
    public function isAvailable(): bool { return $this->stock > 0; }
    public function toArray(): array { return ['id' => $this->id, 'name' => $this->name, 'price' => $this->price, 'stock' => $this->stock]; }
}

interface ProductRepositoryInterface
{
    public function findAvailable(): array;
    public function findById(int $id): ?Product;
    public function save(Product $product): void;
}

final class InMemoryProductRepository implements ProductRepositoryInterface
{
    public function __construct(private array $products = []) {}
    public function findAvailable(): array { return array_filter($this->products, fn(Product $product) => $product->isAvailable()); }
    public function findById(int $id): ?Product { return $this->products[$id] ?? null; }
    public function save(Product $product): void { $this->products[] = $product; file_put_contents('products.json', json_encode(array_map(fn(Product $item) => $item->toArray(), $this->products))); }
}

final class ProductCatalogService
{
    public function __construct(private ProductRepositoryInterface $products) {}
    public function listAvailableProducts(): array { return $this->products->findAvailable(); }
    public function createProduct(string $name, float $price, int $stock): Product
    {
        $product = new Product(random_int(1, 9999), $name, $price, $stock);
        $this->products->save($product);
        return $product;
    }
}

try {
    $service = new ProductCatalogService(new InMemoryProductRepository());
    $method = $_SERVER["REQUEST_METHOD"] ?? "GET";
    $uri = $_SERVER["REQUEST_URI"] ?? "/products";
    header("Content-Type: application/json");
    if ($method === "GET" && $uri === "/products") {
        echo json_encode(array_map(fn(Product $product) => $product->toArray(), $service->listAvailableProducts()));
    } elseif ($method === "POST" && $uri === "/products") {
        echo json_encode($service->createProduct("Book", 12, 3)->toArray());
    }
} catch (\Throwable $exception) {
    header("Content-Type: application/json");
    echo json_encode(["error" => $exception->getMessage()]);
}
""", [("php-project-structure", 2), ("php-oop", 2), ("php-http", 2)], 220)
    };

    private static PhpNativeLessonDefinition N(
        string title,
        string context,
        string objective,
        string inputs,
        string expectedOutput,
        string constraints,
        string starterCode,
        string[] requiredSnippets,
        string finalCorrection,
        IReadOnlyList<(string Slug, int Weight)> skills,
        int xpReward = 60) =>
        new(
            title,
            context,
            objective,
            inputs,
            expectedOutput,
            constraints,
            starterCode,
            requiredSnippets,
            finalCorrection,
            skills,
            [
                $"Indice 1 - situation: {context}",
                $"Indice 2 - strategie: pars des entrees imposees ({inputs}) puis applique la notion avant d'afficher ou retourner le resultat.",
                $"Indice 3 - verification: controle les criteres {string.Join(", ", requiredSnippets.Take(3))}, puis assure-toi de ne pas coder `{expectedOutput}` en dur."
            ],
            xpReward);

    private static Dictionary<string, PhpSupplementalDefinition> PhpSupplementalDefinitions() => new(StringComparer.OrdinalIgnoreCase)
    {
        ["php-strings"] = new("Chaines et concatenation", "Nettoyer le nom `\" Book \"` avec `trim`, le stocker dans `$name`, puis afficher `Product: Book` avec une concatenation.", ["$name", "trim", ".", "echo"], "<?php\n\n$rawName = \" Book \";\n$name = trim($rawName);\n\necho \"Product: \" . $name;", [("php-strings", 2), ("php-variables", 1)]),
        ["php-match"] = new("Expression match", "Utiliser match pour traduire un statut de commande.", ["match", "$status", "'paid'", "Commande payee", "default"], "<?php\n\n$status = 'paid';\n$message = match ($status) {\n    'paid' => 'Commande payee',\n    default => 'Statut inconnu',\n};\n\necho $message;", [("php-conditions", 2), ("php-modern-types", 1)]),
        ["php-while"] = new("Boucle while", "Repeter tant qu'une condition reste vraie.", ["while", "$stock > 0", "$stock--", "echo"], "<?php\n\n$stock = 3;\nwhile ($stock > 0) {\n    echo \"Stock \" . $stock;\n    $stock--;\n}", [("php-loops", 2), ("php-conditions", 1)]),
        ["php-strict-types"] = new("Strict types", "Activer le typage strict dans un fichier PHP.", ["declare(strict_types=1);", "function", ": int"], "<?php\n\ndeclare(strict_types=1);\n\nfunction priceWithTax(int $price): int\n{\n    return $price;\n}", [("php-modern-types", 2), ("php-types", 1)]),
        ["php-nullable-types"] = new("Types nullable", "Utiliser un type nullable avec ?string.", ["?string", "$name", "return"], "<?php\n\nfunction displayName(?string $name): string\n{\n    return $name ?? 'Anonymous';\n}", [("php-modern-types", 2), ("php-types", 1)]),
        ["php-union-types"] = new("Union types", "Accepter plusieurs types avec int|string.", ["int|string", "$value", "return"], "<?php\n\nfunction normalizeId(int|string $value): string\n{\n    return (string) $value;\n}", [("php-modern-types", 2), ("php-types", 1)]),
        ["php-named-arguments"] = new("Arguments nommes", "Appeler une fonction avec des arguments nommes.", ["formatProduct", "name:", "price:"], "<?php\n\nfunction formatProduct(string $name, int $price): string\n{\n    return $name . ' - ' . $price;\n}\n\necho formatProduct(name: 'Book', price: 12);", [("php-modern-types", 2), ("php-functions", 1)]),
        ["php-arrow-functions"] = new("Fonctions flechees", "Utiliser fn($x) => pour une transformation courte.", ["fn($price)", "=>", "$price"], "<?php\n\n$withTax = fn($price) => $price * 1.2;\necho $withTax(10);", [("php-functional-arrays", 1), ("php-functions", 2)]),
        ["php-array-map"] = new("array_map", "Transformer une liste avec array_map.", ["array_map", "fn($product)", "$products"], "<?php\n\n$products = ['Book', 'Pen'];\n$labels = array_map(fn($product) => 'Product: ' . $product, $products);", [("php-functional-arrays", 2), ("php-arrays", 1)]),
        ["php-array-filter"] = new("array_filter", "Filtrer des produits avec array_filter.", ["array_filter", "fn($product)", "stock"], "<?php\n\n$products = [['stock' => 3], ['stock' => 0]];\n$available = array_filter($products, fn($product) => $product['stock'] > 0);", [("php-functional-arrays", 2), ("php-arrays", 1)]),
        ["php-array-reduce"] = new("array_reduce", "Calculer un total avec array_reduce.", ["array_reduce", "$total", "$product"], "<?php\n\n$products = [['price' => 12], ['price' => 8]];\n$total = array_reduce($products, fn($total, $product) => $total + $product['price'], 0);", [("php-functional-arrays", 2), ("php-arrays", 1)]),
        ["php-string-functions"] = new("Fonctions de chaines", "Nettoyer et tester une chaine.", ["trim", "strtolower", "str_contains"], "<?php\n\n$name = strtolower(trim(' Book '));\n$contains = str_contains($name, 'book');", [("php-strings", 2), ("php-functions", 1)]),
        ["php-date-time"] = new("DateTimeImmutable", "Manipuler une date immuable.", ["DateTimeImmutable", "modify", "format"], "<?php\n\n$now = new DateTimeImmutable();\n$tomorrow = $now->modify('+1 day');\necho $tomorrow->format('Y-m-d');", [("php-date-time", 2), ("php-modern-types", 1)]),
        ["php-enums"] = new("Enums PHP", "Modeliser un etat metier avec une enum PHP.", ["enum OrderStatus: string", "case Pending", "case Paid", "case Cancelled"], "<?php\n\nenum OrderStatus: string\n{\n    case Pending = 'pending';\n    case Paid = 'paid';\n    case Cancelled = 'cancelled';\n}", [("php-enums", 2), ("php-modern-types", 1)]),
        ["php-readonly-properties"] = new("Proprietes readonly", "Rendre une donnee immuable apres construction.", ["readonly", "public function __construct", "string $name"], "<?php\n\nfinal class ProductDto\n{\n    public function __construct(public readonly string $name) {}\n}", [("php-readonly", 2), ("php-oop", 1)]),
        ["php-oop-setters"] = new("Setters", "Modifier une propriete via une methode controlee.", ["public function setName", "string $name", "$this->name"], "<?php\n\npublic function setName(string $name): void\n{\n    $this->name = $name;\n}", [("php-oop", 2), ("php-types", 1)]),
        ["php-oop-visibility"] = new("Visibilite", "Distinguer public, private et protected.", ["public", "private", "protected"], "<?php\n\nclass Product\n{\n    public string $name;\n    private int $price;\n    protected int $stock;\n}", [("php-oop", 2)]),
        ["php-oop-static"] = new("Membres statiques", "Declarer une methode static de fabrique.", ["static", "public static function", "new self"], "<?php\n\nfinal class Product\n{\n    public static function create(): self\n    {\n        return new self();\n    }\n}", [("php-oop", 2)]),
        ["php-oop-interface"] = new("Interface", "Definir un contrat ProductFormatterInterface.", ["interface", "ProductFormatterInterface", "format"], "<?php\n\ninterface ProductFormatterInterface\n{\n    public function format(Product $product): string;\n}", [("php-interfaces", 2), ("php-oop", 1)]),
        ["php-oop-abstract-class"] = new("Classe abstraite", "Factoriser un comportement dans une abstract class.", ["abstract class", "abstract public function"], "<?php\n\nabstract class AbstractProductFormatter\n{\n    abstract public function format(Product $product): string;\n}", [("php-inheritance", 2), ("php-oop", 1)]),
        ["php-oop-inheritance"] = new("Heritage", "Etendre une classe de base avec extends.", ["extends", "class DigitalProduct", "Product"], "<?php\n\nclass DigitalProduct extends Product\n{\n}", [("php-inheritance", 2), ("php-oop", 1)]),
        ["php-oop-polymorphism"] = new("Polymorphisme", "Utiliser une interface pour accepter plusieurs implementations.", ["ProductFormatterInterface", "implements", "format"], "<?php\n\nfinal class HtmlProductFormatter implements ProductFormatterInterface\n{\n    public function format(Product $product): string { return ''; }\n}", [("php-interfaces", 1), ("php-inheritance", 2)]),
        ["php-oop-traits"] = new("Traits", "Partager une methode avec un trait.", ["trait", "use", "TimestampableTrait"], "<?php\n\ntrait TimestampableTrait {}\n\nfinal class Product\n{\n    use TimestampableTrait;\n}", [("php-oop", 2)]),
        ["php-oop-composition"] = new("Composition", "Injecter un objet collaborateur.", ["private ProductFormatterInterface", "__construct", "$formatter"], "<?php\n\nfinal class ProductPresenter\n{\n    public function __construct(private ProductFormatterInterface $formatter) {}\n}", [("php-composition", 2), ("php-interfaces", 1)]),
        ["php-custom-exception"] = new("Exception personnalisee", "Creer une exception metier dediee.", ["class InvalidProductPriceException extends", "Exception"], "<?php\n\nclass InvalidProductPriceException extends Exception\n{\n}", [("php-exceptions", 2), ("php-oop", 1)]),
        ["php-try-catch-finally"] = new("try catch finally", "Gerer une exception avec try, catch et finally.", ["try", "catch", "finally"], "<?php\n\ntry {\n    throw new Exception('Error');\n} catch (Exception $exception) {\n    echo $exception->getMessage();\n} finally {\n    echo 'Done';\n}", [("php-exceptions", 2)]),
        ["php-composer-json"] = new("composer.json", "Declarer un fichier composer.json minimal.", ["\"name\"", "\"require\"", "\"autoload\""], "{\n  \"name\": \"app/product-catalog\",\n  \"require\": {},\n  \"autoload\": {}\n}", [("php-composer", 2), ("php-project-structure", 1)]),
        ["php-composer-require"] = new("composer require", "Declarer une dependance dans require.", ["\"require\"", "\"php\"", "^8.2"], "{\n  \"require\": {\n    \"php\": \"^8.2\"\n  }\n}", [("php-composer", 2)]),
        ["php-autoload-psr4"] = new("Autoload PSR-4", "Configurer App\\\\ vers src/.", ["\"autoload\"", "\"psr-4\"", "\"App\\\\\": \"src/\""], "{\n  \"autoload\": {\n    \"psr-4\": {\n      \"App\\\\\": \"src/\"\n    }\n  }\n}", [("php-autoload", 2), ("php-composer", 1)]),
        ["php-namespace-directory"] = new("Namespace et dossier", "Faire correspondre namespace et dossier src.", ["namespace App\\Catalog", "src/Catalog", "class Product"], "<?php\n\n// src/Catalog/Product.php\nnamespace App\\Catalog;\n\nclass Product {}\n", [("php-autoload", 2), ("php-project-structure", 1)]),
        ["php-psr12"] = new("PSR-12", "Respecter une structure de classe lisible.", ["declare(strict_types=1);", "namespace", "final class"], "<?php\n\ndeclare(strict_types=1);\n\nnamespace App;\n\nfinal class ProductCatalog\n{\n}\n", [("php-standards", 2), ("php-modern-types", 1)]),
        ["php-project-structure"] = new("Structure projet PHP", "Organiser src, public et tests.", ["src/", "public/", "tests/"], "src/\npublic/\ntests/\ncomposer.json", [("php-project-structure", 2), ("php-composer", 1)]),
        ["php-dev-scripts"] = new("Scripts Composer", "Ajouter des scripts de developpement.", ["\"scripts\"", "\"test\"", "\"phpunit\""], "{\n  \"scripts\": {\n    \"test\": \"phpunit\"\n  }\n}", [("php-composer", 2), ("php-standards", 1)]),
        ["php-http-get"] = new("GET PHP", "Lire un parametre GET.", ["$_GET", "id", "echo"], "<?php\n\n$id = $_GET['id'] ?? null;\necho $id;", [("php-http", 2)]),
        ["php-http-post"] = new("POST PHP", "Lire une donnee POST.", ["$_POST", "name", "echo"], "<?php\n\n$name = $_POST['name'] ?? '';\necho $name;", [("php-http", 2)]),
        ["php-request-server"] = new("SERVER PHP", "Lire la methode HTTP.", ["$_SERVER", "REQUEST_METHOD"], "<?php\n\n$method = $_SERVER['REQUEST_METHOD'] ?? 'GET';\necho $method;", [("php-http", 2)]),
        ["php-sessions"] = new("Sessions PHP", "Stocker une valeur en session.", ["session_start", "$_SESSION", "user_id"], "<?php\n\nsession_start();\n$_SESSION['user_id'] = 1;", [("php-sessions", 2), ("php-http", 1)]),
        ["php-cookies"] = new("Cookies PHP", "Creer un cookie.", ["setcookie", "theme", "time()"], "<?php\n\nsetcookie('theme', 'dark', time() + 3600);", [("php-cookies", 2), ("php-http", 1)]),
        ["php-file-upload"] = new("Upload fichier", "Lire un fichier envoye avec FILES.", ["$_FILES", "tmp_name", "name"], "<?php\n\n$file = $_FILES['picture'] ?? null;\n$name = $file['name'] ?? '';\n$tmp = $file['tmp_name'] ?? '';", [("php-http", 2)]),
        ["php-basic-routing"] = new("Routing natif", "Choisir une action selon l'URI.", ["$_SERVER", "REQUEST_URI", "match"], "<?php\n\n$path = $_SERVER['REQUEST_URI'] ?? '/';\n$page = match ($path) {\n    '/products' => 'products',\n    default => 'not_found',\n};", [("php-http", 2), ("php-conditions", 1)]),
        ["php-json-response"] = new("Reponse JSON native", "Retourner une reponse JSON.", ["header('Content-Type: application/json')", "json_encode", "echo"], "<?php\n\nheader('Content-Type: application/json');\necho json_encode(['name' => 'Book']);", [("php-json", 2), ("php-http", 1)])
    };

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
        new()
        {
            Title = title,
            Description = description,
            SortOrder = sortOrder,
            RequiredXp = requiredXp,
            Lessons = lessons.Where(lesson => !IsIntermediateCheckpointLesson(lesson)).ToList()
        };

    private static async Task RemoveIntermediateCheckpointLessonsAsync(AppDbContext db)
    {
        var lessons = await db.Lessons
            .Where(lesson => lesson.Slug.EndsWith("-checkpoint") || lesson.Title.StartsWith("Test intermediaire"))
            .ToListAsync();

        if (lessons.Count == 0)
        {
            return;
        }

        var lessonIds = lessons.Select(lesson => lesson.Id).ToList();
        var progress = await db.LessonProgress
            .Where(item => lessonIds.Contains(item.LessonId))
            .ToListAsync();

        db.LessonProgress.RemoveRange(progress);
        db.Lessons.RemoveRange(lessons);
        await db.SaveChangesAsync();
    }

    private static bool IsIntermediateCheckpointLesson(Lesson lesson) =>
        lesson.Slug.EndsWith("-checkpoint", StringComparison.OrdinalIgnoreCase)
        || lesson.Title.StartsWith("Test intermediaire", StringComparison.OrdinalIgnoreCase);

    private static async Task RefreshExistingLessonGuidanceAsync(AppDbContext db)
    {
        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Where(lesson => lesson.FinalCorrection != "")
            .ToListAsync();

        foreach (var lesson in lessons)
        {
            lesson.ExampleCode = MakeIllustrativeExample(lesson.Slug, lesson.ExampleCode, lesson.FinalCorrection);
            lesson.StarterCode = SeparateStarterFromCorrection(lesson.Slug, lesson.StarterCode, lesson.FinalCorrection);
            RefreshExplicitExerciseGuidance(lesson);
        }

        await db.SaveChangesAsync();
    }

    private static void RefreshExplicitExerciseGuidance(Lesson lesson)
    {
        switch (lesson.Slug)
        {
            case "sql-insert":
                lesson.ExercisePrompt = "Ajoute dans Products le produit Id 6, Name N'Learning Mug', CategoryId 1, Price 14.90, Stock 12, IsActive 1, puis affiche Name, Price et Stock de la ligne ajoutee avec WHERE Id = 6.";
                lesson.FailureFeedback = "Utilise INSERT INTO Products avec les colonnes Id, Name, CategoryId, Price, Stock, IsActive et les valeurs 6, N'Learning Mug', 1, 14.90, 12, 1, puis un SELECT de verification sur Id = 6.";
                break;
            case "sql-primary-keys":
                lesson.ExercisePrompt = "Cree la table Suppliers avec Id comme cle primaire et Name nvarchar(80) NOT NULL. Insere le fournisseur Id 1, Name N'Northwind Supply', puis affiche Id et Name avec WHERE Id = 1.";
                lesson.FailureFeedback = "Utilise PRIMARY KEY sur Id, insere (1, N'Northwind Supply'), puis verifie avec SELECT Id, Name WHERE Id = 1.";
                break;
            case "sql-foreign-keys":
                lesson.ExercisePrompt = "Cree Suppliers et SupplierProducts. Insere Suppliers (1, N'Northwind Supply'), relie SupplierId 1 au ProductId 2 dans SupplierProducts, puis affiche SupplierId et ProductId.";
                lesson.FailureFeedback = "Ajoute deux FOREIGN KEY: SupplierId vers Suppliers(Id), ProductId vers Products(Id). Insere le lien (SupplierId 1, ProductId 2).";
                break;
            case "sql-constraints":
                lesson.ExercisePrompt = "Cree Warehouses avec Id PRIMARY KEY, Code nvarchar(10) NOT NULL UNIQUE et Capacity int NOT NULL CHECK (Capacity > 0). Insere (1, N'WH-A', 120), puis affiche Code et Capacity avec WHERE Code = N'WH-A'.";
                lesson.FailureFeedback = "Utilise NOT NULL, UNIQUE et CHECK (Capacity > 0), puis insere WH-A avec Capacity 120.";
                break;
            case "sql-relationships":
                lesson.ExercisePrompt = "Cree Suppliers et SupplierProducts. Insere Suppliers (1, N'Northwind Supply'), relie SupplierId 1 au ProductId 4, puis affiche SupplierName et ProductName avec des JOIN.";
                lesson.FailureFeedback = "Utilise SupplierProducts avec deux FOREIGN KEY, insere le lien (1, 4), puis fais deux JOIN de lecture vers Suppliers et Products.";
                break;
            case "sql-modeling-checkpoint":
                lesson.ExercisePrompt = "Cree Students, Courses et StudentCourses. Insere Students (1, N'Ada'), Courses (1, N'SQL Server'), inscris StudentId 1 au CourseId 1, puis affiche StudentName et CourseTitle.";
                lesson.FailureFeedback = "Combine trois tables, cles primaires, deux cles etrangeres, les insertions Ada / SQL Server et une requete JOIN finale.";
                break;
            case "sql-create-project-tables":
                lesson.ExercisePrompt = "Cree Customers, Orders et OrderItems dans cet ordre. Insere le client Customers (1, N'Ada Lovelace', N'ada@example.com'), puis affiche Name et Email avec WHERE Id = 1.";
                lesson.FailureFeedback = "Respecte l'ordre Customers, Orders, OrderItems, insere Ada Lovelace avec ada@example.com, puis verifie Customers.";
                break;
            case "sql-seed-project-data":
                lesson.ExercisePrompt = "Cree les tables, insere Customers Ada Lovelace et Alan Turing, Orders Id 1 pour Ada et Id 2 pour Alan, puis trois OrderItems. Termine avec COUNT(*) AS LineCount depuis OrderItems.";
                lesson.FailureFeedback = "Insere dans l'ordre Customers, Orders, OrderItems avec les deux clients Ada/Alan et trois lignes de commande, puis compte OrderItems.";
                break;
            case "sql-business-queries":
                lesson.ExercisePrompt = "Cree et remplis le mini-schema avec Ada Lovelace, Alan Turing, deux commandes et trois lignes. Affiche OrderId, CustomerName et ProductName avec des JOIN vers Customers, OrderItems et Products.";
                lesson.FailureFeedback = "Utilise les jointures Orders, Customers, OrderItems et Products, et retourne trois lignes avec les alias OrderId, CustomerName, ProductName.";
                break;
            case "sql-simple-optimization":
                lesson.ExercisePrompt = "Cree et remplis le mini-schema avec Ada Lovelace et Alan Turing. Ajoute l'index IX_Orders_CustomerId sur Orders(CustomerId), puis affiche OrderId et CustomerName des commandes d'Ada Lovelace.";
                lesson.FailureFeedback = "Cree IX_Orders_CustomerId sur Orders(CustomerId), puis filtre WHERE c.Name = N'Ada Lovelace'.";
                break;
            case "sql-project-checkpoint":
                lesson.ExercisePrompt = "Cree et remplis le mini-schema avec deux commandes: Ada totalise 58.50 et Alan 89.99. Affiche OrderId, CustomerName et OrderTotal avec SUM(oi.Quantity * oi.UnitPrice), GROUP BY o.Id, c.Name et ORDER BY OrderTotal DESC.";
                lesson.FailureFeedback = "Combine Customers, Orders, OrderItems, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal, GROUP BY o.Id, c.Name et ORDER BY OrderTotal DESC.";
                break;
        }

        if (lesson.Chapter?.Course?.Language == "php-symfony"
            && lesson.Slug.StartsWith("php-symfony-module-", StringComparison.OrdinalIgnoreCase)
            && !lesson.IsBossFinal)
        {
            var expected = PhpSymfonyExpectedSnippetsFor(lesson.Title);
            var expectedText = string.Join(", ", expected);
            lesson.ExercisePrompt = PhpSymfonyExercisePromptFor(lesson.Title, expectedText);
            lesson.FailureFeedback = $"Ajoute les elements attendus: {expectedText}.";
            lesson.StarterCode = PhpSymfonyStarterFor(lesson.Title, expected);
        }
    }

    private static async Task EnsurePhpSymfonyCourseSeededAsync(AppDbContext db)
    {
        if (!await db.Courses.AnyAsync(course => course.Slug == "php-symfony"))
        {
            var phpCourse = PhpSymfonyCourse();
            AttachIntermediateBosses(phpCourse);
            db.Courses.Add(phpCourse);
        }
        else
        {
            await EnsurePhpSymfonyLessonsSeededAsync(db);
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

    private static async Task EnsurePhpSymfonyLessonsSeededAsync(AppDbContext db)
    {
        var existingCourse = await db.Courses
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.Tests)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.Hints)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.LessonSkills)
            .FirstOrDefaultAsync(course => course.Slug == "php-symfony");

        if (existingCourse is null)
        {
            return;
        }

        var sourceCourse = PhpSymfonyCourse();
        existingCourse.Title = sourceCourse.Title;
        existingCourse.Description = sourceCourse.Description;
        existingCourse.Language = sourceCourse.Language;
        existingCourse.SortOrder = sourceCourse.SortOrder;

        foreach (var sourceChapter in sourceCourse.Chapters)
        {
            var targetChapter = existingCourse.Chapters.FirstOrDefault(chapter => chapter.SortOrder == sourceChapter.SortOrder);
            if (targetChapter is null)
            {
                existingCourse.Chapters.Add(CloneChapter(sourceChapter));
                continue;
            }

            targetChapter.Title = sourceChapter.Title;
            targetChapter.Description = sourceChapter.Description;
            targetChapter.RequiredXp = sourceChapter.RequiredXp;

            var sourceSlugs = sourceChapter.Lessons.Select(lesson => lesson.Slug).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var obsoleteLessons = targetChapter.Lessons.Where(lesson => !sourceSlugs.Contains(lesson.Slug)).ToList();
            if (obsoleteLessons.Count > 0)
            {
                var obsoleteIds = obsoleteLessons.Select(lesson => lesson.Id).ToList();
                var obsoleteProgress = await db.LessonProgress.Where(progress => obsoleteIds.Contains(progress.LessonId)).ToListAsync();
                db.LessonProgress.RemoveRange(obsoleteProgress);
                db.Lessons.RemoveRange(obsoleteLessons);
            }

            foreach (var sourceLesson in sourceChapter.Lessons.OrderBy(lesson => lesson.SortOrder))
            {
                var targetLesson = targetChapter.Lessons.FirstOrDefault(lesson => lesson.Slug.Equals(sourceLesson.Slug, StringComparison.OrdinalIgnoreCase));
                if (targetLesson is not null)
                {
                    RefreshLessonFromSource(db, targetLesson, sourceLesson);
                    continue;
                }

                targetChapter.Lessons.Add(CloneLesson(sourceLesson));
            }
        }
    }

    private static async Task EnsureStaticCourseSeededAsync(AppDbContext db, Course sourceCourse)
    {
        var existingCourse = await db.Courses
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.Tests)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.Hints)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.LessonSkills)
            .FirstOrDefaultAsync(course => course.Slug == sourceCourse.Slug);

        AttachIntermediateBosses(sourceCourse);

        if (existingCourse is null)
        {
            db.Courses.Add(sourceCourse);
            return;
        }

        existingCourse.Title = sourceCourse.Title;
        existingCourse.Description = sourceCourse.Description;
        existingCourse.Language = sourceCourse.Language;
        existingCourse.SortOrder = sourceCourse.SortOrder;

        foreach (var sourceChapter in sourceCourse.Chapters)
        {
            var targetChapter = existingCourse.Chapters.FirstOrDefault(chapter => chapter.SortOrder == sourceChapter.SortOrder);
            if (targetChapter is null)
            {
                existingCourse.Chapters.Add(CloneChapter(sourceChapter));
                continue;
            }

            targetChapter.Title = sourceChapter.Title;
            targetChapter.Description = sourceChapter.Description;
            targetChapter.RequiredXp = sourceChapter.RequiredXp;

            var sourceSlugs = sourceChapter.Lessons.Select(lesson => lesson.Slug).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var obsoleteLessons = targetChapter.Lessons.Where(lesson => !sourceSlugs.Contains(lesson.Slug)).ToList();
            if (obsoleteLessons.Count > 0)
            {
                var obsoleteIds = obsoleteLessons.Select(lesson => lesson.Id).ToList();
                var obsoleteProgress = await db.LessonProgress.Where(progress => obsoleteIds.Contains(progress.LessonId)).ToListAsync();
                db.LessonProgress.RemoveRange(obsoleteProgress);
                db.Lessons.RemoveRange(obsoleteLessons);
            }

            foreach (var sourceLesson in sourceChapter.Lessons.OrderBy(lesson => lesson.SortOrder))
            {
                var targetLesson = targetChapter.Lessons.FirstOrDefault(lesson => lesson.Slug.Equals(sourceLesson.Slug, StringComparison.OrdinalIgnoreCase));
                if (targetLesson is not null)
                {
                    RefreshLessonFromSource(db, targetLesson, sourceLesson);
                    continue;
                }

                targetChapter.Lessons.Add(CloneLesson(sourceLesson));
            }
        }
    }

    private static Chapter CloneChapter(Chapter source) =>
        new()
        {
            Title = source.Title,
            Description = source.Description,
            SortOrder = source.SortOrder,
            RequiredXp = source.RequiredXp,
            Lessons = source.Lessons.Select(CloneLesson).ToList()
        };

    private static Lesson CloneLesson(Lesson source) =>
        new()
        {
            Slug = source.Slug,
            Title = source.Title,
            Objective = source.Objective,
            ConceptSummary = source.ConceptSummary,
            CommonMistakes = source.CommonMistakes,
            Explanation = source.Explanation,
            ExampleCode = source.ExampleCode,
            ExercisePrompt = source.ExercisePrompt,
            StarterCode = source.StarterCode,
            SuccessFeedback = source.SuccessFeedback,
            FailureFeedback = source.FailureFeedback,
            FinalCorrection = source.FinalCorrection,
            XpReward = source.XpReward,
            SortOrder = source.SortOrder,
            IsBossFinal = source.IsBossFinal,
            IsBossPrerequisite = source.IsBossPrerequisite,
            Tests = source.Tests.Select(CloneLessonTest).ToList()
        };

    private static void RefreshLessonFromSource(AppDbContext db, Lesson target, Lesson source)
    {
        target.Title = source.Title;
        target.Objective = source.Objective;
        target.ConceptSummary = source.ConceptSummary;
        target.CommonMistakes = source.CommonMistakes;
        target.Explanation = source.Explanation;
        target.ExampleCode = source.ExampleCode;
        target.ExercisePrompt = source.ExercisePrompt;
        target.StarterCode = source.StarterCode;
        target.SuccessFeedback = source.SuccessFeedback;
        target.FailureFeedback = source.FailureFeedback;
        target.FinalCorrection = source.FinalCorrection;
        target.XpReward = source.XpReward;
        target.SortOrder = source.SortOrder;
        target.IsBossFinal = source.IsBossFinal;
        target.IsBossPrerequisite = source.IsBossPrerequisite;

        db.LessonTests.RemoveRange(target.Tests);
        target.Tests = source.Tests.Select(CloneLessonTest).ToList();
    }

    private static LessonTest CloneLessonTest(LessonTest source) =>
        new()
        {
            Name = source.Name,
            TestType = source.TestType,
            ExpectedOutput = source.ExpectedOutput,
            RequiredSnippet = source.RequiredSnippet,
            HiddenCode = source.HiddenCode,
            MinCount = source.MinCount,
            ExpectedColumns = source.ExpectedColumns,
            ExpectedRowCount = source.ExpectedRowCount,
            SortOrder = source.SortOrder
        };

    private sealed record StaticLessonDefinition(
        string Slug,
        string Title,
        string Objective,
        string[] RequiredSnippets,
        string Correction,
        IReadOnlyList<(string Slug, int Weight)> Skills);

    private sealed record StaticChapterDefinition(int SortOrder, string Title, string Description, string[] LessonSlugs);

    private static Course ReactCourse() =>
        StaticSnippetCourse(
            "react",
            "React",
            "Apprendre React moderne en construisant une interface Product Manager.",
            4,
            ReactChapters(),
            "Boss Final React",
            "Interface Product Manager complete.",
            "react-boss-final-product-manager");

    private static Course ReactNativeCourse() =>
        StaticSnippetCourse(
            "react-native",
            "React Native",
            "Apprendre React Native en construisant une application mobile Product App.",
            5,
            ReactNativeChapters(),
            "Boss Final React Native",
            "Mini-application mobile Product App.",
            "react-native-boss-final-product-app");

    private static Course TailwindCssCourse() =>
        StaticSnippetCourse(
            "tailwindcss",
            "TailwindCSS",
            "Apprendre TailwindCSS en construisant progressivement un Product Dashboard responsive.",
            6,
            TailwindCssChapters(),
            "Boss Final TailwindCSS",
            "Page Product Dashboard responsive.",
            "tailwindcss-boss-final-dashboard");

    private static Course CssCourse() =>
        StaticSnippetCourse(
            "css",
            "CSS",
            "Apprendre CSS en construisant des interfaces responsive.",
            7,
            CssChapters(),
            "Boss Final CSS",
            "Page produit responsive.",
            "css-boss-final-responsive-product-page");

    private static Course JavaScriptCourse() =>
        StaticSnippetCourse(
            "javascript",
            "JavaScript",
            "Apprendre JavaScript moderne et la manipulation du DOM.",
            8,
            JavaScriptChapters(),
            "Boss Final JavaScript",
            "Product List interactive.",
            "javascript-boss-final-product-list");

    private static Course StaticSnippetCourse(
        string slug,
        string title,
        string description,
        int sortOrder,
        IReadOnlyList<StaticChapterDefinition> chapters,
        string bossChapterTitle,
        string bossChapterDescription,
        string bossSlug) =>
        new()
        {
            Slug = slug,
            Title = title,
            Description = description,
            Language = slug,
            SortOrder = sortOrder,
            Chapters = chapters
                .Select(chapter => Chapter($"Module {chapter.SortOrder} - {chapter.Title}", chapter.Description, chapter.SortOrder, 0,
                    chapter.LessonSlugs.Select((lessonSlug, index) => StaticSnippetLesson(lessonSlug, index + 1)).ToList()))
                .Append(Chapter(bossChapterTitle, bossChapterDescription, chapters.Count + 1, 0,
                [
                    StaticSnippetLesson(bossSlug, 1, isBossFinal: true)
                ]))
                .ToList()
        };

    private static Lesson StaticSnippetLesson(string slug, int sortOrder, bool isBossFinal = false)
    {
        var definition = StaticLessonDefinitions()[slug];
        var tests = definition.RequiredSnippets.Select((snippet, index) =>
        {
            var test = Required($"Contient {snippet}", snippet);
            test.SortOrder = index + 1;
            return test;
        }).ToList();

        return Lesson(
            slug,
            definition.Title,
            definition.Objective,
            StaticExplanationFor(slug, definition),
            StaticExampleFor(slug, definition),
            StaticExercisePromptFor(slug, definition),
            StaticStarterWithTodos(slug, definition),
            "Les concepts attendus sont presents dans un code coherent.",
            StaticFailureFeedbackFor(slug, definition),
            isBossFinal ? 180 : 45 + Math.Min(sortOrder, 6) * 5,
            sortOrder,
            tests,
            $"{definition.Title} est une brique du parcours {StaticCourseLabel(slug)}.",
            StaticCommonMistakesFor(slug),
            definition.Correction,
            isBossFinal);
    }

    private static string StaticExplanationFor(string slug, StaticLessonDefinition definition) =>
        $"{StaticCourseLabel(slug)} - cas pratique. La validation statique verifie plusieurs criteres ensemble: {string.Join(", ", definition.RequiredSnippets)}. L'objectif est de produire un morceau exploitable de l'interface Product Catalog, pas seulement de reconnaitre des mots-cles.";

    private static string StaticExercisePromptFor(string slug, StaticLessonDefinition definition)
    {
        var label = StaticCourseLabel(slug);
        return $"""
        Contexte concret:
        {StaticContextFor(label)}

        Objectif:
        {definition.Objective}

        Entrees imposees:
        {StaticInputsFor(label)}

        Sortie attendue:
        {StaticExpectedOutputFor(label)}

        Contraintes de code:
        Utilise les structures attendues: {string.Join(", ", definition.RequiredSnippets)}. Complete le starter sans remplacer le cas metier par une sortie codee en dur.
        """;
    }

    private static string StaticStarterWithTodos(string slug, StaticLessonDefinition definition)
    {
        var starter = StaticStarterFor(slug);
        if (starter.Contains("TODO", StringComparison.OrdinalIgnoreCase))
        {
            return starter;
        }

        var todo = StaticTodoCommentFor(slug, definition);
        return $"{starter.TrimEnd()}\n\n{todo}\n";
    }

    private static string StaticTodoCommentFor(string slug, StaticLessonDefinition definition)
    {
        var objective = $"TODO: applique l'objectif: {definition.Objective}";
        var criteria = $"TODO: couvre les criteres: {string.Join(", ", definition.RequiredSnippets.Take(4))}.";
        var label = StaticCourseLabel(slug);

        return label switch
        {
            "TailwindCSS" => $"<!-- {objective} -->\n<!-- {criteria} -->",
            "CSS" => $"/* {objective} */\n/* {criteria} */",
            _ => $"// {objective}\n// {criteria}"
        };
    }

    private static string StaticFailureFeedbackFor(string slug, StaticLessonDefinition definition) =>
        $"La solution est encore incomplete pour {StaticCourseLabel(slug)}. Ajoute les elements attendus ({string.Join(", ", definition.RequiredSnippets.Take(5))}) dans du code reel, puis verifie que le comportement correspond au contexte Product Catalog.";

    private static string StaticCommonMistakesFor(string slug) =>
        StaticCourseLabel(slug) switch
        {
            "React" => "Erreur frequente 1: mettre le JSX dans un commentaire au lieu de retourner un composant. Erreur frequente 2: oublier l'etat, les props ou les key quand la lecon demande une interaction ou une liste.",
            "React Native" => "Erreur frequente 1: utiliser du HTML web au lieu des composants natifs View, Text, FlatList ou TextInput. Erreur frequente 2: oublier les props mobiles comme renderItem, keyExtractor, onPress ou route.params.",
            "TailwindCSS" => "Erreur frequente 1: ecrire du CSS classique au lieu d'utiliser les classes utilitaires demandees. Erreur frequente 2: oublier les variantes responsive, hover, focus ou dark quand elles sont dans la consigne.",
            "CSS" => "Erreur frequente 1: cibler le mauvais selecteur ou ne pas relier les styles au HTML de preview. Erreur frequente 2: coder une valeur isolee sans traiter layout, responsive ou etats attendus.",
            "JavaScript" => "Erreur frequente 1: manipuler des donnees sans mettre a jour le DOM. Erreur frequente 2: oublier preventDefault, addEventListener, render, localStorage ou la gestion loading/error selon la lecon.",
            _ => "Erreur frequente 1: ne couvrir qu'un snippet au lieu de la combinaison demandee. Erreur frequente 2: remplacer le starter par une sortie codee en dur."
        };

    private static string StaticContextFor(string label) => label switch
    {
        "React" => "Tu developpes l'interface Product Manager: cartes produit, filtres, formulaires, chargement API et etats utilisateur.",
        "React Native" => "Tu developpes l'application mobile Product App: ecrans, listes, formulaires, navigation, stockage local et chargement API.",
        "TailwindCSS" => "Tu construis un Product Dashboard responsive avec des composants utilitaires coherents et reutilisables.",
        "CSS" => "Tu styles une page produit responsive a partir d'un HTML de preview deja fourni.",
        "JavaScript" => "Tu rends une Product List interactive dans le navigateur avec DOM, evenements, donnees locales et fetch.",
        _ => "Tu construis une fonctionnalite concrete du Product Catalog."
    };

    private static string StaticInputsFor(string label) => label switch
    {
        "React" => "Un tableau de produits, des props de composant, des champs de formulaire ou un endpoint `/api/products` selon la lecon.",
        "React Native" => "Des donnees `products`, des props `navigation`/`route`, des saisies TextInput ou un endpoint distant selon la lecon.",
        "TailwindCSS" => "Un extrait HTML Product Dashboard a completer avec les classes Tailwind demandees.",
        "CSS" => "Le HTML de preview expose des classes comme `.product-card`, `.products`, `.product-form` et `.preview-page`.",
        "JavaScript" => "Le HTML de preview expose `#product-form`, `#product-name`, `#product-filter`, `#products` ou `#counter` selon la lecon.",
        _ => "Les donnees fournies par le starter."
    };

    private static string StaticExpectedOutputFor(string label) => label switch
    {
        "React" => "Une portion d'interface Product Manager rendue par des composants React coherents.",
        "React Native" => "Un ecran ou un bloc mobile React Native lisible et connecte aux donnees demandees.",
        "TailwindCSS" => "Un composant visuel Product Dashboard structure, responsive et stylise par classes utilitaires.",
        "CSS" => "Une preview produit lisible, responsive et stylisee par les selecteurs CSS demandes.",
        "JavaScript" => "Une interface DOM mise a jour: liste affichee, ajout/filtre/suppression ou etats API selon la lecon.",
        _ => "Un resultat coherent avec l'objectif de la lecon."
    };

    private static string StaticExampleFor(string slug, StaticLessonDefinition definition)
    {
        var label = StaticCourseLabel(slug);
        var firstSnippet = definition.RequiredSnippets.FirstOrDefault() ?? "concept";
        return $"// Exemple {label}: repere le concept `{firstSnippet}` dans un code minimal avant de faire l'exercice.";
    }

    private static string StaticStarterFor(string slug) =>
        slug switch
        {
            "css-responsive-grid" => ".products {\n  display: grid;\n  gap: 16px;\n}\n\n/* Ajoute les breakpoints 768px et 1024px */\n",
            "css-fluid-width" => ".preview-page {\n  width: 100%;\n  /* Ajoute max-width et centrage */\n}\n",
            "css-clamp-font-size" => ".product-title {\n  /* Utilise clamp pour une taille fluide */\n}\n",
            "js-event-listener" => "const counter = document.querySelector('#counter');\nlet count = 0;\n\n// Ajoute le listener click ici\n",
            "js-render-product-list" => "const products = ['Book', 'Keyboard', 'Mouse'];\nconst list = document.querySelector('#products');\n\n// Affiche les produits dans la liste\n",
            "js-add-product" => "const products = [];\nconst form = document.querySelector('#product-form');\nconst input = document.querySelector('#product-name');\nconst list = document.querySelector('#products');\n\nfunction renderProducts() {\n}\n",
            "javascript-boss-final-product-list" => "const form = document.querySelector('#product-form');\nconst input = document.querySelector('#product-name');\nconst filterInput = document.querySelector('#product-filter');\nconst list = document.querySelector('#products');\n\nlet products = [];\n\nfunction renderProducts() {\n}\n",
            _ when slug.StartsWith("css-", StringComparison.OrdinalIgnoreCase) => ".preview-page {\n}\n\n.product-card {\n}\n",
            _ when slug.StartsWith("js-", StringComparison.OrdinalIgnoreCase) || slug.StartsWith("javascript-", StringComparison.OrdinalIgnoreCase) => "// Complete le JavaScript DOM ici\n",
            _ when slug.StartsWith("tailwind-", StringComparison.OrdinalIgnoreCase) || slug.StartsWith("tailwindcss-", StringComparison.OrdinalIgnoreCase)
                => "<section class=\"\">\n  <!-- Complete l'interface Product Dashboard ici -->\n</section>",
            _ when slug.StartsWith("rn-", StringComparison.OrdinalIgnoreCase) || slug.StartsWith("react-native", StringComparison.OrdinalIgnoreCase)
                => "import React from 'react';\n\n// Complete l'ecran React Native ici.",
            _ => "function App() {\n  // React, useState et useEffect sont disponibles globalement dans le preview.\n  return null;\n}"
        };

    private static string StaticCourseLabel(string slug) =>
        slug.StartsWith("css-", StringComparison.OrdinalIgnoreCase)
            ? "CSS"
            : slug.StartsWith("js-", StringComparison.OrdinalIgnoreCase) || slug.StartsWith("javascript-", StringComparison.OrdinalIgnoreCase)
            ? "JavaScript"
            : slug.StartsWith("tailwind-", StringComparison.OrdinalIgnoreCase) || slug.StartsWith("tailwindcss-", StringComparison.OrdinalIgnoreCase)
            ? "TailwindCSS"
            : slug.StartsWith("rn-", StringComparison.OrdinalIgnoreCase) || slug.StartsWith("react-native", StringComparison.OrdinalIgnoreCase)
            ? "React Native"
            : "React";

    private static Dictionary<string, LessonPlan> StaticSnippetLessonPlans() =>
        StaticLessonDefinitions().ToDictionary(
            item => item.Key,
            item => new LessonPlan(
                item.Value.Skills,
                [
                    $"Commence par la responsabilite principale: {item.Value.Objective}",
                    $"Ajoute les snippets essentiels: {string.Join(", ", item.Value.RequiredSnippets.Take(3))}.",
                    "Complete un exemple minimal coherent sans recopier une application complete."
                ]),
            StringComparer.OrdinalIgnoreCase);

    private static IReadOnlyList<StaticChapterDefinition> ReactChapters() =>
    [
        new(1, "Fondations JSX et composants", "JSX, composants, fragments, classes CSS, expressions et composition.",
            ["react-jsx", "react-component-function", "react-fragments", "react-classname", "react-expression-jsx", "react-composition"]),
        new(2, "Props et composants", "Props typees, children, reuse et ProductCard.",
            ["react-props", "react-props-typescript", "react-children", "react-component-reuse", "react-product-card"]),
        new(3, "Etat et evenements", "useState, events, inputs controles, toggle, counter et filtre produit.",
            ["react-usestate", "react-events", "react-input-controlled", "react-toggle", "react-counter", "react-product-filter"]),
        new(4, "Rendu conditionnel et listes", "Conditions, map, keys, empty state et ProductList.",
            ["react-conditional-rendering", "react-list-map", "react-keys", "react-empty-state", "react-product-list"]),
        new(5, "Formulaires", "Formulaires controles, submit, validation, erreurs et ProductForm.",
            ["react-form-controlled", "react-form-submit", "react-form-validation", "react-form-errors", "react-product-form"]),
        new(6, "Effects et API", "useEffect, fetch, loading, error et API products.",
            ["react-useeffect", "react-fetch", "react-loading-state", "react-error-state", "react-api-products"]),
        new(7, "Hooks avances", "Custom hooks, memo, callback, reducer et useProducts.",
            ["react-custom-hook", "react-usememo", "react-usecallback", "react-usereducer", "react-use-products-hook"]),
        new(8, "Contexte et routing simule", "Context, provider, route simple, params simules et route protegee.",
            ["react-context", "react-context-provider", "react-router-basic", "react-route-params-simulated", "react-protected-route-basic"]),
        new(9, "Qualite, accessibilite, performance", "Decoupage, accessibilite, performance et tests.",
            ["react-component-splitting", "react-accessibility-basic", "react-performance-basic", "react-testing-component", "react-testing-user-event"]),
        new(10, "Projet Product Manager", "Fonctionnalites verticales du Product Manager.",
            ["react-project-layout", "react-project-product-list", "react-project-product-filter", "react-project-product-form", "react-project-product-detail", "react-project-api-loading", "react-project-error-handling"])
    ];

    private static IReadOnlyList<StaticChapterDefinition> ReactNativeChapters() =>
    [
        new(1, "Fondations React Native", "View, Text, StyleSheet, flexbox, Pressable, Image et ScrollView.",
            ["rn-view-text", "rn-style-sheet", "rn-flexbox", "rn-button-pressable", "rn-image", "rn-scrollview"]),
        new(2, "Composants, props et etat", "Composants, props, state, events, conditionnel et ProductCard.",
            ["rn-component", "rn-props", "rn-usestate", "rn-events", "rn-conditional-rendering", "rn-product-card"]),
        new(3, "Listes mobiles", "FlatList, keys, renderItem, empty list et refresh.",
            ["rn-flatlist", "rn-flatlist-key", "rn-render-item", "rn-empty-list", "rn-refresh-control"]),
        new(4, "Formulaires mobiles", "TextInput, input controle, submit, validation et ProductForm.",
            ["rn-textinput", "rn-controlled-input", "rn-form-submit", "rn-form-validation", "rn-product-form"]),
        new(5, "Navigation", "Stack, screens, params, detail et tabs.",
            ["rn-navigation-stack", "rn-screen-component", "rn-navigation-navigate", "rn-navigation-params", "rn-product-detail-screen", "rn-tab-navigation-basic"]),
        new(6, "API et donnees", "useEffect, fetch, loading, error, service produit et refresh.",
            ["rn-useeffect-api", "rn-fetch-api", "rn-loading-state", "rn-error-state", "rn-product-service", "rn-refresh-products"]),
        new(7, "Stockage, plateforme et ergonomie mobile", "AsyncStorage, Platform, SafeAreaView, clavier, permissions et ActivityIndicator.",
            ["rn-async-storage", "rn-platform-select", "rn-safe-area", "rn-keyboard-avoiding-view", "rn-permissions-basic", "rn-activity-indicator"]),
        new(8, "Projet Product App", "Ecrans et flux complets de la Product App.",
            ["rn-project-home-screen", "rn-project-product-list", "rn-project-product-detail", "rn-project-product-form", "rn-project-navigation", "rn-project-api-loading", "rn-project-storage", "rn-project-polished-ui"])
    ];

    private static IReadOnlyList<StaticChapterDefinition> TailwindCssChapters() =>
    [
        new(1, "Fondations utility-first", "Classes utilitaires, texte, poids, couleurs, espacements, dimensions, bordures et ombres.",
            ["tailwind-utility-first", "tailwind-text", "tailwind-font-weight", "tailwind-colors", "tailwind-spacing", "tailwind-sizing", "tailwind-borders-radius", "tailwind-shadows"]),
        new(2, "Layout Tailwind", "Container, flex, alignements, grid, position, overflow et z-index.",
            ["tailwind-container", "tailwind-flex", "tailwind-flex-alignment", "tailwind-grid", "tailwind-grid-responsive", "tailwind-position", "tailwind-overflow", "tailwind-z-index"]),
        new(3, "Responsive design Tailwind", "Prefixes responsive, mobile-first, texte, grilles, navbar, cards et max-width.",
            ["tailwind-responsive-prefixes", "tailwind-mobile-first", "tailwind-responsive-text", "tailwind-responsive-grid", "tailwind-responsive-navbar", "tailwind-responsive-card-list", "tailwind-max-width"]),
        new(4, "Etats et interactions", "Hover, focus, rings, active, disabled, transitions, transforms et animations.",
            ["tailwind-hover-focus", "tailwind-active-disabled", "tailwind-focus-ring", "tailwind-transition", "tailwind-transform", "tailwind-animation-basic"]),
        new(5, "Formulaires et composants UI", "Bouton, card, badge, alert, input, form, table, navbar, sidebar et modal.",
            ["tailwind-button", "tailwind-card", "tailwind-badge", "tailwind-alert", "tailwind-input", "tailwind-form", "tailwind-table", "tailwind-navbar", "tailwind-sidebar", "tailwind-modal"]),
        new(6, "Dark mode et design system leger", "Dark mode, variantes, tokens visuels et theme dashboard.",
            ["tailwind-dark-mode-basic", "tailwind-dark-card", "tailwind-design-tokens", "tailwind-component-variants", "tailwind-dashboard-theme"]),
        new(7, "Projet Product Dashboard", "Sections concretes du Product Dashboard responsive.",
            ["tailwind-dashboard-layout", "tailwind-dashboard-sidebar", "tailwind-dashboard-navbar", "tailwind-dashboard-stats", "tailwind-dashboard-product-card", "tailwind-dashboard-table", "tailwind-dashboard-form", "tailwind-dashboard-responsive"])
    ];

    private static IReadOnlyList<StaticChapterDefinition> CssChapters() =>
    [
        new(1, "Fondations visuelles", "Selecteurs, couleurs, typographie, box model, radius et ombres.",
            ["css-selectors", "css-colors", "css-typography", "css-box-model", "css-border-radius", "css-shadows"]),
        new(2, "Layout moderne", "Display, flexbox, grid, position et overflow.",
            ["css-display", "css-flexbox", "css-grid", "css-position", "css-overflow"]),
        new(3, "Responsive Design", "Media queries, mobile-first, card, grid et navbar responsive.",
            ["css-media-queries", "css-mobile-first", "css-responsive-card", "css-responsive-grid", "css-responsive-navbar", "css-fluid-width", "css-clamp-font-size"]),
        new(4, "UI Components", "Bouton, card, navbar, grille produit et formulaire.",
            ["css-button", "css-card", "css-navbar", "css-product-grid", "css-form"]),
        new(5, "Projet responsive", "Sections completes d'une page produit responsive.",
            ["css-project-header", "css-project-product-hero", "css-project-product-cards", "css-project-responsive-grid", "css-project-form-section"])
    ];

    private static IReadOnlyList<StaticChapterDefinition> JavaScriptChapters() =>
    [
        new(1, "Fondations JS", "Variables, types, conditions, boucles, fonctions, tableaux et objets.",
            ["js-variables", "js-types", "js-conditions", "js-loops", "js-functions", "js-arrays", "js-objects"]),
        new(2, "JavaScript moderne", "Template literals, arrow functions, destructuring, spread/rest, collections et modules.",
            ["js-template-literals", "js-arrow-functions", "js-destructuring", "js-spread-rest", "js-map-filter-reduce", "js-modules-basic"]),
        new(3, "DOM", "Selection, texte, classList, creation d'elements, events et submit.",
            ["js-query-selector", "js-text-content", "js-class-list", "js-create-element", "js-event-listener", "js-form-submit"]),
        new(4, "Projet DOM interactif", "Rendu, ajout, suppression, filtre, etat vide et persistence locale.",
            ["js-render-product-list", "js-add-product", "js-delete-product", "js-filter-products", "js-empty-state", "js-local-storage-products"]),
        new(5, "Asynchrone", "Promises, async/await, fetch, loading/error et rendu API.",
            ["js-promises", "js-async-await", "js-fetch", "js-loading-error", "js-api-render-products"])
    ];

    private static Dictionary<string, StaticLessonDefinition> StaticLessonDefinitions()
    {
        var definitions = new Dictionary<string, StaticLessonDefinition>(StringComparer.OrdinalIgnoreCase);
        void Add(string slug, string title, string objective, string[] snippets, string correction, IReadOnlyList<(string Slug, int Weight)> skills) =>
            definitions[slug] = new StaticLessonDefinition(slug, title, objective, snippets, correction, skills);

        AddReactDefinitions(Add);
        AddReactNativeDefinitions(Add);
        AddTailwindCssDefinitions(Add);
        AddCssDefinitions(Add);
        AddJavaScriptDefinitions(Add);
        return definitions;
    }

    private static void AddReactDefinitions(Action<string, string, string, string[], string, IReadOnlyList<(string Slug, int Weight)>> add)
    {
        void R(string slug, string title, string objective, string[] snippets, string code, params (string Slug, int Weight)[] skills) =>
            add(slug, title, objective, snippets, code, skills);

        R("react-jsx", "JSX React", "Afficher un titre Product Manager en JSX.", ["function App", "return", "<h1", "Product Manager"], "function App() {\n  return <h1>Product Manager</h1>;\n}", ("react-jsx", 2));
        R("react-component-function", "Composant fonction", "Creer un composant fonction React compatible preview.", ["function App", "return", "<", ">"], "function App() {\n  return (\n    <section>\n      <h1>Product Manager</h1>\n    </section>\n  );\n}", ("react-components", 2), ("react-jsx", 1));
        R("react-fragments", "Fragments React", "Retourner plusieurs elements avec un Fragment.", ["<>", "</>", "<h1", "<p"], "function App() {\n  return <><h1>Products</h1><p>Manage catalog</p></>;\n}", ("react-jsx", 2), ("react-components", 1));
        R("react-classname", "className", "Styler un element JSX avec className.", ["className=", "product-card", "return"], "function App() {\n  return <article className=\"product-card\">Book</article>;\n}", ("react-jsx", 1), ("react-components", 1));
        R("react-expression-jsx", "Expressions JSX", "Afficher une expression JavaScript dans JSX.", ["{product.name}", "const product", "return"], "function App() {\n  const product = { name: 'Book' };\n  return <h2>{product.name}</h2>;\n}", ("react-jsx", 2));
        R("react-composition", "Composition", "Composer une interface avec plusieurs composants.", ["function ProductHeader", "function ProductList", "<ProductHeader", "<ProductList"], "function ProductHeader() { return <h1>Products</h1>; }\nfunction ProductList() { return <ul><li>Book</li></ul>; }\nfunction App() { return <><ProductHeader /><ProductList /></>; }", ("react-composition", 2), ("react-components", 1));

        R("react-props", "Props", "Passer un nom de produit par props.", ["props", "props.name", "function ProductCard"], "function ProductCard(props) {\n  return <article>{props.name}</article>;\n}\nfunction App() { return <ProductCard name=\"Book\" />; }", ("react-props", 2), ("react-components", 1));
        R("react-props-typescript", "Props TypeScript", "Typer les props avec type ProductProps.", ["type ProductProps", "name: string", "price: number"], "type ProductProps = { name: string; price: number };\nfunction ProductCard({ name, price }: ProductProps) { return <article>{name} - {price}</article>; }\nfunction App() { return <ProductCard name=\"Book\" price={12} />; }", ("react-typescript-props", 2), ("react-props", 1));
        R("react-children", "children", "Afficher children dans un composant.", ["children", "{children}", "function Panel"], "function Panel({ children }) { return <section>{children}</section>; }\nfunction App() { return <Panel><h1>Products</h1></Panel>; }", ("react-children", 2), ("react-composition", 1));
        R("react-component-reuse", "Reutilisation", "Reutiliser ProductCard deux fois.", ["<ProductCard", "name=", "price="], "function ProductCard({ name, price }) { return <article>{name} - {price}</article>; }\nfunction App() { return <><ProductCard name=\"Book\" price={12} /><ProductCard name=\"Pen\" price={2} /></>; }", ("react-components", 1), ("react-props", 1), ("react-composition", 1));
        R("react-product-card", "ProductCard", "Construire une carte produit reutilisable.", ["function ProductCard", "name", "price", "return", "className"], "function ProductCard({ name, price }) {\n  return <article className=\"product-card\"><h2>{name}</h2><p>{price}</p></article>;\n}\nfunction App() { return <ProductCard name=\"Book\" price={12} />; }", ("react-project", 1), ("react-props", 2), ("react-components", 1));

        R("react-usestate", "useState", "Gerer un etat local de compteur.", ["useState", "set", "count"], "function App() {\n  const [count, setCount] = useState(0);\n  return <button onClick={() => setCount(count + 1)}>Count: {count}</button>;\n}", ("react-state", 2), ("react-events", 1));
        R("react-events", "Evenements", "Reagir a un clic avec onClick.", ["onClick", "handleClick", "button"], "function App() { function handleClick() {} return <button onClick={handleClick}>Save</button>; }", ("react-events", 2));
        R("react-input-controlled", "Input controle", "Controler un input avec value et onChange.", ["useState", "<input", "value=", "onChange", "event.target.value"], "function App() {\n  const [name, setName] = useState('');\n  return <input value={name} onChange={(event) => setName(event.target.value)} />;\n}", ("react-controlled-inputs", 2), ("react-state", 1), ("react-events", 1));
        R("react-toggle", "Toggle", "Basculer un booleen avec useState.", ["useState", "setOpen", "!open"], "function App() { const [open, setOpen] = useState(false); return <button onClick={() => setOpen(!open)}>{open ? 'Open' : 'Closed'}</button>; }", ("react-state", 2), ("react-events", 1));
        R("react-counter", "Counter", "Incrementer un compteur.", ["useState", "count + 1", "onClick"], "function App() { const [count, setCount] = useState(0); return <button onClick={() => setCount(count + 1)}>{count}</button>; }", ("react-state", 2), ("react-events", 1));
        R("react-product-filter", "Filtre produit", "Filtrer des produits avec un input controle.", ["useState", "filter", "includes", "onChange"], "function App() { const products = ['Book', 'Pen']; const [query, setQuery] = useState(''); const filtered = products.filter((product) => product.includes(query)); return <><input value={query} onChange={(event) => setQuery(event.target.value)} />{filtered.map((product) => <p key={product}>{product}</p>)}</>; }", ("react-controlled-inputs", 1), ("react-state", 1), ("react-lists", 1));

        R("react-conditional-rendering", "Rendu conditionnel", "Afficher selon une condition.", ["?", ":", "isAvailable"], "function App() { const isAvailable = true; return <p>{isAvailable ? 'Available' : 'Out of stock'}</p>; }", ("react-conditional-rendering", 2));
        R("react-list-map", "Liste avec map", "Afficher une liste avec .map et key.", [".map(", "key=", "return"], "function App() { const products = [{ id: 1, name: 'Book' }]; return <ul>{products.map((product) => <li key={product.id}>{product.name}</li>)}</ul>; }", ("react-lists", 2), ("react-keys", 1));
        R("react-keys", "Keys", "Donner une key stable aux elements listes.", ["key={product.id}", ".map(", "<li"], "function App() { const products = [{ id: 1, name: 'Book' }]; return <ul>{products.map((product) => <li key={product.id}>{product.name}</li>)}</ul>; }", ("react-keys", 2), ("react-lists", 1));
        R("react-empty-state", "Etat vide", "Afficher un message si la liste est vide.", ["products.length === 0", "&&", "No products"], "function App() { const products = []; return <section>{products.length === 0 && <p>No products</p>}</section>; }", ("react-conditional-rendering", 2), ("react-lists", 1));
        R("react-product-list", "ProductList", "Construire une liste produit complete.", ["function ProductList", ".map(", "key=", "ProductCard"], "function ProductCard({ product }) { return <article>{product.name}</article>; }\nfunction ProductList({ products }) { return <section>{products.map((product) => <ProductCard key={product.id} product={product} />)}</section>; }\nfunction App() { return <ProductList products={[{ id: 1, name: 'Book' }]} />; }", ("react-lists", 2), ("react-components", 1));

        R("react-form-controlled", "Formulaire controle", "Controler les champs name et price.", ["useState", "value=", "onChange"], "function App() { const [name, setName] = useState(''); return <input value={name} onChange={(event) => setName(event.target.value)} />; }", ("react-forms", 1), ("react-controlled-inputs", 2));
        R("react-form-submit", "Submit", "Gerer onSubmit avec preventDefault.", ["<form", "onSubmit", "preventDefault", "useState"], "function App() { const [name, setName] = useState(''); function handleSubmit(event) { event.preventDefault(); } return <form onSubmit={handleSubmit}><input value={name} onChange={(event) => setName(event.target.value)} /></form>; }", ("react-forms", 2), ("react-events", 1));
        R("react-form-validation", "Validation formulaire", "Valider un nom requis.", ["if", "name.trim()", "setError"], "function App() { const [name, setName] = useState(''); const [error, setError] = useState(''); function handleSubmit(event) { event.preventDefault(); if (!name.trim()) { setError('Name is required'); } } return <form onSubmit={handleSubmit}><input value={name} onChange={(event) => setName(event.target.value)} />{error && <p>{error}</p>}</form>; }", ("react-form-validation", 2), ("react-forms", 1));
        R("react-form-errors", "Erreurs formulaire", "Afficher une erreur de validation.", ["error", "&&", "role=\"alert\""], "function App() { const error = 'Name is required'; return <section>{error && <p role=\"alert\">{error}</p>}</section>; }", ("react-form-validation", 2), ("react-conditional-rendering", 1));
        R("react-product-form", "ProductForm", "Assembler un formulaire d'ajout produit.", ["function ProductForm", "onSubmit", "onChange", "useState"], "function ProductForm({ onAdd }) { const [name, setName] = useState(''); function handleSubmit(event) { event.preventDefault(); onAdd(name); } return <form onSubmit={handleSubmit}><input value={name} onChange={(event) => setName(event.target.value)} /></form>; }\nfunction App() { return <ProductForm onAdd={() => {}} />; }", ("react-forms", 2), ("react-project", 1));

        R("react-useeffect", "useEffect", "Executer un effet au montage.", ["useEffect", "[]", "function App"], "function App() { useEffect(() => { document.title = 'Products'; }, []); return <h1>Products</h1>; }", ("react-effects", 2));
        R("react-fetch", "fetch", "Charger des produits avec fetch.", ["fetch", "async", "setProducts"], "function App() { const [products, setProducts] = useState([]); useEffect(() => { async function loadProducts() { const response = await fetch('/api/products'); setProducts(await response.json()); } loadProducts(); }, []); return <p>{products.length}</p>; }", ("react-api", 2), ("react-effects", 1));
        R("react-loading-state", "Loading", "Gerer un etat loading.", ["loading", "setLoading", "finally"], "function App() { const [loading, setLoading] = useState(false); async function load() { try {} finally { setLoading(false); } } return <p>{loading ? 'Loading' : 'Ready'}</p>; }", ("react-loading-error", 2), ("react-effects", 1));
        R("react-error-state", "Error", "Gerer une erreur API.", ["error", "setError", "catch"], "function App() { const [error, setError] = useState(''); async function load() { try {} catch (error) { setError('Unable to load products'); } } return <p>{error}</p>; }", ("react-loading-error", 2), ("react-api", 1));
        R("react-api-products", "API products", "Assembler useEffect, loading, error et products.", ["useEffect", "fetch", "setProducts", "loading", "error"], "function App() { const [products, setProducts] = useState([]); const [loading, setLoading] = useState(true); const [error, setError] = useState(''); useEffect(() => { async function loadProducts() { try { const response = await fetch('/api/products'); setProducts(await response.json()); } catch (error) { setError('Failed'); } finally { setLoading(false); } } loadProducts(); }, []); return <p>{loading ? 'Loading' : error || products.length}</p>; }", ("react-api", 2), ("react-effects", 1), ("react-loading-error", 1));

        R("react-custom-hook", "Custom hook", "Extraire une logique dans un hook.", ["function use", "return", "useState"], "function useProducts() { const [products, setProducts] = useState([]); return { products, setProducts }; }\nfunction App() { const { products } = useProducts(); return <p>{products.length}</p>; }", ("react-custom-hooks", 2), ("react-state", 1));
        R("react-usememo", "useMemo", "Memoiser une liste filtree.", ["useMemo", "filter", "[products]", "const filtered"], "function App() { const products = ['Book']; const query = ''; const filtered = useMemo(() => products.filter((product) => product.includes(query)), [products, query]); return <p>{filtered.length}</p>; }", ("react-memoization", 2), ("react-lists", 1));
        R("react-usecallback", "useCallback", "Stabiliser un callback.", ["useCallback", "setProducts", "[]"], "function App() { const [products, setProducts] = useState([]); const addProduct = useCallback((product) => { setProducts((items) => [...items, product]); }, []); return <button onClick={() => addProduct('Book')}>Add</button>; }", ("react-memoization", 2), ("react-state", 1));
        R("react-usereducer", "useReducer", "Gerer un etat complexe avec reducer.", ["useReducer", "reducer", "dispatch"], "function reducer(state, action) { return state; }\nfunction App() { const [products, dispatch] = useReducer(reducer, []); return <button onClick={() => dispatch({ type: 'add' })}>{products.length}</button>; }", ("react-reducer", 2), ("react-state", 1));
        R("react-use-products-hook", "useProducts hook", "Creer un hook Product Manager complet.", ["function useProducts", "useEffect", "fetch", "loading", "error"], "function useProducts() { const [products, setProducts] = useState([]); const [loading, setLoading] = useState(false); const [error, setError] = useState(''); useEffect(() => { fetch('/api/products').then((response) => response.json()).then(setProducts).catch(() => setError('Failed')).finally(() => setLoading(false)); }, []); return { products, loading, error }; }\nfunction App() { const data = useProducts(); return <p>{data.loading ? 'Loading' : data.products.length}</p>; }", ("react-custom-hooks", 2), ("react-effects", 1), ("react-api", 1));

        R("react-context", "Context", "Creer et lire un context React.", ["createContext", "useContext", "Provider"], "const ProductContext = createContext(null);\nfunction ProductProvider({ children }) { return <ProductContext.Provider value={{ products: [] }}>{children}</ProductContext.Provider>; }\nfunction App() { const value = useContext(ProductContext); return <ProductProvider><p>Products</p></ProductProvider>; }", ("react-context", 2));
        R("react-context-provider", "Provider", "Fournir un context aux enfants.", ["ProductContext.Provider", "value=", "children"], "const ProductContext = createContext(null);\nfunction ProductProvider({ children }) { return <ProductContext.Provider value={{ products: [] }}>{children}</ProductContext.Provider>; }\nfunction App() { return <ProductProvider><h1>Products</h1></ProductProvider>; }", ("react-context", 2), ("react-children", 1));
        R("react-router-basic", "Routing simule", "Representer une route simple sans routeur reel.", ["const routes", "path:", "component:"], "const routes = [{ path: '/products', component: ProductManager }];\nfunction ProductManager() { return <h1>Products</h1>; }\nfunction App() { return routes[0].component(); }", ("react-routing", 2), ("react-components", 1));
        R("react-route-params-simulated", "Parametre route simule", "Lire un id de route simule.", ["params", "id", "product"], "function ProductDetail({ params }) { const product = { id: params.id, name: 'Book' }; return <p>{product.name}</p>; }\nfunction App() { return <ProductDetail params={{ id: 1 }} />; }", ("react-routing", 2), ("react-props", 1));
        R("react-protected-route-basic", "Route protegee basique", "Afficher selon canAccess.", ["canAccess", "children", "return"], "function ProtectedRoute({ canAccess, children }) { return canAccess ? children : <p>Access denied</p>; }\nfunction App() { return <ProtectedRoute canAccess={true}><h1>Products</h1></ProtectedRoute>; }", ("react-routing", 1), ("react-conditional-rendering", 1));

        R("react-component-splitting", "Decoupage composant", "Separer layout, list et form.", ["function ProductLayout", "function ProductList", "function ProductForm"], "function ProductList() { return <section>List</section>; }\nfunction ProductForm() { return <form />; }\nfunction ProductLayout() { return <><ProductList /><ProductForm /></>; }\nfunction App() { return <ProductLayout />; }", ("react-components", 2), ("react-project", 1));
        R("react-accessibility-basic", "Accessibilite", "Associer label et input.", ["label", "htmlFor", "id="], "function App() { return <><label htmlFor=\"name\">Name</label><input id=\"name\" aria-label=\"Product name\" /></>; }", ("react-accessibility", 2), ("react-forms", 1));
        R("react-performance-basic", "Performance de base", "Utiliser useMemo pour eviter un recalcul.", ["useMemo", "products", "filter"], "function App() { const products = ['Book']; const filtered = useMemo(() => products.filter((product) => product), [products]); return <p>{filtered.length}</p>; }", ("react-memoization", 2), ("react-lists", 1));
        R("react-testing-component", "Test composant", "Tester un rendu avec Testing Library.", ["render", "screen", "expect"], "render(<ProductCard product={{ id: 1, name: 'Book' }} />);\nexpect(screen.getByText('Book')).toBeInTheDocument();", ("react-testing", 2));
        R("react-testing-user-event", "Test interaction", "Tester une action utilisateur.", ["userEvent", "click", "expect"], "await userEvent.click(screen.getByRole('button'));\nexpect(screen.getByText('Saved')).toBeInTheDocument();", ("react-testing", 2), ("react-events", 1));

        R("react-project-layout", "Layout projet", "Structurer l'ecran Product Manager.", ["function ProductManager", "ProductList", "ProductForm"], "function ProductList() { return <section>List</section>; }\nfunction ProductForm() { return <form />; }\nfunction ProductManager() { return <main><ProductForm /><ProductList /></main>; }", ("react-project", 2), ("react-components", 1));
        R("react-project-product-list", "Liste projet", "Afficher la liste produits du projet.", [".map(", "key=", "ProductCard"], "function ProductCard({ product }) { return <article>{product.name}</article>; }\nfunction ProductManager() { const products = [{ id: 1, name: 'Book' }]; return <section>{products.map((product) => <ProductCard key={product.id} product={product} />)}</section>; }", ("react-project", 2), ("react-lists", 1));
        R("react-project-product-filter", "Filtre projet", "Filtrer les produits du projet.", ["useState", "filter", "includes"], "function ProductManager() { const products = ['Book']; const [query, setQuery] = useState(''); const filtered = products.filter((product) => product.includes(query)); return <input value={query} onChange={(event) => setQuery(event.target.value)} />; }", ("react-project", 2), ("react-state", 1));
        R("react-project-product-form", "Formulaire projet", "Ajouter un produit via formulaire.", ["onSubmit", "useState", "setProducts"], "function ProductManager() { const [products, setProducts] = useState([]); function handleSubmit(event) { event.preventDefault(); setProducts((items) => [...items, { id: Date.now(), name: 'Book' }]); } return <form onSubmit={handleSubmit} />; }", ("react-project", 2), ("react-forms", 1));
        R("react-project-product-detail", "Detail projet", "Afficher un detail produit.", ["function ProductDetail", "product", "return"], "function ProductDetail({ product }) { return <section>{product.name}</section>; }\nfunction ProductManager() { return <ProductDetail product={{ name: 'Book' }} />; }", ("react-project", 2), ("react-props", 1));
        R("react-project-api-loading", "Loading projet", "Afficher loading pendant fetch.", ["loading", "useEffect", "fetch"], "function ProductManager() { const [loading, setLoading] = useState(false); useEffect(() => { fetch('/api/products').finally(() => setLoading(false)); }, []); return <p>{loading ? 'Loading' : 'Ready'}</p>; }", ("react-project", 1), ("react-api", 2));
        R("react-project-error-handling", "Erreurs projet", "Afficher une erreur API.", ["error", "catch", "role=\"alert\""], "function ProductManager() { const [error, setError] = useState(''); useEffect(() => { fetch('/api/products').catch(() => setError('Unable to load products')); }, []); return <>{error && <p role=\"alert\">{error}</p>}</>; }", ("react-project", 1), ("react-loading-error", 2));

        R("react-boss-final-product-manager", "Boss Final React : Product Manager", "Construire une interface Product Manager.", ["function ProductManager", "function ProductCard", "useState", "onChange", "onSubmit", "preventDefault", ".map(", "key=", "&&", "useEffect", "fetch", "function useProducts", "createContext", "htmlFor", "type"], "type Product = { id: number; name: string; price: number };\nconst ProductContext = createContext(null);\nfunction useProducts() { const [products, setProducts] = useState<Product[]>([]); const [loading, setLoading] = useState(false); const [error, setError] = useState(''); useEffect(() => { async function loadProducts() { try { const response = await fetch('/api/products'); setProducts(await response.json()); } catch (error) { setError('Failed'); } finally { setLoading(false); } } loadProducts(); }, []); return { products, setProducts, loading, error }; }\nfunction ProductCard({ product }: { product: Product }) { return <article><h2>{product.name}</h2><p>{product.price}</p></article>; }\nfunction ProductManager() { const { products, setProducts, loading, error } = useProducts(); const [name, setName] = useState(''); function handleSubmit(event) { event.preventDefault(); setProducts((items) => [...items, { id: Date.now(), name, price: 0 }]); } const filtered = products.filter((product) => product.name.includes(name)); return <ProductContext.Provider value={{ products }}><form onSubmit={handleSubmit}><label htmlFor=\"name\">Name</label><input id=\"name\" aria-label=\"Product name\" value={name} onChange={(event) => setName(event.target.value)} /><button>Add</button></form>{loading && <p>Loading</p>}{error && <p role=\"alert\">{error}</p>}{filtered.length ? filtered.map((product) => <ProductCard key={product.id} product={product} />) : <p>No products</p>}</ProductContext.Provider>; }", ("react-project", 2), ("react-components", 1), ("react-custom-hooks", 1));
    }

    private static void AddReactNativeDefinitions(Action<string, string, string, string[], string, IReadOnlyList<(string Slug, int Weight)>> add)
    {
        void N(string slug, string title, string objective, string[] snippets, string code, params (string Slug, int Weight)[] skills) =>
            add(slug, title, objective, snippets, code, skills);

        N("rn-view-text", "View et Text", "Afficher un ecran simple avec View et Text.", ["View", "Text", "return"], "function HomeScreen() {\n  return <View><Text>Product App</Text></View>;\n}", ("rn-view-text", 2), ("rn-core-components", 1));
        N("rn-style-sheet", "StyleSheet", "Styler un ecran avec StyleSheet.create.", ["StyleSheet.create", "container", "padding"], "const styles = StyleSheet.create({\n  container: { flex: 1, padding: 16 },\n  title: { fontSize: 24 }\n});", ("rn-stylesheet", 2), ("rn-styling", 1));
        N("rn-flexbox", "Flexbox RN", "Organiser une ligne avec flexbox.", ["flex", "flexDirection", "justifyContent", "alignItems"], "const styles = StyleSheet.create({\n  row: { flex: 1, flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }\n});", ("rn-flexbox", 2), ("rn-styling", 1));
        N("rn-button-pressable", "Pressable et Button", "Reagir a une pression mobile.", ["Pressable", "onPress", "Text"], "function SaveButton() {\n  return <Pressable onPress={handleSave}><Text>Save</Text></Pressable>;\n}", ("rn-events", 2), ("rn-core-components", 1));
        N("rn-image", "Image", "Afficher une image produit.", ["Image", "source=", "style="], "<Image source={{ uri: product.imageUrl }} style={styles.image} />", ("rn-core-components", 2), ("rn-styling", 1));
        N("rn-scrollview", "ScrollView", "Rendre un contenu scrollable.", ["ScrollView", "Text", "return"], "function ProductScreen() {\n  return <ScrollView><Text>Products</Text></ScrollView>;\n}", ("rn-core-components", 2));

        N("rn-component", "Composant RN", "Creer un composant mobile ProductTitle.", ["function ProductTitle", "View", "Text"], "function ProductTitle() {\n  return <View><Text>Products</Text></View>;\n}", ("rn-core-components", 1), ("rn-props-state", 1));
        N("rn-props", "Props RN", "Passer un produit en props.", ["type Props", "product", "product.name"], "type Props = { product: { name: string; price: number } };\nfunction ProductRow({ product }: Props) {\n  return <Text>{product.name}</Text>;\n}", ("rn-props-state", 2));
        N("rn-usestate", "useState RN", "Gerer un etat local mobile.", ["useState", "set", "Text"], "function ProductToggle() {\n  const [selected, setSelected] = useState(false);\n  return <Text>{selected ? 'Selected' : 'Product'}</Text>;\n}", ("rn-props-state", 2));
        N("rn-events", "Evenements RN", "Utiliser onPress pour modifier l'etat.", ["onPress", "set", "Pressable"], "<Pressable onPress={() => setSelected(true)}><Text>Select</Text></Pressable>", ("rn-events", 2), ("rn-props-state", 1));
        N("rn-conditional-rendering", "Rendu conditionnel RN", "Afficher selon un etat.", ["?", ":", "Text"], "{available ? <Text>Available</Text> : <Text>Out</Text>}", ("rn-props-state", 1), ("rn-core-components", 1));
        N("rn-product-card", "ProductCard mobile", "Afficher une carte produit mobile.", ["function ProductCard", "name", "price", "View", "Text", "StyleSheet.create"], "function ProductCard({ product }) {\n  return <View style={styles.card}><Text>{product.name}</Text><Text>{product.price}</Text></View>;\n}\nconst styles = StyleSheet.create({ card: { padding: 16 } });", ("rn-core-components", 1), ("rn-props-state", 1), ("rn-stylesheet", 1));

        N("rn-flatlist", "FlatList", "Afficher une liste produits avec FlatList.", ["FlatList", "data=", "renderItem", "keyExtractor"], "<FlatList data={products} keyExtractor={(item) => String(item.id)} renderItem={({ item }) => <Text>{item.name}</Text>} />", ("rn-flatlist", 2), ("rn-lists", 1));
        N("rn-flatlist-key", "keyExtractor", "Ajouter une key stable a FlatList.", ["keyExtractor", "item.id", "FlatList"], "<FlatList data={products} keyExtractor={(item) => String(item.id)} renderItem={({ item }) => <Text>{item.name}</Text>} />", ("rn-flatlist", 2), ("rn-lists", 1));
        N("rn-render-item", "renderItem", "Extraire le rendu d'un item FlatList.", ["renderItem", "item", "Text"], "const renderItem = ({ item }) => <Text>{item.name}</Text>;\n<FlatList data={products} renderItem={renderItem} keyExtractor={(item) => String(item.id)} />", ("rn-flatlist", 2), ("rn-lists", 1));
        N("rn-empty-list", "Liste vide", "Afficher un etat vide FlatList.", ["ListEmptyComponent", "No products", "FlatList"], "<FlatList data={products} ListEmptyComponent={<Text>No products</Text>} renderItem={({ item }) => <Text>{item.name}</Text>} />", ("rn-lists", 2), ("rn-flatlist", 1));
        N("rn-refresh-control", "RefreshControl", "Ajouter un pull-to-refresh.", ["RefreshControl", "refreshing", "onRefresh"], "<FlatList refreshControl={<RefreshControl refreshing={refreshing} onRefresh={loadProducts} />} data={products} renderItem={renderItem} keyExtractor={(item) => String(item.id)} />", ("rn-lists", 1), ("rn-api", 1));

        N("rn-textinput", "TextInput", "Saisir un nom de produit.", ["TextInput", "placeholder", "Product name"], "<TextInput placeholder=\"Product name\" />", ("rn-inputs", 2), ("rn-forms", 1));
        N("rn-controlled-input", "Input controle RN", "Controler TextInput avec value et onChangeText.", ["TextInput", "value=", "onChangeText", "useState"], "function ProductNameInput() {\n  const [name, setName] = useState('');\n  return <TextInput value={name} onChangeText={setName} />;\n}", ("rn-inputs", 2), ("rn-props-state", 1), ("rn-forms", 1));
        N("rn-form-submit", "Submit mobile", "Soumettre un formulaire avec Pressable.", ["Pressable", "onPress", "useState"], "function ProductForm() {\n  const [name, setName] = useState('');\n  return <Pressable onPress={() => handleSubmit(name)}><Text>Save</Text></Pressable>;\n}", ("rn-forms", 2), ("rn-events", 1));
        N("rn-form-validation", "Validation mobile", "Valider un nom requis.", ["name.trim()", "setError", "if"], "if (!name.trim()) {\n  setError('Name is required');\n}", ("rn-forms", 2), ("rn-inputs", 1));
        N("rn-product-form", "ProductForm mobile", "Assembler TextInput, state et Pressable.", ["TextInput", "Pressable", "onChangeText", "onPress", "useState"], "function ProductForm() {\n  const [name, setName] = useState('');\n  return <View><TextInput value={name} onChangeText={setName} /><Pressable onPress={handleSubmit}><Text>Save</Text></Pressable></View>;\n}", ("rn-forms", 2), ("rn-inputs", 1), ("rn-project", 1));

        N("rn-navigation-stack", "Stack navigation", "Declarer une Stack Navigator.", ["createNativeStackNavigator", "Stack.Navigator", "Stack.Screen"], "const Stack = createNativeStackNavigator();\n<Stack.Navigator><Stack.Screen name=\"Products\" component={ProductsScreen} /></Stack.Navigator>", ("rn-navigation", 2));
        N("rn-screen-component", "Screen component", "Creer un ecran ProductsScreen.", ["function ProductsScreen", "View", "Text"], "function ProductsScreen() {\n  return <View><Text>Products</Text></View>;\n}", ("rn-navigation", 1), ("rn-core-components", 1));
        N("rn-navigation-navigate", "navigation.navigate", "Naviguer vers un detail produit.", ["navigation.navigate", "ProductDetail", "productId"], "navigation.navigate('ProductDetail', { productId: product.id });", ("rn-navigation", 2));
        N("rn-navigation-params", "Params navigation", "Lire route.params dans un ecran detail.", ["route.params", "navigation", "ProductDetail"], "function ProductDetailScreen({ route, navigation }) {\n  const { productId } = route.params;\n  return <Text onPress={() => navigation.navigate('Products')}>ProductDetail {productId}</Text>;\n}", ("rn-navigation-params", 2), ("rn-navigation", 1));
        N("rn-product-detail-screen", "Detail mobile", "Construire un ecran detail produit.", ["function ProductDetailScreen", "route.params", "Text"], "function ProductDetailScreen({ route }) {\n  const { product } = route.params;\n  return <View><Text>{product.name}</Text></View>;\n}", ("rn-navigation", 1), ("rn-navigation-params", 1));
        N("rn-tab-navigation-basic", "Tabs RN", "Declarer une navigation par onglets.", ["createBottomTabNavigator", "Tab.Navigator", "Tab.Screen"], "const Tab = createBottomTabNavigator();\n<Tab.Navigator><Tab.Screen name=\"Products\" component={ProductsScreen} /></Tab.Navigator>", ("rn-navigation", 2));

        N("rn-useeffect-api", "useEffect API RN", "Charger au montage avec useEffect.", ["useEffect", "loadProducts", "[]"], "useEffect(() => { loadProducts(); }, []);", ("rn-api", 1), ("rn-loading-error", 1));
        N("rn-fetch-api", "fetch RN", "Charger des produits avec fetch.", ["fetch", "async", "await", "set"], "async function loadProducts() {\n  const response = await fetch('/api/products');\n  setProducts(await response.json());\n}", ("rn-api", 2));
        N("rn-loading-state", "Loading RN", "Afficher un etat loading.", ["loading", "setLoading", "ActivityIndicator"], "{loading ? <ActivityIndicator /> : <FlatList data={products} renderItem={renderItem} keyExtractor={(item) => String(item.id)} />}", ("rn-loading-error", 2), ("rn-api", 1));
        N("rn-error-state", "Error RN", "Afficher une erreur API.", ["error", "setError", "Text"], "{error ? <Text>{error}</Text> : null}", ("rn-loading-error", 2), ("rn-api", 1));
        N("rn-product-service", "Product service RN", "Extraire fetch dans un service.", ["async function getProducts", "fetch", "return"], "async function getProducts() {\n  const response = await fetch('/api/products');\n  return response.json();\n}", ("rn-api", 2), ("rn-project", 1));
        N("rn-refresh-products", "Refresh products", "Recharger une liste de produits.", ["refreshing", "setRefreshing", "loadProducts"], "async function refreshProducts() {\n  setRefreshing(true);\n  await loadProducts();\n  setRefreshing(false);\n}", ("rn-api", 1), ("rn-loading-error", 1));

        N("rn-async-storage", "AsyncStorage", "Sauvegarder et relire une liste locale.", ["AsyncStorage", "setItem", "getItem", "JSON.stringify", "JSON.parse"], "await AsyncStorage.setItem('products', JSON.stringify(products));\nconst savedProducts = JSON.parse(await AsyncStorage.getItem('products') ?? '[]');", ("rn-storage", 2));
        N("rn-platform-select", "Platform.select", "Adapter un style selon plateforme.", ["Platform.select", "ios", "android"], "const padding = Platform.select({ ios: 16, android: 12 });", ("rn-platform", 2));
        N("rn-safe-area", "SafeAreaView", "Respecter les zones sures mobiles.", ["SafeAreaView", "View", "Text"], "<SafeAreaView><View><Text>Products</Text></View></SafeAreaView>", ("rn-safe-area", 2), ("rn-core-components", 1));
        N("rn-keyboard-avoiding-view", "KeyboardAvoidingView", "Eviter le clavier sur formulaire.", ["KeyboardAvoidingView", "behavior", "TextInput"], "<KeyboardAvoidingView behavior=\"padding\"><TextInput /></KeyboardAvoidingView>", ("rn-platform", 1), ("rn-forms", 1));
        N("rn-permissions-basic", "Permissions", "Representer une demande de permission.", ["request", "permission", "granted"], "const permission = await requestPermission();\nif (permission === 'granted') {}", ("rn-permissions", 2));
        N("rn-activity-indicator", "ActivityIndicator", "Afficher une attente mobile.", ["ActivityIndicator", "loading", "Text"], "{loading ? <ActivityIndicator /> : <Text>Products loaded</Text>}", ("rn-loading-error", 2), ("rn-core-components", 1));

        N("rn-project-home-screen", "Home Product App", "Construire l'ecran d'accueil mobile.", ["SafeAreaView", "ProductList", "ProductForm"], "function HomeScreen() {\n  return <SafeAreaView><ProductForm /><ProductList products={products} /></SafeAreaView>;\n}", ("rn-project", 2), ("rn-safe-area", 1), ("rn-core-components", 1));
        N("rn-project-product-list", "Liste Product App", "Afficher FlatList dans le projet.", ["FlatList", "ProductCard", "keyExtractor", "renderItem"], "<FlatList data={products} keyExtractor={(item) => String(item.id)} renderItem={({ item }) => <ProductCard product={item} />} />", ("rn-project", 2), ("rn-flatlist", 1));
        N("rn-project-product-detail", "Detail Product App", "Afficher un detail avec route.params.", ["route.params", "ProductDetail", "Text"], "function ProductDetail({ route }) {\n  const { product } = route.params;\n  return <Text>{product.name}</Text>;\n}", ("rn-project", 2), ("rn-navigation-params", 1));
        N("rn-project-product-form", "Form Product App", "Ajouter un produit mobile.", ["TextInput", "Pressable", "onPress", "onChangeText"], "<TextInput value={name} onChangeText={setName} /><Pressable onPress={handleSubmit}><Text>Save</Text></Pressable>", ("rn-project", 2), ("rn-forms", 1));
        N("rn-project-navigation", "Navigation Product App", "Relier liste et detail.", ["navigation.navigate", "route.params", "Stack.Screen"], "navigation.navigate('ProductDetail', { product });", ("rn-project", 2), ("rn-navigation", 1));
        N("rn-project-api-loading", "API loading Product App", "Charger les produits avec loading/error.", ["useEffect", "fetch", "loading", "error", "ActivityIndicator"], "useEffect(() => { fetch('/api/products').catch(() => setError('Failed')).finally(() => setLoading(false)); }, []);\n{loading ? <ActivityIndicator /> : error ? <Text>{error}</Text> : null}", ("rn-project", 1), ("rn-api", 1), ("rn-loading-error", 1));
        N("rn-project-storage", "Storage Product App", "Persist local avec AsyncStorage.", ["AsyncStorage", "setItem", "getItem", "JSON.stringify"], "await AsyncStorage.setItem('products', JSON.stringify(products));\nawait AsyncStorage.getItem('products');", ("rn-project", 1), ("rn-storage", 2));
        N("rn-project-polished-ui", "UI mobile soignee", "Assembler styles, SafeAreaView et layout mobile.", ["StyleSheet.create", "SafeAreaView", "flex", "padding"], "function ProductApp() { return <SafeAreaView style={styles.container}><Text>Products</Text></SafeAreaView>; }\nconst styles = StyleSheet.create({ container: { flex: 1, padding: 16 } });", ("rn-project", 2), ("rn-styling", 1), ("rn-safe-area", 1));

        N("react-native-boss-final-product-app", "Boss Final React Native : Product App", "Construire une mini-application mobile Product App.", ["View", "Text", "StyleSheet.create", "useState", "useEffect", "FlatList", "TextInput", "Pressable", "navigation.navigate", "route.params", "fetch", "AsyncStorage", "SafeAreaView", "ActivityIndicator", "keyExtractor", "renderItem"], "function ProductApp({ navigation, route }) {\n  const [products, setProducts] = useState([]);\n  const [loading, setLoading] = useState(true);\n  const [error, setError] = useState('');\n  useEffect(() => {\n    async function loadProducts() {\n      try {\n        const response = await fetch('/api/products');\n        const data = await response.json();\n        setProducts(data);\n        await AsyncStorage.setItem('products', JSON.stringify(data));\n        await AsyncStorage.getItem('products');\n      } catch (error) {\n        setError('Failed');\n      } finally {\n        setLoading(false);\n      }\n    }\n    loadProducts();\n  }, []);\n  const renderItem = ({ item }) => <Pressable onPress={() => navigation.navigate('ProductDetail', { productId: item.id })}><Text>{item.name}</Text></Pressable>;\n  if (loading) return <ActivityIndicator />;\n  if (error) return <Text>{error}</Text>;\n  return <SafeAreaView style={styles.container}><View><Text>Products {route.params?.productId}</Text><TextInput /><FlatList data={products} keyExtractor={(item) => String(item.id)} renderItem={renderItem} /></View></SafeAreaView>;\n}\nconst styles = StyleSheet.create({ container: { flex: 1, padding: 16 } });", ("rn-project", 2), ("rn-flatlist", 1), ("rn-navigation", 1));
    }

    private static void AddTailwindCssDefinitions(Action<string, string, string, string[], string, IReadOnlyList<(string Slug, int Weight)>> add)
    {
        void T(string slug, string title, string objective, string[] snippets, string code, params (string Slug, int Weight)[] skills) =>
            add(slug, title, objective, snippets, code, skills);

        T("tailwind-utility-first", "Utility-first", "Styliser une carte produit avec des classes utilitaires.", ["class=", "p-", "bg-", "text-"], "<article class=\"bg-white p-4 text-slate-900\">Book</article>", ("tailwind-utility-first", 2), ("tailwind-spacing", 1));
        T("tailwind-text", "Texte Tailwind", "Styliser un titre avec une taille de texte.", ["text-", "Product Dashboard", "class="], "<h1 class=\"text-3xl text-slate-900\">Product Dashboard</h1>", ("tailwind-typography", 2));
        T("tailwind-font-weight", "Graisse de police", "Mettre en avant un titre avec font-*.", ["font-", "text-", "Product"], "<h2 class=\"text-xl font-semibold text-slate-900\">Product</h2>", ("tailwind-typography", 2));
        T("tailwind-colors", "Couleurs", "Composer fond, texte et accent couleur.", ["bg-", "text-", "border-"], "<div class=\"border border-slate-200 bg-white text-slate-900\">Product</div>", ("tailwind-colors", 2), ("tailwind-borders", 1));
        T("tailwind-spacing", "Espacements", "Gerer padding, margin et gap.", ["p-", "m-", "gap-"], "<section class=\"m-4 grid gap-4 p-6\">Products</section>", ("tailwind-spacing", 2), ("tailwind-layout", 1));
        T("tailwind-sizing", "Dimensions", "Fixer largeur et hauteur utiles.", ["w-", "h-", "class="], "<div class=\"h-24 w-full\">Chart</div>", ("tailwind-sizing", 2), ("tailwind-layout", 1));
        T("tailwind-borders-radius", "Bordures et radius", "Ajouter une bordure et des coins arrondis.", ["border", "rounded", "border-"], "<article class=\"rounded-lg border border-slate-200\">Book</article>", ("tailwind-borders", 2), ("tailwind-components", 1));
        T("tailwind-shadows", "Ombres", "Donner de la profondeur a une card.", ["shadow", "rounded", "bg-"], "<article class=\"rounded-lg bg-white shadow-sm\">Book</article>", ("tailwind-shadows", 2), ("tailwind-components", 1));

        T("tailwind-container", "Container", "Centrer une zone de page.", ["container", "mx-auto", "p-"], "<main class=\"container mx-auto p-6\">Dashboard</main>", ("tailwind-layout", 2), ("tailwind-spacing", 1));
        T("tailwind-flex", "Flex", "Creer une ligne flex.", ["flex", "gap-", "class="], "<section class=\"flex gap-4\">Products</section>", ("tailwind-flexbox", 2), ("tailwind-layout", 1));
        T("tailwind-flex-alignment", "Alignements flex", "Aligner une navbar dashboard.", ["flex", "items-center", "justify-between"], "<nav class=\"flex items-center justify-between\">Products</nav>", ("tailwind-flexbox", 2), ("tailwind-layout", 1));
        T("tailwind-grid", "Grid", "Construire une grille de stats.", ["grid", "grid-cols-", "gap-"], "<section class=\"grid grid-cols-3 gap-4\">Stats</section>", ("tailwind-grid", 2), ("tailwind-spacing", 1));
        T("tailwind-grid-responsive", "Grid responsive", "Creer une grille qui change selon la largeur.", ["grid", "grid-cols-1", "md:grid-cols-", "gap-"], "<section class=\"grid grid-cols-1 gap-4 md:grid-cols-2\">Products</section>", ("tailwind-grid", 2), ("tailwind-responsive", 1));
        T("tailwind-position", "Position", "Positionner un badge sur une card.", ["relative", "absolute", "top-"], "<article class=\"relative\"><span class=\"absolute right-2 top-2\">New</span></article>", ("tailwind-layout", 2), ("tailwind-components", 1));
        T("tailwind-overflow", "Overflow", "Masquer le debordement d'une card.", ["overflow-hidden", "rounded", "class="], "<article class=\"overflow-hidden rounded-lg\">Image</article>", ("tailwind-layout", 1), ("tailwind-components", 1));
        T("tailwind-z-index", "z-index", "Superposer une modal au dashboard.", ["z-", "fixed", "inset-0"], "<div class=\"fixed inset-0 z-50\">Modal</div>", ("tailwind-layout", 2), ("tailwind-components", 1));

        T("tailwind-responsive-prefixes", "Prefixes responsive", "Utiliser sm, md, lg et xl.", ["sm:", "md:", "lg:", "xl:"], "<div class=\"text-sm sm:text-base md:text-lg lg:text-xl xl:text-2xl\">Dashboard</div>", ("tailwind-responsive", 2), ("tailwind-typography", 1));
        T("tailwind-mobile-first", "Mobile-first", "Partir d'une colonne mobile puis elargir.", ["grid-cols-1", "md:grid-cols-", "lg:grid-cols-"], "<section class=\"grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-4\">Stats</section>", ("tailwind-responsive", 2), ("tailwind-grid", 1));
        T("tailwind-responsive-text", "Texte responsive", "Adapter la taille du titre selon l'ecran.", ["text-", "md:text-", "lg:text-"], "<h1 class=\"text-2xl md:text-4xl lg:text-5xl\">Product Dashboard</h1>", ("tailwind-responsive", 2), ("tailwind-typography", 1));
        T("tailwind-responsive-grid", "Grille responsive", "Creer une grille produits 1/2/3 colonnes.", ["grid", "grid-cols-1", "md:grid-cols-2", "lg:grid-cols-3", "gap-"], "<section class=\"grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3\"><article>Book</article></section>", ("tailwind-responsive", 2), ("tailwind-grid", 1), ("tailwind-spacing", 1));
        T("tailwind-responsive-navbar", "Navbar responsive", "Adapter une navbar mobile et desktop.", ["flex", "hidden", "md:block", "md:flex"], "<nav class=\"flex items-center justify-between\"><button class=\"md:hidden\">Menu</button><div class=\"hidden md:block\">Links</div><div class=\"hidden md:flex\">Actions</div></nav>", ("tailwind-responsive", 2), ("tailwind-components", 1));
        T("tailwind-responsive-card-list", "Liste de cards responsive", "Afficher des cards produits sur plusieurs tailles.", ["grid", "gap-", "sm:grid-cols-", "lg:grid-cols-"], "<section class=\"grid gap-4 sm:grid-cols-2 lg:grid-cols-4\">Cards</section>", ("tailwind-responsive", 2), ("tailwind-dashboard", 1));
        T("tailwind-max-width", "Max width", "Limiter la largeur d'un dashboard et le centrer.", ["max-w-", "mx-auto", "w-full"], "<main class=\"mx-auto w-full max-w-6xl p-6\">Dashboard</main>", ("tailwind-responsive", 1), ("tailwind-sizing", 2), ("tailwind-layout", 1));

        T("tailwind-hover-focus", "Hover et focus", "Ajouter des etats interactifs.", ["hover:", "focus:", "transition"], "<button class=\"transition hover:bg-slate-100 focus:ring-2\">Save</button>", ("tailwind-states", 2), ("tailwind-components", 1));
        T("tailwind-active-disabled", "Active et disabled", "Styliser active et disabled.", ["active:", "disabled:", "opacity-"], "<button class=\"active:scale-95 disabled:opacity-50\">Save</button>", ("tailwind-states", 2));
        T("tailwind-focus-ring", "Focus ring", "Rendre un input lisible au clavier.", ["focus:", "focus:ring", "outline"], "<input class=\"rounded border p-2 outline-none focus:ring-2 focus:ring-blue-500\" />", ("tailwind-states", 2), ("tailwind-forms", 1));
        T("tailwind-transition", "Transition", "Animer doucement un changement.", ["transition", "duration-", "hover:"], "<button class=\"transition duration-200 hover:bg-blue-600\">Save</button>", ("tailwind-transitions", 2), ("tailwind-states", 1));
        T("tailwind-transform", "Transform", "Appliquer scale et translate.", ["scale-", "translate-", "hover:"], "<div class=\"translate-y-0 hover:scale-105\">Card</div>", ("tailwind-transitions", 1), ("tailwind-states", 1));
        T("tailwind-animation-basic", "Animation", "Utiliser une animation Tailwind simple.", ["animate-", "class=", "Product"], "<span class=\"animate-pulse\">Product</span>", ("tailwind-transitions", 1), ("tailwind-states", 1));

        T("tailwind-button", "Bouton", "Construire un bouton principal.", ["button", "px-", "py-", "rounded", "bg-", "hover:"], "<button class=\"rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700\">Add</button>", ("tailwind-components", 2), ("tailwind-states", 1));
        T("tailwind-card", "Card", "Construire une card produit complete.", ["p-", "bg-white", "rounded", "shadow", "text-", "font-", "<button"], "<article class=\"rounded-lg bg-white p-4 shadow\"><h2 class=\"text-lg font-semibold\">Book</h2><p class=\"text-slate-600\">12 EUR</p><button class=\"mt-4 rounded bg-blue-600 px-3 py-2 text-white\">Add</button></article>", ("tailwind-components", 2), ("tailwind-spacing", 1), ("tailwind-shadows", 1));
        T("tailwind-badge", "Badge", "Afficher un badge de statut.", ["inline-flex", "rounded", "px-", "text-"], "<span class=\"inline-flex rounded-full bg-green-100 px-2 text-green-700\">In stock</span>", ("tailwind-components", 2), ("tailwind-colors", 1));
        T("tailwind-alert", "Alert", "Afficher une alerte informative.", ["rounded", "border", "bg-", "text-"], "<div class=\"rounded border border-blue-200 bg-blue-50 p-3 text-blue-900\">Saved</div>", ("tailwind-components", 2), ("tailwind-colors", 1));
        T("tailwind-input", "Input", "Styliser un champ de formulaire.", ["<input", "rounded", "border", "focus:"], "<input class=\"rounded border border-slate-300 p-2 focus:ring-2 focus:ring-blue-500\" />", ("tailwind-forms", 2), ("tailwind-states", 1));
        T("tailwind-form", "Formulaire", "Styliser un formulaire produit.", ["<form", "<input", "type=\"text\"", "type=\"number\"", "focus:", "rounded", "border", "<button"], "<form class=\"grid gap-3\"><input type=\"text\" class=\"rounded border p-2 focus:ring-2\" /><input type=\"number\" class=\"rounded border p-2 focus:ring-2\" /><button class=\"rounded bg-blue-600 px-4 py-2 text-white\">Save</button></form>", ("tailwind-forms", 2), ("tailwind-components", 1), ("tailwind-states", 1));
        T("tailwind-table", "Table", "Styliser un tableau produits.", ["table", "w-full", "text-", "border"], "<table class=\"w-full text-left text-sm\"><tr class=\"border-b\"><td>Book</td></tr></table>", ("tailwind-components", 2), ("tailwind-dashboard", 1));
        T("tailwind-navbar", "Navbar", "Construire une navbar dashboard.", ["nav", "flex", "items-center", "justify-between"], "<nav class=\"flex items-center justify-between p-4\">Dashboard</nav>", ("tailwind-components", 2), ("tailwind-flexbox", 1));
        T("tailwind-sidebar", "Sidebar", "Construire une sidebar produit.", ["aside", "w-", "h-", "bg-"], "<aside class=\"h-screen w-64 bg-slate-900 text-white\">Products</aside>", ("tailwind-components", 2), ("tailwind-layout", 1));
        T("tailwind-modal", "Modal", "Construire une modal superposee.", ["fixed", "inset-0", "z-", "bg-"], "<div class=\"fixed inset-0 z-50 bg-black/40\"><section class=\"rounded bg-white p-6\">Modal</section></div>", ("tailwind-components", 2), ("tailwind-layout", 1));

        T("tailwind-dark-mode-basic", "Dark mode basique", "Ajouter une variante dark simple.", ["dark:", "bg-", "text-"], "<section class=\"bg-white text-slate-900 dark:bg-slate-900 dark:text-white\">Dashboard</section>", ("tailwind-dark-mode", 2), ("tailwind-colors", 1));
        T("tailwind-dark-card", "Card dark", "Construire une card compatible dark mode.", ["dark:bg-", "dark:text-", "rounded", "shadow"], "<article class=\"rounded bg-white p-4 text-slate-900 shadow dark:bg-slate-800 dark:text-white\">Book</article>", ("tailwind-dark-mode", 2), ("tailwind-components", 1));
        T("tailwind-design-tokens", "Tokens visuels", "Utiliser une convention coherente de couleurs et spacing.", ["bg-slate", "text-slate", "p-", "gap-"], "<section class=\"grid gap-4 bg-slate-50 p-6 text-slate-900\">Dashboard</section>", ("tailwind-dashboard", 1), ("tailwind-colors", 1), ("tailwind-spacing", 1));
        T("tailwind-component-variants", "Variantes de composants", "Creer deux variantes de bouton coherentes.", ["hover:", "bg-", "border", "rounded"], "<button class=\"rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700\">Primary</button><button class=\"rounded border border-slate-300 px-4 py-2 hover:bg-slate-50\">Secondary</button>", ("tailwind-components", 2), ("tailwind-states", 1));
        T("tailwind-dashboard-theme", "Theme dashboard", "Assembler couleurs, spacing et dark mode sur une section.", ["dark:", "bg-", "text-", "rounded", "shadow"], "<section class=\"rounded bg-white p-6 text-slate-900 shadow dark:bg-slate-900 dark:text-white\">Dashboard</section>", ("tailwind-dashboard", 2), ("tailwind-dark-mode", 1));

        T("tailwind-dashboard-layout", "Layout dashboard", "Assembler le shell du Product Dashboard.", ["min-h-screen", "grid", "bg-", "text-"], "<main class=\"grid min-h-screen bg-slate-50 text-slate-900 md:grid-cols-[16rem_1fr]\">Dashboard</main>", ("tailwind-dashboard", 2), ("tailwind-layout", 1), ("tailwind-grid", 1));
        T("tailwind-dashboard-sidebar", "Sidebar dashboard", "Ajouter une sidebar responsive au dashboard.", ["aside", "hidden", "md:block", "bg-"], "<aside class=\"hidden bg-slate-900 p-4 text-white md:block\">Products</aside>", ("tailwind-dashboard", 2), ("tailwind-responsive", 1), ("tailwind-components", 1));
        T("tailwind-dashboard-navbar", "Navbar dashboard", "Ajouter une navbar avec actions.", ["nav", "flex", "items-center", "justify-between", "button"], "<nav class=\"flex items-center justify-between rounded bg-white p-4 shadow\"><h1>Products</h1><button class=\"rounded bg-blue-600 px-3 py-2 text-white\">Add</button></nav>", ("tailwind-dashboard", 2), ("tailwind-flexbox", 1), ("tailwind-components", 1));
        T("tailwind-dashboard-stats", "Stats dashboard", "Creer une grille de stats.", ["grid", "gap-", "md:grid-cols-", "rounded", "shadow"], "<section class=\"grid gap-4 md:grid-cols-3\"><article class=\"rounded bg-white p-4 shadow\">12 products</article></section>", ("tailwind-dashboard", 2), ("tailwind-grid", 1));
        T("tailwind-dashboard-product-card", "Card produit dashboard", "Construire une card produit dashboard.", ["rounded", "shadow", "p-", "hover:"], "<article class=\"rounded-lg bg-white p-4 shadow hover:shadow-md\"><h2 class=\"font-semibold\">Book</h2></article>", ("tailwind-dashboard", 2), ("tailwind-components", 1), ("tailwind-states", 1));
        T("tailwind-dashboard-table", "Table dashboard", "Afficher les produits en table.", ["table", "w-full", "border", "text-"], "<table class=\"w-full text-sm\"><tr class=\"border-b\"><td>Book</td></tr></table>", ("tailwind-dashboard", 2), ("tailwind-components", 1));
        T("tailwind-dashboard-form", "Form dashboard", "Ajouter un formulaire produit au dashboard.", ["form", "input", "type=\"text\"", "type=\"number\"", "focus:"], "<form class=\"grid gap-3 rounded bg-white p-4 shadow\"><input type=\"text\" class=\"rounded border p-2 focus:ring-2\" /><input type=\"number\" class=\"rounded border p-2 focus:ring-2\" /></form>", ("tailwind-dashboard", 2), ("tailwind-forms", 1));
        T("tailwind-dashboard-responsive", "Dashboard responsive", "Adapter le dashboard mobile/tablet/desktop.", ["grid", "flex", "md:", "lg:", "gap-", "rounded", "shadow", "table", "overflow-x-auto"], "<main class=\"grid gap-6 p-4 md:grid-cols-[16rem_1fr] lg:grid-cols-[18rem_1fr]\"><aside class=\"rounded shadow\">Sidebar</aside><section class=\"grid gap-4\"><nav class=\"flex rounded shadow\">Navbar</nav><div class=\"overflow-x-auto\"><table class=\"w-full\"><tr><td>Book</td></tr></table></div></section></main>", ("tailwind-dashboard", 2), ("tailwind-responsive", 1), ("tailwind-components", 1));

        T("tailwindcss-boss-final-dashboard", "Boss Final TailwindCSS : Product Dashboard", "Construire une page Product Dashboard responsive.", ["flex", "grid", "p-", "gap-", "text-", "bg-", "rounded", "shadow", "hover:", "focus:", "md:", "lg:", "table", "input", "dark:"], "<main class=\"grid min-h-screen gap-6 bg-slate-50 p-6 text-slate-900 dark:bg-slate-950 dark:text-white md:grid-cols-[16rem_1fr] lg:grid-cols-[18rem_1fr]\"><aside class=\"rounded bg-slate-900 p-4 text-white shadow dark:bg-slate-800\">Sidebar</aside><section class=\"grid gap-4\"><nav class=\"flex items-center justify-between rounded bg-white p-4 shadow dark:bg-slate-900\"><button class=\"rounded px-3 py-2 hover:bg-slate-100 focus:ring-2 dark:hover:bg-slate-800\">Add</button></nav><div class=\"grid gap-4 md:grid-cols-3 lg:grid-cols-4\"><article class=\"rounded bg-white p-4 shadow hover:shadow-md dark:bg-slate-900\">Product</article></div><div class=\"overflow-x-auto\"><table class=\"w-full text-sm\"><tr><td>Book</td></tr></table></div><form class=\"grid gap-3\"><input type=\"text\" class=\"rounded border p-2 focus:ring-2\" /><input type=\"number\" class=\"rounded border p-2 focus:ring-2\" /></form></section></main>", ("tailwind-dashboard", 2), ("tailwind-responsive", 1), ("tailwind-components", 1));
    }

    private static void AddCssDefinitions(Action<string, string, string, string[], string, IReadOnlyList<(string Slug, int Weight)>> add)
    {
        void C(string slug, string title, string objective, string[] snippets, string code, params (string Slug, int Weight)[] skills) =>
            add(slug, title, objective, snippets, code, skills);

        C("css-selectors", "Selecteurs CSS", "Cibler header, card et bouton.", [".site-header", ".product-card", "button"], ".site-header { padding: 24px; }\n.product-card { padding: 16px; }\nbutton { cursor: pointer; }", ("css-selectors", 2));
        C("css-colors", "Couleurs CSS", "Appliquer couleurs de fond et de texte.", ["background", "color", "#"], ".product-card { background: #ffffff; color: #111827; }", ("css-colors", 2));
        C("css-typography", "Typographie CSS", "Regler font-size et font-weight.", ["font-size", "font-weight", "line-height"], ".product-card h2 { font-size: 1.25rem; font-weight: 700; line-height: 1.2; }", ("css-typography", 2));
        C("css-box-model", "Box model", "Utiliser padding, margin, border et width.", ["padding", "margin", "border", "width"], ".product-card { width: 100%; padding: 16px; margin: 12px; border: 1px solid #e5e7eb; }", ("css-box-model", 2));
        C("css-border-radius", "Border radius", "Arrondir une card produit.", ["border-radius", ".product-card"], ".product-card { border-radius: 12px; }", ("css-components", 1), ("css-box-model", 1));
        C("css-shadows", "Ombres CSS", "Ajouter une ombre de card.", ["box-shadow", ".product-card"], ".product-card { box-shadow: 0 10px 30px rgba(15, 23, 42, 0.12); }", ("css-components", 1));

        C("css-display", "Display", "Choisir display block, flex ou grid.", ["display", ".product-grid"], ".product-grid { display: grid; gap: 16px; }", ("css-layout", 2));
        C("css-flexbox", "Flexbox", "Aligner une navbar avec flex.", ["display: flex", "align-items", "justify-content"], ".site-header { display: flex; align-items: center; justify-content: space-between; }", ("css-flexbox", 2), ("css-layout", 1));
        C("css-grid", "Grid CSS", "Construire une grille produit.", ["display: grid", "grid-template-columns", "gap"], ".product-grid { display: grid; grid-template-columns: repeat(3, 1fr); gap: 16px; }", ("css-grid", 2), ("css-layout", 1));
        C("css-position", "Position", "Positionner un badge dans une card.", ["position: relative", "position: absolute", "top"], ".product-card { position: relative; }\n.badge { position: absolute; top: 12px; right: 12px; }", ("css-positioning", 2), ("css-components", 1));
        C("css-overflow", "Overflow", "Controler les debordements.", ["overflow", "hidden"], ".product-card { overflow: hidden; }", ("css-layout", 1), ("css-components", 1));

        C("css-media-queries", "Media queries", "Ajouter une media query responsive.", ["@media", "min-width", "768px"], "@media (min-width: 768px) { .product-grid { grid-template-columns: repeat(2, 1fr); } }", ("css-media-queries", 2), ("css-responsive", 1));
        C("css-mobile-first", "Mobile-first", "Partir d'une colonne puis elargir.", ["grid-template-columns: 1fr", "@media", "min-width"], ".product-grid { grid-template-columns: 1fr; }\n@media (min-width: 1024px) { .product-grid { grid-template-columns: repeat(3, 1fr); } }", ("css-responsive", 2), ("css-media-queries", 1));
        C("css-responsive-card", "Card responsive", "Adapter la card produit.", ["max-width", "width", "@media"], ".product-card { width: 100%; max-width: 420px; }\n@media (min-width: 768px) { .product-card { max-width: none; } }", ("css-responsive", 2), ("css-components", 1));
        C("css-responsive-grid", "Grille responsive", "Adapter une grille produit.", [".products", "display: grid", "grid-template-columns", "gap", "@media", "768px", "1024px"], ".products {\n  display: grid;\n  grid-template-columns: 1fr;\n  gap: 16px;\n}\n\n@media (min-width: 768px) {\n  .products { grid-template-columns: repeat(2, 1fr); }\n}\n\n@media (min-width: 1024px) {\n  .products { grid-template-columns: repeat(3, 1fr); }\n}", ("css-responsive", 2), ("css-grid", 1), ("css-media-queries", 1));
        C("css-responsive-navbar", "Navbar responsive", "Adapter une navbar.", [".site-header", "display: flex", "@media"], ".site-header { display: flex; flex-direction: column; }\n@media (min-width: 768px) { .site-header { flex-direction: row; } }", ("css-responsive", 2), ("css-components", 1));
        C("css-fluid-width", "Largeur fluide", "Centrer une page avec largeur fluide et max-width.", ["max-width", "width: 100%", "margin"], ".preview-page { width: 100%; max-width: 1120px; margin: 0 auto; }", ("css-responsive", 2), ("css-box-model", 1));
        C("css-clamp-font-size", "Typographie fluide avec clamp", "Adapter un titre avec clamp.", ["clamp(", "font-size", ".product-title"], ".product-title { font-size: clamp(1.75rem, 4vw, 3rem); }", ("css-responsive", 2), ("css-typography", 1));

        C("css-button", "Bouton CSS", "Styliser un bouton produit.", ["button", "padding", "border-radius", "background"], "button { padding: 10px 14px; border-radius: 8px; background: #2563eb; color: white; }", ("css-components", 2));
        C("css-card", "Card CSS", "Construire une card produit.", [".product-card", "padding", "border-radius", "box-shadow"], ".product-card { padding: 16px; border-radius: 12px; box-shadow: 0 10px 30px rgba(15, 23, 42, 0.12); }", ("css-components", 2), ("css-box-model", 1));
        C("css-navbar", "Navbar CSS", "Construire un header responsive.", [".site-header", "display: flex", "gap"], ".site-header { display: flex; align-items: center; justify-content: space-between; gap: 16px; }", ("css-components", 2), ("css-layout", 1));
        C("css-product-grid", "Grille produits", "Afficher des cards en grille.", [".product-grid", "display: grid", "gap", "minmax"], ".product-grid { display: grid; gap: 16px; grid-template-columns: repeat(auto-fit, minmax(220px, 1fr)); }", ("css-components", 1), ("css-grid", 1), ("css-responsive", 1));
        C("css-form", "Formulaire CSS", "Styliser input et bouton.", ["input", "button", "padding", "border-radius"], "input { padding: 10px; border-radius: 8px; border: 1px solid #d1d5db; }\nbutton { padding: 10px 14px; border-radius: 8px; }", ("css-forms", 2), ("css-components", 1));

        C("css-project-header", "Header projet", "Construire le header responsive de la page produit.", [".site-header", "display: flex", "padding", "@media"], ".site-header { display: flex; flex-direction: column; gap: 16px; padding: 24px; }\n@media (min-width: 768px) { .site-header { flex-direction: row; align-items: center; justify-content: space-between; } }", ("css-project", 2), ("css-responsive", 1), ("css-flexbox", 1));
        C("css-project-product-hero", "Hero produit", "Creer un hero produit en grille responsive.", [".product-hero", "display: grid", "gap", "@media"], ".product-hero { display: grid; gap: 24px; padding: 24px 0; }\n@media (min-width: 768px) { .product-hero { grid-template-columns: 1.3fr 1fr; } }", ("css-project", 2), ("css-grid", 1), ("css-responsive", 1));
        C("css-project-product-cards", "Cards projet", "Finaliser les cartes produit de la page.", [".product-card", "border-radius", "box-shadow", "padding"], ".product-card { padding: 16px; border-radius: 12px; box-shadow: 0 10px 30px rgba(15, 23, 42, 0.12); }", ("css-project", 2), ("css-components", 1), ("css-box-model", 1));
        C("css-project-responsive-grid", "Grille projet responsive", "Assembler une grille produit responsive avec minmax.", [".products", "display: grid", "minmax", "gap", "@media"], ".products { display: grid; gap: 16px; grid-template-columns: 1fr; }\n@media (min-width: 768px) { .products { grid-template-columns: repeat(auto-fit, minmax(220px, 1fr)); } }", ("css-project", 2), ("css-grid", 1), ("css-responsive", 1));
        C("css-project-form-section", "Section formulaire projet", "Styliser une section formulaire coherente.", [".product-form", "input", "button", "gap"], ".product-form { display: grid; gap: 12px; }\n.product-form input, .product-form button { width: 100%; padding: 12px; border-radius: 10px; }", ("css-project", 2), ("css-forms", 1), ("css-components", 1));

        C("css-boss-final-responsive-product-page", "Boss Final CSS : page produit responsive", "Creer une page produit responsive complete.", ["@media", "display: grid", "grid-template-columns", "gap", "padding", "border-radius", "box-shadow", "max-width", "width", "font-size", "clamp"], ".preview-page {\n  width: 100%;\n  max-width: 1120px;\n  margin: 0 auto;\n  padding: 24px;\n  font-size: 16px;\n}\n\n.site-header {\n  display: flex;\n  align-items: center;\n  justify-content: space-between;\n  gap: 16px;\n}\n\n.product-hero {\n  display: grid;\n  gap: 24px;\n  padding: 24px 0;\n}\n\n.product-title {\n  font-size: clamp(2rem, 5vw, 4rem);\n}\n\n.products {\n  display: grid;\n  grid-template-columns: 1fr;\n  gap: 16px;\n}\n\n.product-card {\n  padding: 16px;\n  border-radius: 12px;\n  box-shadow: 0 10px 30px rgba(15, 23, 42, 0.12);\n}\n\n.product-form {\n  display: grid;\n  gap: 12px;\n}\n\n.product-form input,\n.product-form button {\n  width: 100%;\n  padding: 12px;\n  border-radius: 10px;\n}\n\n@media (min-width: 768px) {\n  .product-hero { grid-template-columns: 1.3fr 1fr; }\n  .products { grid-template-columns: repeat(2, 1fr); }\n}\n\n@media (min-width: 1024px) {\n  .products { grid-template-columns: repeat(3, 1fr); }\n}", ("css-project", 2), ("css-responsive", 1), ("css-components", 1));
    }

    private static void AddJavaScriptDefinitions(Action<string, string, string, string[], string, IReadOnlyList<(string Slug, int Weight)>> add)
    {
        void J(string slug, string title, string objective, string[] snippets, string code, params (string Slug, int Weight)[] skills) =>
            add(slug, title, objective, snippets, code, skills);

        J("js-variables", "Variables JS", "Declarer un nom, un prix et un stock.", ["const productName", "let stock", "productName"], "const productName = 'Book';\nlet stock = 3;\nconst label = productName;", ("js-variables", 2), ("js-types", 1));
        J("js-types", "Types JS", "Identifier string, number et boolean.", ["typeof", "string", "number", "boolean"], "const name = 'Book';\nconst price = 12;\nconst available = true;\nconsole.log(typeof name === 'string', typeof price === 'number', typeof available === 'boolean');", ("js-types", 2));
        J("js-conditions", "Conditions JS", "Afficher selon le stock.", ["if", "else", "stock"], "if (stock > 0) { status.textContent = 'Available'; } else { status.textContent = 'Out of stock'; }", ("js-conditions", 2));
        J("js-loops", "Boucles JS", "Parcourir des produits.", ["for", "products", "length"], "for (let index = 0; index < products.length; index += 1) { console.log(products[index]); }", ("js-loops", 2), ("js-arrays", 1));
        J("js-functions", "Fonctions JS", "Creer formatProduct.", ["function formatProduct", "return", "product"], "function formatProduct(product) { return `${product.name} - ${product.price}`; }", ("js-functions", 2));
        J("js-arrays", "Arrays JS", "Declarer et parcourir un tableau.", ["const products", "[", ".map"], "const products = ['Book', 'Pen'];\nconst labels = products.map((product) => product.toUpperCase());", ("js-arrays", 2));
        J("js-objects", "Objects JS", "Modeliser un produit.", ["const product", "name:", "price:"], "const product = { name: 'Book', price: 12, stock: 3 };", ("js-objects", 2));

        J("js-template-literals", "Template literals", "Composer du texte avec backticks.", ["`", "${", "product"], "const label = `${product.name} - ${product.price}`;", ("js-modern-syntax", 2), ("js-types", 1));
        J("js-arrow-functions", "Arrow functions", "Ecrire une fonction flechee.", ["=>", "const", "product"], "const formatProduct = (product) => `${product.name}`;", ("js-functions", 1), ("js-modern-syntax", 2));
        J("js-destructuring", "Destructuring", "Extraire name et price.", ["const {", "name", "price"], "const { name, price } = product;", ("js-modern-syntax", 2), ("js-objects", 1));
        J("js-spread-rest", "Spread et rest", "Copier une liste de produits.", ["...", "products"], "const nextProducts = [...products, newProduct];", ("js-modern-syntax", 2), ("js-arrays", 1));
        J("js-map-filter-reduce", "map/filter/reduce", "Transformer, filtrer et reduire.", [".map", ".filter", ".reduce"], "const names = products.map((product) => product.name);\nconst available = products.filter((product) => product.stock > 0);\nconst total = products.reduce((sum, product) => sum + product.price, 0);", ("js-arrays", 2), ("js-modern-syntax", 1));
        J("js-modules-basic", "Modules JS basiques", "Exporter et importer une fonction.", ["export", "import", "from"], "export function formatProduct(product) { return product.name; }\nimport { formatProduct } from './products.js';", ("js-modern-syntax", 2));

        J("js-query-selector", "querySelector", "Selectionner le DOM.", ["document.querySelector", "#product-list"], "const list = document.querySelector('#product-list');", ("js-dom-selection", 2));
        J("js-text-content", "textContent", "Modifier un texte DOM.", ["textContent", "querySelector"], "document.querySelector('#status').textContent = 'Loaded';", ("js-dom-manipulation", 2), ("js-dom-selection", 1));
        J("js-class-list", "classList", "Ajouter une classe CSS.", ["classList", ".add"], "document.querySelector('#status').classList.add('is-visible');", ("js-dom-manipulation", 2));
        J("js-create-element", "createElement", "Creer un element DOM.", ["createElement", "append", "textContent"], "const item = document.createElement('li');\nitem.textContent = 'Book';\nlist.append(item);", ("js-dom-manipulation", 2));
        J("js-event-listener", "addEventListener", "Incrementer un compteur au clic.", ["document.querySelector", "#counter", "addEventListener", "click", "textContent", "count += 1"], "const counter = document.querySelector('#counter');\nlet count = 0;\ncounter.addEventListener('click', () => {\n  count += 1;\n  counter.textContent = String(count);\n});", ("js-events", 2), ("js-dom-selection", 1), ("js-dom-manipulation", 1));
        J("js-form-submit", "Form submit", "Traiter un formulaire.", ["addEventListener", "submit", "preventDefault", "#product-form"], "document.querySelector('#product-form').addEventListener('submit', (event) => {\n  event.preventDefault();\n});", ("js-events", 2), ("js-forms", 1));

        J("js-render-product-list", "Rendu liste produits", "Afficher un tableau de produits dans le DOM.", ["const products", "document.querySelector", "#products", ".forEach", "createElement", "append"], "const products = ['Book', 'Keyboard', 'Mouse'];\nconst list = document.querySelector('#products');\nproducts.forEach((product) => {\n  const item = document.createElement('li');\n  item.textContent = product;\n  list.append(item);\n});", ("js-project", 2), ("js-dom-manipulation", 1), ("js-arrays", 1));
        J("js-add-product", "Ajouter un produit", "Ajouter un produit via formulaire puis relancer le rendu.", ["addEventListener", "submit", "preventDefault", "push", "renderProducts"], "const products = [];\nconst form = document.querySelector('#product-form');\nconst input = document.querySelector('#product-name');\nfunction renderProducts() {}\nform.addEventListener('submit', (event) => {\n  event.preventDefault();\n  products.push(input.value);\n  renderProducts();\n});", ("js-project", 2), ("js-forms", 1), ("js-events", 1));
        J("js-delete-product", "Supprimer un produit", "Supprimer un produit avec un bouton et filter.", ["addEventListener", "click", "filter", "renderProducts"], "button.addEventListener('click', () => {\n  products = products.filter((product) => product.id !== id);\n  renderProducts();\n});", ("js-project", 2), ("js-events", 1), ("js-arrays", 1));
        J("js-filter-products", "Filtrer les produits", "Filtrer la liste selon la saisie utilisateur.", ["filter", "includes", "addEventListener", "input"], "filterInput.addEventListener('input', () => {\n  const visibleProducts = products.filter((product) => product.name.includes(filterInput.value));\n  renderProducts(visibleProducts);\n});", ("js-project", 2), ("js-arrays", 1), ("js-events", 1));
        J("js-empty-state", "Etat vide", "Afficher un message quand la liste est vide.", ["products.length === 0", "textContent", "empty"], "if (products.length === 0) {\n  empty.textContent = 'Aucun produit';\n}", ("js-project", 2), ("js-dom-manipulation", 1));
        J("js-local-storage-products", "Produits en localStorage", "Sauvegarder et relire la liste de produits.", ["localStorage", "JSON.stringify", "JSON.parse", "setItem", "getItem"], "localStorage.setItem('products', JSON.stringify(products));\nconst savedProducts = JSON.parse(localStorage.getItem('products') ?? '[]');", ("js-local-storage", 2), ("js-project", 1));

        J("js-promises", "Promises", "Utiliser then/catch.", ["Promise", ".then", ".catch"], "Promise.resolve(products).then(renderProducts).catch(console.error);", ("js-async", 2));
        J("js-async-await", "async/await", "Ecrire une fonction async.", ["async function", "await", "return"], "async function loadProducts() { const response = await fetch('/api/products'); return response.json(); }", ("js-async", 2));
        J("js-fetch", "fetch", "Charger des donnees avec fetch.", ["fetch", "await", "json"], "const response = await fetch('/api/products');\nconst products = await response.json();", ("js-fetch", 2), ("js-async", 1));
        J("js-loading-error", "Loading/error", "Gerer loading et erreurs.", ["try", "catch", "finally", "loading"], "let loading = true;\ntry { await loadProducts(); } catch (error) { console.error(error); } finally { loading = false; }", ("js-async", 2));
        J("js-api-render-products", "Rendu API produits", "Charger une API puis afficher les produits.", ["fetch", "await", "renderProducts", "catch"], "async function loadProducts() {\n  try {\n    const response = await fetch('/api/products');\n    const products = await response.json();\n    renderProducts(products);\n  } catch (error) {\n    console.error(error);\n  }\n}", ("js-fetch", 2), ("js-project", 1), ("js-dom-manipulation", 1));

        J("javascript-boss-final-product-list", "Boss Final JavaScript : Product List", "Creer une mini Product List interactive.", ["querySelector", "addEventListener", "createElement", "append", "filter", "map", "localStorage", "JSON.stringify", "JSON.parse", "preventDefault"], "const form = document.querySelector('#product-form');\nconst input = document.querySelector('#product-name');\nconst filterInput = document.querySelector('#product-filter');\nconst list = document.querySelector('#products');\nconst empty = document.querySelector('#empty-state');\n\nlet products = JSON.parse(localStorage.getItem('products') ?? '[]');\nlet filterText = '';\n\nfunction saveProducts() {\n  localStorage.setItem('products', JSON.stringify(products));\n}\n\nfunction renderProducts() {\n  list.textContent = '';\n  const visibleProducts = products.filter((product) => product.name.toLowerCase().includes(filterText.toLowerCase()));\n  empty.textContent = visibleProducts.length === 0 ? 'Aucun produit' : '';\n  visibleProducts.map((product) => {\n    const item = document.createElement('li');\n    item.textContent = product.name;\n    const button = document.createElement('button');\n    button.textContent = 'Supprimer';\n    button.addEventListener('click', () => {\n      products = products.filter((item) => item.id !== product.id);\n      saveProducts();\n      renderProducts();\n    });\n    item.append(button);\n    list.append(item);\n    return item;\n  });\n}\n\nform.addEventListener('submit', (event) => {\n  event.preventDefault();\n  products.push({ id: Date.now(), name: input.value });\n  input.value = '';\n  saveProducts();\n  renderProducts();\n});\n\nfilterInput.addEventListener('input', () => {\n  filterText = filterInput.value;\n  renderProducts();\n});\n\nrenderProducts();", ("js-project", 2), ("js-dom-manipulation", 1), ("js-local-storage", 1));
    }

    private sealed record PhpLessonSpec(
        string Title,
        string Objective,
        string ConceptSummary,
        string Explanation,
        string ExampleCode,
        string ExercisePrompt,
        string StarterCode,
        string SuccessFeedback,
        string FailureFeedback,
        int XpReward,
        string[] RequiredSnippets,
        string CommonMistakes,
        string FinalCorrection);

    private static readonly IReadOnlyDictionary<string, PhpLessonSpec> PhpSymfonyLessonSpecs = new Dictionary<string, PhpLessonSpec>(StringComparer.OrdinalIgnoreCase)
    {
        ["php-syntax"] = new(
            "Syntaxe PHP",
            "Ecrire un premier script PHP valide.",
            "Un script PHP commence par la balise d'ouverture, puis enchaine des instructions terminees par des points-virgules.",
            "PHP execute des instructions. `echo` affiche une chaine et le point-virgule termine l'instruction.",
            "<?php\n\n$message = \"Product Catalog\";\necho $message;",
            "Cree un script qui affiche exactement Product Catalog.",
            "<?php\n\n// Affiche Product Catalog",
            "Le script PHP minimal est valide et affiche le texte demande.",
            "Place la balise PHP au debut, utilise echo et garde le texte exact.",
            25,
            ["<?php", "echo", "Product Catalog"],
            "Ne mets pas de texte avant `<?php` et n'oublie pas le point-virgule.",
            "<?php\n\necho \"Product Catalog\";"),
        ["php-variables"] = new(
            "Variables PHP",
            "Stocker et reutiliser une valeur dans une variable.",
            "Une variable PHP commence par `$` et permet de nommer une donnee reutilisable.",
            "On assigne une valeur avec `=` puis on lit la variable dans une expression ou un echo.",
            "<?php\n\n$name = \"Book\";\necho $name;",
            "Declare `$productName` avec `Book`, puis affiche `Product: Book`.",
            "<?php\n\n$productName = \"\";\n// Affiche Product: Book",
            "La variable produit est declaree et reutilisee dans l'affichage.",
            "Declare `$productName`, stocke `Book`, puis concatene le libelle et la variable.",
            30,
            ["<?php", "$productName", "\"Book\"", "echo", "Product: ", ". $productName"],
            "Une variable sans `$` n'est pas une variable PHP. Evite de dupliquer `Book` dans l'echo.",
            "<?php\n\n$productName = \"Book\";\necho \"Product: \" . $productName;"),
        ["php-types"] = new(
            "Types PHP",
            "Manipuler texte, entier et booleen pour un produit.",
            "PHP type les valeurs dynamiquement, mais les donnees gardent une nature claire: string, int, bool.",
            "Un catalogue produit manipule souvent un nom, un prix, un stock et un etat disponible.",
            "<?php\n\n$name = \"Book\";\n$price = 12;\n$stock = 3;\n$isAvailable = true;",
            "Declare `$name`, `$price`, `$stock` et `$isAvailable`, puis affiche `Book - 12`.",
            "<?php\n\n$name = \"\";\n$price = 0;\n$stock = 0;\n$isAvailable = false;\n",
            "Les types utiles du produit sont presents et reutilises.",
            "Utilise une chaine, deux entiers, un booleen et compose l'affichage avec le nom et le prix.",
            35,
            ["<?php", "$name", "\"Book\"", "$price = 12", "$stock = 3", "$isAvailable = true", "echo", "$name . \" - \" . $price"],
            "Ne mets pas les nombres entre guillemets si tu veux les garder comme entiers.",
            "<?php\n\n$name = \"Book\";\n$price = 12;\n$stock = 3;\n$isAvailable = true;\n\necho $name . \" - \" . $price;"),
        ["php-conditions"] = new(
            "Conditions PHP",
            "Choisir un affichage selon le stock.",
            "`if` et `else` transforment une condition booleenne en decision.",
            "Une condition permet d'afficher un message different selon la disponibilite d'un produit.",
            "<?php\n\n$stock = 3;\nif ($stock > 0) { echo \"Available\"; }",
            "Avec `$stock = 0`, affiche `Out of stock` dans le `else`.",
            "<?php\n\n$stock = 0;\n\n// Ajoute la condition",
            "Le script distingue un produit disponible d'un produit en rupture.",
            "Teste `$stock > 0`, garde un `else`, puis affiche le texte attendu.",
            40,
            ["<?php", "$stock = 0", "if", "$stock > 0", "else", "Out of stock"],
            "N'utilise pas seulement un echo fixe: l'exercice attend une vraie condition.",
            "<?php\n\n$stock = 0;\n\nif ($stock > 0) {\n    echo \"Available\";\n} else {\n    echo \"Out of stock\";\n}"),
        ["php-arrays"] = new(
            "Tableaux indexes",
            "Regrouper plusieurs produits dans un tableau indexe.",
            "Un tableau indexe stocke des valeurs ordonnees accessibles par leur position.",
            "Les index commencent a 0. `count($products)` donne le nombre d'elements.",
            "<?php\n\n$products = [\"Book\", \"Pen\"];\necho $products[0];",
            "Cree `$products` avec `Book`, `Pen`, `Mug`, puis affiche le premier produit et le nombre total.",
            "<?php\n\n$products = [];\n\n// Affiche First: Book\n// Affiche Count: 3",
            "Le tableau contient les trois produits et le code lit le premier element.",
            "Utilise un tableau indexe, `$products[0]` et `count($products)`.",
            40,
            ["<?php", "$products", "[\"Book\", \"Pen\", \"Mug\"]", "$products[0]", "count($products)", "echo"],
            "Le premier element est a l'index 0, pas 1.",
            "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\necho \"First: \" . $products[0];\necho \"Count: \" . count($products);"),
        ["php-associative-arrays"] = new(
            "Tableaux associatifs",
            "Representer un produit avec des cles nommees.",
            "Un tableau associatif associe des cles comme `name`, `price` et `stock` a des valeurs.",
            "Les tableaux associatifs sont utiles avant de passer aux objets et aux entites.",
            "<?php\n\n$product = [\"name\" => \"Book\", \"price\" => 12];\necho $product[\"name\"];",
            "Cree un tableau `$product` avec les cles `name`, `price` et `stock`, puis affiche `Book - 12`.",
            "<?php\n\n$product = [\n    // name, price, stock\n];\n\n// Affiche Book - 12",
            "Le produit est modelise avec des cles explicites et les valeurs sont lues par cle.",
            "Ajoute les trois paires cle/valeur, puis lis `name` et `price` depuis `$product`.",
            45,
            ["<?php", "$product", "\"name\" => \"Book\"", "\"price\" => 12", "\"stock\" => 3", "$product[\"name\"]", "$product[\"price\"]", "echo"],
            "N'utilise pas `$product[0]`: ici les cles sont textuelles.",
            "<?php\n\n$product = [\n    \"name\" => \"Book\",\n    \"price\" => 12,\n    \"stock\" => 3,\n];\n\necho $product[\"name\"] . \" - \" . $product[\"price\"];"),
        ["php-for"] = new(
            "Boucle for",
            "Repeter une action avec un compteur.",
            "`for` convient quand on connait le nombre de repetitions.",
            "Une boucle for combine initialisation, condition et incrementation.",
            "<?php\n\nfor ($i = 1; $i <= 3; $i++) { echo $i; }",
            "Utilise une boucle `for` pour afficher `Product 1`, `Product 2` et `Product 3`.",
            "<?php\n\n// Boucle de 1 a 3",
            "La boucle produit trois affichages a partir d'un compteur.",
            "Declare `$i = 1`, continue jusqu'a 3 et affiche dans le bloc.",
            45,
            ["<?php", "for", "$i = 1", "$i <= 3", "$i++", "echo", "Product "],
            "Verifie que la condition s'arrete a 3 pour eviter une boucle incorrecte.",
            "<?php\n\nfor ($i = 1; $i <= 3; $i++) {\n    echo \"Product \" . $i;\n}"),
        ["php-foreach"] = new(
            "Boucle foreach",
            "Parcourir chaque produit d'un tableau.",
            "`foreach` lit chaque valeur d'un tableau sans gerer un index a la main.",
            "La forme courante est `foreach ($products as $product)`.",
            "<?php\n\nforeach ([\"Book\", \"Pen\"] as $product) { echo $product; }",
            "Parcours `$products` avec `foreach` et affiche `Product: <nom>` pour chaque produit.",
            "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\n// Parcours les produits",
            "Le code parcourt les trois produits avec une variable locale.",
            "Utilise `foreach ($products as $product)`, puis concatene `$product` dans l'echo.",
            45,
            ["<?php", "$products", "foreach", "as $product", "echo", "Product: ", ". $product"],
            "Ne modifie pas le tableau dans cette lecon: parcours seulement ses valeurs.",
            "<?php\n\n$products = [\"Book\", \"Pen\", \"Mug\"];\n\nforeach ($products as $product) {\n    echo \"Product: \" . $product;\n}"),
        ["php-functions"] = new(
            "Fonctions PHP",
            "Extraire une logique d'affichage reutilisable.",
            "Une fonction nomme une logique, accepte des parametres et peut retourner une valeur.",
            "Les types de parametres et de retour rendent l'intention plus claire.",
            "<?php\n\nfunction formatName(string $name): string { return strtoupper($name); }",
            "Cree `formatProduct(string $name, int $price): string`, retourne `Product: Book - 12`, puis appelle la fonction.",
            "<?php\n\nfunction formatProduct(string $name, int $price): string\n{\n    // Retourne le texte demande\n}\n\necho formatProduct(\"Book\", 12);",
            "La fonction est typee, retourne une chaine et elle est appelee.",
            "Garde les types dans la signature, retourne la chaine composee et appelle avec Book et 12.",
            50,
            ["<?php", "function formatProduct", "string $name", "int $price", ": string", "return", "formatProduct(\"Book\", 12)", "Product: "],
            "Ne fais pas seulement un echo dans la fonction: l'exercice attend un `return`.",
            "<?php\n\nfunction formatProduct(string $name, int $price): string\n{\n    return \"Product: \" . $name . \" - \" . $price;\n}\n\necho formatProduct(\"Book\", 12);"),

        ["php-oop-namespace"] = new("Namespace App\\Model", "Placer une classe PHP dans un namespace applicatif.", "Un namespace organise les classes et evite les collisions de noms.", "`namespace App\\Model;` place la classe Product dans le domaine modele du projet.", "<?php\n\nnamespace App\\Model;", "Ecris un fichier PHP qui declare `namespace App\\Model;`.", "<?php\n\n// Declare le namespace ici", "Le namespace du modele Product est present.", "Ajoute la declaration namespace juste apres la balise PHP.", 45, ["<?php", "namespace App\\Model"], "Le namespace doit etre avant la classe.", "<?php\n\nnamespace App\\Model;"),
        ["php-oop-class"] = new("Classe Product finale", "Declarer une classe Product propre.", "`final class` indique que la classe n'est pas destinee a etre etendue.", "Une classe Product centralise les donnees et comportements du produit.", "<?php\n\nfinal class Product {}", "Dans `App\\Model`, declare `final class Product`.", "<?php\n\nnamespace App\\Model;\n\n// Declare Product", "La classe Product finale est declaree.", "Ajoute `final class Product` avec un bloc de classe.", 50, ["<?php", "namespace App\\Model", "final class Product"], "N'oublie pas le mot-cle `class` et les accolades.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n}"),
        ["php-oop-properties"] = new("Proprietes typees", "Ajouter les donnees name, price et stock a Product.", "Les proprietes typees documentent et contraignent les donnees de l'objet.", "Un Product a un nom string, un prix int et un stock int.", "<?php\n\nprivate string $name;\nprivate int $price;", "Dans Product, declare `private string $name`, `private int $price` et `private int $stock`.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n    // Proprietes\n}", "Les trois proprietes privees et typees sont presentes.", "Ajoute trois proprietes privees dans la classe.", 55, ["private string $name", "private int $price", "private int $stock"], "Garde les proprietes privees: les getters viendront ensuite.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n    private string $name;\n    private int $price;\n    private int $stock;\n}"),
        ["php-oop-constructor"] = new("Constructeur Product", "Initialiser Product avec la promotion de proprietes.", "La promotion de proprietes PHP 8 evite de repeter declaration et assignation.", "Un constructeur peut recevoir les donnees obligatoires d'un produit.", "<?php\n\npublic function __construct(private string $name) {}", "Cree un constructeur qui recoit `name`, `price` et `stock` avec types.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n    // Constructeur\n}", "Le constructeur initialise les trois donnees du produit.", "Utilise `__construct` avec trois parametres promus prives.", 55, ["public function __construct", "private string $name", "private int $price", "private int $stock"], "Ne laisse pas des parametres non types.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n    public function __construct(\n        private string $name,\n        private int $price,\n        private int $stock,\n    ) {}\n}"),
        ["php-oop-getters"] = new("Getters Product", "Exposer les donnees sans rendre les proprietes publiques.", "Un getter retourne une propriete en gardant l'encapsulation.", "Symfony et Twig savent lire des getters comme `getName()`.", "<?php\n\npublic function getName(): string { return $this->name; }", "Ajoute `getName(): string` et `getPrice(): int` dans Product.", "<?php\n\nfinal class Product\n{\n    public function __construct(private string $name, private int $price) {}\n\n    // Getters\n}", "Les getters exposes sont publics et types.", "Chaque getter doit retourner `$this->...`.", 55, ["public function getName(): string", "return $this->name", "public function getPrice(): int", "return $this->price"], "Ne rends pas les proprietes publiques pour resoudre l'exercice.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n    public function __construct(private string $name, private int $price, private int $stock) {}\n\n    public function getName(): string\n    {\n        return $this->name;\n    }\n\n    public function getPrice(): int\n    {\n        return $this->price;\n    }\n}"),
        ["php-oop-business-method"] = new("Methode metier isAvailable", "Ajouter une regle metier simple au modele Product.", "Une methode metier donne un nom a une decision du domaine.", "`isAvailable()` peut retourner vrai quand le stock est superieur a zero.", "<?php\n\npublic function isAvailable(): bool { return $this->stock > 0; }", "Ajoute `isAvailable(): bool` qui retourne `true` quand `$stock > 0`.", "<?php\n\nfinal class Product\n{\n    public function __construct(private int $stock) {}\n\n    // Methode metier\n}", "La disponibilite est calculee dans une methode typee.", "Retourne directement la comparaison du stock.", 60, ["public function isAvailable(): bool", "return $this->stock > 0"], "Ne compare pas le nom ou le prix: la disponibilite depend du stock.", "<?php\n\nnamespace App\\Model;\n\nfinal class Product\n{\n    public function __construct(private string $name, private int $price, private int $stock) {}\n\n    public function isAvailable(): bool\n    {\n        return $this->stock > 0;\n    }\n}"),
        ["php-oop-exception"] = new("Exception metier", "Refuser un prix negatif dans Product.", "`InvalidArgumentException` signale qu'une valeur fournie est invalide.", "Une classe peut proteger ses invariants des le constructeur.", "<?php\n\nif ($price < 0) { throw new InvalidArgumentException(); }", "Dans le constructeur, lance `InvalidArgumentException` si `$price < 0`.", "<?php\n\nnamespace App\\Model;\n\nuse InvalidArgumentException;\n\nfinal class Product\n{\n    public function __construct(private int $price)\n    {\n        // Valide le prix\n    }\n}", "Le constructeur protege Product contre les prix negatifs.", "Ajoute un `if`, teste `$price < 0`, puis utilise `throw new InvalidArgumentException`.", 65, ["use InvalidArgumentException", "if ($price < 0)", "throw new InvalidArgumentException"], "Ne corrige pas silencieusement le prix: l'exercice attend une exception.", "<?php\n\nnamespace App\\Model;\n\nuse InvalidArgumentException;\n\nfinal class Product\n{\n    public function __construct(private string $name, private int $price, private int $stock)\n    {\n        if ($price < 0) {\n            throw new InvalidArgumentException('Price cannot be negative.');\n        }\n    }\n}"),

        ["symfony-project-structure"] = new("Structure d'un projet Symfony", "Identifier les dossiers du Product Catalog.", "`src/Controller`, `src/Entity` et `templates` separent code HTTP, modele persistant et affichage.", "Symfony organise les responsabilites par dossier.", "src/Controller\nsrc/Entity\ntemplates/product", "Liste les dossiers `src/Controller`, `src/Entity` et `templates/product`.", "src/\n# Complete la structure", "La structure minimale du Product Catalog est claire.", "Ajoute les trois chemins attendus.", 45, ["src/Controller", "src/Entity", "templates/product"], "Ne melange pas controller, entite et template dans un seul dossier.", "src/Controller\nsrc/Entity\ntemplates/product"),
        ["symfony-controller"] = new("ProductController", "Creer un controleur Symfony moderne.", "Un controller Symfony etend `AbstractController` et declare ses imports HTTP.", "Le controller regroupe les actions de la ressource Product.", "<?php\n\nnamespace App\\Controller;\n\nuse Symfony\\Bundle\\FrameworkBundle\\Controller\\AbstractController;", "Cree `ProductController` avec namespace, imports `AbstractController`, `Response`, `Route`, puis la classe finale.", "<?php\n\nnamespace App\\Controller;\n\n// Imports\n\nfinal class ProductController\n{\n}", "Le squelette de controller Symfony est complet.", "Ajoute les imports Symfony et fais heriter la classe de `AbstractController`.", 55, ["namespace App\\Controller", "use Symfony\\Bundle\\FrameworkBundle\\Controller\\AbstractController", "use Symfony\\Component\\HttpFoundation\\Response", "use Symfony\\Component\\Routing\\Attribute\\Route", "final class ProductController extends AbstractController"], "Un controller sans namespace ni imports sera difficile a relier au projet.", "<?php\n\nnamespace App\\Controller;\n\nuse Symfony\\Bundle\\FrameworkBundle\\Controller\\AbstractController;\nuse Symfony\\Component\\HttpFoundation\\Response;\nuse Symfony\\Component\\Routing\\Attribute\\Route;\n\nfinal class ProductController extends AbstractController\n{\n}"),
        ["symfony-route-index"] = new("Route index produits", "Declarer la route `/products`.", "Les attributs `#[Route(...)]` lient une URL a une methode de controller.", "Une action index liste les produits.", "<?php\n\n#[Route('/products', name: 'product_index')]\npublic function index(): Response { return new Response('Products'); }", "Ajoute une methode `index(): Response` avec `#[Route('/products', name: 'product_index')]` et un `return`.", "<?php\n\nfinal class ProductController extends AbstractController\n{\n    // Route index\n}", "La route index a une URL stable, un nom stable et retourne une Response.", "Cree une methode publique, ajoute l'attribut Route, puis retourne une Response.", 55, ["#[Route('/products', name: 'product_index')]", "public function index(): Response", "return"], "Le nom de route doit rester stable pour Twig et les redirections.", "<?php\n\nnamespace App\\Controller;\n\nuse Symfony\\Bundle\\FrameworkBundle\\Controller\\AbstractController;\nuse Symfony\\Component\\HttpFoundation\\Response;\nuse Symfony\\Component\\Routing\\Attribute\\Route;\n\nfinal class ProductController extends AbstractController\n{\n    #[Route('/products', name: 'product_index')]\n    public function index(): Response\n    {\n        return new Response('Products');\n    }\n}"),
        ["symfony-response"] = new("Response HTML", "Retourner une reponse HTTP simple.", "`Response` represente la reponse HTTP envoyee au navigateur.", "`new Response('Products')` suffit pour une premiere action.", "<?php\n\nreturn new Response('Products');", "Importe `Response` et retourne `new Response('Products')`.", "<?php\n\n// Importe Response\n\n// Retourne une Response", "La methode retourne explicitement une Response Symfony.", "Ajoute l'import `Response`, puis retourne une instance.", 50, ["use Symfony\\Component\\HttpFoundation\\Response", "return new Response", "Products"], "Ne retourne pas une chaine brute si la consigne demande une Response.", "<?php\n\nuse Symfony\\Component\\HttpFoundation\\Response;\n\nreturn new Response('Products');"),
        ["symfony-json-response"] = new("JsonResponse", "Retourner des donnees produit en JSON.", "`JsonResponse` sert aux endpoints API simples.", "Une reponse JSON peut contenir un tableau associatif.", "<?php\n\nreturn new JsonResponse(['name' => 'Book']);", "Retourne une `JsonResponse` contenant `name => Book` et `price => 12`.", "<?php\n\n// Importe JsonResponse\n\n// Retourne les donnees du produit", "La reponse JSON contient les donnees du produit.", "Importe JsonResponse et retourne un tableau avec name et price.", 50, ["use Symfony\\Component\\HttpFoundation\\JsonResponse", "return new JsonResponse", "'name' => 'Book'", "'price' => 12"], "Ne construis pas le JSON a la main avec une chaine.", "<?php\n\nuse Symfony\\Component\\HttpFoundation\\JsonResponse;\n\nreturn new JsonResponse(['name' => 'Book', 'price' => 12]);"),
        ["symfony-route-parameter"] = new("Parametre de route", "Lire un identifiant depuis l'URL.", "Une route avec `{id}` transmet une valeur a la methode.", "Symfony convertit `/products/12` vers le parametre `$id`.", "<?php\n\n#[Route('/products/{id}', name: 'product_show')]\npublic function show(int $id): Response {}", "Declare `#[Route('/products/{id}', name: 'product_show')]` et `show(int $id): Response`.", "<?php\n\nfinal class ProductController extends AbstractController\n{\n    // Route show\n}", "La route detail expose un parametre id type.", "Ajoute `{id}` dans l'URL et `int $id` dans la signature.", 55, ["#[Route('/products/{id}', name: 'product_show')]", "public function show(int $id): Response", "return"], "Garde le meme nom de parametre dans l'URL et la methode.", "<?php\n\nnamespace App\\Controller;\n\nuse Symfony\\Component\\HttpFoundation\\Response;\nuse Symfony\\Component\\Routing\\Attribute\\Route;\n\nfinal class ProductController extends AbstractController\n{\n    #[Route('/products/{id}', name: 'product_show')]\n    public function show(int $id): Response\n    {\n        return new Response('Product ' . $id);\n    }\n}"),

        ["symfony-render-template"] = new("Render Twig", "Rendre un template depuis un controller.", "`$this->render()` delegue l'affichage a Twig et retourne une Response.", "Le controller prepare les donnees, Twig les affiche.", "<?php\n\nreturn $this->render('product/index.html.twig');", "Retourne le template `product/index.html.twig` avec `$this->render(...)`.", "<?php\n\n// Dans une action controller\nreturn null;", "Le controller rend le template produit.", "Utilise `return $this->render('product/index.html.twig')`.", 55, ["return $this->render", "product/index.html.twig"], "Ne mets pas tout le HTML dans `new Response` pour cette lecon.", "<?php\n\nreturn $this->render('product/index.html.twig');"),
        ["symfony-twig-variable"] = new("Variable Twig", "Afficher le nom d'un produit dans Twig.", "Twig affiche les variables avec `{{ ... }}`.", "Un objet `product` expose `name` dans le template.", "{{ product.name }}", "Ecris un extrait Twig qui affiche `{{ product.name }}`.", "{# Extrait Twig attendu #}\n", "Le template affiche la variable produit.", "Utilise les doubles accolades Twig autour de `product.name`.", 45, ["{{ product.name }}"], "N'ecris pas du PHP dans cette lecon: l'extrait attendu est du Twig.", "<h1>{{ product.name }}</h1>"),
        ["symfony-twig-loop"] = new("Boucle Twig", "Parcourir une liste de produits dans Twig.", "`{% for ... %}` parcourt une collection dans un template.", "Twig garde l'affichage lisible cote template.", "{% for product in products %}\n    {{ product.name }}\n{% endfor %}", "Ecris une boucle Twig qui parcourt `products` et affiche `product.name`.", "{# Extrait Twig attendu #}\n", "Le template parcourt les produits.", "Utilise `{% for product in products %}` puis `{{ product.name }}`.", 50, ["{% for product in products %}", "{{ product.name }}", "{% endfor %}"], "N'oublie pas le `{% endfor %}`.", "{% for product in products %}\n    <p>{{ product.name }}</p>\n{% endfor %}"),
        ["symfony-twig-condition"] = new("Condition Twig", "Afficher seulement les produits en stock.", "`{% if ... %}` filtre l'affichage selon une condition.", "Twig peut afficher un bloc uniquement si le stock est positif.", "{% if product.stock > 0 %}\n    {{ product.name }}\n{% endif %}", "Ecris une condition Twig qui affiche le produit seulement si `product.stock > 0`.", "{# Extrait Twig attendu #}\n", "Le template contient une condition de stock.", "Utilise `{% if product.stock > 0 %}` et ferme avec `{% endif %}`.", 50, ["{% if product.stock > 0 %}", "{{ product.name }}", "{% endif %}"], "La condition Twig n'utilise pas de parentheses PHP.", "{% if product.stock > 0 %}\n    <p>{{ product.name }}</p>\n{% endif %}"),
        ["symfony-twig-path"] = new("Lien Twig path", "Construire un lien vers la page detail.", "`path()` genere une URL a partir du nom de route.", "Un lien detail utilise `product_show` et l'id du produit.", "{{ path('product_show', { id: product.id }) }}", "Cree un lien Twig vers `product_show` avec `{ id: product.id }`.", "{# Extrait Twig attendu #}\n", "Le lien detail utilise le nom de route et l'id.", "Utilise `path('product_show', { id: product.id })` dans un href.", 50, ["path('product_show'", "{ id: product.id }"], "Evite d'ecrire `/products/` a la main dans cette lecon.", "<a href=\"{{ path('product_show', { id: product.id }) }}\">Voir</a>"),

        ["symfony-form-type"] = new("ProductType", "Creer une classe de formulaire Symfony.", "Un `AbstractType` decrit les champs d'un formulaire.", "`ProductType` transforme l'entite Product en formulaire.", "<?php\n\nfinal class ProductType extends AbstractType {}", "Cree `ProductType` dans `App\\Form`, etends `AbstractType`, utilise `FormBuilderInterface`, `configureOptions` et `Product::class`.", "<?php\n\nnamespace App\\Form;\n\n// Complete ProductType", "La classe ProductType contient la structure Symfony attendue.", "Ajoute namespace, imports, `buildForm` et `configureOptions`.", 60, ["namespace App\\Form", "extends AbstractType", "FormBuilderInterface", "configureOptions", "Product::class"], "N'oublie pas `data_class`, sinon Symfony ne saura pas quel objet hydrater.", "<?php\n\nnamespace App\\Form;\n\nuse App\\Entity\\Product;\nuse Symfony\\Component\\Form\\AbstractType;\nuse Symfony\\Component\\Form\\FormBuilderInterface;\nuse Symfony\\Component\\OptionsResolver\\OptionsResolver;\n\nfinal class ProductType extends AbstractType\n{\n    public function buildForm(FormBuilderInterface $builder, array $options): void {}\n\n    public function configureOptions(OptionsResolver $resolver): void\n    {\n        $resolver->setDefaults(['data_class' => Product::class]);\n    }\n}"),
        ["symfony-form-fields"] = new("Champs ProductType", "Ajouter les champs du formulaire produit.", "`$builder->add(...)` declare les champs a afficher et traiter.", "Product Catalog manipule `name`, `price` et `stock`.", "<?php\n\n$builder->add('name')->add('price')->add('stock');", "Dans `buildForm`, ajoute les champs `name`, `price` et `stock`.", "<?php\n\npublic function buildForm(FormBuilderInterface $builder, array $options): void\n{\n    // Champs\n}", "Les trois champs produit sont declares.", "Chaine ou repete `$builder->add()` pour chaque champ.", 55, ["->add('name')", "->add('price')", "->add('stock')"], "Les noms de champs doivent correspondre aux proprietes de Product.", "<?php\n\npublic function buildForm(FormBuilderInterface $builder, array $options): void\n{\n    $builder\n        ->add('name')\n        ->add('price')\n        ->add('stock');\n}"),
        ["symfony-create-form"] = new("createForm", "Creer un formulaire depuis un controller.", "`createForm` instancie un type de formulaire pour un objet.", "Le controller cree le formulaire avant de l'afficher ou le traiter.", "<?php\n\n$form = $this->createForm(ProductType::class, $product);", "Cree `$form` avec `$this->createForm(ProductType::class, $product)`.", "<?php\n\n$product = new Product();\n\n// Cree le formulaire", "Le controller cree un formulaire ProductType lie au produit.", "Instancie Product puis passe-le a `createForm`.", 55, ["new Product", "$this->createForm(ProductType::class, $product)", "$form"], "N'appelle pas ProductType directement avec `new`.", "<?php\n\n$product = new Product();\n$form = $this->createForm(ProductType::class, $product);"),
        ["symfony-handle-request"] = new("handleRequest", "Traiter la soumission d'un formulaire.", "`handleRequest` lit la requete et hydrate le formulaire.", "On teste ensuite `isSubmitted()` et `isValid()` avant de persister.", "<?php\n\n$form->handleRequest($request);\nif ($form->isSubmitted() && $form->isValid()) {}", "Traite `$request`, puis teste `$form->isSubmitted() && $form->isValid()`.", "<?php\n\n// $form existe deja\n\n// Traite la requete", "Le flux de soumission est present.", "Appelle `handleRequest($request)` avant les tests.", 60, ["Request $request", "$form->handleRequest($request)", "$form->isSubmitted()", "$form->isValid()"], "Tester `isValid()` sans `handleRequest` ne suffit pas.", "<?php\n\nuse Symfony\\Component\\HttpFoundation\\Request;\n\n$form->handleRequest($request);\nif ($form->isSubmitted() && $form->isValid()) {\n}"),
        ["symfony-validation-constraints"] = new("Contraintes Product", "Valider les donnees produit avec Assert.", "Les contraintes Symfony declarent les regles de validation pres des donnees.", "`Assert\\NotBlank` protege le nom, `PositiveOrZero` protege prix et stock.", "<?php\n\n#[Assert\\NotBlank]\nprivate string $name;", "Ajoute `Assert\\NotBlank` sur `$name` et `Assert\\PositiveOrZero` sur `$price` et `$stock`.", "<?php\n\nuse Symfony\\Component\\Validator\\Constraints as Assert;\n\n// Contraintes Product", "Les contraintes principales du produit sont declarees.", "Importe Assert puis place les attributs au-dessus des proprietes.", 60, ["use Symfony\\Component\\Validator\\Constraints as Assert", "#[Assert\\NotBlank]", "#[Assert\\PositiveOrZero]", "private string $name", "private int $price", "private int $stock"], "N'utilise pas seulement du texte d'erreur: l'exercice attend des attributs Assert.", "<?php\n\nuse Symfony\\Component\\Validator\\Constraints as Assert;\n\n#[Assert\\NotBlank]\nprivate string $name;\n\n#[Assert\\PositiveOrZero]\nprivate int $price;\n\n#[Assert\\PositiveOrZero]\nprivate int $stock;"),
        ["symfony-form-errors"] = new("Erreurs de formulaire", "Afficher une erreur de champ dans Twig.", "`form_errors` affiche les messages de validation d'un champ.", "Le template peut cibler `form.name` pour les erreurs du nom.", "{{ form_errors(form.name) }}", "Ecris un extrait Twig qui affiche `form_errors(form.name)`.", "{# Extrait Twig attendu #}\n", "Le template montre les erreurs du champ name.", "Utilise `{{ form_errors(form.name) }}`.", 45, ["{{ form_errors(form.name) }}"], "Ne confonds pas `form_errors(form)` et `form_errors(form.name)`.", "{{ form_errors(form.name) }}"),

        ["symfony-doctrine-entity"] = new("Entite Product", "Transformer Product en entite Doctrine.", "Une entite Doctrine est une classe persistante mappee avec attributs ORM.", "`#[ORM\\Entity]` connecte la classe au mapping Doctrine.", "<?php\n\n#[ORM\\Entity(repositoryClass: ProductRepository::class)]\nclass Product {}", "Declare `Product` dans `App\\Entity` avec `use Doctrine\\ORM\\Mapping as ORM` et `#[ORM\\Entity(repositoryClass: ProductRepository::class)]`.", "<?php\n\nnamespace App\\Entity;\n\n// Mapping ORM\nclass Product\n{\n}", "Product est reconnue comme entite Doctrine.", "Ajoute le namespace, l'alias ORM et l'attribut Entity.", 60, ["namespace App\\Entity", "use Doctrine\\ORM\\Mapping as ORM", "#[ORM\\Entity(repositoryClass: ProductRepository::class)]", "class Product"], "L'attribut Entity doit etre au-dessus de la classe.", "<?php\n\nnamespace App\\Entity;\n\nuse App\\Repository\\ProductRepository;\nuse Doctrine\\ORM\\Mapping as ORM;\n\n#[ORM\\Entity(repositoryClass: ProductRepository::class)]\nclass Product\n{\n}"),
        ["symfony-doctrine-id"] = new("Identifiant Doctrine", "Ajouter une cle primaire auto-generee.", "`Id`, `GeneratedValue` et une colonne nullable representent l'identifiant avant persistence.", "Doctrine remplit l'id apres insertion.", "<?php\n\n#[ORM\\Id]\n#[ORM\\GeneratedValue]\nprivate ?int $id = null;", "Ajoute `$id` avec `#[ORM\\Id]`, `#[ORM\\GeneratedValue]` et `private ?int $id = null`.", "<?php\n\nclass Product\n{\n    // Id\n}", "L'identifiant Doctrine est correctement declare.", "Empile les attributs ORM au-dessus de la propriete id.", 55, ["#[ORM\\Id]", "#[ORM\\GeneratedValue]", "private ?int $id = null"], "L'id doit etre nullable avant que Doctrine ne le genere.", "<?php\n\n#[ORM\\Id]\n#[ORM\\GeneratedValue]\n#[ORM\\Column]\nprivate ?int $id = null;"),
        ["symfony-doctrine-columns"] = new("Colonnes Product", "Mapper name, price et stock en colonnes Doctrine.", "`#[ORM\\Column]` mappe une propriete PHP vers une colonne SQL.", "Les colonnes portent les donnees persistantes du produit.", "<?php\n\n#[ORM\\Column]\nprivate string $name;", "Ajoute des colonnes ORM pour `$name`, `$price` et `$stock`.", "<?php\n\nclass Product\n{\n    // Colonnes\n}", "Les champs produit sont persistables par Doctrine.", "Ajoute `#[ORM\\Column]` au-dessus de chaque propriete.", 60, ["#[ORM\\Column]", "private string $name", "private int $price", "private int $stock"], "Ne confonds pas contrainte de validation et mapping ORM: les deux peuvent coexister.", "<?php\n\n#[ORM\\Column(length: 255)]\nprivate string $name = '';\n\n#[ORM\\Column]\nprivate int $price = 0;\n\n#[ORM\\Column]\nprivate int $stock = 0;"),
        ["symfony-doctrine-repository"] = new("ProductRepository", "Creer le repository Doctrine de Product.", "Un repository regroupe les requetes liees a une entite.", "`ServiceEntityRepository` fournit les methodes Doctrine de base.", "<?php\n\nfinal class ProductRepository extends ServiceEntityRepository {}", "Declare `ProductRepository` dans `App\\Repository` en etendant `ServiceEntityRepository`.", "<?php\n\nnamespace App\\Repository;\n\n// Repository Product", "Le repository Product est declare.", "Ajoute le namespace repository, la classe et l'heritage.", 60, ["namespace App\\Repository", "final class ProductRepository extends ServiceEntityRepository", "Product::class"], "Un repository doit etre specialise pour Product.", "<?php\n\nnamespace App\\Repository;\n\nuse App\\Entity\\Product;\nuse Doctrine\\Bundle\\DoctrineBundle\\Repository\\ServiceEntityRepository;\nuse Doctrine\\Persistence\\ManagerRegistry;\n\nfinal class ProductRepository extends ServiceEntityRepository\n{\n    public function __construct(ManagerRegistry $registry)\n    {\n        parent::__construct($registry, Product::class);\n    }\n}"),
        ["symfony-doctrine-query"] = new("Requete findAvailable", "Ajouter une requete repository metier.", "Une methode repository donne un nom au filtrage Doctrine.", "`findAvailable()` retourne les produits avec stock positif.", "<?php\n\npublic function findAvailable(): array { return $this->createQueryBuilder('p')->getQuery()->getResult(); }", "Cree `findAvailable(): array` avec `createQueryBuilder('p')` et un filtre `p.stock > 0`.", "<?php\n\nfinal class ProductRepository extends ServiceEntityRepository\n{\n    // Requete disponible\n}", "Le repository expose une requete produits disponibles.", "Utilise QueryBuilder, `andWhere` et `getResult()`.", 65, ["public function findAvailable(): array", "createQueryBuilder('p')", "andWhere('p.stock > 0')", "getQuery()", "getResult()"], "Ne mets pas cette requete dans le controller.", "<?php\n\npublic function findAvailable(): array\n{\n    return $this->createQueryBuilder('p')\n        ->andWhere('p.stock > 0')\n        ->getQuery()\n        ->getResult();\n}"),
        ["symfony-doctrine-save"] = new("Sauvegarder avec Doctrine", "Persister un nouveau produit.", "`EntityManagerInterface` coordonne persist et flush.", "`persist` prepare l'entite, `flush` envoie les changements en base.", "<?php\n\n$entityManager->persist($product);\n$entityManager->flush();", "Injecte `EntityManagerInterface`, puis utilise `persist($product)` et `flush()`.", "<?php\n\n// Sauvegarde Product", "Le flux de sauvegarde Doctrine est complet.", "Ajoute le type EntityManagerInterface puis appelle persist et flush.", 65, ["EntityManagerInterface", "persist($product)", "flush()"], "Sans `flush()`, rien n'est ecrit en base.", "<?php\n\nuse Doctrine\\ORM\\EntityManagerInterface;\n\n$entityManager->persist($product);\n$entityManager->flush();"),
        ["symfony-doctrine-delete"] = new("Supprimer avec Doctrine", "Supprimer un produit persistant.", "`remove` marque l'entite pour suppression, `flush` execute l'operation.", "Une suppression Doctrine suit le meme rythme que la sauvegarde.", "<?php\n\n$entityManager->remove($product);\n$entityManager->flush();", "Utilise `remove($product)`, `flush()` puis redirige vers `product_index`.", "<?php\n\n// Suppression Product", "La suppression Doctrine est complete.", "Supprime avec EntityManager puis redirige apres flush.", 65, ["remove($product)", "flush()", "redirectToRoute('product_index')"], "N'oublie pas la redirection apres suppression.", "<?php\n\n$entityManager->remove($product);\n$entityManager->flush();\n\nreturn $this->redirectToRoute('product_index');"),

        ["symfony-service-class"] = new("ProductService", "Creer un service applicatif produit.", "Un service porte la logique metier hors du controller.", "`final readonly` convient aux services injectes immutables.", "<?php\n\nnamespace App\\Service;\n\nfinal readonly class ProductService {}", "Cree `ProductService` dans `App\\Service` en `final readonly class`.", "<?php\n\nnamespace App\\Service;\n\n// Service Product", "Le service ProductService est declare.", "Ajoute le namespace service et la classe finale readonly.", 55, ["namespace App\\Service", "final readonly class ProductService"], "Ne mets pas la logique service dans le controller.", "<?php\n\nnamespace App\\Service;\n\nfinal readonly class ProductService\n{\n}"),
        ["symfony-service-repository-injection"] = new("Injection du repository", "Injecter ProductRepository dans ProductService.", "Symfony injecte les dependances par constructeur.", "Le service depend du repository pour lire les produits.", "<?php\n\npublic function __construct(private ProductRepository $products) {}", "Dans `ProductService`, injecte `ProductRepository` par constructeur.", "<?php\n\nfinal readonly class ProductService\n{\n    // Constructeur\n}", "Le service recoit son repository par injection.", "Ajoute l'import puis un constructeur avec propriete privee.", 60, ["use App\\Repository\\ProductRepository", "public function __construct", "private ProductRepository $products"], "Evite de creer le repository avec `new`.", "<?php\n\nnamespace App\\Service;\n\nuse App\\Repository\\ProductRepository;\n\nfinal readonly class ProductService\n{\n    public function __construct(private ProductRepository $products) {}\n}"),
        ["symfony-service-method"] = new("Methode getAvailableProducts", "Exposer une operation metier depuis le service.", "Le service peut deleguer la requete au repository et nommer le cas d'usage.", "`getAvailableProducts()` masque le detail de la requete au controller.", "<?php\n\npublic function getAvailableProducts(): array { return $this->products->findAvailable(); }", "Ajoute `getAvailableProducts(): array` qui retourne `$this->products->findAvailable()`.", "<?php\n\nfinal readonly class ProductService\n{\n    public function __construct(private ProductRepository $products) {}\n\n    // Methode service\n}", "Le service expose la liste des produits disponibles.", "Retourne l'appel au repository depuis la methode service.", 60, ["public function getAvailableProducts(): array", "return $this->products->findAvailable()"], "Ne duplique pas la requete dans le service si le repository la porte deja.", "<?php\n\npublic function getAvailableProducts(): array\n{\n    return $this->products->findAvailable();\n}"),
        ["symfony-controller-service-injection"] = new("Injection du service controller", "Injecter ProductService dans ProductController.", "Le controller recoit les cas d'usage via son constructeur.", "Cela evite un controller trop couple a Doctrine.", "<?php\n\npublic function __construct(private ProductService $products) {}", "Dans `ProductController`, injecte `ProductService` via constructeur.", "<?php\n\nfinal class ProductController extends AbstractController\n{\n    // Constructeur\n}", "Le controller depend du service produit.", "Ajoute l'import ProductService et le constructeur.", 60, ["use App\\Service\\ProductService", "public function __construct", "private ProductService $products"], "Ne cree pas le service manuellement.", "<?php\n\nuse App\\Service\\ProductService;\n\npublic function __construct(private ProductService $products) {}"),
        ["symfony-controller-delegation"] = new("Delegation controller-service", "Deleguer la liste des produits au service.", "Le controller orchestre HTTP et delegue la logique metier.", "L'action index appelle le service puis rend Twig.", "<?php\n\n$products = $this->products->getAvailableProducts();", "Dans `index`, appelle `$this->products->getAvailableProducts()` puis passe `products` au render.", "<?php\n\npublic function index(): Response\n{\n    // Delegue au service\n}", "Le controller delegue au service et rend le template.", "Cree `$products`, puis passe-le dans le tableau de donnees Twig.", 65, ["$this->products->getAvailableProducts()", "return $this->render", "'products' => $products"], "Un controller propre ne contient pas la requete Doctrine ici.", "<?php\n\npublic function index(): Response\n{\n    $products = $this->products->getAvailableProducts();\n\n    return $this->render('product/index.html.twig', [\n        'products' => $products,\n    ]);\n}"),

        ["symfony-project-product-list"] = new("Fonction liste produits", "Assembler route, service et Twig pour la liste.", "Une fonctionnalite verticale relie route, controller, service et template.", "La liste `/products` utilise le service et rend `product/index.html.twig`.", "<?php\n\n#[Route('/products', name: 'product_index')]\npublic function index(): Response { return $this->render('product/index.html.twig'); }", "Cree l'action liste: route `/products`, appel service, render Twig avec `products`.", "<?php\n\n// Action index complete", "La fonctionnalite liste est coherente de bout en bout.", "Route l'action, appelle ProductService, puis rends le template avec les produits.", 75, ["#[Route('/products', name: 'product_index')]", "public function index(): Response", "getAvailableProducts()", "return $this->render('product/index.html.twig'", "'products' => $products"], "Ne retourne pas seulement une Response brute: cette lecon attend Twig et service.", "<?php\n\n#[Route('/products', name: 'product_index')]\npublic function index(): Response\n{\n    $products = $this->products->getAvailableProducts();\n\n    return $this->render('product/index.html.twig', ['products' => $products]);\n}"),
        ["symfony-project-product-show"] = new("Fonction detail produit", "Afficher un produit avec conversion de parametre.", "Symfony peut injecter l'entite `Product $product` depuis l'URL.", "La route detail rend un template avec un produit.", "<?php\n\n#[Route('/products/{id}', name: 'product_show')]\npublic function show(Product $product): Response {}", "Cree l'action detail avec route `/products/{id}`, parametre `Product $product` et render Twig.", "<?php\n\n// Action show complete", "La fonctionnalite detail utilise le parametre entite et Twig.", "Type le parametre en Product et passe-le au template.", 75, ["#[Route('/products/{id}', name: 'product_show')]", "public function show(Product $product): Response", "return $this->render('product/show.html.twig'", "'product' => $product"], "Ne remplace pas le parametre Product par un id pour cette lecon.", "<?php\n\n#[Route('/products/{id}', name: 'product_show')]\npublic function show(Product $product): Response\n{\n    return $this->render('product/show.html.twig', ['product' => $product]);\n}"),
        ["symfony-project-product-create"] = new("Fonction creation produit", "Assembler formulaire et Doctrine pour creer un produit.", "La creation combine Request, ProductType, validation formulaire, persist, flush et redirection.", "C'est le premier flux CRUD complet du Product Catalog.", "<?php\n\n#[Route('/products/new', name: 'product_new')]\npublic function new(Request $request): Response {}", "Cree l'action creation complete: route `/products/new`, `Request`, `new Product`, formulaire, `handleRequest`, tests, `persist`, `flush`, `redirectToRoute('product_index')`.", "<?php\n\n// Action new complete", "La fonctionnalite creation couvre formulaire, validation et persistence.", "Suis le flux Symfony: Product, createForm, handleRequest, if valide, persist, flush, redirect.", 85, ["#[Route('/products/new'", "Request $request", "new Product", "createForm(ProductType::class, $product)", "handleRequest($request)", "isSubmitted()", "isValid()", "persist($product)", "flush()", "redirectToRoute('product_index')"], "Sans `handleRequest`, le formulaire ne lit pas la requete. Sans `flush`, Doctrine n'ecrit rien.", "<?php\n\n#[Route('/products/new', name: 'product_new')]\npublic function new(Request $request, EntityManagerInterface $entityManager): Response\n{\n    $product = new Product();\n    $form = $this->createForm(ProductType::class, $product);\n    $form->handleRequest($request);\n\n    if ($form->isSubmitted() && $form->isValid()) {\n        $entityManager->persist($product);\n        $entityManager->flush();\n\n        return $this->redirectToRoute('product_index');\n    }\n\n    return $this->render('product/new.html.twig', ['form' => $form]);\n}"),
        ["symfony-project-product-edit"] = new("Fonction edition produit", "Modifier un produit existant avec formulaire.", "L'edition reutilise ProductType sur une entite deja chargee.", "Doctrine suit deja l'entite, donc `flush()` suffit apres validation.", "<?php\n\n#[Route('/products/{id}/edit', name: 'product_edit')]\npublic function edit(Request $request, Product $product): Response {}", "Cree l'action edition: route edit, `Request`, `Product $product`, formulaire, `handleRequest`, `isValid`, `flush`, redirection.", "<?php\n\n// Action edit complete", "La fonctionnalite edition traite un produit existant.", "Pas besoin de `new Product`: utilise le parametre Product recu.", 80, ["#[Route('/products/{id}/edit'", "Request $request", "Product $product", "createForm(ProductType::class, $product)", "handleRequest($request)", "isSubmitted()", "isValid()", "flush()", "redirectToRoute('product_index')"], "Ne fais pas `persist` obligatoire ici: l'entite existante est deja geree.", "<?php\n\n#[Route('/products/{id}/edit', name: 'product_edit')]\npublic function edit(Request $request, Product $product, EntityManagerInterface $entityManager): Response\n{\n    $form = $this->createForm(ProductType::class, $product);\n    $form->handleRequest($request);\n\n    if ($form->isSubmitted() && $form->isValid()) {\n        $entityManager->flush();\n\n        return $this->redirectToRoute('product_index');\n    }\n\n    return $this->render('product/edit.html.twig', ['form' => $form]);\n}"),
        ["symfony-project-product-delete"] = new("Fonction suppression produit", "Supprimer un produit et revenir a la liste.", "La suppression utilise `remove`, `flush` puis redirection.", "Une action delete reste courte si elle delegue la persistence a EntityManager.", "<?php\n\n#[Route('/products/{id}/delete', name: 'product_delete')]\npublic function delete(Product $product): Response {}", "Cree l'action suppression avec route delete, `Product $product`, `remove`, `flush` et redirection.", "<?php\n\n// Action delete complete", "La fonctionnalite suppression est complete.", "Appelle remove puis flush avant la redirection.", 75, ["#[Route('/products/{id}/delete'", "Product $product", "remove($product)", "flush()", "redirectToRoute('product_index')"], "Sans `flush()`, la suppression ne sera pas appliquee.", "<?php\n\n#[Route('/products/{id}/delete', name: 'product_delete')]\npublic function delete(Product $product, EntityManagerInterface $entityManager): Response\n{\n    $entityManager->remove($product);\n    $entityManager->flush();\n\n    return $this->redirectToRoute('product_index');\n}"),
        ["symfony-project-product-validation"] = new("Validation produit complete", "Ajouter les contraintes de validation du mini-projet.", "La validation protege les champs critiques du formulaire et de l'entite.", "Le Product Catalog refuse un nom vide et des valeurs negatives.", "<?php\n\n#[Assert\\NotBlank]\n#[Assert\\PositiveOrZero]", "Ajoute les contraintes `Assert\\NotBlank` sur `name` et `Assert\\PositiveOrZero` sur `price` et `stock`.", "<?php\n\n// Contraintes Product complete", "Les champs produit portent les contraintes attendues.", "Place les attributs Assert au-dessus des proprietes de l'entite.", 70, ["use Symfony\\Component\\Validator\\Constraints as Assert", "#[Assert\\NotBlank]", "#[Assert\\PositiveOrZero]", "private string $name", "private int $price", "private int $stock"], "Ne mets pas les contraintes dans ProductType pour cet exercice: elles sont attendues sur l'entite.", "<?php\n\nuse Symfony\\Component\\Validator\\Constraints as Assert;\n\n#[Assert\\NotBlank]\nprivate string $name = '';\n\n#[Assert\\PositiveOrZero]\nprivate int $price = 0;\n\n#[Assert\\PositiveOrZero]\nprivate int $stock = 0;"),
        ["symfony-project-protected-route"] = new("Route protegee simple", "Proteger une action de gestion produit.", "`#[IsGranted('ROLE_USER')]` restreint une action a un role.", "La protection simple convient aux actions create, edit ou delete du mini-projet.", "<?php\n\n#[IsGranted('ROLE_USER')]\npublic function new(): Response {}", "Ajoute `#[IsGranted('ROLE_USER')]` sur une action `new`, `edit` ou `delete`.", "<?php\n\n// Action protegee", "Une action sensible est protegee par attribut.", "Importe IsGranted puis place l'attribut au-dessus de l'action.", 70, ["use Symfony\\Component\\Security\\Http\\Attribute\\IsGranted", "#[IsGranted('ROLE_USER')]", "public function"], "La protection doit etre sur l'action, pas seulement mentionnee en commentaire.", "<?php\n\nuse Symfony\\Component\\Security\\Http\\Attribute\\IsGranted;\n\n#[IsGranted('ROLE_USER')]\npublic function new(Request $request): Response\n{\n    return new Response('Protected');\n}"),

        ["php-symfony-boss-final-products"] = new(
            "Boss Final Symfony : Product Catalog complet",
            "Construire une application Symfony Product Catalog complete.",
            "Le boss final assemble entite, repository, service, controller, formulaire, Doctrine, Twig et controle d'acces simple.",
            "Le Product Catalog final doit montrer des blocs coherents plutot que des snippets isoles.",
            "#[Route('/products')]\nfinal class ProductController extends AbstractController\n{\n}",
            "Propose un assemblage coherent du Product Catalog: entite Product, repository, service, ProductController, ProductType, actions CRUD, Doctrine, Twig/render, API JsonResponse, status codes et route protegee.",
            "<?php\n\nnamespace App\\Controller;\n\n// Assemble ici les blocs attendus du Product Catalog.",
            "Le Product Catalog contient les blocs principaux attendus.",
            "Ajoute les blocs manquants: entite, repository, service, controller, formulaire, Doctrine, validation et acces.",
            220,
            ["#[Route", "Controller", "render", "JsonResponse", "Product", "ProductRepository", "Entity", "Id", "Column", "ProductType", "handleRequest", "isSubmitted", "isValid", "Assert", "ProductCatalogService", "__construct", "denyAccessUnlessGranted||IsGranted", "find", "save", "remove||delete", "status"],
            "Ne colle pas seulement une liste de mots-cles: organise les blocs pour que leurs responsabilites restent lisibles.",
            "<?php\n\nnamespace App\\Controller;\n\nuse App\\Entity\\Product;\nuse App\\Form\\ProductType;\nuse App\\Repository\\ProductRepository;\nuse App\\Service\\ProductCatalogService;\nuse Doctrine\\ORM\\EntityManagerInterface;\nuse Doctrine\\ORM\\Mapping as ORM;\nuse Symfony\\Bundle\\FrameworkBundle\\Controller\\AbstractController;\nuse Symfony\\Component\\Form\\AbstractType;\nuse Symfony\\Component\\Form\\FormBuilderInterface;\nuse Symfony\\Component\\HttpFoundation\\JsonResponse;\nuse Symfony\\Component\\HttpFoundation\\Request;\nuse Symfony\\Component\\HttpFoundation\\Response;\nuse Symfony\\Component\\Routing\\Attribute\\Route;\nuse Symfony\\Component\\Security\\Http\\Attribute\\IsGranted;\nuse Symfony\\Component\\Validator\\Constraints as Assert;\n\n#[ORM\\Entity(repositoryClass: ProductRepository::class)]\nclass Product\n{\n    #[ORM\\Id]\n    #[ORM\\GeneratedValue]\n    #[ORM\\Column]\n    private ?int $id = null;\n\n    #[ORM\\Column]\n    #[Assert\\NotBlank]\n    private string $name = '';\n\n    #[ORM\\Column]\n    #[Assert\\PositiveOrZero]\n    private int $price = 0;\n\n    #[ORM\\Column]\n    #[Assert\\PositiveOrZero]\n    private int $stock = 0;\n}\n\nfinal class ProductRepository\n{\n    public function find(int $id): ?Product { return null; }\n    public function findAvailable(): array { return []; }\n    public function save(Product $product): void {}\n    public function remove(Product $product): void {}\n}\n\nfinal readonly class ProductCatalogService\n{\n    public function __construct(private ProductRepository $products) {}\n    public function getAvailableProducts(): array { return $this->products->findAvailable(); }\n}\n\nfinal class ProductType extends AbstractType\n{\n    public function buildForm(FormBuilderInterface $builder, array $options): void\n    {\n        $builder->add('name')->add('price')->add('stock');\n    }\n\n    public function configureOptions($resolver): void\n    {\n        $resolver->setDefaults(['data_class' => Product::class]);\n    }\n}\n\n#[Route('/products')]\nfinal class ProductController extends AbstractController\n{\n    public function __construct(private ProductCatalogService $products) {}\n\n    #[Route('', name: 'product_index')]\n    public function index(): Response { return $this->render('product/index.html.twig'); }\n\n    #[Route('/{id}', name: 'product_show')]\n    public function show(Product $product): Response { return $this->render('product/show.html.twig'); }\n\n    #[Route('/new', name: 'product_new')]\n    #[IsGranted('ROLE_USER')]\n    public function new(Request $request, EntityManagerInterface $entityManager): Response\n    {\n        $product = new Product();\n        $form = $this->createForm(ProductType::class, $product);\n        $form->handleRequest($request);\n        if ($form->isSubmitted() && $form->isValid()) {\n            $entityManager->persist($product);\n            $entityManager->flush();\n            return $this->redirectToRoute('product_index');\n        }\n        return $this->render('product/new.html.twig', ['form' => $form]);\n    }\n\n    #[Route('/api', name: 'product_api')]\n    public function api(): JsonResponse { return new JsonResponse(['status' => 'ok'], 200); }\n\n    #[Route('/{id}/edit', name: 'product_edit')]\n    public function edit(Product $product): Response { return $this->redirectToRoute('product_index'); }\n\n    #[Route('/{id}/delete', name: 'product_delete')]\n    public function delete(Product $product, EntityManagerInterface $entityManager): Response\n    {\n        $entityManager->remove($product);\n        $entityManager->flush();\n        return $this->redirectToRoute('product_index');\n    }\n}")
    };

    private static Course PhpSymfonyCourse() =>
        new()
        {
            Slug = "php-symfony",
            Title = "PHP / Symfony",
            Description = "Apprendre PHP moderne puis construire une mini-application Symfony Product Catalog.",
            Language = "php-symfony",
            SortOrder = 3,
            Chapters =
            [
                PhpSymfonyChapter(1, "PHP 1 - B-A-BA : premiers scripts PHP", "Depuis zero: script, sortie, commentaires, variables, types, conditions et boucles.",
                    ["php-what-is-php", "php-syntax-script", "php-echo-output", "php-comments", "php-variables-product", "php-strings-output", "php-numbers-price", "php-booleans-stock", "php-types-order", "php-condition-stock", "php-condition-discount", "php-match-order-status", "php-loop-invoice-lines", "php-while-stock-decrease"]),
                PhpSymfonyChapter(2, "PHP 2 - Tableaux, listes et donnees metier", "Variables groupees, tableaux indexes, associatifs, listes, filtrage, transformation et reduction.",
                    ["php-array-product-names", "php-array-access-index", "php-array-count-products", "php-associative-product", "php-products-list", "php-foreach-products", "php-filter-products-in-stock", "php-compute-cart-total", "php-array-map-product-labels", "php-array-filter-available", "php-array-reduce-total", "php-sort-products-by-price"]),
                PhpSymfonyChapter(3, "PHP 3 - Fonctions metier", "Extraire la logique dans des fonctions typees, reutilisables et testables.",
                    ["php-function-why", "php-function-format-product", "php-function-parameters", "php-function-return", "php-function-is-available", "php-function-calculate-total", "php-function-default-parameters", "php-function-named-arguments", "php-closure-discount", "php-arrow-function-tax", "php-function-refactor-duplicated-code"]),
                PhpSymfonyChapter(4, "PHP 4 - PHP moderne 8.x", "Strict types, types scalaires, retours, nullable, union, match, enum, readonly, date et syntaxe moderne.",
                    ["php-strict-types", "php-scalar-type-hints", "php-return-types", "php-nullable-types", "php-union-types", "php-match-expression", "php-enum-order-status", "php-match-with-enum", "php-readonly-product", "php-date-time-immutable", "php-string-functions-clean-input", "php-null-coalescing", "php-spread-operator"]),
                PhpSymfonyChapter(5, "PHP 5 - POO PHP professionnelle", "Passer des tableaux au domaine objet: Product, encapsulation, exceptions, interfaces, service et composition.",
                    ["php-oop-why-objects", "php-oop-product-class", "php-oop-properties", "php-oop-constructor", "php-oop-constructor-promotion", "php-oop-getters", "php-oop-business-method", "php-oop-encapsulation", "php-oop-guard-negative-price", "php-oop-custom-exception", "php-oop-interface", "php-oop-implementation", "php-oop-service-composition", "php-oop-abstract-class", "php-oop-trait", "php-oop-try-catch", "php-oop-composition-over-inheritance"]),
                PhpSymfonyChapter(6, "PHP 6 - Organisation professionnelle", "Structure src/public, namespaces, use, Composer, autoload PSR-4 et standards PSR-12.",
                    ["php-project-structure-src-public", "php-namespace-app-domain", "php-use-import", "php-composer-json-minimal", "php-composer-autoload-psr4", "php-psr4-directory-mapping", "php-psr12-class-style", "php-composer-require-package", "php-composer-scripts", "php-autoload-require-vendor"]),
                PhpSymfonyChapter(7, "PHP 7 - PHP web natif : comprendre HTTP", "Requete, reponse, GET, POST, SERVER, session, cookie, fichier, JSON, status code et mini-router.",
                    ["php-http-request-response", "php-get-query-param", "php-post-form-data", "php-server-request-method", "php-server-request-uri", "php-session-cart", "php-cookie-theme", "php-file-upload-validation", "php-json-response-native", "php-http-status-code", "php-basic-router", "php-route-products-get-post"]),
                PhpSymfonyChapter(8, "PHP 8 - Persistance : JSON, fichiers et PDO", "Garder les donnees entre deux requetes avec JSON, fichiers, PDO, requetes preparees et repositories.",
                    ["php-json-encode-products", "php-json-decode-products", "php-file-put-contents", "php-file-get-contents", "php-file-products-repository", "php-pdo-connection", "php-pdo-exception-mode", "php-pdo-prepared-select", "php-pdo-insert-product", "php-pdo-update-stock", "php-pdo-delete-product", "php-pdo-repository"]),
                Chapter("Boss Final PHP - Product Catalog natif", "Mini backend PHP natif de catalogue produits.", 9, 0,
                [
                    PhpSymfonyLesson("php-boss-final-native-product-catalog", 1, isBossFinal: true)
                ]),
                PhpSymfonyChapter(10, "Symfony 1 - Comprendre Symfony", "Pourquoi le framework existe, structure projet, front controller, cycle HTTP, console et .env.",
                    ["symfony-why-framework", "symfony-project-structure", "symfony-public-index", "symfony-request-response-flow", "symfony-bin-console", "symfony-env-file"]),
                PhpSymfonyChapter(11, "Symfony 2 - Routing et Controllers", "Le mini-router PHP devient routes, controllers, Request, Response, JsonResponse et erreurs.",
                    ["symfony-controller-basic", "symfony-route-index", "symfony-response", "symfony-json-response", "symfony-route-parameter", "symfony-request-query", "symfony-request-post", "symfony-redirect", "symfony-not-found"]),
                PhpSymfonyChapter(12, "Symfony 3 - Twig et vues", "Construire les vues du Product Catalog avec render, variables, boucles, conditions, liens, includes et layout.",
                    ["symfony-render-template", "symfony-twig-variable", "symfony-twig-loop", "symfony-twig-condition", "symfony-twig-path", "symfony-twig-include", "symfony-twig-layout", "symfony-twig-product-card"]),
                PhpSymfonyChapter(13, "Symfony 4 - Doctrine ORM", "Le repository PHP devient Doctrine: entite, id, colonnes, repository, find, requetes, save, delete et relations.",
                    ["symfony-doctrine-why-orm", "symfony-doctrine-entity", "symfony-doctrine-id", "symfony-doctrine-columns", "symfony-doctrine-repository", "symfony-doctrine-find", "symfony-doctrine-query", "symfony-doctrine-save", "symfony-doctrine-delete", "symfony-doctrine-relations"]),
                PhpSymfonyChapter(14, "Symfony 5 - Forms et Validation", "Le POST natif devient ProductType, createForm, handleRequest, contraintes et erreurs utilisateur.",
                    ["symfony-form-why", "symfony-form-type", "symfony-form-fields", "symfony-create-form", "symfony-handle-request", "symfony-validation-constraints", "symfony-form-errors", "symfony-form-product-create", "symfony-form-product-edit"]),
                PhpSymfonyChapter(15, "Symfony 6 - Services et architecture", "Le service PHP natif devient service Symfony injecte et teste hors controller.",
                    ["symfony-service-why", "symfony-service-class", "symfony-dependency-injection", "symfony-service-repository-injection", "symfony-controller-service-injection", "symfony-controller-delegation", "symfony-dto", "symfony-command"]),
                PhpSymfonyChapter(16, "Symfony 7 - Securite simple", "Utilisateur, hash de mot de passe, roles, checks d'acces, routes protegees et CSRF.",
                    ["symfony-security-why", "symfony-user-entity", "symfony-password-hashing", "symfony-roles", "symfony-is-granted", "symfony-deny-access-unless-granted", "symfony-protected-route", "symfony-csrf-basic"]),
                PhpSymfonyChapter(17, "Symfony 8 - API Symfony", "JsonResponse, endpoints liste/detail/creation, erreurs de validation, status codes et service layer.",
                    ["symfony-api-json-response", "symfony-api-list-products", "symfony-api-show-product", "symfony-api-create-product", "symfony-api-validation-errors", "symfony-api-status-codes", "symfony-api-service-layer"]),
                PhpSymfonyChapter(18, "Symfony 9 - Projet Product Catalog Symfony", "Fonctionnalites verticales: liste, detail, creation, edition, suppression, validation, service, route protegee, API et architecture.",
                    ["symfony-project-product-list", "symfony-project-product-show", "symfony-project-product-create", "symfony-project-product-edit", "symfony-project-product-delete", "symfony-project-product-validation", "symfony-project-product-service", "symfony-project-protected-route", "symfony-project-api-endpoint", "symfony-project-clean-architecture"]),
                Chapter("Boss Final Symfony", "Application Symfony Product Catalog complete.", 19, 0,
                [
                    PhpSymfonyLesson("php-symfony-boss-final-products", 1, isBossFinal: true)
                ])
            ]
        };

    private static Chapter PhpSymfonyChapter(int sortOrder, string title, string description, string[] lessonSlugs) =>
        Chapter($"Module {sortOrder} - {title}", description, sortOrder, 0,
            lessonSlugs.Select((slug, index) => PhpSymfonyLesson(slug, index + 1)).ToList());

    private static Lesson PhpSymfonyLesson(string slug, int sortOrder, bool isBossFinal = false)
    {
        var spec = PhpSymfonyLessonSpecFor(slug);
        return PhpLesson(
            slug,
            spec.Title,
            spec.Objective,
            spec.Explanation,
            spec.ExampleCode,
            spec.ExercisePrompt,
            spec.StarterCode,
            spec.SuccessFeedback,
            spec.FailureFeedback,
            spec.XpReward,
            sortOrder,
            spec.RequiredSnippets.Select((snippet, index) =>
            {
                var test = Required($"Contient {snippet}", snippet);
                test.SortOrder = index + 1;
                return test;
            }).Concat(PhpForbiddenSnippetsFor(slug).Select((snippet, index) =>
            {
                var test = Forbidden($"Evite {snippet}", snippet);
                test.SortOrder = spec.RequiredSnippets.Length + index + 1;
                return test;
            })).ToList(),
            conceptSummary: spec.ConceptSummary,
            commonMistakes: spec.CommonMistakes,
            finalCorrection: spec.FinalCorrection,
            isBossFinal: isBossFinal);
    }

    private static string[] PhpForbiddenSnippetsFor(string slug) => slug switch
    {
        "php-condition-discount" => ["echo \"Total final : 108\"", "echo 'Total final : 108'"],
        "php-compute-cart-total" or "php-array-reduce-total" or "php-function-calculate-total" => ["echo 39", "return 39"],
        "php-filter-products-in-stock" => ["unset($products", "$products = []"],
        "php-function-format-product" => ["echo \"Book - 12 euros\"", "echo 'Book - 12 euros'"],
        "php-pdo-prepared-select" or "php-pdo-insert-product" => ["query(\"SELECT", "query('SELECT", "query(\"INSERT", "query('INSERT"],
        _ => []
    };

    private static Chapter PhpSymfonyModule(int sortOrder, string title, string description, string[] lessonTitles) =>
        Chapter($"Module {sortOrder} - {title}", description, sortOrder, 0,
            lessonTitles.Select((lessonTitle, index) =>
            {
                var expectedSnippets = PhpSymfonyExpectedSnippetsFor(lessonTitle);
                var expectedText = string.Join(", ", expectedSnippets);
                return PhpLesson(
                    $"php-symfony-module-{sortOrder}-{Slugify(lessonTitle)}",
                    lessonTitle,
                    $"Comprendre et pratiquer: {lessonTitle}.",
                    $"{lessonTitle} fait partie du socle PHP/Symfony du module. L'exercice valide les mots-cles et structures essentiels sans sortir du cadre PHP/Symfony.",
                    PhpSymfonyExampleFor(lessonTitle),
                    PhpSymfonyExercisePromptFor(lessonTitle, expectedText),
                    PhpSymfonyStarterFor(lessonTitle, expectedSnippets),
                    "La notion est presente dans un code PHP/Symfony coherent.",
                    $"Ajoute les elements attendus: {expectedText}.",
                    45 + index * 5,
                    index + 1,
                    PhpSymfonyTestsFor(lessonTitle),
                    conceptSummary: $"{lessonTitle} est une brique du parcours PHP/Symfony.",
                    finalCorrection: PhpSymfonySolutionFor(lessonTitle));
            }).ToList());

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

    private static string PhpSymfonyExercisePromptFor(string lessonTitle, string expectedText) => lessonTitle switch
    {
        "Classes" => "Dans src/Model/Product.php, declare une classe Product finale. Elle servira de modele simple pour les exercices suivants.",
        "Objets" => "En partant d'une classe Product deja disponible, instancie un produit avec new Product et stocke-le dans $product.",
        "Proprietes" => "Dans Product, declare deux proprietes privees: string $name et int $price. Ces champs representent le nom et le prix en euros.",
        "Methodes" => "Dans Product, ajoute getName(): string qui retourne $this->name. Garde une methode publique et typee.",
        "Constructeurs" => "Dans Product, ajoute un constructeur qui recoit string $name et int $price, puis stocke ces valeurs dans l'objet.",
        "Encapsulation" => "Garde int $price prive, puis ajoute getPrice(): int pour exposer la valeur sans rendre la propriete publique.",
        "Structure d'un projet Symfony" => "Ecris les dossiers principaux d'un petit projet Symfony produit: src pour le code PHP et templates pour Twig.",
        "Routes" => "Declare une route #[Route('/products', name: 'product_index')] pour afficher la liste des produits.",
        "Controllers" => "Cree ProductController qui herite de AbstractController. Il contiendra les actions de la ressource Product.",
        "Responses" => "Retourne une Response avec le texte OK depuis une action de controller.",
        "Templates Twig" => "Dans un template Twig produit, affiche le nom du produit avec {{ product.name }}.",
        "Parametres de route" => "Declare une route detail #[Route('/products/{id}', name: 'product_show')] et une action show qui recoit int $id.",
        "Creation de formulaire" => "Dans un controller, cree un formulaire ProductType lie a $product avec $this->createForm(ProductType::class, $product).",
        "Validation" => "Dans Product, ajoute une contrainte #[Assert\\NotBlank] sur la propriete $name pour refuser un nom vide.",
        "Contraintes" => "Dans Product, ajoute #[Assert\\Positive] sur $price pour imposer un prix strictement positif.",
        "Gestion des erreurs" => "Dans le template du formulaire, affiche les erreurs du champ name avec form_errors(form.name).",
        "Traitement de la soumission" => "Dans le controller, appelle handleRequest($request), puis teste isSubmitted() et isValid() avant d'enregistrer.",
        "Entites" => "Transforme Product en entite Doctrine avec l'attribut #[ORM\\Entity].",
        "Repositories" => "Declare ProductRepository comme repository Doctrine en etendant ServiceEntityRepository.",
        "Migrations" => "Cree une classe de migration Version20260101000000 qui etend AbstractMigration.",
        "Relations simples" => "Dans Product, ajoute une relation ManyToOne nullable vers Category avec #[ORM\\ManyToOne].",
        "CRUD avec Doctrine" => "Montre le cycle Doctrine minimal sur $product: persist, flush, puis remove pour une suppression.",
        "Services" => "Cree ProductService, service applicatif dedie aux regles metier produit.",
        "Injection de dependances" => "Dans un controller ou un service, injecte ProductService via __construct(private ProductService $productService).",
        "Configuration" => "Ajoute une entree services.yaml pour App\\Service\\ProductService.",
        "Separation controller / service" => "Dans un controller, delegue la creation d'un produit a $productService->create($data).",
        "Bonnes pratiques Symfony" => "Declare ProductService en final readonly pour montrer un service immutable simple.",
        "Authentification" => "Declare LoginFormAuthenticator en etendant AbstractLoginFormAuthenticator.",
        "Utilisateurs" => "Declare une classe User qui implemente UserInterface.",
        "Roles" => "Retourne un tableau de roles contenant ROLE_USER.",
        "Protection des routes" => "Protege une action account avec #[IsGranted('ROLE_USER')].",
        "Autorisations simples" => "Dans une action, refuse l'acces sans ROLE_USER avec denyAccessUnlessGranted('ROLE_USER').",
        "Mini-application MVC" => "Assemble le debut d'une ressource Product avec une route /products et un ProductController qui herite de AbstractController.",
        "Formulaires" => "Dans ProductController, cree le formulaire ProductType pour $product avec createForm.",
        "Doctrine" => "Dans une action, persiste $product avec Doctrine puis appelle flush.",
        "Securite simple" => "Ajoute une verification ROLE_USER sur une action de gestion produit.",
        _ => $"Complete le code avec un exemple concret de {lessonTitle}. Elements attendus: {expectedText}."
    };

    private static string PhpSymfonyStarterFor(string lessonTitle, IReadOnlyList<string>? expectedSnippets = null)
    {
        var expected = expectedSnippets is { Count: > 0 }
            ? $"// Elements attendus: {string.Join(", ", expectedSnippets)}\n"
            : "";
        return lessonTitle switch
        {
            "Classes" => "<?php\n\n// src/Model/Product.php\n// Declare la classe Product ici.\n",
            "Objets" => "<?php\n\n$product = null;\n// Instancie Product ici.\n",
            "Structure d'un projet Symfony" => "src/\ntemplates/\n",
            "Templates Twig" => "<h1>{# Affiche product.name ici #}</h1>\n",
            "Gestion des erreurs" => "{# Affiche les erreurs du champ name ici #}\n",
            "Configuration" => "services:\n  # Declare App\\Service\\ProductService ici\n",
            _ => $"<?php\n\n// Complete une implementation PHP/Symfony pour: {lessonTitle}\n{expected}"
        };
    }

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
        var expected = PhpSymfonyExpectedSnippetsFor(lessonTitle);

        return expected.Select((snippet, index) =>
        {
            var test = Required($"Contient {snippet}", snippet);
            test.SortOrder = index + 1;
            return test;
        }).ToList();
    }

    private static string[] PhpSymfonyExpectedSnippetsFor(string lessonTitle) => lessonTitle switch
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


    private static async Task EnsureIntermediateBossesSeededAsync(AppDbContext db)
    {
        var courses = await db.Courses
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .ThenInclude(lesson => lesson.Tests)
            .ToListAsync();
        var existingBosses = await db.IntermediateBosses
            .Include(boss => boss.ValidationRules)
            .Include(boss => boss.Hints)
            .ToListAsync();
        var existingBossesByModuleId = existingBosses.ToDictionary(boss => boss.ModuleId);

        foreach (var course in courses)
        {
            foreach (var chapter in course.Chapters.Where(chapter => !chapter.Title.Contains("Boss Final", StringComparison.OrdinalIgnoreCase)))
            {
                var boss = BuildIntermediateBoss(course, chapter);
                if (existingBossesByModuleId.TryGetValue(chapter.Id, out var existingBoss))
                {
                    ApplyIntermediateBoss(existingBoss, boss);
                    continue;
                }

                chapter.IntermediateBoss = boss;
            }
        }

        await db.SaveChangesAsync();
    }

    private static IntermediateBoss BuildIntermediateBoss(Course course, Chapter chapter)
    {
        return course.Language == "php-symfony"
            ? PhpSymfonyIntermediateBoss(chapter.SortOrder) ?? BuildIntermediateBossFromModule(course, chapter)
            : course.Language is "react" or "react-native" or "tailwindcss"
            ? StaticSnippetIntermediateBoss(course.Language, chapter.SortOrder) ?? BuildIntermediateBossFromModule(course, chapter)
            : course.Language == "sqlserver" && chapter.SortOrder == 1
            ? SqlModuleOneIntermediateBoss()
            : course.Language == "csharp" && chapter.SortOrder == 1
                ? CSharpModuleOneIntermediateBoss()
                : BuildIntermediateBossFromModule(course, chapter);
    }

    private static void AttachIntermediateBosses(Course course)
    {
        foreach (var chapter in course.Chapters.Where(chapter => !chapter.Title.Contains("Boss Final", StringComparison.OrdinalIgnoreCase)))
        {
            chapter.IntermediateBoss ??= BuildIntermediateBoss(course, chapter);
        }
    }

    private static void ApplyIntermediateBoss(IntermediateBoss target, IntermediateBoss source)
    {
        target.Slug = source.Slug;
        target.Title = source.Title;
        target.Objective = source.Objective;
        target.Instructions = source.Instructions;
        target.StarterCode = source.StarterCode;
        target.ExpectedResult = source.ExpectedResult;
        target.Solution = source.Solution;
        target.XpReward = source.XpReward;
        target.IsRequiredToUnlockNextModule = source.IsRequiredToUnlockNextModule;

        target.ValidationRules.Clear();
        foreach (var rule in source.ValidationRules.OrderBy(rule => rule.SortOrder))
        {
            target.ValidationRules.Add(new IntermediateBossValidationRule
            {
                Name = rule.Name,
                TestType = rule.TestType,
                ExpectedOutput = rule.ExpectedOutput,
                RequiredSnippet = rule.RequiredSnippet,
                HiddenCode = rule.HiddenCode,
                MinCount = rule.MinCount,
                ExpectedColumns = rule.ExpectedColumns,
                ExpectedRowCount = rule.ExpectedRowCount,
                SortOrder = rule.SortOrder
            });
        }

        target.Hints.Clear();
        foreach (var hint in source.Hints.OrderBy(hint => hint.SortOrder))
        {
            target.Hints.Add(new IntermediateBossHint
            {
                Content = hint.Content,
                SortOrder = hint.SortOrder
            });
        }
    }

    private static IntermediateBoss? StaticSnippetIntermediateBoss(string language, int moduleSortOrder) =>
        language switch
        {
            "react" => moduleSortOrder switch
            {
                1 => PhpSymfonyBoss(
                    "react-module-1-intermediate-boss",
                    "Monstre intermediaire React - JSX",
                    "Construire un composant JSX propre.",
                    "Cree App avec ProductHeader et ProductList, une interface JSX structuree avec className et contenu Product Manager.",
                    "function App() {\n  return null;\n}",
                    "Le composant retourne un JSX lisible et stylable.",
                    90,
                    ["function App", "function ProductHeader", "return", "<", "className=", "Product Manager"],
                    ["Commence par App et ProductHeader.", "Retourne du JSX avec un element principal.", "Ajoute className, composition et le texte Product Manager."],
                    "function ProductHeader() { return <header className=\"product-header\"><h1>Product Manager</h1></header>; }\nfunction ProductList() { return <ul><li>Book</li></ul>; }\nfunction App() { return <><ProductHeader /><ProductList /></>; }"),
                3 => PhpSymfonyBoss(
                    "react-module-3-intermediate-boss",
                    "Monstre intermediaire React - Etat et evenements",
                    "Combiner state, events et input controle.",
                    "Cree un filtre produit avec useState, value, onChange et un bouton qui modifie l'etat.",
                    "function App() {\n  return null;\n}",
                    "Le composant controle une saisie et reagit aux evenements.",
                    95,
                    ["function App", "useState", "value=", "onChange", "event.target.value", "onClick"],
                    ["Ajoute un etat filter.", "Lie l'input avec value et onChange.", "Ajoute une action click qui met a jour l'etat."],
                    "function App() { const [filter, setFilter] = useState(''); return <><input value={filter} onChange={(event) => setFilter(event.target.value)} /><button onClick={() => setFilter('Book')}>Book</button></>; }"),
                5 => PhpSymfonyBoss(
                    "react-module-5-intermediate-boss",
                    "Monstre intermediaire React - Formulaire",
                    "Construire un formulaire produit valide.",
                    "Cree ProductForm avec champs controles, onSubmit, preventDefault et affichage d'erreur.",
                    "function App() {\n  return <form />;\n}",
                    "Le formulaire gere saisie, soumission et erreur.",
                    100,
                    ["function ProductForm", "useState", "form", "onSubmit", "preventDefault", "onChange", "error"],
                    ["Controle au moins un champ avec useState.", "Traite la soumission avec onSubmit et preventDefault.", "Ajoute un message d'erreur si la valeur manque."],
                    "function ProductForm() { const [name, setName] = useState(''); const [error, setError] = useState(''); function handleSubmit(event) { event.preventDefault(); if (!name.trim()) { setError('Name is required'); } } return <form onSubmit={handleSubmit}><input value={name} onChange={(event) => setName(event.target.value)} />{error && <p>{error}</p>}</form>; }\nfunction App() { return <ProductForm />; }"),
                7 => PhpSymfonyBoss(
                    "react-module-7-intermediate-boss",
                    "Monstre intermediaire React - Hook useProducts",
                    "Creer un hook useProducts avec API et etats.",
                    "Cree useProducts avec useState, useEffect, fetch, loading et error.",
                    "function useProducts() {\n  // Complete le hook\n}\nfunction App() {\n  return null;\n}",
                    "Le hook centralise le chargement des produits.",
                    105,
                    ["function useProducts", "useState", "useEffect", "fetch", "loading", "error"],
                    ["Un custom hook est une fonction qui commence par use.", "Stocke products, loading et error avec useState.", "Charge les donnees dans useEffect avec fetch."],
                    "function useProducts() { const [products, setProducts] = useState([]); const [loading, setLoading] = useState(true); const [error, setError] = useState(''); useEffect(() => { fetch('/api/products').then((response) => response.json()).then(setProducts).catch(() => setError('Failed')).finally(() => setLoading(false)); }, []); return { products, loading, error }; }\nfunction App() { const data = useProducts(); return <p>{data.loading ? 'Loading' : data.products.length}</p>; }"),
                10 => PhpSymfonyBoss(
                    "react-module-10-intermediate-boss",
                    "Monstre intermediaire React - Product Manager",
                    "Assembler une mini interface Product Manager.",
                    "Assemble ProductManager avec useProducts, liste, formulaire, filtre et rendu conditionnel.",
                    "function ProductManager() {\n  return null;\n}",
                    "Le Product Manager combine les briques principales du parcours.",
                    120,
                    ["function ProductManager", "useProducts", "useState", ".map(", "key=", "onSubmit", "loading", "error"],
                    ["Commence par recuperer products avec useProducts.", "Ajoute state et formulaire pour creer un produit.", "Affiche loading/error puis map avec key stable."],
                    "function useProducts() { return { products: [{ id: 1, name: 'Book' }], loading: false, error: '' }; }\nfunction ProductManager() { const { products, loading, error } = useProducts(); const [name, setName] = useState(''); function handleSubmit(event) { event.preventDefault(); } if (loading) return <p>Loading</p>; if (error) return <p>{error}</p>; return <form onSubmit={handleSubmit}><input value={name} onChange={(event) => setName(event.target.value)} />{products.map((product) => <p key={product.id}>{product.name}</p>)}</form>; }"),
                _ => null
            },
            "react-native" => moduleSortOrder switch
            {
                1 => PhpSymfonyBoss(
                    "rn-module-1-intermediate-boss",
                    "Monstre intermediaire React Native - Fondations",
                    "Construire un ecran simple avec View, Text et StyleSheet.",
                    "Cree un ecran ProductHome qui utilise View, Text et StyleSheet.create.",
                    "export function ProductHome() {\n  return null;\n}",
                    "L'ecran mobile utilise les composants de base et des styles.",
                    90,
                    ["View", "Text", "StyleSheet.create", "styles"],
                    ["Importe les composants de base.", "Retourne View avec un Text.", "Cree styles via StyleSheet.create."],
                    "export function ProductHome() { return <View style={styles.container}><Text>Products</Text></View>; }\nconst styles = StyleSheet.create({ container: { flex: 1 } });"),
                3 => PhpSymfonyBoss(
                    "rn-module-3-intermediate-boss",
                    "Monstre intermediaire React Native - FlatList",
                    "Afficher une liste produits avec FlatList.",
                    "Cree une FlatList avec data, renderItem et keyExtractor stable.",
                    "<FlatList />",
                    "La liste mobile utilise FlatList correctement.",
                    95,
                    ["FlatList", "data=", "renderItem", "keyExtractor", "item.name"],
                    ["Passe products a data.", "Rends chaque item avec renderItem.", "Ajoute keyExtractor avec item.id."],
                    "<FlatList data={products} keyExtractor={(item) => String(item.id)} renderItem={({ item }) => <Text>{item.name}</Text>} />"),
                4 => PhpSymfonyBoss(
                    "rn-module-4-intermediate-boss",
                    "Monstre intermediaire React Native - Formulaire",
                    "Construire un formulaire produit mobile.",
                    "Assemble TextInput, useState, onChangeText et Pressable pour soumettre.",
                    "export function ProductForm() {\n  return null;\n}",
                    "Le formulaire mobile controle la saisie et declenche une action.",
                    100,
                    ["TextInput", "useState", "value=", "onChangeText", "Pressable", "onPress"],
                    ["Controle le champ avec useState.", "Lie TextInput avec value et onChangeText.", "Soumets via Pressable et onPress."],
                    "export function ProductForm() { const [name, setName] = useState(''); return <View><TextInput value={name} onChangeText={setName} /><Pressable onPress={handleSubmit}><Text>Save</Text></Pressable></View>; }"),
                6 => PhpSymfonyBoss(
                    "rn-module-6-intermediate-boss",
                    "Monstre intermediaire React Native - API",
                    "Gerer API, loading et error.",
                    "Charge les produits avec useEffect, fetch, setProducts, loading et error.",
                    "export function ProductsScreen() {\n  return null;\n}",
                    "L'ecran mobile gere les etats de chargement et d'erreur.",
                    105,
                    ["useEffect", "fetch", "setProducts", "loading", "error"],
                    ["Ajoute un effet de chargement.", "Utilise fetch puis setProducts.", "Garde loading et error dans l'etat."],
                    "useEffect(() => { fetch('/api/products').then((response) => response.json()).then(setProducts).catch(() => setError('Failed')).finally(() => setLoading(false)); }, []);"),
                8 => PhpSymfonyBoss(
                    "rn-module-8-intermediate-boss",
                    "Monstre intermediaire React Native - Product App",
                    "Assembler une mini Product App mobile.",
                    "Assemble SafeAreaView, FlatList, navigation, params et AsyncStorage.",
                    "export function ProductApp() {\n  return null;\n}",
                    "La Product App relie liste, detail, stockage et navigation.",
                    120,
                    ["SafeAreaView", "FlatList", "navigation.navigate", "route.params", "AsyncStorage", "TextInput"],
                    ["Encadre l'ecran avec SafeAreaView.", "Liste les produits avec FlatList.", "Ajoute navigation et stockage local."],
                    "function ProductApp({ navigation, route }: Props) { AsyncStorage.getItem('products'); return <SafeAreaView><TextInput /><FlatList data={products} renderItem={({ item }) => <Pressable onPress={() => navigation.navigate('ProductDetail', { id: item.id })}><Text>{route.params?.id}{item.name}</Text></Pressable>} /></SafeAreaView>; }"),
                _ => null
            },
            "tailwindcss" => moduleSortOrder switch
            {
                1 => PhpSymfonyBoss(
                    "tailwind-module-1-intermediate-boss",
                    "Monstre intermediaire Tailwind - Carte produit",
                    "Creer une carte produit stylisee.",
                    "Construis une card produit avec padding, couleurs, typographie, radius, bordure et ombre.",
                    "<article class=\"\">\n  Book\n</article>",
                    "La card produit combine les fondations visuelles Tailwind.",
                    90,
                    ["p-", "bg-", "text-", "font-", "rounded", "border", "shadow"],
                    ["Commence par bg, text et padding.", "Ajoute border, rounded et shadow.", "Termine par une hierarchie typo avec font et text."],
                    "<article class=\"rounded-lg border border-slate-200 bg-white p-4 text-slate-900 shadow-sm\"><h2 class=\"text-lg font-semibold\">Book</h2></article>"),
                2 => PhpSymfonyBoss(
                    "tailwind-module-2-intermediate-boss",
                    "Monstre intermediaire Tailwind - Layout",
                    "Creer un layout flex/grid.",
                    "Assemble un container centre, une navbar flex et une grille de stats avec gap.",
                    "<main class=\"container mx-auto p-6\"></main>",
                    "Le layout utilise container, flex, grid et espacements.",
                    95,
                    ["container", "mx-auto", "flex", "items-center", "justify-between", "grid", "grid-cols-", "gap-"],
                    ["Centre la page avec container et mx-auto.", "Ajoute une ligne flex pour la barre haute.", "Place les cards dans une grid avec gap."],
                    "<main class=\"container mx-auto p-6\"><nav class=\"flex items-center justify-between\"></nav><section class=\"grid grid-cols-3 gap-4\"></section></main>"),
                3 => PhpSymfonyBoss(
                    "tailwind-module-3-intermediate-boss",
                    "Monstre intermediaire Tailwind - Responsive",
                    "Rendre une grille responsive.",
                    "Cree une grille mobile-first qui passe de 1 colonne a md puis lg.",
                    "<section class=\"grid gap-4\"></section>",
                    "La grille adapte le dashboard aux tailles d'ecran.",
                    100,
                    ["grid", "grid-cols-1", "md:grid-cols-", "lg:grid-cols-", "gap-"],
                    ["Garde une base mobile en une colonne.", "Ajoute md pour tablette.", "Ajoute lg pour desktop."],
                    "<section class=\"grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-4\"></section>"),
                5 => PhpSymfonyBoss(
                    "tailwind-module-5-intermediate-boss",
                    "Monstre intermediaire Tailwind - Formulaire et table",
                    "Creer un formulaire et une table stylises.",
                    "Construis une section avec formulaire produit, inputs focus, bouton et table responsive.",
                    "<section class=\"rounded bg-white p-4 shadow\"></section>",
                    "La section combine formulaire, table et composants UI.",
                    110,
                    ["form", "input", "type=\"text\"", "type=\"number\"", "focus:", "button", "table", "overflow-x-auto", "rounded", "shadow"],
                    ["Pose une section rounded shadow.", "Ajoute form, inputs text/number et focus.", "Ajoute table dans un wrapper overflow-x-auto."],
                    "<section class=\"rounded bg-white p-4 shadow\"><form class=\"grid gap-3\"><input type=\"text\" class=\"rounded border p-2 focus:ring-2\" /><input type=\"number\" class=\"rounded border p-2 focus:ring-2\" /><button class=\"rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700\">Save</button></form><div class=\"overflow-x-auto\"><table class=\"w-full\"><tr><td>Book</td></tr></table></div></section>"),
                7 => PhpSymfonyBoss(
                    "tailwind-module-7-intermediate-boss",
                    "Monstre intermediaire Tailwind - Section dashboard",
                    "Assembler une section dashboard.",
                    "Cree une section avec sidebar, navbar, stats, cards, table responsive et dark mode simple.",
                    "<main class=\"\"></main>",
                    "La section dashboard rassemble les blocs principaux.",
                    120,
                    ["aside", "nav", "grid", "md:", "lg:", "table", "rounded", "shadow", "gap-", "dark:"],
                    ["Ajoute aside et nav.", "Utilise grid avec md/lg pour les stats.", "Ajoute table, cards, espacements et une variante dark."],
                    "<main class=\"grid gap-6 md:grid-cols-[16rem_1fr] lg:grid-cols-[18rem_1fr]\"><aside class=\"rounded shadow dark:bg-slate-900\"></aside><section><nav></nav><div class=\"grid gap-4 md:grid-cols-3 lg:grid-cols-4\"></div><table></table></section></main>"),
                _ => null
            },
            _ => null
        };

    private static IntermediateBoss? PhpSymfonyIntermediateBoss(int moduleSortOrder) => moduleSortOrder switch
    {
        1 => PhpSymfonyBoss(
            "php-module-1-intermediate-boss",
            "Monstre intermediaire PHP - Fondations",
            "Assembler tableaux, conditions, boucles et fonctions dans un script produits.",
            "Cree une liste de produits, une fonction formatProduct, filtre les produits en stock et affiche leur libelle.",
            "<?php\n\n$products = [];\n\nfunction formatProduct(array $product): string\n{\n    // Complete\n}\n\n// Parcours les produits",
            "Le script utilise tableaux, fonction, condition et boucle.",
            90,
            ["$products", "\"name\"", "\"price\"", "\"stock\"", "function formatProduct", "array $product", "foreach", "if", "stock", "echo"],
            ["Commence par un tableau de produits associatifs.", "Ajoute une fonction qui formate name et price.", "Parcours avec foreach et filtre stock > 0 avant echo."],
            "<?php\n\n$products = [[\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3]];\nfunction formatProduct(array $product): string { return $product[\"name\"] . \" - \" . $product[\"price\"]; }\nforeach ($products as $product) { if ($product[\"stock\"] > 0) { echo formatProduct($product); } }"),
        2 => PhpSymfonyBoss(
            "php-module-2-intermediate-boss",
            "Catalogue de produits en tableaux",
            "Filtrer, transformer et calculer une liste de produits.",
            "Cree une liste de produits associatifs, filtre le stock, genere des labels et calcule un total.",
            "<?php\n\n$products = [];\n\n// TODO: filter, map, reduce",
            "Le catalogue utilise tableaux associatifs, foreach, array_filter, array_map et array_reduce.",
            95,
            ["$products", "\"name\"", "\"price\"", "\"stock\"", "foreach", "array_filter", "array_map", "array_reduce", "$product[\"stock\"] > 0"],
            ["Modele d'abord les produits avec name, price, stock.", "Ajoute le foreach puis array_filter et array_map.", "Termine par array_reduce pour calculer le total."],
            "<?php\n\n$products = [[\"name\" => \"Book\", \"price\" => 12, \"stock\" => 3], [\"name\" => \"Pen\", \"price\" => 2, \"stock\" => 0]];\nforeach ($products as $product) { echo $product[\"name\"]; }\n$available = array_filter($products, fn($product) => $product[\"stock\"] > 0);\n$labels = array_map(fn($product) => $product[\"name\"] . \" - \" . $product[\"price\"], $available);\n$total = array_reduce($available, fn($carry, $product) => $carry + $product[\"price\"], 0);"),
        3 => PhpSymfonyBoss(
            "php-module-3-intermediate-boss",
            "Fonctions metier du panier",
            "Creer plusieurs fonctions metier: formatProduct, isAvailable, calculateCartTotal et formatPrice.",
            "Decoupe le traitement panier dans des fonctions typees reutilisables.",
            "<?php\n\n// TODO: fonctions panier",
            "Les fonctions couvrent formatage, disponibilite, total et prix.",
            100,
            ["function formatProduct", "function isAvailable", "function calculateCartTotal", "function formatPrice", "array $product", "array $cart", ": string", ": bool", ": float"],
            ["Commence par les signatures typees.", "Ajoute foreach et total dans calculateCartTotal.", "Fais retourner les valeurs, sans echo dans les fonctions."],
            "<?php\n\nfunction formatProduct(array $product): string { return $product[\"name\"] . \" - \" . $product[\"price\"]; }\nfunction isAvailable(array $product): bool { return $product[\"stock\"] > 0; }\nfunction calculateCartTotal(array $cart): float { $total = 0; foreach ($cart as $line) { $total += $line[\"price\"] * $line[\"quantity\"]; } return $total; }\nfunction formatPrice(float $price, string $currency = \"EUR\"): string { return $price . \" \" . $currency; }"),
        4 => PhpSymfonyBoss("php-module-4-intermediate-boss", "Monstre intermediaire PHP - Composer PSR-4", "Creer un mini-projet Composer PSR-4.", "Ecris un composer.json avec require, autoload PSR-4, App\\\\ vers src/ et script test.", "{\n  \"name\": \"app/product-catalog\"\n}", "Le mini-projet Composer expose autoload et conventions.", 90, ["\"require\"", "\"autoload\"", "\"psr-4\"", "\"App\\\\\": \"src/\"", "\"scripts\"", "\"test\""], ["Ajoute require.", "Configure autoload psr-4.", "Ajoute un script de dev."], "{\n  \"require\": { \"php\": \"^8.2\" },\n  \"autoload\": { \"psr-4\": { \"App\\\\\": \"src/\" } },\n  \"scripts\": { \"test\": \"phpunit\" }\n}"),
        5 => PhpSymfonyBoss("php-module-5-intermediate-boss", "Domaine objet Product Catalog", "Creer Product, ProductRepositoryInterface, InMemoryProductRepository, ProductCatalogService et exception metier.", "Assemble un domaine objet complet avec repository, service, composition et exception.", "<?php\n\n// Domaine objet Product Catalog", "Le domaine objet separe modele, contrat, implementation et service.", 115, ["final class Product", "private string $name", "private float $price", "private int $stock", "InvalidProductPriceException", "interface ProductRepositoryInterface", "implements ProductRepositoryInterface", "array_filter", "ProductCatalogService", "__construct", "$this->"], ["Commence par Product et l'exception.", "Ajoute l'interface puis l'implementation en memoire.", "Injecte le repository dans ProductCatalogService."], "<?php\n\nclass InvalidProductPriceException extends InvalidArgumentException {}\nfinal class Product { public function __construct(private string $name, private float $price, private int $stock) { if ($price < 0) { throw new InvalidProductPriceException(); } } public function isAvailable(): bool { return $this->stock > 0; } }\ninterface ProductRepositoryInterface { public function findAvailable(): array; }\nfinal class InMemoryProductRepository implements ProductRepositoryInterface { public function __construct(private array $products) {} public function findAvailable(): array { return array_filter($this->products, fn(Product $product) => $product->isAvailable()); } }\nfinal class ProductCatalogService { public function __construct(private ProductRepositoryInterface $products) {} public function listAvailableProducts(): array { return $this->products->findAvailable(); } }"),
        7 => PhpSymfonyBoss("php-module-7-intermediate-boss", "Mini endpoint PHP natif", "Creer un mini-router PHP qui retourne une liste de produits en JSON.", "Lis REQUEST_METHOD et REQUEST_URI, route GET /products et retourne une reponse JSON.", "<?php\n\n// Endpoint PHP natif", "L'endpoint lit HTTP et retourne JSON.", 100, ["$_SERVER", "REQUEST_METHOD", "REQUEST_URI", "/products", "match||switch", "header", "Content-Type: application/json", "json_encode"], ["Lis la methode et l'URI.", "Route /products avec match ou switch.", "Ajoute le header JSON puis json_encode."], "<?php\n\n$method = $_SERVER[\"REQUEST_METHOD\"] ?? \"GET\";\n$uri = $_SERVER[\"REQUEST_URI\"] ?? \"/products\";\n$response = match ([$method, $uri]) { [\"GET\", \"/products\"] => [\"products\" => []], default => [\"error\" => \"not_found\"] };\nheader(\"Content-Type: application/json\");\necho json_encode($response);"),
        8 => PhpSymfonyBoss("php-module-8-intermediate-boss", "Mini repository PDO", "Creer une connexion PDO, preparer un SELECT et inserer un produit avec requetes preparees.", "Configure PDO, execute un SELECT par id et un INSERT produit avec parametres.", "<?php\n\n// Repository PDO", "Le repository utilise PDO et des requetes preparees.", 110, ["new PDO", "PDO::ATTR_ERRMODE", "PDO::ERRMODE_EXCEPTION", "prepare", "SELECT", ":id", "INSERT INTO products", ":name", ":price", "execute", "fetch"], ["Cree la connexion PDO.", "Configure le mode exception.", "Utilise prepare/execute pour SELECT et INSERT."], "<?php\n\n$pdo = new PDO($dsn, $username, $password);\n$pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);\n$select = $pdo->prepare(\"SELECT * FROM products WHERE id = :id\");\n$select->execute([\"id\" => 1]);\n$product = $select->fetch();\n$insert = $pdo->prepare(\"INSERT INTO products (name, price) VALUES (:name, :price)\");\n$insert->execute([\"name\" => \"Book\", \"price\" => 12]);"),
        11 => PhpSymfonyBoss("symfony-module-2-intermediate-boss", "Routing et Controller Product", "Assembler route, controller, Response et JsonResponse pour Product.", "Cree un ProductController avec route /products, route detail et reponse JSON.", "<?php\n\n// ProductController routes", "Le controller expose des routes produit coherentes.", 100, ["#[Route", "ProductController", "AbstractController", "Response", "JsonResponse", "/products", "{id}"], ["Cree ProductController.", "Ajoute routes index et show.", "Retourne Response ou JsonResponse."], "<?php\n\n#[Route('/products')]\nfinal class ProductController extends AbstractController { #[Route('', name: 'product_index')] public function index(): Response { return new Response('Products'); } #[Route('/{id}', name: 'product_show')] public function show(int $id): JsonResponse { return new JsonResponse(['id' => $id]); } }"),
        12 => PhpSymfonyBoss("symfony-module-3-intermediate-boss", "Vue Twig Product Catalog", "Construire une vue Twig qui liste les produits.", "Utilise render, une variable products, une boucle Twig, un include et un lien path.", "{% for product in products %}", "La vue Twig affiche le catalogue avec composants reutilisables.", 95, ["render", "products", "{% for product in products %}", "{{ product.name }}", "path(", "include"], ["Passe products au template.", "Boucle dans Twig.", "Extrais la carte avec include."], "<?php\n\nreturn $this->render('product/index.html.twig', ['products' => $products]);\n\n{% for product in products %}\n    {{ include('product/_card.html.twig', { product: product }) }}\n    <a href=\"{{ path('product_show', { id: product.id }) }}\">Voir</a>\n{% endfor %}"),
        13 => PhpSymfonyBoss("symfony-module-4-intermediate-boss", "Entity et Repository Product", "Declarer Product Doctrine et ProductRepository.", "Assemble Entity, Id, Column, repository et une requete findAvailable.", "<?php\n\n// Product entity", "Doctrine sait mapper et charger les produits.", 110, ["Entity", "Id", "Column", "ProductRepository", "ServiceEntityRepository", "findAvailable", "createQueryBuilder"], ["Declare l'entite.", "Ajoute le repository.", "Ajoute findAvailable."], "<?php\n\n#[ORM\\Entity(repositoryClass: ProductRepository::class)] class Product { #[ORM\\Id] #[ORM\\Column] private ?int $id = null; #[ORM\\Column] private string $name = ''; }\nfinal class ProductRepository extends ServiceEntityRepository { public function findAvailable(): array { return $this->createQueryBuilder('p')->andWhere('p.stock > 0')->getQuery()->getResult(); } }"),
        14 => PhpSymfonyBoss("symfony-module-5-intermediate-boss", "Formulaire Product valide", "Assembler ProductType, handleRequest et contraintes de validation.", "Cree un formulaire ProductType, traite la requete, valide puis persiste.", "<?php\n\n// Product form", "Le formulaire hydrate Product et affiche les erreurs.", 105, ["ProductType", "buildForm", "handleRequest", "isSubmitted", "isValid", "Assert", "persist", "flush"], ["Cree ProductType.", "Traite Request.", "Persiste seulement si valide."], "<?php\n\nfinal class ProductType extends AbstractType { public function buildForm(FormBuilderInterface $builder, array $options): void { $builder->add('name')->add('price'); } }\n#[Assert\\NotBlank] private string $name = '';\n$form = $this->createForm(ProductType::class, $product); $form->handleRequest($request); if ($form->isSubmitted() && $form->isValid()) { $entityManager->persist($product); $entityManager->flush(); }"),
        15 => PhpSymfonyBoss("symfony-module-6-intermediate-boss", "Service ProductCatalogService", "Deplacer la logique produit dans un service Symfony.", "Injecte ProductRepository dans ProductCatalogService puis injecte le service dans le controller.", "<?php\n\n// Service ProductCatalog", "Le controller delegue la logique au service.", 105, ["ProductCatalogService", "__construct", "ProductRepository", "getAvailableProducts", "ProductController", "private ProductCatalogService"], ["Cree le service.", "Injecte le repository.", "Injecte le service dans le controller."], "<?php\n\nfinal readonly class ProductCatalogService { public function __construct(private ProductRepository $products) {} public function getAvailableProducts(): array { return $this->products->findAvailable(); } }\nfinal class ProductController { public function __construct(private ProductCatalogService $catalog) {} }"),
        17 => PhpSymfonyBoss("symfony-module-8-intermediate-boss", "API Product Catalog", "Construire une API JSON Symfony pour Product.", "Expose liste, detail, creation, validation et status codes via JsonResponse.", "<?php\n\n// API Product", "L'API utilise service, validation et codes HTTP explicites.", 115, ["JsonResponse", "#[Route('/api/products", "ProductCatalogService", "Request", "status", "201", "400"], ["Commence par JsonResponse.", "Delegue au service.", "Ajoute les status codes."], "<?php\n\n#[Route('/api/products', methods: ['POST'])]\npublic function create(Request $request, ProductCatalogService $catalog): JsonResponse { $product = $catalog->createProduct('Book', 12); return new JsonResponse(['status' => 'created'], 201); }"),
        _ => null
    };

    private static IntermediateBoss PhpSymfonyBoss(
        string slug,
        string title,
        string objective,
        string instructions,
        string starterCode,
        string expectedResult,
        int xpReward,
        IReadOnlyList<string> requiredSnippets,
        IReadOnlyList<string> hints,
        string solution) =>
        new()
        {
            Slug = slug,
            Title = title,
            Objective = objective,
            Instructions = $"""
            Mise en situation:
            {instructions}

            Competences testees:
            {string.Join(", ", requiredSnippets.Take(6))}

            Travail attendu:
            Complete le starter par etapes. Commence par la structure, puis ajoute la logique metier, puis verifie la sortie ou les valeurs retournees.

            Criteres visibles:
            {string.Join("\n", requiredSnippets.Select(snippet => $"- {snippet}"))}

            Rapport final:
            Apres soumission, le resultat detaille les forces, les faiblesses et les revisions suggerees par competence.
            """,
            StarterCode = starterCode,
            ExpectedResult = expectedResult,
            XpReward = xpReward,
            IsRequiredToUnlockNextModule = true,
            ValidationRules = requiredSnippets.Select((snippet, index) => BossSnippet($"Contient {snippet}", snippet, index + 1)).ToList(),
            Hints = hints.Select((hint, index) => BossHint(hint, index + 1)).ToList(),
            Solution = solution
        };

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
            Instructions = "Cree une fiche de personnage pour Ada. Remplace bonus par 7, rends isReady egal a true, calcule finalScore = baseScore + bonus, puis affiche exactement deux lignes: Ada - score 37 puis Pret: True.",
            StarterCode =
            """
            using System;

            string player = "Ada";
            int baseScore = 30;
            int bonus = 0;
            bool isReady = false;

            // Remplace bonus par 7, isReady par true, calcule finalScore, puis affiche exactement:
            // Ada - score 37
            // Pret: True
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
                BossHint("bonus doit valoir 7 et isReady doit valoir true.", 1),
                BossHint("Ajoute finalScore = baseScore + bonus avant les affichages.", 2),
                BossHint("La premiere ligne doit afficher player puis finalScore. La deuxieme doit afficher isReady.", 3)
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
            Instructions = $"Reussis exactement la consigne ci-dessous. Le code doit satisfaire les criteres de validation et produire le resultat attendu. {lesson.ExercisePrompt}",
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
        bool isBossFinal = false)
    {
        var illustrativeExample = MakeIllustrativeExample(slug, exampleCode, finalCorrection);
        var separatedStarter = SeparateStarterFromCorrection(slug, starterCode, finalCorrection);

        return new()
        {
            Slug = slug,
            Title = title,
            Objective = objective,
            ConceptSummary = conceptSummary,
            CommonMistakes = string.IsNullOrWhiteSpace(commonMistakes)
                ? "Verifier le texte exact attendu, les points-virgules et les noms demandes par l'exercice."
                : commonMistakes,
            Explanation = explanation,
            ExampleCode = illustrativeExample,
            ExercisePrompt = exercisePrompt,
            StarterCode = separatedStarter,
            SuccessFeedback = successFeedback,
            FailureFeedback = failureFeedback,
            FinalCorrection = finalCorrection,
            XpReward = xpReward,
            SortOrder = sortOrder,
            IsBossFinal = isBossFinal,
            IsBossPrerequisite = !isBossFinal,
            Tests = tests
        };
    }

    private static string MakeIllustrativeExample(string slug, string exampleCode, string finalCorrection)
    {
        if (IllustrativeExamples.TryGetValue(slug, out var illustrativeExample))
        {
            return illustrativeExample;
        }

        if (slug.StartsWith("sql-", StringComparison.OrdinalIgnoreCase))
        {
            return SqlIllustrativeExample(slug, exampleCode);
        }

        if (IsTooCloseToCorrection(exampleCode, finalCorrection))
        {
            return IsPhpSymfonySlug(slug)
                ? "<?php\n\n$message = \"Exemple independant\";\necho $message;"
                : "using System;\n\nConsole.WriteLine(\"Exemple independant\");";
        }

        return exampleCode;
    }

    private static string SeparateStarterFromCorrection(string slug, string starterCode, string finalCorrection)
    {
        if (!IsTooCloseToCorrection(starterCode, finalCorrection))
        {
            return starterCode;
        }

        if (slug.StartsWith("sql-", StringComparison.OrdinalIgnoreCase))
        {
            return "-- Ecris ta requete ici.";
        }

        if (IsPhpSymfonySlug(slug))
        {
            return "<?php\n\n// Ecris ta solution ici.";
        }

        return "using System;\n\n// Ecris ta solution ici.";
    }

    private static bool IsPhpSymfonySlug(string slug) =>
        slug.StartsWith("php-", StringComparison.OrdinalIgnoreCase)
        || slug.StartsWith("symfony-", StringComparison.OrdinalIgnoreCase);

    private static bool IsTooCloseToCorrection(string candidate, string correction)
    {
        if (string.IsNullOrWhiteSpace(candidate) || string.IsNullOrWhiteSpace(correction))
        {
            return false;
        }

        var normalizedCandidate = NormalizePedagogicalCode(candidate);
        var normalizedCorrection = NormalizePedagogicalCode(correction);

        if (normalizedCandidate == normalizedCorrection)
        {
            return true;
        }

        return normalizedCorrection.Contains(normalizedCandidate, StringComparison.Ordinal)
            && normalizedCandidate.Length >= Math.Min(80, normalizedCorrection.Length);
    }

    private static string NormalizePedagogicalCode(string code)
    {
        var chars = code.Where(character => !char.IsWhiteSpace(character)).ToArray();
        return new string(chars).ToUpperInvariant();
    }

    private static readonly Dictionary<string, string> IllustrativeExamples = new(StringComparer.OrdinalIgnoreCase)
    {
        ["hello-world"] = "Console.WriteLine(\"Texte de demonstration\");",
        ["variables"] = "string city = \"Lyon\";\nConsole.WriteLine(\"Ville: \" + city);",
        ["types"] = "int count = 4;\nstring label = \"Livres\";\nbool available = true;\ndouble price = 12.5;",
        ["operators"] = "int width = 4;\nint height = 2;\nint area = width * height;\nbool isLarge = area >= 8;",
        ["foundations-checkpoint"] = "string product = \"Lampe\";\nint subtotal = 20 + 5;\nConsole.WriteLine(product + \" prix \" + subtotal);",
        ["if-else"] = "int temperature = 12;\nif (temperature < 15) Console.WriteLine(\"Froid\");\nelse Console.WriteLine(\"Doux\");",
        ["switch"] = "string day = \"lundi\";\nswitch (day) { case \"lundi\": Console.WriteLine(\"Debut\"); break; }",
        ["for-loop"] = "for (int index = 1; index <= 3; index++) Console.WriteLine(\"Etape \" + index);",
        ["while-loop"] = "int attempts = 2;\nwhile (attempts > 0) { Console.WriteLine(attempts); attempts--; }",
        ["foreach-loop"] = "foreach (string color in colors) Console.WriteLine(color);",
        ["flow-checkpoint"] = "for (int page = 1; page <= 2; page++) if (page == 2) Console.WriteLine(\"Fin\");",
        ["create-method"] = "static void PrintTitle() { Console.WriteLine(\"Catalogue\"); }\nPrintTitle();",
        ["method-parameters"] = "static void PrintCity(string city) { Console.WriteLine(city); }\nPrintCity(\"Paris\");",
        ["return-value"] = "static int Multiply(int left, int right) { return left * right; }",
        ["scope"] = "static void PrintStatus() { string status = \"Pret\"; Console.WriteLine(status); }",
        ["overload"] = "static void Show(decimal value) { }\nstatic void Show(DateTime value) { }",
        ["methods-checkpoint"] = "static int Square(int value) { return value * value; }",
        ["classes"] = "class Article { public string Title = \"\"; }",
        ["objects"] = "var article = new Article();",
        ["properties"] = "public decimal Amount { get; set; } = 0;",
        ["constructors"] = "public Order(int id) { Id = id; }",
        ["oop-basics-checkpoint"] = "var ticket = new Ticket(\"A12\", 2);",
        ["inheritance"] = "class InvoiceLine : DocumentLine { }",
        ["polymorphism"] = "public virtual string Render() => \"Base\";\npublic override string Render() => \"Detail\";",
        ["interfaces"] = "interface IExportable { void Export(); }",
        ["access-modifiers"] = "private int cacheSize;\nprotected bool isLoaded;\npublic string Title { get; set; } = \"\";",
        ["advanced-oop-checkpoint"] = "ICommand command = new SaveCommand();\ncommand.Execute();",
        ["arrays"] = "string[] colors = { \"Rouge\", \"Vert\", \"Bleu\" };",
        ["lists"] = "var cities = new List<string>();\ncities.Add(\"Lyon\");",
        ["dictionaries"] = "var seats = new Dictionary<string, int>();\nseats[\"A\"] = 12;",
        ["linq"] = "var recent = years.Where(year => year >= 2020);",
        ["data-structures-checkpoint"] = "var total = prices.Where(price => price > 0).Sum();",
        ["try-catch"] = "try { DateTime.Parse(\"x\"); } catch { Console.WriteLine(\"Date invalide\"); }",
        ["exceptions"] = "throw new InvalidOperationException(\"Operation refusee\");",
        ["nullables"] = "string? title = null;\nConsole.WriteLine(title ?? \"Sans titre\");",
        ["errors-checkpoint"] = "string label = optionalLabel ?? \"Non renseigne\";",
        ["relational-databases"] = "Authors(Id, Name) -> Books(AuthorId)",
        ["entity-framework-core"] = "db.Orders.Add(order);\nawait db.SaveChangesAsync();",
        ["dbcontext"] = "class ShopDbContext : DbContext { public DbSet<Order> Orders => Set<Order>(); }",
        ["crud"] = "Create -> Add, Read -> Query, Update -> Change, Delete -> Remove",
        ["database-checkpoint"] = "DbContext + DbSet + migrations"
    };

    private static string SqlIllustrativeExample(string slug, string fallback) =>
        slug switch
        {
            "sql-relational-database" =>
                "SELECT Title\nFROM Books;",
            "sql-tables-rows-columns" =>
                "SELECT Title, Author, PublishedYear\nFROM Books;",
            "sql-server-data-types" =>
                "SELECT Title, PublishedYear, Price, IsAvailable\nFROM Books;",
            "sql-select" =>
                "SELECT Title, Price\nFROM Books;",
            "sql-where" =>
                "SELECT Title, PublishedYear\nFROM Books\nWHERE PublishedYear >= 2020;",
            "sql-foundations-checkpoint" =>
                "SELECT Title, Price, Stock\nFROM Books\nWHERE IsAvailable = 1 AND Price < 25;",
            "sql-order-by" =>
                "SELECT Title, Rating\nFROM Movies\nORDER BY Rating DESC;",
            "sql-top" =>
                "SELECT TOP 3 Title, Rating\nFROM Movies\nORDER BY Rating DESC;",
            "sql-distinct" =>
                "SELECT DISTINCT Country\nFROM Customers;",
            "sql-like" =>
                "SELECT Title\nFROM Books\nWHERE Title LIKE N'%Guide%';",
            "sql-in" =>
                "SELECT Name\nFROM Employees\nWHERE DepartmentId IN (2, 4);",
            "sql-between" =>
                "SELECT Title, Rating\nFROM Movies\nWHERE Rating BETWEEN 7 AND 9;",
            "sql-is-null" =>
                "SELECT Title, ArchivedAt\nFROM Books\nWHERE ArchivedAt IS NULL;",
            "sql-filtering-checkpoint" =>
                "SELECT Title, Price\nFROM Books\nWHERE Price BETWEEN 10 AND 30 AND Title LIKE N'%SQL%';",
            "sql-count" =>
                "SELECT COUNT(*) AS EmployeeCount\nFROM Employees;",
            "sql-sum" =>
                "SELECT SUM(HoursWorked) AS TotalHours\nFROM Timesheets;",
            "sql-avg" =>
                "SELECT AVG(Rating) AS AverageRating\nFROM Reviews;",
            "sql-min-max" =>
                "SELECT MIN(StartDate) AS FirstStart, MAX(StartDate) AS LastStart\nFROM Projects;",
            "sql-group-by" =>
                "SELECT DepartmentId, COUNT(*) AS EmployeeCount\nFROM Employees\nGROUP BY DepartmentId;",
            "sql-having" =>
                "SELECT DepartmentId, COUNT(*) AS EmployeeCount\nFROM Employees\nGROUP BY DepartmentId\nHAVING COUNT(*) >= 3;",
            "sql-aggregation-checkpoint" =>
                "SELECT DepartmentId, SUM(HoursWorked) AS TotalHours\nFROM Timesheets\nGROUP BY DepartmentId\nHAVING SUM(HoursWorked) > 100;",
            "sql-inner-join" =>
                "SELECT e.Name AS EmployeeName, d.Name AS DepartmentName\nFROM Employees e\nINNER JOIN Departments d ON e.DepartmentId = d.Id;",
            "sql-left-join" =>
                "SELECT d.Name AS DepartmentName, e.Name AS EmployeeName\nFROM Departments d\nLEFT JOIN Employees e ON e.DepartmentId = d.Id\nORDER BY d.Name;",
            "sql-right-join" =>
                "SELECT e.Name AS EmployeeName, d.Name AS DepartmentName\nFROM Employees e\nRIGHT JOIN Departments d ON e.DepartmentId = d.Id\nORDER BY d.Name;",
            "sql-full-outer-join" =>
                "SELECT crm.Email AS CrmEmail, newsletter.Email AS NewsletterEmail\nFROM CrmContacts crm\nFULL OUTER JOIN NewsletterSubscribers newsletter ON crm.Email = newsletter.Email;",
            "sql-table-aliases" =>
                "SELECT e.Name AS EmployeeName, d.Name AS DepartmentName\nFROM Employees e\nINNER JOIN Departments d ON e.DepartmentId = d.Id;",
            "sql-joins-checkpoint" =>
                "SELECT o.Id AS OrderId, c.Name AS CustomerName, o.Total\nFROM Orders o\nINNER JOIN Customers c ON o.CustomerId = c.Id;",
            "sql-insert" =>
                "INSERT INTO Employees (Id, Name, DepartmentId)\nVALUES (10, N'Claire Martin', 2);",
            "sql-update" =>
                "UPDATE Employees\nSET DepartmentId = 3\nWHERE Id = 10;",
            "sql-delete" =>
                "DELETE FROM DraftMessages\nWHERE CreatedAt < '2026-01-01';",
            "sql-simple-transaction" =>
                "BEGIN TRANSACTION;\nUPDATE Accounts SET Balance = Balance - 20 WHERE Id = 1;\nUPDATE Accounts SET Balance = Balance + 20 WHERE Id = 2;\nCOMMIT;",
            "sql-rollback-commit" =>
                "BEGIN TRANSACTION;\nUPDATE Orders SET Status = N'Cancelled' WHERE Id = 42;\nROLLBACK;",
            "sql-modification-checkpoint" =>
                "BEGIN TRANSACTION;\nINSERT INTO AuditLog (Message) VALUES (N'Stock corrige');\nUPDATE Products SET Stock = Stock + 5 WHERE Id = 3;\nCOMMIT;",
            "sql-primary-keys" =>
                "CREATE TABLE Departments (\n    Id int NOT NULL PRIMARY KEY,\n    Name nvarchar(80) NOT NULL\n);",
            "sql-foreign-keys" =>
                "CREATE TABLE Employees (\n    Id int NOT NULL PRIMARY KEY,\n    DepartmentId int NOT NULL,\n    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)\n);",
            "sql-constraints" =>
                "CREATE TABLE Rooms (\n    Code nvarchar(20) NOT NULL UNIQUE,\n    Capacity int NOT NULL CHECK (Capacity > 0)\n);",
            "sql-simple-normalization" =>
                "SELECT b.Title AS BookTitle, a.Name AS AuthorName\nFROM Books b\nINNER JOIN Authors a ON b.AuthorId = a.Id;",
            "sql-relationships" =>
                "SELECT s.Name AS SupplierName, p.Name AS ProductName\nFROM Suppliers s\nINNER JOIN Products p ON p.SupplierId = s.Id;",
            "sql-modeling-checkpoint" =>
                "CREATE TABLE Enrollments (\n    StudentId int NOT NULL,\n    CourseId int NOT NULL,\n    CONSTRAINT PK_Enrollments PRIMARY KEY (StudentId, CourseId)\n);",
            "sql-indexes" =>
                "CREATE INDEX IX_Books_Title\nON Books(Title);\n\nSELECT Title\nFROM Books\nWHERE Title = N'SQL Essentials';",
            "sql-views" =>
                "CREATE VIEW AvailableBooks\nAS\nSELECT Title, Price\nFROM Books\nWHERE IsAvailable = 1;\n\nSELECT Title, Price\nFROM AvailableBooks;",
            "sql-stored-procedures" =>
                "CREATE PROCEDURE GetAvailableBooks\nAS\nSELECT Title\nFROM Books\nWHERE IsAvailable = 1;\n\nEXEC GetAvailableBooks;",
            "sql-functions" =>
                "CREATE FUNCTION dbo.PriceWithTax (@price decimal(10,2))\nRETURNS decimal(10,2)\nAS\nBEGIN\n    RETURN @price * 1.20\nEND;",
            "sql-tsql-variables" =>
                "DECLARE @minimumRating decimal(3,1);\nSET @minimumRating = 4.5;\nSELECT Title\nFROM Books\nWHERE Rating >= @minimumRating;",
            "sql-advanced-checkpoint" =>
                "CREATE VIEW LowStockBooks\nAS\nSELECT Title, Stock\nFROM Books\nWHERE Stock < 5;\n\nSELECT Title, Stock\nFROM LowStockBooks;",
            "sql-complete-schema" =>
                "CREATE TABLE Customers (\n    Id int NOT NULL PRIMARY KEY,\n    Email nvarchar(120) NOT NULL UNIQUE\n);\n\nCREATE TABLE Orders (\n    Id int NOT NULL PRIMARY KEY,\n    CustomerId int NOT NULL,\n    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)\n);",
            "sql-create-project-tables" =>
                "CREATE TABLE Customers (\n    Id int NOT NULL PRIMARY KEY,\n    Name nvarchar(80) NOT NULL\n);\n\nCREATE TABLE Orders (\n    Id int NOT NULL PRIMARY KEY,\n    CustomerId int NOT NULL\n);",
            "sql-seed-project-data" =>
                "INSERT INTO Customers (Id, Name)\nVALUES (1, N'Ada Lovelace');\n\nINSERT INTO Orders (Id, CustomerId)\nVALUES (1, 1);",
            "sql-business-queries" =>
                "SELECT o.Id AS OrderId, c.Name AS CustomerName, p.Name AS ProductName\nFROM Orders o\nINNER JOIN Customers c ON o.CustomerId = c.Id\nINNER JOIN OrderItems oi ON oi.OrderId = o.Id\nINNER JOIN Products p ON oi.ProductId = p.Id;",
            "sql-simple-optimization" =>
                "CREATE INDEX IX_Orders_CustomerId\nON Orders(CustomerId);\n\nSELECT Id\nFROM Orders\nWHERE CustomerId = 1;",
            "sql-project-checkpoint" =>
                "SELECT o.Id AS OrderId, c.Name AS CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal\nFROM Orders o\nINNER JOIN Customers c ON o.CustomerId = c.Id\nINNER JOIN OrderItems oi ON oi.OrderId = o.Id\nGROUP BY o.Id, c.Name;",
            "sql-boss-final-ecommerce" =>
                "SELECT c.Name AS CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSpent\nFROM Customers c\nINNER JOIN Orders o ON o.CustomerId = c.Id\nINNER JOIN OrderItems oi ON oi.OrderId = o.Id\nGROUP BY c.Name\nHAVING SUM(oi.Quantity * oi.UnitPrice) >= 50;",
            _ when slug.StartsWith("sql-", StringComparison.OrdinalIgnoreCase) =>
                fallback,
            _ => fallback
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
