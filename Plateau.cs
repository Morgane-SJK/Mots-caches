using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_9_10
{
    public class Plateau
    {
        //Attributs
        private Dé[] tableauDés;
        private char[,] tableauFaceSupérieure;

        //Constructeur
        public Plateau(Dé[] tableauDés)
        {
            this.tableauDés = tableauDés;
            this.tableauFaceSupérieure = new char[4,4];

            // Remplir le tableau de 4x4 avec uniquement les faces supérieures
            this.tableauFaceSupérieure[0, 0] = tableauDés[0].Lettre;
            this.tableauFaceSupérieure[0, 1] = tableauDés[1].Lettre;
            this.tableauFaceSupérieure[0, 2] = tableauDés[2].Lettre;
            this.tableauFaceSupérieure[0, 3] = tableauDés[3].Lettre;
            this.tableauFaceSupérieure[1, 0] = tableauDés[4].Lettre;
            this.tableauFaceSupérieure[1, 1] = tableauDés[5].Lettre;
            this.tableauFaceSupérieure[1, 2] = tableauDés[6].Lettre;
            this.tableauFaceSupérieure[1, 3] = tableauDés[7].Lettre;
            this.tableauFaceSupérieure[2, 0] = tableauDés[8].Lettre;
            this.tableauFaceSupérieure[2, 1] = tableauDés[9].Lettre;
            this.tableauFaceSupérieure[2, 2] = tableauDés[10].Lettre;
            this.tableauFaceSupérieure[2, 3] = tableauDés[11].Lettre;
            this.tableauFaceSupérieure[3, 0] = tableauDés[12].Lettre;
            this.tableauFaceSupérieure[3, 1] = tableauDés[13].Lettre;
            this.tableauFaceSupérieure[3, 2] = tableauDés[14].Lettre;
            this.tableauFaceSupérieure[3, 3] = tableauDés[15].Lettre;
        }
        //Propriétés
        public char[,] TableauFaceSupérieure
        {
            get { return tableauFaceSupérieure; }
            set { tableauFaceSupérieure = value; }
        }

        //Méthodes
        // Tester de façon récursive si le mot est présent sur le plateau
        public bool Test_plateau(char[] mot)
        {
            bool result = false;

            // Effectuer la recherche récursivement pour toutes les positions possibles de la première lettre du mot
            List<int[]> listeCoordonnees = coordonneesLettreSurPlateau(tableauFaceSupérieure, mot[0]);
            if (listeCoordonnees == null)
            {
                //Console.WriteLine("La première lettre du mot " + mot[0] + " n'est pas présente sur le plateau");
            }
            else
            {
                foreach (int[] coordonnee in listeCoordonnees)
                {
                    //Console.WriteLine("La première lettre du mot " + mot[0] + " est présente sur le plateau en " + coordonnee[0] + " et " + coordonnee[1]);
                    result = Verifier_Adjacence(mot, 0, coordonnee[0], coordonnee[1]);
                    //Console.WriteLine("On est de retour de l'appel à Verifier_Adjacence() avec result="+result);

                    // Arrêter la recherche dès que le mot a été trouvé
                    if (result)
                    {
                        //Console.WriteLine("Arrêter la recherche dès que le mot a été trouvé");
                        break;
                    }
                    else
                    {
                        //Console.WriteLine("Continuer la recherche car le mot n'a pas encore été trouvé");
                    }
                }
            }

            return result;
        }

        // Retourne une liste de string composée par lettre + indice ligne + indice colonne
        // Ne pas retourner les lettres temporairement effacées pour éviter d'utiliser plusieurs fois la même lettre
        // Exemple : ("M12","L23") indique que 2 lettres sont contigues :
        // la lettre 'M' de coordonnées 1,2 et la lettre 'L' de coordonnée 2,3
        public List<string> LettresAutour(char[,] tableauFaceSupérieure, int i, int j)
        {
            List<string> lettresAutour = new List<string>();

            switch (i)
            {
                // On est sur la première ligne
                case 0:
                    switch (j)
                    {
                        // On est sur la première colonne
                        case 0:
                            if (tableauFaceSupérieure[i, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j + 1] + Convert.ToString(i) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i + 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j + 1] + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i + 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j] + Convert.ToString(i + 1) + Convert.ToString(j));
                            break;

                        // On est sur la dernière colonne
                        case 3:
                            if (tableauFaceSupérieure[i, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j - 1] + Convert.ToString(i) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i + 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j - 1] + Convert.ToString(i + 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i + 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j] + Convert.ToString(i + 1) + Convert.ToString(j));
                            break;

                        // On est sur une des colonnes intérieures
                        default:
                            if (tableauFaceSupérieure[i, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j - 1] + Convert.ToString(i) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i + 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j - 1] + Convert.ToString(i + 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i + 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j] + Convert.ToString(i + 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i + 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j + 1] + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j + 1] + Convert.ToString(i) + Convert.ToString(j + 1));
                            break;

                    }
                    break;

                // On est sur la dernière ligne
                case 3:
                    switch(j)
                    {
                        // On est sur la première colonne
                        case 0:
                            if (tableauFaceSupérieure[i - 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j] + Convert.ToString(i - 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i - 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j + 1] + Convert.ToString(i - 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j + 1] + Convert.ToString(i) + Convert.ToString(j + 1));
                            break;

                        // On est sur la dernière colonne
                        case 3:
                            if (tableauFaceSupérieure[i, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j - 1] + Convert.ToString(i) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i - 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j - 1] + Convert.ToString(i - 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i - 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j] + Convert.ToString(i - 1) + Convert.ToString(j));
                            break;

                        // On est sur une des colonnes intérieures
                        default:
                            if (tableauFaceSupérieure[i, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j - 1] + Convert.ToString(i) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i - 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j - 1] + Convert.ToString(i - 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i - 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j] + Convert.ToString(i - 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i - 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j + 1] + Convert.ToString(i - 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j + 1] + Convert.ToString(i) + Convert.ToString(j + 1));
                            break;
                    }
                    break;

                default:
                    // On est sur une des lignes intérieures
                    switch (j)
                    {
                        // On est sur une des lignes intérieures et sur la première colonne
                        case 0:
                            if (tableauFaceSupérieure[i - 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j] + Convert.ToString(i - 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i - 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j + 1] + Convert.ToString(i - 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j + 1] + Convert.ToString(i) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i + 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j + 1] + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i + 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j] + Convert.ToString(i + 1) + Convert.ToString(j));
                            break;

                        // On est sur une des lignes intérieures et sur la dernière colonne
                        case 3:
                            if (tableauFaceSupérieure[i - 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j] + Convert.ToString(i - 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i - 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j - 1] + Convert.ToString(i - 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j - 1] + Convert.ToString(i) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i + 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j - 1] + Convert.ToString(i + 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i + 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j] + Convert.ToString(i + 1) + Convert.ToString(j));
                            break;

                        // On est sur une des lignes et colonnes intérieures
                        default:
                            if (tableauFaceSupérieure[i - 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j - 1] + Convert.ToString(i - 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i - 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j] + Convert.ToString(i - 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i - 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i - 1, j + 1] + Convert.ToString(i - 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j + 1] + Convert.ToString(i) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i + 1, j + 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j + 1] + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                            if (tableauFaceSupérieure[i + 1, j] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j] + Convert.ToString(i + 1) + Convert.ToString(j));
                            if (tableauFaceSupérieure[i + 1, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i + 1, j - 1] + Convert.ToString(i + 1) + Convert.ToString(j - 1));
                            if (tableauFaceSupérieure[i, j - 1] != ' ')
                                lettresAutour.Add(tableauFaceSupérieure[i, j - 1] + Convert.ToString(i) + Convert.ToString(j - 1));
                            break;
                    }
                    break;
            }

            //Console.WriteLine("LettresAutour() a retourné une liste de " + lettresAutour.Count);
            return lettresAutour;
        }

        // Pour toutes les lettres composant le mot saisi,
        // vérifier de façon récursive si chacune des lettres de ce mot est bien contigue avec la lettre suivante
        // Dès que la lettre est utilisée, l'effacer du plateau pour ne pas l'utiliser plusieurs fois
        // Arrêter la recherche dès qu'une erreur est détectée
        // mot : mot recherché
        // indice : indice de la lettre courante recherchée
        // abscisse : abscisse (x) de cette lettre sur le plateau
        // ordonnee : ordonnee (y) de cette lettre sur le plateau
        public bool Verifier_Adjacence(char[] mot, int indice, int abscisse, int ordonnee)
        {
            char sauvegardeLettre;
            bool result = false;
            bool stopRechercheRecursive = false;

            //Console.WriteLine("Verifier_Adjacence du mot pour la lettre " + mot[indice] + " située à l'indice=" + indice + " abscisse=" + abscisse + " ordonnee=" + ordonnee);

            // Supprimer temporairement la lettre du plateau pour ne pas l'utiliser 2 fois
            //Console.WriteLine("Effacer la lettre du plateau " + tableauFaceSupérieure[abscisse, ordonnee]);
            sauvegardeLettre = tableauFaceSupérieure[abscisse, ordonnee];
            tableauFaceSupérieure[abscisse, ordonnee] = ' ';
            //afficheTableau(tableauFaceSupérieure);

            // Effectuer le contrôle pour toutes les lettres adjacentes possibles de cette lettre sur le plateau
            List<string> lettresAutour = null;
            //Console.WriteLine("Recherche des lettres contigues à cette lettre " + mot[indice] + " de coordonnée " + abscisse + " " + ordonnee);
            lettresAutour = LettresAutour(tableauFaceSupérieure, abscisse, ordonnee);

            if (lettresAutour == null)
            {
                // La lettre n'a plus de lettre adjacente, on doit stopper la recherche récursive en cours
                //Console.WriteLine("La lettre " + mot[indice] + " n'a plus de lettre adjacente");
                stopRechercheRecursive = true;
            }
            else
            {
                // La lettre a des lettres adjacentes
                //Console.WriteLine("La lettre " + mot[indice] + " a " + lettresAutour.Count + " lettres adjacentes");
                foreach (string lettreAutour in lettresAutour)
                {
                    // Continuer la recherche si on n'a pas encore trouvé le mot
                    if (result == false)
                    {
                        int abscisseSuivant = Convert.ToInt32(lettreAutour.Substring(1, 1));
                        int ordonneeSuivant = Convert.ToInt32(lettreAutour.Substring(2, 1));

                        // Est-on arrivé à la dernière lettre du mot ?
                        if (mot.Length > (indice + 1))
                        {
                            // Est ce que cette lettre adjacente correspond à la lettre suivante du mot ?
                            // Effectuer la comparaison en convertissant les lettres en majuscule
                            if (Char.ToUpper(lettreAutour[0]) == Char.ToUpper(mot[indice + 1]))
                            {
                                //Console.WriteLine("Cette lettre adjacente " + lettreAutour[0] + " correspond bien à la lettre suivante du mot qui est " + mot[indice + 1]);
                                // Est ce que le mot a été trouvé ?
                                if (mot.Length == (indice + 2))
                                {
                                    //Console.WriteLine("LE MOT A ETE TROUVE");
                                    result = true;
                                    stopRechercheRecursive = true;
                                    break;  // Sortir du foreach
                                }

                                // Continuer la recherche si on n'a pas encore trouvé le mot 
                                if ((stopRechercheRecursive == false) && (result == false))
                                {
                                    //Console.WriteLine("Nouvel appel a Verifier_Adjacence() car stopRechercheRecursive=" + stopRechercheRecursive + " et result=" + result);
                                    // Vérifier de façon récursive la présence des lettres suivantes du mot
                                    result = Verifier_Adjacence(mot, indice + 1, abscisseSuivant, ordonneeSuivant);
                                }
                            }
                            else
                            {
                                //Console.WriteLine("Cette lettre adjacente " + lettreAutour[0] + " ne correspond pas à la lettre suivante du mot");
                            }
                        }
                        else
                        {
                            //Console.WriteLine("On arrête la recherche récursive courante car on est arrivé à la dernière lettre du mot");
                            stopRechercheRecursive = true;
                        }
                    }
                }
            }

            // Remettre la lettre précédement effacée du plateau pour ne pas l'utiliser 2 fois
            //Console.WriteLine("Remettre la lettre " + sauvegardeLettre + " précédement effacée");
            tableauFaceSupérieure[abscisse, ordonnee] = sauvegardeLettre;
            //afficheTableau(tableauFaceSupérieure);

            //Console.WriteLine("Verifier_Adjacence du mot pour la lettre " + mot[indice] + " située à l'indice=" + indice + " abscisse=" + abscisse + " ordonnee=" + ordonnee + " retourne " + result);
            return result;
        }

        // Recherche de la liste des coordonnées de la lettre sur le plateau
        public List<int[]> coordonneesLettreSurPlateau(char[,] tableauFaceSupérieure, char lettreATrouver)
        {
            List<int[]> listeCoordonnees = null;
            int[] coordonnees = null;

            for (int i = 0; i < tableauFaceSupérieure.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFaceSupérieure.GetLength(1); j++)
                {
                    if (char.ToUpper(tableauFaceSupérieure[i, j]) == char.ToUpper(lettreATrouver))
                    {
                        // La lettre à trouver est bien présente sur le plateau
                        if (listeCoordonnees == null)
                        {
                            listeCoordonnees = new List<int[]>();
                        }
                        coordonnees = new int[2];
                        coordonnees[0] = i;
                        coordonnees[1] = j;
                        listeCoordonnees.Add(coordonnees);
                        //Console.WriteLine("La lettre " + lettreATrouver + " est bien présente sur le plateau en " + coordonnees[0] + " et " + coordonnees[1]);
                    }
                }
            }

            return listeCoordonnees;
        }
        
        // Affiche le  Plateau
        public void affichePlateau()
        {
            string result = "";

            for (int i = 0; i < tableauFaceSupérieure.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFaceSupérieure.GetLength(1); j++)
                {
                    result = result + tableauFaceSupérieure[i, j] + " ";
                }
                result = result + "\n";
            }
            Console.WriteLine(result);
        }

        // Affiche les propriétés du Plateau
        public override string ToString() 
        {
            string result = "";

            // Affiche Plateau
            affichePlateau();

            // Afficher tableau de dés
            for (int i = 0; i < tableauDés.Length; i++)
            {
               result = result + "Dé " + i + " = " + tableauDés[i] + "\n";
            }
            return result;
        }

        // Test unitaire permettant d'afficher la liste de toutes les lettres contigues de chaque lettre du plateau
        public string Test_LettresAutour()
        {
            string result = "";
            for (int i = 0; i < tableauFaceSupérieure.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFaceSupérieure.GetLength(1); j++)
                {

                    result = result + "\nLettres autour de " + i + " " + j + " " + tableauFaceSupérieure[i, j] + " = ";
                    for (int k = 0; k < LettresAutour(tableauFaceSupérieure, i, j).Count(); k++)
                    {
                        result = result + LettresAutour(tableauFaceSupérieure, i, j).ElementAt(k);
                    }
                }
                result = result + "\n";
            }
            return result;
        }

        // Fonction inutilisée
        // Affiche le tableau passé en paramètre
        // Sert uniquement pour la mise au point
