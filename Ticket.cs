using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Documents;

namespace Logiciel_Caisse
{
    public partial class Ticket : Form
    {
        // Attribut
        private readonly string ticket;

        // Constructeur
        public Ticket(string ticket)
        {
            InitializeComponent();
            ticketTextBox.Text = ticket;    // Affiche le texte du ticket de caisse dans la TextBox
            this.ticket = ticket;           // Attribut le ticket de caisse a l'attribut ticket
        }

        // Retourne un FlowDocument
        private FlowDocument CreateFlowDocument()
        {
            // Creation d'un FlowDocument pour l'impression
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(this.ticket)));
            return doc;
        }

        // Click sur le bouton "Imprimer"
        private void PrintButton_Click(object sender, EventArgs e)
        {
            System.Windows.Controls.PrintDialog printDlg = new System.Windows.Controls.PrintDialog();   // Creation d'un PrintDialog
            FlowDocument doc = CreateFlowDocument();                                                    // On cree un FlowDocument
            doc.Name = "Ticket_de_caisse";                                                              // On appelle le FlowDocument "Ticket_de_caisse"
            IDocumentPaginatorSource idpSource = doc;                                                   // On cree un IDocumentPaginatorSource a partir du FlowDocument

            // Affiche la fenetre pour lancer l'impression
            // Si la fenetre est fermee, impression annulee => return | Sinon, si "OK" appuye, continue dans la fonction
            if (!(bool)printDlg.ShowDialog())
            {
                Debug.WriteLine("Impression annulée");
                return;
            }

            // Appelle la methode PrintDocument pour envoyer le document a l'imprimante, lancement de l'impression...
            try
            {
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Impression du ticket de caisse");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
