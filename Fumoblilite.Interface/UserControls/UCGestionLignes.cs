using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;
using Fumoblilite.Systeme.Interfaces;
using System.Linq;

namespace GestionTransport.Interface.UserControls
{
    public partial class UCGestionLignes : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;
        private Ligne _ligneSelectionnee;

        public UCGestionLignes(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            
            // Initialisation des repositories
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);
            
            // Initialisation des services
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceArret = new ServiceArret(repositoryArret);
        }

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.dgvLignes = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblCouleur = new System.Windows.Forms.Label();
            this.txtCouleur = new System.Windows.Forms.TextBox();
            this.btnCouleur = new System.Windows.Forms.Button();
            this.lblTypeTransport = new System.Windows.Forms.Label();
            this.cboTypeTransport = new System.Windows.Forms.ComboBox();
            this.chkEstActif = new System.Windows.Forms.CheckBox();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tabInfos = new System.Windows.Forms.TabPage();
            this.tabArrets = new System.Windows.Forms.TabPage();
            this.dgvArrets = new System.Windows.Forms.DataGridView();
            this.grpArret = new System.Windows.Forms.GroupBox();
            this.lblArret = new System.Windows.Forms.Label();
            this.cboArret = new System.Windows.Forms.ComboBox();
            this.lblOrdre = new System.Windows.Forms.Label();
            this.nudOrdre = new System.Windows.Forms.NumericUpDown();
            this.lblTempsArret = new System.Windows.Forms.Label();
            this.nudTempsArret = new System.Windows.Forms.NumericUpDown();
            this.lblTempsTrajet = new System.Windows.Forms.Label();
            this.nudTempsTrajet = new System.Windows.Forms.NumericUpDown();
            this.btnAjouterArret = new System.Windows.Forms.Button();
            this.btnSupprimerArret = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLignes)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tabInfos.SuspendLayout();
            this.tabArrets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArrets)).BeginInit();
            this.grpArret.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrdre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsArret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsTrajet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(159, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Gestion des lignes";
            // 
            // dgvLignes
            // 
            this.dgvLignes.AllowUserToAddRows = false;
            this.dgvLignes.AllowUserToDeleteRows = false;
            this.dgvLignes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvLignes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLignes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLignes.Location = new System.Drawing.Point(15, 50);
            this.dgvLignes.MultiSelect = false;
            this.dgvLignes.Name = "dgvLignes";
            this.dgvLignes.ReadOnly = true;
            this.dgvLignes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLignes.Size = new System.Drawing.Size(300, 385);
            this.dgvLignes.TabIndex = 1;
            this.dgvLignes.SelectionChanged += new System.EventHandler(this.dgvLignes_SelectionChanged);
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.chkEstActif);
            this.grpDetails.Controls.Add(this.cboTypeTransport);
            this.grpDetails.Controls.Add(this.lblTypeTransport);
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
            this.grpDetails.Size = new System.Drawing.Size(459, 334);
            this.grpDetails.TabIndex = 2;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Détails de la ligne";
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
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(120, 27);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(20, 60);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(50, 13);
            this.lblNumero.TabIndex = 2;
            this.lblNumero.Text = "Numéro:";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(120, 57);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 20);
            this.txtNumero.TabIndex = 3;
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
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(120, 87);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(200, 20);
            this.txtNom.TabIndex = 5;
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
            // txtCouleur
            // 
            this.txtCouleur.Location = new System.Drawing.Point(120, 117);
            this.txtCouleur.Name = "txtCouleur";
            this.txtCouleur.Size = new System.Drawing.Size(100, 20);
            this.txtCouleur.TabIndex = 7;
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
            // lblTypeTransport
            // 
            this.lblTypeTransport.AutoSize = true;
            this.lblTypeTransport.Location = new System.Drawing.Point(20, 150);
            this.lblTypeTransport.Name = "lblTypeTransport";
            this.lblTypeTransport.Size = new System.Drawing.Size(94, 13);
            this.lblTypeTransport.TabIndex = 9;
            this.lblTypeTransport.Text = "Type de transport:";
            // 
            // cboTypeTransport
            // 
            this.cboTypeTransport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeTransport.FormattingEnabled = true;
            this.cboTypeTransport.Items.AddRange(new object[] {
            "Bus",
            "Métro",
            "Tramway",
            "Train"});
            this.cboTypeTransport.Location = new System.Drawing.Point(120, 147);
            this.cboTypeTransport.Name = "cboTypeTransport";
            this.cboTypeTransport.Size = new System.Drawing.Size(150, 21);
            this.cboTypeTransport.TabIndex = 10;
            // 
            // chkEstActif
            // 
            this.chkEstActif.AutoSize = true;
            this.chkEstActif.Location = new System.Drawing.Point(120, 180);
            this.chkEstActif.Name = "chkEstActif";
            this.chkEstActif.Size = new System.Drawing.Size(64, 17);
            this.chkEstActif.TabIndex = 11;
            this.chkEstActif.Text = "Est actif";
            this.chkEstActif.UseVisualStyleBackColor = true;
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
            this.tabArrets.Controls.Add(this.dgvArrets);
            this.tabArrets.Location = new System.Drawing.Point(4, 22);
            this.tabArrets.Name = "tabArrets";
            this.tabArrets.Padding = new System.Windows.Forms.Padding(3);
            this.tabArrets.Size = new System.Drawing.Size(465, 314);
            this.tabArrets.TabIndex = 1;
            this.tabArrets.Text = "Arrêts";
            this.tabArrets.UseVisualStyleBackColor = true;
            // 
            // dgvArrets
            // 
            this.dgvArrets.AllowUserToAddRows = false;
            this.dgvArrets.AllowUserToDeleteRows = false;
            this.dgvArrets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArrets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArrets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArrets.Location = new System.Drawing.Point(6, 6);
            this.dgvArrets.MultiSelect = false;
            this.dgvArrets.Name = "dgvArrets";
            this.dgvArrets.ReadOnly = true;
            this.dgvArrets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArrets.Size = new System.Drawing.Size(453, 150);
            this.dgvArrets.TabIndex = 0;
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
            // lblArret
            // 
            this.lblArret.AutoSize = true;
            this.lblArret.Location = new System.Drawing.Point(15, 25);
            this.lblArret.Name = "lblArret";
            this.lblArret.Size = new System.Drawing.Size(35, 13);
            this.lblArret.TabIndex = 0;
            this.lblArret.Text = "Arrêt :";
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
            // lblOrdre
            // 
            this.lblOrdre.AutoSize = true;
            this.lblOrdre.Location = new System.Drawing.Point(15, 55);
            this.lblOrdre.Name = "lblOrdre";
            this.lblOrdre.Size = new System.Drawing.Size(39, 13);
            this.lblOrdre.TabIndex = 2;
            this.lblOrdre.Text = "Ordre :";
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
            // lblTempsArret
            // 
            this.lblTempsArret.AutoSize = true;
            this.lblTempsArret.Location = new System.Drawing.Point(15, 85);
            this.lblTempsArret.Name = "lblTempsArret";
            this.lblTempsArret.Size = new System.Drawing.Size(111, 13);
            this.lblTempsArret.TabIndex = 4;
            this.lblTempsArret.Text = "Temps d\'arrêt (min) :";
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
            // lblTempsTrajet
            // 
            this.lblTempsTrajet.AutoSize = true;
            this.lblTempsTrajet.Location = new System.Drawing.Point(200, 85);
            this.lblTempsTrajet.Name = "lblTempsTrajet";
            this.lblTempsTrajet.Size = new System.Drawing.Size(113, 13);
            this.lblTempsTrajet.TabIndex = 6;
            this.lblTempsTrajet.Text = "Temps trajet (min) :";
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
            // UCGestionLignes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabDetails);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.dgvLignes);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCGestionLignes";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCGestionLignes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLignes)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.tabDetails.ResumeLayout(false);
            this.tabInfos.ResumeLayout(false);
            this.tabArrets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArrets)).EndInit();
            this.grpArret.ResumeLayout(false);
            this.grpArret.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrdre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsArret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempsTrajet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.DataGridView dgvLignes;
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
        private System.Windows.Forms.Label lblTypeTransport;
        private System.Windows.Forms.ComboBox cboTypeTransport;
        private System.Windows.Forms.CheckBox chkEstActif;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tabInfos;
        private System.Windows.Forms.TabPage tabArrets;
        private System.Windows.Forms.DataGridView dgvArrets;
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

        private void UCGestionLignes_Load(object sender, EventArgs e)
        {
            ChargerLignes();
            ChargerArrets();
            cboTypeTransport.SelectedIndex = 0; // Sélectionner "Bus" par défaut
        }

        private void ChargerLignes()
        {
            try
            {
                List<Ligne> lignes = _serviceLigne.ObtenirToutes();
                dgvLignes.DataSource = lignes;

                // Configurer l'affichage des colonnes
                dgvLignes.Columns["Id"].Width = 50;
                dgvLignes.Columns["Numero"].HeaderText = "Numéro";
                dgvLignes.Columns["Numero"].Width = 70;
                dgvLignes.Columns["Nom"].Width = 150;
                dgvLignes.Columns["Couleur"].Visible = false;
                dgvLignes.Columns["TypeTransport"].HeaderText = "Type";
                dgvLignes.Columns["TypeTransport"].Width = 80;
                dgvLignes.Columns["EstActif"].HeaderText = "Actif";
                dgvLignes.Columns["EstActif"].Width = 50;
                dgvLignes.Columns["DateCreation"].Visible = false;
                dgvLignes.Columns["DateModification"].Visible = false;
                //dgvLignes.Columns["Arrets"].Visible = false;

                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des lignes : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerArrets()
        {
            try
            {
                List<Arret> arrets = _serviceArret.ObtenirTous();
                
                cboArret.DisplayMember = "Nom";
                cboArret.ValueMember = "Id";
                cboArret.DataSource = arrets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViderChamps()
        {
            txtId.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtCouleur.Text = "#FF0000"; // Rouge par défaut
            cboTypeTransport.SelectedIndex = 0; // "Bus" par défaut
            chkEstActif.Checked = true;
            _ligneSelectionnee = null;
            btnSupprimer.Enabled = false;
            dgvArrets.DataSource = null;
        }

        private void dgvLignes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLignes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvLignes.SelectedRows[0].Cells["Id"].Value);
                _ligneSelectionnee = _serviceLigne.ObtenirParId(id, true);
                if (_ligneSelectionnee != null)
                {
                    txtId.Text = _ligneSelectionnee.Id.ToString();
                    txtNumero.Text = _ligneSelectionnee.Numero;
                    txtNom.Text = _ligneSelectionnee.Nom;
                    txtCouleur.Text = _ligneSelectionnee.Couleur ?? "#FF0000";
                    cboTypeTransport.SelectedItem = _ligneSelectionnee.TypeTransport;
                    chkEstActif.Checked = _ligneSelectionnee.EstActif;
                    btnSupprimer.Enabled = true;
                    
                    // Afficher les arrêts de la ligne
                    AfficherArrets();
                }
            }
        }

        private void AfficherArrets()
        {
            try
            {
                if (_ligneSelectionnee == null || _ligneSelectionnee.Arrets == null)
                {
                    dgvArrets.DataSource = null;
                    return;
                }
                
                // Récupérer les informations des arrêts
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);
                
                // Préparer les données pour l'affichage
                var arretsAffichage = _ligneSelectionnee.Arrets.Select(al => new
                {
                    Id = al.Id,
                    Ordre = al.Ordre,
                    Nom = arrets.ContainsKey(al.ArretId) ? arrets[al.ArretId].Nom : $"Arrêt {al.ArretId}",
                    TempsArret = $"{al.TempsArretMinutes} min",
                    TempsTrajet = $"{al.TempsTrajetSuivantMinutes} min"
                }).OrderBy(a => a.Ordre).ToList();
                
                dgvArrets.DataSource = arretsAffichage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'affichage des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderChamps();
            tabDetails.SelectedIndex = 0; // Afficher l'onglet "Informations"
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumero.Text) || string.IsNullOrWhiteSpace(txtNom.Text))
                {
                    MessageBox.Show("Le numéro et le nom de la ligne sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_ligneSelectionnee == null)
                {
                    // Nouvelle ligne
                    Ligne nouvelleLigne = new Ligne
                    {
                        Numero = txtNumero.Text,
                        Nom = txtNom.Text,
                        Couleur = txtCouleur.Text,
                        TypeTransport = cboTypeTransport.SelectedItem.ToString(),
                        EstActif = chkEstActif.Checked
                    };

                    int id = _serviceLigne.Ajouter(nouvelleLigne);
                    MessageBox.Show("Ligne ajoutée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modification d'une ligne existante
                    _ligneSelectionnee.Numero = txtNumero.Text;
                    _ligneSelectionnee.Nom = txtNom.Text;
                    _ligneSelectionnee.Couleur = txtCouleur.Text;
                    _ligneSelectionnee.TypeTransport = cboTypeTransport.SelectedItem.ToString();
                    _ligneSelectionnee.EstActif = chkEstActif.Checked;

                    bool resultat = _serviceLigne.Modifier(_ligneSelectionnee);
                    if (resultat)
                    {
                        MessageBox.Show("Ligne modifiée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ChargerLignes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_ligneSelectionnee != null)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool resultat = _serviceLigne.Supprimer(_ligneSelectionnee.Id);
                        if (resultat)
                        {
                            MessageBox.Show("Ligne supprimée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ChargerLignes();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCouleur_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialiser la boîte de dialogue avec la couleur actuelle
                if (!string.IsNullOrEmpty(txtCouleur.Text) && txtCouleur.Text.StartsWith("#"))
                {
                    string hexColor = txtCouleur.Text.TrimStart('#');
                    if (hexColor.Length == 6)
                    {
                        int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
                        int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
                        int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
                        colorDialog.Color = Color.FromArgb(r, g, b);
                    }
                }

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color color = colorDialog.Color;
                    txtCouleur.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sélection de la couleur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouterArret_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ligneSelectionnee == null)
                {
                    MessageBox.Show("Veuillez d'abord sélectionner ou créer une ligne.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboArret.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner un arrêt.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int arretId = (int)cboArret.SelectedValue;
                int ordre = (int)nudOrdre.Value;
                int tempsArretMinutes = (int)nudTempsArret.Value;
                int tempsTrajetSuivantMinutes = (int)nudTempsTrajet.Value;

                // Vérifier si l'arrêt est déjà dans la ligne
                if (_ligneSelectionnee.Arrets.Any(a => a.ArretId == arretId))
                {
                    MessageBox.Show("Cet arrêt est déjà dans la ligne.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Créer un nouvel arrêt de ligne
                ArretLigne arretLigne = new ArretLigne
                {
                    LigneId = _ligneSelectionnee.Id,
                    ArretId = arretId,
                    Ordre = ordre,
                    TempsArretMinutes = tempsArretMinutes,
                    TempsTrajetSuivantMinutes = tempsTrajetSuivantMinutes
                };

                // Ajouter l'arrêt à la ligne
                bool resultat = _serviceLigne.AjouterArret(arretLigne);
                if (resultat)
                {
                    MessageBox.Show("Arrêt ajouté à la ligne avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Rafraîchir la ligne sélectionnée
                    _ligneSelectionnee = _serviceLigne.ObtenirParId(_ligneSelectionnee.Id, true);
                    AfficherArrets();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout de l'arrêt à la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimerArret_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvArrets.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un arrêt à supprimer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int arretLigneId = Convert.ToInt32(dgvArrets.SelectedRows[0].Cells["Id"].Value);
                
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet arrêt de la ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool resultat = _serviceLigne.SupprimerArret(arretLigneId);
                    if (resultat)
                    {
                        MessageBox.Show("Arrêt supprimé de la ligne avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Rafraîchir la ligne sélectionnée
                        _ligneSelectionnee = _serviceLigne.ObtenirParId(_ligneSelectionnee.Id, true);
                        AfficherArrets();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression de l'arrêt de la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
