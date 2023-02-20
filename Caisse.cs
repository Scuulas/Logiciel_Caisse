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
                TicketButton.Enabled = !TicketButton.Enabled;
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
            this.bdd.ImportDBfromFile(open_DB.FileName);                    // Importe dans le contenu du fichier .csv dans l'objet bdd
            VegetableComboBox.Items.Clear();                                // Clear de la ComboBox de l'article
            VegetableComboBox.Items.AddRange(bdd.GetDB().Keys.ToArray());   // MaJ des articles dans la ComboBox
        }

        // Ajoute un article dans le panier
        private void AddArticle_Click(object sender, EventArgs e)
        {
            // Si l'article existe dans la BDD et que le poids > 0
            if (this.bdd.IsInDB(VegetableComboBox.Text) && WeightUpDown.Value > 0)
            {
                this.panier.AddArticle(VegetableComboBox.Text, bdd.GetPrice(VegetableComboBox.Text), Convert.ToDouble(WeightUpDown.Value), 1); // Ajoute l'article dans le panier
                this.InterfaceUpdate();                                                                                                     // MaJ de l'interface
            }
        }

        // Ouvre le fichier ticket.txt avec Bloc-notes (notepad.exe)
        private void TicketButton_Click(object sender, EventArgs e)
        {
            if (this.panier.GetIndex() > 0) this.panier.ShowTicketFile();   // Si le panier n'est pas vide => Genere puis ouvre le ticket de caisse
        }

        // Supprime un article du panier
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // Si un numero est selectionne dans la ComboBox
            if (ArticleNumberComboBox.SelectedIndex != -1)
            {
                // Si le numero est dans la range des articles du panier
                if (panier.IsInBasketRange(Convert.ToInt32(ArticleNumberComboBox.Text) - 1))
                {
                    this.panier.DeleteArticle(Convert.ToInt32(ArticleNumberComboBox.Text) - 1); // Supprime l'article
                    this.InterfaceUpdate();                                                     // MaJ de l'interface
                }
            }
        }

        // Vide le panier et met a jour l'affichage
        private void DeleteBasket_Click(object sender, EventArgs e)
        {
            this.panier = new Panier();     // Ecrase le panier par un nouveau
            this.InterfaceUpdate();         // MaJ de l'interface
        }

        // Cree un ticket.txt, ouvre une fenetre Ticket, vide le panier et met a jour l'affichage
        private void PayButton_Click(object sender, EventArgs e)
        {
            // Si le panier n'est pas vide
            if (panier.GetIndex() > 0)
            {
                Ticket ticketWindow = new Ticket(panier.CreateTicketText());    // Cree une classe Ticket avec en parametre le texte du ticket de caisse
                ticketWindow.StartPosition = FormStartPosition.Manual;          // Parametre pour choisir les coordonnees de lancement de la fenetre manuellement
                Point location = this.Location;                                 // Coordonnees de la fenetre Caisse
                ticketWindow.Location = location;                               // Changement de coordonnees de la fenetre Ticket
                ticketWindow.ShowDialog();                                      // Lance la fenetre Ticket

                this.panier = new Panier();                                     // Efface le panier
                this.InterfaceUpdate();                                         // MaJ de l'interface
            }
        }
    }
}