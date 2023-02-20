using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Logiciel_Caisse
{
    // Structure de donnees pour les articles contenus dans le panier (= Dictionary "articles")
    struct Article
    {
        public string name;
        public double price_per_kg;
        public double weight;
        public double price;
        public double quantity;

        public Article(string name, double price_per_kg, double weight, double price, double quantity)
        {
            this.name = name;
            this.price_per_kg = price_per_kg;
            this.weight = weight;
            this.price = price;
            this.quantity = quantity;
        }
    }


    internal class Panier
    {
        // Attributs
        private double montant;                                                         // Montant total du panier
        private readonly Dictionary<int, Article> articles;                             // Structure pour contenir tous les articles dans le panier
        private int index;                                                              // Index: nombre d'articles dans le panier
        private readonly FileInfo ticketFileToExport = new FileInfo(@"ticket.txt");     // Creation de la variable FileInfo pour pouvoir ecrire dans un fichier

        // Constructeur
        public Panier()
        {
            this.montant = 0;
            this.index = 0;
            this.articles = new Dictionary<int, Article>();
        }

        // Getters des attributs montant et index
        public double GetMontant() { return this.montant;  }
        public int GetIndex() { return this.index; }

        // Ajout d'un article dans le panier (le Dictionary articles), ajoute le prix au montant total et incremente this.index
        public void AddArticle(string name, double price_per_kg, double weight, double quantity)
        {
            this.articles.Add(index, new Article(name, price_per_kg, Math.Round(weight, 2), Math.Round(weight * price_per_kg, 2), quantity));
            this.montant += Math.Round(weight * price_per_kg, 2);
            this.index++;
            this.CreateTicketFile();        // MaJ du fichier ticket.txt
        }

        // Retourne une liste contenant tous les numeros des indexes d'un panier (pour le ComboBox)
        public string[] GetBasketRange()
        {
            string[] res = new string[this.index];
            for (int i = 0; i < this.index; i++)
            {
                res[i] = Convert.ToString(i + 1);
            }

            return res;
        }

        // Retourne vrai si l'index existe dans le panier (0 <= index <= nb d'article dans le panier)
        public bool IsInBasketRange(int index)
        {
            return (0 <= index && index <= this.index);
        }

        // Met a jour l'objet PanierListView qui montre le contenu du panier
        public void UpdateDisplay(ListView PanierListView)
        {
            PanierListView.Items.Clear();                           // On vide la ListView

            // Boucle dans les articles du panier
            for (int i = 0; i < this.index; i++)
            {
                // Verification en theorie inutile mais en-cas ¯\_(ツ)_/¯
                if (articles.ContainsKey(i))
                {
                    ListViewItem row = new ListViewItem();          // On instancie une ligne du tableau avec ListViewItem
                    row.Text = (i+1).ToString();                    // Row prend sa position dans le Dictionary du panier
                    row.SubItems.Add(articles[i].name);             // Row prend le nom de l'article
                    row.SubItems.Add(articles[i].price + " €");     // Row prend le prix de l'article
                    PanierListView.Items.Add(row);                  // On ajoute la ligne (row) au tableau
                }
                else { break; }
            }
        }

        // Retourne le texte du ticket de caisse
        public string CreateTicketText()
        {
            // La variable string ticket qui va contenir le texte du ticket de caisse

            // Header :
            string ticket = $"--------------------------------{System.Environment.NewLine}" +
                            $"Primeur de la côte{System.Environment.NewLine}" +
                            $"Avenue de beaurivage{System.Environment.NewLine}" +
                            $"Kuopio Finland{System.Environment.NewLine}" +
                            $"le {DateTime.Now.Date.ToString().Split(' ')[0]}{System.Environment.NewLine}" +
                            $"à {DateTime.Now.ToString("HH:mm")}{System.Environment.NewLine}" +
                            $"{System.Environment.NewLine}";

            // Boucle dans les articles du panier
            for (int i = 0; i < this.index; i++)
            {
                // Verification en theorie inutile mais en-cas ¯\_(ツ)_/¯
                if (articles.ContainsKey(i))
                {
                    // Concatene les informations des articles du panier
                    ticket += $"{articles[i].name} - {articles[i].weight} kg : {articles[i].price} €{System.Environment.NewLine}";
                }
                else { break; }
            }

            // Footer :
            ticket += $"{System.Environment.NewLine}" +
                      $"TOTAL TTC : {Math.Round(montant, 2)} €{System.Environment.NewLine}" +
                      $"TVA : {Math.Round(montant * 0.2, 2)} €{System.Environment.NewLine}" +
                      $"{System.Environment.NewLine}" +
                      $"Merci de votre visite et...{System.Environment.NewLine}" +
                      $"... Gardez la pêche !{System.Environment.NewLine}" +
                      $"--------------------------------";

            return ticket;
        }

        // Cree le fichier ticket.txt avec le texte du ticket de caisse
        public void CreateTicketFile()
        {
            string ticket = CreateTicketText();                                                         // On stocke le texte du ticket de caisse 

            try
            {
                StreamWriter writer = new StreamWriter(ticketFileToExport.Open(FileMode.Truncate));     // On ouvre le stream pour ecrire sur le fichier ticket.txt
                writer.Write(ticket);                                                                   // On ecrite le texte dans le fichier
                writer.Close();                                                                         // On ferme le stream
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }

        }

        // Ouvre le fichier ticket.txt avec le Bloc-notes (notepad.exe)
        public void ShowTicketFile()
        {
            Process.Start("notepad.exe", ticketFileToExport.FullName);
        }

        // Optionnel: permet de supprimer un article (par son index)
        public void DeleteArticle(int index)
        {
            this.montant -= articles[index].price;      // On retire le prix de l'article du montant
            this.articles.Remove(index);                // On retire l'article dans le Dictionary

            // Le but avec cette boucle est de combler le vide de l'article supprime en decalant tous les articles qui etaient apres celui supprime
            for(int i = index; i < this.index; i++)
            {
                // Si l'index suivant n'etait pas vide
                if(articles.ContainsKey(i + 1)) {
                    articles[i] = articles[i + 1];      // On copie le i+1 dans le i
                } else {
                    articles.Remove(i);                 // Sinon (dernier index qui n'a rien apres lui / le dernier a avoir ete decale), on le supprime
                    break;
                }
            }

            this.index--;
            this.CreateTicketFile();                    // MaJ du fichier ticket.txt
        }

        // Fonction de debug: print le contenu du panier dans la console de Debug
        public void DebugPrintPanier()
        {
            foreach(KeyValuePair<int, Article> article in articles)
            {
                Debug.WriteLine($"PANIER => Article: {article.Key}, Name: {article.Value.name}, Price: {article.Value.price}");
            }
        }
    }
}
