using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD_9_10
{
    public class Program
    {
        static void Main(string[] args)
        {

            int dureePartie = 6;    // Durée du jeu = 6 minutes
            int dureeJoueur = 1;     // Durée de la partie de chaque joueur = 1 minute
            int max = 16;
            Dé[] listeDés = null;
            Plateau plateau = null;
            Dictionnaire dictionnaire = null;
            Joueur joueur1, joueur2, joueurActuel;

            // Lire le fichier des mots possibles
            dictionnaire = new Dictionnaire("MotsPossibles.txt");
            
            // Lire le fichier des lettres à placer sur les dés
            TableauDés tableauDés = new TableauDés("Des.txt");

            // Création des 2 joueurs
            joueur1 = new Joueur("Joueur1");
            joueur2 = new Joueur("Joueur2");
            joueurActuel = null;

            // Ne pas permettre la saisie de noms de joueurs vides
            do
            {
                Console.WriteLine("Quel est le nom du joueur 1 ?");
                joueur1.Nom = Console.ReadLine();
            } while (joueur1.Nom.Trim() == "");
            do
            {
                Console.WriteLine("Quel est le nom du joueur 2 ?");
                joueur2.Nom = Console.ReadLine();
            } while (joueur2.Nom.Trim() == "");

            // Date de démarrage du jeu
            DateTime demarrageJeu = DateTime.Now;
            while (DateTime.Now.Subtract(demarrageJeu).Minutes < dureePartie)
            {
                if (joueurActuel == null)
                {
                    joueurActuel = joueur1; // Le premier joueur à jouer est le joueur1
                }
                else if (joueurActuel.Nom == joueur1.Nom)
                {
                    joueurActuel = joueur2; // C'est au tour de joueur2 de jouer
                }
                else
                {
                    joueurActuel = joueur1; // C'est au tour de joueur1 de jouer
                }

                // Date de démarrage du joueur
                DateTime demarrageJoueur = DateTime.Now;

                Console.WriteLine("\nC'est au tour de " + joueurActuel.Nom + " de jouer\n");

                //Effacer la liste des mots du joueur
                //joueurActuel.Motstrouves = null;

                // Créer l'ensemble des dés
                listeDés = new Dé[max];
                for (int i = 0; i < max; i++)
                {
                    listeDés[i] = new Dé(tableauDés.GetLine(i));
                    //Console.WriteLine(listeDés[i]);
                }

                plateau = new Plateau(listeDés);
                plateau.affichePlateau();
                //Console.WriteLine(plateau.ToString());

                // Chaque joueur joue 60 secondes
                while (DateTime.Now.Subtract(demarrageJoueur).Minutes < dureeJoueur)
                {
                    Console.WriteLine("Saisissez un nouveau mot trouvé");
                    string motSaisi = Console.ReadLine();
                    if (Eligible(dictionnaire, plateau, motSaisi, joueurActuel))
                    {
                        joueurActuel.Add_Mot(motSaisi);
                        joueurActuel.Score = joueurActuel.CalculScore(motSaisi);
                        Console.WriteLine(joueurActuel.ToString());
                    }
                }
            }

            // Affichage des scores et du nom du gagnant
            Console.WriteLine("\nFIN DE LA PARTIE");
            Console.WriteLine("Score de " + joueur1.Nom + " : " + joueur1.Score);
            Console.WriteLine("Score de " + joueur2.Nom + " : " + joueur2.Score);
            if (joueur1.Score < joueur2.Score)
            {
                Console.WriteLine(joueur2.Nom + " a gagné");
            }
            else if (joueur2.Score < joueur1.Score)
            {
                Console.WriteLine(joueur1.Nom + " a gagné");
            }
            else
            {
                Console.WriteLine(joueur1.Nom + " et " + joueur2.Nom + " ont le même score");
            }
            Console.ReadLine(); // Permet de garder le score affiché à l'écran
        }

        static bool Eligible(Dictionnaire dictionnaire, Plateau plateau,string mot, Joueur joueurActuel) //Est ce que le mot appartient au dictionnaire, fait plus de 3 lettres, et vérifie les conditions d'adjacence ?
        {
            bool eligible = false;

            char[] motTableau = new char[mot.Length];
            // Transformer le string en tableau de char
            for (int i=0;i<mot.Length;i++)
            {
                motTableau[i] = mot[i];
            }

            //Console.WriteLine("dictionnaire.RechDichoRecursif(mot)=" + dictionnaire.RechDichoRecursif(mot));
            //Console.WriteLine("mot.Length=" + mot.Length);
            //Console.WriteLine("plateau.Test_plateau(motTableau)=" + plateau.Test_plateau(motTableau));
            //Console.WriteLine("joueurActuel.Contain(mot)=" + joueurActuel.Contain(mot));
            if (dictionnaire.RechDichoRecursif(mot) && (mot.Length >= 3)&&(plateau.Test_plateau(motTableau))&&!(joueurActuel.Contain(mot))) 
            {
                // Le mot est eligible s'il respecte les 4 conditions suivantes :
                // Mot présent dans le dictionnaire
                // Mot d'au moins 3 lettres
                // Construction possible à partir des lettres contigues du plateau
                // Mot pas encore trouvé par le joueur
                eligible = true;
            }
            return eligible;
        }

    }
}
