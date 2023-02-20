using System.Windows.Forms;

namespace Logiciel_Caisse
{
    partial class Caisse
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caisse));
            this.ChooseDB = new System.Windows.Forms.Button();
            this.open_DB = new System.Windows.Forms.OpenFileDialog();
            this.vegetableLabel = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.VegetableComboBox = new System.Windows.Forms.ComboBox();
            this.AddArticle = new System.Windows.Forms.Button();
            this.WeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.panierLabel = new System.Windows.Forms.Label();
            this.montantLabel = new System.Windows.Forms.Label();
            this.MontantTextBox = new System.Windows.Forms.TextBox();
            this.TicketButton = new System.Windows.Forms.Button();
            this.PayButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ArticleNumberComboBox = new System.Windows.Forms.ComboBox();
            this.PanierListView = new System.Windows.Forms.ListView();
            this.n = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.article = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DeleteBasket = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WeightUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ChooseDB
            // 
            this.ChooseDB.Location = new System.Drawing.Point(35, 54);
            this.ChooseDB.Name = "ChooseDB";
            this.ChooseDB.Size = new System.Drawing.Size(230, 25);
            this.ChooseDB.TabIndex = 0;
            this.ChooseDB.Text = "Ouvrir la base de données";
            this.ChooseDB.UseVisualStyleBackColor = true;
            this.ChooseDB.Click += new System.EventHandler(this.ChooseDB_Click);
            // 
            // open_DB
            // 
            this.open_DB.DefaultExt = "csv";
            this.open_DB.FileName = "open_DB";
            this.open_DB.Filter = "CSV files (*.csv)|*.csv";
            this.open_DB.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenDB_FileOk);
            // 
            // vegetableLabel
            // 
            this.vegetableLabel.AutoSize = true;
            this.vegetableLabel.Location = new System.Drawing.Point(35, 109);
            this.vegetableLabel.Name = "vegetableLabel";
            this.vegetableLabel.Size = new System.Drawing.Size(90, 13);
            this.vegetableLabel.TabIndex = 2;
            this.vegetableLabel.Text = "Choisir un article :";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(35, 155);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(60, 13);
            this.weightLabel.TabIndex = 3;
            this.weightLabel.Text = "Poids (kg) :";
            // 
            // VegetableComboBox
            // 
            this.VegetableComboBox.Enabled = false;
            this.VegetableComboBox.FormattingEnabled = true;
            this.VegetableComboBox.Location = new System.Drawing.Point(161, 106);
            this.VegetableComboBox.Name = "VegetableComboBox";
            this.VegetableComboBox.Size = new System.Drawing.Size(104, 21);
            this.VegetableComboBox.TabIndex = 4;
            // 
            // AddArticle
            // 
            this.AddArticle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.AddArticle.Enabled = false;
            this.AddArticle.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddArticle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AddArticle.Location = new System.Drawing.Point(35, 196);
            this.AddArticle.Name = "AddArticle";
            this.AddArticle.Size = new System.Drawing.Size(230, 25);
            this.AddArticle.TabIndex = 6;
            this.AddArticle.Text = "Ajouter au panier";
            this.AddArticle.UseVisualStyleBackColor = false;
            this.AddArticle.Click += new System.EventHandler(this.AddArticle_Click);
            // 
            // WeightUpDown
            // 
            this.WeightUpDown.DecimalPlaces = 3;
            this.WeightUpDown.Enabled = false;
            this.WeightUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.WeightUpDown.Location = new System.Drawing.Point(161, 153);
            this.WeightUpDown.Name = "WeightUpDown";
            this.WeightUpDown.Size = new System.Drawing.Size(104, 20);
            this.WeightUpDown.TabIndex = 7;
            this.WeightUpDown.ThousandsSeparator = true;
            // 
            // panierLabel
            // 
            this.panierLabel.AutoSize = true;
            this.panierLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.panierLabel.Location = new System.Drawing.Point(287, 29);
            this.panierLabel.Name = "panierLabel";
            this.panierLabel.Size = new System.Drawing.Size(54, 15);
            this.panierLabel.TabIndex = 8;
            this.panierLabel.Text = "PANIER :";
            // 
            // montantLabel
            // 
            this.montantLabel.AutoSize = true;
            this.montantLabel.Location = new System.Drawing.Point(363, 227);
            this.montantLabel.Name = "montantLabel";
            this.montantLabel.Size = new System.Drawing.Size(136, 13);
            this.montantLabel.TabIndex = 11;
            this.montantLabel.Text = "MONTANT =                    €";
            // 
            // MontantTextBox
            // 
            this.MontantTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MontantTextBox.Location = new System.Drawing.Point(436, 227);
            this.MontantTextBox.Name = "MontantTextBox";
            this.MontantTextBox.ReadOnly = true;
            this.MontantTextBox.Size = new System.Drawing.Size(45, 13);
            this.MontantTextBox.TabIndex = 12;
            this.MontantTextBox.Text = "0";
            this.MontantTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TicketButton
            // 
            this.TicketButton.Enabled = false;
            this.TicketButton.Location = new System.Drawing.Point(287, 253);
            this.TicketButton.Name = "TicketButton";
            this.TicketButton.Size = new System.Drawing.Size(97, 32);
            this.TicketButton.TabIndex = 16;
            this.TicketButton.Text = "Ouvrir le ticket";
            this.TicketButton.UseVisualStyleBackColor = true;
            this.TicketButton.Click += new System.EventHandler(this.TicketButton_Click);
            // 
            // PayButton
            // 
            this.PayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.PayButton.Enabled = false;
            this.PayButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PayButton.Location = new System.Drawing.Point(405, 253);
            this.PayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PayButton.Name = "PayButton";
            this.PayButton.Size = new System.Drawing.Size(95, 32);
            this.PayButton.TabIndex = 17;
            this.PayButton.Text = "Encaisser";
            this.PayButton.UseVisualStyleBackColor = false;
            this.PayButton.Click += new System.EventHandler(this.PayButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.Tomato;
            this.DeleteButton.Enabled = false;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Location = new System.Drawing.Point(138, 257);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(81, 25);
            this.DeleteButton.TabIndex = 18;
            this.DeleteButton.Text = "Retirer l\'article";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ArticleNumberComboBox
            // 
            this.ArticleNumberComboBox.Enabled = false;
            this.ArticleNumberComboBox.FormattingEnabled = true;
            this.ArticleNumberComboBox.Location = new System.Drawing.Point(225, 260);
            this.ArticleNumberComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ArticleNumberComboBox.Name = "ArticleNumberComboBox";
            this.ArticleNumberComboBox.Size = new System.Drawing.Size(40, 21);
            this.ArticleNumberComboBox.TabIndex = 19;
            // 
            // PanierListView
            // 
            this.PanierListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.n,
            this.article,
            this.price});
            this.PanierListView.HideSelection = false;
            this.PanierListView.Location = new System.Drawing.Point(287, 54);
            this.PanierListView.Name = "PanierListView";
            this.PanierListView.Size = new System.Drawing.Size(213, 166);
            this.PanierListView.TabIndex = 22;
            this.PanierListView.UseCompatibleStateImageBehavior = false;
            this.PanierListView.View = System.Windows.Forms.View.Details;
            // 
            // n
            // 
            this.n.Text = "n°";
            this.n.Width = 26;
            // 
            // article
            // 
            this.article.Text = "Article";
            this.article.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.article.Width = 112;
            // 
            // price
            // 
            this.price.Text = "Prix";
            this.price.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.price.Width = 71;
            // 
            // DeleteBasket
            // 
            this.DeleteBasket.BackColor = System.Drawing.Color.Tomato;
            this.DeleteBasket.Enabled = false;
            this.DeleteBasket.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteBasket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteBasket.Location = new System.Drawing.Point(35, 253);
            this.DeleteBasket.Name = "DeleteBasket";
            this.DeleteBasket.Size = new System.Drawing.Size(90, 32);
            this.DeleteBasket.TabIndex = 23;
            this.DeleteBasket.Text = "Vider le panier";
            this.DeleteBasket.UseVisualStyleBackColor = false;
            this.DeleteBasket.Click += new System.EventHandler(this.DeleteBasket_Click);
            // 
            // Caisse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.Controls.Add(this.DeleteBasket);
            this.Controls.Add(this.PanierListView);
            this.Controls.Add(this.ArticleNumberComboBox);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.PayButton);
            this.Controls.Add(this.TicketButton);
            this.Controls.Add(this.MontantTextBox);
            this.Controls.Add(this.montantLabel);
            this.Controls.Add(this.panierLabel);
            this.Controls.Add(this.WeightUpDown);
            this.Controls.Add(this.AddArticle);
            this.Controls.Add(this.VegetableComboBox);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.vegetableLabel);
            this.Controls.Add(this.ChooseDB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 350);
            this.Name = "Caisse";
            this.Text = "Caisse";
            ((System.ComponentModel.ISupportInitialize)(this.WeightUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ChooseDB;
        private OpenFileDialog open_DB;
        private Label vegetableLabel;
        private Label weightLabel;
        private ComboBox VegetableComboBox;
        private Button AddArticle;
        private NumericUpDown WeightUpDown;
        private Label panierLabel;
        private Label montantLabel;
        private TextBox MontantTextBox;
        private Button TicketButton;
        private Button PayButton;
        private Button DeleteButton;
        private ComboBox ArticleNumberComboBox;
        private ListView PanierListView;
        private ColumnHeader n;
        private ColumnHeader article;
        private ColumnHeader price;
        private Button DeleteBasket;
    }
}