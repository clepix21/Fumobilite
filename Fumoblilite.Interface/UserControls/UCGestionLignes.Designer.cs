using System.Windows.Forms;
using System.Drawing;

namespace Fumoblilite.Interface.UserControls
{
    partial class UCGestionLignes
    {
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.FlowLayoutPanel flpLignes;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblCouleur;
        private System.Windows.Forms.TextBox txtCouleur;
        private System.Windows.Forms.Button btnCouleur;
        private System.Windows.Forms.CheckBox chkEstActif;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tabInfos;
        private System.Windows.Forms.TabPage tabArrets;
        private System.Windows.Forms.FlowLayoutPanel flpArrets;
        private System.Windows.Forms.GroupBox grpArret;
        private System.Windows.Forms.Label lblArret;
        private System.Windows.Forms.ComboBox cboArret;
        private System.Windows.Forms.Label lblOrdre;
        private System.Windows.Forms.NumericUpDown nudOrdre;
        private System.Windows.Forms.Label lblTempsArret;
        private System.Windows.Forms.NumericUpDown nudTempsArret;
        private System.Windows.Forms.Label lblTempsTrajet;
        private System.Windows.Forms.NumericUpDown nudTempsTrajet;
        private System.Windows.Forms.Button btnAjouterArret;
        private System.Windows.Forms.Button btnSupprimerArret;
        private System.Windows.Forms.ColorDialog colorDialog;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.flpLignes = new System.Windows.Forms.FlowLayoutPanel();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.chkEstActif = new System.Windows.Forms.CheckBox();
            this.btnCouleur = new System.Windows.Forms.Button();
            this.txtCouleur = new System.Windows.Forms.TextBox();
            this.lblCouleur = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tabInfos = new System.Windows.Forms.TabPage();
            this.tabArrets = new System.Windows.Forms.TabPage();
            this.btnSupprimerArret = new System.Windows.Forms.Button();
            this.btnAjouterArret = new System.Windows.Forms.Button();
            this.grpArret = new System.Windows.Forms.GroupBox();
            this.nudTempsTrajet = new System.Windows.Forms.NumericUpDown();
            this.lblTempsTrajet = new System.Windows.Forms.Label();
            this.nudTempsArret = new System.Windows.Forms.NumericUpDown();
            this.lblTempsArret = new System.Windows.Forms.Label();
            this.nudOrdre = new System.Windows.Forms.NumericUpDown();
            this.lblOrdre = new System.Windows.Forms.Label();
            this.cboArret = new System.Windows.Forms.ComboBox();
            this.lblArret = new System.Windows.Forms.Label();
            this.flpArrets = new System.Windows.Forms.FlowLayoutPanel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.grpDetails.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tabInfos.SuspendLayout();
            this.tabArrets.SuspendLayout();
            this.grpArret.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsTrajet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsArret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrdre)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(183, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Gestion des lignes";
            // 
            // flpLignes
            // 
            this.flpLignes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpLignes.AutoScroll = true;
            this.flpLignes.BackColor = System.Drawing.Color.White;
            this.flpLignes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpLignes.Location = new System.Drawing.Point(15, 50);
            this.flpLignes.Name = "flpLignes";
            this.flpLignes.Size = new System.Drawing.Size(300, 385);
            this.flpLignes.TabIndex = 1;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.chkEstActif);
            this.grpDetails.Controls.Add(this.btnCouleur);
            this.grpDetails.Controls.Add(this.txtCouleur);
            this.grpDetails.Controls.Add(this.lblCouleur);
            this.grpDetails.Controls.Add(this.txtNom);
            this.grpDetails.Controls.Add(this.lblNom);
            this.grpDetails.Controls.Add(this.txtNumero);
            this.grpDetails.Controls.Add(this.lblNumero);
            this.grpDetails.Controls.Add(this.txtId);
            this.grpDetails.Controls.Add(this.lblId);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Location = new System.Drawing.Point(3, 3);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(459, 308);
            this.grpDetails.TabIndex = 2;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Détails de la ligne";
            // 
            // chkEstActif
            // 
            this.chkEstActif.AutoSize = true;
            this.chkEstActif.Location = new System.Drawing.Point(120, 143);
            this.chkEstActif.Name = "chkEstActif";
            this.chkEstActif.Size = new System.Drawing.Size(64, 17);
            this.chkEstActif.TabIndex = 11;
            this.chkEstActif.Text = "Est actif";
            this.chkEstActif.UseVisualStyleBackColor = true;
            // 
            // btnCouleur
            // 
            this.btnCouleur.Location = new System.Drawing.Point(230, 115);
            this.btnCouleur.Name = "btnCouleur";
            this.btnCouleur.Size = new System.Drawing.Size(30, 23);
            this.btnCouleur.TabIndex = 8;
            this.btnCouleur.Text = "...";
            this.btnCouleur.UseVisualStyleBackColor = true;
            this.btnCouleur.Click += new System.EventHandler(this.btnCouleur_Click);
            // 
            // txtCouleur
            // 
            this.txtCouleur.Location = new System.Drawing.Point(120, 117);
            this.txtCouleur.Name = "txtCouleur";
            this.txtCouleur.Size = new System.Drawing.Size(100, 20);
            this.txtCouleur.TabIndex = 7;
            // 
            // lblCouleur
            // 
            this.lblCouleur.AutoSize = true;
            this.lblCouleur.Location = new System.Drawing.Point(20, 120);
            this.lblCouleur.Name = "lblCouleur";
            this.lblCouleur.Size = new System.Drawing.Size(46, 13);
            this.lblCouleur.TabIndex = 6;
            this.lblCouleur.Text = "Couleur:";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(120, 87);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(200, 20);
            this.txtNom.TabIndex = 5;
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(20, 90);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(32, 13);
            this.lblNom.TabIndex = 4;
            this.lblNom.Text = "Nom:";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(120, 57);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 20);
            this.txtNumero.TabIndex = 3;
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(20, 60);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(47, 13);
            this.lblNumero.TabIndex = 2;
            this.lblNumero.Text = "Numéro:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(120, 27);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 30);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(19, 13);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id:";
            // 
            // btnNouveau
            // 
            this.btnNouveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNouveau.Location = new System.Drawing.Point(480, 405);
            this.btnNouveau.Name = "btnNouveau";
            this.btnNouveau.Size = new System.Drawing.Size(90, 30);
            this.btnNouveau.TabIndex = 3;
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.UseVisualStyleBackColor = true;
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrer.Location = new System.Drawing.Point(580, 405);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(90, 30);
            this.btnEnregistrer.TabIndex = 4;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSupprimer.Location = new System.Drawing.Point(680, 405);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(90, 30);
            this.btnSupprimer.TabIndex = 5;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // tabDetails
            // 
            this.tabDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDetails.Controls.Add(this.tabInfos);
            this.tabDetails.Controls.Add(this.tabArrets);
            this.tabDetails.Location = new System.Drawing.Point(330, 50);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(473, 340);
            this.tabDetails.TabIndex = 6;
            // 
            // tabInfos
            // 
            this.tabInfos.Controls.Add(this.grpDetails);
            this.tabInfos.Location = new System.Drawing.Point(4, 22);
            this.tabInfos.Name = "tabInfos";
            this.tabInfos.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfos.Size = new System.Drawing.Size(465, 314);
            this.tabInfos.TabIndex = 0;
            this.tabInfos.Text = "Informations";
            this.tabInfos.UseVisualStyleBackColor = true;
            // 
            // tabArrets
            // 
            this.tabArrets.Controls.Add(this.btnSupprimerArret);
            this.tabArrets.Controls.Add(this.btnAjouterArret);
            this.tabArrets.Controls.Add(this.grpArret);
            this.tabArrets.Controls.Add(this.flpArrets);
            this.tabArrets.Location = new System.Drawing.Point(4, 22);
            this.tabArrets.Name = "tabArrets";
            this.tabArrets.Padding = new System.Windows.Forms.Padding(3);
            this.tabArrets.Size = new System.Drawing.Size(465, 314);
            this.tabArrets.TabIndex = 1;
            this.tabArrets.Text = "Arrêts";
            this.tabArrets.UseVisualStyleBackColor = true;
            // 
            // btnSupprimerArret
            // 
            this.btnSupprimerArret.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSupprimerArret.Location = new System.Drawing.Point(250, 278);
            this.btnSupprimerArret.Name = "btnSupprimerArret";
            this.btnSupprimerArret.Size = new System.Drawing.Size(120, 30);
            this.btnSupprimerArret.TabIndex = 3;
            this.btnSupprimerArret.Text = "Supprimer l\'arrêt";
            this.btnSupprimerArret.UseVisualStyleBackColor = true;
            this.btnSupprimerArret.Click += new System.EventHandler(this.btnSupprimerArret_Click);
            // 
            // btnAjouterArret
            // 
            this.btnAjouterArret.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAjouterArret.Location = new System.Drawing.Point(100, 278);
            this.btnAjouterArret.Name = "btnAjouterArret";
            this.btnAjouterArret.Size = new System.Drawing.Size(120, 30);
            this.btnAjouterArret.TabIndex = 2;
            this.btnAjouterArret.Text = "Ajouter l\'arrêt";
            this.btnAjouterArret.UseVisualStyleBackColor = true;
            this.btnAjouterArret.Click += new System.EventHandler(this.btnAjouterArret_Click);
            // 
            // grpArret
            // 
            this.grpArret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpArret.Controls.Add(this.nudTempsTrajet);
            this.grpArret.Controls.Add(this.lblTempsTrajet);
            this.grpArret.Controls.Add(this.nudTempsArret);
            this.grpArret.Controls.Add(this.lblTempsArret);
            this.grpArret.Controls.Add(this.nudOrdre);
            this.grpArret.Controls.Add(this.lblOrdre);
            this.grpArret.Controls.Add(this.cboArret);
            this.grpArret.Controls.Add(this.lblArret);
            this.grpArret.Location = new System.Drawing.Point(6, 162);
            this.grpArret.Name = "grpArret";
            this.grpArret.Size = new System.Drawing.Size(453, 110);
            this.grpArret.TabIndex = 1;
            this.grpArret.TabStop = false;
            this.grpArret.Text = "Ajouter un arrêt";
            // 
            // nudTempsTrajet
            // 
            this.nudTempsTrajet.Location = new System.Drawing.Point(320, 83);
            this.nudTempsTrajet.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudTempsTrajet.Name = "nudTempsTrajet";
            this.nudTempsTrajet.Size = new System.Drawing.Size(60, 20);
            this.nudTempsTrajet.TabIndex = 7;
            this.nudTempsTrajet.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblTempsTrajet
            // 
            this.lblTempsTrajet.AutoSize = true;
            this.lblTempsTrajet.Location = new System.Drawing.Point(200, 85);
            this.lblTempsTrajet.Name = "lblTempsTrajet";
            this.lblTempsTrajet.Size = new System.Drawing.Size(96, 13);
            this.lblTempsTrajet.TabIndex = 6;
            this.lblTempsTrajet.Text = "Temps trajet (min) :";
            // 
            // nudTempsArret
            // 
            this.nudTempsArret.Location = new System.Drawing.Point(130, 83);
            this.nudTempsArret.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTempsArret.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTempsArret.Name = "nudTempsArret";
            this.nudTempsArret.Size = new System.Drawing.Size(60, 20);
            this.nudTempsArret.TabIndex = 5;
            this.nudTempsArret.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTempsArret
            // 
            this.lblTempsArret.AutoSize = true;
            this.lblTempsArret.Location = new System.Drawing.Point(15, 85);
            this.lblTempsArret.Name = "lblTempsArret";
            this.lblTempsArret.Size = new System.Drawing.Size(102, 13);
            this.lblTempsArret.TabIndex = 4;
            this.lblTempsArret.Text = "Temps d\'arrêt (min) :";
            // 
            // nudOrdre
            // 
            this.nudOrdre.Location = new System.Drawing.Point(60, 53);
            this.nudOrdre.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOrdre.Name = "nudOrdre";
            this.nudOrdre.Size = new System.Drawing.Size(60, 20);
            this.nudOrdre.TabIndex = 3;
            this.nudOrdre.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblOrdre
            // 
            this.lblOrdre.AutoSize = true;
            this.lblOrdre.Location = new System.Drawing.Point(15, 55);
            this.lblOrdre.Name = "lblOrdre";
            this.lblOrdre.Size = new System.Drawing.Size(39, 13);
            this.lblOrdre.TabIndex = 2;
            this.lblOrdre.Text = "Ordre :";
            // 
            // cboArret
            // 
            this.cboArret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArret.FormattingEnabled = true;
            this.cboArret.Location = new System.Drawing.Point(60, 22);
            this.cboArret.Name = "cboArret";
            this.cboArret.Size = new System.Drawing.Size(200, 21);
            this.cboArret.TabIndex = 1;
            // 
            // lblArret
            // 
            this.lblArret.AutoSize = true;
            this.lblArret.Location = new System.Drawing.Point(15, 25);
            this.lblArret.Name = "lblArret";
            this.lblArret.Size = new System.Drawing.Size(35, 13);
            this.lblArret.TabIndex = 0;
            this.lblArret.Text = "Arrêt :";
            // 
            // flpArrets
            // 
            this.flpArrets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpArrets.AutoScroll = true;
            this.flpArrets.BackColor = System.Drawing.Color.White;
            this.flpArrets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpArrets.Location = new System.Drawing.Point(6, 6);
            this.flpArrets.Name = "flpArrets";
            this.flpArrets.Size = new System.Drawing.Size(453, 150);
            this.flpArrets.TabIndex = 0;
            // 
            // UCGestionLignes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabDetails);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.flpLignes);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCGestionLignes";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCGestionLignes_Load);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.tabDetails.ResumeLayout(false);
            this.tabInfos.ResumeLayout(false);
            this.tabArrets.ResumeLayout(false);
            this.grpArret.ResumeLayout(false);
            this.grpArret.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsTrajet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsArret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrdre)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
