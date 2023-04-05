using System.Windows.Forms;

namespace Logiciel_Caisse
{
    partial class Ticket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ticket));
            this.ticketLabel = new System.Windows.Forms.Label();
            this.ticketTextBox = new System.Windows.Forms.TextBox();
            this.printButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ticketLabel
            // 
            this.ticketLabel.AutoSize = true;
            this.ticketLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ticketLabel.Location = new System.Drawing.Point(33, 34);
            this.ticketLabel.Name = "ticketLabel";
            this.ticketLabel.Size = new System.Drawing.Size(69, 20);
            this.ticketLabel.TabIndex = 1;
            this.ticketLabel.Text = "Receipt :";
            // 
            // ticketTextBox
            // 
            this.ticketTextBox.HideSelection = false;
            this.ticketTextBox.Location = new System.Drawing.Point(37, 78);
            this.ticketTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ticketTextBox.Multiline = true;
            this.ticketTextBox.Name = "ticketTextBox";
            this.ticketTextBox.ReadOnly = true;
            this.ticketTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ticketTextBox.Size = new System.Drawing.Size(304, 382);
            this.ticketTextBox.TabIndex = 2;
            this.ticketTextBox.TabStop = false;
            // 
            // printButton
            // 
            this.printButton.BackColor = System.Drawing.Color.White;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Location = new System.Drawing.Point(219, 28);
            this.printButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(124, 31);
            this.printButton.TabIndex = 3;
            this.printButton.Text = "🖨 Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // Ticket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 484);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.ticketTextBox);
            this.Controls.Add(this.ticketLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(394, 531);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(394, 531);
            this.Name = "Ticket";
            this.Text = "Ticket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label ticketLabel;
        private TextBox ticketTextBox;
        private Button printButton;
    }
}