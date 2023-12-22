///**************************************************************************************
///ETML
///Auteur : Omar Egal Ahmed
///Date : 07.12.2023
///Description : Création d'un programme de type jeu en C# Winform: Mastermind. 
///**************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MastermindGraphic
{
    public partial class MastermindGraphic : Form
    {
        
        /// <summary>
        /// Déclaration et initialisation des variables pour le jeu Mastermind.
        /// </summary>
        
        private int _rowsPanel;  // Nombre de lignes dans le tableau de couleurs

        private int _columnsPanel;  // Nombre de colonnes dans le tableau de couleurs

        public List<Color> secretCode;  // Code secret généré aléatoirement

        private Label[,] _panelChoiceColorsArray;  // Tableau de labels pour les choix de couleurs de l'utilisateur

        private Label[,] _panelCheckColorsArray;  // Tableau de labels pour les indicateurs de bonnes et mauvaises positions

        private Button[] _tabColors = new Button[_MAXCOLORS];  // Tableau de boutons représentant les couleurs disponibles

        private const int _MAXCOLORS = 7;  // Nombre maximum de couleurs disponibles

        private const int _MAXCOLORSCHOICE = 5;  // Nombre maximum de couleurs à choisir dans une tentative

        private const int _MARGINBUTTON = 10;  // Marge entre les boutons de couleur

        private int _columns = 0;  // Indice de colonne actuel dans le tableau de choix de couleurs

        private int _rows = 0;  // Indice de ligne actuel dans le tableau de choix de couleurs

        private List<Color> _guess = new List<Color>();  // Liste des couleurs choisies dans la tentative actuelle

        private int attempts = 0;  // Nombre total de tentatives

        private int correctlyPlaced = 0;  // Nombre de couleurs correctement placées dans la tentative actuelle

        private int incorrectlyPlaced = 0;  // Nombre de couleurs incorrectement placées dans la tentative actuelle

        private int misplaced = 0;  // Total des couleurs mal placées dans la tentative actuelle
       
       
        Color[] TabColours = { Color.Green, Color.Yellow, Color.White, Color.Red, Color.Magenta, Color.Blue, Color.Cyan }; // Tableau de couleurs disponibles

        public MastermindGraphic()
        {
            InitializeComponent();
            secretCode = GenerateSecretCode();

        }

        /// <summary>
        /// Initialise une nouvelle partie du jeu Mastermind.
        /// </summary>
        public void InitializeGame()
        {
            // Génère une nouvelle séquence secrète
            secretCode = GenerateSecretCode();

            // Crée des copies de la séquence secrète et de la proposition pour éviter les modifications indésirables.
            List<Color> secretCopy = new List<Color>(secretCode);
            List<Color> guessCopy = new List<Color>(_guess);

            // Copie la séquence secrète et la proposition de l'utilisateur
            CopysecretCode(_guess, secretCopy, guessCopy, secretCode);

            // Réinitialise les compteurs et les éléments graphiques
            attempts = 0;
            _columns = 0;
            _rows = 0;

            // Masque les boutons de réinitialisation et de sortie
            resetButton.Hide();
            exitButton.Hide();

            // Affiche les panels de choix de couleurs et de vérification des couleurs
            colorChoisepanel.Show();
            checkColorspanel.Show();

            // Affiche les boutons de suppression et de validation
            deleteButton.Show();
            buttonValitador.Show();

            // Détermine les dimensions des tableaux de labels pour le choix et la vérification des couleurs
            _columnsPanel = 4;
            _rowsPanel = 10;

            // Initialise les tableaux de labels pour le choix et la vérification des couleurs
            _panelChoiceColorsArray = new Label[_rowsPanel, _columnsPanel];
            InitializePanelcolorchoice(colorChoisepanel, _rowsPanel, _columnsPanel);

            _columnsPanel = 4;
            _rowsPanel = 10;
            _panelCheckColorsArray = new Label[_rowsPanel, _columnsPanel];
            InitializePanelcheckchoice(checkColorspanel, _rowsPanel, _columnsPanel);

            // Crée les boutons de couleurs
            CreateBtnColours();
        }



        /// <summary>
        /// Méthode pour copier la séquence secrète et la proposition de l'utilisateur afin d'éviter des modifications indésirables.
        /// </summary>
        /// <param name="_guess">La proposition de l'utilisateur.</param>
        /// <param name="secretCopy">Une liste à remplir avec une copie de la séquence secrète.</param>
        /// <param name="guessCopy">Une liste à remplir avec une copie de la proposition de l'utilisateur.</param>
        /// <param name="secretCode">La séquence secrète d'origine.</param>
        private void CopysecretCode(List<Color> _guess, List<Color> secretCopy, List<Color> guessCopy, List<Color> secretCode)
        {
            // Efface la liste de la copie de la séquence secrète
            secretCopy.Clear();

            // Ajoute tous les éléments de la séquence secrète originale à la copie
            secretCopy.AddRange(secretCode);

            // Efface la liste de la copie de la proposition de l'utilisateur
            guessCopy.Clear();

            // Ajoute tous les éléments de la proposition de l'utilisateur à la copie
            guessCopy.AddRange(_guess);
        }


        /// <summary>
        /// Méthode pour générer le code secret.
        /// </summary>
        /// <returns></returns>
        private List<Color> GenerateSecretCode()
        {
            List<Color> internSecretcode = new List<Color>();
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                internSecretcode.Add(TabColours[random.Next(_MAXCOLORS)]);
            }

            return internSecretcode;
        }

        /// <summary>
        /// Méthode pour compter le nombre de couleurs correctement placées dans la proposition de l'utilisateur.
        /// </summary>
        /// <param name="guessCopy">Une copie de la proposition de l'utilisateur.</param>
        /// <param name="secretCopy">Une copie de la séquence secrète.</param>
        /// <returns>Le nombre de couleurs correctement placées dans la proposition.</returns>
        private int CheckCorrectlyPlaced(List<Color> guessCopy, List<Color> secretCopy)
        {
            // Initialise le compteur des couleurs correctement placées
            correctlyPlaced = 0;

            // Parcourt les indices des couleurs dans la proposition
            for (int i = 0; i < 4; i++)
            {
                // Calcule la position actuelle dans la copie de la proposition
                int cursor = guessCopy.Count - 4 + i;

                // Vérifie si la couleur à cette position est identique à la couleur correspondante dans la séquence secrète
                if (guessCopy[cursor] == secretCopy[i])
                {
                    // Incrémente le compteur des couleurs correctement placées
                    correctlyPlaced++;

                    // Marque les couleurs dans la séquence secrète et la proposition comme déjà validées
                    secretCopy[i] = Color.DarkGray;
                    guessCopy[cursor] = Color.DarkGray;
                }
            }

            // Retourne le nombre de couleurs correctement placées
            return correctlyPlaced;
        }


        /// <summary>
        /// Méthode pour compter le nombre de réponses incorrectes dans la proposition de l'utilisateur.
        /// </summary>
        /// <param name="guessCopy">Une copie de la proposition de l'utilisateur.</param>
        /// <param name="secretCopy">Une copie de la séquence secrète.</param>
        /// <returns>Le nombre de couleurs mal placées dans la proposition.</returns>
        private int CheckIncorrectlyPlaced(List<Color> guessCopy, List<Color> secretCopy)
        {
            // Initialise le compteur des couleurs mal placées
            incorrectlyPlaced = 0;

            // Parcourt les indices des couleurs dans la proposition
            for (int i = 0; i < 4; i++)
            {
                // Calcule la position actuelle dans la copie de la proposition
                int cursor = guessCopy.Count - 4 + i;

                // Vérifie si la couleur à cette position n'est pas déjà marquée comme correcte
                if (guessCopy[cursor] != Color.DarkGray)
                {
                    // Parcourt les indices des couleurs dans la copie de la séquence secrète
                    for (int j = 0; j < secretCopy.Count; j++)
                    {
                        // Vérifie si la couleur à cette position dans la proposition est présente dans la séquence secrète
                        if (guessCopy[cursor] == secretCopy[j])
                        {
                            // Incrémente le compteur des couleurs mal placées
                            incorrectlyPlaced++;

                            // Marque la couleur dans la séquence secrète comme déjà validée
                            secretCopy[j] = Color.DarkGray;

                            // Sort du deuxième boucle pour éviter de compter la même couleur plusieurs fois
                            break;
                        }
                    }
                }
            }

            // Retourne le nombre de couleurs mal placées
            return incorrectlyPlaced;
        }

        /// <summary>
        /// Méthode pour initialiser le tableau de labels pour le choix des couleurs de l'utilisateur dans un panneau spécifié.
        /// </summary>
        /// <param name="panel">Le panneau dans lequel les labels seront ajoutés.</param>
        /// <param name="rows">Le nombre de lignes dans le tableau.</param>
        /// <param name="columns">Le nombre de colonnes dans le tableau.</param>
        private void InitializePanelcolorchoice(Panel panel, int rows, int columns)
        {
            // Parcourt toutes les lignes
            for (int i = 0; i < rows; ++i)
            {
                // Parcourt toutes les colonnes
                for (int j = 0; j < columns; ++j)
                {
                    // Calcul des dimensions pour chaque label en fonction du panneau
                    int rowHeight = panel.Height / rows;
                    int colHeight = panel.Width / columns;

                    // Création d'un label de choix de couleur
                    Label choice = new Label();

                    // Positionnement du label dans le panneau
                    choice.Location = new Point((j * colHeight), (i * rowHeight));

                    // Couleur de fond initiale du label
                    choice.BackColor = Color.DarkGray;

                    // Dimensions du label
                    choice.Size = new Size(panel.Width / columns, panel.Height / rows);

                    // Tag peut être utilisé pour stocker des informations supplémentaires (commenté ici)
                    choice.Tag = //$"Bouton [{j + 1}, {i + 1}]";

                    // Texte initial du label
                    choice.Text = "y";

                    // Ajout du label au panneau
                    colorChoisepanel.Controls.Add(choice);

                    // Stockage du label dans le tableau pour une référence future
                    _panelChoiceColorsArray[i, j] = choice;
                }
            }
        }


        /// <summary>
        /// Méthode pour initialiser le tableau de labels de contrôle des couleurs dans un panneau spécifié.
        /// </summary>
        /// <param name="panel">Le panneau dans lequel les labels seront ajoutés.</param>
        /// <param name="rows">Le nombre de lignes dans le tableau.</param>
        /// <param name="columns">Le nombre de colonnes dans le tableau.</param>
        private void InitializePanelcheckchoice(Panel panel, int rows, int columns)
        {
            // Parcourt toutes les lignes
            for (int i = 0; i < rows; ++i)
            {
                // Parcourt toutes les colonnes
                for (int j = 0; j < columns; ++j)
                {
                    // Calcul des dimensions pour chaque label en fonction du panneau
                    int rowHeight = panel.Height / rows;
                    int colHeight = panel.Width / columns;

                    // Création d'un label de contrôle
                    Label check = new Label();

                    // Positionnement du label dans le panneau
                    check.Location = new Point((j * colHeight), (i * rowHeight));

                    // Couleur de fond initiale du label
                    check.BackColor = Color.DarkGray;

                    // Dimensions du label
                    check.Size = new Size(panel.Width / columns, panel.Height / rows);

                    // Tag peut être utilisé pour stocker des informations supplémentaires (commenté ici)
                    check.Tag = //$"Bouton [{j + 1}, {i + 1}]";

                    // Texte initial du label
                    check.Text = "y";

                    // Ajout du label au panneau
                    checkColorspanel.Controls.Add(check);

                    // Stockage du label dans le tableau pour une référence future
                    _panelCheckColorsArray[i, j] = check;
                }
            }
        }

        /// <summary>
        /// Méthode de création des boutons de couleurs.
        /// </summary>
        void CreateBtnColours()
        {
            for (int i = 0; i < _MAXCOLORS; i++)
            {
                //Création des boutons et de leurs caractéristiques.
                _tabColors[i] = new Button();
                _tabColors[i].Size = new Size(40, 40);
                _tabColors[i].BackColor = TabColours[i];
                _tabColors[i].Tag = _tabColors[i].BackColor;
                _tabColors[i].Text = "";

                //Localisation des boutons.
                int a = _tabColors[i].Height * i + _MARGINBUTTON * i + _MARGINBUTTON * i;
                _tabColors[i].Location = new Point(a, 10);
                panelColorsbtn.Controls.Add(_tabColors[i]);

                //ajout  d'un événement au clic de chaque bouton
                _tabColors[i].Click += tabcolors_Color_Click;
            }
        }

        /// <summary>
        /// Methode pour la dispotion des deux tableaux de labels en chargement du programme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MastermindGraphic_Load(object sender, EventArgs e)
        {
            InitializeGame();
  
        }

        /// <summary>
        /// Méthode pour le chagement de couleurs du backgrouds du label au click d'un des boutons de couleurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabcolors_Color_Click(object sender, EventArgs e)
        {
            // Récupère le bouton de couleur cliqué
            Button clickedButton = (Button)sender;

            // Vérifie si l'utilisateur peut encore sélectionner une couleur
            if (_rows <= 3)
            {
                // Change la couleur de fond du label correspondant dans le tableau de choix de couleurs
                _panelChoiceColorsArray[_columns, _rows].BackColor = clickedButton.BackColor;

                // Ajoute la couleur choisie à la liste de proposition (_guess)
                _guess.Add(clickedButton.BackColor);

                // Incrémente le compteur de lignes (_rows)
                _rows++;

                // Désactive tous les boutons de couleur si l'utilisateur a fait 4 sélections
                if (_rows == 4)
                {
                    foreach (Button BtnClolors in _tabColors)
                    {
                        BtnClolors.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Méthode de la logique du jeu Mastermind au click du bouton "GO".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonValitador_Click(object sender, EventArgs e)
        {
            // Réactive tous les boutons de couleurs pour la prochaine tentative
            foreach (Button BtnClolors in _tabColors)
            {
                BtnClolors.Enabled = true;
            }

            // Vérifie si l'utilisateur a complété sa proposition de 4 couleurs
            if (_rows == 4)
            {
                _rows = 0;
                _columns++;
                attempts++;

                // Création des copies de la séquence secrète et de la proposition pour éviter les modifications indésirables
                List<Color> secretCopy = new List<Color>(secretCode);
                List<Color> guessCopy = new List<Color>(_guess);

                CopysecretCode(_guess, secretCopy, guessCopy, secretCode);

                // Vérification des couleurs correctement placées
                correctlyPlaced = CheckCorrectlyPlaced(guessCopy, secretCopy);

                // Vérification des couleurs mal placées
                misplaced = CheckIncorrectlyPlaced(guessCopy, secretCopy);

                // Mise à jour des couleurs dans le tableau de vérification (_panelCheckColorsArray)
                for (int i = 0; i < correctlyPlaced; i++)
                {
                    if (i < _panelCheckColorsArray.GetLength(1))
                    {
                        _panelCheckColorsArray[attempts - 1, i].BackColor = Color.White;
                    }
                }

                // Mise à jour des couleurs mal placées
                for (int i = correctlyPlaced; i < correctlyPlaced + misplaced; i++)
                {
                    if (i < _panelCheckColorsArray.GetLength(1))
                    {
                        _panelCheckColorsArray[attempts - 1, i].BackColor = Color.Black;
                    }
                }
            }

            // Vérifie si le code a été deviné
            if (correctlyPlaced == 4)
            {
                MessageBox.Show("Vous avez gagné !");

                // Cache les boutons "GO" et "UNDO"
                deleteButton.Hide();
                buttonValitador.Hide();

                // Affiche les boutons "EXIT" et "RESET"
                resetButton.Show();
                exitButton.Show();

                // Cache les panels pour le jeu
                colorChoisepanel.Hide();
                checkColorspanel.Hide();
            }

            // Vérifie si le nombre maximal de tentatives a été atteint
            if (attempts == 10)
            {
                MessageBox.Show("Vous avez perdu !");

                // Cache les boutons "GO" et "UNDO"
                deleteButton.Hide();
                buttonValitador.Hide();

                // Affiche les boutons "EXIT" et "RESET"
                resetButton.Show();
                exitButton.Show();

                // Cache les panels pour le jeu
                colorChoisepanel.Hide();
                checkColorspanel.Hide();
            }
        }

        /// <summary>
        /// Méthode pour la supression d'une couleur choisie dans le tableau de "_panelChoiceColorsArray".  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {          
           
            if (_rows!= 0)//
            {

                // Met à jour la couleur dans l'interface graphique
                _rows--;
                _panelChoiceColorsArray[_columns, _rows].BackColor = Color.DarkGray;


                // Retire la dernière couleur ajoutée à _guess
                _guess.RemoveAt(_guess.Count - 1);

                // Rend tous les boutons de couleur à nouveau disponibles
                foreach (Button BtnClolors in _tabColors)
                {
                    BtnClolors.Enabled = true;

                }
            }
        }  
        /// <summary>
        /// Méthode pour quitter le programme quand on clique sur le bouton "EXIT".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Méthode pour réinitialiser le jeu lorsque l'utilisateur clique sur le bouton "RESET".
        /// </summary>
        /// <param name="sender">L'objet qui a déclenché l'événement.</param>
        /// <param name="e">Les données de l'événement.</param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            // Efface les panneaux de choix et de vérification des couleurs
            colorChoisepanel.Controls.Clear();
            checkColorspanel.Controls.Clear();

            // Réinitialise les variables de contrôle du jeu
            _columns = 0;
            _rows = 0;

            // Réinitialise les compteurs de couleurs correctement et incorrectement placées
            correctlyPlaced = 0;
            incorrectlyPlaced = 0;
            misplaced = 0;

            // Initialise le jeu avec de nouvelles couleurs secrètes
            InitializeGame();
        }
    }
}


//toujours des undersocore
//Toutes les méthode commence par une majuscule.