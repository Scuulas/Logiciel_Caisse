using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Logiciel_Caisse
{
    public partial class Caisse : Form
    {
        // Attributs
        private readonly DataBase bdd;
        private Panier panier;
        private String pathDB;

        // Constructeur
        public Caisse()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;    // Coordonnees de lancement de Caisse au centre de l'ecran

            this.bdd = new DataBase();                              // Instancie un objet bdd avec la classe DataBase
            this.panier = new Panier();                             // Instancie un objet panier avec la classe Panier
        }

        // Permet d'activer tous les boutons desactives par defaut
        private void EnableButtons()
        {
            // Condition pour ne pas les désactiver en reouvrant une autre BDD (par ex: en ouvrant plusieurs fois un fichier .csv)
            if (bdd.GetDB().Count == 0)
            {
                AddArticle.Enabled = !AddArticle.Enabled;                                           
                DeleteButton.Enabled = !DeleteButton.Enabled;
                DeleteBasket.Enabled = !DeleteBasket.Enabled;
                PayButton.Enabled = !PayButton.Enabled;
                VegetableComboBox.Enabled = !VegetableComboBox.Enabled;
                WeightUpDown.Enabled = !WeightUpDown.Enabled;
                ArticleNumberComboBox.Enabled = !ArticleNumberComboBox.Enabled;
            }
        }

        // Met a jour les differents affichages de l'interface
        private void InterfaceUpdate()
        {
            MontantTextBox.Text = Convert.ToString(Math.Round(this.panier.GetMontant(), 2));    // MaJ du Montant
            panier.UpdateDisplay(PanierListView);                                               // MaJ du Panier
            ArticleNumberComboBox.Items.Clear();                                                // Clear la ComboBox
            ArticleNumberComboBox.Items.AddRange(panier.GetBasketRange());                      // MaJ de la ComboBox avec les nouveaux numeros
            ArticleNumberComboBox.Text = "";                                                    // Clear la ComboBox
            VegetableComboBox.Text = "";                                                        // Clear de l'article
            WeightUpDown.Value = 0;                                                             // Clear du poids
        }

        // Ouvre la fenetre de dialogue pour ouvrir le fichier .csv
        private void ChooseDB_Click(object sender, EventArgs e)
        {
            open_DB.ShowDialog();
        }

        // Une fois le fichier .csv ouvert, active les boutons, importe le contenu du fichier
        private void OpenDB_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.EnableButtons();                                           // Active les boutons
            this.pathDB = open_DB.FileName;
            this.bdd.ImportDBfromFile(pathDB);                    // Importe dans le contenu du fichier .csv dans l'objet bdd
            VegetableComboBox.Items.Clear();                                // Clear de la ComboBox de l'article
            VegetableComboBox.Items.AddRange(bdd.GetDB().Keys.ToArray());   // MaJ des articles dans la ComboBox
        }

        // Ajoute un article dans le panier
        private void AddArticle_Click(object sender, EventArgs e)
        {
            // Si l'article existe dans la BDD et que le poids > 0
            if (this.bdd.IsInDB(VegetableComboBox.Text) && WeightUpDown.Value > 0)
            {
                if ((this.bdd.GetAmount(VegetableComboBox.Text) - WeightUpDown.Value) >= 0)
                {
                    this.panier.AddArticle(VegetableComboBox.Text, bdd.GetPrice(VegetableComboBox.Text), Convert.ToInt32(WeightUpDown.Value)); // Ajoute l'article dans le panier
                    this.bdd.ChangeAmountAsSum(VegetableComboBox.Text,-Convert.ToInt32(WeightUpDown.Value));
                    this.InterfaceUpdate();
                }
                else
                {
                    string message = "Not enough articles left!";
                    string title = "Error";
                    MessageBox.Show(message, title);
                }
            }
        }

        // Supprime un article du panier
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // Si un numero est selectionne dans la ComboBox
            if (ArticleNumberComboBox.SelectedIndex != -1)
            {
                int selectedIndex = Convert.ToInt32(ArticleNumberComboBox.Text) - 1;
                // Si le numero est dans la range des articles du panier
                if (panier.IsInBasketRange(selectedIndex))
                {
                    this.bdd.ChangeAmountAsSum(this.panier.GetArticleFromBasketIndex(selectedIndex),this.panier.GetAmount(selectedIndex));
                    this.panier.DeleteArticle(Convert.ToInt32(ArticleNumberComboBox.Text) - 1); // Supprime l'article
                    this.InterfaceUpdate();                                                     // MaJ de l'interface
                }
            }
        }

        // Vide le panier et met a jour l'affichage
        private void DeleteBasket_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.panier.GetIndex(); i++) 
            {
                this.bdd.ChangeAmountAsSum(this.panier.GetArticleFromBasketIndex(i), this.panier.GetAmount(i));
            }
            this.panier = new Panier();     // Ecrase le panier par un nouveau
            this.InterfaceUpdate();         // MaJ de l'interface
        }

        // Cree un ticket.txt, ouvre une fenetre Ticket, vide le panier et met a jour l'affichage
        private void PayButton_Click(object sender, EventArgs e)
        {
            if (MontantTextBox.Text != "")
            {
                double sum = Convert.ToDouble(MontantTextBox.Text);
                this.panier.CreateTotalSalesFile();
                this.panier.CreateReceiptFile(Convert.ToDouble(sum));
                // Si le panier n'est pas vide
                if (panier.GetIndex() > 0)
                {
                    bdd.ExportToFile(pathDB);
                    bdd.ImportDBfromFile(pathDB);
                    VegetableComboBox.Items.Clear();                                // Clear de la ComboBox de l'article
                    VegetableComboBox.Items.AddRange(bdd.GetDB().Keys.ToArray());   // MaJ des articles dans la ComboBox
                    Ticket ticketWindow = new Ticket(panier.CreateReceiptText(sum));    // Cree une classe Ticket avec en parametre le texte du ticket de caisse
                    ticketWindow.StartPosition = FormStartPosition.Manual;          // Parametre pour choisir les coordonnees de lancement de la fenetre manuellement
                    Point location = this.Location;                                 // Coordonnees de la fenetre Caisse
                    ticketWindow.Location = location;                               // Changement de coordonnees de la fenetre Ticket
                    ticketWindow.ShowDialog();

                    this.panier = new Panier();                                     // Efface le panier
                    this.InterfaceUpdate();                                         // MaJ de l'interface
                }
            }
            else
            {
                string message = "Type a total Price";
                string title = "Error!";
                MessageBox.Show(message, title);
            }
        }

        private void MontantTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!IsDoubleRealNumber(MontantTextBox.Text) && MontantTextBox.Text != ""){
                MontantTextBox.Text = this.panier.GetMontant().ToString();
            }
        }

        private static bool IsDoubleRealNumber(string valueToTest)
        {
            if (double.TryParse(valueToTest, out double d) && !Double.IsNaN(d) && !Double.IsInfinity(d))
            {
                return true;
            }

            return false;
        }
    }
}