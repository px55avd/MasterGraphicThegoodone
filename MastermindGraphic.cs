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
        
        //Déclaration des variables
        private int _rowsPanel;
        private int _columnsPanel;
        public  List<Color> secretCode;
        private static int currentAttempt = 1; // Déclare la variable currentAttempt et initialise sa valeur à 1
        private Label[,] _panelChoiceColorsArray;
        private Label[,] _panelCheckColorsArray;
        private Button[] _tabColors = new Button[_MAXCOLORS];
        private const int _MAXCOLORS = 7;
        private const int _MAXCOLORSCHOICE = 5;
        private const int _MARGINBUTTON = 10;
        private int _columns = 0;
        private int _rows = 0;  
        private List<Color> _guess = new List<Color>();
        private int  attempts = 0;
         private int correctlyPlaced = 0;
        int incorrectlyPlaced = 0;
        int misplaced = 0;

        //Déclaration de tableau de couleur
        Color[] TabColours = { Color.Green, Color.Yellow, Color.White, Color.Red, Color.Magenta, Color.Blue, Color.Cyan };

        public MastermindGraphic()
        {
            InitializeComponent();
            secretCode = GenerateSecretCode();

        }


        public void initializeGame()
        {
            resetButton.Hide();
            exitButton.Hide();

            colorChoisepanel.Show();
            checkColorspanel.Show();

            deleteButton.Show();
            buttonValitador.Show();

            
            
            _columnsPanel = 4;
            _rowsPanel = 10;
            _panelChoiceColorsArray = new Label[_rowsPanel, _columnsPanel];
            InitializePanelcolorchoice(colorChoisepanel, _rowsPanel, _columnsPanel);

            

            _columnsPanel = 4;
            _rowsPanel = 10;
            _panelCheckColorsArray = new Label[_rowsPanel, _columnsPanel];
            InitializePanelcheckchoice(checkColorspanel, _rowsPanel, _columnsPanel);
 

            CreateBtnColours();

        }


        /// <summary>
        /// Methode pour la copie du secretcode et guess code.
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="secretCopy"></param>
        /// <param name="guessCopy"></param>
        /// <param name="secretCode"></param>
        private void CopysecretCode(List<Color> _guess, List<Color> secretCopy, List<Color> guessCopy, List<Color> secretCode)
        {

            secretCopy.Clear();
            secretCopy.AddRange(secretCode);

            guessCopy.Clear();
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
        /// Méthode pour compter le nombre de couleurs corrects.
        /// </summary>
        /// <param name="guessCopy"></param>
        /// <param name="secretCopy"></param>
        /// <returns></returns>
        private int CheckCorrectlyPlaced(List<Color> guessCopy, List<Color> secretCopy)
        {
            correctlyPlaced = 0;

            for (int i = 0; i < 4; i++)
            {
                int cursor = guessCopy.Count-4+i;
               
                if (guessCopy[cursor] == secretCopy[i])
                {
                    correctlyPlaced++;
                    // Marquage des couleurs déjà validées.
                    secretCopy[i] = Color.DarkGray;
                    guessCopy[cursor] = Color.DarkGray;
                }
            }

            return correctlyPlaced;
        }

        /// <summary>
        /// Méthode pour compter le de réponse incorrect.
        /// </summary>
        /// <param name="guessCopy"></param>
        /// <param name="secretCopy"></param>
        /// <returns></returns>
        private int CheckIncorrectlyPlaced(List<Color> guessCopy, List<Color> secretCopy)
        {
             incorrectlyPlaced = 0;

            for (int i = 0; i < 4; i++)
            {
                int cursor = guessCopy.Count - 4 + i;

                if (guessCopy[cursor] != Color.DarkGray)
                {
                    for (int j = 0; j < secretCopy.Count; j++)
                    {
                        if (guessCopy[cursor] == secretCopy[j])
                        {
                            incorrectlyPlaced++;
                            secretCopy[j] = Color.DarkGray; // Marquage des couleurs déjà validées.
                            break;
                        }
                    }
                }
            }
            return incorrectlyPlaced;
        }

        /// <summary>
        /// Méthode pour la création d'un tableau de label pour le choix des couleurs de l'utilisateur.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        private void InitializePanelcolorchoice(Panel panel, int rows, int columns)
        {
            

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    int rowHeight = panel.Height / rows;
                    int colHeight = panel.Width / columns;

                    Label choice = new Label();

                    choice.Location = new Point((j * colHeight),
                                             (i * rowHeight));

                    choice.BackColor = Color.DarkGray;

                    choice.Size = new Size(panel.Width / columns, panel.Height / rows);

                    choice.Tag = //$"Bouton [{j + 1}, {i + 1}]";
                    choice.Text ="y";
                    choice.BackColor = Color.DarkGray;
                   
                    colorChoisepanel.Controls.Add(choice);

                    _panelChoiceColorsArray[i, j] = choice;

                    
                }
            }
        }

        /// <summary>
        /// Méthode pour la création du tableau de labels pour le contrôle des couleurs.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        private void InitializePanelcheckchoice(Panel panel, int rows, int columns)
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    int rowHeight = panel.Height / rows;
                    int colHeight = panel.Width / columns;

                    Label check = new Label();

                    check.Location = new Point((j * colHeight),
                                             (i * rowHeight));

                    check.BackColor = Color.DarkGray;

                    check.Size = new Size(panel.Width / columns, panel.Height / rows);

                    check.Tag = //$"Bouton [{j + 1}, {i + 1}]";
                    check.Text = "y";

                    checkColorspanel.Controls.Add(check);

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
            initializeGame();
            /*resetButton.Hide();
            exitButton.Hide();


            _columnsPanel = 4;
            _rowsPanel = 10;
            _panelChoiceColorsArray = new Label[_rowsPanel, _columnsPanel];
            InitializePanelcolorchoice(colorChoisepanel, _rowsPanel, _columnsPanel);

            _columnsPanel = 4;
            _rowsPanel = 10;
            _panelCheckColorsArray = new Label[_rowsPanel, _columnsPanel];
            InitializePanelcheckchoice(checkColorspanel, _rowsPanel, _columnsPanel);

            CreateBtnColours();*/
        }

        /// <summary>
        /// Méthode pour le chagement de couleurs du backgrouds du label au click d'un des boutons de couleurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabcolors_Color_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button) sender;
            _panelChoiceColorsArray[_columns,_rows ].BackColor = clickedButton.BackColor;
            _guess.Add(clickedButton.BackColor);
            _rows++; 
            if( _rows == 4)
            {
                _rows = 0;
                _columns++;
            }         
        }

        /// <summary>
        /// Méthode de la logique du jeu Mastermind au click du bouton "GO".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonValitador_Click(object sender, EventArgs e)
        {
            {
                attempts++;                  
                
                // Création des copies de la séquence secrète et de la proposition pour éviter les modifications indésirables.
                List <Color> secretCopy = new List<Color>(secretCode);
                List<Color> guessCopy = new List<Color>(_guess);

                CopysecretCode(_guess, secretCopy, guessCopy, secretCode);

                // Vérification des couleurs correctement placées.

                correctlyPlaced = CheckCorrectlyPlaced(guessCopy, secretCopy);

                // Vérificaton des couleurs mal placées.
                misplaced = CheckIncorrectlyPlaced(guessCopy, secretCopy);

                //UpdateCheckColorsLabels(correctlyPlaced, misplaced);
                // Appliquer les changements de BackColor pour les couleurs correctement placées dans _panelCheckColorsArray
                for (int i = 0; i < correctlyPlaced; i++)
                {
                    if (i < _panelCheckColorsArray.GetLength(1))
                    {
                        _panelCheckColorsArray[attempts - 1, i].BackColor = Color.White;
                    }
                }

                // Appliquer les changements de BackColor pour les couleurs mal placées
                for (int i = correctlyPlaced; i < correctlyPlaced + misplaced; i++)
                {
                    if (i < _panelCheckColorsArray.GetLength(1))
                    {
                        _panelCheckColorsArray[attempts - 1, i].BackColor = Color.Black;
                    }
                }

                // TODO Affiche les résultats de la tentative.

                // Vérification si le code a été deviné.
                if (correctlyPlaced == 4)
                {
                    MessageBox.Show("Vous avez gagné");

                    //Cacher les buttons "GO" et "UNDO".
                    deleteButton.Hide();
                    buttonValitador.Hide();

                    //Faire apparaitre les buttons "EXIT" et "RESET".
                    resetButton.Show();
                    exitButton.Show();

                    //Cacher les panels pour le jeu.
                    colorChoisepanel.Hide();
                    checkColorspanel.Hide();


                }
            }   
        }

        /// <summary>
        /// Méthode pour la supression d'une couleur choisie dans le tableau de "_panelChoiceColorsArray".  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            _rows--;
            _panelChoiceColorsArray[_columns, _rows].BackColor = Color.DarkGray;
            
            if (_rows > 5)
            {
                _rows = 0;
                _columns++;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Click(object sender, EventArgs e)
        {

            colorChoisepanel.Controls.Clear();
            checkColorspanel.Controls.Clear();

            _columns = 0;
            _rows = 0;

            correctlyPlaced = 0;
            incorrectlyPlaced = 0;
            misplaced = 0;
            

            initializeGame();
            
        }
    }
}
