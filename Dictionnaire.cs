using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD_9_10
{
    public class Dictionnaire
    {
        //Attribut: un tableau de listes (chaque liste correspondant à un nombre de lettres par mot)
        private List<string> [] motsPossibles;

        //Constructeur
        public Dictionnaire(string filename)
        {
            motsPossibles = new List<string>[100];
            ReadFile(filename);
        }

        //Méthodes
        // Lit le fichier des mots possibles
        public void ReadFile(String filename)
        {
            StreamReader flux = null;
            string line;
            List<string> listeMots;

            try
            {
                flux = new StreamReader(filename);
                int i = 0;

                while ((line = flux.ReadLine()) != null)
                {
                    int n;
                    bool isNumeric = int.TryParse(line, out n);

                    if (! isNumeric) //ne rentre dans le tableau que des listes de mots
                    {
                        listeMots = new List<string>(); //une nouvelle liste de mots contenant le même nombre de lettres est crée 

                        string[] ensembleMots = line.Split(' '); //création d'un tableau de mots
                        for (int j = 0; j < ensembleMots.Length; j++)
                        {
                            listeMots.Add(ensembleMots[j]); //insertion de chaque mot dans la liste
                        }
                        motsPossibles[i] = listeMots; //on insère la liste dans une ligne du tableau

                        i++;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (flux != null) { flux.Close(); }
            }
        }

        // Est ce que le mot saisi est bien un mot du dictionnaire ?
        public bool RechDichoRecursif(string mot)
        {
            mot = mot.ToUpper();
            bool result = false;
            int longueurMot = 0;
            longueurMot = mot.Length;
            for (int i = 0; i < motsPossibles.Length; i++)
            {
                // Est ce que cette liste contient des mots de la même longueur que le mot recherché ?
                if ((motsPossibles[i] != null) && (motsPossibles[i].First().Length == longueurMot))
                {
                    // Savoir si le mot recherché fait partie de la liste des mots connus ayant cette longueur
                    if (motsPossibles[i].Contains(mot))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        // Affiche les propriétés du dictionnaire
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < motsPossibles.Length; i++)
            {
                if (motsPossibles[i] != null)
                {
                    result = result + "\nListe de mots de " + motsPossibles[i].First().Length+ " lettres :\n";

                    foreach (string value in motsPossibles[i])
                    {
                        result = result + " " + value;
                    }
                }
            }

            return result;
        }
    }
}