/*
        public void afficheTableau(char[,] tableau)
        {
            string result = "";

            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                {
                    result = result + tableau[i, j] + " ";
                }
                result = result + "\n";
            }
            Console.WriteLine(result);
        }
*/

        // Fonction inutilisée
        // Clone le tableau des faces supérieures
        /*
         public char[,] CloneTableauFaceSupérieure(char[,] tableauFaceSupérieure)
               {
                   
                   char[,] tableauFaceSupérieureResult = new char[4, 4];

                   // Remplir le tableau de 4x4 avec uniquement les faces supérieures
                   tableauFaceSupérieureResult[0, 0] = tableauFaceSupérieure[0, 0];
                   tableauFaceSupérieureResult[0, 1] = tableauFaceSupérieure[0, 1];
                   tableauFaceSupérieureResult[0, 2] = tableauFaceSupérieure[0, 2];
                   tableauFaceSupérieureResult[0, 3] = tableauFaceSupérieure[0, 3];
                   tableauFaceSupérieureResult[1, 0] = tableauFaceSupérieure[1, 0];
                   tableauFaceSupérieureResult[1, 1] = tableauFaceSupérieure[1, 1];
                   tableauFaceSupérieureResult[1, 2] = tableauFaceSupérieure[1, 2];
                   tableauFaceSupérieureResult[1, 3] = tableauFaceSupérieure[1, 3];
                   tableauFaceSupérieureResult[2, 0] = tableauFaceSupérieure[2, 0];
                   tableauFaceSupérieureResult[2, 1] = tableauFaceSupérieure[2, 1];
                   tableauFaceSupérieureResult[2, 2] = tableauFaceSupérieure[2, 2];
                   tableauFaceSupérieureResult[2, 3] = tableauFaceSupérieure[2, 3];
                   tableauFaceSupérieureResult[3, 0] = tableauFaceSupérieure[3, 0];
                   tableauFaceSupérieureResult[3, 1] = tableauFaceSupérieure[3, 1];
                   tableauFaceSupérieureResult[3, 2] = tableauFaceSupérieure[3, 2];
                   tableauFaceSupérieureResult[3, 3] = tableauFaceSupérieure[3, 3];

                   return tableauFaceSupérieureResult;
               }
       */
    }
}
