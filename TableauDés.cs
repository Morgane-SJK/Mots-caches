using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD_9_10
{
    public class TableauDés
    {
        //Attribut
        private string[] lignes = null;

        //Constructeur
        public TableauDés(string filename)
        {
            lignes = new string[16];
            ReadFile(filename);
        }

        //Méthodes
        // Retourne la ligne demandée
        public string GetLine(int i)
        {
            if (i < 16)
                return lignes[i];
            else
                return "";
        }

        // Lit le fichier
        public void ReadFile(String filename)
        {
            StreamReader flux = null;
            string line;
            try
            {
                flux = new StreamReader(filename);
                int i = 0;

                while ((line = flux.ReadLine()) != null)
                {
                    line = line.Replace(";", "");
                    lignes[i] = line;
                    //Console.WriteLine(lignes[i]);

                    i++;
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
    }
}
