namespace MastermindGraphic
{
    partial class MastermindGraphic
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorChoisepanel = new System.Windows.Forms.Panel();
            this.checkColorspanel = new System.Windows.Forms.Panel();
            this.panelColorsbtn = new System.Windows.Forms.Panel();
            this.buttonValitador = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colorChoisepanel
            // 
            this.colorChoisepanel.Location = new System.Drawing.Point(348, 2);
            this.colorChoisepanel.Name = "colorChoisepanel";
            this.colorChoisepanel.Size = new System.Drawing.Size(378, 534);
            this.colorChoisepanel.TabIndex = 0;
            // 
            // checkColorspanel
            // 
            this.checkColorspanel.Location = new System.Drawing.Point(821, 2);
            this.checkColorspanel.Name = "checkColorspanel";
            this.checkColorspanel.Size = new System.Drawing.Size(295, 534);
            this.checkColorspanel.TabIndex = 1;
            // 
            // panelColorsbtn
            // 
            this.panelColorsbtn.Location = new System.Drawing.Point(348, 559);
            this.panelColorsbtn.Name = "panelColorsbtn";
            this.panelColorsbtn.Size = new System.Drawing.Size(767, 56);
            this.panelColorsbtn.TabIndex = 2;
            // 
            // buttonValitador
            // 
            this.buttonValitador.Location = new System.Drawing.Point(1143, 559);
            this.buttonValitador.Name = "buttonValitador";
            this.buttonValitador.Size = new System.Drawing.Size(87, 55);
            this.buttonValitador.TabIndex = 3;
            this.buttonValitador.Text = "GO";
            this.buttonValitador.UseVisualStyleBackColor = true;
            this.buttonValitador.Click += new System.EventHandler(this.buttonValitador_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(229, 559);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(86, 55);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "UNDO";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(229, 481);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(85, 55);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(1145, 481);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(85, 55);
            this.resetButton.TabIndex = 6;
            this.resetButton.Text = "RESET";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // MastermindGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1257, 630);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.buttonValitador);
            this.Controls.Add(this.panelColorsbtn);
            this.Controls.Add(this.checkColorspanel);
            this.Controls.Add(this.colorChoisepanel);
            this.Name = "MastermindGraphic";
            this.Text = "Mastermind";
            this.Load += new System.EventHandler(this.MastermindGraphic_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel colorChoisepanel;
        private System.Windows.Forms.Panel checkColorspanel;
        private System.Windows.Forms.Panel panelColorsbtn;
        private System.Windows.Forms.Button buttonValitador;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button resetButton;
    }
}

