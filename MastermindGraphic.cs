﻿using System;
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
        public List<Color> _guess = new List<Color>();

        //Déclaration de tableau de couleur
        Color[] TabColours = { Color.Green, Color.Yellow, Color.White, Color.Red, Color.Magenta, Color.Blue, Color.Cyan };

        public MastermindGraphic()
        {
            InitializeComponent();
            secretCode = GenerateSecretCode();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="secretCopy"></param>
        /// <param name="guessCopy"></param>
        /// <param name="secretCode"></param>
        static void CopysecretCode(string guess, List<Color> secretCopy, List<Color> guessCopy, List<Color> secretCode)
        {

            secretCopy.Clear();
            secretCopy.AddRange(secretCode);

            guessCopy.Clear();
            guessCopy.AddRange();
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
            int correctlyPlaced = 0;

            for (int i = 0; i < 4; i++)
            {
                if (guessCopy[i] == secretCopy[i])
                {
                    correctlyPlaced++;
                    // Marquage des couleurs déjà validées.
                    secretCopy[i] = Color.DarkGray;
                    guessCopy[i] = Color.DarkGray;
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
        private int CheckIncorrectlyPlaced(List<char> guessCopy, List<char> secretCopy)
        {
            int incorrectlyPlaced = 0;

            for (int i = 0; i < 4; i++)
            {
                if (guessCopy[i] != ' ')
                {
                    for (int j = 0; j < secretCopy.Count; j++)
                    {
                        if (guessCopy[i] == secretCopy[j])
                        {
                            incorrectlyPlaced++;
                            secretCopy[j] = ' '; // Marquage des couleurs déjà validées.
                            break;
                        }
                    }
                }
            }
            return incorrectlyPlaced;
        }
        /// <summary>
        /// Déclaration d'un tableau de label pour le choix des couleurs de l'utilisateur
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

                    choice.Size = new Size(panel.Width / columns, panel.Height / rows);

                    choice.Tag = //$"Bouton [{j + 1}, {i + 1}]";
                    choice.Text ="y";
                    choice.BackColor = Color.DarkGray;
                   
                    colorChoisepanel.Controls.Add(choice);

                    _panelChoiceColorsArray[i, j] = choice;

                    
                }
            }
        }

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

                    check.Size = new Size(panel.Width / columns, panel.Height / rows);

                    check.Tag = //$"Bouton [{j + 1}, {i + 1}]";
                    check.Text = "y";

                    checkColorspanel.Controls.Add(check);

                    _panelCheckColorsArray[i, j] = check;


                }
            }


        }

        void CreateBtnColours()
        {
            for (int i = 0; i < _MAXCOLORS; i++)
            {
                //Création des boutons et de leurs caractéristiques
                _tabColors[i] = new Button();
                _tabColors[i].Size = new Size(40, 40);
                _tabColors[i].BackColor = TabColours[i];
                _tabColors[i].Tag = _tabColors[i].BackColor;
                _tabColors[i].Text = "";

                //Positionnement des boutons de couleurs
                int a = _tabColors[i].Height * i + _MARGINBUTTON * i + _MARGINBUTTON * i;
                _tabColors[i].Location = new Point(a, 10);
                panelColorsbtn.Controls.Add(_tabColors[i]);

                //attache un événement au clic de chaque bouton
                _tabColors[i].Click += tabcolors_Color_Click;
            }
        }

        private void MastermindGraphic_Load(object sender, EventArgs e)
        {
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

        private void tabcolors_Color_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button) sender;
            _panelChoiceColorsArray[_columns,_rows ].BackColor = clickedButton.BackColor;
            _guess = clickedButton.BackColor;
            _rows++; 
            if( _rows == 4)
            {
                _rows = 0;
                _columns++;

            }
           
        }

        private void buttonValitador_Click(object sender, EventArgs e)
        {
            CheckCorrectlyPlaced( guessCopy, secretCopy);
               /* while (attempts <= 10 && !codeGuessed)
                {
                    Console.WriteLine();
                    Console.Write($"Tentative {attempts}: ");
                    string guess = Console.ReadLine().ToUpper();

                    // Vérification de la proposition est valide.
                    if (guess.Length != 4 || !guess.All(char.IsLetter) || guess.Any(c => "RBGYOPV".IndexOf(c) == -1))
                    {
                        Console.WriteLine();
                        Console.WriteLine("La proposition n'est pas valide. Assurez-vous qu'elle comporte 4 lettres parmi les couleurs disponibles (R, B, G, Y, O, P, V).");
                        attempts++;
                        continue;
                    }

                    int correctlyPlaced = 0;
                    int misplaced = 0;

                    // Création des copies de la séquence secrète et de la proposition pour éviter les modifications indésirables.
                    secretCopy = new List<char>(secretCode);
                    guessCopy = new List<char>(guess);

                    CopysecretCode(guess, secretCopy, guessCopy, secretCode);


                    // Vérification des couleurs correctement placées.

                    correctlyPlaced = CheckCorrectlyPlaced(guessCopy, secretCopy);


                    // Vérificaton des couleurs mal placées.
                    misplaced = CheckIncorrectlyPlaced(guessCopy, secretCopy);


                    Console.WriteLine();
                    PrintFeedback(correctlyPlaced, misplaced);// Affiche les résultats de la tentative.

                    // Vérification si le code a été deviné.
                    codeGuessed = correctlyPlaced == 4;
                    attempts++;
                }

                // Affichage du résultat final.
                Printfinalgame(codeGuessed, secretCode);


                Console.WriteLine("Voulez-vous ré-essayer ? [o/O]");
            } while (Console.ReadLine().ToUpper() == "O");*/
        }
    }
}