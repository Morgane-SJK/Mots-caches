using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_9_10
{
    public class Joueur
    {
        //Attributs
        private string nom;
        private int score;
        private List<string> motstrouves;
        //Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            this.score = 0;
            this.motstrouves = null;
        }
        //Propriétés
        public string Nom
        {
            get{ return nom; }
            set{ nom = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public List<string> Motstrouves
        {
            get { return motstrouves; }
            set { motstrouves = value; }
        }

        //Méthodes
        // Est ce que le mot saisi appartient déjà aux mots trouvés ?
        // Effectuer les comparaisons en passant les 2 mots en majuscule
        public bool Contain(string mot) 
        {
            bool trouve = false;
            if (motstrouves != null)
            {
                for (int i = 0; i < (motstrouves.Count()); i++)
                {
                    if (string.Compare(mot.ToUpper(), motstrouves[i].ToUpper()) == 0) // les 2 mots convertis en majuscule sont les mêmes
                    {
                        trouve = true;
                        break;
                    }
                }
            }
            return trouve;
        }

        // Ajoute le mot dans la liste des mots trouvés s'il n'est pas déjà présent dans la liste
        public void Add_Mot(string mot)
        {
            //if (! Contain(mot))
            //{
                if (motstrouves == null)
                {
                    motstrouves = new List<string>();
                }
                motstrouves.Add(mot);
            //}
        }

        // Calcul des scores en fonction de la longueur du mot
        public int CalculScore(string mot)
        {
             switch (mot.Length)
             {
                case 3:
                    score = score + 2;
                    break;
                case 4:
                    score = score + 3;
                    break;
                case 5:
                    score = score + 4;
                    break;
                case 6:
                    score = score + 5;
                    break;
                default:
                    score = score + 11;
                    break;
             }
             return score;
        }

        // Affiche les propriétés du joueur
        public override string ToString()        {
            string result="";
            if (motstrouves==null)
            {
                result="Le score de " + nom + " est de 0.";
            }
            else
            {
                result = "Le score de " + nom + " est de " + score
                + " grâce aux mots cités suivants :\n";
                for (int i = 0; i < motstrouves.Count(); i++)
                {
                    result = result + motstrouves[i] + " ";
                }
            }
            return result;
        }
    }
}
