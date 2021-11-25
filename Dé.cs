using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_9_10
{
    public class Dé
    {
        //Attributs
        private string ensemble;
        private char lettre;

        //Constructeur
        // Pour améliorer les tirages aléatoires en les basant sur les millisecondes de l'heure actuelle qui augmentent
        // Attendre 13 millisecondes entre chaque tirage
        public Dé(string ensemble)
        {
            this.ensemble = ensemble;
            System.Threading.Thread.Sleep(13);  // Attendre 13 millisecondes entre chaque tirage
            Random random = new Random(DateTime.Now.Millisecond);   //Permet d'améliorer les tirages aléatoires en les basant sur les millisecondes qui augmentent
            Lance(random);  // Initialisation de l'attribut lettre
        }

        //Propriétés
        public string Ensemble
        {
            get { return ensemble; }
            set { ensemble = value; }
        }
        public char Lettre
        {
            get { return lettre; }
            set { lettre = value; }
        }

        //Méthodes
        // Génération aléatoire
        public void Lance( Random r)
        {
            int indice;
            indice = r.Next(6);
            this.lettre = ensemble.ElementAt(indice); //extrait la lettre positionnée à l'indice généré aléatoirement
        }

        // Affiche les propriétés du dé
        public override string ToString()
        {
            string result = "";

            result = "lettre de la face supérieure: " + lettre + "\nensemble des lettres = ";
            for (int i = 0; i < ensemble.Length; i++)
            {
                result = result + ensemble[i] + " ";
            }
            return result;
        }
    }
}
