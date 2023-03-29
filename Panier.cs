using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Logiciel_Caisse
{
    // Structure de donnees pour les articles contenus dans le panier (= Dictionary "articles")
    struct Article
    {
        public string name;
        public int amount;
        public double price;
        public double totalprice;

        public Article(string name ,double price, int amount, double totalprice)
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.totalprice = totalprice;
        }
    }


    internal class Panier
    {
        // Attributs
        private double montant;                                                         // Montant total du panier
        private readonly Dictionary<int, Article> articles;                             // Structure pour contenir tous les articles dans le panier
        private int index;                                                              // Index: nombre d'articles dans le panier
        private FileInfo ticketFileToExport;                                            // Creation de la variable FileInfo pour pouvoir ecrire dans un fichier
        private FileInfo receiptFileToExport;
        private Dictionary<string, int> articlesToExport;
        // Constructeur
        public Panier()
        {
            DateTime now = DateTime.Today;
            this.montant = 0;
            this.index = 0;
            this.articles = new Dictionary<int, Article>();
            this.articlesToExport = new Dictionary<string, int>();
            this.ticketFileToExport = new FileInfo(now.ToString("yyyy") + "\\" + now.ToString("MM") + "\\" + now.ToString("dd") + ".csv");
            if (!this.ticketFileToExport.Exists)
            {
                Directory.CreateDirectory(now.ToString("yyyy") + "\\" + now.ToString("MM") + "\\" + now.ToString("dd") + "_Receipts");    
            }
            else
            {
                try
                {
                    StreamReader reader = new StreamReader(this.ticketFileToExport.ToString());
                    while (!reader.EndOfStream)
                    {
                        String line = reader.ReadLine();
                        string[] temp = line.Split(';');
                        this.articlesToExport.Add(temp[0], Convert.ToInt32(temp[1]));
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
            int receiptCounter = Directory.GetFiles(now.ToString("yyyy") + "\\" + now.ToString("MM") + "\\" + now.ToString("dd") + "_Receipts", "*", SearchOption.AllDirectories).Length;
            this.receiptFileToExport = new FileInfo(now.ToString("yyyy") + "\\" + now.ToString("MM") + "\\" + now.ToString("dd") + "_Receipts" + "\\" + receiptCounter.ToString() + ".txt");
        }

        // Getters des attributs montant et index
        public double GetMontant() { 
            double sum = 0;
            foreach(var article in this.articles)
            {
                sum += article.Value.totalprice;
            }
            Math.Round(sum, 2);
            this.montant = sum;
            return this.montant;  
        }
        public int GetIndex() { return this.index; }

        // Ajout d'un article dans le panier (le Dictionary articles), ajoute le prix au montant total et incremente this.index
        public void AddArticle(string name, double price, int amount)
        {
            this.articles.Add(index, new Article(name, price, amount, Math.Round(amount * price, 2)));
            this.montant += Math.Round(amount * price, 2);
            this.index++;
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
        public void UpdateDisplay(System.Windows.Forms.ListView PanierListView)
        {
            PanierListView.Items.Clear();                           // On vide la ListView

            // Boucle dans les articles du panier
            for (int i = 0; i < this.index; i++)
            {
                // Verification en theorie inutile mais en-cas ¯\_(ツ)_/¯
                if (articles.ContainsKey(i))
                {
                    System.Windows.Forms.ListViewItem row = new System.Windows.Forms.ListViewItem();          // On instancie une ligne du tableau avec ListViewItem
                    row.Text = (i+1).ToString();                    // Row prend sa position dans le Dictionary du panier
                    row.SubItems.Add(articles[i].name);             // Row prend le nom de l'article
                    row.SubItems.Add(articles[i].totalprice + " €");     // Row prend le prix de l'article
                    row.SubItems.Add(articles[i].amount.ToString());
                    PanierListView.Items.Add(row);                  // On ajoute la ligne (row) au tableau
                }
                else { break; }
            }
        }

        // Retourne le texte du ticket de caisse
        public string CreateFileText()
        {
            // La variable string ticket qui va contenir le texte du ticket de caisse

            // Header :
            string ticket = "";

            // Boucle dans les articles du panier
            for (int i = 0; i < this.index; i++)
            {
                // Verification en theorie inutile mais en-cas ¯\_(ツ)_/¯
                if (articlesToExport.ContainsKey(articles[i].name))
                {
                    // Concatene les informations des articles du panier
                    int amount = articlesToExport[articles[i].name] + articles[i].amount;
                    ticket += $"{articles[i].name};{amount}{System.Environment.NewLine}";
                    articlesToExport.Remove(articles[i].name);
                }
                else
                {
                    ticket += $"{articles[i].name};{articles[i].amount}{System.Environment.NewLine}";
                }
            }
            foreach( var article in articlesToExport)
            {
                ticket += $"{article.Key};{article.Value}{System.Environment.NewLine}";
            }

            return ticket;
        }

        // Cree le fichier ticket.txt avec le texte du ticket de caisse
        public void CreateTotalSalesFile()
        {
            string ticket = CreateFileText();                                                         // On stocke le texte du ticket de caisse 

            try
            {
                StreamWriter writer = new StreamWriter(ticketFileToExport.OpenWrite());     // On ouvre le stream pour ecrire sur le fichier ticket.txt
                writer.Write(ticket);                                                                   // On ecrite le texte dans le fichier
                writer.Close();                                                                         // On ferme le stream
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }

        }

        public string CreateReceiptText(double sum)
        {
            // La variable string ticket qui va contenir le texte du ticket de caisse
            // Header :
            string ticket = $"TOTAL : {Math.Round(sum, 2)} €{System.Environment.NewLine}" +
                            $"--------------------------------{System.Environment.NewLine}" +
                            $"Receipt{System.Environment.NewLine}" +
                            $"{System.Environment.NewLine}" +
                            $"from {DateTime.Now.Date.ToString().Split(' ')[0]}{System.Environment.NewLine}" +
                            $"at {DateTime.Now.ToString("HH:mm")}{System.Environment.NewLine}" +
                            $"For buying this articles:{System.Environment.NewLine}" +
                            $"{System.Environment.NewLine}";

            // Boucle dans les articles du panier
            for (int i = 0; i < this.index; i++)
            {
                // Verification en theorie inutile mais en-cas ¯\_(ツ)_/¯
                if (articles.ContainsKey(i))
                {
                    // Concatene les informations des articles du panier
                    ticket += $"{articles[i].name} - {articles[i].amount} pcs : {articles[i].price} €{System.Environment.NewLine}";
                }
                else { break; }
            }

            // Footer :
            ticket += $"{System.Environment.NewLine}" +
                      $"Thanks for visiting{System.Environment.NewLine}" +
                      $"--------------------------------";

            return ticket;
        }

        public void CreateReceiptFile(double sum)
        {
            string ticket = CreateReceiptText(sum);
            

            try
            {
                StreamWriter writer = new StreamWriter(receiptFileToExport.OpenWrite());     // On ouvre le stream pour ecrire sur le fichier ticket.txt
                writer.Write(ticket);                                                                   // On ecrite le texte dans le fichier
                writer.Close();                                                                         // On ferme le stream
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
        }


        // Optionnel: permet de supprimer un article (par son index)
        public void DeleteArticle(int index)
        {
            this.montant -= articles[index].totalprice;      // On retire le prix de l'article du montant
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
        }

        // Fonction de debug: print le contenu du panier dans la console de Debug
        public void DebugPrintPanier()
        {
            foreach(KeyValuePair<int, Article> article in articles)
            {
                Debug.WriteLine($"PANIER => Article: {article.Key}, Name: {article.Value.name}, Price: {article.Value.totalprice}");
            }
        }
    }
}
