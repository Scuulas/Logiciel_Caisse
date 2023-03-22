using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Logiciel_Caisse
{
    internal class Pair
    {
        public int amount;  
        public double price;

        public Pair(int amount, double price)
        {
            this.amount = amount;
            this.price = price;
        }
    }
    internal class DataBase
    {
        // Attribut DB contenant les noms et les prix des articles de la BDD
        private readonly Dictionary<string, Pair> DB;

        // Constructeur
        public DataBase()
        {
            this.DB = new Dictionary<string, Pair>();
        }

        // Retourne le dictionnaire DB
        public Dictionary<string, Pair> GetDB()
        {
            return this.DB;
        }

        // Retourne le prix d'un article en renseignant le nom de celui-ci
        public double GetPrice(string vegetable)
        {
            double price = 0;

            // Dans un try/catch en cas si l'article n'existe pas
            try
            {
                price = this.DB[vegetable].price;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return price;
        }

        // Retourne vrai si l'article existe dans DB
        public bool IsInDB(string article)
        {
            return this.DB.ContainsKey(article);
        }

        // Importe les donnees d'un fichier .csv dans DB
        public void ImportDBfromFile(string fileToImport)
        {
            try
            {
                StreamReader reader = new StreamReader(fileToImport);   // On ouvre un stream pour lire le fichier

                string line = " ; ";

                // Tant que le lecteur n'est pas a la fin du stream / fin du fichier
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();                           // On stocke ligne par ligne ce que le lecteur lit
                    string[] vegetableAndPrice = line.Split(';');       // On parse la ligne par des ; pour obtenir une liste avec le nom et le prix

                    // Si l'article n'existe pas deja dans la BDD, on l'ajoute
                    if ((!IsInDB(vegetableAndPrice[0])) && (Convert.ToInt32(vegetableAndPrice[2])>0))
                    { 
                        this.DB.Add(vegetableAndPrice[0], new Pair(Convert.ToInt32(vegetableAndPrice[2]),Convert.ToDouble(vegetableAndPrice[1])));
                    }
                }

                reader.Close();                                         // On ferme le stream du lecteur
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        // Fonction de debug: print le contenu de DB dans la console de Debug
        public void DebugPrintDB()
        {
            foreach (KeyValuePair<string, Pair> article in DB)
            {
                Debug.WriteLine($"DB => Article: {article.Key}, Price: {article.Value}");
            }
        }
    }
}
